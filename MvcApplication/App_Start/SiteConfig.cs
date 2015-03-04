using ShowLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowWeb
{
	internal static class SiteConfig
	{

		private static IDictionary<string, string> _urlStyleType = new Dictionary<string, string>();

		/// <summary>
		/// Url 行业域名/类型
		/// </summary>
		public static IDictionary<string, string> UrlStyleType {
			get { return _urlStyleType; }
			set { _urlStyleType = value; }
		}
	}
}