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
			window.titleContent = new GUIContent("Link Express");
			window.Show();
		}

		private void OnGUI()
		{
			// TODO: Add Icon on each button.
			LinkExpressSettings settings = LinkExpressSettings.GetOrCreateSettings();
			foreach (LinkExpressSettingsEntry entry in settings.Entries)
			{
				if (!string.IsNullOrEmpty(entry.link))
				{
					if (GUILayout.Button(entry.category.CleanFormat()))
					{
						Application.OpenURL(entry.link);
					}
				}
			}
		}
	}
}