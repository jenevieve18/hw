﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendReminders.aspx.cs" Inherits="HW.Grp.SendReminders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="buttonSendReminder" runat="server" Text="Send Reminders" OnClick="buttonSendReminder_Click" />
        <asp:Button ID="buttonRever" runat="server" Text="Revert SendReminderLastSent for 'iiiii', 'jay123' and 'kkkkk' to a month ago" OnClick="buttonRevert_Click" />
    </div>
    </form>
</body>
</html>