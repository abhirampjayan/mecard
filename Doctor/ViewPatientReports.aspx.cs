using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Doctor_ViewPatientReports : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    secure obj = new secure();
    string DocId = "";
    string patientId = "";
    string appointmentId = "";

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
            if (Session["hakkeemid_d"] != null)
        {
            DocId = Session["hakkeemid_d"].ToString();
            patientId =Request.QueryString["PatientId"].ToString();
            appointmentId = Request.QueryString["AppointmentId"].ToString();
            
                var query = from item in db.tbl_signups
                            where item.u_hakkimid == patientId
                            select item;
                foreach (var ss in query)
                {
                    LblUserName.Text = ss.name;
                }
                CheckLocation();
                BindTestReports();
            }
            else
            {
                Response.Redirect("~/Index/Doctor Login.aspx");
            }
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
    public void BindTestReports()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("date"), new DataColumn("blood_test_report"), new DataColumn("urine_test_report"), new DataColumn("scan_test_report"), new DataColumn("xray_test_report"), new DataColumn("other_test_name"), new DataColumn("other_test_report") });

        try
        {
            var selectRecords = from item in db.tbl_share_records
                                where item.share_d_id == DocId && item.share_h_id == null && item.appointment_id==Convert.ToInt64(appointmentId)
                                select item;

            if (selectRecords.Count() > 0)
            {
                foreach (var ss in selectRecords)
                {
                    var query = from item in db.tbl_test_reports
                                where item.u_id == patientId && item.id == ss.record_id
                                orderby item.date descending
                                select item;
                    foreach (var s in query)
                    {
                        dt.Rows.Add(s.date, s.blood_test_report, s.urine_test_report, s.scan_test_report, s.xray_test_report, s.other_test_name, s.other_test_report);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    foreach (GridViewRow gRow in GridView1.Rows) 
                    {
                        HyperLink HlnkBloodTest = gRow.FindControl("HlnkBloodTest") as HyperLink;
                        HyperLink HlnkUrineTest = gRow.FindControl("HlnkUrineTest") as HyperLink;
                        HyperLink LnkScanReport = gRow.FindControl("LnkScanReport") as HyperLink;
                        HyperLink LnkXrayReport = gRow.FindControl("LnkXrayReport") as HyperLink;
                        HyperLink LnkOtherReport = gRow.FindControl("LnkOtherReport") as HyperLink;
                        Label LblOther = gRow.FindControl("LblOther") as Label;
                        Label LblDate = gRow.FindControl("LblDate") as Label;
                        Label LblDate1 = gRow.FindControl("LblDate1") as Label;

                        LblDate1.Text = DateTime.Parse(LblDate.Text).ToShortDateString();

                        if (HlnkBloodTest.NavigateUrl == "")
                        {
                            HlnkBloodTest.Enabled = false;
                            HlnkBloodTest.ToolTip = "No data";
                            HlnkBloodTest.Text = "No Record";
                            HlnkBloodTest.ForeColor = System.Drawing.Color.Red;
                        }
                        if (HlnkUrineTest.NavigateUrl == "")
                        {
                            HlnkUrineTest.Enabled = false;
                            HlnkUrineTest.ToolTip = "No data";
                            HlnkUrineTest.Text = "No Record";
                            HlnkUrineTest.ForeColor = System.Drawing.Color.Red;
                        }
                        if (LnkScanReport.NavigateUrl == "")
                        {
                            LnkScanReport.Enabled = false;
                            LnkScanReport.ToolTip = "No data";
                            LnkScanReport.Text = "No Record";
                            LnkScanReport.ForeColor = System.Drawing.Color.Red;
                        }
                        if (LnkXrayReport.NavigateUrl == "")
                        {
                            LnkXrayReport.Enabled = false;
                            LnkXrayReport.ToolTip = "No data";
                            LnkXrayReport.Text = "No Record";
                            LnkXrayReport.ForeColor = System.Drawing.Color.Red;
                        }
                        if (LnkOtherReport.NavigateUrl == "")
                        {
                            LnkOtherReport.Enabled = false;
                            LnkOtherReport.ToolTip = "No data";
                            LnkOtherReport.Text = "No Record";
                            LnkOtherReport.ForeColor = System.Drawing.Color.Red;
                            LblOther.Text = "";
                            LblOther.Visible = false;
                        }

                        //Timer timer = Master.FindControl("Timer1") as Timer;
                        //timer.Enabled = false;
                        //Timer1.Enabled = false;
                        //this.ModalPopupExtender2.Show();
                    }
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient haven't any shared reports.')</Script>");
                    //Label1.Text = "This patient haven't a nye shared reports.";
                    //this.ModalPopupExtender1.Show();

                }
            }
            else
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient is not shared any records with you.')</Script>");
                //Label2.Text = "This patient is not shared any records with you.";
                //this.ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindTestReports();
    }
    protected void BtnComplete_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Doctor/Consulting.aspx");
    }
}