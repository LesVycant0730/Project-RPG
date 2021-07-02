using AnimationTypes;
using GameInfo;
using RPG_Data;
using System;
using System.Collections;
using UnityEngine;

public class CombatController : MonoBehaviour
{
	[SerializeField] private CombatCalculator calculator;

	private RPGCharacter currentCharacter;

	private Coroutine CombatCor = null;

	// Action when any party ended their turn
	public static event Action OnTurnEnd;

	// Demo action (Test only)
	//public static event Action<CombatAnimationStatus> OnPlayerAction;

	// Skill action
	public static event Action<SkillData> OnPlayerAction;

	#region Mono
	private void Awake()
	{
		RPGPartyManager.OnCharacterTurn += CheckTurn;
		OnPlayerAction += PlayerAction;
	}

	private void OnDestroy()
	{
		RPGPartyManager.OnCharacterTurn -= CheckTurn;
		OnPlayerAction -= PlayerAction;
	}
	#endregion

	public void CheckTurn(RPGCharacter _char, RPG_Party _party)
	{
		currentCharacter = _char;

		if (_char != null)
		{
			// If Player, combat action will require manual input
			if (_party == RPG_Party.Ally)
			{
				return;
			}
			// If Enemy, combat action will automated
			else
			{
				// Demo only
				//CombatAnimationStatus randAnim = (CombatAnimationStatus)UnityEngine.Random.Range((int)CombatAnimationStatus.Normal_Kick, (int)CombatAnimationStatus.Fireball + 1);
				//CombatActionCoroutine(randAnim);

				// Add skill feedback here for enemy
			}
		}
	}

	#region Demo
	// Called in Button only
	//public void RandomCombatAction()
	//{
	//	CombatAnimationStatus[] randAnimArr = new CombatAnimationStatus[]
	//	{
	//		CombatAnimationStatus.Normal_Kick, CombatAnimationStatus.Self_Heal,
	//		CombatAnimationStatus.Magic_Missile, CombatAnimationStatus.Fireball,
	//		CombatAnimationStatus.Heavy_Kick, CombatAnimationStatus.Magic_Bomb
	//	};

	//	CombatAnimationStatus randAnim = randAnimArr[UnityEngine.Random.Range(0, randAnimArr.Length)];

	//	PlayerAction(randAnim);
	//}

	//public static void InvokePlayerAction(CombatAnimationStatus _status)
	//{
	//	OnPlayerAction?.Invoke(_status);
	//}

	//private void PlayerAction(CombatAnimationStatus _status)
	//{
	//	if (currentCharacter != null)
	//	{
	//		if (currentCharacter.CharacterParty == RPG_Party.Ally)
	//		{
	//			// Disable UI
	//			CombatUIManager.OnCombatActionRegistered();

	//			CombatActionCoroutine(_status);
	//		}
	//	}
	//}
	//private void CombatActionCoroutine(CombatAnimationStatus _status)
	//{
	//	if (CombatCor != null)
	//	{
	//		StopCoroutine(CombatCor);
	//	}

	//	CombatCor = StartCoroutine(CombatSimulation(_status));
	//}

	// Only for simulating combat with animation and feedback used in demonstration 
	//private IEnumerator CombatSimulation(CombatAnimationStatus _status)
	//{
	//	if (currentCharacter == null)
	//		throw new Exception("Attempt to trigger combat from null character reference");

	//	Current Character Animation Process
	//	yield return CombatAnimationManager.AnimateProcess(currentCharacter.Character.Anim, _status, null);

	//	Get skill
	//	bool isHit = calculator.IsHit(currentCharacter.CharacterStat.GetAccuracy());

	//	Get action value
	//	int actionValue = calculator.GetValue(_status, out bool isTargetingOpponent);

	//	Get Random Opponent from opposite party or
	//	Random on either Player or Opponent side if it's dance
	//	RPGCharacter opponent = (_status == CombatAnimationStatus.Dance && UnityEngine.Random.Range(0, 2) == 1) ? currentCharacter : RPGPartyManager.GetRandomOpponent(currentCharacter.CharacterParty);

	//	When hit
	//	if (isHit)
	//	{
	//		if (isTargetingOpponent)
	//		{
	//			Subtract opponent health
	//			opponent.CharacterStatInfo.SubtractHealth(actionValue, out bool isEmpty);

	//			Update UI for the opponent

	//		   CombatUIManager.UpdateCharacterStatusUI(opponent);

	//			Manual Combat Log here

	//		   CombatUIManager.AddCombatLog(CombatLogType.Damage_Target, currentCharacter.Name, _status.ToString(), actionValue, opponent.Name);

	//			Target Character Animation Process
	//			yield return CombatAnimationManager.AnimateProcess(opponent.Character.Anim, isEmpty ? CombatAnimationStatus.Fainted : CombatAnimationStatus.Damaged, () =>
	//			{
	//				Add action feedback here

	//				 When either one is fainted
	//				if (isEmpty)
	//				{
	//					if (opponent != currentCharacter)
	//					{
	//						Opponent fainted log
	//						CombatUIManager.AddCombatLog(CombatLogType.Fainted, opponent.Name);

	//						Current win
	//						CombatUIManager.AddCombatLog(CombatLogType.Winning, currentCharacter.Name);
	//					}
	//					else
	//					{
	//						Jinxed log
	//						CombatUIManager.AddCombatLog(CombatLogType.Jinxed, currentCharacter.Name);
	//					}

	//					End the game
	//					GameplayController.EndGame();
	//				}
	//			});
	//		}
	//		else
	//		{
	//			Add healing value to current character health
	//			currentCharacter.CharacterStatInfo.AddHealth(actionValue);

	//			Update UI for the opponent

	//		   CombatUIManager.UpdateCharacterStatusUI(currentCharacter);

	//			Manual Combat Log here

	//		   CombatUIManager.AddCombatLog(CombatLogType.Heal_Self, currentCharacter.Name, _status.ToString(), actionValue);
	//		}
	//	}
	//	When missed
	//	else
	//	{
	//		Manual Combat Log here
	//		CombatUIManager.AddCombatLog(CombatLogType.Missed, currentCharacter.Name, _status.ToString());

	//		Target Character Animation Process
	//		if (isTargetingOpponent)
	//		{
	//			yield return CombatAnimationManager.AnimateProcess(opponent.Character.Anim, CombatAnimationStatus.Dodged, null);
	//		}
	//	}

	//	End action on this turn
	//   OnTurnEnd?.Invoke();
	//}
	#endregion

	#region Global Method
	public static void InvokePlayerAction(SkillData _skill)
	{
		print($"Invoked: {_skill}");

		OnPlayerAction?.Invoke(_skill);
	}
	#endregion

	private void PlayerAction(SkillData _skill)
	{
		if (currentCharacter != null)
		{
			if (currentCharacter.CharacterParty == RPG_Party.Ally)
			{
				// Disable UI
				CombatUIManager.OnCombatActionRegistered();

				RPGActionCoroutine(_skill);
			}
		}
	}

	private void RPGActionCoroutine(SkillData _skill)
	{
		if (CombatCor != null)
		{
			StopCoroutine(CombatCor);
		}

		CombatCor = StartCoroutine(RPGActionUpdate(_skill));
	}

	// Process of updating the entire combat situation and provide results
	private IEnumerator RPGActionUpdate(SkillData _skill)
	{
		if (currentCharacter == null)
			throw new Exception("Attempt to trigger combat from null character reference");

		// Skill animation
		CombatAnimationStatus anim = _skill.skillAnim;

		// Get result from the action
		CombatActionInfo[] results = calculator.GetResults(RPG_Party.Ally, new RPGCharacter[], 0);

		// Current Character Animation Process
		yield return CombatAnimationManager.AnimateProcess(currentCharacter.Character.Anim, anim, null);

		// Get skill
		bool isHit = calculator.IsHit(currentCharacter.CharacterStat.GetAccuracy());

		// Get action value
		int actionValue = calculator.GetValue(anim, out bool isTargetingOpponent);

		// Get Random Opponent from opposite party or
		// Random on either Player or Opponent side if it's dance
		RPGCharacter opponent = (anim == CombatAnimationStatus.Dance && UnityEngine.Random.Range(0, 2) == 1) ? currentCharacter : RPGPartyManager.GetRandomOpponent(currentCharacter.CharacterParty);

		// When hit
		if (isHit)
		{
			if (isTargetingOpponent)
			{
				// Subtract opponent health
				opponent.CharacterStatInfo.SubtractHealth(actionValue, out bool isEmpty);

				// Update UI for the opponent
				CombatUIManager.UpdateCharacterStatusUI(opponent);

				// Manual Combat Log here
				CombatUIManager.AddCombatLog(CombatLogType.Damage_Target, currentCharacter.Name, anim.ToString(), actionValue, opponent.Name);

				// Target Character Animation Process
				yield return CombatAnimationManager.AnimateProcess(opponent.Character.Anim, isEmpty ? CombatAnimationStatus.Fainted : CombatAnimationStatus.Damaged, () =>
				{
					// Add action feedback here

					// When either one is fainted
					if (isEmpty)
					{
						if (opponent != currentCharacter)
						{
							// Opponent fainted log
							CombatUIManager.AddCombatLog(CombatLogType.Fainted, opponent.Name);

							// Current win
							CombatUIManager.AddCombatLog(CombatLogType.Winning, currentCharacter.Name);
						}
						else
						{
							// Jinxed log
							CombatUIManager.AddCombatLog(CombatLogType.Jinxed, currentCharacter.Name);
						}

						// End the game
						GameplayController.EndGame();
					}
				});
			}
			else
			{
				// Add healing value to current character health
				currentCharacter.CharacterStatInfo.AddHealth(actionValue);

				// Update UI for the opponent
				CombatUIManager.UpdateCharacterStatusUI(currentCharacter);

				// Manual Combat Log here
				CombatUIManager.AddCombatLog(CombatLogType.Heal_Self, currentCharacter.Name, anim.ToString(), actionValue);
			}
		}
		// When missed
		else
		{
			// Manual Combat Log here
			CombatUIManager.AddCombatLog(CombatLogType.Missed, currentCharacter.Name, anim.ToString());

			// Target Character Animation Process
			if (isTargetingOpponent)
			{
				yield return CombatAnimationManager.AnimateProcess(opponent.Character.Anim, CombatAnimationStatus.Dodged, null);
			}
		}

		// End action on this turn
		OnTurnEnd?.Invoke();
	}


}
