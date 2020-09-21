using RPG_Data;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWrapper_Combat : Selectable
{
	public Combat_Action actionType;

	private ActionManager Action_M => ActionManager.Instance;

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);

		Action_M?.RunAction(actionType);
	}
}
