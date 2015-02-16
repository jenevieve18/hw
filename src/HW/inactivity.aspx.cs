using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.FromHW;

namespace HW
{
    public partial class inactivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Db.checkAndLogin();
        }
    }
}