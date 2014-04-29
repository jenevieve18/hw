using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Odbc;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eform
{
	/// <summary>
	/// Summary description for idx.
	/// </summary>
	public class idx : System.Web.UI.Page
	{
		protected Label List;

		private void Page_Load(object sender, System.EventArgs e)
		{
			List.Text = "";

			OdbcDataReader rs = Db.recordSet("SELECT IdxID, Internal FROM Idx ORDER BY SortOrder");
			while(rs.Read())
			{
				List.Text += "<TR><TD>" + rs.GetString(1) + "&nbsp;&nbsp;</TD><TD><button onclick=\"location.href='idxSetup.aspx?IdxID=" + rs.GetInt32(0) + "';return false;\">Edit</button></td></tr>";
			}
			rs.Close();
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
