using UnityEngine;

// Base RPG Class
public abstract class RPGStat : ScriptableObject
{
	[SerializeField] protected string characterName;

	[Header("[HP/SP]")]
	[Space (10)]

	[SerializeField, Range(1, 9999)] protected int health;
	[SerializeField, Range(1, 9999)] protected int stamina;

	[Header("[Damage]")]
	[Space (10)]

	[SerializeField] protected int damage_Physical;
	[SerializeField] protected int damage_Magical;
	[SerializeField] protected float critical_Rate;
	[SerializeField] protected float critical_Damage;

	[Header("[Defense]")]
	[Space (10)]

	[SerializeField] protected int defense_Physical;
	[SerializeField] protected int defense_Magical;

	[Header("[Others]")]
	[Space (10)]

	[SerializeField, Range (0.01f, 1.0f)] protected float accuracy;
	[SerializeField, Range(0.01f, 1.0f)] protected float evasion;
	[SerializeField, Range(0.01f, 1.0f)] protected float resistance;
	[SerializeField] protected int speed;

	// Default RPG Stat
	public RPGStat()
	{
		health = 100;
		stamina = 100;

		damage_Physical = 10;
		damage_Magical = 10;
		critical_Rate = 10.0f;
		critical_Damage = 10.0f;

		defense_Physical = 10;
		defense_Magical = 10;

		accuracy = 0.7f;
		evasion = 0.05f;

		resistance = 0.1f;
		speed = 5;
	}

	public RPGStat(int _hp, int _sp, int _dmgPhy, int _dmgMag, float _critRate, float _critDmg, int _defPhy, int _defMag, float _acc, float _eva, float _res, int _spd)
	{
		health = _hp;
		stamina = _sp;

		damage_Physical = _dmgPhy;
		damage_Magical = _dmgMag;
		critical_Rate = _critRate;
		critical_Damage = _critDmg;

		defense_Physical = _defPhy;
		defense_Magical = _defMag;

		accuracy = _acc;
		evasion = _eva;

		resistance = _res;
		speed = _spd;
	}

	public abstract string GetClass();
	public string GetName() => characterName;
	public int GetHealth() => health;
	public int GetStamina() => stamina;
	public int GetPhyDamage() => damage_Physical;
	public int GetMagDamage() => damage_Magical;
	public float GetCritRate() => critical_Rate;
	public float GetCritDamage() => critical_Damage;
	public int GetPhyDefense() => defense_Physical;
	public int GetMagDefense() => defense_Magical;
	public float GetAccuracy() => accuracy;
	public float GetEvasion() => evasion;
	public float GetResistance() => resistance;
	public int GetSpeed() => speed;
}

[System.Serializable]
public class RPGCharacterInfo
{
	[SerializeField] protected string characterName;
	[SerializeField] protected string characterClass;

	[Header("[HP/SP]")]
	[Space(10)]

	[SerializeField, Range(1, 9999)] protected int health;
	[SerializeField, Range(1, 9999)] protected int stamina;

	[Header("[Damage]")]
	[Space(10)]

	[SerializeField] protected int damage_Physical;
	[SerializeField] protected int damage_Magical;
	[SerializeField] protected float critical_Rate;
	[SerializeField] protected float critical_Damage;

	[Header("[Defense]")]
	[Space(10)]

	[SerializeField] protected int defense_Physical;
	[SerializeField] protected int defense_Magical;

	[Header("[Others]")]
	[Space(10)]

	[SerializeField, Range(0.01f, 1.0f)] protected float accuracy;
	[SerializeField, Range(0.01f, 1.0f)] protected float evasion;
	[SerializeField, Range(0.01f, 1.0f)] protected float resistance;
	[SerializeField] protected int speed;

	public RPGCharacterInfo(RPGStat _stat)
	{
		characterName = _stat.GetName();
		characterClass = _stat.GetClass();

		health = _stat.GetHealth();
		stamina = _stat.GetStamina();

		damage_Physical = _stat.GetPhyDamage();
		damage_Magical = _stat.GetMagDamage();
		critical_Rate = _stat.GetCritRate();
		critical_Damage = _stat.GetCritDamage();

		defense_Physical = _stat.GetPhyDefense();
		defense_Magical = _stat.GetMagDefense();

		accuracy = _stat.GetAccuracy();
		evasion = _stat.GetEvasion();

		resistance = _stat.GetResistance();
		speed = _stat.GetSpeed();
	}
}