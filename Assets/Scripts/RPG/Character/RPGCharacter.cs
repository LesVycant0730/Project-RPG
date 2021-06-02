﻿using UnityEngine;
using System;
using System.Collections.Generic;
using RPG_Data;

[Serializable]
public class RPGCharacter
{
	// The party type for the character
	[SerializeField] private RPG_Party characterPartyType; 

	// The SO reference for the RPG Stat, only reference, is not allowed to modified.
	[SerializeField] private RPGStat characterStatSO;

	// The direct reference for the RPG Stat, the information that will modified and saved.
	[SerializeField] private RPGStat characterStat;

	// The separate class reference for RPG Stat, the information that will be modified and referred throughout but will not be saved.
	[SerializeField] private RPGCharacterInfo characterInfo;

	/// <summary>
	/// <para> Setup character stat (Prohibited to modify except on actual stat allocation/upgrades) </para>
	/// <para> Setup character stat info (Allowed to modify in any scenarios, combat, UI etc.) </para>
	/// <para> Only initialized once at the beginning </para>
	/// </summary>
	public void SetCharacter(RPGStat _stat, RPG_Party _type)
	{
		characterStat = _stat;

		characterPartyType = _type;

		characterInfo = new RPGCharacterInfo(_stat);
	}

	public RPG_Party GetCharacterParty => characterPartyType;

	public RPGStat GetCharacterStatSO => characterStatSO;

	public RPGStat GetCharacterStat() => characterStat;

	public RPGCharacterInfo GetCharacterStatInfo() => characterInfo;
}

[Serializable]
public class RPGParty
{
	public enum PartyAction
	{
		Success,
		Party_Full,
		Invalid_Character,
		Duplicate,
		Failed
	}

	public enum PartyType
	{
		Ally,
		Enemy,
		Neutral
	}

	public const int MIN_PARTY_NUMBER = 1;
	public const int MAX_PARTY_NUMBER = 4;

	[SerializeField] private List<RPGCharacter> partyCharacters = new List<RPGCharacter>();
	
	public PartyType PType { get; private set; }

	public Action OnPartyActive;

	public RPGParty(PartyType _type)
	{
		PType = _type;
	}

	public void AddPartyMember(RPGCharacter _character, out PartyAction _status)
	{
		if (partyCharacters.Contains(_character))
		{
			Debug.Log("The character is already in party.");
			_status = PartyAction.Duplicate;

			return;
		}

		if (partyCharacters.Count >= 4)
		{
			Debug.Log("The party is full.");
			_status = PartyAction.Party_Full;
		}
		else
		{
			if (_character == null)
			{
				Debug.Log("The character to add is invalid or null.");
				_status = PartyAction.Invalid_Character;
			}
			else
			{
				Debug.Log("Add Character: " + _character.GetCharacterStat().name);
				_status = PartyAction.Success;
				partyCharacters.Add(_character);
			}
		}
	}

	public void RemovePartyMember(RPGCharacter _character, out PartyAction _status)
	{
		if (_character == null)
		{
			Debug.Log("The character to remove is invalid or null.");
			_status = PartyAction.Invalid_Character;
			return;
		}

		if (partyCharacters.Contains(_character))
		{
			Debug.Log("Remove Character: " + _character.GetCharacterStat().name);
			_status = PartyAction.Success;
			partyCharacters.Remove(_character);
		}
		else
		{
			_status = PartyAction.Invalid_Character;
		}
	}

	public void DefaultAsSO(bool _useCache)
	{
		partyCharacters.ForEach(character =>
		{
			RPGStat stat = _useCache ? character.GetCharacterStatSO : null;

			switch (PType)
			{
				case PartyType.Ally:

					if (!_useCache)
						stat = CharacterLibrary.GetPlayer(character.GetCharacterStatInfo().characterID);

					character.SetCharacter((RPGStat_Player)stat, RPG_Party.Ally);
					break;

				case PartyType.Enemy:

					if (!_useCache)
						stat = CharacterLibrary.GetEnemy(character.GetCharacterStatInfo().characterID);

					character.SetCharacter(stat, RPG_Party.Enemy);
					break;

				default:

					if (!_useCache)
						stat = CharacterLibrary.GetEnemy(character.GetCharacterStatInfo().characterID);

					character.SetCharacter(stat, RPG_Party.Neutral);
					break;
			}
		});
	}

	public void AddActionToParty(Action _action)
	{
		OnPartyActive -= _action;
		OnPartyActive += _action;
	}

	public void SetPartyActive()
	{
		OnPartyActive?.Invoke();
	}

	#region Value Return
	public RPGCharacter GetFastestCharacter()
	{
		partyCharacters.RemoveAll(character => character == null);

		RPGCharacter character = null;

		int _speed = 0;

		for (int i = 0; i < partyCharacters.Count; i++)
		{
			int characterSpeed = partyCharacters[i].GetCharacterStat().GetSpeed();

			// If character speed is move than current speed, set it as the highest
			if (characterSpeed > _speed)
			{
				_speed = characterSpeed;

				character = partyCharacters[i];
			}
			// If character speed is default 0, set it as default if there's no current character
			else if (characterSpeed == 0 && character == null)
			{
				character = partyCharacters[i];
			}
		}

		return character;
	}

	public static Color PartyColor(RPG_Party _party)
	{
		switch (_party)
		{
			case RPG_Party.Ally: return Color.white;
			case RPG_Party.Enemy: return Color.red;
			default: return Color.green;
		}
	}
	#endregion
}