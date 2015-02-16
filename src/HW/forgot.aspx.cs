using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.FromHW;

namespace HW
{
    public partial class forgot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Db.checkAndLogin();

            if (Convert.ToInt32(HttpContext.Current.Session["UserID"]) == 0)
            {
                if (HttpContext.Current.Request.Form["ForgotEmail"] != null && HttpContext.Current.Request.Form["ForgotEmail"].ToString() != "")
                {
                    if (Db.isEmail(HttpContext.Current.Request.Form["ForgotEmail"].ToString()))
                    {
                        Db.sendPasswordReminder(HttpContext.Current.Request.Form["ForgotEmail"].ToString(), 0);

                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1:
                                sent.Text = "<BR>Om denna e-postadress fanns registrerad så får du ett mail inom ett par minuter.";
                                break;
                            case 2:
                                sent.Text = "<BR>You will receive an email shortly, if the email address was in our records.";
                                break;
                        }
                    }
                    else
                    {
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1:
                                sent.Text = "<BR>Felaktig e-postadress.";
                                break;
                            case 2:
                                sent.Text = "<BR>Incorrect email address.";
                                break;
                        }
                    }
                }
                else
                {
                    sent.Text = "";
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("home.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
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