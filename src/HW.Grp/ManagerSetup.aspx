<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerSetup.aspx.cs" Inherits="HW.Grp.ManagerSetup" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="settingsPane">
			    <asp:Button CssClass="btn" ID="Cancel" runat=server Text="Cancel" />
			    <asp:Button CssClass="btn" ID="Save" runat=server Text="Save" />
			    <!--<asp:Label ID=ErrorMsg runat=server />-->
			    <% if (errorMessage != "") { %>
			    	<span style="color:#cc0000;"><%= errorMessage %></span>
			    <% } %>
            </div>
        </div>

        <div class="smallContent"><br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
	                    <table border="0" cellpadding="0" cellspacing="0">
		                    <tr><td colspan="2"><b>Credentials</b></td></tr>
		                    <tr><td>Name&nbsp;</td><td><asp:TextBox ID="Name" Width=200 runat=server /></td></tr>
		                    <tr><td>Username&nbsp;</td><td><asp:TextBox ID="Usr" Width=200 runat=server /></td></tr>
		                    <tr><td>Password&nbsp;</td><td><asp:TextBox ID="Pas" TextMode=Password Width=200 runat=server /></td></tr>
		                    <tr><td>Email&nbsp;</td><td><asp:TextBox ID="Email" Width=200 runat=server /></td></tr>
                            <tr><td>Organization read only&nbsp;</td><td><asp:CheckBox ID=ReadOnly runat=server /></td></tr>
                        </table>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td valign="top">
	                    <table border="0" cellpadding="0" cellspacing="0">
		                    <tr><td><b>Roles</b></td></tr>
		                    <tr><td><asp:CheckBox ID="SuperUser" Text="Super user (can administer its own manager account, including all units)" runat=server /></td></tr>
		                    <tr><td><asp:CheckBoxList CellPadding=0 CellSpacing=0 ID="ManagerFunctionID" runat=server /></td></tr>
                        </table>
                    </td>
                </tr>
	        </table>
            <table border="0" cellpadding="0" cellspacing="0">
	            <tr>
					<td><b>Organisation access</b></td>
				</tr>
	            <asp:Label ID=OrgTree runat=server />
				<!--<tr>
					<td><%= Session["Sponsor"] %></td>
				</tr>
				<% bool[] DX = new bool[8]; %>
				<% if (sponsorAdminDepartments != null) { %>
					<% foreach (var d in sponsorAdminDepartments) { %>
					<% int depth = d.Department.Depth; %>
					<% DX[depth] = (d.Department.Siblings > 0); %>
					<tr>
						<td>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<% for (int i = 1; i <= depth; i++) { %>
											<% if (i == depth) { %>
												<%= HtmlHelper.Image(string.Format("img/{0}.gif", DX[i] ? "T" : "L"), "", 19, 20) %>
											<% } else { %>
												<%= HtmlHelper.Image(string.Format("img/{0}.gif", DX[i] ? "I" : "null"), "", 19, 20) %>
											<% } %>
										<% } %>
									</td>
									<td><%= FormHelper.CheckBox("DepartmentID", d.Department.Id.ToString(), !d.Admin.SuperUser) %>&nbsp;<%= d.Department.Name %>&nbsp;&nbsp;&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>
					<% } %>
				<% } %>-->
            </table>
        </div>

    </div>

</asp:Content>
