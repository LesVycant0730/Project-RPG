using RPG_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

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

        Character_ID id = _rpgCharacter.CharacterStat.GetID();

        return charactersDirectory.Single(x => x.IsSameID(id));
    }

    public async Task<Character> LoadCharacter(Character_ID _id, RPG_Party _party, Vector3 _pos, Quaternion _rot)
	{
        CharacterAssetReference characterAsset = GetCharacter(_id);

        if (characterAsset != null)
		{
            GameObject go = await AddressablesUtility.LoadAsset(characterAsset.AssetRef, _pos, _rot, null, (handle) =>
            {
                Debug.Log($"Instantiated game object: {_id}");
            });

            return new Character(_id, _party, go);
        }
        else
            return null;
	}
}
