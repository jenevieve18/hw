<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="extendedSurvey.aspx.cs" Inherits="HW.Adm.extendedSurvey" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="800" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Extended surveys</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
		    <tr><td><B>Unit</B></td><td><asp:TextBox ID=MeasureTxt1 runat=server Text="Measure 1"/></td><td><asp:TextBox ID=MeasureTxt2 runat=server Text="Measure 2"/></td><td><B>Not included</B></td></tr>
			<asp:Label ID=ExtendedSurvey runat=server />
		    <tr><td></td><td><asp:Label ID=M1CX runat=server/></td><td><asp:Label ID=M2CX runat=server/></td></tr>
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
</asp:Content>
