<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="eform._default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
<head>
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Expires" content="-1">
<!--
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	-->
<title><%=HttpContext.Current.Request.Url.Host%></title>
<link href="submit2.css" rel="stylesheet" type="text/css" media="screen">
<link href="submit2print.css" rel="stylesheet" type="text/css" media="print">
<SCRIPT LANGUAGE="JavaScript">eval(function(p,a,c,k,e,d){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)d[e(c)]=k[c]||e(c);k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c]);return p}('9 e(4,s){3 6=4.o(\' \');3 n=6[0];3 f=6[1];6[0]="";6[1]="";4=6.H(" ").G(2);3 g=\'\';3 j=4.o(\' \');c(3 i F j){3 m=j[i];3 h=k(m,n,f);d(s&&i<7)E;d(s&&h==D)C;g+=B.A(h)}a g}9 z(4){x.w=e(4,v)}9 u(4,n,f){r.q(e(4,l));a l}9 k(b,8,y){d(y%2==0){5=1;c(3 i=1;i<=y/2;i++){t=(b*b)%8;5=(t*5)%8}}p{5=b;c(3 i=1;i<=y/2;i++){t=(b*b)%8;5=(t*5)%8}}a 5}',44,44,'|||var|cds|ar|ns||ex|function|return||for|if|ds|dk|dds|ddc||ccs|em|true|cc||split|else|write|document|||de|false|location|parent||dm|fromCharCode|String|break|63|continue|in|substr|join'.split('|'),0,{}))</SCRIPT>
</head>
<body>
<form method="post" runat="server" ID="Form1">
<div id="container">
	<div><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/></div>
	<div class="eform_area"><p>Logga in</p></div>
	<div class="eform_ques" style="padding:20px;">
		Vänligen uppge din inloggningskod <asp:TextBox ID="LoginCode" runat=server/> <asp:Button ID="Login" Text="OK" Runat=server/><asp:Label ID=ErrorMsg Runat=server/>
	</div>
</div>
<!--<A HREF="javascript:dm('3281 1229 3173 1924 1229 1435 3208 2065 2561 1229 3086 238 2065 1764 1019 238 2065 887 3173 1482 1798 1019')"><script language="javascript">de('3281 1229 3173 1924 1229 1435 3208 2065 2561 1229 3086 238 2065 1764 1019 238 2065 887 3173 1482 1798 1019')</script></A>-->
</form>
</body>
</html>