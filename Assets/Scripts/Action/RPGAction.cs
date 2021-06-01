using System;
using UnityEngine;

public abstract class RPGAction : MonoBehaviour
{
	protected abstract void InvokeActions<ActionType>(ActionType _actionType) where ActionType : Enum;
	protected abstract void CancelActions<ActionType>(ActionType _actionType) where ActionType : Enum;
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
