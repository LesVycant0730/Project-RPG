using RPG_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RoboRyanTron.SearchableEnum;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

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
    }

    public bool IsSameCharacter(Character_ID _id)
    {
        return id == _id;
    }

    public void Enable(bool _enable)
	{
        isUsing = _enable;
	}
}

[CreateAssetMenu(fileName = "Character Prefab Directory", menuName = "ScriptableObjects/Directory/Character", order = 1)]
public class CharacterPrefabDirectorySO : PrefabDirectorySO
{
    [SerializeField] private CharacterAssetReference[] charactersDirectory;

    public override bool HasDirectory()
    {
        return charactersDirectory != null && charactersDirectory.Length > 0;
    }

#if UNITY_EDITOR
    public void EditorGenerateFullDirectoryArray()
	{
        IEnumerable<Character_ID> ids = Utility.GetTypeElements<Character_ID>();

        charactersDirectory = new CharacterAssetReference[ids.Count()];

        for (int i = 0; i < charactersDirectory.Length; i++)
        {
            charactersDirectory[i] = new CharacterAssetReference(ids.ElementAt(i));
        }
    }

    public void EditorGenerateMissingDirectoryArray()
	{
        if (charactersDirectory != null)
		{
            IEnumerable<Character_ID> ids = Utility.GetTypeElements<Character_ID>();

            CharacterAssetReference[] tempDir = new CharacterAssetReference[ids.Count()];

            for (int i = 0; i < tempDir.Length; i++)
            {
                // Get the element of the Character_ID enum
                Character_ID id = ids.ElementAt(i);

                // Find and compare ID in the directory array
                CharacterAssetReference existingDir = Array.Find(charactersDirectory, x => x.IsSameID(id));

                // Replace the element in the new array with the element from the old array
                tempDir[i] = existingDir ?? new CharacterAssetReference(id);
            }

            charactersDirectory = tempDir;
        }
        else
		{
            EditorGenerateFullDirectoryArray();
		}
    }
    
    public void CheckDirectory()
	{
		if (charactersDirectory != null)
		{
            List<Character_ID> charIDs = new List<Character_ID>();

            foreach(var character in charactersDirectory)
			{
                charIDs.Add(character.ID);
			}

            Utility.IsArrayContainAllEnumElements(charIDs.ToArray(), out List<Character_ID> missingElementList);

            missingElementList.ForEach(x => Debug.LogWarning($"Missing element: {x} in the SO"));
		}
		else
		{
            Debug.LogWarning("Null Directory");
		}
    }
#endif

    public CharacterAssetReference GetCharacter(Character_ID _id)
    {
        return charactersDirectory.Single(x => x.IsSameID(_id));
    }

    public CharacterAssetReference GetCharacter(RPGCharacter _rpgCharacter)
    {
        if (_rpgCharacter == null)
        {
            CustomLog.Log("Attempt to Get Character from an empty reference.");
            return null;
        }

        Character_ID id = _rpgCharacter.GetCharacterStat().GetID();

        return charactersDirectory.Single(x => x.IsSameID(id));
    }

    public async Task<CharacterModel> LoadCharacter(Character_ID _id)
	{
        CharacterAssetReference characterAsset = GetCharacter(_id);

        if (characterAsset != null)
		{
            var op = await AddressablesUtility.LoadAsset<GameObject>(characterAsset.AssetRef, (handle) =>
            {

            });

            return new CharacterModel(_id, op);
        }
        else
            return null;
	}
}
