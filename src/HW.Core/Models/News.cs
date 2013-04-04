//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	public class News : BaseModel
	{
		public NewsCategory Category { get; set; }
		public string Headline { get; set; }
		public DateTime Date { get; set; }
		public string Teaser { get; set; }
		public string Body { get; set; }
		public NewsImage Image { get; set; }
		public NewsImage TeaserImage { get; set; }
		public DateTime Published { get; set; }
		public DateTime Deleted { get; set; }
		public string Link { get; set; }
		public string LinkText { get; set; }
		public string ShortHeadline { get; set; }
	}
	
	public class NewsCategory : BaseModel
	{
		public string Name { get; set; }
		public string ShortName { get; set; }
	}
	
	public class NewsCategoryLanguage : BaseModel
	{
		public NewsCategory Category { get; set; }
		public Language Language { get; set; }
	}
	
	public class NewsChannel : BaseModel
	{
		public string Name { get; set; }
	}
	
	public class NewsImage : BaseModel
	{
		public string Description { get; set; }
		public string FileName { get; set; }
		public string Alternative { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Size { get; set; }
	}
}
