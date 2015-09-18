<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="HW.Invoicing.UserEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/bootstrap-colorpicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-colorpicker.min.js" type="text/javascript"></script>

    <script>
        $(function () {
            $('#<%= textBoxColor.ClientID %>').colorpicker();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit a user</h3>
<div class="form-group">
	<label for="<%= textBoxUsername.ClientID %>">User name</label>
    <asp:TextBox ID="textBoxUsername" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPassword.ClientID %>">Password</label>
    <asp:TextBox ID="textBoxPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxColor.ClientID %>">Color</label>
    <asp:TextBox ID="textBoxColor" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button ID="buttonSave" runat="server" Text="Save user" 
        onclick="buttonSave_Click" CssClass="btn btn-success" />
        or <i><%= HtmlHelper.Anchor("cancel", "users.aspx") %></i>
</div>

</asp:Content>
