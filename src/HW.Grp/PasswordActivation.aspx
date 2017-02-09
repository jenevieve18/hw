<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordActivation.aspx.cs" Inherits="HW.Grp.PasswordActivation" %>

<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>
<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>HealthWatch.se / Group administration
    </title>
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Expires" content="-1">
    <meta name="Robots" content="noarchive">
    <script language="JavaScript">    window.history.forward(1);</script>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <script type="text/javascript" src="assets/js/jquery-1.10.2.js"></script>
    <!--<script type="text/javascript">
    $(document).ready(function () {
        var descriptionS = $("#submenu .description").html();
        $("#submenu a").mouseover(function () {
            $("#submenu .description").html($(this).html());
            $("#submenu .active").css('background-position', 'center -80px');
        });
        $("#submenu a").mouseout(function () {
            $("#submenu .description").html(descriptionS);
            $("#submenu .active").css('background-position', 'center -120px');
        });
    });
	</script>-->
    <%--<style type="text/css">
        .i18n {
            background: url(https://healthwatch.se/includes/resources/rsaquo.gif) no-repeat 0 4px;
            padding-left: 7px;
        }
    </style>--%>
    <style type="text/css">
        #admin #contextbar .actionPane {
            border-top: 1px solid #b0e1f3;
        }
    </style>
    <script type="text/javascript">
        function openWindow(str) {
            alert(str);
            window.location = 'default.aspx';
        }
    </script>
    <link href="assets/css/960.css" type="text/css" rel="stylesheet">
    <link href="assets/css/admin.css" type="text/css" rel="stylesheet">
    <link href="assets/css/site.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">

        <div class="container_16" id="admin">
            <div class="headergroup grid_16">
                <div class="grid_3 alpha">
                    <img src="img/hwlogo.png" width="186" height="126" alt="HealthWatch group administrator">
                </div>
                <div class="grid_8 omega p2">
                    HealthWatch.se<br>
                    <%--<span style="font-size: 14px">
                        <%= R.Str(lid, "login.header", "Group administration")%>
                        <%= HtmlHelper.Anchor(R.Str(lid, "i18n", "På svenska"), string.Format("default.aspx?lid={1}&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery), lid == 1 ? 2 : 1), "class='i18n'")%>
                    </span>--%>
                    <span style="font-size: 14px;">
                        <%= R.Str(lid, "login.header", "Group administration")%>
                        <small>
                            <% if (lid != 1) { %>
                                <%= HtmlHelper.Anchor(R.Str(1, "i18n"), string.Format("default.aspx?lid=1&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                            <% } %>
                            <% if (lid != 2) { %>
                                <%= HtmlHelper.Anchor(R.Str(2, "i18n"), string.Format("default.aspx?lid=2&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                            <% } %>
                            <% if (lid != 4) { %>
                                <%= HtmlHelper.Anchor(R.Str(4, "i18n", "In German"), string.Format("default.aspx?lid=4&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                            <% } %>
                        </small>
                    </span>
                    <br>
                </div>
                <div class="logincontainer grid_5 alpha omega">
                    <div class="gears">
                        &nbsp;&nbsp;
					
                    </div>
                </div>
                <div id="submenu" class="grid_16 alpha">
                </div>
            </div>



            <div class="contentgroup grid_16">
                <div id="contextbar">
                    <div class="actionPane">
                        <div class="actionBlock">
                            <span style="width: 150px" class="desc"><%= R.Str(lid, "password.new", "New Password") %></span>
                            <asp:TextBox ID="textBoxPassword" runat="server" TextMode="Password"></asp:TextBox><br />
                            <span style="width: 150px" class="desc"><%= R.Str(lid, "password.confirm", "Confirm Password") %></span>
                            <asp:TextBox ID="textBoxConfirmPassword" runat="server" TextMode="Password"></asp:TextBox><br />
                            <span style="color: #CC0000;">
                                <asp:Label ID="labelErrorMessage" runat="server" /><br />
                            </span>
                            <asp:Button ID="buttonActivate" runat="server" Text="Activate Password"
                                OnClick="buttonActivate_Click" />
                        </div>
                    </div>
                </div>
            </div>


        </div>


    </form>
</body>
</html>
