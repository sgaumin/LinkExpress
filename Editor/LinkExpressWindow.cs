using System;
using UnityEditor;
using UnityEngine;

namespace LinkExpress
{
	/// <summary>
	/// Editor window which redirects to useful links.
	/// </summary>
	public class LinkExpressWindow : EditorWindow
	{
		[MenuItem("Tools/Link Express")]
		private static void Init()
		{
			// Get existing open window or if none, make a new one:
			LinkExpressWindow window = (LinkExpressWindow)EditorWindow.GetWindow(typeof(LinkExpressWindow));

			Texture texture = null;
			string[] guids = AssetDatabase.FindAssets("LinkExpressIcon");
			if (guids.Length > 0)
			{
				// We are assuming that there is one file using this name, so we are taking the first index.
				texture = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(guids[0]));
			}

			window.titleContent = new GUIContent("Link Express", texture);
			window.Show();
		}

		private void OnGUI()
		{
			// TODO: Add Icon on each button.
			LinkExpressSettings settings = LinkExpressSettings.GetOrCreateSettings();
			foreach (LinkExpressSettingsEntry entry in settings.Entries)
			{
				if (!string.IsNullOrEmpty(entry.Link))
				{
					GUI.backgroundColor = entry.BackgroundColor;
					GUI.contentColor = entry.TextColor;
					if (GUILayout.Button(entry.Category.CleanFormat()))
					{
						Application.OpenURL(entry.Link);
					}
				}
			}
		}
	}
}