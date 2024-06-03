using System;
using Level.Entity.Product;
using UnityEngine;

namespace Level.Entity.Shelf
{
    [Serializable]
    [CreateAssetMenu(menuName = "config/shelfconfig")]
    public sealed class ShelfConfig : EntityConfig
    {
        [SerializeField] private int _itemsStored;
        [SerializeField] private ProductType _productType;
        [SerializeField] private float _customerRadius;
        [SerializeField] private int _progressToUnlock;
        public int ItemsStored => _itemsStored;
        public ProductType ProductType => _productType;
        public float CustomerRadius => _customerRadius;
        public int ProgressToUnlock => _progressToUnlock;
    }
}