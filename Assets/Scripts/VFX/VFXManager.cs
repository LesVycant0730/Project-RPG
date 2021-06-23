using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
	private static VFXManager instance;

	[SerializeField] private VFXPrefabDirectorySO directory;

	private Dictionary<string, GameObject> loadedDirectory = new Dictionary<string, GameObject>();

	public List<GameObject> loadedVFXList = new List<GameObject>();

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

				GameObject go = Instantiate(newVFX, _pos, Quaternion.identity);

				go.AddComponent<VFX_Base>();

				return go;
			}
		}

		return null;
	}

	public static async Task<GameObject> GetVFX(string _id, Character _char)
	{
		if (instance != null)
		{
			if (string.IsNullOrEmpty(_id))
				_id = "Default";

			if (instance.loadedDirectory.TryGetValue(_id, out GameObject vfx))
			{
				return Instantiate(vfx, _char.ModelCenter, Quaternion.identity);
			}
			else
			{
				GameObject newVFX = await instance.directory.LoadVFX(_id, _char.ModelCenter);

				instance.loadedDirectory.Add(_id, newVFX);

				GameObject go = Instantiate(newVFX, _char.ModelCenter, Quaternion.identity);

				VFX_Mesh meshVFX = go.AddComponent<VFX_Mesh>();
				meshVFX.SetMesh(_char.Model);

				return go;
			}
		}

		return null;
	}

	public static void AddVFX(GameObject _vfx)
	{
		if (instance != null && !instance.loadedVFXList.Contains(_vfx))
			instance.loadedVFXList.Add(_vfx);
	}

	public static void RemoveVFX(GameObject _vfx)
	{
		if (instance != null)
			instance.loadedVFXList.Remove(_vfx);
	}
}
