using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class HospitalDoctor_UserReviews : System.Web.UI.Page
{
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
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    SqlCommand cmd;
    databaseDataContext db = new databaseDataContext();
    string DocId = "";
    string HosId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();
        if (Session["HosDocId"] != null)
        {
            DocId = Session["HosDocId"].ToString();
            HosId = Session["HospitalId"].ToString();
            
                GetReviews();
            
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
    }
    public void GetReviews()
    {
        try
        {
            var query = from a in db.tbl_user_feeds
                        join b in db.tbl_signups on a.u_email equals b.u_hakkimid
                        where a.d_email == DocId
                        orderby a.date, a.time
                        select new {a.time,a.date,a.u_review,b.name,a.u_email };

            if(query.Count() >0)
            {
                //GridView1.DataSource=query.ToList();
                //GridView1.DataBind();

                DataList1.DataSource = query.ToList();
                DataList1.DataBind();

                // rating
                foreach (DataListItem dl in DataList1.Items)
                {

                    string email = (dl.FindControl("Label6") as Label).Text;
                    string uid = "";
                    Label lbl1 = new Label();
                    //lbl1 = dl.FindControl("Label1") as Label;
                    var Query1 = from item in db.tbl_signups where item.u_hakkimid == email select item;
                    foreach (var ss in Query1)
                    {
                        //lbl1.Text = "by " + ss.name;
                        uid = ss.u_hakkimid;
                    }

                    //....................Rating--------------------------------------------



                    SqlCommand cmd6 = new SqlCommand("SELECT rate_wt FROM tbl_rating where d_id='" + DocId + "'and u_id='" + uid + "'", con);
                    int wt = 0;
                    try
                    {
                        wt = Convert.ToInt32(cmd6.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        wt = 0;
                    }
                    int b;
                    b = 5 - wt;
                    string ratefill = "";
                    for (int i = 0; i < wt; i++)
                    {
                        ratefill = ratefill + "<img  src='rate/Filled.gif'>";
                    }
                    for (int i = 0; i < b; i++)
                    {
                        ratefill = ratefill + "<img  src='rate/Empty.gif'>";
                    }

                    Literal l = new Literal();
                    l = dl.FindControl("Literal1") as Literal;
                    l.Text = ratefill;

                    //----------------------Rating-----------------------------------------

                    //....................Rating--------------------------------------------



                    SqlCommand cmd61 = new SqlCommand("SELECT rate_bm FROM tbl_rating where d_id='" + DocId + "'and u_id='" + uid + "'", con);
                    int bm = 0;
                    try
                    {
                        bm = Convert.ToInt32(cmd61.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        bm = 0;
                    }
                    string ratefill1 = "";

                    for (int i = 0; i < bm; i++)
                    {
                        ratefill1 = ratefill1 + "<img  src='rate/Filled.gif'>";
                    }
                    int b1;
                    b1 = 5 - bm;
                    for (int i = 0; i < b1; i++)
                    {
                        ratefill1 = ratefill1 + "<img  src='rate/Empty.gif'>";
                    }

                    Literal l1 = new Literal();
                    l1 = dl.FindControl("Literal2") as Literal;
                    l1.Text = ratefill1;
                    //----------------------Rating-----------------------------------------



                    SqlCommand cmd63 = new SqlCommand("SELECT rate_service FROM tbl_rating where d_id='" + DocId + "'and u_id='" + uid + "'", con);
                    int ser = 0;
                    try
                    {
                        ser = Convert.ToInt32(cmd63.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        ser = 0;
                    }
                    string ratefill11 = "";
                    for (int i = 0; i < ser; i++)
                    {
                        ratefill11 = ratefill11 + "<img  src='rate/Filled.gif'>";
                    }
                    int b2;
                    b2 = 5 - ser;
                    for (int i = 0; i < b2; i++)
                    {
                        ratefill11 = ratefill11 + "<img  src='rate/Empty.gif'>";
                    }

                    Literal l11 = new Literal();
                    l11 = dl.FindControl("Literal3") as Literal;
                    l11.Text = ratefill11;
                    //----------------------Rating-----------------------------------------
                }

            }
            else
            {
                //Label1.Text = "You have no appointments";
                //ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No Reviews')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لم يتم تقديم تعليقات')</Script>");
                //}
            }
                       
        }
        catch(Exception ex)
        {
            //Response.Write(ex);
        }
    }


    protected void BtnOk_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/HospitalDoctor/HospitalDoctorConsulting.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/HospitalDoctor/HospitalDoctorConsulting.aspx?l=ar-EG");
        //}
    }
}