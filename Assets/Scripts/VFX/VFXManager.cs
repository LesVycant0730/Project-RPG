using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
	private static VFXManager instance;

	[SerializeField] private VFXPrefabDirectorySO directory;

	private Dictionary<string, GameObject> loadedDirectory = new Dictionary<string, GameObject>();

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

			if (!instance.loadedDirectory.TryGetValue(_id, out GameObject vfxAsset))
			{
				vfxAsset = await instance.directory.LoadVFX(_id);
				instance.loadedDirectory.Add(_id, vfxAsset);
			}

			GameObject vfxObj = Instantiate(vfxAsset, _pos, Quaternion.identity);
			vfxObj.AddComponent<VFX_Base>();

			return vfxObj;
		}

		return null;
	}

	public static async Task<GameObject> GetVFX(string _id, Character _char)
	{
		if (instance != null)
		{
			if (string.IsNullOrEmpty(_id))
				_id = "Default";

			if (!instance.loadedDirectory.TryGetValue(_id, out GameObject vfxAsset))
			{
				vfxAsset = await instance.directory.LoadVFX(_id);
				instance.loadedDirectory.Add(_id, vfxAsset);
			}

			GameObject vfxObj = Instantiate(vfxAsset, _char.ModelCenter, Quaternion.identity);

			// Add VFX Mesh component and set character skin mesh reference
			vfxObj.AddComponent<VFX_Mesh>().SetMesh(_char.Model);

			return vfxObj;
		}

		return null;
	}
}
