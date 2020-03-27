using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class Index_hospial_join : System.Web.UI.Page
{
    secure obj = new secure();
    SMS ob = new SMS();
    MailMessage mail = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    string path = "";
    string fileName = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

    SqlCommand cmd;
    SqlDataReader dr, dr1, dr2;

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
        con.Open();
        BtnResendOTP.Enabled = true;
        //if (Session["Language"].ToString() == "Auto")
        //{
            
        //    LinkButton1.Text = "عربى";
        //}
        //else
        //{
        //    LinkButton1.Text = "English";
        //}

        if (!IsPostBack)
        {
            SqlCommand com11 = new SqlCommand("delete from tbl_mailh where email='" + txt_HEmail.Text + "'", con);
            com11.ExecuteNonQuery();
            SqlCommand com1 = new SqlCommand("delete from tbl_hmail where email='" + txt_HEmail.Text + "'", con);
            com1.ExecuteNonQuery();

        }
    }

    public bool Email_To_HospitalOtp(string email, string otp)
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        TxtOTP.Text = "";
        Label31.Text = "";
        string pass1 = TxtPassword.Text;
        TxtPassword.Attributes.Add("value", pass1);
        string pass2 = TxtConfirm.Text;
        TxtConfirm.Attributes.Add("value", pass2);
        if (ckbTerms.Checked)
        {
          //  Session["eeh"] = txt_HEmail.Text;
            int flag = 0;
            var Query = from item in db.tbl_hospitalregs where item.h_regno == txt_HRegnNo.Text  select item;
            if (Query.Count() > 0)
            {
                flag = 1;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Hospital Registration Number already exist')</Script>");
                    txt_HRegnNo.Focus();

                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تسجيل المستشفى ممبر موجود بالفعل')</Script>");
                //}
                //Label1.Text = "Hospital already exist...!";
                //this.ModalPopupExtender2.Show();
            }
            var Query1 = from item in db.tbl_hospitalregs where item.h_email == txt_HEmail.Text select item;
            if (Query1.Count() > 0)
            {
                flag = 1;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Hospital mail id  already exist')</Script>");
                    txt_HEmail.Focus();

                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم تعريف بريد المستشفى موجود من قبل')</Script>");
                //}
                //Label1.Text = "Hospital already exist...!";
                //this.ModalPopupExtender2.Show();
            }
            // string pp = "+966" + txt_HPhone.Text;
          string  pno = txt_HPhone.Text.Replace(" ", "");
            var Query2 = from item in db.tbl_hospitalregs where item.h_contact ==pno.ToString() select item;
            if (Query2.Count() > 0)
            {
                flag = 1;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Hospital phone number  already exist')</Script>");
                    txt_HPhone.Focus();

                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('مستشفى فونينومبر موجود بالفعل')</Script>");
                //}
                //Label1.Text = "Hospital already exist...!";
                //this.ModalPopupExtender2.Show();
            }








            if (flag==0)
            {




                SqlCommand com = new SqlCommand("select id from tbl_mailh where email='" + txt_HEmail.Text + "'", con);
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

                    // Session["email"] = txt_HEmail.Text;
                    // Session["otp"] = i;
                    //  Session["pass"] = TxtConfirm.Text;
                    //  string pno = "";
                    HiddenField1.Value = i.ToString();
                    Session["hotp"] = i.ToString();

                    SqlCommand com1 = new SqlCommand("insert into tbl_mailh values('" + txt_HEmail.Text + "','1')", con);
                    com1.ExecuteNonQuery();

                    pno = txt_HPhone.Text.Replace(" ", "");
                    string ph = "+966" + pno.ToString();
                    // Session["phno"] = ph;
                    //string mailmessage = "Thank you " + txt_Hname.Text + " for join with Hakkeem and your verification OTP is " + i + " Please submit signed agreement.";
                    //Email(txt_HEmail.Text, mailmessage);
                    //  mail.mail(txt_HEmail.Text, "OTP for registering in Hakkeem is: " + i, "OTP from Hakkeem");
                    ob.Message(ph.ToString(), "OTP for registering in Hakkeem is : " + i);
                    string ph1 = "+91" + pno.ToString();
                    ob.Message(ph1.ToString(), "OTP for registering in Hakkeem is : " + i);

                    Email_To_HospitalOtp(txt_HEmail.Text, i.ToString());

                  
                    //this.ModalPopupExtender1.Show();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }

            }
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please agree the terms and conditions.')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى الموافقة على الشروط والأحكام')</Script>");
            //}
            //Label2.Text = "Please agree the terms and conditions...!";
            //this.ModalPopupExtender3.Show();
        }
        //this.ModalPopupExtender1.Show();
    }

    #region EmailFunction
    public void Email(string email, string msg)
    {
        using (System.IO.StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                long i;
                try
                {
                    var selectId = from item in db.tbl_hospitalregs
                                   select item.h_id;
                    i = selectId.Max() + 1;
                }
                catch (Exception ex)
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

                sb.Append("<tr><td><b>Hospital Name :</b> ");
                sb.Append(txt_Hname.Text);
                sb.Append("</td><td><b>Hospital Address: </b>");
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
    //public void EmailwithoutAgreement(string email, string msg)
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
    #endregion
    protected void Button21_Click(object sender, EventArgs e)
    {
        SqlCommand com1 = new SqlCommand("delete from tbl_hmail where email='" + txt_HEmail.Text + "'", con);
        com1.ExecuteNonQuery();
        SqlCommand com11 = new SqlCommand("delete from tbl_mailh where email='" + txt_HEmail.Text + "'", con);
        com11.ExecuteNonQuery();
    }
    public void pdfCreate()
    {
        using (System.IO.StringWriter sw = new StringWriter())
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Hakkeem_Agreement.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                long i = 0;
                var selectId = from item in db.tbl_hospitalregs
                               select item.h_id;
                try
                {
                    i = selectId.Max() + 1;
                }
                catch (Exception ex)
                {
                    i = 1;
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>AGREMENT FORM</b></td></tr>");

                sb.Append("<tr><td colspan = '2'></td></tr>");
                sb.Append("<tr><td><b>Company Name:</b>");
                sb.Append("Golden Eteqan");
                sb.Append("</td><td><b>Id: </b>");
                sb.Append(i.ToString());
                sb.Append(" </td></tr>");

                sb.Append("<tr><td><b>Hospital Name :</b> ");
                sb.Append(txt_Hname.Text);
                sb.Append("</td><td><b>Hospital Address: </b>");
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
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
        }
    }


    protected void LnkAgreement_Click1(object sender, EventArgs e)
    {

        pdfCreate();

    }
    protected void TxtConfirm_TextChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {

        SqlCommand com1 = new SqlCommand("delete from tbl_hmail where email='" + txt_HEmail.Text + "'", con);
        com1.ExecuteNonQuery();
        SqlCommand com11 = new SqlCommand("delete from tbl_mailh where email='" + txt_HEmail.Text + "'", con);
        com11.ExecuteNonQuery();
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
                var Query = from item in db.tbl_hospitalregs
                            where item.h_email == txt_HEmail.Text
                            select item;

                if (Query.Count() > 0)
                {
                    Response.Redirect("~/Index/Hospita Login.aspx");
                }

                else
                {



                    if (BtnSubmitOTP.Text == "Submit")
                    {
                        BtnSubmitOTP.Enabled = false;
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                        //    BtnSubmitOTP.Text = "Running process..";
                        //}
                        //else
                        //{
                        //    BtnSubmitOTP.Text = "عملية التشغيل ..";
                        //}


                        if (true)
                        {
                            cmd = new SqlCommand("select max(id) as id from tbl_hakkimid where user_type='HOS'", con);
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
                            //txt_hakkimid.Text = "HH_000" + id.ToString();
                            string hid = "HH_000" + id.ToString();
                            string pno = "";
                        pno = txt_HPhone.Text.Replace(" ", "");
                      //  pno = "+918086194845";
                            tbl_hospitalreg th = new tbl_hospitalreg()
                            {
                                h_address = txt_HAddress.Text,
                                h_contact = pno.ToString(),
                                h_email = txt_HEmail.Text,
                                h_name = txt_Hname.Text,
                                h_regno = txt_HRegnNo.Text,
                                h_status = 0,
                                //h_zipcode = txt_Hzip.Text,
                                h_password = obj.EnryptString(TxtPassword.Text),
                                //h_city = drp_HCity.Text,
                                h_about = txt_HAbout.Text,
                                //h_photo = "~/hospital images/" + txt_HRegnNo.Text + LblFilePath.Text,
                                h_date_time = DateTime.Now.ToString("yyyy-MM-dd"),
                                h_otp = Convert.ToInt64(Session["hotp"].ToString()),
                                h_hakkimid = hid.ToString(),
                            };
                            db.tbl_hospitalregs.InsertOnSubmit(th);
                            db.SubmitChanges();
                         
                            cmd = new SqlCommand("insert into tbl_hakkimid values('" + hid.ToString() + "','HOS')", con);
                            cmd.ExecuteNonQuery();
                        //string mailmessage = "Your hospital is added successfully with Hakkeem, please login and upload signed agreement. Your hospital HAKKEEM ID is " + Session["hakkeemid"].ToString();
                        /// // string mailmessage = "<p>Thank you for registering with Hakkeem</p>" + "<p>" + "Your hakkeem id is: " + hid.ToString() + "</p><p>" + "Click the link to access Hakkeem " + "http://www.hakkeem.com" + " and upload signed agreement.</p>";
                        /////  Email(txt_HEmail.Text, mailmessage);
                        string ph = "+966" + pno.ToString();
                        ob.Message(ph.ToString(), " Thank you for registering with Hakkeem  " + "  " + "Your hakkeem id is : " + hid.ToString() + " ");

                        string ph1 = "+91" + pno.ToString();
                        ob.Message(ph1.ToString(), " Thank you for registering with Hakkeem  " + "  " + "Your hakkeem id is : " + hid.ToString() + " ");


                        Email_To_Hospital(txt_HEmail.Text, hid.ToString());

                       
                            //this.Page.RegisterStartupScript("", "<Script Language=JavaScript>alert('Thank you for registering with Hakkeem. Please check your mail and upload the signed agreement. We will contact you with in one business day.');window.location='../../default.aspx'</Script>");
                            //Label3.Text = "Thank you for registering with Hakkeem. Please check your mail and upload the signed agreement. We will contact you with in one business day.";
                            Session["hospital"] = txt_HRegnNo.Text;
                            //this.ModalPopupExtender4.Show();
                            Session["check"] = "1";
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                Response.Redirect("~/Index/Hospita Login.aspx");
                            //}
                            //else
                            //{
                            //    Response.Redirect("~/Index/Hospita Login.aspx?l=ar-EG");
                            //}
                        }
                      
                        else
                        {
                            //RegisterStartupScript("", "<Script Language=JavaScript>alert('You entered OTP is not valid...please check given email')</Script>");
                            //Label4.Text = "You entered OTP is not valid...please check given email";
                            //this.ModalPopupExtender5.Show();
                            BtnSubmitOTP.Enabled = true;
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                BtnSubmitOTP.Text = "Submit";
                                Label31.Text = "Entered OTP is not valid...Check your Mail";
                                Label31.Visible = true;
                            //}
                            //else
                            //{
                            //    BtnSubmitOTP.Text = "عرض";
                            //    Label31.Text = "لقد أدخلت مكتب المدعي العام غير صالح, يرجى التحقق من البريد الإلكتروني";
                            //    Label31.Visible = true;
                            //}

                        }
                    }
                    else
                    {

                    }
                }
            }

        }
    public bool Email_To_Hospital(string email, string hakkeemid)
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
            string set = "http://www.hakkeem.com/visitus.png";
            string btnpath = "http://hakkeem.com/Index/Hospita%20Login.aspx";
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
    
    protected void BtnResendOTP_Click(object sender, EventArgs e)
    {

        SqlCommand com11 = new SqlCommand("delete from tbl_mailh where email='" + txt_HEmail.Text + "'", con);
        com11.ExecuteNonQuery();
        SqlCommand com = new SqlCommand("select id from tbl_hmail where email='" + txt_HEmail.Text + "'", con);
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
                    string pno = "";
                    pno = txt_HPhone.Text.Replace(" ", "");
                    string ph = "+966" + pno.ToString();
                    BtnResendOTP.Enabled = false;
                    //    BtnSubmitOTP.Text = "Submit";
                    //BtnResendOTP.Text = "Running process..";
                    //BtnResendOTP.Enabled = false;
                    Label31.Text = "";
                    TxtOTP.Text = "";
                    Random rd = new Random();
                    int i = rd.Next(000000, 999999);
                    Session["hotp"] = i.ToString();

                    var Query = from item in db.tbl_hospitalregs
                                where item.h_email == txt_HEmail.Text
                                select item;

                    if (Query.Count() > 0)
                    {
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Your hospital already registered with this mail id')</Script>");
                            //Label5.Text = "Your hospital already registered with this mail id";
                            //this.ModalPopupExtender6.Show();
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المستشفى الخاص بك مسجل بالفعل مع هذا الرقم البريد')</Script>");
                        //}
                    }
                    else
                    {
                        SqlCommand com1 = new SqlCommand("insert into tbl_hmail values('" + txt_HEmail.Text + "','1')", con);
                        com1.ExecuteNonQuery();
                        //string mailmessage = "Thank you " + txt_Hname.Text + " for join with Hakkeem and your verification OTP is " + i + " Please submit signed agreement.";
                        //Email(Session["email"].ToString(), mailmessage);
                        //ob.Message(Session["phno"].ToString(), mailmessage);
                        Email_To_HospitalOtp(txt_HEmail.Text, i.ToString());
                        //mail.mail(txt_HEmail.Text, "OTP for registering in Hakkeem is: " + i, "OTP from Hakkeem");
                        ob.Message(ph.ToString(), "OTP for registering in Hakkeem is : " + i);
                        string ph1 = "+91" + pno.ToString();
                        ob.Message(ph1.ToString(), "OTP for registering in Hakkeem is : " + i);
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                        Label31.Text = "OTP send to your Mail-Id...!";
                        //}
                        //else
                        //{
                        //    Label31.Text = "أرسل مكتب المدعي العام إلى معرف البريد ...!";
                        //}
                        //this.ModalPopupExtender1.Show();
                    }

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
            }
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/Hospital.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/Hospital.aspx?l=ar-EG");
        //}
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        TxtOTP.Text = "";
        //this.ModalPopupExtender1.Show();
    }

    protected void TxtOTP_TextChanged(object sender, EventArgs e)
    {
        //if (Session["otp"].ToString() == TxtOTP.Text)
        //{
        //    con.Open();
        //    string pno = "";
        //    pno = txt_HPhone.Text.Replace(" ", "");
        //    tbl_hospitalreg th = new tbl_hospitalreg()
        //    {
        //        h_address = txt_HAddress.Text,
        //        h_contact = "+966" + pno.ToString(),
        //        h_email = txt_HEmail.Text,
        //        h_name = txt_Hname.Text,
        //        h_regno = txt_HRegnNo.Text,
        //        h_status = 0,
        //        //h_zipcode = txt_Hzip.Text,
        //        h_password = Session["pass"].ToString(),
        //        //h_city = drp_HCity.Text,
        //        h_about = txt_HAbout.Text,
        //        //h_photo = "~/hospital images/" + txt_HRegnNo.Text + LblFilePath.Text,
        //        h_date_time = DateTime.Now.ToString("yyyy-MM-dd"),
        //        h_otp = Convert.ToInt64(Session["otp"].ToString()),
        //        h_hakkimid = Session["hakkeemid"].ToString(),
        //    };
        //    db.tbl_hospitalregs.InsertOnSubmit(th);
        //    db.SubmitChanges();

        //    cmd = new SqlCommand("insert into tbl_hakkimid values('" + Session["hakkeemid"].ToString() + "','HOS')", con);
        //    cmd.ExecuteNonQuery();
        //    string mailmessage = "Your hospital is added successfully with Hakkeem, please login and upload signed agreement. Your hospital HAKKEEM ID is " + Session["hakkeemid"].ToString();
        //    mail.mail(Session["email"].ToString(), mailmessage, "Hospital registration");
        //    //this.Page.RegisterStartupScript("", "<Script Language=JavaScript>alert('Thank you for registering with Hakkeem. Please check your mail and upload the signed agreement. We will contact you with in one business day.');window.location='../../default.aspx'</Script>");
        //    Label3.Text = "Thank you for registering with Hakkeem. Please check your mail and upload the signed agreement. We will contact you with in one business day.";
        //    Session["hospital"] = txt_HRegnNo.Text;
        //    this.ModalPopupExtender4.Show();
        //}
        //else
        //{
        //    //RegisterStartupScript("", "<Script Language=JavaScript>alert('You entered OTP is not valid...please check given email')</Script>");
        //    Label4.Text = "You entered OTP is not valid...please check given email";
        //    this.ModalPopupExtender5.Show();
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