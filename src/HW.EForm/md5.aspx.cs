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

namespace eform
{
	/// <summary>
	/// Summary description for md5.
	/// </summary>
	public class md5 : System.Web.UI.Page
	{
		protected TextBox hash;
		protected Button OK;
		protected Label md5str;

		private void Page_Load(object sender, System.EventArgs e)
		{
			OK.Click += new EventHandler(OK_Click);
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

		private void OK_Click(object sender, EventArgs e)
		{
			md5str.Text = Db.HexHashMD5(hash.Text);
		}
	}
}
