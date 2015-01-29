using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HW.Grp
{
    public partial class sql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.ToUpper().Contains("DELETE") && !TextBox1.Text.ToUpper().Contains("WHERE"))
            {
                Label1.Text = "Please provide where clause for delete statement.";
            }
            else if (TextBox1.Text.ToUpper().Contains("DROP"))
            {
                Label1.Text = "Please don't use drop statement in this utility page.";
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["healthWatchSqlConnection"]);
                    SqlDataAdapter da = new SqlDataAdapter(TextBox1.Text, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
            }
        }
    }
}