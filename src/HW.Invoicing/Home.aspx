<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HW.Invoicing.Home1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="row clearfix">
	<div class="col-md-4 column">
		<h2>Welcom</h2>
		<p>
			Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui.
		</p>
		<p><a class="btn" href="#">View details »</a></p>
	</div>
	<div class="col-md-4 column">
		<h2>News</h2>
		<p>
			<% if (latestNews != null) { %>
                <%= latestNews.Content %>
            <% } %>
		</p>
		<p><a class="btn" href="#">View details »</a></p>
	</div>
	<div class="col-md-4 column">
		<h2>About Us</h2>
		<p>
			HealthWatch provides tools for individuals and organisations to preserve and increase health and quality of life, as well as reduce stress-releated problems. HealthWatch is run by Interactive Health Group in Stockholm AB.
		</p>
		<p><a class="btn" href="#">View details »</a></p>
	</div>
</div>

</asp:Content>
