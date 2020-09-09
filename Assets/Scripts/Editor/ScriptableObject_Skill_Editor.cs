using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(SkillData))]
public class ScriptableObject_Skill_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		GUILayout.Space(20);

		SkillData data = (SkillData)target;

		if (GUILayout.Button ("Refresh") && GetRefreshDialogue())
		{
			data.Refresh();
			SetDirty(data);
		}

		if (GUILayout.Button ("Reset to Default") && GetResetDialogue())
		{
			data.Reset();
			SetDirty(data);
		}

		if (GUILayout.Button ("Trigger Effects"))
		{
			data.SimulateEffects();
		}

		GUILayout.Space(20);

		base.OnInspectorGUI();
	}

	private bool GetRefreshDialogue()
	{
		return EditorUtility.DisplayDialog("Refresh data", "Do you want to reset data (Multiple values will be clamped)?", "Yes", "No");
	}

	private bool GetResetDialogue()
	{
		return EditorUtility.DisplayDialog("Reset to default", "Are you sure to reset everything back to default?", "Yes", "No");
	}

	private void SetDirty(Object obj)
	{
		EditorUtility.SetDirty(obj);
	}
}
