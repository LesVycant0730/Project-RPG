using RPG_Data;
using System;

public class CombatAction : RPGAction
{
	public static event Action<Combat_Action> Action_Next;
	public static event Action<Combat_Action> Action_Prev;
	public static event Action Action_Reset;

	/// <summary>
	/// Run action based on the enum parameter
	/// </summary>
	protected override void InvokeActions<ActionType>(ActionType _actionType)
	{
		Combat_Action action = GetActionType<Combat_Action>(_actionType);

		Action_Next?.Invoke(action);
	}

	/// <summary>
	/// Cancel action based on the enum parameter
	/// </summary>
	protected override void CancelActions<ActionType>(ActionType _actionType)
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

	public void RunCombatAction(Combat_Action _actionType)
	{
		print(_actionType);
		InvokeActions(_actionType);
	}

	public void CancelCombatAction(Combat_Action _actionType)
	{
		CancelActions(_actionType);
	}
}
