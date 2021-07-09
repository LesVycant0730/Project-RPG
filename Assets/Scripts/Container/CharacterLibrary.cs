using RPG_Data;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CharacterLibrary : BaseLibrary
{
	public static CharacterLibrary Instance { get; private set; }

	[SerializeField] private RPGStat_Player[] playerInfos;
	[SerializeField] private RPGStat_Enemy[] enemyInfos;

	private HashSet<RPGStat_Player> playerHashset = new HashSet<RPGStat_Player>();
	private HashSet<RPGStat_Enemy> enemyHashset = new HashSet<RPGStat_Enemy>();

	protected override void Awake()
	{
		base.Awake();
		Instance = this;
		SetupLibrary();
	}

	protected override void SetupLibrary()
	{
		foreach (var playerData in playerInfos)
		{
			if (playerData != null)
			{
				playerHashset.Add(ScriptableObject.CreateInstance<RPGStat_Player>().Clone(playerData));
			}
		}

		foreach (var enemyData in enemyInfos)
		{
			if (enemyData != null)
			{
				enemyHashset.Add(ScriptableObject.CreateInstance<RPGStat_Enemy>().Clone(enemyData));
			}
		}
	}

	/// <summary>
	/// Retrieve RPG player stat reference.
	/// </summary>
	/// <param name="_id"></param>
	/// <returns></returns>
	public static RPGStat_Player GetPlayer(Character_ID _id)
	{
		if (Instance != null)
		{
			return Instance.playerInfos.Single(x => x.GetID() == _id);
		}

		return null;
	}

	public static RPGStat_Enemy GetEnemy(Character_ID _id)
	{
		if (Instance != null)
		{
			return Instance.enemyInfos.Single(x => x.GetID() == _id);
		}

		return null;
	}
}
