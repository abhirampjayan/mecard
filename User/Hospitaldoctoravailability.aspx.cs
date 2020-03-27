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

public partial class User_Hospital_doctor_availability : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    databaseDataContext db = new databaseDataContext();
    MailMessage Email = new MailMessage();
    secure obj = new secure();

    protected override void InitializeCulture()
    {
        //Session["Speciality"] = "Auto";
        //string culture = "Auto";
        //try
        //{
        //    culture = Request.QueryString["l"].ToString();
        //    Session["Speciality"] = culture;
        //}
        //catch (Exception ex)
        //{ }
        //// string culture = Session["Speciality"].ToString();
        //if (string.IsNullOrEmpty(culture))
        //    culture = "Auto";
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
        if (!IsPostBack)
        {

            if (Request.QueryString["docid"] != null)
            {
                Session["hosdemail"] = obj.DecryptString(Request.QueryString["docid"].ToString());
                Session["doctor"] = obj.DecryptString(Request.QueryString["docid"].ToString());
                try
                {
                    Session["hid"] = obj.DecryptString(Request.QueryString["hospitalregno"].ToString());
                }
                catch (Exception ex)
                { Session["hid"] = Session["hname"]; }

                Session["lat"] = Request.QueryString["Lat"].ToString();
                Session["long"] = Request.QueryString["Long"].ToString();

                Session["lt"] = Session["lat"].ToString();
                Session["lg"] = Session["long"].ToString();
                Lat.Value = Session["lt"].ToString();
                Long.Value = Session["lg"].ToString();
            }
            else
            {
                Session["lat"] = Session["lt"].ToString();
                Session["long"] = Session["lg"].ToString();
                Lat.Value = Session["lt"].ToString();
                Long.Value = Session["lg"].ToString();
            }
            doctor();
            Availability();
            if (Request.QueryString["doctime"] != null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal3", "$('#myModal3').modal();", true);
                upModal3.Update();
            }
        }
    }

    public void doctor()
    {
        try
        {
            var Query = from item in db.tbl_hdoctors where item.hd_email == Session["hosdemail"].ToString() && item.h_id == Session["hid"].ToString() select item;
            foreach (var ss in Query)
            {
                lblname.Text = ss.hd_name;
                lblql.Text = ss.hd_education;
                lblspec.Text = ss.hd_specialties;
                Image1.ImageUrl = ss.hd_photo;
                var Query1 = from item in db.tbl_hospitalregs where item.h_hakkimid == ss.h_id select item;
                foreach (var s in Query1)
                {
                    hname.Text = s.h_name;
                }
            }

        }
        catch (Exception ex)
        {

        }
    }


    public void Availability()
    {
        try
        {
            if (Request.QueryString["docid"] != null)
            {
                Session["doctor"] = obj.DecryptString(Request.QueryString["docid"].ToString());
            }
            if (Request.QueryString["doctime"] != null)
            {

                Session["time"] = obj.DecryptString(Request.QueryString["doctime"].ToString()) + " " + obj.DecryptString(Request.QueryString["timeperiod"].ToString());

                Session["date"] = obj.DecryptString(Request.QueryString["docdate"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from doctor_availability where a_date='" + obj.DecryptString(Request.QueryString["docdate"].ToString()) + "'", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Day"] = dt.Rows[0]["dayname"].ToString();
                }
            }
            if (Session["date"] != null)
            {
                TextBox1.Text = Session["date"].ToString();
            }
            if (Session["time"] != null)
            {
                Time.Text = Session["time"].ToString();
            }


            var Query = from item in db.tbl_hos_doc_availables where item.hd_id == Session["doctor"].ToString() orderby item.date ascending select item;


            DataList2.DataSource = Query;
            DataList2.DataBind();
            foreach (DataListItem dli in DataList2.Items)

            {
                var Query4 = from item in db.tbl_hos_doc_availables where item.hd_id == Session["doctor"].ToString() select item;




                Label lbl6 = dli.FindControl("Label6") as Label;
                DataList dl3 = dli.FindControl("DataList3") as DataList;
                var Query2 = from item in db.view_hos_doc_available_times where item.date == lbl6.Text && item.hd_email == Session["doctor"].ToString() select item;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("date"), new DataColumn("time"), new DataColumn("email") });
                foreach (var ss in Query2)
                {
                    if (ss.time == "")
                    { }
                    else
                    {
                        int l = ss.time.ToString().Count();
                        string ab1 = ss.time.ToString().Substring(0, l - 2);
                        string ab2 = ss.time.ToString().Substring(l - 2, 2);
                        string timet = ab1.ToString() + " " + ab2.ToString();


                        dt.Rows.Add(ss.date, timet, ss.hd_email);
                    }

                }
                dl3.DataSource = dt;
                dl3.DataBind();
                lbl6.Text = DateTime.Parse(lbl6.Text).ToString("ddd d MMM");
                foreach (DataListItem dl3i in dl3.Items)
                {
                    //Button btn2 = dl3i.FindControl("Button2") as Button;
                    LinkButton lnk2 = dl3i.FindControl("LinkButton2") as LinkButton;
                    string date = DateTime.Parse(lbl6.Text).ToString("yyyy-MM-dd");
                    var Query3 = from item in db.tbl_hos_doc_appmnts
                                 where
                        item.d_id == Session["doctor"].ToString() && item.a_date == date && item.a_time == lnk2.Text
                                 select item;
                    if (Query3.Count() > 0)
                    {
                        foreach (var tt in Query3)
                        {
                            if (tt.a_status == 0)
                            {
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                    lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "Waiting for confirmation";
                                //}
                                //else
                                //{
                                //    lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "في انتظار التأكيد";

                                //}
                            }
                            if (tt.a_status == 1)
                            {
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                    lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false; lnk2.ToolTip = "Booked";
                                //}
                                //else
                                //{
                                //    lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false; lnk2.ToolTip = "حجز";
                                //}
                            }
                        }

                    }
                    else
                    {
                        //lnk2.BackColor = System.Drawing.Color.Green;
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            lnk2.ToolTip = "Available";
                        //}
                        //else
                        //{
                        //    lnk2.ToolTip = "متاح";
                        //}
                    }
                }
            }

            var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["doctor"].ToString() select item;
            foreach (var ss in Query1)
            {
                lblname.Text = ss.hd_name;
                lblql.Text = ss.hd_education;
                lblspec.Text = ss.hd_specialties;
                if (ss.hd_photo == "" || ss.hd_photo == null)
                {
                    Image1.ImageUrl = "../Doctorimages/doctor.png";
                }
                else
                {

                    Image1.ImageUrl = ss.hd_photo;
                }
               
            }

        }
        catch (Exception ex) { }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            var Query = from item in db.tbl_signups
                        where (item.email == obj.EnryptString(login.Text) || item.contact == obj.EnryptString(login.Text) || item.u_hakkimid == login.Text) && item.password == obj.EnryptString(password.Text)
                        select item;
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    Session["hakkemid_u"] = ss.u_hakkimid;
                    TextBox1.Text = Session["date"].ToString();
                    Time.Text = Session["time"].ToString();
                }



                //if (LinkButton1.Enabled == false)
                //{
                tbl_hos_doc_appmnt ha = new tbl_hos_doc_appmnt()
                {
                    d_id = Session["hosdemail"].ToString(),
                    a_date = TextBox1.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    a_status = 0,
                    a_payment = DropDownList2.SelectedItem.Text,
                    a_reason = DropDownList1.SelectedItem.Text,
                    h_id = Session["hid"].ToString(),
                    a_time = Time.Text,
                };
                db.tbl_hos_doc_appmnts.InsertOnSubmit(ha);
                db.SubmitChanges();
                //Availability();
                string hdname = "";
                var doctor1 = from item in db.tbl_hdoctors where item.hd_email == Session["hosdemail"].ToString() && item.h_id == Session["hid"].ToString() select item;
                foreach (var s in doctor1)
                {
                    hdname = s.hd_name.ToString();
                }
                //Your Appointment has been fixed with Doctor[Doctor name] on[date and time]
                //string msg = "Dear patient, your appointment is fixed with Dr." + hdname + " on " + TextBox1.Text + " at " + Time.Text + ". Thank you. Hakkeem Team.";

                string emailuser = "";

                var Query1 = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                foreach (var ss in Query1)
                {
                    emailuser =obj.DecryptString(ss.email);
                }
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + TextBox1.Text + "'", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Day"] = dt.Rows[0]["dayname"].ToString();
                }
                string msg = "Your Appointment has been fixed with Doctor " + hdname + " on " + TextBox1.Text + " and " + Time.Text + ". Thank you. Hakkeem Team.";

                //Email.mail(emailuser.ToString(), msg, "Hakkeem appointment");
                Email_To_userappoinment(emailuser.ToString(), hdname, TextBox1.Text, Time.Text,Session["Day"].ToString());

                try
                {
                    string patient = "";
                    SMS ob = new SMS();
                    var user = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                    foreach (var ss in user)
                    {
                        string s = "+966" + obj.DecryptString(ss.contact);
                        // ob.Message(s.Substring(1, s.Length), msg);
                        ob.Message(s, msg);

                        string s1 = "+91" + obj.DecryptString(ss.contact);
                        // ob.Message(s.Substring(1, s.Length), msg);
                        ob.Message(s1, msg);
                        patient = ss.name;
                    }

                    var doctor = from item in db.tbl_hdoctors where item.hd_email == Session["hosdemail"].ToString() && item.h_id == Session["hid"].ToString() select item;
                    foreach (var ss in doctor)
                    {
                        //Your Appointment has been fixed with patient[patient name] on[date and time]
                        //string dmsg = "Dear doctor, you have a new appointment, patient name: " + patient + " date: " + TextBox1.Text + " time: " + Time.Text + ". Hakkeem Team.";
                        string dmsg = "Your Appointment has been fixed with patient " + patient + " on " + TextBox1.Text + " and " + Time.Text + ". Hakkeem Team.";

                        string s = "+966" + ss.hd_contact;
                        ob.Message(s, dmsg);


                        string s1 = "+91" + ss.hd_contact;
                        ob.Message(s1, dmsg);
                        //Email.mail(ss.hd_email, dmsg, "Hakkeem appointment");
                        Email_To_Doctorappoinment(ss.hd_email, patient, TextBox1.Text, Time.Text,Session["Day"].ToString());
                    }
                }
                catch (Exception ex)
                {

                }
                //}
                //Timer mtimer = Master.FindControl("Timer1") as Timer;
                //mtimer.Enabled = true;
                Session["timeS"] = Session["time"].ToString();
                Session["date"] = Session["date"].ToString();
                //if (Session["Speciality"].ToString() == "Auto")
                //{

                    Response.Redirect("Finish appointment.aspx");
                //}
                //else
                //{
                //    Response.Redirect("Finish appointment.aspx?l=ar-EG");
                //}



            }
            else
            {
                Label1.Visible = true;
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    Label1.Text = "Wrong password or login id";
                //}
                //else
                //{
                //    Label1.Text = "كلمة مرور خاطئة أو معرف تسجيل دخول";
                //}

                //this.ModalPopupExtender1.Show();
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('Wrong password or user name')</Script>");
            }
            //Availability();
        }
        catch (Exception ex)
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{

                Response.Redirect("Finish appointment.aspx");
            //}
            //else
            //{
            //    Response.Redirect("Finish appointment.aspx?l=ar-EG");
            //}

        }
    }

    protected void DataList3_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "doc")
            {
                Session["hosdemail"] = e.CommandArgument.ToString();
                Label lbl7 = e.Item.FindControl("Label7") as Label;
                Label lbl8 = e.Item.FindControl("Label8") as Label;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from doctor_availability where a_date='" + lbl7.Text + "'", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Day"] = dt.Rows[0]["dayname"].ToString();
                }
                Session["date"] = lbl7.Text;
                Session["time"] = lbl8.Text;
                TextBox1.Text = Session["date"].ToString();
                Time.Text = Session["time"].ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal3", "$('#myModal3').modal();", true);
                upModal3.Update();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            if (Session["hakkemid_u"] != null)
            {
                string emailuser = "";

                var Query1 = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                foreach (var ss in Query1)
                {
                    emailuser =obj.DecryptString(ss.email);
                }

                    var Query = from item in db.tbl_hos_doc_appmnts where item.a_date == Session["date"].ToString() && item.u_id == Session["hakkemid_u"].ToString() && (item.a_status == 1 || item.a_status == 0)  && item.a_time==Time.Text select item;
                if (Query.Count() > 0)
                {
                    i = 1;
                }
                else
                {
                    var doctor = from item in db.tbl_doctor_appointments where item.c_id == Session["hakkemid_u"].ToString() && item.a_date == Session["date"].ToString() && item.a_status == 1 && item.a_time == Time.Text select item;
                    if (doctor.Count() > 0)
                    {
                        i = 1;
                    }
                }
                if (i == 1)
                {
                    Label24.Text = "Already you take an appointment this day, so you please choose another day.";
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        Label24.Text = "Already you take an appointment this day, so you please choose another day.";
                        //RegisterStartupScript("", "<Script Language=JavaScript>swal('Already you take an appointment this day, so you please choose another day.')</Script>");
                    //}
                    //else
                    //{
                    //    //RegisterStartupScript("", "<Script Language=JavaScript>swal('إذا كنت تأخذ موعد هذا اليوم، لذلك يرجى اختيار يوم آخر.')</Script>");
                    //    Label24.Text = "إذا كنت تأخذ موعد هذا اليوم، لذلك يرجى اختيار يوم آخر";
                    //}
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#myModal2').modal();", true);
                    upModal2.Update();

                    //Label2.Text = "Already you take an appointment this day, so you please choose another day.";
                    //this.ModalPopupExtender3.Show();
                }
                else
                {
                    tbl_hos_doc_appmnt ha = new tbl_hos_doc_appmnt()
                    {
                        d_id = Session["hosdemail"].ToString(),
                        a_date = TextBox1.Text,
                        u_id = Session["hakkemid_u"].ToString(),
                        a_status = 0,
                        a_payment = DropDownList2.SelectedItem.Text,
                        a_reason = DropDownList1.SelectedItem.Text,
                        h_id = Session["hid"].ToString(),
                        a_time = Time.Text,
                    };
                    db.tbl_hos_doc_appmnts.InsertOnSubmit(ha);
                    db.SubmitChanges();
                    string doctorname = "";
                    var doctor1 = from item in db.tbl_hdoctors where item.hd_email == Session["hosdemail"].ToString() && item.h_id == Session["hid"].ToString() select item;
                    foreach (var d in doctor1)
                    {
                        doctorname = d.hd_name;
                    }
                    //Availability();
                    //Your Appointment has been fixed with Doctor[Doctor name] on[date and time]
                    //string msg = "Dear patient, your appointment is fixed. Thank you. Hakkeem Team.";
                    string msg = "Your Appointment has been fixed with Doctor " + doctorname + " on " + TextBox1.Text + " and " + Time.Text + " Thank you. Hakkeem Team.";
                    //Email.mail(emailuser.ToString(), msg, "Hakkeem appointment");
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + TextBox1.Text + "'", con);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Session["Day"] = dt.Rows[0]["dayname"].ToString();
                    }
                    Email_To_userappoinment(emailuser.ToString(), doctorname, TextBox1.Text, Time.Text,Session["Day"].ToString());
                    try
                    {
                        string patient = "";
                        SMS ob = new SMS();
                        var user = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                        foreach (var ss in user)
                        {
                            string s = "+966" + obj.DecryptString(ss.contact);
                            //  ob.Message(s.Substring(1, s.Length), msg);
                            ob.Message(s, msg);

                            string s1 = "+91" + obj.DecryptString(ss.contact);
                            //  ob.Message(s.Substring(1, s.Length), msg);
                            ob.Message(s1, msg);
                            patient = ss.name;
                        }

                        var doctor = from item in db.tbl_hdoctors where item.hd_email == Session["hosdemail"].ToString() && item.h_id == Session["hid"].ToString() select item;
                        foreach (var ss in doctor)
                        {
                            //Your Appointment has been fixed with patient[patient name] on[date and time]
                            //string dmsg = "Dear doctor, you have a new appointment, patient name: " + patient + " date: " + TextBox1.Text + " time: " + Time.Text + ". Hakkeem Team.";
                            string dmsg = "Your Appointment has been fixed with patient " + patient + " on " + TextBox1.Text + " and " + Time.Text + ". Hakkeem Team.";

                            string s = "+966" + ss.hd_contact;
                            //   ob.Message(s.Substring(1, s.Length), dmsg);
                            ob.Message(s, dmsg);

                            string s1 = "+91" + ss.hd_contact;
                            //   ob.Message(s.Substring(1, s.Length), dmsg);
                            ob.Message(s1, dmsg);
                            //Email.mail(ss.hd_email, dmsg, "Hakkeem appointment");
                            Email_To_Doctorappoinment(ss.hd_email, patient, TextBox1.Text, Time.Text,Session["Day"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    Session["timeS"] = Session["time"].ToString();
                    Session["date"] = Session["date"].ToString();
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        Response.Redirect("Finish appointment.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("Finish appointment.aspx?l=ar-EG");
                    //}

                }
            }
            else
            {
                //Timer mtimer = Master.FindControl("Timer1") as Timer;
                //mtimer.Enabled = false;
                //LinkButton1.Enabled = false;
                //this.ModalPopupExtender1.Show();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
        }
        catch (Exception ex)
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                Response.Redirect("Finish appointment.aspx");
            //}
            //else
            //{
            //    Response.Redirect("Finish appointment.aspx?l=ar-EG");
            //}
        }
    }
    public bool Email_To_userappoinment(string email, string doctorname, string date, string time,string day)
    {
        try
        {
            String messagestr = "";
            bool flag = true;
            string bg = "http://www.hakkeem.com/head1.png";
            string follw = "http://www.hakkeem.com/followus.png";
            string face = "http://www.hakkeem.com/facebook.png";
            string twitter = "http://www.hakkeem.com/twitter.png";
            string insta = "http://www.hakkeem.com/instagram.png";
            string sthetho = "http://www.hakkeem.com/stethoscope1.png";
            string timepath = "http://www.hakkeem.com/time1.png";
            string calender = "http://www.hakkeem.com/calendar1.png";
            string contact = "http://hakkeem.com/ContactUs.aspx";
            string privacy = "http://hakkeem.com/privacy%20policy.aspx";

            messagestr = messagestr + "<body  style='text-align:center;width=100%'>";

            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto; background:#f2f2f2;padding:60px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + "<tr>";
            messagestr = messagestr + " <td>";
            messagestr = messagestr + " <table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + " <tr>";
            messagestr = messagestr + "<td  colspan='2'  style='padding:20px 20px;background-color:#fff;line-height:3.5em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%'></td>";

            messagestr = messagestr + "</tr>";
            messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:20px;text-align:center;font-weight:bold;'>";
            messagestr = messagestr + "Hakkeem Appointment</td></tr>";
            messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:11px;line-height:1.5em;text-align:center;padding-top:10px'>";
            messagestr = messagestr + "'We believe that the greatest gift you can give your family and the world is a healthy you' </td></tr>";

            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td  style='color:#4aa9af;font-weight:bold;'><img src='" + sthetho + "' ></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>Dr." + doctorname + "</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + timepath + "'></td><td style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + time + " "+day+"</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + calender + "'></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + date + "</td></tr></tbody></table></td></tr>";


            messagestr = messagestr + "<tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
            messagestr = messagestr + " <tbody><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<span style='color:#4aa9af'><a href='"+privacy+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='"+contact+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
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
            mail.Subject = "Hakkeem Appointment";
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
    public bool Email_To_Doctorappoinment(string email, string username, string date, string time,string day)
    {
        try
        {
            String messagestr = "";
            bool flag = true;
            string bg = "http://www.hakkeem.com/head1.png";
            string follw = "http://www.hakkeem.com/followus.png";
            string face = "http://www.hakkeem.com/facebook.png";
            string twitter = "http://www.hakkeem.com/twitter.png";
            string insta = "http://www.hakkeem.com/instagram.png";
            string sthetho = "http://www.hakkeem.com/stethoscope1.png";
            string timepath = "http://www.hakkeem.com/time1.png";
            string calender = "http://www.hakkeem.com/calendar1.png";
            string contact = "http://hakkeem.com/ContactUs.aspx";
            string privacy = "http://hakkeem.com/privacy%20policy.aspx";
            messagestr = messagestr + "<body  style='text-align:center;width=100%'>";

            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto; background:#f2f2f2;padding:60px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + "<tr>";
            messagestr = messagestr + " <td>";
            messagestr = messagestr + " <table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + " <tr>";
            messagestr = messagestr + "<td  colspan='2'  style='padding:20px 20px;background-color:#fff;line-height:3.5em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%'></td>";

            messagestr = messagestr + "</tr>";
            messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:20px;text-align:center;font-weight:bold;'>";
            messagestr = messagestr + "Hakkeem Appointment</td></tr>";
            messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:11px;line-height:1.5em;text-align:center;padding-top:10px'>";
            messagestr = messagestr + "'We believe that the greatest gift you can give your family and the world is a healthy you' </td></tr>";

            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td  style='color:#4aa9af;font-weight:bold;'><img src='" + sthetho + "' ></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'" + username + "</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + timepath + "'></td><td style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + time + " "+day+"</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + calender + "'></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + date + "</td></tr></tbody></table></td></tr>";


            messagestr = messagestr + "<tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
            messagestr = messagestr + " <tbody><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<span style='color:#4aa9af'><a href='"+privacy+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='"+contact+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
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
            mail.Subject = "Hakkeem Appointment";
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