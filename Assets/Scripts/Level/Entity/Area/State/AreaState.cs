using Core.Inject;

namespace Level.Entity.Area.State
{
    public abstract class AreaState : global::Core.StateManager.State
    {
        [Inject] protected AreaController _area;
    }
}

