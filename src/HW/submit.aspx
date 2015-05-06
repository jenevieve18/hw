<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="submit.aspx.cs" Inherits="healthWatch.submit" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
  <%=healthWatch.Db.header2("","")%>
     <script type="text/JavaScript">
         //		function showAll()
         //		{
         //			a = document.getElementsByTagName('tr');
         //			for(i=0;i<a.length;i++)
         //			{
         //				a[i].style.display='';
         //			}
         //			a = document.getElementsByTagName('div');
         //			for(i=0;i<a.length;i++)
         //			{
         //				a[i].style.display='';
         //			}
         //		}
         //		var hasval = false;
         //		
         //		function setTxt(s)
         //		{
         //			hasval = (s.length > 0);
         //		}
         //		
         //		function chkTxt(s,f)
         //		{
         //			if(hasval && s == '')
         //			{
         //				updateButtons(-1,f);
         //			}
         //			else if(!hasval && s != '')
         //			{
         //				updateButtons(1,f);
         //			}
         //			setTxt(s);
         //		}
         //		
         //		function isChecked(r)
         //		{ 
         //			var i = r.length;
         //			var c = false;
         //			while(i-- > 0)
         //			{
         //				c = r[i].checked;
         //				if(c)
         //				{
         //					break;
         //				}
         //			} 
         //			return c; 
         //		}
         //		
         //		function vi(qi,s)
         //		{
         //			if(document.getElementById('Q' + qi + 'A'))
         //				document.getElementById('Q' + qi + 'A').style.display = (s ? '' : 'none');
         //				
         //			if(document.getElementById('Q' + qi + 'Q'))
         //				document.getElementById('Q' + qi + 'Q').style.display = (s ? '' : 'none');
         //				
         //			if(document.getElementById('Q' + qi + 'H'))
         //				document.getElementById('Q' + qi + 'H').style.display = (s ? '' : 'none');
         //				
         //			if(document.getElementById('Q' + qi + 'S1'))
         //				document.getElementById('Q' + qi + 'S1').style.display = (s ? '' : 'none');
         //				
         //			if(document.getElementById('Q' + qi + 'S2'))
         //				document.getElementById('Q' + qi + 'S2').style.display = (s ? '' : 'none');
         //				
         //			if(document.getElementById('Q' + qi + 'S3'))
         //				document.getElementById('Q' + qi + 'S3').style.display = (s ? '' : 'none');
         //		}
         //		
         //		function isVal(r,v)
         //		{ 
         //			var i = r.length;
         //			var c = false;
         //			while(i-- > 0)
         //			{
         //				c = (r[i].value == v && r[i].checked);
         //				if(c)
         //				{
         //					break;
         //				}
         //			} 
         //			return c; 
         //		}
         //		
         //		function setRad(r)
         //		{
         //			hasval = isChecked(r);
         //		}
         //		
         //		function chkRad(r,f)
         //		{
         //			var c = isChecked(r);
         //			if(hasval && !c)
         //			{
         //				updateButtons(-1,f);
         //			}
         //			else if(!hasval && c)
         //			{
         //				updateButtons(1,f);
         //			}
         //			hasval = c;
         //		}
         //		
         //		function updateButtons(act,f)
         //		{
         //			var totl = parseInt(eval('document.forms[0].COMPLETEDcount.value'));
         //			var s;
         //			if(f)
         //			{
         //				var cmpl = parseInt(eval('document.forms[0].FORCEDcompleted.value'));
         //				var reqd = parseInt(eval('document.forms[0].FORCEDcount.value'));
         //		
         //				if(reqd == cmpl && parseInt(act) == -1)
         //				{
         //					if(s = document.getElementById('SendCtrl'))
         //					{
         //						s.src = 'submitImages/button_send0_0.gif';
         //					}
         //				}
         //				else if(cmpl + parseInt(act) >= reqd)
         //				{
         //					if(s = document.getElementById('SendCtrl'))
         //					{
         //						s.src = 'submitImages/button_send0_1.gif';
         //					}
         //				}
         //				if(parseInt(act) != 0)
         //				{
         //					eval('document.forms[0].FORCEDcompleted.value='+(cmpl+parseInt(act)));
         //				}
         //			}

         //			if(totl > 0 && totl + parseInt(act) == 0)
         //			{
         //				if(s = document.getElementById('SaveCtrl'))
         //				{
         //					s.src = 'submitImages/button_save0_0.gif';
         //				}
         //			}
         //			else if(totl + parseInt(act) > 0)
         //			{
         //				if(s = document.getElementById('SaveCtrl'))
         //				{
         //					s.src = 'submitImages/button_save0_1.gif';
         //				}
         //			}
         //			if(parseInt(act) != 0)
         //			{
         //				eval('document.forms[0].COMPLETEDcount.value='+(totl+parseInt(act)));
         //			}
         //		}
	</script>
    	<script type="text/javascript">
    	    $(document).ready(function () {
    	        $('.slider').slider({
    	            min: 0,
    	            max: 377,
    	            value: -180,
    	            step: 1,
    	            create: function (event, ui) {
    	                //$(this).find(".ui-slider-handle").css('background', 'none')
    	                $(this).parent().find(".empty").hide()
    	            },
    	            start: function (event, ui) {
    	                $(this).find(".ui-slider-handle").css('width', '19px')
    	                $(this).find(".ui-slider-handle").css('background', 'url("includes/ui/resources/VASknob.gif") 0px 6px no-repeat scroll')
    	            },
    	            change: function (event, ui) {
    	                $(this).find(".ui-slider-handle").css('width', '19px')
    	                $(this).find(".ui-slider-handle").css('background', 'url("includes/ui/resources/VASknob.gif") 0px 6px no-repeat scroll')
    	                $(this).parent().find(".empty").show()
    	                //x = $(this).parent().attr('id');
    	                x = $(this).attr('id');
    	                z = Math.round((ui.value / 377) * 100) //NOTE THIS, update if you change max!
    	                $("input[name=" + x + "]").val(z)
    	            }
    	        });
    	        $('.empty').hover(function () {
    	            $(this).css('background', 'url("includes/ui/resources/VASeraseA.gif")')
    	        }, function () {
    	            $(this).css('background', 'url("includes/ui/resources/VASeraseN.gif")')
    	        });
    	        $('.empty').click(function () {
    	            $(this).parent().find(".ui-slider-handle").css('width', '1px')
    	            $(this).parent().find(".ui-slider-handle").css('background', 'none')
    	            //x = $(this).parent().attr('id');
    	            x = $(this).attr('id');
    	            $("input[name=" + x + "]").val("NULL");
    	            $(this).hide()
    	        });

    	        $(".hundrfix").click(function () {
    	            $(this).parent().find(".slider").slider("value", 377);
    	            $(this).parent().find(".ui-slider-handle").css('width', '19px')
    	            $(this).parent().find(".ui-slider-handle").css('background', 'url("includes/ui/resources/VASknob.gif") 0px 6px no-repeat scroll')
    	        })


    	        //    	        $("form").submit(function () {
    	        //    	            alert($(this).serialize().split('&'))
    	        //    	            return false
    	        //    	        });

    	        //    	        if ($("body").attr("class") == "ie6") {
    	        //    	            $("form input[type=submit]").val("Spara");
    	        //    	        }
    	    })
	</script>
  </head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
	    <form id="form1" method="post" runat="server">
        <div class="container_16 myhealth<%=healthWatch.Db.cobranded() %>">
		<div class="headergroup grid_16">
	   <%=healthWatch.Db.nav2()%>
		</div> <!-- end .headergroup -->
        <!--
		<h1 id="mainHeader"></h1>
		<table border="0" cellpadding="0" cellspacing="0"><tr><td>
		<div style="position:relative;width:560px;">
		
		
		<asp:PlaceHolder ID="Buttons" runat="server"/>
		</div>
		</td><td><img src="img/null.gif" width="20" height="1" /></td><td valign="top">
		<asp:Label ID="Forms" runat=server />
		</td></tr></table>
        -->
			<div class="contentgroup grid_16">

			  <div id="formchoser" visible=false runat=server>
			    <!--<div><span class="lastform">Senaste formul&auml;r</span></div>-->
			    <div id="shortCutForms" runat=server class="forms"></div>
                <!--
			    <div class="more">
			      <span class="otherform">Andra formul&auml;r</span>
			      <select>
			        <option>Välj här</option>
			        <option>eller här</option>
			      </select>
			    </div>
                -->
			  </div>
			  
				<h1 class="header"><asp:Label ID="SurveyName" runat="server" /></h1>
				<div class="meta"><div><asp:Label ID="SurveyIntro" Runat="server"/></div> <div id=Help runat=server class="help" visible=false></div></div>
	
					<div class="questions">
                        <asp:PlaceHolder ID="Survey" Runat="server"/>
					</div><!-- end .questions -->
					<div class="form_footer">
						<asp:Button ID="submitBtn" CssClass="rfloat" runat=server />
						<!--<a href="#" onclick="javascript:location.reload();">Eller klicka h&auml;r om du vill avbryta.</a>-->
					</div>
		</div><!-- end .contentgroup	-->
		<%=healthWatch.Db.bottom2() %>
	  <div class="hidden"><img src="includes/ui/resources/VASeraseA.gif" width="22" height="22" alt="VASeraseA"></div>
    </div> <!-- end .container_12 -->
    </form>
</body>
</html>
