<%@ Import Namespace="HW.Grp" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Org.aspx.cs" Inherits="HW.Grp.Org" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="top">
                <div class="search">
                    <%= R.Str(lid, "search.email", "Search user by email") %>
                    <asp:TextBox ID="SearchEmail" runat="server" />
                    <asp:Button ID="Search" Text="Search" runat="server" />
                </div>
            </div>
            <div class="bottom" id="ActionNav" runat="server">
                <a class="add-unit" href="org.aspx?Action=AddUnit"><%= R.Str(lid, "unit.add", "Add unit")%></a>
                <a class="import-unit" href="org.aspx?Action=ImportUnit"><%= R.Str(lid, "unit.import", "Import units")%></a>
                <a class="add-user" href="org.aspx?Action=AddUser"><%= R.Str(lid, "user.add", "Add user")%></a>
                <a class="import-users" href="org.aspx?Action=ImportUser"><%= R.Str(lid, "user.import", "Import users")%></a>
            </div>
            <div class="actionPane">
                <asp:PlaceHolder ID="AddUnit" runat="server" Visible="false">
                    <div class="actionBlock">
                        <span class="desc"><%= R.Str(lid, "unit.name", "Unit name")%></span>
                        <asp:TextBox CssClass="inpt" ID="Department" runat="server" /><%= R.Str(lid, "display.hierarchy", "(for display in hierarchy)") %><br />
                        <span class="desc"><%= R.Str(lid, "unit.id", "Unit ID")%></span>
                        <asp:TextBox CssClass="inpt" ID="DepartmentShort" runat="server" /><%= R.Str(lid, "user.import.export", "(for user import/export, short name, no spaces)") %><br />
                        <span class="desc"><%= R.Str(lid, "unit.parent", "Parent unit")%></span>
                        <asp:DropDownList CssClass="maxInpt" ID="ParentDepartmentID" runat="server" /><br />
                        <asp:Button ID="CancelUnit" CssClass="btn" Text="Cancel" runat="server" />
                        <asp:Button CssClass="btn" ID="SaveUnit" Text="Save" runat="server" />
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="ImportUnits" runat="server" Visible="false">
                    <div class="actionBlock">
                        <span class="descLong"><%= R.Str(lid, "filename", "Filename")%></span><input type="file" class="inpt" id="UnitsFilename" runat="server" /><br />
                        <span class="descLong"><%= R.Str(lid, "unit.parent.default", "Default parent unit")%></span><asp:DropDownList CssClass="maxInpt" ID="ImportUnitsParentDepartmentID" runat="server" /><br />
                        <span style="color: #CC0000;"><asp:Label ID="ImportUnitsError" runat="server" /></span>
                        <asp:Button ID="CancelImportUnit" Text="Cancel" CssClass="btn" runat="server" />
                        <asp:Button ID="SaveImportUnit" Text="Save" CssClass="btn" runat="server" />
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="AddUser" runat="server" Visible="false">
                    <div class="actionBlock">
                        <span class="desc"><%= R.Str(lid, "email", "Email")%></span><asp:TextBox CssClass="inpt" ID="Email" runat="server" /><br />
                        <span class="desc"><%= R.Str(lid, "unit", "Unit")%></span><asp:DropDownList CssClass="maxInpt" ID="DepartmentID" runat="server" /><br />
                        <asp:PlaceHolder ID="Hidden" runat="server" />
                        <asp:PlaceHolder ID="UserUpdate" runat="server">
                            <span class="desc"><%= R.Str(lid, "status", "Status")%></span>
                            <asp:DropDownList ID="StoppedReason" runat="server">
                                <%-- <asp:ListItem Value="0">Active</asp:ListItem>
                                <asp:ListItem Value="1">Stopped, work related</asp:ListItem>
                                <asp:ListItem Value="2">Stopped, education leave</asp:ListItem>
                                <asp:ListItem Value="14">Stopped, parental leave</asp:ListItem>
                                <asp:ListItem Value="24">Stopped, sick leave</asp:ListItem>
                                <asp:ListItem Value="34">Stopped, do not want to participate</asp:ListItem>
                                <asp:ListItem Value="44">Stopped, no longer associated</asp:ListItem>
                                <asp:ListItem Value="4">Stopped, other reason</asp:ListItem>
                                <asp:ListItem Value="5">Stopped, unknown reason</asp:ListItem>
                                <asp:ListItem Value="6">Stopped, project completed</asp:ListItem>--%>
                            </asp:DropDownList>
                            <%= R.Str(lid, "updated.on", "updated on") %>
                            <asp:TextBox Width="80" ID="Stopped" runat="server" />
                            <%= R.Str(lid, "date.future.no", "(future date not allowed)") %>
                            <br />
                            <%= R.Str(lid, "user.active.perform", "If this user has activated the account, perform the following") %><br />
                            <asp:RadioButtonList ID="UserUpdateFrom" runat="server" RepeatDirection="Vertical"
                                RepeatLayout="table">
                                <%--<asp:ListItem Value="1" Selected>Update the user profile with these settings from today and onwards.</asp:ListItem>
                                <asp:ListItem Value="0">Update the user profile as if these settings were set from start.</asp:ListItem>
                                <asp:ListItem Value="2">The previously registered email address has never been correct and the created account should be detached from organization.</asp:ListItem>--%>
                            </asp:RadioButtonList>
                            <br />
                        </asp:PlaceHolder>
                        <span style="color: #CC0000;">
                            <asp:Label ID="AddUserError" runat="server" />
                        </span>
                        <asp:Button ID="CancelUser" Text="Cancel" CssClass="btn" runat="server" />
                        <asp:Button ID="SaveUser" CssClass="btn" Text="Save" runat="server" />
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="ImportUsers" runat="server" Visible="false">
                    <div class="actionBlock">
                        <span class="descLong"><%= R.Str(lid, "filename", "Filename")%></span><input type="file" class="inpt" id="UsersFilename" runat="server" /><br />
                        <span class="descLong"><%= R.Str(lid, "unit.parent.default", "Default parent unit")%></span><asp:DropDownList CssClass="maxInpt" ID="ImportUsersParentDepartmentID" runat="server" /><br />
                        <span style="color: #CC0000;">
                            <asp:Label ID="ImportUsersError" runat="server" />
                        </span>
                        <asp:Button CssClass="btn" ID="CancelImportUser" Text="Cancel" runat="server" />
                        <asp:Button CssClass="btn" ID="SaveImportUser" Text="Save" runat="server" />
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="DeleteUser" runat="server" Visible="false">
                    <div class="actionBlock">
                        <span class="desc"><%= R.Str(lid, "email", "Email")%></span>
                        <asp:TextBox CssClass="inpt" ReadOnly="true" ID="DeleteUserEmail" runat="server" /><br />
                        <%= R.Str(lid, "user.perform.activated", "If this user has activated the account, perform the following")%><br />
                        <asp:RadioButtonList ID="DeleteUserFrom" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                            <%--<asp:ListItem Value="1" Selected>From today and onwards, disassociate this user with the organization.</asp:ListItem>
                            <asp:ListItem Value="0">Disassociate this user with the organization from start.</asp:ListItem>--%>
                        </asp:RadioButtonList>
                        <br />
                        <asp:Button CssClass="btn" ID="CancelDeleteUser" Text="Cancel" runat="server" />
                        <asp:Button CssClass="btn" ID="SaveDeleteUser" Text="Execute" runat="server" />
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="DeleteDepartment" runat="server" Visible="false">
                    <div class="actionBlock">
                        <span class="desc"><%= R.Str(lid, "unit", "Unit") %></span>
                        <asp:TextBox ReadOnly="true" ID="DeleteDepartmentName" runat="server" /><br />
                        <asp:Button CssClass="btn" ID="CancelDeleteDepartment" Text="Cancel" runat="server" />
                        <asp:Button CssClass="btn" ID="SaveDeleteDepartment" Text="Execute" runat="server" />
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="SearchResults" runat="server" Visible="false">
                    <div class="actionBlock">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <asp:Label ID="SearchResultList" runat="server" />
                        </table>
                    </div>
                </asp:PlaceHolder>
            </div>
        </div>
        <div style="clear: both">
            <asp:Label ID="Actions" runat="server" Visible="false">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                    </tr>
                    <tr>
                        <td valign="top" id="act1" runat="server">
                            <b><%= R.Str(lid, "action", "Action")%></b><br />
                            <a href=""><img src="img/unt_add.gif" border="0" />&nbsp;Add&nbsp;unit</a><br />
                            <a href=""><img src="img/usr_add.gif" border="0" />&nbsp;Add&nbsp;user</a>
                        </td>
                        <td width="5%" id="act2" runat="server">
                            &nbsp;
                        </td>
                        <td valign="top" id="act3" runat="server">
                            &nbsp;<br />
                            <a href=""><img src="img/unt_imp.gif" border="0" />&nbsp;Import&nbsp;units</a><br />
                            <a href=""><img src="img/usr_imp.gif" border="0" />&nbsp;Import&nbsp;users</a>
                        </td>
                        <td width="5%" id="act4" runat="server">
                            &nbsp;
                        </td>
                        <td valign="top">
                            <b>Search&nbsp;user</b><br />
                            Email:&nbsp;&nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td valign="top">
                            <b>Legend</b><br />
                            <img src="img/usr_on.gif" />&nbsp;Show&nbsp;members<br />
                            <img src="img/key.gif" />&nbsp;Display&nbsp;restricted
                        </td>
                        <td width="5%" id="act5" runat="server">
                            &nbsp;
                        </td>
                        <td valign="top" id="act6" runat="server">
                            &nbsp;<br />
                            <img src="img/unt_edt.gif" />&nbsp;Edit&nbsp;unit<br />
                            <img src="img/usr_edt.gif" />&nbsp;Edit&nbsp;user
                        </td>
                        <td width="5%" id="act7" runat="server">
                            &nbsp;
                        </td>
                        <td valign="top" id="act8" runat="server">
                            &nbsp;<br />
                            <img src="img/unt_del.gif" />&nbsp;Delete&nbsp;unit<br />
                            <img src="img/usr_del.gif" />&nbsp;Delete&nbsp;user
                        </td>
                        <td width="20%" id="act9" runat="server">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Label>
            <asp:Label ID="OrgTree" runat="server" />
        </div>
    </div>
</asp:Content>
