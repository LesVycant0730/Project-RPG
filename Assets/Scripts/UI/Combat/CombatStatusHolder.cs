using UnityEngine;

public class CombatStatusHolder : GameplayBaseManager
{
    [SerializeField] private GameObject[] holders;

	protected override void Awake()
	{
		base.Awake();
		ToggleHolders(false);
	}

	protected override void Init()
	{
		base.Init();
		ToggleHolders(true);
	}

	protected override void Exit()
	{
		base.Exit();
		ToggleHolders(false);
	}

	private void ToggleHolders(bool _enabled)
	{
		foreach (var obj in holders)
		{
			if (obj)
				obj.SetActive(_enabled);
		}
	}
}
