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
	/// Summary description for optionComponents.
	/// </summary>
	public class optionComponents : System.Web.UI.Page
	{
		protected Label List;
		protected DropDownList OptionComponentContainerID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			OptionComponentContainerID.SelectedIndexChanged += new EventHandler(OptionComponentContainerID_SelectedIndexChanged);
			if(!IsPostBack)
			{
				OptionComponentContainerID.Items.Add(new ListItem("Uncategorized","0"));
				OdbcDataReader rs = Db.recordSet("SELECT OptionComponentContainerID, OptionComponentContainer FROM OptionComponentContainer");
				while(rs.Read())
				{
					OptionComponentContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				OptionComponentContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["OptionComponentContainerID"]).ToString();
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);
			if(!IsPostBack)
			{
				reloadList();
			}
		}

		private void reloadList()
		{
			List.Text = "";
			int OCCID = Convert.ToInt32("0" + HttpContext.Current.Session["OptionComponentContainerID"]);

			OdbcDataReader rs = Db.recordSet("SELECT OptionComponentID, Internal, ExportValue FROM OptionComponent WHERE OptionComponentContainerID" + (OCCID != 0 ? " = " + OCCID + " " : " IS NULL ") + " ORDER BY LEFT(Internal,2), ExportValue");
			while(rs.Read())
			{
				List.Text += "<tr><td>" + rs.GetString(1) + "</td><td align=\"center\">" + rs.GetInt32(2).ToString() + "</td><td><button onclick=\"location.href='optionComponentSetup.aspx?OptionComponentID=" + rs.GetInt32(0) + "';return false;return false;\">Edit</button></td></tr>";
			}
			rs.Close();
		}

		private void OptionComponentContainerID_SelectedIndexChanged(object sender, EventArgs e)
		{
			HttpContext.Current.Session["OptionComponentContainerID"] = OptionComponentContainerID.SelectedValue;
			reloadList();
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
