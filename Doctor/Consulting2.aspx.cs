using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctor_Consulting : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();

    protected override void InitializeCulture()
    {
        Session["Language"] = "";
        string culture = "";
        try
        {
            culture = Request.QueryString["l"].ToString();
            Session["Language"] = culture;
        }
        catch (Exception ex)
        { }
        // string culture = Session["Language"].ToString();
        if (string.IsNullOrEmpty(culture))
        {
            culture = "Auto";
            Session["Language"] = culture;
        }
        //Use this
        UICulture = culture;
        Culture = culture;
        //OR This
        if (culture != "Auto")
        {

            System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
            System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
        }
        else
        {
            //LinkButton1.Text = "عربى";
        }

        base.InitializeCulture();
    }

    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Session["Language"].ToString() == "Auto")
        {

        }
        else
        {
            this.MasterPageFile = "~/Doctor/ArabicMasterPage.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            CheckLocation();
            patient_details();
        }
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

    public void patient_details()
    {
        var Query = from item in db.tbl_doctor_appointments where item.id == int.Parse(Session["cnsltid"].ToString()) select item;
        foreach (var ss in Query)
        {
            var Query1 = from item in db.tbl_signups where item.u_hakkimid == ss.c_id select item;
            DetailsView1.DataSource = Query1;
            DetailsView1.DataBind();

            TextBox1.Text = ss.a_time;
            TextBox2.Text = ss.a_reason;
        }

        Label lbl5 = DetailsView1.FindControl("Label5") as Label;
        Label lbl6 = DetailsView1.FindControl("Label6") as Label;
        lbl5.Visible = false;
        lbl6.Text = Age(DateTime.Parse(lbl5.Text));
    }

    public static string Age(DateTime birthday)
    {
        DateTime now = DateTime.Today;
        int age = now.Year - birthday.Year;
        if (now < birthday.AddYears(age)) age--;

        return age.ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_doctor_appointments where item.id == int.Parse(Session["cnsltid"].ToString()) select item;
        foreach (var ss in Query)
        {
            tbl_hos_appmnt_history th = new tbl_hos_appmnt_history()
            {
                u_id = ss.c_id,
                d_id = ss.d_id,
                a_time = ss.a_time,
                a_doc_daignose = TextBox3.Text,
                a_doc_prescriptions = TextBox4.Text,
                a_date = DateTime.Parse(ss.a_date).Date,
                a_payment = ss.a_payment,
                a_reason = ss.a_reason,
                status = 0,
                a_end_time = DateTime.Now.ToString("hh:mm tt"),
            };
            db.tbl_hos_appmnt_histories.InsertOnSubmit(th);
            db.SubmitChanges();

            //delete shared records and history for this appointment
            var query = from item in db.tbl_share_histories
                        where item.share_d_id == Session["hakkeemid_d"].ToString() && item.share_h_id == null && item.appointment_id == ss.id
                        select item;

            db.tbl_share_histories.DeleteAllOnSubmit(query);

            var queryR = from item in db.tbl_share_records
                         where item.share_d_id == Session["hakkeemid_d"].ToString() && item.share_h_id == null && item.appointment_id == ss.id
                         select item;
            db.tbl_share_records.DeleteAllOnSubmit(queryR);

            //

            //db.tbl_doctor_appointments.DeleteOnSubmit(ss);
            ss.a_status = 3;
            db.SubmitChanges();
            //RegisterStartupScript("", "<Script Language=JavaScript>swal('Consulting completed')</Script>");
            //Label1.Text = "Consulting completed";
            //this.ModalPopupExtender2.Show();
            if (Session["Language"].ToString() == "Auto")
            {
                lblModalBody.Text = "Consulting completed";
            }
            else
            {
                lblModalBody.Text = "اكتملت الاستشارات";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
            upModal1.Update();

        }
    }

    protected void DetailsView1_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName == "history")
        {
            patient_history();
            //ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
        if (e.CommandName == "TestReports")
        {
            try
            {
                var query = from item in db.tbl_share_records
                            where item.appointment_id == Convert.ToInt32(Session["cnsltid"].ToString()) && item.share_h_id == null
                            select item;
                if (query.Count() > 0)
                {
                    //Timer timer = Master.FindControl("Timer1") as Timer;
                    //timer.Enabled = true;
                    //Timer1.Enabled = true;
                    //this.ModalPopupExtender1.Enabled = false;
                    //Response.Redirect("~/HospitalDoctor/ViewPatientReports.aspx?PatientId=" + LblPatId.Text + "");
                    Response.Redirect("~/Doctor/ViewPatientReports.aspx?PatientId=" + e.CommandArgument.ToString() + "&AppointmentId=" + Session["cnsltid"].ToString() + "");
                }
                else
                {
                    if (Session["Language"].ToString() == "Auto")
                    {
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient is not shared any test reports with you.')</Script>");
                    }
                    else
                    {
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('هذا المريض لا يشارك أي تقارير الاختبار معك.')</Script>");

                    }
                    //Label3.Text = "This patient is not shared any test reports with you.";
                    //this.ModalPopupExtender3.Show();
                }
            }
            catch (Exception ex)
            {
            }
            
        }
    }

    public void patient_history()
    {
        Timer tm = new Timer();
        tm = Master.FindControl("Timer1") as Timer;
        tm.Enabled = false;

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });
    
        var selectHistory = from item in db.tbl_share_histories
                            where item.share_d_id == Session["hakkeemid_d"].ToString() && item.share_h_id == null
                            select item;
        if (selectHistory.Count() > 0)
        {
            foreach(var sss in selectHistory)
            {
            var Query = from item in db.tbl_doctor_appointments where item.id == int.Parse(Session["cnsltid"].ToString()) select item;
            foreach (var ss in Query)
            {
                var Query1 = from item in db.tbl_hos_appmnt_histories where item.u_id == ss.c_id && item.id == sss.history_id select item;

                foreach(var dd in Query1)
                {
                    string a_date =DateTime.Parse (dd.a_date.ToString()).ToString("yyyy-MM-dd");
                    dt.Rows.Add(a_date, dd.h_id, dd.d_id, dd.a_doc_daignose, dd.a_doc_prescriptions);
                }
            }
            }
            if (dt.Rows.Count > 0)
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
                foreach (GridViewRow gr in GridView2.Rows)
                {
                    Label lblhid = gr.FindControl("LblHosId1") as Label;
                    Label lbl1 = gr.FindControl("Label1") as Label;
                    Label lbl2 = gr.FindControl("Label2") as Label;

                    if (!string.IsNullOrWhiteSpace(lblhid.Text))
                    {
                        var Query2 = from item in db.tbl_hdoctors where item.hd_email == lbl2.Text select item;
                        foreach (var d in Query2) { lbl1.Text = d.hd_name; }
                    }
                    else
                    {
                        var Query2 = from item in db.tbl_doctors where item.d_email == lbl2.Text select item;
                        foreach (var d in Query2) { lbl1.Text = d.d_name; }
                    }
                }
            }
            else
            {
                if (Session["Language"].ToString() == "Auto")
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient has not any consulting history with Hakkeem.')</Script>");
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('هذا المريض ليس لديه أي تاريخ الاستشارات مع هكيم.')</Script>");

                }
                //Label3.Text = "This patient has not any consulting history with Hakkeem.";
                //this.ModalPopupExtender3.Show();
            }
        }
        else
        {
            if (Session["Language"].ToString() == "Auto")
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient is not shared any consulting history with you.')</Script>");
            }
            else
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('هذا المريض لا يشارك أي تاريخ الاستشارات معك.')</Script>");

            }
            //Label3.Text = "This patient is not shared any consulting history with you";
            //this.ModalPopupExtender3.Show();
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        patient_history();
        //ModalPopupExtender1.Show();
    }

    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Doctor/Today appointments.aspx");
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Doctor/Today appointments.aspx");
    }
}