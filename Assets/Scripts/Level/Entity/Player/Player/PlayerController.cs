using Core.Inject;
using Core.StateManager;
using Level.Entity.Player.Player.PlayerStates;
using InventoryController = Level.Entity.Inventory.InventoryController;
using Object = UnityEngine.Object;

namespace Level.Entity.Player.Player
{
    public sealed class PlayerController
    {
        private InventoryController _inventoryController;
        public readonly UnitModel Model;
        private readonly PlayerView _view;
        private readonly StateManager<PlayerState> _stateManager;
        public PlayerView View => _view;
        public InventoryController Inventory => _inventoryController;
        public PlayerController(PlayerView view, Context context)
        {
            _view = view;
            var subContext = new Context(context);
            var injector = new Injector(subContext);
            subContext.Install(this);
            subContext.InstallByType(this, typeof(PlayerController));
            subContext.Install(injector);
            _inventoryController = new InventoryController(view.InventoryPositions, 6); //TODO change with config or smth
            _stateManager = new StateManager<PlayerState>();
            injector.Inject(_stateManager);

            _inventoryController.ON_PRODUCTS_CHANGE += CheckCarry;

            var config = context.Get<GameConfig>();
            Model = new UnitModel(config.PlayerWalkSpeed, config.PlayerRotationSpeed);
        }

        public void Dispose()
        {
            _inventoryController.ON_PRODUCTS_CHANGE -= CheckCarry;
            _stateManager.Dispose();
            Object.Destroy(_view.gameObject);
        }

        public void SwitchToState<T>(T instance) where T : PlayerState
        {
            _stateManager.SwitchToState(instance);
        }

        private void CheckCarry()
        {
            _view.SetLayerWeight(_inventoryController.ItemsStored);
        }

    }
}