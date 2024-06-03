using UnityEngine;

namespace Level.Entity
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private EntityType _type;
        public EntityType Type => _type;
    }
}