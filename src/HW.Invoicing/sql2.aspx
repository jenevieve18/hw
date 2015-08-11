<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sql2.aspx.cs" Inherits="HW.Invoicing.sql2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
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


    </div>
    </form>
</body>
</html>
