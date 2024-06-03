using Level.Entity.Item;

namespace Level.Entity.Player.Player.PlayerStates
{
    public sealed class PlayerOnItemState : PlayerItemState
    {
        public PlayerOnItemState(ItemController item) : base(item)
        {
            _item = item;
        }

        public override void Initialize()
        {
            _player.View.Idle(_player.Inventory.ItemsStored);

            base.Initialize();
        }

        public override void PlayerOnItem()
        {
            _item.FirePlayerOnItem();
        }

        public override void OnItemFinished()
        {
        }
    }
}

