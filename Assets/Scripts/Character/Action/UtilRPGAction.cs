using RPG_Data;

public static class UtilRPGAction
{
	public static IRPGCharacterAction GetActionInterface(Combat_Action _action)
	{
		switch (_action)
		{
			case Combat_Action.Assist:
				return new ChAction_Assist();
			case Combat_Action.Defend:
				return new ChAction_Defend();
			default:
				return null;
		}
	}
}
