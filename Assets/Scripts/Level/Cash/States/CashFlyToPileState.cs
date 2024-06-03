using UnityEngine;

namespace Level.Cash.States
{
    public sealed class CashFlyToPileState : CashFlyToPositionState
    {
        public CashFlyToPileState(Vector3 endPosition)
        {
            _endPosition = endPosition;
        }

        public override void OnEnd()
        {
            _cash.Idle();
        }
    }
}