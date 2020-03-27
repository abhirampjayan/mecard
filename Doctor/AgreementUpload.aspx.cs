using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;


public partial class Doctor_AgreementUpload : System.Web.UI.Page
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
        //    this.MasterPageFile = "~/Doctor/ArabicMasterPage.master";
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            //Response.Write(HiddenField1.Value);
            CheckLocation();
            LblDocId.Text = Session["hakkeemid_d"].ToString();
            //if(HiddenField1.Value=="1")
            //{
            //    upload_agrmnt();
            //}
        }
    }
    public void CheckLocation()
    {
        var query = from item in db.tbl_doctor_locations
                    join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                    where item1.d_hakkimid == Session["hakkeemid_d"].ToString()
                    select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
        if (query.Count() <= 0)
        {
            Response.Redirect("~/Doctor/SetLocation.aspx");
        }
    }
    //public void upload_agrmnt()
    //{
    //    if (FileUpload1.HasFile)
    //    {
    //        try
    //        {
    //            string ext = (Path.GetExtension(FileUpload1.FileName)).ToLower();
    //            if (ext == ".pdf" || ext == ".jpg")
    //            {
    //                string path = "~/DoctorAgreements/" + LblDocId.Text + FileUpload1.FileName;

    //                var Query = (from item in db.tbl_doctors
    //                             where item.d_hakkimid == LblDocId.Text
    //                             select item);
    //                foreach (var ss in Query)
    //                {
    //                    if (ss.d_agreement == null)
    //                    {
    //                        FileUpload1.SaveAs(Server.MapPath("~/DoctorAgreements/" + LblDocId.Text + FileUpload1.FileName));
    //                        ss.d_agreement = path;
    //                        db.SubmitChanges();
    //                        Email.mail("mail@goldenetqan.com", " A doctor with registraion id" + LblDocId.Text + "uploaded agreement please check" + "link", "Agreement uploaded");
    //                        HiddenField1.Value = "0";
    //                        this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('Agreement file uploaded succesfully');window.location='Doctor.aspx'</Script>");
    //                        //Label1.Text = "Agreement file uploaded succesfully...!";
    //                        //this.ModalPopupExtender1.Show();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                //RegisterStartupScript("", "<Script Language=JavaScript>alert('Upload only pdf files ')</Script>");
    //                Label2.Text = "Upload only pdf files or jpg files";
    //                this.ModalPopupExtender2.Show();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Response.Write(ex);
    //        }
    //    }
    //}

    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                string ext = (Path.GetExtension(FileUpload1.FileName)).ToLower();
                if (ext == ".pdf" || ext == ".doc")
                {
                    string path = "~/DoctorAgreements/" + LblDocId.Text + FileUpload1.FileName;

                    var Query = (from item in db.tbl_doctors
                                 where item.d_hakkimid == LblDocId.Text
                                 select item);
                    foreach (var ss in Query)
                    {
                        if (ss.d_agreement == null)
                        {
                            FileUpload1.SaveAs(Server.MapPath("~/DoctorAgreements/" + LblDocId.Text + FileUpload1.FileName));
                            ss.d_agreement = path;
                            db.SubmitChanges();
                            Email.mail("mail@goldenetqan.com", " A doctor with registraion id" + LblDocId.Text + "uploaded agreement please check" + "link", "Agreement uploaded");

                            //RegisterStartupScript("", "<Script Language=JavaScript>swal('Agreement file uploaded successfully');window.location='Doctor.aspx'</Script>");
                            //Label1.Text = "Agreement file uploaded succesfully...!";
                            //this.ModalPopupExtender1.Show();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                lblModalBody.Text = "Agreement file uploaded successfully";
                            //}
                            //else
                            //{
                            //    lblModalBody.Text = "تم تحميل ملف الاتفاقية بنجاح";
                            //}
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                    }
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf files ')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف فقط ')</Script>");
                    //}
                    //Label2.Text = "Upload only pdf files or jpg files";
                    //this.ModalPopupExtender2.Show();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
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
        Response.Redirect("~/Doctor/Doctor.aspx");
    }



    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Doctor/DoctorHome.aspx");
    }
}