using System.Collections.Generic;
using System.Linq;
using Audio;
using Core;
using Core.Inject;
using Core.Observer;
using Level;
using Level.Core;
using Level.Entity.CashRegister;
using Level.Entity.Customer;
using Level.Entity.Customer.CustomerThoughts;
using Level.Entity.Customer.States;
using Level.Entity.Item;
using Level.Entity.ProductPool;
using Level.Entity.Units;
using Level.Entity.Units.States;
using Managers;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Modules.CashRegisterModule
{
    public sealed class CashRegisterModule : Module<CashRegisterModuleView>, IObserver
    {
        [Inject] private ProductPool _productPool;
        [Inject] private Timer _timer;
        [Inject] private Context _context;
        [Inject] private GameManager _gameManager;
        [Inject] private LevelView _levelView;
        [Inject] private AudioManager _audioManager;
        [Inject] private GameConfig _gameConfig;
        
        private readonly Dictionary<ItemController, bool> _itemsMap;
        private readonly Dictionary<ItemController, UnitController> _receptionistMap;
        private List<CustomerController> _customers;
        private AudioInstance _customerServedSound;

        private CashRegisterController _cashRegister;
        private int _receptionLvl;
        private int _customersNumber;
         

        public CashRegisterModule(CashRegisterModuleView view) : base(view)
        {
            _itemsMap = new  Dictionary<ItemController, bool>();
            _receptionistMap = new Dictionary<ItemController, UnitController>();
            _customers = new List<CustomerController>();
        }

        public override void Initialize()
        {
            _cashRegister = new CashRegisterController(_view.CashRegisterView, _context);
            _gameManager.CashRegister = _cashRegister;
            _receptionLvl = _cashRegister.Model.Lvl;
            
            _customerServedSound = new AudioInstance(_view.CashRegisterView.CustomerServedSound, AudioType.Sound);
            _audioManager.AssignAudioInstance(_customerServedSound);

            UpdateReceptionistsCount();
            
            _cashRegister.Model.AddObserver(this);
            _timer.TICK += OnTick;
            _timer.ONE_SECOND_TICK += TryToSpawnCustomer;
        }

        public override void Dispose()
        {
            _cashRegister.Model.RemoveObserver(this);
            _timer.TICK -= OnTick;
            _timer.ONE_SECOND_TICK -= TryToSpawnCustomer;

            foreach (var receptionist in _receptionistMap.Values.ToList())
            {
                receptionist.Dispose();
            }
            _view.Receptionist.ReleaseAllInstances();

            foreach (var customer in _customers)
            {
                customer.Dispose();
            }
            _customers.Clear();

            foreach (var customersPool in _view.Customers)
            {
                customersPool.ReleaseAllInstances();
            }

            _audioManager.RemoveAudioInstance(_customerServedSound);
            _cashRegister.Dispose();
        }

        private void UpdateReceptionistsCount()
        {
            int receptionistCount = _cashRegister.Model.CashiersCount;

            if (receptionistCount > 0 && receptionistCount <= _cashRegister.Items.Count)
                _cashRegister.Model.ItemsToShow = receptionistCount;

            else if (receptionistCount > _cashRegister.Items.Count)
            {
                receptionistCount = _cashRegister.Items.Count;

                _cashRegister.Model.CashiersCount = receptionistCount;
                _cashRegister.Model.ItemsToShow = _cashRegister.Items.Count;
            }

            for (int i = 0; i < _cashRegister.Model.ItemsToShow; i++)
            {
                var item = _cashRegister.Items[i];
                if (!_itemsMap.ContainsKey(item))
                {
                    _itemsMap.Add(item, false);
                }
                    
            }

            for (int i = 0; i < _cashRegister.Model.CashiersCount; i++)
            {
                var item = _cashRegister.Items[i];
                if (!_receptionistMap.ContainsKey(item))
                {
                    _gameManager.RemoveItem(item);
                    CreateCashier(item, _cashRegister.Items[i].Transform.position);
                }
            }
        }

        private void OnTick()
        {
            CustomerController customer = _cashRegister.Line.GetFirstCustomer();
            foreach (var item in _itemsMap.Keys.ToList())
            {
                if (!_itemsMap[item] && customer != null)
                {
                    item.Model.Duration = _cashRegister.Model.UnitProceedTime;
                    item.Model.DurationNominal = _cashRegister.Model.UnitProceedTime;
                    item.Model.SetChanged();

                    _itemsMap[item] = true;
                    item.ITEM_FINISHED += OnItemFinished;

                    if(!_receptionistMap.ContainsKey(item))
                        _gameManager.AddItem(item);
                }
                
            }

            foreach (var item in _receptionistMap.Keys.ToList())
            {
                item.Model.Duration -= Time.deltaTime;
                item.Model.SetChanged();

                if (item.Model.Duration <= 0f)
                {
                    item.FireItemFinished();
                }
            }
        }

        private void OnItemFinished(ItemController item)
        {
            _customerServedSound.Play();
            item.ITEM_FINISHED -= OnItemFinished;
            _itemsMap[item] = false;
            
            OnCustomerServe();
        }

        private void OnCustomerServe()
        {
            CustomerController customer = _cashRegister.Line.GetFirstCustomer();
            if (customer == null) return;
            
            customer.ThoughtController.ShowThought(ThoughtType.Happy);
            customer.SwitchToState(new CustomerWalkToRemoveState(_levelView.UnitRemovePoint.transform.position));

            _cashRegister.Line.RearrangeCustomersLine();

            _cashRegister.Model.Cash += _productPool.CalculatePrizeOfNeeds(customer.CustomerNeeds);
            _cashRegister.Model.SetChanged();

            _gameManager.Model.SavePlaceCash(_cashRegister.Model.ID, _cashRegister.Model.Cash);
        }

        private void TryToSpawnCustomer()
        {
            if (_customers.Count < _gameManager.GetUnlockedProductCount() * 2 && _customers.Count < _gameConfig.CustomersMax )
            {
                CreateCustomer();
            }
        }

        private void CreateCustomer()
        {
            Vector3 start = _levelView.UnitSpawnPoint.position;
            int index = Random.Range(0, _view.Customers.Length);
            CustomerView view = _view.Customers[index].Get<CustomerView>();
            CustomerController customer = new CustomerController(view, index, _context);
            customer.View.transform.position = start;
            customer.SwitchToState(new CustomerInitializeState());
            customer.ON_REMOVE += OnCustomerRemove;
            _customers.Add(customer);
        }

        private void OnCustomerRemove(CustomerController customer)
        {
            customer.ON_REMOVE -= OnCustomerRemove;

            customer.Dispose();
            _view.Customers[customer.View.Index].Release(customer.View);
            _customers.Remove(customer);
        }

        private void CreateCashier(ItemController item, Vector3 startPosition)
        {
            UnitView view = _view.Receptionist.Get<UnitView>();
            UnitController unit = new UnitController(view, _context);
            unit.View.transform.position = startPosition;
            unit.SwitchToState(new UnitCashRegisterState());
            _receptionistMap.Add(item, unit);
        }

        public void OnObjectChanged(Observable observable)
        {
            if (_receptionLvl == _cashRegister.Model.Lvl) return;
            _receptionLvl = _cashRegister.Model.Lvl;

            UpdateReceptionistsCount();
        }
    }
}

