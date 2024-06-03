using UI;
using UnityEngine;

namespace Level.Entity
{
    public class EntityWithHudView : EntityView
    {
        [SerializeField] private EntityHudView _hudView;
        public EntityHudView HudView => _hudView;
    }
}