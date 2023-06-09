using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LinkExpress
{
	// Register a SettingsProvider using IMGUI for the drawing framework:
	static class LinkExpressSettingsIMGUIRegister
	{
		[SettingsProvider]
		public static SettingsProvider CreateMyCustomSettingsProvider()
		{
			var settings = LinkExpressSettings.GetSerializedSettings();
			// First parameter is the path in the Settings window.
			// Second parameter is the scope of this setting: it only appears in the Project Settings window.
			var provider = new SettingsProvider("Project/LinkExpress", SettingsScope.Project)
			{
				// By default the last token of the path is used as display name if no label is provided.
				label = "Link Express",

				// Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
				guiHandler = (searchContext) =>
				{
					// For safety, we are making sure that we can only edit settings when editor is not playing.
					EditorGUI.BeginDisabledGroup(Application.isPlaying);

					SerializedProperty entries = settings.FindProperty("entries");
					EditorGUILayout.PropertyField(entries, true);

					EditorGUILayout.PropertyField(settings.FindProperty("allowMovement"));
					EditorGUILayout.PropertyField(settings.FindProperty("allowColorVariation"));

					settings.ApplyModifiedProperties();
					EditorGUI.EndDisabledGroup();
				},

				// Populate the search keywords to enable smart search filtering and label highlighting:
				// TODO: Try to make this list dynamically populated.
				keywords = new HashSet<string>(new[] { "Github", "Itch", "GameJam", "Trello", "Google_Drive", "Twitter", "Other" })
			};

			return provider;
		}
	}
}