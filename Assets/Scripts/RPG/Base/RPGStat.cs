using UnityEngine;

// Base RPG Class
public abstract class RPGStat : ScriptableObject
{
	[SerializeField] protected new string name;

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
		critical_Rate = 10;
		critical_Damage = 10;

		defense_Physical = 10;
		defense_Magical = 10;

		accuracy = 0.7f;
		evasion = 0.05f;

		resistance = 0.1f;
		speed = 5;
	}
}
