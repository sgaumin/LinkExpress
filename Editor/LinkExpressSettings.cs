using UnityEditor;
using UnityEngine;

namespace LinkExpress
{
	public class LinkExpressSettings : ScriptableObject
	{
		public const string k_LinkExpressSettingsPath = "Assets/LinkExpressSettings.asset";

		[SerializeField] private string github;
		[SerializeField] private string itch;
		[SerializeField] private string gamejam;
		[SerializeField] private string trello;
		[SerializeField] private string google_drive;
		[SerializeField] private string twitter;

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
			switch (category)
			{
				case LinkExpressCategory.Github:
					return github;
				case LinkExpressCategory.Itch:
					return itch;
				case LinkExpressCategory.GameJam:
					return gamejam;
				case LinkExpressCategory.Trello:
					return trello;
				case LinkExpressCategory.Google_Drive:
					return google_drive;
				case LinkExpressCategory.Twitter:
					return twitter;
				default:
					return "";
			}
		}
	}
}