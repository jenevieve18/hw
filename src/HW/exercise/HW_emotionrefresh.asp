<%
Randomize()

If Int("0" & Session("UserID")) = 0 Then
	Response.Clear
	Response.Redirect "/default.asp?Rnd=" & Rnd*10000
	Response.Flush
	Response.End
End If

rPage = Int("0" & Request("Page"))
%>
<!--#include virtual="/includes/top.asp"-->
<!--#include virtual="/includes/VASv3.asp"-->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
	<title>PQL - myPQL - K�nslosvalkaren</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<script language="JavaScript" type="text/JavaScript">
	<!--//
		<!--#include virtual="/includes/jsFindObj.asp"-->
		<!--#include virtual="/includes/jsShowHideLayers.asp"-->
		<!--#include virtual="/includes/jsPreloadImages.asp"-->
		<!--#include virtual="/includes/jsSwapImg.asp"-->
		<!--#include virtual="/includes/initJsVASv3.asp"-->
	  //-->
	</script>
	<!--#include virtual="/includes/styleSheet.asp"-->
</head>
<body onload="initVAS();preloadImages('/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif');" marginwidth="0" marginheight="0" rightmargin="0" leftmargin="0" topmargin="0" bottommargin="0">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<form name="stdfrm" method="post" action="PQLemotionrefresh.asp?Page=<%=rPage+1%>">
	<tr>
		<td background="/img/exercise/topBgr.gif" colspan="3">
			<table border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td><img src="/img/null.gif" width="15" height="60"></td>
					<td valign="middle"><img src="/img/base/PQLlogoSmall.gif" width="57" height="36"></td>
					<td valign="middle"><img src="/img/null.gif" width="20" height="1"></td>
					<td valign="middle"><img src="/img/base/exerciseW0.gif" width="117" height="26"></td>
				</tr>
				<tr> 
					<td><img src="/img/null.gif" width="15" height="30"></td>
					<td valign="middle">&nbsp;</td>
					<td valign="middle">&nbsp;</td>
					<td valign="middle">&nbsp;</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr><td colspan="3"><img src="/img/null.gif" width="1" height="10"></td></tr>
	<tr>
		<td><img src="/img/null.gif" width="30" height="1"></td>
		<td>
			<table border="0" cellspacing="0" cellpadding="0" width="100%" class="text">
				<TR><TD CLASS="blueBold"><img src="/img/base/subArrowActiveH.gif" width="14" height="14" align="absmiddle">&nbsp;&nbsp;K�nslosvalkaren - sida <%=rPage+1%> av 3</td></tr>
				<TR><TD>&nbsp;</TD></TR>
<%
Select Case rPage
	Case 0
%>
				<TR><TD>Vid tillf�llen d� det k�nns jobbigt eller obehagligt att t�nka p� n�got besv�rande �r den h�r enkla �vningen bra att ha i bakfickan. Exempel p� situationer som k�nslosvalkaren kan vara s�rskilt anv�ndbar �r: konflikter, otrevliga m�ten, kriser, sorgliga separationer, tala inf�r folk, r�dslor, ilska eller andra obehagliga situationer som kan vara sv�ra att undvika. Det kan handla om att man t�nker p� dessa situationer mycket, eller att man rent av uts�tts f�r dem p� regelbunden basis. K�nslosvalkaren kan fungera som en lemonad en varm sommardag. De f�rsta g�ngerna du utf�r �vningen kan det vara bra om du kan sitta i en lugn milj� d�r du kan slappna av. N�r du �r van vid att utf�ra den s� kan du �ven g�ra den p� plats n�r obehaget intr�ffar.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>1. T�nk p� den situation som besv�rar dig. Skapa g�rna en konkret bild av situationen.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>2. Hur obehaglig k�nns den situationen? Klicka p� skalan nedan.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Mycket obehaglig","Mycket behaglig","text",False,"NULL",True %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 1
%>
				<INPUT TYPE="hidden" NAME="EmotionOne" VALUE="<%=getVASvalue(1)%>">
				<TR><TD>3. F�r nu in ljus �ver och i bilden. L�t nu bilden bli ljusare och ljusare. F�r in mer och mer ljus. F�r in s� mycket ljus i bilden s� att den till slut blir helt vit.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>4. T�nk nu �ter p� den situation som besv�rade dig. Hur obehaglig k�nns situationen nu?</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Mycket obehaglig","Mycket behaglig","text",False,"NULL",True %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" WIDTH="35" HEIGHT="15" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 2
%>
				<TR><TD>F�re k�nslosvalkan</TD></TR>
				<TR><TD><% insertVAS 1,"Mycket obehaglig","Mycket behaglig","text",False,Request("EmotionOne"),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>Efter k�nslosvalkan</TD></TR>
				<TR><TD><% insertVAS 2,"Mycket obehaglig","Mycket behaglig","text",False,getVASvalue(1),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>G�r g�rna om �vningen flera g�nger tills upplevelsen k�nns mer behaglig �n obehaglig. Ibland k�nns det helt bra redan efter f�rsta g�ngen, ibland kan man beh�va utf�ra �vningen 3, 5 eller 10 g�nger. M�nga g�nger kan det vara sv�rt att skapa en konkret bild igen efter f�rsta eller andra g�ngen. Det �r bara bra. Ibland kanske bilden blir komisk, kanske f�r�ndras den och en sur min blir en glad, eller s� f�r man en befriande insikt genom denna enkla �vning.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
	<tr><td colspan="3"><A NAME="bot"></A><img src="/img/null.gif" width="1" height="20"></td></tr>
	<tr><td colspan="3" align="center" class="darkGraySmall">[<a href="javascript:close()">st&auml;ng f&ouml;nster</a>]</td></tr>
<%
End Select
%>
	<tr><td colspan="3"><img src="/img/null.gif" width="1" height="20"></td></tr>
	<tr>
		<td colspan="3" align="center" class="darkGraySmall">
			&copy; Copyright 2003, Bengt Arnetz och Dan Hasson, Uppsala Universitet.
			<BR>
			Denna �vning f�r inte kopieras, spridas eller �ndras utan skriftligt medgivande fr�n upphovsm�nnen.
		</td>
	</tr>
	<tr><td colspan="3"><img src="/img/null.gif" width="1" height="10"></td></tr>
<%
initVASvalues
%>
</form>
</table>
<%
initVAS
%>
</body>
<!--#include virtual="/includes/bot.asp"-->
