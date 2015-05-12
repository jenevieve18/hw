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
	}
}
