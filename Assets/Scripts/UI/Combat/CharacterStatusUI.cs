using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusUI : MonoBehaviour
{
	[Header("Name")]
	[SerializeField] private Text textName;

	[Header ("Health")]
	[SerializeField] private Image imageHealth;
	[SerializeField] private Text textHealth;

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
		textName.text = _name;
	}

	public virtual void UpdateHealth(float _current, float _max)
	{
		float percentage = _current / _max;

		textHealth.text = _current.ToString();
		textHealth.color = (percentage > 0.3f) ? Color.white : Color.red;

		imageHealth.fillAmount = percentage; 
	}
}
