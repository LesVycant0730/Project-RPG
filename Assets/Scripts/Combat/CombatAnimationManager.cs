using System.Collections.Generic;
using UnityEngine;
using AnimationTypes;
using System;

public sealed class CombatAnimationManager : MonoBehaviour, IManager
{
	private static CombatAnimationManager instance;
	public CombatAnimationStatus CurrentAnimationStatus { get; private set; } = CombatAnimationStatus.Idle;

	[SerializeField] private AnimationCombat AnimationCombatRef;

	[SerializeField] private List<Animator> animList = new List<Animator>();

	private Coroutine AnimationCor = null;

	#region Start/End
	public void Init()
	{
		if (AnimationCombatRef == null)
		{
			Debug.LogError("Missing combat animation reference.");
			return;
		}

		instance = this;

		print("Init Combat Animation Manager");
	}

	public void Run()
	{
		//animList.ForEach(x => UpdateTrigger(x, CombatAnimationStatus.Battle_Start));
	}

	public void Exit()
	{
		if (AnimationCombatRef == null)
		{
			Debug.LogError("Missing combat animation reference.");
			return;
		}

		print("Exit Combat Animation Manager");

		animList.ForEach(x => UpdateTrigger(x, CombatAnimationStatus.Idle));
		animList.Clear();
		instance = null;
	}
	#endregion

	#region Animation Reference
	public static void AddAnimator(Animator _anim, bool _runDefault = true)
	{
		if (instance)
		{
			if (!instance.animList.Contains(_anim))
			{
				instance.animList.Add(_anim);
			}

			if (_runDefault)
			{
				UpdateTrigger(_anim, CombatAnimationStatus.Battle_Start);
			}
		}
	}
	
	public void RemoveAnimator(Animator _anim)
	{
		if (instance)
		{
			instance.animList.Remove(_anim);
		}
	}
	#endregion

	#region Combat Action
	public void AnimUpdate_SkillOnEnemy()
	{
		if (AnimationCor != null)
		{
			return;
		}

		AnimationCor = StartCoroutine(RunAnimation(AnimationCor));

	}

	public void AnimUpdate_Default()
	{

	}

	private IEnumerator<Action> RunAnimation(Coroutine _cor)
	{
		if (this != null)
		{
			_cor = null;
			yield return AnimUpdate_Default;

			yield break;
		}

		_cor = null;
		yield return null;
	}
	#endregion

	#region Animation Update
	/// <summary>
	/// Trigger animator from parameter based on CombatAnimationStatus enum.
	/// </summary>
	public static void UpdateTrigger(Animator _anim, CombatAnimationStatus _status)
	{
		if (instance)
		{
			string animTrigger = instance.AnimationCombatRef.GetAnimationTrigger(_status);

			if (_anim)
			{
				_anim.SetTrigger(animTrigger);
			}
			else
			{
				Debug.LogWarning("Attempt to trigger empty animator");
			}
		}
	}
	#endregion
}
