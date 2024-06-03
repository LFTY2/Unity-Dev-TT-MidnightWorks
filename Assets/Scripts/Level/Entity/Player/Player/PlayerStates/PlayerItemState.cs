
using Core;
using Core.Inject;
using Level.Entity.Item;
using Managers;
using UI;
using UnityEngine;

namespace Level.Entity.Player.Player.PlayerStates
{
    public class PlayerItemState : PlayerState
    {
        [Inject] protected Timer _timer;
        [Inject] protected GameView _gameView;
        [Inject] protected GameManager _gameManager;

        protected ItemController _item;

        public PlayerItemState(ItemController item)
        {
            _item = item;
        }

        public override void Initialize()
        {
            _gameManager.RemoveItem(_item);

            _timer.TICK += OnTick;
        }

        public override void Dispose()
        {
            PutItemBack();

            _timer.TICK -= OnTick;
        }

        public virtual void PutItemBack()
        {
            _gameManager.AddItem(_item);
        }

        private void OnTick()
        {
            if (_gameView.Joystick.IsTouched)
            {
                _player.SwitchToState(new PlayerWalkState());
            }

            PlayerOnItem();
        }

        public virtual void PlayerOnItem()
        {
            _item.Model.Duration -= Time.deltaTime;
            _item.Model.SetChanged();

            if (_item.Model.Duration > 0f) return;

            OnItemFinished();
        }

        public virtual void OnItemFinished()
        {
            _item.FireItemFinished();
            _player.SwitchToState(new PlayerIdleState());
        }
    }
}

