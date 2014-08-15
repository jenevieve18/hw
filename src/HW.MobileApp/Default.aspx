<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.MobileApp.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="stylesheet" href="/custom.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
 
    
    
</head>
<body >
    
    <form id="form1" runat="server">
    <a href="Login.aspx" data-ajax="false" >
    
        <div data-role="page" id="default">
            
                <div data-role="content"  >
                <div class="frame ui-corner-all ui-shadow">
                    <img class="nomargin ui-corner-top" src="http://clients.easyapp.se/healthwatch//images/start_imgHeader@2x.png"/><img class="slogan" src="http://clients.easyapp.se/healthwatch/images/divider.gif"/><img class="nomargin" src="http://clients.easyapp.se/healthwatch//images/start_catchPhrase@2x.png"/>
                    <div class="padding8">
                    <p class="center fsize12" ><asp:Label ID="lblWordsOfWisdom" runat="server"></asp:Label>
                    <br />-<i> <asp:Label ID="lblAuthor" runat="server"></asp:Label></i>
                    </p>
                    <h2 class="center">Tap to Start</h2>
                    </div>
                </div>
                
                <asp:CheckBox runat="server" ID="cbSplash" AutoPostBack="true" 
                        Text="Always show on start" data-mini="true" 
                        oncheckedchanged="cbSplash_CheckedChanged" />
                </div>
            
        </div>
        </a>
    </form>
</body>
</html>