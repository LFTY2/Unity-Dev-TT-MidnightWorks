using System.Collections.Generic;
using Core.Inject;
using Core.StateManager;
using Level.Entity.CashRegister.States;
using Level.Entity.Item;
using Managers;
using UnityEngine;

namespace Level.Entity.CashRegister
{
    public sealed class CashRegisterController : EntityController
    {
        private readonly CashRegisterModel _model;
        private readonly CashRegisterView _view;
        private readonly StateManager<CashRegisterState> _stateManager;
        private readonly List<ItemController> _items;
        private readonly ItemController _itemCashPile;
        private readonly ItemController _itemBuyUpdate;
        private readonly LineController _line;

        public CashRegisterView View => _view;
        public CashRegisterModel Model => _model;
        public List<ItemController> Items => _items;
        public ItemController ItemCashPile => _itemCashPile;
        public ItemController ItemBuyUpdate => _itemBuyUpdate;
        public LineController Line => _line;

        public override Transform Transform => _view.transform;

        public CashRegisterController(CashRegisterView view, Context context)
        {
            _view = view;

            var subContext = new Context(context);
            var injector = new Injector(subContext);

            subContext.Install(this);
            subContext.Install(injector);

            _stateManager = new StateManager<CashRegisterState>();
            injector.Inject(_stateManager);

            var gameManager = context.Get<GameManager>();
            var gameConfig = context.Get<GameConfig>();

            string id = gameManager.Model.GenerateEntityID(gameManager.Model.Farm, _view.Type, 0);
            int lvl = gameManager.Model.LoadPlaceLvl(id);

            _model = new CashRegisterModel(id, lvl, _view.Type, _view.Config);
            _model.IsPurchased = true;
            _model.Cash = gameManager.Model.LoadPlaceCash(_model.ID);
            
            _view.HudView.Model = _model;

            _items = new List<ItemController>();
            foreach (var itemView in _view.Items)
            {
                var item = new ItemCashRegisterController(itemView.transform, gameConfig.CashRegisterItemRadius, itemView.Type, itemView);
                _items.Add(item);
            }

            _itemCashPile = new ItemController(view.ItemCashPileView.transform, gameConfig.CashPileRadius, view.ItemCashPileView.Type);
            _itemBuyUpdate = new ItemController(view.ItemBuyUpdateView.transform, gameConfig.BuyUpdateRadius, view.ItemBuyUpdateView.Type);

            _line = new LineController(_view.Line);

            SwitchToState(new CashRegisterIdleState());
        }

        public void SwitchToState<T>(T instance) where T : CashRegisterState
        {
            _stateManager.SwitchToState(instance);
        }

        public void Dispose()
        {
            _stateManager.Dispose();
        }
    }
}

