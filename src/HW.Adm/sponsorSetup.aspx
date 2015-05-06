<%@ Page Language="C#" AutoEventWireup="true" Inherits="sponsorSetup" ValidateRequest=false Codebehind="sponsorSetup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="800" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Sponsor</td></tr>
		</table>
        <iframe height="166" width="830" id=chart runat=server seamless frameborder=0 scrolling="no"></iframe>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
            <tr>
				<td><B>Sponsor key</B>&nbsp;</td>
				<td><asp:Label ID=SponsorKey Width=200 runat=server /></td>
				<td align=right rowspan=5><asp:Label ID="Logotype" runat="server" /></td>
			</tr>
			<tr>
				<td><B>Sponsor</B>&nbsp;</td>
				<td><asp:TextBox ID=Sponsor Width=200 runat=server /></td>
			</tr>
			<tr>
				<td><B>Application&nbsp;name</B>&nbsp;</td>
				<td><asp:TextBox ID=App Width=200 runat=server /></td>
			</tr>
            <tr>
				<td><B>Email&nbsp;from-address</B>&nbsp;</td>
				<td><asp:TextBox ID=EmailFrom Width=200 runat=server /></td>
			</tr>
			<tr>
				<td><B>Logotype</B>&nbsp;</td>
				<td><input type="file" ID=Logo Width=200 runat=server /></td>
			</tr>
            <tr>
				<td><B>Partner</B>&nbsp;</td>
				<td colspan="2"><asp:DropDownList ID=SuperSponsorID runat=server /></td>
			</tr>
            <tr>
				<td><B>Info text</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=InfoText Rows=5 TextMode=MultiLine Width=400 runat=server /><asp:Label ID=TestInfoText runat=server /></td>
			</tr>
            <tr>
				<td><B>Consent text</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=ConsentText Rows=5 TextMode=MultiLine Width=400 runat=server /><asp:Label ID=TestConsentText runat=server /></td>
			</tr>
            <tr>
				<td><B>Treatment offer</B>&nbsp;</td>
				<td colspan="2"><asp:CheckBox ID=TreatmentOffer runat=server /></td>
			</tr>
            <tr>
				<td><B>Treatment offer text</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=TreatmentOfferText Width=400 runat=server /></td>
			</tr>
             <tr>
				<td><B>Treatment offer if needed text</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=TreatmentOfferIfNeededText Width=400 runat=server /></td>
			</tr>
            <tr>
				<td><B>Treatment offer email</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=TreatmentOfferEmail Width=400 runat=server /></td>
			</tr>
            <tr>
				<td><B>Treatment offer background question</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=TreatmentOfferBQ Width=30 runat=server /></td>
			</tr>
            <tr>
				<td><B>Treatment offer background question fn()</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=TreatmentOfferBQfn Width=30 runat=server /></td>
			</tr>
            <tr>
				<td><B>Treatment offer background question more than</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=TreatmentOfferBQmorethan Width=30 runat=server /></td>
			</tr>
            <tr>
				<td><B>Alternative treatment offer text</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=AlternativeTreatmentOfferText Width=400 runat=server /></td>
			</tr>
            <tr>
				<td><B>Alternative treatment offer email</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=AlternativeTreatmentOfferEmail Width=400 runat=server /></td>
			</tr>
            <tr>
				<td valign="top"><B>Language</B>&nbsp;</td>
				<td colspan="2"><asp:RadioButtonList ID=LID runat=server RepeatLayout=Table RepeatDirection=Vertical /></td>
			</tr>
             <tr>
				<td><B>Minimum users to disclose counters</B>&nbsp;</td>
				<td colspan="2"><asp:TextBox ID=MinUserCountToDisclose Width=30 runat=server /></td>
			</tr>
			<tr><td colspan="3">&nbsp;</td></tr>
			<tr><td colspan="3"><asp:Button ID=Back Text="Back" runat=server />&nbsp;<asp:Button ID=Save runat=server Text="Save" />&nbsp;<asp:Button ID=AddExtendedSurvey runat=server Text="Add extended survey" />&nbsp;<asp:Button ID=Close runat=server Text="Close down" />&nbsp;<asp:Button ID=Delete runat=server Text="Delete" />&nbsp;<asp:Button ID=DisconnectAll runat=server Text="Disconnect all" /></td></tr>
		</table>
        <asp:PlaceHolder ID=Departments runat=server />
		<asp:PlaceHolder ID=SponsorExtendedSurvey runat=server />
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Surveys</td></tr>
		</table>
		<table style="margin:20px;" width="900" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td valign="bottom">&nbsp;</td>
				<td valign="bottom"><img src="verticalText.aspx?STR=Include"></td>
				<td valign=bottom><B>Survey</B>&nbsp;</td>
				<td valign=bottom><B>Name&nbsp;for&nbsp;this&nbsp;sponsor</B>&nbsp;</td>
				<td valign=bottom><B>Individual report</B>&nbsp;</td>
				<td valign=bottom><B>Group report</B>&nbsp;</td>
                <td valign=bottom><img src="verticalText.aspx?STR=Only%20Every%20Days"></td>
                <td valign=bottom><img src="verticalText.aspx?STR=Go%20To%20Statistics"></td>
			</tr>
			<asp:PlaceHolder ID=Surveys runat=server />
		</table>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Registration</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td valign="bottom">&nbsp;</td>
				<td valign="bottom"><img src="verticalText.aspx?STR=Include"></td>
				<td valign=bottom><B>Background&nbsp;question</B>&nbsp;</td>
				<td valign="bottom"><img src="verticalText.aspx?STR=Mandatory"></td>
				<td valign="bottom"><img src="verticalText.aspx?STR=Hidden, pre-set on invite"></td>
                <td valign="bottom"><img src="verticalText.aspx?STR=Show in group admin"></td>
                <td valign="bottom"><img src="verticalText.aspx?STR=Include in treatment req"></td>
                <td valign="bottom"><img src="verticalText.aspx?STR=Show as organization"></td>
			</tr>
			<asp:PlaceHolder ID=BQ runat=server />
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>