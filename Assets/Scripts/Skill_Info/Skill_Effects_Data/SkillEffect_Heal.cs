using UnityEngine;
using RPG_Data;
using System;
using TextExtension;

[Serializable]
public class SkillEffect_Heal : SkillEffect
{
	[SerializeField] protected int healAmount;

	public SkillEffect_Heal()
	{
		skillType = Skill_Effect_Type.Heal;
	}

	public override void Trigger()
	{
		int totalHeal = healAmount * totalInstance;
		Debug.Log("Heal: " + healAmount + " per instance. Total Heal: " + EditorLog.ColorLog(totalHeal, LogColor.Yellow));
	}
}
