namespace Level.Entity.Area
{
    public sealed class AreaModel : EntityModel
    {
        public AreaModel(string id, int lvl, EntityType type, AreaConfig config) : base(id, lvl, type)
        {
            PricePurchase = config.PricePurchase;
            TargetPurchaseValue = config.TargetLvl;

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