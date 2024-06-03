using UnityEngine;

namespace Level.Entity.Item
{
    public sealed class ItemCashRegisterController : ItemController
    {
        private readonly ItemView View;

        public ItemCashRegisterController(Transform transform, float radius, ItemType type, ItemView view) : base(transform, radius, type)
        {
            View = view;
            View.Model = Model;
        }
    }
}