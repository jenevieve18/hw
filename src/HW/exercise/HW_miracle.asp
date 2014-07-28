<!--#include virtual="/includes/topNoMyPQL.asp"-->
<%
If Int("0" & Session("UserID")) = 0 And Int("0" & Request.QueryString("AdminMode")) <> 1 Then
	Response.Clear
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Du har loggats ut pga inaktivitet."
		Case 1	Response.Write "You have been logged out because of inactivity."
	End Select
	Response.Flush
	Response.End
End If

rMiracle = ""
rUserID = Int("0" & Session("UserID"))
rExerciseMiracleID = Int("0" & Request("ExerciseMiracleID"))

rArea = Int("0" & Request("Area"))

If Request("Action") = "Delete" Then
	sql.Execute "DELETE FROM [ExerciseMiracle] WHERE ExerciseMiracleID = " & rExerciseMiracleID & " AND UserID = " & rUserID
	rArea = 0
	rExerciseMiracleID = 0
End If

If Int("0" & Request.Form("SaveMiracle")) = 1 Then
	rArea = 1
	rAllowPublish = Int("0" & Request.Form("AllowPublish"))
	rMiracle = Replace("" & Request.Form("Miracle"),"'","''")
	If rExerciseMiracleID = 0 Then
		rs.Open "SET NOCOUNT ON; SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRAN; INSERT INTO [ExerciseMiracle] (UserID, Miracle, AllowPublish) VALUES (" & Int("0" & Session("UserID")) & ",'" & rMiracle & "'," & rAllowPublish & "); SELECT IDENT_CURRENT('ExerciseMiracle') AS ExerciseMiracleID;COMMIT;"
		rExerciseMiracleID = Int("0" & rs("ExerciseMiracleID"))
		rs.Close
	Else
		sql.Execute "UPDATE [ExerciseMiracle] SET Miracle = '" & rMiracle & "', AllowPublish = " & rAllowPublish & ", DateTimeChanged = GETDATE() WHERE ExerciseMiracleID = " & rExerciseMiracleID & " AND UserID = " & Int("0" & Session("UserID"))
	End If
ElseIf rExerciseMiracleID <> 0 Then
	rArea = 1
	rs.Open "SELECT UserID, Miracle, AllowPublish FROM [ExerciseMiracle] WHERE ExerciseMiracleID = " & rExerciseMiracleID & " AND (UserID = " & Int("0" & Session("UserID")) & " OR (AllowPublish = 1 AND Published = 1))", sql
	rUserID = Int("0" & rs("UserID"))
	rMiracle = rs("Miracle")
	rAllowPublish = rs("AllowPublish")
	If rAllowPublish Then
		rAllowPublish = 1
	Else
		rAllowPublish = 2
	End If
	rs.Close
End If

rs.Open "SELECT COUNT(*) AS Published FROM [ExerciseMiracle] WHERE AllowPublish = 1 AND Published = 1", sql
rPublished = Int(rs("Published"))
rs.Close

rs.Open "SELECT COUNT(*) AS MyMiracles FROM [ExerciseMiracle] WHERE UserID = " & rUserID, sql
rMyMiracles = Int(rs("MyMiracles"))
rs.Close
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
	<title>PQL - myPQL - <%
Select Case Int("0" & Session("Lang"))
	Case 0	Response.Write "Mirakelfrågan"
	Case 1	Response.Write "Miracle Question"
End Select
%></title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<script language="JavaScript" type="text/JavaScript">
		window.history.forward(1);
		<!--#include virtual="/includes/jsFindObj.asp"-->
		<!--#include virtual="/includes/jsPreloadImages.asp"-->
		<!--#include virtual="/includes/jsSwapImg.asp"-->
	</script>
	<!--#include virtual="/includes/styleSheet.asp"-->
</head>
<body onload="preloadImages('/img/button/saveH<%=Int("0" & Session("Lang"))%>.gif','/img/button/updateH<%=Int("0" & Session("Lang"))%>.gif');" marginwidth="0" marginheight="0" rightmargin="0" leftmargin="0" topmargin="0" bottommargin="0">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<form name="stdfrm" method="post" action="PQLmiracle.asp#bot">
<input type="hidden" name="SaveMiracle" value="0">
<input type="hidden" name="ExerciseMiracleID" value="<%=rExerciseMiracleID%>">
	<tr>
		<td background="/img/exercise/topBgr.gif" colspan="3">
			<table border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td><img src="/img/null.gif" width="15" height="60"></td>
					<td valign="middle"><img src="/img/base/PQLlogoSmall.gif" width="57" height="36"></td>
					<td valign="middle"><img src="/img/null.gif" width="20" height="1"></td>
					<td valign="middle"><img src="/img/base/exerciseW<%=Int("0" & Session("Lang"))%>.gif"></td>
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
			<table border="0" cellspacing="0" cellpadding="0" width="100%">
				<TR><TD CLASS="blueBold"><img src="/img/base/subArrowActiveH.gif" width="14" height="14" align="absmiddle">&nbsp;&nbsp;<%
Select Case Int("0" & Session("Lang"))
	Case 0	Response.Write "Mirakelfrågan"
	Case 1	Response.Write "Miracle Question"
End Select
%></td></tr>
				<tr>
					<td class="text">
						<br>
<%
Select Case Int("0" & Session("Lang"))
	Case 0
%>
						Om du inte vet vad du vill kan den här frågan leda dig närmare svaret. Genom att detaljerat besvara den här frågan kan du närma dig det du verkligen vill uppnå i livet. Du kan börja förstå vad det är som du faktiskt önskar. Om du vet vad du vill är det mycket lättare att uppnå det eller i alla fall delar av det. Det finns 2 viktiga saker att tänka på när du jobbar med den här frågan:<br><br>
						<ol>
							<li>Låt fantasin flöda! Kom ihåg att det är ett önskat scenario.<br>&nbsp;</li>
							<li>Ordet INTE är förbjudet! Vad du inte vill ha är ointressant i detta sammanhang. Här ska fokus enbart vara vad du vill ha.</li>
						</ol>
						Tänk dig att du går och lägger dig som vanligt en natt. Men just den här natten är väldigt speciell. Under natten sker ett mirakel och när du vaknar så är världen precis exakt som du alltid har önskat dig att den ska vara. Allt du någonsin önskat har inträffat. Beskriv din dag. Börja med det första som händer när du vaknar på morgonen. Ta med alla sinnesintryck. Vad är det första du ser, hör, känner? Vad säger du till dig själv? Känner du någon lukt, smak denna mirakulösa morgon? Fortsätt sedan att beskriva din dag/ditt liv med hjälp av dessa frågor. Mirakelfrågan är en mycket bra hjälpreda i samband med målprogrammering och om man känner att man inte vet vad man vill.<br><br>
						Reflektera slutligen över vad som hindrar dig från att uppnå det som skett efter mirakelnatten. Reflektera även över vad som skulle hända om du uppnådde det du önskat.<br>
<%
	Case 1
%>
						If you don't know what you want, this question can bring you closer to the answer. By answering the question in detail you can get closer to what you really want out of life. You can begin to understand what you really want. If you know what you want, it's much easier to achieve it, or at least portions of it. There are two important things to think about when you work on this question: <br><br>
						<ol>
							<li>Let your imagination flow! Remember that this is a desired scenario.<br>&nbsp;</li>
							<li>The word NOT is forbidden! What you don't want is irrelevant here. You should focus only on what you want. </li>
						</ol>
						Imagine that you go to bed as usual one night. But this specific night is very special. During the night a miracle happens and when you awaken the world is exactly as you have always wished it would be. Everything you ever wanted to happen has happened. Describe your day. Begin with the first thing that happens when you wake up in the morning. Include all the sensations. What is the first thing you see, hear, feel? What do you say to yourself? What do you smell and taste this miraculous morning? Then continue to describe your day/your life with the aid of these questions. The Miracle Question is a very good guide for your goal programming, if you feel that you're not sure what you want. <br><br>
						Finally, think about what keeps you from attaining what happened after the miracle night. Also consider what might happen if you were to attain what you wished. <br>
<%
End Select
%>
						&nbsp;
					</td>
				</tr>
				<tr><td><img src="/img/null.gif" width="1" height="10"></td></tr>
				<tr>
					<td align="center">
						<table border="0" cellspacing="0" cellpadding="0" width="520">
							<tr>
<%
If rArea = 0 Then
	Response.Write "<td><IMG SRC=""/img/base/subArrowActiveH.gif"" WIDTH=""14"" HEIGHT=""14""></td><td><img src=""/img/null.gif"" width=""6"" height=""1""></td><td class=""blueBold"" nowrap>"
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Nytt mirakel"
		Case 1	Response.Write "New miracle"
	End Select
	Response.Write "</td><td><img src=""/img/null.gif"" width=""20"" height=""1""></td>"
Else
	Response.Write "<td><img src=""/img/base/subArrowMoreN.gif"" width=""14"" height=""14"" NAME=""subArrowImg0"" border=""0""></td><td><img src=""/img/null.gif"" width=""6"" height=""1""></td><td class=""redBold"" nowrap><A href=""PQLmiracle.asp?Area=0#bot"" class=""redSmallBold"" ONMOUSEOUT=""swapImgRestore();"" ONMOUSEOVER=""swapImage('subArrowImg0','','/img/base/subArrowMoreH.gif',1)"">"
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Nytt mirakel"
		Case 1	Response.Write "New miracle"
	End Select
	Response.Write "</A></td><td><img src=""/img/null.gif"" width=""20"" height=""1""></td>"
End If

If rArea = 1 Then
	Response.Write "<td><IMG SRC=""/img/base/subArrowActiveH.gif"" WIDTH=""14"" HEIGHT=""14""></td><td><img src=""/img/null.gif"" width=""6"" height=""1""></td><td class=""blueBold"" nowrap>"
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Visa"
		Case 1	Response.Write "Show"
	End Select
	If rUserID = Int("0" & Session("UserID")) Then
		Select Case Int("0" & Session("Lang"))
			Case 0	Response.Write "/ändra"
			Case 1	Response.Write "/change"
		End Select
	End If
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write " mirakel"
		Case 1	Response.Write " miracle"
	End Select
	Response.Write "</td><td><img src=""/img/null.gif"" width=""20"" height=""1""></td>"
End If

If rArea = 2 Then
	Response.Write "<td><IMG SRC=""/img/base/subArrowActiveH.gif"" WIDTH=""14"" HEIGHT=""14""></td><td><img src=""/img/null.gif"" width=""6"" height=""1""></td><td class=""blueBold"" nowrap>"
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Mina mirakel"
		Case 1	Response.Write "My miracles"
	End Select
	Response.Write "</td><td><img src=""/img/null.gif"" width=""20"" height=""1""></td>"
ElseIf rMyMiracles > 0 Then
	Response.Write "<td><img src=""/img/base/subArrowMoreN.gif"" width=""14"" height=""14"" NAME=""subArrowImg2"" border=""0""></td><td><img src=""/img/null.gif"" width=""6"" height=""1""></td><td class=""redBold"" nowrap><A href=""PQLmiracle.asp?Area=2#bot"" class=""redSmallBold"" ONMOUSEOUT=""swapImgRestore();"" ONMOUSEOVER=""swapImage('subArrowImg2','','/img/base/subArrowMoreH.gif',1)"">"
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Mina mirakel"
		Case 1	Response.Write "My miracles"
	End Select
	Response.Write "</A></td><td><img src=""/img/null.gif"" width=""20"" height=""1""></td>"
End If

If rArea = 3 Then
	Response.Write "<td><IMG SRC=""/img/base/subArrowActiveH.gif"" WIDTH=""14"" HEIGHT=""14""></td><td><img src=""/img/null.gif"" width=""6"" height=""1""></td><td class=""blueBold"" nowrap>"
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Publicerade mirakel"
		Case 1	Response.Write "Published miracles"
	End Select
	Response.Write "</td>"
ElseIf rPublished > 0 Then
	Response.Write "<td><img src=""/img/base/subArrowMoreN.gif"" width=""14"" height=""14"" NAME=""subArrowImg3"" border=""0""></td><td><img src=""/img/null.gif"" width=""6"" height=""1""></td><td class=""redBold"" nowrap><A href=""PQLmiracle.asp?Area=3#bot"" class=""redSmallBold"" ONMOUSEOUT=""swapImgRestore();"" ONMOUSEOVER=""swapImage('subArrowImg3','','/img/base/subArrowMoreH.gif',1)"">"
	Select Case Int("0" & Session("Lang"))
		Case 0	Response.Write "Publicerade mirakel"
		Case 1	Response.Write "Published miracles"
	End Select
	Response.Write "</A></td>"
End If
%>
								<td width="100%">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr><td><img src="/img/null.gif" width="1" height="10"></td></tr>
				<tr>
					<td align="center">
						<table width="520" border="0" cellpadding="0" cellspacing="0" bgcolor="#F3F5F7">
							<tr> 
								<td><img SRC="/img/base/transCornerTopLeft.gif" width="8" height="8"></td>
								<td width="100%"><img SRC="/img/null.gif" width="1" height="1"></td>
								<td><img SRC="/img/base/transCornerTopRight.gif" width="8" height="8"></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>
									<table border="0" cellspacing="0" cellpadding="0" class="darkGraySmall" width="100%">
<%
If rArea = 0 Or rArea = 1 Then
	If rUserID = Int("0" & Session("UserID")) Then
%>
										<tr><td colspan="2"><textarea style="width:500px;" name="Miracle" cols="95" rows="12" class="formbox"<% If rUserID <> Int("0" & Session("UserID")) Then Response.Write " DISABLED" End If %>><%=rMiracle%></textarea></td></tr>
										<tr>
											<td nowrap><input type="checkbox" name="AllowPublish" value="1"<% If rAllowPublish = 1 Then Response.Write " CHECKED" End If %>>&nbsp;<%
		Select Case Int("0" & Session("Lang"))
			Case 0	Response.Write "Jag tillåter att mitt mirakel får publiceras anonymt på sajten."
			Case 1	Response.Write "I give permission for my miracle to be published anonymously on the website."
		End Select
%></td>
											<td align="right" nowrap><% If rUserID = Int("0" & Session("UserID")) And rExerciseMiracleID <> 0 Then %><a href="JavaScript:if(confirm('<%
		Select Case Int("0" & Session("Lang"))
			Case 0	Response.Write "Är du säker på att du vill ta bort detta mirakel?"
			Case 1	Response.Write "Are you sure you want to delete this miracle?"
		End Select
%>')){window.location='PQLmiracle.asp?Action=Delete&ExerciseMiracleID=<%=rExerciseMiracleID%>'}" onMouseOut="swapImgRestore()" onMouseOver="swapImage('DeleteButton','','/img/button/deleteH<%=Int("0" & Session("Lang"))%>.gif',1)"><img SRC="/img/button/deleteN<%=Int("0" & Session("Lang"))%>.gif" name="DeleteButton" border="0"></a>&nbsp;<a href="JavaScript:document.forms.stdfrm.SaveMiracle.value='1';document.forms.stdfrm.submit();" onMouseOut="swapImgRestore()" onMouseOver="swapImage('UpdateButton','','/img/button/updateH<%=Int("0" & Session("Lang"))%>.gif',1)"><img SRC="/img/button/updateN<%=Int("0" & Session("Lang"))%>.gif" name="UpdateButton" border="0"></a><% Else %><a href="JavaScript:document.forms.stdfrm.SaveMiracle.value='1';document.forms.stdfrm.submit();" onMouseOut="swapImgRestore()" onMouseOver="swapImage('SaveButton','','/img/button/saveH<%=Int("0" & Session("Lang"))%>.gif',1)"><img SRC="/img/button/saveN<%=Int("0" & Session("Lang"))%>.gif" name="SaveButton" border="0"></a><% End If %></td>
										</tr>
<%
	Else
%>
										<tr><td><%=Replace("" & rMiracle,vbCrLf,"<BR>")%></td></tr>
<%
	End If
ElseIf rArea = 2 Then
%>
										<tr><td class="blueSmallBold"><%
		Select Case Int("0" & Session("Lang"))
			Case 0	Response.Write "Mirakel"
			Case 1	Response.Write "Miracle"
		End Select
%>&nbsp;&nbsp;</td><td class="blueSmallBold"><%
		Select Case Int("0" & Session("Lang"))
			Case 0	Response.Write "Skapat"
			Case 1	Response.Write "Created"
		End Select
%>&nbsp;&nbsp;</td><td class="blueSmallBold"><%
		Select Case Int("0" & Session("Lang"))
			Case 0	Response.Write "Senast ändrat"
			Case 1	Response.Write "Last changed"
		End Select
%>&nbsp;&nbsp;</td><td class="blueSmallBold" align="center"><%
		Select Case Int("0" & Session("Lang"))
			Case 0	Response.Write "Tillåt publicering"
			Case 1	Response.Write "Allow to be published"
		End Select
%></td></tr>
<%
	rs.Open "SELECT ExerciseMiracleID, Miracle, dbo.cf_yearMonthDay(DateTime) AS DateTime, dbo.cf_yearMonthDay(DateTimeChanged) AS DateTimeChanged, AllowPublish FROM [ExerciseMiracle] WHERE UserID = " & Int("0" & Session("UserID")), sql
	While Not rs.EOF
		Response.Write "<tr><td><a href=""PQLmiracle.asp?ExerciseMiracleID=" & rs("ExerciseMiracleID") & "#bot"">" & Left("" & rs("Miracle"),20)
		If Len("" & rs("Miracle")) > 20 Then
			Response.Write "..."
		End If
		Response.Write "</a>&nbsp;&nbsp;</td><td>" & rs("DateTime") & "&nbsp;&nbsp;</td><td>"
		If Not IsNull(rs("DateTimeChanged")) Then
			Response.Write rs("DateTimeChanged")
		End If
		Response.Write "&nbsp;&nbsp;</td><td align=""center""><input type=""checkbox"" name=""Tmp" & Int(Rnd*1000) & """ value=""1"""
		If rs("AllowPublish") Then
			Response.Write " CHECKED"
		End If
		Response.Write " DISABLED></td></tr>"
		rs.MoveNext
	Wend
	rs.Close
ElseIf rArea = 3 Then
%>
										<tr><td class="blueSmallBold"><%
		Select Case Int("0" & Session("Lang"))
			Case 0	Response.Write "Mirakel"
			Case 1	Response.Write "Miracle"
		End Select
%></td></tr>
<%	
	rs.Open "SELECT ExerciseMiracleID, Miracle FROM [ExerciseMiracle] WHERE AllowPublish = 1 AND Published = 1", sql
	While Not rs.EOF
		Response.Write "<tr><td class=""darkGraySmall"" colspan=""2""><a href=""PQLmiracle.asp?ExerciseMiracleID=" & rs("ExerciseMiracleID") & "#bot"">" & Left("" & rs("Miracle"),40)
		If Len("" & rs("Miracle")) > 40 Then
			Response.Write "..."
		End If
		Response.Write "</a></td></tr>"
		rs.MoveNext
	Wend
	rs.Close
End If
%>
									</table>
								</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td><img SRC="/img/base/transCornerBottomLeft.gif" width="8" height="8"></td>
								<td width="100%"><img SRC="/img/null.gif" width="1" height="1"></td>
								<td><img SRC="/img/base/transCornerBottomRight.gif" width="8" height="8"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
		<td><img src="/img/null.gif" width="30" height="1"></td>
	</tr>
	<tr><td colspan="3"><A NAME="bot"></A><img src="/img/null.gif" width="1" height="30"></td></tr>
	<tr><td colspan="3" align="center" class="darkGraySmall">[<a href="javascript:close()"><%
Select Case Int("0" & Session("Lang"))
	Case 0	Response.Write "st&auml;ng f&ouml;nster"
	Case 1	Response.Write "close window"
End Select
%></a>]</td></tr>
	<tr><td colspan="3"><img src="/img/null.gif" width="1" height="10"></td></tr>
	<tr>
		<td colspan="3" align="center" class="darkGraySmall">
<%
Select Case Int("0" & Session("Lang"))
	Case 0
%>
			&copy; Copyright 2003, Bengt Arnetz och Dan Hasson, Uppsala Universitet.
			<BR>
			Denna övning får inte kopieras, spridas eller ändras utan skriftligt medgivande från upphovsmännen.
<%
	Case 1
%>
			&copy; Copyright 2003, Bengt Arnetz and Dan Hasson, Uppsala Universitet.
			<BR>
			This exercise may not be copied, distributed or changed without written permission from the authors.
<%
End Select
%>
		</td>
	</tr>
	<tr><td colspan="3"><img src="/img/null.gif" width="1" height="10"></td></tr>
</form>
</table>
</body>
<!--#include virtual="/includes/bot.asp"-->
