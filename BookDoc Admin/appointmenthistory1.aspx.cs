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
        if (!IsPostBack)
        {
            history();
        }
    }
    public void history()
    {
        try
        {
            // SqlDataAdapter adpt = new SqlDataAdapter("select * from tbl_doctor_appointment", con);
            //DataSet dts = new DataSet();
            //dts.Clear();
            //adpt.Fill(dts);
            //GridView1.DataSource = dts;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("d_id"), new DataColumn("h_id"), new DataColumn("c_id"), new DataColumn("a_date"), new DataColumn("a_time"), new DataColumn("a_status") });

           
            SqlDataAdapter adpt = new SqlDataAdapter("select * from tbl_doctor_appointment", con);
            DataSet dts = new DataSet();
            dts.Clear();
            adpt.Fill(dts);
            for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add(dts.Tables[0].Rows[i][1].ToString(), "", dts.Tables[0].Rows[i][2].ToString(), dts.Tables[0].Rows[i][3].ToString(), dts.Tables[0].Rows[i][4].ToString(), dts.Tables[0].Rows[i][5].ToString());
            }
            SqlDataAdapter adpt1 = new SqlDataAdapter("select * from tbl_hos_doc_appmnt", con);
            DataSet dts1 = new DataSet();
            dts1.Clear();
            adpt1.Fill(dts1);
            for (int i = 0; i < dts1.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add(dts1.Tables[0].Rows[i][2].ToString(), dts1.Tables[0].Rows[i][1].ToString(), dts1.Tables[0].Rows[i][3].ToString(), dts1.Tables[0].Rows[i][4].ToString(), dts1.Tables[0].Rows[i][5].ToString(), dts1.Tables[0].Rows[i][6].ToString());
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();


            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[] { new DataColumn("doctor"), new DataColumn("hospital"), new DataColumn("patient"), new DataColumn("date"), new DataColumn("time"), new DataColumn("status") });

            //var hakkeemdoc = from item in db.tbl_doctor_appointments select item;
            //foreach (var ss in hakkeemdoc)
            //{
            //    dt.Rows.Add(ss.d_id, "", ss.c_id, ss.a_date, ss.a_time, ss.a_status);
            //}
            //var hospitalodc = from item in db.tbl_hos_doc_appmnts select item;
            //foreach (var ss in hospitalodc)
            //{
            //    dt.Rows.Add(ss.d_id, ss.h_id, ss.u_id, ss.a_date, ss.a_time, ss.a_status);
            //}

            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            //foreach (GridViewRow gr in GridView1.Rows)
            //{
            //    Label lbl3 = gr.FindControl("Label3") as Label;
            //    Label lbl1 = gr.FindControl("Label1") as Label;
            //    Label lbl2 = gr.FindControl("Label2") as Label;
            //    Label lbl4 = gr.FindControl("Label4") as Label;
            //    Label lbl5 = gr.FindControl("Label5") as Label;
            //    Label lbl6 = gr.FindControl("Label6") as Label;
            //    Label lbl11 = gr.FindControl("Label11") as Label;
            //    Label lbl12 = gr.FindControl("Label12") as Label;
            //    if (lbl3.Text == "")
            //    {
            //        lbl4.Text = "--------";
            //        var doctor = (from item in db.tbl_doctors where item.d_hakkimid == lbl1.Text select item).First();
            //        lbl2.Text = doctor.d_name;
            //        //var user = (from item in db.tbl_signups where item.u_hakkimid == lbl5.Text select item).First();
            //        //lbl6.Text = user.name;

            //    }
            //    else if (lbl3.Text != "")
            //    {
            //        var hospital = (from item in db.tbl_hospitalregs where item.h_hakkimid == lbl3.Text select item).First();
            //        lbl4.Text = hospital.h_name;
            //        var doctor = (from item in db.tbl_hdoctors where item.hd_email == lbl1.Text select item).First();
            //        lbl2.Text = doctor.hd_name;
            //    }

            //    var patient = (from item in db.tbl_signups where item.u_hakkimid == lbl5.Text select item).First();
            //    lbl6.Text = patient.name;

            //    if (lbl11.Text == "2")
            //    {
            //        //if (Session["Language"].ToString() == "Auto")
            //        //{
            //        lbl12.Text = "Canceled";
            //        //}
            //        //else
            //        //{
            //        //    lbl12.Text = "ألغيت";
            //        //}
            //    }
            //    else if (lbl11.Text == "3")
            //    {
            //        //if (Session["Language"].ToString() == "Auto")
            //        //{
            //        lbl12.Text = "Consulted";
            //        //}
            //        //else
            //        //{
            //        //    lbl12.Text = "استشارة";
            //        //}
            //    }
            //    else
            //    {
            //        //if (Session["Language"].ToString() == "Auto")
            //        //{
            //        lbl12.Text = "Consulting not yet";
            //        //}
            //        //else
            //        //{
            //        //    lbl12.Text = "استشارات لم يتم بعد";
            //        //}
            //    }

            //}
            GridView1.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
}