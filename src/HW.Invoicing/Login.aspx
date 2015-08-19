<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HW.Invoicing.Login" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="row clearfix">
	<div class="col-md-4 column">
        <h3>Log In</h3>

        <% if (errorMessage != null && errorMessage != "") { %>
            <div class="alert alert-danger">
                <%= errorMessage %>
            </div>
        <% } %>

        <div class="form-group">
	        <label for="<%= textBoxLoginName.ClientID %>">User name</label>
            <asp:TextBox ID="textBoxLoginName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxLoginPassword.ClientID %>">Password</label>
            <asp:TextBox ID="textBoxLoginPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>
                <asp:CheckBox ID="checkBoxRememberMe" runat="server" />
                Remember me
            </label>
        </div>
        <div>
            <asp:Button CssClass="btn btn-success" ID="buttonLogin" runat="server" Text="Log In" 
                onclick="buttonLogin_Click" />
        </div>
	</div>
	<div class="col-md-8 column">
	</div>
</div>


</asp:Content>
