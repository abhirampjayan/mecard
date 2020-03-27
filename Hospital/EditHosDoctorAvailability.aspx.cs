using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;


public partial class Hospital_EditHosDoctorAvailability : System.Web.UI.Page
{
    secure obj1 = new secure();
    SMS ob = new SMS();
    databaseDataContext db = new databaseDataContext();
    MailMessage Email = new MailMessage();

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
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
        if (Session["hakkeemid_h"] != null)
        {

            if (!IsPostBack)
            {
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
                SelectDoctors();
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

    public void SelectDoctors()
    {
        var Query = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() select item;
        DdlDoctors.DataSource = Query;
        DdlDoctors.DataValueField = "hd_email";
        DdlDoctors.DataTextField = "hd_name";
        DdlDoctors.DataBind();
        //if (Session["Language"].ToString() == "Auto")
        //{
            DdlDoctors.Items.Insert(0, "---Select doctor---");
        //}
        //else
        //{
        //    DdlDoctors.Items.Insert(0, "---حدد الطبيب---");
        //}
        //if (Session["selecteddoctor"] != null)
        //{
        //    var Query1 = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
        //    foreach(var name in Query1)
        //    DdlDoctors.Items.Insert(0, name);
        //}
    }
    protected void DdlDoctors_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DdlDoctors.SelectedIndex > 0)
        //{
        Session["selecteddoctor"] = DdlDoctors.SelectedItem.Value;
        SelectedDoctorAvailability();
        //}
    }

    private void SelectedDoctorAvailability()
    {

        if (TextBox1.Text == "")
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("h_id"), new DataColumn("hd_id"), new DataColumn("date"), new DataColumn("time") });
            string time = "";
            List<string> date = new List<string>();
            var Query = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() select new { item.id, item.date };
            foreach (var ss in Query)
            {
                var Query1 = from item in db.tbl_hos_doc_times where item.date_id == ss.id select item.time;
                foreach (var t in Query1)
                {
                    if (time == "")
                    {
                        int l = t.ToString().Count();
                        string ab1 = t.ToString().Substring(0, l - 2);
                        string ab2 = t.ToString().Substring(l - 2, 2);
                        string timet = ab1.ToString() + " " + ab2.ToString();
                        time = "<label class='btn btn-xs btn-primary'>" + timet + "</label>" + " ";
                    }
                    else
                    {
                        int l =t.ToString().Count();
                   string     ab1 = t.ToString().Substring(0, l - 2);
                        string ab2 = t.ToString().Substring(l - 2, 2);
                        string timet = ab1.ToString() + " " + ab2.ToString();

                        time += "<label class='btn btn-xs btn-primary'>" + " " + timet + "</label>" + " ";
                    }
                }
                dt.Rows.Add(Session["hakkeemid_h"].ToString(), Session["selecteddoctor"].ToString(), ss.date, time);
                time = "";

            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else if(TextBox1.Text!="")
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("h_id"), new DataColumn("hd_id"), new DataColumn("date"), new DataColumn("time") });
            string time = "";
            List<string> date = new List<string>();
            var Query = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == TextBox1.Text select new { item.id, item.date };
            foreach (var ss in Query)
            {
                var Query1 = from item in db.tbl_hos_doc_times where item.date_id == ss.id select item.time;
                foreach (var t in Query1)
                {
                    if (time == "")
                    {
                        time = "<label class='btn btn-xs btn-primary'>" + t + "</label>" + " ";
                    }
                    else
                    {
                        int l = t.ToString().Count();
                        string ab1 = t.ToString().Substring(0, l - 2);
                        string ab2 = t.ToString().Substring(l - 2, 2);
                        string timet = ab1.ToString() + " " + ab2.ToString();

                        time += "<label class='btn btn-xs btn-primary'>" + " " + timet + "</label>" + " ";
                    }
                }
                dt.Rows.Add(Session["hakkeemid_h"].ToString(), Session["selecteddoctor"].ToString(), ss.date, time);
                time = "";

            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else if(TextBox1.Text!=""&&TextBox2.Text!="")
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        SelectedDoctorAvailability();
        List<string> atime = new List<string>();
        string time = "";
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[1] { new DataColumn("time") });
        CheckBoxList cbl1 = GridView1.Rows[GridView1.EditIndex].FindControl("CheckBoxList1") as CheckBoxList;
        Label lbl5 = GridView1.Rows[GridView1.EditIndex].FindControl("Label5") as Label;
        LinkButton lnk2 = GridView1.Rows[GridView1.EditIndex].FindControl("LinkButton2") as LinkButton;

        var Query = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == lnk2.Text select item.id;
        foreach (var ss in Query)
        {
            var Query1 = from item in db.tbl_hos_doc_times where item.date_id == ss select item.time;
            foreach (var t in Query1)
            {
                atime.Add(t);
            }
        }

        //string[] a = lbl5.Text.Split(',');
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
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        SelectedDoctorAvailability();
    }
    public static List<string> t = new List<string>();
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string time = "";
        Label lbl6 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label6") as Label;
        Label lbl7 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label7") as Label;
        CheckBoxList chk1 = GridView1.Rows[e.RowIndex].Cells[1].FindControl("CheckBoxList1") as CheckBoxList;
        if (chk1.SelectedIndex != -1)
        {
            Session["lbl6"] = lbl6.Text;
            //Session["lbl7"] = lbl7.Text;
            for (int i = 0; i < chk1.Items.Count; i++)
            {
                if (chk1.Items[i].Selected)
                {
                    //if (time == "")
                    //{ time = chk1.Items[i].Text; }
                    //else { time += "," + chk1.Items[i].Text; }
                }
                else
                {
                    //string t = chk1.Items[i].Text;
                    t.Add(chk1.Items[i].Text);
                }



            }
            //Label1.Text = "<b>Do you want to change the availability..?</b>";
            //this.ModalPopupExtender2.Show();

            for (int i = 0; i < t.Count(); i++)
            {
                var Query = from item in db.tbl_hos_doc_appmnts where item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == Session["lbl6"].ToString() && item.a_time == t[i] && item.d_id == Session["selecteddoctor"].ToString() select item;
                if (Query.Count() > 0)
                {
                   
                    foreach (var ss in Query)
                    {
                        string name = "";
                        var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
                        foreach (var n in Query1)
                        { name = n.ToString(); }
                        string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
                        //Email.mail(ss.u_id, msg, "Appointment canceled");
                        var user = (from item in db.tbl_signups where item.u_hakkimid == ss.u_id select item).First();
                        Email.mail(user.email, msg, "Appointment canceled");
                        string s = obj1.DecryptString(user.contact);
                        ob.Message(s.Substring(1, s.Length), msg);
                        var Query2 = from item in db.tbl_hos_doc_appmnts where item.id == int.Parse(ss.id.ToString()) select item;
                        foreach (var dd in Query2)
                        {
                            db.tbl_hos_doc_appmnts.DeleteOnSubmit(dd);
                        }
                        db.SubmitChanges();
                    }
                }
                var Query4 = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == Session["lbl6"].ToString() select item.id;
                foreach (var d in Query4)
                {
                    var Query5 = from item in db.tbl_hos_doc_times where item.date_id == d && item.time == t[i] select item;
                    foreach (var dd in Query5)
                    {
                        db.tbl_hos_doc_times.DeleteOnSubmit(dd);
                    }
                    db.SubmitChanges();
                }
            }
            t.Clear();
            //SelectedDoctorAvailability();
            //Response.Redirect("~/Hospital/EditHosDoctorAvailability.aspx");
            db.SubmitChanges();
            GridView obj = new GridView();
            obj = this.GridView1;
            obj.EditIndex = -1;
            SelectedDoctorAvailability();





        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Checkbox doesnt null')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('مربع الاختيار لا فارغ')</Script>");
            //}
            //Label2.Text = "Checkbox doesn't null";
            //this.ModalPopupExtender1.Show();
        }
    }

    public static List<string> adate = new List<string>();
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbl8 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label8") as Label;
        LinkButton lnk5 = GridView1.Rows[e.RowIndex].Cells[1].FindControl("LinkButton5") as LinkButton;
        LinkButton lnk2 = GridView1.Rows[e.RowIndex].Cells[0].FindControl("LinkButton2") as LinkButton;
        Session["lbl8"] = lbl8.Text;
        Session["lnk5"] = lnk5.Text;
        Session["lnk2"] = lnk2.Text;
        string[] a = lnk5.Text.Split(',');
        for (int i = 0; i < a.Count(); i++)
        {
            //adate.Add(a[i].ToString());
            //Label3.Text = "Do you want to remove this available times..?";
            //ModalPopupExtender3.Show();
            var Query = from item in db.tbl_hos_doc_appmnts where item.d_id == DdlDoctors.SelectedValue.ToString() && item.a_date == lnk2.Text && item.a_time == a[i].ToString() select item;
            if (Query.Count() > 0)
            {

                foreach (var ss in Query)
                {
                    string name = "";
                    var Query1 = from item in db.tbl_hdoctors where item.hd_email == DdlDoctors.SelectedValue.ToString() select item.hd_name;
                    foreach (var n in Query1)
                    { name = n.ToString(); }
                    string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
                    //Email.mail(ss.u_id, msg, "Appointment canceled");
                    var user = (from item in db.tbl_signups where item.u_hakkimid == ss.u_id select item).First();
                    Email.mail(user.email, msg, "Appointment canceled");
                    string s = obj1.DecryptString(user.contact);
                    ob.Message(s.Substring(1, s.Length), msg);
                    db.tbl_hos_doc_appmnts.DeleteOnSubmit(ss);
                    db.SubmitChanges();
                }
            }
        }
        var Query2 = from item in db.tbl_hos_doc_availables where item.date == lnk2.Text && item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == DdlDoctors.SelectedItem.Value select item;
        foreach (var dd in Query2)
        {
            var Query3 = from item in db.tbl_hos_doc_times where item.date_id == dd.id select item;
            foreach (var ss in Query3)
            {
                db.tbl_hos_doc_times.DeleteOnSubmit(ss);
                db.SubmitChanges();
            }
            db.tbl_hos_doc_availables.DeleteOnSubmit(dd);

        }
        db.SubmitChanges();
        SelectedDoctorAvailability();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        SelectedDoctorAvailability();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //for new modal....
        //for (int i = 0; i < t.Count(); i++)
        //{
        //    var Query = from item in db.tbl_hos_doc_appmnts where item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == Session["lbl6"].ToString() && item.a_time == t[i] && item.d_id == Session["selecteddoctor"].ToString() select item;
        //    if (Query.Count() > 0)
        //    {
        //        foreach (var ss in Query)
        //        {
        //            string name = "";
        //            var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
        //            foreach (var n in Query1)
        //            { name = n.ToString(); }
        //            string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
        //            Email.mail(ss.u_id, msg, "Appointment canceled");
        //            var Query2 = from item in db.tbl_hos_doc_appmnts where item.id == int.Parse(ss.id.ToString()) select item;
        //            foreach (var dd in Query2)
        //            {
        //                db.tbl_hos_doc_appmnts.DeleteOnSubmit(dd);
        //            }
        //            db.SubmitChanges();
        //        }
        //    }
        //    var Query4 = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == Session["lbl6"].ToString() select item.id;
        //    foreach (var d in Query4)
        //    {
        //        var Query5 = from item in db.tbl_hos_doc_times where item.date_id == d && item.time == t[i] select item;
        //        foreach (var dd in Query5)
        //        {
        //            db.tbl_hos_doc_times.DeleteOnSubmit(dd);
        //        }
        //        db.SubmitChanges();
        //    }
        //}
        //t.Clear();
        ////SelectedDoctorAvailability();
        ////Response.Redirect("~/Hospital/EditHosDoctorAvailability.aspx");
        //db.SubmitChanges();
        //GridView obj = new GridView();
        //obj = this.GridView1;
        //obj.EditIndex = -1;
        //SelectedDoctorAvailability();

        //for new modal....




        //Response.Redirect("~/Hospital/EditHosDoctorAvailability.aspx");
        //
        //DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("h_id"), new DataColumn("hd_id"), new DataColumn("date"), new DataColumn("time") });
        //string time = "";
        //List<string> date = new List<string>();
        //var Query11 = from item in db.tbl_hos_doc_availables where item.hd_id == DdlDoctors.SelectedItem.Value && item.h_id == Session["hakkeemid_h"].ToString() select new { item.id, item.date };
        //foreach (var ss in Query11)
        //{
        //    var Query1 = from item in db.tbl_hos_doc_times where item.date_id == ss.id select item.time;
        //    foreach (var ttt in Query1)
        //    {
        //        if (time == "")
        //        {
        //            time = "<label class='btn btn-xs btn-success'>" + ttt + "</label>" + " ";
        //        }
        //        else
        //        {
        //            time += "<label class='btn btn-xs btn-success'>" + " " + ttt + "</label>" + " ";
        //        }
        //    }
        //    dt.Rows.Add(Session["hakkeemid_h"].ToString(), DdlDoctors.SelectedItem.Value, ss.date, time);
        //    time = "";

        //}
        //GridView1.DataSource = dt;
        //GridView1.DataBind();

        //




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Hospital/EditHosDoctorAvailability.aspx");
    }

    protected void Button6_Click(object sender, EventArgs e)
    {

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        //for (int i = 0; i < adate.Count(); i++)
        //{
        //    var Query = from item in db.tbl_hos_doc_appmnts where item.d_id == Session["selecteddoctor"].ToString() && item.a_date == Session["lnk2"].ToString() && item.a_time == adate[i].ToString() select item;
        //    if (Query.Count() > 0)
        //    {

        //        foreach (var ss in Query)
        //        {
        //            string name = "";
        //            var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
        //            foreach (var n in Query1)
        //            { name = n.ToString(); }
        //            string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
        //            Email.mail(ss.u_id, msg, "Appointment canceled");

        //            db.tbl_hos_doc_appmnts.DeleteOnSubmit(ss);
        //            db.SubmitChanges();
        //        }
        //    }
        //}
        //var Query2 = from item in db.tbl_hos_doc_availables where item.date == Session["lnk2"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == Session["selecteddoctor"].ToString() select item;
        //foreach (var dd in Query2)
        //{
        //    var Query3 = from item in db.tbl_hos_doc_times where item.date_id == dd.id select item;
        //    foreach (var ss in Query3)
        //    {
        //        db.tbl_hos_doc_times.DeleteOnSubmit(ss);
        //        db.SubmitChanges();
        //    }
        //    db.tbl_hos_doc_availables.DeleteOnSubmit(dd);

        //}
        //adate.Clear();
        //db.SubmitChanges();
        //SelectedDoctorAvailability();
        //


        //
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("h_id"), new DataColumn("hd_id"), new DataColumn("date"), new DataColumn("time") });
        string time = "";
        List<string> date = new List<string>();
        try
        {
            var Query = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == TextBox1.Text select new { item.id, item.date };
            foreach (var ss in Query)
            {
                var Query1 = from item in db.tbl_hos_doc_times where item.date_id == ss.id select item.time;
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
                dt.Rows.Add(Session["hakkeemid_h"].ToString(), Session["selecteddoctor"].ToString(), ss.date, time);
                time = "";

            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctors not available')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الأطباء غير متوفرين')</Script>");

            //}
        }
    }
    public static List<string> removedate = new List<string>();
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        try
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
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("h_id"), new DataColumn("hd_id"), new DataColumn("date"), new DataColumn("time") });
            string time = "";

            //where d.RecordDateTime >= StartDate && d.RecordDateTime <= EndDate
            foreach (var dd in date)
            {
                var Query = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == dd select new { item.id, item.date };
                foreach (var ss in Query)
                {
                    var Query1 = from item in db.tbl_hos_doc_times where item.date_id == ss.id select item.time;
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
                    dt.Rows.Add(Session["hakkeemid_h"].ToString(), Session["selecteddoctor"].ToString(), dd, time);
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
                Panel2.Visible = false;
                Panel1.Visible = true;
                //Label2.Text = "You have no availability in given date range";
                //this.ModalPopupExtender1.Show();
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
        catch(Exception ex)
        {

        }
    }

  

    //remove selected availability
    protected void Button7_Click(object sender, EventArgs e)
    {
        //foreach (var d in removedate)
        //{
        //    var Query11 = from item in db.tbl_hos_doc_availables where item.date == d && item.hd_id == Session["selecteddoctor"].ToString() select item.id;
        //    foreach (var i in Query11)
        //    {
        //        var Query22 = from item in db.tbl_hos_doc_times where item.date_id == i select item.time;
        //        foreach (var at in Query22)
        //        {

        //            var Query = from item in db.tbl_hos_doc_appmnts where item.d_id == Session["selecteddoctor"].ToString() && item.a_date == d && item.a_time == at select item;
        //            if (Query.Count() > 0)
        //            {

        //                foreach (var ss in Query)
        //                {
        //                    string name = "";
        //                    var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
        //                    foreach (var n in Query1)
        //                    { name = n.ToString(); }
        //                    string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
        //                    Email.mail(ss.u_id, msg, "Appointment canceled");

        //                    db.tbl_hos_doc_appmnts.DeleteOnSubmit(ss);
        //                    db.SubmitChanges();
        //                }
        //            }
        //        }
        //    }


        //    var Query2 = from item in db.tbl_hos_doc_availables where item.date == d && item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == Session["selecteddoctor"].ToString() select item;
        //    foreach (var dd in Query2)
        //    {
        //        var Query3 = from item in db.tbl_hos_doc_times where item.date_id == dd.id select item;
        //        foreach (var ss in Query3)
        //        {
        //            db.tbl_hos_doc_times.DeleteOnSubmit(ss);
        //            db.SubmitChanges();
        //        }
        //        db.tbl_hos_doc_availables.DeleteOnSubmit(dd);
        //        db.SubmitChanges();
        //    }
        //    //adate.Clear();
        //    //db.SubmitChanges();
            


        //}
        //removedate.Clear();
        //db.SubmitChanges();
        //SelectedDoctorAvailability();
        //Panel1.Visible = true;
        //Panel2.Visible = false;
        ////Response.Redirect("~/Hospital/EditHosDoctorAvailability.aspx");
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Panel2.Visible = false;
        SelectedDoctorAvailability();
    }


    // Change time
    //protected void Button9_Click(object sender, EventArgs e)
    //{
        
    //}

    protected void Button10_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditHosDoctorAvailability.aspx");
    }

    //current date popup
    protected void Button12_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        Panel1.Visible = true;
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        //if (CheckBoxList2.SelectedIndex != -1)
        //{
        //    foreach (var dd in removedate)
        //    {

        //        var Query11 = from item in db.tbl_hos_doc_availables where item.date == dd && item.hd_id == Session["selecteddoctor"].ToString() select item.id;
        //        foreach (var i in Query11)
        //        {
        //            var Query22 = from item in db.tbl_hos_doc_times where item.date_id == i select item.time;
        //            foreach (var at in Query22)
        //            {

        //                var Query = from item in db.tbl_hos_doc_appmnts where item.d_id == Session["selecteddoctor"].ToString() && item.a_date == dd && item.a_time == at select item;
        //                if (Query.Count() > 0)
        //                {

        //                    foreach (var ss in Query)
        //                    {
        //                        string name = "";
        //                        var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
        //                        foreach (var n in Query1)
        //                        { name = n.ToString(); }
        //                        string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
        //                        Email.mail(ss.u_id, msg, "Appointment canceled");

        //                        db.tbl_hos_doc_appmnts.DeleteOnSubmit(ss);
        //                        db.SubmitChanges();
        //                    }
        //                }
        //            }
        //        }


        //        var Query2 = from item in db.tbl_hos_doc_availables where item.date == dd && item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == Session["selecteddoctor"].ToString() select item;
        //        foreach (var ddd in Query2)
        //        {
        //            var Query3 = from item in db.tbl_hos_doc_times where item.date_id == ddd.id select item;
        //            foreach (var ss in Query3)
        //            {
        //                db.tbl_hos_doc_times.DeleteOnSubmit(ss);
        //                db.SubmitChanges();
        //            }
        //            db.tbl_hos_doc_availables.DeleteOnSubmit(ddd);
        //            db.SubmitChanges();
        //        }



        //    }

        //    foreach (var ss in removedate)
        //    {
        //        tbl_hos_doc_available da = new tbl_hos_doc_available()
        //        {
        //            hd_id = Session["selecteddoctor"].ToString(),
        //            h_id = Session["hakkeemid_h"].ToString(),
        //            date =ss,
        //            //hd_a_time=time,
        //            status = 0,
        //        };
        //        db.tbl_hos_doc_availables.InsertOnSubmit(da);
        //        db.SubmitChanges();
        //        var QueryDate = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() select item.id;
        //        int max = Convert.ToInt32(QueryDate.Max());
        //        int i = 0;
        //        while (i < CheckBoxList2.Items.Count)
        //        {

        //            if (CheckBoxList2.Items[i].Selected)
        //            {
        //                tbl_hos_doc_time dt = new tbl_hos_doc_time()
        //                {
        //                    date_id = max,
        //                    time = CheckBoxList2.Items[i].Text,
        //                };
        //                db.tbl_hos_doc_times.InsertOnSubmit(dt);
        //                db.SubmitChanges();
        //            }

        //            i++;
        //        }
        //    }
        //    removedate.Clear();
        //    //Label9.Text = "Successfully changed the available time in selected date";
        //    //this.ModalPopupExtender8.Show();
        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully changed the available time in selected date')</Script>");

        //}
        //else
        //{
        //    //Label11.Text = "Please select at least one time";
        //    //this.ModalPopupExtender9.Show();
        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select at least one time')</Script>");

        //}
    }

    protected void Button15_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Panel2.Visible = false;
    }

    //protected void Button16_Click(object sender, EventArgs e)
    //{
    //    this.ModalPopupExtender9.Hide();
    //    this.ModalPopupExtender7.Show();
    //}

    protected void RemoveAll_Click(object sender, EventArgs e)
    {
        //Label4.Text = "Do you want remove the selected date availability..?";
        //ModalPopupExtender5.Show();

        foreach (var d in removedate)
        {
            var Query11 = from item in db.tbl_hos_doc_availables where item.date == d && item.hd_id == Session["selecteddoctor"].ToString() select item.id;
            foreach (var i in Query11)
            {
                var Query22 = from item in db.tbl_hos_doc_times where item.date_id == i select item.time;
                foreach (var at in Query22)
                {

                    var Query = from item in db.tbl_hos_doc_appmnts where item.d_id == Session["selecteddoctor"].ToString() && item.a_date == d && item.a_time == at select item;
                    if (Query.Count() > 0)
                    {

                        foreach (var ss in Query)
                        {
                            string name = "";
                            var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
                            foreach (var n in Query1)
                            { name = n.ToString(); }
                            string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
                            //Email.mail(ss.u_id, msg, "Appointment canceled");

                            var user = (from item in db.tbl_signups where item.u_hakkimid == ss.u_id select item).First();
                            Email.mail(user.email, msg, "Appointment canceled");
                            string s = obj1.DecryptString(user.contact);
                            ob.Message(s.Substring(1, s.Length), msg);

                            db.tbl_hos_doc_appmnts.DeleteOnSubmit(ss);
                            db.SubmitChanges();
                        }
                    }
                }
            }


            var Query2 = from item in db.tbl_hos_doc_availables where item.date == d && item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == Session["selecteddoctor"].ToString() select item;
            foreach (var dd in Query2)
            {
                var Query3 = from item in db.tbl_hos_doc_times where item.date_id == dd.id select item;
                foreach (var ss in Query3)
                {
                    db.tbl_hos_doc_times.DeleteOnSubmit(ss);
                    db.SubmitChanges();
                }
                db.tbl_hos_doc_availables.DeleteOnSubmit(dd);
                db.SubmitChanges();
            }
            //adate.Clear();
            //db.SubmitChanges();



        }
        removedate.Clear();
        db.SubmitChanges();
        
        Panel1.Visible = true;
        Panel2.Visible = false;
        SelectedDoctorAvailability();
        //Response.Redirect("~/Hospital/EditHosDoctorAvailability.aspx");
    }

    protected void ChangeTime_Click(object sender, EventArgs e)
    {
        DateTime d1 = DateTime.Parse(DateTime.Now.ToShortDateString());
        DateTime d2 = DateTime.Parse(TextBox1.Text);
        DateTime tdate = DateTime.Parse(TextBox1.Text);
        if (d1 == d2)
        {
            //Label7.Text = "You can't choose the current date in selected date, if you want edit current date availability please edit in separately";
            //this.ModalPopupExtender6.Show();
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
                //Label2.Text = "<b>Alert!</b>   <p>You selected time is less than the starting time.</p>";
                //Label2.ForeColor = System.Drawing.Color.Red;
                //this.ModalPopupExtender1.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You selected time is less than the starting time.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد اخترت الوقت أقل من وقت البدء')</Script>");

                //}
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


                    //Label10.Text = "Do you want change available time..?";
                    //this.ModalPopupExtender7.Show();

                    if (CheckBoxList2.SelectedIndex != -1)
                    {
                        foreach (var dd in removedate)
                        {

                            var Query11 = from item in db.tbl_hos_doc_availables where item.date == dd && item.hd_id == Session["selecteddoctor"].ToString() select item.id;
                            foreach (var i in Query11)
                            {
                                var Query22 = from item in db.tbl_hos_doc_times where item.date_id == i select item.time;
                                foreach (var at in Query22)
                                {

                                    var Query = from item in db.tbl_hos_doc_appmnts where item.d_id == Session["selecteddoctor"].ToString() && item.a_date == dd && item.a_time == at select item;
                                    if (Query.Count() > 0)
                                    {

                                        foreach (var ss in Query)
                                        {
                                            string name = "";
                                            var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
                                            foreach (var n in Query1)
                                            { name = n.ToString(); }
                                            string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " unavailable, please take another appointment";
                                            //Email.mail(ss.u_id, msg, "Appointment canceled");

                                            var user = (from item in db.tbl_signups where item.u_hakkimid == ss.u_id select item).First();
                                            Email.mail(user.email, msg, "Appointment canceled");
                                            string s = obj1.DecryptString(user.contact);
                                            ob.Message(s.Substring(1, s.Length), msg);

                                            db.tbl_hos_doc_appmnts.DeleteOnSubmit(ss);
                                            db.SubmitChanges();
                                        }
                                    }
                                }
                            }


                            var Query2 = from item in db.tbl_hos_doc_availables where item.date == dd && item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == Session["selecteddoctor"].ToString() select item;
                            foreach (var ddd in Query2)
                            {
                                var Query3 = from item in db.tbl_hos_doc_times where item.date_id == ddd.id select item;
                                foreach (var ss in Query3)
                                {
                                    db.tbl_hos_doc_times.DeleteOnSubmit(ss);
                                    db.SubmitChanges();
                                }
                                db.tbl_hos_doc_availables.DeleteOnSubmit(ddd);
                                db.SubmitChanges();
                            }



                        }

                        foreach (var ss in removedate)
                        {
                            tbl_hos_doc_available da = new tbl_hos_doc_available()
                            {
                                hd_id = Session["selecteddoctor"].ToString(),
                                h_id = Session["hakkeemid_h"].ToString(),
                                date = ss,
                                //hd_a_time=time,
                                status = 0,
                            };
                            db.tbl_hos_doc_availables.InsertOnSubmit(da);
                            db.SubmitChanges();
                            var QueryDate = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() select item.id;
                            int max = Convert.ToInt32(QueryDate.Max());
                            int i = 0;
                            while (i < CheckBoxList2.Items.Count)
                            {

                                if (CheckBoxList2.Items[i].Selected)
                                {
                                    tbl_hos_doc_time dtt = new tbl_hos_doc_time()
                                    {
                                        date_id = max,
                                        time = CheckBoxList2.Items[i].Text,
                                    };
                                    db.tbl_hos_doc_times.InsertOnSubmit(dtt);
                                    db.SubmitChanges();
                                }

                                i++;
                            }
                        }
                        removedate.Clear();
                        //Label9.Text = "Successfully changed the available time in selected date";
                        //this.ModalPopupExtender8.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully changed the available time in selected date')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير الوقت المتاح بنجاح في التاريخ المحدد بنجاح')</Script>");

                        //}
                        Response.Redirect("edithosdoctoravailability.aspx");
                    }
                    else
                    {
                        //Label11.Text = "Please select at least one time";
                        //this.ModalPopupExtender9.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select at least one time')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد مرة واحدة على الأقل')</Script>");

                        //}

                    }



                }
                else
                {
                    //Label2.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                    //Label2.ForeColor = System.Drawing.Color.Red;
                    //this.ModalPopupExtender1.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد')</Script>");

                    //}
                }

            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="engage")
        {
            t.Clear();
            GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

            int RowIndex = gvr.RowIndex;
            string time = "";
            Label lbl6 = GridView1.Rows[RowIndex].Cells[2].FindControl("Label6") as Label;
            Label lbl7 = GridView1.Rows[RowIndex].Cells[2].FindControl("Label7") as Label;
            CheckBoxList chk1 = GridView1.Rows[RowIndex].Cells[1].FindControl("CheckBoxList1") as CheckBoxList;
            if (chk1.SelectedIndex != -1)
            {
                Session["lbl6"] = lbl6.Text;
                //Session["lbl7"] = lbl7.Text;
                for (int i = 0; i < chk1.Items.Count; i++)
                {
                    if (chk1.Items[i].Selected)
                    {
                        //if (time == "")
                        //{ time = chk1.Items[i].Text; }
                        //else { time += "," + chk1.Items[i].Text; }
                    }
                    else
                    {
                        //string t = chk1.Items[i].Text;
                        t.Add(chk1.Items[i].Text);
                    }



                }
                //Label1.Text = "<b>Do you want to change the availability..?</b>";
                //this.ModalPopupExtender2.Show();

                for (int i = 0; i < t.Count(); i++)
                {
                    int l = t[i].ToString().Length - 2;
                    t[i] = t[i].ToString().Insert(l, " ");
                    var Query = from item in db.tbl_hos_doc_appmnts where item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == Session["lbl6"].ToString() && item.a_time == t[i] && item.d_id == Session["selecteddoctor"].ToString() select item;
                    if (Query.Count() > 0)
                    {

                        foreach (var ss in Query)
                        {
                            string name = "";
                            var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
                            foreach (var n in Query1)
                            { name = n.ToString(); }
                            string msg = "Dear patient, your booked appointment (" + ss.a_date + "/" + ss.a_time + ") is canceled because the Dr." + name + " engaged with emergency situation, please take another appointment. Thank you Hakkeem Team!";
                            //Email.mail(ss.u_id, msg, "Appointment canceled");
                            var user = (from item in db.tbl_signups where item.u_hakkimid == ss.u_id select item).First();
                            try
                            {
                                Email.mail(obj1.DecryptString(user.email), msg, "Appointment canceled");
                            }
                            catch (Exception ex) { }
                            string s = obj1.DecryptString(user.contact);
                            try
                            {
                                ob.Message(s.Substring(1, s.Length), msg);
                            }
                            catch (Exception ex) { }
                            var Query2 = from item in db.tbl_hos_doc_appmnts where item.id == int.Parse(ss.id.ToString()) select item;
                            foreach (var dd in Query2)
                            {
                                //db.tbl_hos_doc_appmnts.DeleteOnSubmit(dd);
                                dd.a_status = 4;
                            }
                            db.SubmitChanges();
                        }
                    }
                    //var Query4 = from item in db.tbl_hos_doc_availables where item.hd_id == Session["selecteddoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == Session["lbl6"].ToString() select item.id;
                    //foreach (var d in Query4)
                    //{
                    //    var Query5 = from item in db.tbl_hos_doc_times where item.date_id == d && item.time == t[i] select item;
                    //    foreach (var dd in Query5)
                    //    {
                    //        db.tbl_hos_doc_times.DeleteOnSubmit(dd);
                    //    }
                    //    db.SubmitChanges();
                    //}
                }
                t.Clear();
                //SelectedDoctorAvailability();
                //Response.Redirect("~/Hospital/EditHosDoctorAvailability.aspx");
                db.SubmitChanges();
                GridView obj = new GridView();
                obj = this.GridView1;
                obj.EditIndex = -1;
                SelectedDoctorAvailability();





            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Checkbox doesnt null')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('مربع الاختيار لا فارغ')</Script>");
                //}
                //Label2.Text = "Checkbox doesn't null";
                //this.ModalPopupExtender1.Show();
            }
        }
    }
}