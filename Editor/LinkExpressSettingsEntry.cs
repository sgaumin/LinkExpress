using UnityEngine;

namespace LinkExpress
{
	[System.Serializable]
	public class LinkExpressSettingsEntry
	{
		[SerializeField] private LinkExpressCategory category;
		[SerializeField] private string link;
		[SerializeField] private Color backgroundColor = Color.gray;
		[SerializeField] private Color textColor = Color.white;

		public LinkExpressCategory Category => category;
		public string Link => link;
		public Color BackgroundColor => backgroundColor;
		public Color TextColor => textColor;
	}
}