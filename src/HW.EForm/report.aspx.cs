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
	/// Summary description for report.
	/// </summary>
	public class report : System.Web.UI.Page
	{
		protected Label R;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(
				HttpContext.Current.Request.QueryString["RK"] != null 
				&& 
				(
				HttpContext.Current.Request.QueryString["PRUID"] != null
				||
				HttpContext.Current.Request.QueryString["UID"] != null
				)
				)
			{
				R.Text = "";

				int cx = 0;

				OdbcDataReader rs;
				
				if(HttpContext.Current.Request.QueryString["PRUID"] != null)
				{
					rs = Db.recordSet("SELECT " +
						"rp.ReportPartID, " +
						"rpl.Subject, " +
						"rpl.Header, " +
						"rpl.Footer, " +
						"dbo.cf_projectUnitTree(" + HttpContext.Current.Request.QueryString["PRUID"] + ",' » '), " +
						"rp.Type " +
						"FROM Report r " +
						"INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
						"INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID " +
						"AND rpl.LangID = dbo.cf_unitLangID(" + HttpContext.Current.Request.QueryString["PRUID"] + ") " +
						"WHERE REPLACE(CONVERT(VARCHAR(255),r.ReportKey),'-','') = '" + HttpContext.Current.Request.QueryString["RK"] + "' " +
						"ORDER BY rp.SortORDER");
				}
				else
				{
					// User based

					// Incomplete with LangID

					rs = Db.recordSet("SELECT " +
						"rp.ReportPartID, " +
						"rpl.Subject, " +
						"rpl.Header, " +
						"rpl.Footer, " +
						"NULL, " +
						"rp.Type " +
						"FROM Report r " +
						"INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
						"INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = 1 " +
						"WHERE REPLACE(CONVERT(VARCHAR(255),r.ReportKey),'-','') = '" + HttpContext.Current.Request.QueryString["RK"] + "' " +
						"ORDER BY rp.SortORDER");
				}
				while(rs.Read())
				{
					if(cx++ > 0)
					{
						R.Text += "<br/><p style=\"page-break-before:always;\">&nbsp;";
						if(HttpContext.Current.Request.QueryString["Ident"] != null)
						{
							R.Text += HttpContext.Current.Request.QueryString["Ident"].ToString().Replace("_",": ").Replace("$",", ") + ", Sida " + (cx);
						}
						R.Text += "&nbsp;</p><br/>";
					}
					else if(HttpContext.Current.Request.QueryString["Ident"] != null)
					{
						R.Text += "<p>&nbsp;" + HttpContext.Current.Request.QueryString["Ident"].ToString().Replace("_",": ").Replace("$",", ") + ", Sida " + (cx) + "&nbsp;</p><br/>";
					}
					R.Text += "<br/><br/><br/><br/><br/><b style=\"font-size:18px;\">" + rs.GetString(1) + "</b><br/>";
					
					if(HttpContext.Current.Request.QueryString["Anonymous"] == null && !rs.IsDBNull(4) && rs.GetString(4) != "")
						R.Text += rs.GetString(4).Replace("\r","").Replace("\n","<br/>") + "<br/>";

					if(!rs.IsDBNull(2) && rs.GetString(2) != "")
						R.Text += "<br/>" + rs.GetString(2).Replace("\r","").Replace("\n","<br/>") + "<br/>";
					
					if(HttpContext.Current.Request.QueryString["PRUID"] != null)
					{
						R.Text += "<img src=\"reportImage.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&RPID=" + rs.GetInt32(0) + "&PRUID=" + HttpContext.Current.Request.QueryString["PRUID"] + "\"/><br/>";
					}
					else
					{
						R.Text += "<img src=\"reportImage.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&RPID=" + rs.GetInt32(0) + "&UID=" + HttpContext.Current.Request.QueryString["UID"] + "\"/><br/>";
					}
					
					if(!rs.IsDBNull(3) && rs.GetString(3) != "")
						R.Text += rs.GetString(3).Replace("\r","").Replace("\n","<br/>");
				}
				rs.Close();
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			System.IO.StringWriter customWriter = new System.IO.StringWriter();

			if(HttpContext.Current.Request.QueryString["MakePDF"] != null)
			{
				//string ident = "";
				//if(HttpContext.Current.Request.QueryString["Ident"] != null)
				//{
				//	ident = "document.getElementById('eform_header').innerHTML+='" + HttpContext.Current.Request.QueryString["Ident"].ToString().Replace("_",": ").Replace("$",", ") + "<br/><br/>';";
				//}
				HtmlTextWriter localWriter = new HtmlTextWriter(customWriter);
				base.Render(localWriter);
				string content = customWriter.ToString();//.Replace("onload=\"\"","onload=\"" + ident + "\"");
				localWriter.Close();
				localWriter = null;
				customWriter.Close();
				customWriter = null;

				string contentBase = "http" + (HttpContext.Current.Request.IsSecureConnection ? "s" : "") + "://" + HttpContext.Current.Request.Url.Host + "/";//"file:///" + HttpContext.Current.Server.MapPath(".").Replace("\\","/") + "/";
				content = content.Replace("URL(","URL(" + contentBase);
				content = content.Replace("href=\"","href=\"" + contentBase);
				content = content.Replace("HREF=\"","href=\"" + contentBase);
				content = content.Replace("src=\"","src=\"" + contentBase);
				content = content.Replace("SRC=\"","src=\"" + contentBase);
				content = content.Replace("background=\"","background=\"" + contentBase);
				content = content.Replace("BACKGROUND=\"","BACKGROUND=\"" + contentBase);

//				Response.Write(content);
//				Response.End();

				WebSupergoo.ABCpdf6.Doc theDoc = new WebSupergoo.ABCpdf6.Doc();
				theDoc.HtmlOptions.UseScript = true;
				theDoc.HtmlOptions.PageCacheEnabled = false;
				//theDoc.HtmlOptions.AddLinks = true;
				theDoc.HtmlOptions.Timeout = 600000;
				theDoc.MediaBox.String = "A4";
				theDoc.Rect.String = "A4";

				double w = theDoc.MediaBox.Width;
				double h = theDoc.MediaBox.Height;
				double l = theDoc.MediaBox.Left;
				double b = theDoc.MediaBox.Bottom; 
				theDoc.Transform.Rotate(90, l, b);
				theDoc.Transform.Translate(w, 0); 
				theDoc.Rect.Width = h;
				theDoc.Rect.Height = w;

				theDoc.Rect.Inset(0,30);

				theDoc.Page = theDoc.AddPage();
				int obj = theDoc.AddImageHtml(content,true,1244,true);
				while(theDoc.Chainable(obj)) 
				{
					theDoc.Page = theDoc.AddPage();
					obj = theDoc.AddImageToChain(obj);
				}

				theDoc.HPos = 0.5;
				theDoc.VPos = 0.5;
				theDoc.Font = theDoc.AddFont("Helvetica");
				theDoc.FontSize = 9;

				for (int i = 1; i <= theDoc.PageCount; i++) 
				{
					theDoc.PageNumber = i;
					theDoc.Flatten();
				}

				int theID = theDoc.GetInfoInt(theDoc.Root, "Pages");
				theDoc.SetInfo(theID, "/Rotate", "90");

				byte[] output = theDoc.GetData();

				theDoc.Clear();
				theDoc.Dispose();
				theDoc = null;

				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.Charset = "UTF-8";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
				HttpContext.Current.Response.ContentType = "application/pdf";
				HttpContext.Current.Response.AddHeader("content-length", output.LongLength.ToString());
				HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + HttpContext.Current.Request.QueryString["MakePDF"] + ".PDF");
				HttpContext.Current.Response.BinaryWrite(output);
				HttpContext.Current.Response.End();
			}
			else
			{
				base.Render(writer);
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
	}
}
