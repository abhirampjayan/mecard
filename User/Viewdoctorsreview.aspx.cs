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

public partial class User_View_doctors_review : System.Web.UI.Page
{
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    SMS sms = new SMS();
    SqlCommand cmd, com;
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
        con.Open();
        if (!IsPostBack)
        {
            if (Request.QueryString["docid"] != null)
            {
                Session["review"] = obj.DecryptString(Request.QueryString["docid"].ToString());
                Session["doc"] = Session["review"].ToString();

                Session["lat"] = Request.QueryString["Lat"].ToString();
                Session["long"] = Request.QueryString["Long"].ToString();

                Session["lt"] = Session["lat"].ToString();
                Session["lg"] = Session["long"].ToString();
                Lat.Value = Session["lt"].ToString();
                Long.Value = Session["lg"].ToString();
            }
            else
            {
                Session["review"] = Session["doc"].ToString();
                Session["lat"] = Session["lt"].ToString();
                Session["long"] = Session["lg"].ToString();
                Lat.Value = Session["lt"].ToString();
                Long.Value = Session["lg"].ToString();
            }
        }
        if (Session["review"] != null)
        {

        }
        else
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                Response.Redirect("~/User/Search.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/User/Search.aspx?l=ar-EG");
            //}
        }

        if (!IsPostBack)
        {
           
            doctor();
            review();
            Availability();
        }
    }
    protected void DataList3_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "doc")
        {
            //Session["doctor"] = e.CommandArgument.ToString();
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
            TxtApntmtDate.Text = Session["date"].ToString();
            TxtApointmentTime.Text = Session["time"].ToString();
            //TextBox1.Text = Session["date"].ToString();
            //TextBox2.Text = Session["time"].ToString();
            //Label9.Text = "<b>Do you want take an appointment on " + "<span style='color:red'>" + Session["date"] + "</span> at " + "<span style='color:red'>" + Session["time"] + "</span>...?</b>";
            //ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
            upModal.Update();
        }
    }
    public void doctor()
    {
        try
        {
            var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["review"].ToString() select item;
            foreach (var ss in Query)
            {
                if (ss.d_photo == "" || ss.d_photo == null)
                {
                    Image1.ImageUrl = "../Doctorimages/doctor.png";
                }
                else
                {

                    Image1.ImageUrl = ss.d_photo;
                }
                //  Image2.ImageUrl = ss.d_photo;
                lblname.Text = ss.d_name;
               // lblname1.Text = ss.d_name;
                lblql.Text = ss.d_education;
              //  lblql1.Text = ss.d_education;
                lblspec.Text = ss.d_specialties;
              //  lblspec1.Text = ss.d_specialties;
            }
            DetailsView1.DataSource = Query;
            DetailsView1.DataBind();


            Label lbl4 = DetailsView1.FindControl("Label43") as Label;
            List<string> langs = new List<string>();
            var QueryLang = from item in db.tbl_doc_languages where item.doc_id == Session["review"].ToString() select item;
            foreach (var l in QueryLang)
            {
                //langs.Add(l.d_Language);
                lbl4.Text += l.d_Language + " ";
            }



            Rating();
            var fee = from item in db.tbl_fees where item.d_hakkimid == Session["review"].ToString() select item;
            if (fee.Count() > 0)
            {
                foreach (var f in fee)
                {
                    string a = "<label style='font-weight:bolder;color:red'>" + f.rate + "</label>";
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        Label11.Text = "Consultation fee: " + a;
                    //}
                    //else
                    //{
                    //    Label11.Text = "رسوم الاستشارة: " + a;
                    //}
                }
            }
            else
            {

                Label11.ForeColor = System.Drawing.Color.IndianRed;
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    Label11.Text = "Consultation fee not available";
                //}
                //else
                //{
                //    Label11.Text = "رسوم التشاور غير متوفرة";
                //}
            }
        }
        catch(Exception ex)
        {

        }
    }


    public void review()
    {
        try
        {
            var Query = from item in db.tbl_user_feeds where item.d_email == Session["review"].ToString() && item.status == 0  orderby item.id descending select item;
            DataList1.DataSource = Query;
            DataList1.DataBind();


            foreach (DataListItem dl in DataList1.Items)
            {

                string email = (dl.FindControl("Label3") as Label).Text;
                string uid = "";
                Label lbl1 = new Label();
                lbl1 = dl.FindControl("Label1") as Label;
                var Query1 = from item in db.tbl_signups where item.u_hakkimid == email select item;
                foreach (var ss in Query1)
                {
                    lbl1.Text = "by " + ss.name;
                    uid = ss.u_hakkimid;
                }

                //....................Rating--------------------------------------------



                SqlCommand cmd6 = new SqlCommand("SELECT rate_wt FROM tbl_ratingview where d_id='" + Session["review"].ToString() + "'and u_id='" + uid + "'", con);
                int wt = 0;
                try
                {
                    wt = Convert.ToInt32(cmd6.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    wt = 0;
                }
                int b;
                b = 5 - wt;
                string ratefill = "";
                for (int i = 0; i < wt; i++)
                {
                    ratefill = ratefill + "<img  src='rate/Filled.gif'>";
                }
                for (int i = 0; i < b; i++)
                {
                    ratefill = ratefill + "<img  src='rate/Empty.gif'>";
                }

                Literal l = new Literal();
                l = dl.FindControl("Literal4") as Literal;
                l.Text = ratefill;

                //----------------------Rating-----------------------------------------

                //....................Rating--------------------------------------------



                SqlCommand cmd61 = new SqlCommand("SELECT rate_bm FROM tbl_ratingview where d_id='" + Session["review"].ToString() + "'and u_id='" + uid + "'", con);
                int bm = 0;
                try
                {
                    bm = Convert.ToInt32(cmd61.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    bm = 0;
                }
                string ratefill1 = "";

                for (int i = 0; i < bm; i++)
                {
                    ratefill1 = ratefill1 + "<img  src='rate/Filled.gif'>";
                }
                int b1;
                b1 = 5 - bm;
                for (int i = 0; i < b1; i++)
                {
                    ratefill1 = ratefill1 + "<img  src='rate/Empty.gif'>";
                }

                Literal l1 = new Literal();
                l1 = dl.FindControl("Literal5") as Literal;
                l1.Text = ratefill1;
                //----------------------Rating-----------------------------------------

                //....................Rating--------------------------------------------


                //----------------------Rating-----------------------------------------
                //....................Rating--------------------------------------------


                SqlCommand cmd63 = new SqlCommand("SELECT rate_service FROM tbl_ratingview where d_id='" + Session["review"].ToString() + "'and u_id='" + uid + "'", con);
                int ser = 0;
                try
                {
                    ser = Convert.ToInt32(cmd63.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    ser = 0;
                }
                string ratefill11 = "";
                for (int i = 0; i < ser; i++)
                {
                    ratefill11 = ratefill11 + "<img  src='rate/Filled.gif'>";
                }
                int b2;
                b2 = 5 - ser;
                for (int i = 0; i < b2; i++)
                {
                    ratefill11 = ratefill11 + "<img  src='rate/Empty.gif'>";
                }

                Literal l11 = new Literal();
                l11 = dl.FindControl("Literal6") as Literal;
                l11.Text = ratefill11;
                //----------------------Rating-----------------------------------------
            }
            Rating();
        }
        catch (Exception ex) { }
    }
    public void Availability()
    {
        try
        {
            var Query = (from item in db.doctor_availabilities where item.d_id == Session["review"].ToString() orderby item.a_date ascending select item).Take(2);


            DataList2.DataSource = Query;
            DataList2.DataBind();
            foreach (DataListItem dli in DataList2.Items)

            {
                var Query4 = from item in db.doctor_availabilities where item.d_id == Session["review"].ToString() select item;
                //foreach (var ss in Query4)
                //{
                //    DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
                //    DateTime dt2 = DateTime.Parse(ss.a_date);
                //    if (dt2 < dt1)
                //    {
                //        var Query5 = from item in db.doctor_availabilities where item.id == ss.id select item;
                //        foreach (var dd in Query5)
                //        {
                //            db.doctor_availabilities.DeleteOnSubmit(dd);
                //        }
                //        db.SubmitChanges();
                //    }
                //    else if (dt2 > dt1) { }
                //    else if (dt1 == dt2)
                //    {
                //        List<string> listTime = new List<string>();
                //        string time = DateTime.Now.AddHours(2).ToShortTimeString();
                //        var Query6 = from item in db.view_doc_available_times where item.a_date == ss.a_date select item;
                //        foreach (var tt in Query6)
                //        {

                //            DateTime dtt1 = DateTime.Parse(time);
                //            try
                //            {
                //                DateTime dtt2 = DateTime.Parse(tt.time);
                //                if (dtt2 >= dtt1)
                //                {
                //                    listTime.Add(tt.time);
                //                }
                //            }
                //            catch (Exception ex)
                //            {


                //                var qry = from item in db.doctor_availabilities where item.a_date == ss.a_date select item;
                //                foreach (var dd in qry)
                //                {
                //                    db.doctor_availabilities.DeleteOnSubmit(dd);
                //                }
                //                db.SubmitChanges();



                //            }

                //        }
                //        string ntime = "";
                //        foreach (string ttt in listTime)
                //        {
                //            if (ntime == "")
                //            {
                //                ntime = ttt;
                //            }
                //            else
                //            {
                //                ntime += "," + ttt;
                //            }
                //        }
                //        try
                //        {
                //            var Query7 = from item in db.tbl_doc_times where item.date_id == ss.id select item;
                //            foreach (var nn in Query7)
                //            {
                //                nn.time = ntime;
                //            }
                //            db.SubmitChanges();
                //        }
                //        catch (Exception ex)
                //        { }
                //    }
                //}



                Label lbl6 = dli.FindControl("Label6") as Label;
                DataList dl3 = dli.FindControl("DataList3") as DataList;
                var Query2 = from item in db.view_doc_available_times where item.a_date == lbl6.Text && item.d_hakkimid == Session["review"].ToString() select item;
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
                        dt.Rows.Add(ss.a_date, timet, ss.d_email);
                    }

                }
                dl3.DataSource = dt;
                dl3.DataBind();
                lbl6.Text = DateTime.Parse(lbl6.Text).ToString("yyy d MMM");
                foreach (DataListItem dl3i in dl3.Items)
                {
                    //Button btn2 = dl3i.FindControl("Button2") as Button;
                    LinkButton lnk2 = dl3i.FindControl("LinkButton2") as LinkButton;
                    string date = DateTime.Parse(lbl6.Text).ToString("yyyy-MM-dd");
                    var Query3 = from item in db.tbl_doctor_appointments
                                 where
                        item.d_id == Session["review"].ToString() && item.a_date == date && item.a_time == lnk2.Text
                                 select item;
                    if (Query3.Count() > 0)
                    {
                        foreach (var tt in Query3)
                        {
                            if (tt.a_status == 0)
                            { lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "Waiting for confirmation"; }
                            if (tt.a_status == 1 || tt.a_status == 4)
                            {
                                lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false;
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                    lnk2.ToolTip = "Booked";
                                //}
                                //else
                                //{
                                //    lnk2.ToolTip = "حجز";
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
        }
        catch (Exception ex) { }
    }
    protected void BtnTakeAppointment_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["hakkemid_u"] != null)
            {
                var Query11 = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                if (Query11.Count() > 0)
                {
                    String timet = "";
                    string[] t = new string[7];
                    string ab1 = "", ab2 = "";
                    try
                    {
                        t = TxtApointmentTime.Text.ToString().Split(' ');


                        ab1 = t[0].ToString();
                        ab2 = t[1].ToString();
                        timet = ab1.ToString() + ab2.ToString();
                    }
                    catch (Exception ex11)
                    {
                        int l = TxtApointmentTime.Text.Count();
                        ab1 = TxtApointmentTime.Text.Substring(0, l - 2);
                        ab2 = TxtApointmentTime.Text.Substring(l - 2, 2);
                        timet = ab1.ToString() + ab2.ToString();



                    }

                    var Query = from item in db.tbl_doctor_appointments where item.c_id == Session["hakkemid_u"].ToString() && item.a_date == Session["date"].ToString() && item.a_time==timet select item;
                    if (Query.Count() > 0)
                    {
                        Label24.Text = "Already you take an appointment this day, so you please choose another day";
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            Label24.Text = "Already you take an appointment this day, so you please choose another day";
                        //}
                        //else
                        //{
                        //    Label24.Text = "إذا كنت تأخذ موعد هذا اليوم، لذلك يرجى اختيار يوم آخر";
                        //}
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#myModal2').modal();", true);
                        upModal2.Update();
                        //RegisterStartupScript("", "<Script Language=JavaScript>swal('Already you take an appointment this day, so you please choose another day.')</Script>");
                        //Label2.Text = "Already you take an appointment this day, so you please choose another day.";
                        //this.ModalPopupExtender3.Show();

                    }
                    else
                    {
                        var Query1 = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                        foreach (var u in Query1)
                        {
                            string dateTime = DateTime.Now.ToString();

                            string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
                            string createdtime = Convert.ToDateTime(dateTime).ToString("hh:mm tt");
                            //String timet = "";
                            //string[] t = new string[7];
                            //string ab1 = "", ab2 = "";
                            try
                            {
                                t = TxtApointmentTime.Text.Split(' ');


                                ab1 = t[0].ToString();
                                ab2 = t[1].ToString();
                                timet = ab1.ToString() + ab2.ToString();
                            }
                            catch (Exception ex)
                            {
                                int l = TxtApointmentTime.Text.Count();
                                ab1 = TxtApointmentTime.Text.Substring(0, l - 2);
                                ab2 = TxtApointmentTime.Text.Substring(l - 2, 2);
                                timet = ab1.ToString() + ab2.ToString();



                            }


                            tbl_doctor_appointment da = new tbl_doctor_appointment()
                            {
                                d_id = Session["doctor"].ToString(),
                                a_date = TxtApntmtDate.Text,
                                c_id = Session["hakkemid_u"].ToString(),
                                a_status = 1,/*0,*/
                                a_payment = DdlPayments.SelectedItem.Text,
                                a_reason = TxtReasonToVisit.SelectedItem.Text,
                                // a_time = TxtApointmentTime.Text,
                                a_time = timet,
                                app_date = createddate.ToString(),
                                app_time = createdtime.ToString(),
                            };
                            db.tbl_doctor_appointments.InsertOnSubmit(da);
                            db.SubmitChanges();
                            Availability();
                            string doctorname = "";
                            var doctor1 = from item in db.tbl_doctors where item.d_hakkimid == Session["doctor"].ToString() select item;
                            foreach (var s in doctor1)
                            {
                                doctorname = s.d_name.ToString();
                            }
                            //string msg = "Dear patient, your appointment is fixed with "+doctorname+" on appointment date "+TxtApntmtDate.Text+" at "+timet+", Thank you" + "<br />" + " Hakkeem Team.";
                            //Your Appointment has been fixed with Doctor[Doctor name] on[date and time]
                            string msg = "Your Appointment has been fixed with Doctor " + doctorname + " on " + TxtApntmtDate.Text + " and " + timet + ", Thank you" + "" + " Hakkeem Team.";
                            DataTable dt = new DataTable();
                            SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_doctor_appointment where a_date='" + TxtApntmtDate.Text + "'", con);
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                Session["Day"] = dt.Rows[0]["dayname"].ToString();
                            }
                            try {
                                // Email.mail(obj.DecryptString(u.email), msg, "Hakkeem appointment");
                                Email_To_userappoinment(obj.DecryptString(u.email), doctorname, TxtApntmtDate.Text, timet,Session["Day"].ToString());
                            } catch (Exception ex) { }
                            try {
                                string sdf = "+966" + obj.DecryptString(u.contact);
                                sms.Message(sdf, msg);
                                string sdf1 = "+91" + obj.DecryptString(u.contact);
                                sms.Message(sdf1, msg);

                            } catch (Exception ex) { }
                            //string dmsg = "Dear doctor, you have a new appointment with " + u.name + " on " + TxtApntmtDate.Text + " at " + timet + ", Thank you! Hakkeem Team.";
                            //Your Appointment has been fixed with patient[patient name] on[date and time]
                            string dmsg = "Your Appointment has been fixed with patient " + u.name + " on " + TxtApntmtDate.Text + " and " + timet + ", Thank you! Hakkeem Team.";

                            var doctor = from item in db.tbl_doctors where item.d_hakkimid == Session["doctor"].ToString() select item;
                            string demail = "";
                            foreach (var dd in doctor)
                            {
                               
                                try {
                                    string sd = "+966" + obj.DecryptString(dd.d_contact);
                                    sms.Message(sd, dmsg);

                                    string sd1 = "+91" + obj.DecryptString(dd.d_contact);
                                    sms.Message(sd1, dmsg);
                                    //sms.Message("+966" + obj.DecryptString(dd.d_contact), dmsg);
                                } catch (Exception ex) { }
                                demail = dd.d_email;
                            }

                            try
                            {
                                //Email.mail(obj.DecryptString(dd.d_email), dmsg, "Hakkeem appointment");
                                Email_To_Doctorappoinment(obj.DecryptString(demail), u.name, TxtApntmtDate.Text, timet, Session["Day"].ToString());
                            }
                            catch (Exception ex) { }


                            //Label1.Text = "Appointment fixed";
                            //ModalPopupExtender2.Show();
                            //Email("nahasshahul007@gmail.com", msg);
                            //if (Session["Speciality"].ToString() == "Auto")
                            //{
                            //RegisterStartupScript("", "<Script Language=JavaScript>swal('Your appointment is fixed')</Script>");
                            Response.Redirect("~/User/Finish appointment.aspx");
                            //}
                            //else
                            //{
                            //    //RegisterStartupScript("", "<Script Language=JavaScript>swal('تم إصلاح موعدك')</Script>");
                            //    Response.Redirect("~/User/Finish appointment.aspx?l=ar-EG");
                            //}
                            //Response.Redirect("~/User/Finish appointment.aspx");
                        }
                    }
                }
                else
                {
                    Label24.Text = "Hakkeem id not exist";
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        Label24.Text = "Hakkeem id not exist";
                    //}
                    //else
                    //{
                    //    Label24.Text = "هكيم معرف غير موجود";
                    //}
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#myModal2').modal();", true);
                    upModal.Update();
                    //Label2.Text = "Hakkeem id not exist";
                    //ModalPopupExtender3.Show();
                }
            }
            else
            {
                Session["date"] = TxtApntmtDate.Text;

                Session["time"] = TxtApointmentTime.Text;
                Session["rv"] = TxtReasonToVisit.SelectedItem.Text;
                Session["payment"] = DdlPayments.SelectedItem.Text;
                //this.ModalPopupExtender1.Hide();
                //this.ModalPopupExtender4.Show();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();


            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void BtnError_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1.Hide();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            //string script = "$(document).ready(function () { $('[id*=Button3]').click(); });";
            //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            var Query = from item in db.tbl_signups
                        where (item.email == obj.EnryptString(login.Text) || item.contact == obj.EnryptString(login.Text) || item.u_hakkimid == login.Text) && item.password == obj.EnryptString(password.Text)
                        select item;
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    Session["user"] = ss.u_hakkimid;
                    Session["u_hakkeemid"] = Session["user"];
                    Session["usermail"] = obj.DecryptString(ss.email);
                    Session["usercontact"] = obj.DecryptString(ss.contact);
                    Session["username"] = ss.name;
                    //TextBox1.Text = Session["date"].ToString();
                    //TextBox2.Text = Session["time"].ToString();
                }

                //
                var docapmnt = from item in db.tbl_doctor_appointments where item.c_id == Session["user"].ToString() && item.a_date == Session["date"].ToString() && item.a_status == 1 && item.a_time== Session["time"].ToString() select item;
                if (docapmnt.Count() > 0)
                {
                    i = 1;
                }
                else
                {
                    var hosdocapmnt = from item in db.tbl_hos_doc_appmnts where item.u_id == Session["user"].ToString() && item.a_date == Session["date"].ToString() && item.a_status == 1 && item.a_time == Session["time"].ToString() select item;
                    if (hosdocapmnt.Count() > 0)
                    {
                        i = 1;
                    }
                }
                if (i == 0)
                {
                    string dateTime = DateTime.Now.ToString();

                    string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
                    string createdtime = Convert.ToDateTime(dateTime).ToString("hh:mm tt");

                    tbl_doctor_appointment da = new tbl_doctor_appointment()
                    {
                        d_id = Session["review"].ToString(),
                        a_date = Session["date"].ToString(),
                        c_id = Session["user"].ToString(),
                        a_status = 1,/*0,*/
                        a_payment = Session["payment"].ToString(),
                        a_reason = Session["rv"].ToString(),
                        a_time = Session["time"].ToString(),
                        app_date = createddate.ToString(),
                        app_time = createdtime.ToString(),
                    };
                    db.tbl_doctor_appointments.InsertOnSubmit(da);
                    db.SubmitChanges();
                    Availability();
                    string doctorname = "";
                    var doctor1 = from item in db.tbl_doctors where item.d_hakkimid == Session["review"].ToString() select item;
                    foreach (var s in doctor1)
                    {
                        doctorname = s.d_name.ToString();
                    }
                    //Your Appointment has been fixed with Doctor[Doctor name] on[date and time]
                    //string msg = "Dear patient, your appointment is fixed with Dr." + doctorname + " on appointment date " + Session["date"].ToString() + " at " + Session["time"].ToString() + ", Thank you" + "<br />" + " Hakkeem Team.";
                    string msg = "Your Appointment has been fixed with Doctor " + doctorname + " on " + Session["date"].ToString() + " and " + Session["time"].ToString() + ", Thank you" + "" + " Hakkeem Team.";
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_doctor_appointment where a_date='" + Session["date"].ToString() + "'", con);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Session["Day"] = dt.Rows[0]["dayname"].ToString();
                    }
                    try
                    {
                        // Email.mail(Session["usermail"].ToString(), msg, "Appointment fixed");
                        Email_To_userappoinment(Session["usermail"].ToString(), doctorname, Session["date"].ToString(), Session["time"].ToString(),Session["Day"].ToString());
                    }
                    catch (Exception ex) { }
                    try {
                        string ghdf = "+966" + Session["usercontact"].ToString();
                        sms.Message(ghdf, msg);
                        string ghdf1 = "+91" + Session["usercontact"].ToString();
                        sms.Message(ghdf1, msg);
                    } catch (Exception ex) { }
                    //this.ModalPopupExtender1.Hide();
                    //this.ModalPopupExtender2.Show();
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('Your appointment is fixed')</Script>");
                    //string dmsg = "Dear doctor, you have a new appointment with " + Session["username"] + " on " + Session["date"].ToString() + " at " + Session["time"].ToString() + ", Thank you! Hakkeem Team.";
                    //Your Appointment has been fixed with patient[patient name] on[date and time]
                    string dmsg = "Your Appointment has been fixed with patient " + Session["username"] + " on " + Session["date"].ToString() + " and " + Session["time"].ToString() + ", Thank you! Hakkeem Team.";
                    var doctor = from item in db.tbl_doctors where item.d_hakkimid == Session["review"].ToString() select item;
                    foreach (var dd in doctor)
                    {
                        try {
                            //Email.mail(obj.DecryptString(dd.d_email), dmsg, "Hakkeem appointment");
                            Email_To_Doctorappoinment(obj.DecryptString(dd.d_email),Session["username"].ToString(), Session["date"].ToString(), Session["time"].ToString(),Session["Day"].ToString());
                        } catch (Exception ex) { }
                        try {
                            string sd = "+966" + obj.DecryptString(dd.d_contact);
                            sms.Message(sd, dmsg);
                            string sd1 = "+91" + obj.DecryptString(dd.d_contact);
                            sms.Message(sd1, dmsg);
                        } catch (Exception ex) { }
                    }


                    //Timer mtimer = Master.FindControl("Timer1") as Timer;
                    //mtimer.Enabled = true;
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/User/Finish appointment.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/User/Finish appointment.aspx?l=ar-EG");
                    //}
                }
                else
                {
                    Label5.Visible = true;
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        Label5.Text = "Already you have a appointment this date, please choose another date";
                    //}
                    //else
                    //{
                    //    Label5.Text = "إذا كان لديك موعد هذا التاريخ، يرجى اختيار تاريخ آخر";
                    //}
                }
                //

            }
            else
            {
                Label5.Visible = true;
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    Label5.Text = "Wrong password or login id";
                //}
                //else
                //{
                //    Label5.Text = "كلمة مرور خاطئة أو معرف تسجيل دخول";
                //}
                //this.ModalPopupExtender4.Show();
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('Wrong password or user name')</Script>");
            }
        }
        catch (Exception ex) { }

    }
    public void Rating()
    {
        try
        {
            double Total = 0;
            SqlCommand cmddd = new SqlCommand("SELECT rate_wt,rate_bm,rate_service FROM tbl_ratingview where d_id='" + Session["review"].ToString() + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmddd);
            DataTable dtt = new DataTable();
            da.Fill(dtt);
            double Average = 0;
            string avgg = "";
            string ratefill, rateempty, ratehalf;
            if (dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    Total += Convert.ToInt32(dtt.Rows[i][0].ToString()) + Convert.ToInt32(dtt.Rows[i][1].ToString()) + Convert.ToInt32(dtt.Rows[i][2].ToString());

                    Average = Total / 3;
                }

                Average = Average / (dtt.Rows.Count);
                double av = Math.Floor(Average);
                ratefill = "";
                rateempty = "";
                ratehalf = "";

                string str = null;

                str = Average.ToString();
                // avgg= Math.Round(decimal.Parse(Average.ToString()), 2).ToString();
                double balance = 5 - av;
                if (str.Contains(".") == true)
                {
                    ratehalf = "<img src='rate/half.gif'>";

                    balance = balance - 1;
                }
                else
                {
                    ratehalf = "";

                }


                for (int i = 0; i < av; i++)
                {
                    ratefill = ratefill + "<img  src='rate/Filled.gif'>";
                }
                for (int i = 0; i < balance; i++)
                {
                    rateempty = rateempty + "<img  src='rate/empty.gif'>";
                }

            }
            else
            {
                ratefill = "";
                rateempty = "";
                ratehalf = "";
            }
            Literal1.Text = ratefill.ToString();
            Literal2.Text = ratehalf.ToString();
            Literal3.Text = rateempty.ToString();
           // Literal7.Text = ratefill.ToString();
           // Literal8.Text = ratehalf.ToString();
           // Literal9.Text = rateempty.ToString();
            ////....................Rating--------------------------------------------
            //int total = 0;
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ConnectionString);

            //SqlCommand cmd6 = new SqlCommand("SELECT rate_service,rate_bm,rate_wt FROM tbl_rating where d_id='" + Session["review"].ToString() + "'", con);
            //SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
            //DataTable dt6 = new DataTable();
            //da6.Fill(dt6);
            //if (dt6.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt6.Rows.Count; i++)
            //    {
            //        total += Convert.ToInt32(dt6.Rows[i][0].ToString());
            //    }
            //    int Average = total / (dt6.Rows.Count);
            //    Rating1.CurrentRating = Average;

            //}
            ////----------------------Rating-----------------------------------------
        }
        catch (Exception ex) { }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                Response.Redirect("doctoravailability.aspx?docid=" + obj.EnryptString(Session["review"].ToString()) + "&Lat=" + Session["lat"].ToString() + "&Long=" + Session["long"].ToString() + "");
            //}
            //else
            //{
            //    Response.Redirect("doctoravailability.aspx?l=ar-EG&docid=" + obj.EnryptString(Session["review"].ToString()) + "&Lat=" + Session["lat"].ToString() + "&Long=" + Session["long"].ToString() + "");

            //}
        }
        catch (Exception ex) { }
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
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + timepath + "'></td><td style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + time + " "+ day+"</td></tr></tbody></table></td></tr>";
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

            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td  style='color:#4aa9af;font-weight:bold;'><img src='" + sthetho + "' ></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + username + "</td></tr></tbody></table></td></tr>";
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