using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Globalization;

public partial class Doctor_DoctorHome : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    databaseDataContext db = new databaseDataContext();
    MailMessage Email = new MailMessage();
    secure obj = new secure();
    SqlCommand cmd;
    SqlCommand com;
    SMS ob = new SMS();


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
      
         if (!IsPostBack)
        {
            con.Open();
            deltimeover();
            CheckLocation();

            DoctorDetails();
            ProfileCompletionStatus();
            Availability();
            review();
            AllAppointmentsCount();
            AppointmentsCount();
        }

        try
        {
            if (Session["check"].ToString() == "1")
            {
                Session["check"] = "0";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Already you take an appointment this day, so you please choose another day.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('إذا كنت تأخذ موعد هذا اليوم، لذلك يرجى اختيار يوم آخر.')</Script>");

                //}
            }
            else if (Session["check"].ToString() == "2")
            {
                Session["check"] = "0";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Your appointment is fixed')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم إصلاح موعدك')</Script>");
                //}
            }
            else if (Session["check"].ToString() == "3")
            {
                Session["check"] = "0";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Hakkeem id not exist')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('حكيم معرف غير موجود')</Script>");
                //}
            }
        }
        catch (Exception ex)
        {

        }


    }


    private static DateTime ConvertToEngCal(string hijri)
    {
        CultureInfo arSA = new CultureInfo("ar-SA");
        arSA.DateTimeFormat.Calendar = new HijriCalendar();
        return DateTime.ParseExact(hijri, "dd/MM/yy", arSA);
    }
  


     

    public void deltimeover()
    {
        try
        {
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;


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
            //    dt =Convert.ToDateTime(dttime);

            //}









            DateTime time = new DateTime();
            time = DateTime.Now;
            string displayTime = time.ToString("hh:mm tt");
            // MessageBox.Show(displayTime.ToString());
            // MessageBox.Show(dt.ToString("yyyy-MM-dd"));
            string dat = dt.ToString("yyyy-MM-dd");
            com = new SqlCommand("select id from doctor_availability where a_date<'" + dat + "'", con);
            SqlDataReader dtr = com.ExecuteReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    com = new SqlCommand("delete from tbl_doc_time where date_id='" + dtr[0].ToString() + "'", con);
                    com.ExecuteNonQuery();
                }
            }
            dtr.Close();
            com = new SqlCommand("delete from doctor_availability where a_date<'" + dat + "'", con);
            com.ExecuteNonQuery();

            com = new SqlCommand("select id from doctor_availability where a_date='" + dat + "'", con);
            dtr = com.ExecuteReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    // time > LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))

                    //  com = new SqlCommand("delete from tbl_doc_time where date_id='" + dtr[0].ToString() + "' and time > LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))", con);
                    com = new SqlCommand("delete from tbl_doc_time where date_id='" + dtr[0].ToString() + "' and time < LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))", con);
                    com.ExecuteNonQuery();
                }
            }
            com = new SqlCommand("delete from doctor_availability where id not in (select date_id from tbl_doc_time)", con);
            com.ExecuteNonQuery();
        }
        catch (Exception ex) { }
    }
    public void review()
    {
        try
        {
            var Query = from item in db.tbl_user_feeds where item.d_email == Session["hakkeemid_d"].ToString() orderby item.id descending select item;
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



                SqlCommand cmd6 = new SqlCommand("SELECT rate_wt FROM tbl_rating where d_id='" + Session["hakkeemid_d"].ToString() + "'and u_id='" + uid + "'", con);
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
                    ratefill = ratefill + "<img  src='../images/Filled.gif'>";
                }
                for (int i = 0; i < b; i++)
                {
                    ratefill = ratefill + "<img  src='../images/Empty.gif'>";
                }

                Literal l = new Literal();
                l = dl.FindControl("Literal4") as Literal;
                l.Text = ratefill;

                //----------------------Rating-----------------------------------------

                //....................Rating--------------------------------------------



                SqlCommand cmd61 = new SqlCommand("SELECT rate_bm FROM tbl_rating where d_id='" + Session["hakkeemid_d"].ToString() + "'and u_id='" + uid + "'", con);
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
                    ratefill1 = ratefill1 + "<img  src='../images/Filled.gif'>";
                }
                int b1;
                b1 = 5 - bm;
                for (int i = 0; i < b1; i++)
                {
                    ratefill1 = ratefill1 + "<img  src='../images/Empty.gif'>";
                }

                Literal l1 = new Literal();
                l1 = dl.FindControl("Literal5") as Literal;
                l1.Text = ratefill1;
                //----------------------Rating-----------------------------------------

                //....................Rating--------------------------------------------


                //----------------------Rating-----------------------------------------
                //....................Rating--------------------------------------------


                SqlCommand cmd63 = new SqlCommand("SELECT rate_service FROM tbl_rating where d_id='" + Session["hakkeemid_d"].ToString() + "'and u_id='" + uid + "'", con);
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
                    ratefill11 = ratefill11 + "<img  src='../images/Filled.gif'>";
                }
                int b2;
                b2 = 5 - ser;
                for (int i = 0; i < b2; i++)
                {
                    ratefill11 = ratefill11 + "<img  src='../images/Empty.gif'>";
                }

                Literal l11 = new Literal();
                l11 = dl.FindControl("Literal6") as Literal;
                l11.Text = ratefill11;
                //----------------------Rating-----------------------------------------
            }
        }
        catch (Exception ex) { }
    }
    private void ProfileCompletionStatus()
    {
        try
        {
            cmd = new SqlCommand("select d_location,d_address,d_country,d_dob,d_about_you,d_education,d_language,d_specialties,d_photo,d_experience,d_hospital_afili from tbl_doctor where d_hakkimid='" + Session["hakkeemid_d"].ToString() + "'", con);

            int pervale = 0;
            int qualfValue = 0;
            int proflValue = 0;
            int expValue = 0;

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //Personal status
                    if (dr[0].ToString() != "")
                    {
                        pervale = pervale + 20;
                    }
                    if (dr[1].ToString() != "")
                    {
                        pervale = pervale + 20;
                    }
                    if (dr[2].ToString() != "")
                    {
                        pervale = pervale + 20;
                    }
                    if (dr[3].ToString() != "")
                    {
                        pervale = pervale + 10;
                    }
                    if (dr[4].ToString() != "")
                    {
                        pervale = pervale + 10;
                    }
                    //Qualification status
                    if (dr[5].ToString() != "")
                    {
                        pervale = pervale + 10;
                    }
                    if (dr[6].ToString() != "")
                    {
                        pervale = pervale + 10;
                    }
                    if (dr[7].ToString() != "")
                    {
                        qualfValue = qualfValue + 40;
                    }
                    // Profile status
                    //if (dr[8].ToString() != "")
                    //{
                    //    qualfValue = qualfValue + 20;
                    //}
                    if (dr[9].ToString() != "")
                    {
                        qualfValue = qualfValue + 20;
                    }
                    //Experience status
                    if (dr[10].ToString() != "")
                    {
                        qualfValue = qualfValue + 40;
                    }

                    //if (dr[10].ToString() != "")
                    //{
                    //    qualfValue = qualfValue + 20;
                    //}

                }
            }

            dr.Close();

            if (pervale > 100) { pervale = 100; }

            if (qualfValue > 100) { qualfValue = 100; }
            //if (proflValue > 100) { proflValue = 100; }
            //if (expValue > 100) { expValue = 100; }

            personalProgress.Style["width"] = pervale.ToString() + "%";
            personalProgress.Attributes.Add("aria-valuenow", pervale.ToString());
            LblPersonalStatus.Text = pervale + "%";

            qualfProgress.Style["width"] = qualfValue.ToString() + "%";
            qualfProgress.Attributes.Add("aria-valuenow", qualfValue.ToString());
            LblQualfStatus.Text = qualfValue + "%";

            //profileProgress.Style["width"] = proflValue.ToString() + "%";
            //profileProgress.Attributes.Add("aria-valuenow", proflValue.ToString());
            //LblProfileStatus.Text = proflValue + "%";

            //exprncProgress.Style["width"] = expValue.ToString() + "%";
            //exprncProgress.Attributes.Add("aria-valuenow", expValue.ToString());
            //LblExpStatus.Text = expValue + "%";
        }
        catch (Exception ex)
        {

        }
    }


    public void AppointmentsCount()
    {
        var query = from item in db.tbl_doctor_appointments
                    where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == DateTime.Now.ToString("yyyy-MM-dd") && item.a_status == 1
                    orderby item.a_time ascending
                    select item;
        if (query.Count() > 0)
        {
            Label11.Text = query.Count().ToString();
            Label11.CssClass = "glow";
        }
        else
        {
            Label11.Text = "0";
            
        }

    }

    public void AllAppointmentsCount()
    {
        var query = from item in db.tbl_doctor_appointments
                    where item.d_id == Session["hakkeemid_d"].ToString() && item.a_status == 1&& item.a_date != DateTime.Now.ToString("yyyy-MM-dd")
                    orderby item.a_time ascending
                    select item;
        if (query.Count() > 0)
        {
            Label12.Text = query.Count().ToString();
        }
        else
        {
            Label12.Text = "0";
        }

    }


    public void DoctorDetails()
    {
        try
        {
            var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
            foreach (var ss in Query)
            {
               Label1.Text = ss.d_name;
                //Label3.Text = "Freelancer";
                Label4.Text = ss.d_education;
                Label2.Text = ss.d_specialties;
                Label14.Text = ss.d_specialties;
                Label5.Text = ss.d_language;
                if (ss.d_photo == null)
                {
                    Image1.ImageUrl = "images/doctor.svg";
                }
                else
                {
                    Image1.ImageUrl = ss.d_photo;
                }
            }
            List<string> lang = new List<string>();
            var Query1 = from item in db.tbl_doc_languages where item.doc_id == Session["hakkeemid_d"].ToString() select item;
            foreach(var ss in Query1)
            {
                lang.Add(ss.d_Language);

            }
            string s = "";
            for (int i = 0; i < lang.Count(); i++)
            {
                if (s == "")
                {
                    s = lang[i];
                }
                else
                {
                    s += " " + lang[i];
                }

            }
            Label5.Text = s;
        }
        catch (Exception ex)
        {

        }
    }

    public void CheckLocation()
    {
        try
        {
            var query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_hakkimid == Session["hakkeemid_d"].ToString()
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
            if (query.Count() <= 0)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("~/Doctor/SetLocation.aspx");
                //}
                //else
                //{
                //    Response.Redirect("~/Doctor/SetLocation.aspx?l=ar-EG");
                //}
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnUpdateProfile_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor profile.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor profile.aspx?l=ar-EG");
        //}
    }



    public void Availability()
    {
        try
        {
            var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() orderby item.a_date ascending select item;
            if (Query.Count() > 0)
            {

                DataList2.DataSource = Query;
                DataList2.DataBind();
                foreach (DataListItem dli in DataList2.Items)

                {
                    //var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() select item;
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
                    //        //string ntime = "";
                    //        //foreach (string ttt in listTime)
                    //        //{
                    //        //    if (ntime == "")
                    //        //    {
                    //        //        ntime = ttt;
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //       // ntime += "," + ttt;
                    //        //    }
                    //        //}
                    //        //try
                    //        //{
                    //        //    var Query7 = from item in db.tbl_doc_times where item.date_id == ss.id select item;
                    //        //    foreach (var nn in Query7)
                    //        //    {
                    //        //        nn.time = ntime;
                    //        //    }
                    //        //    db.SubmitChanges();
                    //        //}
                    //        //catch (Exception ex)
                    //        //{ }
                    //    }
                    //}



                    Label lbl6 = dli.FindControl("Label6") as Label;
                    DataList dl3 = dli.FindControl("DataList3") as DataList;
                    var Query2 = from item in db.view_doc_available_times where item.a_date == lbl6.Text && item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[3] { new DataColumn("date"), new DataColumn("time"), new DataColumn("email") });
                    foreach (var ss in Query2)
                    {
                        if (ss.time == "")
                        { }
                        else
                        {
                            dt.Rows.Add(ss.a_date, ss.time, ss.d_email);
                        }

                    }
                    dl3.DataSource = dt;
                    dl3.DataBind();
                    lbl6.Text = DateTime.Parse(lbl6.Text).ToString("yyy d MMM");
                    foreach (DataListItem dl3i in dl3.Items)
                    {
                        //Button btn2 = dl3i.FindControl("Button2") as Button;
                        LinkButton lnk2 = dl3i.FindControl("LinkButton2") as LinkButton;
                        int l = lnk2.Text.Length - 2;
                        lnk2.Text = lnk2.Text.Insert(l, " ");
                        string date = DateTime.Parse(lbl6.Text).ToString("yyyy-MM-dd");
                        var Query3 = from item in db.tbl_doctor_appointments
                                     where
                            item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == date && item.a_time == lnk2.Text
                                     select item;
                        if (Query3.Count() > 0)
                        {
                            foreach (var tt in Query3)
                            {
                                if (tt.a_status == 0)
                                { lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "Waiting for confirmation"; }
                                if (tt.a_status == 1|| tt.a_status == 4)
                                { lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false; lnk2.ToolTip = "Booked"; }
                            }

                        }
                        else
                        {
                            //lnk2.BackColor = System.Drawing.Color.Green;
                            //if (Session["Language"].ToString() == "Auto")
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
            else
            {
                DataList2.Visible = false;
                Label23.Visible = true;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Label23.Text = "No availabilities";
                //}
                //else
                //{
                //    Label23.Text = "لا تتوفر";
                //}
            }
        }
        catch (Exception ex) { }
    }


    protected void DataList3_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "doc")
        {
            //Session["doctor"] = e.CommandArgument.ToString();
            Label lbl7 = e.Item.FindControl("Label7") as Label;
            Label lbl8 = e.Item.FindControl("Label8") as Label;
            Session["date"] = lbl7.Text;
            Session["time"] = lbl8.Text;
            TxtApntmtDate.Text = Session["date"].ToString();
            TxtApointmentTime.Text = Session["time"].ToString();
            //TextBox1.Text = Session["date"].ToString();
            //TextBox2.Text = Session["time"].ToString();
            //Label9.Text = "<b>Do you want take an appointment on " + "<span style='color:red'>" + Session["date"] + "</span> at " + "<span style='color:red'>" + Session["time"] + "</span>...?</b>";
            //ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == TextBox1.Text orderby item.a_date ascending select item;
            if (Query.Count() > 0)
            {

                DataList2.DataSource = Query;
                DataList2.DataBind();
                foreach (DataListItem dli in DataList2.Items)

                {
                    var Query4 = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() select item;
                    foreach (var ss in Query4)
                    {
                        DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
                        DateTime dt2 = DateTime.Parse(ss.a_date);
                        if (dt2 < dt1)
                        {
                            var Query5 = from item in db.doctor_availabilities where item.id == ss.id select item;
                            foreach (var dd in Query5)
                            {
                                db.doctor_availabilities.DeleteOnSubmit(dd);
                            }
                            db.SubmitChanges();
                        }
                        else if (dt2 > dt1) { }
                        else if (dt1 == dt2)
                        {
                            List<string> listTime = new List<string>();
                            string time = DateTime.Now.AddHours(2).ToShortTimeString();
                            var Query6 = from item in db.view_doc_available_times where item.a_date == ss.a_date select item;
                            foreach (var tt in Query6)
                            {

                                DateTime dtt1 = DateTime.Parse(time);
                                try
                                {
                                    DateTime dtt2 = DateTime.Parse(tt.time);
                                    if (dtt2 >= dtt1)
                                    {
                                        listTime.Add(tt.time);
                                    }
                                }
                                catch (Exception ex)
                                {


                                    var qry = from item in db.doctor_availabilities where item.a_date == ss.a_date select item;
                                    foreach (var dd in qry)
                                    {
                                        db.doctor_availabilities.DeleteOnSubmit(dd);
                                    }
                                    db.SubmitChanges();



                                }

                            }
                            //string ntime = "";
                            //foreach (string ttt in listTime)
                            //{
                            //    if (ntime == "")
                            //    {
                            //        ntime = ttt;
                            //    }
                            //    else
                            //    {
                            //        ntime += "," + ttt;
                            //    }
                            //}
                            //try
                            //{
                            //    var Query7 = from item in db.tbl_doc_times where item.date_id == ss.id select item;
                            //    foreach (var nn in Query7)
                            //    {
                            //        nn.time = ntime;
                            //    }
                            //    db.SubmitChanges();
                            //}
                            //catch (Exception ex)
                            //{ }
                        }
                    }



                    Label lbl6 = dli.FindControl("Label6") as Label;
                    DataList dl3 = dli.FindControl("DataList3") as DataList;
                    var Query2 = from item in db.view_doc_available_times where item.a_date == lbl6.Text && item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[3] { new DataColumn("date"), new DataColumn("time"), new DataColumn("email") });
                    foreach (var ss in Query2)
                    {
                        if (ss.time == "")
                        { }
                        else
                        {
                            dt.Rows.Add(ss.a_date, ss.time, ss.d_email);
                        }

                    }
                    dl3.DataSource = dt;
                    dl3.DataBind();
                    lbl6.Text = DateTime.Parse(lbl6.Text).ToString("yyy d MMM");
                    foreach (DataListItem dl3i in dl3.Items)
                    {
                        //Button btn2 = dl3i.FindControl("Button2") as Button;
                        LinkButton lnk2 = dl3i.FindControl("LinkButton2") as LinkButton;
                        int l = lnk2.Text.Length - 2;
                        lnk2.Text = lnk2.Text.Insert(l, " ");
                        string date = DateTime.Parse(lbl6.Text).ToString("yyyy-MM-dd");
                        var Query3 = from item in db.tbl_doctor_appointments
                                     where
                            item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == date && item.a_time == lnk2.Text
                                     select item;
                        if (Query3.Count() > 0)
                        {
                            foreach (var tt in Query3)
                            {
                                if (tt.a_status == 0)
                                { lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "Waiting for confirmation"; }
                                if (tt.a_status == 1 || tt.a_status == 4)
                                { lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false; lnk2.ToolTip = "Booked"; }
                            }

                        }
                        else
                        {
                            //lnk2.BackColor = System.Drawing.Color.Green;
                            //if (Session["Language"].ToString() == "Auto")
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
        }
        catch (Exception ex) { }
    }

    protected void BtnTakeAppointment_Click(object sender, EventArgs e)
    {
        try
        {
            var Query11 = from item in db.tbl_signups where item.u_hakkimid == TxtBookDocUserId.Text select item;
            if (Query11.Count() > 0)
            {
                var Query = from item in db.tbl_doctor_appointments where item.c_id == TxtBookDocUserId.Text && item.a_date == Session["date"].ToString() select item;
                if (Query.Count() > 0)
                {
                    Session["check"] = "1";
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("~/Doctor/DoctorHome.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Doctor/DoctorHome.aspx?l=ar-EG");
                    //}
                    //RegisterStartupScript("", "<Script Language=JavaScript>swal('Already you take an appointment this day, so you please choose another day.')</Script>");
                    //Label9.Text = "Already you take an appointment this day, so you please choose another day.";
                    //this.ModalPopupExtender3.Show();
                }
                else
                {
                    var Query1 = from item in db.tbl_signups where item.u_hakkimid == TxtBookDocUserId.Text select item;
                    foreach (var u in Query1)
                    {


                        tbl_doctor_appointment da = new tbl_doctor_appointment()
                        {
                            d_id = Session["hakkeemid_d"].ToString(),
                            a_date = TxtApntmtDate.Text,
                            c_id = TxtBookDocUserId.Text,
                            a_status = 1,/*0,*/
                            a_payment = DdlPayments.SelectedItem.Text,
                            a_reason = TxtReasonToVisit.SelectedItem.Text,
                            a_time = TxtApointmentTime.Text,
                        };
                        db.tbl_doctor_appointments.InsertOnSubmit(da);
                        db.SubmitChanges();
                        Availability();
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_doctor_appointment where a_date='" + TxtApntmtDate.Text + "'", con);
                        sda1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            Session["Day"] = dt1.Rows[0]["dayname"].ToString();
                        }
                        string doctorname = "";
                        var doctor1 = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
                        {
                            foreach (var d in doctor1) { doctorname = d.d_name; }
                        }
                        //string msg = "Dear patient, your appointment is fixed with Dr."+doctorname+ " on "+TxtApntmtDate.Text+" at "+TxtApointmentTime.Text+ ", Thank you. Hakkeem Team.";
                        string msg = "Your Appointment has been fixed with Doctor" + doctorname + " on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + ", Thank you. Hakkeem Team.";

                        /// Email.mail(obj.DecryptString(u.email), msg, "Hakkeem appointment");
                        Email_To_userappoinment(obj.DecryptString(u.email), doctorname, TxtApntmtDate.Text, TxtApointmentTime.Text,Session["Day"].ToString());
                        string dmsg = "Your Appointment has been fixed with patient " + u.name + " on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + ", Thank you.Hakkeem Team.";
                        var doctor = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item; foreach (var d in doctor)
                        {
                            try
                            {
                                //Email.mail(obj.DecryptString(d.d_email), dmsg, "Hakkeem appointment");
                                Email_To_Doctorappoinment(obj.DecryptString(d.d_email), u.name, TxtApntmtDate.Text, TxtApointmentTime.Text, Session["Day"].ToString());
                            } catch (Exception ex) { }
                            try { ob.Message(obj.DecryptString(d.d_contact), dmsg); } catch (Exception ex) { }
                        }
                        try
                        {
                            ob.Message(obj.DecryptString(u.contact), msg);
                        }
                        catch (Exception ex)
                        {

                        }
                        //Label10.Text = "Appointment fixed";
                        //ModalPopupExtender2.Show();
                        //Email("nahasshahul007@gmail.com", msg);
                        //RegisterStartupScript("", "<Script Language=JavaScript>swal('Your appointment fixed')</Script>");
                        //Response.Redirect("~/User/Finish appointment.aspx");
                        Session["check"] = "2";
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            Response.Redirect("~/Doctor/DoctorHome.aspx");
                        //}
                        //else
                        //{
                        //    Response.Redirect("~/Doctor/DoctorHome.aspx?l=ar-EG");
                        //}
                    }
                }
            }
            else
            {
                Session["check"] = "3";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("~/Doctor/DoctorHome.aspx");
                //}
                //else
                //{
                //    Response.Redirect("~/Doctor/DoctorHome.aspx?l=ar-EG");
                //}
                //RegisterStartupScript("", "<Script Language=JavaScript>swal('Hakkeem id not exist')</Script>");
                //Label9.Text = "Hakkeem id not exist";
                //ModalPopupExtender3.Show();
            }
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
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + timepath + "'></td><td style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + time + " "+day+"</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + calender + "'></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + date + "</td></tr></tbody></table></td></tr>";


            messagestr = messagestr + "<tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
            messagestr = messagestr + " <tbody><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<span style='color:#4aa9af'><a href='"+privacy+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href=''"+contact+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
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
    protected void TxtBookDocUserId_TextChanged(object sender, EventArgs e)
    {

    }
   
    protected void BtnError_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1.Hide();
    }

    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (RadioButtonList1.SelectedIndex == 0)
    //    {
    //        var doctor = from item in db.tbl_ratings where item.d_id == Session["hakkeemid_d"].ToString() select item;
    //        foreach (var ss in doctor)
    //        {
    //            ss.status = 0;
    //            db.SubmitChanges();
    //            RegisterStartupScript("", "<Script Language=JavaScript>swal('Succeess')</Script>");
    //        }
    //    }
    //    else if (RadioButtonList1.SelectedIndex == 1)
    //    {
    //        var doctor = from item in db.tbl_ratings where item.d_id == Session["hakkeemid_d"].ToString() select item;
    //        foreach (var ss in doctor)
    //        {
    //            ss.status = 1;
    //            db.SubmitChanges();
    //            RegisterStartupScript("", "<Script Language=JavaScript>swal('Success')</Script>");
    //        }
    //    }
    //}
}