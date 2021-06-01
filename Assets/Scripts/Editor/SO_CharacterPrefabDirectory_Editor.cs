using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(CharacterPrefabDirectorySO))]
public class SO_CharacterPrefabDirectory_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		CharacterPrefabDirectorySO so = (CharacterPrefabDirectorySO)target;

		// Warning Zone
		GUI.color = Color.red;

		GUILayout.Box("WARNING!!! Unless you know what you're doing, PLEASE avoid adding/removing element manually, or changing the Id for the element.\n Use the provided buttons to update the elements.");

		GUI.color = Color.white;

		GUILayout.Space(10);	
		//

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
		
		base.OnInspectorGUI();
	}

	private bool GetDirectoryGenerateConfirmationDialogue()
	{
		return EditorUtility.DisplayDialog("Generate Full Directory Elements", "There are data elements, are you sure want to set all elements back to default values?", "Yes", "No");
	}
}
