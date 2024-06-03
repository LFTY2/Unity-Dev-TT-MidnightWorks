using System;
using Audio;
using Level.Entity.Item;
using Level.Entity.Plant.States;
using Level.Entity.Player.Player.PlayerStates;
using UnityEngine;

namespace Level.Entity.Enrichment.States
{
    public sealed class EnrichmentReadyToPurchaseState : EnrichmentState
    {
        private int _moneyPerTick;
        private AudioInstance _upgradeSound;
        private AudioInstance _upgradedSound;
        public override void Initialize()
        {
            _enrichment.View.HudView.ReadyToPuchase();
            _enrichment.View.HudView.gameObject.SetActive(true);
            _enrichment.View.PlantUnit.gameObject.SetActive(true);
            _upgradeSound = new AudioInstance(_enrichment.View.UpgradeSound, Audio.AudioType.Sound);
            _upgradedSound = new AudioInstance(_enrichment.View.UpgradedSound, Audio.AudioType.Sound);
            _audioManager.AssignAudioInstance(_upgradeSound);
            _audioManager.AssignAudioInstance(_upgradedSound);
            
            _moneyPerTick = (int)Math.Clamp(_enrichment.Model.PricePurchase / 100f, 1f, Mathf.Infinity);

            _enrichment.ItemBuyUpdate.PLAYER_ON_ITEM += PlayerOnItem;

            _gameManager.AddItem(_enrichment.ItemBuyUpdate);
        }

        public override void Dispose()
        {
            _audioManager.RemoveAudioInstance(_upgradeSound);
            _audioManager.RemoveAudioInstance(_upgradedSound);
            _enrichment.ItemBuyUpdate.PLAYER_ON_ITEM -= PlayerOnItem;

            _gameManager.RemoveItem(_enrichment.ItemBuyUpdate);

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

            _enrichment.Model.PricePurchase -= amount;
            _enrichment.Model.SetChanged();

            _gameManager.FireFlyToRemoveCash(_enrichment.View.HudView.transform.position);

            if (_enrichment.Model.PricePurchase > 0) return;
            
            _upgradedSound.Play();

            _gameManager.Model.SavePlaceIsPurchased(_enrichment.Model.ID);
            _enrichment.Model.IsPurchased = _gameManager.Model.LoadPlaceIsPurchased(_enrichment.Model.ID);
            _enrichment.Model.SetChanged();
            
            _gameManager.FireAddGameProgress(_gameManager.Player.View.transform.position, _enrichment.Model.PurchaseProgressReward);

            _gameView.CameraController.SetTarget(_enrichment.View.PlantUnit.transform);
            _gameView.CameraController.ZoomIn(true);
            
            _enrichment.View.HudView.gameObject.SetActive(false);
            
            _enrichment.InitializeItems();
            _enrichment.SwitchToState(new EnrichmentWaitingState());
            _gameManager.Player.SwitchToState(new PlayerPauseState());
        }
    }
}