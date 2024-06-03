using System;
using UnityEngine;

namespace Level.Entity.Product
{
    [Serializable]
    [CreateAssetMenu(menuName = "config/productconfig")]
    public sealed class ProductConfig : ScriptableObject
    {
        [SerializeField] private int _sellPrice;
        [SerializeField] private ProductType _productType;
        [SerializeField] private Sprite _productSprite;
        public int SellPrice => _sellPrice;
        public ProductType ProductType => _productType;
        public Sprite ProductSprite => _productSprite;
    }

    
}