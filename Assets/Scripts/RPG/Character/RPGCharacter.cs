using UnityEngine;

public class RPGCharacter : MonoBehaviour
{
	// The SO reference for the RPG Stat, only reference, is not allowed to modified.
	[SerializeField] private RPGStat characterStatSO;

	// The direct reference for the RPG Stat, the information that will modified and saved.
	private RPGStat characterStat;

	// The separate class reference for RPG Stat, the information that will be modified and referred throughout but will not be saved.
	[SerializeField] private RPGCharacterInfo characterInfo;

	/// <summary>
	/// <para> Setup character stat (Prohibited to modify except on actual stat allocation/upgrades) </para>
	/// <para> Setup character stat info (Allowed to modify in any scenarios, combat, UI etc.) </para>
	/// <para> Only initialized once at the beginning </para>
	/// </summary>
	public void SetCharacter(RPGStat _stat)
	{
		characterStat = _stat;

		characterInfo = new RPGCharacterInfo(_stat);
	}

	public RPGStat GetCharacterStat() => characterStat;

	public RPGCharacterInfo GetCharacterStatInfo() => characterInfo;
}
