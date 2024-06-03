using Level.Entity.Item;

namespace Level.Entity.Player.Player.PlayerStates
{
    public sealed class PlayerIdleState : PlayerFindEntityState
    {
        public override void Initialize()
        {
            base.Initialize();

            _player.View.NavMeshAgent.enabled = false;

            _player.View.Idle(_player.Inventory.ItemsStored);

            _timer.TICK += OnTICK;
        }

        public override void Dispose()
        {
            base.Dispose();

            _timer.TICK -= OnTICK;
        }

        internal void FindClosestUsedItem()
        {
            var item = _gameManager.FindClosestUsedItem();
            if (item == null) return;

            var type = item.Type;
            

            if (type == ItemType.CashRegisterDesk)
                _player.SwitchToState(new PlayerCashRegisterState(item));

            else if (type == ItemType.CashPile || type == ItemType.BuyUpdate || type == ItemType.Plant || type == ItemType.Enrichment)
                _player.SwitchToState(new PlayerOnItemState(item));
            
        }

        private void OnTICK()
        {
            if (!_gameView.Joystick.IsTouched) return;

            _player.SwitchToState(new PlayerWalkState());
        }

        public override void OnSecondTick()
        {
            base.OnSecondTick();

            FindClosestUsedItem();
        }
    }
}