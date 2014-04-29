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
	/// Summary description for projectRoundText.
	/// </summary>
	public class projectRoundText : System.Web.UI.Page
	{
		protected RadioButtonList LangID;
		protected Button Save;
		protected Button Close;
		protected TextBox SurveyIntro;
		protected TextBox InvitationSubject;
		protected TextBox InvitationBody;
		protected TextBox ReminderSubject;
		protected TextBox ReminderBody;
		protected TextBox ExtraInvitationSubject;
		protected TextBox ExtraInvitationBody;
		protected TextBox ExtraReminderSubject;
		protected TextBox ExtraReminderBody;
		protected TextBox SurveyName;
		protected TextBox ThankyouText;
		protected TextBox UnitText;
		protected HtmlInputHidden LastLangID;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			Save.Click += new EventHandler(Save_Click);
			LangID.SelectedIndexChanged += new EventHandler(Save_Click);

			if(!IsPostBack)
			{
				Close.Attributes["onclick"] += "window.close();";

				SqlDataReader rs = Db.sqlRecordSet("SELECT LangID FROM Lang");
				while(rs.Read())
				{
					LangID.Items.Add(new ListItem("<img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\">",rs.GetInt32(0).ToString()));
				}
				rs.Close();

				LangID.SelectedIndex = 0;
				LastLangID.Value = LangID.SelectedValue;

				loadTextElements();
			}
		}

		private void loadTextElements()
		{
			SqlDataReader rs = Db.sqlRecordSet("SELECT " +
				"ISNULL(SurveyIntroJapaneseUnicode,SurveyIntro), " +				// 0
				"ISNULL(InvitationSubjectJapaneseUnicode,InvitationSubject), " +
				"ISNULL(InvitationBodyJapaneseUnicode,InvitationBody), " +
				"ISNULL(ReminderSubjectJapaneseUnicode,ReminderSubject), " +
				"ISNULL(ReminderBodyJapaneseUnicode,ReminderBody), " +
				"ISNULL(SurveyNameJapaneseUnicode,SurveyName), " +				// 5
				"ISNULL(ThankyouTextJapaneseUnicode,ThankyouText), " +
				"ISNULL(UnitTextJapaneseUnicode,UnitText), " +
				"ISNULL(ExtraInvitationSubjectJapaneseUnicode,ExtraInvitationSubject), " +
				"ISNULL(ExtraInvitationBodyJapaneseUnicode,ExtraInvitationBody), " +
				"ISNULL(ExtraReminderSubjectJapaneseUnicode,ExtraReminderSubject), " +		// 10
				"ISNULL(ExtraReminderBodyJapaneseUnicode,ExtraReminderBody) " +
				"FROM ProjectRoundLang " +
				"WHERE LangID = " + LangID.SelectedValue + " " +
				"AND ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"]);
			if(rs.Read())
			{
				SurveyIntro.Text = rs.GetString(0);
				InvitationSubject.Text = rs.GetString(1);
				InvitationBody.Text = rs.GetString(2);
				ReminderSubject.Text = rs.GetString(3);
				ReminderBody.Text = rs.GetString(4);
				SurveyName.Text = (rs.IsDBNull(5) ? "" : rs.GetString(5));
				ThankyouText.Text = (rs.IsDBNull(6) ? "" : rs.GetString(6));
				UnitText.Text = (rs.IsDBNull(7) ? "" : rs.GetString(7));
				ExtraInvitationSubject.Text = (rs.IsDBNull(8) ? "" : rs.GetString(8));
				ExtraInvitationBody.Text = (rs.IsDBNull(9) ? "" : rs.GetString(9));
				ExtraReminderSubject.Text = (rs.IsDBNull(10) ? "" : rs.GetString(10));
				ExtraReminderBody.Text = (rs.IsDBNull(11) ? "" : rs.GetString(11));
			}
			else
			{
				SurveyIntro.Text = "";
				InvitationSubject.Text = "";
				InvitationBody.Text = "";
				ReminderSubject.Text = "";
				ReminderBody.Text = "";
				SurveyName.Text = "";
				ThankyouText.Text = "";
				UnitText.Text = "";
				ExtraInvitationSubject.Text = "";
				ExtraInvitationBody.Text = "";
				ExtraReminderSubject.Text = "";
				ExtraReminderBody.Text = "";
			}
			rs.Close();
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			LastLangID.Value = LangID.SelectedValue;
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

		private void Save_Click(object sender, EventArgs e)
		{
			SqlDataReader rs = Db.sqlRecordSet("SELECT ProjectRoundLangID FROM ProjectRoundLang WHERE LangID = " + LastLangID.Value + " AND ProjectRoundID = " + HttpContext.Current.Request.QueryString["ProjectRoundID"]);
			if(rs.Read())
			{
				Db.sqlExecute("UPDATE ProjectRoundLang SET " +
					"SurveyIntro = '" + SurveyIntro.Text.Replace("'","''") + "'," +
					"InvitationSubject = '" + InvitationSubject.Text.Replace("'","''") + "'," +
					"InvitationBody = '" + InvitationBody.Text.Replace("'","''") + "'," +
					"ReminderSubject = '" + ReminderSubject.Text.Replace("'","''") + "'," +
					"ReminderBody = '" + ReminderBody.Text.Replace("'","''") + "', " +
					"ExtraInvitationSubject = '" + ExtraInvitationSubject.Text.Replace("'","''") + "'," +
					"ExtraInvitationBody = '" + ExtraInvitationBody.Text.Replace("'","''") + "'," +
					"ExtraReminderSubject = '" + ExtraReminderSubject.Text.Replace("'","''") + "'," +
					"ExtraReminderBody = '" + ExtraReminderBody.Text.Replace("'","''") + "', " +
					"ThankyouText = '" + ThankyouText.Text.Replace("'","''") + "', " +
					"UnitText = '" + UnitText.Text.Replace("'","''") + "', " +
					"SurveyName = '" + SurveyName.Text.Replace("'","''") + "', " +
					"SurveyIntroJapaneseUnicode = N'" + SurveyIntro.Text.Replace("'","''") + "'," +
					"InvitationSubjectJapaneseUnicode = N'" + InvitationSubject.Text.Replace("'","''") + "'," +
					"InvitationBodyJapaneseUnicode = N'" + InvitationBody.Text.Replace("'","''") + "'," +
					"ReminderSubjectJapaneseUnicode = N'" + ReminderSubject.Text.Replace("'","''") + "'," +
					"ReminderBodyJapaneseUnicode = N'" + ReminderBody.Text.Replace("'","''") + "', " +
					"ExtraInvitationSubjectJapaneseUnicode = N'" + ExtraInvitationSubject.Text.Replace("'","''") + "'," +
					"ExtraInvitationBodyJapaneseUnicode = N'" + ExtraInvitationBody.Text.Replace("'","''") + "'," +
					"ExtraReminderSubjectJapaneseUnicode = N'" + ExtraReminderSubject.Text.Replace("'","''") + "'," +
					"ExtraReminderBodyJapaneseUnicode = N'" + ExtraReminderBody.Text.Replace("'","''") + "', " +
					"ThankyouTextJapaneseUnicode = N'" + ThankyouText.Text.Replace("'","''") + "', " +
					"UnitTextJapaneseUnicode = N'" + UnitText.Text.Replace("'","''") + "', " +
					"SurveyNameJapaneseUnicode = N'" + SurveyName.Text.Replace("'","''") + "' " +
					"WHERE ProjectRoundLangID = " + rs.GetInt32(0));
			}
			else
			{
				Db.sqlExecute("INSERT INTO ProjectRoundLang (ProjectRoundID,LangID,SurveyIntro,InvitationSubject,InvitationBody,ReminderSubject,ReminderBody,ThankyouText,UnitText,SurveyName,ExtraInvitationSubject,ExtraInvitationBody,ExtraReminderSubject,ExtraReminderBody," +
					"SurveyIntroJapaneseUnicode," +
					"InvitationSubjectJapaneseUnicode," +
					"InvitationBodyJapaneseUnicode," +
					"ReminderSubjectJapaneseUnicode," +
					"ReminderBodyJapaneseUnicode," +
					"ThankyouTextJapaneseUnicode," +
					"UnitTextJapaneseUnicode," +
					"SurveyNameJapaneseUnicode," +
					"ExtraInvitationSubjectJapaneseUnicode," +
					"ExtraInvitationBodyJapaneseUnicode," +
					"ExtraReminderSubjectJapaneseUnicode," +
					"ExtraReminderBodyJapaneseUnicode" +
					") VALUES (" +
					"" + HttpContext.Current.Request.QueryString["ProjectRoundID"] + "," +
					"" + LastLangID.Value + "," +
					"'" + SurveyIntro.Text.Replace("'","''") + "'," +
					"'" + InvitationSubject.Text.Replace("'","''") + "'," +
					"'" + InvitationBody.Text.Replace("'","''") + "'," +
					"'" + ReminderSubject.Text.Replace("'","''") + "'," +
					"'" + ReminderBody.Text.Replace("'","''") + "'," +
					"'" + ThankyouText.Text.Replace("'","''") + "'," +
					"'" + UnitText.Text.Replace("'","''") + "'," +
					"'" + SurveyName.Text.Replace("'","''") + "'," +
					"'" + ExtraInvitationSubject.Text.Replace("'","''") + "'," +
					"'" + ExtraInvitationBody.Text.Replace("'","''") + "'," +
					"'" + ExtraReminderSubject.Text.Replace("'","''") + "'," +
					"'" + ExtraReminderBody.Text.Replace("'","''") + "'," +
					"N'" + SurveyIntro.Text.Replace("'","''") + "'," +
					"N'" + InvitationSubject.Text.Replace("'","''") + "'," +
					"N'" + InvitationBody.Text.Replace("'","''") + "'," +
					"N'" + ReminderSubject.Text.Replace("'","''") + "'," +
					"N'" + ReminderBody.Text.Replace("'","''") + "'," +
					"N'" + ThankyouText.Text.Replace("'","''") + "'," +
					"N'" + UnitText.Text.Replace("'","''") + "'," +
					"N'" + SurveyName.Text.Replace("'","''") + "'," +
					"N'" + ExtraInvitationSubject.Text.Replace("'","''") + "'," +
					"N'" + ExtraInvitationBody.Text.Replace("'","''") + "'," +
					"N'" + ExtraReminderSubject.Text.Replace("'","''") + "'," +
					"N'" + ExtraReminderBody.Text.Replace("'","''") + "'" +
					")");
			}
			rs.Close();

			loadTextElements();
		}
	}
}
