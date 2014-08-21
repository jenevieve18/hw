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
        protected string token;
        protected int id;

        protected TextBox[] measureTextBox;
        protected Label[] measureLabel;
        protected int componentcount;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
            token = Session["token"].ToString();

            int lang = int.Parse(Session["languageId"].ToString());


            if (Request.QueryString["datetime"] != null)
            {
                var temp = Request.QueryString["datetime"];
                int year = int.Parse(temp.Substring(0, 4));
                int month = int.Parse(temp.Substring(5, 2));
                int day = int.Parse(temp.Substring(8, 2));
                int hrs = int.Parse(temp.Substring(11, 2));
                int min = int.Parse(temp.Substring(14, 2));
                int sec = int.Parse(temp.Substring(17, 2));

                date = new DateTime(year, month, day, hrs, min, sec);

            }

            id = int.Parse(Request.QueryString["mcid"]);


            
            measure = service.MeasureEnum(new HWService.MeasureEnumRequest(token, id, lang, 10)).MeasureEnumResult;
            //measureNo = measure.Count();
            lblHeader.Text = measure[0].measureCategory;
            if (!Page.IsPostBack)
            {
                timeHour.DataSource = Enumerable.Range(00, 24).Select(x => x.ToString("D2"));
                timeMin.DataSource = Enumerable.Range(00, 60).Select(x => x.ToString("D2"));
                timeHour.DataBind();
                timeMin.DataBind();
            }

            measureTextBox = new TextBox[measure.Count()];
            measureLabel = new Label[measure.Count()];
            componentcount = measure.Count();
            int mcount = 0;
            foreach (var m in measure)
            {
               
                for (var mc = 0; mc < m.componentCount; mc++)
                {
                    
                    placeHolderList.Controls.Add(new Literal() { Text = "<div class='ui-grid-b'><div class='ui-block-a'>" });
                    measureTextBox[mcount] = new TextBox() { ID = m.measureComponents[mc].measureComponentID.ToString(), EnableViewState = true };
                    measureLabel[mcount] = new Label() { AssociatedControlID = measureTextBox[mcount].ID, Text = m.measureComponents[mc].measureComponent};

                    placeHolderList.Controls.Add(measureLabel[mcount]);
                    placeHolderList.Controls.Add(new Literal() { Text = "</div><div class='ui-block-b'>" });
                    placeHolderList.Controls.Add(measureTextBox[mcount]);
                    placeHolderList.Controls.Add(new Literal() { Text = "</div><div class='ui-block-c'> <span style='color:#A0A0A0;'>" + m.measureComponents[mc].unit + "</span></div></div>"  });
                    
                  /*  if(Page.IsPostBack)
                        measureTextBox[mc] = placeHolderList.FindControl(m.measureComponents[mc].measureComponentID.ToString()) as TextBox;*/
                }
                mcount++;
            }
        }

        
                      
        protected void saveBtnClick(object sender, EventArgs e){
            
            DateTime newDate = new DateTime(date.Year,date.Month,date.Day,int.Parse(timeHour.SelectedValue),int.Parse(timeMin.SelectedValue),date.Second);
            HWService.UserMeasureComponent[] umc = new HWService.UserMeasureComponent[1];
            for(var c = 0; c < measureTextBox.Length;c++)
            {
                if (measureTextBox[c].Text != "")
                {
                    
                    umc[0] = new HWService.UserMeasureComponent();
                    umc[0].MeasureComponentID = int.Parse(measureTextBox[c].ID);
                    umc[0].value = measureTextBox[c].Text;
                    
                    service.UserSetMeasure(new HWService.UserSetMeasureRequest(token,newDate,measure[c].measureID,umc,10));
                }
            }
            
            Response.Redirect("ActivityMeasurement.aspx?datetime=" + newDate.ToString("yyyy-MM-ddTHH:mm:ss")); 
        }
    }

          
}