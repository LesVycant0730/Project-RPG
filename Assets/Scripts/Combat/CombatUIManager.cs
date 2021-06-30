using RPG_Data;
using System;
using UnityEngine;

public class CombatUIManager : GameplayBaseManager
{
	private static CombatUIManager instance;

    // Assign through Inspector
	[SerializeField] private CombatUICanvas canvas;
	[SerializeField] private GameObject characterHighlightPrefab;

	private SpriteRenderer highlightSprite;
	private Transform _characterHighlight;
	private Transform CharacterHighlight
	{
		get
		{
			if (!_characterHighlight)
			{
				_characterHighlight = Instantiate(characterHighlightPrefab, transform).transform;
				highlightSprite = _characterHighlight.GetComponent<SpriteRenderer>();
			}

			return _characterHighlight;
		}
	}

	public static event Action<bool> OnSelectNormalAction;

	#region Interface
	protected override void Init()
	{
		base.Init();

		instance = this;
		
		// Party Action
		RPGPartyManager.OnCharacterTurn += UpdateToCurrentCharacter;

		// Combat UI Action
		CombatAction.Action_Next += CombatUIAction;
		CombatAction.Action_Prev += CombatUIAction;
		CombatAction.Action_Reset += CombatDefaultUIAction;

		canvas.OnCombatToggle(false);
	}

	protected override void Run()
	{
		base.Run();
	}

	protected override void Exit()
	{
		base.Exit();

		// Party Action
		RPGPartyManager.OnCharacterTurn -= UpdateToCurrentCharacter;

		// Combat UI Action
		CombatAction.Action_Next -= CombatUIAction;
		CombatAction.Action_Prev -= CombatUIAction;
		CombatAction.Action_Reset -= CombatDefaultUIAction;

		DisableUpdate();
		CombatDisabledUIAction();
		canvas.OnCombatDefault();
		canvas.OnCombatExit();

		instance = null;
	}
	#endregion

	#region Global Methods
	public static void OnCombatActionEnter(Combat_Action _action)
	{
		if (instance)
		{
			instance.canvas.UpdateDescriptionBox(DescriptionInfoLibrary.GetDescription(_action));
		}
	}

	public static void OnCombatActionEnter(AnimationTypes.CombatAnimationStatus _status)
	{
		if (instance)
		{
			instance.canvas.UpdateDescriptionBox(DescriptionInfoLibrary.GetDescription(_status));
		}
	}

	public static void OnCombatSkillEnter(string _description)
	{
		if (instance)
		{
			instance.canvas.UpdateDescriptionBox(_description);
		}
	}

	public static void OnCombatActionExit()
	{
		if (instance)
		{
			instance.canvas.ClearDescriptionBox();
		}
	}

	public static void AddCombatLog(GameInfo.CombatLogType _type, params object[] _param)
	{
		if (instance)
		{
			instance.canvas.UpdateCombatLogBox(DescriptionInfoLibrary.GetCombatLog(_type, _param));
		}
	}

	public static void OnCombatActionRegistered()
	{
		if (instance)
		{
			instance.canvas.DisableActionButtons();
			instance.canvas.ClearDescriptionBox();
		}
	}

	public static void UpdateCharacterStatusUI(RPGCharacter _character)
	{
		if (instance)
		{
			instance.canvas.UpdateCharacterInfo(_character);
		}
	}

	public static void DisableUpdate()
	{
		if (instance)
		{
			instance.CharacterHighlight.SetActive(false);
		}
	}
	#endregion

	private void UpdateToCurrentCharacter(RPGCharacter _character, RPG_Party _party)
	{
		if (_character != null)
		{
			if (_character.Character.Model)
			{
				Transform model = _character.Character.Model.transform;

				CharacterHighlight.SetParent(model);

				Vector3 pos = new Vector3(model.position.x, 0.01f, model.position.z);

				CharacterHighlight.position = pos;

				highlightSprite.color = RPGParty.PartyColor(_party);
			}

			CharacterHighlight.SetActive(_character.Character.Model != null);
		}

		canvas.OnTurnUpdate(_character, _party);
	}


	private void CombatUIAction(Combat_Action _actionType)
	{
		// First priority, build character and enemy reference, inject it into RPGParty (Ready) 
		// Second, update Skill Selection based on the player skill list
		// Update enemy target highlight
		// Update RPG characters stat UI, for inspection

		// Enable selected UI based on current action type
		canvas.ToggleUIHolder(_actionType, true, out bool CanToggle);

		if (!CanToggle)
		{
			Debug.LogError("Missing UI Holder on action type: " + _actionType);
			return;
		}

		switch (_actionType)
		{
			case Combat_Action.Assist:
				break;
			case Combat_Action.Default:
				CombatDefaultUIAction();
				return;
			case Combat_Action.Target_Attack:
				return;
			case Combat_Action.Skill_Selection:
				break;
			case Combat_Action.Item_Selection:
				break;
			case Combat_Action.Target_Check:
				break;
			case Combat_Action.Target_Selection_Enemy:
				break;
			case Combat_Action.Target_Selection_Ally:
				break;
			case Combat_Action.Target_Assist_Ally:
				break;
			case Combat_Action.Defend:
				break;
			case Combat_Action.Escape:
				break;
			case Combat_Action.Demo:
				break;
			default:
				break;
		}

		OnSelectNormalAction.Invoke(false);
	}

	private void CombatDefaultUIAction()
	{
		print("Default");
		OnSelectNormalAction.Invoke(true);
	}

	private void CombatDisabledUIAction()
	{
		OnSelectNormalAction.Invoke(false);
	}
}
