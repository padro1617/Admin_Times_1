using System.Configuration;


// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	public static class MvcHelper
	{
		/// <summary>
		/// 样式域名
		/// </summary>
		private static string _contentVersion = DotNet.Utilities.ConfigManager.GetString( "ContentVersion", "comadmin" );
		/// <summary>
		/// 带版本号,绝对域名的路径
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string ContentVesion( this UrlHelper helper, string path ) {
			string newpath = helper.Content( path );
			var r = string.Format( "{0}?_v={1}", path, _contentVersion ).ToLower();
			return r;
		}
	}
}
