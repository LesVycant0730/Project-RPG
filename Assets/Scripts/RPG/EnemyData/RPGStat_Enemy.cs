using UnityEngine;
using RPG_Data;
using RoboRyanTron.SearchableEnum;

[CreateAssetMenu(fileName = "PEnemy RPG Data", menuName = "ScriptableObjects/RPG/Enemy", order = 1)]
public class RPGStat_Enemy : RPGStat
{
	[Header("[Class]")]
	[Space(10)]
	[SerializeField, SearchableEnum] private User_Class enemy_Class = User_Class.None;

	[Header("[Object]")]
	[Space(10)]
	[SerializeField] private GameObject objectPrefab = null;

	public override User_Class GetClass() => enemy_Class;
	public override string GetClassName() => enemy_Class.ToString();

	public override GameObject GetObject()
	{
		if (objectPrefab)
		{
			return objectPrefab;
		}
		else
		{
			Debug.LogError("Missing player prefab reference in character: " + name);

			return null;
		}
	}

	public override void Clone<T>(T _type)
	{
		base.Clone(_type);

		var data = _type as RPGStat_Enemy;

		enemy_Class = data.enemy_Class;

		CustomLog.Log("Clone: " + data.characterName);
		CustomLog.Log("Clone: " + enemy_Class);
	}

	public RPGStat_Enemy Clone(RPGStat_Enemy _data)
	{
		Clone<RPGStat_Enemy>(_data);

		return this;
	}

	public RPGStat_Enemy()
	{
		characterName = "New Enemy";
		enemy_Class = User_Class.None;

		health = 100;
		stamina = 100;

		damagePhysical = 10;
		damageMagical = 10;
		criticalRate = 10;
		criticalDamage = 10;

		defensePhysical = 10;
		defenseMagical = 10;

		accuracy = 0.7f;
		evasion = 0.05f;

		resistance = 0.1f;
		speed = 5;
	}
}
