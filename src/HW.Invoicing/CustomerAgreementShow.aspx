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
        td { width: 100px; }
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
    <style type="text/css">
        .label-width 
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">

        <table style="width:100%">
            <tr>
                <td valign="bottom"><img src="uploads/<%= company.InvoiceLogo %>" /></td>
                <td valign="bottom"><b>Engagemangsavtal nummer HCGE-</b></td>
            </tr>
        </table>
        <table style="width:100%;" cellpadding="3">
            <tr>
                <td><b>Kund (Ange: Företagsnamn, Postadress och Org.nr)</b></td>
                <td><b>Agentur</b></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <p></p>
        <p>Mellan ovanstående parter har avtal om engagemang träffats enligt följande:</p>
        <table style="width:100%" cellpadding="3">
            <tr>
                <td class="label-width"><b>Föreläsare:</b></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Datum för föreläsningen:</b></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Speltid:</b></td>
                <td colspan="3"><asp:TextBox ID="TextBox4" CssClass="form-control" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>Föreläsningstitel:</b></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Plats:</b></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Kontaktperson:</b></td>
                <td>
                    <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td><b>Mobil:</b></td>
                <td>
                    <asp:TextBox ID="TextBox14" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>E-post kontaktperson:</b></td>
                <td colspan="3"><asp:TextBox ID="TextBox8" CssClass="form-control" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>Ersättning:</b></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Betalningsvillkor:</b></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Faktureringsadress och eventuellt referensnummer:</b></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />
        <p><b>Övrig information</b><br />
        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox><br />
        </p>
        
        <p>Detta engagemangavtal (Huvudavtal) är en skriftlig bekräftelse på en redan muntlig överenskommelse mellan ovan nämnda Agentur och Kund. Detta Huvudavtal skall tillsammans med tillhörande bilaga returneras till Agenturen inom 14 dagar från att beställningen gjorts. Vid engagemangsdatum inom 14 dagar från beställningen krävs omgående retur till Agenturen.</p>

        <b>Övriga villkor och önskemål</b>
        <ol>
            <li>Inga ändringar får göras i detta avtal efter utskrift.</li>
            <li>Teknik: Föreläsaren tar med sig egen dator (Mac) och behöver tillgång till projektor med VGA-, Thunderbolt- eller HDMI-sladd. Headsetmikrofon och ljudsladd till datorn så att ljud kan spelas upp därifrån önskas.</li>
            <li>Till detta Huvudavtal tillkommer, förutom Rider, även tillhörande bilaga om Avtalsvillkor. Genom undertecknandet av detta Huvudavtal godkänner Parterna, förutom Huvudavtalet, även tillhörande Avtalsvillkor.</li>
        </ol>

        <table style="width:100%" cellpadding="3">
            <tr>
                <td>Ort och Datum</td>
            </tr>
            <tr>
                <td>Signatur</td>
                <td><img src="uploads/<%= company.Signature %>" /></td>
            </tr>
            <tr>
                <td>
                    Namn, Titel<br />
                    Företag
                </td>
                <td>
                    Dan Hasson, VD<br />
                    Hasson Consulting Group AB
                </td>
            </tr>
        </table>
        <br />

        <%= company.Terms.Replace("\n", "<br>") %>

        <br />
        <p>
            <asp:Button ID="buttonNext" runat="server" Text="Next, proceed to contract preview >>" CssClass="btn btn-success" OnClick="buttonNextClick" />
        </p>

    </div>
    </form>
</body>
</html>
