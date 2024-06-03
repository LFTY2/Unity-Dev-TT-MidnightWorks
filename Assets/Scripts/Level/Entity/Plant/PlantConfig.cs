using System;
using Level.Entity.Product;
using UnityEngine;

namespace Level.Entity.Plant
{
    [Serializable]
    [CreateAssetMenu(menuName = "config/plantconfig")]
    public sealed class PlantConfig : EntityConfig
    {
        [SerializeField] private ProductType _productType;
        [SerializeField] private int _pricePurchase;
        [SerializeField] private int _targetPurchaseProgress;
        [SerializeField] private int _growAmount = 3;
        [SerializeField] private float _growTime = 10f;
        [SerializeField] private int _progressReward = 2;
        public ProductType ProductType => _productType;
        public int GrowAmount => _growAmount;
        public int PricePurchase => _pricePurchase;
        public int TargetPurchaseProgress => _targetPurchaseProgress;
        public int ProgressReward => _progressReward;
        public float GrowTime => _growTime;
    }

    
}