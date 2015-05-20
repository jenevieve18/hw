using System;
using System.IO;

namespace HW.Core.Helpers
{
	public class FileHelper
	{
		public FileHelper()
		{
		}
		
		public static void CreateDirectory(string dir)
		{
			if (!Directory.Exists(dir)) {
				Directory.CreateDirectory(dir);
			}
		}
		
		public static string SanitizeFileName(string fileName)
		{
			return fileName.Replace("?", "");
		}
	}
}
