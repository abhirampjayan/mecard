using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Hospital_HospitalDoctorConsulting : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
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
        //    this.MasterPageFile = "~/HospitalDoctor/ArabicHospitalDoctorMaster.master";
        //}
    }
    string DocId = "";
    string HosId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HosDocId"] != null)
        {
            DocId = Session["HosDocId"].ToString();
            HosId = Session["HospitalId"].ToString();
            var selectDoc = from item in db.tbl_hdoctors
                            where item.hd_email == DocId && item.h_id == HosId
                            select item;
            foreach (var s in selectDoc)
            {
                if (s.hd_photo == null)
                {
                    Image1.ImageUrl = "../Doctorimages/doctor.png";
                   // Image1.ImageUrl = "/Clinic/User/mapicons/user.png";
                }
                else
                {
                    Image1.ImageUrl = s.hd_photo;
                }
                //Label1.Text = s.hd_name;
            }

        
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/HospitalDoctorLogin.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/HospitalDoctorLogin.aspx?l=ar-EG");
            //}
        }

        if (!IsPostBack)
        {
            LblCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TodayAppointments();
        }
    }

    public void TodayAppointments()
    {

        var Query = from item in db.tbl_hos_doc_appmnts
                    join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                    where item.a_date == LblCurrentDate.Text && item.d_id == DocId && item.h_id == HosId && item.a_status == 1
                    orderby item.a_time ascending
                    select new { item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name, item.u_id };
        if (Query.Count() > 0)
        {

            GridView1.DataSource = Query;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string Id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
        Label LblReason = GridView1.Rows[e.RowIndex].FindControl("LblReason") as Label;
        Label LblPatientName = GridView1.Rows[e.RowIndex].FindControl("LblPatientName") as Label;
        Label LblId = GridView1.Rows[e.RowIndex].FindControl("LblPatId") as Label;
        //Label lbl = GridView1.Rows[e.RowIndex].FindControl("") as Label;
        clear();
        LblApoId.Text = Id;
        LblPatId.Text = LblId.Text;
        TxtApointmentTime.Text = LblTime.Text;
        TxtPatientName.Text = LblPatientName.Text;
        TxtReasonToVisit.Text = LblReason.Text;
        TxtPrescription.Enabled = true;
        TxtDiagnose.Enabled = true;
        BtnComplete.Enabled = true;
        LnkPrevious.Enabled = true;
        LnkTestReports.Enabled = true;
        TodayAppointments();
        TxtDiagnose.Focus();
    }

    public void clear()
    {
        TxtDiagnose.Text = TxtPrescription.Text = "";
    }

    protected void BtnComplete_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_hos_doc_appmnts
                    where item.id == Convert.ToInt64(LblApoId.Text)
                    select item;
        if (Query.Count() > 0)
        {
            foreach (var ss in Query)
            {
                string date = DateTime.Parse(ss.a_date).ToString("yyyy-MM-dd");
                tbl_hos_appmnt_history tbAp = new tbl_hos_appmnt_history()
                {
                    a_date = DateTime.Parse(date).Date,
                    a_doc_daignose = TxtDiagnose.Text,
                    a_doc_prescriptions = TxtPrescription.Text,
                    a_payment = ss.a_payment.ToString(),
                    a_reason = ss.a_reason.ToString(),
                    a_time = ss.a_time.ToString(),
                    d_id = ss.d_id.ToString(),
                    h_id = ss.h_id.ToString(),
                    u_id = ss.u_id.ToString(),
                    status = 0,
                    a_end_time = DateTime.Now.ToString("hh:mm tt"),
                };
                db.tbl_hos_appmnt_histories.InsertOnSubmit(tbAp);
                db.SubmitChanges();


                // Delete all shared records and history to this doctor for this appointment

                var query = from item in db.tbl_share_histories
                            where item.share_d_id == DocId && item.share_h_id == HosId && item.appointment_id == ss.id
                            select item;
                foreach (var h in query) { h.share_status = 1; }db.SubmitChanges();
                //db.tbl_share_histories.DeleteAllOnSubmit(query);

                var queryR = from item in db.tbl_share_records
                             where item.share_d_id == DocId && item.share_h_id == HosId && item.appointment_id == ss.id
                             select item;
                foreach (var r in queryR) { r.share_status = 1; }db.SubmitChanges();
                //db.tbl_share_records.DeleteAllOnSubmit(queryR);

                // Delete this completed appointment from appoints table
                db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(Query);
                db.SubmitChanges();
            }

            BtnComplete.Enabled = false;
            LnkPrevious.Enabled = false;
            //Label1.Text = "Consulting completed. Thank you.";
            //this.ModalPopupExtender2.Show();
           
            TodayAppointments();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Consulting completed. Thank you.')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('اكتملت الاستشارات. شكرا لكم.')</Script>");

            //}
        }


    }

    protected void LnkPrevious_Click(object sender, EventArgs e)
    {
        PatientHistory();

    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        DeletePassedAppointments();
        TodayAppointments();

    }

    public void PatientHistory()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });
        try
        {
            var selectShareHistory = from item in db.tbl_share_histories
                                     where item.share_h_id == HosId && item.share_d_id == DocId && item.appointment_id == Convert.ToInt64(LblApoId.Text)
                                     select item;
            if (selectShareHistory.Count() > 0)
            {
                foreach (var ss in selectShareHistory)
                {
                    var query = from item in db.tbl_hos_appmnt_histories
                                where item.u_id == LblPatId.Text && item.id == ss.history_id
                                orderby item.id descending
                                select item;
                    foreach (var s in query)
                    {
                        dt.Rows.Add(s.a_date, s.h_id, s.d_id, s.a_doc_daignose, s.a_doc_prescriptions);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    foreach (GridViewRow grow in GridView2.Rows)
                    {
                        Label LblHosId1 = grow.FindControl("LblHosId1") as Label;
                        Label LblDocId = grow.FindControl("LblDocId") as Label;
                        Label LblDoctorName = grow.FindControl("LblDoctorName") as Label;
                        Label LblDate = grow.FindControl("LblDate") as Label;
                        Label LblSowDate = grow.FindControl("LblSowDate") as Label;

                        LblSowDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();
                        if (LblHosId1.Text == "")
                        {
                            var doc = (from item in db.tbl_doctors
                                       where item.d_hakkimid == LblDocId.Text
                                       select item).First();

                            LblDoctorName.Text = doc.d_name.ToString();
                        }
                        else
                        {
                            var doc = (from item in db.tbl_hdoctors
                                       where item.hd_email == LblDocId.Text && item.h_id == LblHosId1.Text
                                       select item).First();

                            LblDoctorName.Text = doc.hd_name.ToString();
                        }
                    }
                    Timer timer = Master.FindControl("Timer1") as Timer;
                    timer.Enabled = false;
                    Timer1.Enabled = false;
                    //this.ModalPopupExtender1.Show();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                else
                {
                    //Label1.Text = "This patient has not any consulting history with Hakkeem.";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient has not any consulting history with Hakkeem.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('هذا المريض ليس لديه أي تاريخ الاستشارات مع حكيم.')</Script>");

                    //}
                }
            }
            else
            {
                //Label1.Text = "This patient is not shared any consulting history with you";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient is not shared any consulting history with you.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('هذا المريض لا يشارك أي تاريخ الاستشارات معك.')</Script>");

                //}
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }

    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        PatientHistory();
        Timer timer = Master.FindControl("Timer1") as Timer;
        timer.Enabled = false;
        Timer1.Enabled = false;
        //this.ModalPopupExtender1.Show();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        //upModal.Update();
    }

    protected void LnkTestReports_Click(object sender, EventArgs e)
    {
        try
        {
            var query = from item in db.tbl_share_records
                        where item.appointment_id == Convert.ToInt32(LblApoId.Text) && item.share_h_id == HosId
                        select item;
            if (query.Count() > 0)
            {
                Timer timer = Master.FindControl("Timer1") as Timer;
                timer.Enabled = true;
                Timer1.Enabled = true;
                //this.ModalPopupExtender1.Enabled = false;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("~/HospitalDoctor/ViewPatientReports.aspx?PatientId=" + LblPatId.Text + "");
                //}
                //else
                //{
                //    Response.Redirect("~/HospitalDoctor/ViewPatientReports.aspx?l=ar-EG&PatientId=" + LblPatId.Text + "");
                //}
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient is not shared any test reports with you.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('هذا المريض لا يشارك أي تقارير الاختبار معك.')</Script>");

                //}
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void DeletePassedAppointments()
    {
        try
        {
            string currentTime = DateTime.Now.AddHours(-1).ToShortTimeString();
            var query = from item in db.tbl_hos_doc_appmnts
                        where item.h_id == Session["HospitalId"].ToString() && item.d_id == Session["HosDocId"].ToString() && item.a_date == DateTime.Now.ToString("yyyy-MM-dd")
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_time = DateTime.Parse(ss.a_time).ToShortTimeString();
                    DateTime curnTime = DateTime.Parse(currentTime);
                    if (DateTime.Parse(a_time) <= curnTime)
                    {
                        var query1 = from item in db.tbl_hos_doc_appmnts
                                     where item.id == ss.id
                                     select item;
                        db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(query1);
                    }
                    db.SubmitChanges();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}