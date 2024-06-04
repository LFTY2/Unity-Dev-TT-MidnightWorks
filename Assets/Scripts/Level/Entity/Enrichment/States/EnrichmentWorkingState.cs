
using Core.Inject;
using Level.Entity.Product;
using UnityEngine;

namespace Level.Entity.Enrichment.States
{
    public sealed class EnrichmentWorkingState : EnrichmentState
    {
        [Inject] private ProductPool.ProductPool _productPool;
        private float _timeToGrow;
        public override void Initialize()
        {
            ProductView product = _enrichment.InInventoryController.RemoveLastProduct();
            _productPool.ReturnToPool(product);
            _enrichment.View.FillBar.Holder.SetActive(true);
            _timeToGrow = _enrichment.View.Config.MakeTime;
            _timer.TICK += OnTick;
        }

        private void OnTick()
        {
            _timeToGrow -= Time.deltaTime;
            _enrichment.View.FillBar.FillImage.fillAmount = 1 - _timeToGrow / _enrichment.View.Config.MakeTime;
            if (_timeToGrow <= 0)
            {
                ProductView product = _productPool.GetProduct(_enrichment.View.Config.OutProduct);
                _enrichment.OutInventoryController.AddProduct(product);
                _enrichment.SwitchToState(new EnrichmentWaitingState());
            }
        }

        public override void Dispose()
        {
            _enrichment.View.FillBar.Holder.SetActive(false);
            _timer.TICK -= OnTick;
        }
    }
}