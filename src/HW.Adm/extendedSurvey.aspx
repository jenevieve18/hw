<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extendedSurvey.aspx.cs" Inherits="HW.Adm.extendedSurvey" %>
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
			<tr><td style="font-size:16px;" align="center">Extended surveys</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
		    <tr><td><B>Unit</td><td><asp:TextBox ID=MeasureTxt1 runat=server Text="Measure 1"/></td><td><B><asp:TextBox ID=MeasureTxt2 runat=server Text="Measure 2"/></B></td><td><B>Not included</B></td></tr>
			<asp:Label ID=ExtendedSurvey runat=server />
		</table>
		<span style="margin:20px;">
            Show statistics named <asp:TextBox ID=SurveyName runat=server /> based on survey <asp:DropDownList AutoPostBack=true ID="SurveyID" runat=server /><asp:Button Text="Submit" ID=submitBtn runat=server />
            <br /><br />
            <i><B>Please note</B> that "database total" is not limited to your organization(s), but is the complete database</i>
            <br /><br />
            <b>Select individual questions</b>
            <br />
            <asp:CheckBoxList ID=Q runat=server />
        </span>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
