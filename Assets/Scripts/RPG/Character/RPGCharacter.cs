using UnityEngine;
using System;
using System.Collections.Generic;
using RPG_Data;
using System.Threading.Tasks;

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

	// The character object reference
	[SerializeField] private Character character;

	// The character UI reference
	[SerializeField] private CharacterStatusUI characterUI;

	public RPG_Party CharacterParty => characterPartyType;
	public RPGStat CharacterStatSO => characterStatSO;
	public RPGStat CharacterStat => characterStat;
	public RPGCharacterInfo CharacterStatInfo => characterInfo;
	public Character Character => character;
	public CharacterStatusUI CharacterUI => characterUI;
	public string Name => characterStat != null ? characterStat.name : "NULL";

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

	public void SetModel(Character _character)
	{
		character = _character;
	}

	public void SetUI(CharacterStatusUI _UI)
	{
		characterUI = _UI;
	}
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

	public const int MIN_PARTY_NUMBER = 1;
	public const int MAX_PARTY_NUMBER = 4;

	[SerializeField] private List<RPGCharacter> partyCharacters = new List<RPGCharacter>();
	
	public RPG_Party PType { get; private set; }

	public Action OnPartyActive;

	public RPGParty(RPG_Party _type)
	{
		PType = _type;
	}

	public async Task InitializePartyMembers()
	{
		if (PType == RPG_Party.Neutral)
		{
			return;
		}

		foreach (var character in partyCharacters)
		{
			character.SetModel(await CombatCharacterManager.GetCharacter(character));
			CombatAnimationManager.AddAnimator(character.Character.Anim);
		}
	}

	public void UnloadPartyMembers()
	{
		foreach (var character in partyCharacters)
		{
			character.SetModel(null);
		}
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
				Debug.Log("Add Character: " + _character.CharacterStat.name);
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
			Debug.Log("Remove Character: " + _character.CharacterStat.name);
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
			RPGStat stat = _useCache ? character.CharacterStatSO : null;

			switch (PType)
			{
				case RPG_Party.Ally:

					if (!_useCache)
						stat = CharacterLibrary.GetPlayer(character.CharacterStatInfo.characterID);

					character.SetCharacter((RPGStat_Player)stat, RPG_Party.Ally);
					break;

				case RPG_Party.Enemy:

					if (!_useCache)
						stat = CharacterLibrary.GetEnemy(character.CharacterStatInfo.characterID);

					character.SetCharacter(stat, RPG_Party.Enemy);
					break;

				default:

					if (!_useCache)
						stat = CharacterLibrary.GetEnemy(character.CharacterStatInfo.characterID);

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
			int characterSpeed = partyCharacters[i].CharacterStat.GetSpeed();

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

	public RPGCharacter GetAnyCharacter()
	{
		System.Random rand = new System.Random();

		return partyCharacters[rand.Next(partyCharacters.Count)];
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
