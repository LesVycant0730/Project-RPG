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

		Debug.Log("Max Damage per Instance: " + EditorLog.ColorLog(damageAmount, LogColor.Red) + ". Max Possible Damage: " + EditorLog.ColorLog(damageAmount * instances.Length, LogColor.Red));
		Debug.Log("Total Successful Damage Dealt: " + EditorLog.ColorLog(damageAmount * successHit, LogColor.Red) + ". Total Missed Hit: " + EditorLog.ColorLog(instances.Length - successHit, LogColor.Red));
	}
}
