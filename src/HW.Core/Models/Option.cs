//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Option : BaseModel
	{
		public int Type { get; set; }
		public int Placement { get; set; }
		public string Internal { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public OptionContainer Container { get; set; }
		public string BackgroundColor { get; set; }
		public int InnerWidth { get; set; }
		public IList<OptionComponents> Components { get; set; }
		
		public OptionComponentLanguage CurrentComponent { get; set; }
	}
	
	public class OptionComponents : BaseModel
	{
		public OptionComponent Component { get; set; }
	}
	
	public class OptionContainer : BaseModel
	{
		public string Container { get; set; }
	}
	
	public class OptionComponent : BaseModel
	{
		public int ExportValue { get; set; }
		public string Internal { get; set; }
		public IList<OptionComponentLanguage> Languages { get; set; }
		public OptionComponentContainer Container { get; set; }
		
		public OptionComponentLanguage CurrentLanguage { get; set; }
	}
	
	public class OptionComponentContainer : BaseModel
	{
		public string Container { get; set; }
	}
	
	public class OptionComponentLanguage : BaseModel
	{
		public Language Language { get; set; }
		public OptionComponent Component { get; set; }
		public string Text { get; set; }
		public string TextJapaneseUnicode { get; set; }
	}
}
