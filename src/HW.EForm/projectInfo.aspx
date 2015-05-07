<html><body style="font-family:arial,helvetica;">
<% 
if(HttpContext.Current.Request.QueryString["PID"] != null) 
{ 
	switch(Convert.ToInt32(HttpContext.Current.Request.QueryString["PID"]))
	{
		case 28:
			if(HttpContext.Current.Request.QueryString["Part"] != null) 
			{ 
%>
<h1>Etiska riktlinjer</h1>
<p>Regionala etikprövningsnämnden har godkänt projektet.</p>

<h1>Data behandlas konfidentiellt!</h1>
<p>Samtliga av oss införskaffade uppgifter åtnjuter sekretesskydd med stöd av 9 kap 4 § i sekretesslagen. Alla insamlade data avidentifieras och kodas sedan i statistikprogram för vidare bearbetning. Inga andra än forskarna har tillgång till data. Data kommer inte att presenteras på individnivå utan endast på gruppnivå.</p>

<h1>Deltagande är frivilligt</h1>
<p>Syftet med undersökningen är att kartlägga föräldrars stressituation. Ju flera deltagare desto bättre! Naturligtvis är deltagande helt frivilligt och kan avbrytas när som helst under undersökningens gång. </p>
<%			} 
			else 
			{
%>
<h1>Etiska riktlinjer</h1>
<p>Regionala etikprövningsnämnden har godkänt projektet den 9 mars 2006, diarienummer 2006/109-31/4.</p>

<h1>Data behandlas konfidentiellt!</h1>
<p>Samtliga av oss införskaffade uppgifter åtnjuter sekretesskydd med stöd av 9 kap 4 § i sekretesslagen. Alla insamlade data avidentifieras och kodas sedan i statistikprogram för vidare bearbetning. Inga andra än forskarna har tillgång till data. Data kommer inte att presenteras på individnivå utan endast på gruppnivå. </p>

<h1>Deltagande är frivilligt</h1>
<p>Syftet med undersökningen är att kartlägga elevers stressituation och förbättra deras hälsa vid behov. Ju flera deltagare desto bättre! Naturligtvis är deltagande helt frivilligt och kan avbrytas när som helst under undersökningens gång.</p>

<h1>Vi ber alla elever att skriva sitt namn  </h1>
<% 
			}
			break;
		case 35:
%>
<h1>Etiska riktlinjer</h1>
<p>Regionala etikprövningsnämnden har godkänt projektet den 9 mars 2006, diarienummer 2010/1088-31/5.</p>

<h1>Data behandlas konfidentiellt!</h1>
<p>Samtliga av oss införskaffade uppgifter åtnjuter sekretesskydd med stöd av 9 kap 4 § i sekretesslagen. Alla insamlade data avidentifieras och kodas sedan i statistikprogram för vidare bearbetning. Inga andra än forskarna har tillgång till data. Data kommer inte att presenteras på individnivå utan endast på gruppnivå.</p>

<h1>Deltagande är frivilligt</h1>
<p>Syftet med undersökningen är att kartlägga situationen hos deltagare i arbetslivsintroduktion och utveckla stresshanteringsprogram för dem. Vår förhoppning är att vid behov kunna erbjuda dig deltagande i ett sådant program. Naturligtvis är deltagandet i studien helt frivilligt och kan avbrytas när som helst under undersökningens gång.</p>

<h1>Vi ber alla deltagare att skriva sitt namn och kontaktuppgifter. Var vänlig fyll i även om du inte är intresserad av att delta i stresshantering.</h1>
<%
			break;
	} 
}
else if(HttpContext.Current.Request.QueryString["SID"] != null) 
{ 
	switch(Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]))
	{
				case 147:
%>
<h1>Information om forskningen</h1>
<p>Syftet med enkäten du just besvarat är att utvärdera projektdeltagarnas självskattade kompetens gällande eHälsa, före respektive efter deltagande i <i>Kompetenslyftet eHälsa i primärvård</i>. Utöver utvärderingen kan dina enkätsvar även komma att användas i forskning om du godkänner detta. </p>
<p>Syftet med forskningen är att vetenskapligt studera effekterna av ett kompetensutvecklingsprogram på attityder till, och kunskap om, användande av olika eHälsasystem. Forskningen genomförs av forskare från Karolinska Institutet.</p>
<p>Godkännande till att enkätsvaren får användas i forskning är helt frivillig. Din arbetssituation påverkas inte av huruvida du väljer att godkänna detta eller inte.  Enkätsvaren sparas i avidentifierad form och redovisas endast på gruppnivå och därmed framgår det aldrig vad enskilda personer har svarat.  Uppgifterna sparas och behandlas i enlighet med personuppgiftslagen (1998:204). </p>
<p>Om du kryssar i rutan för "ja" på enkäten godkänner du att dina svar får användas till forskning. Kryssar du för "nej" kommer dina svar endast att användas till utvärderingen som görs inom ramen för projektet <i>Kompetenslyftet eHälsa i primärvård</i>.</p>
<p>Om du har några frågor eller önskar förtydligande är du varmt välkommen att kontakta:<br/><br/>Christer Sandahl, professor<br/>Medical Management Centrum (MMC)<br/>Karolinska Institutet<br/>Tfn: 08-524 836 18<br/>E-post: <a href="mailto:christer.sandahl@ki.se">christer.sandahl@ki.se</a></p>
<%
			break;
	}
}
%>
</body></html>