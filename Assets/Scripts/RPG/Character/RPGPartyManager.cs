using RPG_Data;
using System;
using System.Linq;
using UnityEngine;

public class RPGPartyManager : GameplayBaseManager
{
	private static RPGPartyManager instance;

	#region RPG Party
	[SerializeField]
    private RPGParty[] rpgParty = new RPGParty[]
    {
        new RPGParty(RPG_Party.Ally),
        new RPGParty(RPG_Party.Enemy),
        new RPGParty(RPG_Party.Neutral)
    };

    public RPGParty CurrentParty { get; private set; }

    public RPGParty GetParty(RPG_Party _type)
	{
        return rpgParty.SingleOrDefault(rpgParty => rpgParty.PType == _type);
	}

	public static event Action<RPGCharacter, RPG_Party> OnCharacterTurn;
	#endregion

	#region RPG Character
	[SerializeField] public RPGCharacter CurrentRPGCharacter { get; private set; }

	private RPGCharacter GetFastestCharacter()
	{
		RPGCharacter character = null;
		int speed = 0;

		for (int i = 0; i < rpgParty.Length; i++)
		{
			RPGCharacter loopChar = rpgParty[i].GetFastestCharacter();

			if (loopChar == null || CurrentRPGCharacter == loopChar)
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

	public static RPGCharacter GetRandomOpponent(RPG_Party _party)
	{
		if (instance == null)
			return null;

		switch (_party)
		{
			case RPG_Party.Ally:
				return instance.GetParty(RPG_Party.Enemy).GetAnyCharacter();
			case RPG_Party.Enemy:
				return instance.GetParty(RPG_Party.Ally).GetAnyCharacter();
		}

		return null;
	}

	public static int GetCharacterIndex(RPGCharacter _character)
	{
		if (instance == null)
			return -1;

		switch (_character.CharacterParty)
		{
			case RPG_Party.Ally:
				return instance.GetParty(RPG_Party.Ally).GetCharacterIndex(_character);
			case RPG_Party.Enemy:
				return instance.GetParty(RPG_Party.Enemy).GetCharacterIndex(_character);
		}

		return -1;
	}
	#endregion

	#region RPG Character (In Combat)
	[SerializeField] private Character _currentCombatCharacter;
	public Character CurrentCombatCharacter
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
					OnCharacterTurn?.Invoke(CurrentRPGCharacter, value.Party);
				}
			}
		}
	}
	#endregion

	#region Party Action (Unused)
	public void AddActionToParty(RPG_Party _type, Action _action)
	{
        RPGParty party = GetParty(_type);

        if (party != null)
        {
            party.AddActionToParty(_action);
        }
    }

    public void ActivateParty(RPG_Party _type)
	{
        RPGParty party = GetParty(_type);

        if (party != null)
		{
            CurrentParty = party;

            CurrentParty.SetPartyActive();
		} 
	}
	#endregion

	#region Party Action
	[ContextMenu("Test Setup")]
	private async void SetupParty()
	{
		foreach (var party in rpgParty)
		{
			await party.InitializePartyMembers();
		}

		UpdateCharacterTurn();
	}

	private async void UpdateCharacterTurn()
	{
		// First character will always be the fastest character
		CurrentRPGCharacter = GetFastestCharacter();

		if (CurrentRPGCharacter != null)
		{
			CurrentCombatCharacter = await CombatCharacterManager.GetCharacter(CurrentRPGCharacter);
		}

		CustomLog.Log($"Fastest Character Update: {CurrentRPGCharacter.CharacterStat.GetName()}");
	}

	private void UnloadParty()
	{
		foreach (var party in rpgParty)
		{
			party.UnloadPartyMembers();
		}

		CurrentRPGCharacter = null;
		CurrentCombatCharacter = null;
	}
	#endregion

	protected override void Init()
	{
		base.Init();

		CombatController.OnTurnEnd += UpdateCharacterTurn;
		CombatAction.Action_Next += UpdateCombatState;
		//CombatAction.Action_Prev += UpdateCombatState;
		//CombatAction.Action_Reset += ResetCombatState;
		instance = this;
	}

	protected override void Run()
	{
		base.Run();
		SetupParty();
	}

	protected override void Exit()
	{
		base.Exit();

		CombatController.OnTurnEnd -= UpdateCharacterTurn;
		CombatAction.Action_Next -= UpdateCombatState;

		UnloadParty();
		instance = null;
	}

	private void Reset()
	{
		rpgParty = new RPGParty[]
		{
			new RPGParty(RPG_Party.Ally),
			new RPGParty(RPG_Party.Enemy),
			new RPGParty(RPG_Party.Neutral)
		};
	}

	private void UpdateCombatState(Combat_Action _action)
	{
		if (CurrentCombatCharacter != null)
		{
			//CurrentCombatCharacter.CombatAnimate(AnimationTypes.CombatAnimationStatus.Normal_Attack);
		}
	}

#if UNITY_EDITOR
	public void DefaultPartySOStats(bool _useCache)
	{
		Array.ForEach(rpgParty, party => 
		{
			party.DefaultAsSO(_useCache);
		});

		UnityEditor.EditorUtility.SetDirty(this);
	}
#endif
}
