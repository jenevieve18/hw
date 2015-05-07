<%@ Page language="c#" %>
<%
	HttpContext.Current.Response.Charset = "UTF-8";
	HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>exportText</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
	
    <form id="Form1" method="post" runat="server">
    <table border=1 cellpadding=2 cellspacing=2>
<%
	int SQ = 0; int cx = 0;
	System.Data.SqlClient.SqlDataReader rs = eform.Db.sqlRecordSet("SELECT " +
		"ISNULL(sql.Question,q.Question), " +
		"av.ValueText, " +
		"av.QuestionID, " +
		"(SELECT TOP 1 ISNULL(ocl.Text,CAST(av2.ValueInt AS VARCHAR(5))) FROM AnswerValue av2 LEFT OUTER JOIN OptionComponent oc ON av2.ValueInt = oc.OptionComponentID AND av2.ValueInt > 100 LEFT OUTER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = q.LangID WHERE av2.DeletedSessionID IS NULL AND av2.AnswerID = av.AnswerID AND av2.QuestionID = av.QuestionID-1), " +
		"(SELECT TOP 1 ql2.Question FROM QuestionLang ql2 WHERE ql2.LangID = q.LangID AND ql2.QuestionID = q.QuestionID-1), " +
		"a.AnswerID, " +
		"av.OptionID, " +
		"qq.Variablename, " +
		"av.ValueTextJapaneseUnicode " +
		"FROM ProjectRound pr " +
		"INNER JOIN Answer a ON pr.ProjectRoundID = a.ProjectRoundID " +
		"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
		"INNER JOIN QuestionLang q ON q.LangID = pr.LangID AND av.QuestionID = q.QuestionID " +
		"INNER JOIN Question qq ON q.QuestionID = qq.QuestionID " +
		"LEFT OUTER JOIN SurveyQuestion sq ON av.QuestionID = sq.QuestionID AND sq.SurveyID = pr.SurveyID " +
		"LEFT OUTER JOIN SurveyQuestionLang sql ON sql.LangID = pr.LangID AND sql.SurveyQuestionID = sq.SurveyQuestionID " +
		"WHERE av.ValueText IS NOT NULL AND av.DeletedSessionID IS NULL AND a.EndDT IS NOT NULL AND pr.ProjectRoundID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectRoundID"]) + " " +
		(HttpContext.Current.Request.QueryString["ProjectRoundUnitID"] != null ? " AND a.ProjectRoundUnitID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectRoundUnitID"]) : "") +
		"ORDER BY av.QuestionID");
	while(rs.Read())
	{
		if(SQ != rs.GetInt32(2))
		{
			cx = 0;
%>
</table><br><table border=1 cellpadding=2 cellspacing=2><tr><td colspan="2">AnswerID</td><!--<td><%=rs.GetString(4)%></td>--><td colspan="2">[<% if(rs.IsDBNull(7)||rs.GetString(7).Trim()==""){%>Q<%=rs.GetInt32(2)%>O<%=rs.GetInt32(6)%><%}else{%><%=rs.GetString(7)%><%}%>] <b><%=(rs.IsDBNull(0) ? rs.GetInt32(2).ToString() : rs.GetString(0))%></b></td></tr>
<%
		}
		SQ = rs.GetInt32(2);
%>
<tr><td><%=(++cx)%></td><td><%=rs.GetInt32(5)%></td><!--<td><%=(rs.IsDBNull(3) ? "-" : rs.GetString(3).ToString().Replace("\r\n","<BR>"))%></td>--><td><%=rs.GetString(1).Replace("\r\n","<BR>")%></td><td><%=(rs.IsDBNull(8) ? "" : rs.GetString(8).Replace("\r\n","<BR>"))%></td></tr>
<%
		
	}
	rs.Close();
%>
</table>
     </form>
	
  </body>
</html>
