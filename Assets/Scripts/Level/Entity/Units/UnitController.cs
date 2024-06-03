using Core.Inject;
using Core.StateManager;
using Level.Entity.Player.Player;
using Level.Entity.Units.States;

namespace Level.Entity.Units
{
    public sealed class UnitController
    {
        private readonly StateManager<UnitState> _stateManager;

        private UnitView _view;
        private UnitModel _model;

        public UnitView View => _view;

        public UnitController(UnitView view, Context context)
        {
            _view = view;
            var subContext = new Context(context);
            var injector = new Injector(subContext);

            subContext.Install(this);
            subContext.Install(injector);

            _stateManager = new StateManager<UnitState>();
            injector.Inject(_stateManager);

            var config = context.Get<GameConfig>();
            _model = new UnitModel(config.CustomerWalkSpeed, config.PlayerRotationSpeed);

            _view.NavMeshAgent.speed = _model.WalkSpeed;
        }

        public void SwitchToState<T>(T instance) where T : UnitState
        {
            _stateManager.SwitchToState(instance);
        }

        public void Dispose()
        {
            _stateManager.Dispose();
        }

    }
}

