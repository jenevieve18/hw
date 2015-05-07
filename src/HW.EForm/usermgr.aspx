<%@ Page language="c#" Codebehind="usermgr.aspx.cs" AutoEventWireup="false" Inherits="eform.usermgr" %>
<html>
<head>
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Expires" content="-1">
<!--
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	-->
<title>eForm</title>
<link href="submit2.css" rel="stylesheet" type="text/css" media="screen">
<link href="submit2print.css" rel="stylesheet" type="text/css" media="print">
</head>
<body>
<form name="usermgr" method="post" runat="server">
<div id="container">
	<br/><br/><br/><br/><br/><br/><br/><br/><br/>
	<div id="eform_header"><p>User manager</p></div>
	<asp:PlaceHolder ID=LoginBox Runat=server>
		<div class="eform_area"><p>Logga in</p></div>
		<div class="eform_ques" style="padding:20px;">
			Användarnamn&nbsp;<asp:TextBox ID="Username" runat=server/>&nbsp;Lösenord&nbsp;<asp:TextBox ID=Password TextMode=Password Runat=server/> <asp:Button ID="Login" Text="OK" Runat=server/>
		</div>
	</asp:PlaceHolder>
	<asp:PlaceHolder ID=LoggedIn Runat=server Visible=False>
		<asp:PlaceHolder ID=EditUser Runat=server visible=false>
			<div class="eform_area"><p>Redigera användare</p></div>
			<div class="eform_ques" style="padding:20px;">
				<asp:Label ID="CodeTxt" Runat=server/>
				<asp:PlaceHolder ID="EmailPH" Runat=server>
				<asp:Label ID="EmailLabel" Runat=server/>&nbsp;<asp:TextBox ID="Email" Width=200 runat=server/>
				<br/>
				</asp:PlaceHolder>
				<asp:PlaceHolder ID="UnitPH" Runat=server>
				Enhet&nbsp;<asp:DropDownList ID=ProjectRoundUnitID Runat=server/>
				<br/>
				</asp:PlaceHolder>
				<asp:PlaceHolder ID="TerminatedPH" Runat=server>
				Avregistrerad&nbsp;<asp:CheckBox ID=Terminated Runat=server/>
				</asp:PlaceHolder>
				<asp:PlaceHolder ID="ExtendedPH" Runat=server>
				<br/>
				Visa utökad enkät&nbsp;<asp:CheckBox ID=Extended Runat=server/>
				</asp:PlaceHolder>
				<br/>
				Påbörjat enkät&nbsp;<asp:Label ID=Started Runat=server/>
				<br/>
				Skickat in enkät&nbsp;<asp:Label ID=Ended Runat=server/>
				<br/><br/>
				<asp:Button ID=Cancel Text="Avbryt" Runat=server/>&nbsp;<asp:Button ID="Save" Text="Spara" Runat=server/>
			</div>
		</asp:PlaceHolder>
		<div class="eform_area"><p>Användare</p></div>
		<div class="eform_ques" style="padding:20px;">
			<asp:Button ID="AddUser" Text="Lägg till användare" Runat=server/> <asp:Label ID="Stats" runat=server/>
			<br/><br/>
			<asp:Label ID="SearchEmailLabel" Runat=server/>&nbsp;<asp:TextBox ID=SearchEmail Width=200 runat=server/>&nbsp;<asp:Button ID=Search Text="Sök" Runat=server/>
			<br/><br/>
			<asp:Label ID=SearchResults Runat=server/>
		</div>
	</asp:PlaceHolder>
</div>
</form>
</body>
</html>

