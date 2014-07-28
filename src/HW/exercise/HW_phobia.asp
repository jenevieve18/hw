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
<form name="stdfrm" method="post" action="PQLphobia.asp?Page=<%=rPage+1%>">
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
				<TR><TD CLASS="blueBold"><img src="/img/base/subArrowActiveH.gif" width="14" height="14" align="absmiddle">&nbsp;&nbsp;Fobiupplösaren - sida <%=rPage+1%> av 6</td></tr>
				<TR><TD>&nbsp;</TD></TR>
<%
Select Case rPage
	Case 0
%>
				<TR>
					<TD>
						Det finns flera strategier som kroppen tar till som skydd mot potentiella hot, obehag och oberäkneliga situationer. Vissa strategier fungerar bra och leder till att såväl kropp som själ utvecklas och andra fungerar mindre bra och kan på sikt medföra en större eller mindre begränsningar i livet. Generellt sett kan man säga att hjärnan älskar regelbundenhet. Hjärnan anpassar sig ständigt efter tillvarons krav och gärna vill ha kontroll över framtiden, vilket regelbundenhetens bidrag till "hjärnfriden". När hjärnan tror att den vet vad den har att vänta sig kan den lättare slappna. 
						<BR><BR>
						Vid olika former av fobier är framförallt emotioner (känslor) inblandade. Alla emotioner medför kroppsliga reaktioner av något slag. Dessa reaktioner kan vara mer eller mindre behagliga beroende på vilken typ av emotion som ligger bakom samt vilken intensitet emotionen har. Känslor kan bara upplevas i realtid vilket betyder att obehagskänslor som uppkommer när man minns en situation beror på hur man minns situationen. Det finns en naturlig tendens hon människor att återuppleva obehagliga situationer gång på gång, kanske med den omedvetna tron och förhoppningen att obehaget kommer att minska eller försvinna. I viss mån kan det vara så, men ofta sker det motsatta. Eftersom känslor endast kan upplevas i nuet så borde det vara tillräckligt att ha lidit av den obehagliga situationen när den skedde. Därför är det synd att upprepade gånger åter utsätta sig för lidandet. 
						<BR><BR>
						En fobireaktion, liksom en ångestreaktion, medför starka känslor av obehag och påverkar och till och med hämmar individers beteende. Det finns många olika slag av fobier som kan vara mer eller mindre uttalade och därmed även skapa varierande mängd obehag. Följande övning syftar till att "lösa upp" fobier. Den kliniska erfarenheten visar att denna övning i många fall kan lindra olika slag av fobier och även eliminera dem helt i vissa fall. För en långvarig effekt kan det vara bra att upprepa övningen vid några olika tillfällen, särskilt om man märker att besvären finns kvar. 
					</TD>
				</TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 1
%>
				<TR>
					<TD>
						<B>Förkunskapskrav:</B>
						<OL>
							<LI>Läs avsnittet om <i>association</i> och <i>dissociation</i> då kunskapen om dessa skillnader i perspektiv är avgörande för att nå framgång i denna övning. Det bästa sättet att få starka känslor från ett minne är att vara associerad i det. En förutsättning för att man ska kunna eliminera starka obehagskänslor som är kopplade till ett minne är att man dissocierar och därmed tittar på situationen utifrån.<BR>&nbsp;</LI>
							<LI>Läs avsnittet om <i>ankring</i> och utför gärna någon av ankringsövningarna.</LI>
						</OL>
						<B>Tips:</B> Det kan vara en fördel om du utför denna övning med hjälp av en vän som kan lotsa dig igenom övningen samt fungera som stöd om det behövs.
					</TD>
				</TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" WIDTH="35" HEIGHT="15" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 2
%>
				<TR>
					<TD>
						<B>Förberedelse</B>
						<BR><BR>
						A. Tänk på det som skapar din fobi. Det kan handla om ormar, folkmassor, att flyga eller vad det nu må vara. Tänk på det obehag som uppstår när du utsätts för identifiera vad det är för känsla du känner. Definiera känslan nedan.
						<BR><BR>
						<INPUT TYPE="text" NAME="MyPhobia" VALUE="<%=Request.Form("MyPhobia")%>" SIZE="40">
						<BR><BR>
						B. Hur starkt är obehaget när du tänker på det som framkallar din fobi?
					</TD>
				</TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls obehagligt","Maximalt obehagligt","darkGraySmall",False,"NULL",True %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" WIDTH="35" HEIGHT="15" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 3
%>				<INPUT TYPE="hidden" NAME="MyPhobia" VALUE="<%=Request.Form("MyPhobia")%>">
				<INPUT TYPE="hidden" NAME="EmotionOne" VALUE="<%=getVASvalue(1)%>">
				<TR>
					<TD>
						<B>Övningen</B>
						<OL>
							<LI>Du kanske kommer att väcka en del mycket obehagliga minnen när du jobbar med din fobi. Därför är det bra att skapa ett starkt trygghetsankare som du kan fokusera på om det skulle bli för jobbigt. Trygghetsankaret kan vara något så enkelt som en situation då du kände dig riktigt trygg. Om du inte har någon sådan kan du även skapa en påhittad situation. Ankra gärna denna trygghetskänsla via beröring på något behändigt ställe som du/din vän mycket lätt kommer åt, till exempel genom att hålla varandra i handen eller, om du är själv, sammanföra två fingrar. Försäkra dig om att beröringen verkligen framkallar trygghetskänslan. Du kan välja att hålla ankaret utlöst under hela processen eller utlösa det vid behov.<BR>&nbsp;</LI>
							<LI>Föreställ dig att du sitter i en biograf eller tittar på TV med en frusen stillbild i svartvitt på filmduken/bildskärmen. När detta är gjort kan du dissociera genom att föreställa dig att du nu går ut ur din kropp och glider ut ur situationen och alltså tittar på dig själv när du tittar på filmduken/TV:n. Filmen du ska titta på kommer att handla om den första gången du upplevde fobin.<BR>&nbsp;</LI>
							<LI>Gå nu bakåt i tiden till den första gången du upplevde den obehagliga händelsen/känslan eller till det tillfälle då fobin skapades. Om du inte minns första tillfället så kan du gå till det tidigast möjliga tillfället. Låt filmen börja lite före den obehagliga upplevelsen, det vill säga när du fortfarande kände dig trygg., och fortsätta igenom och förbi händelsen tills du åter kände dig trygg. Titta på dig själv när du tittar på filmen*. OBS! Om du i detta läge känner ett mycket starkt obehag ska du genast avbryta övningen och återgå till att utifrån betrakta när du tittar på filmen om dig själv. Obehaget tyder nämligen på att du associerat in dig i situationen och du ska vara dissocierad.<BR>&nbsp;</LI>
							<LI>I din hand har du en fjärrkontroll med vars hjälp du ska ändra filmens egenskaper så att den blir mindre obehaglig. Här kan du experimentera fritt. Minska/öka skärmens storlek, ljusmättnaden, färg/svartvitkontrasterna. Höj/sänk volymen, filmduken. För bilden närmare eller längre bort - allt med syftet att ta bort så mycket obehag som möjligt. Till exempel kanske filmen känns mindre obehaglig om den flyttas längre bort ifrån dig, ljuset tonas ned och bilden blir mindre. Experimentera fritt. Det här ett viktigt delmoment för att lära sig hantera upplevelsen och momentet kräver din fulla uppmärksamhet. Var kreativ och flexibel. Kom ihåg att du befinner dig i nuet och från håll betraktar dig själv när du ser på filmen om dig själv för länge sedan. Du kan när som helst utlösa trygghetsankaret om det skulle behövas. Kom ihåg att du är trygg när du sitter här och nu och i fantasin tittar på en film. Den här delen av övningen är klar när du har kunnat titta igenom hela filmen och känner dig trygg filmen igenom.<BR>&nbsp;</LI>
							<LI>Grattis! När du kommit till denna punkt ska du vara riktigt stolt över dig själv! Du har nu upplevt den här obehagliga situationen utan att bryta ihop av negativa känslor. Du kan nu byta position och gå in i dig själv där du sitter och tittar på filmen. Med andra ord sitter du nu och tittar på filmen genom dina gena ögon (associerat tillstånd).<BR>&nbsp;</LI>
							<LI>När du gjort detta och det känns relativt bra kan du kliva in i filmen för att ge stöd till dig själv när du upplever den obehagliga situationen. Ge dig själv det stöd du behöver. Det kan handla om uppmuntran, eller rent av ett scenario där du förklarar för dig själv att du kommer från framtiden och att du vet att du kommer att överleva den här situationen och klara den bra. Låt fantasin flöda, du kan, om du önskar, komma in i form av en insikt som smyger sig in i dig själv i den där situationen som upplevdes som så skrämmande. När du besöker dig själv i det förflutna vet du att du kan hantera situationen och det behöver du förmedla till dig själv. Dock är det ju så att det kan vara bra att bevara en viss hälsosam rädsla, eller mer respekt, för farliga situationer, till exempel i mötet med ormar eller spindlar, men den handikappande rädslan ska vara borta.<BR>&nbsp;</LI>
							<LI>När ditt yngre jag, som upplevde den obehagliga situationen, har förstått och känner sig lugn och trygg kan du flytta detta yngre jag från filmen in i dig själv här och nu. Det kan vara bra att nu sätta av en kort stund på några minuter i lugn och ro för att återhämta dig samt integrera de djupa förändringar som har ägt rum.<BR>&nbsp;</LI>
							<LI>När du känner dig lugn och sansad kan du slutföra och testa att övningen fungerat genom att i associerat tillstånd föreställa dig ett tillfälle i framtiden som skulle ha givit upphov till att fobin skulle ha utlösts. Det här kan ge upphov till lättare obehag, men inte en förlamande rädsla som tidigare.</LI>
						</OL>
					</TD>
				</TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" WIDTH="35" HEIGHT="15" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 4
%>				<INPUT TYPE="hidden" NAME="MyPhobia" VALUE="<%=Request.Form("MyPhobia")%>">
				<INPUT TYPE="hidden" NAME="EmotionOne" VALUE="<%=Request.Form("EmotionOne")%>">
				<TR>
					<TD>
						<B>Uppföljning</B>
						<BR><BR>
						A. Du angav tidigare att det som skapar din fobi är...
						<BR><BR>
						<B>&quot;<%=Request.Form("MyPhobia")%>&quot;</B>
						<BR><BR>
						B. Hur är obehaget när du tänker på det som tidigare framkallat din fobi?
					</TD>
				</TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls obehagligt","Maximalt obehagligt","darkGraySmall",False,"NULL",True %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD ALIGN="RIGHT"><A HREF="JavaScript:document.forms.stdfrm.submit();" ONFOCUS="this.blur();" onMouseOut="swapImgRestore();" onMouseOver="swapImage('nextButtonImg','','/img/button/nextH<%=Int("0" & Session("Lang"))%>.gif',1);"><IMG name="nextButtonImg" SRC="/img/button/nextN<%=Int("0" & Session("Lang"))%>.gif" WIDTH="35" HEIGHT="15" BORDER="0"></A></TD></TR>
			</table>
		</TD>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
<%
	Case 5
%>				<TR><TD><B>&quot;<%=Request.Form("MyPhobia")%>&quot;</B> var före övningen...</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls obehagligt","Maximalt obehagligt","darkGraySmall",False,Request("EmotionOne"),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>...och efter övningen...</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 2,"Inte alls obehagligt","Maximalt obehagligt","darkGraySmall",False,getVASvalue(1),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR>
					<TD>
						Alla personer har mer eller mindre små rädslor för olika saker. Det kan vara utvecklande för kropp och själ att lära sig hantera situationer som starkt begränsar oss. Dessutom kan det vara stärkande och mycket bra för självförtroendet. Genom att bli av med en besvärlig fobi kan du vara tacksam för den styrka och fokuseringsförmåga du besitter, som varit nödvändig för att du ska kunna ha lidit så mycket. Framför allt så kan du ju nu använda denna kraft och fokuseringsförmåga på saker som kan göra ditt liv så mycket roligare att leva. Kraften och intensiteten i känslorna för en fobi kan i en omvänd situation, till exempel en stormförälskelse, bidra till ett rus av oanade dimensioner. Med andra ord kan du genom att bearbeta dina fobier frigöra en stor kraft som kan användas för att öka din livskvalitet och ditt välbefinnande.
						<BR><BR>
						* Detta kallas dubbel dissociation och skapar en nödvändig emotionell distans till det som upplevs.
					</TD>
				</TR>
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
