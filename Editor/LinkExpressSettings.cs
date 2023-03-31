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
			LinkExpressSettings settings = AssetDatabase.LoadAssetAtPath<LinkExpressSettings>(k_LinkExpressSettingsPath);
			if (settings == null)
			{
				// TODO: Look in project before to create another instance
				settings = ScriptableObject.CreateInstance<LinkExpressSettings>();
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
				if (entry.category == category)
				{
					return entry.link;
				}
			}

			return "";
		}
	}
}