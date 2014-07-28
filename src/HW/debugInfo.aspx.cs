using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW
{
    public partial class debugInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            foreach (string s in HttpContext.Current.Session.Keys)
                HttpContext.Current.Response.Write(s + " = " + HttpContext.Current.Session[s] + "<br/>");

            foreach (string s in HttpContext.Current.Request.ServerVariables.Keys)
                HttpContext.Current.Response.Write(s + " = " + HttpContext.Current.Request.ServerVariables[s] + "<br/>");
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}