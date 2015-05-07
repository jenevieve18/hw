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
	/// Summary description for graphSixGreenRed.
	/// </summary>
	public class graphSixGreenRed : System.Web.UI.Page
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

			Graph g = new Graph(600,320,"#FFFFFF");
			g.leftSpacing = 25;
			g.rightSpacing = 160;
			g.innerRightSpacing = 175;
			g.maxH = 200;

			g.setMinMax(0,100);
			g.computeSteping(8);

			g.drawBottomString((HttpContext.Current.Request.QueryString["D1"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D1"].ToString()) : ""),1);
			g.drawBottomString((HttpContext.Current.Request.QueryString["D2"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D2"].ToString()) : ""),2);
			g.drawBottomString((HttpContext.Current.Request.QueryString["D3"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D3"].ToString()) : ""),3);
			g.drawBottomString((HttpContext.Current.Request.QueryString["D4"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D4"].ToString()) : ""),4);
			g.drawBottomString((HttpContext.Current.Request.QueryString["D5"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D5"].ToString()) : ""),5);
			g.drawBottomString((HttpContext.Current.Request.QueryString["D6"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D6"].ToString()) : ""),6);

			g.drawBg(0.00f,0.25f,"33FF33");
			g.drawBg(0.25f,0.40f,"AAFF33");
			g.drawBg(0.40f,0.55f,"EEFF33");
			g.drawBg(0.55f,0.70f,"FFCC33");
			g.drawBg(0.70f,0.90f,"FF8833");
			g.drawBg(0.90f,1.00f,"FF3333");

			g.drawStringInGraph((HttpContext.Current.Request.QueryString["X"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["X"].ToString()) : ""),380,220);

			g.drawStringInGraph((HttpContext.Current.Request.QueryString["L1"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["L1"].ToString()) : ""),420,180);
			g.drawStringInGraph((HttpContext.Current.Request.QueryString["L2"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["L2"].ToString()) : ""),420,135);
			g.drawStringInGraph((HttpContext.Current.Request.QueryString["L3"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["L3"].ToString()) : ""),420,105);
			g.drawStringInGraph((HttpContext.Current.Request.QueryString["L4"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["L4"].ToString()) : ""),420,70);
			g.drawStringInGraph((HttpContext.Current.Request.QueryString["L5"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["L5"].ToString()) : ""),420,40);
			g.drawStringInGraph((HttpContext.Current.Request.QueryString["L6"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["L6"].ToString()) : ""),420,10);

			g.drawAxisExpl((HttpContext.Current.Request.QueryString["A"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["A"].ToString()) : ""),0,false,false);
//
//			if(val1 != float.MinValue)
//			{
//				g.drawBottomString((HttpContext.Current.Request.QueryString["D1"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D1"].ToString()) : ""),1);
//			}
//			if(val2 != float.MinValue)
//			{
//				g.drawBottomString((HttpContext.Current.Request.QueryString["D2"] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D2"].ToString()) : ""),3);
//			}
//			
			g.drawOutlines(11,true,false);
			g.drawAxis();

			float oldVal = (HttpContext.Current.Request.QueryString["V1"] != null ? (float)Convert.ToDouble(strFloatToStr(HttpContext.Current.Request.QueryString["V1"].ToString())) : float.MinValue);
			int oldPos = 1;
			if(oldVal != float.MinValue)
			{
				g.drawCircle(oldPos,(oldVal >= 0 ? oldVal : 0));
			}
			g.drawBottomString((HttpContext.Current.Request.QueryString["D"+1] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D"+1].ToString()) : ""),1);
			for(int i=2; i<=6; i++)
			{
				float val = (HttpContext.Current.Request.QueryString["V"+i] != null ? (float)Convert.ToDouble(strFloatToStr(HttpContext.Current.Request.QueryString["V"+i].ToString())) : float.MinValue);
				if(val != float.MinValue)
				{
					if(oldVal != float.MinValue)
					{
						g.drawStepLine(20,oldPos,(oldVal >= 0 ? oldVal : 0),i,(val >= 0 ? val : 0));
						g.drawCircle(i,(val >= 0 ? val : 0));
					}
					oldVal = val;
					oldPos = i;
				}
				g.drawBottomString((HttpContext.Current.Request.QueryString["D"+i] != null ? Server.UrlDecode(HttpContext.Current.Request.QueryString["D"+i].ToString()) : ""),i);
			}
//
//			if(val1 != float.MinValue && val2 != float.MinValue)
//			{
//				g.drawStepLine(0,1,val1,3,val2);
//			}
//			if(val1 != float.MinValue)
//			{
//				g.drawCircle(1,val1);
//				g.drawStringInGraph(val1.ToString(),25,-15+calculateH(g,val1));
//			}
//			if(val2 != float.MinValue)
//			{
//				g.drawCircle(3,val2);
//				g.drawStringInGraph(val2.ToString(),155,-15+calculateH(g,val2));
//			}

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
