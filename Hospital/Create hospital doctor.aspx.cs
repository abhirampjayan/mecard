using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

public partial class Hospital_Create_hospital_doctor : System.Web.UI.Page
{
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    SMS ob = new SMS();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
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
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
        if (!IsPostBack)
        {
            try
            {
                CheckLocation();
                Fillcity();
            }
            catch (Exception ex)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("../index/hospita login.aspx");
                //}
                //else
                //{
                //    Response.Redirect("../index/hospita login.aspx?l=ar-EG");
                //}
            }
            LoadSpecialities();
        }
    }
    protected void Fillcity()
    {
        con.Open();
        SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_cities", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DdlCity.DataSource = dt;
            DdlCity.DataTextField = "City";
            DdlCity.DataValueField = "id";
            DdlCity.DataBind();
        }
        con.Close();
    }

    public void LoadSpecialities()
    {
        try
        {
            var query = from item in db.tbl_specialities
                        select item;
            if (query.Count() > 0)
            {
                DropDownList1.DataSource = query;
                DropDownList1.DataTextField = "Specialities";
                DropDownList1.DataValueField = "id";
                DropDownList1.DataBind();
            }

        }
        catch (Exception ex)
        {
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
            //Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب تعيين موقعك')</Script>");
            //}
        }


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
        string hcity = "";
        string hname = "";
        var hs = from item in db.tbl_hospitalregs where item.h_hakkimid == Session["hakkeemid_h"].ToString() select item;
        foreach (var h in hs)
        {
            hcity = h.h_city;
            hname = h.h_name;
        }
        int f = 0;
       
        var Queryy = from item in db.tbl_hdoctors where item.hd_email == email.Text  select item;
        if (Queryy.Count() > 0)
        {
            f = 1;
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctors Email Id already exist')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('معرف البريد الإلكتروني للأطباء موجود من قبل')</Script>");
            //}
        }
        SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_hdoctor where hd_contact='" + phone.Text + "'", con);
        DataTable dtp = new DataTable();
        sda.Fill(dtp);
        if (dtp.Rows.Count > 0)
        {
            f = 1;
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('phone number is already exist')</Script>");
                phone.Text = "";
                phone.Focus();
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم الهاتف موجود بالفعل')</Script>");
            //    phone.Text = "";
            //    phone.Focus();
            //}


        }

        var Query = from item in db.tbl_hdoctors where item.hd_email == email.Text && item.hd_id_number == dnumber.Text select item;
            if (Query.Count() > 0)
            {
            f = 1;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor exist')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('طبيب موجود')</Script>");
                //}
                //Label1.Text = "Doctor exist";
                //this.ModalPopupExtender1.Show();
            }
              var Query11 = from item in db.tbl_hdoctors where item.hd_id_number == dnumber.Text select item;
                if (Query11.Count() > 0)
                {
                     f = 1;
                    //Label1.Text = "Doctor identification number exist";
                    //this.ModalPopupExtender1.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number exist')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم تعريف الطبيب موجود')</Script>");

                    //}
                }
               
                    DateTime currentDate = DateTime.Now;
                    DateTime compareDate = Convert.ToDateTime(this.dexpire.Text.Trim(), new CultureInfo("en-GB"));
                    if (currentDate >= compareDate)
                    {
            f = 1;
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number is expired...!')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('!..انتهت صلاحية رقم تعريف الطبيب')</Script>");
                        //}
                        //Label1.Text = "Doctor identification number is expired";
                        //this.ModalPopupExtender1.Show();
                    }
                   if(f==0)
                    {

                        if ((DateTime.Parse(compareDate.ToShortDateString()) - DateTime.Parse(DateTime.Now.ToShortDateString())).TotalDays < 30)
                        {
                            double i = (DateTime.Parse(compareDate.ToShortDateString()) - DateTime.Parse(DateTime.Now.ToShortDateString())).TotalDays;
                            if (i == 30)
                            {
                                Random rd = new Random();
                                int pswd = rd.Next(000000, 999999);



                                tbl_hdoctor td = new tbl_hdoctor()
                                {
                                    hd_name = Fname.Text + " " + Lname.Text,
                                    hd_email = email.Text,
                                    hd_contact =  phone.Text,
                                    hd_specialties = DropDownList1.SelectedItem.Text,
                                    hd_location = DdlCity.SelectedItem.Text,
                                    hd_id_number = dnumber.Text,
                                    hd_status = 1,
                                    hd_date_time = DateTime.Now.ToString(),
                                    hd_password = pswd.ToString(),
                                    //d_city = dcity.Text,
                                    hd_id_expire = dexpire.Text,
                                    h_id = Session["hakkeemid_h"].ToString(),
                                    hd_city = hcity.ToString(),
                                    h_name = hname,
                                };
                                db.tbl_hdoctors.InsertOnSubmit(td);
                                db.SubmitChanges();
                                string message = "";
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                     message = "Doctor identification number is expired with in " + i + " days...!";
                                //}
                                //else
                                //{
                                //    message = " أيام" + i + "انتهت صلاحية رقم تعريف الطبيب مع "  ;
                                //}

                    //   string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.hakkeem.com/Index/HospitalDoctorLogin.aspx" + " and your loging password is " + pswd +" and Hospital Hakkeem Id is "+ Session["hakkeemid_h"].ToString();
                    //   Email.mail(email.Text, msg, "Doctor registration");
                    String hakid = Session["hakkeemid_h"].ToString();
                    Email_To_HospitalDoctor(email.Text, hakid, pswd.ToString());
                    string ph = "+966" + phone.Text;

                    ob.Message(ph, " Thank you for registering with Hakkeem.Hospital Hakkeem ID: " + hakid + " and password is:" + pswd.ToString());

                    string ph1 = "+91" + phone.Text;

                    ob.Message(ph1, " Thank you for registering with Hakkeem.Hospital Hakkeem ID: " + hakid + " and password is:" + pswd.ToString());


                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "')</Script>");
                    //if (Session["Language"].ToString() == "Auto")
                    //            {
                    Label1.Text = "Successfully created doctor and " + message;
                                //}
                                //else
                                //{
                                //    Label1.Text =  message+ "تم إنشاء الطبيب بنجاح ";
                                //}
                                //this.ModalPopupExtender1.Show();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                               Fname.Text = "";
                               Lname.Text = "";
                               email.Text = "";
                               phone.Text = "";
                               DropDownList1.SelectedItem.Text = "";
                               DdlCity.SelectedItem.Text = "";
                               dnumber.Text = "";
                               dexpire.Text = "";
                }
                            else
                            {
                                Random rd = new Random();
                                int pswd = rd.Next(000000, 999999);
                                tbl_hdoctor td = new tbl_hdoctor()
                                {
                                    hd_name = Fname.Text + " " + Lname.Text,
                                    hd_email = email.Text,
                                    hd_contact =  phone.Text,
                                    hd_specialties = DropDownList1.SelectedItem.Text,
                                    hd_location = DdlCity.SelectedItem.Text,
                                    hd_id_number = dnumber.Text,
                                    hd_status = 1,
                                    hd_date_time = DateTime.Now.ToString(),
                                    hd_password = pswd.ToString(),
                                    //d_city = dcity.Text,
                                    hd_id_expire = dexpire.Text,
                                    h_id = Session["hakkeemid_h"].ToString(),
                                    hd_city = hcity,
                                    h_name = hname,
                                };
                                db.tbl_hdoctors.InsertOnSubmit(td);
                                db.SubmitChanges();
                                string message = "";
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                     message = "Doctor identification number is expired with in " + i + " days...!";
                                //}
                                //else
                                //{
                                //    message = " أيام" + i + "انتهت صلاحية رقم تعريف الطبيب مع ";
                                //}
                    /////  string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.hakkeem.com/Index/HospitalDoctorLogin.aspx" + " and your loging password is " + pswd + " and Hospital Hakkeem Id is " + Session["hakkeemid_h"].ToString();
                    /// Email.mail(email.Text, msg, "Doctor registration");
                    String hakid = Session["hakkeemid_h"].ToString();
                    Email_To_HospitalDoctor(email.Text, hakid, pswd.ToString());
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "')</Script>");
                    string ph = "+966" + phone.Text;

                    ob.Message(ph, " Thank you for registering with Hakkeem.Hospital Hakkeem ID: " + hakid + " and password is:" + pswd.ToString());
                    string ph1 = "+91" + phone.Text;

                    ob.Message(ph1, " Thank you for registering with Hakkeem.Hospital Hakkeem ID: " + hakid + " and password is:" + pswd.ToString());


                    //if (Session["Language"].ToString() == "Auto")
                    //            {
                    Label1.Text = "Successfully created doctor and " + message;
                                //}
                                //else
                                //{
                                //    Label1.Text = message + "تم إنشاء الطبيب بنجاح ";
                                //}
                                //this.ModalPopupExtender1.Show();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                                Fname.Text = "";
                                Lname.Text = "";
                                email.Text = "";
                                phone.Text = "";
                                DropDownList1.SelectedItem.Text = "";
                                DdlCity.SelectedItem.Text = "";
                                dnumber.Text = "";
                                dexpire.Text = "";
                }

                        }
                        else
                        {
                            Random rd = new Random();
                            int pswd = rd.Next(000000, 999999);
                            tbl_hdoctor td = new tbl_hdoctor()
                            {
                                hd_name = Fname.Text + " " + Lname.Text,
                                hd_email = email.Text,
                                hd_contact =  phone.Text,
                                hd_specialties = DropDownList1.SelectedItem.Text,
                                hd_location = DdlCity.SelectedItem.Text,
                                hd_id_number = dnumber.Text,
                                hd_status = 1,
                                hd_date_time = DateTime.Now.ToString(),
                                hd_password = pswd.ToString(),
                                //hd_city = dcity.Text,
                                hd_id_expire = dexpire.Text,
                                h_id = Session["hakkeemid_h"].ToString(),
                                hd_city = hcity,
                                h_name = hname,
                            };
                            db.tbl_hdoctors.InsertOnSubmit(td);
                            db.SubmitChanges();
                //string message = "Doctor identification number is expired with in " + i + " days...!";
                ///// string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.hakkeem.com/Index/HospitalDoctorLogin.aspx" + " and your loging password is " + pswd + " and Hospital Hakkeem Id is " + Session["hakkeemid_h"].ToString();
                ////   Email.mail(email.Text, msg, "Doctor registration");
                String hakid = Session["hakkeemid_h"].ToString();
                Email_To_HospitalDoctor(email.Text, hakid, pswd.ToString());
                string ph = "+966" + phone.Text;

                ob.Message(ph, " Thank you for registering with Hakkeem.Hospital Hakkeem ID: " + hakid + " and password is:" + pswd.ToString());

                string ph1 = "+91" + phone.Text;

                ob.Message(ph1, " Thank you for registering with Hakkeem.Hospital Hakkeem ID: " + hakid + " and password is:" + pswd.ToString());
                //if (Session["Language"].ToString() == "Auto")
                //            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully created doctor')</Script>");
                            //}
                            //else
                            //{
                            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم إنشاء الطبيب بنجاح')</Script>");

                            //}
                //Label1.Text = "Successfully created doctor ";
                //this.ModalPopupExtender1.Show();
                              Fname.Text = "";
                              Lname.Text = "";
                              email.Text = "";
                              phone.Text = "";
                              DropDownList1.SelectedItem.Text = "";
                              DdlCity.SelectedItem.Text = "";
                              dnumber.Text = "";
                              dexpire.Text = "";
                        }



                    }

                

            
        

    }
    public bool Email_To_HospitalDoctor(string email, string hakkeemid, string password)
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
            string set = "http://www.hakkeem.com/seturavail.png";
            string btnpath = "http://hakkeem.com/Index/HospitalDoctorLogin";
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
            messagestr = messagestr + "<td  colspan='2'  style='padding:20px 20px;background-color:#fff;line-height:3.5em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%' ></td>";

            messagestr = messagestr + "</tr>";
            messagestr = messagestr + "<tr><td style='text-align:center;font-size:20px;padding:20px 20px;background-color: #fff;color:#4aa9af;font-weight:bold'>WELCOME TO HAKKEEM</td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px; color:#4aa9af;'>";
            messagestr = messagestr + "We're so happy you've joined Hakkeem family.<br><br> We founded Hakkeem because we wanted to create a trustworthy and inspiring platform for you to help arrange your timing and availability in an easy way,also to help you increasing your income.<br> Also we would like to Thank you for joining us and helping us to enchance the would.<br><strong> Here is Hospital Hakkeem ID:" + hakkeemid + " and Password is: " + password + "</strong><br> Your sincerely,<br> Hakkeem Team.";
            messagestr = messagestr + " </td></tr><tr><td style='text-align:center;padding:20px 20px;background-color: #fff;text-align:center'>";
            messagestr = messagestr + " <a href='" + btnpath + "'><img src='" + set + "' height='40px'></a>";

            messagestr = messagestr + "</td></tr><tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
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
            mail.Subject = "Account Activation";
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
    //public bool Email_To_HospitalDoctor(string email, string hakkeemid, string password)
    //{
    //    string cmpnyemail = "";
    //    string number = "";
    //    bool flag = true;
    //    string msgsubject, msgbody;

    //    MailMessage message = new MailMessage();


    //    int id;
    //    try
    //    {

    //        //con.Open();



    //        MailMessage msgMail = new MailMessage();
    //        MailMessage myMessage = new MailMessage();
    //        StringBuilder sb = new StringBuilder();
    //        StringBuilder sbtitle = new StringBuilder();
    //        String messagestr = "";
    //        string path = "www.hakkeem.com/register.png";
    //        string follw = "www.hakkeem.com/followus.png";
    //        string face = "www.hakkeem.com/facebook-logo-button.png";
    //        string twitter = "www.hakkeem.com/twitter-logo-button.png";
    //        string insta = "www.hakkeem.com/instagram-logo.png";
    //        string btnpath = "http://hakkeem.com/Index/HospitalDoctorLogin.aspx";
    //        string contact = "http://hakkeem.com/ContactUs.aspx";
    //        messagestr = messagestr + "<body style='background-color:#e9e9e9'>";
    //        string msg = "<table background='" + path + "' width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto;background-repeat:no-repeat;padding:109px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
    //        messagestr = messagestr + msg;
    //        messagestr = messagestr + "<tbody><tr><td><tr><td>";
    //        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:120px;'>";
    //        messagestr = messagestr + "<tbody><tr><td width='100%' style='font-size:15px;color:#4aa9af;font-family:sans-serif'>";
    //        messagestr = messagestr + "We're so happy you've joined Hakkeem family.<br><br> We founded Hakkeem because we wanted to create a trustworthy and inspiring platform for you to help arrange your timing and availability in an easy way, also to help you increasing your income.";
    //        messagestr = messagestr + "<br><br> Also we would like to Thank you for joining us and helping us to enchance the would.</tr></tbody></table></td></tr>";
    //        messagestr = messagestr + "<tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
    //        messagestr = messagestr + "<tbody><tr><td width='100%' style='font-size:15px;color:#4aa9af;font-weight:bold;font-family:sans-serif'>";
    //        messagestr = messagestr + "Here is Hospital Hakkeem ID:" + hakkeemid + " and Password is: " + password + "";
    //        messagestr = messagestr + "</td></tr></tbody></table></td></tr><tr><td>";
    //        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
    //        messagestr = messagestr + "<tbody><tr><td width='100%' style='font-size:15px;color:#4aa9af;font-family: sans-serif'>";
    //        messagestr = messagestr + "Your sincerely,<br>Hakkeem Team.</td></tr></tbody></table></td></tr><tr><td>";
    //        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0'style='padding-top:10px'>";
    //        messagestr = messagestr + "<tbody><tr><td width='100%' style='text-align:center'>";
    //        messagestr = messagestr + "<a href='" + btnpath + "'style='font-size:15px;font-family:sans-serif;text-decoration:none'><button style='background-color:#4aa9af;border:none;border-radius:20px;padding:8px'><span style='color:#fff'> visit us</span></button></a>";
    //        messagestr = messagestr + "</td></tr></tbody></table></td></tr><tr><td>";
    //        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
    //        messagestr = messagestr + "<tbody><tr><td style='text-align:left;'><span style='color:#4aa9af'><a href='#' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy </a>&nbsp;| &nbsp;<a href='" + contact + "' style ='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
    //        messagestr = messagestr + "</span></td><td style='text-align:right;'><img src='" + follw + "'>&nbsp;";
    //        messagestr = messagestr + "<a href='https://www.facebook.com/hakkeem.etqan.1' style='text-decoration:none'><img src='" + face + "' width='20px' height='20px' title='Facbook'>&nbsp;</a>";
    //        messagestr = messagestr + "<a href='https://twitter.com/Hakkeem_1' style='text-decoration:none'><img src='" + twitter + "' width='20px' height='20px' title='Twitter'>&nbsp;</a>";
    //        messagestr = messagestr + "<a href='https://www.instagram.com/hakkeem_1/' style='text-decoration:none'><img src='" + insta + "' width='20px' height='20px' title='Instagram'>&nbsp;</a>";
    //        messagestr = messagestr + "</td></tr></tbody></table></td></tr><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:25px'>";
    //        messagestr = messagestr + "<tbody><tr><td width='100%' style='color:#4aa9af;font-family:sans-serif;font-size:10px'>";
    //        messagestr = messagestr + "<span style='font-weight:bold;font-size:11px'> Discliamer</span> : Please do not print this email unless it is necessary. Every unprinted email helps the environment.";
    //        messagestr = messagestr + "</td></tr></tbody></table></td></tr></td></tr></tbody></table></body>";
    //        msgbody = messagestr.ToString();
    //        string mailBody = messagestr.ToString();
    //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //        mail.To.Add(email);
    //        mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
    //        mail.Subject = "Account Activation";
    //        mail.SubjectEncoding = System.Text.Encoding.UTF8;
    //        mail.Body = messagestr.ToString();
    //        // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
    //        mail.BodyEncoding = System.Text.Encoding.UTF8;
    //        mail.IsBodyHtml = true;
    //        mail.Priority = MailPriority.High;
    //        SmtpClient client = new SmtpClient();
    //        client.Credentials = new System.Net.NetworkCredential("mail@hakkeem.com", "Hakkeem2018!!");
    //        client.Port = 25;
    //        client.Host = "smtp.goldenetqan.com";
    //        client.EnableSsl = false;
    //        try
    //        {
    //            client.Send(mail);

    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        }
    //        return flag;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        }
    //        throw ex;
    //    }
    //}

    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/Create hospital doctor.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/Create hospital doctor.aspx?l=ar-EG");
        //}
    }
}