using Level.Entity.Item;
using UnityEngine;

namespace Level.Entity.Area
{
    public sealed class AreaView : EntityWithHudView
    {
        [SerializeField] private ItemView _itemBuyUpdateView;
        [SerializeField] private GameObject _floorsHolder;
        [SerializeField] private GameObject _hidingWallsHolder;
        [SerializeField] private GameObject _permanentWallsHolder;
        [SerializeField] private AreaConfig _config;
        [SerializeField] private AudioSource _upgradeSound;
        [SerializeField] private AudioSource _upgradedSound;

        public ItemView ItemBuyUpdateView => _itemBuyUpdateView;
        public AreaConfig Config => _config;
     
        public AudioSource UpgradeSound => _upgradeSound;
        public AudioSource UpgradedSound => _upgradedSound;
        
    }
}
