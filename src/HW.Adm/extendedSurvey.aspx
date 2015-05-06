<%@ Page Language="C#" AutoEventWireup="true" Inherits="extendedSurvey" Codebehind="extendedSurvey.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="1200" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Extended surveys</td></tr>
		</table>
		<table style="margin:20px;" width="1200" border="0" cellspacing="0" cellpadding="0">
            <TR><TD colspan="3"></TD><TD>Chart/tabulated</TD><TD>Chart/tabulated</TD><TD>Tabulated</TD><TD>Tabulated</TD><TR>
		    <tr><td colspan="3"><B>Unit</B></td><td><asp:TextBox ID=MeasureTxt1 runat=server Text="Measure 1"/></td><td><asp:TextBox ID=MeasureTxt2 runat=server Text="Measure 2"/></td><td><asp:TextBox ID=MeasureTxt3 runat=server Text="Measure 3"/></td><td><asp:TextBox ID=MeasureTxt4 runat=server Text="Measure 4"/></td><td><B>Not included</B></td></tr>
			<asp:Label ID=ExtendedSurvey runat=server />
		    <tr><td colspan="3"></td><td><asp:Label ID=M1CX runat=server/></td><td><asp:Label ID=M2CX runat=server/></td></tr>
		</table>
		<span style="margin:20px;">
            Show statistics named <asp:TextBox ID=SurveyName runat=server /> based on survey <asp:DropDownList AutoPostBack=true ID="SurveyID" runat=server /><asp:DropDownList ID="LID" runat=server /><asp:Button Text="Chart" ID=submitBtn runat=server /><asp:Button Text="Tabulate" ID=submitTabBtn runat=server />
            <br /><br />
            <i><B>Please note</B> that "database total" is not limited to your organization(s), but is the complete database</i>
            <br /><br />
            <b><a href="javascript:void(document.getElementById('Q').style.display='');">Select</a> individual questions</b>
            <br />
            <asp:CheckBoxList ID=Q style="display:none;" runat=server />
        </span>
		<%=Db.bottom()%>
		</form>
  </body>
</html>