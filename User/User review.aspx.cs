using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class User_User_review : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
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
        con1.Open();
        if (!IsPostBack)
        {
            Session["r"] = "0";


            Doctor();
        }
        //if (Session["Speciality"].ToString() == "Auto")
        //{
            HyperLink1.NavigateUrl = "~/User/Posted reviews.aspx";
              
        //}
        //else
        //{
        //    HyperLink1.NavigateUrl = "~/User/Posted reviews.aspx?l=ar-EG";
        //}
            if (Session["hakkemid_u"] != null)
        {

        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please login to your session')</Script>");
            //Label2.Text = "Please login to your session";
            //this.ModalPopupExtender3.Show();
        }
    }
    public void Doctor()
    {
        if (Session["hakkemid_u"] != null)
        {
            DataTable dt = new DataTable();

            var Query = from item in db.tbl_hos_appmnt_histories where item.u_id == Session["hakkemid_u"].ToString() && item.status == 0 select item;

            GridView1.DataSource = Query;
            GridView1.DataBind();

            foreach (GridViewRow gr in GridView1.Rows)
            {
                AjaxControlToolkit.Rating rtng = gr.FindControl("Rating1") as AjaxControlToolkit.Rating;
                AjaxControlToolkit.Rating rtng2 = gr.FindControl("Rating2") as AjaxControlToolkit.Rating;
                AjaxControlToolkit.Rating rtng3 = gr.FindControl("Rating3") as AjaxControlToolkit.Rating;
                AjaxControlToolkit.Rating rtng4 = gr.FindControl("Rating4") as AjaxControlToolkit.Rating;
                Label lbl2 = gr.FindControl("Label2") as Label;
                Label lbl1 = gr.FindControl("Label1") as Label;
                Label lbl5 = gr.FindControl("Label5") as Label;
                Image img1 = gr.FindControl("Image1") as Image;
                Label lbl11 = gr.FindControl("Label11") as Label;
                //SqlCommand com = new SqlCommand("select d_name from tbl_doctor where d_hakkimid='" + lbl2.Text + "'",con1);
                //string name = com.ExecuteScalar().ToString();




                //lbl1.Text = name.ToString();

                //.............................................//


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ConnectionString);

                double total = 0.0;
                var Queryr1 = from item in db.tbl_ratings where item.d_id == lbl2.Text select item;
                foreach (var ss in Queryr1)
                {
                    total = int.Parse(ss.rate_bm.ToString()) + int.Parse(ss.rate_wt.ToString()) + int.Parse(ss.rate_bm.ToString());

                }
                //Convert.ToInt32(Math.Ceiling(FloatValue));
                rtng.CurrentRating = (int)(total / 3);



                //-------------------------------------------------//

                int total1 = 0;

                SqlCommand cmd1 = new SqlCommand("SELECT rate_wt FROM tbl_rating where d_id='" + lbl2.Text + "' and u_id='" + lbl11.Text + "'", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        total1 += Convert.ToInt32(dt1.Rows[i][0].ToString());
                    }
                    int Average = total1 / (dt1.Rows.Count);
                    if (Average < 0)
                    {
                        Average = 1;
                        rtng2.CurrentRating = Average;
                    }
                    else
                    {
                        rtng2.CurrentRating = Average;
                    }
                    

                }

                //---------------------------------------------------------//
                int total2 = 0;

                SqlCommand cmd2 = new SqlCommand("SELECT rate_bm FROM tbl_rating where d_id='" + lbl2.Text + "' and u_id='" + lbl11.Text + "'", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        total2 += Convert.ToInt32(dt2.Rows[i][0].ToString());
                    }
                    int Average = total2 / (dt2.Rows.Count);
                    //rtng3.CurrentRating = Average;
                    if (Average < 0)
                    {
                        Average = 1;
                        rtng3.CurrentRating = Average;
                    }
                    else
                    {
                        rtng3.CurrentRating = Average;
                    }

                }
                //------------------------------------------------------------//
                int total3 = 0;

                SqlCommand cmd3 = new SqlCommand("SELECT rate_service FROM tbl_rating where d_id='" + lbl2.Text + "' and u_id='" + lbl11.Text + "'", con);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                if (dt3.Rows.Count > 0)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        total3 += Convert.ToInt32(dt3.Rows[i][0].ToString());
                    }
                    int Average = total3 / (dt3.Rows.Count);
                    //rtng4.CurrentRating = Average;
                    if (Average < 0)
                    {
                        Average = 1;
                        rtng4.CurrentRating = Average;
                    }
                    else
                    {
                        rtng4.CurrentRating = Average;
                    }

                }
                //-----------------------------------------------------------//

                if (!string.IsNullOrWhiteSpace(lbl5.Text))
                {

                    var Query2 = from item in db.tbl_hdoctors where item.h_id == lbl5.Text select item;
                    foreach (var dd in Query2)
                    {
                        lbl1.Text = dd.hd_name;
                        if (dd.hd_photo == "" || dd.hd_photo == null)
                        {
                            img1.ImageUrl = "../Doctorimages/doctor.png";
                        }
                        else
                        {

                            img1.ImageUrl = dd.hd_photo;
                        }
                        
                    }
                }

                else
                {
                    var Query1 = from item in db.tbl_doctors where item.d_hakkimid == lbl2.Text select item;
                    foreach (var dd in Query1)
                    {
                        lbl1.Text = dd.d_name;
                        if (dd.d_photo == "" || dd.d_photo == null)
                        {
                            img1.ImageUrl = "../Doctorimages/doctor.png";
                        }
                        else
                        {

                            img1.ImageUrl = dd.d_photo;
                        }
                       
                    }
                }
            }

        }
        else { Response.Redirect("~/Index/SignInSignUp.aspx"); }

    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if (TextBox1.Text != "")
    //    {
    //        var Query = from item in db.tbl_doctors where item.d_status == 1 && item.d_name.Contains(TextBox1.Text) select item;
    //        GridView1.DataSource = Query;
    //        GridView1.DataBind();
    //    }
    //    else
    //    {
    //        RegisterStartupScript("", "<Script Language=JavaScript>alert('Please enter doctor name')</Script>");
    //    }
    //}

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "clk")
        {
            Session["doctor"] = e.CommandArgument.ToString();
            Session.Add("doctor", e.CommandArgument.ToString());
            Session.Add("user", Session["hakkemid_u"].ToString());
            Response.Redirect("~/User/rating.aspx");

        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Session["hakkemid_u"] != null)
        {

            TextBox txt1 = GridView1.Rows[e.RowIndex].Cells[2].FindControl("TextBox2") as TextBox;
            string email = (GridView1.Rows[e.RowIndex].Cells[1].FindControl("Label2") as Label).Text;
            string id = (GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label6") as Label).Text;
            if (txt1.Text == "")
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please fill the textbox')</Script>");
                //Label2.Text = "Please fill the textbox";
                //this.ModalPopupExtender3.Show();
            }
            else
            {
                tbl_user_feed tu = new tbl_user_feed()
                {
                    u_email = Session["hakkemid_u"].ToString(),
                    d_email = email,
                    date = DateTime.Now.ToShortDateString(),
                    time = DateTime.Now.ToShortTimeString(),
                    status = 0,
                    u_review = txt1.Text,
                    his_id = int.Parse(id),
                };
                db.tbl_user_feeds.InsertOnSubmit(tu);
                db.SubmitChanges();
                var Query = from item in db.tbl_hos_appmnt_histories where item.id == int.Parse(id) select item;
                foreach (var ss in Query) { ss.status = 1; }
                db.SubmitChanges();
                Doctor();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Feedback successfully submit')</Script>");
                //Label2.Text = "Feedback successfully submit";
                //this.ModalPopupExtender3.Show();
            }

        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please login to your session and continue post feedback')</Script>");
            //Label2.Text = "Please login to your session and continue post feedback";
            //this.ModalPopupExtender3.Show();
        }
    }




    protected void Rating2_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
    {

        Session["r"] = Convert.ToInt32(Session["r"]) + int.Parse(e.Value);
        int rowindex = ((sender as AjaxControlToolkit.Rating).NamingContainer as GridViewRow).RowIndex;
        Label lbl2 = (GridView1.Rows[rowindex].Cells[0].FindControl("Label2")) as Label;
        Label lbl5 = (GridView1.Rows[rowindex].Cells[0].FindControl("Label5")) as Label;

        if (!string.IsNullOrWhiteSpace(lbl2.Text))
        {
            var Query = from item in db.tbl_ratings where item.d_id == lbl2.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    if (ss.rate_wt != null)
                    {
                        ss.rate_wt = int.Parse(e.Value);
                        //ss.rate = int.Parse(Session["r"].ToString());
                        ss.rate = ss.rate_wt + ss.rate_service + ss.rate_bm;
                        db.SubmitChanges();
                    }

                }

            }
            else
            {
                tbl_rating tr = new tbl_rating()
                {
                    rate_wt = int.Parse(e.Value),
                    d_id = lbl2.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_bm = 0,
                    rate_service = 0,
                    rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratings.InsertOnSubmit(tr);
                db.SubmitChanges();

            }
            var rate = from item in db.tbl_ratingviews where item.d_id == lbl2.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            if(rate.Count()>0)
            {
                foreach (var ss in rate)
                {
                    if (ss.rate_wt != null)
                    {
                        ss.rate_wt = int.Parse(e.Value);
                        //ss.rate = int.Parse(Session["r"].ToString());
                        //ss.rate = ss.rate_wt + ss.rate_service + ss.rate_bm;
                        db.SubmitChanges();
                    }

                }
            }
            else
            {
                tbl_ratingview trv=new tbl_ratingview()
                {
                    rate_wt = int.Parse(e.Value),
                    d_id = lbl2.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_bm = 0,
                    rate_service = 0,
                    //rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratingviews.InsertOnSubmit(trv);
                db.SubmitChanges();

                

            }
            SqlCommand com = new SqlCommand("select rate from tbl_ratefinal where hakkim_id='" + lbl2.Text + "'", con1);
            int idd = Convert.ToInt32(com.ExecuteScalar());

            int r = idd + int.Parse(e.Value);

            SqlCommand com1 = new SqlCommand("Update tbl_ratefinal set rate='" + r + "' where hakkim_id='" + lbl2.Text + "'", con1);
            com1.ExecuteNonQuery();
        }
        else if (!string.IsNullOrWhiteSpace(lbl5.Text))
        {
            var Query = from item in db.tbl_ratings where item.d_id == lbl5.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    if (ss.rate_wt != null)
                    {
                        ss.rate_wt = int.Parse(e.Value);
                        ss.rate = int.Parse(Session["r"].ToString());
                        db.SubmitChanges();
                    }

                }

            }
            else
            {
                tbl_rating tr = new tbl_rating()
                {
                    rate_wt = int.Parse(e.Value),
                    d_id = lbl5.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_bm = 0,
                    rate_service = 0,
                    rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratings.InsertOnSubmit(tr);
                db.SubmitChanges();

            }

            SqlCommand com = new SqlCommand("select rate from tbl_ratefinal where hakkim_id='" + lbl5.Text + "'", con1);
            int idd = Convert.ToInt32(com.ExecuteScalar());

            int r = idd + int.Parse(e.Value);

            SqlCommand com1 = new SqlCommand("Update tbl_ratefinal set rate='" + r + "' where hakkim_id='" + lbl5.Text + "'", con1);
            com1.ExecuteNonQuery();
        }
        Doctor();
    }

    protected void Rating3_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        Session["r"] = Convert.ToInt32(Session["r"]) + int.Parse(e.Value);
        int rowindex = ((sender as AjaxControlToolkit.Rating).NamingContainer as GridViewRow).RowIndex;
        Label lbl2 = (GridView1.Rows[rowindex].Cells[0].FindControl("Label2")) as Label;
        Label lbl5 = (GridView1.Rows[rowindex].Cells[0].FindControl("Label5")) as Label;

        if (!string.IsNullOrWhiteSpace(lbl2.Text))
        {
            var Query = from item in db.tbl_ratings where item.d_id == lbl2.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            //   Session["r"] = Convert.ToInt32(Session["r"]) + int.Parse(e.Value);
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    if (ss.rate_bm != null)
                    {
                        ss.rate_bm = int.Parse(e.Value);
                        //ss.rate = int.Parse(Session["r"].ToString());
                        ss.rate = ss.rate_service + ss.rate_bm + ss.rate_wt;
                        db.SubmitChanges();
                    }

                }
            }
            else
            {
                tbl_rating tr = new tbl_rating()
                {
                    rate_bm = int.Parse(e.Value),
                    d_id = lbl2.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_service = 0,
                    rate_wt = 0,
                    rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratings.InsertOnSubmit(tr);
                db.SubmitChanges();

            }
            var rate = from item in db.tbl_ratingviews where item.d_id == lbl2.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            if (rate.Count() > 0)
            {
                foreach (var ss in rate)
                {
                    if (ss.rate_bm != null)
                    {
                        ss.rate_bm = int.Parse(e.Value);
                        //ss.rate = int.Parse(Session["r"].ToString());
                        //ss.rate = ss.rate_service + ss.rate_bm + ss.rate_wt;
                        db.SubmitChanges();
                    }

                }
            }
            else
            {
                tbl_ratingview trv = new tbl_ratingview()
                {
                    rate_bm = int.Parse(e.Value),
                    d_id = lbl2.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_service = 0,
                    rate_wt = 0,
                    //rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratingviews.InsertOnSubmit(trv);
                db.SubmitChanges();

            }
            SqlCommand com = new SqlCommand("select rate from tbl_ratefinal where hakkim_id='" + lbl2.Text + "'", con1);
            int idd = Convert.ToInt32(com.ExecuteScalar());

            int r = idd + int.Parse(e.Value);

            SqlCommand com1 = new SqlCommand("Update tbl_ratefinal set rate='" + r + "' where hakkim_id='" + lbl2.Text + "'", con1);
            com1.ExecuteNonQuery();
        }
        else if (!string.IsNullOrWhiteSpace(lbl5.Text))
        {
            var Query = from item in db.tbl_ratings where item.d_id == lbl5.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    if (ss.rate_bm != null)
                    {
                        ss.rate_bm = int.Parse(e.Value);
                        ss.rate = int.Parse(Session["r"].ToString());
                        db.SubmitChanges();
                    }

                }
            }
            else
            {
                tbl_rating tr = new tbl_rating()
                {
                    rate_bm = int.Parse(e.Value),
                    d_id = lbl5.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_service = 0,
                    rate_wt = 0,
                    rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratings.InsertOnSubmit(tr);
                db.SubmitChanges();

            }
            SqlCommand com = new SqlCommand("select rate from tbl_ratefinal where hakkim_id='" + lbl5.Text + "'", con1);
            int idd = Convert.ToInt32(com.ExecuteScalar());

            int r = idd + int.Parse(e.Value);

            SqlCommand com1 = new SqlCommand("Update tbl_ratefinal set rate='" + r + "' where hakkim_id='" + lbl5.Text + "'", con1);
            com1.ExecuteNonQuery();
        }
        Doctor();
    }
    protected void Rating4_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        int rowindex = ((sender as AjaxControlToolkit.Rating).NamingContainer as GridViewRow).RowIndex;
        Label lbl2 = (GridView1.Rows[rowindex].Cells[0].FindControl("Label2")) as Label;
        Label lbl5 = (GridView1.Rows[rowindex].Cells[0].FindControl("Label5")) as Label;
        Session["r"] = Convert.ToInt32(Session["r"]) + int.Parse(e.Value);
        if (!string.IsNullOrWhiteSpace(lbl2.Text))
        {
            var Query = from item in db.tbl_ratings where item.d_id == lbl2.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    if (ss.rate_service != null)
                    {
                        ss.rate_service = int.Parse(e.Value);
                        //ss.rate = int.Parse(Session["r"].ToString());
                        ss.rate = ss.rate_service + ss.rate_bm + ss.rate_wt;
                        db.SubmitChanges();
                    }

                }
            }
            else
            {
                tbl_rating tr = new tbl_rating()
                {
                    rate_service = int.Parse(e.Value),
                    d_id = lbl2.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_bm = 0,
                    rate_wt = 0,
                    rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratings.InsertOnSubmit(tr);
                db.SubmitChanges();

            }
            var rate = from item in db.tbl_ratingviews where item.d_id == lbl2.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            if(rate.Count()>0)
            {
                foreach (var ss in rate)
                {
                    if (ss.rate_service != null)
                    {
                        ss.rate_service = int.Parse(e.Value);
                        //ss.rate = int.Parse(Session["r"].ToString());
                        //ss.rate = ss.rate_service + ss.rate_bm + ss.rate_wt;
                        db.SubmitChanges();
                    }

                }
            }
            else
            {
                tbl_ratingview tr = new tbl_ratingview()
                {
                    rate_service = int.Parse(e.Value),
                    d_id = lbl2.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_bm = 0,
                    rate_wt = 0,
                    //rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratingviews.InsertOnSubmit(tr);
                db.SubmitChanges();
            }
            SqlCommand com = new SqlCommand("select rate from tbl_ratefinal where hakkim_id='" + lbl2.Text + "'", con1);
            int idd = Convert.ToInt32(com.ExecuteScalar());

            int r = idd + int.Parse(e.Value);

            SqlCommand com1 = new SqlCommand("Update tbl_ratefinal set rate='" + r + "' where hakkim_id='" + lbl2.Text + "'", con1);
            com1.ExecuteNonQuery();
        }
        else if (!string.IsNullOrWhiteSpace(lbl5.Text))
        {
            var Query = from item in db.tbl_ratings where item.d_id == lbl5.Text && item.u_id == Session["hakkemid_u"].ToString() select item;
            if (Query.Count() > 0)
            {
                foreach (var ss in Query)
                {
                    if (ss.rate_service != null)
                    {
                        ss.rate_service = int.Parse(e.Value);
                        ss.rate = int.Parse(Session["r"].ToString());
                        db.SubmitChanges();
                    }

                }
            }
            else
            {
                tbl_rating tr = new tbl_rating()
                {
                    rate_service = int.Parse(e.Value),
                    d_id = lbl5.Text,
                    u_id = Session["hakkemid_u"].ToString(),
                    rate_bm = 0,
                    rate_wt = 0,
                    rate = int.Parse(Session["r"].ToString()),
                };
                db.tbl_ratings.InsertOnSubmit(tr);
                db.SubmitChanges();

            }
            SqlCommand com = new SqlCommand("select rate from tbl_ratefinal where hakkim_id='" + lbl5.Text + "'", con1);
            int idd = Convert.ToInt32(com.ExecuteScalar());

            int r = idd + int.Parse(e.Value);

            SqlCommand com1 = new SqlCommand("Update tbl_ratefinal set rate='" + r + "' where hakkim_id='" + lbl5.Text + "'", con1);
            com1.ExecuteNonQuery();
        }
        Doctor();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
}