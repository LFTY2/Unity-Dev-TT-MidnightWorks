namespace Level.Entity
{
    public abstract class EntityUpdatableController
    {
        public bool IsUpdatable(bool isMaxed, int currentProgress, int targetUpdateProgress)
        {
            return !isMaxed && currentProgress >= targetUpdateProgress;
        }

        public bool IsPurchasable(bool isAreaPurchased, int currentValue, int targetValue)
        {
            return isAreaPurchased && currentValue >= targetValue;
        }
    }
}