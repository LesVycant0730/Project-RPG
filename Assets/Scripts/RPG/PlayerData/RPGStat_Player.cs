using UnityEngine;
using RPG_Data;
using RoboRyanTron.SearchableEnum;

[CreateAssetMenu(fileName = "Player RPG Data", menuName = "ScriptableObjects/RPG/Player", order = 1)]
public class RPGStat_Player : RPGStat
{
	[Header ("[Class]")]
	[Space (10)]
	[SerializeField, SearchableEnum] private User_Class player_Class = User_Class.None;

	[Header ("[Object]")]
	[Space (10)]
	[SerializeField] private GameObject objectPrefab = null;

	public override User_Class GetClass() => player_Class;
	public override string GetClassName() => player_Class.ToString();

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

	public RPGStat_Player()
	{
		characterName = "New Player";
		player_Class = User_Class.None;

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

	public RPGStat_Player Clone(RPGStat_Player _data)
	{
		Clone<RPGStat_Player>(_data);

		return this;
	}

	public override void Clone<T>(T _type)
	{
		base.Clone(_type);

		var data = _type as RPGStat_Player;

		player_Class = data.player_Class;

		CustomLog.Log("Clone: " + data.characterName);
		CustomLog.Log("Clone: " + player_Class);
	}
}
