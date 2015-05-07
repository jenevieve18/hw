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
	/// Summary description for options.
	/// </summary>
	public class options : System.Web.UI.Page
	{
		protected Label List;
		protected DropDownList OptionContainerID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			OptionContainerID.SelectedIndexChanged += new EventHandler(OptionContainerID_SelectedIndexChanged);
			if(!IsPostBack)
			{
				OptionContainerID.Items.Add(new ListItem("Uncategorized","0"));
				OdbcDataReader rs = Db.recordSet("SELECT OptionContainerID, OptionContainer FROM OptionContainer");
				while(rs.Read())
				{
					OptionContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				OptionContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["OptionContainerID"]).ToString();
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
			int OCID = Convert.ToInt32("0" + HttpContext.Current.Session["OptionContainerID"]);

			OdbcDataReader rs = Db.recordSet("SELECT " +
				"OptionID, " +
				"Internal, " +
				"OptionType " +
				"FROM [Option] " +
				"WHERE OptionContainerID" + (OCID != 0 ? " = " + OCID + " " : " IS NULL ") + " " +
				"ORDER BY Internal");
			while(rs.Read())
			{
				List.Text += "<tr><td>" + rs.GetString(1) + "&nbsp;&nbsp;</td><td align=\"center\">";
				switch(rs.GetInt32(2))
				{
					case 1:	List.Text += "Single choice"; break;
					case 2: List.Text += "Free text"; break;
					case 3:	List.Text += "Multi choice"; break;
					case 4: List.Text += "Numeric"; break;
					case 9: List.Text += "VAS"; break;
				}
				List.Text += "&nbsp;&nbsp;</td><td><button onclick=\"location.href='optionSetup.aspx?OptionID=" + rs.GetInt32(0) + "';return false;\">Edit</button></td><td>";
				OdbcDataReader rs2 = Db.recordSet("SELECT ocs.OptionComponentID, oc.Internal FROM OptionComponents ocs INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID WHERE ocs.OptionID = " + rs.GetInt32(0));
				while(rs2.Read())
				{
					List.Text += "<a href=\"optionComponentSetup.aspx?OptionComponentID=" + rs2.GetInt32(0) + "\">" + HttpUtility.HtmlDecode(HttpUtility.HtmlEncode(rs2.GetString(1))) + "</a> ";
				}
				rs2.Close();
				List.Text += "</td><td>";
				rs2 = Db.recordSet("SELECT DISTINCT s.SurveyID, s.Internal " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Survey s ON sq.SurveyID = s.SurveyID " +
					"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
					"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
					"WHERE o.OptionID = " + rs.GetInt32(0));
				while(rs2.Read())
				{
					List.Text += "<a href=\"surveySetup.aspx?SurveyID=" + rs2.GetInt32(0) + "\" title=\"" + HttpUtility.UrlEncode(rs2.GetString(1)) + "\">" + rs2.GetInt32(0) + "</a> ";
				}
				rs2.Close();
				List.Text += "</td></tr>";
			}
			rs.Close();
		}

		private void OptionContainerID_SelectedIndexChanged(object sender, EventArgs e)
		{
			HttpContext.Current.Session["OptionContainerID"] = OptionContainerID.SelectedValue;
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
