using System.Collections;
using AnimationTypes;
using UnityEngine;
using System;

public class CombatController : MonoBehaviour
{
	[SerializeField] private CombatCalculator calculator;

	private RPGCharacter currentCharacter;

	private Coroutine CombatCor = null;

	public static event Action OnTurnEnd;
	public static event Action<CombatAnimationStatus> OnPlayerAction;

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

	public void CheckTurn(RPGCharacter _char, RPG_Data.RPG_Party _party)
	{
		currentCharacter = _char;

		if (_char != null)
		{
			// If Player, combat action will require manual input
			if (_party == RPG_Data.RPG_Party.Ally)
			{
				return;
			}
			// If Enemy, combat action will automated
			else
			{
				CombatAnimationStatus randAnim = (CombatAnimationStatus)UnityEngine.Random.Range((int)CombatAnimationStatus.Normal_Attack, (int)CombatAnimationStatus.Spell_Ally + 1);

				CombatActionCoroutine(randAnim);
			}
		}
	}

	// Called in Button only
	public void RandomCombatAction()
	{
		CombatAnimationStatus[] randAnimArr = new CombatAnimationStatus[]
		{
			CombatAnimationStatus.Normal_Attack, CombatAnimationStatus.Spell_Ally,
			CombatAnimationStatus.Spell_Attack, CombatAnimationStatus.Healed
		};

		CombatAnimationStatus randAnim = randAnimArr[UnityEngine.Random.Range(0, randAnimArr.Length)];

		PlayerAction(randAnim);
	}

	public static void InvokePlayerAction(CombatAnimationStatus _status)
	{
		OnPlayerAction?.Invoke(_status);
	}

	private void PlayerAction(CombatAnimationStatus _status)
	{
		if (currentCharacter != null)
		{
			if (currentCharacter.CharacterParty == RPG_Data.RPG_Party.Ally)
			{
				// Disable UI
				CombatUIManager.OnCombatActionRegistered();

				CombatActionCoroutine(_status);
			}
		}
	}

	private void CombatActionCoroutine(CombatAnimationStatus _status)
	{
		if (CombatCor != null)
		{
			StopCoroutine(CombatCor);
		}

		CombatCor = StartCoroutine(CombatSimulation(_status));
	}

	// Only for simulating combat with animation and feedback used in demonstration 
	private IEnumerator CombatSimulation(CombatAnimationStatus _status)
	{
		if (currentCharacter == null)
			throw new Exception("Attempt to trigger combat from null character reference");

		// Current Character Animation Process
		yield return CombatAnimationManager.AnimateProcess(currentCharacter.Character.Anim, _status, null);

		// Get skill
		bool isHit = calculator.IsHit(currentCharacter.CharacterStat.GetAccuracy());

		// When hit
		if (isHit)
		{
			// Check if it's healing status
			int actionValue = calculator.GetValue(_status, out bool isTargetingOpponent);

			if (isTargetingOpponent)
			{
				// Get Random Opponent from opposite party
				RPGCharacter opponent = RPGPartyManager.GetRandomOpponent(currentCharacter.CharacterParty);

				// Subtract opponent health
				opponent.CharacterStatInfo.SubtractHealth(actionValue, out bool isEmpty);

				// Update UI for the opponent
				CombatUIManager.UpdateCharacterStatusUI(opponent);

				// Target Charater Animation Process
				yield return CombatAnimationManager.AnimateProcess(opponent.Character.Anim, isEmpty ? CombatAnimationStatus.Fainted : CombatAnimationStatus.Damaged, () =>
				{
					// Add action feedback here
				});
			}
			else
			{
				// Add healing value to current character health
				currentCharacter.CharacterStatInfo.AddHealth(actionValue);
			}
		}
		// When missed
		else
		{
			Debug.Log("Action missed");
		}

		// End action on this turn
		OnTurnEnd?.Invoke();
	}
}
