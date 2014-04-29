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
	/// Summary description for idxSetup.
	/// </summary>
	public class idxSetup : System.Web.UI.Page
	{
		protected TextBox Internal;
		protected TextBox TargetVal;
		protected TextBox YellowLow;
		protected TextBox GreenLow;
		protected TextBox GreenHigh;
		protected TextBox YellowHigh;
		protected PlaceHolder Text;
		protected PlaceHolder Part;
		protected CheckBox AllPartsRequired;
		protected TextBox RequiredAnswerCount;
		protected Button Save;
		protected DropDownList QuestionID;
		protected DropDownList OptionID;
		protected PlaceHolder NewPart;
		protected TextBox Description;

		int idxID = 0;

		bool updateIndexes = false;

		private void Page_Load(object sender, System.EventArgs e)
		{
			idxID = (HttpContext.Current.Request.QueryString["IdxID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["IdxID"]) : 0);

			if(HttpContext.Current.Request.QueryString["DeletePart"] != null)
			{
				Db.execute("DELETE FROM IdxPartComponent WHERE IdxPartID = " + HttpContext.Current.Request.QueryString["DeletePart"]);
				Db.execute("DELETE FROM IdxPart WHERE IdxPartID = " + HttpContext.Current.Request.QueryString["DeletePart"]);
				updateIndexes = true;
			}

			int cx = 0;
			OdbcDataReader rs = Db.recordSet("SELECT l.LangID, i.Idx FROM Lang l LEFT OUTER JOIN IdxLang i ON l.LangID = i.LangID AND i.IdxID = " + idxID + " ORDER BY l.LangID ASC");
			while(rs.Read())
			{
				Text.Controls.Add(new LiteralControl("<tr><td valign=\"top\" align=\"right\">"));
				if(cx++ == 0)
				{
					Text.Controls.Add(new LiteralControl("Name&nbsp;"));
				}
				Text.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\"></td><td colspan=\"3\">&nbsp;"));
				TextBox text = new TextBox();
				text.Width = Unit.Pixel(300);
				text.ID = "Text" + rs.GetInt32(0);
				Text.Controls.Add(text);
				Text.Controls.Add(new LiteralControl("</td></tr>"));
				if(!IsPostBack && idxID != 0)
				{
					text.Text = (rs.IsDBNull(1) ? "" : rs.GetString(1));
				}
			}
			rs.Close();

			if(!IsPostBack)
			{
				if(idxID == 0)
				{
					RequiredAnswerCount.Text = "10";
					AllPartsRequired.Checked = true;
					TargetVal.Text = "";
					YellowLow.Text = "";
					GreenLow.Text = "";
					GreenHigh.Text = "";
					YellowHigh.Text = "";
					Description.Text = "";
				}
				else
				{
					rs = Db.recordSet("SELECT Internal, AllPartsRequired, RequiredAnswerCount, TargetVal, YellowLow, GreenLow, GreenHigh, YellowHigh, Description FROM Idx WHERE IdxID = " + idxID);
					if(rs.Read())
					{
						Internal.Text = (rs.IsDBNull(0) ? "" : rs.GetString(0));
						AllPartsRequired.Checked = (rs.IsDBNull(1) ? true : rs.GetBoolean(1));
						RequiredAnswerCount.Text = (rs.IsDBNull(2) ? "" : rs.GetInt32(2).ToString());
						TargetVal.Text = (rs.IsDBNull(3) ? "" : rs.GetInt32(3).ToString());
						YellowLow.Text = (rs.IsDBNull(4) ? "" : rs.GetInt32(4).ToString());
						GreenLow.Text = (rs.IsDBNull(5) ? "" : rs.GetInt32(5).ToString());
						GreenHigh.Text = (rs.IsDBNull(6) ? "" : rs.GetInt32(6).ToString());
						YellowHigh.Text = (rs.IsDBNull(7) ? "" : rs.GetInt32(7).ToString());
						Description.Text = (rs.IsDBNull(8) ? "" : rs.GetString(8));
					}
					rs.Close();
				}
			}

			bool hasIdx = true;
			bool hasQue = true;

			if(idxID != 0)
			{
				NewPart.Visible = true;

				rs = Db.recordSet("SELECT p.IdxPartID, q.Internal, i.Internal, p.Multiple, q.Variablename FROM IdxPart p LEFT OUTER JOIN Question q ON q.QuestionID = p.QuestionID LEFT OUTER JOIN Idx i ON p.OtherIdxID = i.IdxID WHERE p.IdxID = " + idxID);
				while(rs.Read())
				{
					Part.Controls.Add(new LiteralControl("<tr><td colspan=\"4\"><img SRC=\"../img/null.gif\" width=\"1\" height=\"2\"></td></tr><tr><td colspan=\"4\" bgcolor=\"#CCCCCC\"><img SRC=\"../img/null.gif\" width=\"1\" height=\"1\"></td></tr><tr><td colspan=\"4\"><img SRC=\"../img/null.gif\" width=\"1\" height=\"2\"></td></tr>"));
					Part.Controls.Add(new LiteralControl("<TR><TD><A HREF=\"idxSetup.aspx?IdxID=" + idxID + "&DeletePart=" + rs.GetInt32(0) + "\">delete</A>&nbsp;</td><TD COLSPAN=\"3\">"));
					TextBox Multiple = new TextBox();
					Multiple.ID = "Multiple" + rs.GetInt32(0);
					Multiple.Attributes["style"] += "width:25px;";
					Part.Controls.Add(Multiple);
					Part.Controls.Add(new LiteralControl(" x " + (rs.IsDBNull(1) ? "Index: " + rs.GetString(2) : (!rs.IsDBNull(4) && rs.GetString(4) != "" ? "[" + rs.GetString(4) + "] " : "") + "Question: " + rs.GetString(1)) + "</TD></TR>"));
					if(!IsPostBack)
					{
						Multiple.Text = rs.GetInt32(3).ToString();
					}
					if(!rs.IsDBNull(1))
					{
						hasIdx = false;

						Part.Controls.Add(new LiteralControl("<TR><TD>&nbsp;</TD><TD COLSPAN=\"3\">"));
						OdbcDataReader rs2 = Db.recordSet("SELECT i.IdxPartComponentID, i.Val, c.Internal FROM IdxPartComponent i INNER JOIN OptionComponent c ON i.OptionComponentID = c.OptionComponentID WHERE i.IdxPartID = " + rs.GetInt32(0));
						while(rs2.Read())
						{
							Part.Controls.Add(new LiteralControl("<SPAN STYLE=\"COLOR:#777777;font-size:10px;\">" + rs2.GetString(2) + " = </SPAN>"));
							TextBox CompVal = new TextBox();
							CompVal.ID = "CompVal" + rs2.GetInt32(0);
							CompVal.Attributes["style"] += "width:25px;";
							Part.Controls.Add(CompVal);
							Part.Controls.Add(new LiteralControl("&nbsp;"));
							if(!IsPostBack)
							{
								CompVal.Text = rs2.GetInt32(1).ToString();
							}
						}
						rs2.Close();
						Part.Controls.Add(new LiteralControl("</TD></TR>"));
					}
					else
					{
						hasQue = false;
					}
				}
				rs.Close();
			}

			if(!IsPostBack)
			{
				OptionID.Items.Add(new ListItem("< N/A >","0"));

				if(hasQue)
				{
					rs = Db.recordSet("SELECT q.Internal, q.QuestionID, q.Variablename, qc.QuestionContainer FROM Question q " +
						"LEFT OUTER JOIN QuestionContainer qc ON q.QuestionContainerID = qc.QuestionContainerID " +
						"WHERE q.QuestionID IN (SELECT qo.QuestionID FROM QuestionOption qo INNER JOIN [Option] o ON qo.OptionID = o.OptionID WHERE o.OptionType = 1)");
					while(rs.Read())
					{
						QuestionID.Items.Add(new ListItem((!rs.IsDBNull(3) ? rs.GetString(3) + "/" : "") + (!rs.IsDBNull(2) && rs.GetString(2) != "" ? "[" + rs.GetString(2) + "] " : "") + rs.GetString(0),rs.GetInt32(1).ToString()));
					}
					rs.Close();
				}

				if(hasIdx)
				{
					rs = Db.recordSet("SELECT Internal, IdxID FROM Idx WHERE IdxID <> " + idxID + " AND IdxID NOT IN (SELECT IdxID FROM IdxPart WHERE OtherIdxID IS NOT NULL)");
					while(rs.Read())
					{
						QuestionID.Items.Add(new ListItem("[IDX] " + rs.GetString(0),"I" + rs.GetInt32(1).ToString()));
					}
					rs.Close();
				}
			}
			QuestionID.SelectedIndexChanged += new EventHandler(QuestionID_SelectedIndexChanged);

			Save.Text = "Save";
			Save.Click += new EventHandler(Save_Click);
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
			updateIndexes = true;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			if(updateIndexes)
			{
				execUpdateIndexes();
			}
		}


		private void execUpdateIndexes()
		{
			OdbcDataReader rs;

			if(idxID != 0)
			{
				Db.execute("UPDATE Idx SET " +
					"TargetVal = " + (TargetVal.Text == "" ? "NULL" : TargetVal.Text) + ", " +
					"Internal = '" + Internal.Text.Replace("'","''") + "', " +
					"Description = " + (Description.Text != "" ? "'" + Description.Text.Replace("'","''") + "'" : "NULL") + ", " +
					"AllPartsRequired = " + (AllPartsRequired.Checked ? "1" : "0") + ", RequiredAnswerCount = " + (RequiredAnswerCount.Text != "" ? RequiredAnswerCount.Text : "0") + ", YellowLow = " + (YellowLow.Text == "" ? "NULL" : YellowLow.Text) + ", GreenLow = " + (GreenLow.Text == "" ? "NULL" : GreenLow.Text) + ", GreenHigh = " + (GreenHigh.Text == "" ? "NULL" : GreenHigh.Text) + ", YellowHigh = " + (YellowHigh.Text == "" ? "NULL" : YellowHigh.Text) + " WHERE IdxID = " + idxID);
			}
			else
			{
				Db.execute("INSERT INTO Idx (TargetVal, Internal, AllPartsRequired, RequiredAnswerCount, YellowLow, GreenLow, GreenHigh, YellowHigh, Description) VALUES (" + (TargetVal.Text == "" ? "NULL" : TargetVal.Text) + ",'" + Internal.Text.Replace("'","''") + "'," + (AllPartsRequired.Checked ? "1" : "0") + "," + (RequiredAnswerCount.Text != "" ? RequiredAnswerCount.Text : "0") + "," + (YellowLow.Text == "" ? "NULL" : YellowLow.Text) + "," + (GreenLow.Text == "" ? "NULL" : GreenLow.Text) + "," + (GreenHigh.Text == "" ? "NULL" : GreenHigh.Text) + "," + (YellowHigh.Text == "" ? "NULL" : YellowHigh.Text) + "," + (Description.Text == "" ? "NULL" : "'" + Description.Text.Replace("'","''") + "'") + ")");
				rs = Db.recordSet("SELECT TOP 1 IdxID FROM Idx ORDER BY IdxID DESC");
				if(rs.Read())
				{
					idxID = rs.GetInt32(0);
				}
				rs.Close();
				Db.execute("UPDATE Idx SET SortOrder = " + idxID + " WHERE IdxID = " + idxID);
			}
			Db.execute("DELETE FROM IdxLang WHERE IdxID = " + idxID);
			rs = Db.recordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Db.execute("INSERT INTO IdxLang (IdxID,LangID,Idx) VALUES (" + idxID + "," + rs.GetInt32(0) + ",'" + ((TextBox)Text.FindControl("Text" + rs.GetInt32(0))).Text.Replace("'","''") + "')");
			}
			rs.Close();

			int maxVal = 0;
			rs = Db.recordSet("SELECT IdxPartID FROM IdxPart WHERE IdxID = " + idxID);
			while(rs.Read())
			{
				int interrimMaxVal = 0;
				OdbcDataReader rs2 = Db.recordSet("SELECT IdxPartComponentID FROM IdxPartComponent WHERE IdxPartID = " + rs.GetInt32(0));
				while(rs2.Read())
				{
					int val = 0;
					try
					{
						val = Convert.ToInt32(((TextBox)Part.FindControl("CompVal" + rs2.GetInt32(0))).Text);
					}
					catch(Exception) {}
					Db.execute("UPDATE IdxPartComponent SET Val = " + val + " WHERE IdxPartComponentID = " + rs2.GetInt32(0));
					if(val > interrimMaxVal)
					{
						interrimMaxVal = val;
					}
				}
				rs2.Close();

				int mult = 0;
				try
				{
					mult = Convert.ToInt32(((TextBox)Part.FindControl("Multiple" + rs.GetInt32(0))).Text);
				}
				catch(Exception) {}

				maxVal += interrimMaxVal*mult;

				Db.execute("UPDATE IdxPart SET Multiple = " + mult + " WHERE IdxPartID = " + rs.GetInt32(0));
			}
			rs.Close();

			if(QuestionID.SelectedValue != "0")
			{
				if(QuestionID.SelectedValue.StartsWith("I"))
				{
					Db.execute("INSERT INTO IdxPart (IdxID,OtherIdxID,Multiple) VALUES (" + idxID + "," + QuestionID.SelectedValue.Substring(1) + ",1)");
				}
				else
				{
					Db.execute("INSERT INTO IdxPart (IdxID,QuestionID, OptionID, Multiple) VALUES (" + idxID + "," + QuestionID.SelectedValue + "," + OptionID.SelectedValue + ",1)");
					rs = Db.recordSet("SELECT TOP 1 IdxPartID FROM IdxPart WHERE IdxID = " + idxID + " ORDER BY IdxPartID DESC");
					if(rs.Read())
					{
						int interrimMaxVal = 0;
						OdbcDataReader rs2 = Db.recordSet("SELECT OptionComponentID, ExportValue FROM OptionComponents WHERE OptionID = " + OptionID.SelectedValue + " ORDER BY SortOrder");
						while(rs2.Read())
						{
							if(rs2.GetInt32(1) > interrimMaxVal)
							{
								interrimMaxVal = rs2.GetInt32(1);
							}
							Db.execute("INSERT INTO IdxPartComponent (IdxPartID, OptionComponentID, Val) VALUES (" + rs.GetInt32(0) + "," + rs2.GetInt32(0) + "," + rs2.GetInt32(1) + ")");
						}
						rs2.Close();
						maxVal += interrimMaxVal;
					}
					rs.Close();
				}
			}

			//rs = Db.recordSet("SELECT SUM(ip.Multiple) FROM Idx i INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID WHERE i.IdxID = " + idxID);
			//if(rs.Read() && !rs.IsDBNull(0))
			//{
			//	maxVal += rs.GetInt32(0);
			//}
			//rs.Close();
			
			int cx = 0;
			rs = Db.recordSet("SELECT COUNT(*) FROM Idx i INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID WHERE i.IdxID = " + idxID);
			if(rs.Read() && !rs.IsDBNull(0))
			{
				cx = rs.GetInt32(0);
			}
			rs.Close();
			
			rs = Db.recordSet("SELECT SUM(ioi.MaxVal*ip.Multiple) FROM Idx i INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID INNER JOIN Idx ioi ON ip.OtherIdxID = ioi.IdxID WHERE i.IdxID = " + idxID);
			if(rs.Read() && !rs.IsDBNull(0))
			{
				maxVal = rs.GetInt32(0);
			}
			rs.Close();

			Db.execute("UPDATE Idx SET MaxVal = " + maxVal + ", CX = " + cx + " WHERE IdxID = " + idxID);

			rs = Db.recordSet("SELECT DISTINCT IdxID FROM IdxPart WHERE OtherIdxID = " + idxID);
			while(rs.Read())
			{
				//OdbcDataReader rs2 = Db.recordSet("SELECT SUM(ip.Multiple) FROM Idx i INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID WHERE i.IdxID = " + rs.GetInt32(0));
				OdbcDataReader rs2 = Db.recordSet("SELECT SUM(ioi.MaxVal*ip.Multiple) FROM Idx i INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID INNER JOIN Idx ioi ON ip.OtherIdxID = ioi.IdxID WHERE i.IdxID = " + rs.GetInt32(0));
				if(rs2.Read())
				{
					Db.execute("UPDATE Idx SET MaxVal = " + rs2.GetInt32(0) + " WHERE IdxID = " + rs.GetInt32(0));
				}
				rs2.Close();
			}
			rs.Close();

			HttpContext.Current.Response.Redirect("idxSetup.aspx?IdxID=" + idxID, true);
		}

		private void QuestionID_SelectedIndexChanged(object sender, EventArgs e)
		{
			OptionID.Items.Clear();
			if(QuestionID.SelectedValue == "0" || QuestionID.SelectedValue.StartsWith("I"))
			{
				OptionID.Items.Add(new ListItem("< N/A >","0"));
			}
			else
			{
				OdbcDataReader rs = Db.recordSet("SELECT o.Internal, o.OptionID FROM QuestionOption qo INNER JOIN [Option] o ON qo.OptionID = o.OptionID WHERE o.OptionType = 1 AND qo.QuestionID = " + QuestionID.SelectedValue);
				while(rs.Read())
				{
					OptionID.Items.Add(new ListItem(rs.GetString(0),rs.GetInt32(1).ToString()));
				}
				rs.Close();
			}
		}
	}
}
