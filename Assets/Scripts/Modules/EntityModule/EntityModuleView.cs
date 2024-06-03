using Level.Entity.Area;
using Level.Entity.Enrichment;
using Level.Entity.Plant;
using Level.Entity.Shelf;
using UnityEngine;

namespace Modules.EntityModule
{
    public sealed class EntityModuleView : MonoBehaviour
    {
        [HideInInspector] public AreaView[] AreaViews;
        [HideInInspector] public PlantView[] PlantViews;
        [HideInInspector] public ShelfView[] ShelfView;
        [HideInInspector] public EnrichmentView[] EnrichmentView;

        private void Awake()
        {
            AreaViews = GetComponentsInChildren<AreaView>();
            PlantViews = GetComponentsInChildren<PlantView>();
            ShelfView = GetComponentsInChildren<ShelfView>();
            EnrichmentView = GetComponentsInChildren<EnrichmentView>();
        }
    }
}
