using Core.Inject;
using Level.Entity.Customer.CustomerThoughts;
using Managers;

namespace Level.Entity.Customer.States
{
    public sealed class CustomerInitializeState : CustomerState
    {
        [Inject] private ProductPool.ProductPool _productPool;
        [Inject] private GameManager _gameManager;
        
        public override void Initialize()
        {
            _customer.CustomerNeeds = _gameManager.GenerateCustomerNeeds();
            _customer.ThoughtController.ShowThought(ThoughtType.Needs);
            _customer.ThoughtController.SetNeeds(_customer.CustomerNeeds);
            _customer.SwitchToState(new CustomerWalkToShelfState());
            
        }

        public override void Dispose()
        {
        }
    }
}