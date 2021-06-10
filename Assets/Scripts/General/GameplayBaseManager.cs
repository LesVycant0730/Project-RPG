using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBaseManager : MonoBehaviour
{
	protected virtual void Awake()
	{
		GameplayController.OnManagerInit += Init;
		GameplayController.OnManagerRun += Run;
		GameplayController.OnManagerExit += Exit;
	}

	protected virtual void OnDestroy()
	{
		GameplayController.OnManagerInit -= Init;
		GameplayController.OnManagerRun -= Run;
		GameplayController.OnManagerExit -= Exit;
	}

	/// <summary>
	/// Always invoked first before Run()
	/// </summary>
	protected virtual void Init()
	{

	}

    /// <summary>
    /// Will invoked after Init()
    /// </summary>
    protected virtual void Run()
	{

	}

    protected virtual void Exit()
	{

	}
}
