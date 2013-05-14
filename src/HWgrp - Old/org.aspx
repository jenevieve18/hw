<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="org.aspx.cs" Inherits="HWgrp___Old.org" %>
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
                <div id="contextbar">
                    <div class="top">
                        <div class="search">
                            Search user by email
                            <asp:TextBox ID=SearchEmail runat=server />
                            <asp:Button ID=Search Text="Search" runat=server />
                        </div>
                    </div>
                    <div class="bottom" id=ActionNav runat=server>
                        <a class="add-unit" href="org.aspx?Action=AddUnit">Add unit</a>
                        <a class="import-unit" href="org.aspx?Action=ImportUnit">Import units</a>
                        <a class="add-user" href="org.aspx?Action=AddUser">Add user</a>
                        <a class="import-users" href="org.aspx?Action=ImportUser">Import users</a>
                    </div>
                    <div class="actionPane">
                    	<asp:PlaceHolder ID="AddUnit" runat=server Visible=false>
		                <div class="actionBlock">
			                <span class="desc">Unit name</span><asp:TextBox CssClass="inpt" ID=Department runat=server /> (for display in hierarchy)<br />
			                <span class="desc">Unit ID</span><asp:TextBox CssClass="inpt" ID=DepartmentShort runat=server /> (for user import/export, short name, no spaces)<br/>
			                <span class="desc">Parent unit</span><asp:DropDownList CssClass="maxInpt" ID=ParentDepartmentID runat=server /><br/>
			                <asp:Button ID="CancelUnit" CssClass="btn" Text="Cancel" runat=server /><asp:Button CssClass="btn" ID="SaveUnit" Text="Save" runat=server />
		                </div>
		                </asp:PlaceHolder>
		                <asp:PlaceHolder ID="ImportUnits" runat=server Visible=false>
		                <div class="actionBlock">
			                <span class="descLong">Filename</span><input type=file class="inpt" ID=UnitsFilename runat=server /><br/>
			                <span class="descLong">Default parent unit</span><asp:DropDownList CssClass="maxInpt" ID=ImportUnitsParentDepartmentID runat=server /><br/>
			                <span style="color:#CC0000;"><asp:Label ID=ImportUnitsError runat=server /></span>
			                <asp:Button ID="CancelImportUnit" Text="Cancel" CssClass=btn runat=server /><asp:Button ID="SaveImportUnit" Text="Save" CssClass=btn runat=server />
		                </div>
		                </asp:PlaceHolder>
		                <asp:PlaceHolder ID="AddUser" runat=server Visible=false>
		                <div class="actionBlock">
			                <span class="desc">Email</span><asp:TextBox CssClass="inpt" ID=Email runat=server /><br />
			                <span class="desc">Unit</span><asp:DropDownList CssClass="maxInpt" ID=DepartmentID runat=server /><br />
			                <asp:PlaceHolder ID="Hidden" runat=server />
			                <asp:PlaceHolder ID="UserUpdate" runat=server>
                            <span class="desc">Status</span>
                                <asp:DropDownList ID=StoppedReason runat=server>
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Stopped, work related</asp:ListItem>
                                    <asp:ListItem Value="2">Stopped, education leave</asp:ListItem>
                                    <asp:ListItem Value="14">Stopped, parental leave</asp:ListItem>
                                    <asp:ListItem Value="24">Stopped, sick leave</asp:ListItem>
                                    <asp:ListItem Value="34">Stopped, do not want to participate</asp:ListItem>
                                    <asp:ListItem Value="44">Stopped, no longer associated</asp:ListItem>
                                    <asp:ListItem Value="4">Stopped, other reason</asp:ListItem>
                                    <asp:ListItem Value="5">Stopped, unknown reason</asp:ListItem>
                                    <asp:ListItem Value="6">Stopped, project completed</asp:ListItem>
                                </asp:DropDownList> updated on <asp:TextBox Width=80 ID=Stopped runat=server /> (future date not allowed)
                            <br />
			                If this user has activated the account, perform the following<br />
                                <asp:RadioButtonList ID=UserUpdateFrom runat=server RepeatDirection=Vertical RepeatLayout=table>
						                <asp:ListItem Value=1 Selected>Update the user profile with these settings from today and onwards.</asp:ListItem>
						                <asp:ListItem Value=0>Update the user profile as if these settings were set from start.</asp:ListItem>
						                <asp:ListItem Value=2>The previously registered email address has never been correct and the created account should be detached from organization.</asp:ListItem>
				                </asp:RadioButtonList><br />
			                </asp:PlaceHolder>
			                <span style="color:#CC0000;"><asp:Label ID=AddUserError runat=server /></span>
			                <asp:Button ID="CancelUser" Text="Cancel" CssClass=btn runat=server /><asp:Button ID="SaveUser" CssClass=btn Text="Save" runat=server />
		                </div>
		                </asp:PlaceHolder>
		                <asp:PlaceHolder ID="ImportUsers" runat=server Visible=false>
		                <div class="actionBlock">
			                <span class="descLong">Filename</span><input type=file class="inpt" ID=UsersFilename runat=server /><br/>
			                <span class="descLong">Default parent unit</span><asp:DropDownList CssClass="maxInpt" ID=ImportUsersParentDepartmentID runat=server /><br/>
			                <span style="color:#CC0000;"><asp:Label ID=ImportUsersError runat=server /></span>
			                <asp:Button CssClass=btn ID="CancelImportUser" Text="Cancel" runat=server /><asp:Button CssClass=btn ID="SaveImportUser" Text="Save" runat=server />
		                </div>
		                </asp:PlaceHolder>
		                <asp:PlaceHolder ID="DeleteUser" runat=server Visible=false>
		                <div class="actionBlock">
			                <span class="desc">Email</span><asp:TextBox CssClass="inpt" ReadOnly=true ID=DeleteUserEmail runat=server /><br />
			                If this user has activated the account, perform the following<br />
                            <asp:RadioButtonList ID=DeleteUserFrom runat=server RepeatDirection=Vertical RepeatLayout="Flow">
						            <asp:ListItem Value=1 Selected>From today and onwards, disassociate this user with the organization.</asp:ListItem>
						            <asp:ListItem Value=0>Disassociate this user with the organization from start.</asp:ListItem>
				            </asp:RadioButtonList><br />
			                <asp:Button CssClass=btn ID="CancelDeleteUser" Text="Cancel" runat=server /><asp:Button CssClass=btn ID="SaveDeleteUser" Text="Execute" runat=server />
		                </div>
		                </asp:PlaceHolder>
		                <asp:PlaceHolder ID="DeleteDepartment" runat=server Visible=false>
		                <div class="actionBlock">
			                <span class="desc">Unit</span><asp:TextBox ReadOnly=true ID=DeleteDepartmentName runat=server /><br />
			                <asp:Button CssClass=btn ID="CancelDeleteDepartment" Text="Cancel" runat=server /><asp:Button CssClass=btn ID="SaveDeleteDepartment" Text="Execute" runat=server />
		                </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="SearchResults" runat=server Visible=false>
                        <div class="actionBlock">
                            <table border="0" cellpadding="0" cellspacing="0">
	    		                <asp:Label ID="SearchResultList" runat=server />
		                    </table>
                        </div>
		                </asp:PlaceHolder>
                    </div>
                </div>
                <div>
                    <asp:Label ID=Actions runat=server Visible=false>
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
				</tr>
				<tr>
					<td valign="top" id="act1" runat=server>
					    <b>Action</b>
					    <br />
					    <a href=""><img src="img/unt_add.gif" border="0" />&nbsp;Add&nbsp;unit</a>
					    <br />
					    <a href=""><img src="img/usr_add.gif" border="0" />&nbsp;Add&nbsp;user</a>
					</td>
					<td width="5%" id="act2" runat=server>&nbsp;</td>
					<td valign="top" id="act3" runat=server>
					    &nbsp;
					    <br />
					    <a href=""><img src="img/unt_imp.gif" border="0" />&nbsp;Import&nbsp;units</a>
					    <br />
					    <a href=""><img src="img/usr_imp.gif" border="0" />&nbsp;Import&nbsp;users</a>
					</td>
					<td width="5%" id="act4" runat=server>&nbsp;</td>
					<td valign="top"><b>Search&nbsp;user</b><br />Email:&nbsp;&nbsp;</td>
					<td width="20%">&nbsp;</td>
					<td valign="top"><b>Legend</b><br /><img src="img/usr_on.gif" />&nbsp;Show&nbsp;members<br /><img src="img/key.gif"/>&nbsp;Display&nbsp;restricted</td>
					<td width="5%" id="act5" runat=server>&nbsp;</td>
					<td valign="top" id="act6" runat=server>&nbsp;<br /><img src="img/unt_edt.gif" />&nbsp;Edit&nbsp;unit<br /><img src="img/usr_edt.gif" />&nbsp;Edit&nbsp;user</td>
					<td width="5%" id="act7" runat=server>&nbsp;</td>
					<td valign="top" id="act8" runat=server>&nbsp;<br /><img src="img/unt_del.gif" />&nbsp;Delete&nbsp;unit<br /><img src="img/usr_del.gif"/>&nbsp;Delete&nbsp;user</td>
					<td width="20%" id="act9" runat=server>&nbsp;</td>
				</tr>
			</table>
		</asp:Label>
		            <asp:Label ID=OrgTree runat=server />
                </div>

            </div><!-- end .contentgroup	-->
        </div> <!-- end .container_12 -->

	</form>
  </body>
</html>
