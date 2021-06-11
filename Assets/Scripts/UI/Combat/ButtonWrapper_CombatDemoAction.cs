using AnimationTypes;
using RoboRyanTron.SearchableEnum;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWrapper_CombatDemoAction : Selectable
{
	[Header ("Demo usage only, trigger action based on the combat animation status")]
	[SerializeField, SearchableEnum] private CombatAnimationStatus _status;

	#region Pointer Update
	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);

		// Execute action
		CombatController.InvokePlayerAction(_status);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		OnSlotUpdate();
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
	}
	#endregion

	#region Update
	private void OnSlotUpdate()
	{
		CombatUIManager.OnCombatActionEnter(_status);
	}
	#endregion
}
