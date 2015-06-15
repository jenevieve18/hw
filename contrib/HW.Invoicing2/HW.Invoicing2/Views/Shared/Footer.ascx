<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<footer>
	<div class="container">
	    <div class="row clearfix">
		    <div class="col-md-2 column">
			    <h6>Copyright &copy; <%= DateTime.Now.Year %></h6>
			    <p>
				    Lorem ipsum dolor sit amet, <strong>consectetur adipiscing elit</strong>. Aliquam eget sapien sapien. Curabitur in metus urna. In hac habitasse platea dictumst. Phasellus eu sem sapien, sed vestibulum velit. Nam purus nibh, lacinia non faucibus et, pharetra in dolor. Sed iaculis posuere diam ut cursus. <em>Morbi commodo sodales nisi id sodales. Proin consectetur, nisi id commodo imperdiet, metus nunc consequat lectus, id bibendum diam velit et dui.</em> Proin massa magna, vulputate nec bibendum nec, posuere nec lacus. <small>Aliquam mi erat, aliquam vel luctus eu, pharetra quis elit. Nulla euismod ultrices massa, et feugiat ipsum consequat eu.</small>
			    </p>
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
				    <li><%: Html.ActionLink("Home", "Index", "Home")%></li>
				    <li><%: Html.ActionLink("Products", "Products", "Home")%></li>
				    <li><%: Html.ActionLink("Services", "Services", "Home")%></li>
				    <li><%: Html.ActionLink("About Us", "About", "Home")%></li>
			    </ul>
                <ul>
				    <li><%: Html.ActionLink("Report an Issue", "Index", "Issue")%></li>
                </ul>
		    </div>
		    <div class="col-md-2 column">
			    <h6>Follow Us</h6>
			    <ul>
				    <li><%: Html.ActionLink("Facebook", "", "")%></li>
				    <li><%: Html.ActionLink("Twitter", "", "")%></li>
				    <li><%: Html.ActionLink("Instagram", "", "")%></li>
				    <li><%: Html.ActionLink("Google+", "", "")%></li>
				    <li><%: Html.ActionLink("LinkedIn", "", "")%></li>
			    </ul>
		    </div>
		    <div class="col-md-2 column">
		    </div>
	    </div>
    </div>
</footer>