using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusUI : CharacterStatusUI
{
	public override void Enable()
	{
		base.Enable();
	}

	public override void Disable()
	{
		base.Disable();
	}

	public override void UpdateName(string _name)
	{
		base.UpdateName(_name);
	}

	public override void UpdateHealth(float _current, float _max)
	{
		base.UpdateHealth(_current, _max);
	}
}
