<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.Invoicing.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="carousel slide" id="carousel-861880">
	<ol class="carousel-indicators">
		<li class="active" data-slide-to="0" data-target="#carousel-861880"></li>
		<li data-slide-to="1" data-target="#carousel-861880"></li>
		<li data-slide-to="2" data-target="#carousel-861880"></li>
	</ol>
	<div class="carousel-inner">
		<div class="item active">
			<img alt="" src="http://lorempixel.com/1600/500/sports/1" />
			<div class="carousel-caption">
				<h4>First Thumbnail label</h4>
				<p>
					Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.
				</p>
			</div>
		</div>
		<div class="item">
			<img alt="" src="http://lorempixel.com/1600/500/sports/2" />
			<div class="carousel-caption">
				<h4>Second Thumbnail label</h4>
				<p>
					Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.
				</p>
			</div>
		</div>
		<div class="item">
			<img alt="" src="http://lorempixel.com/1600/500/sports/3" />
			<div class="carousel-caption">
				<h4>Third Thumbnail label</h4>
				<p>
					Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.
				</p>
			</div>
		</div>
	</div> <a class="left carousel-control" href="#carousel-861880" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a> <a class="right carousel-control" href="#carousel-861880" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>
</div>

<div class="row clearfix">
	<div class="col-md-4 column">
		<h2>Welcome</h2>
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
			Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui.
		</p>
		<p><a class="btn" href="#">View details »</a></p>
	</div>
</div>


</asp:Content>
