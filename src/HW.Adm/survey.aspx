<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="survey.aspx.cs" Inherits="HW.Adm.survey" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="800" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Surveys</td></tr>
		</table>
        <asp:Label ID=Res runat=server />
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
		    <tr><td><B>Unit</td><td><asp:TextBox ID=MeasureTxt1 runat=server Text="Measure 1"/></td><td><B><asp:TextBox ID=MeasureTxt2 runat=server Text="Measure 2"/></B></td><td><B>Not included</B></td></tr>
			<asp:Label ID=Survey runat=server />
		</table>
		<span style="margin:20px;"><asp:DropDownList ID=FromDT runat=server />--<asp:DropDownList ID=ToDT runat=server /><asp:DropDownList ID="ReportID" runat=server /><asp:Button Text="Submit" ID=submit runat=server /></span>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
