﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RPG_Data;
using RoboRyanTron.SearchableEnum;

public class ButtonWrapper_CombatSkill : ButtonWrapper_CombatBase
{
	[SerializeField] private bool setOnStart = false;
	[SerializeField] private Text textButton;
	[SerializeField, SearchableEnum] private Skill_Name skillName = Skill_Name.FINAL_INDEX;
	private ChAction_Skill skill;

	#region Skill Update
	public void SetSkill(Skill_Name _skill)
	{
		skillName = _skill;
		skill = SkillLibrary.GetSkill(skillName);
		textButton.text = skillName.ToString().Replace("_", " ");
	}
	#endregion

	protected override void Awake()
	{
		base.Awake();

		// Set skill reference on awake.
		if (setOnStart)
			SetSkill(skillName);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (!interactable)
			return;

		base.OnPointerEnter(eventData);

		CombatUIManager.OnCombatSkillEnter(skill.GetDescription());
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		if (!interactable)
			return;

		base.OnPointerExit(eventData);

		CombatUIManager.OnCombatActionExit();
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!interactable)
			return;

		base.OnPointerDown(eventData);

		CombatController.SetPlayerAction(skill);
	}

}
