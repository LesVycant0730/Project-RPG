using System;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class RPGAction : MonoBehaviour
{
	public abstract void NextAction<ActionType>(ActionType _actionType) where ActionType : Enum;
	public abstract void BackAction<ActionType>(ActionType _actionType) where ActionType : Enum;
	public abstract void NextElementAction();
	public abstract void PrevElementAction();
	public abstract void RunResetAction();
	public abstract void ClearAllActions();

	protected T GetActionType<T>(object type) where T : Enum
	{
		if (typeof(T) != type.GetType())
		{
			Debug.LogError("Expected type: " + typeof(T).ToString() + ", while received type: " + type.GetType());

			return default;
		}

		return (T)Enum.ToObject(typeof(T), type);
	}
}
