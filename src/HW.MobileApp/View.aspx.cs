using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class View : System.Web.UI.Page
    {
        protected string timeframe;
        protected string compare;
        protected int language;

        protected void Page_Load(object sender, EventArgs e)
        {
            language = int.Parse(Session["languageId"].ToString());
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["tf"] != null)
                    ddlTimeframe.SelectedValue = Request.QueryString["tf"];


                if (Request.QueryString["comp"] != null)
                    ddlCompare.SelectedValue = Request.QueryString["comp"];
            }
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ddlTimeframe.Items.Add(new ListItem(R.Str(language, "options.timeframe.latest")));
            ddlTimeframe.Items.Add(new ListItem(R.Str(language, "options.timeframe.pastWeek")));
            ddlTimeframe.Items.Add(new ListItem(R.Str(language, "options.timeframe.pastMonth")));
            ddlTimeframe.Items.Add(new ListItem(R.Str(language, "options.timeframe.pastYear")));
            ddlTimeframe.Items.Add(new ListItem(R.Str(language, "options.timeframe.sinceFirstMeasure")));

            ddlCompare.Items.Add(new ListItem(R.Str(language, "options.compare.none")));
            ddlCompare.Items.Add(new ListItem(R.Str(language, "options.compare.database")));
        }

        protected void doneBtn_Click(object sender, EventArgs e){

            if (ddlTimeframe.SelectedValue != "Latest")
                Response.Redirect("Statistics.aspx?tf=" + ddlTimeframe.SelectedValue + "&comp=" + ddlCompare.SelectedValue);
            else
                Response.Redirect("Statistics.aspx");
           

        }
        
    }

}