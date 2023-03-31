using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LinkExpress
{
	// Register a SettingsProvider using IMGUI for the drawing framework:
	static class LinkExpressSettingsIMGUIRegister
	{
		[SettingsProvider]
		public static SettingsProvider CreateMyCustomSettingsProvider()
		{
			// First parameter is the path in the Settings window.
			// Second parameter is the scope of this setting: it only appears in the Project Settings window.
			var provider = new SettingsProvider("Project/LinkExpress", SettingsScope.Project)
			{
				// By default the last token of the path is used as display name if no label is provided.
				label = "Link Express",
				// Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
				guiHandler = (searchContext) =>
				{
					var settings = LinkExpressSettings.GetSerializedSettings();
					foreach (LinkExpressCategory link in Enum.GetValues(typeof(LinkExpressCategory)))
					{
						string title = link.CleanFormat();
						EditorGUILayout.PropertyField(settings.FindProperty(link.ToString().ToLower()), new GUIContent(title));
					}
					settings.ApplyModifiedPropertiesWithoutUndo();
				},

				// Populate the search keywords to enable smart search filtering and label highlighting:
				keywords = new HashSet<string>(new[] { "Number", "Some String" })
			};

			return provider;
		}
	}
}