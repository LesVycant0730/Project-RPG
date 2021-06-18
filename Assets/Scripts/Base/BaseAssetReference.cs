using RoboRyanTron.SearchableEnum;
using RPG_Data;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BaseAssetReference
{
    [SerializeField] private AssetReference assetRef = null;
    public AssetReference AssetRef => assetRef;
}

[Serializable]
public class CharacterAssetReference : BaseAssetReference
{
    [SerializeField, SearchableEnum] private Character_ID id = Character_ID.NULL;

    public Character_ID ID => id;

    public bool IsSameID(Character_ID _id)
    {
        return id == _id;
    }

    public CharacterAssetReference(Character_ID _id = Character_ID.NULL)
    {
        id = _id;
    }
}

[Serializable]
public class VFXAssetReference : BaseAssetReference
{
    [SerializeField] private string id;
    public string ID => id;

    public bool IsSameID(string _id)
	{
        return id == _id;
	}

    public VFXAssetReference(string _id)
	{
        id = _id;
	}
}