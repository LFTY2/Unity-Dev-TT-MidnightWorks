using Level.Entity.Units;

using UnityEngine;

namespace Level.Entity.Player.Player
{
    public sealed class PlayerView : UnitStaffView
    {
        [SerializeField] private Transform _aimTransform;
        public Vector3 AimPosition => _aimTransform.position;
     
    }
}