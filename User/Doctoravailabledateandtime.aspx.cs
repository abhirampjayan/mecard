using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Hospital_Doctor_available_date_and_time : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();


    secure obj = new secure();
    List<string> times = new List<string>();

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
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
       
            
            if (!IsPostBack)
            {
                Session["docid"] =obj.DecryptString(Request.QueryString["docid"].ToString());
            Session["hos"] = obj.DecryptString(Request.QueryString["hos"].ToString());

            try
                {
                    CheckLocation();
                }
                catch (Exception ex)
                {
                    Response.Redirect("../index/hospita login.aspx");
                }
                TodayAviablDoctrs();
                SetDoctor();
            }
            else
            {
            }
       


    }

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_regno == Session["hos"].ToString()
                    select new { item1.h_id, item.latitude };

            if (query.Count() <= 0)
            {
            // Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
            //Label8.Text = "You must set your location";
            //ModalPopupExtender4.Show();
             RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location')</Script>");
        }
       
    }

    private void SetDoctor()
    {
        var selctDoc = from doc in db.tbl_hdoctors
                       where doc.hd_email == Session["docid"].ToString()
                       select doc;
        foreach (var ss in selctDoc)
        {
            Label1.Text = ss.hd_name;
            if(ss.hd_photo ==null)
            {
                Image1.ImageUrl = "~/User/mapicons/user .png";
            }
            else
            {
                Image1.ImageUrl = ss.hd_photo;
            }
          
        }
    }

    //public void Doctor_avail()
    //{
    //    var Query = from item in db.tbl_hos_doc_availables where item.hd_id == Session["docid"].ToString() && item.h_id == Session["hospital"].ToString() select item;
    //    GridView1.DataSource = Query;
    //    GridView1.DataBind();


    //    var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["docid"].ToString() select item;
    //    foreach (var ss in Query1)
    //    {
    //        Label1.Text = ss.hd_name;
    //        Image1.ImageUrl = ss.hd_photo;
    //    }

    //}

    public void TodayAviablDoctrs()
    {

        var Query = from item in db.tbl_hos_doc_availables
                    where item.h_id == Session["hos"].ToString() && item.hd_id == Session["docid"].ToString()
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
                var Query2 = from item in db.tbl_hdoctors where item.hd_email == lbl4.Text select item;
                foreach (var n in Query2) { lbl5.Text = n.hd_name; }


                DataList dl4 = dl3.FindControl("DataList4") as DataList;
                var Query1 = from item in db.view_hos_doc_available_times
                             where item.h_regno == Session["hos"].ToString()
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

                    var selctAvl = from avl in db.tbl_hos_doc_appmnts
                                   where avl.a_date == date && avl.a_time == button2.Text && avl.d_id == Session["docid"].ToString() && avl.h_id == Session["hos"].ToString()
                                   select avl;
                    if (selctAvl.Count() > 0)
                    {
                        foreach (var tt in selctAvl)
                        {
                            if (tt.a_status == 0)
                            { button2.BackColor = System.Drawing.Color.Orange; button2.Enabled = false; button2.ToolTip = "Waiting for confirmation"; }
                            if (tt.a_status == 1)
                            { button2.BackColor = System.Drawing.Color.IndianRed; button2.Enabled = false; button2.ToolTip = "Booked"; }
                        }
                    }
                    else
                    {
                        //button2.BackColor = System.Drawing.Color.Green;
                        button2.ToolTip = "Available";
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
                         where item.hd_email == Session["docid"].ToString() && item.date == date && item.h_regno == Session["hos"].ToString()
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
                                     where a.h_id == Session["hos"].ToString() && a.d_id == Session["docid"].ToString() && a.a_date == DateTime.Parse(TextBox1.Text).ToString("yyyy-MM-dd") && a.a_time == RdbAvlTimes.Items[i].Text && a.a_status == 0
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
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor is not available in this date')</Script>");
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
        TxtApointmentTime.Text = RdbAvlTimes.SelectedItem.Text;
        BtnTakeAppointment.Enabled = true;
        DdlPayments.Enabled = true;
        TxtReasonToVisit.Enabled = true;
        TxtBookDocUserId.Enabled = true;

    }
    protected void BtnTakeAppointment_Click(object sender, EventArgs e)
    {
        var selct = from a in db.tbl_hos_doc_appmnts
                    where a.a_date == TxtApntmtDate.Text && a.a_time == TxtApointmentTime.Text && a.d_id == Session["docid"].ToString() && a.h_id == Session["hos"].ToString()
                    select a;
        if (selct.Count() > 0)
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Already have an appointment')</Script>");
            //Label2.Text = "Already have an appointment";
            //this.ModalPopupExtender1.Show();
        }
        else
        {
            var selectUser = from item in db.tbl_signups
                             where (item.email == TxtBookDocUserId.Text || item.u_hakkimid==TxtBookDocUserId.Text)
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
                        d_id = Session["docid"].ToString(),
                        h_id = Session["hos"].ToString(),
                        u_id = ss.email,
                    };
                    db.tbl_hos_doc_appmnts.InsertOnSubmit(tbl);
                    db.SubmitChanges();
                }

               
                BtnTakeAppointment.Enabled = false;
                //Label2.Text = "Appointment set succesfully";

                //this.ModalPopupExtender1.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Appointment set succesfully')</Script>");
                if (Session["AvailableDate"] != null)
                {
                    TodayAviablDoctrs();
                }
                else
                {
                    Session["docid"] = Session["docid"].ToString();

                    TodayAviablDoctrs();
                }
                TxtApntmtDate.Text = TxtApointmentTime.Text = TxtBookDocUserId.Text = "";
            }
            else
            {
               
                //Label2.Text = "The Hakkeem user id doesn't exist. Please give correct Id.";

                //this.ModalPopupExtender1.Show();
                Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('The Hakkeem user id doesn't exist. Please give correct Id.')</Script>");
            }
        }

    }
    protected void DataList4_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Appointment")
        {
            Button Button2 = e.Item.FindControl("Button2") as Button;
            TxtApntmtDate.Text = DateTime.Parse(e.CommandArgument.ToString()).ToString("yyyy-MM-dd");
            TxtApointmentTime.Text = Button2.Text;
            BtnTakeAppointment.Enabled = true;
            DdlPayments.Enabled = true;
            TxtReasonToVisit.Enabled = true;
            TxtBookDocUserId.Enabled = true;
        }
    }

}