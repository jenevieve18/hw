<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="sponsor.aspx.cs" Inherits="HW.Adm.sponsor" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       function exportRedirect(w) {
           var s = '', b = document.forms[0].ExportSponsorID;
           for (var i = 0; i < b.length; i++)
               if (b[i].checked)
                   s += ',' + b[i].value;
           if (w == 2)
               location.href = 'sponsor.aspx?ExportExtendedSponsorID=' + s +
                    '&ExportExtendedSurveyID=' + document.forms[0].ESID[document.forms[0].ESID.selectedIndex].value +
                    '&ExportExtendedLangID=' + document.forms[0].ELID[document.forms[0].ELID.selectedIndex].value;
           else if (w == 5 || w == 7)
               location.href = 'sponsor.aspx?Type=' + w + '&ExportSponsorID=' + s +
                    '&ExportSurveyID=' + document.forms[0].RSID[document.forms[0].RSID.selectedIndex].value +
                    '&ExportLangID=' + document.forms[0].RLID[document.forms[0].RLID.selectedIndex].value;
           else if (w >= 3)
               location.href = 'sponsor.aspx?Type=' + w + '&ExportSponsorID=' + s;
           else
               location.href = 'sponsor.aspx?Type=' + w + '&ExportSponsorID=' + s +
                    '&BQID=' + document.forms[0].BQID[document.forms[0].BQID.selectedIndex].value +
                    '&BQID2=' + document.forms[0].BQID2[document.forms[0].BQID2.selectedIndex].value +
                    '&AM=' + document.forms[0].ActivityMonth[document.forms[0].ActivityMonth.selectedIndex].value;
       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Sponsors</td></tr>
		</table>
		<table style="margin:20px;" border="0" cellspacing="0" cellpadding="0">
			<asp:Label ID=Sponsor runat=server />
		</table>
		<span style="margin:20px;"><button onclick="location.href='sponsorSetup.aspx';">Add</button></span>
		<table style="margin:20px;" border="0" cellspacing="0" cellpadding="0">
		    <asp:Label ID=Merge runat=server Visible=false/>
		</table>
		<asp:Button ID=ExecMerge runat=server text="Merge" visible=false />
</asp:Content>
