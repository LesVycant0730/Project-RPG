using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWrapper_CombatSkill : ButtonWrapper_CombatBase
{
	private bool isInit = false;
	[SerializeField] private Skill skill = null;
	[SerializeField] private Text textButton;

	#region Skill Update
	public void SetSkill(Skill _skill, bool _init)
	{
		if (skill == _skill)
		{
			return;
		}

		isInit = false;
		skill = _skill;

		if (_init)
		{
			Init();
		}
	}

	public void Init()
	{
		if (isInit)
		{
			return;
		}

		isInit = true;
		textButton.text = skill.GetName();
	}
	#endregion

#if UNITY_EDITOR
	protected override void OnValidate()
	{
		if (textButton == null)
		{
			textButton = GetComponentInChildren<Text>();
		}
	}
#endif

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

		CombatUIManager.OnCombatSkillEnter(skill?.GetDescription());
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
