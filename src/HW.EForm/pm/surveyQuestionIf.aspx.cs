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
	/// Summary description for surveyQuestionIf.
	/// </summary>
	public class surveyQuestionIf : System.Web.UI.Page
	{
		protected Button Save;
		protected Button Add;
		protected Button Close;
		protected PlaceHolder SQIF;

		int SQID = 0, SID = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SQID = (HttpContext.Current.Request.QueryString["SurveyQuestionID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SurveyQuestionID"]) : 0);

			if(HttpContext.Current.Request.QueryString["DeleteSQI"] != null)
			{
				Db.execute("DELETE FROM SurveyQuestionIf WHERE SurveyQuestionID = " + SQID + " AND SurveyQuestionIfID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["DeleteSQI"]));

				HttpContext.Current.Response.Redirect("surveyQuestionIf.aspx?SurveyQuestionID=" + SQID,true);
			}

			if(!IsPostBack)
			{
				Close.Attributes["onclick"] += "window.close();";
			}

			OdbcDataReader rs = Db.recordSet("SELECT SurveyID FROM SurveyQuestion WHERE SurveyQuestionID = " + SQID);
			if(rs.Read())
			{
				SID = rs.GetInt32(0);
			}
			rs.Close();
			
			rs = Db.recordSet("SELECT SurveyQuestionIfID, QuestionID, OptionID, OptionComponentID, ConditionAnd FROM SurveyQuestionIf WHERE SurveyQuestionID = " + SQID);
			while(rs.Read())
			{
				if(!IsPostBack)
				{
					populateQ(rs.GetInt32(0),(rs.IsDBNull(1) ? "NULL" : rs.GetInt32(1).ToString()),true);
					populateO(rs.GetInt32(0),(rs.IsDBNull(1) ? "NULL" : rs.GetInt32(1).ToString()),(rs.IsDBNull(2) ? "NULL" : rs.GetInt32(2).ToString()),true);
					populateOC(rs.GetInt32(0),(rs.IsDBNull(2) ? "NULL" : rs.GetInt32(2).ToString()),(rs.IsDBNull(3) ? "NULL" : rs.GetInt32(3).ToString()),true);
					((DropDownList)SQIF.FindControl("SQI" + rs.GetInt32(0) + "C")).SelectedValue = (rs.IsDBNull(4) ? "NULL" : "1");
				}
				else
				{
					populateQ(rs.GetInt32(0),"NULL",false);
				}
			}
			rs.Close();

			Add.Click += new EventHandler(Add_Click);
			Save.Click += new EventHandler(Save_Click);
		}

		private void populateQ(int SQI, string QID, bool load)
		{
			SQIF.Controls.Add(new LiteralControl("<TR><TD><hr></TD></TR>"));

			DropDownList q = new DropDownList();
			q.ID = "SQI" + SQI + "Q";
			q.AutoPostBack = true;
			q.Width = 300;
			q.SelectedIndexChanged += new EventHandler(q_SelectedIndexChanged);
			q.Items.Add(new ListItem("< select >","NULL"));

			OdbcDataReader rs = Db.recordSet("SELECT q.QuestionID, q.Internal " +
				"FROM Question q " +
				"INNER JOIN SurveyQuestion sq ON q.QuestionID = sq.QuestionID " +
				"INNER JOIN SurveyQuestion sqe ON sq.SurveyID = sqe.SurveyID " +
				"WHERE sqe.SurveyQuestionID = " + SQID + " AND sqe.SurveyQuestionID <> sq.SurveyQuestionID");
			while(rs.Read())
			{
				q.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
			}
			rs.Close();

			if(load)
			{
				if(q.Items.FindByValue(QID.ToString()) != null)
				{
					q.SelectedValue = QID.ToString();
				}
			}

			SQIF.Controls.Add(new LiteralControl("<TR><TD>"));
			SQIF.Controls.Add(q);
			SQIF.Controls.Add(new LiteralControl(" <A href=\"surveyQuestionIf.aspx?DeleteSQI=" + SQI + "&SurveyQuestionID=" + SQID + "\">delete</A></TD></TR>"));

			q = new DropDownList();
			q.ID = "SQI" + SQI + "O";
			q.AutoPostBack = true;
			q.Width = 300;
			q.SelectedIndexChanged += new EventHandler(o_SelectedIndexChanged);
			SQIF.Controls.Add(new LiteralControl("<TR><TD>"));
			SQIF.Controls.Add(q);
			SQIF.Controls.Add(new LiteralControl("</TD></TR>"));

			q = new DropDownList();
			q.ID = "SQI" + SQI + "OC";
			q.Width = 300;
			SQIF.Controls.Add(new LiteralControl("<TR><TD>"));
			SQIF.Controls.Add(q);
			SQIF.Controls.Add(new LiteralControl("</TD></TR>"));

			SQIF.Controls.Add(new LiteralControl("<TR><TD>"));
			q = new DropDownList();
			q.ID = "SQI" + SQI + "C";
			q.Width = 300;
			q.Items.Add(new ListItem("OR","NULL"));
			q.Items.Add(new ListItem("AND","1"));
			SQIF.Controls.Add(q);
			SQIF.Controls.Add(new LiteralControl("</TR>"));
		}

		private void populateO(int SQI, string QID, string OID, bool load)
		{
			DropDownList q = ((DropDownList)SQIF.FindControl("SQI" + SQI + "O"));
			q.Items.Clear();
			q.Items.Add(new ListItem("< select >","NULL"));

			if(QID != "NULL")
			{
				OdbcDataReader rs = Db.recordSet("SELECT " +
					"o.OptionID, " +					// 0
					"o.Internal " +						// 1
					"FROM QuestionOption qo " +
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
					"WHERE qo.QuestionID = " + QID);
				while(rs.Read())
				{
					q.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
			}

			if(load)
			{
				if(OID == "NULL")
				{
					q.SelectedIndex = Math.Min(q.Items.Count-1,1);
				}
				else if(q.Items.FindByValue(OID.ToString()) != null)
				{
					q.SelectedValue = OID.ToString();
				}
			}
		}

		private void populateOC(int SQI, string OID, string OCID, bool load)
		{
			DropDownList q = ((DropDownList)SQIF.FindControl("SQI" + SQI + "OC"));
			q.Items.Clear();
			q.Items.Add(new ListItem("< select >","NULL"));

			if(OID != "NULL")
			{
				OdbcDataReader rs = Db.recordSet("SELECT " +
					"oc.OptionComponentID, " +				// 0
					"c.Internal, " +						// 1
					"c.ExportValue " +						// 2
					"FROM OptionComponents oc " +
					"INNER JOIN OptionComponent c ON oc.OptionComponentID = c.OptionComponentID " +
					"WHERE oc.OptionID = " + OID);
				while(rs.Read())
				{
					q.Items.Add(new ListItem((rs.IsDBNull(2) ? "?" : rs.GetInt32(2).ToString()) + ". " + rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
			}

			if(load)
			{
				if(OCID == "NULL")
				{
					q.SelectedIndex = Math.Min(q.Items.Count-1,1);
				}
				else if(q.Items.FindByValue(OCID.ToString()) != null)
				{
					q.SelectedValue = OCID.ToString();
				}
			}
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

		private void Add_Click(object sender, EventArgs e)
		{
			Db.execute("INSERT INTO SurveyQuestionIf (SurveyID,SurveyQuestionID) VALUES (" + SID + "," + SQID + ")");

			HttpContext.Current.Response.Redirect("surveyQuestionIf.aspx?SurveyQuestionID=" + SQID,true);
		}

		private void q_SelectedIndexChanged(object sender, EventArgs e)
		{
			int sqi = Convert.ToInt32(((DropDownList)sender).ID.Replace("SQI","").Replace("Q",""));
			populateO(sqi,((DropDownList)sender).SelectedValue,"NULL",true);
			populateOC(sqi,((DropDownList)SQIF.FindControl("SQI" + sqi + "O")).SelectedValue,"NULL",true);
		}
		private void o_SelectedIndexChanged(object sender, EventArgs e)
		{
			populateOC(Convert.ToInt32(((DropDownList)sender).ID.Replace("SQI","").Replace("O","")),((DropDownList)sender).SelectedValue,"NULL",true);
		}

		private void Save_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("SELECT SurveyQuestionIfID FROM SurveyQuestionIf WHERE SurveyQuestionID = " + SQID);
			while(rs.Read())
			{
				int SQI = rs.GetInt32(0);
				Db.execute("UPDATE SurveyQuestionIf SET ConditionAnd = " + (((DropDownList)SQIF.FindControl("SQI" + SQI + "C")).SelectedValue) + ", QuestionID = " + (((DropDownList)SQIF.FindControl("SQI" + SQI + "Q")).SelectedValue) + ", OptionID = " + (((DropDownList)SQIF.FindControl("SQI" + SQI + "O")).SelectedValue) + ", OptionComponentID = " + (((DropDownList)SQIF.FindControl("SQI" + SQI + "OC")).SelectedValue) + " WHERE SurveyQuestionID = " + SQID + " AND SurveyQuestionIfID = " + SQI);
			}
			rs.Close();

			//HttpContext.Current.Response.Redirect("surveyQuestionIf.aspx?SurveyQuestionID=" + SQID,true);
			Page.RegisterStartupScript("CLOSE","<script type=\"text/javascript\">window.close();</script>");
		}
	}
}
