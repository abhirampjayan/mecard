using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_appointmenthistory : System.Web.UI.Page
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
        //    this.MasterPageFile = "~/BookDoc Admin/AdminArabicMasterPage.master";
        //}
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //con.Open();
        if (!IsPostBack)
        {
            history();
        }
    }
    public void history()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("doctor"), new DataColumn("hospital"), new DataColumn("patient"), new DataColumn("date"), new DataColumn("time"), new DataColumn("reason"), new DataColumn("usertype"), new DataColumn("apmntid") });

            var cancel = from item in db.tbl_apmnt_cancels orderby item.id descending select item;
            foreach(var ss in cancel)
            {
                dt.Rows.Add(ss.d_id, ss.hos_id, ss.u_id, ss.date, ss.time, ss.cancel_rsn, ss.canceled_by, ss.apmnt_id);
            }

            //var hakkeemdoc = from item in db.tbl_doctor_appointments select item;
            //foreach (var ss in hakkeemdoc)
            //{
            //    SqlCommand com = new SqlCommand("select * from tbl_cancel where app_id='" + ss.id + "'", con);
            //    SqlDataReader dtr = com.ExecuteReader();
            //    if (dtr.HasRows)
            //    {
            //        while (dtr.Read())
            //        {
            //            dt.Rows.Add(ss.d_id, "", ss.c_id, ss.a_date, ss.a_time, "", ss.id);
            //        }
            //    }
            //}
            //var hospitalodc = from item in db.tbl_hos_doc_appmnts select item;
            //foreach (var ss in hospitalodc)
            //{
            //    SqlCommand com = new SqlCommand("select * from tbl_hoscancel where app_id='" + ss.id + "'", con);
            //    SqlDataReader dtr = com.ExecuteReader();
            //    if (dtr.HasRows)
            //    {
            //        while (dtr.Read())
            //        {
            //            dt.Rows.Add(ss.d_id, ss.h_id, ss.u_id, ss.a_date, ss.a_time, "", ss.id);
            //        }
            //    }
            //}

            GridView1.DataSource = dt;
            GridView1.DataBind();

            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lbl3 = gr.FindControl("Label3") as Label;
                Label lbl1 = gr.FindControl("Label1") as Label;
                Label lbl2 = gr.FindControl("Label2") as Label;
                Label lbl4 = gr.FindControl("Label4") as Label;
                Label lbl5 = gr.FindControl("Label5") as Label;
                Label lbl6 = gr.FindControl("Label6") as Label;
                Label lbl11 = gr.FindControl("Label11") as Label;
                Label lbl12 = gr.FindControl("Label12") as Label;
                Label lbl14 = gr.FindControl("Label14") as Label;
                Label lbl10 = gr.FindControl("Label10") as Label;
                Label lbl15 = gr.FindControl("Label15") as Label;

                if (lbl3.Text == "")
                {
                    lbl4.Text = "--------";
                    var doctor = (from item in db.tbl_doctors where item.d_hakkimid == lbl1.Text select item).First();
                    lbl2.Text = doctor.d_name;

                    var dapmnt = (from item in db.tbl_doctor_appointments where item.id == int.Parse(lbl10.Text) select item).First();
                    lbl15.Text = dapmnt.a_date + " " + dapmnt.a_time;
                }
                else
                {
                    var hospital = (from item in db.tbl_hospitalregs where item.h_hakkimid == lbl3.Text select item).First();
                    lbl4.Text = hospital.h_name;
                    var doctor = (from item in db.tbl_hdoctors where item.hd_email == lbl1.Text select item).First();
                    lbl2.Text = doctor.hd_name;

                    var dapmnt = (from item in db.tbl_hos_doc_appmnts where item.id == int.Parse(lbl10.Text) select item).First();
                    lbl15.Text = dapmnt.a_date + " " + dapmnt.a_time;
                }

                var patient = (from item in db.tbl_signups where item.u_hakkimid == lbl5.Text select item).First();
                lbl6.Text = patient.name;
                if(lbl14.Text=="u")
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        lbl12.Text = "Patient";
                    //}
                    //else
                    //{
                    //    lbl12.Text = "صبور";
                    //}
                }
                else if(lbl14.Text=="d")
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        lbl12.Text = "Doctor";
                    //}
                    //else
                    //{
                    //    lbl12.Text = "طبيب";
                    //}
                }
                else if(lbl14.Text=="h")
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        lbl12.Text = "Hospital";
                    //}
                    //else
                    //{
                    //    lbl12.Text = "مستشفى";
                    //}
                }
                else if (lbl14.Text == "hkm")
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        lbl12.Text = "Hakkeem authority";
                    //}
                    //else
                    //{
                    //    lbl12.Text = "سلطة الحكيم";
                    //}
                }
                //if(lbl11.Text=="2")
                //{
                //    lbl12.Text = "Canceled";
                //}
                //else if(lbl11.Text=="3")
                //{
                //    lbl12.Text = "Consulted";
                //}

                //SqlCommand com = new SqlCommand("select dt from tbl_cancel where app_id='" + lbl10.Text + "'", con);
                //string dtt = com.ExecuteScalar().ToString();

                //lbl12.Text = "Canceled on " + dtt.ToString();
            }
        }
        catch(Exception ex)
        {

        }

    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        history();
    }
}