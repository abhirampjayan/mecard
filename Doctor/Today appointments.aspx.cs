using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctor_Today_appointments : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
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
        //    this.MasterPageFile = "~/Doctor/ArabicMasterPage.master";
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();
        if (!IsPostBack)
        {
          
            CheckLocation();
            today();
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
      //  today();


    }
    public void CheckLocation()
    {
        var query = from item in db.tbl_doctor_locations
                    join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                    where item1.d_hakkimid == Session["hakkeemid_d"].ToString()
                    select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
        if (query.Count() <= 0)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{

                Response.Redirect("~/Doctor/SetLocation.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Doctor/SetLocation.aspx?l=ar-EG");
            //}
        }
    }
    public void today()
    {

        SqlCommand com1 = new SqlCommand("  select id from tbl_doctor_appointment where id in (select id from tbl_cancel)", con);
        SqlDataReader dtr = com1.ExecuteReader();
       while(dtr.Read())
        { 


    SqlCommand    com = new SqlCommand("delete from tbl_doctor_appointment where a_date = '"+DateTime.Now.ToString("yyyy-MM-dd")+"' and a_time < LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7)) and id<>'"+dtr[0].ToString()+"'", con);
        com.ExecuteNonQuery();

        }
        var Query = from item in db.tbl_doctor_appointments where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == DateTime.Now.ToString("yyyy-MM-dd")&& item.a_status==1 select item;
        if (Query.Count() > 0)
        {
            GridView1.DataSource = Query;
            GridView1.DataBind();

            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lbl4 = new Label();
                lbl4 = gr.FindControl("Label4") as Label;
                Label lbl5 = new Label();
                lbl5 = gr.FindControl("Label5") as Label;
                Label lbl8 = new Label();
                lbl8 = gr.FindControl("Label8") as Label;
                Label lbl9 = new Label();
                lbl9 = gr.FindControl("Label9") as Label;

                Label lbl10 = new Label();
                lbl10 = gr.FindControl("Label10") as Label;
                var Query1 = from item in db.tbl_signups where item.u_hakkimid == lbl4.Text select item;
                foreach (var s in Query1)
                {
                    lbl5.Text = s.name;
                }
                if (lbl8.Text == "0")
                {
                    lbl9.Text = "Waiting";
                    gr.Cells[2].BackColor = System.Drawing.Color.Orange;
                    LinkButton lnk1 = gr.FindControl("LinkButton1") as LinkButton;
                    lnk1.Enabled = false;
                }
                else if (lbl8.Text == "1")
                {
                    lbl9.Text = "Confirm";
                    //gr.Cells[2].BackColor = System.Drawing.Color.LimeGreen;
                    gr.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#18bc9c");
                }
                else if (lbl8.Text == "2")
                {
                    lbl9.Text = "Rejected";
                    //lbl9.ForeColor = System.Drawing.Color.Red;
                    gr.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                    LinkButton lnk1 = gr.FindControl("LinkButton1") as LinkButton;
                    lnk1.Enabled = false;
                }



                else if (lbl8.Text == "3")
                {
                    lbl9.Text = "Consulted";
                    LinkButton lnk1 = gr.FindControl("LinkButton1") as LinkButton;
                    lnk1.Enabled = false;

                }

                SqlCommand comc = new SqlCommand("select * from tbl_cancel where app_id='" + lbl10.Text + "'", con);
                SqlDataReader dtr1 = comc.ExecuteReader();
                if (dtr1.HasRows)
                {
                    string tt = "";
                    while (dtr1.Read())
                    {
                        tt = dtr1[1].ToString();
                    }
                    LinkButton lnk11 = gr.FindControl("LinkButton11") as LinkButton;
                    lnk11.Text = "Canceled on " + tt;
                    lnk11.CommandArgument = "0";


                    LinkButton lnk1 = gr.FindControl("LinkButton1") as LinkButton;
                    lnk1.Enabled = false;
                }
            }
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointments')</Script>");
        }

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="cnslt")
        {
            Session["cnsltid"] = e.CommandArgument.ToString();
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Doctor/Consulting.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Doctor/Consulting.aspx?l=ar-EG");
            //}
        }
        if (e.CommandName == "cnslt1")
        {
            if (e.CommandArgument.ToString() != "0")
            {


                Session["cnsltid"] = e.CommandArgument.ToString();
                var apmnts = from item in db.tbl_doctor_appointments where item.id == int.Parse(e.CommandArgument.ToString()) select item;
                foreach (var ss in apmnts)
                {
                    tbl_apmnt_cancel tc = new tbl_apmnt_cancel()
                    {
                        apmnt_id = ss.id.ToString(),
                        canceled_by = "d",
                        cancel_rsn = "Patient not comming for this consultation",
                        date = DateTime.Now.ToShortDateString(),
                        time = DateTime.Now.ToShortTimeString(),
                        d_id = ss.d_id,
                        u_id = ss.c_id,

                    };
                    db.tbl_apmnt_cancels.InsertOnSubmit(tc);
                    db.SubmitChanges();

                    ss.a_status = 5;
                    db.SubmitChanges();
                }
                //// Response.Redirect("~/Doctor/Consulting.aspx");



                //SqlCommand com1 = new SqlCommand("select c_id from tbl_doctor_appointment where id='" + e.CommandArgument.ToString() + "'", con);
                //string hakk = com1.ExecuteScalar().ToString();

                //SqlCommand com2 = new SqlCommand("select count(*) from tbl_cancel where hakkem_id='" + hakk + "'", con);
                //int tot =Convert.ToInt32(com2.ExecuteScalar());
                //if (tot >= 5)
                //{
                //    SqlCommand com3 = new SqlCommand("update tbl_signup set status='10' where u_hakkimid='"+ hakk + "'", con);
                //    int id1 = Convert.ToInt32(com3.ExecuteNonQuery());
                //    if (id1 != 0)
                //    {


                //        if (Session["Language"].ToString() == "Auto")
                //        {
                //            RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient cancelled the appointments above 5. so automaticaly blocked by admin')</Script>");
                //        }
                //        else
                //        {
                //            RegisterStartupScript("", "<Script Language=JavaScript>swal('هذا المريض ألغى التعيينات أعلاه 5. لذلك أوتوماتيكيا منعت من قبل المشرف')</Script>");

                //        }
                //    }
                //}


                //SqlCommand com = new SqlCommand("insert into tbl_cancel values('" + e.CommandArgument.ToString() + "','"+hakk.ToString()+"','" + DateTime.Now.ToString() + "')", con);
                //int id = Convert.ToInt32(com.ExecuteNonQuery());
                //if (id != 0)
                //{
                //    if (Session["Language"].ToString() == "Auto")
                //    {
                //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Appointment Canceled Successfully.')</Script>");
                //    }
                //    else
                //    {
                //        RegisterStartupScript("", "<Script Language=JavaScript>swal('تم إلغاء التعيين بنجاح')</Script>");

                //    }
                //}
            } 
        }
        today();

    }

    protected void Timer2_Tick(object sender, EventArgs e)
    {
        Response.Write("test");
    }
}