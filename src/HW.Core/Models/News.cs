using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class News : BaseModel
	{
		public virtual NewsCategory Category { get; set; }
		public virtual string Headline { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string Teaser { get; set; }
		public virtual string Body { get; set; }
		public virtual NewsImage Image { get; set; }
		public virtual NewsImage TeaserImage { get; set; }
		public virtual DateTime Published { get; set; }
		public virtual DateTime Deleted { get; set; }
		public virtual string Link { get; set; }
		public virtual string LinkText { get; set; }
		public virtual string ShortHeadline { get; set; }
	}
	
	public class NewsCategory : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual string ShortName { get; set; }
		public virtual int OnlyDirectFromFeed { get; set; }
		
		public virtual IList<News> SomeNews { get; set; }
		public virtual IList<News> News { get; set; }
	}
	
	public class NewsCategoryLanguage : BaseModel
	{
		public virtual NewsCategory Category { get; set; }
		public virtual Language Language { get; set; }
	}
	
	public class NewsChannel : BaseModel
	{
		public virtual NewsSource Source { get; set; }
		public virtual string Feed { get; set; }
		public virtual Language Language { get; set; }
		public virtual DateTime? Pause { get; set; }
		public virtual NewsCategory Category { get; set; }
		public virtual string Internal { get; set; }
		
		public virtual IList<NewsRSS> UndeletedRSS { get; set; }
		public virtual IList<NewsRSS> DeletedRSS { get; set; }
	}
	
	public class NewsRSS : BaseModel
	{
		public virtual NewsChannel Channel { get; set; }
		public virtual string Link { get; set; }
	}
	
	public class NewsImage : BaseModel
	{
		public virtual string Description { get; set; }
		public virtual string FileName { get; set; }
		public virtual string Alternative { get; set; }
		public virtual int Width { get; set; }
		public virtual int Height { get; set; }
		public virtual int Size { get; set; }
	}
	
	public class NewsSource : BaseModel
	{
		public virtual string Source { get; set; }
		public virtual string SourceShort { get; set; }
		public virtual int Favourite { get; set; }
	}
}
