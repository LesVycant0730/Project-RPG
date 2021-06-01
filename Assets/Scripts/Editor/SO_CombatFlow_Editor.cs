using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CombatFlow))]
public class SO_CombatFlowUI_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		GUILayout.Space(20);

		CombatFlow flow = (CombatFlow)target;

		if (GUILayout.Button("Generate Default Elements"))
		{
			if (flow.HasActionFlow && GetDefaultElementDialogue())
			{
				flow.ToDefault();

			}
			else if (flow.HasActionFlow == false)
			{
				flow.ToDefault();
			}
		}

		if (GUILayout.Button("Clear All Elements"))
		{
			flow.Clear();
		}

		GUILayout.Space(20);

		base.OnInspectorGUI();
	}

	private bool GetDefaultElementDialogue()
	{
		return EditorUtility.DisplayDialog("Generate Default Elements", "There are data elements, are you sure want to set all elements back to default values?", "Yes", "No");
	}
}
