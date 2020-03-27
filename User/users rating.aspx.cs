using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class User_users_rating : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString"].ConnectionString);

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
        if(!IsPostBack)
        {
            BindRatings();
        }
    }

   
    public void BindRatings()
    {
        int Total = 0;
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT rate_wt FROM tbl_rating", con);
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

    protected void Rating1_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_rating(rate_wt) VALUES (@Rating)", con);
        cmd.Parameters.AddWithValue("@Rating", SqlDbType.Int).Value = Rating1.CurrentRating;
        cmd.ExecuteNonQuery();
        con.Close();
        BindRatings();
    }
}