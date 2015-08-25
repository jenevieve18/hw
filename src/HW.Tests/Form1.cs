using System;
using System.Drawing;
using System.Windows.Forms;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Tests
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			var r = new SqlExerciseRepository();
			r.UpdateVariant(ConvertHelper.ToInt32(textBox1.Text), richTextBox1.Text);
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			var x = "";
			foreach (var s in richTextBox2.Text.Split('\n')) {
				if (s != "") {
					var ss = s.Replace(";", "").Trim().Split('=');
					x += ss[1] + " = " + ss[0] + ".ToString();\n";
				}
			}
			richTextBox3.Text = x;
		}
	}
}
