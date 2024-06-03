using UnityEngine;

namespace Level.Entity
{
    public abstract class EntityController : EntityUpdatableController
    {
        public int CameraAngleSign;
        public abstract Transform Transform { get; }
    }
}