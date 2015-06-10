using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using ICSharpCode.TextEditor.Document;

namespace ExerciseUpdater
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			
			textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("HTML");
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			var r = new SqlExerciseRepository();
			r.UpdateVariant(ConvertHelper.ToInt32(textBox1.Text), textEditorControl1.Text);
			new Thread(Saving).Start();
		}
		
		void Saving()
		{
			toolStripStatusLabel1.Text = "Saving...";
			Thread.Sleep(2000);
			toolStripStatusLabel1.Text = "Saved!";
			Thread.Sleep(1000);
			toolStripStatusLabel1.Text = "Ready";
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
