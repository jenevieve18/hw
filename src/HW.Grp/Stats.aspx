<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="HW.Grp.Stats" %>

<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Core.Models" %>
<%@ Import Namespace="HW.Grp" %>
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
        <% if (reportParts != null && reportParts.Count > 0)
           { %>
        <% Q additionalQuery = GetGID(urlModels); %>
        <% bool forSingleSeries = (SelectedDepartments.Count <= 1 && (Grouping.SelectedValue == "1" || Grouping.SelectedValue == "2")) || Grouping.SelectedValue == "0"; %>
        <div class="report-parts">
            <% if (reportParts[0] is ReportPartLang)
               { %>
            <div class="action">
                <%--<div class="chart-descriptions" title="Chart Descriptions">--%>
                <div class="chart-descriptions" title="<%= R.Str(lid, "chart.description", "Chart Descriptions") %>">
                    <div>
                        <% foreach (var p in plotTypes)
                           { %>
                        <div>&nbsp;<br />
                        </div>
                        <div class="report-part">
                            <div class="report-part-subject">
                                <span><%= p.ShortName %> - <%= p.Name %></span>
                                <span class="toggle toggle-right toggle-active"></span>
                            </div>
                            <div class="report-part-header"><%= p.Description %></div>
                        </div>
                        <% } %>
                    </div>
                </div>
                <span class="small"><%= R.Str(lid, "graphs.change.all", "Change all graphs to:")%></span>
                <select class="plot-types small">
                    <% foreach (var p in plotTypes)
                       { %>
                    <% if (!p.SupportsMultipleSeries && !forSingleSeries) { } %>
                    <% else
                       { %><option value="<%= p.PlotType.Id %>" <%= p.PlotType.Id == sponsor.DefaultPlotType ? "selected" : "" %>><%= p.ShortName %></option>
                    <% } %>
                    <% } %>
                </select>
                <span class="chart-descriptions-info"></span>
                <span class="small"><%= R.Str(lid, "graphs.export.all", "Export all graphs to:")%></span>
                <span class="button white small export">
                    <% string exportAllDocXUrl = GetExportAllUrl("docx", additionalQuery); %>
                    <span class="hidden hidden-exportall-docx-url"><%= exportAllDocXUrl%></span>
                    <%= HtmlHelper.Anchor("docx", exportAllDocXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-docx-url' target='_blank'") %>
                </span>
                <span class="button white small export">
                    <% string exportAllPptxUrl = GetExportAllUrl("pptx", additionalQuery); %>
                    <span class="hidden hidden-exportall-pptx-url"><%= exportAllPptxUrl%></span>
                    <%= HtmlHelper.Anchor("pptx", exportAllPptxUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-pptx-url' target='_blank'") %>
                </span>
                <% string exportAllXlsUrl = GetExportAllUrl("xls", additionalQuery); %>
                <span class="button white small export">
                    <span class="hidden hidden-exportall-xls-url"><%= exportAllXlsUrl%></span>
                    <%= HtmlHelper.Anchor("xls", exportAllXlsUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-xls-url' target='_blank'")%>
                </span>
                <span class="button white small export">
                    <%= HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportAllXlsUrl + "&PLOT=" + PlotType.Verbose, "class='exportall-xls-verbose-url' target='_blank'")%>
                </span>
            </div>
            <% }
               else
               { %>
            <div class="action">
                <%--<div class="chart-descriptions" title="Chart Descriptions">--%>
                <div class="chart-descriptions" title="<%= R.Str(lid, "chart.description", "Chart Descriptions") %>">
                    <div>
                        <% foreach (var p in plotTypes)
                           { %>
                        <div>&nbsp;<br />
                        </div>
                        <div class="report-part">
                            <div class="report-part-subject">
                                <span><%= p.ShortName %> - <%= p.Name %></span>
                                <span class="toggle toggle-right toggle-active"></span>
                            </div>
                            <div class="report-part-header"><%= p.Description %></div>
                        </div>
                        <% } %>
                    </div>
                </div>
                <span class="small"><%= R.Str(lid, "graphs.change.all", "Change all graphs to:")%></span>
                <select class="plot-types small">
                    <% var xxx = lid == 1 ? new PlotTypeLanguage { PlotType = new PlotType { Id = 1 }, ShortName = "Linje", SupportsMultipleSeries = true } :
                                   new PlotTypeLanguage { PlotType = new PlotType { Id = 1 }, ShortName = "Line", SupportsMultipleSeries = true }; %>
                    <% foreach (var p in new PlotTypeLanguage[] { xxx })
                       { %>
                    <% if (!p.SupportsMultipleSeries && !forSingleSeries) { } %>
                    <% else
                       { %><option value="<%= p.PlotType.Id %>" <%= p.PlotType.Id == sponsor.DefaultPlotType ? "selected" : "" %>><%= p.ShortName %></option>
                    <% } %>
                    <% } %>
                </select>
                <span class="chart-descriptions-info"></span>
                <span class="small"><%= R.Str(lid, "graphs.export.all", "Export all graphs to:")%></span>
                <span class="button white small export">
                    <% string exportAllDocXUrl = GetExportAllUrl2("docx", additionalQuery); %>
                    <span class="hidden hidden-exportall-docx-url"><%= exportAllDocXUrl%></span>
                    <%= HtmlHelper.Anchor("docx", exportAllDocXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-docx-url' target='_blank'") %>
                </span>
                <span class="button white small export">
                    <% string exportAllPptxUrl = GetExportAllUrl2("pptx", additionalQuery); %>
                    <span class="hidden hidden-exportall-pptx-url"><%= exportAllPptxUrl%></span>
                    <%= HtmlHelper.Anchor("pptx", exportAllPptxUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-pptx-url' target='_blank'") %>
                </span>
                <% string exportAllXlsUrl = GetExportAllUrl2("xls", additionalQuery); %>
                <span class="button white small export">
                    <span class="hidden hidden-exportall-xls-url"><%= exportAllXlsUrl%></span>
                    <%= HtmlHelper.Anchor("xls", exportAllXlsUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-xls-url' target='_blank'")%>
                </span>
                <span class="button white small export">
                    <%= HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportAllXlsUrl + "&PLOT=" + PlotType.Verbose, "class='exportall-xls-verbose-url' target='_blank'")%>
                </span>
            </div>
            <% } %>
            <% foreach (var r in reportParts)
               { %>
            <div>&nbsp;<br />
            </div>
            <div class="report-part">
                <div class="hidden selected-plot-type"><%= PlotType.Line%></div>
                <div class="report-part-subject">
                    <span><%= r.Subject %></span>
                    <span class="toggle toggle-right toggle-active"></span>
                </div>
                <div class="report-part-header"><%= r.Header %></div>
                <div class="report-part-content">
                    <% if (r is ReportPartLang)
                       { %>
                    <% string imageUrl = GetReportImageUrlForReportPart(r.ReportPart.Id, r.Id, additionalQuery); %>
                    <span class="hidden hidden-image-url"><%= imageUrl %></span>
                    <img class="report-part-graph" src="<%= imageUrl %>&Plot=<%= GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)) %>" alt="" />
                    <div class="action">
                        <span class="small"><%= R.Str(lid, "graphs.change", "Change this graph to:")%></span>
                        <select class="plot-types small">
                            <% foreach (var p in plotTypes)
                               { %>
                            <% if (!p.SupportsMultipleSeries && !forSingleSeries) { } %>
                            <% else
                               { %><option value="<%= p.PlotType.Id %>" <%= p.PlotType.Id == sponsor.DefaultPlotType ? "selected" : "" %>><%= p.ShortName %></option>
                            <% } %>
                            <% } %>
                        </select>
                        <span class="small"><%= R.Str(lid, "graphs.export", "Export this graph to:")%></span>
                        <span class="button white small export">
                            <% string exportDocXUrl = GetExportUrl(r.ReportPart.Id, r.Id, "docx", additionalQuery); %>
                            <span class="hidden hidden-export-docx-url"><%= exportDocXUrl%></span>
                            <%= HtmlHelper.Anchor("docx", exportDocXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-docx-url' target='_blank'")%>
                        </span>
                        <span class="button white small export">
                            <% string exportPptXUrl = GetExportUrl(r.ReportPart.Id, r.Id, "pptx", additionalQuery); %>
                            <span class="hidden hidden-export-pptx-url"><%= exportPptXUrl%></span>
                            <%= HtmlHelper.Anchor("pptx", exportPptXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-pptx-url' target='_blank'")%>
                        </span>
                        <% string exportXlsUrl = GetExportUrl(r.ReportPart.Id, r.Id, "xls", additionalQuery); %>
                        <span class="button white small export">
                            <span class="hidden hidden-export-xls-url"><%= exportXlsUrl%></span>
                            <%= HtmlHelper.Anchor("xls", exportXlsUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-xls-url' target='_blank'")%>
                        </span>
                        <span class="button white small export">
                            <%= HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportXlsUrl + "&PLOT=" + PlotType.Verbose, "class='export-xls-verbose-url' target='_blank'")%>
                        </span>
                    </div>
                    <% }
                       else
                       { %>
                    <% string imageUrl = GetReportImageUrlForSponsorProject(r.Id, additionalQuery); %>
                    <span class="hidden hidden-image-url"><%= imageUrl %></span>
                    <img class="report-part-graph" src="<%= imageUrl %>&Plot=<%= GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)) %>" alt="" />
                    <div class="action">
                        <span class="small"><%= R.Str(lid, "graphs.change", "Change this graph to:")%></span>
                        <select class="plot-types small">
                            <% var xxx = lid == 1 ? new PlotTypeLanguage { PlotType = new PlotType { Id = 1 }, ShortName = "Linje", SupportsMultipleSeries = true } :
                                               new PlotTypeLanguage { PlotType = new PlotType { Id = 1 }, ShortName = "Line", SupportsMultipleSeries = true }; %>
                            <% foreach (var p in new PlotTypeLanguage[] { xxx })
                               { %>
                            <% if (!p.SupportsMultipleSeries && !forSingleSeries) { } %>
                            <% else
                               { %><option value="<%= p.PlotType.Id %>" <%= p.PlotType.Id == sponsor.DefaultPlotType ? "selected" : "" %>><%= p.ShortName %></option>
                            <% } %>
                            <% } %>
                        </select>
                        <span class="small"><%= R.Str(lid, "graphs.export", "Export this graph to:")%></span>
                        <span class="button white small export">
                            <% string exportAllDocXUrl = GetExportAllUrl2("docx", additionalQuery); %>
                            <span class="hidden hidden-export-docx-url"><%= exportAllDocXUrl%></span>
                            <%= HtmlHelper.Anchor("docx", exportAllDocXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-docx-url' target='_blank'") %>
                        </span>
                        <span class="button white small export">
                            <% string exportAllPptxUrl = GetExportAllUrl2("pptx", additionalQuery); %>
                            <span class="hidden hidden-export-pptx-url"><%= exportAllPptxUrl%></span>
                            <%= HtmlHelper.Anchor("pptx", exportAllPptxUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-pptx-url' target='_blank'") %>
                        </span>
                        <% string exportAllXlsUrl = GetExportAllUrl2("xls", additionalQuery); %>
                        <span class="button white small export">
                            <span class="hidden hidden-export-xls-url"><%= exportAllXlsUrl%></span>
                            <%= HtmlHelper.Anchor("xls", exportAllXlsUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-xls-url' target='_blank'")%>
                        </span>
                        <span class="button white small export">
                            <%= HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportAllXlsUrl + "&PLOT=" + PlotType.Verbose, "class='exportall-xls-verbose-url' target='_blank'")%>
                        </span>
                    </div>
                    <% } %>
                </div>
                <div class="report-part-bottom">&nbsp;</div>
            </div>
            <% } %>
        </div>
        <% } %>
    </div>

    <script type="text/javascript">
        $('.input-daterange').datepicker({
            format: "yyyy M",
            minViewMode: 1,
            language: "<%= GetLang(lid) %>",
            autoclose: true
        });
    </script>

</asp:Content>
