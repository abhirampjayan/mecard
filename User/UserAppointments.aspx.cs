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

public partial class User_UserAppointments : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    MailMessage Email = new MailMessage();
    SMS ob = new SMS();
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
        //if (Session["Speciality"].ToString() == "Auto")
        //{

        //}
        //else
        //{
        //    Button3.Text = "تؤكد";
        //    Button4.Text = "تؤكد";
        //}


        if (Session["hakkemid_u"] == null)
        {
            Response.Redirect("~/Index/SignInSignUp.aspx");
        }
        if (!IsPostBack)
        {
            BindGrvAppointements();
        }
    }

    // Method for Binding Appoitments to the gridview

    public void BindGrvAppointements()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_reason"), new DataColumn("id"), new DataColumn("a_status") });

        try
        {
            // Get appointment Details from Hospital......
            var hosApointment = from item in db.tbl_hos_doc_appmnts
                                join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_hakkimid
                                where item.u_id == Session["hakkemid_u"].ToString()
                                orderby item.a_date, item.a_time descending
                                select new { item1.h_name, item.h_id, item.id, item.a_date, item.a_payment, item.a_reason, item.a_status, item.a_time, item.d_id, item.u_id };

            if (hosApointment.Count() > 0)
            {
                foreach (var ss in hosApointment)
                {
                    var query = (from item in db.tbl_hdoctors where item.hd_email == ss.d_id select item).First();

                    dt.Rows.Add(ss.a_date, ss.h_id, ss.d_id, ss.a_time, ss.h_name, query.hd_name, ss.a_reason, ss.id, ss.a_status);
                }
            }


            // get appointment details from BookDoc doctor..
            var docAppointments = from item in db.tbl_doctor_appointments
                                  join item1 in db.tbl_doctors on item.d_id equals item1.d_hakkimid
                                  where item.c_id == Session["hakkemid_u"].ToString()
                                  orderby item.a_date, item.a_time descending
                                  select new { item1.d_name, item.id, item.a_date, item.a_payment, item.a_reason, item.a_status, item.a_time, item.d_id, item.c_id };

            if (docAppointments.Count() > 0)
            {
                foreach (var ss in docAppointments)
                {
                    dt.Rows.Add(ss.a_date, "", ss.d_id, ss.a_time, "----", ss.d_name, ss.a_reason, ss.id, ss.a_status);
                }
            }

            // Bind the gridview GrvAppoitnments with the data table
            DataView view = dt.DefaultView;
            view.Sort = "a_date DESC";
            DataTable sortedData = view.ToTable();
            GrvAppointments.DataSource = sortedData;
            GrvAppointments.DataBind();

            // Check whether datatable has rows

            if (dt.Rows.Count > 0)
            {

                // Loop through the gridview for checking the status of the appointments

                foreach (GridViewRow gRow in GrvAppointments.Rows)
                {
                    //Label LblStatus = gRow.FindControl("LblStatus") as Label;
                    Label LblSta = gRow.FindControl("LblSta") as Label;
                    LinkButton LnkConfirm = gRow.FindControl("LnkConfirm") as LinkButton;
                    Label lblhosid = gRow.FindControl("LblHosId1") as Label;
                    LinkButton LnkCancel = gRow.FindControl("LnkCancel") as LinkButton;
                    LinkButton LnkShare = gRow.FindControl("LnkShare") as LinkButton;
                    LinkButton lnk7 = gRow.FindControl("LinkButton7") as LinkButton;
                    if (LblSta.Text == "0")
                    {
                        LnkConfirm.Text = "Confirm";
                        LnkConfirm.ForeColor = System.Drawing.Color.Green;
                        LnkConfirm.Enabled = true;
                        //LblStatus.Text = "Waiting";
                    }
                    else if (LblSta.Text == "1")
                    {
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkConfirm.Text = "Confirmed";
                        //}
                        //else
                        //{
                        //    LnkConfirm.Text = "تم تأكيد";
                        //}
                        LnkConfirm.ForeColor = System.Drawing.Color.Red;
                        LnkConfirm.Enabled = false;
                        LnkConfirm.CssClass = "btn btn-xs btn-default disabled";
                        //LblStatus.Text = "Fixed";
                    }
                    else if (LblSta.Text == "2")
                    {
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkConfirm.Text = "Re-Book";
                        //}
                        //else
                        //{
                        //    LnkConfirm.Text = "كتاب مرة أخرى";
                        //}
                        LnkConfirm.ForeColor = System.Drawing.Color.Red;
                        LnkConfirm.Enabled = true;
                        LnkCancel.Visible = false;
                        LnkShare.Enabled = false;
                        LnkShare.CssClass = "disabled";
                        LnkShare.ForeColor = System.Drawing.Color.LightGray;
                        LnkConfirm.CssClass = "btn btn-xs btn-default";
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkShare.ToolTip = "disabled";
                        //}
                        //else
                        //{
                        //    LnkShare.ToolTip = "معاق";
                        //}
                    }
                    else if (LblSta.Text == "4")
                    {
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkConfirm.Text = "Re-Book";
                        //}
                        //else
                        //{
                        //    LnkConfirm.Text = "كتاب مرة أخرى";
                        //}
                        LnkConfirm.ForeColor = System.Drawing.Color.Red;
                        LnkConfirm.Enabled = true;
                        LnkCancel.Visible = false;
                        LnkShare.Enabled = false;
                        LnkShare.CssClass = "disabled";
                        LnkShare.ForeColor = System.Drawing.Color.LightGray;
                        LnkConfirm.CssClass = "btn btn-xs btn-default";
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkShare.ToolTip = "disabled";
                        //}
                        //else
                        //{
                        //    LnkShare.ToolTip = "معاق";
                        //}
                    }
                    else if (LblSta.Text == "3")
                    {
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkConfirm.Text = "Consultation completed";
                        //}
                        //else
                        //{
                        //    LnkConfirm.Text = "اكتملت المشاورات";
                        //}
                        LnkConfirm.CssClass = "btn btn-xs btn-default disabled";
                        LnkConfirm.ForeColor = System.Drawing.Color.Green;
                        LnkConfirm.Enabled = false;
                        LnkCancel.Visible = false;
                        LnkShare.Enabled = false;
                        LnkShare.CssClass = "disabled";
                        LnkShare.ForeColor = System.Drawing.Color.LightGray;

                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkShare.ToolTip = "disabled";
                        //}
                        //else
                        //{
                        //    LnkShare.ToolTip = "معاق";
                        //}
                    }
                    else if (LblSta.Text == "5")
                    {
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkConfirm.Text = "You not attend this consultation";
                        //}
                        //else
                        //{
                        //    LnkConfirm.Text = "أنت لا تحضر هذه المشاورة";
                        //}
                        LnkConfirm.CssClass = "btn btn-xs btn-default disabled";
                        LnkConfirm.ForeColor = System.Drawing.Color.Black;
                        LnkConfirm.Enabled = false;
                        LnkCancel.Visible = false;
                        LnkShare.Enabled = false;
                        LnkShare.CssClass = "disabled";
                        LnkShare.ForeColor = System.Drawing.Color.LightGray;

                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            LnkShare.ToolTip = "disabled";
                        //}
                        //else
                        //{
                        //    LnkShare.ToolTip = "معاق";
                        //}
                    }
                    if (lblhosid.Text != "")
                    {
                        if (LblSta.Text == "0")
                        {
                            LnkConfirm.Text = "Waiting for confirmation";
                            LnkConfirm.ForeColor = System.Drawing.Color.Green;
                            LnkConfirm.Enabled = false;
                            LnkConfirm.CssClass = "btn btn-xs btn-default disabled";
                            //LblStatus.Text = "Waiting";
                        }
                        else if (LblSta.Text == "1")
                        {
                            //if (Session["Speciality"].ToString() == "Auto")
                            //{
                                LnkConfirm.Text = "Confirmed";
                            //}
                            //else
                            //{
                            //    LnkConfirm.Text = "تم تأكيد";
                            //}
                            LnkConfirm.ForeColor = System.Drawing.Color.Red;
                            LnkConfirm.Enabled = false;
                            //LblStatus.Text = "Fixed";
                        }
                    }

                    Label date = gRow.FindControl("LblDate") as Label;
                    Label time = gRow.FindControl("LblTime") as Label;
                    DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
                    DateTime dt2 = DateTime.Parse(date.Text);
                    DateTime t1 = DateTime.Parse(DateTime.Now.ToShortTimeString());
                    DateTime t2 = DateTime.Parse(dt2.ToShortDateString() + " " + time.Text);
                    if (dt2 <= dt1)
                    {
                        if (t2 <= t1)
                        {
                            LnkCancel.Enabled = false;
                            LnkConfirm.Enabled = false;
                            LnkShare.Enabled = false;
                            LnkShare.CssClass = "disabled";
                            LnkShare.ForeColor = System.Drawing.Color.LightGray;
                            LnkConfirm.CssClass = "disabled btn btn-xs btn-default";
                           
                            if (LnkConfirm.Text == "Confirmed" || LnkConfirm.Text == "تم تأكيد" || LnkConfirm.Text == "Re-Book" || LnkConfirm.Text == "كتاب مرة أخرى")
                            {
                                LnkConfirm.ForeColor = System.Drawing.Color.Purple;
                                LnkCancel.Visible = false;
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                    LnkConfirm.Text = "Time expired";
                                //}
                                //else { LnkConfirm.Text = "انتهى الوقت"; }
                            }
                        }
                    }


                }
            }
            else
            {
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointment')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا يوجد موعد')</Script>");

                //}
                //Label2.Text = "No appointments";
                //this.ModalPopupExtender3.Show();
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }

    }

    protected void GrvAppointments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvAppointments.PageIndex = e.NewPageIndex;
        BindGrvAppointements();
    }
    protected void GrvAppointments_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label4.Text = "";
        Label5.Text = "";
        //string id = GrvAppointments.DataKeys[e.RowIndex].Values["id"].ToString();

        Label LblHosId1 = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblHosId1");
        Label LblDocId = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblDocId");
        Label LblTime = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblTime");
        Label LblDate = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblDate");
        LinkButton LnkConfirm = GrvAppointments.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
       
            string doc = obj.EnryptString(LblDocId.Text);
        string lat = ""; string Long = "";
        var d = from item in db.tbl_doctors where item.d_hakkimid == LblDocId.Text select item;
        foreach (var ddd in d)
        {
            var d1 = from item in db.tbl_doctor_locations where item.d_id == ddd.d_id select item;
            foreach (var lt in d1) { lat = lt.latitude.ToString(); Long = lt.longitude.ToString(); }
        }


        if (LblHosId1.Text == "")
        {
            Session["rtime"] = LblTime.Text;
            Session["rdate"] = LblDate.Text;
            Session["rdoctor"] = LblDocId.Text;
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_doctor_appointment where a_date='" + LblDate.Text + "'", con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Day"] = dt.Rows[0]["dayname"].ToString();
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal3", "$('#myModal3').modal();", true);
            upModal3.Update();

            
            //if (Session["Speciality"].ToString() == "Auto")
            //{
            //    Response.Redirect("doctoravailability.aspx?docid=" + doc + "&Lat=" + lat + "&Long=" + Long + "");
            //}
            //else
            //{
            //    Response.Redirect("doctoravailability.aspx?l=ar-EG&docid=" + doc + "&Lat=" + lat + "&Long=" + Long + "");
            //}
        }
        else
        {

            Session["rtime"] = LblTime.Text;
            Session["rdate"] = LblDate.Text;
            Session["rdoctor"] = LblDocId.Text;
            Session["rhospital"] = LblHosId1.Text;
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_doctor_appointment where a_date='" + LblDate.Text + "'", con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Day"] = dt.Rows[0]["dayname"].ToString();
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal4", "$('#myModal4').modal();", true);
            upModal4.Update();

            

            //string hospitalregno = "";
            //var h = from item in db.tbl_hospitalregs where item.h_hakkimid == LblHosId1.Text select item;
            //foreach (var hh in h)
            //{
            //    var hq = from item in db.tbl_hos_locations where item.h_id == hh.h_id select item;
            //    foreach (var hhh in hq) { lat = hhh.latitude.ToString(); Long = hhh.longitude.ToString(); hospitalregno = obj.EnryptString(hh.h_hakkimid); }

            //}
            //if (Session["Speciality"].ToString() == "Auto")
            //{
            //    Response.Redirect("Hospitaldoctoravailability.aspx?docid=" + doc + "&Lat=" + lat + "&Long=" + Long + "&hospitalregno=" + hospitalregno + "");
            //}
            //else
            //{
            //    Response.Redirect("Hospitaldoctoravailability.aspx?l=ar-EG&docid=" + doc + "&Lat=" + lat + "&Long=" + Long + "&hospitalregno=" + hospitalregno + "");

            //}
        }

        // Check whether if hospital id is empty then going to the bookdoc doctor appointment table and updATE that..

        //if (LblHosId1.Text == "")
        //{
        //    var docApointments = from item in db.tbl_doctor_appointments
        //                         where item.c_id == Session["hakkemid_u"].ToString() && item.d_id == LblDocId.Text && item.a_date == LblDate.Text && item.a_time == LblTime.Text
        //                         select item;
        //    foreach (var ss in docApointments)
        //    {
        //        ss.a_status = 1;
        //        db.SubmitChanges();
        //    }
        //}
        //else
        //{
        //    var hosAppointments = from item in db.tbl_hos_doc_appmnts
        //                          where item.u_id == Session["hakkemid_u"].ToString() && item.h_id == LblHosId1.Text && item.d_id == LblDocId.Text && item.a_date == LblDate.Text && item.a_time == LblTime.Text
        //                          select item;
        //    foreach (var ss in hosAppointments)
        //    {
        //        ss.a_status = 1;
        //        db.SubmitChanges();
        //    }
        //}

        //BindGrvAppointements();
    }


    // for Re-Book ...............................

    protected void Button3_Click(object sender, EventArgs e)
    {
        var available = from item in db.doctor_availabilities where item.d_id == Session["rdoctor"].ToString() && item.a_date == Session["rdate"].ToString() select item;
        if (available.Count() > 0)
        {
            string time = Session["rtime"].ToString();
            int l = time.Length - 2;
            time = time.Replace(" ", string.Empty);

            foreach (var aa in available)
            {
                var doctime = from item in db.tbl_doc_times where item.date_id == aa.id && item.time == time select item;
                if (doctime.Count() > 0)
                {
                    string dname = "";
                    string uname = "";
                    var apmnt = from item in db.tbl_doctor_appointments
                                where item.d_id == Session["rdoctor"].ToString() && item.a_date == Session["rdate"].ToString() && item.a_time == Session["rtime"].ToString() && (item.a_status != 1 || item.a_status != 4)
                                select item;
                    if (apmnt.Count() > 0)
                    {
                        foreach (var ss in apmnt)
                        {
                            ss.a_status = 1;
                        }
                        db.SubmitChanges();
                        var doctor = (from item in db.tbl_doctors where item.d_hakkimid == Session["rdoctor"].ToString() select item).First();
                        dname = doctor.d_name;
                        var user = (from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item).First();
                        string msg = "Your appointment has been placed again with doctor " + dname + " on " + Session["rdate"].ToString() + " and " + Session["rtime"].ToString() + " Thank you. Hakkeem Team!";
                        uname = user.name;
                        try
                        {
                           // Email.mail(obj.DecryptString(user.email), msg, "Hakkeem appointment");
                            Email_To_userappoinment(obj.DecryptString(user.email), dname, Session["rdate"].ToString(), Session["rtime"].ToString(),Session["day"].ToString());

                        }
                        catch (Exception ex) { }
                        try
                        {
                            string dph = "+966" + obj.DecryptString(user.contact);
                            ob.Message(dph, msg);

                            string dph1 = "+91" + obj.DecryptString(user.contact);
                            ob.Message(dph1, msg);

                        }
                        catch(Exception ex)
                        {

                        }
                        var doctor1 = (from item in db.tbl_doctors where item.d_hakkimid == Session["rdoctor"].ToString() select item).First();
                        string dmsg = "You have an new appointment again with patient " + uname + " on " + Session["rdate"].ToString() + " and " + Session["rtime"].ToString() + " Thank you. Hakkeem Team!";
                        try
                        {
                            // Email.mail(obj.DecryptString(doctor1.d_email), dmsg, "Hakkeem appointment");
                            Email_To_Doctorappoinment(obj.DecryptString(doctor1.d_email), uname, Session["rdate"].ToString(), Session["rtime"].ToString(), Session["Day"].ToString());
                        }
                        catch(Exception ex) { }
                        try
                        {
                            string sph = "+966" + obj.DecryptString(doctor1.d_contact);
                            ob.Message(sph, dmsg);


                            string sph1 = "+91" + obj.DecryptString(doctor1.d_contact);
                            ob.Message(sph1, dmsg);
                            //ob.Message(obj.DecryptString(doctor1.d_contact), dmsg);
                        }
                        catch (Exception ex) { }
                        Response.Redirect("UserAppointments.aspx");
                    }
                    else
                    {
                       
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                            Label4.Text = "Doctor not available this time, please choose another time.";
                        //}
                        //else
                        //{
                        //    Label4.Text = "طبيب لا تتوفر هذه المرة، يرجى اختيار وقت آخر.";
                        //}
                    }
                }
                else
                {
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        Label4.Text = "Doctor not available this time, please choose another time.";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal3').modal();", true);
                        //upModal3.Update();
                    //}
                    //else
                    //{
                    //    Label4.Text = "طبيب لا تتوفر هذه المرة، يرجى اختيار وقت آخر.";
                    //}
                }
            }
        }
        else
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                Label4.Text = "Doctor not available this time, please choose another time.";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal3').modal();", true);
                //upModal3.Update();
            //}
            //else
            //{
            //    Label4.Text = "طبيب لا تتوفر هذه المرة، يرجى اختيار وقت آخر.";
            //}
        }
        Session["rtime"] = "";
        Session["rdate"] = "";
        Session["rdoctor"] = "";
        //if (Session["Speciality"].ToString() == "Auto")
        //{


        //    Response.Redirect("UserAppointments.aspx");
        //}
        //else
        //{
        //    Response.Redirect("UserAppointments.aspx?l=ar-EG");
        //}
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        string time = Session["rtime"].ToString();
        int l = time.Length - 2;
        time = time.Replace(" ", string.Empty);
        var hapmnt = from item in db.view_hos_doc_available_times where item.h_hakkimid == Session["rhospital"].ToString() && item.hd_email == Session["rdoctor"].ToString() && item.time == time && item.date == Session["rdate"].ToString() select item;
        if(hapmnt.Count()>0)
        {
            foreach(var ss in hapmnt)
            {
                var apmnt = from item in db.tbl_hos_doc_appmnts where item.d_id == ss.hd_email && (item.a_status != 0 || item.a_status != 1 || item.a_status != 4) select item;
                if(apmnt.Count()>0)
                {
                    string dname = ss.hd_name;
                    string uname = "";

                    foreach (var hh in apmnt)
                    {
                        hh.a_status = 0;
                    }
                    db.SubmitChanges();

                    var user = (from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item).First();
                    string msg = "Your appointment has been placed again with doctor " + dname + " on " + Session["rdate"].ToString() + " and " + Session["rtime"].ToString() + " Thank you. Hakkeem Team!";
                    uname = user.name;
            
                    try
                    {
                        //ob.Message(obj.DecryptString(user.contact), msg);
                        string dph = "+966" + obj.DecryptString(user.contact);
                        ob.Message(dph, msg);
                        string dph1 = "+91" + obj.DecryptString(user.contact);
                        ob.Message(dph1, msg);
                    }
                    catch (Exception ex)
                    {

                    }
                    string dmsg = "You have an new appointment again with patient " + uname + " on " + Session["rdate"].ToString() + " and " + Session["rtime"].ToString() + " Thank you. Hakkeem Team!";
                   
                    var hosdoc = (from item in db.tbl_hdoctors where item.h_id == Session["rhospital"].ToString() && item.hd_email == ss.hd_email select item).First();

                    
                    try
                    {
                        string gph = "+966" + hosdoc.hd_contact;
                        ob.Message(gph, dmsg);
                        string gph1 = "+91" + hosdoc.hd_contact;
                        ob.Message(gph1, dmsg);
                        // ob.Message(hosdoc.hd_contact, dmsg);
                    }
                    catch (Exception ex) { }

                    try
                    {
                        //  Email.mail(obj.DecryptString(user.email), msg, "Hakkeem appointment");
                        Email_To_userappoinment(obj.DecryptString(user.email), dname, Session["rdate"].ToString(), Session["rtime"].ToString(), Session["Day"].ToString());
                    }
                    catch (Exception ex) { }
                    try
                    {
                        //  Email.mail(ss.hd_email, dmsg, "Hakkeem appointment");
                        Email_To_Doctorappoinment(ss.hd_email, uname, Session["rdate"].ToString(), Session["rtime"].ToString(), Session["Day"].ToString());
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        Label5.Text = "Doctor not available this time, please choose another time.";
                    //}
                    //else
                    //{
                    //    Label5.Text = "طبيب لا تتوفر هذه المرة، يرجى اختيار وقت آخر.";
                    //}
                }
            }
        }
        else
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                Label5.Text = "Doctor not available this time, please choose another time.";
            //}
            //else
            //{
            //    Label5.Text = "طبيب لا تتوفر هذه المرة، يرجى اختيار وقت آخر.";
            //}
        }
        Session["rtime"] = "";
        Session["rdate"] = "";
        Session["rdoctor"] = "";
        Session["Day"] = "";
        //if (Session["Speciality"].ToString() == "Auto")
        //{


            Response.Redirect("UserAppointments.aspx");
        //}
        //else
        //{
        //    Response.Redirect("UserAppointments.aspx?l=ar-EG");
        //}
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
    // Mail processing Method
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
    //    client.Credentials = new System.Net.NetworkCredential("bookdoc2017", "bookdoc12345");
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

    // Code before sharing . for asign doctor or hospital for sharing ..the information.
    protected void GrvAppointments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // variables for storing informations
        string shareApointmentId = "";
        string shareDocId = "";
        string shareHosApoitmentId = "";
        string shareHosDocId = "";
        string shareHosId = "";

        Label LblHosId1 = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblHosId1");
        Label LblDocId = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblDocId");
        Label LblTime = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblTime");
        Label LblDate = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblDate");

        if (LblHosId1.Text == "")
        {
            var query = from item in db.tbl_doctor_appointments
                        where item.c_id == Session["hakkemid_u"].ToString() && item.d_id == LblDocId.Text && item.a_date == LblDate.Text && item.a_time == LblTime.Text
                        select item;

            foreach (var ss in query)
            {
                shareApointmentId = ss.id.ToString();
                shareDocId = ss.d_id;
                //Session["shareApointmentId"] = ss.id.ToString();
                //Session["shareDocId"] = ss.d_id;
                //shareApointmentId = ss.id.ToString();
                //shareDocId = ss.d_id;
            }
        }
        else
        {
            var hosAppointments = from item in db.tbl_hos_doc_appmnts
                                  where item.u_id == Session["hakkemid_u"].ToString() && item.h_id == LblHosId1.Text && item.d_id == LblDocId.Text && item.a_date == LblDate.Text && item.a_time == LblTime.Text
                                  select item;
            foreach (var s in hosAppointments)
            {
                shareHosApoitmentId = s.id.ToString();
                shareHosDocId = s.d_id;
                shareHosId = s.h_id;
                //Session["shareHosApoitmentId"] = s.id.ToString();
                //Session["shareHosDocId"] = s.d_id;
                //Session["shareHosId"] = s.h_id;
            }
        }

        //if (Session["Speciality"].ToString() == "Auto")
        //{
            Response.Redirect("~/User/SharePreview.aspx?shareApointmentId=" + shareApointmentId + "&shareDocId=" + shareDocId + "&shareHosApoitmentId=" + shareHosApoitmentId + "&shareHosDocId=" + shareHosDocId + "&shareHosId=" + shareHosId + "");
        //}
        //else
        //{
        //    Response.Redirect("~/User/SharePreview.aspx?l=ar-EG&shareApointmentId=" + shareApointmentId + "&shareDocId=" + shareDocId + "&shareHosApoitmentId=" + shareHosApoitmentId + "&shareHosDocId=" + shareHosDocId + "&shareHosId=" + shareHosId + "");

        //}
    }



    protected void Button2_Click(object sender, EventArgs e)
    {



        //string patientName = "";
        //if (Session["LblHosId1"] != null)
        //{

        //    string email = "";
        //    var hosAppointments = from item in db.tbl_hos_doc_appmnts
        //                          where item.u_id == Session["hakkemid_u"].ToString() && item.h_id == Session["LblHosId1"].ToString() && item.d_id == Session["LblDocId"].ToString() && item.a_date == Session["LblDate"].ToString() && item.a_time == Session["LblTime"].ToString()
        //                          select item;
        //    foreach (var ss in hosAppointments)
        //    {
        //        var query = (from item in db.tbl_signups
        //                     where item.u_hakkimid == ss.u_id
        //                     select item).First();

        //        patientName = query.name.ToString();
        //        email = ss.d_id;


        //    }

        //    db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(hosAppointments);
        //    db.SubmitChanges();
        //    Email.mail(email, "An appointment is canceled.. Date :" + " " + Session["LblDate"].ToString() + " Time :" + Session["LblTime"].ToString() + " Ptient Name :" + patientName, "Patient appointment cancelation");
        //    try
        //    {
        //        string m = "An appointment is canceled.. Date :" + " " + Session["LblDate"].ToString() + " Time :" + Session["LblTime"].ToString() + " Ptient Name :" + patientName;
        //        SMS ob = new SMS();
        //        var doctor = (from item in db.tbl_hdoctors where item.hd_email == email select item).First();
        //        string n = doctor.hd_contact;
        //        ob.Message(n.Substring(1, n.Length), m);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //else
        //{
        //    string n = "";
        //    string email = "";
        //    var docApointments = from item in db.tbl_doctor_appointments
        //                         where item.c_id == Session["hakkemid_u"].ToString() && item.d_id == Session["LblDocId"].ToString() && item.a_date == Session["LblDate"].ToString() && item.a_time == Session["LblTime"].ToString()
        //                         select item;
        //    foreach (var ss in docApointments)
        //    {
        //        var query = (from item in db.tbl_signups
        //                     where item.u_hakkimid == ss.c_id
        //                     select item).First();

        //        patientName = query.name.ToString();
        //        var QueryEmail = (from item in db.tbl_doctors where item.d_hakkimid == ss.d_id select item).First();
        //        email = QueryEmail.d_email.ToString();
        //        n = QueryEmail.d_contact;
        //        ss.a_status = 2;
        //    }
        //    //db.tbl_doctor_appointments.DeleteAllOnSubmit(docApointments);
        //    db.SubmitChanges();
        //    Email.mail(email, "An appointment is canceled.. Date :" + " " + Session["LblDate"].ToString() + " Time :" + Session["LblTime"].ToString() + " Ptient Name :" + patientName, "Patient appointment cancelation");
        //    try
        //    {
        //        string m = "An appointment is canceled.. Date :" + " " + Session["LblDate"].ToString() + " Time :" + Session["LblTime"].ToString() + " Ptient Name :" + patientName;
        //        SMS ob = new SMS();


        //        ob.Message(n.Substring(1, n.Length), m);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //Session["LblHosId1"] = null;
        //Session["LblDocId"] = null;
        //Session["LblTime"] = null;
        //Session["LblDate"] = null;
        //BindGrvAppointements();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        RegisterStartupScript("", "<Script Language=JavaScript>swal('Test hakkeem')</Script>");
    }

    protected void GrvAppointments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "report")
        {

            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;

            Label lbl3 = GrvAppointments.Rows[rowIndex].FindControl("Label3") as Label;
            Session["hid"] = lbl3.Text;
            Session["apmntid"] = e.CommandArgument.ToString();
            //Session[""]
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                Response.Redirect("~/User/reporttohakkeem.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/User/reporttohakkeem.aspx?l=ar-EG");
            //}
        }

    }

    protected void GrvAppointments_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GrvAppointments.Rows)
        {
         
           
            Label lbl2 = gr.FindControl("Label2") as Label;
            Label lbl3 = gr.FindControl("Label3") as Label;
            LinkButton lnk7 = gr.FindControl("LinkButton7") as LinkButton;
            Label date = gr.FindControl("LblDate") as Label;
            Label time = gr.FindControl("LblTime") as Label;
            DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime dt2 = DateTime.Parse(date.Text);
            DateTime t1 = DateTime.Parse(DateTime.Now.ToShortTimeString());
            DateTime t2 = DateTime.Parse(dt2.ToShortDateString() + " " + time.Text);
            if (dt2 <= dt1)
            {
                if (t2 <= t1)
                {
                   
                    if (lbl3.Text == "")
                    {
                        var Query = from item in db.tbl_report_forms where item.apmnt_id == int.Parse(lbl2.Text) && item.h_id == "" select item;

                        if (Query.Count() > 0)
                        {
                            lnk7.Enabled = false;
                            //if (Session["Speciality"].ToString() == "Auto")
                            //{
                                lnk7.Text = "Already reported!";
                            //}
                            //else
                            //{
                            //    lnk7.Text = "ذكرت بالفعل!";
                            //}
                            lnk7.CssClass = "btn btn-xs btn-danger disabled";
                        }
                        else
                        {

                            lnk7.Enabled = true;


                        }
                    }
                    else
                    {
                        var Query = from item in db.tbl_report_forms where item.apmnt_id == int.Parse(lbl2.Text) && item.h_id != "" select item;

                        if (Query.Count() > 0)
                        {
                            lnk7.Enabled = false;
                            //if (Session["Speciality"].ToString() == "Auto")
                            //{
                                lnk7.Text = "Already reported!";
                            //}
                            //else
                            //{
                            //    lnk7.Text = "ذكرت بالفعل!";
                            //}
                            lnk7.CssClass = "btn btn-xs btn-danger disabled";
                        }
                        else
                        {

                            lnk7.Enabled = true;


                        }
                    }
                }
                else
                {
                    lnk7.Enabled = false;
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        lnk7.Text = "Consulting not yet!";
                    //}
                    //else
                    //{
                    //    lnk7.Text = "استشارات لا بعد!";
                    //}
                    lnk7.CssClass = "btn btn-xs btn-danger disabled";
                }
            }
            else
            {
                lnk7.Enabled = false;
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    lnk7.Text = "Consulting not yet!";
                //}
                //else
                //{
                //    lnk7.Text = "استشارات لا بعد!";
                //}
                lnk7.CssClass = "btn btn-xs btn-danger disabled";
            }




        }
        //BindGrvAppointements();
    }

    protected void GrvAppointments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
        upModal1.Update();

        //string hospitalName="";

        Label LblHosId1 = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblHosId1");
        Label LblDocId = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblDocId");
        Label LblTime = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblTime");
        Label LblDate = (Label)GrvAppointments.Rows[e.RowIndex].FindControl("LblDate");
        Session["LblHosId1"] = LblHosId1.Text;
        Session["LblDocId"] = LblDocId.Text;
        Session["LblTime"] = LblTime.Text;
        Session["LblDate"] = LblDate.Text;
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter("Select DATENAME(dw,date) as dayname from tbl_apmnt_cancel where date='" + LblDate.Text + "'", con);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Session["CDay"] = dt.Rows[0]["dayname"].ToString();
        }


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Button1.Text = "Please wait...";
        Button1.CssClass = "btn btn-sm btn-primary disabled";
        string patientName = "";
        if (string.IsNullOrEmpty(Session["LblHosId1"] as string))
        {
            string n = "";
            string email = "";
            var docApointments = from item in db.tbl_doctor_appointments
                                 where item.c_id == Session["hakkemid_u"].ToString() && item.d_id == Session["LblDocId"].ToString() && item.a_date == Session["LblDate"].ToString() && item.a_time == Session["LblTime"].ToString()
                                 select item;
            foreach (var ss in docApointments)
            {
                try
                {
                    tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                    {
                        apmnt_id = ss.id.ToString(),
                        d_id = ss.d_id,
                        u_id = ss.c_id,
                        cancel_rsn = RadioButtonList1.SelectedItem.Text,
                        canceled_by = "u",
                        date = DateTime.Now.ToShortDateString(),
                        time = DateTime.Now.ToShortTimeString(),


                    };
                    db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                    db.SubmitChanges();
                }
                catch (Exception ex) { }

                var query = (from item in db.tbl_signups
                             where item.u_hakkimid == ss.c_id
                             select item).First();

                patientName = query.name.ToString();
                string uemail = obj.DecryptString(query.email);
                string uph = obj.DecryptString(query.contact);
                //email = obj.DecryptString(ss.d_id);
                var doctor = (from item in db.tbl_doctors where item.d_hakkimid == ss.d_id select item).First();
                email = obj.DecryptString(doctor.d_email);
                n = obj.DecryptString(doctor.d_contact);
                ss.a_status = 2;
                string doctorname = doctor.d_name;
              
                try
                {
                    string dph = "+966" + n.ToString();
                   // ob.Message(dph, umsg);
                    ob.Message(dph, "Your Appointment has been Cancelled with patient " + patientName + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString());

                    string dph1 = "+91" + n.ToString();
                    // ob.Message(dph, umsg);
                    ob.Message(dph1, "Your Appointment has been Cancelled with patient " + patientName + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString());
                }
                catch (Exception ex)
                {

                }
                try
                {
                    string usph = "+966" + uph.ToString();
                    ob.Message(usph, "Your Appointment has been Cancelled with Doctor " + doctorname + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString());

                    string usph1 = "+91" + uph.ToString();
                    ob.Message(usph1, "Your Appointment has been Cancelled with Doctor " + doctorname + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString());
                }
                catch (Exception ex) { }
                try
                {
                    //Your Appointment has been Cancelled with patient
                    Email_To_AppoinmentCancilation(email, patientName, Session["LblDate"].ToString(), Session["LblTime"].ToString(), Session["CDay"].ToString());
                    //Email.mail(email, "Your Appointment has been Cancelled with patient " + patientName + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString(), "Hakkeem appointment canceled");
                }
                catch (Exception ex) { }
                try
                {
                    Email_To_AppoinmentCancilation(uemail, doctorname, Session["LblDate"].ToString(), Session["LblTime"].ToString(),Session["CDay"].ToString());
                    //Email.mail(uemail, "Your Appointment has been Cancelled with Doctor " + doctorname + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString(), "Hakkeem appointment canceled");
                }
                catch (Exception ex)
                {

                }
               
            }

            //db.tbl_doctor_appointments.DeleteAllOnSubmit(docApointments);
            db.SubmitChanges();

        }
        else
        {
            string email = "";
            string docemail = "";
            string n = "";
            string uemail = "";
            string uph = "";
            string doctorname = "";
            var hosAppointments = from item in db.tbl_hos_doc_appmnts
                                  where item.u_id == Session["hakkemid_u"].ToString() && item.h_id == Session["LblHosId1"].ToString() && item.d_id == Session["LblDocId"].ToString() && item.a_date == Session["LblDate"].ToString() && item.a_time == Session["LblTime"].ToString()
                                  select item;
            foreach (var ss in hosAppointments)
            {

                try
                {
                    tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                    {
                        apmnt_id = ss.id.ToString(),
                        d_id = ss.d_id,
                        u_id = ss.u_id,
                        cancel_rsn = RadioButtonList1.SelectedItem.Text,
                        canceled_by = "u",
                        date = DateTime.Now.ToShortDateString(),
                        time = DateTime.Now.ToShortTimeString(),
                        hos_id = ss.h_id,

                    };
                    db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                    db.SubmitChanges();
                }
                catch (Exception ex) { }



                var query = (from item in db.tbl_signups
                             where item.u_hakkimid == ss.u_id
                             select item).First();
                uemail = obj.DecryptString(query.email);
                uph = obj.DecryptString(query.contact);
                patientName = query.name.ToString();
                var hospital = (from item in db.tbl_hospitalregs where item.h_hakkimid == ss.h_id select item).First();
                email = obj.DecryptString(hospital.h_email);
                docemail = ss.d_id.ToString();
                var hdoctor = (from item in db.tbl_hdoctors where item.hd_email == docemail select item).First();
                n = hdoctor.hd_contact/*"+919539395281"*/;
                ss.a_status = 2;
                doctorname = hdoctor.hd_name;
            }

            //db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(hosAppointments);
            db.SubmitChanges();
          
            try
            {
                string dph = "+966" + n.ToString();
                ob.Message(dph, "Your Appointment has been Cancelled with patient " + patientName + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString());
                string dph1 = "+91" + n.ToString();
                ob.Message(dph1, "Your Appointment has been Cancelled with patient " + patientName + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString());
            }
            catch (Exception ex)
            {

            }
            try
            {
                string usph = "+966" + uph.ToString();
                ob.Message(usph, "Your Appointment has been Cancelled with Doctor " + doctorname + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString());

                string usph1 = "+91" + uph.ToString();
                ob.Message(usph1, "Your Appointment has been Cancelled with Doctor " + doctorname + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString());
            }
            catch (Exception ex) { }
            try
            {
                // Email.mail(email, "Your Appointment has been Cancelled with patient " + patientName + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString(), "Hakkeem appointment canceled");
                //Email.mail(docemail, "Your Appointment has been Cancelled with patient " + patientName + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString(), "Hakkeem appointment canceled");
                Email_To_AppoinmentCancilation(email, patientName, Session["LblDate"].ToString(), Session["LblTime"].ToString(), Session["CDay"].ToString());
                Email_To_AppoinmentCancilation(docemail, patientName, Session["LblDate"].ToString(), Session["LblTime"].ToString(), Session["CDay"].ToString());
            }
            catch (Exception ex)
            {

            }
            try
            {
                //Email.mail(uemail, "Your Appointment has been Cancelled with Doctor " + doctorname + " on " + Session["LblDate"].ToString() + " and " + Session["LblTime"].ToString(), "Hakkeem appointment canceled");
                Email_To_AppoinmentCancilation(uemail, doctorname, Session["LblDate"].ToString(), Session["LblTime"].ToString(),Session["CDay"].ToString());
            }
            catch (Exception ex)
            {

            }
           
        }

        BindGrvAppointements();

        //if (Session["Speciality"].ToString() == "Auto")
        //{


            Response.Redirect("UserAppointments.aspx");
        //}
        //else
        //{
        //    Response.Redirect("UserAppointments.aspx?l=ar-EG");
        //}
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
            messagestr = messagestr + "your appointment cancellation."+name+", Time:"+time+" "+day+", Date:"+date+"";
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
   






}