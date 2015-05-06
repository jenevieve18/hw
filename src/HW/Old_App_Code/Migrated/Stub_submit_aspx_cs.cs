//===========================================================================
// This file was generated as part of an ASP.NET 2.0 Web project conversion.
// This code file 'App_Code\Migrated\Stub_submit_aspx_cs.cs' was created and contains an abstract class 
// used as a base class for the class 'Migrated_submit' in file 'submit.aspx.cs'.
// This allows the the base class to be referenced by all code files in your project.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================


using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace healthWatch
{
    abstract public class submit : System.Web.UI.Page
    {
        public static int createSurveyUser(int untID, string eml)
        {
            int usrID = 0;

            SqlDataReader rs = Db.rs("SELECT ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + untID, "eFormSqlConnection");
            if (rs.Read())
            {
                Db.exec("INSERT INTO ProjectRoundUser (ProjectRoundID,ProjectRoundUnitID,Email) VALUES (" + rs.GetInt32(0) + "," + untID + ",'" + eml.Replace("'", "") + "')", "eFormSqlConnection");
                rs.Close();
                rs = Db.rs("SELECT ProjectRoundUserID FROM [ProjectRoundUser] WHERE ProjectRoundUnitID=" + untID + " AND Email = '" + eml.Replace("'", "") + "' ORDER BY ProjectRoundUserID DESC", "eFormSqlConnection");
                if (rs.Read())
                {
                    usrID = rs.GetInt32(0);
                    Db.exec("INSERT INTO UserProjectRoundUser (UserID, ProjectRoundUnitID, ProjectRoundUserID) VALUES (" + HttpContext.Current.Session["UserID"] + "," + untID + "," + usrID + ")");
                }
            }
            rs.Close();

            return usrID;
        }
    }
}