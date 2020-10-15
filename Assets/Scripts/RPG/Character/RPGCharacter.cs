using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class RPGCharacter
{
	public enum CharacterType
	{
		Ally,
		Enemy,
		Neutral
	}

	// The type/stance for the character
	private CharacterType characterType; 

	// The SO reference for the RPG Stat, only reference, is not allowed to modified.
	[SerializeField] private RPGStat characterStatSO;

	// The direct reference for the RPG Stat, the information that will modified and saved.
	private RPGStat characterStat;

	// The separate class reference for RPG Stat, the information that will be modified and referred throughout but will not be saved.
	[SerializeField] private RPGCharacterInfo characterInfo;

	/// <summary>
	/// <para> Setup character stat (Prohibited to modify except on actual stat allocation/upgrades) </para>
	/// <para> Setup character stat info (Allowed to modify in any scenarios, combat, UI etc.) </para>
	/// <para> Only initialized once at the beginning </para>
	/// </summary>
	public void SetCharacter(RPGStat _stat, CharacterType _type)
	{
		characterStat = _stat;

		characterType = _type;

		characterInfo = new RPGCharacterInfo(_stat);
	}

	public RPGStat GetCharacterStat() => characterStat;

	public RPGCharacterInfo GetCharacterStatInfo() => characterInfo;
}

[Serializable]
public class RPGParty
{
	public enum RPGPartyAction
	{
		Success,
		Party_Full,
		Invalid_Character,
		Duplicate,
		Failed
	}

	public enum RPGPartyType
	{
		Ally,
		Enemy,
		Neutral
	}

	public const int MIN_PARTY_NUMBER = 1;
	public const int MAX_PARTY_NUMBER = 5;

	private HashSet<RPGCharacter> partyCharacters = new HashSet<RPGCharacter>();
	
	public RPGPartyType PartyType { get; private set; }

	public Action OnPartyActive;

	public RPGParty(RPGPartyType _type)
	{
		PartyType = _type;
	}

	public void AddPartyMember(RPGCharacter _character, out RPGPartyAction _status)
	{
		if (partyCharacters.Contains(_character))
		{
			Debug.Log("The character is already in party.");
			_status = RPGPartyAction.Duplicate;

			return;
		}

		if (partyCharacters.Count >= 5)
		{
			Debug.Log("The party is full.");
			_status = RPGPartyAction.Party_Full;
		}
		else
		{
			if (_character == null)
			{
				Debug.Log("The character to add is invalid or null.");
				_status = RPGPartyAction.Invalid_Character;
			}
			else
			{
				Debug.Log("Add Character: " + _character.GetCharacterStat().name);
				_status = RPGPartyAction.Success;
				partyCharacters.Add(_character);
			}
		}
	}

	public void RemovePartyMember(RPGCharacter _character, out RPGPartyAction _status)
	{
		if (_character == null)
		{
			Debug.Log("The character to remove is invalid or null.");
			_status = RPGPartyAction.Invalid_Character;
			return;
		}

		if (partyCharacters.Contains(_character))
		{
			Debug.Log("Remove Character: " + _character.GetCharacterStat().name);
			_status = RPGPartyAction.Success;
			partyCharacters.Remove(_character);
		}
		else
		{
			_status = RPGPartyAction.Invalid_Character;
		}
	}

	public void AddActionToParty(Action _action)
	{
		OnPartyActive += _action;
	}

	public void SetPartyActive()
	{
		OnPartyActive?.Invoke();
	}
}
