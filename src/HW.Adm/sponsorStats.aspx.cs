using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using System.Data.SqlClient;

namespace HW.Adm
{
    public partial class sponsorStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlDataReader rs = Db.rs("SELECT s.SponsorID, s.Sponsor, ss.SuperSponsor FROM Sponsor s LEFT OUTER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID WHERE s.Deleted IS NULL ORDER BY ss.SuperSponsor, s.Sponsor");
                while (rs.Read())
                {
                    Sponsor.Items.Add(new ListItem((rs.IsDBNull(2) ? "OTHER" : rs.GetString(2)) + " / " + rs.GetString(1), rs.GetInt32(0).ToString()));
                }
                rs.Close();
            }
            OK.Click += new EventHandler(OK_Click);
        }

        void OK_Click(object sender, EventArgs e)
        {
            string sponsors = "0";
            SqlDataReader rs = Db.rs("SELECT s.SponsorID, s.Sponsor, ss.SuperSponsor FROM Sponsor s LEFT OUTER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID WHERE s.Deleted IS NULL ORDER BY ss.SuperSponsor, s.Sponsor");
            while (rs.Read())
            {
                if (Sponsor.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
                {
                    sponsors += "," + rs.GetInt32(0);
                }
            }
            rs.Close();

            res.Text = "<br/><br/><b>Average number of logins and time spent on site per user by month</b><br/><table border=\"1\"><tr>" +
                    "<td>Month</td>" +
                    "<td>Logins</td>" +
                    "<td>Min</td>" +
                    "<td>Max</td>" +
                    "<td>Total</td>" +
                    "<td>Minutes</td>" +
                    "<td>Min</td>" +
                    "<td>Max</td>" +
                    "<td>Total</td>" +
                    "<td>n</td>" +
                    "</tr>";
            rs = Db.rs("" +
                "SELECT ROUND(AVG(tmp.AX),1), tmp.CXID, COUNT(tmp.UserID), MIN(tmp.AX), MAX(tmp.AX), SUM(tmp.AX), ROUND(AVG(tmp.t),1), MIN(tmp.t), MAX(tmp.t), SUM(tmp.t) FROM (" +
                "SELECT CAST(COUNT(s.SessionID) AS REAL) AS AX, c.CXID, u.UserID, SUM(dbo.cf_sessionMinutes(s.DT,s.EndDT,s.autoended)) AS t " +
                "FROM [User] u " +
                "INNER JOIN CX c ON c.CXID <= 48 AND c.CXID < dbo.cf_monthsSinceRegistration(u.UserID) " +
                "LEFT OUTER JOIN Session s ON s.UserID = u.UserID AND DATEDIFF(month,u.Created,s.DT) = c.CXID " +
                "WHERE u.SponsorID IN (" + sponsors + ") " +
                "GROUP BY c.CXID, u.UserID" +
                ") tmp GROUP BY tmp.CXID ORDER BY tmp.CXID");
            while (rs.Read())
            {
                res.Text += "<tr>" +
                    "<td>" + rs.GetInt32(1) + "</td>" +
                    "<td>" + rs.GetValue(0) + "</td>" +
                    "<td>" + rs.GetValue(3) + "</td>" +
                    "<td>" + rs.GetValue(4) + "</td>" +
                    "<td>" + rs.GetValue(5) + "</td>" +
                    "<td>" + rs.GetValue(6) + "</td>" +
                    "<td>" + rs.GetValue(7) + "</td>" +
                    "<td>" + rs.GetValue(8) + "</td>" +
                    "<td>" + rs.GetValue(9) + "</td>" +
                    "<td>" + rs.GetInt32(2) + "</td>" +
                    "</tr>";
            }
            rs.Close();
            res.Text += "</table>";

            res.Text += "<br/><br/><b>Average number of exercises and time spent on exercises per user by month</b><br/><table border=\"1\"><tr>" +
                "<td>Month</td>" +
                "<td>Exercises</td>" +
                "<td>Min</td>" +
                "<td>Max</td>" +
                "<td>Total</td>" +
                "<td>Minutes</td>" +
                "<td>Min</td>" +
                "<td>Max</td>" +
                "<td>Total</td>" +
                "<td>n</td>" +
                "</tr>";
            rs = Db.rs("" +
                "SELECT " +
                "ROUND(AVG(tmp.AX),1), " +
                "tmp.CXID, " +
                "COUNT(tmp.UserID), " +
                "MIN(tmp.AX), " +
                "MAX(tmp.AX), " +
                "ROUND(AVG(tmp.M),1), " +
                "MIN(tmp.M), " +
                "MAX(tmp.M), " +
                "SUM(tmp.AX), " +
                "SUM(tmp.M) " +
                "FROM (" +
                "SELECT CAST(COUNT(s.ExerciseStatsID) AS REAL) AS AX, c.CXID, u.UserID, SUM(ISNULL(e.Minutes,0)) AS M " +
                "FROM [User] u " +
                "INNER JOIN CX c ON c.CXID <= 48 AND c.CXID < dbo.cf_monthsSinceRegistration(u.UserID) " +
                "LEFT OUTER JOIN ExerciseStats s ON s.UserID = u.UserID AND DATEDIFF(month,u.Created,s.DateTime) = c.CXID " +
                "LEFT OUTER JOIN ExerciseVariantLang l ON s.ExerciseVariantLangID = l.ExerciseVariantLangID " +
                "LEFT OUTER JOIN ExerciseVariant v ON l.ExerciseVariantID = v.ExerciseVariantID " +
                "LEFT OUTER JOIN Exercise e ON v.ExerciseID = e.ExerciseID " +
                "WHERE u.SponsorID IN (" + sponsors + ") " +
                "GROUP BY c.CXID, u.UserID" +
                ") tmp GROUP BY tmp.CXID ORDER BY tmp.CXID");
            while (rs.Read())
            {
                res.Text += "<tr>" +
                    "<td>" + rs.GetInt32(1) + "</td>" +
                    "<td>" + rs.GetValue(0) + "</td>" +
                    "<td>" + rs.GetValue(3) + "</td>" +
                    "<td>" + rs.GetValue(4) + "</td>" +
                    "<td>" + rs.GetValue(8) + "</td>" +
                    "<td>" + rs.GetValue(5) + "</td>" +
                    "<td>" + rs.GetValue(6) + "</td>" +
                    "<td>" + rs.GetValue(7) + "</td>" +
                    "<td>" + rs.GetValue(9) + "</td>" +
                    "<td>" + rs.GetInt32(2) + "</td>" +
                    "</tr>";
            }
            rs.Close();
            res.Text += "</table>";

            int tot = 0;
            rs = Db.rs("" +
                "SELECT COUNT(*) " +
                "FROM [User] u " +
                "INNER JOIN Session s ON s.UserID = u.UserID " +
                "WHERE u.SponsorID IN (" + sponsors + ")");
            while (rs.Read())
            {
                tot += rs.GetInt32(0);
            }
            rs.Close();

            res.Text += "<br/><br/><b>Login time</b><br/><table border=\"1\"><tr>" +
                    "<td>Time</td>" +
                    "<td>Logins</td>" +
                    "<td>Percentage</td>" +
                    "</tr>";
            rs = Db.rs("" +
                "SELECT COUNT(*), DATEPART(hour,s.DT) " +
                "FROM [User] u " +
                "INNER JOIN Session s ON s.UserID = u.UserID " +
                "WHERE u.SponsorID IN (" + sponsors + ") " +
                "GROUP BY DATEPART(hour,s.DT) ORDER BY DATEPART(hour,s.DT)");
            while (rs.Read())
            {
                res.Text += "<tr>" +
                    "<td>" + rs.GetInt32(1).ToString().PadLeft(2, '0') + ":00-" + rs.GetInt32(1).ToString().PadLeft(2, '0') + ":59</td>" +
                    "<td>" + rs.GetInt32(0) + "</td>" +
                    "<td>" + Math.Round((double)rs.GetInt32(0) / (double)(tot == 0 ? 1 : tot) * 100d, 1) + " %</td>" +
                    "</tr>";
            }
            rs.Close();
            res.Text += "</table>";

            res.Text += "<br/><br/><b>Average time spent on a single form by month</b><br/><table border=\"1\"><tr>" +
                    "<td>Month</td>" +
                    "<td>Seconds</td>" +
                    "<td>Min</td>" +
                    "<td>Max</td>" +
                    "<td>n</td>" +
                    "</tr>";
            rs = Db.rs("" +
                "SELECT c.CXID, ROUND(AVG(DATEDIFF(second,a.StartDT, a.EndDT)),1),  MIN(DATEDIFF(second,a.StartDT, a.EndDT)),  MAX(DATEDIFF(second,a.StartDT, a.EndDT)), COUNT(a.AnswerID) " +
                "FROM [User] u " +
                "INNER JOIN CX c ON c.CXID <= 48 AND c.CXID < dbo.cf_monthsSinceRegistration(u.UserID) " +
                "INNER JOIN UserProjectRoundUser u2 ON u.UserID = u2.UserID " +
                "INNER JOIN UserProjectRoundUserAnswer u3 ON u2.ProjectRoundUserID = u3.ProjectRoundUserID " +
                "INNER JOIN eform..Answer a ON u3.AnswerID = a.AnswerID AND DATEDIFF(month,u.Created,a.StartDT) = c.CXID  " +
                "WHERE u.SponsorID IN (" + sponsors + ") AND DATEDIFF(minute,a.StartDT, a.EndDT) < 10 " +
                "GROUP BY c.CXID");
            while (rs.Read())
            {
                res.Text += "<tr>" +
                    "<td>" + rs.GetInt32(0) + "</td>" +
                    "<td>" + rs.GetValue(1) + "</td>" +
                    "<td>" + rs.GetValue(2) + "</td>" +
                    "<td>" + rs.GetValue(3) + "</td>" +
                    "<td>" + rs.GetValue(4) + "</td>" +
                    "</tr>";
            }
            rs.Close();
            res.Text += "</table>";

            int ex = 0;
            rs = Db.rs("" +
                "SELECT COUNT(*) " +
                "FROM ExerciseStats es " +
                "INNER JOIN [User] u ON es.UserID = u.UserID " +
                "WHERE u.SponsorID IN (" + sponsors + ")");
            if (rs.Read())
            {
                ex = rs.GetInt32(0);
            }
            rs.Close();
            res.Text += "<br/><br/><b>Exercise use count</b><br/><table border=\"1\"><tr>" +
                    "<td>Exercise</td>" +
                    "<td>Use count</td>" +
                    "<td>%</td>" +
                    "</tr>";
            rs = Db.rs("" +
                "SELECT el.Exercise, COUNT(*) " +
                "FROM ExerciseStats es " +
                "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
                "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID  = ev.ExerciseVariantID " +
                "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
                "INNER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND evl.Lang = el.Lang " +
                "INNER JOIN [User] u ON es.UserID = u.UserID " +
                "WHERE u.SponsorID IN (" + sponsors + ") " +
                "GROUP BY el.Exercise");
            while (rs.Read())
            {
                res.Text += "<tr>" +
                    "<td>" + rs.GetString(0) + "</td>" +
                    "<td>" + rs.GetInt32(1) + "</td>" +
                    "<td>" + Math.Round((double)rs.GetInt32(1) / (double)(ex == 0 ? 1 : ex) * 100d, 1) + " %</td>" +
                    "</tr>";
            }
            rs.Close();
            res.Text += "</table>";
        }
    }
}