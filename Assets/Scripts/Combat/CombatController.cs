using System.Collections;
using AnimationTypes;
using UnityEngine;
using System;

public class CombatController : MonoBehaviour
{
	private Character currentCharacter;

	private Coroutine CombatCor = null;

	public static event Action OnTurnEnd;

	private void Awake()
	{
		RPGPartyManager.OnCharacterTurn += CheckTurn;
	}

	private void OnDestroy()
	{
		RPGPartyManager.OnCharacterTurn -= CheckTurn;
	}

	public void CheckTurn(Character _char, RPG_Data.RPG_Party _party)
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
				CombatActionCoroutine();
			}
		}
	}

	public void PlayerAction()
	{
		if (currentCharacter != null)
		{
			if (currentCharacter.Party == RPG_Data.RPG_Party.Ally)
			{
				CombatActionCoroutine();
			}
		}
	}

	private void CombatActionCoroutine()
	{
		if (CombatCor != null)
		{
			StopCoroutine(CombatCor);
		}

		CombatCor = StartCoroutine(CombatAction());
	}

	private IEnumerator CombatAction()
	{
		if (currentCharacter == null)
			throw new Exception("Attempt to trigger combat from null character reference");

		CombatAnimationStatus randAnim = (CombatAnimationStatus)UnityEngine.Random.Range((int)CombatAnimationStatus.Normal_Attack, (int)CombatAnimationStatus.Spell_Ally + 1);

		yield return CombatAnimationManager.AnimateProcess(currentCharacter.Anim, randAnim, () =>
		{
			OnTurnEnd?.Invoke();

			// Add action feedback here
		});

	}
}
