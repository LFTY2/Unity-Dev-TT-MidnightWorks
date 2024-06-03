using Level.Entity.Area;
using Level.Entity.Plant.States;

namespace Level.Entity.Enrichment.States
{
    public sealed class EnrichmentHiddenState : EnrichmentState
    {
        public override void Initialize()
        {
            _enrichment.View.PlantUnit.gameObject.SetActive(false);
            _enrichment.View.HudView.gameObject.SetActive(false);

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
            var area = _gameManager.FindArea(_enrichment.Model.Area);
            CheckIsPurchasable(area, progress);
        }

        private void OnAreaPurchased(AreaController area)
        {
            if (area.Number != _enrichment.Model.Area) return;
            CheckIsPurchasable(area, _gameManager.Model.LoadProgress());
        }

        private void CheckIsPurchasable(AreaController area, int progress)
        {
            if (_enrichment.IsPurchasable(area.Model.IsPurchased, progress, _enrichment.Model.TargetPurchaseValue))
            {
                _enrichment.View.PlantUnit.gameObject.SetActive(true);
                _enrichment.SwitchToState(new EnrichmentReadyToPurchaseState());
            }
               
        }
    }
}