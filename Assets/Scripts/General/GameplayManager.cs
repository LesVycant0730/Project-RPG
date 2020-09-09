using UnityEngine;

public sealed class GameplayManager : MonoBehaviour
{
	private IManager[] Managers;
	private bool isInitialized;

	// Temp use on Toggle Combat button
	public void OnGameplayStart()
	{
		if (Managers == null)
		{
			Managers = GetComponentsInChildren<IManager>();
		}

		foreach (IManager manager in Managers)
		{
			if (manager != null)
			{
				if (isInitialized)
				{
					manager.Exit();
				}
				else
				{
					manager.Init();
				}
			}
		}

		isInitialized = !isInitialized;
	}

	public void Init()
	{
		print("Start Main Gameplay Manager");
	}

	public void Exit()
	{
		print("Exit Main Gameplay Manager");
	}
}
