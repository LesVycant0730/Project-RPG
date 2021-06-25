using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEditor;

public class Shortcut_Editor : MonoBehaviour
{
	private const string assetDirectory = "Assets/Scene/";

	[Shortcut ("Custom/To Testing Scene")]
	public static void ToTestingScene()
	{
		ToScene($"{assetDirectory}Testing_Scene.unity");
	}

	[Shortcut ("Custom/To Combat Scene")]
	public static void ToCombatScene()
	{
		ToScene($"{assetDirectory}Scene_01.unity");
	}

	private static void ToScene(string _scene)
	{
		if (EditorSceneManager.GetActiveScene().isDirty)
		{
			int i = EditorUtility.DisplayDialogComplex("Save scene changes", "You have unsaved changes on the scene, do you want to save and proceed?", "Yes", "No", "Cancel");

			if (i == 0)
			{
				EditorSceneManager.SaveOpenScenes();
			}
			else if (i == 2)
			{
				return;
			}

			EditorSceneManager.OpenScene(_scene);
		}
		else
			EditorSceneManager.OpenScene(_scene);
	}
}
