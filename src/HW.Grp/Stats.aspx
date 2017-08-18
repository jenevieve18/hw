<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="HW.Grp.Stats" %>

<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Core.Models" %>
<%@ Import Namespace="HW.Grp" %>
<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="assets/smoothness/jquery-ui-1.9.2.custom.min.css" />
    <link rel="stylesheet" href="assets/theme1/css/stats.css" />

    <script type="text/javascript" src="assets/ui/jquery-ui-1.9.2.custom.min.js"></script>

    <script type="text/javascript" src="assets/js/stats.js"></script>

    <link rel="stylesheet" href="assets/bootstrap-datepicker/css/bootstrap-combined.min.css" />
    <link rel="stylesheet" href="assets/bootstrap-datepicker/css/bootstrap-datepicker.css" />

    <script type="text/javascript" src="assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="assets/bootstrap-datepicker/locales/bootstrap-datepicker.sv.min.js"></script>
    <script type="text/javascript" src="assets/bootstrap-datepicker/locales/bootstrap-datepicker.de.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="settingsPane">
                <span class="desc"><%= R.Str(lid, "timeframe", "Timeframe")%></span>

                <span class="input-daterange">
                    <input readonly style="cursor: pointer" type="text" class="input-small date" name="startDate" value="<%= startDate.ToString("yyyy MMM", GetCultureInfo(lid)) %>" />
                    <span>--</span>
                    <input readonly style="cursor: pointer" type="text" class="input-small date" name="endDate" value="<%= endDate.ToString("yyyy MMM", GetCultureInfo(lid)) %>" />
                </span>

                <%= R.Str(lid, "survey", "Survey")%>
                <asp:DropDownList AutoPostBack="true" ID="ProjectRoundUnitID" runat="server" />
                <%= R.Str(lid, "aggregation", "Aggregation")%>
                <asp:DropDownList AutoPostBack="true" ID="GroupBy" runat="server">
                </asp:DropDownList><br />
                <span class="desc"><%= R.Str(lid, "grouping", "Grouping")%></span>
                <asp:DropDownList AutoPostBack="true" ID="Grouping" runat="server">
                </asp:DropDownList>
                <br />
                <asp:PlaceHolder ID="Org" runat="server" Visible="false" />
                <asp:CheckBoxList RepeatDirection="Vertical" RepeatLayout="table" CellPadding="0" CellSpacing="0" ID="BQ" runat="server" Visible="false" />
                <asp:Button ID="Execute" CssClass="btn" runat="server" Text="Execute" />
            </div>
        </div>

        <!-- For Statistic Images -->
        <asp:Label ID="StatisticImage" runat="server" />
        <!-- End for Statistic Images -->



    <script type="text/javascript">
        $('.input-daterange').datepicker({
            format: "yyyy M",
            minViewMode: 1,
            language: "<%= GetLang(lid) %>",
            autoclose: true
        });

        var arraySelectIDs = [ <%= ReportPartID %>];


        function onChanged(id, sponsorID) {
            if (id != 0) {

                var e = document.getElementById("selectID" + id);
                var plotType = e.options[e.selectedIndex].value;

                var url = document.getElementById("HiddenID" + id).innerText;


                $.ajax({
                    url: "Stats.aspx/GetImage",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ "id": id, "value": plotType, "url": url, "sponsorID": sponsorID }),
                    success: function (data) {


                        var img = document.getElementById("ImageID" + id);

                        console.log(img.src);
                        img.src = data.d + "?" + new Date().getTime();
                        //img.attr("src", data.d+"?" + new Date().getTime());
                        console.log(img.src);

                    }

                });

            } else { // if zero
                var parent = document.getElementById("selectID" + id);
                

                console.log(arraySelectIDs);
                for (i = 0; i < arraySelectIDs.length; i++){
                    var selectedID = arraySelectIDs[i];
                    console.log(selectedID);
                    var e = document.getElementById("selectID" + selectedID);
                    console.log(plotType);
                    e.selectedIndex = parent.selectedIndex;
                    onChanged(selectedID, sponsorID);
                };
            }
            
        }
    </script>
</asp:Content>
