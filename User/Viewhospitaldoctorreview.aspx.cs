using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_View_hospital_doctor_review : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    secure obj = new secure();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

    SqlCommand cmd, com;

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
        if (!IsPostBack)
        {
            con.Open();
            try
            {
                if (Request.QueryString["docid"] != null)
                {
                    Session["hdoctor"] = obj.DecryptString(Request.QueryString["docid"].ToString());
                    Session["lat"] = Request.QueryString["Lat"].ToString();
                    Session["long"] = Request.QueryString["Long"].ToString();

                    Session["lt"] = Session["lat"].ToString();
                    Session["lg"] = Session["long"].ToString();
                    Lat.Value = Session["lt"].ToString();
                    Long.Value = Session["lg"].ToString();
                }
                else
                {

                    Session["lat"] = Session["lt"].ToString();
                    Session["long"] = Session["lg"].ToString();
                    Lat.Value = Session["lt"].ToString();
                    Long.Value = Session["lg"].ToString();
                }
                doctor();
                hospital();
                Rating();
                GetReviews();
            }
            catch (Exception ex) { }
        }
       
    }

    public void doctor()
    {
        try
        {
            var Query = from item in db.tbl_hdoctors where item.hd_email == Session["hdoctor"].ToString() select item;
            foreach (var ss in Query)
            {
                if (ss.hd_photo == "" || ss.hd_photo == null)
                {
                    Image1.ImageUrl = "../Doctorimages/doctor.png";
                }
                else
                {

                    Image1.ImageUrl = ss.hd_photo;
                }
                lblname.Text = ss.hd_name;
                lblql.Text = ss.hd_education;
                lblspec.Text = ss.hd_specialties;

            }
            DetailsView1.DataSource = Query;
            DetailsView1.DataBind();
            Rating();
        }
        catch (Exception ex) { }
    }

    public void Rating()
    {

        //....................Rating--------------------------------------------
        //int total = 0;
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ConnectionString);

        //SqlCommand cmd6 = new SqlCommand("SELECT rate_service,rate_bm,rate_wt FROM tbl_rating where d_id='" + Session["hdoctor"].ToString() + "'", con);
        //SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
        //DataTable dt6 = new DataTable();
        //da6.Fill(dt6);
        //if (dt6.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dt6.Rows.Count; i++)
        //    {
        //        total += Convert.ToInt32(dt6.Rows[i][0].ToString());
        //    }
        //    int Average = total / (dt6.Rows.Count);
        //Rating1.CurrentRating = Average;

        //}
        //----------------------Rating-----------------------------------------
        try
        {
            double Total = 0;
            SqlCommand cmddd = new SqlCommand("SELECT rate_wt,rate_bm,rate_service FROM tbl_ratingview where d_id='" + Session["hdoctor"].ToString() + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmddd);
            DataTable dtt = new DataTable();
            da.Fill(dtt);
            double Average = 0;
            string avgg = "";
            string ratefill, rateempty, ratehalf;
            if (dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    Total += Convert.ToInt32(dtt.Rows[i][0].ToString()) + Convert.ToInt32(dtt.Rows[i][1].ToString()) + Convert.ToInt32(dtt.Rows[i][2].ToString());

                    Average = Total / 3;
                }

                Average = Average / (dtt.Rows.Count);
                double av = Math.Floor(Average);
                ratefill = "";
                rateempty = "";
                ratehalf = "";

                string str = null;

                str = Average.ToString();
                // avgg= Math.Round(decimal.Parse(Average.ToString()), 2).ToString();
                double balance = 5 - av;
                if (str.Contains(".") == true)
                {
                    ratehalf = "<img src='rate/half.gif'>";

                    balance = balance - 1;
                }
                else
                {
                    ratehalf = "";

                }


                for (int i = 0; i < av; i++)
                {
                    ratefill = ratefill + "<img  src='rate/Filled.gif'>";
                }
                for (int i = 0; i < balance; i++)
                {
                    rateempty = rateempty + "<img  src='rate/empty.gif'>";
                }

            }
            else
            {
                ratefill = "";
                rateempty = "";
                ratehalf = "";
            }
            Literal1.Text = ratefill.ToString();
            Literal2.Text = ratehalf.ToString();
            Literal3.Text = rateempty.ToString();

        }
        catch (Exception ex) { }

    }

    public void review()
    {
        try
        {
            //con.Open();
            //SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_user_feed where d_email ='" + Session["hdoctor"].ToString() + "' and status ='0'", con);
            //DataTable dtnewone = new DataTable();
            //sda.Fill(dtnewone);
            //DataList1.DataSource = dtnewone;
            //DataList1.DataBind();
            var Query = from item in db.tbl_user_feeds where item.d_email == Session["hdoctor"].ToString()  orderby item.id descending select item;
            DataList1.DataSource = Query;
            DataList1.DataBind();


            foreach (DataListItem dl in DataList1.Items)
            {

                string email = (dl.FindControl("Label3") as Label).Text;
                string uid = "";
                Label lbl1 = new Label();
                lbl1 = dl.FindControl("Label1") as Label;
                string hdemail = Session["hdoctor"].ToString();
                Label lbl2 = new Label();
                lbl2 = dl.FindControl("Label2") as Label;
                var Query2 = from item in db.tbl_user_feeds where item.d_email == hdemail && item.status == 0 select item;
                foreach (var ss in Query2)
                {
                    lbl2.Text = ss.u_review;
                   

                }
                var Query1 = from item in db.tbl_signups where item.u_hakkimid == email select item;
                foreach (var ss in Query1)
                {
                    lbl1.Text = "by " + ss.name;
                    uid = ss.u_hakkimid;

                }

                //....................Rating--------------------------------------------



                SqlCommand cmd6 = new SqlCommand("SELECT rate_wt FROM tbl_ratingview where d_id='" + Session["review"].ToString() + "'and u_id='" + uid + "'", con);
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
                l = dl.FindControl("Literal4") as Literal;
                l.Text = ratefill;

                //----------------------Rating-----------------------------------------

                //....................Rating--------------------------------------------



                SqlCommand cmd61 = new SqlCommand("SELECT rate_bm FROM tbl_ratingview where d_id='" + Session["review"].ToString() + "'and u_id='" + uid + "'", con);
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
                l1 = dl.FindControl("Literal5") as Literal;
                l1.Text = ratefill1;
                //----------------------Rating-----------------------------------------

                //....................Rating--------------------------------------------


                //----------------------Rating-----------------------------------------
                //....................Rating--------------------------------------------


                SqlCommand cmd63 = new SqlCommand("SELECT rate_service FROM tbl_ratingview where d_id='" + Session["review"].ToString() + "'and u_id='" + uid + "'", con);
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
                l11 = dl.FindControl("Literal6") as Literal;
                l11.Text = ratefill11;
                //----------------------Rating-----------------------------------------
            }
            Rating();
           // con.Close();
        }
        catch (Exception ex) { }
    }

    public void GetReviews()
    {
        try
        {
            var query = from a in db.tbl_user_feeds
                        join b in db.tbl_signups on a.u_email equals b.u_hakkimid
                        where a.d_email == Session["hdoctor"].ToString()
                        orderby a.date, a.time
                        select new { a.time, a.date, a.u_review, b.name, a.u_email };

            if (query.Count() > 0)
            {
                //GridView1.DataSource=query.ToList();
                //GridView1.DataBind();

                DataList1.DataSource = query.ToList();
                DataList1.DataBind();

                // rating
                foreach (DataListItem dl in DataList1.Items)
                {

                    string email = (dl.FindControl("Label3") as Label).Text;
                    string uid = "";
                    Label lbl1 = new Label();
                    lbl1 = dl.FindControl("Label1") as Label;
                    var Query1 = from item in db.tbl_signups where item.u_hakkimid == email select item;
                    foreach (var ss in Query1)
                    {
                        lbl1.Text = "by " + ss.name;
                        uid = ss.u_hakkimid;
                    }
                    Label lbl2 = new Label();
                    lbl2 = dl.FindControl("Label2") as Label;
                    string hdemail = Session["hdoctor"].ToString();
                    var Query2 = from item in db.tbl_user_feeds where item.d_email == hdemail && item.status == 0 select item;
                    foreach (var ss in Query2)
                    {
                        lbl2.Text = ss.u_review;


                    }
                    //....................Rating--------------------------------------------



                    SqlCommand cmd6 = new SqlCommand("SELECT rate_wt FROM tbl_rating where d_id='" + Session["hdoctor"].ToString() + "'and u_id='" + uid + "'", con);
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
                    l = dl.FindControl("Literal11") as Literal;
                    l.Text = ratefill;

                    //----------------------Rating-----------------------------------------

                    //....................Rating--------------------------------------------



                    SqlCommand cmd61 = new SqlCommand("SELECT rate_bm FROM tbl_rating where d_id='" + Session["hdoctor"].ToString() + "'and u_id='" + uid + "'", con);
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
                    l1 = dl.FindControl("Literal21") as Literal;
                    l1.Text = ratefill1;
                    //----------------------Rating-----------------------------------------



                    SqlCommand cmd63 = new SqlCommand("SELECT rate_service FROM tbl_rating where d_id='" + Session["hdoctor"].ToString() + "'and u_id='" + uid + "'", con);
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
                    l11 = dl.FindControl("Literal31") as Literal;
                    l11.Text = ratefill11;
                    //----------------------Rating-----------------------------------------
                }

            }
            else
            {
                //Label1.Text = "You have no appointments";
                //ModalPopupExtender2.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Reviews')</Script>");
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    public void hospital()
    {
        try
        {
            string hid = "";
            var doctor = from item in db.tbl_hdoctors where item.hd_email == Session["hdoctor"].ToString() select item;
            foreach (var ss in doctor)
            {
                hid = ss.h_id;
            }
            var hospital = from item in db.tbl_hospitalregs where item.h_hakkimid == hid select item;
            DetailsView2.DataSource = hospital;
            DetailsView2.DataBind();
            Label lbl11 = DetailsView2.FindControl("Label11") as Label;
            Label lbl12 = DetailsView2.FindControl("Label12") as Label;
            string[] ary = lbl12.Text.Split(' ');
            lbl11.Text = ary[0].ToString();
        }
        catch (Exception ex) { }
    }


}