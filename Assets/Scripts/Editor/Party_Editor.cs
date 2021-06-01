using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(RPGPartyManager))]
public class Party_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		RPGPartyManager party = (RPGPartyManager)target;

		if (GUILayout.Button("Default to SO (Cached)"))
		{
			party.DefaultPartySOStats(true);
		}

		if (GUILayout.Button("Default to SO (Non-Cached, From Library and Runtime Only)"))
		{
			party.DefaultPartySOStats(false);
		}

		GUILayout.Space(30);

		base.OnInspectorGUI();

	}
}
