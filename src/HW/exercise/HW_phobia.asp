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
				<TR><TD CLASS="blueBold"><img src="/img/base/subArrowActiveH.gif" width="14" height="14" align="absmiddle">&nbsp;&nbsp;Fobiuppl�saren - sida <%=rPage+1%> av 6</td></tr>
				<TR><TD>&nbsp;</TD></TR>
<%
Select Case rPage
	Case 0
%>
				<TR>
					<TD>
						Det finns flera strategier som kroppen tar till som skydd mot potentiella hot, obehag och ober�kneliga situationer. Vissa strategier fungerar bra och leder till att s�v�l kropp som sj�l utvecklas och andra fungerar mindre bra och kan p� sikt medf�ra en st�rre eller mindre begr�nsningar i livet. Generellt sett kan man s�ga att hj�rnan �lskar regelbundenhet. Hj�rnan anpassar sig st�ndigt efter tillvarons krav och g�rna vill ha kontroll �ver framtiden, vilket regelbundenhetens bidrag till "hj�rnfriden". N�r hj�rnan tror att den vet vad den har att v�nta sig kan den l�ttare slappna. 
						<BR><BR>
						Vid olika former av fobier �r framf�rallt emotioner (k�nslor) inblandade. Alla emotioner medf�r kroppsliga reaktioner av n�got slag. Dessa reaktioner kan vara mer eller mindre behagliga beroende p� vilken typ av emotion som ligger bakom samt vilken intensitet emotionen har. K�nslor kan bara upplevas i realtid vilket betyder att obehagsk�nslor som uppkommer n�r man minns en situation beror p� hur man minns situationen. Det finns en naturlig tendens hon m�nniskor att �teruppleva obehagliga situationer g�ng p� g�ng, kanske med den omedvetna tron och f�rhoppningen att obehaget kommer att minska eller f�rsvinna. I viss m�n kan det vara s�, men ofta sker det motsatta. Eftersom k�nslor endast kan upplevas i nuet s� borde det vara tillr�ckligt att ha lidit av den obehagliga situationen n�r den skedde. D�rf�r �r det synd att upprepade g�nger �ter uts�tta sig f�r lidandet. 
						<BR><BR>
						En fobireaktion, liksom en �ngestreaktion, medf�r starka k�nslor av obehag och p�verkar och till och med h�mmar individers beteende. Det finns m�nga olika slag av fobier som kan vara mer eller mindre uttalade och d�rmed �ven skapa varierande m�ngd obehag. F�ljande �vning syftar till att "l�sa upp" fobier. Den kliniska erfarenheten visar att denna �vning i m�nga fall kan lindra olika slag av fobier och �ven eliminera dem helt i vissa fall. F�r en l�ngvarig effekt kan det vara bra att upprepa �vningen vid n�gra olika tillf�llen, s�rskilt om man m�rker att besv�ren finns kvar. 
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
						<B>F�rkunskapskrav:</B>
						<OL>
							<LI>L�s avsnittet om <i>association</i> och <i>dissociation</i> d� kunskapen om dessa skillnader i perspektiv �r avg�rande f�r att n� framg�ng i denna �vning. Det b�sta s�ttet att f� starka k�nslor fr�n ett minne �r att vara associerad i det. En f�ruts�ttning f�r att man ska kunna eliminera starka obehagsk�nslor som �r kopplade till ett minne �r att man dissocierar och d�rmed tittar p� situationen utifr�n.<BR>&nbsp;</LI>
							<LI>L�s avsnittet om <i>ankring</i> och utf�r g�rna n�gon av ankrings�vningarna.</LI>
						</OL>
						<B>Tips:</B> Det kan vara en f�rdel om du utf�r denna �vning med hj�lp av en v�n som kan lotsa dig igenom �vningen samt fungera som st�d om det beh�vs.
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
						<B>F�rberedelse</B>
						<BR><BR>
						A. T�nk p� det som skapar din fobi. Det kan handla om ormar, folkmassor, att flyga eller vad det nu m� vara. T�nk p� det obehag som uppst�r n�r du uts�tts f�r identifiera vad det �r f�r k�nsla du k�nner. Definiera k�nslan nedan.
						<BR><BR>
						<INPUT TYPE="text" NAME="MyPhobia" VALUE="<%=Request.Form("MyPhobia")%>" SIZE="40">
						<BR><BR>
						B. Hur starkt �r obehaget n�r du t�nker p� det som framkallar din fobi?
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
						<B>�vningen</B>
						<OL>
							<LI>Du kanske kommer att v�cka en del mycket obehagliga minnen n�r du jobbar med din fobi. D�rf�r �r det bra att skapa ett starkt trygghetsankare som du kan fokusera p� om det skulle bli f�r jobbigt. Trygghetsankaret kan vara n�got s� enkelt som en situation d� du k�nde dig riktigt trygg. Om du inte har n�gon s�dan kan du �ven skapa en p�hittad situation. Ankra g�rna denna trygghetsk�nsla via ber�ring p� n�got beh�ndigt st�lle som du/din v�n mycket l�tt kommer �t, till exempel genom att h�lla varandra i handen eller, om du �r sj�lv, sammanf�ra tv� fingrar. F�rs�kra dig om att ber�ringen verkligen framkallar trygghetsk�nslan. Du kan v�lja att h�lla ankaret utl�st under hela processen eller utl�sa det vid behov.<BR>&nbsp;</LI>
							<LI>F�rest�ll dig att du sitter i en biograf eller tittar p� TV med en frusen stillbild i svartvitt p� filmduken/bildsk�rmen. N�r detta �r gjort kan du dissociera genom att f�rest�lla dig att du nu g�r ut ur din kropp och glider ut ur situationen och allts� tittar p� dig sj�lv n�r du tittar p� filmduken/TV:n. Filmen du ska titta p� kommer att handla om den f�rsta g�ngen du upplevde fobin.<BR>&nbsp;</LI>
							<LI>G� nu bak�t i tiden till den f�rsta g�ngen du upplevde den obehagliga h�ndelsen/k�nslan eller till det tillf�lle d� fobin skapades. Om du inte minns f�rsta tillf�llet s� kan du g� till det tidigast m�jliga tillf�llet. L�t filmen b�rja lite f�re den obehagliga upplevelsen, det vill s�ga n�r du fortfarande k�nde dig trygg., och forts�tta igenom och f�rbi h�ndelsen tills du �ter k�nde dig trygg. Titta p� dig sj�lv n�r du tittar p� filmen*. OBS! Om du i detta l�ge k�nner ett mycket starkt obehag ska du genast avbryta �vningen och �terg� till att utifr�n betrakta n�r du tittar p� filmen om dig sj�lv. Obehaget tyder n�mligen p� att du associerat in dig i situationen och du ska vara dissocierad.<BR>&nbsp;</LI>
							<LI>I din hand har du en fj�rrkontroll med vars hj�lp du ska �ndra filmens egenskaper s� att den blir mindre obehaglig. H�r kan du experimentera fritt. Minska/�ka sk�rmens storlek, ljusm�ttnaden, f�rg/svartvitkontrasterna. H�j/s�nk volymen, filmduken. F�r bilden n�rmare eller l�ngre bort - allt med syftet att ta bort s� mycket obehag som m�jligt. Till exempel kanske filmen k�nns mindre obehaglig om den flyttas l�ngre bort ifr�n dig, ljuset tonas ned och bilden blir mindre. Experimentera fritt. Det h�r ett viktigt delmoment f�r att l�ra sig hantera upplevelsen och momentet kr�ver din fulla uppm�rksamhet. Var kreativ och flexibel. Kom ih�g att du befinner dig i nuet och fr�n h�ll betraktar dig sj�lv n�r du ser p� filmen om dig sj�lv f�r l�nge sedan. Du kan n�r som helst utl�sa trygghetsankaret om det skulle beh�vas. Kom ih�g att du �r trygg n�r du sitter h�r och nu och i fantasin tittar p� en film. Den h�r delen av �vningen �r klar n�r du har kunnat titta igenom hela filmen och k�nner dig trygg filmen igenom.<BR>&nbsp;</LI>
							<LI>Grattis! N�r du kommit till denna punkt ska du vara riktigt stolt �ver dig sj�lv! Du har nu upplevt den h�r obehagliga situationen utan att bryta ihop av negativa k�nslor. Du kan nu byta position och g� in i dig sj�lv d�r du sitter och tittar p� filmen. Med andra ord sitter du nu och tittar p� filmen genom dina gena �gon (associerat tillst�nd).<BR>&nbsp;</LI>
							<LI>N�r du gjort detta och det k�nns relativt bra kan du kliva in i filmen f�r att ge st�d till dig sj�lv n�r du upplever den obehagliga situationen. Ge dig sj�lv det st�d du beh�ver. Det kan handla om uppmuntran, eller rent av ett scenario d�r du f�rklarar f�r dig sj�lv att du kommer fr�n framtiden och att du vet att du kommer att �verleva den h�r situationen och klara den bra. L�t fantasin fl�da, du kan, om du �nskar, komma in i form av en insikt som smyger sig in i dig sj�lv i den d�r situationen som upplevdes som s� skr�mmande. N�r du bes�ker dig sj�lv i det f�rflutna vet du att du kan hantera situationen och det beh�ver du f�rmedla till dig sj�lv. Dock �r det ju s� att det kan vara bra att bevara en viss h�lsosam r�dsla, eller mer respekt, f�r farliga situationer, till exempel i m�tet med ormar eller spindlar, men den handikappande r�dslan ska vara borta.<BR>&nbsp;</LI>
							<LI>N�r ditt yngre jag, som upplevde den obehagliga situationen, har f�rst�tt och k�nner sig lugn och trygg kan du flytta detta yngre jag fr�n filmen in i dig sj�lv h�r och nu. Det kan vara bra att nu s�tta av en kort stund p� n�gra minuter i lugn och ro f�r att �terh�mta dig samt integrera de djupa f�r�ndringar som har �gt rum.<BR>&nbsp;</LI>
							<LI>N�r du k�nner dig lugn och sansad kan du slutf�ra och testa att �vningen fungerat genom att i associerat tillst�nd f�rest�lla dig ett tillf�lle i framtiden som skulle ha givit upphov till att fobin skulle ha utl�sts. Det h�r kan ge upphov till l�ttare obehag, men inte en f�rlamande r�dsla som tidigare.</LI>
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
						<B>Uppf�ljning</B>
						<BR><BR>
						A. Du angav tidigare att det som skapar din fobi �r...
						<BR><BR>
						<B>&quot;<%=Request.Form("MyPhobia")%>&quot;</B>
						<BR><BR>
						B. Hur �r obehaget n�r du t�nker p� det som tidigare framkallat din fobi?
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
%>				<TR><TD><B>&quot;<%=Request.Form("MyPhobia")%>&quot;</B> var f�re �vningen...</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 1,"Inte alls obehagligt","Maximalt obehagligt","darkGraySmall",False,Request("EmotionOne"),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD>...och efter �vningen...</TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR><TD><% insertVAS 2,"Inte alls obehagligt","Maximalt obehagligt","darkGraySmall",False,getVASvalue(1),False %></TD></TR>
				<TR><TD>&nbsp;</TD></TR>
				<TR>
					<TD>
						Alla personer har mer eller mindre sm� r�dslor f�r olika saker. Det kan vara utvecklande f�r kropp och sj�l att l�ra sig hantera situationer som starkt begr�nsar oss. Dessutom kan det vara st�rkande och mycket bra f�r sj�lvf�rtroendet. Genom att bli av med en besv�rlig fobi kan du vara tacksam f�r den styrka och fokuseringsf�rm�ga du besitter, som varit n�dv�ndig f�r att du ska kunna ha lidit s� mycket. Framf�r allt s� kan du ju nu anv�nda denna kraft och fokuseringsf�rm�ga p� saker som kan g�ra ditt liv s� mycket roligare att leva. Kraften och intensiteten i k�nslorna f�r en fobi kan i en omv�nd situation, till exempel en stormf�r�lskelse, bidra till ett rus av oanade dimensioner. Med andra ord kan du genom att bearbeta dina fobier frig�ra en stor kraft som kan anv�ndas f�r att �ka din livskvalitet och ditt v�lbefinnande.
						<BR><BR>
						* Detta kallas dubbel dissociation och skapar en n�dv�ndig emotionell distans till det som upplevs.
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
