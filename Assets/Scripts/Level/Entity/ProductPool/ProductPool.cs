using System.Collections.Generic;
using System.Linq;
using Level.Entity.Customer;
using Level.Entity.Product;
using UI;
using UI.Core;
using UnityEngine;

namespace Level.Entity.ProductPool
{
    public class ProductPool 
    {
        private List<ComponentPoolFactory> _productsPool;
        private List<ProductConfig> _productConfigs = new();
        public ProductPool(GameView _gameView)
        {
            _productsPool = _gameView.ProductViews;
            _productConfigs = Resources.LoadAll<ProductConfig>("Products").ToList();
            for (var i = 0; i < _productsPool.Count; i++)
            {
                var poolFactory = _productsPool[i];
                poolFactory.Initialize(_productConfigs[i].Name);
            }
        }

        public ProductView GetProduct(ProductType productType)
        {
            for (var i = 0; i < _productConfigs.Count; i++)
            {
                if (_productConfigs[i].ProductType == productType)
                {
                    ProductView productInstance = _productsPool[i].Get<ProductView>();
                    return productInstance.GetComponent<ProductView>();
                }
            }

            Debug.LogWarning("PRODUCT OF THAT TYPE WERE NOT FOUND");
            return null;
        }

        public void ReturnToPool(ProductView productView)
        {
            if (productView == null)
            {
                Debug.Log(productView);
                return;
            }
            for (var i = 0; i < _productConfigs.Count; i++)
            {
                if (_productConfigs[i].ProductType == productView.ProductConfig.ProductType)
                {
                    _productsPool[i].Release(productView);
                    return;
                }
            }
        }

        public Sprite GetProductSprite(ProductType productType)
        {
            ProductConfig product = GetProductConfig(productType);
            return product.ProductSprite;
        }

        private int GetProductCost(ProductType productType)
        {
            ProductConfig product = GetProductConfig(productType);
            return product.SellPrice;
        }
        

        private ProductConfig GetProductConfig(ProductType productType)
        {
            foreach (var product in _productConfigs)
            {
                if (product.ProductType == productType)
                {
                    return product;
                }
            }

            return null;
        }

        public int CalculatePrizeOfNeeds(CustomerNeeds customerNeeds)
        {
            return GetProductCost(customerNeeds.ProductType) * customerNeeds.Number;
        }
    }
}