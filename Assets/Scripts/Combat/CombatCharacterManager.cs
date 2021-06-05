using RPG_Data;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;

public class CombatCharacterManager : MonoBehaviour, IManager
{
    private static CombatCharacterManager instance;

    [SerializeField] private CharacterPrefabDirectorySO characterDirectorySO;

    [SerializeField] private CharacterModel[] _characterArr;

    private CharacterModel[] CharacterArr
	{
        get
		{
            if (Utility.IsNullOrEmpty(_characterArr))
			{
                _characterArr = new CharacterModel[Utility.GetEnumLength<Character_ID>()];

                for (int i = 0; i < _characterArr.Length; i++)
				{
                    _characterArr[i] = new CharacterModel((Character_ID)i);
				}
            }

            return _characterArr;
		}
	}

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
            CharacterModel model = Array.Find(instance.CharacterArr, x => x.IsUsing);

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
                    instance.CharacterArr[(int)asset.ID] = newModel;

                    return newModel;
                }
            }
        }

        return null;
    }

	public void Init()
	{
        instance = this;
    }

    public void Run()
	{
        
	}

	public void Exit()
	{
        
	}
}
