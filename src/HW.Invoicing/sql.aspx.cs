using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class sql : System.Web.UI.Page
    {
        protected string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            //HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["invoicing"].ConnectionString);
                SqlDataAdapter da = new SqlDataAdapter(textBoxSqlStatement.Text, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    gridViewResult.DataSource = ds;
                    gridViewResult.DataBind();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }
    }
}