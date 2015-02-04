<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="SuperAdmin.aspx.cs" Inherits="HW.Grp.SuperAdmin" %>
<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
        <div class="search">
            <%= R.Str(lid, "search.email", "Search user by email") %>
            <asp:TextBox ID="SearchEmail" runat="server" />
            <asp:Button ID="Search" Text="Search" runat="server" onclick="Search_Click" />
        </div>
        
        <% if (users != null) { %>
            <% if (users.Count > 0) { %>
                <div class="smallContent">
                    <table border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <th><%= R.Str(lid, "manager.username", "Username")%></th>
                            <th><%= R.Str(lid, "email", "Email")%></th>
                            <th><%= R.Str(lid, "sponsor", "Sponsor")%></th>
                            <th><%= R.Str(lid, "department", "Department")%></th>
                            <th><%= R.Str(lid, "invite.resolve", "Resolve<br/>Invitation")%></th>
                        </tr>
                        <% var j = 0; %>
                        <% foreach (var u in users) { %>
                            <tr<%= (j % 2 == 0) ? " bgcolor='#F2F2F2'" : "" %>>
                                <td><%= u.Name%></td>
                                <td><%= u.Email%></td>
                                <td><%= u.Sponsor.Name%></td>
                                <td><%= u.Department.Name%></td>
                                <td>
                                    <% var i = u.Sponsor.SentInvites[0]; %>
                                    <a href="superadmin.aspx?SearchEmail=<%= searchQuery %>&SponsorID=<%= u.Sponsor.Id %>&SendSPIID=<%= i.Id %>&Rnd=<%= (new Random(unchecked((int)DateTime.Now.Ticks))).Next() %>"><%= i.Sent == null ? "Send" : "Resend" %></a>
                                </td>
                            </tr>
                            <% j++; %>
                        <% } %>
                    </table>
                </div>
            <% } else { %>
                <p>0 <%= R.Str(lid, "found.results", "results found.")%></p>
            <% } %>
        <% } %>
        <hr />

        <div class="smallContent">
		    <h3><%= R.Str(lid, "administration", "Administration")%></h3>
		    <table border="0" cellspacing="0" cellpadding="5">
		        <asp:Label ID=SponsorID runat=server />
		    </table>
            <hr />
            <asp:PlaceHolder ID=ESS runat=server>
				<h3><%= R.Str(lid, "survey.extended.stats", "Extended survey statistics")%></h3>
				<table border="0" cellspacing="0" cellpadding="5">
					<tr>
						<td><b><%= R.Str(lid, "unit", "Unit")%></b></td>
						<td><asp:TextBox ID=MeasureTxt1 runat=server Text="Measure 1"/></td>
						<td><b><asp:TextBox ID=MeasureTxt2 runat=server Text="Measure 2"/></b></td>
						<td><b><%= R.Str(lid, "included.not", "Not included")%></B></td>
						<td><b><%= R.Str(lid, "count.answer", "Answer count")%></b></td>
					</tr>
					<asp:Label ID=ExtendedSurvey runat=server />
				</table>
				<%= R.Str(lid, "stats.named", "Show statistics named")%> 
                <asp:TextBox ID=SurveyName runat=server /> 
                <%= R.Str(lid, "questions.based", "based on questions in survey")%>
                <asp:DropDownList ID="SurveyID" runat=server />
                <asp:Button Text="Submit" ID=submit runat=server /><br />
				<hr />
            </asp:PlaceHolder>
		    <h3>Survey statistics</h3>
            <table border="0" cellspacing="0" cellpadding="5">
				<tr>
					<td><b><%= R.Str(lid, "unit", "Unit")%></b></td>
					<td><asp:TextBox ID=Measure2Txt1 runat=server Text="Measure 1"/></td>
					<td><b><asp:TextBox ID=Measure2Txt2 runat=server Text="Measure 2"/></b></td>
					<td><b><%= R.Str(lid, "included.not", "Not included")%></b></td>
					<td><b><%= R.Str(lid, "answer.last", "Last month<br />answer count")%></b></td>
				</tr>
			    <asp:Label ID=Survey runat=server />
		    </table>
		    <asp:DropDownList ID=FromDT runat=server />--<asp:DropDownList ID=ToDT runat=server /><asp:DropDownList ID="ReportID" runat=server />
            <asp:Button Text="Submit" ID=submit2 runat=server />
        </div>
    </div>

</asp:Content>
