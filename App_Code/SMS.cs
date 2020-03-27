using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for SMS
/// </summary>
public class SMS
{

   public string api_key = "2ceef1b8";
   public string api_secret = "08b79a10e5baf4d2";
   public string from = "Hakkeem";


    public void Message(string to, string msg)
    {
        try
        {
            string api = "https://rest.nexmo.com/sms/json?api_key=" + api_key + "&api_secret=" + api_secret + "&to=" + to + "&from=" + from + "&text=" + msg + "";
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(api);
            HttpWebResponse res = (HttpWebResponse)rq.GetResponse();
        }
        catch (Exception ex) { }
    }

}