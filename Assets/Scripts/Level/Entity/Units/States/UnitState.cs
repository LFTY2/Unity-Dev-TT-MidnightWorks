using Core.Inject;
using Core.StateManager;

namespace Level.Entity.Units.States
{
    public abstract class UnitState : State
    {
        [Inject] protected UnitController _unit;
    }
}