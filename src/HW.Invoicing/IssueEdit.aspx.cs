﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class IssueEdit : System.Web.UI.Page
    {
        SqlIssueRepository r = new SqlIssueRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                dropDownListStatus.Items.Clear();
                foreach (var s in Issue.GetStatuses())
                {
                    dropDownListStatus.Items.Add(new ListItem(s.Name, s.Id.ToString()));
                }
                var i = r.Read(id);
                if (i != null)
                {
                    textBoxTitle.Text = i.Title;
                    textBoxDescription.Text = i.Description;
//                    checkBoxReactivate.Checked = !i.Inactive;
//                    placeHolderReactivate.Visible = i.Inactive;
                    dropDownListStatus.SelectedValue = i.Status.ToString();
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                var i = new Issue
                {
                    Title = textBoxTitle.Text,
                    Description = textBoxDescription.Text,
                    Status = ConvertHelper.ToInt32(dropDownListStatus.SelectedValue),
//                    Inactive = !checkBoxReactivate.Checked
                };
                r.Update(i, ConvertHelper.ToInt32(Request.QueryString["Id"]));
                Response.Redirect("issues.aspx");
            }
        }
    }
}