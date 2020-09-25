using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Animation), true)]
public class SO_Animation_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		GUILayout.Space(20);

		Animation data = (Animation)target;

		if (GUILayout.Button("Generate Animation Array") && CheckAnimationArray())
		{
			data.GenerateAnimationArray();
		}

		GUILayout.Space(20);

		base.OnInspectorGUI();
	}

	private bool CheckAnimationArray()
	{
		return EditorUtility.DisplayDialog("Generate Animation", "All previous data will be lost and regenerated, still proceed?", "Yes", "No");
	}
}
