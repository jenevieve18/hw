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
<p>Regionala etikpr�vningsn�mnden har godk�nt projektet.</p>

<h1>Data behandlas konfidentiellt!</h1>
<p>Samtliga av oss inf�rskaffade uppgifter �tnjuter sekretesskydd med st�d av 9 kap 4 � i sekretesslagen. Alla insamlade data avidentifieras och kodas sedan i statistikprogram f�r vidare bearbetning. Inga andra �n forskarna har tillg�ng till data. Data kommer inte att presenteras p� individniv� utan endast p� gruppniv�.</p>

<h1>Deltagande �r frivilligt</h1>
<p>Syftet med unders�kningen �r att kartl�gga f�r�ldrars stressituation. Ju flera deltagare desto b�ttre! Naturligtvis �r deltagande helt frivilligt och kan avbrytas n�r som helst under unders�kningens g�ng. </p>
<%			} 
			else 
			{
%>
<h1>Etiska riktlinjer</h1>
<p>Regionala etikpr�vningsn�mnden har godk�nt projektet den 9 mars 2006, diarienummer 2006/109-31/4.</p>

<h1>Data behandlas konfidentiellt!</h1>
<p>Samtliga av oss inf�rskaffade uppgifter �tnjuter sekretesskydd med st�d av 9 kap 4 � i sekretesslagen. Alla insamlade data avidentifieras och kodas sedan i statistikprogram f�r vidare bearbetning. Inga andra �n forskarna har tillg�ng till data. Data kommer inte att presenteras p� individniv� utan endast p� gruppniv�. </p>

<h1>Deltagande �r frivilligt</h1>
<p>Syftet med unders�kningen �r att kartl�gga elevers stressituation och f�rb�ttra deras h�lsa vid behov. Ju flera deltagare desto b�ttre! Naturligtvis �r deltagande helt frivilligt och kan avbrytas n�r som helst under unders�kningens g�ng.</p>

<h1>Vi ber alla elever att skriva sitt namn  </h1>
<% 
			}
			break;
		case 35:
%>
<h1>Etiska riktlinjer</h1>
<p>Regionala etikpr�vningsn�mnden har godk�nt projektet den 9 mars 2006, diarienummer 2010/1088-31/5.</p>

<h1>Data behandlas konfidentiellt!</h1>
<p>Samtliga av oss inf�rskaffade uppgifter �tnjuter sekretesskydd med st�d av 9 kap 4 � i sekretesslagen. Alla insamlade data avidentifieras och kodas sedan i statistikprogram f�r vidare bearbetning. Inga andra �n forskarna har tillg�ng till data. Data kommer inte att presenteras p� individniv� utan endast p� gruppniv�.</p>

<h1>Deltagande �r frivilligt</h1>
<p>Syftet med unders�kningen �r att kartl�gga situationen hos deltagare i arbetslivsintroduktion och utveckla stresshanteringsprogram f�r dem. V�r f�rhoppning �r att vid behov kunna erbjuda dig deltagande i ett s�dant program. Naturligtvis �r deltagandet i studien helt frivilligt och kan avbrytas n�r som helst under unders�kningens g�ng.</p>

<h1>Vi ber alla deltagare att skriva sitt namn och kontaktuppgifter. Var v�nlig fyll i �ven om du inte �r intresserad av att delta i stresshantering.</h1>
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
<p>Syftet med enk�ten du just besvarat �r att utv�rdera projektdeltagarnas sj�lvskattade kompetens g�llande eH�lsa, f�re respektive efter deltagande i <i>Kompetenslyftet eH�lsa i prim�rv�rd</i>. Ut�ver utv�rderingen kan dina enk�tsvar �ven komma att anv�ndas i forskning om du godk�nner detta. </p>
<p>Syftet med forskningen �r att vetenskapligt studera effekterna av ett kompetensutvecklingsprogram p� attityder till, och kunskap om, anv�ndande av olika eH�lsasystem. Forskningen genomf�rs av forskare fr�n Karolinska Institutet.</p>
<p>Godk�nnande till att enk�tsvaren f�r anv�ndas i forskning �r helt frivillig. Din arbetssituation p�verkas inte av huruvida du v�ljer att godk�nna detta eller inte.  Enk�tsvaren sparas i avidentifierad form och redovisas endast p� gruppniv� och d�rmed framg�r det aldrig vad enskilda personer har svarat.  Uppgifterna sparas och behandlas i enlighet med personuppgiftslagen (1998:204). </p>
<p>Om du kryssar i rutan f�r "ja" p� enk�ten godk�nner du att dina svar f�r anv�ndas till forskning. Kryssar du f�r "nej" kommer dina svar endast att anv�ndas till utv�rderingen som g�rs inom ramen f�r projektet <i>Kompetenslyftet eH�lsa i prim�rv�rd</i>.</p>
<p>Om du har n�gra fr�gor eller �nskar f�rtydligande �r du varmt v�lkommen att kontakta:<br/><br/>Christer Sandahl, professor<br/>Medical Management Centrum (MMC)<br/>Karolinska Institutet<br/>Tfn: 08-524 836 18<br/>E-post: <a href="mailto:christer.sandahl@ki.se">christer.sandahl@ki.se</a></p>
<%
			break;
	}
}
%>
</body></html>