using System;
using System.Collections;
using System.Web;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace healthWatch
{
    public class Graph
    {
        public Bitmap objBitmap;
        public Graphics objGraphics;

        StringFormat drawFormat;
        StringFormat drawFormatCenter;
        StringFormat drawFormatFar;
        StringFormat drawFormatNear;
        StringFormat drawFormatFarCenter;
        StringFormat drawFormatFarNear;
        StringFormat drawFormatNearCenter;

        static int outerTopSpacing = 40;
        static int topSpacing = outerTopSpacing + 20;
        static int bottomSpacing = 40;
        static int rightSpacing = 35;
        static int innerRightSpacing = rightSpacing + 15;

        int w = 0;
        int h = 0;

        public int leftSpacing = 40;
        public int barW = 0;
        public int vertLines = 11;
        public float steping = 0;

        public float maxVal = float.NegativeInfinity;
        public float minVal = float.PositiveInfinity;
        public float baseline = 0;
        public float maxH = 240;
        
        float fontSize = 8f;
        float smallFontSize = 6.5f;
        float medFontSize = 7f;
        float dif = 0;

        string font = "Tahoma";

        string[] colors;

        public Graph(int width, int height)
        {
            init(width, height, "#F2F2F2");
        }

        public Graph(int width, int height, string color)
        {
            init(width, height, color);
        }

        private void init(int width, int height, string color)
        {
            w = width;
            h = height;

            //colors = new string[20] { "95CE22", "FDB827", "CC0000", "65DD31", "31CFDD", "4467E9", "C444E9", "B49595", "CFFFA8", "FFB4A8", "FFA8FC", "67E944", "6744E9", "44E967", "E94467", "E96744", "33FF33", "FFFF33", "FF3333", "8EFF70" };
			colors = new string[31] {
				"95CE22", 
					     "FDB827","CC0000","65DD31",
												    "00FF00","0000FF","990099","CC0000",
				"FF6600","006600","999999","FFFF00","000066","996600","FF0099",
				//						   "31CFDD","4467E9","C444E9","FFFD78","CFFFA8",
				//"FFB4A8","CCCCCC","CC7700","6744E9","44E967","E94467"
																			   "E96744",
				"000000","FF9900","3366FF","FFCC66","FFA8FC","67298E","146930","00FFE4",
				"96FF00","6A1AFD","B49595","7D61B1","78B161","5B6258","FFFFFF"};

            objBitmap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(ColorTranslator.FromHtml(color));

            drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            drawFormatCenter.LineAlignment = StringAlignment.Center;

            drawFormatFar = new StringFormat();
            drawFormatFar.Alignment = StringAlignment.Far;

            drawFormatNear = new StringFormat();
            drawFormatNear.Alignment = StringAlignment.Near;

            drawFormatFarCenter = new StringFormat();
            drawFormatFarCenter.Alignment = StringAlignment.Far;
            drawFormatFarCenter.LineAlignment = StringAlignment.Center;

            drawFormatFarNear = new StringFormat();
            drawFormatFarNear.Alignment = StringAlignment.Far;
            drawFormatFarNear.LineAlignment = StringAlignment.Near;

            drawFormatNearCenter = new StringFormat();
            drawFormatNearCenter.Alignment = StringAlignment.Near;
            drawFormatNearCenter.LineAlignment = StringAlignment.Center;
        }

        public void roundMinMax()
        {
            setMinMax((float)Math.Floor((double)minVal), (float)Math.Ceiling((double)maxVal));
            float subMin = 0f, addMax = 0f;
            while (minVal == maxVal || (maxVal - minVal) / (float)(vertLines - 1) != (float)Math.Round((double)(maxVal - minVal) / (double)(vertLines - 1), 0))
            {
                if (minVal == maxVal || Convert.ToInt32(minVal) % 10 == 0)
                {
                    addMax = 1f;
                    subMin = 0f;
                }
                else
                {
                    addMax = 0f;
                    subMin = 1f;
                }
                setMinMax(minVal - subMin, maxVal + addMax);
            }
        }

        public void drawAxis()
        {
            drawAxis(false);
        }

        public void drawBg(float lowVal, float topVal, int color)
        {
            float mMaxH = maxH * 100 / 100;
            float dMaxH = maxH - mMaxH;
            int atH = Convert.ToInt32(mMaxH - (Convert.ToDouble(topVal) - minVal) / (maxVal - minVal) * mMaxH);
            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing, topSpacing + 1 + dMaxH + atH, w - leftSpacing - rightSpacing, Convert.ToInt32((Convert.ToDouble(topVal - lowVal) - minVal) / (maxVal - minVal) * mMaxH) - 1);
        }

        public void drawBg(float lowPercent, float topPercent, string color)
        {
            float mMaxH = maxH * 100 / 100;
            float dMaxH = maxH - mMaxH;
            int atH = Convert.ToInt32(mMaxH - topPercent * mMaxH);
            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + color)), leftSpacing, topSpacing + 1 + dMaxH + atH, w - leftSpacing - rightSpacing, Convert.ToInt32((topPercent - lowPercent) * mMaxH) - 1);
        }

        public void drawAxis(bool right)
        {
            #region Horizontal axis
            if (!right)
            {
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), w - rightSpacing - 3, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH) - 3, w - rightSpacing, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), w - rightSpacing - 3, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH) + 3, w - rightSpacing, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH));
            }
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH), w - (right ? innerRightSpacing : rightSpacing), topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH));
            #endregion

            #region Vertical axis
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing - 3, outerTopSpacing + 3, leftSpacing, outerTopSpacing);
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + 3, outerTopSpacing + 3, leftSpacing, outerTopSpacing);
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing, outerTopSpacing, leftSpacing, topSpacing + Convert.ToInt32(maxH));
            if (HttpContext.Current.Request.QueryString["DISABLED"] != null && baseline != 0)
            {
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing - 3, topSpacing + 10 + Convert.ToInt32(maxH) - 3, leftSpacing, topSpacing + 10 + Convert.ToInt32(maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + 3, topSpacing + 10 + Convert.ToInt32(maxH) - 3, leftSpacing, topSpacing + 10 + Convert.ToInt32(maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing, topSpacing + Convert.ToInt32(maxH), leftSpacing, topSpacing + 10 + Convert.ToInt32(maxH));
            }
            #endregion
        }

        public void drawRightAxis()
        {
            #region Vertical axis
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), w - innerRightSpacing - 3, outerTopSpacing + 3, w - innerRightSpacing, outerTopSpacing);
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), w - innerRightSpacing + 3, outerTopSpacing + 3, w - innerRightSpacing, outerTopSpacing);
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), w - innerRightSpacing, outerTopSpacing, w - innerRightSpacing, topSpacing + Convert.ToInt32(maxH));
            if (baseline != 0)
            {
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), w - innerRightSpacing - 3, bottomSpacing + Convert.ToInt32(maxH) - 3, w - innerRightSpacing, bottomSpacing + Convert.ToInt32(maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), w - innerRightSpacing + 3, bottomSpacing + Convert.ToInt32(maxH) - 3, w - innerRightSpacing, bottomSpacing + Convert.ToInt32(maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), w - innerRightSpacing, Convert.ToInt32(maxH), w - innerRightSpacing, bottomSpacing + Convert.ToInt32(maxH));
            }
            #endregion
        }

        public void printCopyRight()
        {
            objGraphics.TranslateTransform(0, h);
            objGraphics.RotateTransform(90F);
            objGraphics.DrawString("HealthWatch, " + DateTime.Now.ToString("yyyy-MM-dd, HH:mm"), (new Font(font, medFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#AAAAAA"))), topSpacing - h, 5 - w, drawFormatNear);
            objGraphics.ResetTransform();
        }

        public void resetMinMax()
        {
            maxVal = float.NegativeInfinity;
            minVal = float.PositiveInfinity;
        }

        public void setMinMax(float min, float max)
        {
            if (min < minVal)
                minVal = min;
            if (max > maxVal)
                maxVal = max;
        }

        public bool computeMinMax(float minAdd, float maxAdd)
        {
            if (float.IsNaN(maxVal) || float.IsNegativeInfinity(maxVal) || float.IsNaN(minVal) || float.IsPositiveInfinity(minVal))
            {
                return false;
            }
            else
            {
                #region Compute min and max values for graph
                if (minAdd != 0f || maxAdd != 0f)
                {
                    dif = Math.Abs(maxVal - minVal);
                    if (dif == 0)
                        dif = Math.Abs(maxVal * maxAdd);
                    if (minVal > 0)
                    {
                        minVal -= dif * minAdd;
                        if (minVal < 0)
                        {
                            minVal = 0;
                        }
                    }
                    else
                    {
                        minVal += dif * minAdd;
                        if (minVal > 0)
                        {
                            minVal = 0;
                        }
                    }
                    if (maxVal > 0)
                    {
                        maxVal += dif * maxAdd;
                        if (maxVal < 0)
                        {
                            maxVal = 0;
                        }
                    }
                    else
                    {
                        maxVal -= dif * maxAdd;
                        if (maxVal > 0)
                        {
                            maxVal = 0;
                        }
                    }
                    int dec = (int)Math.Floor(Math.Log10(dif));
                    float sqrt = (float)Math.Pow(10, dec);
                    minVal = (float)Math.Floor(minVal / sqrt) * sqrt;
                    maxVal = (float)Math.Ceiling(maxVal / sqrt) * sqrt;
                }
                #endregion

                baseline = (minVal < 0 ? Math.Abs(minVal) : 0);

                return true;
            }
        }

        public void computeSteping(int steps)
        {
            steping = (float)(w - leftSpacing - innerRightSpacing) / (float)(steps - 1);
            barW = (int)(steping * 0.7);
        }

        public void render()
        {
            OctreeQuantizer q = new OctreeQuantizer(255, 8);
            q.Quantize(objBitmap).Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
        }

        public void drawColorExplCircle(string s1, string s2, string color, int x, int y)
        {
            objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + color)), x + leftSpacing, y, 10, 10);
            objGraphics.DrawEllipse(new Pen(ColorTranslator.FromHtml("#000000")), x + leftSpacing, y, 10, 10);
            objGraphics.DrawString(s2, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), x + leftSpacing + 20, y + 5, drawFormatNearCenter);
            objGraphics.DrawString(s1, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), x + leftSpacing - 2, y + 5, drawFormatFarCenter);
        }

        public void drawCircleOutlineAt(string s, string color, float v, int spacing)
        {
            float mMaxH = maxH;
            float dMaxH = maxH - maxH;

            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing - 10 - 30 * spacing, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), leftSpacing + 10, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH));
            objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + color)), leftSpacing - 20 - 30 * spacing, topSpacing - 5 + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), 10, 10);
            objGraphics.DrawEllipse(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing - 20 - 30 * spacing, topSpacing - 5 + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), 10, 10);
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing - 22 - 30 * spacing, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (v - minVal) / (maxVal - minVal) * mMaxH) + 1, drawFormatFarCenter);

            //int st = (w-rightSpacing-(leftSpacing+10))/40;
            //for(int i = 1; leftSpacing+10+st*i <= w-rightSpacing; i++)
            //{
            //	objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000")),leftSpacing+10+st*i-st/2,topSpacing+Convert.ToInt32(maxH-(v-minVal)/(maxVal-minVal)*maxH),leftSpacing+10+st*i,topSpacing+Convert.ToInt32(maxH-(v-minVal)/(maxVal-minVal)*maxH));
            //}
        }
        public void drawOutlineAt(string s, float v)
        {
            float mMaxH = maxH;
            float dMaxH = maxH - maxH;

            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#CCCCCC")), leftSpacing, topSpacing + Convert.ToInt32(maxH - v / (maxVal - minVal) * maxH), w - rightSpacing, topSpacing + Convert.ToInt32(maxH - v / (maxVal - minVal) * maxH));
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing - 5, topSpacing + dMaxH + Convert.ToInt32(mMaxH - v / (maxVal - minVal) * mMaxH), drawFormatFarCenter);
        }
        public void drawOutlines(int cx)
        {
            drawOutlines(cx, true, false, 0, 100);
        }

        public void drawOutlines(int cx, bool vert)
        {
            drawOutlines(cx, vert, false, 0, 100);
        }

        public void drawOutlines(int cx, bool vert, bool skipLast, int from, int to)
        {
            #region Outlines
            for (int ii = 0; ii < cx; ii++)
            {
                if (ii != 0)
                {
                    // Horizontal height marker lines
                    for (int i = 0; i < vertLines; i++)
                    {
                        objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#CCCCCC")), leftSpacing + (ii - 1) * steping, topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * maxH), leftSpacing + ii * steping + (skipLast ? 0 : 5), topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * maxH));
                    }
                    if (vert && !(skipLast && ii == cx - 1))
                    {
                        // Vertical length marker line
                        if (baseline != 0)
                        {
                            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#CCCCCC")), leftSpacing + ii * steping, topSpacing - 5, leftSpacing + ii * steping, topSpacing + maxH + 5);
                        }
                        else
                        {
                            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#CCCCCC")), leftSpacing + ii * steping, topSpacing - 5, leftSpacing + ii * steping, topSpacing + maxH);
                        }
                    }
                }
                else
                {
                    float mMaxH = maxH * to / 100 - maxH * from / 100;
                    float dMaxH = maxH - maxH * to / 100;

                    for (int i = 0; i < vertLines; i++)
                    {
                        try
                        {
                            objGraphics.DrawString(Convert.ToString(Math.Round((maxVal - minVal) / (vertLines - 1) * i + minVal, 2)), (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing - 5, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * mMaxH), drawFormatFarCenter);
                        }
                        catch (Exception) { }
                    }
                }
            }
            #endregion
        }

        public void drawOutlinesRight()
        {
            drawOutlinesRight(0, 100);
        }

        public void drawOutlinesRight(int from, int to)
        {
            float mMaxH = maxH * to / 100 - maxH * from / 100;
            float dMaxH = maxH - maxH * to / 100;

            for (int i = 0; i < vertLines; i++)
            {
                objGraphics.DrawString(Convert.ToString(Math.Round((maxVal - minVal) / (vertLines - 1) * i + minVal, 2)), (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), w - innerRightSpacing + 5, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * mMaxH), drawFormatNearCenter);
            }
        }

        public void drawN()
        {
            objGraphics.DrawString("" +
                "ø\n" +
                "n",
                (new Font(font, smallFontSize)),
                (new SolidBrush(ColorTranslator.FromHtml("#000000"))),
                2,
                h - 25,
                drawFormatNear);
        }
        public void drawNval(int n1, float avg1, int n2, float avg2, int cx, bool two)
        {
            int avg1dec = (avg1 > 100f ? 0 : (avg1 > 25f ? 1 : 2));

            if (two)
            {
                int avg2dec = (avg2 > 100f ? 0 : (avg2 > 25f ? 1 : 2));

                objGraphics.DrawString("" + Math.Round((Convert.ToDouble(avg1)), avg1dec) + "\n" + n1, (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#" + colors[0]))), leftSpacing + Convert.ToInt32(cx * steping), h - 25, drawFormatFar);
                objGraphics.DrawString("  /\n  /", (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + Convert.ToInt32(cx * steping), h - 25, drawFormat);
                objGraphics.DrawString("  " + Math.Round((Convert.ToDouble(avg2)), avg2dec) + "\n   " + n2, (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#" + colors[1]))), leftSpacing + Convert.ToInt32(cx * steping), h - 25, drawFormatNear);
            }
            else
            {
                objGraphics.DrawString("" + Math.Round((Convert.ToDouble(avg1)), avg1dec) + "\n" + n1, (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + Convert.ToInt32(cx * steping), h - 25, drawFormat);
            }
        }

        public void drawNSTD(int dH)
        {
            objGraphics.DrawString("" +
                "ø\n" +
                "n\n" +
                "std",
                (new Font(font, smallFontSize)),
                (new SolidBrush(ColorTranslator.FromHtml("#000000"))),
                2,
                h - dH,
                drawFormatNear);
        }
        public void drawNSTDval(int n1, float std1, float avg1, int n2, float std2, float avg2, int cx, bool two, int dH)
        {
            int std1dec = (std1 > 100f ? 0 : (std1 > 25f ? 1 : 2));
            int avg1dec = (avg1 > 100f ? 0 : (avg1 > 25f ? 1 : 2));

            if (two)
            {
                int std2dec = (std2 > 100f ? 0 : (std2 > 25f ? 1 : 2));
                int avg2dec = (avg2 > 100f ? 0 : (avg2 > 25f ? 1 : 2));

                objGraphics.DrawString("" + Math.Round((Convert.ToDouble(avg1)), avg1dec) + "\n" + n1 + "\n" + Math.Round((Convert.ToDouble(std1)), std1dec), (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#" + colors[0]))), leftSpacing + Convert.ToInt32(cx * steping), h - dH, drawFormatFar);
                objGraphics.DrawString("  /\n  /\n  /", (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + Convert.ToInt32(cx * steping), h - dH, drawFormat);
                objGraphics.DrawString("  " + Math.Round((Convert.ToDouble(avg2)), avg2dec) + "\n   " + n2 + "\n  " + Math.Round((Convert.ToDouble(std2)), std2dec), (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#" + colors[1]))), leftSpacing + Convert.ToInt32(cx * steping), h - dH, drawFormatNear);
            }
            else
            {
                objGraphics.DrawString("" + Math.Round((Convert.ToDouble(avg1)), avg1dec) + "\n" + n1 + "\n" + Math.Round((Convert.ToDouble(std1)), std1dec), (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + Convert.ToInt32(cx * steping), h - dH, drawFormat);
            }
        }
        public void drawLeftHeader(string s)
        {
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), 20, 20, drawFormatNearCenter);
        }
        public void drawHeader(string s)
        {
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), w / 2, topSpacing - 25, drawFormat);
        }
        public void drawCenterHeader(string s)
        {
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), w / 2, topSpacing - 25, drawFormatCenter);
        }
        public void drawRightHeader(string s)
        {
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), w - 5, 14, drawFormatFarNear);
        }

        public void drawBottomStringExpl(string s, bool left, bool right)
        {
            if (left)
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), 0, topSpacing + Convert.ToInt32(maxH) + 15, drawFormatNear);
            if (right)
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), w, topSpacing + Convert.ToInt32(maxH) + 15, drawFormatFar);
        }

        public void drawBottomString(string s, int cx)
        {
            drawBottomString(s, cx, false);
        }

        public void drawBottomString(string s, int cx, bool vert)
        {
            if (vert)
            {
                objGraphics.TranslateTransform(0, h);
                objGraphics.RotateTransform(90F);
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), topSpacing + Convert.ToInt32(maxH) + 15 - h, -leftSpacing - Convert.ToInt32(cx * steping) - 10, drawFormatNear);
                objGraphics.ResetTransform();
            }
            else
            {
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + Convert.ToInt32(cx * steping), topSpacing + Convert.ToInt32(maxH) + 15, drawFormat);
            }
        }
        public void drawNSTDvalVert(int n1, float std1, int n2, float std2, int cx, bool show2) // Not used
        {
            string s = "\nn=" + n1 + (show2 ? "/" + n2 : "") + " stdev=" + Math.Round((Convert.ToDouble(std1)), 2) + (show2 ? "/" + Math.Round((Convert.ToDouble(std2)), 2) : "");
            objGraphics.TranslateTransform(0, h);
            objGraphics.RotateTransform(90F);
            objGraphics.DrawString(s, (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), topSpacing + Convert.ToInt32(maxH) + 15 - h, -leftSpacing - Convert.ToInt32(cx * steping) - 10, drawFormatNear);
            objGraphics.ResetTransform();
        }

        public void drawStringInGraph(string s, int l, int t)
        {
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + l, topSpacing + t, drawFormatNearCenter);
        }

        public void drawLine(int color, int x1, int y1, int x2, int y2)
        {
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#" + colors[color]), 2), leftSpacing + x1, topSpacing + y1, leftSpacing + x2, topSpacing + y2);
        }

        public void drawStepLine(int color, int x1, float y1, int x2, float y2)
        {
            drawStepLine(color, x1, y1, x2, y2, 0, 100);
        }

        public void drawStepLine(int color, int x1, float y1, int x2, float y2, int from, int to)
        {
            float mMaxH = maxH * to / 100 - maxH * from / 100;
            float dMaxH = maxH - maxH * to / 100;

            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#" + colors[color]), 2), leftSpacing + Convert.ToInt32(x1 * steping), topSpacing + dMaxH + Convert.ToInt32(mMaxH - (y1 - minVal) / (maxVal - minVal) * mMaxH), leftSpacing + Convert.ToInt32(x2 * steping), topSpacing + dMaxH + Convert.ToInt32(mMaxH - (y2 - minVal) / (maxVal - minVal) * mMaxH));
        }

        public void drawGreyArea(int x1, float y1, int x2, float y2)
        {
            objGraphics.DrawPolygon(new Pen(ColorTranslator.FromHtml("#DCDCDC")), new Point[] { new Point(leftSpacing + Convert.ToInt32(x1 * steping) + 1, topSpacing + Convert.ToInt32(maxH - (y1 - minVal) / (maxVal - minVal) * maxH)), new Point(leftSpacing + Convert.ToInt32(x2 * steping), topSpacing + Convert.ToInt32(maxH - (y2 - minVal) / (maxVal - minVal) * maxH)), new Point(leftSpacing + Convert.ToInt32(x2 * steping), topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH)), new Point(leftSpacing + Convert.ToInt32(x1 * steping) + 1, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH)) });
            objGraphics.FillPolygon(new SolidBrush(ColorTranslator.FromHtml("#DCDCDC")), new Point[] { new Point(leftSpacing + Convert.ToInt32(x1 * steping) + 1, topSpacing + Convert.ToInt32(maxH - (y1 - minVal) / (maxVal - minVal) * maxH)), new Point(leftSpacing + Convert.ToInt32(x2 * steping), topSpacing + Convert.ToInt32(maxH - (y2 - minVal) / (maxVal - minVal) * maxH)), new Point(leftSpacing + Convert.ToInt32(x2 * steping), topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH)), new Point(leftSpacing + Convert.ToInt32(x1 * steping) + 1, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH)) });
        }

        public void drawBar(int color, int i, float v)
        {
            drawBar(color, i, v, Convert.ToInt32(steping), barW, 1, 0, 100, false);
        }

        public void drawBar(int color, int i, float v, bool writeN, bool percent)
        {
            drawBar(color, i, v, Convert.ToInt32(steping), barW, 1, 0, 100, writeN, percent);
        }

        public void drawBar(int color, int i, float v, int occupy)
        {
            drawBar(color, i, v, Convert.ToInt32(steping), barW, 1, 0, occupy, false);
        }

        public void drawBar(int color, int i, float v, int barDivision, int align, int occupy)
        {
            drawBar(color, i, v, Convert.ToInt32(steping), barW, barDivision, align, occupy, false);
        }

        public void drawBar(int color, int i, float v, int s, int b, int barDivision, int align, int occupy)
        {
            drawBar(color, i, v, s, b, barDivision, align, occupy, false);
        }

        public void drawBar(int color, int i, float v, int s, int b, int barDivision, int align, int occupy, bool writeN)
        {
            drawBar(color, i, v, s, b, barDivision, align, occupy, writeN, false);
        }

        public void drawReference(int i, int v)
        {
            drawReference(leftSpacing + Convert.ToInt32(i * steping), topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), "");
        }

        public void drawReference(int x, int y, string expl)
        {
            objGraphics.FillPolygon(
                new SolidBrush(ColorTranslator.FromHtml("#000000")),
                new Point[] {
							new Point(x,y-6),
							new Point(x+6,y),
							new Point(x,y+6),
							new Point(x-6,y)
						}
                );
            objGraphics.DrawPolygon(
                new Pen(ColorTranslator.FromHtml("#999999")),
                new Point[] {
							new Point(x,y-6),
							new Point(x+6,y),
							new Point(x,y+6),
							new Point(x-6,y)
						}
                );

            if (expl != "")
                objGraphics.DrawString(expl, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), x + 5, y, drawFormatNearCenter);
        }

        public void drawReferenceLine(int v)
        {
            drawReferenceLine(v, "");
        }

        public void drawReferenceLine(int v, string s)
        {
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 2), leftSpacing, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), w - 50 - innerRightSpacing, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH));
            if (s != "")
            {
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), w - innerRightSpacing - 45, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), drawFormatNearCenter);
            }
        }

        public void drawReferenceLineExpl(int x, int y, string expl)
        {
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 2), x, y, x + 20, y);
            objGraphics.DrawString(expl, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), x + 25, y, drawFormatNearCenter);
        }

        public void drawBar(int color, int i, float v, int s, int b, int barDivision, int align, int occupy, bool writeN, bool percent)
        {
			float mMaxH = maxH * occupy / 100;
			float dMaxH = maxH - mMaxH;
			int atH = 0;
			if (v != -1)
			{
				atH = Convert.ToInt32(mMaxH - (Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH);
				objGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + i * s - b / 2 + b / 2 * align, topSpacing + dMaxH + atH, b / barDivision, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH));
				objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + 1 + i * s - b / 2 + b / 2 * align, topSpacing + 1 + dMaxH + atH, b / barDivision - 1, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH) - 1);
				if (barDivision <= 2)
				{
					objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#999999")), leftSpacing + b / barDivision + 1 + i * s - b / 2 + b / 2 * align, topSpacing + 2 + dMaxH + atH, 2, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH) - 2);
				}
				if (writeN && b / barDivision > 30)
				{
					objGraphics.DrawString(v.ToString() + (percent ? "%" : ""), (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + 1 + i * s - b / 2 + b / 2 * align + (b / barDivision - 1) / 2, topSpacing + 2 + dMaxH + (atH > mMaxH / 5 ? atH - 15 : atH + 15), drawFormatCenter);
				}
			}
			else if (writeN && b / barDivision > 30)
			{
				atH = Convert.ToInt32(mMaxH - (0d - minVal) / (maxVal - minVal) * mMaxH);
				objGraphics.DrawString("X", (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + 1 + i * s - b / 2 + b / 2 * align + (b / barDivision - 1) / 2, topSpacing + 2 + dMaxH + (atH > mMaxH / 5 ? atH - 15 : atH + 15), drawFormatCenter);
			}
        }

        public void drawCircle(string color, int i, float v, float lastV)
        {
            drawCircle(color, i, v, lastV, 0);
        }
		public void drawCircle(int color, int i, float v, float lastV, int c, int lastPos, int size)
		{
			drawCircle(colors[color], i, v, lastV, c, lastPos, size);
		}
		public void drawCircle(string color, int i, float v, float lastV, int c)
		{
			drawCircle(color, i, v, lastV, c, i-1, 6);
		}
        public void drawCircle(string color, int i, float v, float lastV, int c, int lastPos, int size)
        {
            if (lastV != -1f)
            {
				objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 1), leftSpacing + Convert.ToInt32((lastPos) * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(lastV) - minVal) / (maxVal - minVal) * maxH), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * maxH));
            }
            if (c > 0)
            {
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 1), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal + 8) / (maxVal - minVal) * maxH), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 1), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal + 8) / (maxVal - minVal) * maxH), leftSpacing + Convert.ToInt32(i * steping) + 5, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal + 5) / (maxVal - minVal) * maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 1), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal + 8) / (maxVal - minVal) * maxH), leftSpacing + Convert.ToInt32(i * steping) - 5, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal + 5) / (maxVal - minVal) * maxH));
            }
            else if (c < 0)
            {
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 1), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal - 8) / (maxVal - minVal) * maxH), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 1), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal - 8) / (maxVal - minVal) * maxH), leftSpacing + Convert.ToInt32(i * steping) + 5, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal - 5) / (maxVal - minVal) * maxH));
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 1), leftSpacing + Convert.ToInt32(i * steping), topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal - 8) / (maxVal - minVal) * maxH), leftSpacing + Convert.ToInt32(i * steping) - 5, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal - 5) / (maxVal - minVal) * maxH));
            }

			objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + color)), leftSpacing + Convert.ToInt32(i * steping) - size, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * maxH) - size, size * 2, size * 2);
			objGraphics.DrawEllipse(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + Convert.ToInt32(i * steping) - size, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * maxH) - size, size * 2, size * 2);
        }
		public void drawDot(int color, int pos, float val, int lastPos, float lastVal, int size)
		{
			if (lastVal != -1f)
			{
				objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + Convert.ToInt32((float)lastPos * steping) - size, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(lastVal) - minVal) / (maxVal - minVal) * maxH) - size, size * 2, size * 2);
				objGraphics.DrawEllipse(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + Convert.ToInt32((float)lastPos * steping) - size, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(lastVal) - minVal) / (maxVal - minVal) * maxH) - size, size * 2, size * 2);
			}

			objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + Convert.ToInt32((float)pos*steping) - size, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(val) - minVal) / (maxVal - minVal) * maxH) - size, size * 2, size * 2);
			objGraphics.DrawEllipse(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + Convert.ToInt32((float)pos * steping) - size, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(val) - minVal) / (maxVal - minVal) * maxH) - size, size * 2, size * 2);
		}
        
        public void drawCircle(string color, int i, float v)
        {
            objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + color)), leftSpacing + Convert.ToInt32(i * steping) - 3, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * maxH) - 3, 6, 6);
            objGraphics.DrawEllipse(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + Convert.ToInt32(i * steping) - 3, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * maxH) - 3, 6, 6);
        }

        public void drawMultiBar(int color, int i, float v, int s, int b, int barDivision, int align, int occupy, bool writeN, bool percent)
        {
            float mMaxH = maxH * occupy / 100;
            float dMaxH = maxH - mMaxH;
            int atH = Convert.ToInt32(mMaxH - (Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH);
            objGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + i * s - b / 2 + b / barDivision * align, topSpacing + dMaxH + atH, b / barDivision, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH));
            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + 1 + i * s - b / 2 + b / barDivision * align, topSpacing + 1 + dMaxH + atH, b / barDivision - 1, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH) - 1);
            if (barDivision <= 2)
            {
                objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#999999")), leftSpacing + b / barDivision + 1 + i * s - b / 2 + b / barDivision * align, topSpacing + 2 + dMaxH + atH, 2, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH) - 2);
            }
            if (writeN && b / barDivision > 30)
            {
                objGraphics.DrawString(v.ToString() + (percent ? "%" : ""), (new Font(font, smallFontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + 1 + i * s - b / 2 + b / barDivision * align + (b / barDivision - 1) / 2, topSpacing + 2 + dMaxH + (atH > mMaxH / 5 ? atH - 15 : atH + 15), drawFormatCenter);
            }
        }

        public void drawExplBox(string s, int height)
        {
            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), leftSpacing, h - height - 5, w - leftSpacing - rightSpacing, height);
            objGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing, h - height - 5, w - leftSpacing - rightSpacing, height);
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + 10, h - height + 8, drawFormatNearCenter);
        }

        public void drawExplBoxExpl(string s, int color, int cx, int height, int cols)
        {
            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + 10 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 + 18, 10, 10);
            objGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + 10 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 + 18, 10, 10);
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + 25 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 + 23, drawFormatNearCenter);
        }

        public void drawColorExplBox(string s, int color, int x, int y)
        {
            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), x, y, 10, 10);
            objGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), x, y, 10, 10);
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), x + 20, y + 5, drawFormatNearCenter);
        }

        public void drawExplBoxExpl2Rows(string s, int color, int cx, int height, int cols)
        {
            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + 10 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 * 2 + 18, 10, 10);
            objGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), leftSpacing + 10 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 * 2 + 18, 10, 10);
            objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), leftSpacing + 25 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 * 2 + 17, drawFormatNear);
        }

        public void drawAxisExpl(string s, int color, bool right, bool box)
        {
            if (!right)
            {
                if (box)
                {
                    objGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), 5, 15, 10, 10);
                    objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), 6, 16, 9, 9);
                }
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), 20, 20, drawFormatNearCenter);
            }
            else
            {
                if (box)
                {
                    objGraphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), w - 15, 15, 10, 10);
                    objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), w - 14, 16, 9, 9);
                }
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#000000"))), w - 20, 20, drawFormatFarCenter);
            }
        }
    }


    public unsafe class OctreeQuantizer : Quantizer
    {
        /// <summary>
        /// Construct the octree quantizer
        /// </summary>
        /// <remarks>
        /// The Octree quantizer is a two pass algorithm. The initial pass sets up the octree,
        /// the second pass quantizes a color based on the nodes in the tree
        /// </remarks>
        /// <param name="maxColors">The maximum number of colors to return</param>
        /// <param name="maxColorBits">The number of significant bits</param>
        public OctreeQuantizer(int maxColors, int maxColorBits)
            : base(false)
        {
            if (maxColors > 255)
                throw new ArgumentOutOfRangeException("maxColors", maxColors, "The number of colors should be less than 256");

            if ((maxColorBits < 1) | (maxColorBits > 8))
                throw new ArgumentOutOfRangeException("maxColorBits", maxColorBits, "This should be between 1 and 8");

            // Construct the octree
            _octree = new Octree(maxColorBits);

            _maxColors = maxColors;
        }

        /// <summary>
        /// Process the pixel in the first pass of the algorithm
        /// </summary>
        /// <param name="pixel">The pixel to quantize</param>
        /// <remarks>
        /// This function need only be overridden if your quantize algorithm needs two passes,
        /// such as an Octree quantizer.
        /// </remarks>
        protected override void InitialQuantizePixel(Color32* pixel)
        {
            // Add the color to the octree
            _octree.AddColor(pixel);
        }

        /// <summary>
        /// Override this to process the pixel in the second pass of the algorithm
        /// </summary>
        /// <param name="pixel">The pixel to quantize</param>
        /// <returns>The quantized value</returns>
        protected override byte QuantizePixel(Color32* pixel)
        {
            byte paletteIndex = (byte)_maxColors;	// The color at [_maxColors] is set to transparent

            // Get the palette index if this non-transparent
            if (pixel->Alpha > 0)
                paletteIndex = (byte)_octree.GetPaletteIndex(pixel);

            return paletteIndex;
        }

        /// <summary>
        /// Retrieve the palette for the quantized image
        /// </summary>
        /// <param name="original">Any old palette, this is overrwritten</param>
        /// <returns>The new color palette</returns>
        protected override ColorPalette GetPalette(ColorPalette original)
        {
            // First off convert the octree to _maxColors colors
            ArrayList palette = _octree.Palletize(_maxColors - 1);

            // Then convert the palette based on those colors
            for (int index = 0; index < palette.Count; index++)
                original.Entries[index] = (Color)palette[index];

            // Add the transparent color
            original.Entries[_maxColors] = Color.FromArgb(0, 0, 0, 0);

            return original;
        }

        /// <summary>
        /// Stores the tree
        /// </summary>
        private Octree _octree;

        /// <summary>
        /// Maximum allowed color depth
        /// </summary>
        private int _maxColors;

        /// <summary>
        /// Class which does the actual quantization
        /// </summary>
        private class Octree
        {
            /// <summary>
            /// Construct the octree
            /// </summary>
            /// <param name="maxColorBits">The maximum number of significant bits in the image</param>
            public Octree(int maxColorBits)
            {
                _maxColorBits = maxColorBits;
                _leafCount = 0;
                _reducibleNodes = new OctreeNode[9];
                _root = new OctreeNode(0, _maxColorBits, this);
                _previousColor = 0;
                _previousNode = null;
            }

            /// <summary>
            /// Add a given color value to the octree
            /// </summary>
            /// <param name="pixel"></param>
            public void AddColor(Color32* pixel)
            {
                // Check if this request is for the same color as the last
                if (_previousColor == pixel->ARGB)
                {
                    // If so, check if I have a previous node setup. This will only ocurr if the first color in the image
                    // happens to be black, with an alpha component of zero.
                    if (null == _previousNode)
                    {
                        _previousColor = pixel->ARGB;
                        _root.AddColor(pixel, _maxColorBits, 0, this);
                    }
                    else
                        // Just update the previous node
                        _previousNode.Increment(pixel);
                }
                else
                {
                    _previousColor = pixel->ARGB;
                    _root.AddColor(pixel, _maxColorBits, 0, this);
                }
            }

            /// <summary>
            /// Reduce the depth of the tree
            /// </summary>
            public void Reduce()
            {
                int index;

                // Find the deepest level containing at least one reducible node
                for (index = _maxColorBits - 1; (index > 0) && (null == _reducibleNodes[index]); index--) ;

                // Reduce the node most recently added to the list at level 'index'
                OctreeNode node = _reducibleNodes[index];
                _reducibleNodes[index] = node.NextReducible;

                // Decrement the leaf count after reducing the node
                _leafCount -= node.Reduce();

                // And just in case I've reduced the last color to be added, and the next color to
                // be added is the same, invalidate the previousNode...
                _previousNode = null;
            }

            /// <summary>
            /// Get/Set the number of leaves in the tree
            /// </summary>
            public int Leaves
            {
                get { return _leafCount; }
                set { _leafCount = value; }
            }

            /// <summary>
            /// Return the array of reducible nodes
            /// </summary>
            protected OctreeNode[] ReducibleNodes
            {
                get { return _reducibleNodes; }
            }

            /// <summary>
            /// Keep track of the previous node that was quantized
            /// </summary>
            /// <param name="node">The node last quantized</param>
            protected void TrackPrevious(OctreeNode node)
            {
                _previousNode = node;
            }

            /// <summary>
            /// Convert the nodes in the octree to a palette with a maximum of colorCount colors
            /// </summary>
            /// <param name="colorCount">The maximum number of colors</param>
            /// <returns>An arraylist with the palettized colors</returns>
            public ArrayList Palletize(int colorCount)
            {
                while (Leaves > colorCount)
                    Reduce();

                // Now palettize the nodes
                ArrayList palette = new ArrayList(Leaves);
                int paletteIndex = 0;
                _root.ConstructPalette(palette, ref paletteIndex);

                // And return the palette
                return palette;
            }

            /// <summary>
            /// Get the palette index for the passed color
            /// </summary>
            /// <param name="pixel"></param>
            /// <returns></returns>
            public int GetPaletteIndex(Color32* pixel)
            {
                return _root.GetPaletteIndex(pixel, 0);
            }

            /// <summary>
            /// Mask used when getting the appropriate pixels for a given node
            /// </summary>
            private static int[] mask = new int[8] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

            /// <summary>
            /// The root of the octree
            /// </summary>
            private OctreeNode _root;

            /// <summary>
            /// Number of leaves in the tree
            /// </summary>
            private int _leafCount;

            /// <summary>
            /// Array of reducible nodes
            /// </summary>
            private OctreeNode[] _reducibleNodes;

            /// <summary>
            /// Maximum number of significant bits in the image
            /// </summary>
            private int _maxColorBits;

            /// <summary>
            /// Store the last node quantized
            /// </summary>
            private OctreeNode _previousNode;

            /// <summary>
            /// Cache the previous color quantized
            /// </summary>
            private int _previousColor;

            /// <summary>
            /// Class which encapsulates each node in the tree
            /// </summary>
            protected class OctreeNode
            {
                /// <summary>
                /// Construct the node
                /// </summary>
                /// <param name="level">The level in the tree = 0 - 7</param>
                /// <param name="colorBits">The number of significant color bits in the image</param>
                /// <param name="octree">The tree to which this node belongs</param>
                public OctreeNode(int level, int colorBits, Octree octree)
                {
                    // Construct the new node
                    _leaf = (level == colorBits);

                    _red = _green = _blue = 0;
                    _pixelCount = 0;

                    // If a leaf, increment the leaf count
                    if (_leaf)
                    {
                        octree.Leaves++;
                        _nextReducible = null;
                        _children = null;
                    }
                    else
                    {
                        // Otherwise add this to the reducible nodes
                        _nextReducible = octree.ReducibleNodes[level];
                        octree.ReducibleNodes[level] = this;
                        _children = new OctreeNode[8];
                    }
                }

                /// <summary>
                /// Add a color into the tree
                /// </summary>
                /// <param name="pixel">The color</param>
                /// <param name="colorBits">The number of significant color bits</param>
                /// <param name="level">The level in the tree</param>
                /// <param name="octree">The tree to which this node belongs</param>
                public void AddColor(Color32* pixel, int colorBits, int level, Octree octree)
                {
                    // Update the color information if this is a leaf
                    if (_leaf)
                    {
                        Increment(pixel);
                        // Setup the previous node
                        octree.TrackPrevious(this);
                    }
                    else
                    {
                        // Go to the next level down in the tree
                        int shift = 7 - level;
                        int index = ((pixel->Red & mask[level]) >> (shift - 2)) |
                            ((pixel->Green & mask[level]) >> (shift - 1)) |
                            ((pixel->Blue & mask[level]) >> (shift));

                        OctreeNode child = _children[index];

                        if (null == child)
                        {
                            // Create a new child node & store in the array
                            child = new OctreeNode(level + 1, colorBits, octree);
                            _children[index] = child;
                        }

                        // Add the color to the child node
                        child.AddColor(pixel, colorBits, level + 1, octree);
                    }

                }

                /// <summary>
                /// Get/Set the next reducible node
                /// </summary>
                public OctreeNode NextReducible
                {
                    get { return _nextReducible; }
                    set { _nextReducible = value; }
                }

                /// <summary>
                /// Return the child nodes
                /// </summary>
                public OctreeNode[] Children
                {
                    get { return _children; }
                }

                /// <summary>
                /// Reduce this node by removing all of its children
                /// </summary>
                /// <returns>The number of leaves removed</returns>
                public int Reduce()
                {
                    _red = _green = _blue = 0;
                    int children = 0;

                    // Loop through all children and add their information to this node
                    for (int index = 0; index < 8; index++)
                    {
                        if (null != _children[index])
                        {
                            _red += _children[index]._red;
                            _green += _children[index]._green;
                            _blue += _children[index]._blue;
                            _pixelCount += _children[index]._pixelCount;
                            ++children;
                            _children[index] = null;
                        }
                    }

                    // Now change this to a leaf node
                    _leaf = true;

                    // Return the number of nodes to decrement the leaf count by
                    return (children - 1);
                }

                /// <summary>
                /// Traverse the tree, building up the color palette
                /// </summary>
                /// <param name="palette">The palette</param>
                /// <param name="paletteIndex">The current palette index</param>
                public void ConstructPalette(ArrayList palette, ref int paletteIndex)
                {
                    if (_leaf)
                    {
                        // Consume the next palette index
                        _paletteIndex = paletteIndex++;

                        // And set the color of the palette entry
                        palette.Add(Color.FromArgb(_red / _pixelCount, _green / _pixelCount, _blue / _pixelCount));
                    }
                    else
                    {
                        // Loop through children looking for leaves
                        for (int index = 0; index < 8; index++)
                        {
                            if (null != _children[index])
                                _children[index].ConstructPalette(palette, ref paletteIndex);
                        }
                    }
                }

                /// <summary>
                /// Return the palette index for the passed color
                /// </summary>
                public int GetPaletteIndex(Color32* pixel, int level)
                {
                    int paletteIndex = _paletteIndex;

                    if (!_leaf)
                    {
                        int shift = 7 - level;
                        int index = ((pixel->Red & mask[level]) >> (shift - 2)) |
                            ((pixel->Green & mask[level]) >> (shift - 1)) |
                            ((pixel->Blue & mask[level]) >> (shift));

                        if (null != _children[index])
                            paletteIndex = _children[index].GetPaletteIndex(pixel, level + 1);
                        else
                            throw new Exception("Didn't expect this!");
                    }

                    return paletteIndex;
                }

                /// <summary>
                /// Increment the pixel count and add to the color information
                /// </summary>
                public void Increment(Color32* pixel)
                {
                    _pixelCount++;
                    _red += pixel->Red;
                    _green += pixel->Green;
                    _blue += pixel->Blue;
                }

                /// <summary>
                /// Flag indicating that this is a leaf node
                /// </summary>
                private bool _leaf;

                /// <summary>
                /// Number of pixels in this node
                /// </summary>
                private int _pixelCount;

                /// <summary>
                /// Red component
                /// </summary>
                private int _red;

                /// <summary>
                /// Green Component
                /// </summary>
                private int _green;

                /// <summary>
                /// Blue component
                /// </summary>
                private int _blue;

                /// <summary>
                /// Pointers to any child nodes
                /// </summary>
                private OctreeNode[] _children;

                /// <summary>
                /// Pointer to next reducible node
                /// </summary>
                private OctreeNode _nextReducible;

                /// <summary>
                /// The index of this node in the palette
                /// </summary>
                private int _paletteIndex;

            }
        }


    }


    public unsafe abstract class Quantizer
    {
        /// <summary>
        /// Construct the quantizer
        /// </summary>
        /// <param name="singlePass">If true, the quantization only needs to loop through the source pixels once</param>
        /// <remarks>
        /// If you construct this class with a true value for singlePass, then the code will, when quantizing your image,
        /// only call the 'QuantizeImage' function. If two passes are required, the code will call 'InitialQuantizeImage'
        /// and then 'QuantizeImage'.
        /// </remarks>
        public Quantizer(bool singlePass)
        {
            _singlePass = singlePass;
        }

        /// <summary>
        /// Quantize an image and return the resulting output bitmap
        /// </summary>
        /// <param name="source">The image to quantize</param>
        /// <returns>A quantized version of the image</returns>
        public Bitmap Quantize(Image source)
        {
            // Get the size of the source image
            int height = source.Height;
            int width = source.Width;

            // And construct a rectangle from these dimensions
            Rectangle bounds = new Rectangle(0, 0, width, height);

            // First off take a 32bpp copy of the image
            Bitmap copy = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            // And construct an 8bpp version
            Bitmap output = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            // Now lock the bitmap into memory
            using (Graphics g = Graphics.FromImage(copy))
            {
                g.PageUnit = GraphicsUnit.Pixel;

                // Draw the source image onto the copy bitmap,
                // which will effect a widening as appropriate.
                g.DrawImageUnscaled(source, bounds);
            }

            // Define a pointer to the bitmap data
            BitmapData sourceData = null;

            try
            {
                // Get the source image bits and lock into memory
                sourceData = copy.LockBits(bounds, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                // Call the FirstPass function if not a single pass algorithm.
                // For something like an octree quantizer, this will run through
                // all image pixels, build a data structure, and create a palette.
                if (!_singlePass)
                    FirstPass(sourceData, width, height);

                // Then set the color palette on the output bitmap. I'm passing in the current palette 
                // as there's no way to construct a new, empty palette.
                output.Palette = this.GetPalette(output.Palette);

                // Then call the second pass which actually does the conversion
                SecondPass(sourceData, output, width, height, bounds);
            }
            finally
            {
                // Ensure that the bits are unlocked
                copy.UnlockBits(sourceData);
            }

            // Last but not least, return the output bitmap
            return output;
        }

        /// <summary>
        /// Execute the first pass through the pixels in the image
        /// </summary>
        /// <param name="sourceData">The source data</param>
        /// <param name="width">The width in pixels of the image</param>
        /// <param name="height">The height in pixels of the image</param>
        protected virtual void FirstPass(BitmapData sourceData, int width, int height)
        {
            // Define the source data pointers. The source row is a byte to
            // keep addition of the stride value easier (as this is in bytes)
            byte* pSourceRow = (byte*)sourceData.Scan0.ToPointer();
            Int32* pSourcePixel;

            // Loop through each row
            for (int row = 0; row < height; row++)
            {
                // Set the source pixel to the first pixel in this row
                pSourcePixel = (Int32*)pSourceRow;

                // And loop through each column
                for (int col = 0; col < width; col++, pSourcePixel++)
                    // Now I have the pixel, call the FirstPassQuantize function...
                    InitialQuantizePixel((Color32*)pSourcePixel);

                // Add the stride to the source row
                pSourceRow += sourceData.Stride;
            }
        }

        /// <summary>
        /// Execute a second pass through the bitmap
        /// </summary>
        /// <param name="sourceData">The source bitmap, locked into memory</param>
        /// <param name="output">The output bitmap</param>
        /// <param name="width">The width in pixels of the image</param>
        /// <param name="height">The height in pixels of the image</param>
        /// <param name="bounds">The bounding rectangle</param>
        protected virtual void SecondPass(BitmapData sourceData, Bitmap output, int width, int height, Rectangle bounds)
        {
            BitmapData outputData = null;

            try
            {
                // Lock the output bitmap into memory
                outputData = output.LockBits(bounds, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                // Define the source data pointers. The source row is a byte to
                // keep addition of the stride value easier (as this is in bytes)
                byte* pSourceRow = (byte*)sourceData.Scan0.ToPointer();
                Int32* pSourcePixel = (Int32*)pSourceRow;
                Int32* pPreviousPixel = pSourcePixel;

                // Now define the destination data pointers
                byte* pDestinationRow = (byte*)outputData.Scan0.ToPointer();
                byte* pDestinationPixel = pDestinationRow;

                // And convert the first pixel, so that I have values going into the loop
                byte pixelValue = QuantizePixel((Color32*)pSourcePixel);

                // Assign the value of the first pixel
                *pDestinationPixel = pixelValue;

                // Loop through each row
                for (int row = 0; row < height; row++)
                {
                    // Set the source pixel to the first pixel in this row
                    pSourcePixel = (Int32*)pSourceRow;

                    // And set the destination pixel pointer to the first pixel in the row
                    pDestinationPixel = pDestinationRow;

                    // Loop through each pixel on this scan line
                    for (int col = 0; col < width; col++, pSourcePixel++, pDestinationPixel++)
                    {
                        // Check if this is the same as the last pixel. If so use that value
                        // rather than calculating it again. This is an inexpensive optimisation.
                        if (*pPreviousPixel != *pSourcePixel)
                        {
                            // Quantize the pixel
                            pixelValue = QuantizePixel((Color32*)pSourcePixel);

                            // And setup the previous pointer
                            pPreviousPixel = pSourcePixel;
                        }

                        // And set the pixel in the output
                        *pDestinationPixel = pixelValue;
                    }

                    // Add the stride to the source row
                    pSourceRow += sourceData.Stride;

                    // And to the destination row
                    pDestinationRow += outputData.Stride;
                }
            }
            finally
            {
                // Ensure that I unlock the output bits
                output.UnlockBits(outputData);
            }
        }

        /// <summary>
        /// Override this to process the pixel in the first pass of the algorithm
        /// </summary>
        /// <param name="pixel">The pixel to quantize</param>
        /// <remarks>
        /// This function need only be overridden if your quantize algorithm needs two passes,
        /// such as an Octree quantizer.
        /// </remarks>
        protected virtual void InitialQuantizePixel(Color32* pixel)
        {
        }

        /// <summary>
        /// Override this to process the pixel in the second pass of the algorithm
        /// </summary>
        /// <param name="pixel">The pixel to quantize</param>
        /// <returns>The quantized value</returns>
        protected abstract byte QuantizePixel(Color32* pixel);

        /// <summary>
        /// Retrieve the palette for the quantized image
        /// </summary>
        /// <param name="original">Any old palette, this is overrwritten</param>
        /// <returns>The new color palette</returns>
        protected abstract ColorPalette GetPalette(ColorPalette original);

        /// <summary>
        /// Flag used to indicate whether a single pass or two passes are needed for quantization.
        /// </summary>
        private bool _singlePass;

        /// <summary>
        /// Struct that defines a 32 bpp colour
        /// </summary>
        /// <remarks>
        /// This struct is used to read data from a 32 bits per pixel image
        /// in memory, and is ordered in this manner as this is the way that
        /// the data is layed out in memory
        /// </remarks>
        [StructLayout(LayoutKind.Explicit)]
        public struct Color32
        {
            /// <summary>
            /// Holds the blue component of the colour
            /// </summary>
            [FieldOffset(0)]
            public byte Blue;
            /// <summary>
            /// Holds the green component of the colour
            /// </summary>
            [FieldOffset(1)]
            public byte Green;
            /// <summary>
            /// Holds the red component of the colour
            /// </summary>
            [FieldOffset(2)]
            public byte Red;
            /// <summary>
            /// Holds the alpha component of the colour
            /// </summary>
            [FieldOffset(3)]
            public byte Alpha;

            /// <summary>
            /// Permits the color32 to be treated as an int32
            /// </summary>
            [FieldOffset(0)]
            public int ARGB;

            /// <summary>
            /// Return the color for this Color32 object
            /// </summary>
            public Color Color
            {
                get { return Color.FromArgb(Alpha, Red, Green, Blue); }
            }
        }
    }
}