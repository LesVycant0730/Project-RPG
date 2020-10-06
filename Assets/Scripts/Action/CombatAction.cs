using RPG_Data;
using System;
using UnityEngine;

public class CombatAction : RPGAction
{
	public static Action<Combat_Action> Action_Next;
	public static Action<Combat_Action> Action_Prev;

	/// <summary>
	/// Run action based on the enum parameter
	/// </summary>
	protected override void InvokeActions<ActionType>(ActionType _actionType)
	{
		Combat_Action action = GetActionType<Combat_Action>(_actionType);

		Debug.Log("Run Action: " + action);

		Action_Next?.Invoke(action);
	}

	/// <summary>
	/// Cancel action based on the enum parameter
	/// </summary>
	protected override void CancelActions<ActionType>(ActionType _actionType)
	{
		Combat_Action action = GetActionType<Combat_Action>(_actionType);

		Debug.Log("Cancel Action: " + action);

		Action_Prev?.Invoke(action);
	}

	public override void RunDefaultAction()
	{
		Action_Next?.Invoke(Combat_Action.Default);
	}

	public override void ClearAllActions()
	{
		Action_Next = null;
		Action_Prev = null;
	}

	public void RunCombatAction(Combat_Action _actionType)
	{
		InvokeActions(_actionType);
	}
}
