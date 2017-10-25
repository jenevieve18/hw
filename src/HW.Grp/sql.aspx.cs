﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.Grp
{
    public partial class sql : System.Web.UI.Page
    {
        SqlConnection con = null;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        bool InvalidToExecute(string pin)
        {
            return (
                    textBoxSql.Text.ToUpper().Contains("DELETE") &&
                    !textBoxSql.Text.ToUpper().Contains("WHERE")
                ) ||
                (
                    textBoxSql.Text.ToUpper().Contains("UPDATE") &&
                    !textBoxSql.Text.ToUpper().Contains("WHERE")
                ) ||
                (pin != "Start123!!!")
                ;
        }

        protected void buttonExecute_Click(object sender, EventArgs e)
        {
            if (InvalidToExecute(textBoxPIN.Text))
            {
                labelMessage.Text = "Invalid statement. If it's an UPDATE or DELETE statement it should have a WHERE clause. Also please try to check your PIN.";
                gridViewResult.DataSource = null;
                gridViewResult.DataBind();
                //        	} else if (TextBox1.Text.ToUpper().Contains("DROP")) {
                //        		Label1.Text = "Please don't use drop statement in this utility page.";
            }
            else
            {
                labelMessage.Text = "";
                try
                {
                    con = new SqlConnection("database=healthwatch;server=10.0.0.2,1433;user=hwdev;pwd=hwdev;");
                    SqlDataAdapter da = new SqlDataAdapter(textBoxSql.Text, con);
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
                    labelMessage.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        protected void buttonAddMeToRealm_Click(object sender, EventArgs e)
        {
            try {
                con = new SqlConnection("database=healthwatch;server=10.0.0.2,1433;user=hwdev;pwd=hwdev;");
                var cmd = new SqlCommand(@"
insert into Realm(SponsorId, RealmType, RealmIdentifier, IdpUrl, IdpCertificate, UserKeyAttribute)
select SponsorId, RealmType, @IPAddress, IdpUrl, IdpCertificate, UserKeyAttribute from Realm where RealmIdentifier = '1.1.1.1/24'", con);
                con.Open();
                string ip = Request.UserHostAddress + "/24";
                cmd.Parameters.Add("@IPAddress", ip);
                cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                labelMessage.Text = ex.Message;
            } finally {
                con.Close();
            }
        }

        protected void buttonRemoveMeFromRealm_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("database=healthwatch;server=10.0.0.2,1433;user=hwdev;pwd=hwdev;");
                var cmd = new SqlCommand("delete from Realm where ", con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                labelMessage.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}