using System.Collections.Generic;
using Level.Entity.Customer;
using Level.Entity.Product;
using UI;
using UI.Core;
using UnityEngine;

namespace Level.Entity.ProductPool
{
    public class ProductPool 
    {
        private List<ComponentPoolFactory> _productsPool = new List<ComponentPoolFactory>();
        private List<ProductConfig> _productConfigs = new List<ProductConfig>();
        public ProductPool(GameView _gameView)
        {
            _productsPool = _gameView.ProductViews;
            foreach (var poolFactory in _productsPool)
            {
                ProductConfig productConfig = poolFactory.Prefab.GetComponent<ProductView>().ProductConfig;
                _productConfigs.Add(productConfig);
            }
        }

        public ProductView GetProduct(ProductType productType, Vector3 spawnPos)
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