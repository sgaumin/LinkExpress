using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LinkExpress
{
	public class LinkExpressSettings : ScriptableObject
	{
		public const string k_LinkExpressSettingsPath = "Assets/LinkExpressSettings.asset";

		[SerializeField] private LinkExpressSettingsEntry[] entries;

		public LinkExpressSettingsEntry[] Entries => entries;

		internal static LinkExpressSettings GetOrCreateSettings()
		{
			LinkExpressSettings settings = null;
			string[] guids = AssetDatabase.FindAssets("t: LinkExpressSettings");
			if (guids.Length > 0)
			{
				// We are assuming that there is one settings file in the project, so we are taking the first index.
				settings = AssetDatabase.LoadAssetAtPath<LinkExpressSettings>(AssetDatabase.GUIDToAssetPath(guids[0]));
			}

			if (settings == null)
			{
				settings = CreateInstance<LinkExpressSettings>();
				AssetDatabase.CreateAsset(settings, k_LinkExpressSettingsPath);
				AssetDatabase.SaveAssets();
			}
			return settings;
		}

		internal static SerializedObject GetSerializedSettings()
		{
			return new SerializedObject(GetOrCreateSettings());
		}

		internal string GetLink(LinkExpressCategory category)
		{
			foreach (LinkExpressSettingsEntry entry in entries)
			{
				if (entry.Category == category)
				{
					return entry.Link;
				}
			}

			return "";
		}
	}
}