using System;

namespace LinkExpress
{
	public static class LinkExpressUtils
	{
		internal static string CleanFormat(this string text)
		{
			return text.Replace('_', ' ');
		}

		internal static string CleanFormat(this Enum text)
		{
			return text.ToString().Replace('_', ' ');
		}
	}
}