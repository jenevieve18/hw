<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExerciseShow.aspx.cs" Inherits="HW.Grp.ExerciseShow" %>
<%@ Import Namespace="HW.Core.FromHW" %>
<!doctype html>
<html lang="en" class="no-js">
<head runat="server">
    <meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<meta http-equiv="Pragma" content="no-cache">
	<meta http-equiv="Expires" content="-1">
	<meta name="Robots" content="noarchive">
    <meta name="description" content="Exercises">

	<title><%= langId == 1 ? "Övningar" : "Exercises"%> - HealthWatch</title>
	
    <link rel="shortcut icon" href="favicon.ico">
	<link rel="apple-touch-icon" href="apple-touch-icon.png">
	
    <link type="text/css" rel="stylesheet" href="includes2/css/960.css">
	<link type="text/css" rel="stylesheet" href="includes2/css/site.css">
	
    <link type="text/css" href="includes/ui/css/ui-lightness/jquery-ui-1.8.11.custom.css" rel="Stylesheet">
	
    <script type="text/javascript" src="includes/ui/js/jquery-1.5.1.min.js"></script>
	<script type="text/javascript" src="includes/ui/js/jquery-ui-1.8.11.custom.min.js"></script>
	
    <script type="text/JavaScript">	    eval(function (p, a, c, k, e, d) { e = function (c) { return (c < a ? '' : e(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36)) }; if (!''.replace(/^/, String)) { while (c--) d[e(c)] = k[c] || e(c); k = [function (e) { return d[e] } ]; e = function () { return '\\w+' }; c = 1 }; while (c--) if (k[c]) p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]); return p } ('9 e(4,s){3 6=4.o(\' \');3 n=6[0];3 f=6[1];6[0]="";6[1]="";4=6.H(" ").G(2);3 g=\'\';3 j=4.o(\' \');c(3 i F j){3 m=j[i];3 h=k(m,n,f);d(s&&i<7)E;d(s&&h==D)C;g+=B.A(h)}a g}9 z(4){x.w=e(4,v)}9 u(4,n,f){r.q(e(4,l));a l}9 k(b,8,y){d(y%2==0){5=1;c(3 i=1;i<=y/2;i++){t=(b*b)%8;5=(t*5)%8}}p{5=b;c(3 i=1;i<=y/2;i++){t=(b*b)%8;5=(t*5)%8}}a 5}', 44, 44, '|||var|cds|ar|ns||ex|function|return||for|if|ds|dk|dds|ddc||ccs|em|true|cc||split|else|write|document|||de|false|location|parent||dm|fromCharCode|String|break|63|continue|in|substr|join'.split('|'), 0, {}))</script>
	<script type="text/JavaScript">	    AC_FL_RunContent = 0;</script><script src="/AC_RunActiveContent.js" language="javascript"></script>
	<script type="text/javascript">	    $(document).ready(function () { $("#hide").toggle(function () { $("#underlay").hide(); }, function () { $("#underlay").show(); }); $("#hide_ol").toggle(function () { $(".index").hide(); }, function () { $(".index").show(); }); $('[placeholder]').focus(function () { var input = $(this); if (input.val() == input.attr('placeholder')) { input.val(''); input.removeClass('placeholder'); } }).blur(function () { var input = $(this); if (input.val() == '' || input.val() == input.attr('placeholder')) { input.addClass('placeholder'); input.val(input.attr('placeholder')); } }).blur(); });</script>
	<script type="text/JavaScript">	    window.history.forward(1);</script>

    <script type="text/javascript">        $("body").addClass("popup"); $(document).ready(function () { $("body").addClass("popup"); });</script>
    <script src="AC_ActiveX.js" type="text/javascript"></script>
</head>

<!--[if lt IE 7 ]> <body class="ie6" class="popup"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7" class="popup"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8" class="popup"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9" class="popup"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body class="popup"> <!--<![endif]-->

    <form id="Form1" method="post" runat="server">

        <input id="sponsorID" type="hidden" value="<%= sponsorId %>" />
        <input id="sponsorAdminID" type="hidden" value="<%= sponsorAdminID %>" />
        <input id="exerciseVariantLangID" type="hidden" value="<%= exerciseVariantLangId %>" />

        <div class="popupie">
	        <div class="header">
		        <h1>HealthWatch.se<%= headerText %></h1>
                <a href="#" id="printBtn" onclick="window.print();return false;" class="print">
                    <%= langId == 1 ? "Skriv ut" : "Print" %>
                </a>
	        </div>
	        <div class="content">
                <img src="img/hwlogosmall.gif" />
                <%= logos %>
                <br /><br />
		        <asp:PlaceHolder id="exercise" runat="server"/>		
		        <% if (evl != null && evl.Variant.Exercise.PrintOnBottom) { %>
			        <br><br>
			        <a href='#' id='printBtn2' onclick='window.print();return false;' class='print'>
				        <%= langId == 1 ? "Skriv ut" : "Print" %>
			        </a>
		        <% } %>
            </div>
            <div class="footer">&copy; <%=DateTime.Now.Year%> www.healthwatch.se</div>
        </div>

    </form>
</body>
</html>