using UnityEngine;

namespace Level.Entity.Inventory
{
    public class InventoryDot : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
    }
}