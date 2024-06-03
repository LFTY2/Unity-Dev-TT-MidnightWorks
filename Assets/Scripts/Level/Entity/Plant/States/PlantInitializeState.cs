

namespace Level.Entity.Plant.States
{
    public sealed class PlantInitializeState : PlantState
    {
        public override void Initialize()
        {
            var area = _gameManager.FindArea(_plant.Model.Area);
            _plant.View.ItemCollectView.gameObject.SetActive(false);
            if (_plant.Model.IsPurchased && area.Model.IsPurchased)
            {
                _plant.View.HudView.gameObject.SetActive(false);
                _plant.SwitchToState(new PlantGrowingState());
            }
            else
            {
                if (_plant.IsPurchasable(area.Model.IsPurchased, _gameManager.Model.LoadProgress(), _plant.Model.TargetPurchaseValue))
                    _plant.SwitchToState(new PlantReadyToPurchaseState());
                else
                    _plant.SwitchToState(new PlantHiddenState());
            }
        }

        public override void Dispose()
        {
        }
    }
}