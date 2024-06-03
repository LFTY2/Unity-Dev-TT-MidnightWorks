using Core.UI;
using UnityEngine;

namespace Level.Entity.Item
{
    public enum ItemType
    {
        None,
        CashPile,
        BuyUpdate,
        CashRegisterDesk,
        Plant,
        Enrichment
    }

    public class ItemView : BehaviourWithModel<ItemModel>
    {
        [SerializeField] private ItemType _type;
        public ItemType Type => _type;

        protected override void OnModelChanged(ItemModel model)
        {
        }

        public Vector3 Position
        {
            get { return transform.position; }
        }
    }
}