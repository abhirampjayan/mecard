using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_read_report_form : System.Web.UI.Page
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
        //    this.MasterPageFile = "~/BookDoc Admin/AdminArabicMasterPage.master";
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            apointment();
        }
    }

    public void apointment()
    {
        var Query = from item in db.tbl_report_forms where item.id == int.Parse(Session["apmntid"].ToString()) select item;
        DetailsView1.DataSource = Query;
        DetailsView1.DataBind();

        DetailsView2.DataSource = Query;
        DetailsView2.DataBind();

        foreach (DetailsViewRow dr in DetailsView1.Rows)
        {
            Label lbl8 = dr.FindControl("Label8") as Label;
            Label lbl9 = dr.FindControl("Label9") as Label;
            Label lbl13 = dr.FindControl("Label13") as Label;
            Label lbl10 = dr.FindControl("Label10") as Label;
            Label lbl11 = dr.FindControl("Label11") as Label;
            Label lbl12 = dr.FindControl("Label12") as Label;
            var user = from itemk in db.tbl_signups where itemk.u_hakkimid == lbl8.Text select itemk;
            foreach (var ss in user)
            {
                lbl9.Text = ss.name;
            }

            if (lbl13.Text == "")
            {

                var report = (from item in db.tbl_report_forms where item.id == int.Parse(Session["apmntid"].ToString()) select item.apmnt_id).First();


                var doctor = from item in db.tbl_doctor_appointments where item.id == int.Parse(report.ToString()) select item;
                foreach (var ss in doctor)
                {
                    var doc = from item in db.tbl_doctors where item.d_hakkimid == ss.d_id select item;
                    foreach (var d in doc)
                    {
                        lbl10.Text = d.d_name;
                        lbl11.Text = d.d_hakkimid;
                    }
                }
                lbl12.Text = "---";
                lbl13.Text = "---";
            }
            else
            {

                var report = (from item in db.tbl_report_forms where item.id == int.Parse(Session["apmntid"].ToString()) select item.apmnt_id).First();
                var hospital = from item in db.tbl_hospitalregs where item.h_hakkimid == lbl13.Text select item;
                foreach (var ss in hospital)
                {
                    lbl12.Text = ss.h_name;
                }
                var doctor = from item in db.tbl_hos_doc_appmnts where item.id == int.Parse(report.ToString()) select item;
                foreach (var d in doctor)
                {
                    var doc = (from item in db.tbl_hdoctors where item.hd_email == d.d_id && item.h_id == lbl13.Text select item.hd_name).First();

                    lbl10.Text = doc.ToString();
                    lbl11.Text = "---";
                }
            }

            //var apmnt = from item in db.


        }

        foreach (DetailsViewRow dr in DetailsView2.Rows)
        {

            Label lbl14 = dr.FindControl("Label14") as Label;
            Label lbl15 = dr.FindControl("Label15") as Label;
            Label lbl16 = dr.FindControl("Label16") as Label;

            if(lbl14.Text=="")
            {
                var report = (from item in db.tbl_report_forms where item.id == int.Parse(Session["apmntid"].ToString()) select item.apmnt_id).First();
                var apmnt = from item in db.tbl_doctor_appointments where item.id == int.Parse(report.ToString()) select item;
                foreach(var ss in apmnt)
                {
                    lbl15.Text = ss.app_date;
                    lbl16.Text = ss.app_time;
                }
            }
            else
            {
                var report = (from item in db.tbl_report_forms where item.id == int.Parse(Session["apmntid"].ToString()) select item.apmnt_id).First();
                var apmnt = from item in db.tbl_hos_doc_appmnts where item.id == int.Parse(report.ToString()) select item;
                foreach (var ss in apmnt)
                {
                    lbl15.Text = ss.a_date;
                    lbl16.Text = ss.a_time;
                }
            }
        }

        var details = from item in db.tbl_report_forms where item.id == int.Parse(Session["apmntid"].ToString()) select item;
        foreach(var ss in details)
        {
            Label5.Text = ss.reason;
            Label7.Text = ss.description;
        }
    }
}