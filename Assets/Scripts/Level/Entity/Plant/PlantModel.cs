namespace Level.Entity.Plant
{
    public sealed class PlantModel : EntityModel
    {
        public PlantModel(string id, int lvl, EntityType type, PlantConfig config) : base(id, lvl, type)
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