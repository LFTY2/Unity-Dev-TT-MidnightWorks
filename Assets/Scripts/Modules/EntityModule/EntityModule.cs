
using Core.Inject;
using Level;
using Level.Core;
using Level.Entity.Area;
using Level.Entity.Area.State;
using Level.Entity.Enrichment;
using Level.Entity.Plant;
using Level.Entity.Shelf;
using Managers;


namespace Modules.EntityModule
{
    public sealed class EntityModule : Module<EntityModuleView>
    {
        [Inject] private Context _context;
        [Inject] private GameManager _gameManager;
        [Inject] private LevelView _levelView;

        public EntityModule(EntityModuleView view) : base(view)
        {
            
        }

        public override void Initialize()
        {
            SetAreas();
            SetShelf();
            SetEnrichment();
            SetPlants();
        }

        public override void Dispose()
        {
            foreach (var area in _gameManager.Areas)
            {
                area.Dispose();
            }
            _gameManager.Areas.Clear();
            
            _gameManager.Entities.Clear();
        }

        private void SetAreas()
        {
            foreach (var view in _view.AreaViews)
            {
                var area = new AreaController(view, _context);
                _gameManager.Areas.Add(area);
                _levelView.AddLvl(area.Number);
            }

            foreach (var area in _gameManager.Areas)
            {
                area.SwitchToState(new AreaInitializeState());
            }
        }

   
        private void SetPlants()
        {
            foreach (var view in _view.PlantViews)
            {
                PlantController plantController = new PlantController(view, _context);
                _levelView.AddReward(plantController.Model.Area, plantController.Model.PurchaseProgressReward);
                _gameManager.Plants.Add(plantController);
                _gameManager.Entities.Add(plantController);
            }
        }
        
        private void SetEnrichment()
        {
            foreach (var view in _view.EnrichmentView)
            {
                EnrichmentController enrichment = new EnrichmentController(view, _context);
                _levelView.AddReward(enrichment.Model.Area, enrichment.Model.PurchaseProgressReward);
                _gameManager.Enrichments.Add(enrichment);
                _gameManager.Entities.Add(enrichment);
            }
        }
        private void SetShelf()
        {
            foreach (var view in _view.ShelfView)
            {
                ShelfController shelf = new ShelfController(view, _context);
                _gameManager.Shelfs.Add(shelf);
                _gameManager.Entities.Add(shelf);
            }
        }
    }
}
