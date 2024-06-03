namespace Level.Entity.Enrichment.States
{
    public sealed class EnrichmentWaitingState : EnrichmentState
    {
        public override void Initialize()
        {
            _enrichment.InInventoryController.ON_PRODUCTS_CHANGE += Check;
            _enrichment.OutInventoryController.ON_PRODUCTS_CHANGE += Check;
            Check();
        }

        private void Check()
        {
            if (_enrichment.InInventoryController.ItemsStored > 0 && !_enrichment.OutInventoryController.IsFull)
            {
                _enrichment.SwitchToState(new EnrichmentWorkingState());
            }
        }
        
        public override void Dispose()
        {
            _enrichment.InInventoryController.ON_PRODUCTS_CHANGE -= Check;
            _enrichment.OutInventoryController.ON_PRODUCTS_CHANGE -= Check;
        }
    }
}