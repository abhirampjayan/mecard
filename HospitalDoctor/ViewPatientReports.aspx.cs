using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class HospitalDoctor_ViewPatientReports : System.Web.UI.Page
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
    string patientId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HosDocId"] != null)
        {
            DocId = Session["HosDocId"].ToString();
            HosId = Session["HospitalId"].ToString();
            patientId = Request.QueryString["PatientId"].ToString();
            if (!IsPostBack)
            {
                var query = from item in db.tbl_signups
                            where item.u_hakkimid == patientId
                            select item;
                foreach (var ss in query)
                {
                    LblUserName.Text = ss.name;
                }
                BindTestReports();
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
    }

    public void BindTestReports()
    {
        string bloodTest = "";
        string urineTes = "";
        string scan = "";
        string xray = "";
        string other = "";
        string otherName = "";
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("date"), new DataColumn("blood_test_report"), new DataColumn("urine_test_report"), new DataColumn("scan_test_report"), new DataColumn("xray_test_report"), new DataColumn("other_test_name"), new DataColumn("other_test_report") });

        try
        {
            string[] singleRecords;
            var selectRecords = from item in db.tbl_share_records
                                where item.share_d_id == DocId && item.share_h_id == HosId
                                select item;

            if (selectRecords.Count() > 0)
            {
                foreach (var ss in selectRecords)
                {
                    singleRecords = null;
                    singleRecords = ss.records.Split(',');

                    var query = from item in db.tbl_test_reports
                                where item.u_id == patientId && item.id == ss.record_id
                                orderby item.date descending
                                select item;
                    foreach (var s in query)
                    {
                        foreach (var sss in singleRecords)
                        {
                            if(sss=="1")
                            {
                                bloodTest = s.blood_test_report;
                            }
                            if (sss == "2")
                            {
                                urineTes = s.urine_test_report;
                            }
                            if (sss == "3")
                            {
                                scan = s.scan_test_report;
                            }
                            if (sss == "4")
                            {
                                xray = s.xray_test_report;
                            }
                            if (sss == "5")
                            {
                                other=s.other_test_report;
                                otherName=s.other_test_name;
                            }
                        }
                        dt.Rows.Add(s.date, bloodTest, urineTes, scan, xray, otherName, other);
                        bloodTest = urineTes = scan = xray = other = otherName = "";
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
                            HlnkBloodTest.Text = "------";
                        }
                        if (HlnkUrineTest.NavigateUrl == "")
                        {
                            HlnkUrineTest.Enabled = false;
                            HlnkUrineTest.ToolTip = "No data";
                            HlnkUrineTest.Text = "------";
                        }
                        if (LnkScanReport.NavigateUrl == "")
                        {
                            LnkScanReport.Enabled = false;
                            LnkScanReport.ToolTip = "No data";
                            LnkScanReport.Text = "------";
                        }
                        if (LnkXrayReport.NavigateUrl == "")
                        {
                            LnkXrayReport.Enabled = false;
                            LnkXrayReport.ToolTip = "No data";
                            LnkXrayReport.Text = "------";
                        }
                        if (LnkOtherReport.NavigateUrl == "")
                        {
                            LnkOtherReport.Enabled = false;
                            LnkOtherReport.ToolTip = "No data";
                            LnkOtherReport.Text = "------";
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
                    //Label1.Text = "This patient haven't a any shared reports.";
                    //ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient have no any shared reports with you.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('هذا المريض ليس لديه أي تقارير مشتركة معك.')</Script>");

                    //}
                }
            }
            else
            {
                //Label1.Text = "This patient is not shared any records with you.";
                //ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>alert('This patient is not shared any records with you.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>alert('هذا المريض ليس لديه أي تقارير مشتركة معك.')</Script>");

                //}
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }
}