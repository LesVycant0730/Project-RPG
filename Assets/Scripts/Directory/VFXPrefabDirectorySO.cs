using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "VFX Prefab Directory", menuName = "ScriptableObjects/Directory/VFX", order = 1)]
public class VFXPrefabDirectorySO : PrefabDirectorySO
{
	[SerializeField] private VFXAssetReference[] vfxDirectory;

	public override bool HasDirectory()
	{
		return !Utility.IsNullOrEmpty(vfxDirectory);
	}

	private VFXAssetReference GetVFX(string _id)
	{
		return vfxDirectory.Single(x => x.IsSameID(_id));
	}

	/// <summary>
	/// Prepare VFX asset
	/// </summary>
	/// <param name="_id"></param>
	/// <param name="_pos"></param>
	/// <returns></returns>
	public async Task<GameObject> LoadVFX(string _id, Vector3 _pos)
	{
		VFXAssetReference vfxAsset = GetVFX(_id);

		if (vfxAsset != null)
		{
			GameObject vfx = await AddressablesUtility.LoadAsset<GameObject>(vfxAsset.AssetRef, (handle) =>
			{
				CustomLog.Log($"Loaded vfx: {_id}");
			});

			return vfx;
		}
		else
			return null;
	}
}
