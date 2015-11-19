using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using System.Web.Services;

namespace HW.Invoicing
{
    public partial class CustomerTimebooks : System.Web.UI.Page
    {
    	SqlCustomerRepository cr = new SqlCustomerRepository();
    	protected IList<Customer> customers;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

        	int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
        	
        	customers = cr.FindSubscribersByCompany(companyId);
        	foreach (var c in customers) {
        		c.Timebooks = cr.FindTimebooks(c.Id);
        	}
        }

        [WebMethod]
        public static List<object> FindSubscribersByCompany(int companyId, int customerId, int timebookId)
        {
            var r = new SqlCustomerRepository();

            r.MoveTimebook(customerId, timebookId);

            var customers = r.FindSubscribersByCompany(companyId);
            var data = new List<object>();
            foreach (var c in customers)
            {
                var d = new
                {
                    id = c.Id,
                    name = c.Name,
                    timebooks = new List<object>()
                };
                foreach (var t in r.FindTimebooks(c.Id))
                {
                    d.timebooks.Add(new { id = t.Id, timebook = t.ToString() });
                }
                data.Add(d);
            }
            return data;
        }
    }
}