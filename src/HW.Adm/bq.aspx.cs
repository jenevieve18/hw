using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
	public partial class bq : System.Web.UI.Page
	{
		SqlQuestionRepository repository = new SqlQuestionRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
//        	string query = string.Format(
//        		@"
//SELECT BQID,
//	Internal,
//	Type,
//	Comparison,
//	Variable
//FROM BQ ORDER BY Internal"
//        	);
//        	SqlDataReader rs = Db.rs(query);
//            if (rs.Read())
			BQ.Text = "<TR><TD><B>Question</B>&nbsp;&nbsp;</TD><TD><B>Variable</B>&nbsp;&nbsp;</TD><TD><B>Type</B>&nbsp;&nbsp;</TD><TD><B>Comparison</B>&nbsp;&nbsp;</TD></TR>";
			foreach (var q in repository.FindAllBackgroundQuestions())
			{
//				BQ.Text = "<TR><TD><B>Question</B>&nbsp;&nbsp;</TD><TD><B>Variable</B>&nbsp;&nbsp;</TD><TD><B>Type</B>&nbsp;&nbsp;</TD><TD><B>Comparison</B>&nbsp;&nbsp;</TD></TR>";
//				do
//				{
//					BQ.Text += "<TR><TD><A HREF=\"bqSetup.aspx?BQID=" + rs.GetInt32(0) + "\">" + (rs.GetString(1).Length > 30 ? rs.GetString(1).Substring(0, 27) + "..." : rs.GetString(1)) + "</A>&nbsp;&nbsp;</TD><TD>" + (rs.IsDBNull(4) ? "" : rs.GetString(4)) + "&nbsp;&nbsp;<TD>";
				    BQ.Text += "<TR><TD><A HREF=\"bqSetup.aspx?BQID=" + q.Id + "\">" + (q.Internal.Length > 30 ? q.Internal.Substring(0, 27) + "..." : q.Internal) + "</A>&nbsp;&nbsp;</TD><TD>" + (q.Variable == "" ? "" : q.Variable) + "&nbsp;&nbsp;<TD>";
//					switch (rs.GetInt32(2))
					switch (q.Type)
					{
							case 1: BQ.Text += "Select one, radio-style"; break;
							case 2: BQ.Text += "Text"; break;
							case 3: BQ.Text += "Date"; break;
							case 4: BQ.Text += "Numeric"; break;
							case 7: BQ.Text += "Select one, drop-down-style"; break;
							case 9: BQ.Text += "VAS"; break;
					}
//					BQ.Text += "&nbsp;&nbsp;</TD><TD>" + (rs.IsDBNull(3) ? "" : "X") + "&nbsp;&nbsp;</TD></TR>";
					BQ.Text += "&nbsp;&nbsp;</TD><TD>" + (q.Comparison == 0 ? "" : "X") + "&nbsp;&nbsp;</TD></TR>";
//				}
//				while (rs.Read());
			}
//			rs.Close();
		}
	}
}