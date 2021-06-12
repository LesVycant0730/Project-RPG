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
	[SerializeField, Range(0, 999)] private int maxNormalAttackDamage = 100; 
	[SerializeField, Range(0, 999)] private int maxHeavyAttackDamage = 200;
	[SerializeField, Range(0, 999)] private int maxNormalSpellDamage = 150;
	[SerializeField, Range(0, 999)] private int maxHeavySpellDamage = 350;
	[SerializeField, Range(0, 999)] private int maxFireSpellDamage = 200;
	[SerializeField, Range(0, 999)] private int maxHealSpellValue = 200;

	public int GetValue(CombatAnimationStatus _status, out bool _isTargetingOpponent)
	{
		_isTargetingOpponent = _status != CombatAnimationStatus.Self_Heal;

		switch (_status)
		{
			case CombatAnimationStatus.Normal_Kick:
				return maxNormalAttackDamage;
			case CombatAnimationStatus.Magic_Missile:
				return maxNormalSpellDamage;
			case CombatAnimationStatus.Fireball:
				return maxFireSpellDamage;
			case CombatAnimationStatus.Self_Heal:
				return maxHealSpellValue;
			case CombatAnimationStatus.Heavy_Kick:
				return maxHeavyAttackDamage;
			case CombatAnimationStatus.Magic_Bomb:
				return maxHeavySpellDamage;
			case CombatAnimationStatus.Dance:
				return 9999;
			default:
				break;
		}

		return 0;
	}
}
