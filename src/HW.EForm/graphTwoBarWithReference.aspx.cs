using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eform
{
	/// <summary>
	/// Summary description for graphTwoBarWithReference.
	/// </summary>
	public class graphTwoBarWithReference : System.Web.UI.Page
	{
		public static string strFloatToStr(string s)
		{
			s = s.Trim();
			s = s.Replace(",",System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
			s = s.Replace(".",System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
			return s;
		}
		private int calculateH(Graph g,float v)
		{
			float mMaxH = g.maxH, dMaxH = 0;

			return Convert.ToInt32(dMaxH+Convert.ToInt32(mMaxH-(v-g.minVal)/(g.maxVal-g.minVal)*mMaxH));
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
//			HttpContext.Current.Request.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
//			HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

			float val1 = (HttpContext.Current.Request.QueryString["V1"] != null ? (float)Convert.ToDouble(strFloatToStr(HttpContext.Current.Request.QueryString["V1"].ToString())) : float.MinValue);
			float val2 = (HttpContext.Current.Request.QueryString["V2"] != null ? (float)Convert.ToDouble(strFloatToStr(HttpContext.Current.Request.QueryString["V2"].ToString())) : float.MinValue);
			float ref1 = (HttpContext.Current.Request.QueryString["R1"] != null ? (float)Convert.ToDouble(strFloatToStr(HttpContext.Current.Request.QueryString["R1"].ToString())) : float.MinValue);
			float ref2 = (HttpContext.Current.Request.QueryString["R2"] != null ? (float)Convert.ToDouble(strFloatToStr(HttpContext.Current.Request.QueryString["R2"].ToString())) : float.MinValue);

			Graph g = new Graph(300,320,"#FFFFFF");
			g.leftSpacing = 50;
			g.maxH = 200;

			float minval = Math.Min(ref1,Math.Min(ref2,Math.Min(val1,val2)));
			float maxval = Math.Max(ref1,Math.Max(ref2,Math.Max(val1,val2)))*1.2f;
			g.setMinMax(Math.Max(0,Math.Min(0,minval)),Math.Max(Math.Max(0,Math.Min(0,minval))+0.04f,maxval));
			g.computeSteping(5);
			
			if(ref1 != float.MinValue || ref2 != float.MinValue)
			{
				g.drawColorExplBox("Referensintervall",23,150,15);
				if(ref1 == float.MinValue)
				{
					g.drawColorArea(g.minVal,ref2,"EEFF33");
				}
				else if(ref2 == float.MinValue)
				{
					g.drawColorArea(ref1,g.maxVal,"EEFF33");
				}
				else
				{
					g.drawColorArea(ref1,ref2,"EEFF33");
				}
			}
			g.drawAxisExpl((HttpContext.Current.Request.QueryString["A"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["A"].ToString()) : ""),0,false,false);

			if(val1 != float.MinValue)
			{
				g.drawBottomString((HttpContext.Current.Request.QueryString["D1"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D1"].ToString()) : ""),1);
			}
			if(val2 != float.MinValue)
			{
				g.drawBottomString((HttpContext.Current.Request.QueryString["D2"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D2"].ToString()) : ""),3);
			}
			
			g.drawOutlines(5,true,false);
			g.drawAxis();

			if(val1 != float.MinValue && val2 != float.MinValue)
			{
				g.drawStepLine(0,1,val1,3,val2);
			}
			if(val1 != float.MinValue)
			{
				g.drawCircle(1,val1);
				g.drawStringInGraph(val1.ToString(),25,-15+calculateH(g,val1));
			}
			if(val2 != float.MinValue)
			{
				g.drawCircle(3,val2);
				g.drawStringInGraph(val2.ToString(),155,-15+calculateH(g,val2));
			}

			g.render();
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
