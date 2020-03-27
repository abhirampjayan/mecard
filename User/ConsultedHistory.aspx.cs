using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.IO;

public partial class User_ConsultedHistory : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

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
            Response.Redirect("~/Index/SignInSignUp.aspx");

        }
        if (!IsPostBack)
        {
            //PatientHistory();
            BindHospital();
            BindDoctor();
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    // Report View

    protected void BtnViewReport_Click(object sender, EventArgs e)
    {
        try
        {
            // Check whether only form and to dates are selected
            if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GetHistoryByDate();
            }

            // Check whether only form and to dates and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GettingHistoryByDateHospital();
            }

            // Check whether only form and to dates ,doctor and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GettingHistoryByDateDoctorHospital();
            }

            // Check whether only form and to dates and doctor selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GettingHistoryByDateDoctorHospital();
            }

            // Check whether only form and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GettingHistoryByFromDateHospital();
            }

            // Check whether only form and doctor selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GettingHistoryByFromDateDoctor();
            }

            // Check whether only form selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GettingHistoryByFromDate();
            }

            // Check whether only doctor selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GettingHistoryByDoctor();
            }

            // Check whether only hospital selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GettingHistoryByHospital();
            }

            // Check whether only docotr and hospital selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GettingHistoryByHospitalDoctor();
            }

            else
            {
                Panel3.Visible = false;
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any option to take report..')</Script>");
                //Label2.Text = "Please select any option to take report..";
                //this.ModalPopupExtender2.Show();
               
            }
        }
        catch (Exception ex)
        {

        }
    }

    // Download or Print
    protected void BtnPrintDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string reportName = "Consulted History" + " " + DateTime.Now.ToShortDateString() + ".pdf";
            // Check whether only form and to dates are selected
            if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex <= 0)
            {

                GetHistoryByDate();
                ExportToPdfDateDoctorHospital(reportName, TxtFrom.Text, TxtTo.Text, "", "");
            }

            // Check whether only form and to dates and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByDateHospital();
                ExportToPdfDateDoctorHospital(reportName, TxtFrom.Text, TxtTo.Text, "", DdlHospital.SelectedItem.Text);
            }

            // Check whether only form and to dates ,doctor and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByDateDoctorHospital();
                ExportToPdfDateDoctorHospital(reportName, TxtFrom.Text, TxtTo.Text, DdlDoctorName.SelectedItem.Text, DdlHospital.SelectedItem.Text);
            }

            // Check whether only form and to dates and doctor selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {

                GettingHistoryByDateDoctorHospital();
                ExportToPdfDateDoctorHospital(reportName, TxtFrom.Text, TxtTo.Text, DdlDoctorName.SelectedItem.Text, "");
            }

            // Check whether only form and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByFromDateHospital();
                ExportToPdfDateDoctorHospital(reportName, TxtFrom.Text, "", "", DdlHospital.SelectedItem.Text);
            }

            // Check whether only form and doctor selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {

                GettingHistoryByFromDateDoctor();
                ExportToPdfDateDoctorHospital(reportName, TxtFrom.Text, "", DdlDoctorName.SelectedItem.Text, "");
            }

            // Check whether only form selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex <= 0)
            {

                GettingHistoryByFromDate();
                ExportToPdfDateDoctorHospital(reportName, TxtFrom.Text, "", "", "");
            }

            // Check whether only doctor selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {

                GettingHistoryByDoctor();
                ExportToPdfDateDoctorHospital(reportName, "", "", DdlDoctorName.SelectedItem.Text, "");
            }

            // Check whether only hospital selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByHospital();
                ExportToPdfDateDoctorHospital(reportName, "", "", "", DdlHospital.SelectedItem.Text);
            }

            // Check whether only docotr and hospital selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByHospitalDoctor();
                ExportToPdfDateDoctorHospital(reportName, "", "", DdlDoctorName.SelectedItem.Text, DdlHospital.SelectedItem.Text);
            }

            else
            {
                Panel3.Visible = false;
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any option to take report..')</Script>");
                //Label2.Text = "Please select any option to take report..";
                //this.ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex) { }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            // Check whether only form and to dates are selected
            if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex <= 0)
            {

                GetHistoryByDate();
            }

            // Check whether only form and to dates and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {
                GettingHistoryByDateHospital();
            }

            // Check whether only form and to dates ,doctor and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByDateDoctorHospital();
            }

            // Check whether only form and to dates and doctor selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {

                GettingHistoryByDateDoctorHospital();
            }

            // Check whether only form and hospital selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByFromDateHospital();
            }

            // Check whether only form and doctor selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {

                GettingHistoryByDateHospital();
            }

            // Check whether only form selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex <= 0)
            {

                GettingHistoryByFromDate();
            }

            // Check whether only doctor selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex <= 0)
            {

                GettingHistoryByDoctor();
            }

            // Check whether only hospital selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex <= 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByHospital();
            }

            // Check whether only docotr and hospital selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDoctorName.SelectedIndex > 0 && DdlHospital.SelectedIndex > 0)
            {

                GettingHistoryByHospitalDoctor();
            }

            else
            {

            }
        }
        catch (Exception ex) { }
    }

    protected void TxtTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Panel3.Visible = false;
            if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                DateTime toDate = DateTime.Parse(TxtTo.Text);
                DateTime fromDate = DateTime.Parse(TxtFrom.Text);
                //LblValidFrom.Visible = false;
                if (toDate < fromDate)
                {
                    //RegisterStartupScript("", "<script>alert('To date must be greater than or equal to from date.')</script>");
                    TxtTo.Text = "";
                    TxtTo.Focus();
                    //Label2.Text = "To date must be greater than or equal to from date.";
                    //this.ModalPopupExtender2.Show();
                    RegisterStartupScript("", "<script>swal('To date must be greater than or equal to from date.')</script>");
                }
                else
                {
                    DdlHospital.Focus();
                }
            }
            else
            {
                TxtTo.Text = "";
                TxtFrom.Focus();
            }
        }
        catch (Exception ex) { }
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

    //Methods for Binding Dropdowns

    public void BindHospital()
    {
        try
        {
            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_hakkimid
                        select new { item1.h_hakkimid, item1.h_name };
            if (query.Count() > 0)
            {
                DdlHospital.DataSource = query.Distinct();
                DdlHospital.DataTextField = "h_name";
                DdlHospital.DataValueField = "h_hakkimid";
                DdlHospital.DataBind();
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    DdlHospital.Items.Insert(0, "--select--");
                //}
                //else
                //{
                //    DdlHospital.Items.Insert(0, "--تحديد--");
                //}
            }
            else
            {
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    DdlHospital.Items.Insert(0, "--select--");
                //}
                //else
                //{
                //    DdlHospital.Items.Insert(0, "--تحديد--");
                //}
            }
        }
        catch (Exception ex) { }
    }

    public void BindDoctor()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Name"), new DataColumn("D_Id") });

            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString()
                        select item;

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    if (ss.h_id == null)
                    {
                        var doc = from item in db.tbl_doctors
                                  where item.d_hakkimid == ss.d_id
                                  select item;
                        foreach (var s in doc)
                        {
                            dt.Rows.Add(s.d_name, s.d_hakkimid);
                        }
                    }
                    else
                    {
                        var doc = from item in db.tbl_hdoctors
                                  where item.hd_email == ss.d_id
                                  select item;
                        foreach (var s in doc)
                        {
                            dt.Rows.Add(s.hd_name, s.hd_email);
                        }
                    }
                }

                DataView dview = new DataView(dt);
                DataTable dvalue = dview.ToTable(true, "Name", "D_Id");

                DdlDoctorName.DataSource = dvalue;
                DdlDoctorName.DataTextField = "Name";
                DdlDoctorName.DataValueField = "D_Id";
                DdlDoctorName.DataBind();
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    DdlDoctorName.Items.Insert(0, "--select--");
                //}
                //else
                //{
                //    DdlDoctorName.Items.Insert(0, "--تحديد--");
                //}
            }
            else
            {
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    DdlDoctorName.Items.Insert(0, "--select--");
                //}
                //else
                //{
                //    DdlDoctorName.Items.Insert(0, "--تحديد--");
                //}
            }
        }
        catch (Exception ex) { }
    }

    // Methods for getting consulted histories based conditions

    #region GettingHistories



    public void GetHistoryByBookDocId()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });

            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString()
                        select item;

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data.')</Script>");
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    // For from to To dates
    public void GetHistoryByDate()
    {
        try
        {
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[9] { new DataColumn("a_date"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("d_id"), new DataColumn("h_id") });

            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });


            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString() && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate)
                        orderby item.a_date, item.a_time descending
                        select item;

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    //dt.Rows.Add(ss.a_date, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.d_id, ss.h_id);
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt1.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt1.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }

                }
                if (dt1.Rows.Count > 0)
                {

                    GridView1.DataSource = dt1;
                    GridView1.DataBind();

                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    //for Dates and hospotal
    public void GettingHistoryByDateHospital()
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString() && item.h_id == DdlHospital.SelectedValue && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate)
                        select item;

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    //dt.Rows.Add(ss.a_date, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.d_id, ss.h_id);
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt1.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt1.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }


                }
                if (dt1.Rows.Count > 0)
                {

                    GridView1.DataSource = dt1;
                    GridView1.DataBind();

                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    // For dates,hospital and doctor
    public void GettingHistoryByDateDoctorHospital()
    {
        try
        {

            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlHospital.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            where item.u_id == Session["hakkemid_u"].ToString() && item.d_id == DdlDoctorName.SelectedValue && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate)
                            select item;

                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        string a_date = ss.a_date.ToString();
                        //dt.Rows.Add(ss.a_date, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.d_id, ss.h_id);
                        if (ss.h_id == null)
                        {
                            var quer1 = (from item in db.tbl_doctors
                                         where item.d_hakkimid == ss.d_id
                                         select item).First();
                            //foreach(var doc in quer1 )
                            //{
                            dt1.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                            //}
                        }
                        else
                        {
                            var quer1 = (from item in db.tbl_hdoctors
                                         where item.hd_email == ss.d_id
                                         select item).First();
                            //foreach (var doc in quer1)
                            //{
                            var quer2 = (from item in db.tbl_hospitalregs
                                         where item.h_hakkimid == quer1.h_id
                                         select item).First();
                            dt1.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                            //}
                        }

                    }
                    if (dt1.Rows.Count > 0)
                    {

                        GridView1.DataSource = dt1;
                        GridView1.DataBind();

                    }
                    else
                    {
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                        Panel3.Visible = false;
                        //Label2.Text = "No data";
                        //this.ModalPopupExtender2.Show();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    }
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                }

            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlHospital.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            where item.u_id == Session["hakkemid_u"].ToString() && item.d_id == DdlDoctorName.SelectedValue && item.h_id == DdlHospital.SelectedValue && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate)
                            select item;

                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        string a_date = ss.a_date.ToString();
                        //dt.Rows.Add(ss.a_date, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.d_id, ss.h_id);
                        if (ss.h_id == null)
                        {
                            var quer1 = (from item in db.tbl_doctors
                                         where item.d_hakkimid == ss.d_id
                                         select item).First();
                            //foreach(var doc in quer1 )
                            //{
                            dt1.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                            //}
                        }
                        else
                        {
                            var quer1 = (from item in db.tbl_hdoctors
                                         where item.hd_email == ss.d_id
                                         select item).First();
                            //foreach (var doc in quer1)
                            //{
                            var quer2 = (from item in db.tbl_hospitalregs
                                         where item.h_hakkimid == quer1.h_id
                                         select item).First();
                            dt1.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                            //}
                        }
                    }
                    if (dt1.Rows.Count > 0)
                    {

                        GridView1.DataSource = dt1;
                        GridView1.DataBind();

                    }
                    else
                    {
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                        Panel3.Visible = false;
                        //Label2.Text = "No data";
                        //this.ModalPopupExtender2.Show();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    }
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                }

            }
            else
            {
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    // For From date only
    public void GettingHistoryByFromDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });

            string date = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString() && item.a_date == DateTime.Parse(date).Date
                        orderby item.a_date, item.a_time descending
                        select item;
            if (query.Count() > 0)
            {
                //GridView1.DataSource = query.ToList();
                //GridView1.DataBind();
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
                //foreach (GridViewRow grow in GridView1.Rows)
                //{
                //    Label LblHosId1 = grow.FindControl("LblHosId1") as Label;
                //    Label LblDocId = grow.FindControl("LblDocId") as Label;
                //    Label LblDoctorName = grow.FindControl("LblDoctorName") as Label;
                //    Label LblHospital = grow.FindControl("LblHospital") as Label;

                //    if (LblHosId1.Text == "")
                //    {
                //        var doc = (from item in db.tbl_doctors
                //                   where item.d_email == LblDocId.Text
                //                   select item).First();

                //        LblDoctorName.Text = doc.d_name.ToString();
                //        LblHospital.Text = "-----";
                //    }
                //    else
                //    {
                //        var doc = (from item in db.tbl_hdoctors
                //                   where item.hd_email == LblDocId.Text && item.h_id == LblHosId1.Text
                //                   select item).First();

                //        var hos = (from item in db.tbl_hospitalregs
                //                   where item.h_regno == LblHosId1.Text
                //                   select item).First();

                //        LblHospital.Text = hos.h_name.ToString();
                //        LblDoctorName.Text = doc.hd_name.ToString();
                //    }

                //}

            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    // For Form date and hospital
    public void GettingHistoryByFromDateHospital()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });

            string date = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString() && item.a_date == DateTime.Parse(date).Date && item.h_id == DdlHospital.SelectedValue
                        orderby item.a_date, item.a_time descending
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    // For From date and doctor
    public void GettingHistoryByFromDateDoctor()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });

            string date = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString() && item.a_date == DateTime.Parse(date).Date && item.d_id == DdlDoctorName.SelectedValue
                        orderby item.a_date, item.a_time descending
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    // For doctor only
    public void GettingHistoryByDoctor()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });


            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString() && item.d_id == DdlDoctorName.SelectedValue
                        orderby item.a_date, item.a_time descending
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    // for Hospital only
    public void GettingHistoryByHospital()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });

            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString() && item.h_id == DdlHospital.SelectedValue
                        orderby item.a_date, item.a_time descending
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    // for hospital and doctor
    public void GettingHistoryByHospitalDoctor()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("id"), new DataColumn("a_date"), new DataColumn("h_id"), new DataColumn("d_id"), new DataColumn("a_time"), new DataColumn("h_name"), new DataColumn("d_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_doc_prescriptions") });


            var query = from item in db.tbl_hos_appmnt_histories
                        where item.u_id == Session["hakkemid_u"].ToString() && item.h_id == DdlHospital.SelectedValue && item.d_id == DdlDoctorName.SelectedValue
                        orderby item.a_date, item.a_time descending
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_date = ss.a_date.ToString();
                    if (ss.h_id == null)
                    {
                        var quer1 = (from item in db.tbl_doctors
                                     where item.d_hakkimid == ss.d_id
                                     select item).First();
                        //foreach(var doc in quer1 )
                        //{
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, "----", quer1.d_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                    else
                    {
                        var quer1 = (from item in db.tbl_hdoctors
                                     where item.hd_email == ss.d_id
                                     select item).First();
                        //foreach (var doc in quer1)
                        //{
                        var quer2 = (from item in db.tbl_hospitalregs
                                     where item.h_hakkimid == quer1.h_id
                                     select item).First();
                        dt.Rows.Add(ss.id, DateTime.Parse(a_date).ToShortDateString(), ss.h_id, ss.d_id, ss.a_time, quer2.h_name, quer1.hd_name, ss.a_doc_daignose, ss.a_doc_prescriptions);
                        //}
                    }
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    #endregion

    //Methods For export to pdf 
    #region Pdf CreAtion

    public void ExportToPdfDateDoctorHospital(string reportName, string fromDate, string toDate, string docName, string hosName)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename='" + reportName + "'");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        StringBuilder sb = new StringBuilder();
        sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>CONSULTED REPORT</b></td></tr>");

        // checking whether only from and to daates are selected.
        if (fromDate != "" && toDate != "" && docName == "" && hosName == "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>From : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>To : ");
            sb.Append(toDate);
            sb.Append("</td></tr>");
        }
        // checking whether only from date is selected
        else if (fromDate != "" && toDate == "" && docName == "" && hosName == "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>");
            sb.Append("</td></tr>");
        }

        // checking whether only from date and doctor name is selected
        else if (fromDate != "" && toDate == "" && docName != "" && hosName == "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>Doctor Name : ");
            sb.Append(docName.ToUpper());
            sb.Append("</td></tr>");
        }

        // checking whether only from to date and doctor name is selected
        else if (fromDate != "" && toDate != "" && docName != "" && hosName == "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>From : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>To : ");
            sb.Append(toDate);
            sb.Append("</td></tr>");
            sb.Append("<tr><td align='center'>Doctor Name : ");
            sb.Append(docName.ToUpper());
            sb.Append("<tr><td align='center'>");
            sb.Append("</td></tr>");
        }

        // checking whether only from to date and hospital is selected
        else if (fromDate != "" && toDate != "" && docName == "" && hosName != "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>From : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>To : ");
            sb.Append(toDate);
            sb.Append("</td></tr>");
            sb.Append("<tr><td align='center'>Hospital : ");
            sb.Append(hosName.ToUpper());
            sb.Append("</td><td align='center'>");
            sb.Append("</td></tr>");
        }

        // checking whether only from date and hospital is selected
        else if (fromDate != "" && toDate == "" && docName == "" && hosName != "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>Hospital : ");
            sb.Append(hosName.ToUpper());
            sb.Append("</td></tr>");
        }

        // checking whether only hospital is selected
        else if (fromDate == "" && toDate == "" && docName == "" && hosName != "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(DateTime.Now.ToShortDateString());
            sb.Append("</td><td align='center'>Hospital : ");
            sb.Append(hosName.ToUpper());
            sb.Append("</td></tr>");
        }
        // checking whether only doctor is selected
        else if (fromDate == "" && toDate == "" && docName != "" && hosName == "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(DateTime.Now.ToShortDateString());
            sb.Append("</td><td align='center'>Doctor : ");
            sb.Append(docName.ToUpper());
            sb.Append("</td></tr>");
        }
        // checking whether only doctor and hospital is selected
        else if (fromDate == "" && toDate == "" && docName != "" && hosName != "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Hospital : ");
            sb.Append(hosName.ToUpper());
            sb.Append("</td><td align='center'>Doctor : ");
            sb.Append(docName.ToUpper());
            sb.Append("</td></tr>");
        }

        // checking whether all fields are selected
        else
        {
            sb.Append("<tr><td align='center'>From : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>To : ");
            sb.Append(toDate);
            sb.Append("</td></tr>");
            sb.Append("<tr><td align='center'>Doctor Name : ");
            sb.Append(docName.ToUpper());
            sb.Append("</td><td align='center'>Hospital : ");
            sb.Append(hosName.ToUpper());
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
    #endregion



}