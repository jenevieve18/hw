﻿/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class ExerciseExporterTests
	{
		SqlExerciseRepository r = new SqlExerciseRepository();
		ExerciseExporter x = new ExerciseExporter();
		ExerciseVariantLanguage e;
		
		[SetUp]
		public void Setup()
		{
//			e = r.ReadExerciseVariant(72);
			e = new ExerciseVariantLanguage {
				Content = @"<div class='block'>
          <p>Målgrupp: HR som processledare för chefsgrupper (10–20 chefer).</p>
          <p>Följande övning genomförs i samband med neddragningar, förslagsvis ledd av HR. Syftet är att öka medvetenheten hos chefer om vilka val, känslor och etiska frågeställningar de kan komma att ställas inför. Genom ökad förståelse för konsekvenser
            (t ex reaktioner och känslor) hos de olika rollerna kan cheferna bättre förbereda sig för en neddragning. Som processledare leder du övningen och de efterföljande diskussionerna och hjälper deltagarna att resonera kring upplevelserna.</p>
          <ol style='padding-left:30px;'>
            <li>Börja med att dela upp cheferna i 3 grupper.</li>
            <li>Berätta för cheferna att de nu är en familj och att föräldrarna i familjen samlar sina barn för att berätta om att familjen har ekonomiska problem. De har nu inte längre råd att köpa mat till alla i familjen och att de därför behöver lämna
              bort några av barnen.</li>
            <li>Det är nu dags att dela ut roller. Ena gruppen utgörs av föräldrar, den andra av barn som får stanna kvar i familjen och den tredje består av dem som blir bortlämnade.</li>
            <li>Varje grupp får under ca 10 minuter sitta och diskutera hur det känns att ingå i den grupp man hamnat i.</li>
            <li>Därefter ska respektive grupp berätta för de andra hur det känns att befinna sig i just den gruppen.</li>
            <li>När varje grupp har redovisat kan ni ha en gemensam diskussion kring hur man bäst stöttar respektive grupp. Byt från ett familjeperspektiv till ett arbetsgrupps- eller organisationsperspektiv.Nedan följer exempel på vad man kan tänka på när
              man diskuterar.</li>
          </ol>
          <p>OBS! Denna övning kan väcka starka reaktioner hos vissa av deltagarna. Det är därför viktigt att vara vaksam på och ta hand om dessa deltagares reaktioner. Det finns en nackdel med övningen som behöver tas upp med deltagarna efter att övningen
            har genomförts. Familjeexemplet gör att det kan vara svårt att få med perspektivet att vissa personer kan uppfatta det som något positivt att få lämna en anställning. I verkligheten kan det finnas individer som blir lättade och glada över
            att de får möjlighet att till en nystart i livet och ägna sig åt annat de hellre önskar. Den reaktionen är svår att översätta till familjeexemplet.</p>
          <p>Om en grupp har svårt att reflektera och problematisera, t ex enbart drar slutsatsen att det känns bra att vara kvar i organisationen kan du som processledare aktivera personer i de andra i grupperna. Du kan också ställa några följdfrågor för
            att stimulera fram fler perspektiv.</p>
          <br />
          <div>
            <p>Tips till punkt 6 i övningen som du kan ta upp när ni diskuterar hur man stöttar respektive grupp:</p>
            <h2>De som får sluta</h2>
            <p>
              <b>Visa empati</b>
              <br /> Lyssna på medarbetarens reaktioner och låt personen ha sina känslor. Man kan även försöka spegla vilken känsla som personen förmedlar eller vad personen själv beskriver som jobbigt. T ex: ”Det låter jobbigt. Jag förstår att du är orolig
              för din ekonomi.” (om det är det som personen har pratat om)
              <br />
              <br />
              <b>Hjälp att komma vidare</b>
              <br /> T ex: ”Jag är orolig för dig och mån om att det ska gå bra för dig. Att du får sluta är väldigt sorgligt och jag vet att de som arbetar aktivt med att gå vidare klarar sig bättre. Jag har märkt att du ofta pratar om hur jobbigt det är men
              sällan pratar om hur du kan komma vidare eller vad du vill göra åt din situation. Kan vi diskutera hur jag kan hjälpa dig att bli mer aktiv?” Fråga om den andra personen vill att man diskuterar situationen.Tänk på att hur du behandlar de
              som slutar även kommer att påverka de som får stanna.
            </p>
            <h2>De som får stanna</h2>
            <p>
              Visa empati för att det kan vara jobbigt att stanna kvar. Medarbetare kan känna både skuldkänslor och oro för arbetsbelastningen för de som blir kvar eftersom det är färre personer som utför arbetsuppgifterna. Känslan kommer inte gå över men medarbetarna
              kommer uppleva att du förstår och då har du större möjlighet att prata om det praktiska.
              <br />
              <br /> Hjälp till med hur man ska hantera förändrade arbetsuppgifter. Hur kan man till exempel prioritera för att få en hanterbar arbetssituation om en grupp blir mindre men fortfarande har samma arbetsuppgifter?
              <br />
              <br /> Hur du behandlar de som får sluta kommer även att påverka engagemanget hos de som får stanna. Om de bevittnar att man hanterar de som får sluta på ett dåligt sätt kommer det påverka deras uppfattning om organisationen/dig som chef. En del
              i detta är att de lätt kan tänka att nästa gång kommer det att ”drabba mig”.
            </p>
            <h2>Gruppen som helhet bestående av både de som får stanna och de som kommer att sluta.</h2>
            <p>
              Berätta på ett möte om vad som är bestämt och att några kommer att sluta. Säg något själv om vad du känner inför det som visar att du ser att det är en svår situation. Du kan erbjuda möjligheten att ställa frågor eller komma till dig om man undrar något.
            </p>
            <h2>De som behöver lämna beskedet om vilka som får stanna eller sluta. </h2>
            <p>Diskutera hur man kan få stöd som chef i processen, t ex i ett nätverk av andra chefer i samma situation. Att leda en grupp i en neddragning är en svår uppgift. Däremot bör man inte söka stöd för den tunga uppgiften i medarbetargruppen utan
              bland andra chefskollegor eller sin närmaste chef. Fundera även på vad man kan göra som individ för att orka med uppgiften. (Balans jobb/fritid, arbetstempo på jobbet etc.)</p>
          </div>
          <p>&nbsp;</p>
        </div>"
			};
		}
		
		[Test]
		public void TestMethod()
		{
			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create)) {
				MemoryStream s = x.Export(e);
				s.WriteTo(f);
			}
			Process.Start(@"test.pdf");
		}
	}
}
