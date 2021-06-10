using RPG_Data;
using System;
using System.Linq;
using UnityEngine;
using System.Collections;

public class RPGPartyManager : GameplayBaseManager
{
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
					CombatUIManager.UpdateToCurrentCharacter(value, value.Party);
				}
			}
		}
	}
	#endregion

	#region Party Action
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

    private async void SetupParty()
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

	private void UnloadParty()
	{
		foreach (var party in rpgParty)
		{
			party.UnloadPartyMembers();
		}

		CurrentCharacter = null;
		CurrentCombatCharacter = null;
	}
	#endregion

	protected override void Init()
	{
		base.Init();

		CombatAction.Action_Next += UpdateCombatState;
		//CombatAction.Action_Prev += UpdateCombatState;
		//CombatAction.Action_Reset += ResetCombatState;
	}

	protected override void Run()
	{
		base.Run();
		SetupParty();
	}

	protected override void Exit()
	{
		base.Exit();

		CombatAction.Action_Next -= UpdateCombatState;

		UnloadParty();
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
			CurrentCombatCharacter.CombatAnimate(AnimationTypes.CombatAnimationStatus.Normal_Attack);
		}
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
