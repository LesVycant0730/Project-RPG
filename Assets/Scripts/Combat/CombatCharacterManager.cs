using RPG_Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using System;

public class CombatCharacterManager : MonoBehaviour, IManager
{
    private static CombatCharacterManager instance;

    [SerializeField] private CharacterPrefabDirectorySO characterDirectorySO;

    [SerializeField] private CharacterModel[] characterArr;

    public static CharacterAssetReference GetCharacter(Character_ID _id)
	{
        if (instance != null)
		{
            return instance.characterDirectorySO.GetCharacter(_id);
        }
        else
		{
            return null;
        }
    }

    public static CharacterModel GetCharacter(RPGCharacter _rpgCharacter)
	{
        if (_rpgCharacter == null)
		{
            CustomLog.Log("Attempt to Get Character from an empty reference.");
            return null;
		}

        if (instance != null)
        {

            //return instance.characterDirectorySO.GetCharacter(_rpgCharacter.GetCharacterStat().GetID());
            return null;
        }
        else
        {
            return null;
        }
    }

    private CharacterModel GetCharacterModel(RPGCharacter _rpgCharacter)
	{
        if (Utility.IsNullOrEmpty(characterArr) || _rpgCharacter == null)
		{
            Debug.LogWarning("Attempt to create character from an empty, null array or empty RPGCharacter reference.");
            return null;
		}

        // Get the instantiated model in character array
        CharacterModel model = characterArr.Single(x => x.IsSameCharacter(_rpgCharacter.GetCharacterStat().GetID()));

        if (model != null)
		{
            return model;
		}
        else
		{
            CharacterAssetReference asset = characterDirectorySO.GetCharacter(_rpgCharacter.GetCharacterStat().GetID());

            if (asset != null)
			{
               //await asset.AssetRef.LoadAssetAsync();
			}

            return null;

        }
    }

	public void Init()
	{
        instance = this;

        characterArr = new CharacterModel[Utility.GetEnumLength<Character_ID>()];
    }

    public void Run()
	{
        
	}

	public void Exit()
	{
        
	}
}
