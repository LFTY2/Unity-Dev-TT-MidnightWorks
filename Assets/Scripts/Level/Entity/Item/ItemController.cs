using System;
using Core.Observer;
using UnityEngine;
using Utils;

namespace Level.Entity.Item
{
    public class ItemModel : Observable
    {
        public float Duration;
        public float DurationNominal;
    }

    public class ItemController
    {
        public event Action<ItemController> PLAYER_ON_ITEM;
        public event Action<ItemController> ITEM_FINISHED;

        private ItemModel _model;

        private Transform _transform;
        private float _radius;
        private ItemType _type;

        public Transform Transform => _transform;
        public float Radius => _radius;
        public ItemType Type => _type;
        public ItemModel Model => _model;
        public int Area;

        public ItemController(Transform transform, float radius, ItemType type)
        {
            _model = new ItemModel();

            _transform = transform;
            _radius = radius;
            _type = type;
        }

        internal virtual void FireItemFinished()
        {
            ITEM_FINISHED.SafeInvoke(this);
  
        }

        internal virtual void FirePlayerOnItem()
        {
            PLAYER_ON_ITEM.SafeInvoke(this);
        }
    }
}