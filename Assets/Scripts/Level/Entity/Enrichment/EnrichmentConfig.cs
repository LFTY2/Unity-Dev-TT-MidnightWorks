using System;
using Level.Entity.Product;
using UnityEngine;

namespace Level.Entity.Enrichment
{
    [Serializable]
    [CreateAssetMenu(menuName = "config/enrichmentconfig")]
    public sealed class EnrichmentConfig : EntityConfig
    {
        [SerializeField] private ProductType _inProduct;
        [SerializeField] private ProductType _outProduct;
        [SerializeField] private int _pricePurchase;
        [SerializeField] private int _targetPurchaseProgress;
        [SerializeField] private int _progressReward = 3;
        [SerializeField] private float _makeTime = 5f;
        [SerializeField] private int _inventorySize = 6;
        
        public ProductType InProduct => _inProduct;
        public ProductType OutProduct => _outProduct;
        public int PricePurchase => _pricePurchase;
        public int TargetPurchaseProgress => _targetPurchaseProgress;
        public int ProgressReward => _progressReward;
        public float MakeTime => _makeTime;
        public int InventorySize => _inventorySize;
    }

    
}