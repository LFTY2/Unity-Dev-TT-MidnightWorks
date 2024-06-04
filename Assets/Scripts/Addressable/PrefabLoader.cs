using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Addressable
{
    public static class PrefabLoader
    {
        public static async Task<GameObject> Load(string assetAddress)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(assetAddress);
            GameObject prefabGameObject = await handle.Task;
            prefabGameObject.SetActive(false);
            return prefabGameObject;
        }

        public static void Unload(GameObject prefab)
        {
            if (prefab != null)
            {
                prefab.SetActive(false);
                Addressables.ReleaseInstance(prefab);
            }
        }
    }
}