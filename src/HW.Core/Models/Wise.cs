using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Wise : BaseModel
	{
		public DateTime? LastShown { get; set; }
		public List<WiseLanguage> Languages { get; set; }
        public string WiseName {
            get {
                if (HasLanguages) {
                    return Languages[0].WiseName;
                }
                return "";
            }
        }
        
        public WiseLanguage FindLanguage(int langID)
        {
            foreach (var l in Languages) {
                if (l.Language.Id == langID) {
                    return l;
                }
            }
            return null;
        }
        
        public bool HasLanguages {
            get {
                return Languages != null && Languages.Count > 0;
            }
        }
	}
	
	public class WiseLanguage : BaseModel
	{
		public Wise Wise { get; set; }
		public Language Language { get; set; }
		public string WiseName { get; set; }
		public string WiseBy { get; set; }
	}
	
	public struct WordsOfWisdom
	{
		public string words;
		public string author;
	}
}
