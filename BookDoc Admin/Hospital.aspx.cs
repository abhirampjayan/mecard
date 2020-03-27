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

public partial class BookDoc_Admin_Hospital : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    MailMessage Mail = new MailMessage();
    SMS ob1 = new SMS();
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
        if (!IsPostBack)
        {
            rdb_status.Items.FindByValue("0").Selected = true;
            Hospital();
        }
    }
    public void Hospital()
    {
        var hospital = from item in db.tbl_hospitalregs where item.h_status == 1 orderby item.h_id descending select item;
        GridView1.DataSource = hospital;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Hospital();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = (GridView1.Rows[e.RowIndex].Cells[1].FindControl("Label10") as Label).Text;
        Session["id"] = id;
            //Label2.Text = "Do you want remove this hospital..?";
            //this.ModalPopupExtender2.Show();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
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
            String email = "";
            string name = "";
            string ph = "";
            string id = Session["id"].ToString();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_temp_hospitalreg(h_id,h_name,h_address,h_zipcode,h_regno,h_email,h_contact,h_status,h_password,h_password_reset,h_city,h_photo,h_about,h_date_time,h_agreement,h_otp,h_hakkimid) select * from tbl_hospitalreg where h_hakkimid='" + id + "'", con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("Update tbl_temp_hospitalreg set reason='" + TextBox2.Text + "' ,date='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where h_hakkimid='" + id + "'", con);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_hospitalreg where h_hakkimid='" + id + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                email = dt.Rows[0]["h_email"].ToString();
                name = dt.Rows[0]["h_name"].ToString();
                ph= dt.Rows[0]["h_contact"].ToString();
                SqlCommand cmd4 = new SqlCommand("Delete from tbl_hdoctor where h_id='" + id + "'", con);
                cmd4.ExecuteNonQuery();
                SqlCommand cmd5 = new SqlCommand("Delete from tbl_blk_hos_doctor where hos_hakkeem_id='" + id + "'", con);
                cmd5.ExecuteNonQuery();
                SqlCommand cmd6 = new SqlCommand("Delete from tbl_hos_doc_appmnt where h_id='" + id + "'", con);
                cmd6.ExecuteNonQuery();
                SqlCommand cmd8 = new SqlCommand("Delete from tbl_hos_doc_available where h_id='" + id + "'", con);
                cmd8.ExecuteNonQuery();
            }
            SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_hos_doc_available where h_id='" + id + "'", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    int datid = Convert.ToInt32(dr["id"].ToString());
                    SqlCommand cmd5 = new SqlCommand("Delete from tbl_hos_doc_time where date_id='" + datid + "'", con);
                    cmd5.ExecuteNonQuery();
                }

            }
            if(con.State.ToString()=="Closed")
            { con.Open(); }
            SqlCommand cmd7 = new SqlCommand("Delete from tbl_hos_doc_available where h_id='" + id + "'", con);
            cmd7.ExecuteNonQuery();

            // EmailString(email, "sorry!!  " + name + " your account removed from Hakkeem");


            string pno = "+966" + ph.ToString();

            ob1.Message(pno, " Your account has been removed by Hakkeem authority");

            string pno1 = "+91" + ph.ToString();

            ob1.Message(pno1, " Your account has been removed by Hakkeem authority");
            Email_To_AccountDeleteion(email);
           
            if (con.State.ToString() == "Closed")
            { con.Open(); }
            else
            {
                con.Close();
            }
            SqlCommand cmd2 = new SqlCommand("Delete from tbl_blk_hospital where hospital_hakkeem_id='" + id + "'", con);
            cmd2.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand("Delete from tbl_hakkimID where hakkim_ID='" + id + "'", con);
            cmd3.ExecuteNonQuery();
            SqlCommand cmd9 = new SqlCommand("Delete from tbl_hos_appmnt_history where h_id='" + id + "'", con);
            cmd9.ExecuteNonQuery();
            //SqlCommand cmd15 = new SqlCommand("Delete from tbl_share_records where share_h_id='" + id + "'", con);
            //cmd15.ExecuteNonQuery();
            con.Close();
            var delete = from item in db.tbl_hospitalregs where item.h_hakkimid == Session["id"].ToString() select item;
            foreach (var h in delete)
            {
                //h.h_status = 2;
                db.tbl_hospitalregs.DeleteOnSubmit(h);
            }
            db.SubmitChanges();
            Hospital();
        }
        catch (Exception ex)
        {

        }
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("Hospital.aspx");
        //}
        //else
        //{
        //    Response.Redirect("Hospital.aspx?l=ar-EG");
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        //var delete = from item in db.tbl_hospitalregs where item.h_hakkimid == Session["id"].ToString() select item;
        //foreach (var h in delete)
        //{
        //    h.h_status = 2;
        //}
        //db.SubmitChanges();
        //Hospital();
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label lbl15 = gr.FindControl("Label15") as Label;
            LinkButton lnk4 = gr.FindControl("LinkButton4") as LinkButton;
            LinkButton lnk5 = gr.FindControl("LinkButton5") as LinkButton;

            var Query = from item in db.tbl_blk_hospitals where item.hospital_hakkeem_id == lbl15.Text select item;
            if (Query.Count() > 0)
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="open")
        {
            Session["hakkeemid_h"] = e.CommandArgument.ToString();
            Response.Redirect("hospital_details.aspx");
        }
        if (e.CommandName == "blk")
        {
            var hsptl = from item in db.tbl_blk_hospitals where item.hospital_hakkeem_id == e.CommandArgument.ToString() select item;
            if(hsptl.Count()>0)
            {

            }
            else
            {
                tbl_blk_hospital th = new tbl_blk_hospital()
                {
                    hospital_hakkeem_id = e.CommandArgument.ToString(),
                };
                db.tbl_blk_hospitals.InsertOnSubmit(th);
                db.SubmitChanges();

                string email = "";
                string ph = "";
                
                var Query = from item in db.tbl_hospitalregs where item.h_hakkimid == e.CommandArgument.ToString() select item;
                foreach(var ss in Query)
                {
                    email = ss.h_email;
                    ph = ss.h_contact;
                }
                string msg = "Tempararly your hospital account has been deactivated, please contact Hakkeem authority";
                try
                {
                    // Mail.mail(email, msg, "Account deactivated");
                    string pno = "+966" + ph.ToString();

                    ob1.Message(pno, "Tempararly your hospital account has been deactivated, please contact Hakkeem authority");

                    string pno1 = "+91" + ph.ToString();

                    ob1.Message(pno1, "Tempararly your hospital account has been deactivated, please contact Hakkeem authority");

                    Email_To_AccountInActive(email);
                    
                }
                catch (Exception ex) { }
                Hospital();
            }
        }
        if (e.CommandName == "ublk")
        {
            var hsptl = from item in db.tbl_blk_hospitals where item.hospital_hakkeem_id == e.CommandArgument.ToString() select item;
            if (hsptl.Count() > 0)
            {
                foreach(var h in hsptl)
                {
                    db.tbl_blk_hospitals.DeleteOnSubmit(h);
                    db.SubmitChanges();
                }

                string email = "";
                string ph = "";
                var Query = from item in db.tbl_hospitalregs where item.h_hakkimid == e.CommandArgument.ToString() select item;
                foreach (var ss in Query)
                {
                    email = ss.h_email;
                    ph = ss.h_contact;
                }
                string msg = "Your hospital account has been activated.";
                try
                {
                    // Mail.mail(email, msg, "Account activated");





                    string pno = "+966" + ph.ToString();

                    ob1.Message(pno, "Your account has been activated by Hakkeem authority.");

                    string pno1 = "+91" + ph.ToString();

                    ob1.Message(pno1, "Your account has been activated by Hakkeem authority.");
                    Email_To_AccountActive(email);
                   
                }
                catch (Exception ex) { }

            }
            Hospital();
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            var hospital = from item in db.tbl_hospitalregs where item.h_status == 1 && item.h_hakkimid == TextBox1.Text || item.h_contact == TextBox1.Text orderby item.h_id descending select item;
            if (hospital.Count() > 0)
            {
                GridView1.DataSource = hospital;
                GridView1.DataBind();
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
            Hospital();
        }
    }
    protected void rdb_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdb_status.SelectedValue.ToString() == "0")
        {
            Response.Redirect("Hospital.aspx");
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
            SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_hospitalreg  where h_status = 1 and h_hakkimid not in (select hospital_hakkeem_id from tbl_blk_hospital) order by h_id desc ", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                    GridView1.Visible = false;
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('آسف! غير معثور عليه')</Script>");
                //    GridView1.Visible = false;
                //}
            }
            // }
            con.Close();

        }
        else if (rdb_status.SelectedValue.ToString() == "2")
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_blk_hospital", con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //var user = from item in db.tbl_signups orderby item.id descending select item;
                SqlDataAdapter sda1 = new SqlDataAdapter("Select blk.*,U.* from tbl_blk_hospital blk inner join tbl_hospitalreg U on U.h_hakkimid=blk.hospital_hakkeem_id where U.h_status = 1 order by U.h_id desc", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    GridView1.Visible = true;
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                }
                

            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                    GridView1.Visible = false;
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('آسف! غير معثور عليه')</Script>");
                //    GridView1.Visible = false;
                //}
            }
            con.Close();
        }
        else
        {
            Hospital();
        }
    }
}