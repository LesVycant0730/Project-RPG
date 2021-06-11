using AnimationTypes;
using RPG_Data;
using System;
using UnityEngine;

namespace GameInfo
{
	[CreateAssetMenu(fileName = "Action Description", menuName = "ScriptableObjects/Description/Action", order = 1)]
	public class InfoDescription : ScriptableObject
	{
		[Serializable]
		protected class CombatAction
		{
			[SerializeField] private Combat_Action type;
			[SerializeField, TextArea (3, 5)] private string description;

			public Combat_Action Type => type;
			public string Description => description;
		}

		[Serializable]
		protected class DemoAction
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
	}
}

