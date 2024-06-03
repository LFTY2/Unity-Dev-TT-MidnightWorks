using UnityEngine;

namespace Level.Entity.Units.States
{
    public sealed class UnitCashRegisterState : UnitState
    {
        public override void Initialize()
        {
            _unit.View.NavMeshAgent.enabled = false;
            _unit.View.CashRegister();
            _unit.View.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        public override void Dispose()
        {
        }
    }
}