using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW
{
    public partial class ext : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request["Action"] != null)
            {
                switch (HttpContext.Current.Request["Action"])
                {
                    case "Login":
                        {
                            if (HttpContext.Current.Request["UN"] != null && HttpContext.Current.Request["PW"] != null)
                            {
                                HttpContext.Current.Response.Write(Db.checkUsernamePassword(HttpContext.Current.Request["UN"].ToString(), HttpContext.Current.Request["PW"].ToString()).ToString());
                            }
                            else
                            {
                                HttpContext.Current.Response.Write("0");
                            }
                            break;
                        }
                    case "Submit":
                        {
                            string ret = "";
                            foreach (string s in HttpContext.Current.Request.QueryString.Keys)
                                ret += s + " = " + HttpContext.Current.Request.QueryString[s] + "&";
                            foreach (string s in HttpContext.Current.Request.Form.Keys)
                                ret += s + " = " + HttpContext.Current.Request.Form[s] + "&";
                            Db.exec("INSERT INTO Debug (DebugTxt) VALUES ('" + ret.Replace("'", "''") + "')", "eFormSqlConnection");
                            HttpContext.Current.Response.Write("1");
                            break;
                        }
                    case "DeviceLogin":
                        {
                            if (HttpContext.Current.Request["UDID"] != null)
                            {
                                HttpContext.Current.Response.Clear();
                                HttpContext.Current.Response.ClearHeaders();
                                HttpContext.Current.Response.ContentType = "application/xml";

                                HttpContext.Current.Response.Write("" +
                                    "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><iHear version=\"1.0\">" +
                                    "<device InternID=\"Unknown\" id=\"" + HttpContext.Current.Request["UDID"].ToString() + "\" />" +
                                    "<company>" +
                                    "<name>Tinmin</name>" +
                                    "<logoImgUrl>http://ihear.brickit.se/tinminLogga.png</logoImgUrl>" +
                                    "<companyText language=\"sv\">" +
                                    "<loginHeader>Saknar du inloggningsuppgifter?</loginHeader>" +
                                    "<loginBody>Vänligen gå till www.healthwatch.se/forgot.aspx</loginBody>" +
                                    "</companyText>" +
                                    "<companyText language=\"en\">" +
                                    "<loginHeader>Lost your login details?</loginHeader>" +
                                    "<loginBody>Please visit www.healthwatch.se/forgot.aspx</loginBody>" +
                                    "</companyText>" +
                                    "</company>" +
                                    "<tests>" +
                                    "<tonaudiometri>" +
                                    "<freqs>" +
                                    "<freqTA name=\"1KHz -28dB(Ref) ATH-M20\" hertz=\"1000\" maxdB=\"85.0\" status=\"ON\" />" +
                                    "<freqTA name=\"1,5KHz -38,5dB(Ref) ATH-M20\" hertz=\"1500\" maxdB=\"83.0\" status=\"OFF\" />" +
                                    "<freqTA name=\"2KHz -38,5dB(Ref) ATH-M20\" hertz=\"2000\" maxdB=\"83.0\" status=\"OFF\" />" +
                                    "<freqTA name=\"3KHz -33dB(Ref) ATH-M20\" hertz=\"3000\" maxdB=\"78.0\" status=\"OFF\" />" +
                                    "<freqTA name=\"4KHz -25,5dB(Ref) ATH-M20\" hertz=\"4000\" maxdB=\"78.0\" status=\"OFF\" />" +
                                    "<freqTA name=\"6KHz -24,5dB(Ref) ATH-M20\" hertz=\"6000\" maxdB=\"85.0\" status=\"OFF\" />" +
                                    "<freqTA name=\"8KHz -22dB(Ref) ATH-M20\" hertz=\"8000\" maxdB=\"83.0\" status=\"ON\" />" +
                                    "<freqTA name=\"500Hz -5dB(Ref) ATH-M20\" hertz=\"500\" maxdB=\"82.0\" status=\"ON\" />" +
                                    "<freqTA name=\"250Hz -3dB(Ref) ATH-M20\" hertz=\"250\" maxdB=\"82.0\" status=\"OFF\" />" +
                                    "<freqTA name=\"125Hz 0dB(Ref) ATH-M20\" hertz=\"125\" maxdB=\"82.0\" status=\"OFF\" />" +
                                    "</freqs>" +
                                    "</tonaudiometri>" +
                                    "<tolerans>" +
                                    "<freqs>" +
                                    "<freqTOL name=\"1,0kHz sine REF(0dB)\" hertz=\"1000\" maxdB=\"85.0\" status=\"ON\" />" +
                                    "<freqTOL name=\"1,5kHz sine REF(0dB)\" hertz=\"1500\" maxdB=\"83.0\" status=\"OFF\" />" +
                                    "<freqTOL name=\"2,0kHz sine REF(0dB)\" hertz=\"2000\" maxdB=\"83.0\" status=\"ON\" />" +
                                    "<freqTOL name=\"3,0kHz sine REF(0dB)\" hertz=\"3000\" maxdB=\"78.0\" status=\"OFF\" />" +
                                    "<freqTOL name=\"4,0kHz sine REF(0dB)\" hertz=\"4000\" maxdB=\"78.0\" status=\"OFF\" />" +
                                    "<freqTOL name=\"6,0kHz sine REF(0dB)\" hertz=\"6000\" maxdB=\"85.0\" status=\"OFF\" />" +
                                    "<freqTOL name=\"8,0kHz sine REF(0dB)\" hertz=\"8000\" maxdB=\"83.0\" status=\"OFF\" />" +
                                    "<freqTOL name=\"500Hz sine REF(0dB)\" hertz=\"500\" maxdB=\"82.0\" status=\"OFF\" />" +
                                    "<freqTOL name=\"250Hz sine REF(0dB)\" hertz=\"250\" maxdB=\"82.0\" status=\"ON\" />" +
                                    "<freqTOL name=\"125Hz sine REF(0dB)\" hertz=\"125\" maxdB=\"82.0\" status=\"OFF\" /> " +
                                    "</freqs>" +
                                    "</tolerans>" +
                                    "</tests>" +
                                    "</iHear>");
                                HttpContext.Current.Response.Flush();
                                HttpContext.Current.Response.Close();
                            }
                            break;
                        }
                }
            }
        }
    }
}