using UnityEngine;
using RPG_Data;
using RoboRyanTron.SearchableEnum;

[CreateAssetMenu(fileName = "Skill Data", menuName = "ScriptableObjects/Skill Data", order = 1)]
public class SkillData : ScriptableObject
{
    [SerializeField] private bool ToggleOnValidate = false;
    [Space (10)]

    // Skill Info Section
    [Header("Skill Info"), SearchableEnum]
    public Skill_Name skillName;

    [TextArea(0, 100)]
    public string skillDescription;
    //
    [Header ("Skill Detail")]
	public Skill_Class skillClass = Skill_Class.None;

    public bool hasUserClassRestriction;

    [ConditionalHide(false, ConditionalSourceField = "hasUserClassRestriction")]
    public User_Class[] userClass;

    [Header("Skill Effects")]
    [Space(20)]

    [SerializeField]
    // Skill Effect Section
    public SkillEffectCollection skillEffectCollection;

	#region Data Check
	private void OnValidate()
	{
        if (!ToggleOnValidate)
		{
            return;
		}

		if (skillDescription == "")
		{
            skillDescription = "This skill is called " + skillName;
		}
	}
	#endregion

	#region Action
	public void Reset()
	{
        if (Application.isPlaying)
		{
            return;
		}

        skillClass = Skill_Class.None;
        hasUserClassRestriction = false;
        userClass = null;
        skillDescription = string.Empty;
        skillEffectCollection = null;
	}

    /// <summary>
    /// Editor only, simulate the existing skill effect elements inside this SkillEffect data array.
    /// </summary>
    public void SimulateEffects()
	{
        if (skillEffectCollection == null)
		{
            return;
		}

        SkillEffect[] skillEffects = skillEffectCollection.EffectArray();

        foreach (var effect in skillEffects)
		{
            if (effect == null)
			{
                Debug.LogError("There's empty skill effect element in " + name);
			}
            else
			{
                effect.Trigger();
			}
		}
	}

	public void Refresh()
	{
        bool validateDefault = ToggleOnValidate;

        ToggleOnValidate = true;
        OnValidate();
        ToggleOnValidate = validateDefault;

        Debug.Log("Refresh");
    }
	#endregion
}
