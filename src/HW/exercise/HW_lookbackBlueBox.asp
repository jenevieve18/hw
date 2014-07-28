<!--#include virtual="/includes/top.asp"-->
<!--#include virtual="/includes/VASv3.asp"-->
<%
rPage = Int("0" & Request("Page"))
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
	<title>PQL - myPQL - Återblicksövningen</title>
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
<body onload="initVAS();preloadImages('/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif');">
<form name="stdfrm" method="post" action="PQLlookback.asp?Page=<%=rPage+1%>">
<img src="/img/base/exercise<%=Int("0" & Session("Lang"))%>.gif"><BR><BR>
<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#F3F5F7">
	<tr> 
		<td><img SRC="/img/base/transCornerTopLeft.gif" width="8" height="8"></td>
		<td width="100%"><img SRC="/img/null.gif" width="1" height="1"></td>
		<td><img SRC="/img/base/transCornerTopRight.gif" width="8" height="8"></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>
			<table border="0" cellspacing="0" cellpadding="0" class="text">
				<TR><TD CLASS="blueBold"><img src="/img/base/subArrowActiveH.gif" width="14" height="14" align="absmiddle">&nbsp;&nbsp;Återblicksövningen - sida <%=rPage+1%> av 4</td></tr>
				<TR><TD>&nbsp;</TD></TR>
<%
Select Case rPage
	Case 0
%>
				<TR><TD>Denna övning är ämnad för att kunna förhålla sig annorlunda till ett problem eller situation som verkligen är besvärande.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>1. Ta fram ett problem som besvärar dig mycket. Hur mycket besvärar detta problem dig? Sätt ett kryss på linjen.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls","Maximalt","text",False,"NULL",True %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="#" onclick="document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" WIDTH="35" HEIGHT="15" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td>&nbsp;</td>
	</tr>
<%
	Case 1
%>
				<INPUT TYPE="hidden" NAME="LookbackOne" VALUE="<%=getVASvalue(1)%>">
				<TR><TD>2. Tänk dig att du har en farkost som du kan resa in i framtiden med. Jag vill be dig att resa <B>exakt</B> 100 år in i framtiden.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>3. Kliv ut ur farkosten och titta tillbaka på ditt problem i nutiden. Samla gärna ihop din upplevelse av problemet till en bild eller sekvens. Titta tillbaka på problemet. Vad tänker du när du 100 år framåt i tiden tittar på problemet?</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><I>OBS! Om du har väldigt starka känslor av besvär även 100 år framåt i tiden har du "rest tillbaka" till nutiden. Se då till att åka in i framtiden igen. Om du fortfarande skulle ha svårt att stanna kvar i framtiden så kan du skapa en behaglig miljö, till exempel en skön soffa där du kan blicka tillbaka på ditt problem via TV:n.</I></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>4. När du är klar med detta kan du kliva in i din farkost igen och åka tillbaka till nutiden.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="#" onclick="document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" WIDTH="35" HEIGHT="15" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td>&nbsp;</td>
	</tr>
<%
	Case 2
%>
				<INPUT TYPE="hidden" NAME="LookbackOne" VALUE="<%=Request("LookbackOne")%>">
				<TR><TD>5. Väl i nuet vill jag be dig att återigen tänka på ditt problem. Hur besvärligt känns det nu?</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls","Maximalt","text",False,"NULL",True %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="#" onclick="document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" WIDTH="35" HEIGHT="15" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td>&nbsp;</td>
	</tr>
<%
	Case 3
%>
				<tr><TD><I>Hur mycket besvärar detta problem dig?</i></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>Före tidsresa</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls","Maximalt","text",False,Request("LookbackOne"),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>Efter tidsresa</TD></TR>
				<TR><TD><% insertVAS 2,"Inte alls","Maximalt","text",False,getVASvalue(1),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>Med lite perspektiv kan det vara mycket lättare att förhålla sig annorlunda till ett problem eller en besvärande situation. Det kan även vara lättare att finna konstruktiva lösningar.</TD></TR>
			</table>
		</TD>
		<td>&nbsp;</td>
	</tr>
	<tr><td colspan="3"><img src="/img/null.gif" width="1" height="20"></td></tr>
	<tr>
		<td><img src="/img/null.gif" width="8" height="1"></td>
		<td width="100%" align="center" class="darkGraySmall">[<a href="javascript:close()">st&auml;ng f&ouml;nster</a>]</td>
		<td><img src="/img/null.gif" width="8" height="1"></td>
	</tr>
<%
End Select
%>
	<tr>
		<td><img SRC="/img/base/transCornerBottomLeft.gif" width="8" height="8"></td>
		<td width="100%"><img SRC="/img/null.gif" width="1" height="1"></td>
		<td><img SRC="/img/base/transCornerBottomRight.gif" width="8" height="8"></td>
	</tr>
</table>
<table border="0" width="100%"><tr><td align="center" class="darkGraySmall">&copy; Copyright. Denna övning får inte kopieras eller spridas utan medgivande från upphovsmännen.</td></tr></table>
<%
initVASvalues
%>
</form>
<%
initVAS
%>
</body>
<!--#include virtual="/includes/bot.asp"-->
