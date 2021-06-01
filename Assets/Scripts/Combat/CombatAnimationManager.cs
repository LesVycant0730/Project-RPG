using System.Collections.Generic;
using UnityEngine;
using AnimationTypes;
using System;

public sealed class CombatAnimationManager : MonoBehaviour, IManager
{
	public CombatAnimationStatus CurrentAnimationStatus { get; private set; } = CombatAnimationStatus.Idle;

	[SerializeField] private AnimationCombat AnimationCombatRef;

	[SerializeField] private List<Animator> animList;

	private Coroutine AnimationCor = null;

	#region Start/End
	public void Init()
	{
		if (AnimationCombatRef == null)
		{
			Debug.LogError("Missing combat animation reference.");
			return;
		}

		print("Init Combat Animation Manager");

		animList.ForEach(x => UpdateTrigger(x, CombatAnimationStatus.Battle_Start));
	}

	public void Run()
	{

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
	private void UpdateTrigger(Animator _anim, CombatAnimationStatus _status)
	{
		string animTrigger = AnimationCombatRef.GetAnimationTrigger(_status);

		_anim?.SetTrigger(animTrigger);
	}
	#endregion
}
