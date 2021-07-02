using AnimationTypes;
using RPG_Data;

// Note: The base class for skill
[System.Serializable]
public class Skill : IRPGAction
{
    public SkillData Data { get; private set; }

    public Skill(SkillData _data)
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
