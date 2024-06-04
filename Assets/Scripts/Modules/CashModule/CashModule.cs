using System.Collections.Generic;
using Audio;
using Core.Inject;
using Level.Cash;
using Level.Core;
using Level.Entity;
using Level.Entity.Item;
using Managers;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Modules.CashModule
{
    public sealed class CashModule : Module<CashModuleView>
    {
        private const float _heightAbovePile = 3f;
        private const float _heightAbovePlayer = 1.5f;
        private const float _cashFlyToRemoveRate = 0.1f;

        [Inject] private GameManager _gameManager;
        [Inject] private Context _context;
        [Inject] private AudioManager _audioManager;

        private readonly Dictionary<CashPileView, CashPileController> _cashPilesMap;
        private readonly Dictionary<ItemController, CashPileView> _itemsMap;
        private List<CashController> _tempCashes;

        private float _cashFlyToRemoveTimer;
        private AudioInstance _audioInstanceCash;

        public CashModule(CashModuleView view) : base(view)
        {
            _cashPilesMap = new Dictionary<CashPileView, CashPileController>();
            _itemsMap = new Dictionary<ItemController, CashPileView>();
            _tempCashes = new List<CashController>();
            _view.Initialize(GameConstants.CashPrefab);
        }

        public override void Initialize()
        {
            _view.OnInitialized += AddPile;
            _audioInstanceCash = new AudioInstance(_view.CashCollectSound, AudioType.Sound);
            _audioManager.AssignAudioInstance(_audioInstanceCash);
        }

        private void AddPile()
        {
            _view.OnInitialized -= AddPile;
            AddCashPile(_gameManager.CashRegister.View.CashPileView, _gameManager.CashRegister.ItemCashPile, _gameManager.CashRegister.Model);
            _gameManager.FLY_TO_REMOVE_CASH += CashFlyToRemove;
        }

        void AddCashPile(CashPileView view, ItemController itemCashPile, EntityModel model)
        {
            view.CASH_FLY_TO_PILE += CashFlyToPile;
            view.CASH_FLY_TO_PLAYER += CashFlyToPlayer;

            var pile = new CashPileController(view, model);

            _cashPilesMap[view] = pile;
            _itemsMap[itemCashPile] = view;

            itemCashPile.PLAYER_ON_ITEM += PlayerOnItem;

            _gameManager.AddItem(itemCashPile);
        }

        public override void Dispose()
        {
            _audioManager.RemoveAudioInstance(_audioInstanceCash);
            _gameManager.FLY_TO_REMOVE_CASH -= CashFlyToRemove;

            foreach (var cashPile in _cashPilesMap.Values)
            {
                cashPile.View.CASH_FLY_TO_PILE -= CashFlyToPile;
                cashPile.View.CASH_FLY_TO_PLAYER -= CashFlyToPlayer;

                foreach (var cash in cashPile.View.Cashes)
                {
                    cash.REMOVE_CASH -= OnRemoveCash;
                    cash.Dispose();
                }
                cashPile.View.Cashes.Clear();
            }

            foreach (var cash in _tempCashes)
            {
                cash.REMOVE_CASH -= OnRemoveCash;
                cash.Dispose();
            }
            _tempCashes.Clear();

            _view.ReleaseAllInstances();

            foreach (var item in _itemsMap.Keys)
            {
                item.PLAYER_ON_ITEM -= PlayerOnItem;
            }
        }

        CashController Cash(Vector3 position)
        {
            var cashView = _view.Get<CashView>();
            var cash = new CashController(cashView, position, _context);
            return cash;
        }

        private void CashFlyToPile(CashPileView cashPileView, Vector3 endPosition)
        {
            
            CashController cash = Cash(cashPileView.transform.position + (Vector3.up * _heightAbovePile));
            cash.FlyToPile(endPosition);
            cashPileView.Cashes.Add(cash);
            cash.REMOVE_CASH += OnRemoveCash;
        }

        private void CashFlyToPlayer(CashPileView cashPileView, int index)
        {
            _audioInstanceCash.Play();
            
            var cash = cashPileView.Cashes[index];
            cash.FlyToPlayer();
            cashPileView.Cashes.Remove(cash);
            _tempCashes.Add(cash);
        }

        private void CashFlyToRemove(Vector3 endPosition)
        {
            _cashFlyToRemoveTimer += Time.deltaTime;
            if (_cashFlyToRemoveTimer < _cashFlyToRemoveRate) return;

            _cashFlyToRemoveTimer = 0f;

            CashController cash = Cash(_gameManager.Player.View.transform.position + (Vector3.up * _heightAbovePlayer));
            cash.FlyToRemove(endPosition);
            cash.REMOVE_CASH += OnRemoveCash;
        }

        private void OnRemoveCash(CashController cash)
        {
            cash.REMOVE_CASH -= OnRemoveCash;
            _view.Release(cash.View);
            cash.Dispose();
            _tempCashes.Remove(cash);
        }

        private void PlayerOnItem(ItemController item)
        {
            var cashPileView = _itemsMap[item];
            var cashPile = _cashPilesMap[cashPileView];

            if (cashPile.Model.Cash <= 0) return;

            int amount = 1;
            cashPile.Model.Cash -= amount;
            _gameManager.Model.SavePlaceCash(cashPile.Model.ID, cashPile.Model.Cash);
            cashPile.Model.SetChanged();

            _gameManager.Model.Cash += amount;
            _gameManager.Model.Save();
            _gameManager.Model.SetChanged();
        }
    }
}