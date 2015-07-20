using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HW.Adm
{
    public partial class sql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void buttonExecuteClick(object sender, EventArgs e)
        {
            try
            {
                var con = new SqlConnection(ConfigurationManager.AppSettings["SqlConnection"]);
                var da = new SqlDataAdapter(textBoxSql.Text, con);
                var ds = new DataSet();
                da.Fill(ds);

                gridViewResult.DataSource = ds;
                gridViewResult.DataBind();
            }
            catch (Exception ex)
            {
                labelMessage.Text = ex.Message;
            }
        }
    }
}