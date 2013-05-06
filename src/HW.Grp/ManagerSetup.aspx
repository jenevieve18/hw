<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerSetup.aspx.cs" Inherits="HW.Grp.ManagerSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="settingsPane">
			    <asp:Button CssClass="btn" ID="Cancel" runat=server Text="Cancel" />
			    <asp:Button CssClass="btn" ID="Save" runat=server Text="Save" />
			    <asp:Label ID=ErrorMsg runat=server />
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
	            <tr><td><b>Organisation access</b></td></tr>
	            <asp:Label ID=OrgTree runat=server />
            </table>
        </div>

    </div>

</asp:Content>
