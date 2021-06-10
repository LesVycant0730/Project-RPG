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

		instance = null;
	}
	#endregion

	public static void OnCombatActionEnter(Combat_Action _action)
	{
		if (instance)
		{
			instance.canvas.UpdateDescriptionBox(_action.ToString());
		}
	}

	public static void OnCombatSkillEnter(string _description = "")
	{
		if (instance)
		{
			instance.canvas.UpdateDescriptionBox(_description);
		}
	}

	public static void OnCombatSkillExit()
	{
		if (instance)
		{
			instance.canvas.ClearDescriptionBox();
		}
	}

	private void UpdateToCurrentCharacter(Character _character, RPG_Party _party)
	{
		if (_character != null)
		{
			if (_character.Model)
			{
				CharacterHighlight.SetParent(_character.Model.transform);

				Vector3 pos = new Vector3(_character.Model.transform.position.x, 0.01f, _character.Model.transform.position.z);

				CharacterHighlight.position = pos;

				highlightSprite.color = RPGParty.PartyColor(_party);
			}

			CharacterHighlight.SetActive(_character.Model != null);
		}

		canvas.OnTurnUpdate(_character, _party);
	}

	public static void DisableUpdate()
	{
		if (instance)
		{
			instance.CharacterHighlight.SetActive(false);
		}
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
			default:
				break;
		}

		OnSelectNormalAction.Invoke(false);
	}

	private void CombatDefaultUIAction()
	{
		OnSelectNormalAction.Invoke(true);
	}

	private void CombatDisabledUIAction()
	{
		OnSelectNormalAction.Invoke(false);
	}
}
