using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
/// <summary>
/// Summary description for MailMessage
/// </summary>
public class MailMessage
{
    public MailMessage()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void mail(string email, string msg, string subject)
    {
        try
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("mail@goldenetqan.com", "Hakkeem", System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = msg;
            //mail.Attachment = "attachment path";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("mail@goldenetqan.com", "Etqan2016!!");
            client.Port = 587;
            client.Host = "smtp.goldenetqan.com";
            client.EnableSsl = false;
            try
            {
                client.Send(mail);
                //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch (Exception ex)
            {
                //Exception ex2 = ex;
                //string errorMessage = string.Empty;
                //while (ex2 != null)
                //{
                //    errorMessage += ex2.ToString();
                //    ex2 = ex2.InnerException;
                //}
                //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
        }
        catch (Exception ex) { }
    }
}