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

        protected TextBox[] measureTextBox;
        protected Label[] measureLabel;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
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


            timeHour.DataSource = Enumerable.Range(00, 24).Select(x => x.ToString("D2"));
            timeMin.DataSource = Enumerable.Range(00, 60).Select(x => x.ToString("D2"));
            timeHour.DataBind();
            timeMin.DataBind();


            foreach (var m in measure)
            {
                measureTextBox = new TextBox[m.componentCount];
                measureLabel = new Label[m.componentCount];

                for (var mc = 0; mc < m.componentCount; mc++)
                {
                    placeHolderList.Controls.Add(new Literal() { Text = "<div data-role='fieldcontain'>" });

                    measureTextBox[mc] = new TextBox() { ID = m.measureComponents[mc].measureComponentID.ToString(),EnableViewState = true };
                    if (measureTextBox[mc].ID == "8")
                    {
                        if (Session["tb8"] != null)
                        {
                            measureTextBox[mc].Text = Session["tb8"].ToString();
                            timeHour.SelectedValue = "02";
                        }

                    }
                    if (measureTextBox[mc].ID == "7")
                    {
                        if (Session["tb7"] != null)
                        {
                            measureTextBox[mc].Text = Session["tb7"].ToString();
                            timeMin.SelectedValue = "02";
                        }

                    }

                    measureLabel[mc] = new Label() { AssociatedControlID = measureTextBox[mc].ID, Text = m.measureComponents[mc].measureComponent };

                    if (measureTextBox[mc].ID == "5" || measureTextBox[mc].ID == "6")
                    {
                        measureTextBox[mc].TextChanged += new EventHandler(weightHeight_textChanged);
                        measureTextBox[mc].AutoPostBack = true;
                    }

                    placeHolderList.Controls.Add(measureLabel[mc]);
                    placeHolderList.Controls.Add(measureTextBox[mc]);

                    placeHolderList.Controls.Add(new Literal() { Text = " " + m.measureComponents[mc].unit + " </div>" });

                  /*  if(Page.IsPostBack)
                        measureTextBox[mc] = placeHolderList.FindControl(m.measureComponents[mc].measureComponentID.ToString()) as TextBox;*/

                }
            }
        }

        
        protected void weightHeight_textChanged(object sender, EventArgs e) {
            TextBox t = sender as TextBox;
            if (t.ID == "5")
            {
                Session.Add("tb8", Math.Pow(double.Parse(t.Text) / 100.0, 2).ToString()) ;
                
            }
            if (t.ID == "6" && Session["tb8"] != null)
            {
                double temp = double.Parse(Session["tb8"].ToString());
                Session.Add("tb7", (double.Parse(t.Text)/temp).ToString());
                
            }

            
           /* if (getTextBox(5) != null)
                getTextBox(8).Text = Math.Pow(double.Parse(getTextBox(5).Text) / 100.0, 100).ToString();
            
            if (getTextBox(5) != null && getTextBox(6) != null)
                getTextBox(7).Text = (double.Parse(getTextBox(6).Text) / Math.Pow(double.Parse(getTextBox(5).Text) / 100.0, 100)).ToString();*/
            
        }
                
        protected void saveBtnClick(object sender, EventArgs e){
            
            HWService.UserMeasureComponent[] mc = new HWService.UserMeasureComponent[measureNo];

            DateTime newDate = new DateTime(year,month,day,int.Parse(timeHour.SelectedValue),int.Parse(timeMin.SelectedValue),sec);

            foreach(var c in measureTextBox)
            {
                if (c.Text != "")
                {
                    service.UserSetMeasureParameterized(token, newDate, measure[0].measureID, int.Parse(c.ID), c.Text, 0, "", 10);
                }
            }
            Response.Redirect("ActivityMeasurement.aspx?datetime=" + newDate.ToString("yyyy-MM-ddTHH:mm:ss")); 
        }
    }

          
}