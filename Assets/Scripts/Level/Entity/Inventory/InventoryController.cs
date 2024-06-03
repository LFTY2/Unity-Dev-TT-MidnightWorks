using System;
using System.Collections.Generic;
using Level.Entity.Product;
using UnityEngine;

namespace Level.Entity.Inventory
{
    public class InventoryController
    {
        public event Action ON_PRODUCTS_CHANGE; 
        private List<ProductView> _products = new List<ProductView>();
        private Transform[] _productPositions;
        private int _size;
        public int ItemsStored => _products.Count;
        public bool IsFull => ItemsStored >= _size;
        
        public InventoryController(Transform[] productPositions, int size)
        {
            _size = size;
            _productPositions = productPositions;
        }

        private void ChangeView()
        {
            for (var i = 0; i < ItemsStored; i++)
            {
                Transform productTransform = _products[i].transform;
                productTransform.parent = _productPositions[i];
                productTransform.localPosition = Vector3.zero;
                productTransform.localRotation = Quaternion.identity;
            }
            ON_PRODUCTS_CHANGE?.Invoke();
        }

        public void AddProduct(ProductView product)
        {
            if (IsFull) return;
            _products.Add(product);
            ChangeView();
        }
        
        public ProductView RemoveLastProduct()
        {
            if (_products.Count > 0)
            {
                ProductView productToRemove = _products[^1];
                _products.Remove(productToRemove);
                ChangeView();
                return productToRemove;
            }
            return null;
        }

        public void RemoveProduct(ProductView productToRemove)
        {
            _products.Remove(productToRemove);
            ChangeView();
        }

        public bool CheckOnProductType(ProductType productType)
        {
            for (int i = _products.Count - 1; i >= 0; i--)
            {
                if (_products[i].ProductConfig.ProductType == productType)
                {
                    return true;
                }
            }
            return false;
        }
        
        public ProductView GetProductByType(ProductType productType)
        {
            for (int i = _products.Count - 1; i >= 0; i--)
            {
                if (_products[i].ProductConfig.ProductType == productType)
                {
                    return _products[i];
                }
            }
            return null;
        }
    }
}