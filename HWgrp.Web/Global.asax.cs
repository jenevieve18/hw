using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HW.Core;
using HW.Core.Helpers;

namespace HWgrp.Web
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			AppContext.SetRepositoryFactory(ConfigurationManager.AppSettings["RepositoryFactory"]);
			R.SetResource("HWgrp.Web.resources");
		}

		protected void Session_Start(object sender, EventArgs e)
		{
//			HttpRequest httpRequest = HttpContext.Current.Request;
//			if (httpRequest.Browser.IsMobileDevice) {
//				string path = httpRequest.Url.PathAndQuery;
//				bool isOnMobilePage = path.StartsWith("/Mobile/", StringComparison.OrdinalIgnoreCase);
//				if (!isOnMobilePage) {
//					string redirectTo = "~/Mobile/";
//
//					HttpContext.Current.Response.Redirect(redirectTo);
//				}
//			}
		}

//		public override string GetVaryByCustomString(HttpContext context, string custom)
//		{
//			if (string.Equals(custom, "isMobileDevice", StringComparison.OrdinalIgnoreCase)) {
//				return context.Request.Browser.IsMobileDevice.ToString();
//			}
//
//			return base.GetVaryByCustomString(context, custom);
//		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}