using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG_Data;
using System;
using System.Threading.Tasks;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
	private static VFXManager instance;

	[SerializeField] private VFXPrefabDirectorySO directory;

	private Dictionary<string, GameObject> loadedDirectory = new Dictionary<string, GameObject>();

	public List<GameObject> go = new List<GameObject>();

	private void Awake()
	{
        instance = this;
	}

	public static async Task<GameObject> GetVFX(string _id, Vector3 _pos)
	{
		if (instance != null)
		{
			if (string.IsNullOrEmpty(_id))
				_id = "Default";

			if (instance.loadedDirectory.TryGetValue(_id, out GameObject vfx))
			{
				return Instantiate(vfx, _pos, Quaternion.identity);
			}
			else
			{
				GameObject newVFX = await instance.directory.LoadVFX(_id, _pos);

				instance.loadedDirectory.Add(_id, newVFX);

				return Instantiate(newVFX, _pos, Quaternion.identity);
			}
		}

		return null;
	}
}
