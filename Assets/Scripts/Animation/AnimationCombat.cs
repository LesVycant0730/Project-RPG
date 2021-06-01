﻿using System.Collections.Generic;
using UnityEngine;
using System;
using AnimationTypes;
using System.Linq;

[CreateAssetMenu(fileName = "Animation Combat", menuName = "ScriptableObjects/Animation/Combat", order = 1)]
public class AnimationCombat : Animation
{
	[Serializable]
	private struct AnimationDict
	{
		public CombatAnimationStatus status;
		public string animTrigger;
	}

	private Type animType = typeof(CombatAnimationStatus);

	[SerializeField, ConditionalHide (HideInInspector = false, Inverse = true)] 
	private int animationCounts;

	[SerializeField] private AnimationDict[] animationArray;

	public override void GenerateAnimationArray()
	{
		IEnumerable<CombatAnimationStatus> typeLength = Utility.GetTypeElements<CombatAnimationStatus>();

		animationCounts = typeLength.Count();

		animationArray = new AnimationDict[animationCounts];

		for (int i = 0; i < animationArray.Length; i++)
		{
			animationArray[i].status = typeLength.ElementAt(i);
		}
	}

	public override string GetAnimationTrigger(CombatAnimationStatus _status)
	{
		return animationArray.Single(x => x.status == _status).animTrigger; 
	}
}
