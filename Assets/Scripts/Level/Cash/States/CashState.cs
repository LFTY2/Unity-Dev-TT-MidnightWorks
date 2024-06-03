using Core.Inject;
using Core.StateManager;

namespace Level.Cash.States
{
    public abstract class CashState : State
    {
        [Inject] protected CashController _cash;
    }
}