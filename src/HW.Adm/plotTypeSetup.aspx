<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plotTypeSetup.aspx.cs" Inherits="HW.Adm.plotTypeSetup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <%=Db.header()%>
</head>
<body>
    <form id="form1" runat="server">
        <%=Db.nav()%>
        <table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Plot type setup</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>Name&nbsp;</td>
                <td>
                    <%--<asp:DropDownList ID="RequiredUserLevel" runat=server>
                        <asp:ListItem Value="0">End user</asp:ListItem>
                        <asp:ListItem Value="10">Manager</asp:ListItem>
                    </asp:DropDownList>--%>
                    <asp:TextBox ID="textBoxName" runat="server"></asp:TextBox>
                </td>
            </tr>
		    <tr>
                <td>Description&nbsp;</td>
                <td><asp:TextBox ID="textBoxDescription" runat="server" TextMode="MultiLine" Width="200" Height="50"></asp:TextBox></td>
            </tr>
            <%--<tr>
                <td>Category&nbsp;</td>
                <td><asp:DropDownList ID=ExerciseCategoryID runat=server /></td>
            </tr>
            <tr>
                <td>Image (optional)&nbsp;</td>
                <td><input type="file" runat=server id=ExerciseImg /></td>
            </tr>
            <tr>
                <td>Time (statistics)&nbsp;</td>
                <td><asp:TextBox ID=Minutes runat=server Width=35 />minutes</td>
            </tr>
            <tr>
                <td valign="top">Javascript (for exercise)</td><td><asp:TextBox ID=textBoxJavascript runat=server Width="800" Height="150" ViewStateMode="Inherit" TextMode="MultiLine" /></td>
            </tr>--%>
            <asp:PlaceHolder ID="placeHolderLanguages" runat=server />
		</table>
        <asp:Button runat="server" Text="Cancel" id="buttonCancel" OnClientClick="window.location='plottype.aspx'; return false;">   </asp:Button>
        <asp:Button ID="buttonSave" runat=server Text="Save" /><%-- Add
        <asp:DropDownList ID=ExerciseTypeID runat=server />--%>
        <%=Db.bottom()%>
    </form>
</body>
</html>
