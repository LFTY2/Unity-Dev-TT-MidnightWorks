using Core.Inject;
using Level.Entity.Plant.States;

namespace Level.Entity.Shelf.States
{
    public sealed class ShelfInitializeState : ShelfState
    {
        [Inject] private ProductPool.ProductPool _productPool;
        public override void Initialize()
        {
            _shelf.View.ItemStoreProducts.gameObject.SetActive(false);
            _shelf.View.ProductSprite.sprite = _productPool.GetProductSprite(_shelf.View.ShelfConfig.ProductType);
            var area = _gameManager.FindArea(_shelf.Model.Area);
            if (_shelf.CheckIsPurchasable(area, _gameManager.Model.LoadProgress()))
            {
                _shelf.SwitchToState(new ShelfUnlockedState());
            }
            else
            {
                _shelf.SwitchToState(new ShelfHiddenState());
            }
        }

        public override void Dispose()
        {
        }
    }
}