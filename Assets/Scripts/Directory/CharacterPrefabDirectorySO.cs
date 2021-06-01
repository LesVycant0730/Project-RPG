using RPG_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RoboRyanTron.SearchableEnum;

[Serializable]
public class CombatCharacter
{
    [SerializeField, SearchableEnum] private Character_ID id = Character_ID.NULL;
    [SerializeField] private GameObject mainObject;
    [SerializeField] private GameObject model;
    [SerializeField] private Animator anim;

    public Character_ID ID => id;
    public GameObject MainObject => mainObject;
    public GameObject Model => model;
    public Animator Anim => anim;

    public CombatCharacter(Character_ID _id = Character_ID.NULL)
    {
        id = _id;
        mainObject = null;
        model = null;
        anim = null;
    }

    public CombatCharacter(Character_ID _id, GameObject _mainObj, GameObject _model, Animator _anim)
    {
        id = _id;
        mainObject = _mainObj;
        model = _model;
        anim = _anim;
    }

    public bool IsSameCharacter(Character_ID _id)
    {
        return id == _id;
    }
}

[CreateAssetMenu(fileName = "Character Prefab Directory", menuName = "ScriptableObjects/Directory/Character", order = 1)]
public class CharacterPrefabDirectorySO : PrefabDirectorySO
{
    [SerializeField] private CombatCharacter[] charactersDirectory;

    public override bool HasDirectory()
    {
        return charactersDirectory != null && charactersDirectory.Length > 0;
    }

#if UNITY_EDITOR
    public void EditorGenerateFullDirectoryArray()
	{
        IEnumerable<Character_ID> ids = Utility.GetTypeElements<Character_ID>();

        charactersDirectory = new CombatCharacter[ids.Count()];

        for (int i = 0; i < charactersDirectory.Length; i++)
        {
            charactersDirectory[i] = new CombatCharacter(ids.ElementAt(i));
        }
    }

    public void EditorGenerateMissingDirectoryArray()
	{
        if (charactersDirectory != null)
		{
            IEnumerable<Character_ID> ids = Utility.GetTypeElements<Character_ID>();

            CombatCharacter[] tempDir = new CombatCharacter[ids.Count()];

            for (int i = 0; i < tempDir.Length; i++)
            {
                // Get the element of the Character_ID enum
                Character_ID id = ids.ElementAt(i);

                // Find and compare ID in the directory array
                CombatCharacter existingDir = Array.Find(charactersDirectory, x => x.IsSameCharacter(id));

                // Replace the element in the new array with the element from the old array
                tempDir[i] = existingDir ?? new CombatCharacter(id);
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

            Utility.IsArrayContainAllTypeElements(charIDs.ToArray(), out List<Character_ID> hey);
		}
		else
		{
            Debug.LogWarning("Null Directory");
		}
    }
#endif

    public CombatCharacter GetCharacter(Character_ID _id)
    {
        return charactersDirectory.Single(x => x.IsSameCharacter(_id));
    }

    public CombatCharacter GetCharacter(RPGCharacter _rpgCharacter)
    {
        if (_rpgCharacter == null)
        {
            CustomLog.Log("Attempt to Get Character from an empty reference.");
            return null;
        }

        Character_ID id = _rpgCharacter.GetCharacterStat().GetID();

        return charactersDirectory.Single(x => x.IsSameCharacter(id));
    }
}
