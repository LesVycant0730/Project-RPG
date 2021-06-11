using AnimationTypes;
using UnityEngine;

/// <summary>
/// The script only use for combat simulation and testing.
/// </summary>
public class CombatCalculator : MonoBehaviour
{
	[Header ("Accuracy")]
	[SerializeField, Range(0.0f, 1.0f)] private float maxAccuracyNeeded = 1.0f;

	public bool IsHit(float _accuracy)
	{
		return _accuracy >= Random.Range(0.0f, maxAccuracyNeeded);
	}

	[Header("Action")]
	[SerializeField, Range(0.0f, 999.0f)] private float maxNormalAttackDamage = 100.0f; 
	[SerializeField, Range(0.0f, 999.0f)] private float maxNormalSpellDamage = 150.0f;
	[SerializeField, Range(0.0f, 999.0f)] private float maxFireSpellDamage = 200.0f;
	[SerializeField, Range(0.0f, 999.0f)] private float maxHealSpellValue = 200.0f;

	public float GetValue(CombatAnimationStatus _status, out bool _isSelfTarget)
	{
		_isSelfTarget = _status == CombatAnimationStatus.Healed;

		switch (_status)
		{
			case CombatAnimationStatus.Normal_Attack:
				return maxNormalAttackDamage;
			case CombatAnimationStatus.Spell_Ally:
				return maxNormalSpellDamage;
			case CombatAnimationStatus.Spell_Attack:
				return maxFireSpellDamage;
			case CombatAnimationStatus.Healed:
				return maxHealSpellValue;
			default:
				break;
		}

		return 0;
	}
}
