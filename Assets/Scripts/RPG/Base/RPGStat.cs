using UnityEngine;
using RPG_Data;
using System.Collections.Generic;
using RoboRyanTron.SearchableEnum;

// Base RPG Class
public abstract class RPGStat : ScriptableObject
{
	[SerializeField] protected string characterName;
	[SerializeField, SearchableEnum] protected Character_ID characterID;

	[Header("[HP/SP]")]
	[Space (10)]

	[SerializeField, Range(1, 9999)] protected int health;
	[SerializeField, Range(1, 9999)] protected int stamina;

	[Header("[Damage]")]
	[Space (10)]

	[SerializeField] protected int damagePhysical;
	[SerializeField] protected int damageMagical;
	[SerializeField] protected float criticalRate;
	[SerializeField] protected float criticalDamage;

	[Header("[Defense]")]
	[Space (10)]

	[SerializeField] protected int defensePhysical;
	[SerializeField] protected int defenseMagical;

	[Header("[Others]")]
	[Space (10)]

	[SerializeField, Range (0.01f, 1.0f)] protected float accuracy;
	[SerializeField, Range(0.01f, 1.0f)] protected float evasion;
	[SerializeField, Range(0.01f, 1.0f)] protected float resistance;
	[SerializeField] protected int speed;

	[Header("[Skills]")]
	[SerializeField, SearchableEnum] protected List<Skill_Name> unlockedSkills = new List<Skill_Name>();

	// Default RPG Stat
	public RPGStat()
	{
		health = 100;
		stamina = 100;

		damagePhysical = 10;
		damageMagical = 10;
		criticalRate = 10.0f;
		criticalDamage = 10.0f;

		defensePhysical = 10;
		defenseMagical = 10;

		accuracy = 0.7f;
		evasion = 0.05f;

		resistance = 0.1f;
		speed = 5;

		unlockedSkills = new List<Skill_Name>();
	}

	public RPGStat(int _hp, int _sp, int _dmgPhy, int _dmgMag, float _critRate, float _critDmg, int _defPhy, int _defMag, float _acc, float _eva, float _res, int _spd)
	{
		health = _hp;
		stamina = _sp;

		damagePhysical = _dmgPhy;
		damageMagical = _dmgMag;
		criticalRate = _critRate;
		criticalDamage = _critDmg;

		defensePhysical = _defPhy;
		defenseMagical = _defMag;

		accuracy = _acc;
		evasion = _eva;

		resistance = _res;
		speed = _spd;
	}

	public abstract User_Class GetClass();
	public abstract string GetClassName();
	public abstract GameObject GetObject();
	public string GetName() => characterName;
	public Character_ID GetID() => characterID;
	public int GetHealth() => health;
	public int GetStamina() => stamina;
	public int GetPhyDamage() => damagePhysical;
	public int GetMagDamage() => damageMagical;
	public float GetCritRate() => criticalRate;
	public float GetCritDamage() => criticalDamage;
	public int GetPhyDefense() => defensePhysical;
	public int GetMagDefense() => defenseMagical;
	public float GetAccuracy() => accuracy;
	public float GetEvasion() => evasion;
	public float GetResistance() => resistance;
	public int GetSpeed() => speed;
	public bool HasSkill(Skill_Name _skill) => unlockedSkills.Contains(_skill);
	public List<Skill_Name> GetSkillList() => unlockedSkills;

	public virtual void Clone<T>(T _stat) where T : RPGStat
	{
		characterName = _stat.characterName;

		health = _stat.health;
		stamina = _stat.stamina;

		damagePhysical = _stat.damagePhysical;
		damageMagical = _stat.damageMagical;
		criticalRate = _stat.criticalRate;
		criticalDamage = _stat.criticalDamage;

		defensePhysical = _stat.defensePhysical;
		defenseMagical = _stat.defenseMagical;

		accuracy = _stat.accuracy;
		evasion = _stat.evasion;

		resistance = _stat.resistance;
		speed = _stat.speed;
	}
}

[System.Serializable]
public class RPGCharacterInfo
{
	[SerializeField, ConditionalHide(false)] public string characterName;
	[SerializeField, ConditionalHide(false)] public string characterClass;
	[SerializeField, ConditionalHide(false)] public Character_ID characterID;

	[Header("[HP/SP]")]
	[Space(10)]

	[SerializeField, Range(1, 9999), ConditionalHide(false)] public int maxHealth;
	[SerializeField, Range(1, 9999), ConditionalHide(false)] public int currentHealth;
	[SerializeField, Range(1, 9999), ConditionalHide(false)] public int maxStamina;
	[SerializeField, Range(1, 9999), ConditionalHide(false)] public int currentStamina;

	[Header("[Damage]")]
	[Space(10)]

	[SerializeField, ConditionalHide(false)] public int damagePhysical;
	[SerializeField, ConditionalHide(false)] public int damageMagical;
	[SerializeField, Range(0.01f, 1.0f), ConditionalHide(false)] public float criticalRate;
	[SerializeField, Range(1.0f, 20.0f), ConditionalHide(false)] public float criticalDamage;

	[Header("[Defense]")]
	[Space(10)]

	[SerializeField, ConditionalHide(false)] public int defensePhysical;
	[SerializeField, ConditionalHide(false)] public int defenseMagical;

	[Header("[Others]")]
	[Space(10)]

	[SerializeField, Range(0.01f, 1.0f), ConditionalHide(false)] public float accuracy;
	[SerializeField, Range(0.01f, 1.0f), ConditionalHide(false)] public float evasion;
	[SerializeField, Range(0.01f, 1.0f), ConditionalHide(false)] public float resistance;
	[SerializeField, ConditionalHide(false)] public int speed;

	[Header("[Skills]")]
	[SerializeField, ConditionalHide(false)] public List<Skill_Name> unlockedSkills;

	public RPGCharacterInfo(RPGStat _stat)
	{
		characterName = _stat.GetName();
		characterClass = _stat.GetClassName();
		characterID = _stat.GetID();

		maxHealth = _stat.GetHealth();
		maxStamina = _stat.GetStamina();

		currentHealth = maxHealth;
		currentStamina = maxStamina;

		damagePhysical = _stat.GetPhyDamage();
		damageMagical = _stat.GetMagDamage();
		criticalRate = _stat.GetCritRate();
		criticalDamage = _stat.GetCritDamage();

		defensePhysical = _stat.GetPhyDefense();
		defenseMagical = _stat.GetMagDefense();

		accuracy = _stat.GetAccuracy();
		evasion = _stat.GetEvasion();

		resistance = _stat.GetResistance();
		speed = _stat.GetSpeed();

		unlockedSkills = _stat.GetSkillList();
	}

	public void AddHealth(int _value)
	{
		currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
	}

	public void SubtractHealth(int _value, out bool _isEmpty)
	{
		currentHealth = Mathf.Clamp(currentHealth - _value, 0, maxHealth);

		_isEmpty = currentHealth <= 0;
	}
}