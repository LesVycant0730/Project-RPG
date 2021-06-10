using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusUI : MonoBehaviour
{
	[Header("Name")]
	[SerializeField] private Text txt_Name;

	[Header ("Health")]
	[SerializeField] private Image img_Health;
	[SerializeField] private Text txt_Health;

	public virtual void Enable()
	{
		gameObject.SetActive(true);
	}

	public virtual void Disable()
	{
		gameObject.SetActive(false);
	}

	public virtual void UpdateName(string _name)
	{
		txt_Name.text = _name;
	}

	public virtual void UpdateHealth(float _current, float _max)
	{
		float percentage = _current / _max;

		txt_Health.text = _current.ToString();
		txt_Health.color = (percentage > 0.3f) ? Color.white : Color.red;

		img_Health.fillAmount = percentage; 
	}
}
