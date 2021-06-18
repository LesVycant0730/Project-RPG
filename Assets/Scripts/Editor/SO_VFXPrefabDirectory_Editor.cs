using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(VFXPrefabDirectorySO))]
public class SO_VFXPrefabDirectory_Editor : SO_PrefabDirectory_Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		BaseInspectorGUI();
	}
}
