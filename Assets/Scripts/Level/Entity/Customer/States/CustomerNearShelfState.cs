using Core;
using Core.Inject;
using Level.Entity.Customer.CustomerThoughts;
using Level.Entity.Plant;
using Level.Entity.Product;
using Level.Entity.Shelf;
using Managers;

namespace Level.Entity.Customer.States
{
    public sealed class CustomerNearShelfState : CustomerState
    {
        [Inject] private LevelView _levelView;
        [Inject] private ProductPool.ProductPool _productPool;
        [Inject] private GameManager _gameManager;
        private ShelfController _shelf;
        private int _waitTime;
        public override void Initialize()
        {
            _customer.View.NavMeshAgent.enabled = false;
            _customer.View.Idle(0);
            _waitTime = 10;
            _shelf = _gameManager.FindShelf(_customer.CustomerNeeds.ProductType);
            
            _shelf.InventoryController.ON_PRODUCTS_CHANGE += CheckProductsOnAvailable;
            _timer.ONE_SECOND_TICK += OnSecond;
            
            CheckProductsOnAvailable();
        }
        private void OnSecond()
        {
            _waitTime--;
            if (_waitTime <= 0)
            {
                _customer.ThoughtController.ShowThought(ThoughtType.Angry);
                _shelf.InventoryController.ON_PRODUCTS_CHANGE -= CheckProductsOnAvailable;
                _customer.SwitchToState(new CustomerWalkToRemoveState(_levelView.UnitRemovePoint.transform.position));
            }
        }
        private void CheckProductsOnAvailable()
        {
            if (_shelf.InventoryController.ItemsStored >= _customer.CustomerNeeds.Number)
            {
                GetProductsFromShelf();
            }
        }
        private void GetProductsFromShelf()
        {
            _shelf.InventoryController.ON_PRODUCTS_CHANGE -= CheckProductsOnAvailable;
            for (int i = 0; i < _customer.CustomerNeeds.Number; i++)
            {
                ProductView product = _shelf.InventoryController.RemoveLastProduct();
                _productPool.ReturnToPool(product);
            }
            _customer.ThoughtController.ShowThought(ThoughtType.Pay);
            _customer.SwitchToState(new CustomerWalkToCashRegisterState());
        }
        public override void Dispose()
        {
            
            _timer.ONE_SECOND_TICK -= OnSecond;
        }
    }
}