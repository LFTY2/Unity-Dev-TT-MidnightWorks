using System;
using System.Collections.Generic;
using Domain;
using Level.Entity;
using Level.Entity.Area;
using Level.Entity.CashRegister;
using Level.Entity.Customer;
using Level.Entity.Enrichment;
using Level.Entity.Item;
using Level.Entity.Plant;
using Level.Entity.Player.Player;
using Level.Entity.Product;
using Level.Entity.Shelf;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public sealed class GameManager : IDisposable
    {
        public event Action<Vector3, int> ADD_GAME_PROGRESS;
        public event Action<int> PROGRESS_CHANGED;
        public event Action<int> LEVEL_CHANGED;
        public event Action<AreaController> AREA_PURCHASED;
        public event Action<Vector3> FLY_TO_REMOVE_CASH;
        public event Action<Vector3, int> ON_NOTIFICATION_NEED_LVL;

        private List<ItemController> _items;
        public readonly GameModel Model;
        public PlayerController Player;
        public CashRegisterController CashRegister;
        public List<AreaController> Areas;
        public List<EntityController> Entities;
        public List<ShelfController> Shelfs;
        public List<PlantController> Plants;
        public List<EnrichmentController> Enrichments;
        

        public GameManager(GameConfig config)
        {
            Model = GameModel.Load(config);
            Areas = new List<AreaController>();
            Plants = new List<PlantController>();
            Enrichments = new List<EnrichmentController>();
            _items = new List<ItemController>();
            Entities = new List<EntityController>();
            Shelfs = new List<ShelfController>();
        }

        public void Dispose()
        {
        }

        public void AddItem(ItemController item)
        {
            if (!_items.Contains(item))
            {
                _items.Add(item);
            }
        }

        public void RemoveItem(ItemController item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
        }
        
        
        public ItemController FindClosestUsedItem()
        {
            foreach (var item in _items)
            {
                if (item != null && Vector3.Distance(item.Transform.position, Player.View.transform.position) < item.Radius)
                    return item;
            }
            return null;
        }

        public EntityController FindClosestEntity(float radius)
        {
            foreach (var entity in Entities)
            {
                if (entity != null && Vector3.Distance(entity.Transform.position, Player.View.transform.position) < radius)
                    return entity;
            }
            return null;
        }
        
        internal AreaController FindArea(int number)
        {
            foreach (var area in Areas)
            {
                if (area.Number == number)
                    return area;
            }
            return null;
        }

        private List<ProductType> GetAllProductTypes()
        {
            List<ProductType> productTypesUnlocked = new List<ProductType>();
            foreach (var shelf in Shelfs)
            {
                if (shelf.CheckIsPurchasable(FindArea(shelf.Model.Area), Model.LoadProgress()))
                    productTypesUnlocked.Add(shelf.View.ShelfConfig.ProductType);
            }

            return productTypesUnlocked;
        }

        public CustomerNeeds GenerateCustomerNeeds()
        {
            List<ProductType> productTypesUnlocked = GetAllProductTypes();
            int randProduct = Random.Range(0, productTypesUnlocked.Count);
            int productAmount = Random.Range(1, 4);
            return new CustomerNeeds(productTypesUnlocked[randProduct], productAmount);
        }

        public int GetUnlockedProductCount()
        {
            return GetAllProductTypes().Count;
        }

        public ShelfController FindShelf(ProductType productType)
        {
            foreach (var shelf in Shelfs)
            {
                if (shelf.View.ShelfConfig.ProductType == productType)
                {
                    return shelf;
                }
            }
            return null;
        }

        public void FireAddGameProgress(Vector3 position, int progressDelta)
        {
            ADD_GAME_PROGRESS.SafeInvoke(position, progressDelta);
        }

        public void FireProgressChanged(int progress)
        {
            PROGRESS_CHANGED.SafeInvoke(progress);
        }

        public void FireAreaPurchased(AreaController area)
        {
            AREA_PURCHASED.SafeInvoke(area);
        }

        public void FireLevelChanged(int lvl)
        {
            LEVEL_CHANGED.SafeInvoke(lvl);
        }

        public void FireFlyToRemoveCash(Vector3 endPosition)
        {
            FLY_TO_REMOVE_CASH.SafeInvoke(endPosition);
        }

        internal void FireNotificationNeedLvl(Vector3 itemPosition, int lvl)
        {
            ON_NOTIFICATION_NEED_LVL.SafeInvoke(itemPosition, lvl);
        }
    }
}