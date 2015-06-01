using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExerciseUpdater
{
	public partial class EditorForm : Form
	{
		public EditorForm()
		{
			InitializeComponent();
		}
		
		void OpenToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (var d = new OpenFileDialog()) {
				if (d.ShowDialog() == DialogResult.OK) {
					tabControl1.TabPages.Add(new EditorTab(d.FileName));
				}
			}
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}
