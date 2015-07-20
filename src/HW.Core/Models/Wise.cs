using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
    public class FAQ : BaseModel
    {
        public string Name { get; set; }
        public IList<FAQLanguage> Languages { get; set; }

        public FAQ()
        {
            Languages = new List<FAQLanguage>();
        }

        public void AddLang(FAQLanguage l)
        {
            l.FAQ = this;
            Languages.Add(l);
        }

        public FAQLanguage FindLanguage(int langId)
        {
            foreach (var x in Languages)
            {
                if (x.Language.Id == langId)
                {
                    return x;
                }
            }
            return null;
        }
    }

    public class FAQLanguage : BaseModel
    {
        public FAQ FAQ { get; set; }
        public Language Language { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

	public class Wise : BaseModel
	{
		public virtual DateTime? LastShown { get; set; }
		public virtual IList<WiseLanguage> Languages { get; set; }
	}
	
	public class WiseLanguage : BaseModel
	{
		public virtual Wise Wise { get; set; }
		public virtual Language Language { get; set; }
		public virtual string WiseName { get; set; }
		public virtual string WiseBy { get; set; }
	}
	
	public struct WordsOfWisdom
	{
		public string words;
		public string author;
	}
}
