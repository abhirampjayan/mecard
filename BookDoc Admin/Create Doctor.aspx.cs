using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

public partial class BookDoc_Admin_Create_Doctor : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    MailMessage Email = new MailMessage();
    secure obj = new secure();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    SqlCommand cmd;
    SMS ob1 = new SMS();
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
        if (!IsPostBack)
        {
            Doctor();
            speciality();
            City();
        }
    }

    protected void City()
    {
        var Query = from item in db.tbl_cities select item.City;
        dl_city.DataSource = Query;
        dl_city.DataBind();
    }

    protected void speciality()
    {
        var Query = from item in db.tbl_specialities select item.Specialities;
        DropDownList1.DataSource = Query;
        DropDownList1.DataBind();
    }


    public void Doctor()
    {
        try
        {
            if (Session["d_id"] != null)
            {
                var Query = from item in db.tbl_doctors where item.d_id == int.Parse(Session["d_id"].ToString()) select item;
                foreach (var ss in Query)
                {
                    string[] a = ss.d_name.Split(' ');
                    try
                    {
                        Fname.Text = a[0].ToString();
                        Lname.Text = a[1].ToString();
                       
                    }
                    catch (Exception ex)
                    {
                        Lname.Text = "";

                    }
                    email.Text = obj.DecryptString(ss.d_email);

                    string pp = obj.DecryptString(ss.d_contact);
                    //  phone.Text = pp.Substring(4, 9);
                    phone.Text = pp.ToString();
                DropDownList1.Items.Insert(0, ss.d_specialties);
                dl_city.Items.Insert(0, ss.d_city);
                //DropDownList1.SelectedItem.Text = ss.d_specialties;
                //dl_city.SelectedItem.Text = ss.d_city;
                //zipcode.Text = ss.d_location;
                //dcity.Text = ss.d_city;
                if (ss.d_agreement != null)
                    {
                        HyperLink1.NavigateUrl = ss.d_agreement.ToString();
                        Panel1.Visible = false;
                    }
                    else
                    {
                        HyperLink1.Visible = false;
                        Panel1.Visible = true;
                    }

                }
            }
            else
            {
                Session["d_id"] = null;
                Session["d_id"] = "";
                HyperLink1.Visible = false;
                Panel1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            HyperLink1.Visible = false;
            Panel1.Visible = true;

        }
    }

    private static DateTime ConvertToEngCal(string hijri)
    {
        CultureInfo arSA = new CultureInfo("ar-SA");
        arSA.DateTimeFormat.Calendar = new HijriCalendar();
        return DateTime.ParseExact(hijri, "dd/MM/yy", arSA);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Session["d_id"] != null && Session["d_id"].ToString() != "")
        {
            var Query111 = from item in db.tbl_doctors where item.d_id_number == dnumber.Text select item;
            if (Query111.Count() > 0)
            {
                //Label2.Text = "Doctor identification number is expired...!";
                //ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number is exist...!')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم تعريف الطبيب موجود')</Script>");
                //}
            }
            else
            {
                DateTime currentDate = DateTime.Now;
                //string dd = System.DateTime.Now.ToString();
                //string[] dd1 = new string[7];

                //dd1 = dd.Split('-');
                //string[] dd2 = new string[7];
                //dd2 = dd1[2].Split(' ');

                ////    Response.Write("<br>" + dd2[0]);
                //string dttime = System.DateTime.Now.ToString();

                //if (dd2[0] == "2018" || dd2[0] == "2019" || dd2[0] == "2020")
                //{

                //}
                //else
                //{
                //    dttime = ConvertToEngCal(dttime).ToString("dd-MM-yyyy");

                //    currentDate =Convert.ToDateTime(dttime);

                //}











                DateTime compareDate = Convert.ToDateTime(this.dexpire.Text.Trim(), new CultureInfo("en-GB"));
                if (currentDate >= compareDate)
                {
                    //Label2.Text = "Doctor identification number is expired...!";
                    //ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number is expired...!')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('انتهت صلاحية رقم تعريف الطبيب')</Script>");

                    //}
                }
                else
                {

                    if ((DateTime.Parse(compareDate.ToShortDateString()) - DateTime.Parse(DateTime.Now.ToShortDateString())).TotalDays < 30)
                    {
                        double i = (DateTime.Parse(compareDate.ToShortDateString()) - DateTime.Parse(DateTime.Now.ToShortDateString())).TotalDays;
                        if (i == 30)
                        {
                            string email = "";
                            string ph = "";
                            var Query = from item in db.tbl_doctors where item.d_id == int.Parse(Session["d_id"].ToString()) select item;
                            Random rd = new Random();
                            int pswd = rd.Next(000000, 999999);
                            if (FileUpload2.HasFile)
                            {
                                string extn = Path.GetExtension(FileUpload2.FileName).ToLower();
                                if (extn == ".pdf" || extn == ".jpg")
                                {
                                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName));
                                    foreach (var ss in Query)
                                    {
                                        email = obj.DecryptString(ss.d_email);
                                        ss.d_id_number = dnumber.Text;
                                        ss.d_status = 1;
                                        ph=obj.DecryptString(ss.d_contact);
                                        //ss.d_password = pswd.ToString();
                                        ss.d_id_expire = dexpire.Text;
                                        ss.d_agreement = "~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName;
                                    }
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    //Label2.Text = "Upload only pdf or jpg files";
                                    //ModalPopupExtender2.Show();
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf or jpg files')</Script>");
                                    //}
                                    //else
                                    //{
                                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف أو جبغ فقط')</Script>");
                                    //}
                                }
                            }
                            else
                            {
                                foreach (var ss in Query)
                                {
                                    email = obj.DecryptString(ss.d_email);
                                    ss.d_id_number = dnumber.Text;
                                    ss.d_status = 1;
                                    //ss.d_password = pswd.ToString();
                                    ss.d_id_expire = dexpire.Text;
                                    ph = obj.DecryptString(ss.d_contact);
                                }
                                db.SubmitChanges();
                            }
                            string message = "";
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                message = "Doctor identification number is expired with in one month...!";
                            //}
                            //else
                            //{
                            //    message = "انتهت صلاحية رقم تعريف الطبيب في شهر واحد";
                            //}
                            //string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.hakkeem.com" + " and your loging password is " + pswd;
                            /// string msg = "Dear doctor, your Hakkeem account is approved by Hakkeem authority." + "<p>Thank you. Hakkeem Team</p>";
                            ///   Email.mail(email, msg, "Doctor registration");
                            string pno = "+966" + ph.ToString();
                            ob1.Message(pno.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");
                            string pno1 = "+91" + ph.ToString();
                            ob1.Message(pno1.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");
                            Email_To_ApproveDoctor(email);
                          
                            //RegisterStartupScript("", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "')</Script>");
                            //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>swal('Successfully created doctor')</Script>");
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                Label1.Text = "Successfully created doctor and " + message;
                            //}
                            //else
                            //{
                            //    Label1.Text = "تم إنشاء الطبيب بنجاح " + message;
                            //}
                            //ModalPopupExtender1.Show();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                            //RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number is expired with in one month...!')</Script>");
                        }
                        else
                        {
                            string email = "";
                            string ph = "";
                            var Query = from item in db.tbl_doctors where item.d_id == int.Parse(Session["d_id"].ToString()) select item;
                            Random rd = new Random();
                            int pswd = rd.Next(000000, 999999);
                            if (FileUpload2.HasFile)
                            {
                                string extn = Path.GetExtension(FileUpload2.FileName).ToLower();
                                if (extn == ".pdf" || extn == ".jpg")
                                {
                                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName));
                                    foreach (var ss in Query)
                                    {
                                        email = obj.DecryptString(ss.d_email);
                                        ss.d_id_number = dnumber.Text;
                                        ss.d_status = 1;
                                        //ss.d_password = pswd.ToString();
                                        ss.d_id_expire = dexpire.Text;
                                        ph = obj.DecryptString(ss.d_contact);
                                        ss.d_agreement = "~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName;
                                    }
                                    db.SubmitChanges();
                                    string message = "";
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        message = "Doctor identification number is expired with in " + i + " days...!";
                                    //}
                                    //else
                                    //{
                                    //    message = "انتهت صلاحية رقم تعريف الطبيب مع " + i + " أيام";
                                    //}

                                    //string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.goldenetqan.com" + " and your loging password is " + pswd;
                                    ///  string msg = "Dear doctor, your Hakkeem account is approved by Hakkeem authority." + "<p>Thank you. Hakkeem Team</p>";
                                    ////   Email.mail(email, msg, "Doctor registration");
                                    string pno = "+966" + ph.ToString();
                                    ob1.Message(pno.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");
                                    string pno1 = "+91" + ph.ToString();
                                    ob1.Message(pno1.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");
                                    Email_To_ApproveDoctor(email);
                                    //string pno = "+966" + ph.ToString();
                                    //ob1.Message(pno.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");
                                    ////ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "');window.location='Doctor request.aspx';</Script>");
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        Label1.Text = "Successfully created doctor and " + message;
                                    //}
                                    //else
                                    //{
                                    //    Label1.Text = "تم إنشاء الطبيب بنجاح " + message;
                                    //}
                                    //ModalPopupExtender1.Show();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();

                                }
                                else
                                {
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf or jpg files')</Script>");
                                    //}
                                    //else
                                    //{
                                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف أو جبغ فقط')</Script>");

                                    //}
                                    //Label2.Text = "Upload only pdf or jpg files";
                                    //ModalPopupExtender2.Show();
                                }
                            }
                            else
                            {
                                foreach (var ss in Query)
                                {
                                    email = obj.DecryptString(ss.d_email);
                                    ss.d_id_number = dnumber.Text;
                                    ph = obj.DecryptString(ss.d_contact);
                                    ss.d_status = 1;
                                    //ss.d_password = pswd.ToString();
                                    ss.d_id_expire = dexpire.Text;
                                }
                                db.SubmitChanges();
                                string message = "";
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                    message = "Doctor identification number is expired with in " + i + " days...!";
                                //}
                                //else
                                //{
                                //    message = "انتهت صلاحية رقم تعريف الطبيب مع " + i + " أيام";
                                //}

                                //string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.hakkeem.com" + " and your loging password is " + pswd;
                                ///   string msg = "Dear doctor, your Hakkeem account is approved by Hakkeem authority." + "<p>Thank you. Hakkeem Team</p>";
                                ////   Email.mail(email, msg, "Doctor registration");
                                string pno = "+966" + ph.ToString();
                                ob1.Message(pno.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");

                                string pno1 = "+91" + ph.ToString();
                                ob1.Message(pno1.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");

                                Email_To_ApproveDoctor(email);
                               
                                //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "');window.location='Doctor request.aspx';</Script>");
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                    Label1.Text = "Successfully created doctor and " + message;
                                //}
                                //else
                                //{
                                //    Label1.Text = "تم إنشاء الطبيب بنجاح " + message;
                                //}
                                //ModalPopupExtender1.Show();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                            //string message = "Doctor identification number is expired with in " + i + " days...!";
                            //string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.goldenetqan.com" + " and your loging password is " + pswd;
                            //Email.mail(email, msg,"Doctor registration");
                            //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "');window.location='Doctor request.aspx';</Script>");
                            //RegisterStartupScript("", "<Script Language=JavaScript>alert('" + message + "')</Script>");
                        }
                    }
                    else
                    {
                        string email = "";
                        string ph = "";
                        var Query = from item in db.tbl_doctors where item.d_id == int.Parse(Session["d_id"].ToString()) select item;
                        Random rd = new Random();
                        int pswd = rd.Next(000000, 999999);

                        if (FileUpload2.HasFile)
                        {
                            string extn = Path.GetExtension(FileUpload2.FileName).ToLower();
                            if (extn == ".pdf" || extn == ".jpg")
                            {
                                FileUpload2.PostedFile.SaveAs(Server.MapPath("~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName));
                                foreach (var ss in Query)
                                {
                                    email = obj.DecryptString(ss.d_email);
                                    ss.d_id_number = dnumber.Text;
                                    ph = obj.DecryptString(ss.d_contact);
                                    ss.d_status = 1;
                                    //ss.d_password = pswd.ToString();
                                    ss.d_id_expire = dexpire.Text;
                                    ss.d_agreement = "~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName;
                                }
                                db.SubmitChanges();
                                //string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.hakkeem.com" + " and your loging password is " + pswd;
                                /////        string msg = "Dear doctor, your Hakkeem account is approved by Hakkeem authority." + "<p>Thank you. Hakkeem Team</p>";
                                //////       Email.mail(email, msg, "Doctor registration");
                                string pno = "+966" + ph.ToString();
                                ob1.Message(pno.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");
                                string pno1 = "+91" + ph.ToString();
                                ob1.Message(pno1.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");
                                Email_To_ApproveDoctor(email);
                       
                                //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor ');window.location='Doctor request.aspx';</Script>");
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                    Label1.Text = "Successfully created doctor.";
                                //}
                                //else
                                //{
                                //    Label1.Text = "تم إنشاء الطبيب بنجاح.";
                                //}
                                //ModalPopupExtender1.Show();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }
                            else
                            {
                                //Label2.Text = "Upload only pdf or jpg files";
                                //ModalPopupExtender2.Show();
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf or jpg files')</Script>");
                                //}
                                //else
                                //{
                                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف أو جبغ فقط')</Script>");

                                //}
                            }
                        }
                        else
                        {
                            foreach (var ss in Query)
                            {
                                email = obj.DecryptString(ss.d_email);
                                ph = obj.DecryptString(ss.d_contact);
                                ss.d_id_number = dnumber.Text;
                                ss.d_status = 1;
                                //ss.d_password = pswd.ToString();
                                ss.d_id_expire = dexpire.Text;
                            }
                            db.SubmitChanges();
                            //string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.hakkeem.com" + " and your loging password is " + pswd;
                            //////    string msg = "Dear doctor, your Hakkeem account is approved by Hakkeem authority." + "<p>Thank you. Hakkeem Team</p>";
                            //////  Email.mail(email, msg, "Doctor registration");
                            string pno = "+966" + ph.ToString();
                            ob1.Message(pno.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");
                            string pno1 = "+91" + ph.ToString();
                            ob1.Message(pno1.ToString(), "Dear doctor, your Hakkeem account is approved by Hakkeem authority.");

                            Email_To_ApproveDoctor(email);
                           
                            //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor ');window.location='Doctor request.aspx';</Script>");
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                Label1.Text = "Successfully created doctor";
                            //}
                            //else
                            //{
                            //    Label1.Text = "تم إنشاء الطبيب بنجاح";
                            //}
                            //ModalPopupExtender1.Show();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }


                        //string message = "Doctor identification number is expired with in " + i + " days...!";
                        //string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.goldenetqan.com" + " and your loging password is " + pswd;
                        //Email.mail(email, msg, "Doctor registration");
                        //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor ');window.location='Doctor request.aspx';</Script>");

                    }

                }
            }
            Session["d_id"] = null;
        }
        ///////////////////////////////     new doctor  ///////////////////////////
        else
        {
            var doc = from item in db.tbl_hakkimIDs where item.user_type == "DOC" select item.id;
            Int64 maxid;
            try
            {
                maxid = Convert.ToInt64(doc.Max());
            }
            catch (Exception ex)
            {
                maxid = 0;
            }
            maxid = maxid + 1;
            string hid = "HD_000" + maxid.ToString();

            //Random rd = new Random();
            //int pswd = rd.Next(000000, 999999);
           


            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            string pass = finalString.ToString();
            string mpass = pass;
            pass = obj.EnryptString(pass);

            //var Query1 = from item in db.tbl_doctors where item.d_contact == obj.EnryptString(phone.Text)select item;
            //if (Query1.Count() > 0)
            //{
            //    if (Session["Language"].ToString() == "Auto")
            //    {
            //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Phone Number is already exist')</Script>");
            //        phone.Text = "";
            //        phone.Focus();
            //    }
            //    else
            //    {
            //        RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم الهاتف موجود بالفعل')</Script>");
            //        phone.Text = "";
            //        phone.Focus();
            //    }
                
            //}

            var Query = from item in db.tbl_doctors where item.d_email == obj.EnryptString(email.Text) && (item.d_status == 1 || item.d_status == 0)  select item;
            if (Query.Count() > 0)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor exist')</Script>");
                    email.Text = "";
                    email.Focus();
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('طبيب موجود')</Script>");
                //}
                //Label2.Text = "Doctor exist";
                //ModalPopupExtender2.Show();
            }
            else
            {
                var Query12 = from item in db.tbl_doctors where item.d_id_number == dnumber.Text select item;
                if (Query12.Count() > 0)
                {
                    //Label2.Text = "Doctor identification number exist";
                    //ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number exist')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('رقم تعريف الطبيب موجود')</Script>");

                    //}
                }

                else
                {
                    DateTime currentDate = DateTime.Now;
                    DateTime compareDate = Convert.ToDateTime(this.dexpire.Text.Trim(), new CultureInfo("en-GB"));
                    if (currentDate >= compareDate)
                    {
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number is expired...!')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('انتهت صلاحية رقم تعريف الطبيب')</Script>");

                        //}
                        //Label2.Text = "Doctor identification number is expired...!";
                        //ModalPopupExtender2.Show();
                    }
                    else
                    {

                        if ((DateTime.Parse(compareDate.ToShortDateString()) - DateTime.Parse(DateTime.Now.ToShortDateString())).TotalDays < 30)
                        {
                            double i = (DateTime.Parse(compareDate.ToShortDateString()) - DateTime.Parse(DateTime.Now.ToShortDateString())).TotalDays;
                            if (i == 30)
                            {
                               

                                if (FileUpload2.HasFile)
                                {
                                    string extn = Path.GetExtension(FileUpload2.FileName).ToLower();
                                    if (extn == ".pdf" || extn == ".jpg")
                                    {
                                        FileUpload2.PostedFile.SaveAs(Server.MapPath("~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName));

                                        tbl_doctor td = new tbl_doctor()
                                        {
                                            d_name = Fname.Text + " " + Lname.Text,
                                            d_email = obj.EnryptString(email.Text),
                                            d_contact = obj.EnryptString(phone.Text),
                                            d_specialties = DropDownList1.SelectedItem.Text,
                                            d_city = dl_city.SelectedItem.Text,
                                            //d_location = zipcode.Text,
                                            d_id_number = dnumber.Text,
                                            d_status = 1,
                                            d_date_time = DateTime.Now.ToString(),
                                            d_password = pass,
                                            //d_city = dcity.Text,
                                            d_id_expire = dexpire.Text,
                                            d_agreement = "~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName,
                                            d_photo = "~/Doctorimages/doctor.png",
                                            d_hakkimid=hid,
                                            
                                        };
                                        db.tbl_doctors.InsertOnSubmit(td);
                                        db.SubmitChanges();
                                        tbl_hakkimID hd = new tbl_hakkimID()
                                        {
                                            hakkim_ID = hid,
                                            user_type = "DOC",
                                        };
                                        db.tbl_hakkimIDs.InsertOnSubmit(hd);
                                        db.SubmitChanges();

                                        tbl_rating tr = new tbl_rating()
                                        {
                                            rate_bm = 0,
                                            rate_service = 0,
                                            rate_wt = 0,
                                            rate = 0,
                                            d_id = hid,
                                        };
                                        db.tbl_ratings.InsertOnSubmit(tr);
                                        db.SubmitChanges();

                                        con.Open();
                                        cmd = new SqlCommand("insert into tbl_ratefinal values('" + hid.ToString() + "','0')", con);
                                        cmd.ExecuteNonQuery();
                                        con.Close();

                                        //tbl_rateFinal rf = new tbl_rateFinal()
                                        //{
                                        //    hakkim_id = hid,
                                        //    rate = "0",
                                        //};
                                        //db.tbl_rateFinals.InsertOnSubmit(rf);
                                        //db.SubmitChanges();

                                        string message = "";
                                        //if (Session["Language"].ToString() == "Auto")
                                        //{
                                            message = "Doctor identification number is expired with in " + i + " days...!";
                                        //}
                                        //else
                                        //{
                                        //    message = "انتهت صلاحية رقم تعريف الطبيب مع " + i + "أيام";
                                        //}

                                        ///  string msg = "Successfully created your Hakkeem account, Using following link to signin " + "http://www.hakkeem.com" + " and your loging hakkeem id is "+hid+" and password is " + mpass;
                                        ////  Email.mail(email.Text, msg, "Doctor registration");
                                        string pno = "";
                                        pno = phone.Text.Replace(" ", "");
                                        string ph = "+966" + pno.ToString();

                                        ob1.Message(ph, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);


                                        
                                        string ph1 = "+91" + pno.ToString();

                                        ob1.Message(ph1, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);

                                        Email_To_NewDoctor(email.Text, hid, mpass);
                                       
                                        //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>swal('Successfully created doctor and " + message + "');window.location='Doctor request.aspx';</Script>");
                                        //if (Session["Language"].ToString() == "Auto")
                                        //{
                                            Label1.Text = "Successfully created doctor and " + message;
                                        //}
                                        //else
                                        //{
                                        //    Label1.Text = "تم إنشاء الطبيب بنجاح " + message;
                                        //}
                                        //ModalPopupExtender1.Show();
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                        upModal.Update();
                                    }
                                    else
                                    {
                                        //Label2.Text = "Upload only pdf or jpg files!";
                                        //ModalPopupExtender2.Show();
                                        //if (Session["Language"].ToString() == "Auto")
                                        //{
                                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf files')</Script>");
                                        //}
                                        //else
                                        //{
                                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف فقط')</Script>");

                                        //}
                                    }
                                }
                                else
                                {
                                    tbl_doctor td = new tbl_doctor()
                                    {
                                        d_name = Fname.Text + " " + Lname.Text,
                                        d_email = obj.EnryptString(email.Text),
                                        d_contact = obj.EnryptString(phone.Text),
                                        d_specialties = DropDownList1.SelectedItem.Text,
                                        d_city = dl_city.SelectedItem.Text,
                                        //d_location = zipcode.Text,
                                        d_id_number = dnumber.Text,
                                        d_status = 1,
                                        d_date_time = DateTime.Now.ToString(),
                                        d_password = pass,
                                        //d_city = dcity.Text,
                                        d_id_expire = dexpire.Text,
                                        d_photo = "~/Doctorimages/doctor.png",
                                        d_hakkimid = hid,
                                    };
                                    db.tbl_doctors.InsertOnSubmit(td);
                                    db.SubmitChanges();
                                    tbl_hakkimID hd = new tbl_hakkimID()
                                    {
                                        hakkim_ID = hid,
                                        user_type = "DOC",
                                    };
                                    db.tbl_hakkimIDs.InsertOnSubmit(hd);
                                    db.SubmitChanges();
                                    tbl_rating tr = new tbl_rating()
                                    {
                                        rate_bm = 0,
                                        rate_service = 0,
                                        rate_wt = 0,
                                        rate = 0,
                                        d_id = hid,
                                    };
                                    db.tbl_ratings.InsertOnSubmit(tr);
                                    db.SubmitChanges();
                                    //tbl_rateFinal rf = new tbl_rateFinal()
                                    //{
                                    //    hakkim_id = hid,
                                    //    rate = "0",
                                    //};
                                    //db.tbl_rateFinals.InsertOnSubmit(rf);
                                    //db.SubmitChanges();
                                    con.Open();
                                    cmd = new SqlCommand("insert into tbl_ratefinal values('" + hid.ToString() + "','0')", con);
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                    string message = "";
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        message = "Doctor identification number is expired with in " + i + " days...!";

                                    //}
                                    //else
                                    //{
                                    //    message = "انتهت صلاحية رقم تعريف الطبيب مع " + i + " أيام";

                                    //}
                                    /////  string msg = "Successfully created your Hakkeem account, Using following link to signin " + "http://www.hakkeem.com" + " and your loging hakkeem id is " + hid + " and password is " + mpass;
                                    // ///  Email.mail(email.Text, msg, "Doctor registration");

                                    string pno = "";
                                    pno = phone.Text.Replace(" ", "");
                                    string ph = "+966" + pno.ToString();

                                    ob1.Message(ph, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);



                                   
                                    string ph1 = "+91" + pno.ToString();

                                    ob1.Message(ph1, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);
                                    Email_To_NewDoctor(email.Text, hid, mpass);
                                    
                                    //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "');window.location='Doctor request.aspx';</Script>");
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        Label1.Text = "Successfully created doctor and " + message;
                                    //}
                                    //else
                                    //{
                                    //    Label1.Text = "تم إنشاء الطبيب بنجاح" + message;
                                    //}
                                    //ModalPopupExtender1.Show();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();
                                }



                            }
                            else
                            {
                                //Random rd = new Random();
                                //int pswd = rd.Next(000000, 999999);

                                if (FileUpload2.HasFile)
                                {
                                    string extn = Path.GetExtension(FileUpload2.FileName).ToLower();
                                    if (extn == ".pdf" || extn == ".jpg")
                                    {
                                        FileUpload2.PostedFile.SaveAs(Server.MapPath("~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName));
                                        tbl_doctor td = new tbl_doctor()
                                        {
                                            d_name = Fname.Text + " " + Lname.Text,
                                            d_email = obj.EnryptString(email.Text),
                                            d_contact = obj.EnryptString(phone.Text),
                                            d_specialties = DropDownList1.SelectedItem.Text,
                                            d_city = dl_city.SelectedItem.Text,
                                            //d_location = zipcode.Text,
                                            d_id_number = dnumber.Text,
                                            d_status = 1,
                                            d_date_time = DateTime.Now.ToString(),
                                            d_password = pass,
                                            //d_city = dcity.Text,
                                            d_id_expire = dexpire.Text,
                                            d_agreement = "~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName,
                                            d_photo = "~/Doctorimages/doctor.png",
                                            d_hakkimid=hid,
                                        };
                                        db.tbl_doctors.InsertOnSubmit(td);
                                        db.SubmitChanges();
                                        tbl_hakkimID hd = new tbl_hakkimID()
                                        {
                                            hakkim_ID = hid,
                                            user_type = "DOC",
                                        };
                                        db.tbl_hakkimIDs.InsertOnSubmit(hd);
                                        db.SubmitChanges();

                                        tbl_rating tr = new tbl_rating()
                                        {
                                            rate_bm = 0,
                                            rate_service = 0,
                                            rate_wt = 0,
                                            rate = 0,
                                            d_id = hid,
                                        };
                                        db.tbl_ratings.InsertOnSubmit(tr);
                                        db.SubmitChanges();

                                        //tbl_rateFinal rf = new tbl_rateFinal()
                                        //{
                                        //    hakkim_id = hid,
                                        //    rate = "0",
                                        //};
                                        //db.tbl_rateFinals.InsertOnSubmit(rf);
                                        //db.SubmitChanges();

                                        con.Open();
                                        cmd = new SqlCommand("insert into tbl_ratefinal values('" + hid.ToString() + "','0')", con);
                                        cmd.ExecuteNonQuery();
                                        con.Close();

                                        string message = "";
                                        //if (Session["Language"].ToString() == "Auto")
                                        //{
                                            message = "Doctor identification number is expired with in " + i + " days...!";

                                        //}
                                        //else
                                        //{
                                        //    message = "انتهت صلاحية رقم تعريف الطبيب مع " + i + " أيام";

                                        //}
                                        string pno = "";
                                        pno = phone.Text.Replace(" ", "");
                                        string ph = "+966" + pno.ToString();

                                        ob1.Message(ph, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);

                                        string ph1 = "+91" + pno.ToString();

                                        ob1.Message(ph1, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);

                                        Email_To_NewDoctor(email.Text, hid, mpass);
                                       
                                        ////  string msg = "Successfully created your Hakkeem account, Using following link to signin " + "http://www.hakkeem.com" + " and your loging hakkeem id is " + hid + " and password is " + mpass;
                                        ////   Email.mail(email.Text, msg, "Doctor registration");
                                        //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "');window.location='Doctor request.aspx';</Script>");
                                        //if (Session["Language"].ToString() == "Auto")
                                        //{
                                            Label1.Text = "Successfully created doctor and " + message;
                                        //}
                                        //else
                                        //{
                                        //    Label1.Text = "تم إنشاء الطبيب بنجاح " + message;
                                        //}
                                        //ModalPopupExtender1.Show();
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                        upModal.Update();
                                    }
                                    else
                                    {
                                        //Label2.Text = "Upload only pdf or jpg files!";
                                        //ModalPopupExtender2.Show();
                                        //if (Session["Language"].ToString() == "Auto")
                                        //{
                                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf or jpg files')</Script>");
                                        //}
                                        //else
                                        //{
                                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف أو جبغ فقط')</Script>");

                                        //}
                                    }
                                }
                                else
                                {
                                    tbl_doctor td = new tbl_doctor()
                                    {
                                        d_name = Fname.Text + " " + Lname.Text,
                                        d_email = obj.EnryptString(email.Text),
                                        d_contact = obj.EnryptString(phone.Text),
                                        d_specialties = DropDownList1.SelectedItem.Text,
                                        d_city = dl_city.SelectedItem.Text,
                                        //d_location = zipcode.Text,
                                        d_id_number = dnumber.Text,
                                        d_status = 1,
                                        d_date_time = DateTime.Now.ToString(),
                                        d_password = pass,
                                        //d_city = dcity.Text,
                                        d_id_expire = dexpire.Text,
                                        d_photo = "~/Doctorimages/doctor.png",
                                        d_hakkimid=hid,
                                    };
                                    db.tbl_doctors.InsertOnSubmit(td);
                                    db.SubmitChanges();
                                    tbl_hakkimID hd = new tbl_hakkimID()
                                    {
                                        hakkim_ID = hid,
                                        user_type = "DOC",
                                    };
                                    db.tbl_hakkimIDs.InsertOnSubmit(hd);
                                    db.SubmitChanges();

                                    tbl_rating tr = new tbl_rating()
                                    {
                                        rate_bm = 0,
                                        rate_service = 0,
                                        rate_wt = 0,
                                        rate = 0,
                                        d_id = hid,
                                    };
                                    db.tbl_ratings.InsertOnSubmit(tr);
                                    db.SubmitChanges();

                                    //tbl_rateFinal rf = new tbl_rateFinal()
                                    //{
                                    //    hakkim_id = hid,
                                    //    rate = "0",
                                    //};
                                    //db.tbl_rateFinals.InsertOnSubmit(rf);
                                    //db.SubmitChanges();

                                    con.Open();
                                    cmd = new SqlCommand("insert into tbl_ratefinal values('" + hid.ToString() + "','0')", con);
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                    string message = "";
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                       message = "Doctor identification number is expired with in " + i + " days...!";

                                    //}
                                    //else
                                    //{
                                    //    message = "انتهت صلاحية رقم تعريف الطبيب مع " + i + " أيام";

                                    //}



                                    string pno = "";
                                    pno = phone.Text.Replace(" ", "");
                                    string ph = "+966" + pno.ToString();

                                    ob1.Message(ph, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);


                                   
                                    string ph1 = "+91" + pno.ToString();

                                    ob1.Message(ph1, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);




                                    Email_To_NewDoctor(email.Text, hid, mpass);
                                    
                                    ///// string msg = "Successfully created your Hakkeem account, Using following link to signin " + "http://www.hakkeem.com" + " and your loging hakkeem id is " + hid + " and password is " + mpass;
                                    /////  Email.mail(email.Text, msg, "Doctor registration");
                                    //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor and " + message + "');window.location='Doctor request.aspx';</Script>");
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        Label1.Text = "Successfully created doctor and " + message;
                                    //}
                                    //else
                                    //{
                                    //    Label1.Text = "تم إنشاء الطبيب بنجاح " + message;
                                    //}
                                    //ModalPopupExtender1.Show();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();
                                }




                            }

                        }
                        else
                        {
                            //Random rd = new Random();
                            //int pswd = rd.Next(000000, 999999);

                            if (FileUpload2.HasFile)
                            {
                                string extn = Path.GetExtension(FileUpload2.FileName).ToLower();
                                if (extn == ".pdf" || extn == ".jpg")
                                {
                                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName));
                                    tbl_doctor td = new tbl_doctor()
                                    {
                                        d_name = Fname.Text + " " + Lname.Text,
                                        d_email = obj.EnryptString(email.Text),
                                        d_contact = obj.EnryptString(phone.Text),
                                        d_specialties = DropDownList1.SelectedItem.Text,
                                        d_city = dl_city.SelectedItem.Text,
                                        //d_location = zipcode.Text,
                                        d_id_number = dnumber.Text,
                                        d_status = 1,
                                        d_date_time = DateTime.Now.ToString(),
                                        d_password = pass,
                                        //d_city = dcity.Text,
                                        d_id_expire = dexpire.Text,
                                        d_agreement = "~/DoctorAgreements/" + Session["d_id"] + FileUpload2.FileName,
                                        d_photo = "~/Doctorimages/doctor.png",
                                        d_hakkimid = hid,
                                    };
                                    db.tbl_doctors.InsertOnSubmit(td);
                                    db.SubmitChanges();
                                    tbl_hakkimID hd = new tbl_hakkimID()
                                    {
                                        hakkim_ID = hid,
                                        user_type = "DOC",
                                    };
                                    db.tbl_hakkimIDs.InsertOnSubmit(hd);
                                    db.SubmitChanges();

                                    tbl_rating tr = new tbl_rating()
                                    {
                                        rate_bm = 0,
                                        rate_service = 0,
                                        rate_wt = 0,
                                        rate = 0,
                                        d_id = hid,
                                    };
                                    db.tbl_ratings.InsertOnSubmit(tr);
                                    db.SubmitChanges();

                                    //tbl_rateFinal rf = new tbl_rateFinal()
                                    //{
                                    //    hakkim_id = hid,
                                    //    rate = "0",
                                    //};
                                    //db.tbl_rateFinals.InsertOnSubmit(rf);
                                    //db.SubmitChanges();

                                    con.Open();
                                    cmd = new SqlCommand("insert into tbl_ratefinal values('" + hid.ToString() + "','0')", con);
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                    string pno = "";
                                    pno = phone.Text.Replace(" ", "");
                                    string ph = "+966" + pno.ToString();

                                    ob1.Message(ph, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);

                                  
                                    string ph1 = "+91" + pno.ToString();

                                    ob1.Message(ph1, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);
                                    Email_To_NewDoctor(email.Text, hid, mpass);
                                   
                                    //string message = "Doctor identification number is expired with in " + i + " days...!";
                                    /////   string msg = "Successfully created your Hakkeem account, Using following link to signin " + "http://www.hakkeem.com" + " and your loging hakkeem id is " + hid + " and password is " + mpass;
                                    ////  Email.mail(email.Text, msg, "Doctor registration");
                                    //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor ');window.location='Doctor request.aspx';</Script>");
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        Label1.Text = "Successfully created doctor";
                                    //}
                                    //else
                                    //{
                                    //    Label1.Text = "تم إنشاء الطبيب بنجاح";
                                    //}
                                    //ModalPopupExtender1.Show();
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                    upModal.Update();
                                }
                                else
                                {
                                    //Label2.Text = "Upload only pdf or jpg files!";
                                    //ModalPopupExtender2.Show();
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Upload only pdf or jpg files')</Script>");
                                    //}
                                    //else
                                    //{
                                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحميل ملفات بدف أو جبغ فقط')</Script>");

                                    //}
                                }
                            }
                            else
                            {
                                tbl_doctor td = new tbl_doctor()
                                {
                                    d_name = Fname.Text + " " + Lname.Text,
                                    d_email = obj.EnryptString(email.Text),
                                    d_contact = obj.EnryptString(phone.Text),
                                    d_specialties = DropDownList1.SelectedItem.Text,
                                    d_city = dl_city.SelectedItem.Text,
                                    //d_location = zipcode.Text,
                                    d_id_number = dnumber.Text,
                                    d_status = 1,
                                    d_date_time = DateTime.Now.ToString(),
                                    d_password = pass,
                                    //d_city = dcity.Text,
                                    d_id_expire = dexpire.Text,
                                    d_photo = "~/Doctorimages/doctor.png",
                                    d_hakkimid = hid,
                                };
                                db.tbl_doctors.InsertOnSubmit(td);
                                db.SubmitChanges();
                                tbl_hakkimID hd = new tbl_hakkimID()
                                {
                                    hakkim_ID = hid,
                                    user_type = "DOC",
                                };
                                db.tbl_hakkimIDs.InsertOnSubmit(hd);
                                db.SubmitChanges();

                                tbl_rating tr = new tbl_rating()
                                {
                                    rate_bm = 0,
                                    rate_service = 0,
                                    rate_wt = 0,
                                    rate = 0,
                                    d_id = hid,
                                };
                                db.tbl_ratings.InsertOnSubmit(tr);
                                db.SubmitChanges();

                                //tbl_rateFinal rf = new tbl_rateFinal()
                                //{
                                //    hakkim_id = hid,
                                //    rate = "0",
                                //};
                                //db.tbl_rateFinals.InsertOnSubmit(rf);
                                //db.SubmitChanges();
                                con.Open();
                                cmd = new SqlCommand("insert into tbl_ratefinal values('" + hid.ToString() + "','0')", con);
                                cmd.ExecuteNonQuery();
                                con.Close();
                                //string message = "Doctor identification number is expired with in " + i + " days...!";
                                //string msg = "Successfully created your Hakkeem account, so you can act as a doctor in Hakkeem. Using following link to signin " + "http://www.goldenetqan.com" + " and your loging password is " + pswd;
                                /////  string msg = "Successfully created your Hakkeem account, Using following link to signin " + "http://www.hakkeem.com" + " and your loging hakkeem id is " + hid + " and password is " + mpass;
                                /////  Email.mail(email.Text, msg, "Doctor registration");

                                string pno = "";
                                pno = phone.Text.Replace(" ", "");
                                string ph = "+966" + pno.ToString();

                                ob1.Message(ph, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);

                           
                                string ph1 = "+91" + pno.ToString();

                                ob1.Message(ph1, " Thank you for registering with Hakkeem.Your hakkeem id : " + hid + " and password is:" + mpass);
                                Email_To_NewDoctor(email.Text, hid, mpass);
                              
                                //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<Script Language=JavaScript>alert('Successfully created doctor ');window.location='Doctor request.aspx';</Script>");
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                    Label1.Text = "Successfully created doctor";
                                //}
                                //else
                                //{
                                //    Label1.Text = "تم إنشاء الطبيب بنجاح";
                                //}
                                //ModalPopupExtender1.Show();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                                upModal.Update();
                            }




                        }



                    }
                }
            }

            Session["d_id"] = null;
        }
    }
    public bool Email_To_NewDoctor(string email, string hakkeemid, string password)
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
            messagestr = messagestr + "We're so happy you've joined Hakkeem family.<br><br> We founded Hakkeem because we wanted to create a trustworthy and inspiring platform for you to help arrange your timing and availability in an easy way,also to help you increasing your income.<br> Also we would like to Thank you for joining us and helping us to enchance the would.<br><strong> Here is your Hakkeem ID:" + hakkeemid + " and password is:" + password + "</strong><br> Your sincerely,<br> Hakkeem Team.";
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
    //public bool Email_To_NewDoctor(string email, string hakkeemid, string password)
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
    //        string btnpath = "http://hakkeem.com/Index/Doctor%20login.aspx";
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
    //        messagestr = messagestr + "Here is your Hakkeem ID:" + hakkeemid + " and password is:"+password+"";
    //        messagestr = messagestr + "</td></tr></tbody></table></td></tr><tr><td>";
    //        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
    //        messagestr = messagestr + "<tbody><tr><td width='100%' style='font-size:15px;color:#4aa9af;font-family: sans-serif'>";
    //        messagestr = messagestr + "Your sincerely,<br>Hakkeem Team.</td></tr></tbody></table></td></tr><tr><td>";
    //        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0'style='padding-top:10px'>";
    //        messagestr = messagestr + "<tbody><tr><td width='100%' style='text-align:center'>";
    //        messagestr = messagestr + "<a href='" + btnpath + "'style='font-size:15px;font-family:sans-serif;text-decoration:none'><button style='background-color:#4aa9af;border:none;border-radius:20px;padding:8px'><span style='color:#fff'> Set your availability now</span></button></a>";
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
    public bool Email_To_ApproveDoctor(string email)
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

    protected void BtnNewDoc_Click(object sender, EventArgs e)
    {
        Session["d_id"] = null;
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/BookDoc Admin/Create Doctor.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/BookDoc Admin/Create Doctor.aspx?l=ar-EG");
        //}
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/BookDoc Admin/Doctor request.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/BookDoc Admin/Doctor request.aspx?l=ar-EG");
        //}
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/BookDoc Admin/Doctor request.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/BookDoc Admin/Doctor request.aspx?l=ar-EG");
        //}
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        if (Session["d_id"] != null)
        {
            //con.Open();
            //String email = "";
            //string name = "";
            //SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_doctor where d_id='" + Session["d_id"] + "'", con);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    email = obj.DecryptString(dt.Rows[0]["d_email"].ToString());
            //    name = dt.Rows[0]["d_name"].ToString();
            //}
            //EmailString(email, "sorry!!  " + name + " your account removed from Hakkeem");
            //SqlCommand cmd = new SqlCommand("Delete from tbl_doctor where d_id='" + Session["d_id"] + "'", con);
            //cmd.ExecuteNonQuery();
            //con.Close();
            Session["d_id"] = null;
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/BookDoc Admin/Doctor request.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/BookDoc Admin/Doctor request.aspx?l=ar-EG");
            //}
        }
        else
        {
            Fname.Text = "";
            Lname.Text = "";
            email.Text = "";
            phone.Text = "";
            dnumber.Text = "";
            dexpire.Text = "";
            Session["d_id"] = null;
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/BookDoc Admin/Create Doctor.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/BookDoc Admin/Create Doctor.aspx?l=ar-EG");
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