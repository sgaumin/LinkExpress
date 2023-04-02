using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace LinkExpress
{
	/// <summary>
	/// Editor window which redirects to useful links.
	/// </summary>
	public class LinkExpressWindow : EditorWindow
	{
		private List<AnimFloat> startHorizontalOffsets;
		private List<AnimFloat> currentHorizontalOffsets;
		private AnimFloat colorAnimator;

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

		private void OnEnable()
		{
			EditorApplication.playModeStateChanged += LogPlayModeState;
		}

		private void OnDisable()
		{
			EditorApplication.playModeStateChanged -= LogPlayModeState;
		}

		private void LogPlayModeState(PlayModeStateChange state)
		{
			switch (state)
			{
				case PlayModeStateChange.EnteredPlayMode:
					Setup();
					break;
				case PlayModeStateChange.ExitingPlayMode:
					Clear();
					break;
			}
		}

		private void Update()
		{
			if (Application.isPlaying)
			{
				UpdateAnimationParameters();
			}
		}

		private void OnGUI()
		{
			LinkExpressSettings settings = LinkExpressSettings.GetOrCreateSettings();
			int i = 0;
			foreach (LinkExpressSettingsEntry entry in settings.Entries)
			{
				if (!string.IsNullOrEmpty(entry.Link))
				{
					float colorOffset = (15f * colorAnimator.value) / 255f;
					Color color = entry.BackgroundColor;
					GUI.backgroundColor = new Color(color.r + colorOffset, color.g + colorOffset, color.b + colorOffset);
					GUI.contentColor = entry.TextColor;

					GUILayout.BeginHorizontal();

					// Animation
					if (currentHorizontalOffsets != null && currentHorizontalOffsets.Count > 0)
						GUILayout.Space(10f * currentHorizontalOffsets[i++].value);

					if (GUILayout.Button(entry.Category.CleanFormat()))
					{
						Application.OpenURL(entry.Link);
					}

					GUILayout.EndHorizontal();
				}
			}
		}

		private void Setup()
		{
			LinkExpressSettings settings = LinkExpressSettings.GetOrCreateSettings();
			startHorizontalOffsets = new List<AnimFloat>();
			currentHorizontalOffsets = new List<AnimFloat>();
			colorAnimator = new AnimFloat(0f);

			for (int i = 0; i < settings.Entries.Length; i++)
			{
				AnimFloat offset = new AnimFloat(i * 0.1f);
				startHorizontalOffsets.Add(offset);

				AnimFloat value = new AnimFloat(0f);
				value.valueChanged.AddListener(Repaint);
				currentHorizontalOffsets.Add(value);
			}
		}

		private void UpdateAnimationParameters()
		{
			for (int i = 0; i < currentHorizontalOffsets.Count; i++)
			{
				AnimFloat startOffset = startHorizontalOffsets[i];
				AnimFloat currentOffset = currentHorizontalOffsets[i];
				currentOffset.value = (Mathf.Sin((Time.unscaledTime + startOffset.value) * 8) + 1) / 2;
			}

			colorAnimator.value = Mathf.Sin(Time.unscaledTime * 8f);
		}

		private void Clear()
		{
			startHorizontalOffsets.Clear();
			currentHorizontalOffsets.Clear();
			colorAnimator.value = 0f;

			Repaint();
		}
	}
}