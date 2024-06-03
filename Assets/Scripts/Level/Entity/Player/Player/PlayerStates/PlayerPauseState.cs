using Core;
using Core.Inject;
using Managers;
using UI;

namespace Level.Entity.Player.Player.PlayerStates
{
    public sealed class PlayerPauseState : PlayerState
    {
        [Inject] private GameView _gameView;
        [Inject] private Timer _timer;
        [Inject] private GameManager _gameManager;

        public override void Initialize()
        {
            _player.View.Idle(_player.Inventory.ItemsStored);

            _timer.TICK += OnTICK;
        }

        public override void Dispose()
        {
            _timer.TICK -= OnTICK;
        }

        private void OnTICK()
        {
            if (!_gameView.Joystick.IsTouched) return;

            _player.SwitchToState(new PlayerWalkState());
        }
    }
}

