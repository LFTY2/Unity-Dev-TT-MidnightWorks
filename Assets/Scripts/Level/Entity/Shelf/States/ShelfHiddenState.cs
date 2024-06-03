using Level.Entity.Area;

namespace Level.Entity.Shelf.States
{
    public sealed class ShelfHiddenState : ShelfState
    {
        public override void Initialize()
        {
            _shelf.View.ShelfGameObject.gameObject.SetActive(false);

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
            var area = _gameManager.FindArea(_shelf.Model.Area);
            CheckIsPurchasable(area, progress);
        }

        private void OnAreaPurchased(AreaController area)
        {
            if (area.Number != _shelf.Model.Area) return;
            CheckIsPurchasable(area, _gameManager.Model.LoadProgress());
        }

        private void CheckIsPurchasable(AreaController area, int progress)
        {
            if (_shelf.CheckIsPurchasable(area, progress))
            {
                _shelf.SwitchToState(new ShelfUnlockedState());
            }
        }
    }
}