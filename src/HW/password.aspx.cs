using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.FromHW;

namespace HW
{
    public partial class password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (HttpContext.Current.Request.QueryString["K"] != null && HttpContext.Current.Request.QueryString["K"].ToString().Length >= 9)
            {
                int userID = 0;
                try
                {
                    SqlDataReader rs = Db.rs("SELECT u.UserID FROM [User] u " +
                        "WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),8) = '" + HttpContext.Current.Request.QueryString["K"].ToString().Substring(0, 8).Replace("'", "") + "' " +
                        "AND u.UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["K"].ToString().Substring(8)));
                    if (rs.Read())
                    {
                        userID = rs.GetInt32(0);
                    }
                    rs.Close();

                    if (userID == 0)
                    {
                        throw (new Exception());
                    }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Redirect("forgot.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }

                if (HttpContext.Current.Request.Form["ForgotPassword"] != null)
                {
                    if (HttpContext.Current.Request.Form["ForgotPassword"].ToString().Length >= 5)
                    {
                        if (userID != 0)
                        {
                            Db.exec("UPDATE [User] SET Password = '" + Db.HashMD5(HttpContext.Current.Request.Form["ForgotPassword"].ToString()) + "', " +
                                "UserKey = NEWID() " +
                                "WHERE UserID = " + userID);
                        }

                        if (HttpContext.Current.Request.QueryString["NL"] != null)
                        {
                            HttpContext.Current.Response.Redirect("register.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                        }
                        else
                        {
                            Db.checkAndLogin(userID);
                        }
                    }
                    else
                    {
                        error.Text = "Lösenordet måste vara minst 5 tecken.";
                    }
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