<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.measureInfo" Codebehind="measureInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><%=healthWatch.Db.header()%></head>
<body>
    <form id="form1" runat="server">
		<asp:Label ID="Contents" runat="server" />
    </form>
</body>
</html>
