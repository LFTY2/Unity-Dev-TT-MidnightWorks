using Level.Entity.Product;

namespace Level.Entity.Customer
{
    public class CustomerNeeds
    {
        private readonly ProductType _product;
        private readonly int _number;

        public ProductType ProductType => _product;
        public int Number => _number;

        public CustomerNeeds(ProductType product, int number)
        {
            _product = product;
            _number = number;
        }
    }
}