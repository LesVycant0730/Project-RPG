using UnityEngine;
using RPG_Data;

// Note: The base class for skill
public class Skill : IRPGAction
{
    [SerializeField]
    private SkillData skill_Data;

    public int GetHPCost()
    {
        return 0;
    }

    public int GetSPCost()
    {
        return 0;
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
