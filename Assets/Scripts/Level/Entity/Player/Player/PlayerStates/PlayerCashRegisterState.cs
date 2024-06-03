using Audio;
using Level.Entity.Item;

namespace Level.Entity.Player.Player.PlayerStates
{
    public sealed class PlayerCashRegisterState : PlayerItemState
    {

        public PlayerCashRegisterState(ItemController item) : base(item)
        {
            _item = item;
        }

        public override void Initialize()
        {
            _player.View.CashRegister();

            base.Initialize();
        }
    }
}

