using UnityEngine;

namespace Level.Entity.Units
{
    public class UnitStaffView : UnitView
    {
        [SerializeField] private Transform[] _inventoryPositions;
        public Transform[] InventoryPositions => _inventoryPositions;
    
    }
}