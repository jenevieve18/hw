using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using System.Web.SessionState;

namespace eform
{
	/// <summary>
	/// Summary description for ErrorHandling.
	/// </summary>
	public class ErrorHandling
	{
		public static string filterForXml(string s)
		{
			return s.Replace("&","&amp;").Replace(">","&gt;").Replace("<","&lt;").Replace("\"","&quot;").Replace("'","&apos;");
		}
//		public static string sendSMS(string to, string msg, string from)
//		{
//			string ret = "";
//			try
//			{
//				DateTime DT = DateTime.Now;
//
//				// Set the XML content
//				string strXML="<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
//				strXML += "<sms-teknik>";
//				strXML += "<copyright>Copyright (c) 2003-2006 SMS-Teknik AB</copyright>";
//				strXML += "<send_later>0</send_later>";
//				strXML += "<send_date>" + DT.ToString("yyyy-MM-dd") + "</send_date>";
//				strXML += "<send_time>" + DT.ToString("HH:mm:ss") + "</send_time>";
//				strXML += "<udmessage><![CDATA[" + msg + "]]></udmessage>";
//				strXML += "<smssender>" + filterForXml(from) + "</smssender>";
//				strXML += "<flash>0</flash>";
//				strXML += "<items>";
//				strXML += "<recipient>";
//				strXML += "<orgaddress>" + filterForXml(to) + "</orgaddress>";
//				strXML += "</recipient>";
//				strXML += "</items>";
//				strXML += "</sms-teknik>";
//
//				ret = postSMS("SendSMSv2.asp",strXML);
//			}
//			catch(Exception ex)
//			{
//				projectSetup.sendMail(
//					"support@eform.se",
//					"info@eform.se",
//					"eForm reported an error when trying to report error over SMS",
//					ex.Message + ex.StackTrace,
//					System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"]);
//			}
//			return ret;
//		}
//		public static string postSMS(string function, string message)
//		{
//			// Set the URL to send the XML
//			String sURL = "https://www.smsteknik.se/Member/SMSConnectDirect/" + function + "?id=Pharma+Consulting&user=sms21G7L%3D&pass=1ZN7Sd";
//
//			return httpPost(sURL, message);
//		}
//		public static string httpPost(string sURL, string message)
//		{
//			// Create the request object
//			System.Net.HttpWebRequest req = System.Net.HttpWebRequest.Create(sURL) as System.Net.HttpWebRequest;
//
//			// Set the request content type
//			req.ContentType = "application/x-www-form-urlencoded";
//			
//			// You must set the ContentLength to send anything
//			UTF8Encoding encodedData = new UTF8Encoding();
//			byte[] byteArray = encodedData.GetBytes(message);
//			req.ContentLength = byteArray.Length;
//
//			// Set request method to use
//			req.Method="POST";
//
//			// Create a stream to write the XML data
//			Stream s = req.GetRequestStream(); //gives you a stream
//
//			// Write the XML data
//			s.Write(byteArray,0,byteArray.Length);
//
//			// You MUST close these stream object NOW, or you can blow up the connection to the web service()
//			s.Close();
//
//			// Get the response
//			HttpWebResponse WebResponse = (HttpWebResponse) req.GetResponse();
//			
//			// We will read data via the response stream
//			Stream StreamReader = WebResponse.GetResponseStream();
//
//			// Used to build entire input
//			StringBuilder sb  = new StringBuilder();
//			string tempString = null;
//			int count = 0;
//			byteArray = new byte[1024];
//
//			do
//			{
//				// Fill the buffer with data
//				count = StreamReader.Read(byteArray, 0, byteArray.Length);
//
//				// Make sure we read some data
//				if (count != 0)
//				{
//					// translate from bytes to ASCII text
//					tempString = Encoding.ASCII.GetString(byteArray, 0, count);
//
//					// continue building the string
//					sb.Append(tempString);
//				}
//			}
//			while (count > 0); // Any more data to read?
//
//			// Clean up
//			WebResponse.Close();
//			StreamReader.Close();
//			req = null;
//
//			return sb.ToString();
//		}

		public ErrorHandling(Exception theException, HttpSessionState theHttpSessionState, HttpRequest theHttpRequest)
		{
			System.Text.StringBuilder message = new System.Text.StringBuilder();
			string formKeys = "", sessionData = "";

			try
			{
				foreach(string s in theHttpRequest.Form.AllKeys)
				{
					formKeys += s + " = " + (theHttpRequest.Form[s] != null ? theHttpRequest.Form[s] : "") + "\r\n";
				}
			}
			catch(Exception){}
			if(theHttpSessionState != null)
			{
				try
				{
					foreach(string s in theHttpSessionState.Keys)
						sessionData += s + " = " + theHttpSessionState[s].ToString() + "\r\n";
				}
				catch(Exception){}
			}
			try
			{
				int exceptionLevel = 0;
				while(theException != null)
				{
					exceptionLevel++;
					message.Append(exceptionLevel + ". " + (theException.Message != null ? theException.Message + "\r\n" : "") + (theException.TargetSite != null ? theException.TargetSite.Name + "\r\n" : "") + (theException.StackTrace != null ? theException.StackTrace + "\r\n" : ""));
					theException = theException.InnerException;
				}
			}
			catch(Exception){}
			string ret = "";
			try
			{
				
			}
			catch(Exception){}
			try
			{
				projectSetup.sendMail(
					"support@eform.se",
					"info@eform.se",
					"eForm reported an error",
					"URL: " + theHttpRequest.RawUrl + 
					(message.Length > 0 ? "\r\n\r\n" + message.ToString() : "") + 
					"\r\n\r\n" + formKeys + 
					"\r\n\r\n" + sessionData + 
					"\r\n\r\nSMS: " + ret,
					System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"]);
			}
			catch(Exception){}
		}

	}
}
