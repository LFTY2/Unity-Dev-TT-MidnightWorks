using Core.Inject;
using Core.StateManager;

namespace Level.Entity.CashRegister.States
{
    public abstract class CashRegisterState : State
    {
        [Inject] protected CashRegisterController _cashRegister;
    }
}

