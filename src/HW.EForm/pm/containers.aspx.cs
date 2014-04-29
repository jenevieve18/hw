using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
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
	/// Summary description for containers.
	/// </summary>
	public class containers : System.Web.UI.Page
	{
		protected Label List;
		protected TextBox NewContainer;
		protected Button AddContainer;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Request.QueryString["Delete"] != null)
			{
				Db.execute("UPDATE Question SET QuestionContainerID = NULL WHERE QuestionContainerID = " + HttpContext.Current.Request.QueryString["Delete"]);
				Db.execute("UPDATE [Option] SET OptionContainerID = NULL WHERE OptionContainerID = " + HttpContext.Current.Request.QueryString["Delete"]);
				Db.execute("UPDATE OptionComponent SET OptionComponentContainerID = NULL WHERE OptionComponentContainerID = " + HttpContext.Current.Request.QueryString["Delete"]);
				Db.execute("DELETE FROM QuestionContainer WHERE QuestionContainerID = " + HttpContext.Current.Request.QueryString["Delete"]);
				Db.execute("DELETE FROM OptionContainer WHERE OptionContainerID = " + HttpContext.Current.Request.QueryString["Delete"]);
				Db.execute("DELETE FROM OptionComponentContainer WHERE OptionComponentContainerID = " + HttpContext.Current.Request.QueryString["Delete"]);
			}
			AddContainer.Click += new EventHandler(AddContainer_Click);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			List.Text = "";

			List.Text += "<TR><TD>&lt; Uncategorized &gt;&nbsp;&nbsp;</TD><TD ALIGN=\"CENTER\">";

			OdbcDataReader rs2 = Db.recordSet("SELECT COUNT(*) FROM Question WHERE QuestionContainerID IS NULL");
			if(rs2.Read())
			{
				List.Text += rs2.GetInt32(0).ToString();
			}
			rs2.Close();

			List.Text += "</TD><TD ALIGN=\"CENTER\">";

			rs2 = Db.recordSet("SELECT COUNT(*) FROM [Option] WHERE OptionContainerID IS NULL");
			if(rs2.Read())
			{
				List.Text += rs2.GetInt32(0).ToString();
			}
			rs2.Close();

			List.Text += "</TD><TD ALIGN=\"CENTER\">";

			rs2 = Db.recordSet("SELECT COUNT(*) FROM OptionComponent WHERE OptionComponentContainerID IS NULL");
			if(rs2.Read())
			{
				List.Text += rs2.GetInt32(0).ToString();
			}
			rs2.Close();

			List.Text += "</TD><TD>&nbsp;</TD></TR>";

			OdbcDataReader rs = Db.recordSet("SELECT QuestionContainerID, QuestionContainer FROM QuestionContainer ORDER BY QuestionContainer");
			while(rs.Read())
			{
				List.Text += "<TR><TD>" + rs.GetString(1) + "&nbsp;&nbsp;</TD><TD ALIGN=\"CENTER\">";

				rs2 = Db.recordSet("SELECT COUNT(*) FROM Question WHERE QuestionContainerID = " + rs.GetInt32(0));
				if(rs2.Read())
				{
					List.Text += rs2.GetInt32(0).ToString();
				}
				rs2.Close();

				List.Text += "</TD><TD ALIGN=\"CENTER\">";

				rs2 = Db.recordSet("SELECT COUNT(*) FROM [Option] WHERE OptionContainerID = " + rs.GetInt32(0));
				if(rs2.Read())
				{
					List.Text += rs2.GetInt32(0).ToString();
				}
				rs2.Close();

				List.Text += "</TD><TD ALIGN=\"CENTER\">";

				rs2 = Db.recordSet("SELECT COUNT(*) FROM OptionComponent WHERE OptionComponentContainerID = " + rs.GetInt32(0));
				if(rs2.Read())
				{
					List.Text += rs2.GetInt32(0).ToString();
				}
				rs2.Close();

				List.Text += "</TD><TD>[<A HREF=\"JavaScript:if(confirm('Are you sure?')){location.href='containers.aspx?Delete=" + rs.GetInt32(0) + "';}\">delete</A>]</TD></TR>";
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

		private void AddContainer_Click(object sender, EventArgs e)
		{
			Db.execute("INSERT INTO QuestionContainer (QuestionContainer) VALUES ('" + NewContainer.Text.Replace("'","''") + "')");
			Db.execute("INSERT INTO OptionContainer (OptionContainer) VALUES ('" + NewContainer.Text.Replace("'","''") + "')");
			Db.execute("INSERT INTO OptionComponentContainer (OptionComponentContainer) VALUES ('" + NewContainer.Text.Replace("'","''") + "')");

			HttpContext.Current.Response.Redirect("containers.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(),true);
		}

	}
}
