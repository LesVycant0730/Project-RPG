using RPG_Data;
using UnityEngine;

public sealed class CombatManager : MonoBehaviour, IManager
{
    public static CombatManager Instance { get; private set; }

	[SerializeField] private Combat_Action currentAction;

	private void Awake()
	{
		Instance = this;
	}

	public void Init()
	{
		print("Init combat manager");

		CombatAction.Action_Next += UpdateCombatState;
		CombatAction.Action_Prev += UpdateCombatState;
	}

	public void Exit()
	{
		print("Exit combat manager");

		CombatAction.Action_Next -= UpdateCombatState;
		CombatAction.Action_Prev -= UpdateCombatState;
	}

	private void UpdateCombatState(Combat_Action _action)
	{
		currentAction = _action;

		// Expand action here
		switch (currentAction)
		{
			case Combat_Action.Default:
				break;
			default:
				break;
		}
	}
}
