using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusUI : MonoBehaviour
{
	[Header("Info")]
	[SerializeField] protected RPGCharacterInfo info;

	[Header("General")]
	[SerializeField] protected Text textName;
	[SerializeField] protected Text textClass;
	[SerializeField] protected Text textCharacterID;

	[Header("Health")]
	[SerializeField] protected bool includeMaxHealth;
	[SerializeField] protected Image imageHealth;
	[SerializeField] protected Text textHealth;

	[Header("Stamina")]
	[SerializeField] protected bool includeMaxStamina;
	[SerializeField] protected Image imageStamina;
	[SerializeField] protected Text textStamina;

	[Header("Damage")]
	[SerializeField] protected Text textDamagePhy; 
	[SerializeField] protected Text textDamageMag; 
	[SerializeField] protected Text textCritRate; 
	[SerializeField] protected Text textCritDamage;

	[Header("Defense")]
	[SerializeField] protected Text textDefensePhy;
	[SerializeField] protected Text textDefenseMag;

	[Header("Others")]
	[SerializeField] protected Text textAccuracy;
	[SerializeField] protected Text textEvasion;
	[SerializeField] protected Text textResistance;
	[SerializeField] protected Text textSpeed;

	private const string PERCENTAGE_FORMAT = "#0.##%";

	public virtual void Enable()
	{
		gameObject.SetActive(true);
	}

	public virtual void Disable()
	{
		gameObject.SetActive(false);
	}

	[ContextMenu ("Refresh Info")]
	private void RefreshInfo()
	{
		UpdateInfo(info);
	}

	public virtual void UpdateInfo(RPGCharacterInfo _info)
	{
		// Set character info reference
		info = _info;

		if (info == null)
			return;

		// Update general stuff
		SetName();
		SetClass();
		SetCharacterID();

		// Update Health/Stamina
		SetHealth();
		SetStamina();

		// Update Damage/Crit
		SetDamage();

		// Update Defense
		SetDefense();

		// Update Others
		SetOthers();
	}

	public virtual void SetName()
	{
		if (textName)
			textName.text = info.characterName;
	}

	public virtual void SetClass()
	{
		if (textClass)
			textClass.text = info.characterClass;
	}

	public virtual void SetCharacterID()
	{
		if (textCharacterID)
			textCharacterID.text = info.characterID.ToString();
	}

	public virtual void SetHealth()
	{
		// Get Health Percentage
		float percentage = (float)info.currentHealth / info.maxHealth;

		// Health text
		if (textHealth)
		{
			textHealth.text = includeMaxHealth ? $"{info.currentHealth} / {info.maxHealth}" : info.currentHealth.ToString();
			textHealth.color = (percentage > 0.3f) ? Color.white : Color.red;
		}

		// Health bar
		if (imageHealth)
			imageHealth.fillAmount = percentage;
	}

	public virtual void SetStamina()
	{
		// Get Stamina Percentage
		float percentage = (float)info.currentStamina / info.maxStamina;

		// Stamina text
		if (textStamina)
			textStamina.text = includeMaxStamina ? $"{info.currentStamina} / {info.maxStamina}" : info.currentStamina.ToString();

		// Stamina bar
		if (imageStamina)
			imageStamina.fillAmount = percentage;
	}

	public virtual void SetDamage()
	{
		// Physical Damage text
		if (textDamagePhy)
			textDamagePhy.text = info.damagePhysical.ToString();

		// Magical Damage text
		if (textDamageMag)
			textDamageMag.text = info.damageMagical.ToString();

		// Critical Rate text
		if (textCritRate)
			textCritRate.text = info.criticalRate.ToString(PERCENTAGE_FORMAT);

		// Critical Damage text
		if (textCritDamage)
			textCritDamage.text = info.criticalDamage.ToString(PERCENTAGE_FORMAT);
	}

	public virtual void SetDefense()
	{
		// Physical Defense text
		if (textDefensePhy)
			textDefensePhy.text = info.defensePhysical.ToString();

		// Magical Defense text
		if (textDefenseMag)
			textDefenseMag.text = info.defenseMagical.ToString();
	}

	public virtual void SetOthers()
	{
		// Accuracy text
		if (textAccuracy)
			textAccuracy.text = info.accuracy.ToString(PERCENTAGE_FORMAT);

		// Evasion text
		if (textEvasion)
			textEvasion.text = info.evasion.ToString(PERCENTAGE_FORMAT);

		// Resistance text
		if (textResistance)
			textResistance.text = info.resistance.ToString(PERCENTAGE_FORMAT);

		// Speed text
		if (textSpeed)
			textSpeed.text = info.speed.ToString();
	}
}
