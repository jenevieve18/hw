/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/12/2016
 * Time: 3:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace HW.WebService2
{
	[WebService]
	public class Soap : System.Web.Services.WebService
	{
		/// <summary>
		/// Logs into the web service
		/// </summary>
		/// <param name="userName">The User Name to login in as</param>
		/// <param name="password">User's password</param>
		/// <returns>True on successful login.</returns>
		[WebMethod(EnableSession=true)]
		public bool Login(string userName, string password)
		{
			//NOTE: There are better ways of doing authentication. This is just illustrates Session usage.
			UserName = userName;
			return true;
		}
		
		/// <summary>
		/// Logs out of the Session.
		/// </summary>
		[WebMethod(EnableSession=true)]
		public void Logout()
		{    
			Context.Session.Abandon();
		}
		
		/// <summary>
		/// UserName of the logged in user.
		/// </summary>
		private string UserName {
			get {return (string)Context.Session["User"];}
			set {Context.Session["User"] = value;}
		}
	}
}
