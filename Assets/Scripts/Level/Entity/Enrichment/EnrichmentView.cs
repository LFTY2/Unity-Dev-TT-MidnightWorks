using Level.Entity.Item;
using UI;
using UnityEngine;

namespace Level.Entity.Enrichment
{
    public sealed class EnrichmentView : EntityWithHudView
    {
        [SerializeField] private EnrichmentConfig _config;
        [SerializeField] private ItemView _itemBuyUpdateView;
        [SerializeField] private ItemView _itemCollectView;
        [SerializeField] private ItemView _itemPlaceView;
        [SerializeField] private GameObject _plantUnit;
        [SerializeField] private AudioSource _upgradeSound;
        [SerializeField] private AudioSource _upgradedSound;
        [SerializeField] private FillBarView _fillBar;
        [SerializeField] private Transform[] _inTransforms;
        [SerializeField] private Transform[] _outTransforms;

        public GameObject PlantUnit => _plantUnit;
        public EnrichmentConfig Config => _config;
        public ItemView ItemBuyUpdateView => _itemBuyUpdateView;
        public ItemView ItemCollectView => _itemCollectView;
        public ItemView ItemPlaceView => _itemPlaceView;
        public AudioSource UpgradeSound => _upgradeSound;
        public AudioSource UpgradedSound => _upgradedSound;
        public FillBarView FillBar => _fillBar;
        public Transform[] InTransforms => _inTransforms;
        public Transform[] OutTransforms => _outTransforms;
       
    }
}
