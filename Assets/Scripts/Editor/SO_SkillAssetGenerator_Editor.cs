using RPG_Data;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SO_SkillAssetGenerator_Editor : EditorWindow
{
	private const string path = "Assets/ScriptableObject/Skill/";

	[MenuItem ("Custom Tools/Skill Asset/Generator")]
	static void Init()
	{
		SO_SkillAssetGenerator_Editor window = (SO_SkillAssetGenerator_Editor)GetWindow(typeof(SO_SkillAssetGenerator_Editor));

		window.Show();
	}

	private void OnGUI()
	{
		GUILayout.Label("Settings", EditorStyles.boldLabel);

		GUILayout.Space(10);

		if (GUILayout.Button("Generate Skill SO"))
		{
			GenerateAsset();
		}

		GUILayout.Space(10);

		if (GUILayout.Button("Delete Obsolete Skill SO"))
		{
			DeleteObsoletes();
		}
	}

	private void GenerateAsset()
	{
		int optionIndex = GetGeneratorOption();

		if (optionIndex == 1)
		{
			return;
		}

		for (int i = 0; i < (int)Skill_Name.FINAL_INDEX; i++)
		{
			Skill_Name skill = (Skill_Name)i;
			string assetName = $"Skill_{skill}.asset";

			if (optionIndex == 0 && File.Exists(path + assetName))
			{
				continue;
			}
			else
			{
				var obj = CreateInstance<SkillData>();
				obj.skillName = skill;
				obj.skillDescription = "This skill is " + skill.ToString().Replace("_", " ");
				AssetDatabase.CreateAsset(obj, path + assetName);
				AssetDatabase.SaveAssets();
			}
		}
	}

	private int GetGeneratorOption()
	{
		return EditorUtility.DisplayDialogComplex("Generate Skill Assets.", "Choose your generate options.", "Normal (No override)", "Back", "Force (Override)");
	}

	private void DeleteObsoletes()
	{
		// Current existing assets
		string[] files = Directory.GetFiles(path, "*asset");

		// All intended skill asset paths
		string[] skillAssetPaths = new string[(int)Skill_Name.FINAL_INDEX];

		for (int i = 0; i < (int)Skill_Name.FINAL_INDEX; i++)
		{
			skillAssetPaths[i] = $"{path}Skill_{(Skill_Name)i}.asset";
		}

		foreach (var obsoletePath in files.Except(skillAssetPaths))
		{
			Debug.Log($"Deleted obsolete asset: {obsoletePath}");
			AssetDatabase.DeleteAsset(obsoletePath);
		}
	}
}
