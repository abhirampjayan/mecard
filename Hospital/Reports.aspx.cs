using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.IO;

public partial class Doctor_Reports : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

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
        if (Session["doctor"] == null)
        {
            Response.Redirect("~/Index/Doctor login.aspx");
        }
        if (!IsPostBack)
        {
            CheckLocation();
            BindDropdown();
        }
    }
    public void CheckLocation()
    {
        var query = from item in db.tbl_doctor_locations
                    join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                    where item1.d_email == Session["doctor"].ToString()
                    select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
        if (query.Count() <= 0)
        {
            Response.Redirect("~/Doctor/SetLocation.aspx");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    #region CodeForViewReport
    protected void BtnViewReport_Click(object sender, EventArgs e)
    {
        // All fields are selected
        if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDiagnose();
        }

         // only Form date selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryBysingleDate();
        }

        // if from and to dates are selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDate();
        }

        // if from date and diagnose selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByFromDateDiagnose();
        }

        // if only diagnose selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDiagnose();
        }

        // if no fields selected
        else
        {
            Panel3.Visible = false;
            RegisterStartupScript("", "<script>swal('Please select any options to take report')</script>");
            
            //Label1.Text = "Please select any options to take report";
            //this.ModalPopupExtender1.Show();
        }


        TxtBookDocId.Text = "";
    }

    protected void BtnViewReportId_Click(object sender, EventArgs e)
    {
        Panel3.Visible = true;
        GetHistoryByBookDocId();
        TxtFrom.Text = TxtTo.Text = "";
    }
    #endregion

    #region CodeForDownloadReport
    //protected void BtnDownload_Click(object sender, EventArgs e)
    //{
    //    TxtBookDocId.Text = "";
    //    string name = "Consulted History" + " " + DateTime.Now.ToShortDateString() + ".pdf";

    //    try
    //    {

    //        DataTable dt = new DataTable();
    //        dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

    //        string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
    //        string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

    //        // Check whether diagnose is selected .. if not selected peroform the following..
    //        if (DdlDiagnose.SelectedIndex <= 0)
    //        {
    //            var query = from item in db.tbl_hos_appmnt_histories
    //                        join item1 in db.tbl_signups on item.u_id equals item1.email
    //                        where item.h_id == null && item.d_id == Session["doctor"].ToString()
    //                        orderby item.a_date, item.a_time descending
    //                        select new { item.a_date, item.a_doc_daignose, item.a_end_time, item.a_doc_prescriptions, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };

    //            if (query.Count() > 0)
    //            {
    //                foreach (var ss in query)
    //                {
    //                    if (DateTime.Parse(fromDate) <= DateTime.Parse(ss.a_date) && DateTime.Parse(ss.a_date) <= DateTime.Parse(toDate))
    //                    {
    //                        dt.Rows.Add(ss.a_date, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
    //                    }
    //                }
    //                if (dt.Rows.Count > 0)
    //                {
    //                    GridView1.DataSource = dt;
    //                    GridView1.DataBind();
    //                    ExportToPdfForDate(name);
    //                }
    //                else
    //                {
    //                    RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
    //                    Panel3.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
    //                Panel3.Visible = false;
    //            }
    //        }

    //        // diagnose is selected perom this code
    //        else
    //        {
    //            var query = from item in db.tbl_hos_appmnt_histories
    //                        join item1 in db.tbl_signups on item.u_id equals item1.email
    //                        where item.h_id == null && item.d_id == Session["doctor"].ToString() && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text
    //                        orderby item.a_date, item.a_time descending
    //                        select new { item.a_date, item.a_doc_daignose, item.a_end_time, item.a_doc_prescriptions, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };

    //            if (query.Count() > 0)
    //            {
    //                foreach (var ss in query)
    //                {
    //                    if (DateTime.Parse(fromDate) <= DateTime.Parse(ss.a_date) && DateTime.Parse(ss.a_date) <= DateTime.Parse(toDate))
    //                    {
    //                        dt.Rows.Add(ss.a_date, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
    //                    }
    //                }
    //                if (dt.Rows.Count > 0)
    //                {
    //                    GridView1.DataSource = dt;
    //                    GridView1.DataBind();
    //                    ExportToPdfForDate(name);
    //                }
    //                else
    //                {
    //                    RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
    //                    Panel3.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
    //                Panel3.Visible = false;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex);
    //    }
    //}


    //protected void BtnDownloadId_Click(object sender, EventArgs e)
    //{

    //    TxtTo.Text = TxtFrom.Text = "";
    //    string name = "Consulted History" + " " + DateTime.Now.ToShortDateString() + ".pdf";
    //    string patientName = "";
    //    try
    //    {

    //        var query = from item in db.tbl_hos_appmnt_histories
    //                    join item1 in db.tbl_signups on item.u_id equals item1.email
    //                    where item.h_id == null && item.d_id == Session["doctor"].ToString() && item.u_id == TxtBookDocId.Text
    //                    orderby item.a_date, item.a_time descending
    //                    select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
    //        if (query.Count() > 0)
    //        {
    //            GridView1.DataSource = query.ToList();
    //            GridView1.DataBind();
    //            foreach (var ss in query)
    //            {
    //                patientName = ss.name;
    //            }
    //            ExportToPdfBookDocId(name, patientName);
    //        }
    //        else
    //        {
    //            RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
    //            Panel3.Visible = false;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex);
    //    }

    //}
    #endregion


    protected void TxtTo_TextChanged(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        if (TxtFrom.Text != "" && TxtTo.Text != "")
        {
            DateTime toDate = DateTime.Parse(TxtTo.Text);
            DateTime fromDate = DateTime.Parse(TxtFrom.Text);
            LblValidFrom.Visible = false;
            if (toDate < fromDate)
            {
               
                TxtTo.Text = "";
                TxtTo.Focus();
                //Label2.Text = "To date must be greater than or equal to from date.";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('To date must be greater than or equal to from date.')</Script>");
            }
            else
            {
                DdlDiagnose.Focus();
            }
        }
        else
        {
            TxtTo.Text = "";
            TxtFrom.Focus();
        }

    }
    protected void TxtFrom_TextChanged(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        if (TxtFrom.Text != "")
        {
            TxtTo.Enabled = true;
        }
        else
        {
            TxtTo.Text = "";
            TxtTo.Enabled = false;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (TxtBookDocId.Text != "")
        {
            GetHistoryByBookDocId();
        }
        // All fields are selected
        if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0)
        {
           
            GetHistoryByDateDiagnose();
        }

         // only Form date selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0)
        {
           
            GetHistoryBysingleDate();
        }

        // if from and to dates are selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0)
        {
           
            GetHistoryByDate();
        }

        // if from date and diagnose selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0)
        {
           
            GetHistoryByFromDateDiagnose();
        }

        // if only diagnose selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0)
        {
           
            GetHistoryByDiagnose();
        }

       // if the most visited patient button clicked
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() == "Most Patient")
        {
            GettingHistoryOfMostVisitedPatient();
        }

        // if the most patient visited diseases button clicked
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() == "Most Disease")
        {
            GettingHistoryOfMostVisitedPatient();
        }

    }

    public void BindDropdown()
    {
        try
        {
            var Query = from item in db.tbl_hos_appmnt_histories
                        where item.h_id == null && item.d_id == Session["doctor"].ToString()
                        select item;
            if (Query.Count() > 0)
            {
                DdlDiagnose.DataSource = Query.Distinct();
                DdlDiagnose.DataValueField = "id";
                DdlDiagnose.DataTextField = "a_doc_daignose";
                DdlDiagnose.DataBind();
                DdlDiagnose.Items.Insert(0, "--select--");
            }
            else
            {
                DdlDiagnose.Items.Clear();
                DdlDiagnose.Items.Insert(0, "--select--");
            }
        }
        catch (Exception ex)
        {
        }
    }

    #region GettingHistories
    public void GetHistoryByDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.email
                        where item.d_id == Session["doctor"].ToString() && item.h_id == null && DateTime.Parse(fromDate) <= item.a_date && item.a_date <=DateTime.Parse(toDate)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_end_time, item.a_doc_prescriptions, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string date=ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(date).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    //foreach(GridViewRow gRow in GridView1.Rows)
                    //{
                    //    Label LblDate = gRow.FindControl("LblDate") as Label;
                    //    Label LblViewDate = gRow.FindControl("LblViewDate") as Label;

                    //    LblViewDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();
                    //}
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                   
                    //Label3.Text = "No Data..";
                    //this.ModalPopupExtender3.Show();
                }
            }
            else
            {
              
                Panel3.Visible = false;
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //Label4.Text = "No data..!";
                //this.ModalPopupExtender4.Show();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByBookDocId()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.email
                        where item.h_id == null && item.d_id == Session["doctor"].ToString() && item.u_id == TxtBookDocId.Text
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string date = ss.a_date.ToString();
                    dt.Rows.Add(DateTime.Parse(date).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    //foreach(GridViewRow gRow in GridView1.Rows)
                    //{
                    //    Label LblDate = gRow.FindControl("LblDate") as Label;
                    //    Label LblViewDate = gRow.FindControl("LblViewDate") as Label;

                    //    LblViewDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();
                    //}
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                   
                    //Label5.Text = "No data...!";
                    //this.ModalPopupExtender5.Show();
                }
               
            }
            else
            {
                Panel3.Visible = false;
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data.')</Script>");

                //Label6.Text = "No data..!";
                //this.ModalPopupExtender6.Show();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByDateDiagnose()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.email
                        where item.d_id == Session["doctor"].ToString() && item.h_id == null && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text && DateTime.Parse(fromDate) <=item.a_date && item.a_date <=DateTime.Parse(toDate)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_end_time, item.a_doc_prescriptions, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string date = ss.a_date.ToString();
                    dt.Rows.Add(DateTime.Parse(date).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    //foreach(GridViewRow gRow in GridView1.Rows)
                    //{
                    //    Label LblDate = gRow.FindControl("LblDate") as Label;
                    //    Label LblViewDate = gRow.FindControl("LblViewDate") as Label;

                    //    LblViewDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();
                    //}
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");

                    //Label6.Text = "No data..!";
                    //this.ModalPopupExtender6.Show();
                }
            }
            else
            {
                Panel3.Visible = false;
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
              
                //Label6.Text = "No data..!";
                //this.ModalPopupExtender6.Show();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByDiagnose()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });


            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.email
                        where item.h_id == null && item.d_id == Session["doctor"].ToString() && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string date = ss.a_date.ToString();
                    dt.Rows.Add(DateTime.Parse(date).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    //foreach(GridViewRow gRow in GridView1.Rows)
                    //{
                    //    Label LblDate = gRow.FindControl("LblDate") as Label;
                    //    Label LblViewDate = gRow.FindControl("LblViewDate") as Label;

                    //    LblViewDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();
                    //}
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                   
                    //Label6.Text = "No data..!";
                    //this.ModalPopupExtender6.Show();
                }
            }
            else
            {
                Panel3.Visible = false;
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data.')</Script>");
              
                //Label6.Text = "No data..!";
                //this.ModalPopupExtender6.Show();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryBysingleDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate=DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.email
                        where item.h_id == null && item.d_id == Session["doctor"].ToString() && item.a_date == DateTime.Parse(fromDate)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string date = ss.a_date.ToString();
                    dt.Rows.Add(DateTime.Parse(date).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    //foreach(GridViewRow gRow in GridView1.Rows)
                    //{
                    //    Label LblDate = gRow.FindControl("LblDate") as Label;
                    //    Label LblViewDate = gRow.FindControl("LblViewDate") as Label;

                    //    LblViewDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();
                    //}
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");

                    //Label6.Text = "No data..!";
                    //this.ModalPopupExtender6.Show();
                }
            }
            else
            {
                Panel3.Visible = false;
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data.')</Script>");

                //Label6.Text = "No data..!";
                //this.ModalPopupExtender6.Show();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByFromDateDiagnose()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string frmDate=DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.email
                        where item.h_id == null && item.d_id == Session["doctor"].ToString() && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text && item.a_date == DateTime.Parse(frmDate)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string date = ss.a_date.ToString();
                    dt.Rows.Add(DateTime.Parse(date).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    //foreach(GridViewRow gRow in GridView1.Rows)
                    //{
                    //    Label LblDate = gRow.FindControl("LblDate") as Label;
                    //    Label LblViewDate = gRow.FindControl("LblViewDate") as Label;

                    //    LblViewDate.Text = DateTime.Parse(LblDate.Text).ToShortDateString();
                    //}
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                  
                    //Label6.Text = "No data..!";
                    //this.ModalPopupExtender6.Show();
                }
            }
            else
            {
                Panel3.Visible = false;
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data.')</Script>");

                //Label6.Text = "No data..!";
                //this.ModalPopupExtender6.Show();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    private void GettingHistoryOfMostVisitedPatient()
    {
        try
        {
            string u_id = "";
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = "";

            con.Open();
            SqlCommand cmd=new SqlCommand();
            cmd.Connection = con;
            if (TxtFrom.Text != "" && TxtTo.Text == "")
            {
                cmd.CommandText = "select top 1 u_id from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL  and a_date='" + fromDate + "'  group by u_id order by COUNT(u_id) desc ";
               //cmd = new SqlCommand("select top 1 u_id from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL  and a_date between '" + DateTime.Parse(fromDate) + "' and '" + DateTime.Parse(toDate) + "'  group by u_id order by COUNT(u_id) desc ", con);
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");
                cmd.CommandText = "select top 1 u_id from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL  and a_date between '" +fromDate + "' and '" +toDate + "'  group by u_id order by COUNT(u_id) desc ";
            }

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                u_id = dr["u_id"].ToString();
            }
            dr.Close();
            con.Close();

            if (TxtFrom.Text != "" && TxtTo.Text == "")
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            join item1 in db.tbl_signups on item.u_id equals item1.email
                            where item.u_id == u_id && item.a_date==DateTime.Parse(fromDate) && item.d_id == Session["doctor"].ToString() && item.h_id == null
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);

                    }
                    if (dt.Rows.Count > 0)
                    {
                        //this.ModalPopupExtender1.Hide();
                        Panel3.Visible = true;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        BtnPrintDownload.CommandArgument = BtnMostUser.Text;
                    }
                    else
                    {
                        Panel3.Visible = false;
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");

                        //Label6.Text = "No data..!";
                        //this.ModalPopupExtender6.Show();
                    }
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");

                    //Label6.Text = "No data..!";
                    //this.ModalPopupExtender6.Show();
                }
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            join item1 in db.tbl_signups on item.u_id equals item1.email
                            where item.u_id == u_id && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate) && item.d_id == Session["doctor"].ToString() && item.h_id == null
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);

                    }
                    if (dt.Rows.Count > 0)
                    {
                        //this.ModalPopupExtender1.Hide();
                        Panel3.Visible = true;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        BtnPrintDownload.CommandArgument = BtnMostUser.Text;
                    }
                    else
                    {
                        Panel3.Visible = false;
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                      
                        //Label6.Text = "No data..!";
                        //this.ModalPopupExtender6.Show();
                    }
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");

                    //Label6.Text = "No data..!";
                    //this.ModalPopupExtender6.Show();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void GettingHistoryOfMostPatientVisitedDisease()
    {
        try
        {
            string diseaseName = "";
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = "";

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if(TxtFrom.Text != "" && TxtTo.Text =="")
            {
                cmd.CommandText = "select top 1 a_doc_daignose from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL and a_date= '" +fromDate + "'  group by a_doc_daignose order by COUNT(a_doc_daignose) desc";
            //SqlCommand cmd = new SqlCommand("select top 1 a_doc_daignose from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL and a_date between '" + DateTime.Parse(fromDate) + "' and '" + DateTime.Parse(toDate) + "'  group by a_doc_daignose order by COUNT(a_doc_daignose) desc ", con);
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");
                cmd.CommandText = "select top 1 a_doc_daignose from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL and a_date between '" +fromDate + "' and '" + toDate + "' group by a_doc_daignose order by COUNT(a_doc_daignose) desc";
            }

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                diseaseName = dr["a_doc_daignose"].ToString();
            }
            dr.Close();
            con.Close();

            if (TxtFrom.Text != "" && TxtTo.Text == "")
            {

                var query = from item in db.tbl_hos_appmnt_histories
                            join item1 in db.tbl_signups on item.u_id equals item1.email
                            where item.a_doc_daignose == diseaseName &&  item.a_date==DateTime.Parse(fromDate) && item.d_id == Session["doctor"].ToString() && item.h_id == null
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);

                    }
                    if (dt.Rows.Count > 0)
                    {
                        //this.ModalPopupExtender1.Hide();
                        Panel3.Visible = true;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        BtnPrintDownload.CommandArgument = BtnMostDisease.Text;
                    }
                    else
                    {
                        Panel3.Visible = false;
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");

                        //Label6.Text = "No data..!";
                        //this.ModalPopupExtender6.Show();
                    }
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");

                    //Label6.Text = "No data..!";
                    //this.ModalPopupExtender6.Show();
                }
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            join item1 in db.tbl_signups on item.u_id equals item1.email
                            where item.a_doc_daignose == diseaseName && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate) && item.d_id == Session["doctor"].ToString() && item.h_id == null
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);

                    }
                    if (dt.Rows.Count > 0)
                    {
                        //this.ModalPopupExtender1.Hide();
                        Panel3.Visible = true;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        BtnPrintDownload.CommandArgument = BtnMostDisease.Text;
                    }
                    else
                    {
                        Panel3.Visible = false;
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");

                        //Label6.Text = "No data..!";
                        //this.ModalPopupExtender6.Show();
                    }
                }
                else
                {
                    Panel3.Visible = false;
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                  
                    //Label6.Text = "No data..!";
                    //this.ModalPopupExtender6.Show();
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    #endregion

    #region Exports to Pdfs

    public void ExportToPdfForDate(string reportName, string frmDate, string toDate, string diagnose)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename='" + reportName + "'");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        StringBuilder sb = new StringBuilder();
        sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>CONSULTED REPORT</b></td></tr>");

        // check whether from and to date only 
        if (reportName != "" && frmDate != "" && toDate != "" && diagnose == "")
        {
            sb.Append("<tr><td align='center'>From : ");
            sb.Append(frmDate);
            sb.Append("</td><td align='center'>To : ");
            sb.Append(toDate);
            sb.Append("</td></tr>");
        }

        // check whether from only
        else if (reportName != "" && frmDate != "" && toDate == "" && diagnose == "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(frmDate);
            sb.Append("</td><td align='center'>");
            sb.Append("</td></tr>");
        }

         // check whether from and diagnose only
        else if (reportName != "" && frmDate != "" && toDate == "" && diagnose != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(frmDate);
            sb.Append("</td><td align='center'>Diagnose : ");
            sb.Append(diagnose.ToUpper());
            sb.Append("</td></tr>");
        }

             // check whether diagnose only
        else if (reportName != "" && frmDate == "" && toDate == "" && diagnose != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(DateTime.Now.ToShortDateString());
            sb.Append("</td><td align='center'>Diagnose : ");
            sb.Append(diagnose.ToUpper());
            sb.Append("</td></tr>");
        }

        // check whether all fields selected
        else
        {
            sb.Append("<tr><td align='center'>From : ");
            sb.Append(frmDate);
            sb.Append("</td><td align='center'>To: ");
            sb.Append(toDate);
            sb.Append("</td></tr>");
            sb.Append("<tr><td align='center'>Diagnose : ");
            sb.Append(diagnose.ToUpper());
            sb.Append("</td><td align='center'>");
            sb.Append("</td></tr>");
        }

        sb.Append("</table>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView1.RenderControl(hw);
        StringReader sr = new StringReader(sb.ToString() + sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        GridView1.AllowPaging = true;
        GridView1.DataBind();
    }

    public void ExportToPdfBookDocId(string reportName, string patientName)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename='" + reportName + "'");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        StringBuilder sb = new StringBuilder();
        sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>CONSULTED REPORT</b></td></tr>");
        sb.Append("<tr><td align='center'>Name : ");
        sb.Append(patientName);
        sb.Append("</td><td align='center'>Date : ");
        sb.Append(DateTime.Now.ToShortDateString());
        sb.Append("</td></tr></table>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView1.RenderControl(hw);
        StringReader sr = new StringReader(sb.ToString() + sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        GridView1.AllowPaging = true;
        GridView1.DataBind();
    }


    #endregion

     
    protected void BtnPrintDownload_Click(object sender, EventArgs e)
    {
        string name = "Consulted History" + " " + DateTime.Now.ToShortDateString() + ".pdf";


        // if BookDoc id is selected
        if (TxtBookDocId.Text != "" && BtnPrintDownload.CommandArgument.ToString()=="")
        {
            string fileName = "Consulted History" + " " + DateTime.Now.ToShortDateString() + ".pdf";
            string patientName = "";
            try
            {
                var query= from item in db.tbl_signups 
                           where item.email==TxtBookDocId.Text
                           select item;
                foreach(var s  in query)
                {
                    patientName=s.name;
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex);
            }
            GetHistoryByBookDocId();
            ExportToPdfBookDocId(fileName,patientName);

        }

        //  if dates and diagnose are selected
        else
        {
            TxtBookDocId.Text = "";
            // All fields are selected
            if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
               
                GetHistoryByDateDiagnose();
                ExportToPdfForDate(name, TxtFrom.Text, TxtTo.Text, DdlDiagnose.SelectedItem.Text);
            }

             // only Form date selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
               
                GetHistoryBysingleDate();
                ExportToPdfForDate(name, TxtFrom.Text, "", "");
            }

            // if from and to dates are selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                
                GetHistoryByDate();
                ExportToPdfForDate(name, TxtFrom.Text, TxtTo.Text, "");
            }

            // if from date and diagnose selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                
                GetHistoryByFromDateDiagnose();
                ExportToPdfForDate(name, TxtFrom.Text, "", DdlDiagnose.SelectedItem.Text);
            }

            // if only diagnose selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                
                GetHistoryByDiagnose();
                ExportToPdfForDate(name, "", "", DdlDiagnose.SelectedItem.Text);
            }

             // if the most visited patients button clicked
            else if (BtnPrintDownload.CommandArgument.ToString() == "Most Patient" && TxtFrom.Text != "" && TxtTo.Text != "" )
            {
                //RegisterStartupScript("", "<script>alert('Please ')</script>");
                GettingHistoryOfMostVisitedPatient();
                ExportToPdfForDate(name, TxtFrom.Text, TxtTo.Text, "");
            }

            // if the most patients visited disease button clicked
            else if (BtnPrintDownload.CommandArgument.ToString() == "Most Disease" && TxtFrom.Text != "" && TxtTo.Text != "")
            {
                //RegisterStartupScript("", "<script>alert('Please ')</script>");
                GettingHistoryOfMostPatientVisitedDisease();
                ExportToPdfForDate(name, TxtFrom.Text, TxtTo.Text, "");
            }

            // if the most visited patients button clicked single date
            else if (BtnPrintDownload.CommandArgument.ToString() == "Most Patient" && TxtFrom.Text != "" && TxtTo.Text == "" )
            {
                //RegisterStartupScript("", "<script>alert('Please ')</script>");
                GettingHistoryOfMostVisitedPatient();
                ExportToPdfForDate(name, TxtFrom.Text, "", "");
            }

            // if the most patients visited disease button clicked single date
            else if (BtnPrintDownload.CommandArgument.ToString() == "Most Disease" && TxtFrom.Text != "" && TxtTo.Text == "" )
            {
                //RegisterStartupScript("", "<script>alert('Please ')</script>");
                GettingHistoryOfMostPatientVisitedDisease();
                ExportToPdfForDate(name, TxtFrom.Text, "", "");
            }

            else
            {
                Panel3.Visible = false;
            }
        }
    }

    protected void BtnMostUser_Click(object sender, EventArgs e)
    {
        TxtBookDocId.Text = "";
        GettingHistoryOfMostVisitedPatient();
    }
    protected void BtnMostDisease_Click(object sender, EventArgs e)
    {
        TxtBookDocId.Text = "";
        GettingHistoryOfMostPatientVisitedDisease();
    }
}