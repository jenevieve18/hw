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
	/// Summary description for optionSetup.
	/// </summary>
	public class optionSetup : System.Web.UI.Page
	{
		protected TextBox Internal;
		protected TextBox Variablename;
		protected DropDownList OptionType;
		protected DropDownList OptionPlacement;
		protected TextBox Width;
		protected TextBox Height;
		protected PlaceHolder OptionComponents;
		protected Button Save;
		protected TextBox InnerWidth;
		protected DropDownList OptionContainerID;
		protected DropDownList OptionComponentContainerID;

		int optionID = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			optionID = (HttpContext.Current.Request.QueryString["OptionID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["OptionID"]) : 0);

			Save.Click += new EventHandler(Save_Click);

			OdbcDataReader rs;

			OptionComponentContainerID.SelectedIndexChanged += new EventHandler(OptionComponentContainerID_SelectedIndexChanged);			
			if(!IsPostBack)
			{
				OptionContainerID.Items.Add(new ListItem("Uncategorized","0"));
				rs = Db.recordSet("SELECT OptionContainerID, OptionContainer FROM OptionContainer");
				while(rs.Read())
				{
					OptionContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				OptionContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["OptionContainerID"]).ToString();

				OptionComponentContainerID.Items.Add(new ListItem("Uncategorized","0"));
				rs = Db.recordSet("SELECT OptionComponentContainerID, OptionComponentContainer FROM OptionComponentContainer");
				while(rs.Read())
				{
					OptionComponentContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				OptionComponentContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["OptionComponentContainerID"]).ToString();
			}

			if(HttpContext.Current.Request.QueryString["MoveUp"] != null)
			{
				int sortOrder = Db.getInt32("SELECT SortOrder FROM OptionComponents WHERE OptionComponentsID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
				rs = Db.recordSet("SELECT OptionComponentsID, SortOrder FROM OptionComponents WHERE OptionID = " + optionID + " AND SortOrder < " + sortOrder + " ORDER BY SortOrder DESC");
				if(rs.Read())
				{
					Db.execute("UPDATE OptionComponents SET SortOrder = " + sortOrder + " WHERE OptionComponentsID = " + rs.GetInt32(0));
					Db.execute("UPDATE OptionComponents SET SortOrder = " + rs.GetInt32(1) + " WHERE OptionComponentsID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
				}
				rs.Close();

				HttpContext.Current.Response.Redirect("optionSetup.aspx?OptionID=" + optionID, true);
			}

			if(!IsPostBack && optionID != 0)
			{
				rs = Db.recordSet("SELECT Internal, Variablename, OptionType, OptionPlacement, Width, Height, InnerWidth, OptionContainerID FROM [Option] WHERE OptionID = " + optionID);
				if(rs.Read())
				{
					Internal.Text = rs.GetString(0);
					Variablename.Text = rs.GetString(1);
					OptionType.SelectedValue = rs.GetInt32(2).ToString();
					OptionPlacement.SelectedValue = rs.GetInt32(3).ToString();
					Width.Text = rs.GetInt32(4).ToString();
					Height.Text = rs.GetInt32(5).ToString();
					InnerWidth.Text = (rs.IsDBNull(6) ? "0" : rs.GetInt32(6).ToString());
					OptionContainerID.SelectedValue = (rs.IsDBNull(7) ? "0" : rs.GetInt32(7).ToString());
				}
				rs.Close();
			}

			OptionComponents.Controls.Add(new LiteralControl("<TR><TD></TD><TD VALIGN=\"TOP\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">"));
			int cx = 0;
			bool first = true;

			int OCCID = Convert.ToInt32("0" + HttpContext.Current.Session["OptionComponentContainerID"]);

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			rs = Db.recordSet("SELECT q.Internal, q.QuestionID FROM Question q INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID WHERE qo.OptionID = " + optionID);
			if(rs.Read())
			{
				sb.Append("<tr><td colspan=\"4\"><br/><u>Used in questions</u></td></tr>");

				do
				{
					sb.Append("<tr><td colspan=\"4\"><a href=\"questionSetup.aspx?QuestionID=" + rs.GetInt32(1) + "\">" + rs.GetString(0) + "</a></td></tr>");
				}
					while(rs.Read());
			}
			rs.Close();

			rs = Db.recordSet("SELECT " +
				"c.OptionComponentID, c.Internal, c.ExportValue, oc.OptionComponentsID, oc.ExportValue " +
				"FROM OptionComponent c " +
				"LEFT OUTER JOIN OptionComponents oc ON c.OptionComponentID = oc.OptionComponentID AND oc.OptionID = " + optionID + " " +
				"WHERE oc.OptionID IS NOT NULL OR (c.OptionComponentContainerID" + (OCCID != 0 ? " = " + OCCID + " " : " IS NULL ") + ") " +
				"ORDER BY oc.OptionID DESC, oc.SortOrder ASC, c.Internal");
			if(rs.Read())
			{
				if(!rs.IsDBNull(3))
				{
					OptionComponents.Controls.Add(new LiteralControl("<TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD><u>Selected component(s)</u>&nbsp;&nbsp;</TD><TD><u>Export value</u>&nbsp;&nbsp;</TD></TR>"));
				}

				do
				{
					if(!rs.IsDBNull(3))
					{
						OptionComponents.Controls.Add(new LiteralControl("<TR><TD>"));
						if(cx != 0)
						{
							OptionComponents.Controls.Add(new LiteralControl("<A HREF=\"optionSetup.aspx?OptionID=" + optionID + "&MoveUp=" + rs.GetInt32(3) + "\"><IMG SRC=\"../img/UpToolSmall.gif\" border=\"0\"></A>"));
						}
						OptionComponents.Controls.Add(new LiteralControl("</TD><TD>"));
						CheckBox cb = new CheckBox();
						cb.ID = "OptionComponentsID" + rs.GetInt32(3);
						if(!IsPostBack)
						{
							cb.Checked = true;
						}
						OptionComponents.Controls.Add(cb);
						OptionComponents.Controls.Add(new LiteralControl("</TD><TD>" + rs.GetString(1) + "</TD><TD nowrap><nobr>" + rs.GetInt32(0) + " -> "));
						TextBox ev = new TextBox();
						ev.ID = "ExportValue" + rs.GetInt32(3);
						ev.Width = Unit.Pixel(30);
						if(!IsPostBack)
						{
							ev.Text = rs.GetInt32(4).ToString();
						}
						OptionComponents.Controls.Add(ev);
						OptionComponents.Controls.Add(new LiteralControl("</nobr></TD></TR>"));
						cx++;
					}
					else
					{
						if(first)
						{
							OptionComponents.Controls.Add(new LiteralControl(sb.ToString() + "</TABLE></TD><TD>&nbsp;</TD><TD VALIGN=\"TOP\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD>&nbsp;</TD><TD><u>Available components</u>&nbsp;&nbsp;</TD><TD><u>Default export value</u></TD></TR>"));
							first = false;
						}
						OptionComponents.Controls.Add(new LiteralControl("<TR><TD>"));
						CheckBox cb = new CheckBox();
						cb.ID = "OptionComponentID" + rs.GetInt32(0);
						OptionComponents.Controls.Add(cb);
						OptionComponents.Controls.Add(new LiteralControl("</TD><TD>" + rs.GetString(1) + "</TD><TD align=\"center\">" + rs.GetInt32(2).ToString() + "</TD></TR>"));
					}
				}
				while(rs.Read());
			}
			rs.Close();

			if(first)
			{
				OptionComponents.Controls.Add(new LiteralControl(sb.ToString()));
			}

			OptionComponents.Controls.Add(new LiteralControl("</TABLE></TD></TR>"));
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
			int type = Convert.ToInt32(OptionType.SelectedValue);
			int width = Convert.ToInt32("0" + Width.Text);
			int innerWidth = Convert.ToInt32("0" + InnerWidth.Text);
			if(width == 0)
			{
				switch(type)
				{
					case 1:	width = 100; break;
					case 2: width = 250; innerWidth = (innerWidth != 0 ? innerWidth : 100); break;
					case 3: goto case 1;
					case 4: goto case 2;
				}
			}
			int height = Convert.ToInt32("0" + Height.Text);
			if(height == 0)
			{
				switch(type)
				{
					case 2: height = 1; break;
					case 4: goto case 2;
				}
			}
			
			if(optionID != 0)
			{
				Db.execute("UPDATE [Option] SET OptionContainerID = " + (OptionContainerID.SelectedValue == "0" ? "NULL" : OptionContainerID.SelectedValue) + ", InnerWidth = " + innerWidth + ", Internal = '" + Internal.Text.Replace("'","") + "', Variablename = '" + Variablename.Text.Replace("'","") + "', OptionType = " + type + ", OptionPlacement = " + Convert.ToInt32(OptionPlacement.SelectedValue) + ", Width = " + width + ", Height = " + height + " WHERE OptionID = " + optionID);
			}
			else
			{
				optionID = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO [Option] (Internal,Variablename,OptionType,OptionPlacement,Width,Height,InnerWidth,OptionContainerID) VALUES ('" + Internal.Text.Replace("'","") + "','" + Variablename.Text.Replace("'","") + "'," + type + "," + Convert.ToInt32(OptionPlacement.SelectedValue) + "," + width + "," + height + "," + innerWidth + "," + (OptionContainerID.SelectedValue == "0" ? "NULL" : OptionContainerID.SelectedValue) + ");SELECT OptionID FROM [Option] WHERE Internal = '" + Internal.Text.Replace("'","") + "' ORDER BY OptionID DESC;COMMIT;");
			}

			OdbcDataReader rs = Db.recordSet("SELECT OptionComponentsID FROM OptionComponents WHERE OptionID = " + optionID);
			while(rs.Read())
			{
				if(((CheckBox)OptionComponents.FindControl("OptionComponentsID" + rs.GetInt32(0))).Checked)
				{
					string ev = ((TextBox)OptionComponents.FindControl("ExportValue" + rs.GetInt32(0))).Text.Replace("'","");
					Db.execute("UPDATE OptionComponents SET ExportValue = " + (ev != "" ? ev : "0") + " WHERE OptionComponentsID = " + rs.GetInt32(0));
				}
				else
				{
					Db.execute("UPDATE OptionComponents SET OptionID = -" + optionID + " WHERE OptionComponentsID = " + rs.GetInt32(0));
				}
			}
			rs.Close();
			
			rs = Db.recordSet("SELECT OptionComponentID, ExportValue FROM OptionComponent");
			while(rs.Read())
			{
				if(OptionComponents.FindControl("OptionComponentID" + rs.GetInt32(0)) != null)
				{
					if(((CheckBox)OptionComponents.FindControl("OptionComponentID" + rs.GetInt32(0))).Checked)
					{
						int optionComponentsID = Db.getInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO [OptionComponents] (OptionComponentID,OptionID,ExportValue) VALUES (" + rs.GetInt32(0) + "," + optionID + "," + rs.GetInt32(1) + ");SELECT OptionComponentsID FROM [OptionComponents] WHERE OptionComponentID = " + rs.GetInt32(0) + " AND OptionID = " + optionID + " ORDER BY OptionComponentsID DESC;COMMIT;");
						Db.execute("UPDATE OptionComponents SET SortOrder = " + optionComponentsID + " WHERE OptionComponentsID = " + optionComponentsID);
					}
				}
			}
			rs.Close();

			HttpContext.Current.Session["OptionContainerID"] = OptionContainerID.SelectedValue;
			HttpContext.Current.Response.Redirect("optionSetup.aspx?OptionID=" + optionID, true);
		}

		private void OptionComponentContainerID_SelectedIndexChanged(object sender, EventArgs e)
		{
			HttpContext.Current.Session["OptionComponentContainerID"] = OptionComponentContainerID.SelectedValue;
			HttpContext.Current.Response.Redirect("optionSetup.aspx?OptionID=" + optionID, true);
		}
	}
}
