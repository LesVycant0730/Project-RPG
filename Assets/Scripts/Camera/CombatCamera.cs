using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG_Data;

public class CombatCamera : CameraBase
{
	protected override void Awake()
	{
		base.Awake();

		RPGPartyManager.OnCharacterTurn += OnCharacterTurn;
		RPGPartyManager.OnTargetSelected += OnTargetSelected;
	}

	protected void OnDestroy()
	{
		RPGPartyManager.OnCharacterTurn -= OnCharacterTurn;
		RPGPartyManager.OnTargetSelected -= OnTargetSelected;
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
	}

	public override void Enable()
	{
		base.Enable();
	}

	public override void Disable()
	{
		base.Disable();
	}

	private void OnCharacterTurn(RPGCharacter _character, RPG_Party _party)
	{
		if (_party == RPG_Party.Ally)
		{
			current = _character.Character.RootTransform;
		}
	}

	private void OnTargetSelected(RPGCharacter _target)
	{
		target = _target.Character.RootTransform;
	}

	public override void SetCameraCurrent(Transform _transform)
	{
		base.SetCameraCurrent(_transform);
	}

	public override void SetCameraTarget(Transform _transform)
	{
		base.SetCameraTarget(_transform);
	}
}
