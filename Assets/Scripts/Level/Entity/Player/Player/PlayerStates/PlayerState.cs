using Core.Inject;
using Core.StateManager;

namespace Level.Entity.Player.Player.PlayerStates
{
    public abstract class PlayerState : State
    {
        [Inject] protected PlayerController _player;
    }
}