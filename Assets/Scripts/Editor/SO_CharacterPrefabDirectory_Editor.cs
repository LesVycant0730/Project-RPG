using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(CharacterPrefabDirectorySO))]
public class SO_CharacterPrefabDirectory_Editor : SO_PrefabDirectory_Editor
{
	public override void OnInspectorGUI()
	{
		CharacterPrefabDirectorySO so = (CharacterPrefabDirectorySO)target;

		base.OnInspectorGUI();

		// Buttons
		if (GUILayout.Button("Generate Full Character Array"))
		{
			if (!so.HasDirectory() || GetDirectoryGenerateConfirmationDialogue())
				so.EditorGenerateFullDirectoryArray();
		}

		GUILayout.Box("- Will reset the entire array to a new array with all Character_ID and empty references.");

		if (GUILayout.Button("Refresh Character Array"))
		{
			if (!so.HasDirectory() || GetDirectoryGenerateConfirmationDialogue())
				so.EditorGenerateMissingDirectoryArray();
		}

		GUILayout.Box("- Will refresh the array, retain, add back missing elements, and reorder elements.");

		if (GUILayout.Button("Check Character Array"))
		{
			if (!so.HasDirectory() || GetDirectoryGenerateConfirmationDialogue())
				so.CheckDirectory();
		}

		GUILayout.Box("- Check array elements and prompt warning if have any wrong element order, duplication, or missing references");
		//

		GUILayout.Space(30);
		
		// Draw default base inspector from editor
		DrawDefaultInspector();
	}

	private bool GetDirectoryGenerateConfirmationDialogue()
	{
		return EditorUtility.DisplayDialog("Generate Full Directory Elements", "There are data elements, are you sure want to set all elements back to default values?", "Yes", "No");
	}
}
