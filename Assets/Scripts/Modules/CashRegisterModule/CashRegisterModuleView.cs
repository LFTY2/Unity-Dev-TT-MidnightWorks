using Level.Entity.CashRegister;
using UI.Core;
using UnityEngine;

namespace Modules.CashRegisterModule
{
    public sealed class CashRegisterModuleView : MonoBehaviour
    {
        [SerializeField] private CashRegisterView _cashRegisterView;

        public ComponentPoolFactory[] Customers;
        public ComponentPoolFactory Receptionist;

        public CashRegisterView CashRegisterView => _cashRegisterView;
    }
}


