using Level.Entity.Area;

namespace Level.Entity.Plant.States
{
    public sealed class PlantHiddenState : PlantState
    {
        public override void Initialize()
        {
            _plant.View.PlantUnit.gameObject.SetActive(false);
            _plant.View.HudView.gameObject.SetActive(false);

            _gameManager.PROGRESS_CHANGED += OnProgressChanged;
            _gameManager.AREA_PURCHASED += OnAreaPurchased;
        }

        public override void Dispose()
        {
            _gameManager.PROGRESS_CHANGED -= OnProgressChanged;
            _gameManager.AREA_PURCHASED -= OnAreaPurchased;
        }

        private void OnProgressChanged(int progress)
        {
            var area = _gameManager.FindArea(_plant.Model.Area);
            CheckIsPurchasable(area, progress);
        }

        private void OnAreaPurchased(AreaController area)
        {
            if (area.Number != _plant.Model.Area) return;
            CheckIsPurchasable(area, _gameManager.Model.LoadProgress());
        }

        private void CheckIsPurchasable(AreaController area, int progress)
        {
            if (_plant.IsPurchasable(area.Model.IsPurchased, progress, _plant.Model.TargetPurchaseValue))
                _plant.SwitchToState(new PlantReadyToPurchaseState());
        }
    }
}