using System;
using Audio;
using Core.Inject;
using Level.Entity.Item;
using Managers;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Level.Entity.Area.State
{
    public sealed class AreaReadyToPurchaseState : AreaState
    {
        [Inject] private GameManager _gameManager;
        [Inject] private AudioManager _audioManager;
        private int _moneyPerTick;
        private AudioInstance _upgradeSound;
        private AudioInstance _upgradedSound;

        public override void Initialize()
        {
            _area.Model.IsLocked = false;
            _area.Model.SetChanged();
            
            _upgradeSound = new AudioInstance(_area.View.UpgradeSound, AudioType.Sound);
            _upgradedSound = new AudioInstance(_area.View.UpgradedSound, AudioType.Sound);
            _audioManager.AssignAudioInstance(_upgradeSound);
            _audioManager.AssignAudioInstance(_upgradedSound);

            _area.View.HudView.gameObject.SetActive(true);
            _area.View.HudView.ReadyToPuchase();

            _area.ItemBuyUpdate.PLAYER_ON_ITEM += PlayerOnItem;
            
            _moneyPerTick = (int)Math.Clamp(_area.Model.PricePurchase / 100f, 1f, Mathf.Infinity);

            _gameManager.AddItem(_area.ItemBuyUpdate);
        }

        public override void Dispose()
        {
            _area.ItemBuyUpdate.PLAYER_ON_ITEM -= PlayerOnItem;
            _gameManager.RemoveItem(_area.ItemBuyUpdate);
            _moneyPerTick = 0;
            _audioManager.RemoveAudioInstance(_upgradeSound);
            _audioManager.RemoveAudioInstance(_upgradedSound);
        }

        void PlayerOnItem(ItemController item)
        {
            if (_gameManager.Model.Cash < _moneyPerTick) return;
            
            _upgradeSound.Play();

            int amount = _moneyPerTick;

            _gameManager.Model.Cash -= amount;
            _gameManager.Model.SetChanged();
            _gameManager.Model.Save();

            _area.Model.PricePurchase -= amount;
            _area.Model.SetChanged();

            _gameManager.FireFlyToRemoveCash(_area.View.HudView.transform.position);

            if (_area.Model.PricePurchase > 0) return;

            _upgradedSound.Play();
            _gameManager.Model.SavePlaceIsPurchased(_area.Model.ID);
            _area.Model.IsPurchased = _gameManager.Model.LoadPlaceIsPurchased(_area.Model.ID);
            _area.Model.SetChanged();

            _gameManager.FireAreaPurchased(_area);

            _area.SwitchToState(new AreaPurchasedState());
        }
    }
}

