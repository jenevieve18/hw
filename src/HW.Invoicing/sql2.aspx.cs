using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HW.Invoicing
{
    public partial class sql2 : System.Web.UI.Page
    {
        protected string message;

        bool InvalidToExecute(string pin)
        {
            return pin != "Start123!!!";
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            if (InvalidToExecute(textBoxPIN.Text))
            {
                message = "Invalid PIN. Please check and try again!";
                gridViewResult.DataSource = null;
                gridViewResult.DataBind();
            }
            else
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
}