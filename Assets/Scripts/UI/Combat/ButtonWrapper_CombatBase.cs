using RPG_Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWrapper_CombatBase : Selectable
{
	[Space (10), SerializeField]
	private Combat_Action actionType;

	protected ActionManager Action_M => ActionManager.Instance;

	#region Pointer Update
	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);

		Action_M.Action_Next(actionType);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);

		OnSlotUpdate();
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);

		CombatUIManager.OnCombatSkillExit();
	}
	#endregion

	#region Init/Event
	protected override void Start()
	{
		CombatUIManager.OnSelectNormalAction += Toggle;
	}

	protected override void OnDestroy()
	{
		CombatUIManager.OnSelectNormalAction -= Toggle;
	}
	#endregion

	#region Update
	private void Toggle(bool _value)
	{
		//gameObject.SetActive(_value);
	}

	public virtual void OnSlotUpdate()
	{
		CustomLog.Log("Action: " + actionType);
		CombatUIManager.OnCombatActionEnter(actionType);
	}
	#endregion
}
