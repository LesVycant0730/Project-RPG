using RPG_Data;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SkillEffect
{
	private bool canOverrideSkillType = false;

	[ConditionalHide ("canOverrideSkillType", HideInInspector = false, Inverse = true)]
	public Skill_Effect_Type skillType;

	[SerializeField]
	private bool isAOE;

	public bool isPercentage = true;

	[ConditionalHide("isPercentage", true, inverse: false)]
	public float hpCostPercentage, spCostPercentage;

	[ConditionalHide("isPercentage", true, inverse: true)]
	public int hpCostFixed, spCostFixed;

	[SerializeField] 
	protected int totalInstance;

	[SerializeField, Range (0, 100)]
	protected int successRate;


	#region Property Method
	public bool IsAOE()
	{
		return isAOE;
	}
	public int TotalInstance()
	{
		return totalInstance;
	}

	public int SuccessRate()
	{
		return successRate;
	}

	public bool GetInstanceSuccessChance()
	{
		return UnityEngine.Random.Range(0, 100) >= (100 - successRate);
	}

	public bool[] GetAllInstanceSuccessChance(out int _successfulInstance)
	{
		bool[] instances = new bool[totalInstance];
		_successfulInstance = 0;

		for (int i = 0; i < instances.Length; i++)
		{
			bool isSuccess = GetInstanceSuccessChance();

			instances[i] = isSuccess;

			if (isSuccess)
			{
				_successfulInstance++;
			}
		}

		return instances;
	}
	#endregion

	#region Action
	public void ClampValues()
	{
		hpCostFixed = Mathf.Clamp(hpCostFixed, 0, 9999);
		spCostFixed = Mathf.Clamp(spCostFixed, 0, 9999);

		hpCostPercentage = Mathf.Clamp(hpCostPercentage, 0.0f, 1.0f);
		spCostPercentage = Mathf.Clamp(spCostPercentage, 0.0f, 1.0f);
	}

	public void Use(ref int _userHP, ref int _userSP)
	{
		if (isPercentage)
		{
			_userHP -= Mathf.RoundToInt(_userHP * hpCostPercentage);
			_userSP -= Mathf.RoundToInt(_userSP * spCostPercentage);
		}
		else
		{
			_userHP -= hpCostFixed;
			_userSP -= spCostFixed;
		}
		
		Trigger();
	}

	public abstract void Trigger();
	#endregion
}

[Serializable]
public class SkillEffectCollection
{
	[SerializeField]
	private bool hasDamage, hasHeal;

	[ConditionalHide ("hasDamage", hideInInspector: true)]
	public SkillEffect_Damage SE_Damage;

	[ConditionalHide("hasHeal", hideInInspector: true)]
	public SkillEffect_Heal SE_Heal;

	public SkillEffectCollection()
	{
		SE_Damage = null;
		SE_Heal = null;
	}

	public SkillEffect[] EffectArray()
	{
		List<SkillEffect> skillEffects = new List<SkillEffect>();

		if (hasDamage)
		{
			skillEffects.Add(SE_Damage);
		}

		if (hasHeal)
		{
			skillEffects.Add(SE_Heal);
		}

		return skillEffects.ToArray();
	}

	public bool ContainEffect(Skill_Effect_Type _type)
	{
		switch (_type)
		{
			case Skill_Effect_Type.Damage:
				return hasDamage;

			case Skill_Effect_Type.Heal:
				return hasHeal;

			default:
				return false;
		}
	}
}
