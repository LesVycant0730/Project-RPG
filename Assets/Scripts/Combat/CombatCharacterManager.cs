using RPG_Data;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class CombatCharacterManager : GameplayBaseManager
{
    private static CombatCharacterManager instance;

    [SerializeField] private CharacterPrefabDirectorySO characterDirectorySO;

    [SerializeField] private Character[] _characterArr;

    private Character[] CharacterArr
	{
        get
		{
            if (Utility.IsNullOrEmpty(_characterArr))
			{
                _characterArr = new Character[Utility.GetEnumLength<Character_ID>()];

                for (int i = 0; i < _characterArr.Length; i++)
				{
                    _characterArr[i] = new Character((Character_ID)i);
				}
            }

            return _characterArr;
		}
	}

    public static event Action<Character, RPG_Party> OnNewCharacterAdded;

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

    public async static Task<Character> GetCharacter(RPGCharacter _rpgCharacter)
	{
        if (_rpgCharacter == null)
		{
            throw new Exception("Attempt to Get Character from an empty reference.");
		}

        if (instance != null)
        {
            Character_ID id = _rpgCharacter.CharacterStat.GetID();
            Character character = Array.Find(instance.CharacterArr, x => x.IsSameCharacter(id));
            
            if (character != null && character.IsRegistered())
            {
                return character;
            }
            else
			{
                CharacterAssetReference asset = instance.characterDirectorySO.GetCharacter(_rpgCharacter.CharacterStat.GetID());

                if (asset != null)
                {
                    // Character Party
                    RPG_Party party = _rpgCharacter.GetCharacterParty;

                    // Get Position and Rotation based on the party
                    Vector3 pos = CombatArea.GetPositionAndRotation(party, out Quaternion rot);

                    // Load a new character
                    Character newCharacter = await instance.characterDirectorySO.LoadCharacter(asset.ID, party, pos, rot);

                    // Update loaded character reference to the array
                    instance.CharacterArr[(int)asset.ID] = newCharacter;  

                    // Invoke action for new added character
                    OnNewCharacterAdded?.Invoke(newCharacter, _rpgCharacter.GetCharacterParty);

                    return newCharacter;
                }
            }
        }

        return null;
    }

    protected override void Init()
	{
        base.Init();
        instance = this;
        print("Init");
    }

    protected override void Run()
	{
        base.Run();
	}

    protected override void Exit()
	{
		base.Exit();
	}
}
