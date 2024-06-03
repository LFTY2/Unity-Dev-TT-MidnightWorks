using UnityEngine;

namespace Level.Entity.Customer.States
{
    public sealed class CustomerWalkToRemoveState : CustomerWalkState
    {
        public CustomerWalkToRemoveState(Vector3 position) : base(position)
        {
            _endPosition = position;
        }
        
        public override void OnReachDistance()
        {
            _customer.FireUnitRemove();
        }
    }
}