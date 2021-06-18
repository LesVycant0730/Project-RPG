using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.VFX;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "VFX Prefab Directory", menuName = "ScriptableObjects/Directory/VFX", order = 1)]
public class VFXPrefabDirectorySO : PrefabDirectorySO
{
	[SerializeField] private VFXAssetReference[] vfxDirectory;

	public override bool HasDirectory()
	{
		return !Utility.IsNullOrEmpty(vfxDirectory);
	}

	public VFXAssetReference GetCharacter(string _id)
	{
		return vfxDirectory.Single(x => x.IsSameID(_id));
	}

	/// <summary>
	/// Prepare VFX asset
	/// </summary>
	/// <param name="_id"></param>
	/// <param name="_pos"></param>
	/// <returns></returns>
	public async Task<VisualEffect> LoadVFX(string _id, Vector3 _pos)
	{
		VFXAssetReference vfxAsset = GetCharacter(_id);

		if (vfxAsset != null)
		{
			VisualEffect vfx = await AddressablesUtility.LoadAsset<VisualEffect>(vfxAsset.AssetRef, (handle) =>
			{
				CustomLog.Log($"Loaded vfx: {_id}");
				Instantiate(handle.Result);
			});

			return vfx;
		}
		else
			return null;
	}
}
