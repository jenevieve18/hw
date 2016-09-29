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
	
	    public FAQLanguage FindLanguage(int langID)
	    {
	        foreach (var l in Languages) {
	            if (l.Language.Id == langID) {
	                return l;
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
}
