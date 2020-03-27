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

public partial class BookDoc_Admin_users : System.Web.UI.Page
{
    secure obj = new secure();
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();
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
            users();
        }
    }

    public void users()
    {
        var user = from item in db.tbl_signups orderby item.id descending select item;
        GridView1.DataSource = user;
        GridView1.DataBind();
        foreach(GridViewRow gr in GridView1.Rows)
        {
            string hakkeemid = (gr.FindControl("Label2") as Label).Text;
            string contact = (gr.FindControl("Label3") as Label).Text;

            Label lbl3 = gr.FindControl("Label3") as Label;
            lbl3.Text = obj.DecryptString(lbl3.Text);

          
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //string email = "";
            string id = (GridView1.Rows[e.RowIndex].Cells[1].FindControl("Label2") as Label).Text;
            Session["uhakkimid"] = id;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();

            
        }
        catch(Exception ex)
        {

        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        users();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label lbl4 = gr.FindControl("Label4") as Label;
            LinkButton lnk5 = gr.FindControl("LinkButton5") as LinkButton;
            LinkButton lnk6 = gr.FindControl("LinkButton6") as LinkButton;

            var Query = from item in db.tbl_blk_users where item.user_hakkeemid == lbl4.Text select item;
            if (Query.Count() > 0)
            {
                lnk5.Visible = false;
                lnk6.Visible = true;
                lnk6.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lnk6.Visible = false;
                lnk5.Visible = true;
            }

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "blk")
        {
            var Query = from item in db.tbl_blk_users where item.user_hakkeemid == e.CommandArgument.ToString() select item;
            if (Query.Count() > 0)
            {

            }
            else
            {
                tbl_blk_user tb = new tbl_blk_user()
                {
                    user_hakkeemid = e.CommandArgument.ToString(),
                };
                db.tbl_blk_users.InsertOnSubmit(tb);
                db.SubmitChanges();
                string email = "";
                string ph = "";
                string name = "";
                var user1 = from item in db.tbl_signups where item.u_hakkimid == e.CommandArgument.ToString() select item;
                foreach (var ss in user1)
                {
                    email = obj.DecryptString(ss.email);
                    ph = obj.DecryptString(ss.contact);
                    name = ss.name;
                }
                string msg = "Tempararly your account has been deactivated, please contact Hakkeem authority";
                try
                {

                    //   Email.mail(email, msg, "Account deactivated");
                    Email_To_AccountInActive(email);
                
                }
                catch (Exception ex) { }
                try
                {
                    ob.Message(ph, msg);
                }
                catch (Exception ex) { }

                var apmnt = from item in db.tbl_doctor_appointments where item.c_id == e.CommandArgument.ToString() && item.a_status == 1 select item;
                if (apmnt.Count() > 0)
                {
                    foreach (var ss in apmnt)
                    {
                        tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                        {
                            apmnt_id = ss.id.ToString(),
                            canceled_by = "hkm",
                            cancel_rsn = "Blocked by Hakkeem authority",
                            date = DateTime.Now.ToShortDateString(),
                            time = DateTime.Now.ToShortTimeString(),
                            u_id = ss.c_id,
                            d_id = ss.d_id,

                        };
                        db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                        db.SubmitChanges();

                        var doctor = from item in db.tbl_doctors where item.d_hakkimid == ss.d_id select item;
                        foreach (var u in doctor)
                        {
                            email = obj.DecryptString(u.d_email);
                            ph = obj.DecryptString(u.d_contact);
                        }
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_doctor_appointment where a_date='" + ss.a_date + "'", con);
                        sda1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            Session["Day"] = dt1.Rows[0]["dayname"].ToString();
                        }
                        msg = "Your Appointment has been Cancelled with Patient " + name + " on " + ss.a_date + " and " + ss.a_time + " Thank you Hakkeem Team";
                        try
                        {

                            //Email.mail(email, msg, "Hakkeem appointment canceled");
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
            users();
        }
        if (e.CommandName == "ublk")
        {
            var Query = from item in db.tbl_blk_users where item.user_hakkeemid == e.CommandArgument.ToString() select item;
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    db.tbl_blk_users.DeleteOnSubmit(ss);
                }
                db.SubmitChanges();


                string email = "";
                string ph = "";
                string name = "";
                var user1 = from item in db.tbl_signups where item.u_hakkimid == e.CommandArgument.ToString() select item;
                foreach (var ss in user1)
                {
                    email = obj.DecryptString(ss.email);
                    ph = obj.DecryptString(ss.contact);
                    name = ss.name;
                }
                string msg = "Your account has been activated by Hakkeem authority";
                try
                {
                    // Email.mail(email, msg, "Account activated");
                    Email_To_AccountActive(email);
                }
                catch (Exception ex) { }
                try
                {
                    ob.Message(ph, msg);
                }
                catch (Exception ex) { }


            }
            users();
        }
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
            messagestr = messagestr + "your appointment cancellation." + name + ", Time:" + time + " "+day+", Date:" + date + "";
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            var user = from item in db.tbl_signups where item.u_hakkimid == TextBox1.Text || item.contact == obj.EnryptString(TextBox1.Text) orderby item.id descending select item;
            if (user.Count() > 0)
            {
                GridView1.DataSource = user;
                GridView1.DataBind();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    string hakkeemid = (gr.FindControl("Label2") as Label).Text;
                    string contact = (gr.FindControl("Label3") as Label).Text;

                    Label lbl3 = gr.FindControl("Label3") as Label;
                    lbl3.Text = obj.DecryptString(lbl3.Text);


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
            GridView1.Visible = false;
        }
        else
        {
            users();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string email = "";
            string ph = "";
        con.Open();
        string id = Session["uhakkimid"].ToString();
        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_temp_signup(id,name,email,dob,contact,password,address,country,photo,status,gender,age,date_time,otp,passwordreset,u_hakkimid) select * from tbl_signup where u_hakkimid='" + id + "'", con);
        cmd.ExecuteNonQuery();
            SqlCommand cmd1=new SqlCommand ("Update tbl_temp_signup set reason='"+TextBox2.Text+ "',date='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where u_hakkimid='" + id + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
       
        var user = from item in db.tbl_signups where item.u_hakkimid == id select item;
        foreach (var ss in user)
        {
                email = obj.DecryptString(ss.email);
                ph= obj.DecryptString(ss.contact);
                db.tbl_signups.DeleteOnSubmit(ss);

        }
        db.SubmitChanges();
        try
        {
            string demail = "";
           
            string msg;
            string name = "";
            var data = from item in db.tbl_doctor_appointments where item.c_id == id select item;
            foreach (var ss in data)
            {
                if (ss.a_status == 1)
                {
                    var userdetails = from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item;
                    foreach (var ud in userdetails)
                    {
                        name = ud.name;
                    }
                    var doctor = from item in db.tbl_doctors where item.d_hakkimid == ss.d_id select item;
                    foreach (var u in doctor)
                    {
                        demail = obj.DecryptString(u.d_email);
                        ph = obj.DecryptString(u.d_contact);
                    }
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_doctor_appointment where a_date='" + ss.a_date + "'", con);
                        sda1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            Session["Day"] = dt1.Rows[0]["dayname"].ToString();
                        }
                        msg = "Your Appointment has been Cancelled with Patient " + name + " on " + ss.a_date + " and " + ss.a_time + " Thank you Hakkeem Team";
                    try
                    {

                            //Email.mail(email, msg, "Hakkeem appointment canceled");
                            Email_To_AppoinmentCancilation(email,name,ss.a_date,ss.a_time,Session["Day"].ToString());
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        ob.Message(ph, msg);
                    }
                    catch (Exception ex) { }
                }
            }
            db.tbl_doctor_appointments.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            var data = from item in db.tbl_hakkimIDs where item.hakkim_ID == id && item.user_type == "USR" select item;
            db.tbl_hakkimIDs.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            var data = from item in db.tbl_hos_appmnt_histories where item.u_id == id select item;
            db.tbl_hos_appmnt_histories.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            var data = from item in db.tbl_hos_doc_appmnts where item.u_id == id select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex)
        { }
        try
        {
            var data = from item in db.tbl_ratings where item.u_id == id select item;
            db.tbl_ratings.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            var data = from item in db.tbl_ratingviews where item.u_id == id select item;
            db.tbl_ratingviews.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            var data = from item in db.tbl_report_forms where item.u_id == id select item;
            db.tbl_report_forms.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex)
        {

        }
        try
        {
            var data = from item in db.tbl_test_reports where item.u_id == id select item;
            db.tbl_test_reports.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            var data = from item in db.tbl_user_feeds where item.u_email == id select item;
            db.tbl_user_feeds.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
        {
            var data = from item in db.tbl_apmnt_cancels where item.u_id == id select item;
            db.tbl_apmnt_cancels.DeleteAllOnSubmit(data);
            db.SubmitChanges();
        }
        catch (Exception ex) { }
        try
            {
                string pno = "+966" + ph.ToString();

                ob.Message(pno, " Your account has been removed by Hakkeem authority");

                string pno1 = "+91" + ph.ToString();

                ob.Message(pno1, " Your account has been removed by Hakkeem authority");
                //Email.mail(email, "Your account has been removed by Hakkeem authority", "Account deactivated");
                Email_To_AccountDeleteion(email);
             

            }
        catch (Exception ex)
        { }
        users();
        }
        catch (Exception ex)
        {

        }
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("users.aspx");
        //}
        //else
        //{
        //    Response.Redirect("users.aspx?l=ar-EG");
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
    protected void rdb_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdb_status.SelectedValue.ToString() == "0")
        {
            Response.Redirect("users.aspx");
        }
        else  if (rdb_status.SelectedValue.ToString()=="1")
        {
            con.Open();
            //DataTable dt = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_blk_users", con);
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
                //var user = from item in db.tbl_signups orderby item.id descending select item;
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_signup  where  u_hakkimid not in (select user_hakkeemid from tbl_blk_users)", con);
                sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    string hakkeemid = (gr.FindControl("Label2") as Label).Text;
                    string contact = (gr.FindControl("Label3") as Label).Text;

                    Label lbl3 = gr.FindControl("Label3") as Label;
                    lbl3.Text =  obj.DecryptString(lbl3.Text);


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
        else if(rdb_status.SelectedValue.ToString() == "2")
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_blk_users ", con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //var user = from item in db.tbl_signups orderby item.id descending select item;
                SqlDataAdapter sda1 = new SqlDataAdapter("Select blk.*,U.* from tbl_blk_users blk inner join tbl_signup U on U.u_hakkimid=blk.user_hakkeemid", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {

                    GridView1.Visible = true;
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        string hakkeemid = (gr.FindControl("Label2") as Label).Text;
                        string contact = (gr.FindControl("Label3") as Label).Text;

                        Label lbl3 = gr.FindControl("Label3") as Label;
                        lbl3.Text =  obj.DecryptString(lbl3.Text);


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
        else
        {
            users();
        }
    }
}