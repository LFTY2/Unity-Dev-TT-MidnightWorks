using Core;
using Core.Inject;
using Core.StateManager;

namespace Level.Entity.Customer.States
{
    public abstract class CustomerState : State
    {
        [Inject] protected CustomerController _customer;
        [Inject] protected Timer _timer;
    }
}