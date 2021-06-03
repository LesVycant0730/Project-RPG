using RoboRyanTron.SearchableEnum;
using RPG_Data;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class CharacterAssetReference
{
    [SerializeField, SearchableEnum] private Character_ID id = Character_ID.NULL;
    [SerializeField] private AssetReference assetRef;

    public Character_ID ID => id;
    public AssetReference AssetRef => assetRef;

    public bool IsSameID(Character_ID _id)
    {
        return id == _id;
    }

    public CharacterAssetReference(Character_ID _id = Character_ID.NULL)
    {
        id = _id;
        assetRef = null;
    }
}

[Serializable]
public class CharacterModel
{
    [SerializeField, SearchableEnum] private Character_ID id = Character_ID.NULL;
    [SerializeField] private GameObject model;
    [SerializeField] private Animator anim;
    [SerializeField] private bool isUsing = false;

    public Character_ID ID => id;
    public GameObject Model => model;
    public Animator Anim => anim;
    public bool IsUsing => isUsing;

    public CharacterModel(Character_ID _id = Character_ID.NULL)
    {
        id = _id;
        model = null;
        anim = null;
    }

    public CharacterModel(Character_ID _id, GameObject _model)
    {
        id = _id;
        model = _model;
        anim = _model.GetComponent<Animator>();
        isUsing = true;
    }

    public bool IsSameCharacter(Character_ID _id)
    {
        return id == _id;
    }

    public void Enable()
    {
        isUsing = true;
    }

    public void Disable()
    {
        isUsing = false;
    }

    public void Resume()
    {
        if (anim)
        {
            anim.speed = 1.0f;
        }
    }

    public void Pause()
    {
        if (anim)
        {
            anim.speed = 0;
        }
    }

    public void Default()
    {
        isUsing = false;
        anim = null;
        Destroy();
    }

    private void Destroy()
    {
        if (model)
        {
            Addressables.ReleaseInstance(model);
            model = null;
        }
    }
}
