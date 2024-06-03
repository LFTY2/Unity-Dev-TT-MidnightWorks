using Core.Observer;

namespace Level.Entity
{
    public abstract class EntityModel : Observable
    {
        public EntityType Type;
        public string ID;
    
        public long Cash;

        public int Lvl;
        public int LvlNext;

        public bool IsPurchased { get; internal set; }
        public bool IsMaxed { get; internal set; }
        public bool IsLocked { get; internal set; }

        public int PricePurchase;
        public int PriceNextLvl;

        public int Area;
        public int TargetPurchaseValue;
        public int TargetUpdateProgress;

        public int PurchaseProgressReward;

        public EntityModel(string id, int lvl, EntityType type)
        {
            ID = id;
            Lvl = lvl;
            Type = type;
        }

        public abstract int GetLvlLength();

        public virtual void UpdateModel()
        {
            LvlNext = Lvl + 1;
            int lvlLength = GetLvlLength();
            if (LvlNext >= lvlLength)
            {
                LvlNext = Lvl;
                IsMaxed = true;
            }
            GetUpdatedValues();
        }
        public abstract void GetUpdatedValues();
    }
}