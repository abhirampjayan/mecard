using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_rating : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

    protected override void InitializeCulture()
    {
        //Session["Speciality"] = "Auto";
        //string culture = "Auto";
        //try
        //{
        //    culture = Request.QueryString["l"].ToString();
        //    Session["Speciality"] = culture;
        //}
        //catch (Exception ex)
        //{ }
        //// string culture = Session["Speciality"].ToString();
        //if (string.IsNullOrEmpty(culture))
        //    culture = "Auto";
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


    protected void Page_Load(object sender, EventArgs e)
    {
        //Label3.Text = "0 Users have rated this Product";
        //Label4.Text = "Average rating for this Product is 0";
        if (!IsPostBack)
        {
            Data();
            rating();
            //BindRatings();
        }
    }
    public void Data()
    {

        var Query = from item in db.tbl_doctors where item.d_email == Session["doctor"].ToString() select item;
        if (Query.Count() > 0) { foreach (var ss in Query) { Image1.ImageUrl = ss.d_photo; Label1.Text = ss.d_name; Label2.Text = ss.d_about_you; } }
        else
        {
            var Query1 = from item in db.tbl_hdoctors where item.hd_email == Session["doctor"].ToString() select item;
            if (Query1.Count() > 0) { foreach (var ss in Query1) { Image1.ImageUrl = ss.hd_photo; Label1.Text = ss.hd_name; Label2.Text = ss.hd_about_you; } }
        }
    }
  
    public void BindRatings()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString"].ConnectionString);
       
        int Total = 0;
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT rate_wt FROM tbl_rating where d_id='"+Session["doctor"].ToString()+"'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Total += Convert.ToInt32(dt.Rows[i][0].ToString());
            }
            int Average = Total / (dt.Rows.Count);
            Rating1.CurrentRating = Average;
            Label1.Text = dt.Rows.Count + " " + "Users have rated this Product";
            Label2.Text = "Average rating for this Product is" + " " + Convert.ToString(Average);
        }
    }



    //public void BindRatings()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString"].ConnectionString);
    //    int Total = 0;
    //    int Total1 = 0;
    //    int Total2 = 0;
    //    int Total3 = 0;
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("SELECT rate_wt FROM tbl_rating where d_id='" + Session["doctor"].ToString() + "'", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            Total += Convert.ToInt32(dt.Rows[i][0].ToString());
    //        }
    //        int Average = Total / (dt.Rows.Count);
    //        Rating1.CurrentRating = Average;
    //        //Label3.Text = dt.Rows.Count + " " + "Users have rated this Product";
    //        //Label4.Text = "Average rating for this Product is" + " " + Convert.ToString(Average);
    //    }
    //    SqlCommand cmd1 = new SqlCommand("SELECT rate_bm FROM tbl_rating where d_id='" + Session["doctor"].ToString() + "'", con);
    //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
    //    DataTable dt1 = new DataTable();
    //    da.Fill(dt1);
    //    if (dt1.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt1.Rows.Count; i++)
    //        {
    //            Total1 += Convert.ToInt32(dt1.Rows[i][0].ToString());
    //        }
    //        int Average = Total1 / (dt1.Rows.Count);
    //        Rating2.CurrentRating = Average;
    //        //Label3.Text = dt.Rows.Count + " " + "Users have rated this Product";
    //        //Label4.Text = "Average rating for this Product is" + " " + Convert.ToString(Average);
    //    }
    //    SqlCommand cmd2 = new SqlCommand("SELECT rate_service FROM tbl_rating where d_id='" + Session["doctor"].ToString() + "'", con);
    //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //    DataTable dt2 = new DataTable();
    //    da.Fill(dt2);
    //    if (dt2.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt2.Rows.Count; i++)
    //        {
    //            Total2 += Convert.ToInt32(dt2.Rows[i][0].ToString());
    //        }
    //        int Average = Total2 / (dt2.Rows.Count);
    //        Rating3.CurrentRating = Average;
    //        //Label3.Text = dt.Rows.Count + " " + "Users have rated this Product";
    //        //Label4.Text = "Average rating for this Product is" + " " + Convert.ToString(Average);
    //    }

    //}
    public void rating()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString"].ConnectionString);
        int Total3 = 0;
        SqlCommand cmd3 = new SqlCommand("SELECT rate_wt FROM tbl_rating where d_id='" + Session["doctor"].ToString() + "'", con);
        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);
        if (dt3.Rows.Count > 0)
        {
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                Total3 += Convert.ToInt32(dt3.Rows[i][0].ToString());
            }
            int Average = Total3 / (dt3.Rows.Count);
            Rating1.CurrentRating = Average;
            //Label3.Text = dt3.Rows.Count + " " + "Users have rated this doctor";
            //Label4.Text = "Average rating for this doctor is" + " " + Convert.ToString(Average);
        }

        //Rating2---------------------

       
        int Total4 = 0;
        SqlCommand cmd4 = new SqlCommand("SELECT rate_bm FROM tbl_rating where d_id='" + Session["doctor"].ToString() + "'", con);
        SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
        DataTable dt4 = new DataTable();
        da4.Fill(dt4);
        if (dt4.Rows.Count > 0)
        {
            for (int i = 0; i < dt4.Rows.Count; i++)
            {
                Total4 += Convert.ToInt32(dt4.Rows[i][0].ToString());
            }
            int Average = Total4 / (dt4.Rows.Count);
            Rating2.CurrentRating = Average;
            //Label3.Text = dt3.Rows.Count + " " + "Users have rated this doctor";
            //Label4.Text = "Average rating for this doctor is" + " " + Convert.ToString(Average);
        }

        //Rating3........................................................
        
        int Total5 = 0;
        SqlCommand cmd5 = new SqlCommand("SELECT rate_service FROM tbl_rating where d_id='" + Session["doctor"].ToString() + "'", con);
        SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
        DataTable dt5 = new DataTable();
        da5.Fill(dt5);
        if (dt5.Rows.Count > 0)
        {
            for (int i = 0; i < dt5.Rows.Count; i++)
            {
                Total5 += Convert.ToInt32(dt5.Rows[i][0].ToString());
            }
            int Average = Total5 / (dt5.Rows.Count);
            Rating3.CurrentRating = Average;
            //Label3.Text = dt5.Rows.Count + " " + "Users have rated this doctor";
            //Label4.Text = "Average rating for this doctor is" + " " + Convert.ToString(Average);
        }
        int total = 0;

        SqlCommand cmd6 = new SqlCommand("SELECT rate_service,rate_bm,rate_wt FROM tbl_rating where d_id='" + Session["doctor"].ToString() + "'", con);
        SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
        DataTable dt6 = new DataTable();
        da6.Fill(dt6);
        if (dt6.Rows.Count > 0)
        {
            for (int i = 0; i < dt6.Rows.Count; i++)
            {
                total += Convert.ToInt32(dt6.Rows[i][0].ToString());
            }
            int Average = total / (dt6.Rows.Count);
            Rating4.CurrentRating = Average;
            //Label3.Text = dt5.Rows.Count + " " + "Users have rated this doctor";
            //Label4.Text = "Average rating for this doctor is" + " " + Convert.ToString(Average);
        }
    }
   

    protected void Rating1_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        var Query = from item in db.tbl_ratings where item.d_id == Session["doctor"].ToString() && item.u_id == Session["hakkemid_u"].ToString() select item;
        if (Query.Count() > 0)
        {
            foreach (var ss in Query)
            {
                if (ss.rate_wt == 0)
                {
                    ss.rate_wt = Rating1.CurrentRating;
                    db.SubmitChanges();
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You already rate this doctor')</Script>");
                }
            }

        }
        else
        {
            tbl_rating tr = new tbl_rating()
            {
                rate_wt = Rating1.CurrentRating,
                d_id = Session["doctor"].ToString(),
                u_id = Session["hakkemid_u"].ToString(),
                rate_bm=0,
                rate_service=0,
            };
            db.tbl_ratings.InsertOnSubmit(tr);
            db.SubmitChanges();

        }
        //rating();
        //BindRatings();
    }



    protected void Rating2_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        var Query = from item in db.tbl_ratings where item.d_id == Session["doctor"].ToString() && item.u_id == Session["hakkemid_u"].ToString() select item;
        if (Query.Count() > 0)
        {
            foreach (var ss in Query)
            {
                if (ss.rate_bm == 0)
                {
                    ss.rate_bm = Rating2.CurrentRating;
                    db.SubmitChanges();
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You already rate this doctor')</Script>");
                }
            }
        }
        else
        {
            tbl_rating tr = new tbl_rating()
            {
                rate_bm = Rating2.CurrentRating,
                d_id = Session["doctor"].ToString(),
                u_id = Session["hakkemid_u"].ToString(),rate_service=0,rate_wt=0,
            };
            db.tbl_ratings.InsertOnSubmit(tr);
            db.SubmitChanges();

        }
        //rating();
        //BindRatings();
    }

    protected void Rating3_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        var Query = from item in db.tbl_ratings where item.d_id == Session["doctor"].ToString() && item.u_id == Session["hakkemid_u"].ToString() select item;
        if (Query.Count() > 0)
        {
            foreach (var ss in Query)
            {
                if (ss.rate_service == 0)
                {
                    ss.rate_service = Rating3.CurrentRating;
                    db.SubmitChanges();
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You already rate this doctor')</Script>");
                }
            }
        }
        else
        {
            tbl_rating tr = new tbl_rating()
            {
                rate_service = Rating3.CurrentRating,
                d_id = Session["doctor"].ToString(),
                u_id = Session["hakkemid_u"].ToString(),rate_bm=0,rate_wt=0,
            };
            db.tbl_ratings.InsertOnSubmit(tr);
            db.SubmitChanges();

        }
        //rating();
        //BindRatings();
    }
}