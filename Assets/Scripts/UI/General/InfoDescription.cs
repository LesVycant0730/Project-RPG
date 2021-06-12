using AnimationTypes;
using RPG_Data;
using System;
using TextExtension;
using UnityEngine;

namespace GameInfo
{
	public enum CombatLogType
	{
		Damage_Target,
		Damage_Self,
		Damage_Multiple,

		Heal_Self,

		Missed,

		Fainted,
		Winning
	}

	public enum CombatLogKeywordType
	{
		String,
		Int
	}

	public enum CombatLogKeyword
	{
		Player_Current,
		Opponent_Current,

		Action_Name,
		Action_Value,

		Action_Success,
		Action_Failed
	}

	[CreateAssetMenu(fileName = "Action Description", menuName = "ScriptableObjects/Description/Action", order = 1)]
	public class InfoDescription : ScriptableObject
	{
		#region Action Description
		[Serializable]
		protected struct CombatAction
		{
			[SerializeField] private Combat_Action type;
			[SerializeField, TextArea (3, 5)] private string description;

			public Combat_Action Type => type;
			public string Description => description;
		}

		[Serializable]
		protected struct DemoAction
		{
			[SerializeField] private CombatAnimationStatus type;
			[SerializeField, TextArea(3, 5)] private string description;

			public CombatAnimationStatus Type => type;
			public string Description => description;
		}

		[SerializeField] private CombatAction[] combatActions;
		[SerializeField] private DemoAction[] demoActions;

		public string GetDescription(Combat_Action _type)
		{
			return Array.Find(combatActions, x => x.Type == _type).Description;
		}

		public string GetDescription(CombatAnimationStatus _type)
		{
			return Array.Find(demoActions, x => x.Type == _type).Description;
		}
		#endregion

		#region Combat Log Description
		[Serializable]
		protected struct CombatLogKey
		{
			[SerializeField] private CombatLogKeyword key;
			[SerializeField] private LogColor color;
			[SerializeField] private string word;

			public CombatLogKeyword Key => key;
			public LogColor Color => color;
			public string Word => word;

			public string Replace(string _word)
			{
				return LogExtension.ColorLog(_word, color);
			}
		}

		[Serializable]
		protected struct CombatLog
		{
			[SerializeField] private CombatLogType type;
			[SerializeField, NonReorderable] private CombatLogKeyword[] keywords;
			[SerializeField, TextArea(3, 5)] private string description;

			public CombatLogType Type => type;
			public CombatLogKeyword[] Keywords => keywords;
			public string Description => description;

			public bool HasSameKeywordAmount (int _length)
			{
				return keywords.Length == _length;
			}
		}
		#endregion

		#region Combat Log Value Return
		[SerializeField] private CombatLogKey[] combatLogKeys;
		[SerializeField] private CombatLog[] combatLogs;

		public string GetLog(CombatLogType _type, params object[] _param)
		{
			CombatLog log = Array.Find(combatLogs, x => x.Type == _type);

			// Check keyword types and parameter types
			bool isKeywordCorrect = log.HasSameKeywordAmount(_param.Length);

			if (isKeywordCorrect)
			{
				string des = log.Description;

				// Loop through the arrays to replace the keyword by the parameter
				for (int i = 0; i < _param.Length; i++)
				{
					// Search the key from the established arrays in the SO
					var key = Array.Find(combatLogKeys, x => x.Key == log.Keywords[i]);

					// Replace the keyword retrieved from the array
					des = des.Replace(key.Word, key.Replace(_param[i].ToString()));
				}

				return $"[{DateTime.Now.ToLongTimeString()}] {des}\n";
			}
			else
			{
				return string.Empty;
			}
		}
		#endregion
	}
}

