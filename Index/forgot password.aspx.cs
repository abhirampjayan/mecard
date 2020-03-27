﻿using System;
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
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    string userType = "";
    secure obj = new secure();
    SMS ob = new SMS();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

    SqlCommand cmd, com;
    public string h_regn = "";
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
        //if (Session["Language"].ToString() == "Auto")
        //{
        //    LinkButton1.Text = "عربى";
        //}
        //else
        //{
            LinkButton1.Text = "English";
        //}

        con.Open();
        userType = Request.QueryString["usertype"];

        if (!IsPostBack)
        {
            if (userType == "hospital")
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    LblHeading.InnerText = "Please enter your hospital registration number to search for your account";
                    TxtEmail.Attributes.Add("placeholder", "Enter hospital registration number");
                //}
                //else
                //{
                //    LblHeading.InnerText = "يرجى إدخال رقم تسجيل المستشفى للبحث عن حسابك";
                //    TxtEmail.Attributes.Add("placeholder", "أدخل رقم تسجيل المستشفى");
                //}

            }
            else if (userType == "hosdoctor")
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    LblHeading.InnerText = "Please enter your hospital hakkeem id and email to search for your account";
                    TxtHosRegnNo.Attributes.Add("placeholder", "Enter hospital hakkeem id");
                    TxtEmail.Attributes.Add("placeholder", "Enter email id");
                //}
                //else
                //{
                //    LblHeading.InnerText = "يرجى إدخال معرف حكيم المستشفى والبريد الإلكتروني للبحث عن حسابك";
                //    TxtHosRegnNo.Attributes.Add("placeholder", "أدخل المستشفى حكيم إد");
                //    TxtEmail.Attributes.Add("placeholder", "أدخل معرف البريد الإلكتروني");
                //}
                RequiredFieldValidator2.Enabled = true;
                TxtHosRegnNo.Enabled = true;
                TxtHosRegnNo.Visible = true;
            }
            else if (userType == "user")
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    TxtEmail.Attributes.Add("placeholder", "Enter email id");
                //}
                //else
                //{
                //    TxtEmail.Attributes.Add("placeholder", "أدخل معرف البريد الإلكتروني");
                //}
            }
            else if (userType == "doctor")
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    TxtEmail.Attributes.Add("placeholder", "Enter email id");
                //}
                //else
                //{
                //    TxtEmail.Attributes.Add("placeholder", "أدخل معرف البريد الإلكتروني");
                //}
            }
        }
    }



    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        CancelProcess();
    }

    private void CancelProcess()
    {
        if (userType == "user")
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/SignInSignUp.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/SignInSignUp.aspx?l=ar-EG");
            //}
        }
        else if (userType == "doctor")
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/Doctor login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/Doctor login.aspx?l=ar-EG");
            //}
        }
        else if (userType == "hospital")
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/Hospita Login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/Hospita Login.aspx?l=ar-EG");
            //}
        }
        else if (userType == "hosdoctor")
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/HospitalDoctorLogin.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/HospitalDoctorLogin.aspx?l=ar-EG");
            //}
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        PnlUserFailed.Visible = false;

        try
        {
            if (userType == "user")
            {
                var selctUser = from item in db.tbl_signups
                                where item.email == obj.EnryptString(TxtEmail.Text) || item.contact == obj.EnryptString(TxtEmail.Text)
                                select item;
                if (selctUser.Count() > 0)
                {
                    PnlFindAcnt.Visible = false;
                    PnlRecovryOptions.Visible = true;
                    foreach (var ss in selctUser)
                    {
                        LblEmail.Text = obj.DecryptString(ss.email);
                        if (ss.contact != null)
                        {
                            LblText.Text = obj.DecryptString(ss.contact);
                        }
                        else
                        {
                            RdbReset1.Enabled = false;
                            LblText.Visible = false;
                        }
                        LblUName.Text = ss.name;
                    }
                    RdbReset.Focus();
                }
                else
                {
                    TxtEmail.Text = "";
                    PnlUserFailed.Visible = true;
                }
            }
            else if (userType == "doctor")
            {
                var selctUser = from item in db.tbl_doctors
                                where item.d_email == obj.EnryptString(TxtEmail.Text) || item.d_contact == obj.EnryptString(TxtEmail.Text)
                                select item;
                if (selctUser.Count() > 0)
                {
                    PnlFindAcnt.Visible = false;
                    PnlRecovryOptions.Visible = true;
                    foreach (var ss in selctUser)
                    {
                        LblEmail.Text = obj.DecryptString(ss.d_email);
                        if (ss.d_contact != null)
                        {
                            LblText.Text = obj.DecryptString(ss.d_contact);
                        }
                        else
                        {
                            RdbReset1.Enabled = false;
                            LblText.Visible = false;
                        }
                        LblUName.Text = "Dr." + " " + ss.d_name;
                    }
                    RdbReset.Focus();
                }
                else
                {
                    TxtEmail.Text = "";
                    PnlUserFailed.Visible = true;
                }
            }
            else if (userType == "hospital")
            {
                var selctUser = from item in db.tbl_hospitalregs
                                where item.h_regno == TxtEmail.Text
                                select item;
                if (selctUser.Count() > 0)
                {
                    PnlFindAcnt.Visible = false;
                    PnlRecovryOptions.Visible = true;
                    foreach (var ss in selctUser)
                    {
                        LblEmail.Text = ss.h_email;
                        if (ss.h_contact != null)
                        {
                            LblText.Text = ss.h_contact;
                        }
                        else
                        {
                            RdbReset1.Enabled = false;
                            LblText.Visible = false;
                        }
                        LblUName.Text = ss.h_name;
                    }
                    RdbReset.Focus();
                }
                else
                {
                    TxtEmail.Text = "";
                    PnlUserFailed.Visible = true;
                }
            }
            else if (userType == "hosdoctor")
            {
                var selctUser = from item in db.tbl_hdoctors
                                where item.h_id == TxtHosRegnNo.Text && item.hd_email == TxtEmail.Text
                                select item;
                if (selctUser.Count() > 0)
                {
                    PnlFindAcnt.Visible = false;
                    PnlRecovryOptions.Visible = true;
                    foreach (var ss in selctUser)
                    {
                        LblEmail.Text = ss.hd_email;
                        if (ss.hd_contact != null)
                        {
                            LblText.Text = ss.hd_contact;
                        }
                        else
                        {
                            RdbReset1.Enabled = false;
                            LblText.Visible = false;
                        }
                        LblUName.Text = "Dr." + " " + ss.hd_name;
                    }
                    RdbReset.Focus();
                }
                else
                {
                    TxtEmail.Text = "";
                    TxtHosRegnNo.Text = "";
                    PnlUserFailed.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void BtnContinue_Click(object sender, EventArgs e)
    {

        if (RdbReset.Checked == true)
        {
            PnlNoSelect.Visible = false;
            Random rd = new Random();
            int i = rd.Next(000000, 999999);

            try
            {
                if (userType == "user")
                {
                    var query = from item in db.tbl_signups
                                where item.email == obj.EnryptString(LblEmail.Text)
                                select item;
                    foreach (var ss in query)
                    {
                        ss.passwordreset = obj.EnryptString(i.ToString());
                        db.SubmitChanges();
                    }
                    //  Email.mail(LblEmail.Text, "Hi" + " " + LblUName.Text + "," + " " + i.ToString() + " " + "This is your password reset code.", "Password recovery");

                    Email_To_forgotpassword(LblEmail.Text, i.ToString());
                    if (con.State.ToString() == "Closed")
                    {
                        con.Open();
                    }
                    com = new SqlCommand("update tbl_signup set passwordreset='" + obj.EnryptString(i.ToString()) + "',password='" + obj.EnryptString(i.ToString()) + "' where email='" + obj.EnryptString(LblEmail.Text) + "' ", con);
                    com.ExecuteNonQuery();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/Index/PasswordReset.aspx?type=user&email=" + obj.EnryptString(LblEmail.Text) + "");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Index/PasswordReset.aspx?l=ar-EG&type=user&email=" + obj.EnryptString(LblEmail.Text) + "");
                    //}
                }
                else if (userType == "doctor")
                {
                    var query = from item in db.tbl_doctors
                                where item.d_email == obj.DecryptString(LblEmail.Text)
                                select item;
                    foreach (var ss in query)
                    {
                        ss.d_password_reset = obj.EnryptString(i.ToString());
                    }
                    db.SubmitChanges();
                    // Email.mail(LblEmail.Text, "Hi" + " " + LblUName.Text + "," + " " + i.ToString() + " " + "This is your password reset code.", "Password recovery");

                    Email_To_forgotpassword(LblEmail.Text, i.ToString());
                    if (con.State.ToString() == "Closed")
                    {
                        con.Open();
                    }
                    com = new SqlCommand("update tbl_doctor set d_password_reset='" + obj.EnryptString(i.ToString()) + "',d_password='" + obj.EnryptString(i.ToString()) + "' where d_email='" + obj.EnryptString(LblEmail.Text) + "' ", con);
                    com.ExecuteNonQuery();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/Index/PasswordReset.aspx?type=doctor&email=" + obj.EnryptString(LblEmail.Text) + "");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Index/PasswordReset.aspx?l=ar-EG&type=doctor&email=" + obj.EnryptString(LblEmail.Text) + "");
                    //}
                }
                else if (userType == "hospital")
                {
                    string email = "";
                    var query = from item in db.tbl_hospitalregs
                                where item.h_regno == TxtEmail.Text
                                select item;
                    foreach (var ss in query)
                    {
                        ss.h_password_reset = i.ToString();
                        email = ss.h_email;
                    }
                    db.SubmitChanges();
                    //  Email.mail(email, "Your request for reset account password." + " " + i.ToString() + " " + "This is your password reset code.", "Password recovery");
                    Email_To_forgotpassword(LblEmail.Text, i.ToString());
                    if (con.State.ToString() == "Closed")
                    {
                        con.Open();
                    }
                    com = new SqlCommand("update tbl_hospitalreg set h_password_reset='" + obj.EnryptString(i.ToString()) + "',h_password='" + obj.EnryptString(i.ToString()) + "' where h_regno='" + TxtEmail.Text + "' ", con);
                    com.ExecuteNonQuery();


                    Label LblH_Regn = new Label();
                    LblH_Regn.Text = this.h_regn;
                    string queryString = "";
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        queryString = "~/Index/PasswordReset.aspx?type=hospital&email=" + obj.EnryptString(email);
                    //}
                    //else
                    //{
                    //    queryString = "~/Index/PasswordReset.aspx?l=ar-EG&type=hospital&email=" + obj.EnryptString(email);
                    //}
                    Response.Redirect(queryString);
                }
                else if (userType == "hosdoctor")
                {
                    var query = from item in db.tbl_hdoctors
                                where item.hd_email == TxtEmail.Text && item.h_id == TxtHosRegnNo.Text
                                select item;
                    foreach (var ss in query)
                    {
                        ss.hd_password_reset = i.ToString();
                    }
                    db.SubmitChanges();
                    string queryString = "";
                    Email_To_forgotpassword(LblEmail.Text, i.ToString());
                    //Email.mail(TxtEmail.Text, "Your request for reset account password." + " " + i.ToString() + " " + "This is your password reset code.", "Password recovery");
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        queryString = "~/Index/PasswordReset.aspx?type=hosdoctor&email=" + obj.EnryptString(TxtEmail.Text) + "&hregn=" + TxtHosRegnNo.Text;
                    //}
                    //else
                    //{
                    //    queryString = "~/Index/PasswordReset.aspx?l=ar-EG&type=hosdoctor&email=" + obj.EnryptString(TxtEmail.Text) + "&hregn=" + TxtHosRegnNo.Text;

                    //}
                    Response.Redirect(queryString);
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex);
            }
        }
        else if (RdbReset1.Checked == true)
        {
            PnlNoSelect.Visible = false;
            Random rd = new Random();
            int i = rd.Next(000000, 999999);
            string ph = "";
            try
            {
                if (userType == "user")
                {
                    var query = from item in db.tbl_signups
                                where item.email == obj.EnryptString(LblEmail.Text)
                                select item;
                    foreach (var ss in query)
                    {
                        ph = obj.DecryptString(ss.contact);
                        ss.passwordreset = obj.EnryptString(i.ToString());
                        db.SubmitChanges();
                    }
                    //Email.mail(LblEmail.Text, "Hi" + " " + LblUName.Text + "," + " " + i.ToString() + " " + "This is your password reset code.", "Password recovery");
                    try
                    {
                        ob.Message(ph, "Hi" + " " + LblUName.Text + "," + " " + i.ToString() + " " + "This is your password reset code.");
                    }
                    catch (Exception ex) { }
                    com = new SqlCommand("update tbl_signup set passwordreset='" + obj.EnryptString(i.ToString()) + "',password='" + obj.EnryptString(i.ToString()) + "' where email='" + obj.EnryptString(LblEmail.Text) + "' ", con);
                    com.ExecuteNonQuery();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/Index/PasswordReset.aspx?type=user&email=" + obj.EnryptString(LblEmail.Text) + "");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Index/PasswordReset.aspx?l=ar-EG&type=user&email=" + obj.EnryptString(LblEmail.Text) + "");
                    //}
                }
                else if (userType == "doctor")
                {
                    var query = from item in db.tbl_doctors
                                where item.d_email == obj.DecryptString(LblEmail.Text)
                                select item;
                    foreach (var ss in query)
                    {
                        ph = obj.DecryptString(ss.d_contact);
                        ss.d_password_reset = obj.EnryptString(i.ToString());
                    }
                    db.SubmitChanges();
                    //Email.mail(LblEmail.Text, "Hi" + " " + LblUName.Text + "," + " " + i.ToString() + " " + "This is your password reset code.", "Password recovery");
                    try
                    {
                        ob.Message(ph, "Hi" + " " + LblUName.Text + "," + " " + i.ToString() + " " + "This is your password reset code.");
                    }
                    catch (Exception ex) { }
                    com = new SqlCommand("update tbl_doctor set d_password_reset='" + obj.EnryptString(i.ToString()) + "',d_password='" + obj.EnryptString(i.ToString()) + "' where d_email='" + obj.EnryptString(LblEmail.Text) + "' ", con);
                    com.ExecuteNonQuery();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/Index/PasswordReset.aspx?type=doctor&email=" + obj.EnryptString(LblEmail.Text) + "");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Index/PasswordReset.aspx?l=ar-EG&type=doctor&email=" + obj.EnryptString(LblEmail.Text) + "");
                    //}
                }
                else if (userType == "hospital")
                {
                    string email = "";
                    var query = from item in db.tbl_hospitalregs
                                where item.h_regno == TxtEmail.Text
                                select item;
                    foreach (var ss in query)
                    {
                        ss.h_password_reset = i.ToString();
                        email = ss.h_email;
                        ph = ss.h_contact;
                    }
                    db.SubmitChanges();
                    //Email.mail(email, "Your request for reset account password." + " " + i.ToString() + " " + "This is your password reset code.", "Password recovery");
                    try
                    {
                        ob.Message(ph, "Your request for reset  password." + " " + i.ToString() + " " + "This is your password reset code.");
                    }
                    catch (Exception ex) { }
                    com = new SqlCommand("update tbl_hospitalreg set h_password_reset='" + obj.EnryptString(i.ToString()) + "',h_password='" + obj.EnryptString(i.ToString()) + "' where h_regno='" + TxtEmail.Text + "' ", con);
                    com.ExecuteNonQuery();


                    Label LblH_Regn = new Label();
                    LblH_Regn.Text = this.h_regn;
                    string queryString = "";
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        queryString = "~/Index/PasswordReset.aspx?type=hospital&email=" + obj.EnryptString(email);
                    //}
                    //else
                    //{
                    //    queryString = "~/Index/PasswordReset.aspx?l=ar-EG&type=hospital&email=" + obj.EnryptString(email);
                    //}
                    Response.Redirect(queryString);
                }
                else if (userType == "hosdoctor")
                {
                    var query = from item in db.tbl_hdoctors
                                where item.hd_email == TxtEmail.Text && item.h_id == TxtHosRegnNo.Text
                                select item;
                    foreach (var ss in query)
                    {
                        ss.hd_password_reset = i.ToString();
                        ph = ss.hd_contact;
                    }
                    db.SubmitChanges();
                    string queryString = "";
                    //Email.mail(TxtEmail.Text, "Your request for reset account password." + " " + i.ToString() + " " + "This is your password reset code.", "Password recovery");
                    try
                    {
                        ob.Message(ph, "Your request for reset  password." + " " + i.ToString() + " " + "This is your password reset code.");
                    }
                    catch (Exception ex) { }
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        queryString = "~/Index/PasswordReset.aspx?type=hosdoctor&email=" + obj.EnryptString(TxtEmail.Text) + "&hregn=" + TxtHosRegnNo.Text;
                    //}
                    //else
                    //{
                    //    queryString = "~/Index/PasswordReset.aspx?l=ar-EG&type=hosdoctor&email=" + obj.EnryptString(TxtEmail.Text) + "&hregn=" + TxtHosRegnNo.Text;

                    //}
                    Response.Redirect(queryString);
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex);
            }
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('please choose your Email Id or Contact Number')</Script>");
        }
        //else if(RdbReset1.Checked==true)
        //{
        //    PnlNoSelect.Visible = false;
        //}
        //else
        //{
        //    PnlNoSelect.Visible = true;
        //}
    }
    public bool Email_To_forgotpassword(string email, string code)
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
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>FORGOT PASSWORD</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "This is your password reset code:<br><span style='font-size:20px'><strong>" + code + "</strong></span>";
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
            mail.Subject = "Hakkeem Forgot Password";
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
    protected void BtnCancel1_Click(object sender, EventArgs e)
    {
        CancelProcess();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            //CancellSession();
            //if (Session["Language"].ToString() == "ar-EG")
            //{
            //    Session["Language"] = "Auto";
                Response.Redirect("forgot password.aspx");
            //}
            //else
            //{
            //    Session["Language"] = "ar-EG";

            //    Response.Redirect("forgot password.aspx?l=ar-EG");
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