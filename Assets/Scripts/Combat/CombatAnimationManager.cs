using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using AnimationTypes;
using System;

public sealed class CombatAnimationManager : GameplayBaseManager
{
	private static CombatAnimationManager instance;
	public CombatAnimationStatus CurrentAnimationStatus { get; private set; } = CombatAnimationStatus.Idle;

	[SerializeField] private AnimationCombat AnimationCombatRef;

	[SerializeField] private List<Animator> animList = new List<Animator>();

	private Coroutine AnimationCor = null;

	#region Start/End
	protected override void Init()
	{
		base.Init();

		if (AnimationCombatRef == null)
		{
			Debug.LogError("Missing combat animation reference.");
			return;
		}

		instance = this;
	}

	protected override void Run()
	{
		base.Run();
	}

	protected override void Exit()
	{
		base.Exit();

		if (AnimationCombatRef == null)
		{
			Debug.LogError("Missing combat animation reference.");
			return;
		}

		animList.ForEach(x => UpdateAnimation(x, CombatAnimationStatus.Idle));
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
				UpdateAnimation(_anim, CombatAnimationStatus.Battle_Start);
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
	public static void UpdateAnimation(Animator _anim, CombatAnimationStatus _status)
	{
		if (instance)
		{
			string animClip = instance.AnimationCombatRef.GetAnimationTrigger(_status);

			if (_anim)
			{
				_anim.CrossFadeInFixedTime(animClip, 0.25f);
			}
			else
			{
				Debug.LogWarning("Attempt to trigger empty animator");
			}
		}
	}

	public static void Test(Animator _anim, CombatAnimationStatus _status, Action _action)
	{
		if (instance)
		{
			instance.StartCoroutine(IsAnimationSucceed(_anim, _status, _action));
		}
	}

	public static IEnumerator IsAnimationSucceed(Animator _anim, CombatAnimationStatus _status, Action _action)
	{
		if (instance)
		{
			string animClip = instance.AnimationCombatRef.GetAnimationTrigger(_status);

			if (_anim)
			{
				_anim.CrossFadeInFixedTime(animClip, 0.25f);
				yield return _anim.WaitForAnimation(animClip, _action);
			}
			else
			{
				Debug.LogWarning("Attempt to trigger empty animator");
			}
		}

	}
	#endregion
}
