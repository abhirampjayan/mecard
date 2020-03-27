using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

public partial class Index_userReg : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    SMS ob1 = new SMS();
    SqlCommand cmd;
    SqlDataReader dr, dr1, dr2;
    secure obj = new secure();

    protected override void InitializeCulture()
    {
        Session["Language"] = "Auto";
        string culture = "";
        try
        {
            culture = Request.QueryString["l"].ToString();
            Session["Language"] = culture;
        }
        catch (Exception ex)
        { }
        // string culture = Session["Language"].ToString();
        if (string.IsNullOrEmpty(culture))
        {
            culture = "Auto";
            Session["Language"] = culture;
        }
        //Use this
        UICulture = culture;
        Culture = culture;
        //OR This
        if (culture != "Auto")
        {

            System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
            System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
        }
        else
        {
            //LinkButton1.Text = "عربى";
        }

        base.InitializeCulture();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        BtnResendOTP.Enabled = true;
        if (Session["Language"].ToString() == "Auto")
        {
            LinkButton1.Text = "عربى";
        }
        else
        {
            LinkButton1.Text = "English";
        }

        con.Open();

        if (!IsPostBack)
        {
            SqlCommand com1 = new SqlCommand("delete from tbl_umail where email='" + TextBox1.Text + "'", con);
            com1.ExecuteNonQuery();
            SqlCommand com11 = new SqlCommand("delete from tbl_mailu where email='" + TextBox1.Text + "'", con);
            com11.ExecuteNonQuery();

        }
    }
    protected void Button21_Click(object sender, EventArgs e)
    {
        SqlCommand com1 = new SqlCommand("delete from tbl_umail where email='" + TextBox1.Text + "'", con);
        com1.ExecuteNonQuery();
        SqlCommand com11 = new SqlCommand("delete from tbl_mailu where email='" + TextBox1.Text + "'", con);
        com11.ExecuteNonQuery();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TxtOTP.Text = "";
        Label24.Text = "";
        string pass1 = textbox5.Text;
        textbox5.Attributes.Add("value", pass1);
        string pass2 = TextBox6.Text;
        TextBox6.Attributes.Add("value", pass2);
        //string code = countrycode.Value;
        if (ChkAgree.Checked)
        {
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_signup where contact='" + obj.EnryptString(TextBox7.Text) + "'", con);
                DataTable dtp = new DataTable();
                sda.Fill(dtp);
                if (dtp.Rows.Count > 0)
                {
                    if (Session["Language"].ToString() == "Auto")
                    {
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('phone number is already exist')</Script>");
                        TextBox7.Text = "";
                        TextBox7.Focus();
                    }
                    else
                    {
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم الهاتف موجود بالفعل')</Script>");
                        TextBox7.Text = "";
                        TextBox7.Focus();
                    }
                    

                }
                var Query = from item in db.tbl_signups where item.email == obj.EnryptString(TextBox1.Text) select item;
                if (Query.Count() > 0)
                {
                    if (Session["Language"].ToString() == "Auto")
                    {
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('User exist...! Choose another email id')</Script>");
                    }
                    else
                    {
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('المستخدم موجود ...! اختر معرف بريد إلكتروني آخر')</Script>");
                    }
                    //Label1.Text = "User exist...! Choose another email id";
                    //this.ModalPopupExtender2.Show();
                }
                else
                {

                    if (TextBox7.Text != "")
                    {

                        DateTime dob = DateTime.Parse(TextBox4.Text);
                        int CurrntAge = int.Parse(Age(dob));
                        if ((CurrntAge >= 18) && (CurrntAge <= 100))
                        {


                            SqlCommand com = new SqlCommand("select id from tbl_mailu where email='" + TextBox1.Text + "'", con);
                            int t;
                            try
                            {
                                t = Convert.ToInt32(com.ExecuteScalar());

                            }
                            catch (Exception ex)
                            {
                                t = 0;
                            }


                            if (t != 0)
                            {

                            }
                            else
                            { 

                                Random rd = new Random();
                                int i = rd.Next(000000, 999999);
                                HiddenField1.Value = i.ToString();
                                Session["uotp"] = i.ToString();
                                //  int age = new DateTime((DateTime.Now - Convert.ToDateTime(TextBox4.Text)).Ticks).Year;
                                //Session["name"] = TextBox3.Text;
                                Session["email"] = TextBox1.Text;
                                //Session["otp"] = i.ToString();
                                //Session["age"] = age.ToString();
                                //Session["pass"] = TextBox6.Text;
                                string pno = "";
                                pno = TextBox7.Text.Replace(" ", "");
                                string ph = "+966" + pno.ToString();
                                Session["phno"] = ph;

                                MailMessage mailmsg = new MailMessage();
                                mailmsg.mail(TextBox1.Text, "OTP for registering in Hakkeem is: " + i, "OTP from Hakkeem");

                                SqlCommand com1 = new SqlCommand("insert into tbl_mailu values('" + TextBox1.Text + "','1')", con);
                                com1.ExecuteNonQuery();

                                ob1.Message(ph.ToString(), "OTP for registering in Hakkeem is: " + i);
                                //this.ModalPopupExtender1.Show();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }

                        }
                        else
                        {
                            if (Session["Language"].ToString() == "Auto")
                            {
                                RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be atleast 18')</Script>");
                            }
                            else
                            {
                                RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك ما بين 18 سنة و 100 سنة')</Script>");

                            }
                            //Label2.Text = "Your age must be 18 years and above";
                            //this.ModalPopupExtender3.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            if (Session["Language"].ToString() == "Auto")
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please agree our terms and conditions')</Script>");
            }
            else
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى الموافقة على الشروط والأحكام')</Script>");

            }
            //Label3.Text = "Please agree our terms and conditions";
            //this.ModalPopupExtender4.Show();
        }

    }

    public static string Age(DateTime birthday)
    {
        DateTime now = DateTime.Today;
        int age = now.Year - birthday.Year;

        if (now < birthday.AddYears(age))
            age--;

        return age.ToString();

    }

    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        DateTime dob = DateTime.Parse(TextBox4.Text);
        int CurrntAge = int.Parse(Age(dob));
        if ((CurrntAge >= 18) && (CurrntAge <= 100))
        {
            TextBox4.Text = "";
            if (Session["Language"].ToString() == "Auto")
            {
                this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be atleast 18')</Script>");
            }
            else
            {
                this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك ما بين 18 سنة و 100 سنة')</Script>");

            }
        }
        else
        {
            DropDownList1.Focus();
        }
    }

    protected void LnkSIgnIn_Click(object sender, EventArgs e)
    {

    }

    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {
        SqlCommand com1 = new SqlCommand("delete from tbl_umail where email='" + TextBox1.Text + "'", con);
        com1.ExecuteNonQuery();
        SqlCommand com11 = new SqlCommand("delete from tbl_mailu where email='" + TextBox1.Text + "'", con);
        com11.ExecuteNonQuery();
        try
        {
            if (TxtOTP.Text == "")
            {
                
                Label24.Visible = true;


                if (Session["Language"].ToString() == "Auto")
                {

                    Label24.Text = "* Please enter OTP";
                }
                else
                {
                    Label24.Text = "* يرجى إدخال مكتب المدعي العام";
                }
            }
            else

            {
                var Query = from item in db.tbl_signups
                            where item.email == obj.EnryptString(TextBox1.Text)
                            select item;

                if (Query.Count() > 0)
                {
                    Response.Redirect("~/Index/SignInSignUp.aspx");
                }

                else
                {







                    Label24.Visible = false;
                    if (BtnSubmitOTP.Text == "Submit" || BtnSubmitOTP.Text == "عرض")
                    {

                        cmd = new SqlCommand("select max(id) as id from tbl_hakkimid where user_type='USR'", con);
                        Int64 id;
                        try
                        {

                            id = Convert.ToInt64(cmd.ExecuteScalar());

                        }
                        catch (Exception ex)
                        {
                            id = 0;
                        }

                        id = id + 1;
                        //txt_hakkimid.Text = "HU_000" + id.ToString();
                        string hid = "HU_000" + id.ToString();



                        string pno = "";
                        pno = TextBox7.Text.Replace(" ", "");
                        string ph = "+966" + pno.ToString();
                        BtnSubmitOTP.Enabled = false;
                        if (Session["Language"].ToString() == "Auto")
                        {
                            BtnSubmitOTP.Text = "Running process..";
                        }
                        else
                        {
                            BtnSubmitOTP.Text = "عملية التشغيل ..";
                        }

                        if (Session["uotp"].ToString() == TxtOTP.Text)
                        {

                            tbl_signup ob = new tbl_signup()
                            {
                                name = TextBox3.Text + " " + txtLastName.Text,
                                email = obj.EnryptString(TextBox1.Text),
                                dob = TextBox4.Text,
                                gender = DropDownList1.SelectedItem.Text,
                                password = obj.EnryptString(TextBox6.Text),

                                contact = obj.EnryptString(pno.ToString()),
                                age = Convert.ToInt64(ph.ToString()),
                                status = 1,
                                otp = Convert.ToInt64(HiddenField1.Value.ToString()),
                                u_hakkimid = hid.ToString(),

                            };
                            db.tbl_signups.InsertOnSubmit(ob);
                            db.SubmitChanges();

                            cmd = new SqlCommand("insert into tbl_hakkimid values('" + hid.ToString() + "','USR')", con);
                            cmd.ExecuteNonQuery();
                            String username = TextBox3.Text + " " + txtLastName.Text;
                            //// MailMessage mailmsgConfirm = new MailMessage();
                            //// mailmsgConfirm.mail(TextBox1.Text, "<p>Thank you for registering with Hakkeem Wish you better health.</p>" + "<p> Hakkeem id is " + hid.ToString() + "</p><p>Click the link to access Hakkeem " + "http://www.hakkeem.com" + "</p>", "Hakkeem registration");
                            Email_To_User(TextBox1.Text,username);
                             Session["phno"] = ph.ToString();
                            ob1.Message(ph.ToString(), "<p>Thank you for registering with Hakkeem Wish you better health.</p>" + "<p> Hakkeem id is " + hid.ToString() + "</p>");
                            //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Succesfully complete your registration. Now you can use Hakkeem ');window.location='/default.aspx'</Script>");
                            //Label4.Text = "Succesfully complete your registration. Thank you Hakkeem Team";

                            //this.ModalPopupExtender5.Show();
                            //Session["check"] = "1";
                            Session["user"] = TextBox1.Text;
                            Session["hakkemid_u"] = hid.ToString();
                            if (Session["Language"].ToString() == "Auto")
                            {
                                Response.Redirect("~/user/search.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/user/search.aspx?l=ar-EG");
                            }
                        }
                        else
                        {
                            //RegisterStartupScript("", "<Script Language=JavaScript>alert('You entered OTP is not valid...please check given email')</Script>");
                            //this.ModalPopupExtender1.Show();
                            Label24.Visible = true;
                            BtnSubmitOTP.Enabled = true;
                            if (Session["Language"].ToString() == "Auto")
                            {
                                Label24.Text = "Entered OTP is not valid...Check your Mail";
                            }
                            else
                            {
                                Label24.Text = "لقد أدخلت مكتب المدعي العام غير صالح ... يرجى التحقق من البريد الإلكتروني";
                            }
                            //this.ModalPopupExtender6.Show();
                        }
                        //BtnSubmitOTP.Enabled = true;
                        BtnSubmitOTP.Text = "Submit";


                    }



                }
            }
        }
        catch (Exception ex)
        {
            if (Session["Language"].ToString() == "Auto")
            {
                Label24.Text = "Something went wrong..!";
            }
            else
            {
                Label24.Text = "هناك خطأ ما..!";
            }
        }
    }

    public bool Email_To_User(string email, string Uname)
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
            //       String path = "images/mail/logo.png";
            //     path = ImageToBase64(path); 
            messagestr = messagestr + "<body style='background: #4aa9af'>";
            string path = "www.hakkeem.com/p.png";
            string btnpath = "http://www.hakkeem.com/Index/SignInSignUp.aspx";
            //   string msg= "<table background='"+path+"' width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto;padding:190px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
            string msg = "<table background='" + path + "' width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto;padding:117px;font-family:Arial,Tahoma,Verdana;font-size:12px;color:#7b7979;letter-spacing:0.02em;line-height:2em;'>";
            //     messagestr = messagestr + "<table background='p.png' width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto;padding:190px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
            messagestr = messagestr + msg;
            messagestr = messagestr + "<tbody><tr><td><table><br><br><br><br><br><br><br></table>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
            messagestr = messagestr + "<tbody><tr><td colspan='2' style='padding:35px 0;text-align:center'>Welcome to Hakkeem your account has been activated as a user.<br>Kindly log into your account and complete your profile to start  on Hakkeem. <br><br>";
            messagestr = messagestr + "<button type='submit' style='background: #d9534f;padding:8px;color:#fff;border-radius:20px;border:none;border-color:#d9534f 1px solid'><a href='" + btnpath + "' style='text-decoration:none;color:#fff;'>Start the journey</a> </button>";
            messagestr = messagestr + "</td></tr></tbody></table> </td> </tr></tbody></table>";
            messagestr = messagestr + "</body>";
            msgbody = messagestr.ToString();
            string mailBody = messagestr.ToString();
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("mail@goldenetqan.com", "Hakkeem", System.Text.Encoding.UTF8);
            mail.Subject = "Account Activation";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = messagestr.ToString();
            // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
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
    protected void BtnResendOTP_Click(object sender, EventArgs e)
    {
        SqlCommand com11 = new SqlCommand("delete from tbl_mailu where email='" + TextBox1.Text + "'", con);
        com11.ExecuteNonQuery();

        SqlCommand com = new SqlCommand("select id from tbl_umail where email='" + TextBox1.Text + "'", con);
        int t;
        try
        {
            t = Convert.ToInt32(com.ExecuteScalar());

        }
        catch (Exception ex)
        {
            t = 0;
        }


        if (t != 0)
        {
            if (Session["Language"].ToString() == "Auto")
            {
                Label24.Text = "OTP send to your Mail-Id...!";
            }
            else
            {
                Label24.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
            }
        }


        else
        {


            try
            {


                if (BtnResendOTP.Text == "Resend" || BtnResendOTP.Text == "إعادة إرسال")
                {
                    BtnResendOTP.Enabled = false;
                    // BtnSubmitOTP0.Text = "Sending...";

                    if (Session["Language"].ToString() == "Auto")
                    {
                        BtnResendOTP.Text = "Running process..";
                    }
                    else
                    {
                        BtnResendOTP.Text = "عملية التشغيل ..";
                    }

                    //    BtnSubmitOTP.Text = "Submit";
                    //BtnResendOTP.Text = "Running process..";
                    //BtnResendOTP.Enabled = false;
                    Label24.Text = "";
                    TxtOTP.Text = "";
                    var Query = from item in db.tbl_signups
                                where item.email == TextBox1.Text
                                select item;
                    if (Query.Count() > 0)
                    {
                        //RegisterStartupScript("", "<Script Language=JavaScript>swal('You are already registered with this mail id.')</Script>");
                        if (Session["Language"].ToString() == "Auto")
                        {
                            Label24.Text = "You are already registered with this mail id";
                        }
                        else
                        {
                            Label24.Text = "أنت مسجل بالفعل مع هذا الرقم البريدي";
                        }
                        //this.ModalPopupExtender7.Show();
                    }
                    else
                    {

                        SqlCommand com1 = new SqlCommand("insert into tbl_umail values('" + TextBox1.Text + "','1')", con);
                        com1.ExecuteNonQuery();


                        Random rd = new Random();
                        int i = rd.Next(000000, 999999);
                        Session["uotp"] = i.ToString();
                        string pno = "";
                        pno = TextBox7.Text.Replace(" ", "");
                        string ph = "+966" + pno.ToString();
                        MailMessage mailmsg = new MailMessage();
                        mailmsg.mail(TextBox1.Text, "OTP for registering in Hakkeem is: " + Session["uotp"].ToString(), "OTP from Hakkeem");
                        ob1.Message(ph.ToString(), "OTP for registering in Hakkeem is: " + Session["uotp"].ToString());
                        //this.ModalPopupExtender1.Show();
                        if (Session["Language"].ToString() == "Auto")
                        {
                            Label24.Text = "OTP send to your Mail-Id...!";
                        }
                        else
                        {
                            Label24.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
                        }
                    }
                    BtnResendOTP.Enabled = true;
                    BtnResendOTP.Text = "Resend";
                }
            }
            catch (Exception ex)
            {
                if (Session["Language"].ToString() == "Auto")
                {
                    Label24.Text = "Something went wrong..!";
                }
                else
                {
                    Label24.Text = "هناك خطأ ما..!";
                }
                BtnResendOTP.Enabled = true;
                BtnResendOTP.Text = "Resend";
            }
        }
    }


    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_signups
                    where item.email == Session["email"].ToString()
                    select item;
        if (Query.Count() > 0)
        {

            Session["user"] = Session["email"].ToString();
            foreach (var ss in Query)
            {
                Session["hakkemid_u"] = ss.u_hakkimid.ToString();

            }
            if (Session["Language"].ToString() == "Auto")
            {
                Response.Redirect("~/User/Search.aspx");
            }
            else
            {
                Response.Redirect("~/User/Search.aspx?l=ar-EG");
            }
        }
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        TxtOTP.Text = "";
        //this.ModalPopupExtender1.Show();
    }

    protected void TxtOTP_TextChanged(object sender, EventArgs e)
    {
        //if (Session["otp"].ToString() == TxtOTP.Text)
        //{
        //    string pno = "";
        //    pno = TextBox7.Text.Replace(" ", "");
        //    tbl_signup ob = new tbl_signup()
        //    {
        //        name = TextBox3.Text + " " + txtLastName.Text,
        //        email = TextBox1.Text,
        //        dob = TextBox4.Text,
        //        gender = DropDownList1.SelectedItem.Text,
        //        password = Session["pass"].ToString(),
        //        contact = "+966" + pno.ToString(),
        //        age = Convert.ToInt64(Session["age"].ToString()),
        //        status = 1,
        //        otp = Convert.ToInt64(Session["otp"].ToString()),
        //        u_hakkimid = Session["hakkeemid"].ToString(),

        //    };
        //    db.tbl_signups.InsertOnSubmit(ob);
        //    db.SubmitChanges();

        //    cmd = new SqlCommand("insert into tbl_hakkimid values('" + Session["hakkeemid"].ToString() + "','USR')", con);
        //    cmd.ExecuteNonQuery();

        //    MailMessage mailmsgConfirm = new MailMessage();
        //    mailmsgConfirm.mail(TextBox1.Text, "Your account is successfully created with Hakkeem and your HAKKEEM ID is " + Session["hakkeemid"].ToString(), "User Registration");

        //    //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Succesfully complete your registration. Now you can use Hakkeem ');window.location='/Hakkeem/Index.aspx'</Script>");
        //    Label4.Text = "Succesfully complete your registration. Thank you Hakkeem Team";

        //    this.ModalPopupExtender5.Show();
        //}
        //else
        //{
        //    //RegisterStartupScript("", "<Script Language=JavaScript>alert('You entered OTP is not valid...please check given email')</Script>");
        //    //this.ModalPopupExtender1.Show();
        //    Label5.Text = "You entered OTP is not valid...please check given email";
        //    this.ModalPopupExtender6.Show();
        //}
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Language"].ToString() == "Auto")
            {
                LinkButton1.Text = "English";
                Session["Language"] = "ar-EG";
                Response.Redirect(Request.Path + "?l=ar-EG");

            }
            else
            {
                Session["Language"] = "Auto";
                LinkButton1.Text = "عربى";
                Response.Redirect(Request.Path);

            }
        }
        catch (Exception ex)
        {
            //LinkButton1.Text = "English";

        }
    }

   
}