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
        

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["tf"] != null)
                    ddlTimeframe.SelectedValue = Request.QueryString["tf"];


                if (Request.QueryString["comp"] != null)
                    ddlCompare.SelectedValue = Request.QueryString["comp"];
            }
            
        }

        protected void doneBtn_Click(object sender, EventArgs e){

            if (ddlTimeframe.SelectedValue != "Latest")
                Response.Redirect("Statistics.aspx?tf=" + ddlTimeframe.SelectedValue + "&comp=" + ddlCompare.SelectedValue);
            else
                Response.Redirect("Statistics.aspx");
           

        }
        
    }

}