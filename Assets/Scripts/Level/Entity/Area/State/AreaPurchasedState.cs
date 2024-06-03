using Core.Inject;
using Managers;

namespace Level.Entity.Area.State
{
    public sealed class AreaPurchasedState : AreaState
    {
        [Inject] private GameManager _gameManager;
        

        public override void Initialize()
        {
            _area.View.HudView.gameObject.SetActive(false);

            OnLvlChanged(_gameManager.Model.LoadLvl());

            _gameManager.LEVEL_CHANGED += OnLvlChanged;
            _gameManager.AREA_PURCHASED += OnAreaPurchased;
        }

        public override void Dispose()
        {
            _gameManager.LEVEL_CHANGED -= OnLvlChanged;
            _gameManager.AREA_PURCHASED -= OnAreaPurchased;
        }

        private void OnAreaPurchased(AreaController area)
        {
           
        }

        private void OnLvlChanged(int lvl)
        {
            
        }
    }
}

