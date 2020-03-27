using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index_HospitaLogin : System.Web.UI.Page
{

    secure obj = new secure();
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    SMS ob = new SMS();
    string userType = "";
    string userEmail = "";
    string hregn = "";
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


    protected void Page_Load(object sender, EventArgs e)
    {
        userType = Request.QueryString["type"];
        if (userType == "user" || userType == "doctor")
        {
            userEmail = Request.QueryString["email"];
        }
        else if (userType == "hospital")
        {
            userEmail = Request.QueryString["email"];
        }
        else if (userType == "hosdoctor")
        {
            userEmail = Request.QueryString["email"];
            hregn = Request.QueryString["hregn"];
        }
    }
    protected void BtnContinue_Click(object sender, EventArgs e)
    {
        PnlCodeFailed.Visible = false;
        try
        {
            if (userType == "user")
            {
                var selectUser = from item in db.tbl_signups
                                 where item.email == userEmail && item.passwordreset == obj.EnryptString(TxtCode.Text)
                                 select item;
                if (selectUser.Count() > 0)
                {
                    PnlSecurityCode.Visible = false;
                    PnlNewPassword.Visible = true;
                    TxtNewPassword.Focus();
                }
                else
                {
                    PnlCodeFailed.Visible = true;




                }

            }
            else if (userType == "doctor")
            {
                var selectUser = from item in db.tbl_doctors
                                 where item.d_email == userEmail && item.d_password_reset == obj.EnryptString(TxtCode.Text)
                                 select item;
                if (selectUser.Count() > 0)
                {
                    PnlSecurityCode.Visible = false;
                    PnlNewPassword.Visible = true;
                    TxtNewPassword.Focus();
                }
                else
                {
                    PnlCodeFailed.Visible = true;
                }

            }
            else if (userType == "hospital")
            {
                var selectUser = from item in db.tbl_hospitalregs
                                 where item.h_email == obj.DecryptString(userEmail) && item.h_password_reset == obj.EnryptString(TxtCode.Text)
                                 select item;
                if (selectUser.Count() > 0)
                {
                    PnlSecurityCode.Visible = false;
                    PnlNewPassword.Visible = true;
                    TxtNewPassword.Focus();
                }
                else
                {
                    PnlCodeFailed.Visible = true;
                }

            }
            else if (userType == "hosdoctor")
            {
                var selectUser = from item in db.tbl_hdoctors
                                 where item.h_id == hregn && item.hd_password_reset == TxtCode.Text && item.hd_email == obj.DecryptString(userEmail)
                                 select item;
                if (selectUser.Count() > 0)
                {
                    PnlSecurityCode.Visible = false;
                    PnlNewPassword.Visible = true;
                    TxtNewPassword.Focus();
                }
                else
                {
                    PnlCodeFailed.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    public bool Email_To_changepassword(string email)
    {
        string cmpnyemail = "";
        string number = "";
        bool flag = true;
        string msgsubject, msgbody;

        MailMessage message = new MailMessage();


        int id;
        try
        {

            //con.Open();



            MailMessage msgMail = new MailMessage();
            MailMessage myMessage = new MailMessage();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbtitle = new StringBuilder();
            String messagestr = "";
            string bg = "http://www.hakkeem.com/head1.png";
            string follw = "http://www.hakkeem.com/followus.png";
            string face = "http://www.hakkeem.com/facebook.png";
            string twitter = "http://www.hakkeem.com/twitter.png";
            string insta = "http://www.hakkeem.com/instagram.png";
            string contact = "http://hakkeem.com/ContactUs.aspx";
            string privacy = "http://hakkeem.com/privacy%20policy.aspx";
            messagestr = messagestr + "<body style='text-align:center;width=100%'>";

            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto; background:#f2f2f2;padding:60px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + "<tr>";
            messagestr = messagestr + " <td>";
            messagestr = messagestr + " <table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + " <tr>";
            messagestr = messagestr + "<td  colspan='2' style='padding:20px 20px;background-color:#fff;line-height:2.2em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%' ></td>";

            messagestr = messagestr + "</tr>";
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>RESET PASSWORD</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "your password is changed successfully.";
            messagestr = messagestr + " </td></tr><tr><td style='text-align:center;padding:20px 20px;background-color: #fff;text-align:center'>";
            messagestr = messagestr + "</td></tr><tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
            messagestr = messagestr + " <tbody><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<span style='color:#4aa9af'><a href='"+privacy+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='" + contact + "' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
            messagestr = messagestr + "</span></td></tr><br><br><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<img src='" + follw + "'>";
            messagestr = messagestr + "<a href='https://www.facebook.com/hakkeem.etqan.1' style='text-decoration:none'><img src='" + face + "' title='Facbook'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://twitter.com/Hakkeem_1' style='text-decoration:none'><img src='" + twitter + "'  title='Twitter'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://www.instagram.com/hakkeem_1/' style='text-decoration:none'><img src='" + insta + "'  title='Instagram'>&nbsp;</a>";
            messagestr = messagestr + "</td></tr></tbody></table></td></tr>";

            messagestr = messagestr + "<tr><td  colspan='2' style='background-color:#fff;padding:20px 20px'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:25px'>";
            messagestr = messagestr + " <tbody><tr>";
            messagestr = messagestr + "<td width='100%' style='color:#4aa9af;font-family:sans-serif; font-size:10px'>";
            messagestr = messagestr + " <span style='font-weight:bold;font-size:10px'> Discliamer</span> : Please do not print this email unless it is necessary. Every unprinted email helps the environment.";
            messagestr = messagestr + "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body>";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
            mail.Subject = "Hakkeem Reset Password";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = messagestr.ToString();
            // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("mail@hakkeem.com", "Hakkeem2018!!");
            client.Port = 25;
            client.Host = "smtp.goldenetqan.com";
            client.EnableSsl = false;
            try
            {
                client.Send(mail);

            }
            catch (Exception ex)
            {

            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return flag;
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            throw ex;
        }
    }
    protected void BtnContinue1_Click(object sender, EventArgs e)
    {
        try
        {
            if (userType == "user")
            {
                string username = "";
                string uph = "";
                var query = from item in db.tbl_signups
                            where item.email == userEmail
                            select item;
                foreach (var ss in query)
                {
                    username = ss.name;
                    uph = obj.DecryptString(ss.contact);
                }
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        Session["hakkemid_u"] = ss.u_hakkimid.ToString();
                        ss.password = obj.EnryptString(TxtConfrimPassword.Text);
                        ss.passwordreset = null;
                        db.SubmitChanges();
                    }
                    Session["user"] = obj.DecryptString(userEmail);
                    try
                    {
                        // Email.mail(obj.DecryptString(userEmail), "Dear " + username + ", your password is changed successfully" + "<p>Thank you. Hakkeem Team</p>", "Password recovery");
                        Email_To_changepassword(obj.DecryptString(userEmail));
                    }
                    catch (Exception ex) { }
                    try
                    {
                        ob.Message(uph, "Dear " + username + ", your password is changed successfully" + "Thank you.Hakkeem Team");
                    }
                    catch (Exception ex) { }
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("../User/Search.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("../User/Search.aspx?l=ar-EG");
                    //}
                }
                else
                { RegisterStartupScript("", "<Script Language=JavaScript>swal('Your mail id not valid')</Script>"); }
            }
            else if (userType == "doctor")
            {
                string dph = "";
                var query = from item in db.tbl_doctors
                            where item.d_email == userEmail
                            select item;
                foreach (var ss in query)
                {
                    dph = obj.DecryptString(ss.d_contact);
                }
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {

                        Session["hakkeemid_d"] = ss.d_hakkimid.ToString();
                        ss.d_password = obj.EnryptString(TxtConfrimPassword.Text);
                        ss.d_password_reset = null;
                        db.SubmitChanges();
                    }
                    Session["doctor"] = userEmail;
                    try
                    {
                        Email_To_changepassword(obj.DecryptString(userEmail));
                        // Email.mail(obj.DecryptString(userEmail), "Dear doctor, your password is changed successfully" + "<p>Thank you. Hakkeem Team</p>", "Password recovery");
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        ob.Message(dph, "Dear doctor, your password is changed successfully" + "Thank you. Hakkeem Team");
                    }
                    catch (Exception ex) { }
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/Doctor/DoctorHome.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Doctor/DoctorHome.aspx?l=ar-EG");
                    //}
                }
                else
                { RegisterStartupScript("", "<Script Language=JavaScript>swal('Your mail id not valid')</Script>"); }

            }
            else if (userType == "hospital")
            {
                string hname = "";
                string hph = "";
                string emailAddress = "";
                var query = from item in db.tbl_hospitalregs
                            where item.h_email == obj.DecryptString(userEmail)
                            select item;
                foreach (var ss in query)
                {
                    hname = ss.h_name;
                    hph = ss.h_contact;
                }
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        Session["hakkeemid_h"] = ss.h_hakkimid;
                        Session["hospital"] = ss.h_regno;
                        ss.h_password = obj.EnryptString(TxtConfrimPassword.Text);
                        ss.h_password_reset = null;
                        emailAddress = ss.h_email;
                        db.SubmitChanges();
                    }


                    try
                    {
                        Email_To_changepassword(emailAddress);
                        //Email.mail(obj.DecryptString(emailAddress), "Dear " + hname + " hospital authority, your hospital password is changed successfully" + "<p>Thank you. Hakkeem Team", "Password recovery");
                    }
                    catch (Exception ex) { }
                    try
                    {
                        ob.Message(hph, "Dear " + hname + " hospital authority, your hospital password is changed successfully" + "Thank you. Hakkeem Team");
                    }
                    catch (Exception ex)
                    { }
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/Hospital/hospital.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Hospital/hospital.aspx?l=ar-EG");
                    //}
                }
                else
                { RegisterStartupScript("", "<Script Language=JavaScript>swal('Your mail id not valid')</Script>"); }

            }
            else if (userType == "hosdoctor")
            {
                string hdname = "";
                string hdph = "";
                var query = from item in db.tbl_hdoctors
                            where item.hd_email == obj.DecryptString(userEmail) && item.h_id == hregn
                            select item;
                foreach (var ss in query)
                {
                    hdname = ss.hd_name;
                    hdph = ss.hd_contact;
                }
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        Session["HosDocId"] = ss.hd_email;
                        Session["HospitalId"] = ss.h_id;
                        ss.hd_password = TxtConfrimPassword.Text;
                        ss.hd_password_reset = null;
                        db.SubmitChanges();
                    }

                    try
                    {
                        Email_To_changepassword(obj.DecryptString(userEmail));
                        //Email.mail(obj.DecryptString(userEmail), "Dear doctor, your password is changed successfully" + "<p>Thank you. Hakkeem Team", "Password recovery");
                    }
                    catch (Exception ex) { }
                    try { ob.Message(hdph, "Dear doctor, your password is changed successfully" + "Thank you. Hakkeem Team"); }
                    catch (Exception ex) { }
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/HospitalDoctor/HospitalDoctorConsulting.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/HospitalDoctor/HospitalDoctorConsulting.aspx?l=ar-EG");
                    //}
                }
                else
                { RegisterStartupScript("", "<Script Language=JavaScript>swal('Your mail id not valid')</Script>"); }

            }
        }
        catch (Exception ex)
        {

        }
    }

    //public void Email(string email, string msg)
    //{
    //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //    mail.To.Add(email);
    //    mail.From = new MailAddress("bookdoc2017@gmail.com", "BookDoc", System.Text.Encoding.UTF8);
    //    mail.Subject = "BookDoc Password Reset";
    //    mail.SubjectEncoding = System.Text.Encoding.UTF8;
    //    mail.Body = msg;
    //    //mail.Attachment = "attachment path";
    //    mail.BodyEncoding = System.Text.Encoding.UTF8;
    //    mail.IsBodyHtml = true;
    //    mail.Priority = MailPriority.High;
    //    SmtpClient client = new SmtpClient();
    //    client.Credentials = new System.Net.NetworkCredential("bookdoc2017", "bookdoc12345");
    //    client.Port = 25;
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
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        DeletePasswordResetCode();
        CanelProcess();
    }

    private void DeletePasswordResetCode()
    {
        try
        {
            if (userType == "user")
            {
                var selectUser = from item in db.tbl_signups
                                 where item.email == userEmail
                                 select item;
                foreach (var ss in selectUser)
                {
                    ss.passwordreset = null;
                }
                db.SubmitChanges();
            }
            else if (userType == "doctor")
            {
                var selectUser = from item in db.tbl_doctors
                                 where item.d_email == userEmail
                                 select item;
                foreach (var ss in selectUser)
                {
                    ss.d_password_reset = null;
                }
                db.SubmitChanges();
            }
            else if (userType == "hospital")
            {
                var selectUser = from item in db.tbl_hospitalregs
                                 where item.h_regno == userEmail
                                 select item;
                foreach (var ss in selectUser)
                {
                    ss.h_password_reset = null;
                }
                db.SubmitChanges();
            }
            else if (userType == "hosdoctor")
            {
                var selectUser = from item in db.tbl_hdoctors
                                 where item.hd_email == userEmail && item.h_id == hregn
                                 select item;
                foreach (var ss in selectUser)
                {
                    ss.hd_password_reset = null;
                }
                db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void BtnCancel1_Click(object sender, EventArgs e)
    {
        DeletePasswordResetCode();
        CanelProcess();
    }

    private void CanelProcess()
    {
        if (userType == "user")
        {
            Response.Redirect("~/Index/SignInSignUp.aspx");
        }
        else if (userType == "doctor")
        {
            Response.Redirect("~/Index/Doctor login.aspx");
        }
        else if (userType == "hospital")
        {
            Response.Redirect("~/Index/Hospita Login.aspx");
        }
        else if (userType == "hosdoctor")
        {
            Response.Redirect("~/Index/HospitalDoctorLogin.aspx");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            //CancellSession();
            //if (Session["Language"].ToString() == "ar-EG")
            //{
            //    Session["Language"] = "Auto";
                Response.Redirect("PasswordReset.aspx");
            //}
            //else
            //{
            //    Session["Language"] = "ar-EG";

            //    Response.Redirect("PasswordReset.aspx?l=ar-EG");
            //}
        }
        catch (Exception ex)
        {

            //Response.Redirect("index.aspx");

        }

        //Session["Language"] = "ar-EG";

        //Response.Redirect("doctor login.aspx?l=ar-EG");
    }
}