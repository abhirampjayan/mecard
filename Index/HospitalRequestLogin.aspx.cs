using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.IO;

public partial class Index_HospitalRequestLogin : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

    string H_Id = "";
    string H_Mail = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnSignIn_Click(object sender, EventArgs e)
    {
        if(TxtH_RegnNo.Text !="" && TxtH_RegnDate.Text !="")
        {
           string date=DateTime.Parse(TxtH_RegnDate.Text).ToString("yyyy-MM-dd");
           var selectHospital = from hospital in db.tbl_hospitalregs
                                where hospital.h_regno == TxtH_RegnNo.Text && hospital.h_date_time == date
                                select hospital;
           if (selectHospital.Count() > 0)
           {
               foreach(var ss in selectHospital)
               {
                   if (ss.h_status==0 )
                   {
                       if (ss.h_agreement == "")
                       {
                           BtnUpload.Visible = true;
                           FileUpload1.Visible = true;
                       }
                       else
                       {
                           RegisterStartupScript("", "<Script Language=JavaScript>alert('You are already uploaded the agreement file. Please wait for approval')</Script>");
                       }
                   }
                   else
                   {
                          RegisterStartupScript("", "<Script Language=JavaScript>alert('This hospital is already complete the registration')</Script>");
                   }
               }
              
               //foreach(var s in selectHospital)
               //{
               //    H_Id = s.h_id.ToString();
               //    H_Mail = s.h_email.ToString();
               //}
           }
           else
           {
               //Response.Write("<script>alert('Hospital Not registered. Please Register ')</sccript>");
               RegisterStartupScript("", "<Script Language=JavaScript>alert('Registartion Number or Date is incorrect ')</Script>");
           }

        }
        else
        {
            //Response.Write("<script>alert('Registartion Number or Date mismatch.')</sccript>");
            RegisterStartupScript("", "<Script Language=JavaScript>alert('Registartion Number or Date mismatch.')</Script>");
        }

    }
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        string date=DateTime.Parse(TxtH_RegnDate.Text).ToString("yyyy-MM-dd");
           var selectHospital = from hospital in db.tbl_hospitalregs
                                where hospital.h_regno == TxtH_RegnNo.Text && hospital.h_date_time == date
                                select hospital;
           if (selectHospital.Count() > 0)
           {
               string Hos_Id = TxtH_RegnNo.Text;
               if (FileUpload1.HasFile)
               {
                   string extn = (Path.GetExtension(FileUpload1.FileName)).ToLower();
                   if (extn == ".pdf")
                   {

                       string path = "~/HospitalAgreementsImages/" + Hos_Id + FileUpload1.FileName;
                       FileUpload1.SaveAs(Server.MapPath("~/HospitalAgreementsImages/" + Hos_Id + FileUpload1.FileName));

                       //var select = from a in db.tbl_hospitalregs
                       //             where a.h_id == Convert.ToInt32(H_Id)
                       //             select a;
                       foreach (var s in selectHospital)
                       {
                           s.h_agreement = path;
                           db.SubmitChanges();
                       }

                       //Response.Write("<script>alert('Uploaded Succesfully')</sccript>");
                       RegisterStartupScript("", "<Script Language=JavaScript>alert('Uploaded Succesfully')</Script>");
                       BtnUpload.Visible = false;
                       FileUpload1.Visible = false;
                       Email("goldenetqan@gmail.com", " A hospital with registraion number" + Hos_Id + "uploaded agreement please check" + "link");
                   }
                   else
                   {
                       //Response.Write("<script>alert('Upload only pdf files')</sccript>");
                       RegisterStartupScript("", "<Script Language=JavaScript>alert('Upload only pdf files ')</Script>");
                   }
               }
               else
               {
                   //Response.Write("<script>alert('Uploaded Succesfully')</sccript>");
                   RegisterStartupScript("", "<Script Language=JavaScript>alert('Please choose a file')</Script>");

               }
           }
           else
           {
               RegisterStartupScript("", "<Script Language=JavaScript>alert('incorrect Registration number or date ')</Script>");
           }

    }
    #region EmailFunction

    public void Email(string email, string msg)
    {
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(email);
        mail.From = new MailAddress("bookdoc2017@gmail.com", "BookDoc", System.Text.Encoding.UTF8);
        mail.Subject = "Account creation";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = msg;
        //mail.Attachment = "attachment path";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("bookdoc2017@gmail.com", "bookdoc12345");
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        client.EnableSsl = true;
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
    #endregion
}