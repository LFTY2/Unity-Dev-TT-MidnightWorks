using Level.Entity.Item;
using UnityEngine;

namespace Level.Entity.Shelf
{
    public sealed class ShelfView : EntityView
    {
        [SerializeField] private ShelfConfig _shelfConfig;
        [SerializeField] private ItemView _itemStoreProducts;
        [SerializeField] private AudioSource _placeSound;
        [SerializeField] private GameObject _shelfGameObject;
        [SerializeField] private SpriteRenderer _productSprite;
        [SerializeField] private Transform[] _inventoryPositions;
        
        
        public ShelfConfig ShelfConfig => _shelfConfig;
        public ItemView ItemStoreProducts => _itemStoreProducts;
        public GameObject ShelfGameObject => _shelfGameObject;
        public AudioSource PlaceSound => _placeSound;
        public SpriteRenderer ProductSprite => _productSprite;
        public Transform[] InventoryPositions => _inventoryPositions;
    }
}
