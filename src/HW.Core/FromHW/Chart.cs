using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;

namespace HW.Core.FromHW
{
	public class Chart
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

        static int outerTopSpacing = 0;
        static int topSpacing = outerTopSpacing + 10;
        static int bottomSpacing = 30;
        static int rightSpacing = 15;
        static int innerRightSpacing = rightSpacing + 15;

        int w = 0;
        int h = 0;

        public int leftSpacing = 30;
        public int barW = 0;
        public int vertLines = 11;
        public float steping = 0;

        public float maxVal = float.NegativeInfinity;
        public float minVal = float.PositiveInfinity;
        public float baseline = 0;
        public float maxH = 290;

        float fontSize = 8f;
        float smallFontSize = 6.5f;
        float medFontSize = 7f;
        float dif = 0;

        string font = "Tahoma";

        string[] colors;

        public Chart(int width, int height)
        {
            init(width, height, "#f7f7f7");
        }

        public Chart(int width, int height, string color)
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
												    "4684EE","FF9900","00804F","DC3912",
				"FF6600","006600","999999","FFFF00","000066","996600","FF0099",
				//						   "31CFDD","4467E9","C444E9","FFFD78","CFFFA8",
				//"FFB4A8","CCCCCC","CC7700","6744E9","44E967","E94467"
																			   "E96744",
				"000000","FF9900","3366FF","FFCC66","FFA8FC","67298E","146930","00FFE4",
				"96FF00","6A1AFD","B49595","7D61B1","78B161","5B6258","FFFFFF"};

            objBitmap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;

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

        public void drawAxis()
        {
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#e6e6e6")), leftSpacing, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH), w - innerRightSpacing, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH));
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#e6e6e6")), leftSpacing, topSpacing, leftSpacing, topSpacing + Convert.ToInt32(maxH));
            objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#e6e6e6")), w - innerRightSpacing, topSpacing, w - innerRightSpacing, topSpacing + Convert.ToInt32(maxH));
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
            float mMaxH = maxH * to / 100 - maxH * from / 100;
            float dMaxH = maxH - maxH * to / 100;

            for (int i = 0; i < vertLines; i++)
            {
                if (i != 0)
                {
                    if (i % 2 == 0)
                    {
                        objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#ffffff")),
                            leftSpacing + 0 * steping,
                            topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * maxH),
                            (leftSpacing + (cx - 1) * steping) - (leftSpacing + 0 * steping),
                            (topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * maxH)) - (topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (vertLines - 1) * (i + 1) / (maxVal - minVal) * maxH)));
                    }
                }
            }
            for (int i = 0; i < vertLines; i++)
            {
                objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml((i == (vertLines - 1) ? "#e6e6e6" : "#efefef"))),
                    leftSpacing + 0 * steping,
                    topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * maxH),
                    leftSpacing + (cx-1) * steping,
                    topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * maxH));

                objGraphics.DrawString(Convert.ToString(Math.Round((maxVal - minVal) / (vertLines - 1) * i + minVal, 2)), (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#808080"))), leftSpacing - 5, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * mMaxH), drawFormatFarCenter);
            }
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
                objGraphics.DrawString(Convert.ToString(Math.Round((maxVal - minVal) / (vertLines - 1) * i + minVal, 2)), (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#808080"))), w - innerRightSpacing + 5, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * mMaxH), drawFormatNearCenter);
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
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#808080"))), topSpacing + Convert.ToInt32(maxH) + 15 - h, -leftSpacing - Convert.ToInt32(cx * steping) - 10, drawFormatNear);
                objGraphics.ResetTransform();
            }
            else
            {
                objGraphics.DrawString(s, (new Font(font, fontSize)), (new SolidBrush(ColorTranslator.FromHtml("#808080"))), leftSpacing + Convert.ToInt32(cx * steping), topSpacing + Convert.ToInt32(maxH) + 15, drawFormat);
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

        public void drawStepLineBroken(int color, int x1, float y1, int x2, float y2)
        {
            drawStepLineBroken(color, x1, y1, x2, y2, 0, 100);
        }

        public void drawStepLineBroken(int color, int x1, float y1, int x2, float y2, int from, int to)
        {
            float mMaxH = maxH * to / 100 - maxH * from / 100;
            float dMaxH = maxH - maxH * to / 100;

            Pen p = new Pen(ColorTranslator.FromHtml("#" + colors[color]), 2);
            p.DashStyle = DashStyle.Dash;
            objGraphics.DrawLine(p, leftSpacing + Convert.ToInt32(x1 * steping), topSpacing + dMaxH + Convert.ToInt32(mMaxH - (y1 - minVal) / (maxVal - minVal) * mMaxH), leftSpacing + Convert.ToInt32(x2 * steping), topSpacing + dMaxH + Convert.ToInt32(mMaxH - (y2 - minVal) / (maxVal - minVal) * mMaxH));
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
            drawCircle(color, i, v, lastV, c, i - 1, 6);
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
        public void drawDot(int color, int pos, float val, int lastPos, float lastVal)
        {
            if (lastVal != -1f)
            {
                objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + Convert.ToInt32((float)lastPos * steping) - 4, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(lastVal) - minVal) / (maxVal - minVal) * maxH) - 4, 8, 8);
                objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), leftSpacing + Convert.ToInt32((float)lastPos * steping) - 2.2f, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(lastVal) - minVal) / (maxVal - minVal) * maxH) - 2.2f, 4.4f, 4.4f);
            }

            objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + Convert.ToInt32((float)pos * steping) - 4, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(val) - minVal) / (maxVal - minVal) * maxH) - 4, 8,8);
            objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), leftSpacing + Convert.ToInt32((float)pos * steping) - 2.2f, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(val) - minVal) / (maxVal - minVal) * maxH) - 2.2f, 4.4f, 4.4f);
        }
        public void drawSquare(int color, int pos, float val, int lastPos, float lastVal)
        {
            if (lastVal != -1f)
            {
                objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + Convert.ToInt32((float)lastPos * steping) - 4, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(lastVal) - minVal) / (maxVal - minVal) * maxH) - 4, 8, 8);
                objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), leftSpacing + Convert.ToInt32((float)lastPos * steping) - 2.2f, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(lastVal) - minVal) / (maxVal - minVal) * maxH) - 2.2f, 4.4f, 4.4f);
            }

            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing + Convert.ToInt32((float)pos * steping) - 4, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(val) - minVal) / (maxVal - minVal) * maxH) - 4, 8, 8);
            objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), leftSpacing + Convert.ToInt32((float)pos * steping) - 2.2f, topSpacing + maxH - Convert.ToInt32((Convert.ToDouble(val) - minVal) / (maxVal - minVal) * maxH) - 2.2f, 4.4f, 4.4f);
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
	}
}
