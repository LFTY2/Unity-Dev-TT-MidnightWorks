using Level.Cash;
using Level.Entity.Item;
using UnityEngine;

namespace Level.Entity.CashRegister
{
    public sealed class CashRegisterView : EntityWithHudView
    {
        [SerializeField] private ItemView _itemBuyUpdateView;
        [SerializeField] private RouteView _line;
        [SerializeField] private CashRegisterConfig _config;
        [SerializeField] private ItemFillBarView[] _items;
        [SerializeField] private CashPileView _cashPileView;
        [SerializeField] private ItemView _itemCashPileView;
        [SerializeField] private AudioSource _customerServedSound;
        [SerializeField] private AudioSource _upgradeSound;
        [SerializeField] private AudioSource _upgradedSound;
        public ItemView ItemBuyUpdateView => _itemBuyUpdateView;
        public ItemFillBarView[] Items => _items;
        public RouteView Line => _line;
        public CashRegisterConfig Config => _config;
        public CashPileView CashPileView => _cashPileView;
        public ItemView ItemCashPileView => _itemCashPileView;
        public AudioSource CustomerServedSound => _customerServedSound;
        public AudioSource UpgradeSound => _upgradeSound;
        public AudioSource UpgradedSound => _upgradedSound;
    }
}