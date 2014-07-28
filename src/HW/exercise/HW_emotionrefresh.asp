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
	<title>PQL - myPQL - Känslosvalkaren</title>
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
				<TR><TD CLASS="blueBold"><img src="/img/base/subArrowActiveH.gif" width="14" height="14" align="absmiddle">&nbsp;&nbsp;Känslosvalkaren - sida <%=rPage+1%> av 3</td></tr>
				<TR><TD>&nbsp;</TD></TR>
<%
Select Case rPage
	Case 0
%>
				<TR><TD>Vid tillfällen då det känns jobbigt eller obehagligt att tänka på något besvärande är den här enkla övningen bra att ha i bakfickan. Exempel på situationer som känslosvalkaren kan vara särskilt användbar är: konflikter, otrevliga möten, kriser, sorgliga separationer, tala inför folk, rädslor, ilska eller andra obehagliga situationer som kan vara svåra att undvika. Det kan handla om att man tänker på dessa situationer mycket, eller att man rent av utsätts för dem på regelbunden basis. Känslosvalkaren kan fungera som en lemonad en varm sommardag. De första gångerna du utför övningen kan det vara bra om du kan sitta i en lugn miljö där du kan slappna av. När du är van vid att utföra den så kan du även göra den på plats när obehaget inträffar.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>1. Tänk på den situation som besvärar dig. Skapa gärna en konkret bild av situationen.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>2. Hur obehaglig känns den situationen? Klicka på skalan nedan.</TD></TR>
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
				<TR><TD>3. För nu in ljus över och i bilden. Låt nu bilden bli ljusare och ljusare. För in mer och mer ljus. För in så mycket ljus i bilden så att den till slut blir helt vit.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>4. Tänk nu åter på den situation som besvärade dig. Hur obehaglig känns situationen nu?</TD></TR>
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
				<TR><TD>Före känslosvalkan</TD></TR>
				<TR><TD><% insertVAS 1,"Mycket obehaglig","Mycket behaglig","text",False,Request("EmotionOne"),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>Efter känslosvalkan</TD></TR>
				<TR><TD><% insertVAS 2,"Mycket obehaglig","Mycket behaglig","text",False,getVASvalue(1),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>Gör gärna om övningen flera gånger tills upplevelsen känns mer behaglig än obehaglig. Ibland känns det helt bra redan efter första gången, ibland kan man behöva utföra övningen 3, 5 eller 10 gånger. Många gånger kan det vara svårt att skapa en konkret bild igen efter första eller andra gången. Det är bara bra. Ibland kanske bilden blir komisk, kanske förändras den och en sur min blir en glad, eller så får man en befriande insikt genom denna enkla övning.</TD></TR>
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
			Denna övning får inte kopieras, spridas eller ändras utan skriftligt medgivande från upphovsmännen.
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
