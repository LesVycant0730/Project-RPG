using RPG_Data;

namespace UI_Tooltip
{
    public enum Tooltip
	{
        Target_Attack,
        Skill_Selection,
        Assist,
        Item_Selection,
        Target_Check,
        Defend,
        Escape,

        // Will use individual tooltip reference
        Skill,
        Item
    }

    public static class TooltipBase
    {
        public static string GetBasicTooltip(Tooltip _tooltip)
		{
            return _tooltip.ToString();
		}

        public static string GetSkillTooltip(Skill_Name _name)
		{
            return SkillLibrary.GetSkill(_name).GetDescription();
		}
    }
}

