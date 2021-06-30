using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RPG_Data;

public class ButtonWrapper_CombatSkill : ButtonWrapper_CombatBase
{
	[SerializeField] private Text textButton;
	[SerializeField] private Skill_Name skillName = Skill_Name.FINAL_INDEX;
	private Skill skill;

	#region Skill Update
	public void SetSkill(Skill_Name _skill)
	{
		skillName = _skill;
		skill = SkillLibrary.GetSkill(skillName);
		textButton.text = skillName.ToString().Replace("_", " ");
	}
	#endregion

	protected override void Start()
	{
		base.Start();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);

		CombatUIManager.OnCombatSkillEnter(skill.GetDescription());
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);

		CombatUIManager.OnCombatActionExit();
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
	}

}
