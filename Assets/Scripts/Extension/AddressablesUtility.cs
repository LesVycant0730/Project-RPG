using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Threading.Tasks;

public static class AddressablesUtility
{
    private static bool isInitialized = false;
    public static bool IsReady => isInitialized;

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        Addressables.InitializeAsync().Completed += (obj) => { isInitialized = true; };
    }

    public async static void AddressableAction<TObject>(Task<TObject> _action)
	{
        var handle = await _action;
	}

    public async static Task<TObject> LoadAsset<TObject>(AssetReference _asset, Action<AsyncOperationHandle<TObject>> _action)
    {
        if (_asset == null)
            throw new Exception("Tried to load asset from an empty asset reference");

        var handle = _asset.LoadAssetAsync<TObject>();

        if (_action != null)
            handle.Completed += _action;

        await handle.Task;

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

        var handle = _asset.InstantiateAsync(_pos, _rotation, _parent);

        if (_action != null)
            handle.Completed += _action;

        await handle.Task;

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