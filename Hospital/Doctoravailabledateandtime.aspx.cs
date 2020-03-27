using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Net.Mail;

public partial class Hospital_Doctor_available_date_and_time : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    SMS ob = new SMS();
    secure obj = new secure();
    List<string> times = new List<string>();

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
        if (Session["hakkeemid_h"] != null)
        {
            
            if (!IsPostBack)
            {try
                {
                    Session["doctor"] = obj.DecryptString(Request.QueryString["docid"].ToString());
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='"+ Session["doctor"]+"' ", con);
                    DataTable dtt = new DataTable();
                    sda.Fill(dtt);
                    if(dtt.Rows.Count>0)
                    {
                        Session["Docph"] = dtt.Rows[0]["hd_contact"].ToString();
                    }
                }
                catch (Exception ex)
                {
                }
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
                TodayAviablDoctrs();
                SetDoctor();
            }
            else
            {
            }
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/Hospita Login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/Hospita Login.aspx?l=ar-EG");
            //}
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

    private void SetDoctor()
    {
        var selctDoc = from doc in db.tbl_hdoctors
                       where doc.hd_email == Session["doctor"].ToString()
                       select doc;
        foreach (var ss in selctDoc)
        {
            Label1.Text = ss.hd_name;
            if(ss.hd_photo ==null)
            {
                Image1.ImageUrl = "../Doctorimages/doctor.png";
            }
            else
            {
                Image1.ImageUrl = ss.hd_photo;
            }
          
        }
    }

    //public void Doctor_avail()
    //{
    //    var Query = from item in db.tbl_hos_doc_availables where item.hd_id == Session["doctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() select item;
    //    GridView1.DataSource = Query;
    //    GridView1.DataBind();


    //    var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["doctor"].ToString() select item;
    //    foreach (var ss in Query1)
    //    {
    //        Label1.Text = ss.hd_name;
    //        Image1.ImageUrl = ss.hd_photo;
    //    }

    //}

    public void TodayAviablDoctrs()
    {

        var Query = from item in db.tbl_hos_doc_availables
                    where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == Session["doctor"].ToString()
                    orderby item.date ascending
                    select item;
        if (Query.Count() > 0)
        {
            DataList3.DataSource = Query;
            DataList3.DataBind();
            foreach (DataListItem dl3 in DataList3.Items)
            {
                Label lbl4 = dl3.FindControl("Label4") as Label;
                Label lbl5 = dl3.FindControl("Label5") as Label;
                Label Lbl6 = dl3.FindControl("Label6") as Label;




               // SqlCommand com = new SqlCommand("select date_id from tbl_hos_doc_available where date='"+Lbl6.Text+"'",con); ;
              //  int idd = Convert.ToInt32(com.ExecuteScalar());




                var Query2 = from item in db.tbl_hdoctors where item.hd_email == lbl4.Text select item;
                foreach (var n in Query2) { lbl5.Text = n.hd_name; }


                DataList dl4 = dl3.FindControl("DataList4") as DataList;
                var Query1 = from item in db.view_hos_doc_available_times
                             where item.h_hakkimid == Session["hakkeemid_h"].ToString() && item.date==Lbl6.Text
                             orderby item.date ascending
                             select item;
                List<string> lst = new List<string>();
                string time = "";
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("time"), new DataColumn("date") });
                //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("time") });
                foreach (var tt in Query1)
                {
                    //string[] a = tt.hd_a_time.Split(',');
                    //for (int i = 0; i < a.Count(); i++)
                    //{try
                    try
                    {
                        dt.Rows.Add(tt.time.ToString(), Lbl6.Text);
                    }
                    catch (Exception ex)
                    { }
                    //}

                }

                DataView view = new DataView(dt);
                DataTable dvalue = view.ToTable(true, "time", "date");
                dl4.DataSource = dvalue;
                dl4.DataBind();


                foreach (DataListItem dl in dl4.Items)
                {
                    string date = DateTime.Parse(Lbl6.Text).ToString("yyyy-MM-dd");
                    Button button2 = dl.FindControl("Button2") as Button;
                    String timet = "";
                    string[] t = new string[7];
                    string ab1 = "", ab2 = "";
                    try
                    {

                        int l = button2.Text.Count();
                        ab1 = button2.Text.Substring(0, l - 2);
                        ab2 = button2.Text.Substring(l - 2, 2);
                        timet = ab1.ToString() + " " + ab2.ToString();

                    }
                    catch (Exception ex)
                    {

                    }





                    var selctAvl = from avl in db.tbl_hos_doc_appmnts
                                   where avl.a_date == date && avl.a_time == timet.ToString() && avl.d_id == Session["doctor"].ToString() && avl.h_id == Session["hakkeemid_h"].ToString()
                                   select avl;
                    if (selctAvl.Count() > 0)
                    {
                        foreach (var tt in selctAvl)
                        {
                            if (tt.a_status == 0)
                            { button2.BackColor = System.Drawing.Color.Orange;
                                button2.Enabled = false;
                                button2.ToolTip = "Waiting for confirmation"; }
                            if (tt.a_status == 1)
                            { button2.BackColor = System.Drawing.Color.IndianRed;
                                button2.Enabled = false;
                                button2.ToolTip = "Booked"; }
                        }
                    }
                    else
                    {
                        //button2.BackColor = System.Drawing.Color.Green;
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            button2.ToolTip = "Available";
                        //}
                        //else
                        //{
                        //    button2.ToolTip = "متاح";
                        //}
                    }

                }

            }

        }
        else
        {
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["AvailableDate"] = TextBox1.Text;
        RdbAvlTimes.Items.Clear();
        string date = DateTime.Parse(TextBox1.Text).ToString("yyyy-MM-dd");

        var selectDate = from item in db.view_hos_doc_available_times
                         where item.hd_email == Session["doctor"].ToString() && item.date == date && item.h_hakkimid == Session["hakkeemid_h"].ToString()
                         select item;
        if (selectDate.Count() > 0)
        {
            foreach (var dt in selectDate)
            {
                //times = (dt.hd_a_time.ToString()).Split(',').ToList());
                times.Add(dt.time.ToString());

            }

            foreach (string ss in times)
            {
                RdbAvlTimes.Items.Add(ss);
                for (int i = 0; i < RdbAvlTimes.Items.Count; i++)
                {
                    var selectTime = from a in db.tbl_hos_doc_appmnts
                                     where a.h_id == Session["hakkeemid_h"].ToString() && a.d_id == Session["doctor"].ToString() && a.a_date == DateTime.Parse(TextBox1.Text).ToString("yyyy-MM-dd") && a.a_time == RdbAvlTimes.Items[i].Text && a.a_status == 0
                                     select a;
                    if (selectTime.Count() > 0)
                    {
                        RdbAvlTimes.Items[i].Enabled = false;
                        //RdbAvlTimes.ToolTip = "Booked";

                    }
                }
            }
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor is not available in this date')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الطبيب غير متوفر في هذا التاريخ')</Script>");
            //}
            //Label2.Text = "Doctor is not available in this date";
            //this.ModalPopupExtender1.Show();
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void RdbAvlTimes_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtApntmtDate.Text = Session["AvailableDate"].ToString();
        String timet = "";
        string[] t = new string[7];
        string ab1 = "", ab2 = "";
        try
        {
           
            int l = RdbAvlTimes.SelectedItem.Text.Count();
            ab1 = RdbAvlTimes.SelectedItem.Text.Substring(0, l - 2);
            ab2 = RdbAvlTimes.SelectedItem.Text.Substring(l - 2, 2);
            timet = ab1.ToString() + " " + ab2.ToString();

        }
        catch (Exception ex)
        {

        }
        TxtApointmentTime.Text = timet;
        BtnTakeAppointment.Enabled = true;
        DdlPayments.Enabled = true;
        TxtReasonToVisit.Enabled = true;
        TxtBookDocUserId.Enabled = true;

    }
    protected void BtnTakeAppointment_Click(object sender, EventArgs e)
    {
        string usermsg = "";
        string docmsg = "";
        var selct = from a in db.tbl_hos_doc_appmnts
                    where a.a_date == TxtApntmtDate.Text && a.a_time == TxtApointmentTime.Text && a.d_id == Session["doctor"].ToString() && a.h_id == Session["hakkeemid_h"].ToString()
                    select a;
        if (selct.Count() > 0)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Already have an appointment')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لديك بالفعل موعد')</Script>");
            //}
        }
       
            //Label2.Text = "Already have an appointment";
            //this.ModalPopupExtender1.Show();
        
        else
        {


            var selct1 = from a in db.tbl_hos_doc_appmnts
                         where a.a_date == TxtApntmtDate.Text && a.a_time == TxtApointmentTime.Text && a.d_id == Session["doctor"].ToString() && a.h_id == Session["hakkeemid_h"].ToString()
                         select a;
            if (selct1.Count() > 0)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You were already took an appointment on this day')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('كنت قد اتخذت بالفعل موعد في هذا اليوم')</Script>");
                //}
            }

            else
            {

                var selectUser = from item in db.tbl_signups
                                 where (item.email == TxtBookDocUserId.Text || item.u_hakkimid == TxtBookDocUserId.Text)
                                 select item;
                if (selectUser.Count() > 0)
                {
                    foreach (var ss in selectUser)
                    {
                        tbl_hos_doc_appmnt tbl = new tbl_hos_doc_appmnt
                        {
                            a_time = TxtApointmentTime.Text,
                            a_payment = DdlPayments.SelectedItem.Text,
                            a_date = TxtApntmtDate.Text,
                            a_reason = TxtReasonToVisit.Text,
                            a_status = 0,
                            d_id = Session["doctor"].ToString(),
                            h_id = Session["hakkeemid_h"].ToString(),
                            u_id = ss.u_hakkimid,
                        };
                        db.tbl_hos_doc_appmnts.InsertOnSubmit(tbl);
                        db.SubmitChanges();
                    }
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + TxtApntmtDate.Text + "'", con);
                    sda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Session["Day"] = dt1.Rows[0]["dayname"].ToString();
                    }
                    string username = "";
                    string email = "";
                    string userph = "";
                    string docph = "";
                    SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_signup where u_hakkimid='" + TxtBookDocUserId.Text + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if(dt.Rows.Count>0)
                    {
                        username = dt.Rows[0]["name"].ToString();
                        email = obj.DecryptString(dt.Rows[0]["email"].ToString());
                        userph = obj.DecryptString(dt.Rows[0]["contact"].ToString());
                    }
                    con.Close();

                    Email_To_Doctorappoinment(Session["doctor"].ToString(), username, TxtApntmtDate.Text, TxtApointmentTime.Text,Session["Day"].ToString());
                    //   docph = Session["Docph"].ToString();
                    SqlDataAdapter sda11 = new SqlDataAdapter("Select hd_contact from tbl_hdoctor where hd_email='"+ Session["doctor"].ToString() + "'", con);
                    DataSet dts = new DataSet();
                    dts.Clear();
                    sda11.Fill(dts);
                    docph = dts.Tables[0].Rows[0][0].ToString();


                     docmsg = "Your Appointment has been Cancelled with Patient " + username + " on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + " Thank you Hakkeem Team";
                    string ph = "+966" + docph;
                    ob.Message(ph, docmsg);

                    string ph1 = "+91" + docph;
                    ob.Message(ph1, docmsg);


                    Email_To_userappoinment(email, Label1.Text, TxtApntmtDate.Text,TxtApointmentTime.Text,Session["Day"].ToString());
                    usermsg = "Your Appointment has been Cancelled with Doctor " + Label1.Text + " on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + " Thank you Hakkeem Team";
                    string phu = "+966" + userph;
                    ob.Message(phu, usermsg);


                    string phu1 = "+91" + userph;
                    ob.Message(phu1, usermsg);

                    //if (Session["Language"].ToString() == "Auto")
                    //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Appointment set succesfully')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تعيين تعيين بنجاح')</Script>");
                    //}

                    BtnTakeAppointment.Enabled = false;
                    //Label2.Text = "Appointment set succesfully";

                    //this.ModalPopupExtender1.Show();
                    if (Session["AvailableDate"] != null)
                    {
                        TodayAviablDoctrs();
                    }
                    else
                    {
                        Session["doctor"] = Session["doctor"].ToString();

                        TodayAviablDoctrs();
                    }
                    TxtApntmtDate.Text = TxtApointmentTime.Text = TxtBookDocUserId.Text = "";
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('The Hakkeem user id doesn't exist. Please give correct Id.')</Script>");
                    //}
                    //else
                    //{
                    //    Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('معرف المستخدم هكيم غير موجود. يرجى إعطاء الرقم التعريفي الصحيح')</Script>");

                    //}
                    //Label2.Text = "The Hakkeem user id doesn't exist. Please give correct Id.";

                    //this.ModalPopupExtender1.Show();
                } 
            }
        }

    }
    protected void DataList4_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Appointment")
        {
            Button Button2 = e.Item.FindControl("Button2") as Button;
            TxtApntmtDate.Text = DateTime.Parse(e.CommandArgument.ToString()).ToString("yyyy-MM-dd");
            String timet = "";
            string[] t = new string[7];
            string ab1 = "", ab2 = "";
            try
            {

                int l = Button2.Text.Count();
                ab1 = Button2.Text.Substring(0, l - 2);
                ab2 = Button2.Text.Substring(l - 2, 2);
                timet = ab1.ToString() + " " + ab2.ToString();

            }
            catch (Exception ex)
            {

            }

            TxtApointmentTime.Text = timet;
            BtnTakeAppointment.Enabled = true;
            DdlPayments.Enabled = true;
            TxtReasonToVisit.Enabled = true;
            TxtBookDocUserId.Enabled = true;
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

            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td  style='color:#4aa9af;font-weight:bold;'><img src='" + sthetho + "' ></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + username + "</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + timepath + "'></td><td style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + time + " " +day+"</td></tr></tbody></table></td></tr>";
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