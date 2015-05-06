<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.chart" Codebehind="chart.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="/includes/css/960.css">
    <link type="text/css" rel="stylesheet" href="/includes/css/site.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="datagroup grid_16">
    <asp:PlaceHolder ID=charts runat=server />
    </div>
    </form>
</body>
</html>
