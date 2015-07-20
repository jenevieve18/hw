<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sql.aspx.cs" Inherits="HW.Adm.sql" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="textBoxSql" runat="server" TextMode="MultiLine" Width="400px" 
            Height="200px"></asp:TextBox>
        <br />
        <asp:Label ID="labelMessage" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="buttonExecute" runat="server" Text="Execute" OnClick="buttonExecuteClick" />
        <br /><br />
        <asp:GridView ID="gridViewResult" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
