using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eForm
{
	/// <summary>
	/// Summary description for sent.
	/// </summary>
	public class sent : System.Web.UI.Page
	{
		int projectID = 0;
		string surveyName = "";

		public string printLogo()
		{
			string logo = "";

			if(System.IO.File.Exists(Server.MapPath("img\\project\\logo" + projectID + ".gif")))
			{
				logo = "<img src=\"img/project/logo" + projectID + ".gif?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">";
			}

			return logo;
		}

		public string printSurveyName()
		{
			return surveyName;
		}
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			projectID = (HttpContext.Current.Request.QueryString["P"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["P"]) : 0);
			surveyName = (HttpContext.Current.Request.QueryString["S"] != null ? HttpContext.Current.Request.QueryString["S"].ToString() : "");
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
	}
}
