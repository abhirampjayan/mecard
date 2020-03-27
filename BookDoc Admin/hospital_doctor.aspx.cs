using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_hospital_doctor : System.Web.UI.Page
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
        if(!IsPostBack)
        {
            rdb_status.Items.FindByValue("0").Selected = true;
            hospital_doctor();
        }
    }

    public void hospital_doctor()
    {
        var doctor = from item in db.tbl_hdoctors orderby item.hd_id descending select item;
        GridView1.DataSource = doctor;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        hospital_doctor();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbl3 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label3") as Label;
        Label lbl6 = GridView1.Rows[e.RowIndex].Cells[4].FindControl("Label6") as Label;

        tbl_blk_hos_doctor thb = new tbl_blk_hos_doctor()
        {
            hos_hakkeem_id = lbl3.Text,
            doctor_id = lbl6.Text,
        };
        db.tbl_blk_hos_doctors.InsertOnSubmit(thb);
        db.SubmitChanges();
        hospital_doctor();

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbl3 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label3") as Label;
        Label lbl6 = GridView1.Rows[e.RowIndex].Cells[4].FindControl("Label6") as Label;

        var doctor = from item in db.tbl_blk_hos_doctors where item.hos_hakkeem_id == lbl3.Text && item.doctor_id == lbl6.Text select item;
        foreach(var ss in doctor)
        {
            db.tbl_blk_hos_doctors.DeleteOnSubmit(ss);

        }
        db.SubmitChanges();
        hospital_doctor();
      
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label lbl3 = gr.FindControl("Label3") as Label;
            Label lbl6 = gr.FindControl("Label6") as Label;
            //LinkButton lnk4 = gr.FindControl("LinkButton4") as LinkButton;
            //LinkButton lnk5 = gr.FindControl("LinkButton5") as LinkButton;
            Label lbl7 = gr.FindControl("Label7") as Label;

            var Query = from item in db.tbl_blk_hos_doctors where item.doctor_id == lbl6.Text&&item.hos_hakkeem_id==lbl3.Text select item;
            if (Query.Count() > 0)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    lbl7.ForeColor = System.Drawing.Color.Red;
                    lbl7.Text = "Active";
                //}
                //else
                //{
                //    lbl7.ForeColor = System.Drawing.Color.Red;
                //    lbl7.Text = "نشيط";
                //}
                //lnk4.Visible = false;
                //lnk5.Visible = true;
                //lnk5.ForeColor = System.Drawing.Color.Red;


            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    lbl7.Text = "Inactive";
                //}
                //else
                //{
                //    lbl7.Text = "غير نشط";
                //}
                //lnk5.Visible = false;
                //lnk4.Visible = true;
            }

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            var hdoctor = from item in db.tbl_hdoctors where item.h_id==TextBox1.Text || item.hd_contact == TextBox1.Text orderby item.hd_id descending select item;
            if (hdoctor.Count() > 0)
            {
                GridView1.DataSource = hdoctor;
                GridView1.DataBind();
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('آسف! غير معثور عليه')</Script>");
                //}
            }
        }
        else
        {
            hospital_doctor();
        }
    }
    protected void rdb_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdb_status.SelectedValue.ToString() == "0")
        {
            Response.Redirect("hospital_doctor.aspx");
        }
       else if (rdb_status.SelectedValue.ToString() == "1")
        {
            con.Open();
            //DataTable dt = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_blk_users", con);
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //var user = from item in db.tbl_signups orderby item.id descending select item;
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_hdoctor  where h_id not in (select hos_hakkeem_id from tbl_blk_hos_doctor) order by hd_id desc ", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                    GridView1.Visible = false;
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('آسف! غير معثور عليه')</Script>");
                //    GridView1.Visible = false;
                //}
            }
            
            // }
            con.Close();

        }
        else if (rdb_status.SelectedValue.ToString() == "2")
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_blk_hos_doctor ", con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //var user = from item in db.tbl_signups orderby item.id descending select item;
                SqlDataAdapter sda1 = new SqlDataAdapter("Select blk.*,U.* from tbl_blk_hos_doctor blk inner join tbl_hdoctor U on U.h_id=blk.hos_hakkeem_id   order by U.hd_id desc", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                GridView1.Visible = true;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
                    GridView1.Visible = false;
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('آسف! غير معثور عليه')</Script>");
                //    GridView1.Visible = false;
                //}
            }
            con.Close();
        }
        else
        {
            hospital_doctor();
        }
    }
}