using System;
using UnityEngine;

public static class UtilVFX
{
	public static Type GetVFXType(VFX_Type type)
	{
		switch (type)
		{
			case VFX_Type.Standard:
				return typeof(VFX_Base);
			case VFX_Type.Mesh:
				return typeof(VFX_Mesh);
			default:
				return typeof(VFX_Base);
		}
	}

	public static void AddVFXComponent(GameObject obj, VFX_Type type)
	{

	}
}
