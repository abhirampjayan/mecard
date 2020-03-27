using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

public partial class Doctor_Doctor_availability : System.Web.UI.Page
{
    secure obj = new secure();
    SMS ob = new SMS();
    databaseDataContext db = new databaseDataContext();
    MailMessage Email = new MailMessage();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    public enum MessageType { Success, Error, Info, Warning };

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
        //    this.MasterPageFile = "~/Doctor/ArabicMasterPage.master";
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Timer t1 = new Timer();
        t1 = Master.FindControl("Timer1") as Timer;
        t1.Enabled = false;
        if (!IsPostBack)
        {
            CheckLocation();
            Doctor_Availability();
        }

    }


    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }



    public void CheckLocation()
    {
        var query = from item in db.tbl_doctor_locations
                    join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                    where item1.d_hakkimid == Session["hakkeemid_d"].ToString()
                    select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
        if (query.Count() <= 0)
        {
            Response.Redirect("~/Doctor/SetLocation.aspx");
        }
    }
    public void Doctor_Availability()
    {
        if (TextBox1.Text == "")
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("d_id"), new DataColumn("date"), new DataColumn("time") });
            string time = "";
            List<string> date = new List<string>();
            var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() select new { item.id, item.a_date };
            foreach (var ss in Query)
            {
                var Query1 = from item in db.tbl_doc_times where item.date_id == ss.id select item.time;
                foreach (var t in Query1)
                {
                    if (time == "")
                    {
                        time = "<label class='btn btn-xs btn-primary'>" + t + "</label>" + " ";
                    }
                    else
                    {
                        time += "<label class='btn btn-xs btn-primary'>" + " " + t + "</label>" + " ";
                    }
                }
                dt.Rows.Add(Session["hakkeemid_d"].ToString(), ss.a_date, time);
                time = "";
            }


            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("d_id"), new DataColumn("date"), new DataColumn("time") });
            string time = "";
            List<string> date = new List<string>();
            var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == TextBox1.Text select new { item.id, item.a_date };
            foreach (var ss in Query)
            {
                var Query1 = from item in db.tbl_doc_times where item.date_id == ss.id select item.time;
                foreach (var t in Query1)
                {
                    if (time == "")
                    {
                        time = "<label class='btn btn-xs btn-primary'>" + t + "</label>" + " ";
                    }
                    else
                    {
                        time += "<label class='btn btn-xs btn-primary'>" + " " + t + "</label>" + " ";
                    }
                }
                dt.Rows.Add(Session["hakkeemid_d"].ToString(), ss.a_date, time);
                time = "";
            }


            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Doctor_Availability();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        Doctor_Availability();

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[1] { new DataColumn("time") });
        CheckBoxList cbl1 = GridView1.Rows[GridView1.EditIndex].FindControl("CheckBoxList1") as CheckBoxList;
        LinkButton lnk2 = GridView1.Rows[GridView1.EditIndex].FindControl("LinkButton2") as LinkButton;
        Label lbl5 = GridView1.Rows[GridView1.EditIndex].FindControl("Label5") as Label;
        List<string> atime = new List<string>();
        string time = "";
        var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == lnk2.Text select item.id;
        foreach (var ss in Query)
        {
            var Query1 = from item in db.tbl_doc_times where item.date_id == ss select item.time;
            foreach (var t in Query1)
            {

                atime.Add(t);
            }

        }

        for (int i = 0; i < atime.Count(); i++)
        {
            dt.Rows.Add(atime[i].ToString());
        }

        cbl1.DataSource = dt;
        cbl1.DataTextField = "time";
        cbl1.DataBind();
        for (int i = 0; i < cbl1.Items.Count; i++)
        {
            cbl1.Items[i].Selected = true;
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Doctor_Availability();
    }
    //public static List<string> t = new List<string>();
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string time = "";
        Label lbl6 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label6") as Label;
        Label lbl7 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label7") as Label;
        CheckBoxList chk1 = GridView1.Rows[e.RowIndex].Cells[1].FindControl("CheckBoxList1") as CheckBoxList;
        List<string> ch = new List<string>();
        for (int i = 0; i < chk1.Items.Count; i++)
        {
            if (chk1.Items[i].Selected)
            {
                ch.Add(chk1.Items[i].Text);
            }
        }
        if (ch.Count == 0)
        {
            //ShowMessage("Checkbox doesn't null", MessageType.Success);
            //RegisterStartupScript("", "<Script Language=JavaScript>swal('Checkbox doesn't null')</Script>");
            //Label7.Text = "Checkbox doesn't null..!";
            //this.ModalPopupExtender4.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{

                RegisterStartupScript("", "<Script Language=JavaScript>swal('Checkbox doesnt null')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('مربع الاختيار لا فارغ')</Script>");
            //}
        }
        if (chk1.SelectedIndex != -1)
        {
            //Session["lbl6"] = lbl6.Text;
            //Session["lbl7"] = lbl7.Text;

            for (int i = 0; i < chk1.Items.Count; i++)
            {
                if (chk1.Items[i].Selected)
                {
                    if (time == "")
                    { time = chk1.Items[i].Text; }
                    else { time += "," + chk1.Items[i].Text; }
                }
                else
                {
                    string t = chk1.Items[i].Text;
                    int l = t.Length - 2;
                    string appointment_time = t.Insert(l, " ");
                    //t.Add(chk1.Items[i].Text);

                    var Query = from item in db.tbl_doctor_appointments where item.a_date == lbl6.Text && item.a_time == appointment_time && item.d_id == Session["hakkeemid_d"].ToString() select item;
                    if (Query.Count() > 0)
                    {
                        foreach (var ss in Query)
                        {
                            tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                            {
                                apmnt_id = ss.id.ToString(),
                                canceled_by = "d",
                                cancel_rsn = "Doctor changed the availability",
                                date = DateTime.Now.ToShortDateString(),
                                time = DateTime.Now.ToShortTimeString(),
                                u_id = ss.c_id,
                                d_id = ss.d_id,

                            };
                            db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                            db.SubmitChanges();



                            string name = "";
                            var Query1 = from item in db.tbl_doctors where item.d_email == Session["hakkeemid_d"].ToString() select item.d_name;
                            foreach (var n in Query1)
                            { name = n.ToString(); }
                            //string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
                            string msg = "Your Appointment has been Cancelled with Doctor " + name + " on " + ss.a_date + " and " + ss.a_time + " Thank you Hakkeem Team";
                            var user = (from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item).First();
                            try
                            {
                                //Email.mail(obj.DecryptString(user.email), msg, "Hakkeem appointment canceled");
                                Email_To_AppoinmentCancilation(obj.DecryptString(user.email), name, ss.a_date, ss.a_time);
                            }
                            catch (Exception ex) { }
                            string s = obj.DecryptString(user.contact);
                            try
                            {
                                ob.Message(s.Substring(1, s.Length), msg);
                            }
                            catch (Exception ex) { }
                            var Query2 = from item in db.tbl_doctor_appointments where item.id == int.Parse(ss.id.ToString()) select item;
                            foreach (var dd in Query2)
                            {


                                dd.a_status = 2;


                                //db.tbl_doctor_appointments.DeleteOnSubmit(dd);
                            }
                            db.SubmitChanges();
                        }
                    }

                    var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == lbl6.Text select item.id;
                    foreach (var d in Query4)
                    {
                        var Query5 = from item in db.tbl_doc_times where item.date_id == d && item.time == t select item;
                        foreach (var dd in Query5)
                        {
                            db.tbl_doc_times.DeleteOnSubmit(dd);
                        }
                        db.SubmitChanges();
                    }
                }


                GridView1.EditIndex = -1;
                Doctor_Availability();


            }
            //ShowMessage("Successfully changed the availability", MessageType.Success);
            //Label2.Text = "<b>Do you want to change the availability..?</b>";

            //this.ModalPopupExtender2.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully changed the availability')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير التوفر بنجاح')</Script>");
            //}
        }

    }


    //protected void BtnConfirm_Click(object sender, EventArgs e)
    //{

    //}

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Doctor_Availability();
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor availability.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor availability.aspx?l=ar-EG");
        //}
    }





    //public static List<string> adate = new List<string>();
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        Label lbl8 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label8") as Label;
        LinkButton lnk5 = GridView1.Rows[e.RowIndex].Cells[1].FindControl("LinkButton5") as LinkButton;
        LinkButton lnk2 = GridView1.Rows[e.RowIndex].Cells[0].FindControl("LinkButton2") as LinkButton;
        //Session["lbl8"] = lbl8.Text;
        //Session["lnk5"] = lnk5.Text;
        //Session["lnk2"] = lnk2.Text;
        string[] a = lnk5.Text.Split(',');
        //for (int i = 0; i < a.Count(); i++)
        //{
        //    adate.Add(a[i].ToString());
        //}
        //Label3.Text = "Do you want to remove this available time..?";
        //ModalPopupExtender3.Show();
        for (int i = 0; i < a.Count(); i++)
        {
            string appointment_time = a[i].ToString();
            int l = appointment_time.Length - 2;
            appointment_time = appointment_time.Insert(l, " ");
            var Query = from item in db.tbl_doctor_appointments where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == lnk2.Text && item.a_time == appointment_time select item;
            if (Query.Count() > 0)
            {

                foreach (var ss in Query)
                {

                    tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                    {
                        apmnt_id = ss.id.ToString(),
                        canceled_by = "d",
                        cancel_rsn = "Doctor removed the availability",
                        date = DateTime.Now.ToShortDateString(),
                        time = DateTime.Now.ToShortTimeString(),
                        u_id = ss.c_id,
                        d_id = ss.d_id,

                    };
                    db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                    db.SubmitChanges();

                    string name = "";
                    var Query1 = from item in db.tbl_doctors where item.d_email == Session["hakkeemid_d"].ToString() select item.d_name;
                    foreach (var n in Query1)
                    { name = n.ToString(); }
                    //string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
                    string msg = "Your Appointment has been Cancelled with Doctor " + name + " on " + ss.a_date + " and " + ss.a_time + " Thank you Hakkeem Team";

                    var user = (from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item).First();
                    try
                    {
                        // Email.mail(obj.DecryptString(user.email), msg, "Appointment canceled");
                        Email_To_AppoinmentCancilation(obj.DecryptString(user.email), name, ss.a_date, ss.a_time);
                    }
                    catch (Exception ex) { }
                    string s = obj.DecryptString(user.contact);
                    try
                    {
                        ob.Message(s.Substring(1, s.Length), msg);
                    }
                    catch (Exception ex) { }
                    //db.tbl_doctor_appointments.DeleteOnSubmit(ss);
                    ss.a_status = 2;
                    db.SubmitChanges();
                }
            }
        }
        var Query2 = from item in db.doctor_availabilities where item.a_date == lnk2.Text && item.d_id == Session["hakkeemid_d"].ToString() select item;
        foreach (var dd in Query2)
        {
            var Query3 = from item in db.tbl_doc_times where item.date_id == dd.id select item;
            foreach (var ss in Query3)
            {
                db.tbl_doc_times.DeleteOnSubmit(ss);
                db.SubmitChanges();
            }
            db.doctor_availabilities.DeleteOnSubmit(dd);

        }
        db.SubmitChanges();
        Doctor_Availability();
    }
    public bool Email_To_AppoinmentCancilation(string email, string name, string date, string time)
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
            messagestr = messagestr + "your appointment cancellation." + name + ", Time:" + time + ", Date:" + date + "";
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
    public bool Email_To_AppoinmentCancilationpatient(string email, string name, string date, string time)
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
            messagestr = messagestr + "your appointment cancellation." + name + ", Time:" + time + ", Date:" + date + "";
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        Doctor_Availability();
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor availability.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor availability.aspx?l=ar-EG");
        //}
    }


    //Confirm button- want to change availability
    protected void Button1_Click(object sender, EventArgs e)
    {


        //for (int i = 0; i < t.Count(); i++)
        //{
        //    var Query = from item in db.tbl_doctor_appointments where item.a_date == Session["lbl6"].ToString() && item.a_time == t[i] && item.d_id == Session["hakkeemid_d"].ToString() select item;
        //    if (Query.Count() > 0)
        //    {
        //        foreach (var ss in Query)
        //        {
        //            string name = "";
        //            var Query1 = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item.d_name;
        //            foreach (var n in Query1)
        //            { name = n.ToString(); }
        //            string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
        //            Email.mail(ss.c_id, msg, "Appointment canceled");
        //            var Query2 = from item in db.tbl_doctor_appointments where item.id == int.Parse(ss.id.ToString()) select item;
        //            foreach (var dd in Query2)
        //            {
        //                db.tbl_doctor_appointments.DeleteOnSubmit(dd);
        //            }
        //            db.SubmitChanges();
        //        }
        //    }

        //    var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == Session["lbl6"].ToString() select item.id;
        //    foreach (var d in Query4)
        //    {
        //        var Query5 = from item in db.tbl_doc_times where item.date_id == d && item.time == t[i].ToString() select item;
        //        foreach (var dd in Query5)
        //        {
        //            db.tbl_doc_times.DeleteOnSubmit(dd);
        //        }
        //        db.SubmitChanges();
        //    }
        //}
        //t.Clear();
        //Doctor_Availability();
        //Response.Redirect("~/Doctor/Doctor availability.aspx");
    }


    // Remove availability - Confirm
    protected void Button5_Click(object sender, EventArgs e)
    {
        //for (int i = 0; i < adate.Count(); i++)
        //{
        //    var Query = from item in db.tbl_doctor_appointments where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == Session["lnk2"].ToString() && item.a_time == adate[i] select item;
        //    if (Query.Count() > 0)
        //    {

        //        foreach (var ss in Query)
        //        {
        //            string name = "";
        //            var Query1 = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item.d_name;
        //            foreach (var n in Query1)
        //            { name = n.ToString(); }
        //            string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
        //            Email.mail(ss.c_id, msg, "Appointment canceled");

        //            db.tbl_doctor_appointments.DeleteOnSubmit(ss);
        //            db.SubmitChanges();
        //        }
        //    }
        //}
        //var Query2 = from item in db.doctor_availabilities where item.a_date == Session["lnk2"].ToString() && item.d_id == Session["hakkeemid_d"].ToString() select item;
        //foreach (var dd in Query2)
        //{
        //    var Query3 = from item in db.tbl_doc_times where item.date_id == dd.id select item;
        //    foreach (var ss in Query3)
        //    {
        //        db.tbl_doc_times.DeleteOnSubmit(ss);
        //        db.SubmitChanges();
        //    }
        //    db.doctor_availabilities.DeleteOnSubmit(dd);
        //    db.SubmitChanges();
        //}
        //adate.Clear();

        //Doctor_Availability();
        //Response.Redirect("~/Doctor/Doctor availability.aspx");
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        Doctor_Availability();
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor availability.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor availability.aspx?l=ar-EG");
        //}
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        TextBox2.Text = "";
        Panel1.Visible = true;
        Panel2.Visible = false;
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("d_id"), new DataColumn("date"), new DataColumn("time") });
        string time = "";
        //List<string> date = new List<string>();
        var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == TextBox1.Text select new { item.id, item.a_date };
        foreach (var ss in Query)
        {
            var Query1 = from item in db.tbl_doc_times where item.date_id == ss.id select item.time;
            foreach (var t in Query1)
            {
                if (time == "")
                {
                    time = "<label class='btn btn-xs btn-primary'>" + t + "</label>" + " ";
                }
                else
                {
                    time += "<label class='btn btn-xs btn-primary'>" + " " + t + "</label>" + " ";
                }
            }
            dt.Rows.Add(Session["hakkeemid_d"].ToString(), ss.a_date, time);
            time = "";
        }


        GridView1.DataSource = dt;
        GridView1.DataBind();
        TextBox2.Enabled = true;
    }
    public static List<string> removedate = new List<string>();
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        List<string> date = new List<string>();
        DateTime from_date = DateTime.Parse(TextBox1.Text);
        DateTime to_date = DateTime.Parse(TextBox2.Text);

        //date.Add(from_date.ToString("yyyy-MM-dd"));
        //from_date.AddDays(1);
        for (DateTime i = from_date; i <= to_date; i = i.AddDays(1))
        {
            date.Add(i.ToString("yyyy-MM-dd"));
        }

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("d_id"), new DataColumn("date"), new DataColumn("time") });
        string time = "";

        //where d.RecordDateTime >= StartDate && d.RecordDateTime <= EndDate
        foreach (var dd in date)
        {
            var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == dd select new { item.id, item.a_date };
            foreach (var ss in Query)
            {
                var Query1 = from item in db.tbl_doc_times where item.date_id == ss.id select item.time;
                foreach (var t in Query1)
                {
                    if (time == "")
                    {
                        time = "<label class='btn btn-xs btn-primary'>" + t + "</label>" + " ";
                    }
                    else
                    {
                        time += "<label class='btn btn-xs btn-primary'>" + " " + t + "</label>" + " ";
                    }
                }
                dt.Rows.Add(Session["hakkeemid_d"].ToString(), ss.a_date, time);
                time = "";
            }

        }
        foreach (var d in date)
        {
            removedate.Add(d);
        }
        date.Clear();

        GridView2.DataSource = dt;
        GridView2.DataBind();

        if (GridView2.Rows.Count > 0)
        {

        }
        else
        {

            //Label12.Text = "You have no availability in given date range";
            //this.ModalPopupExtender8.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You have no availability in given date range')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('ليس لديك توفر في نطاق زمني معين')</Script>");
            //}

        }
    }

    //protected void Remove_Click(object sender, EventArgs e)
    //{


    //}

    protected void Button8_Click(object sender, EventArgs e)
    {

    }

    // Confirm multiple selected availabilities
    protected void Button7_Click(object sender, EventArgs e)
    {
        //foreach (var dd in removedate)
        //{

        //    var Query11 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item.id;
        //    foreach (var i in Query11)
        //    {
        //        var Query22 = from item in db.tbl_doc_times where item.date_id == i select item.time;
        //        foreach (var at in Query22)
        //        {

        //            var Query = from item in db.tbl_doctor_appointments where item.a_date == dd && item.a_time == at && item.d_id == Session["hakkeemid_d"].ToString() select item;
        //            if (Query.Count() > 0)
        //            {
        //                foreach (var ss in Query)
        //                {
        //                    string name = "";
        //                    var Query1 = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item.d_name;
        //                    foreach (var n in Query1)
        //                    { name = n.ToString(); }
        //                    string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
        //                    Email.mail(ss.c_id, msg, "Appointment canceled");
        //                    var Query2 = from item in db.tbl_doctor_appointments where item.id == int.Parse(ss.id.ToString()) select item;
        //                    foreach (var d in Query2)
        //                    {
        //                        db.tbl_doctor_appointments.DeleteOnSubmit(d);
        //                    }
        //                    db.SubmitChanges();
        //                }
        //            }

        //            var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == dd select item.id;
        //            foreach (var d in Query4)
        //            {
        //                var Query5 = from item in db.tbl_doc_times where item.date_id == d && item.time == at select item;
        //                foreach (var d1 in Query5)
        //                {
        //                    db.tbl_doc_times.DeleteOnSubmit(d1);
        //                }
        //                db.SubmitChanges();

        //            }
        //        }
        //    }

        //    var Query33 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item;
        //    foreach (var ss in Query33)
        //    {
        //        db.doctor_availabilities.DeleteOnSubmit(ss);
        //    }
        //    db.SubmitChanges();

        //}
        //removedate.Clear();
        //Doctor_Availability();
        ////Label12.Text = "Successfully remove selected date and its available time also";
        ////this.ModalPopupExtender8.Show();
        //RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully remove selected date and its available time also')</Script>");
    }



    protected void Button3_Click(object sender, EventArgs e)
    {
        DateTime d1 = DateTime.Parse(DateTime.Now.ToShortDateString());
        DateTime d2 = DateTime.Parse(TextBox1.Text);
        DateTime tdate = DateTime.Parse(TextBox1.Text);
        if (d1 == d2)
        {
            //Label7.Text = "You can't choose the current date in selected date, if you want edit current date availability please edit in separately";
            //this.ModalPopupExtender4.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You cant choose the current date in selected date, if you want edit current date availability please edit in separately')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا يمكنك اختيار التاريخ الحالي في التاريخ المحدد، إذا كنت ترغب في تحرير توافر التاريخ الحالي يرجى تعديل في بشكل منفصل')</Script>");

            //}
        }
        else
        {
            string ts = DateTime.Parse(TextBox4.Text).ToShortTimeString();
            string te = DateTime.Parse(TextBox5.Text).ToShortTimeString();
            string tn = DateTime.Now.ToShortTimeString();
            if (DateTime.Parse(ts) > DateTime.Parse(te))
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You selected time is less than the starting time.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد اخترت الوقت أقل من وقت البدء')</Script>");

                //}

                //Label7.Text = "<b>Alert!</b>   <p>You selected time is less than the starting time.</p>";
                //Label7.ForeColor = System.Drawing.Color.Red;
                //this.ModalPopupExtender4.Show();
            }
            else
            {
                List<string> dt = new List<string>();

                DateTime t1 = DateTime.Parse(TextBox4.Text);

                DateTime t2 = DateTime.Parse(TextBox5.Text);

                string start_time = t1.ToShortTimeString();
                string end_time = t2.ToShortTimeString();
                int d = int.Parse(DropDownList1.SelectedItem.Text);
                d = d + int.Parse(DropDownList2.SelectedItem.Text);
                while (DateTime.Parse(start_time) < DateTime.Parse(end_time))
                {

                    if (DateTime.Parse(start_time).AddMinutes(d) <= DateTime.Parse(end_time))
                    {
                        dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));
                        start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                    }
                    else
                    {
                        break;
                        //message
                    }


                }
                if (dt.Count() > 0)
                {
                    CheckBoxList2.DataSource = dt;
                    CheckBoxList2.DataBind();
                    for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                    {
                        CheckBoxList2.Items[i].Selected = true;
                    }
                    //Change code........for new popup window

                    if (CheckBoxList2.SelectedIndex != -1)
                    {
                        foreach (var dd in removedate)
                        {

                            var Query11 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item.id;
                            foreach (var i in Query11)
                            {
                                var Query22 = from item in db.tbl_doc_times where item.date_id == i select item.time;
                                foreach (var at in Query22)
                                {
                                    string appointment_time = at;
                                    int l = appointment_time.Length - 2;
                                    appointment_time = appointment_time.Insert(l, " ");
                                    var Query = from item in db.tbl_doctor_appointments where item.a_date == dd && item.a_time == appointment_time && item.d_id == Session["hakkeemid_d"].ToString() select item;
                                    if (Query.Count() > 0)
                                    {
                                        foreach (var ss in Query)
                                        {
                                            tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                                            {
                                                apmnt_id = ss.id.ToString(),
                                                canceled_by = "d",
                                                cancel_rsn = "Doctor removed the availability",
                                                date = DateTime.Now.ToShortDateString(),
                                                time = DateTime.Now.ToShortTimeString(),
                                                u_id = ss.c_id,
                                                d_id = ss.d_id,

                                            };
                                            db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                                            db.SubmitChanges();


                                            string name = "";
                                            var Query1 = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item.d_name;
                                            foreach (var n in Query1)
                                            { name = n.ToString(); }
                                            //string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " change the available time, please take another appointment the same date";
                                            string msg = "Your Appointment has been Cancelled with Doctor " + name + " on " + ss.a_date + " and " + ss.a_time + " Thank you Hakkeem Team";
                                            var user = (from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item).First();
                                            // Email.mail(obj.DecryptString(user.email), msg, "Appointment canceled");
                                            Email_To_AppoinmentCancilation(obj.DecryptString(user.email), name, ss.a_date, ss.a_time);
                                            string s = obj.DecryptString(user.contact);
                                            ob.Message(s.Substring(1, s.Length), msg);
                                            var Query2 = from item in db.tbl_doctor_appointments where item.id == int.Parse(ss.id.ToString()) select item;
                                            foreach (var ddd in Query2)
                                            {
                                                ddd.a_status = 2;
                                                //db.tbl_doctor_appointments.DeleteOnSubmit(ddd);
                                            }
                                            db.SubmitChanges();
                                        }
                                    }

                                    var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == dd select item.id;
                                    foreach (var ddd in Query4)
                                    {
                                        var Query5 = from item in db.tbl_doc_times where item.date_id == d && item.time == at select item;
                                        foreach (var ddd1 in Query5)
                                        {
                                            db.tbl_doc_times.DeleteOnSubmit(ddd1);
                                        }
                                        db.SubmitChanges();

                                    }
                                }
                            }

                            var Query33 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item;
                            foreach (var ss in Query33)
                            {
                                db.doctor_availabilities.DeleteOnSubmit(ss);
                            }
                            db.SubmitChanges();



                        }

                        foreach (var ss in removedate)
                        {
                            doctor_availability da = new doctor_availability()
                            {
                                d_id = Session["hakkeemid_d"].ToString(),

                                a_date = ss,
                                //a_time = "",
                                status = 0,
                            };
                            db.doctor_availabilities.InsertOnSubmit(da);
                            db.SubmitChanges();
                            var QueryDate = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() select item.id;
                            int max = Convert.ToInt32(QueryDate.Max());
                            int i = 0;
                            while (i < CheckBoxList2.Items.Count)
                            {

                                if (CheckBoxList2.Items[i].Selected)
                                {
                                    tbl_doc_time dddt = new tbl_doc_time()
                                    {
                                        date_id = max,
                                        time = CheckBoxList2.Items[i].Text,
                                    };
                                    db.tbl_doc_times.InsertOnSubmit(dddt);
                                    db.SubmitChanges();
                                }

                                i++;
                            }
                        }
                        removedate.Clear();
                        //Label12.Text = "Successfully changed the available time in selected date";
                        //this.ModalPopupExtender8.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully changed the available time in selected date.')</Script>");

                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير الوقت المتاح بنجاح في التاريخ المحدد بنجاح')</Script>");

                        //}
                    }

                    else
                    {
                        //Label11.Text = "Please select at least one time";
                        //this.ModalPopupExtender7.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select at least one time.')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد مرة واحدة على الأقل.')</Script>");

                        //}

                    }
                    //End the new modal design


                    //Confusion..........
                    //Label10.Text = "Do you want change available time..?";
                    //this.ModalPopupExtender6.Show();
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد')</Script>");

                    //}
                    //Label7.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                    //Label7.ForeColor = System.Drawing.Color.Red;
                    //this.ModalPopupExtender4.Show();
                }

            }
        }
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        //if (CheckBoxList2.SelectedIndex != -1)
        //{
        //    foreach (var dd in removedate)
        //    {

        //        var Query11 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item.id;
        //        foreach (var i in Query11)
        //        {
        //            var Query22 = from item in db.tbl_doc_times where item.date_id == i select item.time;
        //            foreach (var at in Query22)
        //            {

        //                var Query = from item in db.tbl_doctor_appointments where item.a_date == dd && item.a_time == at && item.d_id == Session["hakkeemid_d"].ToString() select item;
        //                if (Query.Count() > 0)
        //                {
        //                    foreach (var ss in Query)
        //                    {
        //                        string name = "";
        //                        var Query1 = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item.d_name;
        //                        foreach (var n in Query1)
        //                        { name = n.ToString(); }
        //                        string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " change the available time, please take another appointment the same date";
        //                        Email.mail(ss.c_id, msg, "Appointment canceled");
        //                        var Query2 = from item in db.tbl_doctor_appointments where item.id == int.Parse(ss.id.ToString()) select item;
        //                        foreach (var d in Query2)
        //                        {
        //                            db.tbl_doctor_appointments.DeleteOnSubmit(d);
        //                        }
        //                        db.SubmitChanges();
        //                    }
        //                }

        //                var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == dd select item.id;
        //                foreach (var d in Query4)
        //                {
        //                    var Query5 = from item in db.tbl_doc_times where item.date_id == d && item.time == at select item;
        //                    foreach (var d1 in Query5)
        //                    {
        //                        db.tbl_doc_times.DeleteOnSubmit(d1);
        //                    }
        //                    db.SubmitChanges();

        //                }
        //            }
        //        }

        //        var Query33 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item;
        //        foreach (var ss in Query33)
        //        {
        //            db.doctor_availabilities.DeleteOnSubmit(ss);
        //        }
        //        db.SubmitChanges();



        //    }

        //    foreach (var ss in removedate)
        //    {
        //        doctor_availability da = new doctor_availability()
        //        {
        //            d_id = Session["hakkeemid_d"].ToString(),

        //            a_date = ss,
        //            //a_time = "",
        //            status = 0,
        //        };
        //        db.doctor_availabilities.InsertOnSubmit(da);
        //        db.SubmitChanges();
        //        var QueryDate = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() select item.id;
        //        int max = Convert.ToInt32(QueryDate.Max());
        //        int i = 0;
        //        while (i < CheckBoxList2.Items.Count)
        //        {

        //            if (CheckBoxList2.Items[i].Selected)
        //            {
        //                tbl_doc_time dt = new tbl_doc_time()
        //                {
        //                    date_id = max,
        //                    time = CheckBoxList2.Items[i].Text,
        //                };
        //                db.tbl_doc_times.InsertOnSubmit(dt);
        //                db.SubmitChanges();
        //            }

        //            i++;
        //        }
        //    }
        //    removedate.Clear();
        //    //Label12.Text = "Successfully changed the available time in selected date";
        //    //this.ModalPopupExtender8.Show();
        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully changed the available time in selected date.')</Script>");


        //}
        //else
        //{
        //    //Label11.Text = "Please select at least one time";
        //    //this.ModalPopupExtender7.Show();
        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select at least one time.')</Script>");

        //}
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor availability.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor availability.aspx?l = ar - EG");
        //}
    }



    protected void Button13_Click(object sender, EventArgs e)
    {
        //this.ModalPopupExtender6.Show();
    }

    protected void Button14_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor availability.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor availability.aspx?l = ar - EG");
        //}
    }

    protected void RemoveAll_Click(object sender, EventArgs e)
    {
        //Label9.Text = "Do you want remove this selected availability..?";
        //ModalPopupExtender5.Show();

        foreach (var dd in removedate)
        {

            var Query11 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item.id;
            foreach (var i in Query11)
            {
                var Query22 = from item in db.tbl_doc_times where item.date_id == i select item.time;
                foreach (var at in Query22)
                {
                    string appointment_time = at;
                    int l = appointment_time.Length - 2;
                    appointment_time = appointment_time.Insert(l, " ");

                    var Query = from item in db.tbl_doctor_appointments where item.a_date == dd && item.a_time == appointment_time && item.d_id == Session["hakkeemid_d"].ToString() select item;
                    if (Query.Count() > 0)
                    {
                        foreach (var ss in Query)
                        {

                            tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                            {
                                apmnt_id = ss.id.ToString(),
                                canceled_by = "d",
                                cancel_rsn = "Doctor removed the availability",
                                date = DateTime.Now.ToShortDateString(),
                                time = DateTime.Now.ToShortTimeString(),
                                u_id = ss.c_id,
                                d_id = ss.d_id,

                            };
                            db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                            db.SubmitChanges();


                            string name = "";
                            var Query1 = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item.d_name;
                            foreach (var n in Query1)
                            { name = n.ToString(); }
                            //string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
                            string msg = "Your Appointment has been Cancelled with Doctor " + name + " on " + ss.a_date + " and " + ss.a_time + " Thank you Hakkeem Team";

                            //Email.mail(ss.c_id, msg, "Appointment canceled");
                            var user = (from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item).First();
                            try
                            {
                                // Email.mail(obj.DecryptString(user.email), msg, "Hakkeem appointment canceled");
                                Email_To_AppoinmentCancilation(obj.DecryptString(user.email), name, ss.a_date, ss.a_time);
                            }
                            catch (Exception ex) { }
                            string s = obj.DecryptString(user.contact);
                            try
                            {
                                ob.Message(s.Substring(1, s.Length), msg);
                            }
                            catch (Exception ex) { }
                            var Query2 = from item in db.tbl_doctor_appointments where item.id == int.Parse(ss.id.ToString()) select item;
                            foreach (var d in Query2)
                            {
                                ss.a_status = 2;
                                //db.tbl_doctor_appointments.DeleteOnSubmit(d);
                            }
                            db.SubmitChanges();
                        }
                    }

                    var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == dd select item.id;
                    foreach (var d in Query4)
                    {
                        var Query5 = from item in db.tbl_doc_times where item.date_id == d && item.time == at select item;
                        foreach (var d1 in Query5)
                        {
                            db.tbl_doc_times.DeleteOnSubmit(d1);
                        }
                        db.SubmitChanges();

                    }
                }
            }

            var Query33 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item;
            foreach (var ss in Query33)
            {
                db.doctor_availabilities.DeleteOnSubmit(ss);
            }
            db.SubmitChanges();

        }
        removedate.Clear();
        Doctor_Availability();
        //Label12.Text = "Successfully remove selected date and its available time also";
        //this.ModalPopupExtender8.Show();
        //RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully remove selected date and its available time also')</Script>");
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor availability.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor availability.aspx?l=ar-EG");
        //}
    }



    protected void ChangeAllTime_Click(object sender, EventArgs e)
    {
        DateTime d1 = DateTime.Parse(DateTime.Now.ToShortDateString());
        DateTime d2 = DateTime.Parse(TextBox1.Text);
        DateTime tdate = DateTime.Parse(TextBox1.Text);
        if (d1 == d2)
        {
            //Label7.Text = "You can't choose the current date in selected date, if you want edit current date availability please edit in separately";
            //this.ModalPopupExtender4.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You cant choose the current date in selected date, if you want edit current date availability please edit in separately')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا يمكنك اختيار التاريخ الحالي في التاريخ المحدد، إذا كنت ترغب في تحرير توافر التاريخ الحالي يرجى تعديل في بشكل منفصل')</Script>");

            //}
        }
        else
        {
            string ts = DateTime.Parse(TextBox4.Text).ToShortTimeString();
            string te = DateTime.Parse(TextBox5.Text).ToShortTimeString();
            string tn = DateTime.Now.ToShortTimeString();
            if (DateTime.Parse(ts) > DateTime.Parse(te))
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You selected time is less than the starting time.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد اخترت الوقت أقل من وقت البدء')</Script>");

                //}

                //Label7.Text = "<b>Alert!</b>   <p>You selected time is less than the starting time.</p>";
                //Label7.ForeColor = System.Drawing.Color.Red;
                //this.ModalPopupExtender4.Show();
            }
            else
            {
                List<string> dt = new List<string>();

                DateTime t1 = DateTime.Parse(TextBox4.Text);

                DateTime t2 = DateTime.Parse(TextBox5.Text);

                string start_time = t1.ToShortTimeString();
                string end_time = t2.ToShortTimeString();
                int d = int.Parse(DropDownList1.SelectedItem.Text);
                d = d + int.Parse(DropDownList2.SelectedItem.Text);
                while (DateTime.Parse(start_time) < DateTime.Parse(end_time))
                {

                    if (DateTime.Parse(start_time).AddMinutes(d) <= DateTime.Parse(end_time))
                    {
                        dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));
                        start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                    }
                    else
                    {
                        break;
                        //message
                    }


                }
                if (dt.Count() > 0)
                {
                    CheckBoxList2.DataSource = dt;
                    CheckBoxList2.DataBind();
                    for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                    {
                        CheckBoxList2.Items[i].Selected = true;
                    }
                    //Change code........for new popup window

                    if (CheckBoxList2.SelectedIndex != -1)
                    {
                        foreach (var dd in removedate)
                        {

                            var Query11 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item.id;
                            foreach (var i in Query11)
                            {
                                var Query22 = from item in db.tbl_doc_times where item.date_id == i select item.time;
                                foreach (var at in Query22)
                                {
                                    string appointment_time = at;
                                    int l = appointment_time.Length - 2;
                                    appointment_time = appointment_time.Insert(l, " ");

                                    var Query = from item in db.tbl_doctor_appointments where item.a_date == dd && item.a_time == appointment_time && item.d_id == Session["hakkeemid_d"].ToString() select item;
                                    if (Query.Count() > 0)
                                    {
                                        foreach (var ss in Query)
                                        {

                                            tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                                            {
                                                apmnt_id = ss.id.ToString(),
                                                canceled_by = "d",
                                                cancel_rsn = "Doctor changed the availability",
                                                date = DateTime.Now.ToShortDateString(),
                                                time = DateTime.Now.ToShortTimeString(),
                                                u_id = ss.c_id,
                                                d_id = ss.d_id,

                                            };
                                            db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                                            db.SubmitChanges();

                                            string name = "";
                                            var Query1 = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item.d_name;
                                            foreach (var n in Query1)
                                            { name = n.ToString(); }
                                            //string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " change the available time, please take another appointment the same date";
                                            string msg = "Your Appointment has been Cancelled with Doctor " + name + " on " + ss.a_date + " and " + ss.a_time + " Thank you Hakkeem Team";

                                            //Email.mail(ss.c_id, msg, "Appointment canceled");
                                            var user = (from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item).First();
                                            try
                                            {
                                                // Email.mail(obj.DecryptString(user.email), msg, "Hakkeem appointment canceled");
                                                Email_To_AppoinmentCancilation(obj.DecryptString(user.email), name, ss.a_date, ss.a_time);
                                            }
                                            catch (Exception ex) { }
                                            string s = obj.DecryptString(user.contact);
                                            try
                                            {
                                                ob.Message(s.Substring(1, s.Length), msg);
                                            }
                                            catch (Exception ex) { }
                                            var Query2 = from item in db.tbl_doctor_appointments where item.id == int.Parse(ss.id.ToString()) select item;
                                            foreach (var ddd in Query2)
                                            {
                                                ss.a_status = 2;
                                                //db.tbl_doctor_appointments.DeleteOnSubmit(ddd);
                                            }
                                            db.SubmitChanges();
                                        }
                                    }

                                    var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == dd select item.id;
                                    foreach (var ddd in Query4)
                                    {
                                        var Query5 = from item in db.tbl_doc_times where item.date_id == d && item.time == at select item;
                                        foreach (var ddd1 in Query5)
                                        {
                                            db.tbl_doc_times.DeleteOnSubmit(ddd1);
                                        }
                                        db.SubmitChanges();

                                    }
                                }
                            }

                            var Query33 = from item in db.doctor_availabilities where item.a_date == dd && item.d_id == Session["hakkeemid_d"].ToString() select item;
                            foreach (var ss in Query33)
                            {
                                db.doctor_availabilities.DeleteOnSubmit(ss);
                            }
                            db.SubmitChanges();



                        }

                        foreach (var ss in removedate)
                        {
                            doctor_availability da = new doctor_availability()
                            {
                                d_id = Session["hakkeemid_d"].ToString(),

                                a_date = ss,
                                //a_time = "",
                                status = 0,
                            };
                            db.doctor_availabilities.InsertOnSubmit(da);
                            db.SubmitChanges();
                            var QueryDate = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() select item.id;
                            int max = Convert.ToInt32(QueryDate.Max());
                            int i = 0;
                            while (i < CheckBoxList2.Items.Count)
                            {

                                if (CheckBoxList2.Items[i].Selected)
                                {
                                    tbl_doc_time dddt = new tbl_doc_time()
                                    {
                                        date_id = max,
                                        time = CheckBoxList2.Items[i].Text,
                                    };
                                    db.tbl_doc_times.InsertOnSubmit(dddt);
                                    db.SubmitChanges();
                                }

                                i++;
                            }
                        }
                        removedate.Clear();
                        //Label12.Text = "Successfully changed the available time in selected date";
                        //this.ModalPopupExtender8.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully changed the available time in selected date.')</Script>");

                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير الوقت المتاح بنجاح في التاريخ المحدد بنجاح')</Script>");

                        //}
                    }

                    else
                    {
                        //Label11.Text = "Please select at least one time";
                        //this.ModalPopupExtender7.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select at least one time.')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد مرة واحدة على الأقل')</Script>");

                        //}

                    }
                    //End the new modal design


                    //Confusion..........
                    //Label10.Text = "Do you want change available time..?";
                    //this.ModalPopupExtender6.Show();
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد')</Script>");

                    //}
                    //Label7.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                    //Label7.ForeColor = System.Drawing.Color.Red;
                    //this.ModalPopupExtender4.Show();
                }

            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="engaged")
        {
            GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

            int RowIndex = gvr.RowIndex;

            string time = "";
            Label lbl6 = GridView1.Rows[RowIndex].Cells[2].FindControl("Label6") as Label;
            Label lbl7 = GridView1.Rows[RowIndex].Cells[2].FindControl("Label7") as Label;
            CheckBoxList chk1 = GridView1.Rows[RowIndex].Cells[1].FindControl("CheckBoxList1") as CheckBoxList;
            List<string> ch = new List<string>();
            for (int i = 0; i < chk1.Items.Count; i++)
            {
                if (chk1.Items[i].Selected)
                {
                    ch.Add(chk1.Items[i].Text);
                }
            }
            //if (ch.Count == 0)
            //{
            //    //ShowMessage("Checkbox doesn't null", MessageType.Success);
            //    //RegisterStartupScript("", "<Script Language=JavaScript>swal('Checkbox doesn't null')</Script>");
            //    //Label7.Text = "Checkbox doesn't null..!";
            //    //this.ModalPopupExtender4.Show();
            //    if (Session["Language"].ToString() == "Auto")
            //    {

            //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Checkbox doesnt null')</Script>");
            //    }
            //    else
            //    {
            //        RegisterStartupScript("", "<Script Language=JavaScript>swal('مربع الاختيار لا فارغ')</Script>");
            //    }
            //}
            //if (chk1.SelectedIndex != -1)
            //{
                //Session["lbl6"] = lbl6.Text;
                //Session["lbl7"] = lbl7.Text;

                for (int i = 0; i < chk1.Items.Count; i++)
                {
                    if (chk1.Items[i].Selected)
                    {
                        if (time == "")
                        { time = chk1.Items[i].Text; }
                        else { time += "," + chk1.Items[i].Text; }
                    }
                    else
                    {
                        string t = chk1.Items[i].Text;
                        //t.Add(chk1.Items[i].Text);
                        int l = t.Length - 2;
                        t = t.Insert(l, " ");

                        var Query = from item in db.tbl_doctor_appointments where item.a_date == lbl6.Text && item.a_time == t && item.d_id == Session["hakkeemid_d"].ToString() select item;
                        if (Query.Count() > 0)
                        {
                            foreach (var ss in Query)
                            {

                                tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                                {
                                    apmnt_id = ss.id.ToString(),
                                    canceled_by = "d",
                                    cancel_rsn = "Doctor engaged on this time",
                                    date = DateTime.Now.ToShortDateString(),
                                    time = DateTime.Now.ToShortTimeString(),
                                    u_id = ss.c_id,
                                    d_id = ss.d_id,

                                };
                                db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                                db.SubmitChanges();

                                string name = "";
                                var Query1 = from item in db.tbl_doctors where item.d_email == Session["hakkeemid_d"].ToString() select item;
                                foreach (var n in Query1)
                                { name = n.d_name; }
                                //string msg = "Dear patient, your Hakkeem appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
                                string msg = "Your Appointment has been Cancelled with Doctor " + name + " on " + ss.a_date + " and " + ss.a_time + ", because the doctor engaged with emergency situation. Please choose a another doctor appointment through "+ "http://www.hakkeem.com"+ " Thank you Hakkeem Team";
                                var user = (from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item).First();
                                try
                                {
                                //Email.mail(obj.DecryptString(user.email), msg, "Hakkeem appointment canceled");
                                Email_To_AppoinmentCancilationpatient(obj.DecryptString(user.email), name, ss.a_date, ss.a_time);
                            }
                                catch (Exception ex) { }
                                string s = obj.DecryptString(user.contact);
                                try
                                {
                                    ob.Message(s, msg);
                                }
                                catch (Exception ex) { }
                                var Query2 = from item in db.tbl_doctor_appointments where item.id == int.Parse(ss.id.ToString()) select item;
                                foreach (var dd in Query2)
                                {
                                    //doctor engaged
                                    dd.a_status = 4;
                                    //db.tbl_doctor_appointments.DeleteOnSubmit(dd);
                                }
                                db.SubmitChanges();
                            }
                        }

                        //var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == lbl6.Text select item.id;
                        //foreach (var d in Query4)
                        //{
                        //    var Query5 = from item in db.tbl_doc_times where item.date_id == d && item.time == t select item;
                        //    foreach (var dd in Query5)
                        //    {
                        //        db.tbl_doc_times.DeleteOnSubmit(dd);
                        //    }
                        //    db.SubmitChanges();
                        //}
                    }


                    GridView1.EditIndex = -1;
                    Doctor_Availability();


                }
                //ShowMessage("Successfully changed the availability", MessageType.Success);
                //Label2.Text = "<b>Do you want to change the availability..?</b>";

                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully changed the availability')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير التوفر بنجاح')</Script>");
                //}
            //}
        }
    }
}