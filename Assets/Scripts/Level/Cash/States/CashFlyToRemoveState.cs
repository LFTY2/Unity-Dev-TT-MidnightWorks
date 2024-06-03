using UnityEngine;

namespace Level.Cash.States
{
    public sealed class CashFlyToRemoveState : CashFlyToPositionState
    {
        public CashFlyToRemoveState(Vector3 endPosition)
        {
            _endPosition = endPosition;
        }

        public override void OnEnd()
        {
            _cash.FireRemoveCash();
        }
    }
}