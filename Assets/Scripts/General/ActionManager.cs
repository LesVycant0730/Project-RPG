using RPG_Data;
using UnityEngine;

[RequireComponent (typeof(CombatAction))]
public class ActionManager : MonoBehaviour
{
	public static ActionManager Instance { get; private set; }

    [SerializeField] private CombatAction action_Combat;

	#region Mono
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

        action_Combat = GetComponent<CombatAction>();
	}
	#endregion

	#region Action
	public void ActionNext(Combat_Action _actionType)
	{
		CustomLog.Log("Run Next Action from: " + CombatManager.CurrentAction + ", to: " + _actionType);
		action_Combat.NextAction(_actionType);
	}

	public void ActionNext()
	{
		var nextAction = CombatManager.GetNextAction(CombatManager.CurrentAction, out bool hasAction);

		if (nextAction != Combat_Action.NONE && hasAction)
		{
			if (CombatManager.CurrentAction == nextAction)
			{
				return;
			}

			CustomLog.Log("Run Next Action from: " + CombatManager.CurrentAction + ", to: " + nextAction);
			action_Combat.NextAction(nextAction);
		}
	}

	public void Action_Back(Combat_Action _actionType)
	{
		CustomLog.Log("Run Previous Action from: " + CombatManager.CurrentAction + ", to: " + _actionType);
		action_Combat.BackAction(_actionType);
	}

	public void ActionBack()
	{
		var prevAction = CombatManager.GetPreviousAction(out bool hasAction);

		if (prevAction != Combat_Action.NONE && hasAction)
		{
			// If same action, skip it
			if (CombatManager.CurrentAction == prevAction)
				return;

			// Check if previous action is back to custom action instead of prefix previous action
			var finalPrevAction = prevAction == Combat_Action.Previous_Action ? CombatManager.PreviousAction : prevAction;

			action_Combat.BackAction(finalPrevAction);
		}
		else if (prevAction == Combat_Action.NONE)
		{
			action_Combat.BackAction(prevAction);
		}
	}

	public void ActionCheckAndRun(Combat_Action _action)
	{
		if (CombatManager.CurrentAction == _action)
		{
			if (_action == Combat_Action.Default)
			{
				ActionNext(Combat_Action.Default);
			}
			else
				ActionBack();
		}
		else
		{
			ActionNext(_action);
		}
	}

	// Reset the action state to default
	public void ResetAction()
	{
		action_Combat.RunResetAction();
	}
	#endregion
}
