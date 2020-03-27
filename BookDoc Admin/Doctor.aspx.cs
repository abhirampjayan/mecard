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

public partial class BookDoc_Admin_Doctor : System.Web.UI.Page
{ secure obj = new secure();
    databaseDataContext db = new databaseDataContext();
    MailMessage Mail = new MailMessage();
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
        //    this.MasterPageFile = "~/BookDoc Admin/AdminArabicMasterPage.master";
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            rdb_status.Items.FindByValue("0").Selected = true;
            doctor();
           
        }
    }



    //public void DoctorList()
    //{
    //    try
    //    {
    //        if (Session["d_id"] != null)
    //        {
    //            var Query = from item in db.tbl_doctors where item.d_id == int.Parse(Session["d_id"].ToString()) select item;
    //            foreach (var ss in Query)
    //            {
    //                string[] a = ss.d_name.Split(' ');
    //                try
    //                {
    //                    Fname.Text = a[0].ToString();
    //                    Lname.Text = a[1].ToString();

    //                }
    //                catch (Exception ex)
    //                {
    //                    Lname.Text = "";

    //                }
    //                email.Text = obj.DecryptString(ss.d_email);

    //                string pp = obj.DecryptString(ss.d_contact);
    //                //  phone.Text = pp.Substring(4, 9);
    //                phone.Text = pp.ToString();
    //                DropDownList1.Items.Insert(0, ss.d_specialties);
    //                dl_city.Items.Insert(0, ss.d_city);
    //                //DropDownList1.SelectedItem.Text = ss.d_specialties;
    //                //dl_city.SelectedItem.Text = ss.d_city;
    //                //zipcode.Text = ss.d_location;
    //                //dcity.Text = ss.d_city;
    //                if (ss.d_agreement != null)
    //                {
    //                    HyperLink1.NavigateUrl = ss.d_agreement.ToString();
    //                    Panel1.Visible = false;
    //                }
    //                else
    //                {
    //                    HyperLink1.Visible = false;
    //                    Panel1.Visible = true;
    //                }

    //            }
    //        }
    //        else
    //        {
    //            Session["d_id"] = null;
    //            Session["d_id"] = "";
    //            HyperLink1.Visible = false;
    //            Panel1.Visible = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        HyperLink1.Visible = false;
    //        Panel1.Visible = true;

    //    }
    //}

    public void doctor()
    {
        SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_doctor  where d_status = 1 and  d_id_expire >= GETDATE() order by d_id desc ", con);
        DataSet dts = new DataSet();
        dts.Clear();
        sda1.Fill(dts);
      //  var doctor1 = from item in db.tbl_doctors where item.d_status == 1 && item.d_id_expire>= GETDATE() orderby item.d_id descending select item;
        GridView1.DataSource = dts;
        GridView1.DataBind();


        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label cno = new Label();
            cno=(Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");

            string   lno = obj.DecryptString(cno.Text);
            Label lno1 = new Label();
            lno1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");
            lno1.Text = lno;


            Label email = new Label();
            email = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");
           

            string lemail = obj.DecryptString(email.Text);
            Label email1 = new Label();
            email1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");
            email1.Text = lemail;
            // 
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        doctor();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        doctor();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = (GridView1.Rows[e.RowIndex].Cells[1].FindControl("Label13") as Label).Text;
        Session["id"] = id;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        upModal.Update();
            //Label2.Text = "Do you want remove this doctor..?";
            //this.ModalPopupExtender2.Show();

        }
        catch (Exception ex)
        {

        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = (GridView2.Rows[e.RowIndex].Cells[1].FindControl("Label15") as Label).Text;
            Session["id"] = id;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
            //Label2.Text = "Do you want remove this doctor..?";
            //this.ModalPopupExtender2.Show();

        }
        catch (Exception ex)
        {

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            string id = Session["id"].ToString();
            String email = "";
            string name = "";
            string ph = "";
            int Doctorid;
               SqlCommand cmd = new SqlCommand("INSERT INTO tbl_temp_doctor(d_id,d_name,d_country,d_language,d_location,d_email,d_contact,d_address,d_address2,d_education,d_college,d_experience,d_hospital_afili,d_profesional_mem,d_award_publication,d_specialties,d_pro_statement,d_status,d_photo,d_dob,d_sex,d_date_time,d_id_number,d_password,d_password_reset,d_about_you,d_user_rating,d_city,d_id_expire,d_age,d_otp,d_agreement,d_hakkimid) select * from tbl_doctor where d_hakkimid='" + id + "'", con);
               cmd.ExecuteNonQuery();
               SqlCommand cmd1 = new SqlCommand("Update tbl_temp_doctor set reason='" + TextBox2.Text + "',date='"+ DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where d_hakkimid='" + id + "'", con);
               cmd1.ExecuteNonQuery();
               SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_doctor where d_hakkimid='" + id + "'", con);
               DataTable dt = new DataTable();
               sda.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                String Deemail = dt.Rows[0]["d_email"].ToString();
                email = obj.DecryptString(Deemail);
                name = dt.Rows[0]["d_name"].ToString();
                string phon= dt.Rows[0]["d_contact"].ToString();
                ph = obj.DecryptString(phon);
                Doctorid = Convert.ToInt32(dt.Rows[0]["d_id"].ToString());
                SqlCommand cmd8 = new SqlCommand("Delete from tbl_doctor_location where d_id='" + Doctorid + "'", con);
                cmd8.ExecuteNonQuery();
               }
            //  EmailString(email, "sorry!!  " + name + " your account removed from Hakkeem");
            string pno = "+966" + ph.ToString();

            ob.Message(pno, " Your account has been removed by Hakkeem authority");

            string pno1 = "+91" + ph.ToString();

            ob.Message(pno1, " Your account has been removed by Hakkeem authority");

            Email_To_AccountDeleteion(email);
           
            SqlDataAdapter sda1 = new SqlDataAdapter("Select * from doctor_availability where d_id='" + id + "'", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    if (con.State.ToString() == "Closed")
                    { con.Open(); }
                    int datid = Convert.ToInt32(dr["id"].ToString());
                    SqlCommand cmd5 = new SqlCommand("Delete from tbl_doc_time where date_id='" + datid + "'", con);
                    cmd5.ExecuteNonQuery();
                    
                }
                
            }
            if(con.State.ToString()=="Closed")
            { con.Open(); }
            SqlCommand cmd2=new SqlCommand ("Delete from doctor_availability where d_id='" + id + "'", con);
            cmd2.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand("Delete from tbl_blk_hakkeem_doctor where hakkeem_id='" + id + "'", con);
            cmd3.ExecuteNonQuery();
            SqlCommand cmd4 = new SqlCommand("Delete from tbl_doc_language where doc_id='" + id + "'", con);
            cmd4.ExecuteNonQuery();
            SqlCommand cmd6 = new SqlCommand("Delete from tbl_doctor_appointment where d_id='" + id + "'", con);
            cmd6.ExecuteNonQuery();
            SqlCommand cmd7 = new SqlCommand("Delete from tbl_doctor_certificate where hakkimid='" + id + "'", con);
            cmd7.ExecuteNonQuery();
            SqlCommand cmd9 = new SqlCommand("Delete from tbl_fee where d_hakkimid='" + id + "'", con);
            cmd9.ExecuteNonQuery();
            SqlCommand cmd10 = new SqlCommand("Delete from tbl_hakkimID where hakkim_ID='" + id + "'", con);
            cmd10.ExecuteNonQuery();
            SqlCommand cmd11 = new SqlCommand("Delete from tbl_rateFinal where hakkim_id='" + id + "'", con);
            cmd11.ExecuteNonQuery();
            SqlCommand cmd12 = new SqlCommand("Delete from tbl_rateFinal where hakkim_id='" + id + "'", con);
            cmd12.ExecuteNonQuery();
            SqlCommand cmd13 = new SqlCommand("Delete from tbl_rating where d_id='" + id + "'", con);
            cmd13.ExecuteNonQuery();
            SqlCommand cmd14 = new SqlCommand("Delete from tbl_ratingview where d_id='" + id + "'", con);
            cmd14.ExecuteNonQuery();
            SqlCommand cmd15 = new SqlCommand("Delete from tbl_share_records where share_d_id='" + id + "'", con);
            cmd15.ExecuteNonQuery();

            con.Close();
        var delete = from item in db.tbl_doctors where item.d_hakkimid == Session["id"].ToString() select item;
        foreach (var d in delete)
        {
            //d.d_status = 2;
            db.tbl_doctors.DeleteOnSubmit(d);
        }
        db.SubmitChanges();
        doctor();
        }
        catch (Exception ex)
        {

        }
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("Doctor.aspx");
        //}
        //else
        //{
        //    Response.Redirect("Doctor.aspx?l=ar-EG");
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
    public bool Email_To_AccountActive(string email)
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
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>Account Active</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "Your account has been activated by Hakkeem authority.";
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
            mail.Subject = "Hakkeem Account Active";
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
    public bool Email_To_AccountInActive(string email)
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
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>Account InActive</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "Tempararly your account has been deactivated, please contact Hakkeem authority.";
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
            mail.Subject = "Hakkeem Account InActive";
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
        //var delete = from item in db.tbl_doctors where item.d_hakkimid == Session["id"].ToString() select item;
        //foreach (var d in delete)
        //{
        //    d.d_status = 2;
        //}
        //db.SubmitChanges();
        //doctor();
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if(e.CommandName=="open")
        {
            Session["hakkeemid"] = e.CommandArgument.ToString();
            Response.Redirect("doctor_details.aspx");
        }


        if (e.CommandName == "blk")
        {
            var Query = from item in db.tbl_blk_hakkeem_doctors where item.hakkeem_id == e.CommandArgument.ToString() select item;
            if (Query.Count() > 0)
            {

            }
            else
            {
                tbl_blk_hakkeem_doctor tb = new tbl_blk_hakkeem_doctor()
                {
                    hakkeem_id = e.CommandArgument.ToString(),
                };
                db.tbl_blk_hakkeem_doctors.InsertOnSubmit(tb);
                db.SubmitChanges();
                string email = "";
                string ph = "";
                string name = "";
                var doctor1 = from item in db.tbl_doctors where item.d_hakkimid == e.CommandArgument.ToString() select item;
                foreach(var ss in doctor1)
                {
                    email = obj.DecryptString(ss.d_email);
                    ph = obj.DecryptString(ss.d_contact);
                    name = ss.d_name;
                }
                string msg = "Tempararly your account has been deactivated, please contact Hakkeem authority";
                try
                {
                    //  Mail.mail(email, msg, "Account deactivated");
                    Email_To_AccountInActive(email);
                }
                catch (Exception ex) { }
                try
                {
                    ob.Message(ph, msg);
                }
                catch (Exception ex) { }

                var apmnt = from item in db.tbl_doctor_appointments where item.d_id == e.CommandArgument.ToString() && item.a_status == 1 select item;
                if(apmnt.Count()>0)
                {
                    foreach (var ss in apmnt)
                    {

                        tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                        {
                            apmnt_id = ss.id.ToString(),
                            canceled_by = "hkm",
                            cancel_rsn = "Doctor - Blocked by hakkeem authority",
                            date = DateTime.Now.ToShortDateString(),
                            time = DateTime.Now.ToShortTimeString(),
                            u_id = ss.c_id,
                            d_id = ss.d_id,

                        };
                        db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                        db.SubmitChanges();
                        

                        var user = from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item;
                        foreach (var u in user)
                        {
                            email = obj.DecryptString(u.email);
                            ph = obj.DecryptString(u.contact);
                        }
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,date) as dayname from tbl_apmnt_cancel where date='" + ss.a_date + "'", con);
                        sda1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            Session["Day"] = dt1.Rows[0]["dayname"].ToString();
                        }
                        msg = "Your Appointment has been Cancelled with Doctor " + name + " on " + ss.a_date + " and " + ss.a_time + " Thank you Hakkeem Team";
                        try
                        {
                            //  Mail.mail(email, msg, "Hakkeem appointment canceled");
                            Email_To_AppoinmentCancilation(email, name, ss.a_date, ss.a_time,Session["Day"].ToString());
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            ob.Message(ph, msg);
                        }
                        catch (Exception ex) { }
                        ss.a_status = 2;
                    }
                    db.SubmitChanges();
                }

            }
            doctor();
        }
        if (e.CommandName == "ublk")
        {
            var Query = from item in db.tbl_blk_hakkeem_doctors where item.hakkeem_id == e.CommandArgument.ToString() select item;
            if (Query.Count() > 0)
            {
                foreach(var ss in Query)
                {
                    db.tbl_blk_hakkeem_doctors.DeleteOnSubmit(ss);
                }
                db.SubmitChanges();


                string email = "";
                string ph = "";
                string name = "";
                var doctor1 = from item in db.tbl_doctors where item.d_hakkimid == e.CommandArgument.ToString() select item;
                foreach (var ss in doctor1)
                {
                    email = obj.DecryptString(ss.d_email);
                    ph = obj.DecryptString(ss.d_contact);
                    name = ss.d_name;
                }
                string msg ="Your account has been activated by Hakkeem authority";
                try
                {
                    //  Mail.mail(email, msg, "Account activated");
                    Email_To_AccountActive(email);
                }
                catch (Exception ex) { }
                try
                {
                    ob.Message(ph, msg);
                }
                catch (Exception ex) { }


            }
            doctor();
        }
        //doctor();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "open")
        {
            Session["d_id"] = e.CommandArgument.ToString();
          //  DoctorList();
           
             Response.Redirect("expdoctor.aspx");
        }

        if (e.CommandName == "open1")
        {
            Session["hakkeemid"] = e.CommandArgument.ToString();
            Response.Redirect("doctor_details.aspx");
        }




        doctor();
       
    }
    public bool Email_To_AppoinmentCancilation(string email, string name, string date, string time,string day)
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
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>Appointment Cancellation</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "Your appointment cancellation." + name + ", Time:" + time + " "+day+", Date:" + date + "";
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
            mail.Subject = "Hakkeem Appointment Cancellation";
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
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        foreach(GridViewRow gr in GridView1.Rows)
        {
            Label lbl13 = gr.FindControl("Label13") as Label;
            LinkButton lnk4 = gr.FindControl("LinkButton4") as LinkButton;
            LinkButton lnk5 = gr.FindControl("LinkButton5") as LinkButton;

            var Query = from item in db.tbl_blk_hakkeem_doctors where item.hakkeem_id == lbl13.Text select item;
            if(Query.Count()>0)
            {
                lnk4.Visible = false;
                lnk5.Visible = true;
                lnk5.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lnk5.Visible = false;
                lnk4.Visible = true;
            }

        }
    }
    protected void GridView2_DataBound(object sender, EventArgs e)
    {
        //foreach (GridViewRow gr in GridView2.Rows)
        //{
        //    Label lbl13 = gr.FindControl("Label13") as Label;
        //    LinkButton lnk4 = gr.FindControl("LinkButton4") as LinkButton;
        //    LinkButton lnk5 = gr.FindControl("LinkButton5") as LinkButton;

        //    var Query = from item in db.tbl_blk_hakkeem_doctors where item.hakkeem_id == lbl13.Text select item;
        //    if (Query.Count() > 0)
        //    {
        //        lnk4.Visible = false;
        //        lnk5.Visible = true;
        //        lnk5.ForeColor = System.Drawing.Color.Red;
        //    }
        //    else
        //    {
        //        lnk5.Visible = false;
        //        lnk4.Visible = true;
        //    }

        //}
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            var doctor = from item in db.tbl_doctors where item.d_status == 1 && item.d_hakkimid == TextBox1.Text || item.d_contact == obj.EnryptString(TextBox1.Text) orderby item.d_id descending select item;
            if (doctor.Count() > 0)
            {
                GridView1.DataSource = doctor;
                GridView1.DataBind();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    Label cno = new Label();
                    cno = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");

                    string lno = obj.DecryptString(cno.Text);
                    Label lno1 = new Label();
                    lno1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");
                    lno1.Text = lno;


                    Label email = new Label();
                    email = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");


                    string lemail = obj.DecryptString(email.Text);
                    Label email1 = new Label();
                    email1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");
                    email1.Text = lemail;
                    // 
                }
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('آسف! غير معثور عليه')</Script>");
                //}
            }
        }
        else
        {
            doctor();
        }
    }
    protected void rdb_status_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (rdb_status.SelectedValue.ToString() == "0")
        {
            Response.Redirect("Doctor.aspx");
        }
       else  if (rdb_status.SelectedValue.ToString() == "1")
        {
            con.Open();
            //DataTable dt = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_blk_users", con);
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //var user = from item in db.tbl_signups orderby item.id descending select item;
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_doctor  where d_status = 1 and  d_id_expire >= GETDATE() and d_hakkimid not in (select hakkeem_id from tbl_blk_hakkeem_doctor) order by d_id desc ", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                GridView2.Visible = false;
                GridView1.Visible = true;
                GridView1.DataSource = dt1;
                GridView1.DataBind();

                foreach (GridViewRow gr in GridView1.Rows)
                {
                    Label cno = new Label();
                    cno = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");

                    string lno = obj.DecryptString(cno.Text);
                    Label lno1 = new Label();
                    lno1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");
                    lno1.Text = lno;


                    Label email = new Label();
                    email = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");


                    string lemail = obj.DecryptString(email.Text);
                    Label email1 = new Label();
                    email1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");
                    email1.Text = lemail;


                }
            }
            else
            {


                
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                    GridView1.Visible = false;
              
            }
            // }
            con.Close();

        }
        else if (rdb_status.SelectedValue.ToString() == "2")
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_blk_hakkeem_doctor ", con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //var user = from item in db.tbl_signups orderby item.id descending select item;
                SqlDataAdapter sda1 = new SqlDataAdapter("Select blk.*,U.* from tbl_blk_hakkeem_doctor blk inner join tbl_doctor U on U.d_hakkimid=blk.hakkeem_id where U.d_status = 1 order by U.d_id desc", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {

                    GridView2.Visible = false;

                    GridView1.Visible = true;
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        Label cno = new Label();
                        cno = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");

                        string lno = obj.DecryptString(cno.Text);
                        Label lno1 = new Label();
                        lno1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");
                        lno1.Text = lno;


                        Label email = new Label();
                        email = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");


                        string lemail = obj.DecryptString(email.Text);
                        Label email1 = new Label();
                        email1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");
                        email1.Text = lemail;


                    }
                }
                else
                {


                      RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                      GridView1.Visible = false;
                   
                }
            }
            else
            {
                  RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                    GridView1.Visible = false;
                
            }
            con.Close();
        }
        else if (rdb_status.SelectedValue.ToString() == "3")
        {
            con.Open();
          
                //var user = from item in db.tbl_signups orderby item.id descending select item;
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_doctor where d_status = 1 and d_id_expire < GETDATE() order by d_id desc", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    GridView1.Visible = false;

                    GridView2.Visible = true;
                    GridView2.DataSource = dt1;
                    GridView2.DataBind();
                    foreach (GridViewRow gr in GridView2.Rows)
                    {
                        Label cno = new Label();
                        cno = (Label)GridView2.Rows[gr.RowIndex].FindControl("Label17");

                        string lno = obj.DecryptString(cno.Text);
                        Label lno1 = new Label();
                        lno1 = (Label)GridView2.Rows[gr.RowIndex].FindControl("Label17");
                        lno1.Text = lno;


                        Label email = new Label();
                        email = (Label)GridView2.Rows[gr.RowIndex].FindControl("Label18");


                        string lemail = obj.DecryptString(email.Text);
                        Label email1 = new Label();
                        email1 = (Label)GridView2.Rows[gr.RowIndex].FindControl("Label18");
                        email1.Text = lemail;


                    }
                }
                else
                {


                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                    GridView1.Visible = false;

                }
           
            con.Close();
        }

        else
        {
            doctor();
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