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
using System.Data.SqlClient;
using System.Configuration;

public partial class Hospital_HospitalReports : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

    protected override void InitializeCulture()
    {
        //Session["Language"] = "Auto";
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
        if(Session["hakkeemid_h"] ==null)
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
        if(!IsPostBack)
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
            BindDoctorsName();
            BindDropdown();
        }
    }

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                    select new { item1.h_id, item.latitude };
        try
        {
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
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}

    #region CodeForViewReport
    protected void BtnViewReport_Click(object sender, EventArgs e)
    {
        // From, To, Diagnose selected
        if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <=0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDiagnose();
        }

         // only Form date selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <=0)
        {
            Panel3.Visible = true;
            GetHistoryBysingleDate();
        }

        // if from and to dates are selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDate();
        }

        // if from date and diagnose selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByFromDateDiagnose();
        }

        // if only diagnose selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDiagnose();
        }

        // Only doctor name selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex >0)
        {
            Panel3.Visible = true;
            GetHistoryByDoctorName();
        }

            // Only from date and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByFromDateDoctorName();
        }

            // Only from to dates and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDoctorName();
        }

            // Only  from to dates, diagnose and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDiagnoseDoctorName();
        }

          // Only  diagnose and doctor name selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDiagnoseDoctorName();
        }
         

        // if no fields selected
        else
        {
            //RegisterStartupScript("", "<script>alert('Please select any options to take report')</script>");
            Panel3.Visible = false;
            //Label2.Text = "Please select any options to take report";
            //this.ModalPopupExtender2.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any options to take report')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد أي خيارات لاتخاذ التقرير')</Script>");
            //}
        }


        TxtBookDocId.Text = "";
    }

    protected void BtnViewReportId_Click(object sender, EventArgs e)
    {
        Panel3.Visible = true;
        GetHistoryByBookDocId();
        TxtFrom.Text = TxtTo.Text = "";
        DdlDoctorName.SelectedIndex = DdlDiagnose.SelectedIndex = 0;
    }
    #endregion


    protected void BtnPrintDownload_Click(object sender, EventArgs e)
    {
        string name = "Consulted History" + " " + DateTime.Now.ToShortDateString() + ".pdf";


        // if BookDoc id is selected
        if (TxtBookDocId.Text != "" && BtnPrintDownload.CommandArgument.ToString() =="")
        {
            string fileName = "Consulted History" + " " + DateTime.Now.ToShortDateString() + ".pdf";
            string patientName = "";
            
            try
            {
                var query= from item in db.tbl_signups
                           where (item.email==TxtBookDocId.Text||item.u_hakkimid==TxtBookDocId.Text)
                           select item;
                foreach(var ss in query)
                {
                    patientName=ss.name;
                }
            }
            catch(Exception ex)
            {
            }

            GetHistoryByBookDocId();
            ExportToPdfBookDocId(fileName, patientName);
            TxtBookDocId.Text = "";
            Panel3.Visible = false;
        }

        //  if dates and diagnose are selected
        else
        {
            TxtBookDocId.Text = "";
            // check whether from to dates, diagnose selected
            if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                Panel3.Visible = true;
                GetHistoryByDateDiagnose();
                ExportToPdfForDateDiagnoseDoctorName(name,TxtFrom.Text,TxtTo.Text,"",DdlDiagnose.SelectedItem.Text);
            }

             // only Form date selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() =="")
            {
                Panel3.Visible = true;
                GetHistoryBysingleDate();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text,"","","") ;
            }

            // if from and to dates are selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() =="")
            {
                Panel3.Visible = true;
                GetHistoryByDate();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, TxtTo.Text, "","");
            }

            // if from date and diagnose selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                Panel3.Visible = true;
                GetHistoryByFromDateDiagnose();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, "", "", DdlDiagnose.SelectedItem.Text);
            }

            // if only diagnose selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                Panel3.Visible = true;
                GetHistoryByDiagnose();
                ExportToPdfDoctorNameDiagnose(name,"",(DdlDiagnose.SelectedItem.Text).ToUpper());
            }

                 // Only doctor name selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                Panel3.Visible = true;
                GetHistoryByDoctorName();
                ExportToPdfDoctorNameDiagnose(name, (DdlDoctorName.SelectedItem.Text).ToUpper(),"");
            }

                // Only from date and doctor name selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                Panel3.Visible = true;
                GetHistoryByFromDateDoctorName();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, "", DdlDoctorName.SelectedItem.Text,"");
            }

                // Only from to dates and doctor name selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                Panel3.Visible = true;
                GetHistoryByDateDoctorName();
                //ExportToPdfForDate(name);
                ExportToPdfForDateDiagnoseDoctorName(name,TxtFrom.Text,TxtTo.Text,DdlDoctorName.SelectedItem.Text,"");
            }

                // Only  from to dates, diagnose and doctor name selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                Panel3.Visible = true;
                GetHistoryByDateDiagnoseDoctorName();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, TxtTo.Text, DdlDoctorName.SelectedItem.Text, DdlDiagnose.SelectedItem.Text);
            }

               // Only  diagnose and doctor name selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0 && BtnPrintDownload.CommandArgument.ToString() == "")
            {
                GetHistoryByDiagnoseDoctorName();
                ExportToPdfDoctorNameDiagnose(name,(DdlDoctorName.SelectedItem.Text).ToUpper(), (DdlDiagnose.SelectedItem.Text).ToUpper());
            }

               // Only  from date and most patient button clicked
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && BtnPrintDownload.CommandArgument.ToString() == "Most Patient")
            {
                GettingHistoryOfMostVisitedPatient();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, "", "", "");
            }
            // Only  from date and most disease button clicked
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && BtnPrintDownload.CommandArgument.ToString() == "Most Disease")
            {
                GettingHistoryOfMostPatientVisitedDisease();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, "", "", "");
            }
            // Only  from date and most doctor button clicked
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && BtnPrintDownload.CommandArgument.ToString() == "Most Doctor")
            {
                GettingHistoryOfMostPatientVisitedDoctor();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, "", "", "");
            }

                 // from and to date and most patient button clicked
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && BtnPrintDownload.CommandArgument.ToString() == "Most Patient")
            {
                GettingHistoryOfMostVisitedPatient();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, TxtTo.Text, "", "");
            }
            // from and to date and most disease button clicked
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && BtnPrintDownload.CommandArgument.ToString() == "Most Disease")
            {
                GettingHistoryOfMostPatientVisitedDisease();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, TxtTo.Text, "", "");
            }
            //  from and to date and most doctor button clicked
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && BtnPrintDownload.CommandArgument.ToString() == "Most Doctor")
            {
                GettingHistoryOfMostPatientVisitedDoctor();
                ExportToPdfForDateDiagnoseDoctorName(name, TxtFrom.Text, TxtTo.Text, "", "");
            }

            

            // if no fields selected
            else
            {
                Panel3.Visible = false;
                //RegisterStartupScript("", "<script>alert('Please ')</script>");
            }
        }
    }

    protected void TxtTo_TextChanged(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        if (TxtFrom.Text != "" && TxtTo.Text != "")
        {
            DateTime toDate = DateTime.Parse(TxtTo.Text);
            DateTime fromDate = DateTime.Parse(TxtFrom.Text);
            //LblValidFrom.Visible = false;
            if (toDate < fromDate)
            {
               
                TxtTo.Text = "";
                TxtTo.Focus();
                //Label2.Text = "To date must be greater than or equal to from date.";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('To date must be greater than or equal to from date.')</script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('ويجب أن يكون التاريخ حتى الآن أكبر من أو يساوي من التاريخ')</script>");

                //}
            }
            else
            {
                DdlDiagnose.Focus();
                if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByDateDiagnose();
                }

                // only Form date selected
                else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
                {
                    Panel3.Visible = true;
                    GetHistoryBysingleDate();
                }

                // if from and to dates are selected
                else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByDate();
                }

                // if from date and diagnose selected
                else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByFromDateDiagnose();
                }

                // if only diagnose selected
                else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByDiagnose();
                }

                // Only doctor name selected
                else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByDoctorName();
                }

                // Only from date and doctor name selected
                else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByFromDateDoctorName();
                }

                // Only from to dates and doctor name selected
                else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByDateDoctorName();
                }

                // Only  from to dates, diagnose and doctor name selected
                else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByDateDiagnoseDoctorName();
                }

                // Only  diagnose and doctor name selected
                else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
                {
                    Panel3.Visible = true;
                    GetHistoryByDiagnoseDoctorName();
                }


                // if no fields selected
                else
                {
                    //RegisterStartupScript("", "<script>alert('Please select any options to take report')</script>");
                    Panel3.Visible = false;
                    //Label2.Text = "Please select any options to take report";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any options to take report')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد أي خيارات لاتخاذ التقرير')</Script>");
                    //}
                  //  RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any options to take report')</Script>");
                }


                TxtBookDocId.Text = "";
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
            if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GetHistoryByDateDiagnose();
            }

            // only Form date selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GetHistoryBysingleDate();
            }

            // if from and to dates are selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GetHistoryByDate();
            }

            // if from date and diagnose selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GetHistoryByFromDateDiagnose();
            }

            // if only diagnose selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
            {
                Panel3.Visible = true;
                GetHistoryByDiagnose();
            }

            // Only doctor name selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GetHistoryByDoctorName();
            }

            // Only from date and doctor name selected
            else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GetHistoryByFromDateDoctorName();
            }

            // Only from to dates and doctor name selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GetHistoryByDateDoctorName();
            }

            // Only  from to dates, diagnose and doctor name selected
            else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GetHistoryByDateDiagnoseDoctorName();
            }

            // Only  diagnose and doctor name selected
            else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
            {
                Panel3.Visible = true;
                GetHistoryByDiagnoseDoctorName();
            }


            // if no fields selected
            else
            {
                //RegisterStartupScript("", "<script>alert('Please select any options to take report')</script>");
                Panel3.Visible = false;
                //Label2.Text = "Please select any options to take report";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any options to take report')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد أي خيارات لاتخاذ التقرير')</Script>");
                //}

                //   RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any options to take report')</Script>");
            }


            TxtBookDocId.Text = "";
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
        // From, To, Diagnose selected
        if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
           
            GetHistoryByDateDiagnose();
        }

         // only Form date selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            GetHistoryBysingleDate();
        }

        // if from and to dates are selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            GetHistoryByDate();
        }

        // if from date and diagnose selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
           GetHistoryByFromDateDiagnose();
        }

        // if only diagnose selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            GetHistoryByDiagnose();
        }

        // Only doctor name selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            GetHistoryByDoctorName();
        }

            // Only from date and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
           GetHistoryByFromDateDoctorName();
        }

            // Only from to dates and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            GetHistoryByDateDoctorName();
        }

            // Only  from to dates, diagnose and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
        {
            GetHistoryByDateDiagnoseDoctorName();
        }

        // Only  diagnose and doctor name selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
        {
            GetHistoryByDiagnoseDoctorName();
        }

        // if no fields selected
        else
        {
           
        }
    }

    public void BindDropdown()
    {
        try
        {
            var Query = from item in db.tbl_hos_appmnt_histories
                        where item.h_id == Session["hakkeemid_h"].ToString()
                        select item;
            if (Query.Count() > 0)
            {
                DdlDiagnose.DataSource = Query.Distinct();
                DdlDiagnose.DataValueField = "id";
                DdlDiagnose.DataTextField = "a_doc_daignose";
                DdlDiagnose.DataBind();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    DdlDiagnose.Items.Insert(0, "--select--");
                //}
                //else
                //{
                //    DdlDiagnose.Items.Insert(0, "--تحديد--");
                //}
            }
            else
            {
                DdlDiagnose.Items.Clear();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    DdlDiagnose.Items.Insert(0, "--select--");
                //}
                //else
                //{
                //    DdlDiagnose.Items.Insert(0, "--تحديد--");
                //}
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void BindDoctorsName()
    {
        try
        {
            var Query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString()
                        select item;
            if (Query.Count() > 0)
            {
                DdlDoctorName.DataSource = Query.Distinct();
                DdlDoctorName.DataValueField = "hd_email";
                DdlDoctorName.DataTextField = "hd_name";
                DdlDoctorName.DataBind();
                //if (Session["Language"].ToString() == "Auto")
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
                DdlDoctorName.Items.Clear();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    DdlDoctorName.Items.Insert(0, "--select--");
                //}
                //else
                //{
                //    DdlDoctorName.Items.Insert(0, "--تحديد--");
                //}
            }
        }
        catch (Exception ex)
        {
        }
    }

    // Methods for getting consulted histories based conditions

    #region GettingHistories

    public void GetHistoryByDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"), new DataColumn("doc_name"),new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && DateTime.Parse(fromDate) <=item.a_date && item.a_date <=DateTime.Parse(toDate)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_end_time, item.a_doc_prescriptions, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name , item1.u_hakkimid };

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}

                   
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                }
            }
            else
            {
               
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
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
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name") ,new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });


            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && (item.u_id == TxtBookDocId.Text||item1.u_hakkimid==TxtBookDocId.Text)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
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
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name") ,new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text && DateTime.Parse(fromDate) <=item.a_date && item.a_date <=DateTime.Parse(toDate)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_end_time, item.a_doc_prescriptions, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name,item1.u_hakkimid };

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
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
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data.";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
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
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name") ,new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string date=DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == DateTime.Parse(date).Date
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data.";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
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
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string date=DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text && item.a_date == DateTime.Parse(date).Date
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByDoctorName()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name") ,new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == DdlDoctorName.SelectedValue
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByFromDateDoctorName()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name") ,new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string date=DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == DdlDoctorName.SelectedValue && item.a_date == DateTime.Parse(date).Date
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data.";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByDateDoctorName()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == DdlDoctorName.SelectedValue &&DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_end_time, item.a_doc_prescriptions, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }

                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data.";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByDateDiagnoseDoctorName()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text && item.d_id==DdlDoctorName.SelectedValue && DateTime.Parse(fromDate) <= item.a_date && item.a_date<= DateTime.Parse(toDate)
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_end_time, item.a_doc_prescriptions, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public void GetHistoryByDiagnoseDoctorName()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            var query = from item in db.tbl_hos_appmnt_histories
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.a_doc_daignose == DdlDiagnose.SelectedItem.Text && item.d_id== DdlDoctorName.SelectedValue
                        orderby item.a_date, item.a_time descending
                        select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var selectDoctorName = from item in db.tbl_hdoctors
                                           where item.hd_email == ss.d_id
                                           select item;
                    foreach (var s in selectDoctorName)
                    {
                        string consDate = ss.a_date.ToString();
                        dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data.')</Script>");
                Panel3.Visible = false;
                //Label2.Text = "No data.";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                //}
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
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = "";

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (TxtFrom.Text != "" && TxtTo.Text == "")
            {
                cmd.CommandText = "select top 1 u_id from tbl_hos_appmnt_history where h_id='" + Session["hakkeemid_h"].ToString() + "' and a_date='" + fromDate + "'  group by u_id order by COUNT(u_id) desc ";
                //cmd = new SqlCommand("select top 1 u_id from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL  and a_date between '" + DateTime.Parse(fromDate) + "' and '" + DateTime.Parse(toDate) + "'  group by u_id order by COUNT(u_id) desc ", con);
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");
                cmd.CommandText = "select top 1 u_id from tbl_hos_appmnt_history where h_id='" + Session["hakkeemid_h"].ToString() + "' and a_date between '" + fromDate + "' and '" + toDate + "'  group by u_id order by COUNT(u_id) desc ";
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
                            join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                            where item.u_id == u_id && item.a_date == DateTime.Parse(fromDate) && item.h_id == Session["hakkeemid_h"].ToString() 
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        var selectDoctorName = from item in db.tbl_hdoctors
                                               where item.hd_email == ss.d_id
                                               select item;
                        foreach (var s in selectDoctorName)
                        {
                            string consDate = ss.a_date.ToString();
                            dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(),s.hd_name ,ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                        }

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
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                        Panel3.Visible = false;
                        //Label2.Text = "No data";
                        //this.ModalPopupExtender2.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                        //}
                    }
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                            where item.u_id == u_id && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate) && item.h_id == Session["hakkeemid_h"].ToString()
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        var selectDoctorName = from item in db.tbl_hdoctors
                                               where item.hd_email == ss.d_id
                                               select item;
                        foreach (var s in selectDoctorName)
                        {
                            string consDate = ss.a_date.ToString();
                            dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                        }
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
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                        Panel3.Visible = false;
                        //Label2.Text = "No data.";
                        //this.ModalPopupExtender2.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                        //}
                    }
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data.";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
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
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"),new DataColumn("doc_name") ,new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = "";

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (TxtFrom.Text != "" && TxtTo.Text == "")
            {
                cmd.CommandText = "select top 1 a_doc_daignose from tbl_hos_appmnt_history where h_id='" + Session["hakkeemid_h"].ToString() + "' and a_date= '" + fromDate + "'  group by a_doc_daignose order by COUNT(a_doc_daignose) desc";
                //SqlCommand cmd = new SqlCommand("select top 1 a_doc_daignose from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL and a_date between '" + DateTime.Parse(fromDate) + "' and '" + DateTime.Parse(toDate) + "'  group by a_doc_daignose order by COUNT(a_doc_daignose) desc ", con);
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");
                cmd.CommandText = "select top 1 a_doc_daignose from tbl_hos_appmnt_history where h_id='" + Session["hakkeemid_h"].ToString() + "' and a_date between '" + fromDate + "' and '" + toDate + "' group by a_doc_daignose order by COUNT(a_doc_daignose) desc";
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
                            join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                            where item.a_doc_daignose == diseaseName && item.a_date == DateTime.Parse(fromDate) && item.h_id == Session["hakkeemid_h"].ToString() 
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        var selectDocName = from item in db.tbl_hdoctors
                                            where item.hd_email == ss.d_id
                                            select item;
                        foreach (var s in selectDocName)
                        {
                            string consDate = ss.a_date.ToString();
                            dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                        }

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
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                        Panel3.Visible = false;
                        //Label2.Text = "No data";
                        //this.ModalPopupExtender2.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                        //}
                    }
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                            where item.a_doc_daignose == diseaseName && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate) && item.h_id == Session["hakkeemid_h"].ToString()
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        var selectDoctorName = from item in db.tbl_hdoctors
                                               where item.hd_email == ss.d_id
                                               select item;

                        foreach (var s in selectDoctorName)
                        {
                            string consDate = ss.a_date.ToString();
                            dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                        }
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
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                        Panel3.Visible = false;
                        //Label2.Text = "No data";
                        //this.ModalPopupExtender2.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                        //}
                    }
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data.";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    private void GettingHistoryOfMostPatientVisitedDoctor()
    {
        try
        {
            string d_id = "";
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("a_date"), new DataColumn("doc_name"), new DataColumn("a_doc_daignose"), new DataColumn("a_end_time"), new DataColumn("a_doc_prescriptions"), new DataColumn("a_time"), new DataColumn("id"), new DataColumn("u_id"), new DataColumn("name") });

            string fromDate = DateTime.Parse(TxtFrom.Text).ToString("yyyy-MM-dd");
            string toDate = "";

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (TxtFrom.Text != "" && TxtTo.Text == "")
            {
                cmd.CommandText = "select top 1 d_id from tbl_hos_appmnt_history where h_id='" + Session["hakkeemid_h"].ToString() + "' and a_date='" + fromDate + "'  group by d_id order by COUNT(d_id) desc ";
                //cmd = new SqlCommand("select top 1 u_id from tbl_hos_appmnt_history where d_id='" + Session["doctor"].ToString() + "' and h_id IS NULL  and a_date between '" + DateTime.Parse(fromDate) + "' and '" + DateTime.Parse(toDate) + "'  group by u_id order by COUNT(u_id) desc ", con);
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                toDate = DateTime.Parse(TxtTo.Text).ToString("yyyy-MM-dd");
                cmd.CommandText = "select top 1 d_id from tbl_hos_appmnt_history where h_id='" + Session["hakkeemid_h"].ToString() + "' and a_date between '" + fromDate + "' and '" + toDate + "'  group by d_id order by COUNT(d_id) desc ";
            }

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                d_id = dr["d_id"].ToString();
            }
            dr.Close();
            con.Close();

            if (TxtFrom.Text != "" && TxtTo.Text == "")
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                            where item.d_id == d_id && item.a_date == DateTime.Parse(fromDate) && item.h_id == Session["hakkeemid_h"].ToString()
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        var selectDoctorName = from item in db.tbl_hdoctors
                                               where item.hd_email == ss.d_id
                                               select item;
                        foreach (var s in selectDoctorName)
                        {
                            string consDate = ss.a_date.ToString();
                            dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                        }

                    }
                    if (dt.Rows.Count > 0)
                    {
                        //this.ModalPopupExtender1.Hide();
                        Panel3.Visible = true;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        BtnPrintDownload.CommandArgument = BtnMostDoctor.Text;
                    }
                    else
                    {
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                        Panel3.Visible = false;
                        //Label2.Text = "No data";
                        //this.ModalPopupExtender2.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                        //}
                    }
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
            else if (TxtFrom.Text != "" && TxtTo.Text != "")
            {
                var query = from item in db.tbl_hos_appmnt_histories
                            join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                            where item.d_id == d_id && DateTime.Parse(fromDate) <= item.a_date && item.a_date <= DateTime.Parse(toDate) && item.h_id == Session["hakkeemid_h"].ToString()
                            orderby item.a_date, item.a_time descending
                            select new { item.a_date, item.a_doc_daignose, item.a_doc_prescriptions, item.a_end_time, item.a_time, item.d_id, item.h_id, item.id, item.status, item.u_id, item1.name, item1.u_hakkimid };
                if (query.Count() > 0)
                {
                    foreach (var ss in query)
                    {
                        var selectDoctorName = from item in db.tbl_hdoctors
                                               where item.hd_email == ss.d_id
                                               select item;
                        foreach (var s in selectDoctorName)
                        {
                            string consDate = ss.a_date.ToString();
                            dt.Rows.Add(DateTime.Parse(consDate).ToShortDateString(), s.hd_name, ss.a_doc_daignose, ss.a_end_time, ss.a_doc_prescriptions, ss.a_time, ss.id, ss.u_id, ss.name);
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        //this.ModalPopupExtender1.Hide();
                        Panel3.Visible = true;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        BtnPrintDownload.CommandArgument = BtnMostDoctor.Text;
                    }
                    else
                    {
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                        Panel3.Visible = false;
                        //Label2.Text = "No data";
                        //this.ModalPopupExtender2.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                        //}
                    }
                }
                else
                {
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('No Data..')</Script>");
                    Panel3.Visible = false;
                    //Label2.Text = "No data";
                    //this.ModalPopupExtender2.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No Data..')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا توجد بيانات ..')</Script>");
                    //}
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    // Methods for generating report as a pdf document

    #region Exports to Pdfs
    public void ExportToPdfForDate(string reportName)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename='" + reportName + "'");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        StringBuilder sb = new StringBuilder();
        sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>CONSULTED REPORT</b></td></tr>");
        sb.Append("<tr><td align='center'>From : ");
        sb.Append(TxtFrom.Text);
        sb.Append("</td><td align='center'>To : ");
        sb.Append(TxtTo.Text);
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

    public void ExportToPdfDoctorNameDiagnose(string reportName, string doctorName,string diagnose)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename='" + reportName + "'");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        StringBuilder sb = new StringBuilder();
        sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>CONSULTED REPORT</b></td></tr>");
        if (doctorName == "" && diagnose != "")
        {
            sb.Append("<tr><td align='center'>Diagnose : ");
            sb.Append(diagnose);
            sb.Append("</td><td align='center'>Date : ");
            sb.Append(DateTime.Now.ToShortDateString());
            sb.Append("</td></tr>");
        }
        else if (doctorName != "" && diagnose == "")
        {
            sb.Append("<tr><td align='center'>Doctor Name : ");
            sb.Append(doctorName);
            sb.Append("</td><td align='center'>Date : ");
            sb.Append(DateTime.Now.ToShortDateString());
            sb.Append("</td></tr>");
        }
        else
        {
            sb.Append("<tr><td align='center'>Doctor Name : ");
            sb.Append(doctorName);
            sb.Append("</td><td align='center'>Diagnose : ");
            sb.Append(diagnose);
            sb.Append("</td><td align='center'>Date : ");
            sb.Append(DateTime.Now.ToShortDateString());
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

    public void ExportToPdfForDateDiagnoseDoctorName(string reportName,string fromDate,string toDate,string docName,string diagnose)
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
        if(fromDate !="" && toDate !="" && docName =="" && diagnose =="" && reportName !="")
        {
        sb.Append("<tr><td align='center'>From : ");
        sb.Append(fromDate);
        sb.Append("</td><td align='center'>To : ");
        sb.Append(toDate);
        sb.Append("</td></tr>");
        }
            // checking whether only from date is selected
        else if (fromDate != "" && toDate == "" && docName == "" && diagnose == "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(fromDate);
             sb.Append("</td><td align='center'>");
             sb.Append("</td></tr>");
        }

            // checking whether only from date and doctor name is selected
        else if (fromDate != "" && toDate == "" && docName != "" && diagnose == "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>Doctor Name : ");
             sb.Append(docName.ToUpper());
             sb.Append("</td></tr>");
        }

            // checking whether only from to date and doctor name is selected
        else if (fromDate != "" && toDate != "" && docName != "" && diagnose == "" && reportName != "")
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

            // checking whether only from to date and diagnose is selected
        else if (fromDate != "" && toDate != "" && docName == "" && diagnose != "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>From : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>To : ");
            sb.Append(toDate);
            sb.Append("</td></tr>");
            sb.Append("<tr><td align='center'>Diagnose : ");
            sb.Append(diagnose.ToUpper());
            sb.Append("</td><td align='center'>");
            sb.Append("</td></tr>");
        }

        // checking whether only from date and diagnose is selected
        else if (fromDate != "" && toDate == "" && docName == "" && diagnose != "" && reportName != "")
        {
            sb.Append("<tr><td align='center'>Date : ");
            sb.Append(fromDate);
            sb.Append("</td><td align='center'>Diagnose : ");
            sb.Append(diagnose.ToUpper());
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
            sb.Append("</td><td align='center'>Diagnose : ");
            sb.Append(diagnose.ToUpper());
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
    protected void BtnMostDoctor_Click(object sender, EventArgs e)
    {
        TxtBookDocId.Text = "";
        GettingHistoryOfMostPatientVisitedDoctor();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/SetHospitalLocation.aspx?l=ar-EG");
        //}
    }

    protected void DdlDiagnose_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDiagnose();
        }

        // only Form date selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryBysingleDate();
        }

        // if from and to dates are selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDate();
        }

        // if from date and diagnose selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByFromDateDiagnose();
        }

        // if only diagnose selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDiagnose();
        }

        // Only doctor name selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDoctorName();
        }

        // Only from date and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByFromDateDoctorName();
        }

        // Only from to dates and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDoctorName();
        }

        // Only  from to dates, diagnose and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDiagnoseDoctorName();
        }

        // Only  diagnose and doctor name selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDiagnoseDoctorName();
        }


        // if no fields selected
        else
        {
            //RegisterStartupScript("", "<script>alert('Please select any options to take report')</script>");
            Panel3.Visible = false;
            //Label2.Text = "Please select any options to take report";
            //this.ModalPopupExtender2.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any options to take report')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد أي خيارات لاتخاذ التقرير')</Script>");
            //}
            
        }


        TxtBookDocId.Text = "";
    }

    protected void DdlDoctorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDiagnose();
        }

        // only Form date selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryBysingleDate();
        }

        // if from and to dates are selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDate();
        }

        // if from date and diagnose selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByFromDateDiagnose();
        }

        // if only diagnose selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex <= 0)
        {
            Panel3.Visible = true;
            GetHistoryByDiagnose();
        }

        // Only doctor name selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDoctorName();
        }

        // Only from date and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByFromDateDoctorName();
        }

        // Only from to dates and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex <= 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDoctorName();
        }

        // Only  from to dates, diagnose and doctor name selected
        else if (TxtFrom.Text != "" && TxtTo.Text != "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDateDiagnoseDoctorName();
        }

        // Only  diagnose and doctor name selected
        else if (TxtFrom.Text == "" && TxtTo.Text == "" && DdlDiagnose.SelectedIndex > 0 && DdlDoctorName.SelectedIndex > 0)
        {
            Panel3.Visible = true;
            GetHistoryByDiagnoseDoctorName();
        }


        // if no fields selected
        else
        {
            //RegisterStartupScript("", "<script>alert('Please select any options to take report')</script>");
            Panel3.Visible = false;
            //Label2.Text = "Please select any options to take report";
            //this.ModalPopupExtender2.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select any options to take report')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد أي خيارات لاتخاذ التقرير')</Script>");
            //}
        }


        TxtBookDocId.Text = "";
    }
}