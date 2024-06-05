using System;
using Audio;
using Level.Entity.Item;
using Level.Entity.Player.Player.PlayerStates;
using UnityEngine;

namespace Level.Entity.Plant.States
{
    public sealed class PlantReadyToPurchaseState : PlantState
    {
        private int _moneyPerTick;
        private AudioInstance _upgradeSound;
        private AudioInstance _upgradedSound;
        public override void Initialize()
        {
            _plant.View.HudView.gameObject.SetActive(true);
            _plant.View.HudView.ReadyToPuchase();
            
            _upgradeSound = new AudioInstance(_plant.View.UpgradeSound, Audio.AudioType.Sound);
            _upgradedSound = new AudioInstance(_plant.View.UpgradedSound, Audio.AudioType.Sound);
            _audioManager.AssignAudioInstance(_upgradeSound);
            _audioManager.AssignAudioInstance(_upgradedSound);

            _plant.View.PlantUnit.SetActive(true);
            
            _moneyPerTick = (int)Math.Clamp(_plant.Model.PricePurchase / 100f, 1f, Mathf.Infinity);

            _plant.ItemBuyUpdate.PLAYER_ON_ITEM += PlayerOnItem;

            _gameManager.AddItem(_plant.ItemBuyUpdate);
        }

        public override void Dispose()
        {
            _audioManager.RemoveAudioInstance(_upgradeSound);
            _audioManager.RemoveAudioInstance(_upgradedSound);
            _plant.ItemBuyUpdate.PLAYER_ON_ITEM -= PlayerOnItem;

            _gameManager.RemoveItem(_plant.ItemBuyUpdate);

            _moneyPerTick = 0;
        }

        void PlayerOnItem(ItemController item)
        {
            if (_gameManager.Model.Cash < _moneyPerTick) return;

            int amount = _moneyPerTick;
            
            _upgradeSound.Play();

            _gameManager.Model.Cash -= amount;
            _gameManager.Model.SetChanged();
            _gameManager.Model.Save();

            _plant.Model.PricePurchase -= amount;
            _plant.Model.SetChanged();

            _gameManager.FireFlyToRemoveCash(_plant.View.HudView.transform.position);

            if (_plant.Model.PricePurchase > 0) return;
            
            _upgradedSound.Play();

            _gameManager.Model.SavePlaceIsPurchased(_plant.Model.ID);
            _plant.Model.IsPurchased = _gameManager.Model.LoadPlaceIsPurchased(_plant.Model.ID);
            _plant.Model.SetChanged();
            _gameManager.FireAddGameProgress(_gameManager.Player.View.transform.position, _plant.Model.PurchaseProgressReward);
            _plant.View.HudView.gameObject.SetActive(false);
            _gameView.CameraController.SetTarget(_plant.View.PlantUnit.transform);
            _gameView.CameraController.ZoomIn(true);

            _plant.SwitchToState(new PlantGrowingState());
            _gameManager.Player.SwitchToState(new PlayerPauseState());
        }
    }
}