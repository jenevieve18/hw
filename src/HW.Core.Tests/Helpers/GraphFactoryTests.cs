﻿/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class GraphFactoryTests
	{
		Form f;
		PictureBox p;
		ExtendedGraph g;
		
		[SetUp]
		public void Setup()
		{
			f = new Form();
			f.Size = new Size(1000, 650);
			f.KeyDown += delegate(object sender, KeyEventArgs e) {
				if (e.KeyCode == Keys.Escape) {
					f.Close();
				}
			};
			
			p = new PictureBox();
			p.Dock = DockStyle.Fill;
		}
		
		[Test]
		public void TestMethod()
		{
		}
		
		[TearDown]
		public void Teardown()
		{
			p.Image = g.objBitmap;
			f.Controls.Add(p);
//			f.ShowDialog();
		}
	}
}