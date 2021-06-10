using UnityEngine;
using System;

public class GameplayController : MonoBehaviour
{
	private bool isInitialized;

	public static event Action OnManagerInit;
	public static event Action OnManagerRun;
	public static event Action OnManagerExit;

	// Temp use on Toggle Combat button
	public void OnCombatToggle()
	{
		isInitialized = !isInitialized;

		if (isInitialized)
		{
			OnManagerInit?.Invoke();
			OnManagerRun?.Invoke();

			ActionManager.Instance.ResetAction();
		}
		else
		{
			OnManagerExit?.Invoke();
		}
	}
}
