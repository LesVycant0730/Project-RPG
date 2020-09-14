﻿using UnityEngine;

public sealed class CombatManager : MonoBehaviour, IManager
{
    public static CombatManager Instance { get; private set; }

	[SerializeField] private CombatAction action_Combat;

	private void Awake()
	{
		Instance = this;
	}

	public void Init()
	{
		print("Init combat manager");
	}

	public void Exit()
	{
		print("Exit combat manager");
	}
}