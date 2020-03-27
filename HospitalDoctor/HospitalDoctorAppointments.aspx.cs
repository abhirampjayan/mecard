using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class HospitalDoctor_HospitalDoctorAppointments : System.Web.UI.Page
{
    secure obj = new secure();
    databaseDataContext db = new databaseDataContext();
    string DocId = "";
    string HosId = "";
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HosDocId"] != null)
        {
            DocId = Session["HosDocId"].ToString();
            HosId = Session["HospitalId"].ToString();
           
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
            SelectApointments();
        }

    }

    public void SelectApointments()
    {
        try
        {
            var query = from item in db.tbl_hos_doc_appmnts
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid

                        where item.d_id == DocId && item.h_id == HosId && item.a_status==1
                        orderby item.a_date, item.a_time ascending
                        select new {item.a_time,item.a_date,item.a_reason,item1.name };
            if (query.Count() > 0)
            {
                GridView1.DataSource = query.ToList();
                GridView1.DataBind();
            }
            else
            {
                //Label1.Text = "You have no appointments";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Appoitments')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا تعيينات')</Script>");
                //}
            }

        }
        catch(Exception ex)
        {
            //Response.Write(ex);
        }
    }

    public void SelectApointmentsByDate()
    {
        try
        {
            var query = from item in db.tbl_hos_doc_appmnts
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid

                        where item.d_id == DocId && item.h_id == HosId && item.a_status == 1 && item.a_date==DateTime.Parse(TxtSearchDate.Text).ToString("yyyy-MM-dd")
                        orderby item.a_date, item.a_time ascending
                        select new { item.a_time, item.a_date, item.a_reason, item1.name };
            if (query.Count() > 0)
            {
                GridView1.DataSource = query.ToList();
                GridView1.DataBind();
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Appointments')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا تعيينات')</Script>");
                //}
                //Label1.Text = "You have no appointments";
                //this.ModalPopupExtender2.Show();
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if(TxtSearchDate.Text=="")
        {
        GridView1.PageIndex = e.NewPageIndex;
        SelectApointments();
        }
        if (TxtSearchDate.Text != "")
        {
            GridView1.PageIndex = e.NewPageIndex;
            SelectApointmentsByDate();
        }
    }
    protected void BtnSearchPatient_Click(object sender, EventArgs e)
    {
        SelectApointmentsByDate();
    }
    protected void BtnViewAll_Click(object sender, EventArgs e)
    {
        SelectApointments();
    }
}