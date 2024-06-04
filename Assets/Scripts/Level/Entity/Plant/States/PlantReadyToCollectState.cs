using Core.Inject;
using Level.Entity.Item;
using Level.Entity.Product;
using UnityEngine;

namespace Level.Entity.Plant.States
{
    public sealed class PlantReadyToCollectState : PlantState
    {
        [Inject] private ProductPool.ProductPool _productPool;
        public override void Initialize()
        {
            _plant.View.PlantUnit.gameObject.SetActive(true);
            for (int i = 0; i < _plant.View.Config.GrowAmount; i++)
            {
                ProductView productView = _productPool.GetProduct(_plant.View.Config.ProductType);
                _plant.InventoryController.AddProduct(productView);
            }
            _plant.ItemCollect.PLAYER_ON_ITEM += Collect;
            _plant.View.ItemCollectView.gameObject.SetActive(true);
            _gameManager.AddItem(_plant.ItemCollect);
        }

        private void Collect(ItemController itemController)
        {
            if (_plant.InventoryController.ItemsStored == 0 || _gameManager.Player.Inventory.IsFull) return;

            ProductView productView = _plant.InventoryController.RemoveLastProduct();
            _gameManager.Player.Inventory.AddProduct(productView);
            if (_plant.InventoryController.ItemsStored == 0)
            {
                _plant.SwitchToState(new PlantGrowingState());
            }
        }

        public override void Dispose()
        {
            _plant.ItemCollect.PLAYER_ON_ITEM -= Collect;
            _plant.View.ItemCollectView.gameObject.SetActive(false);
            _gameManager.RemoveItem(_plant.ItemCollect);
        }
    }
}