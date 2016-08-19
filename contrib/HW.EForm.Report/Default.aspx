<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.EForm.Report.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="assets/css/main.css">
    <link rel="stylesheet" href="assets/css/login.css">

</head>
<body>
    <form id="form1" runat="server">
    <div class="container" style="margin-top:30px">
      <div class="row">

          <div class="panel panel-default login">
  <%--<div class="panel-heading">
    <h3 class="panel-title">Please sign in</h3>
  </div>--%>
  <div class="panel-body">
            
            <img src="assets/img/hwlogo.png">
          <h3>Login</h3>
            <% if (errorMessage != "") { %>
            <div class="alert alert-danger"><%= errorMessage %></div>
            <% } %>
            <div class="form-group">
                <asp:TextBox ID="textBoxEmail" runat="server" placeholder="Username or email address" CssClass="form-control"></asp:TextBox>
              <%--<input placeholder="Username or email address" class="form-control">--%>
            </div>
            <div class="form-group">
                <asp:TextBox ID="textBoxPassword" runat="server" placeholder="Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
              <%--<input placeholder="Password" class="form-control">--%>
            </div>
            <div class="form-group">
<asp:Button ID="buttonLogin" runat="server" Text="Login" CssClass="btn btn-default" />
            </div>
  </div>
</div>

       
      </div>
    
    </div>
    </form>
</body>
</html>
