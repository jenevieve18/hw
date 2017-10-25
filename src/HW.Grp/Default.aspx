<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.Grp.Default" %>

<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>HealthWatch.se / Group admin</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta name="Robots" content="noarchive" />
    <script type="text/javascript" language="JavaScript">window.history.forward(1);</script>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <%--<link rel="stylesheet" href="assets/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap-responsive.css" />--%>
    
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/2.3.2/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/2.3.2/css/bootstrap-responsive.min.css" />--%>

    <link rel="stylesheet" href="assets/bootstrap-datepicker/css/bootstrap-combined.min.css" />

    <link rel="stylesheet" href="assets/theme1/css/default.css" />

</head>
<body>
    <form id="form1" runat="server" class="form-signin">

        <header>
           <center>
            <asp:Image ID="Image1" runat="server" ImageUrl="assets/theme2/img/hwlogo.png" />
         
            <h2>
                <%= R.Str(lid, "login.header", "Group administration") %>
            </h2>
            </center>
        </header>

        <% if (errorMessage != "") { %>
            <div class="alert alert-error">
                <%= errorMessage %>
            </div>
        <% } %>

     
      <%if(Session["IPAddress"].ToString() == "Not RealmIdentifier"){ %>
        <p style="font-size: 14px">
            <small>
                <% if (lid != 1) { %>
                    <%= HtmlHelper.Anchor(R.Str(1, "i18n"), string.Format("default.aspx?lid=1&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                <% } %>
                <% if (lid != 2) { %>
                    <%= HtmlHelper.Anchor(R.Str(2, "i18n"), string.Format("default.aspx?lid=2&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                <% } %>
                <% if (lid != 4) { %>
                    <%= HtmlHelper.Anchor(R.Str(4, "i18n"), string.Format("default.aspx?lid=4&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                <% } %>
            </small>
        </p>
           <%= FormHelper.Input("ANV", "", string.Format("class='input-block-level' placeholder='{0}'", R.Str(lid, "user.name", "Email or Username")))%>
           <%= FormHelper.Password("LOS", "", string.Format("class='input-block-level' placeholder='{0}'", R.Str(lid, "user.password", "Password")))%>
       

            <button class="btn btn-large btn-info" type="submit">
                <i class="icon-circle-arrow-right"></i><%= R.Str(lid, "login.signin", "Sign in") %>
            </button>
	   <% } %>


        <%else {%>
        <p style="font-size: 14px; margin: 0 0 10px 55px !important">
            <small>
                <% if (lid != 1) { %>
                    <%= HtmlHelper.Anchor(R.Str(1, "i18n"), string.Format("default.aspx?lid=1&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                <% } %>
                <% if (lid != 2) { %>
                    <%= HtmlHelper.Anchor(R.Str(2, "i18n"), string.Format("default.aspx?lid=2&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                <% } %>
                <% if (lid != 4) { %>
                    <%= HtmlHelper.Anchor(R.Str(4, "i18n"), string.Format("default.aspx?lid=4&r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)), "class='i18n'")%>
                <% } %>
            </small>
        </p>
           <center>
                <button class="btn btn-large btn-info" runat="server" onserverclick="RedirectPage">
                    <i class="icon-circle-arrow-right"></i><%= R.Str(lid, "login.signinIDP", "Log in") %>
               </button>&nbsp;&nbsp;
           </center>


         <%   }%>


       <%-- <% if (adminNews.Count > 0) { %>
            <hr />
            <div class="news">
                <h4><%= R.Str(lid, "news", "News") %></h4>
                <% var i = 0; %>
                <% foreach (var n in adminNews) { %>
                <p>
                    <span class="date"><%= n.Date.Value.ToString("MMM d, yyyy", GetCultureInfo(lid)).ToUpper() %></span>
                    <%= n.News %>
                </p>
                <% if (i < adminNews.Count - 1) { %>
                    <hr />
                <% } %>
                    <% i++; %>
                <% } %>
            </div>
        <% } %>--%>
    </form>
      <%if (Session["IPAddress"].ToString() == "Not RealmIdentifier")
              { %>
    <div class="footer">
        &copy; Interactive Health Group <%= DateTime.Now.ToString("yyyy") %><br />
        Version <%= typeof(Default).Assembly.GetName().Version%>
        </div>
	   <% }
              else
              { %>
    <div class="footer">
        &copy; Interactive Health Group <%= DateTime.Now.ToString("yyyy") %><br />
        Version <%= typeof(Default).Assembly.GetName().Version%>
    </div>
    <% } %>
</body>
</html>
