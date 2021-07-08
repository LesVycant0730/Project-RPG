using AnimationTypes;
using RPG_Data;

// Note: The base class for character skill action
[System.Serializable]
public class ChAction_Skill : IRPGCharacterAction
{
    public SkillData Data { get; private set; }

    public ChAction_Skill(SkillData _data)
	{
        Data = _data;
	}

    public string GetName()
	{
        return Data.skillName.ToString();
	}

    public Skill_Name GetEnum()
	{
        return Data.skillName;
	}

    public int GetHPCost()
    {
        return Data.hpCost;
    }

    public int GetSPCost()
    {
        return Data.spCost;
    }

    public bool HasClassRestriction()
    {
        return Data.hasUserClassRestriction;
    }

    public User_Class[] GetClass()
    {
        return Data.userClass;
    }

    public string GetDescription()
    {
        return Data.skillDescription;
    }

    public string GetVFXName()
	{
        return Data.vfxName;
	}

    public CombatAnimationStatus GetAnimationType()
	{
        return Data.skillAnim;
	}

	void IRPGCharacterAction.ActionStart()
	{
        UnityEngine.Debug.Log("Action Start");
	}

	void IRPGCharacterAction.ActionEnd()
	{
		throw new System.NotImplementedException();
	}

	void IRPGCharacterAction.OnTurnStart()
	{
		throw new System.NotImplementedException();
	}

	void IRPGCharacterAction.OnTurnEnd()
	{
		throw new System.NotImplementedException();
	}
}
