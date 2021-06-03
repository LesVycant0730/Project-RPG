using RPG_Data;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;

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

    public async static Task<CharacterModel> GetCharacter(RPGCharacter _rpgCharacter)
	{
        if (_rpgCharacter == null)
		{
            throw new Exception("Attempt to Get Character from an empty reference.");
		}

        if (instance != null)
        {
            if (Utility.IsNullOrEmpty(instance.characterArr))
			{
                throw new Exception("Attempt to create character from an empty or null array");
            }

            CharacterModel model =/* instance.characterArr.Single(x => x.IsSameCharacter(_rpgCharacter.GetCharacterStat().GetID()));*/ null;

            if (model != null)
            {
                return model;
            }
            else
			{
                // Update Library
                CharacterAssetReference asset = instance.characterDirectorySO.GetCharacter(_rpgCharacter.CharacterStat.GetID());

                if (asset != null)
                {
                    CharacterModel newModel = await instance.characterDirectorySO.LoadCharacter(asset.ID);
                    instance.characterArr[(int)asset.ID] = newModel;

                    return newModel;
                }
            }
        }

        return null;
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
