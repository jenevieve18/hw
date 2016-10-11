using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Manager : BaseModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
	}
	
	public class ManagerFunction : BaseModel
	{
		public ManagerFunction()
		{
		}
		
		public ManagerFunction(IList<ManagerFunctionLang> languages)
		{
			Languages = languages;
		}
		
		public string URL { get; set; }

		public IList<ManagerFunctionLang> Languages { get; set; }
		
		public ManagerFunctionLang SelectedLanguage {
			get { return Languages[0]; }
		}
        
        public bool HasLanguages
        {
            get {
                return Languages != null && Languages.Count > 0;
            }
        }
		
		public const int Organization = 1;
		public const int Statistics = 2;
		public const int Messages = 3;
		public const int Managers = 4;
		public const int TEST = 6;
		public const int Exercises = 7;
		public const int Reminders = 8;
		public const int MyExercises = 9;
		
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
	}
	
	public class ManagerFunctionLang : BaseModel
	{
		public string Function { get; set; }
		public string URL { get; set; }
		public string Expl { get; set; }
		public Language Language { get; set; }
		public ManagerFunction ManagerFunction { get; set; }
		
		public override string ToString()
		{
			return Function;
		}
	}
}
