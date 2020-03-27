using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for MessageClass
/// </summary>
public class MessageClass
{
    public MessageClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string SendMessage(string username, string password, string msg, string sender, string numbers)
    {
       
            //int temp = '0';

            HttpWebRequest req = (HttpWebRequest)
            WebRequest.Create("http://www.mobily.ws/api/msgSend.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string postData = "mobile=" + username + "&password=" + password + "&numbers=" + numbers + "&sender=" + sender + "&msg=" + ConvertToUnicode(msg) + "&applicationType=59";
            req.ContentLength = postData.Length;

            StreamWriter stOut = new
            StreamWriter(req.GetRequestStream(),
            System.Text.Encoding.ASCII);
            stOut.Write(postData);
            stOut.Close();
            // Do the request to get the response
            string strResponse;
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            return strResponse;
       
    }
    private string ConvertToUnicode(string val)
    {

        string msg2 = string.Empty;

        for (int i = 0; i < val.Length; i++)
        {
            msg2 += convertToUnicode(System.Convert.ToChar(val.Substring(i, 1)));
        }

        return msg2;
    }
    private string convertToUnicode(char ch)
    {
        System.Text.UnicodeEncoding class1 = new System.Text.UnicodeEncoding();
        byte[] msg = class1.GetBytes(System.Convert.ToString(ch));

        return fourDigits(msg[1] + msg[0].ToString("X"));
    }
    private string fourDigits(string val)
    {
        string result = string.Empty;

        switch (val.Length)
        {
            case 1: result = "000" + val; break;
            case 2: result = "00" + val; break;
            case 3: result = "0" + val; break;
            case 4: result = val; break;
        }

        return result;
    }
}