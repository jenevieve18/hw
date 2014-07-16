<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="SuperAdmin.aspx.cs" Inherits="HW.Grp.SuperAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
        <div class="search">
            Search user by email
            <asp:TextBox ID="SearchEmail" runat="server" />
            <asp:Button ID="Search" Text="Search" runat="server" onclick="Search_Click" />
        </div>
        
        <% if (users != null) { %>
            <% if (users.Count > 0) { %>
                <div class="smallContent">
                    <table border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <th>Username</th>
                            <th>Email</th>
                            <th>Sponsor</th>
                            <th>Department</th>
                            <th>Resolve<br />Invitation</th>
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
                <p>0 results found.</p>
            <% } %>
        <% } %>
        <hr />

        <div class="smallContent">
		    <h3>Administration</h3>
		    <table border="0" cellspacing="0" cellpadding="5">
		        <asp:Label ID=SponsorID runat=server />
		    </table>
            <hr />
            <asp:PlaceHolder ID=ESS runat=server>
				<h3>Extended survey statistics</h3>
				<table border="0" cellspacing="0" cellpadding="5">
					<tr><td><B>Unit</td><td><asp:TextBox ID=MeasureTxt1 runat=server Text="Measure 1"/></td><td><B><asp:TextBox ID=MeasureTxt2 runat=server Text="Measure 2"/></B></td><td><B>Not included</B></td><td><b>Answer count</b></td></tr>
					<asp:Label ID=ExtendedSurvey runat=server />
				</table>
				Show statistics named <asp:TextBox ID=SurveyName runat=server /> based on questions in survey <asp:DropDownList ID="SurveyID" runat=server /><asp:Button Text="Submit" ID=submit runat=server /><br />
				<hr />
            </asp:PlaceHolder>
		    <h3>Survey statistics</h3>
            <table border="0" cellspacing="0" cellpadding="5">
				<tr>
					<td><B>Unit</td>
					<td><asp:TextBox ID=Measure2Txt1 runat=server Text="Measure 1"/></td>
					<td><B><asp:TextBox ID=Measure2Txt2 runat=server Text="Measure 2"/></B></td>
					<td><B>Not included</B></td>
					<td><b>Last month<br />answer count</b></td>
				</tr>
			    <asp:Label ID=Survey runat=server />
		    </table>
		    <asp:DropDownList ID=FromDT runat=server />--<asp:DropDownList ID=ToDT runat=server /><asp:DropDownList ID="ReportID" runat=server />
            <asp:Button Text="Submit" ID=submit2 runat=server />
        </div>
    </div>

</asp:Content>
