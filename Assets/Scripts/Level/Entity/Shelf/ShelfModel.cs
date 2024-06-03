namespace Level.Entity.Shelf
{
    public sealed class ShelfModel : EntityModel
    {
        public ShelfModel(string id, int lvl, EntityType type, ShelfConfig config) : base(id, lvl, type)
        {
            TargetPurchaseValue = config.ProgressToUnlock;
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