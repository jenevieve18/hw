using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm2
{
    public partial class OptionEdit : System.Web.UI.Page
    {
    	protected Option option;
    	QuestionService s = new QuestionService();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	option = s.ReadOption(ConvertHelper.ToInt32(Request.QueryString["OptionID"]));
        }
    }
}