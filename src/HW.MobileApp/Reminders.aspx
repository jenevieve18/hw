<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="HW.MobileApp.Reminders" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Settings.aspx"><%= R.Str(language,"button.cancel") %></a>
    <h1><%= R.Str(language, "reminder.title")%></h1>
    <a id="saveBtn" onserverclick="saveChangesBtn_Click" runat="server"><%= R.Str(language, "button.save")%></a>
</div>
<div data-role="content">

    <div data-role="fieldcontain">
        <asp:Label ID="Label1" runat="server" Text="&nbsp;" AssociatedControlID="dropDownListReminder"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListReminder" runat="server" 
            AutoPostBack="true" ViewStateMode="Enabled" EnableViewState="true" 
            onselectedindexchanged="dropDownListReminder_SelectedIndexChanged" >
            <%-- <asp:ListItem Text="Never" Value="0"></asp:ListItem>
            <asp:ListItem Text="Regularly" Value="1"></asp:ListItem>
            <asp:ListItem Text="Inactivity" Value="2"></asp:ListItem>--%>
        </asp:DropDownList>
    </div>

    <div id="divReminderInactivity" runat="server" style="display:none;">
        <div data-role="fieldcontain">
                <fieldset data-role="controlgroup" data-mini="true" data-type="horizontal" >
                <legend><asp:Label ID="Label6" runat="server" Text="At" ></asp:Label></legend>
                <asp:DropDownList data-mini="true" ID="dropDownListInactivityTime" runat="server"></asp:DropDownList>
                <asp:DropDownList data-mini="true" ID="dropDownListInactivityCount" runat="server">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList data-mini="true" ID="dropDownListInactivityPeriod" runat="server">
                    <%-- <asp:ListItem Text="Days" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Weeks" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Months" Value="30"></asp:ListItem>--%>
                </asp:DropDownList>
                </fieldset>
            </div>
    </div>

    <div id="divReminderRegularly" runat="server" style="display:none;"> 
        <div data-role="fieldcontain">
            <asp:Label ID="LblSchedule" runat="server" Text="Schedule" AssociatedControlID="dropDownListRegularSchedule"></asp:Label>
            <asp:DropDownList data-mini="true" ID="dropDownListRegularSchedule" runat="server" 
                AutoPostBack="true" ViewStateMode="Enabled" EnableViewState="true" 
                onselectedindexchanged="dropDownListSchedule_SelectedIndexChanged">
                <%-- <asp:ListItem Text="Daily" Value="1"></asp:ListItem>
                <asp:ListItem Text="Weekly" Value="2"></asp:ListItem>
                <asp:ListItem Text="Monthly" Value="3"></asp:ListItem>--%>
            </asp:DropDownList>
        </div>

        <div data-role="fieldcontain">
            <asp:Label ID="LblTime" runat="server" Text="At"  AssociatedControlID="dropDownListRegularTime"></asp:Label>
            <asp:DropDownList data-mini="true" ID="dropDownListRegularTime" runat="server">
            </asp:DropDownList>
        </div>

        <div id="divScheduleDaily" runat="server" style="display:none;">
            <div data-role="fieldcontain">
 	            <fieldset data-role="controlgroup">
		           <legend>Every</legend>
		            <asp:CheckBox data-mini="true" id="cbMonday" text="Monday" runat="server" />
                    <asp:CheckBox ID="cbTuesday" data-mini="true" text="Tuesday" runat="server" />
                    <asp:CheckBox ID="cbWednesday" data-mini="true"  text="Wednesday" runat="server" />
                    <asp:CheckBox ID="cbThursday" data-mini="true"  text="Thursday" runat="server" />
                    <asp:CheckBox ID="cbFriday" data-mini="true"  text="Friday" runat="server" />
                    <asp:CheckBox ID="cbSaturday" data-mini="true"  text="Saturday" runat="server" />
                    <asp:CheckBox ID="cbSunday" data-mini="true"  text="Sunday" runat="server" />
                </fieldset>
            </div>
        </div>

        <div id="divScheduleWeekly" runat="server" style="display:none;">
            <div data-role="fieldcontain">
                <asp:Label ID="Label2" runat="server" Text="Every"  AssociatedControlID="dropDownListWeeklyDay"></asp:Label>
                <asp:DropDownList data-mini="true" ID="dropDownListWeeklyDay" runat="server">
                    <%-- <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Sunday" Value="7"></asp:ListItem>--%>

                </asp:DropDownList>
            </div>

            <div data-role="fieldcontain">
                <asp:Label ID="Label3" runat="server" Text="For Every"  AssociatedControlID="dropDownListWeeklyForEvery"></asp:Label>
                <asp:DropDownList data-mini="true" ID="dropDownListWeeklyForEvery" runat="server">
                <%-- <asp:ListItem Text="Week" Value="1"></asp:ListItem>
                <asp:ListItem Text="Other Week" Value="2"></asp:ListItem>
                <asp:ListItem Text="Third Week" Value="3"></asp:ListItem>--%>
                </asp:DropDownList>
            </div>
        </div>

        <div id="divScheduleMonthly" runat="server" style="display:none;">
            <div data-role="fieldcontain">
                <fieldset data-role="controlgroup" data-mini="true" data-type="horizontal" >
                <legend><asp:Label ID="Label4" runat="server" Text="Every"  AssociatedControlID="dropDownListMonthlyDayNo"></asp:Label></legend>
                <asp:DropDownList data-mini="true" ID="dropDownListMonthlyDayNo" runat="server">
                    <%-- <asp:ListItem Text="1st" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2nd" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3rd" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4th" Value="4"></asp:ListItem>--%>
                </asp:DropDownList>
                <asp:DropDownList data-mini="true" ID="dropDownListMonthlyDay" runat="server">
                    <%-- <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Sunday" Value="7"></asp:ListItem>--%>
                </asp:DropDownList>
                </fieldset>
            </div>

            <div data-role="fieldcontain">
                <asp:Label ID="Label5" runat="server" Text="Of Every"  AssociatedControlID="dropDownListMonthlyOfEvery"></asp:Label>
                <asp:DropDownList data-mini="true" ID="dropDownListMonthlyOfEvery" runat="server">
                    <%-- <asp:ListItem Text="Week" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Other Week" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Third Week" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Sixth Week" Value="6"></asp:ListItem>--%>
                </asp:DropDownList>
            </div>
        </div>

    </div>
    
</div>

</asp:Content>
