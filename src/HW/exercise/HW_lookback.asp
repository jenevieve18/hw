<!--#include virtual="/includes/topNoMyPQL.asp"-->
<%
If Int("0" & Session("UserID")) = 0 And Int("0" & Request.QueryString("AdminMode")) <> 1 Then
	Response.Clear
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Du har loggats ut pga inaktivitet."
		Case 1	Response.Write "You have been logged out because of inactivity."
	End Select
	Response.Flush
	Response.End
End If

rPage = Int("0" & Request("Page"))
%>
<!--#include virtual="/includes/VASv3.asp"-->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
	<title>PQL - myPQL - <%
Select Case Int("0" & Session("Lang"))
	Case 0	Response.Write "Återblicksövningen"
	Case 1	Response.Write "Retrospective Exercise"
End Select
%></title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<script language="JavaScript" type="text/JavaScript">
		window.history.forward(1);
		<!--#include virtual="/includes/jsFindObj.asp"-->
		<!--#include virtual="/includes/jsShowHideLayers.asp"-->
		<!--#include virtual="/includes/jsPreloadImages.asp"-->
		<!--#include virtual="/includes/jsSwapImg.asp"-->
		<!--#include virtual="/includes/initJsVASv3.asp"-->
	</script>
	<!--#include virtual="/includes/styleSheet.asp"-->
</head>
<body onload="initVAS();preloadImages('/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif');" marginwidth="0" marginheight="0" rightmargin="0" leftmargin="0" topmargin="0" bottommargin="0">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<form name="stdfrm" method="post" action="PQLlookback.asp?Page=<%=rPage+1%>">
	<tr>
		<td background="/img/exercise/topBgr.gif" colspan="3">
			<table border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td><img src="/img/null.gif" width="15" height="60"></td>
					<td valign="middle"><img src="/img/base/PQLlogoSmall.gif" width="57" height="36"></td>
					<td valign="middle"><img src="/img/null.gif" width="20" height="1"></td>
					<td valign="middle"><img src="/img/base/exerciseW<%=Int("0" & Session("Lang"))%>.gif" width="117" height="26"></td>
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
				<TR><TD CLASS="blueBold"><img src="/img/base/subArrowActiveH.gif" width="14" height="14" align="absmiddle">&nbsp;&nbsp;<%
Select Case Int("0" & Session("Lang"))
	Case 0	Response.Write "Återblicksövningen - sida " & rPage+1 & " av 4"
	Case 1	Response.Write "Retrospective Exercise - page " & rPage+1 & " of 4"
End Select
%></td></tr>
				<TR><TD>&nbsp;</TD></TR>
<%
Select Case rPage
	Case 0
		Select Case Int("0" & Session("Lang"))
			Case 0
%>
				<TR><TD>Denna övning är ämnad för att kunna förhålla sig annorlunda till ett problem eller situation som verkligen är besvärande.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>1. Ta fram ett problem som besvärar dig mycket. Hur mycket besvärar detta problem dig? Sätt ett kryss på linjen.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls","Maximalt","text",False,"NULL",True %></TD></TR>
<%
			Case 1
%>
				<TR><TD>This exercise is intended to give you the ability to take a different attitude to a problem or situation that is really bothers you.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>1. Think up a problem that really troubles you. How much does it bother you? Put an X on the line.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Not at all","Maximally","text",False,"NULL",True %></TD></TR>
<%
		End Select
%>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 1
%>
				<INPUT TYPE="hidden" NAME="LookbackOne" VALUE="<%=getVASvalue(1)%>">
<%
		Select Case Int("0" & Session("Lang"))
			Case 0
%>
				<TR><TD>2. Tänk dig att du har en farkost som du kan resa in i framtiden med. Jag vill be dig att resa <B>exakt</B> 100 år in i framtiden.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>3. Kliv ut ur farkosten och titta tillbaka på ditt problem i nutiden. Samla gärna ihop din upplevelse av problemet till en bild eller sekvens. Titta tillbaka på problemet. Vad tänker du när du 100 år framåt i tiden tittar på problemet?</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><I>OBS! Om du har väldigt starka känslor av besvär även 100 år framåt i tiden har du "rest tillbaka" till nutiden. Se då till att åka in i framtiden igen. Om du fortfarande skulle ha svårt att stanna kvar i framtiden så kan du skapa en behaglig miljö, till exempel en skön soffa där du kan blicka tillbaka på ditt problem via TV:n.</I></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>4. När du är klar med detta kan du kliva in i din farkost igen och åka tillbaka till nutiden.</TD></TR>
<%
			Case 1
%>
				<TR><TD>2. Imagine that you have a vehicle that will take you into the future. Now travel mentally <B>exactly</B> 100 years into the future.</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>3. Climb out of the vehicle and look back at your present problem. Try to gather together your experience of the problem into an image or a sequence. Look back at the problem. What do you think when you observe the problem from the perspective of 100 years in the future?</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><I>IMPORTANT! If you have very strong troubled feelings despite the 100-year perspective, then you have reverted to the present. Try once again to travel into the future. If you still find it difficult to stay in the future, you can create a pleasant environment, e.g. a comfortable sofa from which you can observe your problem on a TV.</I></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>4. When you have done this you can climb into your time-travel vehicle again and return to the present.</TD></TR>
<%
		End Select
%>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 2
%>
				<INPUT TYPE="hidden" NAME="LookbackOne" VALUE="<%=Request("LookbackOne")%>">
<%
		Select Case Int("0" & Session("Lang"))
			Case 0
%>
				<TR><TD>5. Väl i nuet vill jag be dig att återigen tänka på ditt problem. Hur besvärligt känns det nu?</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls","Maximalt","text",False,"NULL",True %></TD></TR>
<%
			Case 1
%>
				<TR><TD>5. When you're back, think again about your problem. How troublesome does it seem now?</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Not at all","Maximally","text",False,"NULL",True %></TD></TR>
<%
		End Select
%>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 3
		Select Case Int("0" & Session("Lang"))
			Case 0
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
<%
			Case 1
%>
				<tr><TD><I>How much does this problem bother you?</i></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>Before the time travel</TD></TR>
				<TR><TD><% insertVAS 1,"Not at all","Maximally","text",False,Request("LookbackOne"),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>After the time travel</TD></TR>
				<TR><TD><% insertVAS 2,"Not at all","Maximally","text",False,getVASvalue(1),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>A little perspective allows you to take a different attitude to a problem or a disturbing situation. It may also make it easier to find constructive solutions to the problem.</TD></TR>
<%
		End Select
%>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
	<tr><td colspan="3"><A NAME="bot"></A><img src="/img/null.gif" width="1" height="20"></td></tr>
	<tr><td colspan="3" align="center" class="darkGraySmall">[<a href="javascript:close()"><%
Select Case Int("0" & Session("Lang"))
	Case 0	Response.Write "st&auml;ng f&ouml;nster"
	Case 1	Response.Write "close window"
End Select
%></a>]</td></tr>
<%
End Select
%>
	<tr><td colspan="3"><img src="/img/null.gif" width="1" height="20"></td></tr>
	<tr>
		<td colspan="3" align="center" class="darkGraySmall">
<%
Select Case Int("0" & Session("Lang"))
	Case 0
%>
			&copy; Copyright 2003, Bengt Arnetz och Dan Hasson, Uppsala Universitet.
			<BR>
			Denna övning får inte kopieras, spridas eller ändras utan skriftligt medgivande från upphovsmännen.
<%
	Case 1
%>
			&copy; Copyright 2003, Bengt Arnetz and Dan Hasson, Uppsala Universitet.
			<BR>
			This exercise may not be copied, distributed or changed without written permission from the authors.
<%
End Select
%>
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
