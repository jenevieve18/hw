using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eform
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
	public class _default : System.Web.UI.Page
	{
		protected TextBox LoginCode;
		protected Button Login;
		protected Label ErrorMsg;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Request.Url.Host.IndexOf("webbqps") >= 0)
			{
				HttpContext.Current.Response.Redirect("/info",true);
			}

			if(IsPostBack)
			{
				loginCheck();
			}
			Login.Click += new EventHandler(Login_Click);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private void loginCheck()
		{
			string UK = "";
			string key = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Server.HtmlDecode(LoginCode.Text.Trim())).ToLower().Replace("-","").Replace(";","").Replace("'","").Replace("\"","");
			if(key.Length > 15)
			{
				key = key.Substring(0,15);
			}

			if(key != "")
			{
				int PRUID = 0;
				try
				{
					PRUID = Convert.ToInt32(key.Substring(4));
				}
				catch(Exception){}

				if(PRUID != 0)
				{
					SqlDataReader rs = Db.sqlRecordSet("SELECT u.ProjectRoundUserID, LEFT(CONVERT(VARCHAR(255),u.UserKey),8) FROM ProjectRoundUser u WHERE LOWER(RIGHT(CONVERT(VARCHAR(255),u.UserKey),4)) = '" + key.Substring(0,4) + "' AND u.ProjectRoundUserID = " + PRUID);
					if(rs.Read())
					{
						UK = rs.GetString(1) + rs.GetInt32(0).ToString();
					}
					rs.Close();
				}
				if(UK == "")
				{
					SqlDataReader rs = Db.sqlRecordSet("SELECT ProjectRoundUnitID, UnitKey FROM ProjectRoundUnit WHERE UniqueID = '" + key + "'");
					if(rs.Read())
					{
						UK = "U" + rs.GetGuid(1).ToString().Substring(0,8) + rs.GetInt32(0).ToString();
					}
					rs.Close();
				}
			}
			if(UK != "")
			{
				HttpContext.Current.Response.Redirect("/submit.aspx?K=" + UK + "&R=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(),true);
			}
			else
			{
				ErrorMsg.Text = "<BR/><BR/><SPAN STYLE=\"color:#cc0000;\">Felaktig kod</SPAN>";
			}
		}

		private void Login_Click(object sender, EventArgs e)
		{
			//removed
		}
	}
}
