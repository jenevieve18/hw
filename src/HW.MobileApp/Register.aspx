<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HW.MobileApp.Register" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />

	<link rel="stylesheet" href="/custom.css" />

    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>




</head>
<body>
    <form id="form1" runat="server">
        
        <div data-role="page" id="register">


<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Login.aspx" data-icon="arrow-l" rel="external">Cancel</a>
    <h1>Register</h1>
    <a id="createBtn" onserverclick="createBtn_Click" runat="server" data-icon="check">Create</a>

</div>
<div data-role="content">
    <div class="header">
        <h4><asp:Label id="lblHeader" runat="server" text="Account Details"></asp:Label></h4>
        <h4 style="color:Red"><asp:Label ID="labelMessage" runat="server"></asp:Label></h4>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="lblLanguage" runat="server" Text="Language" AssociatedControlID="dropDownListLanguage"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListLanguage"
            runat="server" AutoPostBack="true" ViewStateMode="Enabled" EnableViewState="true"
            onselectedindexchanged="dropDownListLanguage_SelectedIndexChanged">
            <asp:ListItem value="2">English</asp:ListItem>
            <asp:ListItem value="1">Swedish</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="lblUsername" runat="server" AssociatedControlID="textBoxUsername">Username<span class="req">*</span></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxUsername" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="lblPassword" runat="server" Text="Password<span class='req'>*</span>" AssociatedControlID="textBoxPassword"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxPassword" TextMode="Password" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password<span class='req'>*</span>" AssociatedControlID="textBoxConfirmPassword"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="textBoxEmail">Email<span class="req">*</span></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxEmail" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="lblAltEmail" runat="server" Text="Alternate Email" AssociatedControlID="textBoxAlternateEmail"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxAlternateEmail" runat="server"></asp:TextBox>
    </div>


     <div class="header">
        <h4>Personal Information</h4>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
    </div>
    <div data-role="fieldcontain">
    
    <fieldset data-role="controlgroup" data-mini="true" data-type="horizontal" >
    <legend><asp:Label ID="Label11" runat="server" AssociatedControlID="birthYear">Birthdate<span class="req">*</span></asp:Label></legend>
        <asp:DropDownList ID="birthYear" runat="server" >
        </asp:DropDownList>
        <asp:DropDownList ID="birthMonth" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="birthDay" runat="server"  >
        </asp:DropDownList>
    </fieldset>
    </div>   

    <div data-role="fieldcontain">
    <fieldset data-role="controlgroup" data-mini="true" >
    <legend><asp:Label ID="Label12" runat="server" AssociatedControlID="rdbGender">Gender<span class="req">*</span></asp:Label></legend>
        <asp:RadioButtonList ID="rdbGender" runat="server" >
            <asp:ListItem Text="Male" Value="1" ></asp:ListItem>
            <asp:ListItem Text="Female" Value="2" ></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
    </div>
    <div data-role="fieldcontain">
    <fieldset data-role="controlgroup" data-mini="true" >
    <legend><asp:Label ID="Label13" runat="server" AssociatedControlID="rdbStatus">Status<span class="req">*</span></asp:Label></legend>
        <asp:RadioButtonList ID="rdbStatus" runat="server">
            <asp:ListItem Text="Married" Value="369" ></asp:ListItem>
            <asp:ListItem Text="Single" Value="370" ></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
    </div>

    <div data-role="fieldcontain">
        <asp:Label ID="Label6" runat="server" AssociatedControlID="dropDownListOccupation">Occupation<span class="req">*</span></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListOccupation" runat="server" 
            onselectedindexchanged="dropDownListOccupation_SelectedIndexChanged"
            AutoPostBack="true" ViewStateMode="Enabled" EnableViewState="true" >
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
    <fieldset data-role="controlgroup" data-mini="true" >
    <legend><asp:Label ID="Label14" runat="server" Text="Occupation Type" AssociatedControlID="rdbOccupationType"></asp:Label></legend>
        <asp:RadioButtonList ID="rdbOccupationType" runat="server" >
            <asp:ListItem Text="Full time" Value="405"  ></asp:ListItem>
            <asp:ListItem Text="Part time" Value="406" ></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
    </div>

    
       
    <div data-role="fieldcontain" id="divIndustry" style="display:none;" runat="server">
        <asp:Label ID="LblIndustry" runat="server" Text="Industry" AssociatedControlID="dropDownListIndustry"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListIndustry" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain" id="divJob" style="display:none;" runat="server">
        <asp:Label ID="LblJob" runat="server" Text="Job"  AssociatedControlID="dropDownListJob"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListJob" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain" id="divManagerial" style="display:none;" runat="server">
    <fieldset data-role="controlgroup" data-mini="true" >
    <legend><asp:Label ID="LblManage" runat="server" Text="Managerial post with subordinate staff?" AssociatedControlID="rdbManagerial"></asp:Label></legend>
        <asp:RadioButtonList ID="rdbManagerial" runat="server"  
            ViewStateMode="Enabled" >
            <asp:ListItem Text="Yes" Value="413" ></asp:ListItem>
            <asp:ListItem Text="No" Value="412" ></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
    </div>
    <div data-role="fieldcontain" id="divStudyArea" style="display:none;" runat="server">
        <asp:Label ID="LblStudyArea" runat="server" Text="Study Area"  AssociatedControlID="dropDownListStudyArea"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListStudyArea"  runat="server">
        </asp:DropDownList>
    </div>
    
    

    <div data-role="fieldcontain">
        <asp:Label ID="Label9" runat="server" AssociatedControlID="dropDownListAnnualIncome">Annual Income<span class="req">*</span></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListAnnualIncome" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label10" runat="server" AssociatedControlID="dropDownListEducation">Education<span class="req">*</span></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListEducation" runat="server">
        </asp:DropDownList>

    <div data-role="fieldcontain">
    <fieldset data-role="controlgroup">
        <legend><asp:Label ID="Label7" runat="server"><a target="_blank" href="https://healthwatch.se/policy.aspx?Rnd=876338515">Terms & Conditions of the Service</a>  <span class='req'>*</span></legend></asp:Label>
        <asp:CheckBox data-mini="true" ID="cbTerms" Text=" I accept" runat="server" ></asp:CheckBox >
    </fieldset>
    </div>
    <style>
    .header { text-align:center; }
    .header h4 { margin-bottom:0 }
    .header img { width:235px }
    .front_header {
text-align: center;
margin: 15px 0px 25px 0px;
font-size: 16px;
}
        .front_note {
max-width: 560px;
margin-left: auto;
margin-right: auto;
min-width: 268px;
padding: 15px 15px 70px 15px;
}
.front_logo {
width: 180px;
height: 126px;
margin-bottom: 30px;
display: inline-block;
vertical-align: bottom;
margin-right: 20px;
}
.front_controls {
max-width: 350px;
margin: 0 auto;
min-height: 250px;
display: inline-block;
min-width: 320px;
}
.center {
    text-align:center;
}
.front_header_img {
width: 235px;
}
</style>
    </div>
 <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="home">My Health</a></li>
                        <li><a href="News.aspx" data-icon="grid">News</a></li>
                        <li><a href="More.aspx" data-icon="info">More</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div data-role="page" id="welcome">

            <div data-role="header" data-theme="b" data-position="fixed">
            <h1>Register</h1>
            <a id="A1" onserverclick="loginBtn_Click" runat="server" class="ui-btn-right" data-icon="check">Login</a>
            </div>
            <div data-role="content">
                <div class="front_note center">
                    <img class="front_logo" src="http://clients.easyapp.se/healthwatch/images/hw_logo@2x.png" />
                    <div class="front_controls">
                        <div class="front_header">
                            <h4 class="text">Welcome</h4>
                            
                            <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
                        </div>
                        <p>You are now registered. Please login and edit profile.</p>
                    </div>
                </div>
                
            </div>


        </div>
    </form>
</body>
</html>
