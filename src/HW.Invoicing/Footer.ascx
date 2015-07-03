<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="HW.Invoicing.Footer" %>
<%@ Import namespace="HW.Core.Helpers" %>
<footer>
	<div class="container">
	    <div class="row clearfix">
		    <div class="col-md-2 column">
			    <h6>Copyright &copy; <%= DateTime.Now.Year %></h6>
			    <p>All pictures and text are copyright. Copyright (http://www.healthwatch.se)</p>
<p>You may download anything on these ages for your own personal, non-commercial use, including using the pictures on your own webstite if a copyright notice is included.</p>
<p>For any other purposes please contact info@healthwatch.se</p>
<p>Links to internet sites from this site should not be construed as an endorsement of the views contained therein.</p>
		    </div>
		    <div class="col-md-4 column">
			    <h6>About Us</h6>
			    <p>
				    HealthWatch provides tools for individuals and organisations to preserve and increase health and quality of life, as well as reduce stress-releated problems. HealthWatch is run by Interactive Health Group in Stockholm AB.
			    </p>
                <address>
                    <strong>Interactive Health Group</strong><br />
                    Stockholm AB<br />
                    Box 4047<br />
                    10261 Stockholm<br />
                    Sweden<br />
                    <abbr title="Phone">P:</abbr> (123) 456-7890
                </address>
		    </div>
		    <div class="col-md-2 column">
			    <h6>Navigation</h6>
			    <ul>
				    <li><%= HtmlHelper.Anchor("Home", "home.aspx") %></li>
				    <li><%= HtmlHelper.Anchor("Products", "") %></li>
				    <li><%= HtmlHelper.Anchor("Services", "") %></li>
				    <li><%= HtmlHelper.Anchor("About Us", "") %></li>
			    </ul>
                <ul>
				    <li><%= HtmlHelper.Anchor("Report an Issue", "issueadd.aspx") %></li>
                </ul>
		    </div>
		    <div class="col-md-2 column">
			    <h6>Follow Us</h6>
			    <ul>
				    <li><%= HtmlHelper.Anchor("Facebook", "") %></li>
				    <li><%= HtmlHelper.Anchor("Twitter", "") %></li>
				    <li><%= HtmlHelper.Anchor("Instagram", "") %></li>
				    <li><%= HtmlHelper.Anchor("Google+", "") %></li>
				    <li><%= HtmlHelper.Anchor("LinkedIn", "") %></li>
			    </ul>
		    </div>
		    <div class="col-md-2 column">
		    </div>
	    </div>
    </div>
</footer>