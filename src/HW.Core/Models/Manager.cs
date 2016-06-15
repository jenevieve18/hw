using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Manager : BaseModel
	{
		public virtual string Email { get; set; }
		public virtual string Password { get; set; }
		public virtual string Name { get; set; }
		public virtual string Phone { get; set; }
	}
	
	public class ManagerFunction : BaseModel
	{
//		public virtual string Function { get; set; }
//		public virtual string Expl { get; set; }
		public virtual string URL { get; set; }
		public virtual IList<ManagerFunctionLang> Languages { get; set; }
		public virtual ManagerFunctionLang SelectedLanguage {
			get { return Languages[0]; }
		}
		
		public const int Organization = 1;
		public const int Statistics = 2;
		public const int Messages = 3;
		public const int Managers = 4;
		public const int TEST = 6;
		public const int Exercises = 7;
		public const int Reminders = 8;
		
		public ManagerFunction()
		{
		}
		
		public ManagerFunction(IList<ManagerFunctionLang> languages)
		{
			Languages = languages;
		}
		
        public ManagerFunctionLang FindLanguage(int langID)
        {
            foreach (var l in Languages)
            {
                if (l.Language.Id == langID)
                {
                    return l;
                }
            }
            return null;
        }
        
        public bool HasLanguages
        {
            get
            {
                return Languages != null && Languages.Count > 0;
            }
        }
	}
	
	public class ManagerFunctionLang : BaseModel
	{
		public virtual string Function { get; set; }
		public virtual string URL { get; set; }
		public virtual string Expl { get; set; }
		public Language Language { get; set; }
		public ManagerFunction ManagerFunction { get; set; }
		
		public override string ToString()
		{
			return Function;
		}
	}
}
