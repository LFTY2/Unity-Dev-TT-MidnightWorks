using System;
using Core.Inject;
using Core.StateManager;
using Level.Entity.Inventory;
using Level.Entity.Item;
using Level.Entity.Plant.States;

using Managers;
using UnityEngine;

namespace Level.Entity.Plant
{
    public sealed class PlantController : EntityController, IDisposable
    {
        private readonly PlantModel _model;
        private readonly PlantView _view;
        private readonly StateManager<PlantState> _stateManager;
        private readonly ItemController _itemBuyUpdate;
        private readonly ItemController _itemCollect;
        private readonly InventoryController _inventoryController;
        public InventoryController InventoryController => _inventoryController;
        public PlantView View => _view;
        public PlantModel Model => _model;
        public ItemController ItemCollect => _itemCollect;
        public ItemController ItemBuyUpdate => _itemBuyUpdate;

        public Vector3 InitialPosition { get; internal set; }

        public override Transform Transform => _view.transform;

        public PlantController(PlantView view, Context context)
        {
            _view = view;

            var subContext = new Context(context);
            var injector = new Injector(subContext);

            subContext.Install(this);
            subContext.InstallByType(this, typeof(PlantController));
            subContext.Install(injector);

            _stateManager = new StateManager<PlantState>();
            injector.Inject(_stateManager);

            var gameManager = context.Get<GameManager>();
            var gameConfig = context.Get<GameConfig>();
            string id = gameManager.Model.GenerateEntityID(gameManager.Model.Farm, _view.Type, view.Config.Number);
            _view.name = id;
            int lvl = gameManager.Model.LoadPlaceLvl(id);

            _model = new PlantModel(id, lvl, _view.Type, _view.Config);
            _model.IsPurchased = gameManager.Model.LoadPlaceIsPurchased(id);
            _model.Cash = gameManager.Model.LoadPlaceCash(_model.ID);

            _view.HudView.Model = _model;

            _inventoryController = new InventoryController(_view.PlantPositions, _view.Config.GrowAmount);
            _itemBuyUpdate = new ItemController(view.ItemBuyUpdateView.transform, gameConfig.BuyUpdateRadius,
                view.ItemBuyUpdateView.Type);
            _itemCollect = new ItemController(view.ItemCollectView.transform, gameConfig.PlantItemRadius,
                view.ItemCollectView.Type);

            SwitchToState(new PlantInitializeState());
        }

        public void SwitchToState<T>(T instance) where T : PlantState
        {
            _stateManager.SwitchToState(instance);
        }

        public void Dispose()
        {
            _stateManager.Dispose();
        }
    }
}
