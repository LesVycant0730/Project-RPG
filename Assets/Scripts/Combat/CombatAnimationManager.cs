using AnimationTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CombatAnimationManager : GameplayBaseManager
{
	private static CombatAnimationManager instance;
	public CombatAnimationStatus CurrentAnimationStatus { get; private set; } = CombatAnimationStatus.Idle;

	[SerializeField] private AnimationCombat AnimationCombatRef;

	[SerializeField] private List<Animator> animList = new List<Animator>();

	[SerializeField] private float currentAnimSpeed = 1.0f;

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
			_anim.speed = instance.currentAnimSpeed;

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


	public static IEnumerator AnimateProcess(Animator _anim, CombatAnimationStatus _status, Action _actionOnAnimEnd)
	{
		if (instance)
		{
			string animClip = instance.AnimationCombatRef.GetAnimationTrigger(_status);

			if (_anim)
			{
				_anim.CrossFadeInFixedTime(!string.IsNullOrEmpty(animClip) ? animClip : "Fight_Idle_01", 0.25f);
				yield return _anim.WaitForAnimation(animClip, _actionOnAnimEnd);
			}
			else
			{
				Debug.LogWarning("Attempt to trigger empty animator");
			}
		}

	}

	// Used in slider UI
	public void OnSliderValueChanged(float value)
	{
		currentAnimSpeed = value;
		animList.ForEach(x => x.speed = value);
	}
	#endregion
}
