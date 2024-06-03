using System;
using Core.Inject;
using Core.StateManager;
using Level.Entity.Enrichment.States;
using Level.Entity.Inventory;
using Level.Entity.Item;
using Level.Entity.Plant;
using Level.Entity.Product;
using Managers;
using UnityEngine;

namespace Level.Entity.Enrichment
{
    public sealed class EnrichmentController : EntityController, IDisposable
    {
        private readonly EnrichmentModel _model;
        private readonly EnrichmentView _view;
        private readonly StateManager<EnrichmentState> _stateManager;
        private readonly ItemController _itemBuyUpdate;
        private readonly ItemController _itemCollect;
        private readonly ItemController _itemPlace;
        private readonly InventoryController _inInventory;
        private readonly InventoryController _outInventory;
        private readonly GameManager _gameManager;
        public EnrichmentView View => _view;
        public EnrichmentModel Model => _model;
        public ItemController ItemCollect => _itemCollect;
        public ItemController ItemBuyUpdate => _itemBuyUpdate;
        public ItemController ItemPlace => _itemPlace;
        public InventoryController InInventoryController => _inInventory;
        public InventoryController OutInventoryController => _outInventory;

        public Vector3 InitialPosition { get; internal set; }

        public override Transform Transform => _view.transform;

        public EnrichmentController(EnrichmentView view, Context context)
        {
            _view = view;

            var subContext = new Context(context);
            var injector = new Injector(subContext);

            subContext.Install(this);
            subContext.InstallByType(this, typeof(PlantController));
            subContext.Install(injector);

            _stateManager = new StateManager<EnrichmentState>();
            injector.Inject(_stateManager);

            _gameManager = context.Get<GameManager>();
            var gameConfig = context.Get<GameConfig>();
            string id = _gameManager.Model.GenerateEntityID(_gameManager.Model.Farm, _view.Type, view.Config.Number);
            _view.name = id;
            int lvl = _gameManager.Model.LoadPlaceLvl(id);

            _model = new EnrichmentModel(id, lvl, _view.Type, _view.Config);
            _model.IsPurchased = _gameManager.Model.LoadPlaceIsPurchased(id);
            _model.Cash = _gameManager.Model.LoadPlaceCash(_model.ID);
            
            _view.HudView.Model = _model;

            _inInventory = new InventoryController(_view.InTransforms, _view.Config.InventorySize);
            _outInventory = new InventoryController(_view.OutTransforms, _view.Config.InventorySize);
            
            _itemBuyUpdate = new ItemController(view.ItemBuyUpdateView.transform, gameConfig.BuyUpdateRadius,
                view.ItemBuyUpdateView.Type);
            _itemCollect = new ItemController(view.ItemCollectView.transform, gameConfig.UtilityItemRadius,
                view.ItemCollectView.Type);
            _itemPlace = new ItemController(view.ItemPlaceView.transform, gameConfig.UtilityItemRadius,
                view.ItemCollectView.Type);
            _view.FillBar.Holder.SetActive(false);
            view.ItemCollectView.gameObject.SetActive(false);
            view.ItemPlaceView.gameObject.SetActive(false);
            SwitchToState(new EnrichmentInitializeState());
        }
        
        public void SwitchToState<T>(T instance) where T : EnrichmentState
        {
            _stateManager.SwitchToState(instance);
        }

        public void InitializeItems()
        {
            _view.ItemCollectView.gameObject.SetActive(true);
            _view.ItemPlaceView.gameObject.SetActive(true);
            
            _gameManager.AddItem(ItemPlace);
            _gameManager.AddItem(ItemCollect);
            
            _itemPlace.PLAYER_ON_ITEM += PlaceProduct;
            _itemCollect.PLAYER_ON_ITEM += CollectProduct;
        }

        private void PlaceProduct(ItemController itemController)
        {
            InventoryController playerInventory = _gameManager.Player.Inventory;
            if (playerInventory.CheckOnProductType(_view.Config.InProduct) && !_inInventory.IsFull)
            {
                ProductView product = playerInventory.GetProductByType(_view.Config.InProduct);
                playerInventory.RemoveProduct(product);
                
                _inInventory.AddProduct(product);
            }
        }

        private void CollectProduct(ItemController itemController)
        {
            InventoryController playerInventory = _gameManager.Player.Inventory;
            if (_outInventory.ItemsStored > 0 && !playerInventory.IsFull)
            {
                ProductView product = _outInventory.RemoveLastProduct();
                playerInventory.AddProduct(product);
            }
        }

        public void Dispose()
        {
            _stateManager.Dispose();
            DisposeItems();
        }

        private void DisposeItems()
        {
            _gameManager.RemoveItem(ItemPlace);
            _gameManager.RemoveItem(ItemCollect);
            
            ItemPlace.PLAYER_ON_ITEM -= CollectProduct;
            ItemCollect.PLAYER_ON_ITEM -= CollectProduct;
        }
    }
}
