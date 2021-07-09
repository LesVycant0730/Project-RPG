using RPG_Data;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary : BaseLibrary
{
	public static SkillLibrary Instance { get; private set; }

    [SerializeField] private SkillData[] skillDataSO;

	private readonly Dictionary<Skill_Name, ChAction_Skill> skillDict = new Dictionary<Skill_Name, ChAction_Skill>();

#if UNITY_EDITOR
	[ContextMenu ("Refresh SO")]
	private void RefreshSO()
	{
		var paths = GetLibraryAssets();

		skillDataSO = new SkillData[paths.Length];

		for (int i = 0; i < paths.Length; i++)
		{
			skillDataSO[i] = (SkillData)UnityEditor.AssetDatabase.LoadAssetAtPath(paths[i], typeof(SkillData));
		}
	}
#endif

	protected override void Awake()
	{
		base.Awake();
		Instance = this;
		SetupLibrary();
	}

	protected override void SetupLibrary()
	{
		foreach (var skillData in skillDataSO)
		{
			if (skillData != null)
			{
				ChAction_Skill data = new ChAction_Skill(skillData);
				skillDict.Add(data.GetEnum(), data);
			}
		}
	}

	// -> Expand more skill system
	/// <summary>
	/// Retrieve Skill reference through its name
	/// </summary>
	/// <param name="_skillName"></param>
	/// <returns></returns>
	public static ChAction_Skill GetSkill(Skill_Name _skillName)
	{
		if (Instance != null)
		{
			return Instance.skillDict[_skillName];
		}

		return new ChAction_Skill(null);
	}
}
