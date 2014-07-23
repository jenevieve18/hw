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
        .front_header_img width
        {
            width: 235px;
        }
        .nomargin{margin: 0px; padding:0px;display:inline;}
        
        .ui-checkbox .ui-btn-text {font-size:12px;}
        
    </style>
    <form id="form1" runat="server">
    <a href="Login.aspx" data-ajax="false" >
    
        <div data-role="page" >
            
                <div data-role="content"  style="height:100% !important;background-color: rgb(0,127,181) !important;">
                <div style="background-color: white;" class="ui-corner-all ui-shadow">
                    <img class="nomargin ui-corner-top" src="http://clients.easyapp.se/healthwatch//images/start_imgHeader@2x.png"/><img class="nomargin" style="margin-top:-4px;display:block;height:10px;width:100%;" src="http://clients.easyapp.se/healthwatch/images/divider.gif"/><img class="nomargin" src="http://clients.easyapp.se/healthwatch//images/start_catchPhrase@2x.png"/>
                    <div style="padding:8px;">
                    <p class="center" style="font-size:12px;"><asp:Label ID="lblWordsOfWisdom" runat="server"></asp:Label>
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