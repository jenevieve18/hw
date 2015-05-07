<%@ Page language="c#" Codebehind="sponsoradmin.aspx.cs" AutoEventWireup="false" Inherits="eform.sponsoradmin" %>
<html>
<head>
	<meta http-equiv="Pragma" content="no-cache">
	<meta http-equiv="Expires" content="-1">
	<!--
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	-->
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>eForm</title>
	<link href="submit2.css" rel="stylesheet" type="text/css" media="screen">
	<link href="submit2print.css" rel="stylesheet" type="text/css" media="print">
</head>
<body>
<form name="Form1" method="post" runat="server" ID="Form1">
<div id="container">
	<br/><br/><br/><br/><br/><br/><br/><br/><br/>
	<div id="eform_header"><p>Projektadministration</p></div>
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
				<table>
					<tr><td>Användarnamn</td><td><asp:TextBox Width=400 ID="UserUsername" Runat=server/></td></tr>
					<tr><td>Lösenord</td><td><asp:TextBox Width=400 ID="UserPassword" Runat=server/></td></tr>
					<tr><td>Namn</td><td><asp:TextBox Width=400 ID="UserFullname" Runat=server/></td></tr>
					<tr><td>E-post</td><td><asp:TextBox Width=400 ID="UserEmail" Runat=server/></td></tr>
					<tr><td>Projekt</td><td><asp:DropDownList Width=400 ID="UserSponsorID" Runat=server/></td></tr>
				</table>
				<br/><br/>
				<asp:Button ID=CancelUser Text="Avbryt" Runat=server/>&nbsp;<asp:Button ID="SaveUser" Text="Spara" Runat=server/>&nbsp;<asp:Button ID="DeleteUser" Visible=False Text="Ta bort" Runat=server/>
			</div>
		</asp:PlaceHolder>
		<asp:PlaceHolder ID="EditProject" Runat=server visible=false>
			<div class="eform_area"><p>Redigera project</p></div>
			<div class="eform_ques" style="padding:20px;">
				<table>
					<tr><td>Namn</td><td><asp:TextBox Width=400 ID="Sponsor" Runat=server/></td></tr>
					<tr><td>Mall</td><td><asp:DropDownList Width=400 ID="ProjectID" Runat=server/></td></tr>
				</table>
				<br/><br/>
				<asp:Button ID="CancelProject" Text="Avbryt" Runat=server/>&nbsp;<asp:Button ID="SaveProject" Text="Spara" Runat=server/>
			</div>
		</asp:PlaceHolder>
		<div class="eform_area"><p>Administrationskonsoll</p></div>
		<div class="eform_ques" style="padding:20px;">
			<asp:Button ID="AddUser" Text="Lägg till användare" Runat=server/><asp:Button ID="AddProject" Text="Lägg till projekt" Runat=server/><asp:Button ID="Logout" Text="Logga ut" Runat=server/>
			<br/><br/>
			<asp:Label ID=SearchResults Runat=server/>
		</div>
	</asp:PlaceHolder>
</div>
</form>
</body>
</html>

