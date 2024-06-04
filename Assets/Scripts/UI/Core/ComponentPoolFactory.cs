using System;
using System.Collections.Generic;
using Addressable;
using UnityEngine;

namespace UI.Core
{
    public class ComponentPoolFactory : MonoBehaviour
    {
        public event Action OnInitialized;
        [SerializeField] private int _count;
        [SerializeField] private Transform _content;
        [SerializeField] private Transform _poolStorage;
        private GameObject _prefab;
        private readonly HashSet<GameObject> _instances = new();
        private Queue<GameObject> _pool = new();
        private int CountInstances => _instances.Count;
        
        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            if (CountInstances > 0)
                return;

            for (int i = 0; i < _count; i++)
            {
                Get<Transform>();
            }
            ReleaseAllInstances();
        }

        public async void Initialize(string assetAddress)
        {
            _prefab = await PrefabLoader.Load(assetAddress);
            IsInitialized = true;
            OnInitialized?.Invoke();
        }
        

        public T Get<T>() where T : Component
        {
            return Get<T>(CountInstances);
        }

        public T Get<T>(int sublingIndex) where T : Component
        {
            bool isNewInstance = false;
            if (_pool.Count == 0)
            {
                GameObject result = Instantiate(_prefab);

                if (null == result)
                    return null;

                _pool.Enqueue(result);
                isNewInstance = true;
            }

            T resultComponent = _pool.Dequeue().GetComponent<T>();
            if (null == resultComponent)
            {
                return resultComponent;
            }

            var go = resultComponent.gameObject;
            var t = resultComponent.transform;
            if (isNewInstance || (_poolStorage != null && _poolStorage != _content))
            {
                t.SetParent(_content, false);
            }

            _instances.Add(go);

            if (!go.activeSelf)
            {
                go.SetActive(true);
            }

            if (t.GetSiblingIndex() != sublingIndex)
            {
                t.SetSiblingIndex(sublingIndex);
            }

            return resultComponent;
        }

        public void Release<T>(T component) where T : Component
        {
            var go = component.gameObject;
            if (_instances.Contains(go))
            {
                go.SetActive(false);
                if (_poolStorage)
                {
                    go.transform.SetParent(_poolStorage, false);
                }
                _pool.Enqueue(go);
                _instances.Remove(go);
            }
        }

        public void ReleaseAllInstances()
        {
            foreach (GameObject instance in _instances)
            {
                instance.SetActive(false);
                if (_poolStorage)
                {
                    instance.transform.SetParent(_poolStorage, false);
                }
                _pool.Enqueue(instance);
            }
            _instances.Clear();
        }

        public void Dispose()
        {
            ReleaseAllInstances();
            PrefabLoader.Unload(_prefab);

            foreach (GameObject gameObject in _pool)
            {
                GameObject.Destroy(gameObject);
            }
            _pool.Clear();
        }
    }
}
