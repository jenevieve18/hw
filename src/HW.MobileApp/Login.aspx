<%@ Page Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HW.MobileApp.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
}
.center {
    text-align:center;
}
.front_header_img {
width: 235px;
}
    </style>

            <div data-role="header" data-theme="b" data-position="fixed">
                <h1>&nbsp;</h1>
            </div>
            <div data-role="content">
                <div class="front_note center">
                    <img style="width:180px" class="front_logo" src="http://clients.easyapp.se/healthwatch/images/hw_logo@2x.png" />
                    <div class="front_controls">
                        <div class="front_header">
                            <h4 class="text">Login to a better life.</h4>
                            <h4 style="color:Red"><asp:Label ID="labelMessage" runat="server"></asp:Label></h4>
                            <img style="width:235px" class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
                        </div>
                        <asp:TextBox ID="textBoxUsername" runat="server" placeholder="Username or Email"></asp:TextBox>
                        <asp:TextBox ID="textBoxPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <asp:Button ID="buttonLogin" runat="server" Text="Log In" OnClick="LoginButtonClick" />
                        <a href="ForgotPassword.aspx" data-role="button">Forgot password?</a>
                    </div>
                </div>
            </div>

    

</asp:Content>