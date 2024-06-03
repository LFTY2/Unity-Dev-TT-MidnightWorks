using System;
using Core.Inject;
using Core.StateManager;
using Level.Entity.Area;
using Level.Entity.Inventory;
using Level.Entity.Item;
using Level.Entity.Plant;
using Level.Entity.Shelf.States;
using Managers;
using UnityEngine;

namespace Level.Entity.Shelf
{
    public sealed class ShelfController : EntityController, IDisposable
    {
        private readonly ShelfModel _model;
        private readonly ShelfView _view;
        private readonly StateManager<ShelfState> _stateManager;
        private readonly ItemController _itemPlaceProduct;
        private readonly InventoryController _inventoryController;
        public ShelfView View => _view;
        public ShelfModel Model => _model;
        public ItemController ItemPlaceProduct => _itemPlaceProduct;
        public InventoryController InventoryController => _inventoryController;
        public override Transform Transform => _view.transform;
        

        public ShelfController(ShelfView view, Context context)
        {
            _view = view;

            var subContext = new Context(context);
            var injector = new Injector(subContext);

            subContext.Install(this);
            subContext.InstallByType(this, typeof(PlantController));
            subContext.Install(injector);

            _stateManager = new StateManager<ShelfState>();
            injector.Inject(_stateManager);

            var gameManager = context.Get<GameManager>();
            var gameConfig = context.Get<GameConfig>();
            string id = gameManager.Model.GenerateEntityID(gameManager.Model.Farm, _view.Type, view.ShelfConfig.Number);
            _view.name = id;
            int lvl = gameManager.Model.LoadPlaceLvl(id);

            _model = new ShelfModel(id, lvl, _view.Type, _view.ShelfConfig);
            _model.IsPurchased = gameManager.Model.LoadPlaceIsPurchased(id);
            _model.Cash = gameManager.Model.LoadPlaceCash(_model.ID);

            _itemPlaceProduct = new ItemController(view.ItemStoreProducts.transform, gameConfig.UtilityItemRadius,
                view.ItemStoreProducts.Type);

            _inventoryController = new InventoryController(_view.InventoryPositions, _view.ShelfConfig.ItemsStored);

            SwitchToState(new ShelfInitializeState());
        }

        public void SwitchToState<T>(T instance) where T : ShelfState
        {
            _stateManager.SwitchToState(instance);
        }

        public void Dispose()
        {
            _stateManager.Dispose();
        }
        
        public bool CheckIsPurchasable(AreaController area, int progress)
        {
            return IsPurchasable(area.Model.IsPurchased, progress, _model.TargetPurchaseValue);
        }
    }
}
