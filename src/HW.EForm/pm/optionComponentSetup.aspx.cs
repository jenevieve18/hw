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
	public class optionComponentSetup : System.Web.UI.Page
	{
		protected TextBox Internal;
		protected TextBox ExportValue;
		protected PlaceHolder Lang;
		protected Button Save;
		protected DropDownList OptionComponentContainerID;

		int optionComponentID = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			optionComponentID = Convert.ToInt32((HttpContext.Current.Request.QueryString["OptionComponentID"] != null ? HttpContext.Current.Request.QueryString["OptionComponentID"].ToString() : "0"));

			Save.Click += new EventHandler(Save_Click);

			SqlDataReader rs;

			if(!IsPostBack)
			{
				OptionComponentContainerID.Items.Add(new ListItem("Uncategorized","0"));
				rs = Db.sqlRecordSet("SELECT OptionComponentContainerID, OptionComponentContainer FROM OptionComponentContainer");
				while(rs.Read())
				{
					OptionComponentContainerID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				OptionComponentContainerID.SelectedValue = Convert.ToInt32("0" + HttpContext.Current.Session["OptionComponentContainerID"]).ToString();
			}

			int cx = 0;
			rs = Db.sqlRecordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Lang.Controls.Add(new LiteralControl("<tr><td align=\"right\">"));
				if(cx++ == 0)
				{
					Lang.Controls.Add(new LiteralControl("Text&nbsp;"));
				}
				Lang.Controls.Add(new LiteralControl("<img SRC=\"../img/lang/" + rs.GetInt32(0) + ".gif\"></td><td>&nbsp;"));
				TextBox text = new TextBox();
				text.Width = Unit.Pixel(320);
				text.ID = "Text" + rs.GetInt32(0);
				Lang.Controls.Add(text);
				Lang.Controls.Add(new LiteralControl("</td></tr>"));
			}
			rs.Close();

			if(!IsPostBack && optionComponentID != 0)
			{
				rs = Db.sqlRecordSet("SELECT Internal, ExportValue, OptionComponentContainerID FROM [OptionComponent] WHERE OptionComponentID = " + optionComponentID);
				if(rs.Read())
				{
					Internal.Text = rs.GetString(0);
					ExportValue.Text = rs.GetInt32(1).ToString();
					OptionComponentContainerID.SelectedValue = (rs.IsDBNull(2) ? "0" : rs.GetInt32(2).ToString());
				}
				rs.Close();

				rs = Db.sqlRecordSet("SELECT LangID,ISNULL(TextJapaneseUnicode,Text) FROM OptionComponentLang WHERE OptionComponentID = " + optionComponentID);
				while(rs.Read())
				{
					((TextBox)Lang.FindControl("Text" + rs.GetInt32(0))).Text = rs.GetString(1);
				}
				rs.Close();
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

		private void Save_Click(object sender, EventArgs e)
		{
			string ev = ExportValue.Text.Replace("'","");
			string occid = Convert.ToInt32(OptionComponentContainerID.SelectedValue).ToString();
			if(occid == "0")
			{
				occid = "NULL";
			}
			if(optionComponentID != 0)
			{
				Db.sqlExecute("UPDATE [OptionComponent] SET OptionComponentContainerID = " + occid + ", Internal = '" + Internal.Text.Replace("'","") + "', ExportValue = " + (ev != "" ? ev : "0") + " WHERE OptionComponentID = " + optionComponentID);
			}
			else
			{
				optionComponentID = Db.sqlGetInt32("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO [OptionComponent] (Internal,ExportValue,OptionComponentContainerID) VALUES ('" + Internal.Text.Replace("'","") + "'," + (ev != "" ? ev : "0") + "," + occid + ");SELECT OptionComponentID FROM [OptionComponent] WHERE Internal = '" + Internal.Text.Replace("'","") + "' ORDER BY OptionComponentID DESC;COMMIT;");
			}
			Db.sqlExecute("DELETE FROM [OptionComponentLang] WHERE OptionComponentID = " + optionComponentID);
			SqlDataReader rs = Db.sqlRecordSet("SELECT LangID FROM Lang ORDER BY LangID ASC");
			while(rs.Read())
			{
				Db.sqlExecute("INSERT INTO [OptionComponentLang] (OptionComponentID,LangID,Text,TextJapaneseUnicode) VALUES (" + optionComponentID + "," + rs.GetInt32(0) + ",'" + ((TextBox)Lang.FindControl("Text" + rs.GetInt32(0))).Text.Replace("'","''") + "',N'" + ((TextBox)Lang.FindControl("Text" + rs.GetInt32(0))).Text.Replace("'","''") + "')");
			}
			rs.Close();

			HttpContext.Current.Session["OptionComponentContainerID"] = OptionComponentContainerID.SelectedValue;
			HttpContext.Current.Response.Redirect("optionComponentSetup.aspx?OptionComponentID=" + optionComponentID, true);
		}
	}
}
