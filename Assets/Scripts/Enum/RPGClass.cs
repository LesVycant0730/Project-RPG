namespace RPG_Data
{
    public enum User_Class
    {
        Leader,
        Sorcerer,
        Pyromancer,
        Cleric,
        Navigator,
        Guide,

        None = 99
    }

    public enum Enemy_Class
    {
        Ghoul,
        Spirit,
        Giant,
        Undead
    }

    public enum RPG_Stat
	{
        Strength, Agility, 
        Intelligence, Endurance, 
        Resistance, Luck
    }

    public enum RPG_Substat
	{
        Health_Point, Stamina_Point,
        Physical_Damage, Magical_Damage, 
        Critical_Rate, Critical_Damage, 
        Accuracy, Resistance, 
        Physical_Defense, Magical_Defense, 
        Evasion, Speed
    }

    public enum Skill_Class
    {
        // Single target
        Physical_Damage,
        Magic_Damage,
        Heal_Support,
        Aura_Support,

        // AOE
        AOE_Physical_Damage,
        AOE_Magic_Damage,
        AOE_Heal_Support,
        AOE_Aura_Support,

        Multiple,

        None = 99
    }

    public enum Skill_Effect_Type
    {
        Damage,
        Heal,
        Cleanse,
        Stun,
        Immunity,
        Cure_Poison,
        Cure_Confusion,
        InstaKill
    }

    public enum Skill_Effect
    {
        Physical_Normal,
        Physical_Pierce,
        Physical_Pure,
        Magic_Normal,
        Magic_Pierce,
        Magic_Pure,
        HP_Reduction,
        HP_Absorption,
        MP_Reduction,
        MP_Absorption,
        Sleep,
        Stun,
        Taunt
    }

    public enum Skill_Name
	{

	}

    public enum Combat_Action
	{
        // Phase 1
        Default,

        // Phase 2
        Target_Attack,
        Skill_Selection,
        Assist,
        Item_Selection,
        Target_Check,

        // Phase 3 (Action End)
        Target_Skill_Enemy,
        Target_Skill_Ally,
        Target_Assist_Ally,

        // No Phase (Action End)
        Defend,
        Escape
	}
}

