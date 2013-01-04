<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HWgrp.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <% foreach (var s in sponsorRepository.FindAndCountDetailsBySuperAdmin(1)) { %>
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
</asp:Content>
