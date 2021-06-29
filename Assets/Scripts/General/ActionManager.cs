using RPG_Data;
using UnityEngine;

[RequireComponent (typeof(CombatAction))]
public class ActionManager : MonoBehaviour
{
	public static ActionManager Instance { get; private set; }

    [SerializeField] private CombatAction action_Combat;

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

	public void ActionNext(Combat_Action _actionType)
	{
		CustomLog.Log("Run Next Action from: " + CombatManager.CurrentAction + ", to: " + _actionType);
		action_Combat.RunCombatAction(_actionType);
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
			action_Combat.RunCombatAction(nextAction);
		}
	}

	public void Action_Back(Combat_Action _actionType)
	{
		CustomLog.Log("Run Previous Action from: " + CombatManager.CurrentAction + ", to: " + _actionType);
		action_Combat.CancelCombatAction(_actionType);
	}

	public void ActionBack()
	{
		var prevAction = CombatManager.GetPreviousAction(out bool hasAction);

		if (prevAction != Combat_Action.NONE && hasAction)
		{
			if (CombatManager.CurrentAction == prevAction)
			{
				return;
			}

			CustomLog.Log("Run Back Action from: " + CombatManager.CurrentAction + ", to: " + prevAction);
			action_Combat.CancelCombatAction(prevAction);
		}
		else if (prevAction == Combat_Action.NONE)
		{
			action_Combat.CancelCombatAction(prevAction);
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

	public void ResetAction()
	{
		action_Combat.RunResetAction();
	}
}
