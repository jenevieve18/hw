<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="HW.MobileApp.ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
    <style>
        .front_header {
text-align: center;
margin: 15px 0px 25px 0px;
font-size: 16px;
}
        .front_note {
max-width: 560px;
margin-left: auto;
margin-right: auto;
min-width: 268px;
padding: 15px 15px 70px 15px;
}
.front_logo {
width: 180px;
height: 126px;
margin-bottom: 30px;
display: inline-block;
vertical-align: bottom;
margin-right: 20px;
}
.front_controls {
max-width: 350px;
margin: 0 auto;
min-height: 250px;
display: inline-block;
min-width: 320px;
}
.center {
    text-align:center;
}
.front_header_img {
width: 235px;
}
</style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div data-role="header" data-theme="b" data-position="fixed">
                <a href="#" data-role="button" data-icon="back">Back</a>
                <h1>&nbsp;</h1>
            </div>
            <div data-role="content">
                <div class="front_note center">
                    <img class="front_logo" src="http://clients.easyapp.se/healthwatch/images/hw_logo@2x.png" />
                    <div class="front_controls">
                        <div class="front_header">
                            <h4 class="text">Forgot your password?</h4>
                            <h4 style="color:Red"><asp:Label ID="labelMessage" runat="server"></asp:Label></h4>
                            <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
                        </div>
                        <asp:TextBox ID="textBoxEmailAddress" runat="server" placeholder="Email address"></asp:TextBox>
                        <asp:Button ID="buttonSubmit" runat="server" Text="Submit" />
                    </div>
                </div>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="#" data-icon="home" class="ui-btn-active">My Health</a></li>
                        <li><a href="News.aspx" data-icon="calendar">News</a></li>
                        <li><a href="More.aspx" data-icon="bullets">More</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>