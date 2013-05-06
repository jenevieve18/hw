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
		public virtual int Type { get; set; }
		public virtual int Placement { get; set; }
		public virtual string Internal { get; set; }
		public virtual int Width { get; set; }
		public virtual int Height { get; set; }
		public virtual OptionContainer Container { get; set; }
		public virtual string BackgroundColor { get; set; }
		public virtual int InnerWidth { get; set; }
		public virtual IList<OptionComponents> Components { get; set; }
		
		public virtual OptionComponentLanguage CurrentComponent { get; set; }
	}
	
	public class OptionComponents : BaseModel
	{
		public virtual OptionComponent Component { get; set; }
	}
	
	public class OptionContainer : BaseModel
	{
		public virtual string Container { get; set; }
	}
	
	public class OptionComponent : BaseModel
	{
		public virtual int ExportValue { get; set; }
		public virtual string Internal { get; set; }
		public virtual IList<OptionComponentLanguage> Languages { get; set; }
		public virtual OptionComponentContainer Container { get; set; }
		
		public virtual OptionComponentLanguage CurrentLanguage { get; set; }
	}
	
	public class OptionComponentContainer : BaseModel
	{
		public virtual string Container { get; set; }
	}
	
	public class OptionComponentLanguage : BaseModel
	{
		public virtual Language Language { get; set; }
		public virtual OptionComponent Component { get; set; }
		public virtual string Text { get; set; }
		public virtual string TextJapaneseUnicode { get; set; }
	}
}
