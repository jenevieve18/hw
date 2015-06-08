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
    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="160px" Width="440px" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button ID="Button1" runat="server" Text="Execute" onclick="Button1_Click" CssClass="btn btn-success" />
</div>

<br />
<asp:GridView ID="GridView1" runat="server" CssClass="table table-hover"></asp:GridView>

</asp:Content>
