using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_Del_Doctors : System.Web.UI.Page
{
    secure obj = new secure();
    databaseDataContext db = new databaseDataContext();
    MailMessage Mail = new MailMessage();
    SMS ob = new SMS();
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
            doctor();
        }
    }
    public void doctor()
    {
        con.Open();
        SqlDataAdapter Sda = new SqlDataAdapter("Select * from tbl_temp_doctor order by d_id desc", con);
        DataTable dt = new DataTable();
        Sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label cno = new Label();
            cno = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");

            string lno = obj.DecryptString(cno.Text);
            Label lno1 = new Label();
            lno1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label11");
            lno1.Text = lno;


            Label email = new Label();
            email = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");


            string lemail = obj.DecryptString(email.Text);
            Label email1 = new Label();
            email1 = (Label)GridView1.Rows[gr.RowIndex].FindControl("Label12");
            email1.Text = lemail;
            // 
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        doctor();
    }
}