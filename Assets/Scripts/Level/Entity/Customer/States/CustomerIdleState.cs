namespace Level.Entity.Customer.States
{
    public sealed class CustomerIdleState : CustomerState
    {
        public override void Initialize()
        {
            _customer.View.NavMeshAgent.enabled = false;
            _customer.View.Idle(0);
        }

        public override void Dispose()
        {
        }
    }
}