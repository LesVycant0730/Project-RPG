using System;
using System.Linq;
using UnityEngine;

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

	public RPGCharacter GetFastestCharacter(out CombatCharacter _combatCharacter)
	{
		RPGCharacter character = null;
		_combatCharacter = null;
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
				int charSpeed = loopChar.GetCharacterStat().GetSpeed();

				if (speed == charSpeed || charSpeed > speed)
				{
					character = loopChar;
				}
			}
		}

		if (character != null)
		{
			_combatCharacter = CombatCharacterManager.GetCharacter(character);
		}

		return character;
	}
	#endregion

	#region RPG Character (In Combat)
	[SerializeField] private CombatCharacter _currentCombatCharacter;
	public CombatCharacter CurrentCombatCharacter
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
					CombatUIManager.UpdateToCurrentCharacter(value.Model.transform, RPG_Data.RPG_Party.Ally);
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

    public void InitializeParty()
    {
        // First character will always be the fastest character
        CurrentCharacter = GetFastestCharacter(out CombatCharacter combatChar);
		CurrentCombatCharacter = combatChar;
		
		CustomLog.Log($"Fastest Character Update: {CurrentCharacter.GetCharacterStat().GetName()}");
	}
	#endregion

	public void Init()
	{

	}

	public void Run()
	{
		InitializeParty();
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
