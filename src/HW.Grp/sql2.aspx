<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sql2.aspx.cs" Inherits="HW.Grp.sql2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="settingsPane">
                <h3>Enter SQL Command</h3>
                <span style="font-style:italic;color:#ff5b2b">
                    <asp:Label ID="labelMessage" runat="server" Text=""></asp:Label>
                </span>
                
                <p>PIN: <asp:TextBox ID="textBoxPIN" runat="server" TextMode="Password"></asp:TextBox></p>
                <p><asp:TextBox ID="textBoxSql" runat="server" TextMode="MultiLine" Height="160px" 
                    Width="440px"></asp:TextBox></p>
                <p><asp:Button ID="buttonExecute" runat="server" Text="Execute" 
                    onclick="buttonExecute_Click" /></p>
            </div>
            <asp:GridView ID="gridViewResult" runat="server">
            </asp:GridView>
    </div>
    </form>
</body>
</html>
