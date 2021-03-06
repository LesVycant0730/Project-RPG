using RPG_Data;
using UnityEngine;

public sealed class CombatManager : GameplayBaseManager
{
    public static CombatManager Instance { get; private set; }

	public static Combat_Action CurrentAction { get; private set; }

	[SerializeField] private CombatFlow combatFlowSetting;

	private static RPGAction OnTargetAlly;
	private static RPGAction OnTargetEnemy;

	protected override void Awake()
	{
		base.Awake();
		Instance = this;
	}

	protected override void Init()
	{
		base.Init();
		CombatAction.Action_Next += UpdateCombatState;
		CombatAction.Action_Prev += UpdateCombatState;
		CombatAction.Action_Reset += ResetCombatState;
	}

	protected override void Run()
	{
		base.Run();
	}

	protected override void Exit()
	{
		base.Exit();
		CombatAction.Action_Next -= UpdateCombatState;
		CombatAction.Action_Prev -= UpdateCombatState;
		CombatAction.Action_Reset -= ResetCombatState;
	}

	private void UpdateCombatState(Combat_Action _action)
	{
		CurrentAction = _action;

		// Expand action here
		switch (CurrentAction)
		{
			case Combat_Action.Default:
				break;
			case Combat_Action.Target_Attack:
				break;
			default:
				break;
		}
	}

	private void ResetCombatState()
	{
		UpdateCombatState(Combat_Action.Default);
	}

	#region Get Action State
	public static Combat_Action GetNextAction(Combat_Action _currentAction, out bool _hasNextAction)
	{
		if (Instance != null)
		{
			return Instance.combatFlowSetting.GetNextAction(_currentAction, out _hasNextAction);
		}

		_hasNextAction = false;

		return Combat_Action.NONE;
	}

	public static Combat_Action GetPreviousAction(out bool _hasPrevAction)
	{
		if (Instance != null)
		{
			return Instance.combatFlowSetting.GetPrevAction(CurrentAction, out _hasPrevAction);
		}

		_hasPrevAction = false;

		return Combat_Action.NONE;
	}
	#endregion
}
