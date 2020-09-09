using UnityEngine;
using AnimationTypes;
using System;
using System.Linq;
using System.Collections.Generic;

public abstract class Animation : ScriptableObject
{
	[SerializeField] protected AnimationType animationType;

	public abstract void GenerateAnimationArray();

	/// <summary>
	/// Return all enum values from the type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	protected static IEnumerable<T> GetAnimationTypeArray<T>()
	{
		return Enum.GetValues(typeof(T)).Cast<T>();
	}

	public virtual string GetAnimationTrigger(AnimationType _type)
	{
		return string.Empty;
	}

	public virtual string GetAnimationTrigger(CombatAnimationStatus _type)
	{
		return string.Empty;
	}
}
