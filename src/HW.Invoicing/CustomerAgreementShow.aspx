<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAgreementShow.aspx.cs" Inherits="HW.Invoicing.CustomerAgreementShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>IHG Invoicing</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

	<!--link rel="stylesheet/less" href="less/bootstrap.less" type="text/css" /-->
	<!--link rel="stylesheet/less" href="less/responsive.less" type="text/css" /-->
	<!--script src="js/less-1.3.3.min.js"></script-->
	<!--append ‘#!watch’ to the browser URL, then refresh the page. -->
	
	<link href="css/bootstrap.min.css" rel="stylesheet">
	<link href="css/style.css" rel="stylesheet">

	<!--<link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>-->

	<style type="text/css">
	    body {
		    padding-top:30px;
	    }
	     h1, h2, h3, h4, h5, h6, .h1, .h2, .h3, .h4, .h5, .h6 {
	 	    font-family: 'Open Sans', sans-serif;
	     }
	    footer {
		    margin-top:40px;
		      background: #333;
		      color: #eee;
		      font-size: 11px;
		      padding:20px 0px;
	    }
	    footer ul 
	    {
	        padding-left: 20px;
	    }
	</style>

    <style type="text/css">
        table { border-collapse: collapse; empty-cells: show; }

        td { position: relative; }

        tr.strikeout td:before {
            content: " ";
            position: absolute;
            top: 50%;
            left: 0;
            border-bottom: 1px solid red;
            width: 100%;
        }

        tr.strikeout td:after {
            content: "\00B7";
            font-size: 1px;
        }

        /* Extra styling */
        /*td { width: 100px; }*/
        th { text-align: left; }
    </style>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
        <script src="js/html5shiv.js"></script>
    <![endif]-->

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="img/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="img/apple-touch-icon-57-precomposed.png">
    <!--<link rel="shortcut icon" href="img/favicon.png">-->
  
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/scripts.js"></script>

    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.number.min.js" type="text/javascript"></script>
    
    <%--<link href="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/e8bddc60e73c1ec2475f827be36e1957af72e2ea/build/css/bootstrap-datetimepicker.css" rel="stylesheet">
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>
    <script src="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/e8bddc60e73c1ec2475f827be36e1957af72e2ea/src/js/bootstrap-datetimepicker.js"></script>
--%>
    <script type="text/javascript">
        var dateTimeAndPlaces = [];
        <% foreach (var d in dateTimeAndPlaces) { %>
            var d = {
                'date': '<%= d.Date.Value.ToString("yyyy-MM-dd") %>',
                'timeFrom': '<%= d.TimeFrom %>',
                'timeTo': '<%= d.TimeTo %>',
                'address': '<%= d.Address %>'
            };
            dateTimeAndPlaces.push(d);
        <% } %>
        $(function () {
            initDates();
            if (dateTimeAndPlaces.length > 0) {
                $('#dateTimeAndPlaces').html("");
                var i = 0;
                dateTimeAndPlaces.forEach(function(e) {
                    appendDateTimeAndPlace(e.date, e.timeFrom, e.timeTo, e.address, i == 0);
                    initDates();
                    bindRemoveDateTimeAndPlace();
                    i++;
                });
            }
            $('#buttonAddMoreTimeAndPlace').click(function () {
                appendDateTimeAndPlace("", "", "", "", false);
                initDates();
                bindRemoveDateTimeAndPlace();
            });
        });
        function appendDateTimeAndPlace(date, timeFrom, timeTo, address, start) {
            var s = "";
            if (start) {
                s = "<tr>" +
                    "<td></td>" +
                    "<td><input value='" + date + "' type='text' id='agreement-date' name='agreement-date' class='date form-control'></td>" +
                    "<td><input value='" + timeFrom + "' type='text' id='agreement-timefrom' name='agreement-timefrom' class='form-control'></td>" +
                    "<td><input value='" + timeTo + "' type='text' id='agreement-timeto' name='agreement-timeto' class='form-control'></td>" +
                    "<td><input value='" + address + "' type='text' id='agreement-address' name='agreement-address' class='form-control'></td>" +
                    "<td></td>" +
                    "</tr>";
            } else {
                s = "<tr>" +
                    "<td></td>" +
                    "<td><input value='" + date + "' type='text' id='agreement-date' name='agreement-date' class='date form-control'></td>" +
                    "<td><input value='" + timeFrom + "' type='text' id='agreement-timefrom' name='agreement-timefrom' class='form-control'></td>" +
                    "<td><input value='" + timeTo + "' type='text' id='agreement-timeto' name='agreement-timeto' class='form-control'></td>" +
                    "<td><input value='" + address + "' type='text' id='agreement-address' name='agreement-address' class='form-control'></td>" +
                    "<td><a href='javascript:;' class='removeDateTimeAndPlace'><img src='img/cross.png'></a></td>" +
                    "</tr>";
            }
            $('#dateTimeAndPlaces').append(s);
        }
        function bindRemoveDateTimeAndPlace() {
            $('.removeDateTimeAndPlace').click(function () {
                $(this).closest('tr').remove();
            });
        }
        function initDates() {
            $('.date').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
        }
    </script>

    <style type="text/css">
        .label-width
        {
            width:200px;
        }
        .label2-width
        {
            width:100px;
        }
        .date-width 
        {
            width:120px;
        }
        .time-width 
        {
            width:75px;
        }
        .icon-width 
        {
            width:16px;
        }
        .compensation 
        {
            text-align:right;
        }
    </style>

</head>
<body>
    <div class="container">
    <form id="form1" class="form-horizontal" runat="server" role="form">
        
        <asp:Panel ID="Panel1" runat="server" DefaultButton="buttonNext">

        <table style="width:100%" cellpadding="2">
            <tr>
                <td class="col-md-6" valign="bottom"><img src="uploads/<%= company.InvoiceLogo %>" /></td>
                <td valign="bottom"><b>Engagemangsavtal nummer <%= company.AgreementPrefix %>-<%= agreement.Id.ToString("000") %></b></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td><b>Kund</b> <i>Ange: Företagsnamn, Postadress och Organisationsnummer</i></td>
                <td><b>Agentur</b></td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%" cellpadding="2">
                        <tr>
                            <td class="label-width"><b>Företagsnamn</b></td>
                            <td>
                                <asp:TextBox ID="textBoxCustomerName" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Postadress</b></td>
                            <td>
                                <asp:TextBox ID="textBoxCustomerPostalAddress" CssClass="form-control" runat="server" TextMode="MultiLine" Height="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Organisationsnummer</b></td>
                            <td>
                                <asp:TextBox ID="textBoxCustomerNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Faktureringsadress</b></td>
                            <td>
                                <asp:TextBox ID="textBoxCustomerInvoiceAddress" CssClass="form-control" runat="server" TextMode="MultiLine" Height="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Eventuellt referensnummer</b></td>
                            <td>
                                <asp:TextBox ID="textBoxCustomerReferenceNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <asp:Label ID="labelCompanyName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>

        <p></p>
        <p>Mellan ovanstående parter har avtal om engagemang träffats enligt följande:</p>
        <table style="width:100%" cellpadding="2">
            <thead>
                <tr>
                    <td class="label-width"><b>Föreläsare</b>:</td>
                    <td colspan="5">
                        <asp:Label ID="labelAgreementLecturer" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Föreläsningstitel</b>:</td>
                    <td colspan="5">
                        <asp:TextBox ID="textBoxAgreementLectureTitle" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="date-width"><b>Datum för föreläsningen</b></td>
                    <td class="time-width"><b>Från klockan</b></td>
                    <td class="time-width"><b>Till klockan</b></td>
                    <td><b>Plats och adress</b></td>
                    <td class="icon-width"></td>
                </tr>
            </thead>

            <tbody id="dateTimeAndPlaces">
                <tr>
                    <td></td>
                    <td><input type='text' id='agreement-date' name='agreement-date' class='date form-control' /></td>
                    <td><input type='text' id='agreement-timefrom' name='agreement-timefrom' class='form-control' /></td>
                    <td><input type='text' id='agreement-timeto' name='agreement-timeto' class='form-control' /></td>
                    <td><input type='text' id='agreement-address' name='agreement-address' class='form-control' /></td>
                    <td></td>
                </tr>
            </tbody>

            <tfoot>
                <tr>
                    <td align="right" colspan="7">
                        <a href="javascript:;" id="buttonAddMoreTimeAndPlace" class="btn btn-default">Lägg till tid och plats</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
                <%--<tr>
                    <td><b>Plats och adress</b>:</td>
                    <td colspan="6">
                        <asp:TextBox ID="textBoxAgreementLocation" CssClass="form-control" runat="server" TextMode="MultiLine" Height="100"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td><b>Kontaktperson</b>:</td>
                    <td colspan="5">
                        <asp:TextBox ID="textBoxAgreementContact" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><b>Mobil</b>:</td>
                    <td colspan="5">
                        <asp:TextBox ID="textBoxAgreementMobile" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><b>E-post kontaktperson</b>:</td>
                    <td colspan="5">
                        <asp:TextBox ID="textBoxAgreementEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><b>Ersättning</b>:</td>
                    <td class="date-width">
                        <asp:TextBox ID="textBoxAgreementCompensation" CssClass="compensation form-control" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="4">SEK + moms. Eventuella resekostnader och logi tillkommer.</td>
                </tr>
                <tr>
                    <td><b>Betalningsvillkor</b>:</td>
                    <td colspan="5">
                        <asp:Label ID="labelPaymentTerms" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </tfoot>
        </table>

        <br />
        <p>
            <b>Övrig information</b> <i>Beskriv gärna målgruppen här.</i><br />
            <asp:TextBox ID="textBoxAgreementOtherInformation" CssClass="form-control" runat="server" TextMode="MultiLine" Height="210"></asp:TextBox><br />
        </p>
        
        <p>Detta engagemangavtal (Huvudavtal) är en skriftlig bekräftelse på en redan muntlig överenskommelse mellan ovan nämnda Agentur och Kund. Detta Huvudavtal skall tillsammans med tillhörande bilaga returneras till Agenturen inom 14 dagar från att beställningen gjorts. Vid engagemangsdatum inom 14 dagar från beställningen krävs omgående retur till Agenturen.</p>

        <b>Övriga villkor och önskemål</b>
        <ol>
            <li>Inga ändringar får göras i detta avtal efter utskrift.</li>
            <li>Teknik: Föreläsaren tar med sig egen dator (Mac) och behöver tillgång till projektor med VGA-, Thunderbolt- eller HDMI-sladd. Headsetmikrofon och ljudsladd till datorn så att ljud kan spelas upp därifrån önskas.</li>
            <li>Till detta Huvudavtal tillkommer, förutom Rider, även tillhörande bilaga om Avtalsvillkor. Genom undertecknandet av detta Huvudavtal godkänner Parterna, förutom Huvudavtalet, även tillhörande Avtalsvillkor.</li>
        </ol>

        <table style="width:100%" cellpadding="2">
            <tr>
                <td class="label2-width"><b>Ort och Datum</b></td>
                <td>
                    <asp:TextBox ID="textBoxAgreementContactPlaceSigned" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td class="date-width">
                    <asp:TextBox ID="textBoxAgreementContactDateSigned" CssClass="date form-control" runat="server"></asp:TextBox>
                </td>
                <td style="width:10px;"></td>
                <td>
                    Stockholm den
                    <asp:Label ID="labelAgreementDateSigned" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td colspan="3">
                    <b>Signatur</b> <i>Ange namn och titel på den som ska signera avtalet</i>
                </td>
                <td></td>
                <td><img src="uploads/<%= company.Signature %>" /></td>
            </tr>--%>
            <tr>
                <td><b>Namn</b></td>
                <td colspan="2">
                    <asp:TextBox ID="textBoxAgreementContactName" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td></td>
                <td>
                    Dan Hasson, VD<br />
                    Hasson Consulting Group AB
                </td>
            </tr>
            <tr>
                <td><b>Titel</b></td>
                <td colspan="2">
                    <asp:TextBox ID="textBoxAgreementContactTitle" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td><b>Företag</b></td>
                <td colspan="2">
                    <asp:TextBox ID="textBoxAgreementContactCompany" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />

        <h3>Avtalsvillkor</h3>

        <p><strong>1. Gemensamma villkor</strong></p>

        <p>1.1 I och med undertecknandet av Huvudavtalet till denna bilaga godk&auml;nner Kundenoch Agenturen villkoren i denna bilaga.</p>

        <p>1.2 Eventuella anm&auml;rkningar som ber&ouml;r artistiskt material eller framf&ouml;rande kringengagemanget enligt detta avtal utg&ouml;r inte grund f&ouml;r skadest&aring;ndsanspr&aring;k.</p>

        <p>1.3 Merv&auml;rdesskatt och kostnader f&ouml;r resa och boende tillkommer p&aring; avtaladers&auml;ttning. Agenturen har r&auml;tt att i efterhand debitera merv&auml;rdesskatt enligt besluttagna av myndigheter. Anm&auml;rkningar mot fakturerade tj&auml;nster som l&auml;mnassenare &auml;n &aring;tta (8) dagar efter mottagen faktura beaktas inte.</p>

        <p>1.4 Vid eventuella skador, olyckor eller andra h&auml;ndelser som leder till f&ouml;rs&auml;kringsfallskall den v&aring;llande parten &auml;ven st&aring; f&ouml;r den skadelidandes sj&auml;lvrisk.</p>

        <p>1.5 Agenturen f&ouml;rbeh&aring;ller sig r&auml;tten att ensidigt &auml;ndra betalningsvillkoren ochefterdebitera dr&ouml;jsm&aring;lsr&auml;ntan vid f&ouml;rsent inkommen betalning samt avmyndigheter eventuellt beslutade f&ouml;r&auml;ndringar av skatter och/eller avgifter.Agenturen skall upplysa Kunden om dessa f&ouml;rh&aring;llanden f&ouml;re fakturering enligtde nya villkoren.</p>

        <p><strong>2. Kundens &aring;taganden</strong></p>

        <p>2.1 Kunden ansvarar f&ouml;r att en erforderlig och giltig ansvarsf&ouml;rs&auml;kring omfattandeengagemanget i sin helhet finns.</p>

        <p>2.2 Kunden svarar f&ouml;r produktionens och Gruppens/Artistens/F&ouml;redragsh&aring;llarens s&auml;kerhet p&aring; platsen f&ouml;rengagemanget. Om s&aring; erfordras skall Kundens egna eller f&ouml;rordnadeordningsvakter finnas.</p>

        <p>2.3 Kunden f&aring;r i f&ouml;rekommande fall g&auml;rna tillhandah&aring;lla ett l&aring;sbart eller bevakat omkl&auml;dningsrum med tillg&aring;ngtill toalett, spegel, dricksvatten och i f&ouml;rekommande fall dusch och mat.</p>

        <p>2.4 Kunden &auml;r, i de fall denne st&aring;r f&ouml;r ljud-/ljusanl&auml;ggning, skyldig att tillse attutrustningen &auml;r i funktionsdugligt skick och motsvarar de krav Artisten st&auml;llt.</p>

        <p>2.5 Kunden ansvarar f&ouml;r att inga ljud- och/eller videoinspelningar av Artistensframtr&auml;dande f&aring;r f&ouml;rekomma utan samtycke fr&aring;n Agenturen och f&ouml;redragsh&aring;llaren.</p>

        <p>2.6 I de fall Kunden &auml;r en artistf&ouml;rmedlare f&aring;r Kundens p&aring;slag till slutkund f&aring;r inte &ouml;verstiga 20 % av det &ouml;verenskomna prisetmed Agenturen. Agenturen har r&auml;tt att fr&aring;ga slutkund om slutpriset. InnanAgenturen kontaktar slutkunden skall Kunden underr&auml;ttas.</p>

        <p><strong>3. Agenturens &aring;taganden</strong></p>

        <p>3.1 Agenturen ansvarar f&ouml;r att Artisten har g&auml;llande f&ouml;rs&auml;kringar f&ouml;r sig sj&auml;lv och f&ouml;rsin egendom.</p>

        <p>3.2 Agenturen garanterar att andra avtal inte hindrar eller st&ouml;r &aring;tagandenenligt detta avtal.</p>

        <p>3.3 Agenturen ansvarar f&ouml;r att Artisten f&ouml;ljer de regler och anvisningar som Kundenuppr&auml;ttat p&aring; platsen f&ouml;r engagemanget.</p>

        <p>3.4 S&aring; snart Artisten intagit och/eller p&aring;b&ouml;rjat sitt framtr&auml;dande betraktas Agenturensuppdrag som genomf&ouml;rt. Detta g&auml;ller &auml;ven i de fall engagemanget inte kangenomf&ouml;ras p&aring; avsett s&auml;tt p&aring; grund av bristande f&ouml;ruts&auml;ttningar som Kundensvarar f&ouml;r.</p>

        <p>3.5 Agenturen &auml;r vid Artistens sjukdomsfall, eller andra dylika personligaomst&auml;ndigheter som g&ouml;r Artisten of&ouml;rm&ouml;gen att delta i engagemanget, skyldig attomedelbart meddela Kunden. Sjukdom skall styrkas med l&auml;karintyg. Agenturensoch Kundens &aring;taganden enligt detta avtal upph&auml;vs vid s&aring;dana omst&auml;ndigheter.</p>

        <p>Agenturen skall, i dessa fall, efter b&auml;sta f&ouml;rm&aring;ga f&ouml;rs&ouml;ka finna en l&auml;mplig ers&auml;ttare. Om Kunden inte anser att artistens ers&auml;ttare uppfyller de krav somKunden st&auml;ller eller &auml;r l&auml;mplig annulleras avtalet.</p>

        <p><strong>4. Avbokningsregler</strong></p>

        <p>4.1 Ett undertecknat exemplar av Huvudavtalet skall returneras till Agenturen inomfjorton (14) dagar efter best&auml;llningen f&ouml;r att vara giltigt, f&ouml;rutsatt att avtalet &auml;ruts&auml;nt i tid som m&ouml;jligg&ouml;r detta, annars 14 dagar efter att Kunden erh&aring;llit avtalet. Med best&auml;llningsdatum avses den dag d&aring;Agenturen och Kunden muntligen &ouml;verenskom om engagemanget, tid, plats,artist, ers&auml;ttning etc. F&ouml;r det fall avtalet ej returnerats i enlighet med dessabest&auml;mmelser &auml;ger Agenturen r&auml;tt att h&auml;va avtalet. Innan s&aring; sker &auml;r Agenturenskyldig att informera Kunden d&auml;rom. Om Kunden annullerar best&auml;llningentill&auml;mpas g&auml;llande avbokningsavgifter.</p>

        <p>4.2 Vid engagemangsdatum inom fjorton (14) dagar fr&aring;n best&auml;llningen kr&auml;vsomg&aring;ende retur av det undertecknade Huvudavtalet till Agenturen. Om Kundenannullerar best&auml;llningen inom samma tid skall ers&auml;ttning erl&auml;ggas till Agenturenenligt g&auml;llande avbokningsavgifter.</p>

        <p><strong>5. Avbokningsavgift</strong></p>

        <p>5.1 Vid avbokning under en period fram till en (1) m&aring;nad f&ouml;re engagemangeterl&auml;gger Kunden till Agenturen en avbokningsavgift om 10 % + merv&auml;rdesskatt av &ouml;verenskommen ers&auml;ttning.</p>

        <p>5.2 Vid avbokning i perioden en (1) m&aring;nad till tv&aring; (2) veckor f&ouml;re engagemangeterl&auml;gger Kunden till Agenturen 50 % + merv&auml;rdesskatt av&ouml;verenskommen ers&auml;ttning.</p>

        <p>5.3 Vid avbokning vid mindre &auml;n fjorton (14) dagar f&ouml;re engagemanget erl&auml;ggerKunden till Agenturen hela den &ouml;verenskomna ers&auml;ttningen +merv&auml;rdesskatt.</p>

        <p><strong>6. Force majeure</strong></p>

        <p>Force majeure befriar Agenturen och Kunden fr&aring;n alla &aring;taganden enligt dettaavtal. Som force majeure r&auml;knas krig, politiska omv&auml;lvningar, strejk, lock-out,eldsv&aring;da, naturkatastrofer, som t.ex. askmoln, &ouml;versv&auml;mningar, sn&ouml;stormar, ellerandra f&ouml;rh&aring;llanden av j&auml;mf&ouml;rbar natur. D&aring;ligt v&auml;der betraktas inte som forcemajeure. Det &aring;ligger Agenturen och Kunden att r&auml;tta sin planering efter r&aring;dandeoch t&auml;nkbara omst&auml;ndigheter.</p>

        <p><strong>7. Sekretess</strong></p>

        <p>Agenturen och Kunden skall behandla uppgifterna i Huvudavtalet r&ouml;randeengagemanget samt parternas inb&ouml;rdes f&ouml;rh&aring;llande konfidentiellt, och inteavsl&ouml;ja s&aring;dan information f&ouml;r annan. Detta &aring;tagande skall g&auml;lla &auml;ven efter avtalethar upph&ouml;rt.</p>

        <p><strong>8. Avtalsbrott</strong></p>

        <p>Om Agenturen eller Kunden bryter mot best&auml;mmelserna i detta avtal samt attden felande parten inte inom trettio (30) dagar efter skriftligen uppmaningvidtagit r&auml;ttelse, har motparten r&auml;tt att med omedelbar verkan s&auml;ga upp dettaavtal i f&ouml;rh&aring;llande till den felande parten. Respektive part har r&auml;tt att kr&auml;va denfelande parten p&aring; utebliven eller f&ouml;rv&auml;ntat ers&auml;ttning.</p>

        <p><strong>9. Tvister</strong></p>

        <p>Svensk lag skall till&auml;mpas p&aring; detta avtal. Eventuella tvister skall avg&ouml;ras i f&ouml;rsta hand via muntlig f&ouml;rhandling i god ton och i andra han avallm&auml;n domstol med Stockholms tingsr&auml;tt som f&ouml;rsta instans.</p>

        <p>&nbsp;</p>

        <br />
        <p>
            <asp:Button ID="buttonNext" runat="server" Text="Nästa, fortsätt till förhandsvisning av avtalet >>" CssClass="btn btn-success" OnClick="buttonNextClick" />
        </p>

        </asp:Panel>

    </form>
    </div>
</body>
</html>
