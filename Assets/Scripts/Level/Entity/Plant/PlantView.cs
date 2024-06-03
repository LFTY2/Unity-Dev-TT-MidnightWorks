using Level.Entity.Item;
using UI;
using UnityEngine;

namespace Level.Entity.Plant
{
    public sealed class PlantView : EntityWithHudView
    {
        [SerializeField] private PlantConfig _config;
        [SerializeField] private ItemView _itemBuyUpdateView;
        [SerializeField] private GameObject _plantUnit;
        [SerializeField] private AudioSource _upgradeSound;
        [SerializeField] private AudioSource _upgradedSound;
        [SerializeField] private ItemView itemCollectView;
        [SerializeField] private FillBarView _fillBar;
        [SerializeField] private Transform _growTransform;
        [SerializeField] private Transform[] _plantPositions;

        public Transform[] PlantPositions => _plantPositions;
        public GameObject PlantUnit => _plantUnit;
        public PlantConfig Config => _config;
        public ItemView ItemBuyUpdateView => _itemBuyUpdateView;
        public AudioSource UpgradeSound => _upgradeSound;
        public AudioSource UpgradedSound => _upgradedSound;
        public FillBarView FillBar => _fillBar;
        public ItemView ItemCollectView => itemCollectView;
        public Transform GrowTransform => _growTransform;
    }
}
