using UnityEngine;
using RPG_Data;
using RoboRyanTron.SearchableEnum;
using AnimationTypes;

[CreateAssetMenu(fileName = "Skill Data", menuName = "ScriptableObjects/Skill Data", order = 1)]
public class SkillData : ScriptableObject
{
    [SerializeField] private bool ToggleOnValidate = false;
    [Space (10)]

    // Skill Info Section
    [Header("Skill Info"), SearchableEnum]
    public Skill_Name skillName;

    [TextArea(0, 100)]
    public string skillDescription = string.Empty;
    //
    [Header ("Skill Detail")]
	public Skill_Class skillClass = Skill_Class.None;

    public bool hasUserClassRestriction;

    [ConditionalHide(false, ConditionalSourceField = nameof(hasUserClassRestriction))]
    public User_Class[] userClass;

    [Header("Skill Cost")]
    public int hpCost, spCost;

    // Skill Effect Section
    [Header("Skill Effects")]
    [Space(20)]
    public SkillEffectCollection skillEffectCollection;

    // VFX and Animation
    [Header ("VFX && Animation")]
    public string vfxName;
    public CombatAnimationStatus skillAnim;

	#region Data Check
	private void OnValidate()
	{
        if (!ToggleOnValidate)
		{
            return;
		}

		if (string.IsNullOrEmpty(skillDescription))
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
