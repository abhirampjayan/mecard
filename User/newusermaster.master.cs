using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class User_newusermaster : System.Web.UI.MasterPage
{
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();

    //protected override void InitializeCulture()
    //{
    //    Session["Speciality"] = "Auto";
    //    string culture = "Auto";
    //    try
    //    {
    //        culture = Request.QueryString["l"].ToString();
    //        Session["Speciality"] = culture;
    //    }
    //    catch (Exception ex)
    //    { }
    //    // string culture = Session["Speciality"].ToString();
    //    if (string.IsNullOrEmpty(culture))
    //        culture = "Auto";
    //    //Use this
    //    UICulture = culture;
    //    Culture = culture;
    //    //OR This
    //    if (culture != "Auto")
    //    {

    //        System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
    //        System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
    //        System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
    //    }
    //    else
    //    {
    //        //LinkButton1.Text = "عربى";
    //    }

    //    base.InitializeCulture();
    //}


    protected void Page_Load(object sender, EventArgs e)
    {
       


        if (!IsPostBack)
        {
           

            //try
            //{
            //if (Session["searchcommon"].ToString() != "")
            //{
            //    // HyperLink2.NavigateUrl = "~/User/SearchCommon.aspx";
            //    HyperLink2.NavigateUrl = "~/Hakkeem/Index.aspx";
            //    Session["searchcommon"] = "";
            //    //     Response.Redirect("searchcommon.aspx");
            //}
            //}
            //catch (Exception ex)
            //{
            //HyperLink2.NavigateUrl = "~/Hakkeem/Index.aspx";
            //}
            try
            {
                if (Session["hakkemid_u"].ToString() != "")
                {
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        HyperLink2.NavigateUrl = "~/User/Search.aspx";

                    //}
                    //else
                    //{
                    //    HyperLink8.NavigateUrl = "~/User/Search.aspx?l=ar-EG";
                    //}


                }
                else
                {
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        HyperLink2.NavigateUrl = "~/default.aspx";
                    //}
                    //else
                    //{
                    //    HyperLink8.NavigateUrl = "~/default.aspx?l=ar-EG";
                    //}

                }
            }
            catch (Exception ex)
            {
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    HyperLink2.NavigateUrl = "~/default.aspx";
                //}
                //else
                //{
                //    HyperLink8.NavigateUrl = "~/default.aspx?l=ar-EG";
                //}
            }


        }

        //if (Session["Speciality"].ToString() == "Auto")
        //{
        //    HyperLink2.NavigateUrl = "~/Hakkeem/Index.aspx?l=ar-EG";
        //}
        //else
        //{
        //    HyperLink2.NavigateUrl = "~/Hakkeem/Index.aspx?l=ar-EG";
        //}

user();
    }

   

    public void user()
    {
        try
        {
            if (Session["hakkemid_u"].ToString() != null || Session["hakkemid_u"].ToString() != "")
            {
                var Query = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                foreach (var ss in Query)
                {
                    HyperLink1.Visible = true;
                  //  HyperLink11.Visible = true;
                    HyperLink1.NavigateUrl = "~/User/User account.aspx";
                //    HyperLink11.NavigateUrl = "~/User/User account.aspx?l=ar-EG";

                    HyperLink3.NavigateUrl = "~/User/User review.aspx";
                 //   HyperLink10.NavigateUrl = "~/User/User review.aspx?l=ar-EG";

                    HyperLink4.NavigateUrl = "~/User/ConsultedHistory.aspx";
                 //   HyperLink12.NavigateUrl = "~/User/ConsultedHistory.aspx?l=ar-EG";


                    HyperLink5.NavigateUrl = "~/User/UserAppointments.aspx";
                //    HyperLink13.NavigateUrl = "~/User/UserAppointments.aspx?l=ar-EG";

                    HyperLink6.NavigateUrl = "~/User/UploadTestReports.aspx";
                 //   HyperLink14.NavigateUrl = "~/User/UploadTestReports.aspx?l=ar-EG";

                    string[] n = ss.name.Split(' ');
                    //LinkButton1.Text = /*"Hello, "+*/n[0];
                    //LinkButton4.Text = /*"Hello, "+*/n[0];
                    LinkButton2.Text = "SignOut";
                    LinkButton2.Visible = true;
               //     LinkButton5.Text = "خروج";
                  //  LinkButton5.Visible = true;
                    if (ss.photo != null)
                    {
                        //ImgUser.ImageUrl = ss.photo;
                        //ImgUser1.ImageUrl = ss.photo;
                    }
                    else
                    {
                        //ImgUser.ImageUrl = "~/User/mapicons/user.png";
                        //ImgUser1.ImageUrl = "~/User/mapicons/user.png";
                    }
                    //Label1.Text = ss.name;
                    //Label2.Text = "SignOut";
                }
            }
            else
            {
               // HyperLink1.Visible = false; HyperLink11.Visible = false;
                //LinkButton1.Text = "Patient";
                //LinkButton4.Text = "صبور";
                //LinkButton1.PostBackUrl = "~/Index/SignInSignUp.aspx";
                //LinkButton4.PostBackUrl = "~/Index/SignInSignUp.aspx?l=ar-EG";
                //ImgUser.Visible = false;
                //ImgUser1.Visible = false;
                LinkButton2.Visible = false;
             //   LinkButton5.Visible = false;
                //LblUserName.Visible = false;
                //Label1.Visible = false;
                //Label2.Text = "SignIn";



                HyperLink1.Visible = false; 
              //  HyperLink11.Visible = false;

                HyperLink3.Visible = false;
             //   HyperLink10.Visible = false;

                HyperLink4.Visible = false;
             //   HyperLink12.Visible = false;


                HyperLink5.Visible = false;
             //   HyperLink13.Visible = false;

                HyperLink6.Visible = false;
             //   HyperLink14.Visible = false;



            }
        }
        catch (Exception ex)
        {
          //  HyperLink1.Visible = false; HyperLink11.Visible = false;
            //LinkButton1.Text = "Patient";
            //LinkButton4.Text = "صبور";
            //LinkButton1.PostBackUrl = "~/Index/SignInSignUp.aspx";
            //LinkButton4.PostBackUrl = "~/Index/SignInSignUp.aspx?l=ar-EG";
            //ImgUser.Visible = false;
            //ImgUser1.Visible = false;


            HyperLink1.Visible = false;
          //  HyperLink11.Visible = false;

            HyperLink3.Visible = false;
         //   HyperLink10.Visible = false;

            HyperLink4.Visible = false;
           // HyperLink12.Visible = false;


            HyperLink5.Visible = false;
         //   HyperLink13.Visible = false;

            HyperLink6.Visible = false;
           // HyperLink14.Visible = false;
        }
        login();
    }

    public void login()
    {
        if (!string.IsNullOrEmpty(Session["hakkemid_u"] as string))
        {
            var user = (from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item).First();
            patient.Text = user.name;
          //  arabicPatient.Text = user.name;
        }


        //if (Session["hakkemid_u"].ToString() != "" || Session["hakkemid_u"].ToString() != null)
        //{
        //    var user = (from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item).First();
        //    patient.Text = user.name;
           
        //}
        else
        {
            patient.Text = "<li class='dropdown'><a href='../Index/SignInSignUp.aspx'>Patient</a></li>";
           // arabicPatient.Text = "<li class='dropdown'><a href='../Index/SignInSignUp.aspx?l=ar-EG'>صبور</a></li>";
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["db_BookDocConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select d_name from tbl_doctor where " +
                "d_name like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["d_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //if (LinkButton1.Text == "Patient")
        //{
        //    Response.Redirect("~/Index/SignInSignUp.aspx");
        //}
        //else
        //{
        //    //Session["hakkemid_u"] = null;
        //    //Response.Redirect("user account.aspx");
        //    LinkButton1.Enabled = false;
        //}
    }

    //public void appointment_cancel()
    //{
    //    DateTime dt = DateTime.Now;
    //    string date = dt.ToString("yyyy-MM-dd");
    //    var Query = from item in db.tbl_doctor_appointments where item.a_date == date && item.a_status == 0 select item;
    //    foreach (var ss in Query)
    //    {
    //        DateTime d = DateTime.Parse(ss.a_time);
    //        string t = d.ToString("h:mm tt");
    //        string s_result = (DateTime.Parse(t).AddHours(-2)).ToString("h:mm tt");
    //        DateTime d1 = DateTime.Now;
    //        string t1 = d1.ToString("h:mm tt");

    //        if (DateTime.Parse(t1) == DateTime.Parse(s_result))
    //        {

    //            string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is not confirmed yet, so please confirm it within 30 Minutes otherwise your appointment is cancelled ";
    //            Email.mail(ss.c_id, msg, "Appointment canceled");

    //        }
    //        DateTime dd = DateTime.Parse(ss.a_time);
    //        string tt = d.ToString("h:mm tt");
    //        string s_result1 = (DateTime.Parse(t).AddHours(-1.30)).ToString("h:mm tt");
    //        DateTime dd1 = DateTime.Now;
    //        string tt1 = d1.ToString("h:mm tt");
    //        if (DateTime.Parse(tt1) == DateTime.Parse(s_result1))
    //        {

    //            string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is not confirmed yet, so your appointment is cancelled.";
    //            Email.mail(ss.c_id, msg,"Appointment canceled");
    //            db.tbl_doctor_appointments.DeleteOnSubmit(ss);
    //            db.SubmitChanges();
    //        }
    //    }
    //}

    //public void Email(string email, string msg)
    //{
    //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //    mail.To.Add(email);
    //    mail.From = new MailAddress("bookdoc2017@gmail.com", "Hakkeem", System.Text.Encoding.UTF8);
    //    mail.Subject = "Appointment alert";
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

    protected void Timer1_Tick(object sender, EventArgs e)
    {

        //doctor_available();
        //appointment_cancel();
    }


    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["user"] = "";
        Session["hakkemid_u"] = "";
        Session["hakkemid_u"] = null;
        Session["ggggg"] = "1";
        //if (Session["Speciality"].ToString() == "Auto")
        //{
            Response.Redirect("~/Index/SignInSignUp.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Index/SignInSignUp.aspx?l=ar-EG");
        //}
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        try
        {
            //if (LinkButton3.Text == "عربى")
            //{
            //LinkButton3.Text = "الإنجليزية";
            //Session["la"] = "الإنجليزية";
         //   Response.Redirect(Request.Path + "?l=ar-EG");

            //}
            //else
            //{
            //    Session["la"] = "عربى";
            //    LinkButton3.Text = "عربى";
            //    Response.Redirect(Request.Path);

            //}
        }
        catch (Exception ex)
        {


        }

        Session["Language"] = "Auto";

        Response.Redirect(Request.Path);
    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {

        try
        {


            //if (LinkButton3.Text == "عربى")
            //{
            //LinkButton6.Text = "عربى";
            //Session["la"] = "عربى";
            Response.Redirect(Request.Path);
            //Response.Redirect(Request.Path + "?l=ar-EG");

            //}
            //else
            //{
            //    Session["la"] = "عربى";
            //    LinkButton3.Text = "عربى";
            //    Response.Redirect(Request.Path);

            //}
        }
        catch (Exception ex)
        {



        }

        Session["Language"] = "Auto";


        Response.Redirect(Request.Path);
    }



}
