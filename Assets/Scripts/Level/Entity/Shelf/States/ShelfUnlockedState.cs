using Audio;
using Core.Inject;
using Level.Entity.Item;
using Level.Entity.Product;

namespace Level.Entity.Shelf.States
{
    public sealed class ShelfUnlockedState : ShelfState
    {
        [Inject] private AudioManager _audioManager;
        private AudioInstance _placeSound;
        public override void Initialize()
        {
            _shelf.View.ItemStoreProducts.gameObject.SetActive(true);
            _shelf.View.ShelfGameObject.gameObject.SetActive(true);
            _shelf.ItemPlaceProduct.PLAYER_ON_ITEM += PlaceItem;
            _gameManager.AddItem(_shelf.ItemPlaceProduct);
           
            _placeSound = new AudioInstance(_shelf.View.PlaceSound, Audio.AudioType.Sound);
            _audioManager.AssignAudioInstance(_placeSound);
        }

        private void PlaceItem(ItemController item)
        {
            if (_gameManager.Player.Inventory.CheckOnProductType(_shelf.View.ShelfConfig.ProductType) && !_shelf.InventoryController.IsFull)
            {
                ProductView productView =
                    _gameManager.Player.Inventory.GetProductByType(_shelf.View.ShelfConfig.ProductType);
                _gameManager.Player.Inventory.RemoveProduct(productView);
                _shelf.InventoryController.AddProduct(productView);
                _placeSound.Play();
            }
        }

        public override void Dispose()
        {
            _shelf.ItemPlaceProduct.PLAYER_ON_ITEM -= PlaceItem;
            _gameManager.RemoveItem(_shelf.ItemPlaceProduct);
            _audioManager.RemoveAudioInstance(_placeSound);
        }
    }
}