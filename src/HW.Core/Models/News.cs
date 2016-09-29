using System;
using System.Collections.Generic;

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
		public int OnlyDirectFromFeed { get; set; }
		
		public IList<News> SomeNews { get; set; }
		public IList<News> News { get; set; }
	}
	
	public class NewsCategoryLanguage : BaseModel
	{
		public NewsCategory Category { get; set; }
		public Language Language { get; set; }
	}
	
	public class NewsChannel : BaseModel
	{
		public NewsSource Source { get; set; }
		public string Feed { get; set; }
		public Language Language { get; set; }
		public DateTime? Pause { get; set; }
		public NewsCategory Category { get; set; }
		public string Internal { get; set; }
		
		public IList<NewsRSS> UndeletedRSS { get; set; }
		public IList<NewsRSS> DeletedRSS { get; set; }
	}
	
	public class NewsRSS : BaseModel
	{
		public NewsChannel Channel { get; set; }
		public string Link { get; set; }
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
	
	public class NewsSource : BaseModel
	{
		public string Source { get; set; }
		public string SourceShort { get; set; }
		public int Favourite { get; set; }
	}
	
	public class AdminNews : BaseModel
	{
		public DateTime? Date { get; set; }
		public string News { get; set; }
	}
}
