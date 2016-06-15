<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managerFuncAdd.aspx.cs" Inherits="HW.Adm.managerFuncAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

  <head>
   <%=Db.header()%>
  </head>
  <body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="buttonSave">
		<%=Db.nav()%>
        <table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Add Manager Function</td></tr>
		</table>
        <table style="margin:20px;" border="0" cellspacing="0" cellpadding="0">
            <tr><td>URL</td><td>
                <asp:TextBox ID="textBoxUrl" runat="server"></asp:TextBox></td></tr>
            <asp:PlaceHolder ID="placeHolderLanguages" runat="server"></asp:PlaceHolder>
		</table>
        <asp:Button runat="server" Text="Cancel" id="buttonCancel" OnClientClick="window.location='managerFunc.aspx'; return false;">   </asp:Button>
        <asp:Button ID="buttonSave" runat="server" Text="Save" OnClick="buttonSaveClick" />
		<%=Db.bottom()%>
            </asp:Panel>
    </form>
</body>
</html>
