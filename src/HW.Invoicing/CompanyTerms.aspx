<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyTerms.aspx.cs" Inherits="HW.Invoicing.CompanyTerms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <img src="uploads/<%= company.InvoiceLogo %>" />
    
        <table>
            <tr>
                <td>Föreläsare:</td>
                <td></td>
            </tr>
            <tr>
                <td>Datum för föreläsningen:</td>
                <td></td>
            </tr>
            <tr>
                <td>Speltid:</td>
                <td></td>
            </tr>
            <tr>
                <td>Föreläsningstitel: </td>
                <td></td>
            </tr>
            <tr>
                <td>Plats:</td>
                <td></td>
            </tr>
            <tr>
                <td>Kontaktperson:</td>
                <td></td>
            </tr>
            <tr>
                <td>E-post kontaktperson:</td>
                <td></td>
            </tr>
            <tr>
                <td>Ersättning:</td>
                <td></td>
            </tr>
            <tr>
                <td>Betalningsvillkor:</td>
                <td></td>
            </tr>
            <tr>
                <td>Faktureringsadress och eventuellt referensnummer:</td>
                <td></td>
            </tr>
        </table>

        Övrig information<br />
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>

        <%= company.Terms %>

    </div>
    </form>
</body>
</html>
