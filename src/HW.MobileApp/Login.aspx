<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HW.MobileApp.Login" %>
<%@ Import Namespace="HW.MobileApp" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="stylesheet" href="/custom.css" />
    <link rel="Stylesheet" href="css/custom.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
 
    
    <script>
        $.mobile.ajaxEnabled = false;
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
        <div data-role="page" id="login">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1>&nbsp;</h1>
            </div>
            <div data-role="content" class="nopadding">
                <div class="front_note center">
                    <img class="front_logo" src="http://clients.easyapp.se/healthwatch/images/hw_logo@2x.png" />
                    <div class="front_controls">
                        <div class="front_header">
                            <h4><asp:Label ID="labelMessage" runat="server"></asp:Label></h4>
                            <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
                        </div>
                        <asp:TextBox ID="textBoxUsername" runat="server" placeholder="Username or Email"></asp:TextBox>
                        <asp:TextBox ID="textBoxPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <fieldset data-role="controlgroup">
                           <asp:Button ID="buttonLogin" runat="server" Text="Log In" OnClick="LoginButtonClick" />
                        </fieldset>
                        
                        <div class="ui-grid-a">
                            <div class="ui-block-a">
                                <fieldset data-role="controlgroup">
                                    <a href="Register.aspx" data-role="button" rel="external" id="create"><%= R.Str("user.createAccount") %></a>
                                </fieldset>
                            </div>
                            <div class="ui-block-b">
                                <fieldset data-role="controlgroup">
                                    <a href="ForgotPassword.aspx" data-role="button" id="forget"><%= R.Str("user.forgotPassword") %></a>        
                                </fieldset>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health"><%= R.Str("home.myHealth") %></a></li>
                        <li><a href="News.aspx" data-icon="news"><%= R.Str("home.news") %></a></li>
                        <li><a href="More.aspx" data-icon="more"><%= R.Str("home.more") %></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
