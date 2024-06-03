

using Level.Entity.Plant.States;

namespace Level.Entity.Enrichment.States
{
    public sealed class EnrichmentInitializeState : EnrichmentState
    {
        public override void Initialize()
        {
            var area = _gameManager.FindArea(_enrichment.Model.Area);
            if (_enrichment.Model.IsPurchased && area.Model.IsPurchased)
            {
                _enrichment.InitializeItems();
                _enrichment.View.HudView.gameObject.SetActive(false);
                _enrichment.View.PlantUnit.gameObject.SetActive(true);
                _enrichment.SwitchToState(new EnrichmentWaitingState());
            }
                
            else
            {
                if (_enrichment.IsPurchasable(area.Model.IsPurchased, _gameManager.Model.LoadProgress(), _enrichment.Model.TargetPurchaseValue))
                    _enrichment.SwitchToState(new EnrichmentReadyToPurchaseState());
                else
                    _enrichment.SwitchToState(new EnrichmentHiddenState());
            }
        }

        public override void Dispose()
        {
        }
    }
}