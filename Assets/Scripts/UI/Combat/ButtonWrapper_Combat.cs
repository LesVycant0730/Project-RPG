using RPG_Data;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWrapper_Combat : Button
{
	public Combat_Action actionType;

	public override void OnSubmit(BaseEventData eventData)
	{
		base.OnSubmit(eventData);
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
	}
}
