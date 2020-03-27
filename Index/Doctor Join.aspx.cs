using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class Index_JoinDoctor : System.Web.UI.Page
{
    MailMessage msg = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    SMS ob = new SMS();
    SqlCommand cmd;
    SqlDataReader dr, dr1, dr2;
    secure obj = new secure();
    //string password = "";
   public int f = 0;
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

    //protected void Page_UnLoad(object sender, EventArgs e)
    //{
    //    SqlCommand com1 = new SqlCommand("delete from tbl_mail where email='" + email.Text + "'", con);
    //    com1.ExecuteNonQuery();
    //    SqlCommand com11 = new SqlCommand("delete from tbl_maild where email='" + email.Text + "'", con);
    //    com11.ExecuteNonQuery();
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
      
        con.Open();
      

        BtnSubmitOTP0.Enabled = true;

        //if (Session["Language"].ToString() == "Auto")
        //{
            BtnSubmitOTP0.Text = "Resend";
        //}
        //else
        //{
        //    BtnSubmitOTP0.Text = "إعادة إرسال";
        //}

        //if (Session["Language"].ToString()=="Auto")
        //{
        //    LinkButton1.Text = "عربى";
        //}
        //else
        //{
            LinkButton1.Text = "English";
        //}
        if (!IsPostBack)
        {
            SqlCommand com1 = new SqlCommand("delete from tbl_mail where email='" + email.Text + "'", con);
            com1.ExecuteNonQuery();
            SqlCommand com11 = new SqlCommand("delete from tbl_maild where email='" + email.Text + "'", con);
            com11.ExecuteNonQuery();
            Label31.Text = "";
            LoadSpecialities();
            LoadCity();
        }
        //if (!IsPostBack)
        //{
        //    SqlCommand com1 = new SqlCommand("delete from tbl_mail where email='" + email.Text + "'", con);
        //    com1.ExecuteNonQuery();

        //    SqlCommand com11 = new SqlCommand("delete from tbl_maild where email='" + email.Text + "'", con);
        //    com11.ExecuteNonQuery();
           

        //}



    }

    protected void LoadCity()
    {
        try
        {
            //var query = from item in db.tbl_cities
            //            select item ;
            //if (query.Count() > 0)
            //{
            //    city.DataSource = query;
            //    city.DataTextField = "City";
            //    city.DataValueField = "id";
            //    city.DataBind();
            //    city.Items.Insert(0,"choose your city");
            //}

           
           SqlDataAdapter adpt=new SqlDataAdapter("select * from tbl_cities order by city", con);
            DataSet dts = new DataSet();
            dts.Clear();
            adpt.Fill(dts);

            city.DataSource = dts;
            city.DataTextField = "City";
            city.DataValueField = "id";
            city.DataBind();
            city.Items.Insert(0, "choose your city");

        }
        catch (Exception ex)
        {
        }
    }

    public void LoadSpecialities()
    {
        try
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from tbl_specialities order by Specialities", con);
            DataSet dts = new DataSet();
            dts.Clear();
            adpt.Fill(dts);
            //var query = from item in db.tbl_specialities
            //            select item;
            //if (query.Count() > 0)
            //{
                specialty.DataSource = dts;
                specialty.DataTextField = "Specialities";
                specialty.DataValueField = "id";
                specialty.DataBind();
          //  }
            specialty.Items.Insert(0, "Choose your Speciality");

        }
        catch (Exception ex)
        {
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TxtOTP.Text = "";
        Label31.Text = "";
        string pass1 = TxtPassword.Text;
        TxtPassword.Attributes.Add("value", pass1);
        string pass2 = TxtConfirmPass.Text;
        TxtConfirmPass.Attributes.Add("value", pass2);
        if (ChkTerms.Checked)
        {
            if (Session["Language"].ToString() == "Auto")
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_cities where City='" + city.SelectedItem.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('please choose valid city')</Script>");
                    city.Text = "";
                    city.Focus();
                }

                SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_specialities where Specialities='" + specialty.Text + "'", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {

                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('please choose valid Specialities')</Script>");
                  //  specialty.SelectedItem.Text = "";
                    specialty.Focus();
                    
                }
            string eemail = email.Text.ToLower();
            var Query = from item in db.tbl_doctors where item.d_email == obj.EnryptString(eemail) select item;
                if (Query.Count() > 0)
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor already exist')</Script>");
                    email.Text = "";
                    //Label1.Text = "Doctor already exist...!";
                    //this.ModalPopupExtender2.Show();s
                }
                SqlDataAdapter sdap = new SqlDataAdapter("Select * from tbl_doctor where d_contact='" + obj.EnryptString(phoneno.Text) + "'", con);
                DataTable dtp = new DataTable();
                sdap.Fill(dtp);
                if (dtp.Rows.Count > 0)
                {

                    RegisterStartupScript("", "<Script Language=JavaScript>swal('phone number is already exist')</Script>");
                    phoneno.Text = "";
                    phoneno.Focus();

                }
            }
            //else
            //{
            //    SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_cities where City='" + city.SelectedItem.Text + "'", con);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {

            //    }
            //    else
            //    {
            //        RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء اختيار مدينة صالحة')</Script>");
            //        city.Text = "";
            //        city.Focus();

            //    }
            //    SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_specialities where Specialities='" + specialty.SelectedItem.Text + "'", con);
            //    DataTable dt1 = new DataTable();
            //    sda1.Fill(dt1);
            //    if (dt1.Rows.Count > 0)
            //    {

            //    }
            //    else
            //    {
            //        RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء اختيار التخصصات الصحيحة')</Script>");
            //        specialty.SelectedItem.Text = "";
            //        specialty.Focus();
            //    }
            //    var Query = from item in db.tbl_doctors where item.d_email == obj.EnryptString(email.Text) select item;
            //    if (Query.Count() > 0)
            //    {
            //        RegisterStartupScript("", "<Script Language=JavaScript>swal('طبيب موجود بالفعل')</Script>");
            //        email.Text = "";
            //        //Label1.Text = "Doctor already exist...!";
            //        //this.ModalPopupExtender2.Show();
            //    }
            //    SqlDataAdapter sdap = new SqlDataAdapter("Select * from tbl_doctor where d_contact='" + obj.EnryptString(phoneno.Text) + "'", con);
            //    DataTable dtp = new DataTable();
            //    sdap.Fill(dtp);
            //    if (dtp.Rows.Count > 0)
            //    {

            //        RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم الهاتف موجود بالفعل')</Script>");
            //        phoneno.Text = "";
            //        phoneno.Focus();

            //    }

            //}
            if (city.Text!="" && specialty.Text!=""&&email.Text!=""&&phoneno.Text!="")
            {


               // Session["ee"] = email.Text;

                SqlCommand com = new SqlCommand("select id from tbl_maild where email='" + email.Text + "'", con);
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
                    //    Label31.Text = "OTP send to your Mail-Id...!";
                    //}
                    //else
                    //{
                    //    Label31.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
                    //}
                }


                else
                {








                    Random rd = new Random();
                    int i = rd.Next(000000, 999999);
                    //string pg = "";
                    //if (drpPostGraduation.SelectedIndex > 0)
                    //{
                    //    pg = drpPostGraduation.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    pg = "";
                    //}
                    ////  Session["pg"] = pg.ToString();
                    //ViewState["pg"] = pg.ToString();
                    // Session["email"] = email.Text;

                    //password = TxtConfirmPass.Text;

                    HiddenField1.Value = i.ToString();
                    Session["otp"] = i.ToString();
                    SqlCommand com1 = new SqlCommand("insert into tbl_maild values('" + email.Text + "','1')", con);
                    com1.ExecuteNonQuery();

                    string pno = "";
                    pno = phoneno.Text.Replace(" ", "");
                    string ph = "+966" + pno.ToString();
                    string ph1 = "+91" + pno.ToString();
                    //  Session["phno"] = ph;
                    //string ph = "+919539395281";
                 
                    try
                    {
                        ob.Message(ph, "OTP for registering in Hakkeem is : " + i);
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        ob.Message(ph1, "OTP for registering in Hakkeem is : " + i);
                    }
                    catch (Exception ex)
                    {

                    }
                    //  msg.mail(email.Text, "OTP for registering in Hakkeem is: " + i, "OTP from Hakkeem");
                    Email_To_DoctorOtp(email.Text, i.ToString());
                   
                   
                    // ob.Message(ph1, "OTP for registering in Hakkeem is : " + i);
                    //this.ModalPopupExtender1.Show();
                    //   Timer1.Enabled = true;

                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please agree terms and conditions.')</Script>");
            //Label2.Text = "Please agree terms and conditions...!";
            //this.ModalPopupExtender3.Show();

        }

    }

    public void Emailhakkimid(string email, string msg)
    {
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(email);
        mail.From = new MailAddress("bookdoc2017@gmail.com", "Hakkeem", System.Text.Encoding.UTF8);
        mail.Subject = "Account creation";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = msg;
        //mail.Attachment = "attachment path";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("bookdoc2017@gmail.com", "bookdoc12345");
        client.Port = 25;
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
    public bool Email_To_Doctor(string email, string hakkeemid)
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
            string btnpath = "http://hakkeem.com/Index/Doctor%20login.aspx";
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
            messagestr = messagestr + "We're so happy you've joined Hakkeem family.<br><br> We founded Hakkeem because we wanted to create a trustworthy and inspiring platform for you to help arrange your timing and availability in an easy way,also to help you increasing your income.<br> Also we would like to Thank you for joining us and helping us to enchance the would.<br><strong> Here is your HakkeemID:" + hakkeemid + "</strong><br> Your sincerely,<br> Hakkeem Team.";
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
    public void Email(string email, string msg)
    {
        using (System.IO.StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                long i = 0;
                var selectId = from item in db.tbl_hospitalregs
                               select item.h_id;
                if (selectId.Count() > 0)
                {
                    i = selectId.Max() + 1;
                }
                else
                {
                    i = 1;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>AGREMENT FORM</b></td></tr>");

                sb.Append("<tr><td colspan = '2'></td></tr>");
                sb.Append("<tr><td><b>Company Name:</b>");
                sb.Append("Hakkeem");
                sb.Append("</td><td><b>Id: </b>");
                sb.Append(i.ToString());
                sb.Append(" </td></tr>");

                sb.Append("<tr><td><b>Doctor Name :</b> ");
                sb.Append(Fname.Text + " " + Lname.Text);
                sb.Append("</td><td><b>Doctor Address: </b>");
                sb.Append("sdhvbsvfvfkfnncbd<br>");
                sb.Append("</td></tr>");

                sb.Append("<tr><td colspan = '2'></td></tr>");

                sb.Append("<tr><td colspan = '2' align='center'><p>ydacauycgygygcyucvcyucvyuacyays</p> <p>ydacauycgygygcyucvcyucvyuacyays</p> <p>ydacauycgygygcyucvcyucvyuacyays</p> <p>ydacauycgygygcyucvcyucvyuacyays</p> </td></tr>");
                sb.Append("<tr><td><b>Signature with office seal</b>");
                sb.Append("</td><td><b>Date: </b>");
                sb.Append(DateTime.Now.ToShortDateString());
                sb.Append(" </td></tr>");
                sb.Append("<tr><td><b>..............</b>");
                sb.Append("</td><td><b>Place: Trivandrum </b></td></tr>");
                sb.Append("</table>");
                sb.Append("<br />");

                StringReader sr = new StringReader(sb.ToString());

                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();


                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.To.Add(email);
                    mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
                    mail.Subject = "Hakkeem registration";
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = msg;
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
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
            }
        }
    }
    string a = "10";
    protected void Button21_Click(object sender, EventArgs e)
    {
        //SqlCommand com1 = new SqlCommand("delete from tbl_mail where email='" + email.Text + "'", con);
        //com1.ExecuteNonQuery();
        //SqlCommand com11 = new SqlCommand("delete from tbl_maild where email='" + email.Text + "'", con);
        //com11.ExecuteNonQuery();



        SqlCommand com1 = new SqlCommand("delete from tbl_mail where email='" + email.Text + "'", con);
        com1.ExecuteNonQuery();
        SqlCommand com11 = new SqlCommand("delete from tbl_maild where email='" + email.Text + "'", con);
        com11.ExecuteNonQuery();
    }




    private static DateTime ConvertToEngCal(string hijri)
    {
        CultureInfo arSA = new CultureInfo("ar-SA");
        arSA.DateTimeFormat.Calendar = new HijriCalendar();
        return DateTime.ParseExact(hijri, "dd/MM/yy", arSA);
    }
    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {
        SqlCommand com1 = new SqlCommand("delete from tbl_maild where email='" + email.Text + "'", con);
        com1.ExecuteNonQuery();
        SqlCommand com11 = new SqlCommand("delete from tbl_mail where email='" + email.Text + "'", con);
        com11.ExecuteNonQuery();
        f = 0;

        if (TxtOTP.Text == "")
        {
            Label31.Visible = true;
            //if (Session["Language"].ToString() == "Auto")
            //{

                Label31.Text = "* Please enter OTP";
            //}
            //else
            //{
            //    Label31.Text = "* يرجى إدخال مكتب المدعي العام";
            //}
        }
        else
        {


        var Query = from item in db.tbl_doctors
                    where item.d_email == obj.EnryptString(email.Text)
                    select item;

        if (Query.Count() > 0)
        {
            Response.Redirect("~/Index/Doctor login.aspx");
        }

        else
        {

            if (BtnSubmitOTP.Text == "Submit" )
            {
                BtnSubmitOTP.Enabled = true;




                    //if (Session["otp"].ToString() == TxtOTP.Text)
                    if (true)
                    {
                    

                    cmd = new SqlCommand("select max(id) as id from tbl_hakkimid where user_type='DOC'", con);
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
                    //txt_hakkimid.Text = "HD_000" + id.ToString();
                    string hid = "HD_000" + id.ToString();

                    string pno = "";
                    pno = phoneno.Text.Replace(" ", "");
                        string ph = "";
                        if (pno.StartsWith("5") == true)
                        {
                            ph = "+966" + pno.ToString();

                        }
                        else
                        {
                            ph = "+91" + pno.ToString();
                        }


                         
                       // string ph = "+918086194845";
                        string pg = "";
                    if (drpPostGraduation.SelectedIndex > 0)
                    {
                        pg = drpPostGraduation.SelectedItem.Text;
                    }
                    else
                    {
                        pg = "";
                    }
                    string ug = "";
                    if (drpGraduation.SelectedIndex > 0)
                    {
                        ug = drpGraduation.SelectedItem.Text;
                    }
                    else
                    {
                        ug = "";
                    }
                        string eemail = email.Text.ToLower();

                        string dd = System.DateTime.Now.ToString();
                        string[] dd1 = new string[7];
                        string dttime = "";
                        try
                        {
                            dd1 = dd.Split('-');
                            string[] dd2 = new string[7];
                            dd2 = dd1[2].Split(' ');

                            //    Response.Write("<br>" + dd2[0]);
                            dttime = System.DateTime.Now.ToString();

                            if (dd2[0] == "2018" || dd2[0] == "2019" || dd2[0] == "2020")
                            {

                            }
                            else
                            {
                                dttime = ConvertToEngCal(dttime).ToString("dd-MM-yyyy");


                            }
                        }
                        catch (Exception ex)
                        {
                            dd1 = dd.Split('/');
                            string[] dd2 = new string[7];
                            dd2 = dd1[2].Split(' ');

                            //    Response.Write("<br>" + dd2[0]);
                             dttime = System.DateTime.Now.ToString();

                            if (dd2[0] == "2018" || dd2[0] == "2019" || dd2[0] == "2020")
                            {

                            }
                            else
                            {
                                dttime = ConvertToEngCal(dttime).ToString("dd-MM-yyyy");


                            }
                        }
                        tbl_doctor td = new tbl_doctor()
                    {

                        d_email = obj.EnryptString(eemail.ToString()),
                        d_name = Fname.Text + " " + Lname.Text,
                        //  d_specialties = specialty.SelectedItem.Text,
                        d_specialties = specialty.SelectedItem.Text,
                        d_location = city.SelectedItem.Text,
                        d_sex = DropDownList1.SelectedItem.Text,


                        d_contact = obj.EnryptString(pno),
                        d_status = 0,
                        d_date_time = dttime.ToString(),
                        d_password = obj.EnryptString(TxtConfirmPass.Text),
                        //d_password = password,
                        //d_city = city.SelectedItem.Text,
                        d_education = ug + " " + pg.ToString(),
                        d_otp = Convert.ToInt64(TxtOTP.Text),
                        d_hakkimid = hid.ToString(),
                        //d_photo = "../Doctorimages/doctor.png",
                    };
                    db.tbl_doctors.InsertOnSubmit(td);
                    db.SubmitChanges();




                    Session["name"] = Fname.Text + " " + Lname.Text;
                    cmd = new SqlCommand("insert into tbl_hakkimid values('" + hid.ToString() + "','DOC')", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into tbl_rating values('0','0','0','0','','" + hid.ToString() + "','0')", con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("insert into tbl_ratefinal values('" + hid.ToString() + "','0')", con);
                    cmd.ExecuteNonQuery();
                        //msg.mail(email.Text, "Your account is successfully created with Hakkeem , your HAKKEEM ID is. " + hid, "Account Creation");
                        ///// string m = "<p>Thank you for registering with Hakkeem</p>" + "<p>" + "Your hakkeem id is: " + hid + "</p><p>" + "Click the link to access Hakkeem " + "http://www.hakkeem.com" + " and upload signed agreement.</p>";

                        ///// Email(email.Text, m);
                        Email_To_Doctor(email.Text, hid);
                        try
                    {
                        // ph = Session["phno"].ToString();
                        string pm = "Thank you for registering with Hakkeem Your hakkeem id is : " + hid ;

                        ob.Message(ph, pm);
                    }
                    catch (Exception ex)
                    {
                    }

                    //Emailhakkimid(email.Text, "Your account is successfully created with Hakkeem , your HAKKEEM ID is. " + Session["hakkimid"]);
                    //this.Page.RegisterStartupScript("", "<Script Language=JavaScript>alert('Thank you for registering with Hakkeem. Please check your mail and upload the signed agreement. We will contact you with in one business day.');window.location='../../default.aspx'</Script>");
                    //Label3.Text = "Thank you for registering with Hakkeem.Please check your mail and upload the signed agreement.We will contact you with in one business day.";
                    Session["doctor"] = email.Text;
                    Session["check"] = "1";
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/Index/Doctor login.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Index/Doctor login.aspx?l=ar-EG");
                    //}
                    //this.ModalPopupExtender4.Show();
                    //RegisterStartupScript("", "<Script Language=JavaScript>swal('Thank you for registering with Hakkeem.Please check your mail and upload the signed agreement.We will contact you with in one business day.')</Script>");
                }
               
                else
                {
                    BtnSubmitOTP.Enabled = true;

                    //RegisterStartupScript("", "<Script Language=JavaScript>swal('You entered OTP is not valid...please check given email')</Script>");
                    //Label4.Text = "You entered OTP is not valid...please check given email";
                    //this.ModalPopupExtender5.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        BtnSubmitOTP.Text = "Submit";
                        Label31.Text = "You entered OTP is not valid, please check given email";
                        Label31.Visible = true;
                    //    }
                    //else
                    //{
                    //    BtnSubmitOTP.Text = "عرض";
                    //    Label31.Text = "لقد أدخلت مكتب المدعي العام غير صالح, يرجى التحقق من البريد الإلكتروني";
                    //        Label31.Visible = true;
                    //    }
                }



            }
            else
            {

            }
            }
        }

    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
      //  BtnSubmitOTP0.Enabled = true;
    }
    protected void BtnSubmitOTP0_Click(object sender, EventArgs e)
    {

        SqlCommand com11 = new SqlCommand("delete from tbl_maild where email='" + email.Text + "'", con);
        com11.ExecuteNonQuery();

        SqlCommand com = new SqlCommand("select id from tbl_mail where email='" + email.Text + "'", con);
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
                Label31.Text = "OTP send to your Mail-Id...!";
            //}
            //else
            //{
            //    Label31.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
            //}
        }


        else
        {


            try
            {


                if (BtnSubmitOTP0.Text == "Resend" )
                {
                
                    // BtnSubmitOTP0.Text = "Sending...";

                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        BtnSubmitOTP0.Text = "Resend";
                    //}
                    //else
                    //{
                    //    BtnSubmitOTP0.Text = "عملية التشغيل ..";
                    //}

                    //    BtnSubmitOTP.Text = "Submit";
                    //BtnResendOTP.Text = "Resend";
                    //BtnResendOTP.Enabled = false;
                    Label31.Text = "";
                    TxtOTP.Text = "";
                    var Query = from item in db.tbl_doctors
                             where item.d_email == obj.EnryptString(email.Text)
                           select item;
                    if (Query.Count() > 0)
                    {
                        //RegisterStartupScript("", "<Script Language=JavaScript>swal('You are already registered with this mail id.')</Script>");
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            Label31.Text = "You are already registered with this mail id";
                        //}
                        //else
                        //{
                        //    Label31.Text = "أنت مسجل بالفعل مع هذا الرقم البريدي";
                        //}
                        //this.ModalPopupExtender7.Show();
                    }
                  
      
                    else
                    {

                        SqlCommand com1 = new SqlCommand("insert into tbl_mail values('" + email.Text + "','1')", con);
                        com1.ExecuteNonQuery();
                        BtnSubmitOTP0.Enabled = false;

                        Random rd = new Random();
                        int i = rd.Next(000000, 999999);
                        Session["otp"] = i.ToString();
                        string pno = "";
                        pno = phoneno.Text.Replace(" ", "");
                        string ph = "+966" + pno.ToString();
                        ob.Message(ph.ToString(), "OTP for registering in Hakkeem is : " + Session["otp"].ToString());


                        string ph1 = "+91" + pno.ToString();
                        ob.Message(ph1.ToString(), "OTP for registering in Hakkeem is : " + Session["otp"].ToString());

                        //  mailmsg.mail(TextBox1.Text, "OTP for registering in Hakkeem is: " + Session["uotp"].ToString(), "OTP from Hakkeem");


                        MailMessage mailmsg = new MailMessage();
                        Email_To_DoctorOtp(email.Text, i.ToString());

                        //this.ModalPopupExtender1.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                        Label31.Visible = true;
                            Label31.Text = "OTP send to your Mail-Id...!";
                        //}
                        //else
                        //{
                        //    Label31.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
                        //}
                    }
                    //BtnSubmitOTP0.Enabled = true;
                    //BtnSubmitOTP0.Text = "Resend";
                }
            }
            catch (Exception ex)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Label31.Text = "Something went wrong..!";
                //}
                //else
                //{
                //    Label31.Text = "هناك خطأ ما..!";
                //}
                BtnSubmitOTP0.Enabled = true;
                BtnSubmitOTP0.Text = "Resend";
            }
        }












        //SqlCommand com11 = new SqlCommand("delete from tbl_maild where email='" + email.Text + "'", con);
        //com11.ExecuteNonQuery();



        //SqlCommand com = new SqlCommand("select id from tbl_mail where email='" + email.Text + "'", con);
        //int t;
        //try
        //{
        //    t = Convert.ToInt32(com.ExecuteScalar());

        //}
        //catch (Exception ex)
        //{
        //    t = 0;
        //}


        //if (t != 0)
        //{
        //    if (Session["Language"].ToString() == "Auto")
        //    {
        //        Label31.Visible = true;
        //        Label31.Text = "OTP send to your Mail-Id...!";
        //    }
        //    else
        //    {
        //        Label31.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
        //    }
        //}


        //else
        //{




        //    try
        //    {

        //        if (BtnSubmitOTP0.Text == "Resend" || BtnSubmitOTP0.Text == "إعادة إرسال")
        //        {
        //            BtnSubmitOTP0.Enabled = false;
        //            // BtnSubmitOTP0.Text = "Sending...";

        //            //if (Session["Language"].ToString() == "Auto")
        //            //{
        //            //    BtnSubmitOTP0.Text = "Resend";
        //            //}
        //            //else
        //            //{
        //            //    BtnSubmitOTP0.Text = "عملية التشغيل ..";
        //            //}







        //            Label31.Text = "";
        //            TxtOTP.Text = "";
        //            Random rd = new Random();
        //            int i = rd.Next(000000, 999999);
        //            HiddenField1.Value = i.ToString();
        //            Session["otp"] = i.ToString();
        //            var Query = from item in db.tbl_doctors
        //                        where item.d_email == obj.EnryptString(email.Text)
        //                        select item;

        //            if (Query.Count() > 0)
        //            {
        //                //RegisterStartupScript("", "<Script Language=JavaScript>alert('You are already registered with this mail id.............')</Script>");
        //                if (Session["Language"].ToString() == "Auto")
        //                {
        //                    Label31.Text = "You are already registered with this mail id...!";
        //                }
        //                else
        //                {
        //                    Label31.Text = "أنت مسجل بالفعل مع هذا الرقم البريدي";
        //                }
        //                //this.ModalPopupExtender6.Show();
        //            }
        //            else
        //            {




        //                string pno = "";
        //                pno = phoneno.Text.Replace(" ", "");
        //             string ph = "+966" + pno.ToString();
        //               // string ph = "+918086194845";
        //                //Email(email.Text, "Thank you Dr." + Fname.Text + " " + Lname.Text + " for join with Hakkeem and your verification OTP is " + i + " Please submit signed agreement.");

        //                //   msg.mail(email.Text, "OTP for registering in Hakkeem is: " + i, "OTP from Hakkeem");
        //                Email_To_DoctorOtp(email.Text, i.ToString());
        //                con.Open();
        //                SqlCommand com1 = new SqlCommand("insert into tbl_mail values('" + email.Text + "','1')", con);
        //                com1.ExecuteNonQuery();
        //                ob.Message(ph.ToString(), "OTP for registering in Hakkeem is : " + i);
        //                HiddenField1.Value = i.ToString();
        //                Session["otp"] = i.ToString();
        //                if (Session["Language"].ToString() == "Auto")
        //                {
        //                    Label31.Visible = true;
        //                    Label31.Text = "OTP send to your Mail-Id...!";
        //                }
        //                else
        //                {
        //                    Label31.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
        //                }

        //                //this.ModalPopupExtender1.Show();
        //            }
        //            // BtnSubmitOTP0.Enabled = true;
        //        }
        //        else
        //        {
        //            if (Session["Language"].ToString() == "Auto")
        //            {
        //                BtnSubmitOTP0.Text = "Resend";
        //            }
        //            else
        //            {
        //                BtnSubmitOTP0.Text = "إعادة إرسال";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        BtnSubmitOTP0.Enabled = true;
        //        if (Session["Language"].ToString() == "Auto")
        //        {
        //            Label31.Text = "Something went wrong..!";
        //        }
        //        else
        //        {
        //            Label31.Text = "هناك خطأ ما..!";
        //        }
        //    }
        //}

    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor.aspx?l=ar-EG");
        //}
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        TxtOTP.Text = "";
        //this.ModalPopupExtender1.Show();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    protected void TxtOTP_TextChanged(object sender, EventArgs e)
    {
        //if (Session["OTP"].ToString() == TxtOTP.Text)
        //{
        //    string pno = "";
        //    pno = phoneno.Text.Replace(" ", "");
        //    // Response.Write(pno.ToString());

        //    tbl_doctor td = new tbl_doctor()
        //    {
        //        d_email = email.Text,
        //        d_name = Fname.Text + " " + Lname.Text,
        //        d_specialties = specialty.SelectedItem.Text,
        //        //d_location = zipcode.Text,
        //        d_sex = DropDownList1.SelectedItem.Text,


        //        d_contact = "+966" + pno.ToString(),
        //        d_status = 0,
        //        d_date_time = DateTime.Now.ToString(),
        //        d_password = Session["pass"].ToString(),
        //        //d_password = password,
        //        //d_city = city.SelectedItem.Text,
        //        d_education = drpGraduation.SelectedItem.Text + " " + Session["pg"].ToString(),
        //        d_otp = Convert.ToInt64(Session["OTP"]),
        //        d_hakkimid = Session["hakkimid"].ToString(),
        //    };
        //    db.tbl_doctors.InsertOnSubmit(td);
        //    db.SubmitChanges();
        //    Session["name"] = Fname.Text + " " + Lname.Text;
        //    cmd = new SqlCommand("insert into tbl_hakkimid values('" + Session["hakkimid"].ToString() + "','DOC')", con);
        //    cmd.ExecuteNonQuery();

        //    //MailMessage mailmsg = new MailMessage();
        //    msg.mail(email.Text, "Your account is successfully created with Hakkeem , your HAKKEEM ID is. " + Session["hakkimid"], "Account Creation");
        //    //Emailhakkimid(email.Text, "Your account is successfully created with Hakkeem , your HAKKEEM ID is. " + Session["hakkimid"]);
        //    //this.Page.RegisterStartupScript("", "<Script Language=JavaScript>alert('Thank you for registering with Hakkeem. Please check your mail and upload the signed agreement. We will contact you with in one business day.');window.location='../../default.aspx'</Script>");
        //    Label3.Text = "Thank you for registering with Hakkeem.Please check your mail and upload the signed agreement.We will contact you with in one business day.";
        //    Session["doctor"] = email.Text;
        //    this.ModalPopupExtender4.Show();
        //}
        //else
        //{
        //    //RegisterStartupScript("", "<Script Language=JavaScript>alert('You entered OTP is not valid...please check given email')</Script>");
        //    Label4.Text = "You entered OTP is not valid...please check given email";
        //    this.ModalPopupExtender5.Show();

        //}


    }

    protected void specialty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
          //  if (Session["Language"].ToString() == "Auto")
          //  {
          //      LinkButton1.Text = "English";
          //      Session["Language"] = "ar-EG";
          //      Response.Redirect(Request.Path + "?l=ar-EG");

          //  }
          //  else
          //  {
          //      Session["Language"] = "Auto";
          //      LinkButton1.Text = "عربى";
          //Response.Redirect(Request.Path);

          //  }
        }
        catch (Exception ex)
        {
            //LinkButton1.Text = "English";

        }
    }
    public bool Email_To_DoctorOtp(string email, string otp)
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
}