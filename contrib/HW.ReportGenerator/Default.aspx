<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.ReportGenerator.Default" %>

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
    <div class="container">
      <div class="row">
        <div class="login"><img src="assets/img/hwlogo.png">
          <h3>Login</h3>
          <form action="dashboard.html">
            <div class="form-group">
              <input placeholder="Username or email address" class="form-control">
            </div>
            <div class="form-group">
              <input placeholder="Password" class="form-control">
            </div>
            <div class="form-group">
<asp:Button ID="buttonLogin" runat="server" Text="Login" CssClass="btn btn-default" />
            </div>
          </form>
        </div>
      </div>
    
    </div>
    </form>
</body>
</html>
