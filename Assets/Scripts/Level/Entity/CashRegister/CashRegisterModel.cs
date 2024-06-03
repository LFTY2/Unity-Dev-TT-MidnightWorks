
namespace Level.Entity.CashRegister
{
    public sealed class CashRegisterModel : EntityModel
    {
        public int CashiersCount;
        public float UnitProceedTime = 1f;

        public CashRegisterLvlConfig[] Lvls;
        public int ItemsToShow = 1;

        public CashRegisterModel(string id, int lvl, EntityType type, CashRegisterConfig config) : base(id, lvl, type)
        {
            Lvls = config.Lvls;

            UpdateModel();
        }

        public override int GetLvlLength()
        {
            return Lvls.Length;
        }

        public override void GetUpdatedValues()
        {
            TargetUpdateProgress = Lvls[LvlNext].TargetUpdateProgress;
            PriceNextLvl = Lvls[LvlNext].Price;
            CashiersCount = Lvls[Lvl].CashiersCount;
        }
    }
}