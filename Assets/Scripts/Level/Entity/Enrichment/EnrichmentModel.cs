namespace Level.Entity.Enrichment
{
    public sealed class EnrichmentModel : EntityModel
    {
        public EnrichmentModel(string id, int lvl, EntityType type, EnrichmentConfig config) : base(id, lvl, type)
        {
            PurchaseProgressReward = config.ProgressReward;
            PricePurchase = config.PricePurchase;
            TargetPurchaseValue = config.TargetPurchaseProgress;
            Area = config.Area;
            UpdateModel();
        }

        public override int GetLvlLength()
        {
            return 0;
        }

        public override void GetUpdatedValues()
        {
            
        }
    }
}