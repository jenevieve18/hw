<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="superadmin.aspx.cs" Inherits="HWgrp.superadmin" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=Db.header()%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
    <form id="Form1" method="post" runat="server">
		<div class="container_16" id="admin">
		<%=Db2.nav()%>
        <div class="contentgroup grid_16">
            <div class="smallContent">
		        <h3>Administration</h3>
		        <table border="0" cellspacing="0" cellpadding="5">
		            <!--<asp:Label ID=SponsorID runat=server />-->
                    <tr>
                        <td><b>Name</b></td>
                        <td><b># of ext<br>surveys</b></td>
                        <td><b># of added<br>users</b></td>
                        <td><b># of invited<br>users</b></td>
                        <td><b># of activated<br>users</b></td>
                        <td><b>1st invite sent</b></td>
                        <td><b>Super privileges</b></td>
                    </tr>
                    <% int cx = 0, bx = 0, totInvitees = 0, totNonClosedInvites = 0, totActive = 0, totNonClosedActive = 0; %>
                    <% foreach (var s in sponsorRepository.FindAndCountDetailsBySuperAdmin(superAdminID)) { %>
                    <tr <%= cx % 2 == 0 ? "style='background-color:#f2f2f2'" : "" %>>
                        <% totInvitees += s.SentInvites.Count; %>
                        <td>
                            <a target="_blank" href="<%= HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.Url.PathAndQuery.Substring(0, HttpContext.Current.Request.Url.PathAndQuery.LastIndexOf("/")) + "/default.aspx?SA=0&SKEY=" + s.SponsorKey + s.Id.ToString() %>"><%= s.Name %></a>
                        </td>
                        <td><%= s.ExtendedSurveys.Count %></td>
                        <td><%= s.Invites.Count %></td>
                        <td><%= s.SentInvites.Count %></td>
                        <td><%= s.ActiveInvites.Count %></td>
                        <td><%= s.MinimumInviteDate == null ? "N/A" : s.MinimumInviteDate.Value.ToString("yyyy-MM-dd") %></td>
                        <td><%= !s.SuperAdminSponsors[0].SeeUsers ? "No" : "Yes" %></td>
                        <td>
                            <% if (s.MinimumInviteDate != null) { %>
                                <a href="superadmin.aspx?ATSID=<%= s.Id %>"><img border="0" src="img/auditTrail.gif"/></a>
                            <% } %>
                            <% if (s.Closed) { %>
                                <span style="color:#cc0000;"><%= s.ClosedAt.Value.ToString("yyyy-MM-dd") %></span>
                            <% } %>
                        </td>
                        <% if (WithATSID(s.Id)) { %>
                            <% int y = s.MinimumInviteDate.Value.Year; int m = s.MinimumInviteDate.Value.Month; %>
                            <% int y2 = DateTime.Now.Year; int m2 = DateTime.Now.Month; %>
                            <% for (int i = y; i <= y2; i++) { %>
                                <% for (int ii = (i == y ? m : 1); ii <= (i == y2 ? m2 : 12); ii++) { %>
                                    <% string iii = i.ToString() + "-" + ii.ToString().PadLeft(2, '0'); %>
                                    <tr <%= cx % 2 == 0 ? "style='background-color:#f2f2f2'" : "" %>>
                                        <td><%= iii %></td>
                                        <td></td>
                                        <td></td>
                                        <% DateTime dt = DateTime.Parse(iii + "-01").AddMonths(1); %>
                                        <td><%= sponsorRepository.CountSentInvitesBySponsor(s.Id, dt).ToString() %></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                <% } %>
                            <% } %>
                        <% } %>
                        <% cx++; %>
                    </tr>
                    <% } %>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                        <td>
                            <% if (totNonClosedInvites != totInvitees) { %>
                                <span style="text-decoration:line-through;color:#cc0000;"><%= totInvitees %></span>
                            <% } else { %>
                                <b><%= totNonClosedInvites%></b>
                            <% } %>
                        </td>
                        <td>
                            <% if (totNonClosedActive != totActive) { %>
                                <span style="text-decoration:line-through;color:#cc0000;"><%= totActive%></span>
                            <% } else { %>
                                <b><%= totNonClosedActive %></b>
                            <% } %>
                        </td>
                    </tr>
		        </table>
                <hr />
                
                <asp:PlaceHolder ID=ESS runat=server>
		        <h3>Extended survey statistics</h3>
		        <table border="0" cellspacing="0" cellpadding="5">
		            <tr>
		            	<td><B>Unit</td>
                        <td><asp:TextBox ID=MeasureTxt1 runat=server Text="Measure 1"/></td>
                        <td><B><asp:TextBox ID=MeasureTxt2 runat=server Text="Measure 2"/></B></td>
                        <td><B>Not included</B></td>
                        <td><b>Answer count</b></td>
                    </tr>
			        <!--<asp:Label ID=ExtendedSurvey runat=server />-->
                    <tr>
                        <td><i>Database, all <b>other</b> organizations</i></td>
                        <td><input type="radio" value="1" name="Measure0"/></td>
                        <td><input type="radio" value="2" name="Measure0"/></td>
                        <td><input type="radio" checked="" value="0" name="Measure0"/></td>
                    </tr>
                    <% int cx = 0, bx = 0; %>
                    <% foreach (var u in sponsorRepository.FindExtendedSurveysBySuperAdmin(superAdminID)) { %>
                    <tr <%= cx % 2 == 0 ? "style='background-color:#f2f2f2'" : "" %>>
                        <% UpdateExtendedSurvey(u); %>
                        <td><%= u.Sponsor.Name + (u.Internal != "" ? ", " + u.Internal : "") + (u.RoundText != "" ? ", " + u.RoundText : "") %></td>
                        <td><input type="radio" value="1" name="Measure<%= u.ProjectRoundUnit.Id %>"/></td>
                        <td><input type="radio" value="2" name="Measure<%= u.ProjectRoundUnit.Id %>"/></td>
                        <td><input type="radio" checked="" value="0" name="Measure<%= u.ProjectRoundUnit.Id %>"/></td>
                        <td><%= u.ProjectRoundUnit.Answers.Count %></td>
                        <% cx++; %>
                        <% bx += u.ProjectRoundUnit.Answers.Count; %>
                    </tr>
                    <% } %>
                    <tr style="background-color:#cccccc;">
                        <td><i>Total for your organization(s)</i></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td><%= bx %></td>
                    </tr>
		        </table>
		        Show statistics named <asp:TextBox ID="SurveyName" runat="server" /> based on questions in survey <asp:DropDownList ID="SurveyID" runat=server /><asp:Button Text="Submit" ID=submit runat=server /><br />
                <hr />
                </asp:PlaceHolder>
                <% ESS.Visible = cx > 0; %>
		        
		        <h3>Survey statistics</h3>
                <table border="0" cellspacing="0" cellpadding="5">
                    <tr>
                        <td><B>Unit</td>
                        <td><asp:TextBox ID=Measure2Txt1 runat=server Text="Measure 1"/></td>
                        <td><B><asp:TextBox ID=Measure2Txt2 runat=server Text="Measure 2"/></B></td>
                        <td><B>Not included</B></td>
                        <td><b>Last month<br />answer count</b></td>
                    </tr>
			        <!--<asp:Label ID=Survey runat=server />-->
			        <tr>
			        	<td><i>Database, all <b>other</b> organizations</i></td>
			        	<td><input type="radio" value="1" name="Measure_0"/></td>
			        	<td><input type="radio" value="2" name="Measure_0"/></td>
			        	<td><input type="radio" checked="" value="0" name="Measure_0"/></td>
			        </tr>
                    <% cx = 0; bx = 0; %>
                    <% foreach (var u in sponsorRepository.FindDistinctRoundUnitsWithReportBySuperAdmin(superAdminID)) { %>
                    <tr <%= cx % 2 == 0 ? "style='background-color:#f2f2f2'" : "" %>>
                        <% UpdateProjectRoundUnit(u); %>
                        <td><%= u.Sponsor.Name %>, <%= u.Navigation %></td>
                        <td><input type="radio" value="1" name="Measure_<%= u.ProjectRoundUnit.Id %>"/></td>
                        <td><input type="radio" value="2" name="Measure_<%= u.ProjectRoundUnit.Id %>"/></td>
                        <td><input type="radio" checked="" value="0" name="Measure_<%= u.ProjectRoundUnit.Id %>"/></td>
                        <td><%= u.ProjectRoundUnit.Answers.Count %></td>
                        <% cx++; %>
                        <% bx += u.ProjectRoundUnit.Answers.Count; %>
                    </tr>
                    <% } %>
                    <tr style="background-color:#cccccc">
                        <td><i>Total for your organization(s)</i></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td><%= bx %></td>
                    </tr>
		        </table>
		        <asp:DropDownList ID=FromDT runat=server />--<asp:DropDownList ID=ToDT runat=server /><asp:DropDownList ID="ReportID" runat=server /><asp:Button Text="Submit" ID=submit2 runat=server />
            </div>
        </div>
	</form>
</body>
</html>
