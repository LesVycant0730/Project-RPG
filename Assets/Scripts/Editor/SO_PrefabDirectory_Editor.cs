using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(PrefabDirectorySO))]
public class SO_PrefabDirectory_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		// Warning Zone
		GUI.color = Color.red;

		GUILayout.Box("WARNING!!! Unless you know what you're doing, PLEASE avoid adding/removing element manually, or changing the Id for the element.\n Use the provided buttons to update the elements.");

		GUI.color = Color.white;

		GUILayout.Space(10);
		//
	}

	protected void BaseInspectorGUI()
	{
		base.OnInspectorGUI();
	}
}
