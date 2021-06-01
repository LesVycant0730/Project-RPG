using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableSlot : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}
}
