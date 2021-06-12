
namespace AnimationTypes
{
	public enum AnimationType
	{
		Exploration,
		Combat,
		Cutscene,
		Misc
	}
	public enum CombatAnimationStatus
	{
		// Start, End
		Battle_Start, Battle_End,
		
		// Idle
		Idle, Idle_Hurt, Idle_Stun,
		
		// General
		Selected, 

		// Action 
		Normal_Attack, Spell_Attack, Spell_Ally,
		Action_Failed, 
		
		// Feedback
		Damaged, Healed, Fainted, Dodged,

		// Demo
		Self_Heal, Normal_Kick, Magic_Missile,
		Fireball,

		// Demo 2
		Heavy_Kick, Magic_Bomb,

		// Extra
		Dance
	}
}

