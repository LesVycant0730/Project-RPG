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

    public static TObject LoadAsset<TObject>(object key)
    {
        if (!IsReady)
            throw new Exception("Addressables is not initialized");

        var op = Addressables.LoadAssetAsync<TObject>(key);

        if (!op.IsDone)
            throw new Exception($"Addressables failed to load: {key}");

        if (op.Result == null)
            throw new Exception($"Sync load asset has null result {key}, Exception: {op.OperationException}");

        return op.Result;
    }

    public async static Task<TObject> LoadAsset<TObject>(AssetReference _asset, Action<AsyncOperationHandle<TObject>> _action)
    {
        if (_asset == null)
            throw new Exception("Tried to load asset from an empty asset reference");

        var handle = _asset.LoadAssetAsync<TObject>();

        if (_action != null)
            handle.Completed += _action;

        await handle.Task;

        return handle.Result;
    }
}
