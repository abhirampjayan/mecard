using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_Del_users : System.Web.UI.Page
{
    secure obj = new secure();
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();
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
            users();
        }
    }

    public void users()
    {
        //var user = from item in db.tbl_temp_signups orderby item.id descending select item;
        con.Open();
        SqlDataAdapter Sda = new SqlDataAdapter("Select * from tbl_temp_signup order by id desc", con);
        DataTable dt = new DataTable();
        Sda.Fill(dt);
        if(dt.Rows.Count>0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
       
        foreach (GridViewRow gr in GridView1.Rows)
        {
            string hakkeemid = (gr.FindControl("Label2") as Label).Text;
            string contact = (gr.FindControl("Label3") as Label).Text;

            Label lbl3 = gr.FindControl("Label3") as Label;

       string s= obj.DecryptString(lbl3.Text);
            if (s.StartsWith("5") == true)
            {
                lbl3.Text = "+966" + s;
            }
            else
            {
                lbl3.Text = "+91" + s;
            }
          


        }
        con.Close();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        users();
    }


}