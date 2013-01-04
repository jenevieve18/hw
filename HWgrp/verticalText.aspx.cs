using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;

namespace HWgrp
{
    public partial class verticalText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string text = Server.UrlDecode(HttpContext.Current.Request.QueryString["STR"].ToString());
            text = Regex.Replace(text, "<(.*?)>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            int w = 16;
            int h = 10 + Convert.ToInt32(Convert.ToDouble(text.Length) * 6.8F);

            Bitmap objBitmap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(ColorTranslator.FromHtml("#" + (HttpContext.Current.Request.QueryString["BG"] == null ? "FFFFFF" : HttpContext.Current.Request.QueryString["BG"].ToString())));

            objGraphics.TranslateTransform(0, h);
            objGraphics.RotateTransform(-90F);

            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Near;

            objGraphics.DrawString(text, (new Font("Arial", 8.5f, FontStyle.Bold)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), 0, 0, strFormat);

            objGraphics.ResetTransform();

            OctreeQuantizer q = new OctreeQuantizer(255, 8);
            q.Quantize(objBitmap).Save(Response.OutputStream, ImageFormat.Gif);
        }
    }
}