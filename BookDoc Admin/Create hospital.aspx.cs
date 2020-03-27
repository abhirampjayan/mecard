using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class BookDoc_Admin_Create_hospital : System.Web.UI.Page
{
    MailMessage mail = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    string filePath = "";
    SMS ob1 = new SMS();
    secure obj = new secure();
    SqlCommand cmd = new SqlCommand();
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
        //    this.MasterPageFile = "~/BookDoc Admin/AdminArabicMasterPage.master";
        //}
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (Session["hosptal_Id"] == null)
            {
               
              
                HyperLink1.Visible = false;
               
                Panel1.Visible = true;

            }
            else
            {
                LinkCreate.Visible = true;
            //if (Session["Language"].ToString() == "Auto")
            //{
                Button1.Text = "Confirm Hospital";
            //}
            //else
            //{
            //    Button1.Text = "تأكيد المستشفى";
            //}
             
                var selectHospital = from hospital in db.tbl_hospitalregs
                                     where hospital.h_id == Convert.ToInt32(Session["hosptal_Id"].ToString())
                                     select hospital;
                foreach (var hos in selectHospital)
                {
                    haddrs.Text = hos.h_address.ToString();
                    Hname.Text = hos.h_name.ToString();
                    hregno.Text = hos.h_regno.ToString();
                //hcity.Text = hos.h_city.ToString();
                //hcontact.Text = hos.h_contact.ToString().Substring(4,9);
                //if (hos.h_zipcode != null)
                //{

                //    hzipcode.Text = hos.h_zipcode.ToString();
                //}
                    hcontact.Text = hos.h_contact.ToString();
                    hemail.Text = hos.h_email.ToString();
                    if (hos.h_photo != null)
                    {
                        //Image1.ImageUrl = hos.h_photo.ToString();
                        FileUpload1.Enabled = false;
                    }

                    habout.Text = hos.h_about.ToString();


                //Agrement disabled

                    //if (hos.h_agreement != null)
                    //{
                    //    HyperLink1.NavigateUrl = hos.h_agreement.ToString();
                    //    HyperLink1.Enabled = true;
                    //    Panel1.Visible = false;
                    //}
                    //else
                    //{
                    //    HyperLink1.Enabled = false;
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                    //    HyperLink1.Text = "Agreement not available";
                    //}
                    //else
                    //{
                    //    HyperLink1.Text = "الاتفاقية غير متوفرة";
                    //}
                    //    HyperLink1.Visible = false;
                    //    Panel1.Visible = true;
                    //}
                }
            
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Confirm Hospital")
        {
            string emailId = hemail.Text;
            string pno = hcontact.Text.Replace(" ", "");
            string ph = "+966" + pno.ToString();
            string ph1 = "+966" + pno.ToString();
            if (FileUpload2.HasFile)
            {
                 string extn = (Path.GetExtension(FileUpload2.FileName)).ToLower();
                 if (extn == ".pdf"||extn==".jpg")
                 {
                    //Email(emailId, "Your hospital is added successfully with Hakkeem and Login id is " + hregno.Text + " ,password is " + "BD" + hregno.Text + " please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com");
                    //string msg= "Your hospital is added successfully with Hakkeem and Login id is " + hregno.Text + " ,password is " + "BD" + hregno.Text + " please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com";
                    ///  string msg = "Your hospital is added successfully with Hakkeem. please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com";
                    ////     mail.mail(emailId, msg, "Hospital registration");
                  
                    ob1.Message(ph.ToString(), "Dear Hospital, your Hakkeem account is approved by Hakkeem authority.");
                    ob1.Message(ph1.ToString(), "Dear Hospital, your Hakkeem account is approved by Hakkeem authority.");
                    Email_To_ApproveHospital(emailId);

                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/HospitalAgreementsImages/" + hregno.Text + FileUpload2.FileName));
                     var selectHospital = from hospital in db.tbl_hospitalregs
                                          where hospital.h_id == Convert.ToInt32(Session["hosptal_Id"].ToString())
                                          select hospital;
                     foreach (var hos in selectHospital)
                     {
                         hos.h_agreement = "~/HospitalAgreementsImages/" + hregno.Text + FileUpload2.FileName;
                         hos.h_status = 1;
                         db.SubmitChanges();
                     }
                     Session["hosptal_Id"] = null;

                    //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Succesfully Added hospital');window.location='HospitalRequest.aspx';</Script>");
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Label1.Text = "Succesfully Added hospital";
                    //}
                    //else
                    //{
                    //    Label1.Text = "تمت إضافة المستشفى بنجاح";
                    //}
                    //ModalPopupExtender1.Show();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();

                }
                 else
                 {
                    //Label2.Text = "Upload only pdf files or jpg files";
                    //ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf files or jpg files ')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف فقط أو ملفات جبيغ ')</Script>");
                    //}
                }
            }

            else
            {
                //Email(emailId, "Your hospital is added successfully with Hakkeem and Login id is " + hregno.Text + " ,password is " + "BD" + hregno.Text + " please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com");
                // string msg = "Your hospital is added successfully with Hakkeem and Login id is " + hregno.Text + " ,password is " + "BD" + hregno.Text + " please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com";
                ////  string msg = "Your hospital is added successfully with Hakkeem. please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com";
                //// mail.mail(emailId, msg, "Hospital registration");
                Email_To_ApproveHospital(emailId);
                ob1.Message(ph.ToString(), "Dear Hospital, your Hakkeem account is approved by Hakkeem authority.");
                var selectHospital = from hospital in db.tbl_hospitalregs
                                     where hospital.h_id == Convert.ToInt32(Session["hosptal_Id"].ToString())
                                     select hospital;
                foreach (var hos in selectHospital)
                {
                    hos.h_status = 1;
                    db.SubmitChanges();
                }
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('Succesfully Added hospital')</Script>");
                Session["hosptal_Id"] = null;
                //Response.Redirect("HospitalRequest.aspx");
                //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Succesfully Added hospital');window.location='HospitalRequest.aspx';</Script>");
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Label1.Text = "Succesfully Added hospital";
                //}
                //else
                //{
                //    Label1.Text = "تمت إضافة المستشفى بنجاح";
                //}
                //ModalPopupExtender1.Show();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            
        }
        else
        {
            if(FileUpload2.HasFile)
            {
                string extn = (Path.GetExtension(FileUpload2.FileName)).ToLower();
                if(extn==".pdf"||extn==".jpg")
                {
                    //var Query1 = from item in db.tbl_hospitalregs where item.h_contact == hcontact.Text select item;
                    //if (Query1.Count() > 0)
                    //{
                    //    if (Session["Language"].ToString() == "Auto")
                    //    {
                    //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Phone Number is already exist')</Script>");
                    //        hcontact.Text = "";
                    //        hcontact.Focus();
                    //    }
                    //    else
                    //    {
                    //        RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم الهاتف موجود بالفعل')</Script>");
                    //        hcontact.Text = "";
                    //        hcontact.Focus();
                    //    }

                    //}
                    var Query = from item in db.tbl_hospitalregs where (item.h_regno == hregno.Text)||(item.h_email==hemail.Text) select item;
                    if (Query.Count() > 0)
                    {
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Hospital already exist')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المستشفى موجود بالفعل')</Script>");
                        //}
                        //Label2.Text = "Hospital already exist";
                        //ModalPopupExtender2.Show();
                    }
                    else
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/hospital images/" + hregno.Text + FileUpload1.FileName));
                        FileUpload2.PostedFile.SaveAs(Server.MapPath("~/HospitalAgreementsImages/" + hregno.Text + FileUpload2.FileName));
                        string path = "~/hospital images/" + hregno.Text + FileUpload1.FileName;
                        //Image1.ImageUrl = path;

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
                        tbl_hospitalreg th = new tbl_hospitalreg()
                        {
                            h_address = haddrs.Text,
                            h_contact = hcontact.Text,
                            h_email = hemail.Text,
                            h_name = Hname.Text,
                            h_regno = hregno.Text,
                            h_status = 1,
                            //h_zipcode = hzipcode.Text,
                            h_password = obj.EnryptString("HH" + hregno.Text),
                            //h_city = hcity.Text,
                            h_about = habout.Text,
                            h_photo = path,
                            h_agreement = "~/HospitalAgreementsImages/" + hregno.Text + FileUpload2.FileName,
                            h_date_time = DateTime.Now.ToString(),
                            h_hakkimid = hid.ToString(),
                        };
                        db.tbl_hospitalregs.InsertOnSubmit(th);
                        db.SubmitChanges();
                        con.Open();
                        cmd = new SqlCommand("insert into tbl_hakkimid values('" + hid.ToString() + "','HOS')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string passsword = "HH" + hregno.Text;
                        //Email(hemail.Text, "Your hospital is added successfully with Hakkeem and Login id is " + hregno.Text + " ,password is " + "BD" + hregno.Text + " please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com");
                        ///// string msg = "Your hospital is added successfully with Hakkeem and Login id is " + hregno.Text + " ,password is " + "BD" + hregno.Text + " please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com";
                        /// mail.mail(hemail.Text, msg, "Hospital registration");
                        Email_To_NewHospital(hemail.Text, hregno.Text, passsword);
                        string pno = "";
                        pno = hcontact.Text.Replace(" ", "");
                        string ph = "+966" + pno.ToString();
                       
                        ob1.Message(ph, " Thank you for registering with Hakkeem.Your Register No: " + hregno.Text + " and password is:" + passsword);


                        string ph1 = "+91" + pno.ToString();

                        ob1.Message(ph1, " Thank you for registering with Hakkeem.Your Register No: " + hregno.Text + " and password is:" + passsword);
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('Successfully create hospital')</Script>");
                        //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Succesfully create hospital');window.location='Create hospital.aspx';</Script>");
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                        Label1.Text = "Succesfully create hospital";
                        //}
                        //else
                        //{
                        //    Label1.Text = "إنشاء المستشفى بنجاح";
                        //}
                        //ModalPopupExtender1.Show();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                }
                else
                {
                    //Label2.Text = "Upload only pdf files or jpg files";
                    //ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf files or jpg files ')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف فقط أو ملفات جبيغ')</Script>");
                    //}
                }
            }
            else
            {
                //var Query1 = from item in db.tbl_hospitalregs where item.h_contact == hcontact.Text select item;
                //if (Query1.Count() > 0)
                //{
                //    if (Session["Language"].ToString() == "Auto")
                //    {
                //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Phone Number is already exist')</Script>");
                //        hcontact.Text = "";
                //        hcontact.Focus();
                //    }
                //    else
                //    {
                //        RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم الهاتف موجود بالفعل')</Script>");
                //        hcontact.Text = "";
                //        hcontact.Focus();
                //    }

                //}
                var Query = from item in db.tbl_hospitalregs where item.h_regno == hregno.Text select item;
            if (Query.Count() > 0)
            {
                    //Label2.Text = "Hospital already exist";
                    //ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Hospital already exist')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المستشفى موجود بالفعل')</Script>");
                    //}
                }
            else
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/hospital images/" + hregno.Text + FileUpload1.FileName));
                string path = "~/hospital images/" + hregno.Text + FileUpload1.FileName;
               // Image1.ImageUrl = path;

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
                    tbl_hospitalreg th = new tbl_hospitalreg()
                {
                    h_address = haddrs.Text,
                    h_contact = hcontact.Text,
                    h_email = hemail.Text,
                    h_name = Hname.Text,
                    h_regno = hregno.Text,
                    h_status = 1,
                    //h_zipcode = hzipcode.Text,
                    h_password = obj.EnryptString("HH" + hregno.Text),
                    //h_city = hcity.Text,
                    h_about = habout.Text,
                    h_photo = path,
                    h_date_time = DateTime.Now.ToString(),
                    h_hakkimid=hid.ToString(),
                };
                db.tbl_hospitalregs.InsertOnSubmit(th);
                db.SubmitChanges();
                    con.Open();
                    cmd = new SqlCommand("insert into tbl_hakkimid values('" + hid.ToString() + "','HOS')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    //Email(hemail.Text, "Your hospital is added successfully with Hakkeem and Login id is " + hregno.Text + " ,password is " + "BD" + hregno.Text + " please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com");
                    ////  string msg = "Your hospital is added successfully with Hakkeem and Login id is " + hregno.Text + " ,password is " + "BD" + hregno.Text + " please follow this link to access your session on Hakkeem " + "http://www.hakkeem.com";
                    ////  mail.mail(hemail.Text, msg, "Hospital registration");
                    string passsword = "HH" + hregno.Text;
                    Email_To_NewHospital(hemail.Text, hregno.Text, passsword);
                    string pno = "";
                    pno = hcontact.Text.Replace(" ", "");
                    string ph = "+966" + pno.ToString();
                    
                    ob1.Message(ph, " Thank you for registering with Hakkeem.Your Register No: " + hregno.Text + " and password is:" + passsword);


                    string ph1 = "+91" + pno.ToString();

                    ob1.Message(ph1, " Thank you for registering with Hakkeem.Your Register No: " + hregno.Text + " and password is:" + passsword);
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('Successfully create hospital')</Script>");
                    //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Succesfully create hospital');window.location='Create hospital.aspx';</Script>");
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                    Label1.Text = "Succesfully create hospital";
                    //}
                    //else
                    //{
                    //    Label1.Text = "إنشاء المستشفى بنجاح";
                    //}
                    //ModalPopupExtender1.Show();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
        }
    }
    public bool Email_To_ApproveHospital(string email)
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
            messagestr = messagestr + "<tr><td style='text-align:center;font-size:20px;padding:20px 20px;background-color: #fff;color:#4aa9af;font-weight:bold'>ACCOUNT ACTIVATION</td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px; color:#4aa9af;'>";
            messagestr = messagestr + "Welcome to Hakkeem your account approved by Hakkeem authority.<br>Kindly log into your account and complete your profile to start on Hakkeem.";
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
            mail.Subject = "Hakkeem Account Activation";
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
    public bool Email_To_NewHospital(string email, string regno, string password)
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
            messagestr = messagestr + "We're so happy you've joined Hakkeem family.<br><br> We founded Hakkeem because we wanted to create a trustworthy and inspiring platform for you to help arrange your timing and availability in an easy way,also to help you increasing your income.<br> Also we would like to Thank you for joining us and helping us to enchance the would.<br><strong> Here is your Register No:" + regno + " and Password is: " + password + "</strong><br> Your sincerely,<br> Hakkeem Team.";
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
    //public bool Email_To_NewHospital(string email, string regno, string password)
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
    //        string btnpath = "http://hakkeem.com/Index/Hospita%20Login.aspx";
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
    //        messagestr = messagestr + "Here is your Register No:" + regno + " and Password is: "+password+"";
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
    protected void BtnDownload_Click(object sender, EventArgs e)
    {
        //Response.ContentType = "Application/pdf";
        //Response.AppendHeader("Content-Disposition", "attachment; filename=help.pdf");
        //Response.TransmitFile(Server.MapPath(filePath));
        //Response.End();
        Response.Redirect(filePath);
    }
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Session["Agrement"].ToString());
    //}
    protected void LinkCreate_Click(object sender, EventArgs e)
    {
        LinkCreate.Visible = false;
        Session["hosptal_Id"] = null;
        //haddrs.Text = habout.Text = hcity.Text = hcontact.Text = hemail.Text = Hname.Text = hregno.Text = hzipcode.Text = "";
        HyperLink1.Visible = false;
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/BookDoc Admin/Create hospital.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/BookDoc Admin/Create hospital.aspx?l=ar-EG");
        //}
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/BookDoc Admin/Create hospital.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/BookDoc Admin/Create hospital.aspx?l=ar-EG");
        //}
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/BookDoc Admin/Create hospital.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/BookDoc Admin/Create hospital.aspx?l=ar-EG");
        //}
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        if (Session["hosptal_Id"] != null)

        {
            //con.Open();
            //String email = "";
            //string name = "";
            //SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_hospitalreg where h_id='" + Session["hosptal_Id"] + "'", con);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    email = dt.Rows[0]["h_email"].ToString();
            //    name = dt.Rows[0]["h_name"].ToString();
            //}
            //EmailString(email, "sorry!!  " + name + " your account removed from Hakkeem");
            //SqlCommand cmd = new SqlCommand("Delete from tbl_hospitalreg where h_id='" + Session["hosptal_Id"] + "'", con);
            //cmd.ExecuteNonQuery();
            //con.Close();
            Session["hosptal_Id"] = null;
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/BookDoc Admin/HospitalRequest.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/BookDoc Admin/HospitalRequest.aspx?l=ar-EG");
            //}
        }
        else
        {
            Hname.Text = "";
            hregno.Text = "";
            haddrs.Text = "";
            hcontact.Text = "";
            hemail.Text = "";
            habout.Text = "";
            Session["hosptal_Id"] = null;
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/BookDoc Admin/Create hospital.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/BookDoc Admin/Create hospital.aspx?l=ar-EG");
            //}
        }
    }
    public void EmailString(string email, string msg)
    {



        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(email);
        mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
        mail.Subject = "Account Deletion";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = msg;
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