<%@ Page language="c#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Webb-QPS</title>
<link href="webbqps.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="main">
	<div id="mainhold">
		<div id="container">
			<div id="header"><a href="default.aspx"><img src="img/webbQPS_logga.gif" width="126" height="61" border="0" style="margin-top:8px; margin-left:30px" /></a></div>
			<div id="navi">
				<div id="menu"><a href="default.aspx"><img src="img/navi_bulletN.gif" width="16" height="13" border="0" />Hem</a> <a href="page-id-2.aspx"><img src="img/navi_bulletA.gif" width="16" height="13" border="0" />F&ouml;r projektdeltagare</a> <a href="page-id-3.aspx"><img src="img/navi_bulletN.gif" width="16" height="13" border="0" />P&aring;g&aring;ende projekt</a> <a href="page-id-5.aspx"><img src="img/navi_bulletN.gif" width="16" height="13" border="0" />Kontakt</a></div>
			</div>
			<div id="content">
			<div style="float:left; width:440px">
			<h1>F&ouml;r projektdeltagare</h1>
			<p>Fyll i din e-postadress nedan om du har tappat bort länken till din enkät eller redan har besvarat enkäten och vill få din återkoppling. Länk till enkät/återkoppling skickas då till din e-postadress.</p>
			<p>OBS! Detta gäller enbart om du är deltagare i något av våra projekt och din e-postadress finns registrerad hos oss.</p>
			<p><form method=post action=page-id-2.aspx><input type="text" name="Email"> <input type="submit" value="Skicka"></form></p>
<%
if(HttpContext.Current.Request.Form["Email"] != null)
{
	try
	{
		System.Data.Odbc.OdbcConnection dataConnection = new System.Data.Odbc.OdbcConnection("Driver={SQL Server};Server=212.112.175.151,1433;Database=eForm;uid=eForm;pwd=knasb01!ar;option=3;");
		dataConnection.Open();
		System.Data.Odbc.OdbcCommand dataCommand = new System.Data.Odbc.OdbcCommand("" +
			"SELECT " +
			"u.ProjectRoundUserID, " +
			"u.UserKey, " +
			"u.Email, " +
			"REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-',''), " +
			"a.EndDT " +
			"FROM ProjectRoundUser u " +
			"INNER JOIN ProjectRoundUnit r ON u.ProjectRoundUnitID = r.ProjectRoundUnitID " +
			"INNER JOIN ProjectRound p ON p.ProjectRoundID = r.ProjectRoundID " +
			"LEFT OUTER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
			"WHERE p.ProjectRoundID = 74 AND u.Email = '" + HttpContext.Current.Request.Form["Email"].ToString().Replace("'","") + "' " +
			"ORDER BY ISNULL(a.EndDT, '2012-12-12') DESC", 
			dataConnection);
		dataCommand.CommandTimeout = 900;
		System.Data.Odbc.OdbcDataReader dataReader = dataCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
		if(dataReader.Read())
		{
			System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
			msg.To = dataReader.GetString(2);
			msg.From = "info@webbqps.se";
			msg.Subject = "Webb-QPS";
			if(dataReader.IsDBNull(4))
			{
				msg.Body = "Här kommer länken till din enkät.\r\n\r\nhttp://webbqps.se/submit.aspx?K=" + dataReader.GetGuid(1).ToString().Substring(0,8) + dataReader.GetInt32(0).ToString();
			}
			else
			{
				msg.Body = "Du har besvarat enkäten.\r\n\r\n";
				if(System.IO.File.Exists(System.Web.HttpContext.Current.Request.MapPath("/archive/" + dataReader.GetString(3) + ".pdf")))
				{
					msg.Body += "Om du vill ha din återkoppling skickad till dig kan du klicka på länken nedan.\r\n\r\nhttp://webbqps.se/sendPDF.aspx?AK=" + dataReader.GetString(3);
				}
				else
				{
					msg.Body += "Din återkoppling har ännu inte genererats pga köbildning i systemet. Ett e-postmeddelande kommer att skickas till dig när den finns tillgänglig.";
				}
			}
			msg.BodyFormat = System.Web.Mail.MailFormat.Text;
			msg.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
			System.Web.Mail.SmtpMail.SmtpServer = "mail.interactivehealthgroup.com";
			System.Web.Mail.SmtpMail.Send(msg);
%>
<p style="color:#CC0000;">Informationen är nu skickad till din e-postadress.</p>
<%
		}
		else
		{
%>
<p style="color:#CC0000;">Vi kunde tyvärr inte hitta den e-postadress du uppgav.</p>
<%
		}
		dataReader.Close();
	}
	catch (Exception e)
	{
		HttpContext.Current.Response.Write("<!--" + e.Message + "-->");
%>
<p style="color:#CC0000;">Ett fel uppstod. Vänligen försök senare.</p>
<%
	}
}
%>
			<p><b style="font-size:14px;">Chefsstudien vid Karolinska Universitetssjukhuset</b></p>
			<p><a href="page-id-8.aspx">Information för deltagande chefer &raquo;</A></p>
			<p><a href="page-id-9.aspx">Information för deltagande chefers medarbetare &raquo;</A></p>
			</div>
			<div style="float:right; width:220px">
				<div id="box_top"><span class="box_top_header">Pågående projekt</span></div>
				<div id="box_main">
				  <p>Chefsstudien vid Karolinska Universitetssjukhuset</p>
				  <p><a href="page-id-3.aspx">Mer information &raquo;</a></p>
				</div>
				<div id="box_bottom"></div>
			</div>
			</div>
			<div style="clear:both; height:40px"></div>
			<div id="footer"><img align="right" src="img/loggar.gif" width="200" height="45" />&copy; 2007-2008 Karolinska Institutet.</div>
	</div>
</div>
</body>
</html>