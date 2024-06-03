using Core.Inject;
using Level.Entity.CashRegister;
using Managers;
using UnityEngine;

namespace Level.Entity.Customer.States
{
    public sealed class CustomerWalkToCashRegisterState : CustomerWalkState
    {
        [Inject] private GameManager _gameManager;

        private CashRegisterController _cashRegister;
        private static Vector3 _position;

        public CustomerWalkToCashRegisterState() : base(_position)
        {
        }

        public override void Initialize()
        {
            _cashRegister = _gameManager.CashRegister;
            _endPosition = _cashRegister.Line.GetAvailablePlace().position;

            base.Initialize();
            
        }

        public override void OnReachDistance()
        {
            Transform place = _cashRegister.Line.GetAvailablePlace();
            _cashRegister.Line.PlaceUnitMap[place] = _customer;
            _customer.SwitchToState(new CustomerWalkState(place.transform.position));
        }
    }
}