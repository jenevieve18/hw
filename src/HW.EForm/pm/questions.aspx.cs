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
	/// Summary description for WebForm1.
	/// </summary>
	public class questions : System.Web.UI.Page
	{
		protected Label List;
		protected DropDownList QuestionContainerID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			QuestionContainerID.SelectedIndexChanged += new EventHandler(QuestionContainerID_SelectedIndexChanged);
			if(!IsPostBack)
			{
				QuestionContainerID.Items.Add(new ListItem("Uncategorized","0"));
				OdbcDataReader rs = Db.recordSet("SELECT QuestionContainerID, QuestionContainer FROM QuestionContainer");
				while(rs.Read())
				{
					QuestionContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				QuestionContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["QuestionContainerID"]).ToString();

				if(HttpContext.Current.Request.QueryString["CopyQuestionID"] != null)
				{
					OdbcDataReader rs3, rs2;
					
					rs = Db.recordSet("SELECT " +
						"Variablename, " +			// 0
						"OptionsPlacement, " +
						"FontFamily, " +
						"FontSize, " +
						"FontDecoration, " +
						"FontColor, " +				// 5
						"Underlined, " +
						"QuestionContainerID, " +
						"Internal, " +
						"Box " +
						"FROM Question " +
						"WHERE QuestionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["CopyQuestionID"]));
					while(rs.Read())
					{
						string var = (rs.IsDBNull(0) ? "notset" : rs.GetString(0).Replace("'",""));
						bool ok = false; int cx = 1;
						while(!ok)
						{
							cx++;
							ok = true;
							rs2 = Db.recordSet("SELECT COUNT(*) FROM Question WHERE QuestionContainerID " + (rs.IsDBNull(7) ? " IS NULL" : "= " + rs.GetInt32(7)) + " AND Variablename = '" + var + cx + "'");
							if(rs2.Read() && rs2.GetInt32(0) > 0)
							{
								ok = false;
							}
							rs2.Close();
						}
						Db.execute("INSERT INTO Question (" +
							"Variablename, " +			// 0
							"OptionsPlacement, " +
							"FontFamily, " +
							"FontSize, " +
							"FontDecoration, " +
							"FontColor, " +				// 5
							"Underlined, " +
							"QuestionContainerID, " +
							"Internal, " +
							"Box" +
							") VALUES (" +
							"'" + var + cx + "'," +
							"" + (rs.IsDBNull(1) ? "NULL" : rs.GetInt32(1).ToString()) + "," +
							"" + (rs.IsDBNull(2) ? "NULL" : rs.GetInt32(2).ToString()) + "," +
							"" + (rs.IsDBNull(3) ? "NULL" : rs.GetInt32(3).ToString()) + "," +
							"" + (rs.IsDBNull(4) ? "NULL" : rs.GetInt32(4).ToString()) + "," +
							"" + (rs.IsDBNull(5) ? "NULL" : "'" + rs.GetString(5).Replace("'","''") + "'") + "," +
							"" + (rs.IsDBNull(6) ? "NULL" : rs.GetInt32(6).ToString()) + "," +
							"" + (rs.IsDBNull(7) ? "NULL" : rs.GetInt32(7).ToString()) + "," +
							"" + (rs.IsDBNull(8) ? "NULL" : "'" + rs.GetString(8).Replace("'","''") + "'") + "," +
							"" + (rs.IsDBNull(9) ? "NULL" : rs.GetInt32(9).ToString()) + "" +
							")");

						int QID = 0;
						rs2 = Db.recordSet("SELECT TOP 1 QuestionID FROM Question ORDER BY QuestionID DESC");
						if(rs2.Read())
						{
							QID = rs2.GetInt32(0);
						}
						rs2.Close();

						rs2 = Db.recordSet("SELECT QuestionCategoryID FROM QuestionCategoryQuestion WHERE QuestionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["CopyQuestionID"]));
						while(rs2.Read())
						{
							Db.execute("INSERT INTO QuestionCategoryQuestion (QuestionCategoryID,QuestionID) VALUES (" + rs2.GetInt32(0) + "," + QID + ")");
						}
						rs2.Close();

						rs2 = Db.recordSet("SELECT LangID, Question, QuestionShort, QuestionArea FROM QuestionLang WHERE QuestionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["CopyQuestionID"]));
						while(rs2.Read())
						{
							Db.execute("INSERT INTO QuestionLang (QuestionID, LangID, Question, QuestionShort, QuestionArea) VALUES (" + QID + "," + rs2.GetInt32(0) + "," + (rs2.IsDBNull(1) ? "NULL" : "'" + rs2.GetString(1).Replace("'","''") + "'") + "," + (rs2.IsDBNull(2) ? "NULL" : "'" + rs2.GetString(2).Replace("'","''") + "'") + "," + (rs2.IsDBNull(3) ? "NULL" : "'" + rs2.GetString(3).Replace("'","''") + "'") + ")");
						}
						rs2.Close();

						rs2 = Db.recordSet("SELECT OptionID, OptionPlacement, Variablename, Forced, Hide, QuestionOptionID FROM QuestionOption WHERE QuestionID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["CopyQuestionID"]) + " ORDER BY SortOrder");
						while(rs2.Read())
						{
							Db.execute("INSERT INTO QuestionOption (QuestionID,OptionID,OptionPlacement,Variablename,Forced,Hide) VALUES (" +
								"" + QID + "," +
								"" + (rs2.IsDBNull(0) ? "NULL" : rs2.GetInt32(0).ToString()) + "," +
								"" + (rs2.IsDBNull(1) ? "NULL" : rs2.GetInt32(1).ToString()) + "," +
								"" + (rs2.IsDBNull(2) ? "NULL" : "'" + rs2.GetString(2).Replace("'","''") + "'") + "," +
								"" + (rs2.IsDBNull(3) ? "NULL" : rs2.GetInt32(3).ToString()) + "," +
								"" + (rs2.IsDBNull(4) ? "NULL" : rs2.GetInt32(4).ToString()) + "" +
								")");

							int QOID = 0;
							rs3 = Db.recordSet("SELECT TOP 1 QuestionOptionID FROM QuestionOption ORDER BY QuestionOptionID DESC");
							if(rs3.Read())
							{
								QOID = rs3.GetInt32(0);
							}
							rs3.Close();

							rs3 = Db.recordSet("SELECT StartDT, EndDT, LowVal, HighVal FROM QuestionOptionRange WHERE QuestionOptionID = " + rs2.GetInt32(5));
							while(rs3.Read())
							{
								Db.execute("INSERT INTO QuestionOptionRange (QuestionOptionID, StartDT, EndDT, LowVal, HighVal) VALUES (" +
									QOID + "," + 
									"" + (rs3.IsDBNull(0) ? "NULL" : "'" + rs3.GetDateTime(0).ToString("yyyy-MM-dd") + "'") + "," +
									"" + (rs3.IsDBNull(1) ? "NULL" : "'" + rs3.GetDateTime(1).ToString("yyyy-MM-dd") + "'") + "," +
									"" + (rs3.IsDBNull(2) ? "NULL" : "" + rs3.GetDecimal(2).ToString().Replace(",",".") + "") + "," +
									"" + (rs3.IsDBNull(3) ? "NULL" : "" + rs3.GetDecimal(3).ToString().Replace(",",".") + "") + "" +
									")");
							}
							rs3.Close();
						}
						rs2.Close();
						Db.execute("UPDATE QuestionOption SET SortOrder = QuestionOptionID WHERE SortOrder IS NULL");
					}
					rs.Close();
				}
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
			int QCID = Convert.ToInt32("0" + HttpContext.Current.Session["QuestionContainerID"]);
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"q.QuestionID, " +
				"q.Internal, " +
				"q.Variablename " +
				"FROM [Question] q " +
				"WHERE QuestionContainerID" + (QCID != 0 ? " = " + QCID + " " : " IS NULL ") + "" +
				"ORDER BY q.Internal");
			while(rs.Read())
			{
				List.Text += "<tr>" +
					"<td>" +
					"<span style=\"color:#cccccc;\">" + rs.GetInt32(0).ToString().PadLeft(5,'0') + "</span> " +
					"[" + (rs.IsDBNull(2) || rs.GetString(2) == "" ? "Q" + rs.GetInt32(0) : rs.GetString(2)) + "] " +
					"" + rs.GetString(1) + "&nbsp;&nbsp;" +
					"</td>" +
					"<td>";
				int cx = 0;
				OdbcDataReader rs2 = Db.recordSet("SELECT " +
					"o.Internal, " +
					"o.OptionID, " +
					"oc.OptionContainer " +
					"FROM QuestionOption qo " +
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
					"LEFT OUTER JOIN OptionContainer oc ON o.OptionContainerID = oc.OptionContainerID " +
					"WHERE qo.QuestionID = " + rs.GetInt32(0) + " " +
					"ORDER BY qo.SortOrder");
				while(rs2.Read())
				{
					if(cx != 0)
					{
						List.Text += ", ";
					}
					cx++;
					List.Text += "<span style=\"color:#cccccc;\">" + rs2.GetInt32(1).ToString().PadLeft(5,'0') + "</span> " + (rs2.IsDBNull(2) ? "Uncategorized" : rs2.GetString(2)) + " / " + rs2.GetString(0);
				}
				rs2.Close();
				List.Text += "&nbsp;&nbsp;</td><td><button onclick=\"location.href='questionSetup.aspx?QuestionID=" + rs.GetInt32(0) + "';return false;\">Edit</button><button onclick=\"location.href='questions.aspx?CopyQuestionID=" + rs.GetInt32(0) + "';return false;\">Copy</button></td><td>";
				rs2 = Db.recordSet("SELECT s.SurveyID, s.Internal FROM SurveyQuestion sq INNER JOIN Survey s ON sq.SurveyID = s.SurveyID WHERE sq.QuestionID = " + rs.GetInt32(0));
				while(rs2.Read())
				{
					List.Text += "<a href=\"surveySetup.aspx?SurveyID=" + rs2.GetInt32(0) + "\" title=\"" + HttpUtility.UrlEncode(rs2.GetString(1)) + "\">" + rs2.GetInt32(0) + "</a> ";
				}
				rs2.Close();
				List.Text += "</td></tr>";
			}
			rs.Close();
		}

		private void QuestionContainerID_SelectedIndexChanged(object sender, EventArgs e)
		{
			HttpContext.Current.Session["QuestionContainerID"] = QuestionContainerID.SelectedValue;
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
