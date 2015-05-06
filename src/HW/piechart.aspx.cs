using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace healthWatch
{
    public partial class piechart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.DataVisualization.Charting.Chart c = new System.Web.UI.DataVisualization.Charting.Chart();
            c.RenderType = RenderType.BinaryStreaming;
            c.ImageType = ChartImageType.Png;
            c.AntiAliasing = AntiAliasingStyles.All;
            c.Height = 80;
            c.Width = 80;
            //c.BorderlineWidth = 0;
            c.IsSoftShadows = true;

            //ChartArea3DStyle w = new ChartArea3DStyle();
            //w.Enable3D = true;
            
            c.ChartAreas.Add("pie");
            c.ChartAreas["pie"].Position = new ElementPosition(-3, -3, 106, 106);
            //c.ChartAreas["pie"].BorderWidth = 0;
            //c.ChartAreas["pie"].AxisX.IsMarginVisible = false;
            //c.ChartAreas["pie"].AxisY.IsMarginVisible = false;
            //c.ChartAreas["pie"].AlignmentStyle = AreaAlignmentStyles.All;
            //c.ChartAreas["pie"].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            //c.ChartAreas["pie"].ShadowOffset = 20;
            //c.ChartAreas["pie"].ShadowColor = System.Drawing.ColorTranslator.FromHtml("#ececec");
            //c.ChartAreas["pie"].Area3DStyle = w;

            int val = (HttpContext.Current.Request.QueryString["V"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["V"].ToString()) : 0);
            c.Series.Add("pie");
            c.Series["pie"].ChartType = SeriesChartType.Pie;
            //c.Series["pie"].ShadowOffset = 1;
            //c.Series["pie"].ShadowColor = System.Drawing.ColorTranslator.FromHtml("#ececec");
            c.Series["pie"].BorderColor = System.Drawing.Color.White;
            c.Series["pie"].BorderWidth = 1;
            c.Series["pie"]["PieStartAngle"] = "270";
            //c.Series["pie"]["PieLineColor"] = "Black";
            //c.Series["pie"]["PieLabelStyle"] = "Outside";
            //c.Series["pie"]["CollectedSliceExploded"] = "True";
            //c.Series["pie"]["PieDrawingStyle"] = "SoftEdge";//, Concave
            c.Series["pie"].Points.AddY(val);
            c.Series["pie"].Points[0].BackGradientStyle = GradientStyle.TopBottom;
            if (HttpContext.Current.Request.QueryString["C"] != null)
            {
                switch (HttpContext.Current.Request.QueryString["C"].ToString())
                {
                    case "green":
                        {
                            c.Series["pie"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#c0d22a");
                            c.Series["pie"].Points[0].BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#82be23");
                        }
                        break;
                    case "orange":
                        {
                            c.Series["pie"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#ffa300");
                            c.Series["pie"].Points[0].BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#ff7e00");
                        }
                        break;
                    case "purple":
                        {
                            c.Series["pie"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#d70877");
                            c.Series["pie"].Points[0].BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#b7105e");
                        }
                        break;
                }
            }
            else
            {
                if (val > 70)
                {
                    c.Series["pie"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#c0d22a");
                    c.Series["pie"].Points[0].BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#82be23");
                }
                else if (val > 40)
                {
                    c.Series["pie"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#ffa300");
                    c.Series["pie"].Points[0].BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#ff7e00");
                }
                else
                {
                    c.Series["pie"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#d70877");
                    c.Series["pie"].Points[0].BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#b7105e");
                }
            }
            //c.Series["pie"].Points[0]["Exploded"] = "True";
            c.Series["pie"].Points.AddY(100-val);
            c.Series["pie"].Points[1].BackGradientStyle = GradientStyle.TopBottom;
            c.Series["pie"].Points[1].Color = System.Drawing.ColorTranslator.FromHtml("#ececec");
            c.Series["pie"].Points[1].BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //c.Series["pie"].Points[1]["Exploded"] = "True";
            
            //c.ChartAreas["pie"].Area3DStyle.Enable3D = true;// Draw chart as 3D Cylinder
            //c.Series["pie"]["DrawingStyle"] = "Cylinder";

            var ms = new System.IO.MemoryStream();
            c.SaveImage(ms, ChartImageFormat.Png);

            Response.Cache.SetCacheability(System.Web.HttpCacheability.ServerAndPrivate);

            Response.ContentType = "image/png";
            Response.BinaryWrite(ms.ToArray());
            Response.OutputStream.Flush();
        }
    }
}