using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class UtilAddressables
{
    private static bool isInitialized = false;
    public static bool IsReady => isInitialized;

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        Addressables.InitializeAsync().Completed += (obj) => { isInitialized = true; };
    }

    public async static Task<TObject> LoadAsset<TObject>(AssetReference _asset, Action<AsyncOperationHandle<TObject>> _action)
    {
        if (_asset == null)
            throw new Exception("Tried to load asset from an empty asset reference");
        
        // Load desired asset (Not instantiate)
        var handle = _asset.LoadAssetAsync<TObject>();

        // Add action invoked after addressable operation is completed
        if (_action != null)
            handle.Completed += _action;

        // Wait for addressable task
        await handle.Task;

        // Return the status and result
        if (handle.Status == AsyncOperationStatus.Succeeded)
		{
            return handle.Result;
		}
        else
		{
            throw new Exception("Failed addressable operation");
		}
    }

    public async static Task<GameObject> LoadAsset(AssetReference _asset, Vector3 _pos, Quaternion _rotation, 
        Transform _parent = null, Action<AsyncOperationHandle<GameObject>> _action = null)
    {
        if (_asset == null)
            throw new Exception("Tried to load asset from an empty asset reference");

        // Instantiate game object directly
        var handle = _asset.InstantiateAsync(_pos, _rotation, _parent);

        // Add action invoked after addressable operation is completed
        if (_action != null)
            handle.Completed += _action;

        // Wait for addressable task
        await handle.Task;

        // Return the status and result
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            throw new Exception("Failed addressable operation");
        }
    }
}
