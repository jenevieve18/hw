<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="faqadd.aspx.cs" Inherits="HW.Adm.faqadd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
    <form id="form1" runat="server">
		<%=Db.nav()%>
        <table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Add FAQ</td></tr>
		</table>
        <table style="margin:20px;" border="0" cellspacing="0" cellpadding="0">
            <tr><td>Name</td><td>
                <asp:TextBox ID="textBoxName" runat="server"></asp:TextBox></td></tr>
            <asp:PlaceHolder ID="placeHolderLanguages" runat="server"></asp:PlaceHolder>
		</table>
        <button onclick="location.href='faq.aspx';">Cancel</button>
        <asp:Button ID="buttonSave" runat="server" Text="Save" OnClick="buttonSaveClick" />
		<%=Db.bottom()%>
    </form>
</body>
</html>
