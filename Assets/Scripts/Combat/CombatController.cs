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
		RPGPartyManager.OnCharacterTurn += StartCombat;
	}

	private void OnDestroy()
	{
		RPGPartyManager.OnCharacterTurn -= StartCombat;
	}

	public void StartCombat(Character _char, RPG_Data.RPG_Party _party)
	{
		currentCharacter = _char;

		if (_char != null && _party == RPG_Data.RPG_Party.Enemy)
		{
			if (CombatCor != null)
			{
				StopCoroutine(CombatCor);
			}

			CombatCor = StartCoroutine(CombatAction());
		}
	}

	private IEnumerator CombatAction()
	{
		if (currentCharacter == null)
			throw new Exception("Attempt to trigger combat from null character reference");

		CombatAnimationStatus randAnim = (CombatAnimationStatus)UnityEngine.Random.Range(0, Utility.GetEnumLength<CombatAnimationStatus>());

		yield return CombatAnimationManager.WaitForAnimation(currentCharacter.Anim, randAnim, () =>
		{
			Debug.Log("hey hey");
			OnTurnEnd?.Invoke();
		});

	}
}
