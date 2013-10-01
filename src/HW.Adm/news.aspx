<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="HW.Adm.news" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
   <script type="text/javascript" src="http://www.google.com/jsapi"></script>
   <script type="text/javascript">       google.load("language", "1");</script>
   <script language="Javascript">
       var check = false;
       function translate() {
           var langFrom = document.forms[0].transLangFrom[document.forms[0].transLangFrom.selectedIndex].value, langTo = document.forms[0].transLangTo[document.forms[0].transLangTo.selectedIndex].value;
           google.language.translate(document.forms[0].Header.value, langFrom, langTo, function (result) { if (!result.error) { document.forms[0].Header.value = result.translation; } });
           google.language.translate(document.forms[0].Teaser.value, langFrom, langTo, function (result) { if (!result.error) { document.forms[0].Teaser.value = result.translation; } });
           google.language.translate(document.forms[0].Body.value, langFrom, langTo, function (result) { if (!result.error) { document.forms[0].Body.value = result.translation; } });
       }
       function toggleNews() {
           for (var i = 0; i < document.forms[0].rssID.length; i++) {
               document.getElementById('h' + document.forms[0].rssID[i].value).style.backgroundColor = (document.forms[0].rssID[i].checked ? '#f2f2f2' : '');
               document.getElementById('d' + document.forms[0].rssID[i].value).style.backgroundColor = (document.forms[0].rssID[i].checked ? '#f2f2f2' : '');
               document.getElementById('l' + document.forms[0].rssID[i].value).style.backgroundColor = (document.forms[0].rssID[i].checked ? '#f2f2f2' : '');
               if (document.getElementById('a' + document.forms[0].rssID[i].value))
                   document.getElementById('a' + document.forms[0].rssID[i].value).style.backgroundColor = (document.forms[0].rssID[i].checked ? '#f2f2f2' : '');
               document.getElementById('t' + document.forms[0].rssID[i].value).style.fontWeight = (document.forms[0].rssID[i].checked ? 'bold' : '');
               document.getElementById('d' + document.forms[0].rssID[i].value).style.display = (document.forms[0].rssID[i].checked ? '' : 'none');
               document.getElementById('l' + document.forms[0].rssID[i].value).style.display = (document.forms[0].rssID[i].checked ? '' : 'none');
               if (document.getElementById('a' + document.forms[0].rssID[i].value))
                   document.getElementById('a' + document.forms[0].rssID[i].value).style.display = (document.forms[0].rssID[i].checked ? '' : 'none');
           }
       }
       function copyAll() {
           copyHeader();
           copyArticle(true);
           copyArticle(false);
           copyDT();
           copyLink();
           document.forms[0].DirectFromFeed.checked = true;
           document.forms[0].Published.checked = true;
           return false;
       }
       function copyLink() {
           for (var i = 0; i < document.forms[0].rssID.length; i++) {
               if (document.forms[0].rssID[i].checked) {
                   if (document.getElementById('rssLinkAlt' + document.forms[0].rssID[i].value))
                       document.forms[0].Link.value = document.getElementById('rssLinkAlt' + document.forms[0].rssID[i].value).href;
                   else
                       document.forms[0].Link.value = document.getElementById('rssLink' + document.forms[0].rssID[i].value).href;
                   s = document.getElementById('i' + document.forms[0].rssID[i].value).src.replace('.gif', '');
                   s = s.substr(s.length - 1, 1);
                   document.forms[0].LinkLangID[s].checked = true;
               }
           }
       }
       function copyHeader() {
           for (var i = 0; i < document.forms[0].rssID.length; i++) {
               if (document.forms[0].rssID[i].checked) {
                   document.forms[0].Header.value = document.getElementById('t' + document.forms[0].rssID[i].value).innerHTML;
               }
           }
       }
       function copyArticle(targ) {
           for (var i = 0; i < document.forms[0].rssID.length; i++) {
               if (document.forms[0].rssID[i].checked) {
                   if (targ)
                       document.forms[0].Teaser.value = document.getElementById('rssArt' + document.forms[0].rssID[i].value).innerHTML;
                   else
                       document.forms[0].Body.value = document.getElementById('rssArt' + document.forms[0].rssID[i].value).innerHTML;
               }
           }
       }
       function copyDT() {
           for (var i = 0; i < document.forms[0].rssID.length; i++) {
               if (document.forms[0].rssID[i].checked) {
                   document.forms[0].DT.value = eval('document.forms[0].rssDT' + document.forms[0].rssID[i].value + '.value');
               }
           }
       }
       function selectAll() {
           check = !check;
           for (var i = 0; i < document.forms[0].rssIDdelete.length; i++) {
               document.forms[0].rssIDdelete[i].checked = check;
           }
       }
       function toggleLang() {
           document.forms[0].NewsLangID.value = (document.forms[0].NewsLangID.value == '0' ? '1' : '0');
           document.forms[0].submit();
       }
   </script>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
	    <input id="NewsID" type="hidden" name="NewsID" value="0" runat="server" />
	    <input id="LoadNews" type="hidden" name="LoadNews" value="0" runat="server" />
	    <input id="DeleteImage" type="hidden" name="DeleteImage" value="0" runat="server" />
	    <input id="NewsLangID" type="hidden" name="NewsLangID" value="0" runat="server" />
		<%=Db.nav()%>
		<div id="img1" style="display:none;position:absolute;top:100px;left:110px;border: solid 1px #333333;"></div>
		<div id="img2" style="display:none;position:absolute;top:300px;left:60px;border: solid 1px #333333;"></div>
		<table border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td width="500" valign="top">
					<table width="400" border="0" cellspacing="0" cellpadding="0">
						<tr><td style="font-size:16px;" align="center">News</td></tr>
					</table>
					<table border="0" cellspacing="0" cellpadding="0" id="ChangeNews" runat="server">
						<tr><td colspan="2"><B>Heading</B> [<a class="small" href="JavaScript:copyHeader();">copy</a>]</td></tr>
						<tr><td colspan="2"><asp:TextBox Width="400" ID="Header" runat="server" /></td></tr>
						<tr><td colspan="2"><B>Teaser</B> [<a class="small" href="JavaScript:copyArticle(true);">copy</a>]</td></tr>
						<tr><td colspan="2"><asp:TextBox Rows="3" TextMode="MultiLine" Width="400" ID="Teaser" runat="server" /></td></tr>
						<tr><td colspan="2"><B>Teaser image</B> <asp:Label ID="TeaserImage" runat="server" /></td></tr>
						<tr><td colspan="2"><input style="width:400px;" type="file" id="TeaserImageID" runat="server" /></td></tr>
						<tr><td colspan="2"><B>Article</B> [<a class="small" href="JavaScript:copyArticle(false);">copy</a>]</td></tr>
						<tr><td colspan="2"><asp:TextBox Rows="12" TextMode="MultiLine" Width="400" ID="Body" runat="server" /></td></tr>
						<tr><td colspan="2"><B>Image</B> <asp:Label ID="Image" runat="server" /></td></tr>
						<tr><td colspan="2"><input style="width:400px;" type="file" id="ImageID" runat="server" /></td></tr>
						<tr><td><B>Date/time</B> [<a class="small" href="JavaScript:copyDT();">copy</a>]</td><td><B>Link text</B></td></tr>
						<tr><td><asp:TextBox Width="150" ID="DT" runat="server" /></td><td><asp:TextBox Width="250" ID="LinkText" runat="server" /></td></tr>
						<tr><td colspan="2"><B>Link</B> [<a class="small" href="JavaScript:copyLink();">copy</a>]</td></tr>
						<tr><td colspan="2"><asp:TextBox Width="400" ID="Link" runat="server" /></td></tr>
						<tr><td><B>Link language</B></td><td><B>Category</B></td></tr>
						<tr><td valign="top"><asp:RadioButtonList CellPadding="0" CellSpacing="0" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table" ID="LinkLangID" runat="server" /></td><td><asp:RadioButtonList CellPadding="0" CellSpacing="0" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table" ID="NewsCategoryID" runat="server" /></td></tr>
						<tr><td><b>Snatched</b><input type="checkbox" value="1" name="DirectFromFeed" id="DirectFromFeed" runat="server" /></td><td rowspan="3"><asp:Button Height="30" ID="Cancel" runat="server" /><asp:Button Height="30" ID="DeleteArticle" runat="server" /><button style="height:30px;" onclick="copyAll();" type=button>Snatch</button><asp:Button Height="30" ID="Save" runat="server" /><br /><asp:DropDownList runat=server ID=transLangFrom /><asp:DropDownList runat=server ID=transLangTo />[<a href="JavaScript:translate();">Translate</a>]</td></tr>
						<tr><td><b>Published</b><input type="checkbox" value="1" name="Published" id="Published" runat="server" /></td></tr>
						<tr><td><b>Only in category</b><input type="checkbox" value="1" name="OnlyInCategory" id="OnlyInCategory" runat="server" /></td></tr>
					</table>
					<asp:PlaceHolder ID="NewsListContainer" runat="server">
						<table width="400" border="0" cellspacing="0" cellpadding="0">
							<tr><td align="center"><asp:Button Width="150" Height="30" ID="AddArticle" runat="server" /></td></tr>
						</table>
						<br />
						<asp:Label ID="NewsList" runat="server" />
					</asp:PlaceHolder>
				</td>
				<td width="500" valign="top" align="center">
					<div alin="center">
						<span style="font-size:16px;">Feeds</span><br />
						<asp:DropDownList ID=SourceType autopostback=true runat=server /><asp:DropDownList ID=SourceID autopostback=true runat=server /><asp:DropDownList ID=LangID autopostback=true runat=server /><br />
						[<asp:LinkButton ID=DeleteOldNews runat=server Text="Delete all older than 7 days" />]<br />
					</div>
					<asp:PlaceHolder ID="Right" runat="server" /><br />
					<asp:Button Width="150" Height="30" ID="Delete" runat="server" />
				</td>
			</tr>
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
