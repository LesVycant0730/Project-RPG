using RPG_Data;
using UnityEngine;

public class CombatCharacterManager : MonoBehaviour, IManager
{
    private static CombatCharacterManager instance;

    [SerializeField] private CharacterPrefabDirectorySO characterDirectorySO;

    public static CombatCharacter GetCharacter(Character_ID _id)
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

    public static CombatCharacter GetCharacter(RPGCharacter _rpgCharacter)
	{
        if (_rpgCharacter == null)
		{
            CustomLog.Log("Attempt to Get Character from an empty reference.");
            return null;
		}

        if (instance != null)
        {
            return instance.characterDirectorySO.GetCharacter(_rpgCharacter.GetCharacterStat().GetID());
        }
        else
        {
            return null;
        }
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
