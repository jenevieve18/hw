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

namespace healthWatch
{
	/// <summary>
	/// Summary description for login.
	/// </summary>
	public partial class login : System.Web.UI.Page
	{

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string homeURL = HttpContext.Current.Session["HomeURL"].ToString();
            if (HttpContext.Current.Request.QueryString["Remove"] != null)
            {
                homeURL = "closed.aspx?Xref=X01" + HttpContext.Current.Session["UserID"].ToString().PadLeft(5, '0');
                Db.exec("UPDATE [User] SET Username = Username + 'DELETED', Email = Email + 'DELETED', Reminder = 0 WHERE UserID = " + HttpContext.Current.Session["UserID"]);
                Db.exec("UPDATE [SponsorInvite] SET UserID = NULL WHERE UserID = " + HttpContext.Current.Session["UserID"]);
            }
            Db.exec("UPDATE [Session] SET EndDT = GETDATE() WHERE EndDT IS NULL AND SessionID = " + Convert.ToInt32(HttpContext.Current.Session["SessionID"]));
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect(homeURL, true);
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
		}
		#endregion
	}
}
