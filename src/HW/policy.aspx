<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="policy.aspx.cs" Inherits="HW.policy" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
    <%
      switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
      {
          case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Integritetspolicy, användarvillkor, forskning och PUL","Integritetspolicy, användarvillkor, forskning och PUL")); break;
          case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Privacy policy, terms of use, research and PUL", "Privacy policy, terms of use, research and PUL")); break;
          }
          %>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
<div style="margin:20px;width:535px;">
  <%
      switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
      {
          case 1:%>
		<h1>Integritetspolicy, användarvillkor, forskning och PUL</h1>
        <br />
		<p><b>§ 1 Anonymitet och sekretess</b><br />All information som Du registrerar på HealthWatch behandlas med högsta möjliga grad av konfidentialitet. Du har möjligheten att vara helt anonym. Alla som arbetar med HealthWatch-konceptet omfattas dessutom av de delar av sekretesslagen (SFS 1980:100) som innehåller bestämmelser om sekretess på hälso- och sjukvårdsområdet (kap 7 § 1).<br /><br />Ett vanligt problem med sekretess är Din egen dator, framför allt om Du arbetar på ett företag och har Ditt e-postkonto där. Om Du önskar kan Du skapa ett e-postkonto på t ex Gmail, Hushmail, Hotmail, Yahoomail eller annan webbaserad e-posttjänst.</p>
        <br />
		<p><b>§ 2 Lagring och hantering av information</b><br />Informationen som Du registrerar på HealthWatch lagras i en databas. Information på individnivå finns endast tillgänglig för Dig själv under förutsättning att Du hanterar Ditt användarnamn och lösenord med varsamhet.<br /><br />Informationen används av systemet för att beräkna och sammanställa resultat på gruppnivå, t ex hur svarsvärdena ser ut i genomsnitt för personer med ett specifikt karakteristika, t ex kön och åldersgrupp. Dessa resultat analyseras i avidentifierad form i för att Du själv ska kunna jämföra Dig med gruppen som helhet och för att kontinuerligt förbättra precisionen vid beräkning av gränsvärden för risknivåer. Om Du registrerar Dig med en företags- eller organisationstillhörighet så återkopplas även sammanställningar av gruppresultat till ansvariga på företaget/organisationen.<br /><br />För att öka tillförlitligheten och skydda den enskilda individens information sker ingen sammanställning av resultat för grupper med mindre än 10 svarande och i grupper med lägre än 50 procents svarsfrekvens. I vissa fall, då grupper med färre än 10 personer önskar återkoppling på gruppnivå, krävs ett skriftligt samtycke från alla i gruppen eller från behörig företrädare för organisationen om den sänkta gränsen ska gälla generellt. Den absoluta minimigränsen för återkoppling på gruppnivå är 7 personer.<br /><br />Du kan när som helst ta bort ditt konto. Dina mätvärden kommer då att sparas, men Dina personuppgifter kommer att raderas efter 30 dagar. Under denna ”ångermånad” är det möjligt att återaktivera kontot.</p>
        <br />
		<p><b>§ 3 Återkoppling av resultat på individnivå</b><br />Mätning och återkoppling skapar goda förutsättningar för att på ett optimalt sätt jobba med kompetens- och hälsoutveckling. Du får återkoppling via grafer samt tillgång till övningar för stresshantering och hälsopromotion som är speciellt framtagna för att maximera återhämtning, koncentrationsförmåga, prestationsförmåga och allmänt välbefinnande. All den information som Du registrerar på HealthWatch kan Du använda helt fritt enligt Dina önskemål. Det finns funktioner som gör att Du kan skriva ut Dina registrerade svar och ta med Dig dessa inför en hälsokontroll, läkarbesök eller dylikt. Det går även bra att logga in direkt på hemsidan under besöket och visa resultaten i realtid.</p>
        <br />
		<p><b>§ 4 Återkoppling av resultat på grupp- och organisationsnivå</b><br />Allt förbättringsarbete i en organisation effektiviseras av kvalitetssäkrad mätning. Baserat på resultaten från dessa mätningar kan man ta fram handlingsplaner/interventioner som sedan följs upp med regelbundna intervall för att bedöma interventionens framgång på kort och lång sikt. Kompetens- och hälsoutveckling inom ett företag är strategiska områden för ett företag som vill optimera sin prestation.<br /><br />Inom ramen för HealthWatch kan efter begäran företag/organisation få tillgång till avidentifierad information på gruppnivå. Svaren kan inte härledas till enskilda individer. Endast individen själv har tillgång till individuella data.</p>
        <br />
		<p><b>§ 5 Personuppgiftslagen</b><br />Enligt personuppgiftslagen (SFS 1998:204) måste alla som inhämtar och lagrar personuppgifter via Internet inhämta samtycke från de personer som lämnar uppgifterna. Med personuppgifter räknas samtliga uppgifter som kan knytas till Dig som person. Genom att Du registrerar Dig på HealthWatch samtycker Du till att de uppgifter Du matar in lagras och hanteras i enlighet med § 2. Om Du är en företagsanvändare samtycker Du även till att återkoppling på gruppnivå enligt § 4 kommer att ske.</p>
        <br />
		<p><b>§ 6 Cookies</b><br />Enligt lagen om elektronisk kommunikation (SFS 2003:389) skall alla som besöker en webbplats med cookies få information om dessa. En cookie är en liten textfil med information som sparas på Din dator. Tjänstens servrar använder sig av så kallade sessions-cookies för att hålla reda på vem Du är när Du loggat in. Ingen personlig information sparas och information om ditt besök på HealthWatch kan inte spåras. Sessions-cookies lagras dessutom endast tillfälligt och tas bort från Din dator efter att du lämnat HealthWatch.</p>
        <br />
		<p><b>§ 7 Disclaimer</b><br />Det finns idag ingen behandling som garanterat hjälper alla. De tekniker som ingår i HealthWatchkonceptet är dock genomgående sådana som i tidigare studier och kliniskt överlag visat goda effekter på gruppnivå för att förbättra sömn och återhämtning, koncentrationsförmåga samt psykiskt och fysiskt välbefinnande. HealthWatch tar inget ansvar för eventuella medicinska tillstånd som kan ligga till grund för Dina problem. Allt deltagande sker alltid av egen fri vilja och på eget ansvar. Vi rekommenderar att medicinska besvär alltid utreds ordentligt av kvalificerad vårdpersonal. Att Du använder HealthWatch innebär inte att Du ska avbryta en pågående medicinering eller behandling. Om Du tar receptbelagda mediciner bör Du alltid samråda med Din läkare inför eventuell nedtrappning eller avslut.</p>
        <br />
        <p>Senast uppdaterad 25 Sep 2013</p>
         <% break;
          case 2: %>
        <h1>Privacy policy, terms of use, research and PUL</h1>
        <br />
<p><b>§ 1 Anonymity and confidentiality</b><br />Any information that you register on Health Watch is treated with the highest possible level of confidentiality. You have the option to remain anonymous. Everyone who works with Health Watch concept also enjoy the parts of the Secrecy Act (SFS 1980:100), which provides for confidentiality in the health field (Chapter 7 § 1).<br /><br />A common problem with privacy is your own computer, especially if you work at a company and have your email account there. If you wish, you can create an email account such as Gmail, Hushmail, Hotmail, Yahoo Mail or other web based email service.</p>
<br />
<p><b>§ 2 The storage and handling of information</b><br />The information you register on Health Watch is stored in a database. Information on the individual level are available only for yourself, provided that you handle your username and password with care.<br /><br />This information is used by the system to calculate and compile results at the group level, such as how response values ​​look like the average for people with specific characteristics such as gender and age group. These results are analyzed anonymously so that you should be able to compare yourself with the group as a whole, and to continuously improve the precision in calculations for risk level cut-offs. If you sign up with a corporate or organizational affiliation to reconnect even summaries of group performance to managers at the company / organization.<br /><br />In order to increase reliability and protect the individual's information, no summaries or feedback of results are given for groups with fewer than 10 respondents and in groups with less than 50 percent response rate. In some instances, when groups with less than 10 individuals would like to obtain group level feedback, a written consent from all participants in that group is mandatory. Alternatively, if this lower level should apply for the organization in general, an authorized representative of the company needs to sign a general consent. The absolute minimum level for group level feedback is 7 individuals.<br /><br />You can always delete your account. Your readings will then be saved, but your personal information will be deleted after 30 days. During this "cooling-off month" is it possible to reactivate the account.</p>
<br />
<p><b>§ 3 The feedback of results at the individual level</b><br />Measurement and feedback creates good conditions for an optimal work with competence and health development. You will receive feedback via graphs and access to training for stress management and health promotion that is specially designed to maximize recovery, concentration, performance and general well-being. All the information you register on Health Watch, you can use freely according to your wishes. There are features that let you print your recorded answers and bring them before a medical examination, medical examinations and the like. It is also possible to log directly on the site during the visit and display the results in real time.</p>
<br />
<p><b>§ 4 The feedback of results on group and organizational level</b><br />All improvement in an organization more efficient by quality-assured measurement. Based on the results of these measurements, one can develop action plans / interventions which are then followed up at regular intervals to assess the intervention's success in the short and long term. Competence and health development within a company's strategic areas for a company to optimize its performance.<br /><br />Under the Health Watch can request company / organization have access to anonymous information at the group level. The answers can not be traced to specific individuals. Only the individual himself has access to individual data.</p>
<br />
<p><b>§ 5 Data Protection Act</b><br />According to Personal Data Act (SFS 1998:204), anyone who collects and stores personal information via the Internet to obtain the consent of the people who supply it. With personal data included all data that can be linked to you personally. By the time of registration on Health Watch, you agree that any information you enter is stored and handled in accordance with § 2. If you are a business user you agree also that the feedback at group level in accordance with § 4 will be done.</p>
<br />
<p><b>§ 6 Cookies</b><br />Under the Electronic Communications Act (SFS 2003:389), all visitors to a website with cookies should be informed about these. A cookie is a small text file with information stored on your computer. Service's servers use so-called session cookies to keep track of who you are when you log on. No personal information is stored and information about your visit to the Health Watch can not be traced. Session cookies are stored also only temporary and are deleted from your computer after you leave Health Watch.</p>
<br />
<p><b>§ 7 Disclaimer</b><br />There is currently no treatment that is guaranteed to help everyone. The techniques included in the Health Watch concept is consistently those of previous studies and clinical general shown good effects on the group level to improve sleep and recovery, concentration and mental and physical well-being. Health Watch takes no responsibility for any medical conditions that could form the basis of your problems. All participation is always by my own free will and responsibility. We recommend that medical conditions are always investigated thoroughly by a qualified health professional. You are using the Health Watch does not mean you should cancel a current medication or treatment. If you take prescription medications, you should always consult your doctor before any withdrawal or termination.</p>
<br />
        <p>Last updated 25 Sep 2013</p>
<% break;
      }
             %>
</div>
  </body>
</html>

