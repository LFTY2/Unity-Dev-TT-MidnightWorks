using System;
using Audio;
using Core.Inject;
using Level.Entity.Item;
using Level.Entity.Player.Player.PlayerStates;
using Managers;
using UI;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Level.Entity.CashRegister.States
{
    public sealed class CashRegisterIdleState : CashRegisterState
    {
        [Inject] private GameManager _gameManager;
        [Inject] private GameView _gameView;
        [Inject] private AudioManager _audioManager;
        private AudioInstance _upgradeSound;
        private AudioInstance _upgradedSound;
        
        private int _moneyPerTick;

        public override void Initialize()
        {
            CheckIsUpdatable(_gameManager.Model.LoadProgress());
            _moneyPerTick = (int)Math.Clamp(_cashRegister.Model.PriceNextLvl / 100f, 1f, Mathf.Infinity);
            
            _upgradeSound = new AudioInstance(_cashRegister.View.UpgradeSound, AudioType.Sound);
            _upgradedSound = new AudioInstance(_cashRegister.View.UpgradedSound, AudioType.Sound);
            _audioManager.AssignAudioInstance(_upgradeSound);
            _audioManager.AssignAudioInstance(_upgradedSound);

            _cashRegister.ItemBuyUpdate.PLAYER_ON_ITEM += PlayerOnItem;
            _gameManager.PROGRESS_CHANGED += CheckIsUpdatable;
        }

        public override void Dispose()
        {
            _cashRegister.ItemBuyUpdate.PLAYER_ON_ITEM -= PlayerOnItem;
            _gameManager.PROGRESS_CHANGED -= CheckIsUpdatable;
            _moneyPerTick = 0;
        }

        private void CheckIsUpdatable(int progress)
        {
            bool isUpdatable = _cashRegister.IsUpdatable(_cashRegister.Model.IsMaxed, progress, _cashRegister.Model.TargetUpdateProgress);
            _cashRegister.View.HudView.gameObject.SetActive(isUpdatable);

            if (isUpdatable)
            {
                _gameManager.AddItem(_cashRegister.ItemBuyUpdate);
                _cashRegister.View.HudView.ReadyToUpdate();
            }
            else _gameManager.RemoveItem(_cashRegister.ItemBuyUpdate);
        }

        void PlayerOnItem(ItemController item)
        {
            if (_gameManager.Model.Cash < _moneyPerTick || !_cashRegister.IsUpdatable(_cashRegister.Model.IsMaxed, _gameManager.Model.LoadProgress(), _cashRegister.Model.TargetUpdateProgress)) return;

            _upgradeSound.Play();
            
            int amount = _moneyPerTick;

            _gameManager.Model.Cash -= amount;
            _gameManager.Model.Save();
            _gameManager.Model.SetChanged();

            _cashRegister.Model.PriceNextLvl -= amount;

            _gameManager.FireFlyToRemoveCash(_cashRegister.View.HudView.transform.position);

            if (_cashRegister.Model.PriceNextLvl <= 0)
            {
                _upgradedSound.Play();
                
                _cashRegister.Model.Lvl++;
                _gameManager.Model.SavePlaceLvl(_cashRegister.Model.ID, _cashRegister.Model.Lvl);
                _cashRegister.Model.UpdateModel();

                CheckIsUpdatable(_gameManager.Model.LoadProgress());

                _gameView.CameraController.SetTarget(_cashRegister.View.transform);
                _gameView.CameraController.ZoomIn(true);

                _gameManager.Player.SwitchToState(new PlayerPauseState());
            }

            _cashRegister.Model.SetChanged();
        }
    }
}


