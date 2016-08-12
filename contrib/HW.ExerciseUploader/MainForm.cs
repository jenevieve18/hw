/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 8/10/2016
 * Time: 2:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HW.ExerciseUploader
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			
			ListDirectory(treeView1, @"D:\ian\projects\grp-exercises\src");
		}
		
		void ListDirectory(TreeView treeView, string path)
		{
			treeView.Nodes.Clear();

			var stack = new Stack<TreeNode>();
			var rootDirectory = new DirectoryInfo(path);
			var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
			stack.Push(node);

			while (stack.Count > 0)
			{
				var currentNode = stack.Pop();
				var directoryInfo = (DirectoryInfo)currentNode.Tag;
				foreach (var directory in directoryInfo.GetDirectories())
				{
					var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
					currentNode.Nodes.Add(childDirectoryNode);
					stack.Push(childDirectoryNode);
				}
				foreach (var file in directoryInfo.GetFiles()) {
					currentNode.Nodes.Add(new TreeNode(file.Name));
				}
			}

			treeView.Nodes.Add(node);
		}
	}
}
