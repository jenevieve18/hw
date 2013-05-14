<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="superadmin.aspx.cs" Inherits="HWgrp___Old.superadmin" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=Db.header()%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
    <form id="Form1" method="post" runat="server">
		<div class="container_16" id="admin">
		<%=Db2.nav()%>
        <div class="contentgroup grid_16">
            <div class="smallContent">
		        <h3>Administration</h3>
		        <table border="0" cellspacing="0" cellpadding="5">
		            <asp:Label ID=SponsorID runat=server />
		        </table>
                <hr />
                <asp:PlaceHolder ID=ESS runat=server>
		        <h3>Extended survey statistics</h3>
		        <table border="0" cellspacing="0" cellpadding="5">
		            <tr><td><B>Unit</td><td><asp:TextBox ID=MeasureTxt1 runat=server Text="Measure 1"/></td><td><B><asp:TextBox ID=MeasureTxt2 runat=server Text="Measure 2"/></B></td><td><B>Not included</B></td><td><b>Answer count</b></td></tr>
			        <asp:Label ID=ExtendedSurvey runat=server />
		        </table>
		        Show statistics named <asp:TextBox ID=SurveyName runat=server /> based on questions in survey <asp:DropDownList ID="SurveyID" runat=server /><asp:Button Text="Submit" ID=submit runat=server /><br />
                <hr />
                </asp:PlaceHolder>
		        <h3>Survey statistics</h3>
                    <table border="0" cellspacing="0" cellpadding="5">
		            <tr>
                        <td><B>Unit</td>
                        <td><asp:TextBox ID=Measure2Txt1 runat=server Text="Measure 1"/></td>
                        <td><B><asp:TextBox ID=Measure2Txt2 runat=server Text="Measure 2"/></B></td>
                        <td><B>Not included</B></td>
                        <td><b>Last month<br />answer count</b></td>
                        </tr>
			        <asp:Label ID=Survey runat=server />
		        </table>
		        <asp:DropDownList ID=FromDT runat=server />--<asp:DropDownList ID=ToDT runat=server /><asp:DropDownList ID="ReportID" runat=server /><asp:Button Text="Submit" ID=submit2 runat=server />
            </div>
        </div>
	</form>
</body>
</html>