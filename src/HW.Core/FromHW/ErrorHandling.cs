using System;
using System.Web;
using System.Web.SessionState;

namespace HW.Core.FromHW
{
	public class ErrorHandling
	{
		private string cutAt(int i, string s)
        {
            return (s.Length <= i ? s : s.Substring(0, i));
        }
        public ErrorHandling(Exception theException, HttpSessionState theHttpSessionState, HttpRequest theHttpRequest)
        {
            string message = "", err = "", reqErr = "";

            try
            {
                reqErr = "";
                if (theHttpRequest != null)
                    reqErr += "" +
                       "'" + (theHttpRequest.RequestType != null ? cutAt(8, theHttpRequest.RequestType) : "") + "'," +
                        "" + (theHttpRequest.ServerVariables["SERVER_PORT"] != null ? Convert.ToInt32(theHttpRequest.ServerVariables["SERVER_PORT"].ToString()) : 0) + "," +
                        "'" + (theHttpRequest.ServerVariables["HTTPS"] != null ? cutAt(4, theHttpRequest.ServerVariables["HTTPS"].ToString()).Replace("'", "") : "") + "'," +
                        "'" + (theHttpRequest.ServerVariables["LOCAL_ADDR"] != null ? cutAt(16, theHttpRequest.ServerVariables["LOCAL_ADDR"].ToString()).Replace("'", "") : "") + "'," +
                        "'" + (theHttpRequest.UserHostAddress != null ? cutAt(16, theHttpRequest.UserHostAddress).Replace("'", "") : "") + "'," +
                        "'" + (theHttpRequest.UserAgent != null ? cutAt(255, theHttpRequest.UserAgent).Replace("'", "") : "") + "'," +
                        "'" + (theHttpRequest.RawUrl != null ? cutAt(255, theHttpRequest.RawUrl).Replace("'", "") : "") + "',";

                string formKeys = "", sessionData = "";
                if (theHttpRequest != null && theHttpRequest.Form != null)
                    foreach (string s in theHttpRequest.Form.AllKeys)
                        formKeys += s + " = " + (theHttpRequest.Form[s] != null ? theHttpRequest.Form[s] : "") + "\r\n";
                if (theHttpSessionState != null)
                    foreach (string s in theHttpSessionState.Keys)
                        sessionData += s + " = " + (theHttpSessionState[s] != null ? theHttpSessionState[s].ToString() : "") + "\r\n";

                int exceptionLevel = 0;
                while (theException != null)
                {
                    exceptionLevel++;

                    message += exceptionLevel + ". " + theException.Message + "\r\n" + theException.TargetSite.ToString() + "\r\n" + theException.StackTrace + "\r\n";

                    theException = theException.InnerException;
                }
            }
            catch (Exception x) { err += x.Message + x.Source + x.StackTrace + x.TargetSite + "\r\n\r\n"; }

            try
            {
                Db.sendMail(
                    "support@healthwatch.se",
                    "support@healthwatch.se",
                    (theHttpRequest != null ? "URL: " + theHttpRequest.RawUrl + "\r\n\r\n" : "") +
                    (message != "" ? message + "\r\n\r\n" : "") +
                    err,
                    "HW reported an error");
            }
            catch (Exception x) { err += x.Message + x.Source + x.StackTrace + x.TargetSite + "\r\n\r\n"; }
        }
	}
}
