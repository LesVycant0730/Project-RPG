using UnityEngine;
using RPG_Data;
using RoboRyanTron.SearchableEnum;

[CreateAssetMenu(fileName = "Player RPG Data", menuName = "ScriptableObjects/RPG/Player", order = 1)]
public class RPGStat_Player : RPGStat
{
	[Header ("[Class]")]
	[Space (10)]
	[SerializeField, SearchableEnum] private User_Class player_Class = User_Class.None;
	//[SerializeField] private 

	public RPGStat_Player()
	{
		name = "New Player";
		player_Class = User_Class.None;

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
