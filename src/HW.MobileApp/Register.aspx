<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HW.MobileApp.Register" %>
<%@ Import Namespace="HW.MobileApp" %>
<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />

	<link rel="stylesheet" href="/custom.css" />
    <link rel="Stylesheet" href="css/custom.css" />

    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
    
    <style type="text/css">
        
       
    .header { text-align:left;margin-top:40px;margin-left:-8px;}
    .header h3 { margin-bottom:0; margin-left:20px; }
    .header img { width:235px; }
    
    .ui-grid-a label, .ui-grid-a a  {margin:13px 10px 0px 0px;font-size:12px;}  
    .ui-grid-a .ui-block-a{ width:24%; text-align:right; margin-right:1%;}
    .ui-grid-a .ui-block-b{ width:75%; }
    .ui-select .ui-btn-text { padding:0px !important;font-size:12px;font-weight:normal;}
            
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        
        <div data-role="page" id="register">


<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Login.aspx" rel="external"><%= R.Str("button.cancel") %></a>
    <h1><%= R.Str("register.title") %></h1>
    <a id="createBtn" onserverclick="createBtn_Click" runat="server"><%= R.Str("register.create") %></a>

</div>
<div data-role="content" style="padding:5px;">
    <div style="left:20px;top:42px;position:absolute;font-size:11px;font-style:italic;color:#909090  ;">
        <p><%= R.Str("label.required") %></p>
    </div>

    <div class="header">
        <h3><asp:Label id="lblHeader" runat="server" text="Account Details"></asp:Label></h3>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
        <h3><asp:Label ID="labelMessage" runat="server"></asp:Label></h3>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblLanguage" runat="server" Text="Language" AssociatedControlID="dropDownListLanguage"></asp:Label>
    </div>
    <div class="ui-block-b">
        <fieldset data-role="controlgroup">
        <asp:DropDownList data-mini="true" ID="dropDownListLanguage"
            runat="server" AutoPostBack="true" ViewStateMode="Enabled" 
                EnableViewState="true" 
                onselectedindexchanged="dropDownListLanguage_SelectedIndexChanged">
            <asp:ListItem value="2">English</asp:ListItem>
            <asp:ListItem value="1">Swedish</asp:ListItem>
        </asp:DropDownList></fieldset>
    </div>
    </div>


    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblUsername" runat="server" AssociatedControlID="textBoxUsername">Username<span class="req">*</span>
                </asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxUsername" runat="server"></asp:TextBox>
    </div>
    </div>

   
    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblPassword" runat="server" Text="Password<span class='req'>*</span>" AssociatedControlID="textBoxPassword"></asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxPassword" TextMode="Password" EnableViewState="true" runat="server"></asp:TextBox>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password<span class='req'>*</span>" AssociatedControlID="textBoxConfirmPassword"></asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxConfirmPassword" TextMode="Password" EnableViewState="true" runat="server"></asp:TextBox>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="textBoxEmail">Email<span class="req">*</span></asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxEmail" runat="server"></asp:TextBox>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblAltEmail" runat="server" Text="Alternate Email" AssociatedControlID="textBoxAlternateEmail"></asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxAlternateEmail" runat="server"></asp:TextBox>
    </div>
    </div>


    <div class="header">
        <h3><asp:Label id="Label1" runat="server" text="Personal Information"></asp:Label></h3>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
        <h3><asp:Label ID="label2" runat="server"></asp:Label></h3>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblBirth" runat="server" AssociatedControlID="birthYear"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" data-type="horizontal" >
        <asp:DropDownList ID="birthYear" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="birthMonth" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="birthDay" runat="server" >
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>   

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblGender" runat="server" AssociatedControlID="rdbGenderMale"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:RadioButton ID="rdbGenderMale"  GroupName="rdbGender" Value="1" runat="server" />
        <asp:RadioButton ID="rdbGenderFemale" GroupName="rdbGender" Value="2" runat="server" />
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
    <asp:Label ID="lblStatus" runat="server" AssociatedControlID="rdbStatusMarried" ></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:RadioButton ID="rdbStatusMarried"  GroupName="rdbStatus" Text="Married" Value="369" runat="server" />
        <asp:RadioButton ID="rdbStatusSingle" GroupName="rdbStatus" Text="Single" Value="370" runat="server" />
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
    <asp:Label ID="lblOccupation" runat="server" AssociatedControlID="dropDownListOccupation"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup">
        <asp:DropDownList data-mini="true" ID="dropDownListOccupation" runat="server" 
            AutoPostBack="true" ViewStateMode="Enabled" EnableViewState="true" 
            onselectedindexchanged="dropDownListOccupation_SelectedIndexChanged">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
    <asp:Label ID="lblOccupationType" runat="server" AssociatedControlID="rdbOccupationTypeFull"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:RadioButton ID="rdbOccupationTypeFull" GroupName="rdbOccupationType" Value="405" runat="server" />
        <asp:RadioButton ID="rdbOccupationTypePart" GroupName="rdbOccupationType" Value="406" runat="server" />
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a" id="divIndustry" style="display:none;" runat="server">
    <div class="ui-block-a">
        <asp:Label ID="LblIndustry" runat="server" AssociatedControlID="dropDownListIndustry"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup">
        <asp:DropDownList data-mini="true" ID="dropDownListIndustry" runat="server">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>

    
    <div class="ui-grid-a" id="divJob" style="display:none;" runat="server">
    <div class="ui-block-a">
        <asp:Label ID="LblJob" runat="server" Text="Job"  AssociatedControlID="dropDownListJob"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup">
        <asp:DropDownList data-mini="true" ID="dropDownListJob" runat="server">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a" id="divManagerial" style="display:none;" runat="server">
    <div class="ui-block-a">
    <asp:Label ID="LblManage" runat="server" AssociatedControlID="rdbMangerialYes"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:RadioButton ID="rdbMangerialYes"  GroupName="rdbMangerial" Text="Yes" 
            Value="412" runat="server" oncheckedchanged="rdbMangerialYes_CheckedChanged" AutoPostBack="true"/>
        <asp:RadioButton ID="rdbMangerialNo" GroupName="rdbMangerial" Text="No" 
            Value="413" runat="server"/>
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a" id="divSubordinates" style="display:none;" runat="server">
    <div class="ui-block-a">
        <asp:Label ID="lblSubordinates" runat="server" AssociatedControlID="dropDownListSubordinates"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:DropDownList data-mini="true" ID="dropDownListSubordinates"  runat="server">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a" id="divStudyArea" style="display:none;" runat="server">
    <div class="ui-block-a">
        <asp:Label ID="LblStudyArea" runat="server" Text="Study Area"  AssociatedControlID="dropDownListStudyArea"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:DropDownList data-mini="true" ID="dropDownListStudyArea"  runat="server">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>

    

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblIncome" runat="server" AssociatedControlID="dropDownListAnnualIncome"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:DropDownList data-mini="true" ID="dropDownListAnnualIncome" runat="server">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblEducation" runat="server" AssociatedControlID="dropDownListEducation"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:DropDownList data-mini="true" ID="dropDownListEducation" runat="server">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblCoffee" runat="server" AssociatedControlID="dropDownListCoffee"></asp:Label>
    </div>
    <div class="ui-block-b">
    <fieldset data-role="controlgroup" data-mini="true" >
        <asp:DropDownList data-mini="true" ID="dropDownListCoffee" runat="server">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>

    <hr style="width:80%;"/>

    <div class="ui-grid-a" style="text-align:center;">
    <asp:Label ID="lblTerms" runat="server"><a <%=policylink %>>Terms & Conditions of the Service<span class='req'>*</span></a></asp:Label>
    <asp:CheckBox ID="cbTerms" Text=" I accept" runat="server" data-mini="true"></asp:CheckBox >
    </div>

  
    
</div>
 <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health"rel="external">My Health</a></li>
                        <li><a href="News.aspx" data-icon="news">News</a></li>
                        <li><a href="More.aspx" data-icon="more">More</a></li>
                    </ul>
                </div>
            </div>
        </div>
     
    </form>
</body>
</html>
