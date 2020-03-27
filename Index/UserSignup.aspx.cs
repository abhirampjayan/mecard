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
        //Session["Language"] = "Auto";
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
        BtnResendOTP.Enabled = true;
        //if (Session["Language"].ToString() == "Auto")
        //{
        //    LinkButton1.Text = "عربى";
        //}
        //else
        //{
        //    LinkButton1.Text = "English";
        //}

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
         //   Session["eeu"] = TextBox1.Text;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_signup where contact='" + obj.EnryptString(TextBox7.Text) + "'", con);
                DataTable dtp = new DataTable();
                sda.Fill(dtp);
                if (dtp.Rows.Count > 0)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('phone number is already exist')</Script>");
                        TextBox7.Text = "";
                        TextBox7.Focus();   
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم الهاتف موجود بالفعل')</Script>");
                    //    TextBox7.Text = "";
                    //    TextBox7.Focus();
                    //}
                    

                }
                string eemail = TextBox1.Text.ToLower();
                var Query = from item in db.tbl_signups where item.email == obj.EnryptString(eemail.ToString()) || item.email == obj.EnryptString(TextBox1.Text) select item;
                if (Query.Count() > 0)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('User exist...! Choose another email id')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المستخدم موجود ...! اختر معرف بريد إلكتروني آخر')</Script>");
                    //}
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
                                Session["phno"] = pno;
                                ob1.Message(ph.ToString(), "OTP for registering in Hakkeem is : " + i);
                                string ph1 = "+91" + pno.ToString();
                                ob1.Message(ph1.ToString(), "OTP for registering in Hakkeem is : " + i);

                                MailMessage mailmsg = new MailMessage();
                                // mailmsg.mail(TextBox1.Text, "OTP for registering in Hakkeem is: " + i, "OTP from Hakkeem");
                                Email_To_UserOtp(TextBox1.Text, i.ToString());
                                if(con.State.ToString()=="Closed")
                                {
                                    con.Open();
                                }
                                 SqlCommand com1 = new SqlCommand("insert into tbl_mailu values('" + TextBox1.Text + "','1')", con);
                                com1.ExecuteNonQuery();

                          
                                //this.ModalPopupExtender1.Show();
                              
                            }
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                        else
                        {
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be atleast 18')</Script>");
                            //}
                            //else
                            //{
                            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك ما بين 18 سنة و 100 سنة')</Script>");

                            //}
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
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please agree our terms and conditions')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى الموافقة على الشروط والأحكام')</Script>");

            //}
            //Label3.Text = "Please agree our terms and conditions";
            //this.ModalPopupExtender4.Show();
        }

    }
    
    public bool Email_To_UserOtp(string email, string otp)
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
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>OTP</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "OTP for registering in Hakkeem is:<br><span style='font-size:20px'><strong>" + otp + "</strong></span>";
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
            mail.Subject = "Hakkeem OTP";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = messagestr.ToString();
            // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("tahcomdev@gmail.com", "shiva123*");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
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
            //if (Session["Language"].ToString() == "Auto")
            //{
                this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be atleast 18')</Script>");
            //}
            //else
            //{
            //    this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك ما بين 18 سنة و 100 سنة')</Script>");

            //}
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


                //if (Session["Language"].ToString() == "Auto")
                //{

                    Label24.Text = "* Please enter OTP";
                //}
                //else
                //{
                //    Label24.Text = "* يرجى إدخال مكتب المدعي العام";
                //}
            }
            else

            {
                string eemail = TextBox1.Text.ToLower();

                var Query = from item in db.tbl_signups
                            where item.email == obj.EnryptString(TextBox1.Text) || item.email == obj.EnryptString(eemail.ToString())
                            select item;

                if (Query.Count() > 0)
                {
                    Response.Redirect("~/Index/SignInSignUp.aspx");
                }

                else
                {







                    Label24.Visible = false;
                    if (BtnSubmitOTP.Text == "Submit")
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
                      //   string ph = "+966" + pno.ToString();

                        string ph = "";
                        if (pno.StartsWith("5") == true)
                        {
                            ph = "+966" + pno.ToString();

                        }
                        else
                        {
                            ph = "+91" + pno.ToString();
                        }




                        //  string ph = "+918086194845";
                        BtnSubmitOTP.Enabled = false;
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            BtnSubmitOTP.Text = "Resend";
                        //}
                        //else
                        //{
                        //    BtnSubmitOTP.Text = "عملية التشغيل ..";
                        //}
                      
                        if (true)
                        {

                            tbl_signup ob = new tbl_signup()
                            {
                                name = TextBox3.Text + " " + txtLastName.Text,
                                email = obj.EnryptString(eemail),
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
                            Email_To_User(TextBox1.Text,hid.ToString());
                             Session["phno"] = ph.ToString();
                            ob1.Message(ph.ToString(), "Thank you for registering with Hakkeem Wish you better health. Your Hakkeem ID : " + hid.ToString() );
                            //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Succesfully complete your registration. Now you can use Hakkeem ');window.location='/default.aspx'</Script>");
                            //Label4.Text = "Succesfully complete your registration. Thank you Hakkeem Team";

                            //this.ModalPopupExtender5.Show();
                            //Session["check"] = "1";
                            Session["user"] = eemail;
                            Session["hakkemid_u"] = hid.ToString();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                Response.Redirect("~/user/search.aspx");
                            //}
                            //else
                            //{
                            //    Response.Redirect("~/user/search.aspx?l=ar-EG");
                            //}
                        }
                        else
                        {
                            //RegisterStartupScript("", "<Script Language=JavaScript>alert('You entered OTP is not valid...please check given email')</Script>");
                            //this.ModalPopupExtender1.Show();
                            Label24.Visible = true;
                            BtnSubmitOTP.Enabled = true;
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                Label24.Text = "Entered OTP is not valid...Check your Mail";
                            //}
                            //else
                            //{
                            //    Label24.Text = "لقد أدخلت مكتب المدعي العام غير صالح ... يرجى التحقق من البريد الإلكتروني";
                            //}
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
            //if (Session["Language"].ToString() == "Auto")
            //{
                Label24.Text = "Something went wrong..!";
            //}
            //else
            //{
            //    Label24.Text = "هناك خطأ ما..!";
            //}
        }
    }

    public bool Email_To_User(string email, string hakkeemid)
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
            string set = "http://www.hakkeem.com/setpng.png";
            string btnpath = "http://hakkeem.com/Index/SignInSignUp.aspx";
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
            messagestr = messagestr + "<td  colspan='2'  style='padding:20px 20px;background-color:#fff;line-height:3.5em;font-size:12px;color:#4aa9af;text-align:center'><img src='"+bg+"' width='100%' ></td>";

            messagestr = messagestr + "</tr>";
            messagestr = messagestr + "<tr><td style='text-align:center;font-size:20px;padding:20px 20px;background-color: #fff;color:#4aa9af;font-weight:bold'>WELCOME TO HAKKEEM</td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px; color:#4aa9af;'>";
            messagestr = messagestr + "We're so happy you've joined Hakkeem family.<br><br> We founded Hakkeem because we wanted to create a trustworthy and inspiring platform for you to help arrange your timing and availability in an easy way,also to help you increasing your income.<br> Also we would like to Thank you for joining us and helping us to enchance the would.<br><strong> Here is your HakkeemID:"+hakkeemid+"</strong><br> Your sincerely,<br> Hakkeem Team.";
            messagestr = messagestr + " </td></tr><tr><td style='text-align:center;padding:20px 20px;background-color: #fff;text-align:center'>";
            messagestr = messagestr + " <a href='"+btnpath+"'><img src='"+set+"' height='35px'></a>";

            messagestr = messagestr + "</td></tr><tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
            messagestr = messagestr + " <tbody><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<span style='color:#4aa9af'><a href='"+privacy+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='"+contact+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
            messagestr = messagestr + "</span></td></tr><br><br><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<img src='"+follw+"'>";
            messagestr = messagestr + "<a href='https://www.facebook.com/hakkeem.etqan.1' style='text-decoration:none'><img src='"+face+"' title='Facbook'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://twitter.com/Hakkeem_1' style='text-decoration:none'><img src='"+twitter+"'  title='Twitter'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://www.instagram.com/hakkeem_1/' style='text-decoration:none'><img src='"+insta+"'  title='Instagram'>&nbsp;</a>";
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
            //if (Session["Language"].ToString() == "Auto")
            //{
                Label24.Text = "OTP send to your Mail-Id...!";
            //}
            //else
            //{
            //    Label24.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
            //}
        }


        else
        {


            try
            {


                if (BtnResendOTP.Text == "Resend" || BtnResendOTP.Text == "إعادة إرسال")
                {
                   
                    // BtnSubmitOTP0.Text = "Sending...";

                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        BtnResendOTP.Text = "Resend";
                    //}
                    //else
                    //{
                    //    BtnResendOTP.Text = "عملية التشغيل ..";
                    //}

                    //    BtnSubmitOTP.Text = "Submit";
                    //BtnResendOTP.Text = "Resend";
                    //BtnResendOTP.Enabled = false;
                    Label24.Text = "";
                    TxtOTP.Text = "";
                    var Query = from item in db.tbl_signups
                                where item.email == TextBox1.Text
                                select item;
                    if (Query.Count() > 0)
                    {
                        //RegisterStartupScript("", "<Script Language=JavaScript>swal('You are already registered with this mail id.')</Script>");
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            Label24.Text = "You are already registered with this mail id";
                        //}
                        //else
                        //{
                        //    Label24.Text = "أنت مسجل بالفعل مع هذا الرقم البريدي";
                        //}
                        //this.ModalPopupExtender7.Show();
                    }
                    else
                    {

                        SqlCommand com1 = new SqlCommand("insert into tbl_umail values('" + TextBox1.Text + "','1')", con);
                        com1.ExecuteNonQuery();
                        BtnResendOTP.Enabled = false;

                        Random rd = new Random();
                        int i = rd.Next(000000, 999999);
                        Session["uotp"] = i.ToString();
                        string pno = "";
                        pno = TextBox7.Text.Replace(" ", "");
                        string ph = "+966" + pno.ToString();
                        ob1.Message(ph.ToString(), "OTP for registering in Hakkeem is : " + Session["uotp"].ToString());
                        string ph1 = "+91" + pno.ToString();
                        ob1.Message(ph1.ToString(), "OTP for registering in Hakkeem is : " + Session["uotp"].ToString());
                        MailMessage mailmsg = new MailMessage();
                        Email_To_UserOtp(TextBox1.Text, i.ToString());
                        //  mailmsg.mail(TextBox1.Text, "OTP for registering in Hakkeem is: " + Session["uotp"].ToString(), "OTP from Hakkeem");
                       
                        //this.ModalPopupExtender1.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                        Label24.Visible = true;
                            Label24.Text = "OTP send to your Mail-Id...!";
                        //}
                        //else
                        //{
                        //    Label24.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
                        //}
                    }
                    //BtnResendOTP.Enabled = true;
                    //BtnResendOTP.Text = "Resend";
                }
            }
            catch (Exception ex)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Label24.Text = "Something went wrong..!";
                //}
                //else
                //{
                //    Label24.Text = "هناك خطأ ما..!";
                //}
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
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/User/Search.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/User/Search.aspx?l=ar-EG");
            //}
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
            //if (Session["Language"].ToString() == "Auto")
            //{
            //    LinkButton1.Text = "English";
            //    Session["Language"] = "ar-EG";
            //    Response.Redirect(Request.Path + "?l=ar-EG");

            //}
            //else
            //{
            //    Session["Language"] = "Auto";
            //    LinkButton1.Text = "عربى";
            //    Response.Redirect(Request.Path);

            //}
        }
        catch (Exception ex)
        {
            //LinkButton1.Text = "English";

        }
    }

   
}