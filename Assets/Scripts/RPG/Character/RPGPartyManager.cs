using System;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

public class RPGPartyManager : MonoBehaviour, IManager
{
	#region RPG Party
	[SerializeField]
    private RPGParty[] rpgParty = new RPGParty[]
    {
        new RPGParty(RPGParty.PartyType.Ally),
        new RPGParty(RPGParty.PartyType.Enemy),
        new RPGParty(RPGParty.PartyType.Neutral)
    };

    public RPGParty CurrentParty { get; private set; }

    public RPGParty GetParty(RPGParty.PartyType _type)
	{
        return rpgParty.SingleOrDefault(rpgParty => rpgParty.PType == _type);
	}
	#endregion

	#region RPG Character
	[SerializeField] public RPGCharacter CurrentCharacter { get; private set; }

	public RPGCharacter GetFastestCharacter()
	{
		RPGCharacter character = null;
		int speed = 0;

		for (int i = 0; i < rpgParty.Length; i++)
		{
			RPGCharacter loopChar = rpgParty[i].GetFastestCharacter();

			if (loopChar == null)
			{
				continue;
			}
			else
			{
				int charSpeed = loopChar.CharacterStat.GetSpeed();

				if (speed == charSpeed || charSpeed > speed)
				{
					character = loopChar;
				}
			}
		}
		return character;
	}
	#endregion

	#region RPG Character (In Combat)
	[SerializeField] private CharacterModel _currentCombatCharacter;
	public CharacterModel CurrentCombatCharacter
	{
		get
		{
			return _currentCombatCharacter;
		}
		private set
		{
			if (_currentCombatCharacter != value)
			{
				_currentCombatCharacter = value;

				if (value != null)
				{
					CombatUIManager.UpdateToCurrentCharacter(value, RPG_Data.RPG_Party.Ally);
				}
			}
		}
	}
	#endregion

	#region Party Action
	public void AddActionToParty(RPGParty.PartyType _type, Action _action)
	{
        RPGParty party = GetParty(_type);

        if (party != null)
        {
            party.AddActionToParty(_action);
        }
    }

    public void ActivateParty(RPGParty.PartyType _type)
	{
        RPGParty party = GetParty(_type);

        if (party != null)
		{
            CurrentParty = party;

            CurrentParty.SetPartyActive();
		} 
	}

    public async void InitializeParty()
    {
		foreach (var party in rpgParty)
		{
			await party.InitializePartyMembers();
		}

        // First character will always be the fastest character
        CurrentCharacter = GetFastestCharacter();

		if (CurrentCharacter != null)
		{
			CurrentCombatCharacter = await CombatCharacterManager.GetCharacter(CurrentCharacter);
		}
		
		CustomLog.Log($"Fastest Character Update: {CurrentCharacter.CharacterStat.GetName()}");
	}
	#endregion

	public void Init()
	{
		InitializeParty();

	}

	public void Run()
	{
	}

	public void Exit()
	{
		CurrentCharacter = null;
		CurrentCombatCharacter = null;
	}

	private void Reset()
	{
		rpgParty = new RPGParty[]
		{
			new RPGParty(RPGParty.PartyType.Ally),
			new RPGParty(RPGParty.PartyType.Enemy),
			new RPGParty(RPGParty.PartyType.Neutral)
		};
	}

	public void DefaultPartySOStats(bool _useCache)
	{
		Array.ForEach(rpgParty, party => 
		{
			party.DefaultAsSO(_useCache);
		});

		UnityEditor.EditorUtility.SetDirty(this);
	}
}
