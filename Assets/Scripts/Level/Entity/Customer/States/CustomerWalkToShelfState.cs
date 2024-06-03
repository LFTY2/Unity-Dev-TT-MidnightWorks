using Core.Inject;
using Level.Entity.Plant;
using Level.Entity.Shelf;
using Managers;
using UnityEngine;

namespace Level.Entity.Customer.States
{
    public sealed class CustomerWalkToShelfState : CustomerWalkState
    {
        [Inject] private GameManager _gameManager;

        private ShelfController _shelf;
        private static Vector3 position;

        public CustomerWalkToShelfState() : base(position)
        {
        }

        public override void Initialize()
        {
            _shelf = _gameManager.FindShelf(_customer.CustomerNeeds.ProductType);
            float radius = _shelf.View.ShelfConfig.CustomerRadius; 
            float randomAngle = Random.Range(1f, 2f) * Mathf.PI;
            float offsetX = Mathf.Cos(randomAngle) * radius;
            float offsetZ = Mathf.Sin(randomAngle) * radius;
            Vector3 shelfPosition = _shelf.Transform.position;
            Vector3 randomPosition = new Vector3(shelfPosition.x + offsetX, shelfPosition.y, shelfPosition.z + offsetZ);
            _endPosition = randomPosition;

            base.Initialize();
        }

        public override void OnReachDistance()
        {
            _customer.SwitchToState(new CustomerNearShelfState());
        }
    }
}