using System;
using System.Collections;
using System.Web;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using HW.Core;

public class Graph
{
	public Bitmap objBitmap;
	public Graphics objGraphics;

	StringFormat drawFormat;
	protected StringFormat drawFormatCenter;
	StringFormat drawFormatFar;
	StringFormat drawFormatNear;
	StringFormat drawFormatFarCenter;
	StringFormat drawFormatFarNear;
	protected StringFormat drawFormatNearCenter;

	static int outerTopSpacing = 40;
	static int bottomSpacing = 40;
//	static int rightSpacing = 35;
	static int rightSpacing = 110;
	static int innerRightSpacing = rightSpacing + 15;

	public int w = 0;
	int h = 0;

	public int topSpacing = outerTopSpacing + 20;
	public int leftSpacing = 40;
	public int barW = 0;
	public int vertLines = 11;
	public int steping = 0;

	public float maxVal = float.NegativeInfinity;
	public float minVal = float.PositiveInfinity;
	public float baseline = 0;
	public float maxH = 240;

	float dif = 0;
	
	protected Font normalFont = new Font("Tahoma", 7f);
	protected Font smallFont = new Font("Tahoma", 6.5f);
	protected Font bigFont = new Font("Tahoma", 8f);
	
	protected SolidBrush solidBlackBrush = new SolidBrush(ColorTranslator.FromHtml("#000000"));
	protected SolidBrush shadowBrush = new SolidBrush(ColorTranslator.FromHtml("#999999"));
	protected Pen blackPen = new Pen(ColorTranslator.FromHtml("#000000"));

	protected string[] colors;

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

		colors = new string[22] { "95CE22", "FDB827", "CC0000", "65DD31", "31CFDD", "4467E9", "C444E9", "B49595", "5555A8", "FFB4A8", "FFA8FC", "67E944", "6744E9", "44E967", "E94467", "E96744", "33FF33", "FFFF33", "FF3333", "8EFF70", "000000", "EFEFEF" };

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
		//objGraphics.DrawString(minVal.ToString() + ":" + maxVal.ToString(), new Font("Arial", 10), new SolidBrush(ColorTranslator.FromHtml("#000000")), 300, 15, drawFormatCenter);
		setMinMax((float)Math.Floor((double)minVal/10)*10, (float)Math.Ceiling((double)maxVal/10)*10);
		float subMin = 0f, addMax = 0f;
		while (minVal == maxVal || (maxVal - minVal) / (float)(vertLines - 1) != (float)Math.Round((double)(maxVal - minVal) / (double)(vertLines - 1), 0)) {
			//if (minVal == maxVal || Convert.ToInt32(minVal) % 10 == 0)
			if (Convert.ToInt32(maxVal) % 10 == 0) {
				addMax = 0f;
				subMin = 1f;
			} else {
				addMax = 1f;
				subMin = 0f;
			}
			setMinMax(minVal - subMin, maxVal + addMax);
		}
	}
	
	public void drawAxis()
	{
		drawAxis(HttpContext.Current.Request.QueryString["DISABLED"]);
	}

	public void drawAxis(object disabled)
	{
		drawAxis(false, disabled);
	}

	public void drawBg(float lowVal, float topVal, int color)
	{
		float mMaxH = maxH * 100 / 100;
		float dMaxH = maxH - mMaxH;
		int atH = Convert.ToInt32(mMaxH - (Convert.ToDouble(topVal) - minVal) / (maxVal - minVal) * mMaxH);
		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[color])), leftSpacing, topSpacing + 1 + dMaxH + atH, w - leftSpacing - rightSpacing, Convert.ToInt32((Convert.ToDouble(topVal - lowVal) - minVal) / (maxVal - minVal) * mMaxH) - 1);
	}
	
	public void drawBgFromString(float lowVal, float topVal, string color)
	{
		float mMaxH = maxH * 100 / 100;
		float dMaxH = maxH - mMaxH;
		int top = Convert.ToInt32(mMaxH - (Convert.ToDouble(topVal) - minVal) / (maxVal - minVal) * mMaxH);
		int low = Convert.ToInt32(mMaxH - (Convert.ToDouble(lowVal) - minVal) / (maxVal - minVal) * mMaxH);
		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + color)), leftSpacing, topSpacing + 1 + dMaxH + top, w - leftSpacing - rightSpacing, low - top);
	}
	
	public void drawBgFromString2(float lowVal, float topVal, string color, string label)
	{
		float mMaxH = maxH * 100 / 100;
		float dMaxH = maxH - mMaxH;
		int top = Convert.ToInt32(mMaxH - (Convert.ToDouble(topVal) - minVal) / (maxVal - minVal) * mMaxH);
		int low = Convert.ToInt32(mMaxH - (Convert.ToDouble(lowVal) - minVal) / (maxVal - minVal) * mMaxH);
		float x = leftSpacing;
		float y = topSpacing + 1 + dMaxH + top;
		float innerWidth = w - leftSpacing - rightSpacing;
		float height = low - top;
		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + color)), x + innerWidth, y, 10, height);
		
//		objGraphics.TranslateTransform(0, h);
//		objGraphics.RotateTransform(90F);
		objGraphics.DrawString(label, smallFont, solidBlackBrush, x + innerWidth + 12, y + (height / 2), drawFormatNear);
//		objGraphics.ResetTransform();
	}

	public void drawBg(float lowPercent, float topPercent, string color)
	{
		float mMaxH = maxH * 100 / 100;
		float dMaxH = maxH - mMaxH;
		int atH = Convert.ToInt32(mMaxH - topPercent * mMaxH);
		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + color)), leftSpacing, topSpacing + 1 + dMaxH + atH, w - leftSpacing - rightSpacing, Convert.ToInt32((topPercent - lowPercent) * mMaxH) - 1);
	}

	public void drawAxis(bool right, object disabled)
	{
		#region Horizontal axis
		if (1 == 0 && !right)
		{
			objGraphics.DrawLine(blackPen, w - rightSpacing - 3, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH) - 3, w - rightSpacing, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH));
			objGraphics.DrawLine(blackPen, w - rightSpacing - 3, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH) + 3, w - rightSpacing, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH));
		}
		objGraphics.DrawLine(blackPen, leftSpacing, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH), w - GetRightSpacing(right), topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH));
		#endregion

		#region Vertical axis
		objGraphics.DrawLine(blackPen, leftSpacing - 3, outerTopSpacing + 3, leftSpacing, outerTopSpacing);
		objGraphics.DrawLine(blackPen, leftSpacing + 3, outerTopSpacing + 3, leftSpacing, outerTopSpacing);
		objGraphics.DrawLine(blackPen, leftSpacing, outerTopSpacing, leftSpacing, topSpacing + Convert.ToInt32(maxH));
//		if (HttpContext.Current.Request.QueryString["DISABLED"] != null && baseline != 0)
		if (disabled != null && baseline != 0)
		{
			objGraphics.DrawLine(blackPen, leftSpacing - 3, topSpacing + 10 + Convert.ToInt32(maxH) - 3, leftSpacing, topSpacing + 10 + Convert.ToInt32(maxH));
			objGraphics.DrawLine(blackPen, leftSpacing + 3, topSpacing + 10 + Convert.ToInt32(maxH) - 3, leftSpacing, topSpacing + 10 + Convert.ToInt32(maxH));
			objGraphics.DrawLine(blackPen, leftSpacing, topSpacing + Convert.ToInt32(maxH), leftSpacing, topSpacing + 10 + Convert.ToInt32(maxH));
		}
		#endregion
	}

	public void drawRightAxis()
	{
		#region Vertical axis
		objGraphics.DrawLine(blackPen, w - innerRightSpacing - 3, outerTopSpacing + 3, w - innerRightSpacing, outerTopSpacing);
		objGraphics.DrawLine(blackPen, w - innerRightSpacing + 3, outerTopSpacing + 3, w - innerRightSpacing, outerTopSpacing);
		objGraphics.DrawLine(blackPen, w - innerRightSpacing, outerTopSpacing, w - innerRightSpacing, topSpacing + Convert.ToInt32(maxH));
		if (baseline != 0)
		{
			objGraphics.DrawLine(blackPen, w - innerRightSpacing - 3, bottomSpacing + Convert.ToInt32(maxH) - 3, w - innerRightSpacing, bottomSpacing + Convert.ToInt32(maxH));
			objGraphics.DrawLine(blackPen, w - innerRightSpacing + 3, bottomSpacing + Convert.ToInt32(maxH) - 3, w - innerRightSpacing, bottomSpacing + Convert.ToInt32(maxH));
			objGraphics.DrawLine(blackPen, w - innerRightSpacing, Convert.ToInt32(maxH), w - innerRightSpacing, bottomSpacing + Convert.ToInt32(maxH));
		}
		#endregion
	}

	public void printCopyRight()
	{
		objGraphics.TranslateTransform(0, h);
		objGraphics.RotateTransform(90F);
		objGraphics.DrawString("eForm, " + DateTime.Now.ToString("yyyy-MM-dd, HH:mm") + " GMT", normalFont, (new SolidBrush(ColorTranslator.FromHtml("#AAAAAA"))), topSpacing - h, 5 - w, drawFormatNear);
		objGraphics.ResetTransform();
	}

	public void resetMinMax()
	{
		maxVal = float.NegativeInfinity;
		minVal = float.PositiveInfinity;
	}

	public void setMinMax(float min, float max)
	{
		if (min < minVal) {
			minVal = min;
		}
		if (max > maxVal) {
			maxVal = max;
		}
	}

	public bool computeMinMax(float minAdd, float maxAdd)
	{
		if (float.IsNaN(maxVal) || float.IsNegativeInfinity(maxVal) || float.IsNaN(minVal) || float.IsPositiveInfinity(minVal)) {
			return false;
		} else {
			#region Compute min and max values for graph
			if (minAdd != 0f || maxAdd != 0f) {
				dif = Math.Abs(maxVal - minVal);
				if (dif == 0) {
					dif = Math.Abs(maxVal * maxAdd);
				}
				if (minVal < 0) {
					minVal -= dif * minAdd;
				} else {
					minVal -= dif * minAdd;
					if (minVal < 0) {
						minVal = 0;
					}
				}
				if (maxVal < 0) {
					maxVal += dif * maxAdd;
					if (maxVal > 0) {
						maxVal = 0;
					}
				} else {
					maxVal += dif * maxAdd;
				}
				/*if (minVal > 0)
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
				}*/
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
		steping = (w - leftSpacing - innerRightSpacing) / (steps - 1);
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
		objGraphics.DrawEllipse(blackPen, x + leftSpacing, y, 10, 10);
		objGraphics.DrawString(s2, bigFont, solidBlackBrush, x + leftSpacing + 20, y + 5, drawFormatNearCenter);
		objGraphics.DrawString(s1, bigFont, solidBlackBrush, x + leftSpacing - 2, y + 5, drawFormatFarCenter);
	}

	public void drawCircleOutlineAt(string s, string color, float v, int spacing)
	{
		float mMaxH = maxH;
		float dMaxH = maxH - maxH;

		objGraphics.DrawLine(blackPen, leftSpacing - 10 - 30 * spacing, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), leftSpacing + 10, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH));
		objGraphics.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#" + color)), leftSpacing - 20 - 30 * spacing, topSpacing - 5 + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), 10, 10);
		objGraphics.DrawEllipse(blackPen, leftSpacing - 20 - 30 * spacing, topSpacing - 5 + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), 10, 10);
		objGraphics.DrawString(s, bigFont, solidBlackBrush, leftSpacing - 22 - 30 * spacing, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (v - minVal) / (maxVal - minVal) * mMaxH) + 1, drawFormatFarCenter);

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
		objGraphics.DrawString(s, bigFont, solidBlackBrush, leftSpacing - 5, topSpacing + dMaxH + Convert.ToInt32(mMaxH - v / (maxVal - minVal) * mMaxH), drawFormatFarCenter);
	}
	
	public void drawOutlines(int steps)
	{
		drawOutlines(steps, true, false);
	}
	
	public void drawOutlines(int steps, bool vals)
	{
		drawOutlines(steps, vals, false);
	}
	
	public int GetRightSpacing(bool right)
	{
		return right ? innerRightSpacing : rightSpacing;
	}
	
	public void drawOutlines(int steps, bool vals, bool right)
	{
		// Horizontal height marker lines
		for (int i = 0; i < steps; i++) {
			int x1 = leftSpacing;
			int y1 = topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (steps - 1) * i / (maxVal - minVal) * maxH);
			int x2 = w - GetRightSpacing(right);
			int y2 = topSpacing + Convert.ToInt32(maxH - (maxVal - minVal) / (steps - 1) * i / (maxVal - minVal) * maxH);
			objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#CCCCCC")), x1, y1, x2, y2);
		}

		float mMaxH = maxH;
		float dMaxH = maxH - maxH;

		if (vals) {
			for (int i = 0; i < steps; i++) {
				string s = Convert.ToString(Math.Round((maxVal - minVal) / (steps - 1) * i + minVal, 2));
				float x = leftSpacing - 5;
				float y = topSpacing + dMaxH + Convert.ToInt32(mMaxH - (maxVal - minVal) / (steps - 1) * i / (maxVal - minVal) * mMaxH);
				objGraphics.DrawString(s, bigFont, solidBlackBrush, x, y, drawFormatFarCenter);
			}
		}
	}

	public void drawAxisVal(string s, int steps, int i)
	{
		float mMaxH = maxH;
		float dMaxH = maxH - maxH;

		objGraphics.DrawString(s, bigFont, solidBlackBrush, leftSpacing - 5, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (maxVal - minVal) / (steps - 1) * i / (maxVal - minVal) * mMaxH), drawFormatFarCenter);
	}
	
	protected SolidBrush GetSolidBrush(string color)
	{
		return new SolidBrush(ColorTranslator.FromHtml("#" + color));
	}
	
	public void drawBottomString(string s, int cx)
	{
		drawBottomString(s, cx, false);
	}

	public void drawBottomString(string s, int cx, bool vert)
	{
		if (vert) {
			objGraphics.TranslateTransform(0, h);
			objGraphics.RotateTransform(90F);
			objGraphics.DrawString(s, bigFont, solidBlackBrush, topSpacing + Convert.ToInt32(maxH) + 15 - h, -leftSpacing - cx * steping - 10, drawFormatNear);
			objGraphics.ResetTransform();
		} else {
			objGraphics.DrawString(s, bigFont, solidBlackBrush, leftSpacing + cx * steping, topSpacing + Convert.ToInt32(maxH) + 15, drawFormat);
		}
	}

	public void drawStringInGraph(string s, int l, int t)
	{
		objGraphics.DrawString(s, bigFont, solidBlackBrush, leftSpacing + l, topSpacing + t, drawFormatNearCenter);
	}
	
	public void drawLine(ILine line)
	{
		drawLine(line.Color, line.X1, (int)line.Y1, line.X2, (int)line.Y2, line.T);
	}
	
	public void drawLine(int color, int x1, int y1, int x2, int y2, int t)
	{
		objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#" + colors[color]), t), leftSpacing + x1, topSpacing + y1, leftSpacing + x2, topSpacing + y2);
	}

	public void drawStepLine(int color, int x1, float y1, int x2, float y2)
	{
		drawStepLine(color, x1, y1, x2, y2, 0, 100, 2);
	}

	public void drawStepLine(int color, int x1, float y1, int x2, float y2, int t)
	{
		drawStepLine(color, x1, y1, x2, y2, 0, 100, t);
	}

	public void drawStepLine(int color, int x1, float y1, int x2, float y2, int from, int to, int t)
	{
		float mMaxH = maxH * to / 100 - maxH * from / 100;
		float dMaxH = maxH - maxH * to / 100;

		objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#" + colors[color]), t), leftSpacing + x1 * steping, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (y1 - minVal) / (maxVal - minVal) * mMaxH), leftSpacing + x2 * steping, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (y2 - minVal) / (maxVal - minVal) * mMaxH));
	}

	public void drawBar(int color, int i, float v)
	{
		drawBar(color, i, v, steping, barW, 1, 0, 100, false);
	}

	public void drawColorExplBox(string s, int color, int x, int y)
	{
		objGraphics.FillRectangle(GetSolidBrush(colors[color]), x, y, 10, 10);
		objGraphics.DrawRectangle(blackPen, x, y, 10, 10);
		objGraphics.DrawString(s, bigFont, solidBlackBrush, x + 20, y + 5, drawFormatNearCenter);
	}

	public void drawAxisExpl(string s, int color, bool right, bool box)
	{
		if (!right) {
			if (box) {
				objGraphics.DrawRectangle(blackPen, 5, 15, 10, 10);
				objGraphics.FillRectangle(GetSolidBrush(colors[color]), 6, 16, 9, 9);
			}
			objGraphics.DrawString(s, bigFont, solidBlackBrush, 20, 20, drawFormatNearCenter);
		} else {
			if (box) {
				objGraphics.DrawRectangle(blackPen, w - 15, 15, 10, 10);
				objGraphics.FillRectangle(GetSolidBrush(colors[color]), w - 14, 16, 9, 9);
			}
			objGraphics.DrawString(s, bigFont, solidBlackBrush, w - 20, 20, drawFormatFarCenter);
		}
	}

	public void drawReferenceLine(int v, string s)
	{
		objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 2), leftSpacing, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), w - 50 - innerRightSpacing, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH));
		if (s != "") {
			objGraphics.DrawString(s, bigFont, solidBlackBrush, w - innerRightSpacing - 45, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), drawFormatNearCenter);
		}
	}

	public void drawBar(int color, int i, float v, int s, int b, int barDivision, int align, int occupy, bool writeN, bool percent)
	{
		float mMaxH = maxH * occupy / 100;
		float dMaxH = maxH - mMaxH;
		int atH = Convert.ToInt32(mMaxH - (Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH);
		float x = leftSpacing + i * s - b / 2 + b / 2 * align;
		float y = topSpacing + dMaxH + atH;
		float width = b / barDivision;
		float height = Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH);
		objGraphics.DrawRectangle(blackPen, x, y, width, height);
		objGraphics.FillRectangle(GetSolidBrush(colors[color]), leftSpacing + 1 + i * s - b / 2 + b / 2 * align, topSpacing + 1 + dMaxH + atH, b / barDivision - 1, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH) - 1);
		if (barDivision <= 2) {
			objGraphics.FillRectangle(shadowBrush, leftSpacing + b / barDivision + 1 + i * s - b / 2 + b / 2 * align, topSpacing + 2 + dMaxH + atH, 2, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH) - 2);
		}
		if (writeN && b / barDivision > 30) {
			objGraphics.DrawString(v.ToString() + (percent ? "%" : ""), smallFont, solidBlackBrush, leftSpacing + 1 + i * s - b / 2 + b / 2 * align + (b / barDivision - 1) / 2, topSpacing + 2 + dMaxH + (atH > mMaxH / 5 ? atH - 15 : atH + 15), drawFormatCenter);
		}
	}
	
	public void drawCircle(int i, float v)
	{
		drawCircle(i, v, 20);
	}
	
	public void drawCircle(int i, float v, int color)
	{
		float mMaxH = maxH;
		float dMaxH = maxH - mMaxH;
		int atH = Convert.ToInt32(mMaxH - (Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH);
		objGraphics.DrawEllipse(
			new Pen(ColorTranslator.FromHtml("#" + colors[color])),
			leftSpacing + i * steping - 5,
			topSpacing + dMaxH + atH - 5,
			10,
			10
		);
	}

	public void drawBar(int color, int i, float v, int s, int b, int barDivision, int align, int occupy, bool writeN)
	{
		drawBar(color, i, v, s, b, barDivision, align, occupy, writeN, false);
	}

	public void drawReference(int i, int v)
	{
		drawReference(leftSpacing + i * steping, topSpacing + Convert.ToInt32(maxH - (v - minVal) / (maxVal - minVal) * maxH), "");
	}

	public void drawReference(int x, int y, string expl)
	{
		objGraphics.FillPolygon(
			solidBlackBrush,
			new Point[] {
				new Point(x, y-6),
				new Point(x+6, y),
				new Point(x, y+6),
				new Point(x-6, y)
			}
		);
		objGraphics.DrawPolygon(
			new Pen(ColorTranslator.FromHtml("#999999")),
			new Point[] {
				new Point(x, y-6),
				new Point(x+6, y),
				new Point(x, y+6),
				new Point(x-6, y)
			}
		);

		if (expl != "") {
			objGraphics.DrawString(expl, bigFont, solidBlackBrush, x + 5, y, drawFormatNearCenter);
		}
	}

//	public void drawQuadrant(string stl, string str, string sll, string slr, string tl, string ctl, string tr, string ctr, string ll, string cll, string lr, string clr, string q1, string q2)
//	{
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[0])), 100, 70, 200, 200);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[1])), 300, 70, 200, 200);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[1])), 100, 270, 200, 200);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[2])), 300, 270, 200, 200);
//
//		objGraphics.DrawRectangle(blackPen, 100, 70, 200, 200);
//		objGraphics.DrawRectangle(blackPen, 300, 70, 200, 200);
//		objGraphics.DrawRectangle(blackPen, 100, 270, 200, 200);
//		objGraphics.DrawRectangle(blackPen, 300, 270, 200, 200);
//
//		objGraphics.TranslateTransform(0, h);
//		objGraphics.RotateTransform(-90F);
//		objGraphics.DrawString(q1, new Font("Arial", 10), solidBlackBrush, 210, 40, drawFormatCenter);
//		objGraphics.DrawString(slr, new Font("Arial", 11), solidBlackBrush, 110, 80, drawFormatCenter);
//		objGraphics.DrawString(sll, new Font("Arial", 11), solidBlackBrush, 310, 80, drawFormatCenter);
//		objGraphics.ResetTransform();
//
//		objGraphics.DrawString(tr, new Font("Arial", 20), solidBlackBrush, 400, 170, drawFormatCenter);
//		objGraphics.DrawString(lr, new Font("Arial", 20), solidBlackBrush, 400, 370, drawFormatCenter);
//		objGraphics.DrawString(tl, new Font("Arial", 20), solidBlackBrush, 200, 170, drawFormatCenter);
//		objGraphics.DrawString(ll, new Font("Arial", 20), solidBlackBrush, 200, 370, drawFormatCenter);
//
//		objGraphics.DrawString(ctr, new Font("Arial", 12), solidBlackBrush, 400, 220, drawFormatCenter);
//		objGraphics.DrawString(clr, new Font("Arial", 12), solidBlackBrush, 400, 420, drawFormatCenter);
//		objGraphics.DrawString(ctl, new Font("Arial", 12), solidBlackBrush, 200, 220, drawFormatCenter);
//		objGraphics.DrawString(cll, new Font("Arial", 12), solidBlackBrush, 200, 420, drawFormatCenter);
//
//		objGraphics.DrawString(q2, new Font("Arial", 10), solidBlackBrush, 300, 15, drawFormatCenter);
//		objGraphics.DrawString(stl, new Font("Arial", 11), solidBlackBrush, 200, 55, drawFormatCenter);
//		objGraphics.DrawString(str, new Font("Arial", 11), solidBlackBrush, 400, 55, drawFormatCenter);
//	}
//
//	public void drawNiner(
//		string stl,
//		string stc,
//		string str,
//		string sll,
//		string slc,
//		string slr,
//
//		string tl,
//		string ctl,
//
//		string tc,
//		string ctc,
//
//		string tr,
//		string ctr,
//
//
//		string cl,
//		string ccl,
//
//		string cc,
//		string ccc,
//
//		string cr,
//		string ccr,
//
//
//		string ll,
//		string cll,
//
//		string lc,
//		string clc,
//
//		string lr,
//		string clr,
//
//		string q1,
//		string q2,
//
//		bool grey
//	)
//	{
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[(grey ? 21 : 0)])), 100, 70, 133, 133);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[21])), 233, 70, 133, 133);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[(grey ? 21 : 1)])), 366, 70, 133, 133);
//
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[21])), 100, 203, 133, 133);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[21])), 233, 203, 133, 133);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[21])), 366, 203, 133, 133);
//
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[(grey ? 21 : 1)])), 100, 336, 133, 133);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[21])), 233, 336, 133, 133);
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#" + colors[(grey ? 21 : 2)])), 366, 336, 133, 133);
//
//		objGraphics.DrawRectangle(blackPen, 100, 70, 133, 133);
//		objGraphics.DrawRectangle(blackPen, 233, 70, 133, 133);
//		objGraphics.DrawRectangle(blackPen, 366, 70, 133, 133);
//		objGraphics.DrawRectangle(blackPen, 100, 203, 133, 133);
//		objGraphics.DrawRectangle(blackPen, 233, 203, 133, 133);
//		objGraphics.DrawRectangle(blackPen, 366, 203, 133, 133);
//		objGraphics.DrawRectangle(blackPen, 100, 336, 133, 133);
//		objGraphics.DrawRectangle(blackPen, 233, 336, 133, 133);
//		objGraphics.DrawRectangle(blackPen, 366, 336, 133, 133);
//
//		objGraphics.TranslateTransform(0, h);
//		objGraphics.RotateTransform(-90F);
//		objGraphics.DrawString(q1, new Font("Arial", 10), solidBlackBrush, 210, 40, drawFormatCenter);
//		objGraphics.DrawString(slr, new Font("Arial", 11), solidBlackBrush, 86, 80, drawFormatCenter);
//		objGraphics.DrawString(slc, new Font("Arial", 11), solidBlackBrush, 213, 80, drawFormatCenter);
//		objGraphics.DrawString(sll, new Font("Arial", 11), solidBlackBrush, 343, 80, drawFormatCenter);
//		objGraphics.ResetTransform();
//
//
//		objGraphics.DrawString(tr, new Font("Arial", 20), solidBlackBrush, 433, 137, drawFormatCenter);
//		objGraphics.DrawString(cr, new Font("Arial", 20), solidBlackBrush, 433, 270, drawFormatCenter);
//		objGraphics.DrawString(lr, new Font("Arial", 20), solidBlackBrush, 433, 403, drawFormatCenter);
//
//		objGraphics.DrawString(tc, new Font("Arial", 20), solidBlackBrush, 300, 137, drawFormatCenter);
//		objGraphics.DrawString(cc, new Font("Arial", 20), solidBlackBrush, 300, 270, drawFormatCenter);
//		objGraphics.DrawString(lc, new Font("Arial", 20), solidBlackBrush, 300, 403, drawFormatCenter);
//
//		objGraphics.DrawString(tl, new Font("Arial", 20), solidBlackBrush, 166, 137, drawFormatCenter);
//		objGraphics.DrawString(cl, new Font("Arial", 20), solidBlackBrush, 166, 270, drawFormatCenter);
//		objGraphics.DrawString(ll, new Font("Arial", 20), solidBlackBrush, 166, 403, drawFormatCenter);
//
//
//		objGraphics.DrawString(ctr, new Font("Arial", 20), solidBlackBrush, 433, 167, drawFormatCenter);
//		objGraphics.DrawString(ccr, new Font("Arial", 20), solidBlackBrush, 433, 300, drawFormatCenter);
//		objGraphics.DrawString(clr, new Font("Arial", 20), solidBlackBrush, 433, 433, drawFormatCenter);
//
//		objGraphics.DrawString(ctc, new Font("Arial", 20), solidBlackBrush, 300, 167, drawFormatCenter);
//		objGraphics.DrawString(ccc, new Font("Arial", 20), solidBlackBrush, 300, 300, drawFormatCenter);
//		objGraphics.DrawString(clc, new Font("Arial", 20), solidBlackBrush, 300, 433, drawFormatCenter);
//
//		objGraphics.DrawString(ctl, new Font("Arial", 20), solidBlackBrush, 166, 167, drawFormatCenter);
//		objGraphics.DrawString(ccl, new Font("Arial", 20), solidBlackBrush, 166, 300, drawFormatCenter);
//		objGraphics.DrawString(cll, new Font("Arial", 20), solidBlackBrush, 166, 433, drawFormatCenter);
//
//		objGraphics.DrawString(q2, new Font("Arial", 10), solidBlackBrush, 300, 15, drawFormatCenter);
//		objGraphics.DrawString(stl, new Font("Arial", 11), solidBlackBrush, 166, 55, drawFormatCenter);
//		objGraphics.DrawString(stc, new Font("Arial", 11), solidBlackBrush, 300, 55, drawFormatCenter);
//		objGraphics.DrawString(str, new Font("Arial", 11), solidBlackBrush, 433, 55, drawFormatCenter);
//	}
//
//	public void drawNSTDvalVert(int n1, float std1, int n2, float std2, int cx, bool show2) // Not used
//	{
//		string s = "\nn=" + n1 + (show2 ? "/" + n2 : "") + " stdev=" + Math.Round((Convert.ToDouble(std1)), 2) + (show2 ? "/" + Math.Round((Convert.ToDouble(std2)), 2) : "");
//		objGraphics.TranslateTransform(0, h);
//		objGraphics.RotateTransform(90F);
//		objGraphics.DrawString(s, smallFont, solidBlackBrush, topSpacing + Convert.ToInt32(maxH) + 15 - h, -leftSpacing - cx * steping - 10, drawFormatNear);
//		objGraphics.ResetTransform();
//	}
//
//	public void drawLine(int color, int x1, int y1, int x2, int y2)
//	{
//		drawLine(color, x1, y1, x2, y2, 2);
//	}
//
//	public void drawGreyArea(int x1, float y1, int x2, float y2)
//	{
//		objGraphics.DrawPolygon(new Pen(ColorTranslator.FromHtml("#DCDCDC")), new Point[] { new Point(leftSpacing + x1 * steping + 1, topSpacing + Convert.ToInt32(maxH - (y1 - minVal) / (maxVal - minVal) * maxH)), new Point(leftSpacing + x2 * steping, topSpacing + Convert.ToInt32(maxH - (y2 - minVal) / (maxVal - minVal) * maxH)), new Point(leftSpacing + x2 * steping, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH)), new Point(leftSpacing + x1 * steping + 1, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH)) });
//		objGraphics.FillPolygon(new SolidBrush(ColorTranslator.FromHtml("#DCDCDC")), new Point[] { new Point(leftSpacing + x1 * steping + 1, topSpacing + Convert.ToInt32(maxH - (y1 - minVal) / (maxVal - minVal) * maxH)), new Point(leftSpacing + x2 * steping, topSpacing + Convert.ToInt32(maxH - (y2 - minVal) / (maxVal - minVal) * maxH)), new Point(leftSpacing + x2 * steping, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH)), new Point(leftSpacing + x1 * steping + 1, topSpacing + Convert.ToInt32(maxH - baseline / (maxVal - minVal) * maxH)) });
//	}
//
//	public void drawOutlinesRight()
//	{
//		drawOutlinesRight(0, 100);
//	}
//
//	public void drawOutlinesRight(int from, int to)
//	{
//		float mMaxH = maxH * to / 100 - maxH * from / 100;
//		float dMaxH = maxH - maxH * to / 100;
//
//		for (int i = 0; i < vertLines; i++) {
//			objGraphics.DrawString(Convert.ToString(Math.Round((maxVal - minVal) / (vertLines - 1) * i + minVal, 2)), bigFont, solidBlackBrush, w - innerRightSpacing + 5, topSpacing + dMaxH + Convert.ToInt32(mMaxH - (maxVal - minVal) / (vertLines - 1) * i / (maxVal - minVal) * mMaxH), drawFormatNearCenter);
//		}
//	}
//
//	public void drawN()
//	{
//		objGraphics.DrawString(
//			@"ø
	//n",
//			smallFont,
//			solidBlackBrush,
//			2,
//			h - 25,
//			drawFormatNear
//		);
//	}
//
//
//	public void drawNval(int n1, float avg1, int n2, float avg2, int cx, bool two)
//	{
//		int avg1dec = (avg1 > 100f ? 0 : (avg1 > 25f ? 1 : 2));
//
//		if (two) {
//			int avg2dec = (avg2 > 100f ? 0 : (avg2 > 25f ? 1 : 2));
//
//			objGraphics.DrawString("" + Math.Round((Convert.ToDouble(avg1)), avg1dec) + "\n" + n1, smallFont, GetSolidBrush(colors[0]), leftSpacing + cx * steping, h - 25, drawFormatFar);
//			objGraphics.DrawString("  /\n  /", smallFont, solidBlackBrush, leftSpacing + cx * steping, h - 25, drawFormat);
//			objGraphics.DrawString("  " + Math.Round((Convert.ToDouble(avg2)), avg2dec) + "\n   " + n2, smallFont, GetSolidBrush(colors[1]), leftSpacing + cx * steping, h - 25, drawFormatNear);
//		} else {
//			objGraphics.DrawString("" + Math.Round((Convert.ToDouble(avg1)), avg1dec) + "\n" + n1, smallFont, solidBlackBrush, leftSpacing + cx * steping, h - 25, drawFormat);
//		}
//	}
//
//	public void drawNSTD(int dH)
//	{
//		objGraphics.DrawString(
//			@"ø
	//n
	//std",
//			smallFont,
//			solidBlackBrush,
//			2,
//			h - dH,
//			drawFormatNear
//		);
//	}
//
//	public void drawNSTDval(int n1, float std1, float avg1, int n2, float std2, float avg2, int cx, bool two, int dH)
//	{
//		int std1dec = (std1 > 100f ? 0 : (std1 > 25f ? 1 : 2));
//		int avg1dec = (avg1 > 100f ? 0 : (avg1 > 25f ? 1 : 2));
//
//		if (two) {
//			int std2dec = (std2 > 100f ? 0 : (std2 > 25f ? 1 : 2));
//			int avg2dec = (avg2 > 100f ? 0 : (avg2 > 25f ? 1 : 2));
//
//			objGraphics.DrawString("" + Math.Round((Convert.ToDouble(avg1)), avg1dec) + "\n" + n1 + "\n" + Math.Round((Convert.ToDouble(std1)), std1dec), smallFont, GetSolidBrush(colors[0]), leftSpacing + cx * steping, h - dH, drawFormatFar);
//			objGraphics.DrawString("  /\n  /\n  /", smallFont, solidBlackBrush, leftSpacing + cx * steping, h - dH, drawFormat);
//			objGraphics.DrawString("  " + Math.Round((Convert.ToDouble(avg2)), avg2dec) + "\n   " + n2 + "\n  " + Math.Round((Convert.ToDouble(std2)), std2dec), smallFont, GetSolidBrush(colors[1]), leftSpacing + cx * steping, h - dH, drawFormatNear);
//		} else {
//			objGraphics.DrawString("" + Math.Round((Convert.ToDouble(avg1)), avg1dec) + "\n" + n1 + "\n" + Math.Round((Convert.ToDouble(std1)), std1dec), smallFont, solidBlackBrush, leftSpacing + cx * steping, h - dH, drawFormat);
//		}
//	}
//
//	public void drawLeftHeader(string s)
//	{
//		objGraphics.DrawString(s, bigFont, solidBlackBrush, 20, 20, drawFormatNearCenter);
//	}
//
//	public void drawHeader(string s)
//	{
//		objGraphics.DrawString(s, bigFont, solidBlackBrush, w / 2, topSpacing - 25, drawFormat);
//	}
//
//	public void drawCenterHeader(string s)
//	{
//		objGraphics.DrawString(s, bigFont, solidBlackBrush, w / 2, topSpacing - 25, drawFormatCenter);
//	}
//
//	public void drawRightHeader(string s)
//	{
//		objGraphics.DrawString(s, bigFont, solidBlackBrush, w - 5, 14, drawFormatFarNear);
//	}
//
//	public void drawBottomStringExpl(string s, bool left, bool right)
//	{
//		if (left)
//			objGraphics.DrawString(s, bigFont, solidBlackBrush, 0, topSpacing + Convert.ToInt32(maxH) + 15, drawFormatNear);
//		if (right)
//			objGraphics.DrawString(s, bigFont, solidBlackBrush, w, topSpacing + Convert.ToInt32(maxH) + 15, drawFormatFar);
//	}
//
//	public void drawBar(int color, int i, float v, bool writeN, bool percent)
//	{
//		drawBar(color, i, v, steping, barW, 1, 0, 100, writeN, percent);
//	}
//
//	public void drawBar(int color, int i, float v, int occupy)
//	{
//		drawBar(color, i, v, steping, barW, 1, 0, occupy, false);
//	}
//
//	public void drawBar(int color, int i, float v, int barDivision, int align, int occupy)
//	{
//		drawBar(color, i, v, steping, barW, barDivision, align, occupy, false);
//	}
//
//	public void drawBar(int color, int i, float v, int s, int b, int barDivision, int align, int occupy)
//	{
//		drawBar(color, i, v, s, b, barDivision, align, occupy, false);
//	}
//
//	public void drawReferenceLine(int v)
//	{
//		drawReferenceLine(v, "");
//	}
//
//	public void drawReferenceLineExpl(int x, int y, string expl)
//	{
//		objGraphics.DrawLine(new Pen(ColorTranslator.FromHtml("#000000"), 2), x, y, x + 20, y);
//		objGraphics.DrawString(expl, bigFont, solidBlackBrush, x + 25, y, drawFormatNearCenter);
//	}
//
//	public void drawMultiStd(int color, int i, float val, float std, int s, int b, int barDivision, int align, int occupy, bool writeN, bool percent)
//	{
//		//incomplete
//
//		float mMaxH = maxH * occupy / 100;
//		float dMaxH = maxH - mMaxH;
//		int atH = Convert.ToInt32(mMaxH - (Convert.ToDouble(val) - minVal) / (maxVal - minVal) * mMaxH);
//
//		int x = 1 + i * s - b / 2 + b / barDivision * align + (b / barDivision - 1) / 2;
//
//		drawLine(color, x - 10, Convert.ToInt32(maxH - ((val - std) - minVal) / (maxVal - minVal) * maxH), x + 10, Convert.ToInt32(maxH - ((val - std) - minVal) / (maxVal - minVal) * maxH), 1);
//		drawLine(color, x, Convert.ToInt32(maxH - ((val - std) - minVal) / (maxVal - minVal) * maxH), x, Convert.ToInt32(maxH - ((val + std) - minVal) / (maxVal - minVal) * maxH), 1);
//		drawLine(color, x - 10, Convert.ToInt32(maxH - ((val + std) - minVal) / (maxVal - minVal) * maxH), x + 10, Convert.ToInt32(maxH - ((val + std) - minVal) / (maxVal - minVal) * maxH), 1);
//
//		if (writeN && b / barDivision > 30) {
//			objGraphics.DrawString(val.ToString() + (percent ? "%" : ""), smallFont, solidBlackBrush, leftSpacing + x, topSpacing + maxH - 15, drawFormatCenter);
//		}
//	}
//
//	public void drawMultiBar(int color, int i, float v, int s, int b, int barDivision, int align, int occupy, bool writeN, bool percent)
//	{
//		float mMaxH = maxH * occupy / 100;
//		float dMaxH = maxH - mMaxH;
//		int atH = Convert.ToInt32(mMaxH - (Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH);
//
//		int x = leftSpacing + i * s - b / 2 + b / barDivision * align;
//		objGraphics.DrawRectangle(blackPen, x, topSpacing + dMaxH + atH, b / barDivision, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH));
//		objGraphics.FillRectangle(GetSolidBrush(colors[color]), x + 1, topSpacing + 1 + dMaxH + atH, b / barDivision - 1, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH) - 1);
//		if (barDivision <= 2) {
//			objGraphics.FillRectangle(shadowBrush, x + b / barDivision + 1, topSpacing + 2 + dMaxH + atH, 2, Convert.ToInt32((Convert.ToDouble(v) - minVal) / (maxVal - minVal) * mMaxH) - 2);
//		}
//		if (writeN && b / barDivision > 30) {
//			objGraphics.DrawString(v.ToString() + (percent ? "%" : ""), smallFont, solidBlackBrush, x + (b / barDivision - 1) / 2, topSpacing + 2 + dMaxH + (atH > mMaxH / 5 ? atH - 15 : atH + 15), drawFormatCenter);
//		}
//	}
//
//	public void drawExplBox(string s, int height)
//	{
//		objGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), leftSpacing, h - height - 5, w - leftSpacing - rightSpacing, height);
//		objGraphics.DrawRectangle(blackPen, leftSpacing, h - height - 5, w - leftSpacing - rightSpacing, height);
//		objGraphics.DrawString(s, bigFont, solidBlackBrush, leftSpacing + 10, h - height + 8, drawFormatNearCenter);
//	}
//
//	public void drawExplBoxExpl(string s, int color, int cx, int height, int cols)
//	{
//		objGraphics.FillRectangle(GetSolidBrush(colors[color]), leftSpacing + 10 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 + 18, 10, 10);
//		objGraphics.DrawRectangle(blackPen, leftSpacing + 10 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 + 18, 10, 10);
//		objGraphics.DrawString(s, bigFont, solidBlackBrush, leftSpacing + 25 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 + 23, drawFormatNearCenter);
//	}
//
//	public void drawExplBoxExpl2Rows(string s, int color, int cx, int height, int cols)
//	{
//		objGraphics.FillRectangle(GetSolidBrush(colors[color]), leftSpacing + 10 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 * 2 + 18, 10, 10);
//		objGraphics.DrawRectangle(blackPen, leftSpacing + 10 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 * 2 + 18, 10, 10);
//		objGraphics.DrawString(s, bigFont, solidBlackBrush, leftSpacing + 25 + ((w - leftSpacing - rightSpacing) / 2 * (cx % cols)), h - height + (((cx + (cx % cols)) / cols) - (cx % cols)) * 14 * 2 + 17, drawFormatNear);
//	}
}
