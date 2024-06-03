using Core;
using Core.Inject;
using Managers;
using UnityEngine;

namespace Level.Entity.Customer.States
{
    public class CustomerWalkState : CustomerState
    {
        protected Vector3 _endPosition;
        
        public CustomerWalkState(Vector3 position)
        {
            _endPosition = position;
        }

        public override void Initialize()
        {
            _customer.View.Walk(0);

            _customer.View.NavMeshAgent.enabled = true;
            _customer.View.NavMeshAgent.SetDestination(_endPosition);
            
            _timer.TICK += OnTick;
        }

        public override void Dispose()
        {
            _timer.TICK -= OnTick;
        }

        private void OnTick()
        {
            if (Vector3.Distance(_customer.View.transform.position, _endPosition) > 0.05f) return;

            OnReachDistance();
        }

        public virtual void OnReachDistance()
        {
            _customer.SwitchToState(new CustomerIdleState());
        }
    }
}