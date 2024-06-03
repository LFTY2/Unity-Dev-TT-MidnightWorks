using UnityEngine;

namespace Level.Entity.Product
{
    public class ProductView : MonoBehaviour
    {
        [SerializeField] private ProductConfig _productConfig;

        public ProductConfig ProductConfig => _productConfig;
    }
}