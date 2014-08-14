<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HW.MobileApp.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="stylesheet" href="/custom.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
 
    
    <style>
        #blue {
            background-color: rgb(0,127,181);
        }
        #red {
            background-color: rgb(220,0,102);
        }
        #green {
            background-color: rgb(189,209,66);
        }
        #orange {
            background-color: rgb(241,136,7);
        }
        img { width:100%; }
    </style>
</head>
<body>
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
            /*max-width: 350px;*/
            margin: 0 auto;
            /*min-height: 250px;*/
            display: inline-block;
            /*min-width: 320px;*/
            margin-top:-20px;
            padding:0px 0px 0px 0px;
        }
        .center {
            text-align:center;
        }
        .front_header_img {
            width: 235px;
            
        }
        h4 { margin:0px 0px 0px 0px;}
        
        .ui-controlgroup-controls {  min-width:100% !important;}
        .ui-controlgroup-controls .ui-btn-text { font-size:12px;}
        .ui-grid-a .ui-block-a{ width:40%; }
        .ui-grid-a .ui-block-b{ margin-left:20px;width:50%; }
        .ui-grid-a .ui-block-a .ui-btn{ width:127px; }
        .ui-grid-a .ui-block-b .ui-btn{ width:143px; }
        
    </style>
    
    <form id="form1" runat="server">
        <div data-role="page" id="login">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1>&nbsp;</h1>
            </div>
            <div data-role="content" style="padding:0px 0px 0px 0px;">
                <div class="front_note center">
                    <img style="width:180px" class="front_logo" src="http://clients.easyapp.se/healthwatch/images/hw_logo@2x.png" />
                    <div class="front_controls">
                        <div class="front_header">
                            <h4><asp:Label ID="labelMessage" runat="server"></asp:Label></h4>
                            <img style="width:235px" class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
                        </div>
                        <asp:TextBox ID="textBoxUsername" runat="server" placeholder="Username or Email"></asp:TextBox>
                        <asp:TextBox ID="textBoxPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <fieldset data-role="controlgroup">
                           <asp:Button ID="buttonLogin" runat="server" Text="Log In" OnClick="LoginButtonClick" />
                        </fieldset>
                        
                <div class="ui-grid-a">
                <div class="ui-block-a">
                    <fieldset data-role="controlgroup">
                    <a href="Register.aspx" data-role="button" rel="external" id="create">Create account</a>
                    </fieldset>
                </div>
                <div class="ui-block-b">
                    <fieldset data-role="controlgroup">
                    <a href="ForgotPassword.aspx" data-role="button" id="forget">Forgot password?</a>        
                    </fieldset>
                </div>
                </div>
          
        
                            
                        
                    </div>
                </div>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="home">My Health</a></li>
                        <li><a href="News.aspx" data-icon="grid">News</a></li>
                        <li><a href="More.aspx" data-icon="info">More</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
