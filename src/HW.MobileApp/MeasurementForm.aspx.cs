using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class MeasurementForm : System.Web.UI.Page
    {

        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.Measure[] measure;
        protected DateTime date;
        protected int measureNo;
        string token;
        int year ;
        int month;
        int day  ;
        int hrs  ;
        int min  ;
        int sec;
        protected void Page_Load(object sender, EventArgs e)
        {
            token = "";
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");
            token = Session["token"].ToString();

            int lang = int.Parse(Session["languageId"].ToString());

            if (Request.QueryString["datetime"] != null)
            {
                var temp = Request.QueryString["datetime"];
                 year = int.Parse(temp.Substring(0, 4));
                 month = int.Parse(temp.Substring(5, 2));
                 day = int.Parse(temp.Substring(8, 2));
                 hrs = int.Parse(temp.Substring(11, 2));
                 min = int.Parse(temp.Substring(14, 2));
                 sec = int.Parse(temp.Substring(17, 2));

                date = new DateTime(year, month, day, hrs, min, sec);

            }
            
            int id = int.Parse(Request.QueryString["mcid"]);

            measure = service.MeasureEnum(new HWService.MeasureEnumRequest(token, id, lang, 10)).MeasureEnumResult;
            measureNo = measure.Count();
        }
        
        protected void saveBtnClick(object sender, EventArgs e){
            
            string tempids = mcids.Value;
            string tempidvalues = mcidvals.Value;
            string[] componentIds = tempids.Split('x');
            string[] componentIdValues = tempidvalues.Split('x');
            HWService.UserMeasureComponent[] mc = new HWService.UserMeasureComponent[measureNo];

            DateTime newDate = new DateTime(year,month,day,int.Parse(inhours.Value.ToString()),int.Parse(inmins.Value.ToString()),sec);

            for (int i = 0; i < measureNo; i++)
            {
                if (componentIdValues[i] != "")
                {
                    service.UserSetMeasureParameterized(token, newDate, measure[i].measureID, int.Parse(componentIds[i]), componentIdValues[i], 0, "", 10);
                }
            }
            Response.Redirect("ActivityMeasurement.aspx?datetime=" + newDate.ToString("yyyy-MM-ddTHH:mm:ss")); 
        }
    }

          
}