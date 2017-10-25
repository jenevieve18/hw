using System;
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
            con = new SqlConnection("database=healthwatch;server=10.0.0.2,1433;user=hwdev;pwd=hwdev;");
            //con = new SqlConnection(@"Server=DESKTOP-EDQBT7K\SQLEXPRESS;Database=healthwatch;Trusted_Connection=True;");        
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
            SqlDataReader rs = null;
            try
            {
                string identifier = Request.UserHostAddress + "/24";
                var cmd = new SqlCommand("select 1 from Realm where RealmIdentifier = @RealmIdentifier", con);
                cmd.Parameters.Add("@RealmIdentifier", identifier);
                con.Open();
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    labelMessage.Text = "Woah! You are already added to the realm.";
                }
                else
                {
                    cmd = new SqlCommand(@"
insert into Realm(SponsorId, RealmType, RealmIdentifier, IdpUrl, IdpCertificate, UserKeyAttribute)
select SponsorId, RealmType, @RealmIdentifier, IdpUrl, IdpCertificate, UserKeyAttribute from Realm where RealmIdentifier = '::1/24'", con);

                    cmd.ExecuteNonQuery();
                    labelMessage.Text = "You are now added to realm!";
                }
            } catch (Exception ex) {
                labelMessage.Text = ex.Message;
            } finally {
                rs.Close();
                con.Close();
            }
        }

        protected void buttonRemoveMeFromRealm_Click(object sender, EventArgs e)
        {
            try
            {
                var cmd = new SqlCommand("delete from Realm where RealmIdentifier = @RealmIdentifier", con);
                con.Open();
                string identifier = Request.UserHostAddress + "/24";
                cmd.Parameters.Add("@RealmIdentifier", identifier);
                cmd.ExecuteNonQuery();
                labelMessage.Text = "Ouch! You are removed from realm!";
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