using RPG_Data;
using System;

public class CombatAction : RPGAction
{
	// General action flow navigation
	public static event Action<Combat_Action> Action_Next;
	public static event Action<Combat_Action> Action_Prev;

	// Combat target selection action
	public static event Action Action_NextTarget;
	public static event Action Action_PrevTarget;

	// Reset/Confirm action
	public static event Action Action_Reset;
	public static event Action Action_Confirmed;

	/// <summary>
	/// Run action based on the enum parameter
	/// </summary>
	public override void NextAction<ActionType>(ActionType _actionType)
	{
		Combat_Action action = GetActionType<Combat_Action>(_actionType);

		Action_Next?.Invoke(action);
	}

	/// <summary>
	/// Cancel action based on the enum parameter
	/// </summary>
	public override void BackAction<ActionType>(ActionType _actionType)
	{
		Combat_Action action = GetActionType<Combat_Action>(_actionType);

		Action_Prev?.Invoke(action);
	}

	public override void RunResetAction()
	{
		Action_Reset?.Invoke();
	}

	public override void ClearAllActions()
	{
		Action_Next = null;
		Action_Prev = null;
	}

	/// <summary>
	/// Get the next target from the combat action
	/// </summary>
	public override void NextElementAction()
	{
		Action_NextTarget?.Invoke();
	}

	/// <summary>
	/// Get the previous target from the combat action
	/// </summary>
	public override void PrevElementAction()
	{
		Action_PrevTarget?.Invoke();
	}


	public void ConfirmTargetCombatAction()
	{
		Action_Confirmed?.Invoke();
	}
}
