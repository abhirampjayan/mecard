using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class User_SharePreview : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

    // variables for storing informations
  public  string shareApointmentId = "";
  public string shareDocId = "";
  public string shareHosApoitmentId = "";
  public string shareHosDocId = "";
  public string shareHosId = "";


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
        if (Session["hakkemid_u"] == null)
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/SignInSignUp.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/SignInSignUp.aspx?l=ar-EG");
            //}
        }
        this.shareApointmentId = Request.QueryString["shareApointmentId"];
        this.shareDocId = Request.QueryString["shareDocId"];
        this.shareHosApoitmentId = Request.QueryString["shareHosApoitmentId"];
        this.shareHosDocId = Request.QueryString["shareHosDocId"];
        this.shareHosId = Request.QueryString["shareHosId"];
        if(!IsPostBack)
        {
           
        }
    }

    // Method for showing the selected history 
    public void BindGridHistory()
    {
        string fromDate = DateTime.Parse(txtHisFromDate.Text).ToString("yyyy-MM-dd");
        string toDate = DateTime.Parse(txtHisToDate.Text).ToString("yyyy-MM-dd");

                try
                {
                    var query = from item in db.tbl_hos_appmnt_histories
                                where item.u_id == Session["hakkemid_u"].ToString() && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate)
                                orderby item.a_date descending
                                select item;
                    if (query.Count() > 0)
                    {
                        GrvShareHistory.DataSource = query;
                        GrvShareHistory.DataBind();
                        foreach (GridViewRow grow in GrvShareHistory.Rows)
                        {
                             Label LblDocId = grow.FindControl("LblDocId") as Label;
                            Label LblHistoryDoctor = grow.FindControl("LblHistoryDoctor") as Label;
                            Label LblHosId = grow.FindControl("LblHosId") as Label;
                            Label LblDate = grow.FindControl("LblDate") as Label;
                            Label LblHistoryDate = grow.FindControl("LblHistoryDate") as Label;
                            Label LblHistoryHospital = grow.FindControl("LblHistoryHospital") as Label;
                            LblHistoryDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();

                            if (LblHosId.Text == "")
                            {
                                LblHistoryHospital.Text = "----";

                                var selectDoctor = (from item in db.tbl_doctors
                                                    where item.d_hakkimid == LblDocId.Text
                                                    select item).First();
                                LblHistoryDoctor.Text = selectDoctor.d_name;
                            }
                            else
                            {
                                var selectHospital = from item in db.tbl_hdoctors
                                                     join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_hakkimid
                                                     where item.hd_email == LblDocId.Text
                                                     select new {item1.h_name,item.hd_name };
                                foreach(var ss in selectHospital)
                                {
                                    LblHistoryDoctor.Text = ss.hd_name.ToString();
                                    LblHistoryHospital.Text = ss.h_name.ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                
                PnlHistory.Visible = false;
                        txtHisToDate.Text = "";
                //Label2.Text = "No Records are founded..";
                //this.ModalPopupExtender3.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No records are founded..')</Script>");
            }

                }
                catch (Exception ex)
                {

                }
            
    }

    //Method for showing Test Reports
    public void BindRecords()
    {
       
        string fromDate = DateTime.Parse(TxtFromDate.Text).ToString("yyyy-MM-dd");
        string toDate = DateTime.Parse(TxtToDate.Text).ToString("yyyy-MM-dd");

        try
        {
            var query = from item in db.tbl_test_reports
                        where item.u_id == Session["hakkemid_u"].ToString() && (DateTime.Parse(fromDate) <= item.date && item.date <= DateTime.Parse(toDate))
                        orderby item.date descending
                        select item;
            if (query.Count() > 0)
            {
                GrvShareRecords.DataSource = query;
                GrvShareRecords.DataBind();
                foreach(GridViewRow grow in GrvShareRecords.Rows)
                {
                    Label LblHistoryDate = grow.FindControl("LblHistoryDate") as Label;
                    Label LblDate = grow.FindControl("LblDate") as Label;
                    Label LblRecordId = grow.FindControl("LblRecordId") as Label;
                    Label LblBlood = grow.FindControl("LblBlood") as Label;
                    Label Label1Urine = grow.FindControl("Label1Urine") as Label;
                    Label LblScan = grow.FindControl("LblScan") as Label;
                    Label LblXRay = grow.FindControl("LblXRay") as Label;
                     Label LblOther = grow.FindControl("LblOther") as Label;
                    CheckBox CkbBlood = grow.FindControl("CkbBlood") as CheckBox;
                    CheckBox CkbUrine = grow.FindControl("CkbUrine") as CheckBox;
                    CheckBox CkbScan = grow.FindControl("CkbScan") as CheckBox;
                    CheckBox CkbXray = grow.FindControl("CkbXray") as CheckBox;
                    CheckBox CkbOther = grow.FindControl("CkbOther") as CheckBox;

                    LblHistoryDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();

                    if(LblBlood.Text=="")
                    {
                        LblBlood.Text = "No Data";
                        LblBlood.Visible = true;
                        CkbBlood.Visible = false;
                    }
                    if (Label1Urine.Text == "")
                    {
                        Label1Urine.Text = "No Data";
                        Label1Urine.Visible = true;
                        CkbUrine.Visible = false;
                    }
                    if (LblScan.Text == "")
                    {
                        LblScan.Text = "No Data";
                        LblScan.Visible = true;
                        CkbScan.Visible = false;
                    }
                    if (LblXRay.Text == "")
                    {
                        LblXRay.Text = "No Data";
                        LblXRay.Visible = true;
                        CkbXray.Visible = false;
                    }
                    if (LblOther.Text == "")
                    {
                        LblOther.Text = "No Data";
                        LblOther.Visible = true;
                        CkbOther.Visible = false;
                    }
                }
            }
            else
            {
                PnlRecords.Visible = false;
               
                TxtFromDate.Text = TxtToDate.Text = "";
                //Label2.Text = "No records are founded";
                //this.ModalPopupExtender3.Show();

                RegisterStartupScript("", "<Script Language=JavaScript>swal('No records are founded..')</Script>");
            }
        }
        catch(Exception ex)
        {
        }
       

    }

    protected void txtHisToDate_TextChanged(object sender, EventArgs e)
    {
        if (txtHisFromDate.Text != "")
        {
            DateTime dt1 = DateTime.Parse(txtHisFromDate.Text);
            DateTime dt2 = DateTime.Parse(txtHisToDate.Text);
            if (dt1 <= dt2)
            {
                PnlHistory.Visible = true;
                BindGridHistory();
                LblError1.Visible = false;
            }
            else
            {
                PnlHistory.Visible = false;
                //LblError1.Text = "The slected TO date must be greater than or equal to FROM date.";
                //LblError1.Visible = true;
               
                txtHisToDate.Text = "";
                //Label2.Text = "The To date must be greater than or equal to From date..";
                //this.ModalPopupExtender3.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('The To date must be greater than or equal to From date..')</Script>");
            }
        }
        else
        {
            PnlHistory.Visible = false;
           
            txtHisToDate.Text = "";
            //Label2.Text = "Please select from date...";
            //this.ModalPopupExtender3.Show();
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select from date...')</Script>");
            //LblError1.Text = "Please select FROM date";
            //LblError1.Visible = true;  
        }
    }

    protected void BtnShareHistory_Click(object sender, EventArgs e)
    {
             List<string> HistoryId = new List<string>();
             DateTime dt1 = DateTime.Parse(txtHisFromDate.Text);
            DateTime dt2 = DateTime.Parse(txtHisToDate.Text);
            if (dt1 <= dt2)
            {
                //BindGridHistory();
                string fromDate= DateTime.Parse(txtHisFromDate.Text).ToString("yyyy-MM-dd");
                string toDate= DateTime.Parse(txtHisToDate.Text).ToString("yyyy-MM-dd");

                CheckBox CkbALLSelect = GrvShareHistory.HeaderRow.FindControl("CkbALLSelect") as CheckBox;
                if (CkbALLSelect.Checked == true)
                {
                    var queryHistory = from item in db.tbl_hos_appmnt_histories
                                       where DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate) && item.u_id==Session["hakkemid_u"].ToString()
                                       select item;
                    HistoryId.Clear(); 
                    foreach (var ss in queryHistory)
                    {
                        HistoryId.Add(ss.id.ToString());
                    }
                }
                else
                {
                    
                    
                    foreach (GridViewRow grow in GrvShareHistory.Rows)
                    {
                        CheckBox CheckBox1 = grow.FindControl("CheckBox1") as CheckBox;
                        Label LblHisId = grow.FindControl("LblHisId") as Label;

                        if (CheckBox1.Checked == true)
                        {
                            HistoryId.Add(LblHisId.Text);
                        }
                    }
                }

            if (HistoryId.Count <= 0)
            {

                //LblError1.Text = "No items selected for sharing..";
                PnlHistory.Visible = false;
                txtHisToDate.Text = "";
                //LblError1.Visible = true;
                //Label2.Text = "No items selected for sharing.";
                //this.ModalPopupExtender3.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No items selected for sharing.')</Script>");
            }
            else
            {

                if (Request.QueryString["shareHosId"].ToString() == "")
                {
                    foreach (var ss in HistoryId)
                    {
                        var query = from item in db.tbl_share_histories
                                    where item.appointment_id == Convert.ToInt64(Request.QueryString["shareApointmentId"]) && item.history_id == Convert.ToInt64(ss) && item.share_d_id == Request.QueryString["shareDocId"] && item.share_h_id == null
                                    select item;
                        if (query.Count() <= 0)
                        {
                            tbl_share_history td = new tbl_share_history()
                            {
                                appointment_id = Convert.ToInt64(Request.QueryString["shareApointmentId"]),
                                history_id = Convert.ToInt32(ss),
                                share_h_id = null,
                                share_d_id = Request.QueryString["shareDocId"],
                            };
                            db.tbl_share_histories.InsertOnSubmit(td);
                            db.SubmitChanges();
                        }
                    }

                }
                else
                {
                    foreach (var ss in HistoryId)
                    {
                        var query = from item in db.tbl_share_histories
                                    where item.appointment_id == Convert.ToInt64(Request.QueryString["shareHosApoitmentId"]) && item.history_id == Convert.ToInt64(ss) && item.share_d_id == Request.QueryString["shareHosDocId"] && item.share_h_id == Request.QueryString["shareHosId"]
                                    select item;
                        if (query.Count() <= 0)
                        {
                            tbl_share_history td = new tbl_share_history()
                            {
                                appointment_id = Convert.ToInt64(Request.QueryString["shareHosApoitmentId"]),
                                history_id = Convert.ToInt32(ss),
                                share_h_id = Request.QueryString["shareHosId"],
                                share_d_id = Request.QueryString["shareHosDocId"],
                            };
                            db.tbl_share_histories.InsertOnSubmit(td);
                            db.SubmitChanges();
                        }

                    }

                }


                //LblError1.Text = "Shared succesfully.";
                PnlHistory.Visible = false;
                //LblError1.Visible = true;


                txtHisFromDate.Text = txtHisToDate.Text = "";
                //Label2.Text = "Shared successfully...";
                //this.ModalPopupExtender3.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Shared successfully...')</Script>");
            }
        }
            else
            {
                PnlHistory.Visible = false;
                //LblError1.Text = "The slected TO date must be greater than or equal to FROM date.";
                //LblError1.Visible = true;
             
                txtHisToDate.Text = "";
            //Label2.Text = "The To date must be greater than From date..";
            //this.ModalPopupExtender3.Show();
            RegisterStartupScript("", "<Script Language=JavaScript>swal('The To date must be greater than From date..')</Script>");
        }

    }

    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        if (TxtFromDate.Text != "")
        {
            DateTime dt1 = DateTime.Parse(TxtFromDate.Text);
            DateTime dt2 = DateTime.Parse(TxtToDate.Text);
            if (dt1 <= dt2)
            {
                PnlRecords.Visible = true;
                BindRecords();
            }
            else
            {
               
                PnlRecords.Visible = false;
                TxtToDate.Text = "";
                //Label2.Text = "The to date must be greter than or equal to from date..";
                //this.ModalPopupExtender3.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('The to date must be greter than or equal to from date..')</Script>");
            }
        }
        else
        {
           
            PnlRecords.Visible = false;
            TxtToDate.Text = "";
            //Label2.Text = "Please select a from date..";
            //this.ModalPopupExtender3.Show();
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select a from date..')</Script>");

        }
    }

    protected void BtnShare_Click(object sender, EventArgs e)
    {
        if (GrvShareRecords.Rows.Count > 0)
        {
            if (shareHosId == "")
            {
                CheckBox CkbAllSelect = GrvShareRecords.HeaderRow.FindControl("CkbAllSelect") as CheckBox;
                if (CkbAllSelect.Checked == true)
                {
                    string fromDate = DateTime.Parse(TxtFromDate.Text).ToString("yyyy-MM-dd");
                    string toDate = DateTime.Parse(TxtToDate.Text).ToString("yyyy-MM-dd");

                    var queryRecords = from item in db.tbl_test_reports
                                       where DateTime.Parse(fromDate) <= item.date && item.date <= DateTime.Parse(toDate) && item.u_id == Session["hakkemid_u"].ToString()
                                       select item;
                    foreach (var ss in queryRecords)
                    {
                        var queryApoinmnt = from item in db.tbl_share_records
                                            where item.appointment_id == Convert.ToInt64(shareApointmentId) && item.record_id == Convert.ToInt64(ss.id) && item.share_d_id == shareDocId && item.share_h_id == null
                                            select item;
                        if (queryApoinmnt.Count() <= 0)
                        {
                            tbl_share_record trd = new tbl_share_record()
                            {
                                appointment_id = Convert.ToInt64(shareApointmentId),
                                record_id = Convert.ToInt64(ss.id),
                                share_d_id = shareDocId,
                                share_h_id = null,
                                records = "1,2,3,4,5,",
                            };
                            db.tbl_share_records.InsertOnSubmit(trd);
                            db.SubmitChanges();
                        }
                    }
                  
                    PnlRecords.Visible = false;
                    TxtToDate.Text = TxtFromDate.Text = "";
                    //Label2.Text = "Shared succesfully..";
                    //this.ModalPopupExtender3.Show();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Shared succesfully..')</Script>");
                }
                else
                {
                    List<int> checkBoxCounts = new List<int>();
                    checkBoxCounts.Clear();
                    foreach (GridViewRow grow in GrvShareRecords.Rows)
                    {
                        Label LblRecordId = grow.FindControl("LblRecordId") as Label;
                        CheckBox CkbAll = grow.FindControl("CkbAll") as CheckBox;
                        CheckBox CkbBlood = grow.FindControl("CkbBlood") as CheckBox;
                        CheckBox CkbUrine = grow.FindControl("CkbUrine") as CheckBox;
                        CheckBox CkbScan = grow.FindControl("CkbScan") as CheckBox;
                        CheckBox CkbXray = grow.FindControl("CkbXray") as CheckBox;
                        CheckBox CkbOther = grow.FindControl("CkbOther") as CheckBox;

                       
                        if (CkbAll.Checked == true)
                        {
                            
                            checkBoxCounts.Add(1);
                            var query = from item in db.tbl_share_records
                                        where item.appointment_id == Convert.ToInt64(shareApointmentId) && item.record_id == Convert.ToInt64(LblRecordId.Text) && item.share_d_id == shareDocId && item.share_h_id == null
                                        select item;
                            if (query.Count() <= 0)
                            {
                                tbl_share_record trd = new tbl_share_record()
                                {
                                    appointment_id = Convert.ToInt64(shareApointmentId),
                                    record_id = Convert.ToInt64(LblRecordId.Text),
                                    share_d_id = shareDocId,
                                    share_h_id = null,
                                    records = "1,2,3,4,5,",
                                };
                                db.tbl_share_records.InsertOnSubmit(trd);
                                db.SubmitChanges();
                            }
                        }
                        else
                        {
                            var query = from item in db.tbl_share_records
                                        where item.appointment_id == Convert.ToInt64(shareApointmentId) && item.record_id == Convert.ToInt64(LblRecordId.Text) && item.share_d_id == shareDocId && item.share_h_id == null
                                        select item;
                            if (query.Count() <= 0)
                            {
                                tbl_share_record trd = new tbl_share_record();
                                if (CkbBlood.Checked == true || CkbOther.Checked == true || CkbScan.Checked == true || CkbUrine.Checked == true || CkbXray.Checked == true)
                                {
                                    checkBoxCounts.Add(1);
                                    trd.record_id = Convert.ToInt64(LblRecordId.Text);
                                    trd.appointment_id = Convert.ToInt64(shareApointmentId);
                                    trd.share_d_id = shareDocId;
                                    trd.share_h_id = null;
                                    if (CkbBlood.Checked == true)
                                    {
                                        trd.records = "1,";
                                    }
                                    if (CkbUrine.Checked == true)
                                    {
                                        trd.records += "2,";
                                    }
                                    if (CkbScan.Checked == true)
                                    {
                                        trd.records += "3,";
                                    }
                                    if (CkbXray.Checked == true)
                                    {
                                        trd.records += "4,";
                                    }
                                    if (CkbOther.Checked == true)
                                    {
                                        trd.records += "5,";
                                    }
                                    db.tbl_share_records.InsertOnSubmit(trd);
                                    db.SubmitChanges();
                                }
                            }
                            else
                            {
                                if (CkbBlood.Checked == true || CkbOther.Checked == true || CkbScan.Checked == true || CkbUrine.Checked == true || CkbXray.Checked == true)
                                {
                                    checkBoxCounts.Add(1);
                                }
                            }
                        }
                    }
                    if (checkBoxCounts.Count > 0)
                    {
                       
                        checkBoxCounts.Clear();
                        //Label2.Text = "Shared successfully..";
                        //this.ModalPopupExtender3.Show();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Shared succesfully..')</Script>");
                    }
                    else
                    {
                       
                        checkBoxCounts.Clear();
                        //Label2.Text = "Please select any record to share..";
                        //this.ModalPopupExtender3.Show();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any record to share..')</Script>");
                    }
                    PnlRecords.Visible = false;
                    TxtToDate.Text = TxtFromDate.Text = "";
                }
            }

            // if the selected doctor is an hospital doctor
            else
            {
                CheckBox CkbAllSelect = GrvShareRecords.HeaderRow.FindControl("CkbAllSelect") as CheckBox;
                if (CkbAllSelect.Checked == true)
                {
                    string fromDate = DateTime.Parse(TxtFromDate.Text).ToString("yyyy-MM-dd");
                    string toDate = DateTime.Parse(TxtToDate.Text).ToString("yyyy-MM-dd");

                    var queryRecords = from item in db.tbl_test_reports
                                       where DateTime.Parse(fromDate) <= item.date && item.date <= DateTime.Parse(toDate) && item.u_id == Session["hakkemid_u"].ToString()
                                       select item;
                    foreach (var ss in queryRecords)
                    {
                        var queryApoinmnt = from item in db.tbl_share_records
                                            where item.appointment_id == Convert.ToInt64(shareHosApoitmentId) && item.record_id == Convert.ToInt64(ss.id) && item.share_d_id == shareHosDocId && item.share_h_id == shareHosId
                                            select item;
                        if (queryApoinmnt.Count() <= 0)
                        {
                            tbl_share_record trd = new tbl_share_record()
                            {
                                appointment_id = Convert.ToInt64(shareHosApoitmentId),
                                record_id = Convert.ToInt64(ss.id),
                                share_d_id = shareHosDocId,
                                share_h_id = shareHosId,
                                records = "1,2,3,4,5,",
                            };
                            db.tbl_share_records.InsertOnSubmit(trd);
                            db.SubmitChanges();
                        }
                    }
                   
                    PnlRecords.Visible = false;
                    TxtToDate.Text = TxtFromDate.Text = "";
                    //Label2.Text = "Shared succesfully..";
                    //this.ModalPopupExtender3.Show();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Shared successfully..')</Script>");
                }
                else
                {
                    List<int> checkBoxselectCount = new List<int>();
                    checkBoxselectCount.Clear();
                    foreach (GridViewRow grow in GrvShareRecords.Rows)
                    {
                        Label LblRecordId = grow.FindControl("LblRecordId") as Label;
                        CheckBox CkbAll = grow.FindControl("CkbAll") as CheckBox;
                        CheckBox CkbBlood = grow.FindControl("CkbBlood") as CheckBox;
                        CheckBox CkbUrine = grow.FindControl("CkbUrine") as CheckBox;
                        CheckBox CkbScan = grow.FindControl("CkbScan") as CheckBox;
                        CheckBox CkbXray = grow.FindControl("CkbXray") as CheckBox;
                        CheckBox CkbOther = grow.FindControl("CkbOther") as CheckBox;


                        if (CkbAll.Checked == true)
                        {
                            
                            checkBoxselectCount.Add(1);
                            var query = from item in db.tbl_share_records
                                        where item.appointment_id == Convert.ToInt64(shareHosApoitmentId) && item.record_id == Convert.ToInt64(LblRecordId.Text) && item.share_d_id == shareHosDocId && item.share_h_id == shareHosId
                                        select item;
                            if (query.Count() <= 0)
                            {
                                tbl_share_record trd = new tbl_share_record()
                                {
                                    appointment_id = Convert.ToInt64(shareHosApoitmentId),
                                    record_id = Convert.ToInt64(LblRecordId.Text),
                                    share_d_id = shareHosDocId,
                                    share_h_id = shareHosId,
                                    records = "1,2,3,4,5,",
                                };
                                db.tbl_share_records.InsertOnSubmit(trd);
                                db.SubmitChanges();
                            }
                        }
                        else
                        {
                           
                            var query = from item in db.tbl_share_records
                                        where item.appointment_id == Convert.ToInt64(shareHosApoitmentId) && item.record_id == Convert.ToInt64(LblRecordId.Text) && item.share_d_id == shareHosDocId && item.share_h_id == shareHosId
                                        select item;
                            if (query.Count() <= 0)
                            {
                                tbl_share_record trd = new tbl_share_record();
                                if (CkbBlood.Checked == true || CkbOther.Checked == true || CkbScan.Checked == true || CkbUrine.Checked == true || CkbXray.Checked == true)
                                {
                                    checkBoxselectCount.Add(1);

                                    trd.record_id = Convert.ToInt64(LblRecordId.Text);
                                    trd.appointment_id = Convert.ToInt64(shareHosApoitmentId);
                                    trd.share_d_id = shareHosDocId;
                                    trd.share_h_id = shareHosId;
                                    if (CkbBlood.Checked == true)
                                    {
                                        trd.records = "1,";
                                    }
                                    if (CkbUrine.Checked == true)
                                    {
                                        trd.records += "2,";
                                    }
                                    if (CkbScan.Checked == true)
                                    {
                                        trd.records += "3,";
                                    }
                                    if (CkbXray.Checked == true)
                                    {
                                        trd.records += "4,";
                                    }
                                    if (CkbOther.Checked == true)
                                    {
                                        trd.records += "5,";
                                    }
                                    db.tbl_share_records.InsertOnSubmit(trd);
                                    db.SubmitChanges();
                                }
                            }
                            else
                            {
                                if (CkbBlood.Checked == true || CkbOther.Checked == true || CkbScan.Checked == true || CkbUrine.Checked == true || CkbXray.Checked == true)
                                {
                                    checkBoxselectCount.Add(1);
                                }

                            }
                        }
                    }
                    if (checkBoxselectCount.Count > 0)
                    {
                        
                        checkBoxselectCount.Clear();
                        //Label2.Text = "Shared successfully";
                        //this.ModalPopupExtender3.Show();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Shared successfully..')</Script>");
                    }
                    else
                    {
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('Please select any record to share..')</Script>");
                        checkBoxselectCount.Clear();
                        //Label2.Text = "Please select any record to share..";
                        //this.ModalPopupExtender3.Show();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any record to share..')</Script>");

                    }
                    PnlRecords.Visible = false;
                    TxtToDate.Text = TxtFromDate.Text = "";
                }
            }
        }

        else
        {
            
            PnlRecords.Visible = false;
            TxtToDate.Text = "";
            //Label2.Text = "No records selected for sharing..";
            //this.ModalPopupExtender3.Show();
            RegisterStartupScript("", "<Script Language=JavaScript>swal('No records selected for sharing..')</Script>");
        }
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        //if (Session["Speciality"].ToString() == "Auto")
        //{
            Response.Redirect("~/User/UserAppointments.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/User/UserAppointments.aspx?l=ar-EG");
        //}
    }
}