using System;
using Core.Inject;
using Core.StateManager;
using Level.Entity.Customer.CustomerThoughts;
using Level.Entity.Customer.States;
using Level.Entity.Player.Player;

namespace Level.Entity.Customer
{
    public sealed class CustomerController
    {
        public Action<CustomerController> ON_REMOVE;
        public Action<CustomerController> ON_REMOVE_FROM_LINE;

        private readonly StateManager<CustomerState> _stateManager;

        private CustomerView _view;
        private UnitModel _model;
        private CustomerThoughtController _thoughtController;
        public CustomerThoughtController ThoughtController => _thoughtController;

        public CustomerView View => _view;
        public CustomerNeeds CustomerNeeds { get; set; }
        
        public CustomerController(CustomerView view, Context context)
        {
            _view = view;

            var subContext = new Context(context);
            var injector = new Injector(subContext);

            subContext.Install(this);
            subContext.Install(injector);

            _stateManager = new StateManager<CustomerState>();
            injector.Inject(_stateManager);

            var config = context.Get<GameConfig>();
            _model = new UnitModel(config.CustomerWalkSpeed, config.PlayerRotationSpeed);

            _view.NavMeshAgent.speed = _model.WalkSpeed;
            _thoughtController = new CustomerThoughtController(_view.CustomerThought);
            injector.Inject(_thoughtController);
            _thoughtController.Initialize();
        }

        public void SwitchToState<T>(T instance) where T : CustomerState
        {
            _stateManager.SwitchToState(instance);
        }

        public void Dispose()
        {
            _thoughtController.Dispose();
            _stateManager.Dispose();
        }

        public void FireUnitRemove()
        {
            ON_REMOVE?.Invoke(this);
        }

        public void FireUnitRemoveFromLine()
        {
            ON_REMOVE_FROM_LINE?.Invoke(this);
        }
        
    }
}

