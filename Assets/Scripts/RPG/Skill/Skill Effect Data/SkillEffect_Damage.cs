using TextExtension;
using RPG_Data;
using System;
using UnityEngine;

[Serializable]
public class SkillEffect_Damage : SkillEffect
{
	[SerializeField] protected int damageAmount;

	public SkillEffect_Damage()
	{
		skillType = Skill_Effect_Type.Damage;
	}

	public override void Trigger()
	{
		bool[] instances = GetAllInstanceSuccessChance(out int successHit);

		Debug.Log("Max Damage per Instance: " + LogExtension.ColorLog(damageAmount, LogColor.Red) + ". Max Possible Damage: " + LogExtension.ColorLog(damageAmount * instances.Length, LogColor.Red));
		Debug.Log("Total Successful Damage Dealt: " + LogExtension.ColorLog(damageAmount * successHit, LogColor.Red) + ". Total Missed Hit: " + LogExtension.ColorLog(instances.Length - successHit, LogColor.Red));
	}
}
