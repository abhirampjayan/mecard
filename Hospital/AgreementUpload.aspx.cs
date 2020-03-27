using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class Hospital_AgreementUpload : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    MailMessage Email = new MailMessage();

    protected override void InitializeCulture()
    {
        //Session["Language"] = "";
        //string culture = "";
        //try
        //{
        //    culture = Request.QueryString["l"].ToString();
        //    Session["Language"] = culture;
        //}
        //catch (Exception ex)
        //{ }
        //// string culture = Session["Language"].ToString();
        //if (string.IsNullOrEmpty(culture))
        //{
        //    culture = "Auto";
        //    Session["Language"] = culture;
        //}
        ////Use this
        //UICulture = culture;
        //Culture = culture;
        ////OR This
        //if (culture != "Auto")
        //{

        //    System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
        //    System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
        //    System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
        //}
        //else
        //{
        //    //LinkButton1.Text = "عربى";
        //}

        //base.InitializeCulture();
    }
    void Page_PreInit(Object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{

        //}
        //else
        //{
        //    this.MasterPageFile = "~/hospital/ArabichospitalMaster.master";
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            try
            {
                CheckLocation();
            }
            catch (Exception ex)
            {
                Response.Redirect("../index/hospita login.aspx");
            }
            LblH_RegnNo.Text = Session["hakkeemid_h"].ToString();
        }
    }

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                    select new { item1.h_id, item.latitude };
       
            if (query.Count() <= 0)
            {
            //Label8.Text = "You must set your location";
            //ModalPopupExtender4.Show();
            RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location')</Script>");
            //  Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
        }

    }
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                string ext = (Path.GetExtension(FileUpload1.FileName)).ToLower();
                if (ext == ".pdf"||ext==".jpg")
                {
                    string path = "~/HospitalAgreementsImages/" + LblH_RegnNo.Text + FileUpload1.FileName;

                    var Query = (from item in db.tbl_hospitalregs
                                 where item.h_hakkimid == LblH_RegnNo.Text
                                 select item);
                    foreach (var ss in Query)
                    {
                        if (ss.h_agreement == null)
                        {
                            FileUpload1.SaveAs(Server.MapPath("~/HospitalAgreementsImages/" + LblH_RegnNo.Text + FileUpload1.FileName));
                            ss.h_agreement = path;
                            db.SubmitChanges();
                            //Mail to admin
                            Email.mail("mail@goldenetqan.com", " A hospital with registraion number" + LblH_RegnNo.Text + "uploaded agreement please check" + "link","Agreement uploaded");

                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Agreement file uploaded succesfully');window.location='Hospital.aspx'</Script>");
                            //Label1.Text = "Agreement file uploaded succesfully";
                            //this.ModalPopupExtender1.Show();
                        }
                    }
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf files or image format files')</Script>");
                    //Label2.Text = "Upload only pdf files or jpg files";
                    //this.ModalPopupExtender2.Show();
                }
            }
            else
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please choose a file')</Script>");
                //Label2.Text = "Please choose a file";
                //this.ModalPopupExtender2.Show();
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex);
        }
    }

    #region EmailFunction

    //public void Email(string email, string msg)
    //{
    //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //    mail.To.Add(email);
    //    mail.From = new MailAddress("bookdoc2017@gmail.com", "Hakkeem", System.Text.Encoding.UTF8);
    //    mail.Subject = "Account creation";
    //    mail.SubjectEncoding = System.Text.Encoding.UTF8;
    //    mail.Body = msg;
    //    //mail.Attachment = "attachment path";
    //    mail.BodyEncoding = System.Text.Encoding.UTF8;
    //    mail.IsBodyHtml = true;
    //    mail.Priority = MailPriority.High;
    //    SmtpClient client = new SmtpClient();
    //    client.Credentials = new System.Net.NetworkCredential("bookdoc2017@gmail.com", "bookdoc12345");
    //    client.Port = 587;
    //    client.Host = "smtp.gmail.com";
    //    client.EnableSsl = true;
    //    try
    //    {
    //        client.Send(mail);
    //        //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        //Exception ex2 = ex;
    //        //string errorMessage = string.Empty;
    //        //while (ex2 != null)
    //        //{
    //        //    errorMessage += ex2.ToString();
    //        //    ex2 = ex2.InnerException;
    //        //}
    //        //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
    //    }
    //}
    #endregion

    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Hospital/Hospital.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
    }
}