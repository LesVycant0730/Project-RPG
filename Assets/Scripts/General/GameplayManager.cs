using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
	private IManager[] Managers;
	private bool isInitialized;

	// Temp use on Toggle Combat button
	public void OnCombatToggle()
	{
		if (Managers == null)
		{
			Managers = GetComponentsInChildren<IManager>();
		}

		isInitialized = !isInitialized;

		// Exit or Init the managers
		Array.ForEach(Managers, manager =>
		{
			if (isInitialized)
				manager.Init();
			else
				manager.Exit();
		});

		if (isInitialized)
		{
			// Run the managers after finished all the Init
			Array.ForEach(Managers, manager => manager.Run());

			ActionManager.Instance.ResetAction();
		}
	}
}
