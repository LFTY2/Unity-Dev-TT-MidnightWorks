using Core.Inject;
using Level.Entity.Item;
using Level.Entity.Player.Player.PlayerStates;
using Managers;

namespace Level.Entity.Area.State
{
    public sealed class AreaWaitForLevelState : AreaState
    {
        [Inject] private GameManager _gameManager;

        private AreaController _prevArea;

        public override void Initialize()
        {
            int prevAreaNumber = _area.Number - 1;
            if (prevAreaNumber > 1)
                _prevArea = _gameManager.FindArea(prevAreaNumber);

            _area.Model.IsLocked = true;
            _area.Model.SetChanged();

            _area.View.HudView.Locked();

            OnLvlChanged(_gameManager.Model.LoadLvl());

            _gameManager.AddItem(_area.ItemBuyUpdate);

            _area.ItemBuyUpdate.PLAYER_ON_ITEM += PlayerOnItem;
            _gameManager.LEVEL_CHANGED += OnLvlChanged;
            _gameManager.AREA_PURCHASED += OnAreaPurchased;
        }

        public override void Dispose()
        {
            _gameManager.RemoveItem(_area.ItemBuyUpdate);

            _area.ItemBuyUpdate.PLAYER_ON_ITEM -= PlayerOnItem;
            _gameManager.LEVEL_CHANGED -= OnLvlChanged;
            _gameManager.AREA_PURCHASED -= OnAreaPurchased;
        }

        private void OnAreaPurchased(AreaController area)
        {
            if (_prevArea == null) return;
            if (area.Number != _prevArea.Number) return;

            OnLvlChanged(_gameManager.Model.LoadLvl());
        }

        private void OnLvlChanged(int lvl)
        {
            if (_area.IsPurchasable(lvl, _area.Model.TargetPurchaseValue))
                _area.SwitchToState(new AreaReadyToPurchaseState());
        }

        private void PlayerOnItem(ItemController item)
        {
            _gameManager.FireNotificationNeedLvl(item.Transform.position, _area.Model.TargetPurchaseValue);
            _gameManager.Player.SwitchToState(new PlayerPauseState());
            _gameManager.AddItem(item);
        }
    }
}

