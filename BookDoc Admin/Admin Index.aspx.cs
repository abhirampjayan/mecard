using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_Admin_Index : System.Web.UI.Page
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
           // this.MasterPageFile = "~/BookDoc Admin/AdminArabicMasterPage.master";
       // }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //dh();
            //HosReqstNo();
            //DoctorReqstNo();
            user();
            doctor();
            hospital();
            hospital_doctor();
        }
    }


    public void user()
    {
        var user = from item in db.tbl_signups where item.status == 1 select item;
        Label4.Text = user.Count().ToString();
        Label23.Text = user.Count().ToString();
        var latest_user = (from item in db.tbl_signups orderby item.id descending select item).Take(3);
        GridView1.DataSource = latest_user;
        GridView1.DataBind();
        foreach(GridViewRow gr in GridView1.Rows)
        {
            Image img = gr.FindControl("Image1") as Image;

            if (img.ImageUrl == "")
            {
                //img.ImageUrl = "../BookDoc Admin/images/user.png";
                img.ImageUrl = "../BookDoc Admin/images/user.png";
            }
        }
        int count1 = 0;
        int count2 = 0;
        var active = from item in db.tbl_signups where item.status == 1 select item;
        count1 = active.Count();
        foreach(var ss in active)
        {
            var blk_user = from item in db.tbl_blk_users where item.user_hakkeemid == ss.u_hakkimid select item;
            if(blk_user.Count()>0)
            {
                count2 += 1;
            }
        }
        Label27.Text = (count1 - count2).ToString();
        var blk_usercount = from item in db.tbl_blk_users select item;
        Label28.Text = blk_usercount.Count().ToString();
        con.Open();
       SqlDataAdapter sda=new SqlDataAdapter ("select count(id) as uid from tbl_temp_signup", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if(dt.Rows.Count>0)
        {
            Label29.Text = dt.Rows[0]["uid"].ToString();
        }
        else
        {
            Label29.Text = "0";
        }
        con.Close();

    }

    public void doctor()
    {
        var doctor = from item in db.tbl_doctors where item.d_status == 1 select item;
        Label5.Text = doctor.Count().ToString();

        var latest_doctor = (from item in db.tbl_doctors where item.d_status == 1 orderby item.d_id descending select item).Take(3);
        GridView2.DataSource = latest_doctor;
        GridView2.DataBind();
        foreach (GridViewRow gr in GridView2.Rows)
        {
            Image img = gr.FindControl("Image1") as Image;

            if (img.ImageUrl == "")
            {
                //img.ImageUrl = "../BookDoc Admin/images/user.png";
                img.ImageUrl = "../BookDoc Admin/images/user.png";
            }
        }

        var doc_rqst = from item in db.tbl_doctors where item.d_status == 0 select item;
        Label24.Text = doc_rqst.Count().ToString();


        int count1 = 0;
        int count2 = 0;
        var active = from item in db.tbl_doctors where item.d_status == 1 select item;
        count1 = active.Count();
        foreach (var ss in active)
        {
            var blk_doc = from item in db.tbl_blk_hakkeem_doctors where item.hakkeem_id == ss.d_hakkimid select item;
            if (blk_doc.Count() > 0)
            {
                count2 += 1;
            }
        }
        Label30.Text = (count1 - count2).ToString();
        var blk_doccount = from item in db.tbl_blk_hakkeem_doctors select item;
        Label31.Text = blk_doccount.Count().ToString();
        con.Open();
        SqlDataAdapter sda = new SqlDataAdapter("select count(d_id) as d_id from tbl_temp_doctor", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label32.Text = dt.Rows[0]["d_id"].ToString();
        }
        else
        {
            Label32.Text = "0";
        }
        con.Close();
    }
    public void hospital()
    {
        var hospital = from item in db.tbl_hospitalregs where item.h_status == 1 select item;
        Label20.Text = hospital.Count().ToString();

        var latest_hospital = (from item in db.tbl_hospitalregs where item.h_status == 1 orderby item.h_id descending select item).Take(3);
        GridView3.DataSource = latest_hospital;
        GridView3.DataBind();
        foreach (GridViewRow gr in GridView2.Rows)
        {
            Image img = gr.FindControl("Image1") as Image;

            if (img.ImageUrl == "")
            {
                //img.ImageUrl = "../BookDoc Admin/images/user.png";
                img.ImageUrl = "../BookDoc Admin/images/user.png";
            }
        }

        var hos_rqst = from item in db.tbl_hospitalregs where item.h_status == 0 select item;
        Label25.Text = hos_rqst.Count().ToString();


        int count1 = 0;
        int count2 = 0;
        var active = from item in db.tbl_hospitalregs where item.h_status == 1 select item;
        count1 = active.Count();
        foreach (var ss in active)
        {
            var blk_hos = from item in db.tbl_blk_hospitals where item.hospital_hakkeem_id == ss.h_hakkimid select item;
            if (blk_hos.Count() > 0)
            {
                count2 += 1;
            }
        }
        Label33.Text = (count1 - count2).ToString();
        var blk_hoscount = from item in db.tbl_blk_hospitals select item;
        Label34.Text = blk_hoscount.Count().ToString();

        con.Open();
        SqlDataAdapter sda = new SqlDataAdapter("select count(h_id) as hid from tbl_temp_hospitalreg", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label35.Text = dt.Rows[0]["hid"].ToString();
        }
        else
        {
            Label35.Text = "0";
        }
        con.Close();


    }
    public void hospital_doctor()
    {
        var hdoctor = from item in db.tbl_hdoctors where item.hd_status == 1 select item;
        Label21.Text = hdoctor.Count().ToString();
        Label26.Text= hdoctor.Count().ToString();
        var latest = (from item in db.tbl_hdoctors where item.hd_status == 1 orderby item.h_id descending select item).Take(3);
        GridView4.DataSource = latest;
        GridView4.DataBind();
        foreach (GridViewRow gr in GridView2.Rows)
        {
            Image img = gr.FindControl("Image1") as Image;

            if (img.ImageUrl == "")
            {
                //img.ImageUrl = "../BookDoc Admin/images/user.png";
                img.ImageUrl = "../BookDoc Admin/images/user.png";
            }
        }
    }

    //public void HosReqstNo()
    //{
    //    var query = from item in db.tbl_hospitalregs
    //                where item.h_status == 0
    //                select item;
    //    if (query.Count() > 0)
    //    {
    //        Label11.Text = query.Count().ToString();
    //    }
    //    else
    //    {
    //        Label11.Text = "0";
    //    }
    //    var query1 = from item in db.tbl_blk_hospitals
    //                 select item;
    //    if (query1.Count() > 0)
    //    {
    //        Label15.Text = query1.Count().ToString();
    //    }
    //    else
    //    {
    //        Label15.Text = "0";
    //    }
    //}
    //public void DoctorReqstNo()
    //{
    //    var query = from item in db.tbl_doctors
    //                where item.d_status == 0
    //                select item;
    //    if (query.Count() > 0)
    //    {
    //        Label13.Text = query.Count().ToString();
    //    }
    //    else
    //    {
    //        Label13.Text = "0";
    //    }

    //    var query1 = from item in db.tbl_blk_hakkeem_doctors
    //                 select item;
    //    if (query1.Count() > 0)
    //    {
    //        Label17.Text = query1.Count().ToString();
    //    }
    //    else
    //    {
    //        Label17.Text = "0";
    //    }
    //}
    //public void dh()
    //{
    //    var Query = from item in db.tbl_doctors where item.d_status == 1 select item;
    //    Label2.Text = Query.Count().ToString();

    //    var Query1 = from item in db.tbl_hospitalregs where item.h_status == 1 select item;
    //    Label3.Text = Query1.Count().ToString();

    //    var Query2 = from item in db.tbl_signups where item.status == 1 select item;
    //    Label9.Text = Query2.Count().ToString();

    //    var query3 = from item in db.tbl_blk_users
    //                 select item;
    //    if (query3.Count() > 0)
    //    {
    //        Label19.Text = query3.Count().ToString();
    //    }
    //    else
    //    {
    //        Label19.Text = "0";
    //    }
    //}
   
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Label1.Text = DateTime.Now.ToString();
    }

    //protected void Label2_Click(object sender, EventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        Response.Redirect("Doctor.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("Doctor.aspx?l=ar-EG");
    //    }
    //}

    //protected void Label7_Click(object sender, EventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        Response.Redirect("Hospital.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("Hospital.aspx?l=ar-EG");
    //    }
    //}

    //protected void Label8_Click(object sender, EventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        Response.Redirect("users.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("users.aspx?l=ar-EG");
    //    }
    //}

    //protected void Label10_Click(object sender, EventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        Response.Redirect("HospitalRequest.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("HospitalRequest.aspx?l=ar-EG");
    //    }
    //}

    //protected void Label12_Click(object sender, EventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        Response.Redirect("Doctor request.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("Doctor request.aspx?l=ar-EG");
    //    }
    //}

    //protected void Label14_Click(object sender, EventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        Response.Redirect("Hospital.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("Hospital.aspx?l=ar-EG");
    //    }
    //}

    //protected void Label16_Click(object sender, EventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        Response.Redirect("Doctor.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("Doctor.aspx?l=ar-EG");
    //    }
    //}

    //protected void Label18_Click(object sender, EventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        Response.Redirect("users.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("users.aspx?l=ar-EG");
    //    }
    //}
} 