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

public partial class Hospital_Doctor_details : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();
    secure obj = new secure();
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
        con.Open();
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
        if (!IsPostBack)
        {
            try
            {
                CheckLocation();
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
            doctor();
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
            // Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
            //Label8.Text = "You must set your location";
            //ModalPopupExtender4.Show();
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

    public void doctor()
    {
        var Query = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_status == 1 select item;
        GridView1.DataSource = Query;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_status == 1  && (item.hd_name.Contains(TextBox1.Text) || item.hd_id_number.Contains(TextBox1.Text)) select item;
        if (Query.Count() > 0)
        {
            GridView1.DataSource = Query;
            GridView1.DataBind();
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor not exist')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الطبيب غير موجود')</Script>");
            //}
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "del")
           {
            string email= e.CommandArgument.ToString();
            Session["DocEmail"] = email.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
            //SqlCommand com = new SqlCommand("delete from tbl_hdoctor where hd_email='" + email + "'", con);
            //com.ExecuteNonQuery();

           }
        }
        catch (Exception ex)
        {

        }

        if (e.CommandName=="check")
        {
            if (Session["AvailableDate"] != null)
            {
                Session["doctor"] = obj.EnryptString(e.CommandArgument.ToString());
                Session["AvailableDate"] = null;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("~/Hospital/Doctoravailabledateandtime.aspx");
                //}
                //else
                //{
                //    Response.Redirect("~/Hospital/Doctoravailabledateandtime.aspx?l=ar-EG");
                //}
            }
            else
            {
                Session["doctor"] = e.CommandArgument.ToString();
                //Response.Redirect("~/Hospital/Doctoravailabledateandtime.aspx?docid="+ obj.EnryptString(e.CommandArgument.ToString()) + "");
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("~/Hospital/Doctoravailabledateandtime.aspx");
                //}
                //else
                //{
                //    Response.Redirect("~/Hospital/Doctoravailabledateandtime.aspx?l=ar-EG");
                //}
            }

        }
    }
    protected void btnclick_Click(object sender, EventArgs e)
    {
       
        try
        {
            string email = Session["DocEmail"].ToString();
            string num = "";
            var hdoc = from item in db.tbl_hdoctors where item.hd_email == email select item;
            foreach (var ss in hdoc)
            {
                tbl_temp_hdoctor tb = new tbl_temp_hdoctor()
                {
                    hd_id = ss.hd_id,
                    hd_about_you = ss.hd_about_you,
                    hd_address = ss.hd_address,
                    hd_address2 = ss.hd_address2,
                    hd_age = ss.hd_age,
                    hd_award_publication = ss.hd_award_publication,
                    hd_city = ss.hd_city,
                    hd_college = ss.hd_college,
                    hd_contact = ss.hd_contact,
                    hd_country = ss.hd_country,
                    hd_date_time = ss.hd_date_time,
                    hd_dob = ss.hd_dob,
                    hd_education = ss.hd_education,
                    hd_email = ss.hd_email,
                    hd_experience = ss.hd_experience,
                    hd_hospital_afili = ss.hd_hospital_afili,
                    hd_id_expire = ss.hd_id_expire,
                    hd_id_number = ss.hd_id_number,
                    hd_language = ss.hd_language,
                    hd_location = ss.hd_location,
                    hd_name = ss.hd_name,
                    hd_password = ss.hd_password,
                    hd_password_reset = ss.hd_password_reset,
                    hd_photo = ss.hd_photo,
                    hd_profesional_mem = ss.hd_profesional_mem,
                    hd_pro_statement = ss.hd_pro_statement,
                    hd_sex = ss.hd_sex,
                    hd_specialties = ss.hd_specialties,
                    hd_status = ss.hd_status,
                    hd_user_rating = ss.hd_user_rating,
                    delete_date_and_time = DateTime.Now.ToString(),
                    reason=txtreason.Text,
                    h_id = Session["hakkeemid_h"].ToString(),
                    
                };
                db.tbl_temp_hdoctors.InsertOnSubmit(tb);
                db.SubmitChanges();

            }
            db.tbl_hdoctors.DeleteAllOnSubmit(hdoc);
            db.SubmitChanges();
            Email_To_AccountDeleteion(email);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + Session["DocEmail"].ToString() + "' ", con);
            DataTable dtt = new DataTable();
            sda.Fill(dtt);
            if (dtt.Rows.Count > 0)
            {
                num = dtt.Rows[0]["hd_contact"].ToString();
            }
            string pno = "+966" + num.ToString();

            ob.Message(pno, " Your account has been removed by Hakkeem authority");

            string pno1 = "+91" + num.ToString();

            ob.Message(pno1, " Your account has been removed by Hakkeem authority");

        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_apmnt_cancels where item.d_id == email select item;
            db.tbl_apmnt_cancels.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_blk_hos_doctors where item.doctor_id == email && item.hos_hakkeem_id == Session["hakkeemid_h"].ToString() select item;
            db.tbl_blk_hos_doctors.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_hos_appmnt_histories where item.d_id == email && item.h_id == Session["hakkeemid_h"].ToString() select item;
            db.tbl_hos_appmnt_histories.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_hos_doc_appmnts where item.d_id == email && item.h_id == Session["hakkeemid_h"].ToString() select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_hos_doc_availables where item.hd_id == email && item.h_id == Session["hakkeemid_h"].ToString() select item;
            foreach (var ss in data)
            {
                try
                {
                    var time = from item in db.tbl_hos_doc_times where item.date_id == ss.id select item;
                    db.tbl_hos_doc_times.DeleteAllOnSubmit(time);
                    db.SubmitChanges();
                }
                catch (Exception ex) { }
            }
            db.tbl_hos_doc_availables.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_rateFinals where item.hakkim_id == email select item;
            db.tbl_rateFinals.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_ratings where item.d_id == email select item;
            db.tbl_ratings.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_report_forms where item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == email select item;
            db.tbl_report_forms.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_share_histories where item.share_h_id == Session["hakkeemid_h"].ToString() && item.share_d_id == email select item;
            db.tbl_share_histories.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_share_records where item.share_d_id == email && item.share_h_id == Session["hakkeemid_h"].ToString() select item;
            db.tbl_share_records.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            string email = Session["DocEmail"].ToString();
            var data = from item in db.tbl_user_feeds where item.d_email == email select item;
            db.tbl_user_feeds.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("doctor details.aspx");
        //}
        //else
        //{
        //    Response.Redirect("doctor details.aspx?l=ar-EG");
        //}

    }
    public bool Email_To_AccountDeleteion(string email)
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
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>Account Deletion</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "Your account has been removed by Hakkeem authority";
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
            mail.Subject = "Hakkeem Account Deletion";
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/SetHospitalLocation.aspx?l=ar-EG");
        //}
    }

    //protected void TextBox1_TextChanged(object sender, EventArgs e)
    //{
    //    var Query = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_status == 1 && (item.hd_name.Contains(TextBox1.Text) || item.hd_id_number.Contains(TextBox1.Text)) select item;
    //    GridView1.DataSource = Query;
    //    GridView1.DataBind();
    //}

    
}