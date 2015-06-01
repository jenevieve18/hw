using System;
using System.Windows.Forms;
using ICSharpCode.TextEditor;

namespace ExerciseUpdater
{
	public class EditorTab : TabPage
	{
		public EditorTab(string file)
		{
			var e = new TextEditorControl();
			e.Dock = DockStyle.Fill;
			
			e.LoadFile(file);
			Controls.Add(e);
		}
	}
}
