<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="HW.MobileApp.Form" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.4.2/jquery.mobile-1.4.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div data-role="header">
                <a href="Dashboard.aspx" data-icon="home">Cancel</a>
                <h1>Form</h1>
                <a href="#" data-icon="info" class="ui-btn-right">Save</a>
            </div>
            <div data-role="content">
                
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
