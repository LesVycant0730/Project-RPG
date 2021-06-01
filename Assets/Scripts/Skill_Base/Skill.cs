using UnityEngine;
using RPG_Data;

// Note: The base class for skill
[System.Serializable]
public class Skill : IRPGAction
{
    [SerializeField]
    private SkillData skill_Data;

    public Skill(SkillData _skillData)
	{
        skill_Data = _skillData;
	}

    public string GetName()
	{
        return skill_Data.skillName.ToString();
	}

    public Skill_Name GetEnum()
	{
        return skill_Data.skillName;
	}

    public int GetHPCost()
    {
        return skill_Data.hpCost;
    }

    public int GetSPCost()
    {
        return skill_Data.spCost;
    }

    public bool HasClassRestriction()
    {
        return skill_Data.hasUserClassRestriction;
    }

    public User_Class[] GetClass()
    {
        return skill_Data.userClass;
    }

    public string GetDescription()
    {
        return skill_Data.skillDescription;
    }

	void IRPGAction.ActionStart()
	{
		throw new System.NotImplementedException();
	}

	void IRPGAction.ActionEnd()
	{
		throw new System.NotImplementedException();
	}

	void IRPGAction.OnTurnStart()
	{
		throw new System.NotImplementedException();
	}

	void IRPGAction.OnTurnEnd()
	{
		throw new System.NotImplementedException();
	}
}
