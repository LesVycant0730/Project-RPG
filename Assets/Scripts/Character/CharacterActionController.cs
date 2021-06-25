using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Act as a controller to manage individual character action such as animation event
/// If the prefab is not attached, the component will be added through spawning.
/// </summary>
[DisallowMultipleComponent]
public class CharacterActionController : MonoBehaviour
{
	private Character character;

	// Called in animation event
    public async void RPGAction()
	{
		// Add action feedback here
		print("RPG Action");

		// Test spawn vfx
		await VFXManager.GetVFX("Healing_Mesh_01", character);
	}

	public void Setup(Character _character)
	{
		character = _character;
	}
}
