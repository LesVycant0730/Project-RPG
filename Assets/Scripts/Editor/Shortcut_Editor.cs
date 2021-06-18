using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class Shortcut_Editor : MonoBehaviour
{
	private const string assetDirectory = "Assets/Scene/";

	[Shortcut ("Custom/To Testing Scene")]
	public static void ToTestingScene()
	{
		EditorSceneManager.OpenScene($"{assetDirectory}Testing_Scene.unity");
	}

	[Shortcut ("Custom/To Combat Scene")]
	public static void ToCombatScene()
	{
		EditorSceneManager.OpenScene($"{assetDirectory}Scene_01.unity");
	}
}
