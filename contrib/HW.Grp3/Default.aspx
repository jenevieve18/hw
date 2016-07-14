<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.Grp3.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Administration | HealthWatch</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-2.2.4.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css" />

    <style>
        body
        {
            background: #efefef;
        }
        #form1
        {
            width: 300px;
            margin: auto;
            background: white;
            border: 1px solid #cccccc;
            border-radius: 3px;
            padding: 20px;
            margin: 20px auto 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
            <img src="assets/img/hwlogo.png" alt="HealthWatch group administration" />
            <h4>Group administration</h4>
            <p>
                <small>
                    <a href="default.aspx?lid=1&amp;r=%2fdefault.aspx" class="i18n">På svenska</a>
                    <a href="default.aspx?lid=2&amp;r=%2fdefault.aspx" class="i18n">In English</a>
                    <a href="default.aspx?lid=4&amp;r=%2fdefault.aspx" class="i18n">Auf Deutsch</a>
                </small>
            </p>
            <div class="form-group">
                <asp:TextBox ID="textBoxUsername" placeholder="Username or email address" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="textBoxPassword" placeholder="Password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="buttonLogin" CssClass="btn btn-lg btn-info" runat="server" Text="Login" />
            </div>
        </div>
        <div class="news">
            <h4>News</h4>
            <% for (int i = 0; i < 3; i++) { %>
                <p>
                    <span class="text-muted">OCT 17, 2014</span>
                    Version 2.6 published. Alphabetical sorting of managers, bug fixes.
                </p>
                <% if (i < 3 - 1) { %>
                    <hr />
                <% } %>
            <% } %>
        </div>
    </form>
</body>
</html>
