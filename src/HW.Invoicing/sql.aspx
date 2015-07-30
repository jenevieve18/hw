<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="sql.aspx.cs" Inherits="HW.Invoicing.sql" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Enter SQL Command</h3>
<% if (message != null && message != "") { %>
    <div class="alert alert-danger"><%= message %></div>
<% } %>
<%-- <asp:Label ID="Label1" runat="server" Text="" CssClass="alert alert-danger"></asp:Label>--%>
<div class="form-group">
    <label for="<%= textBoxPIN.ClientID %>">PIN</label>
    <asp:TextBox ID="textBoxPIN" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
</div>
<div class="form-group">
    <asp:TextBox ID="textBoxSqlStatement" runat="server" TextMode="MultiLine" Height="160px" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button ID="buttonExecute" runat="server" Text="Execute" onclick="buttonExecute_Click" CssClass="btn btn-success" />
</div>

<br />
<asp:GridView ID="gridViewResult" runat="server" CssClass="table table-hover"></asp:GridView>

</asp:Content>
