using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_DoctorDetails : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();
        if (!IsPostBack)
        {

            doctor();
            review();
            Availability();
        }
    }
    public void doctor()
    {
        try
        {
            var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["dh"].ToString() select item;
            foreach (var ss in Query)
            {
                if (ss.d_photo == "" || ss.d_photo == null)
                {
                    Image1.ImageUrl = "../Doctorimages/doctor.png";
                }
                else
                {

                    Image1.ImageUrl = ss.d_photo;
                }
                //  Image2.ImageUrl = ss.d_photo;
                lblname.Text = ss.d_name;
                // lblname1.Text = ss.d_name;
                lblql.Text = ss.d_education;
                //  lblql1.Text = ss.d_education;
                lblspec.Text = ss.d_specialties;
                Session["didd"] = ss.d_id;

                //  lblspec1.Text = ss.d_specialties;
            }
            DetailsView1.DataSource = Query;
            DetailsView1.DataBind();


            Label lbl4 = DetailsView1.FindControl("Label43") as Label;
            List<string> langs = new List<string>();
            var QueryLang = from item in db.tbl_doc_languages where item.doc_id == Session["dh"].ToString() select item;
            foreach (var l in QueryLang)
            {
                //langs.Add(l.d_Language);
                lbl4.Text += l.d_Language + " ";
            }

            var Queryloc = from item in db.tbl_doctor_locations where item.d_id ==Convert.ToInt32(Session["didd"].ToString()) select item;
            foreach (var l in Queryloc)
            {
                //langs.Add(l.d_Language);
                //  lbl4.Text += l.d_Language + " ";
                Lat.Value = l.latitude.ToString();
                Long.Value = l.longitude.ToString();
            }

            Rating();
            var fee = from item in db.tbl_fees where item.d_hakkimid == Session["dh"].ToString() select item;
            if (fee.Count() > 0)
            {
                foreach (var f in fee)
                {
                    string a = "<label style='font-weight:bolder;color:red'>" + f.rate + "</label>";
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                    Label11.Text = "Consultation fee: " + a;
                    //}
                    //else
                    //{
                    //    Label11.Text = "رسوم الاستشارة: " + a;
                    //}
                }
            }
            else
            {

                Label11.ForeColor = System.Drawing.Color.IndianRed;
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                Label11.Text = "Consultation fee not available";
                //}
                //else
                //{
                //    Label11.Text = "رسوم التشاور غير متوفرة";
                //}
            }
        }
        catch (Exception ex)
        {

        }
    }


    public void review()
    {
        try
        {
            var Query = from item in db.tbl_user_feeds where item.d_email == Session["dh"].ToString() && item.status == 0 orderby item.id descending select item;
            DataList1.DataSource = Query;
            DataList1.DataBind();


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

                //....................Rating--------------------------------------------



                SqlCommand cmd6 = new SqlCommand("SELECT rate_wt FROM tbl_ratingview where d_id='" + Session["dh"].ToString() + "'and u_id='" + uid + "'", con);
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



                SqlCommand cmd61 = new SqlCommand("SELECT rate_bm FROM tbl_ratingview where d_id='" + Session["dh"].ToString() + "'and u_id='" + uid + "'", con);
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


                SqlCommand cmd63 = new SqlCommand("SELECT rate_service FROM tbl_ratingview where d_id='" + Session["dh"].ToString() + "'and u_id='" + uid + "'", con);
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
        }
        catch (Exception ex) { }
    }
    public void Availability()
    {
        try
        {
            var Query = (from item in db.doctor_availabilities where item.d_id == Session["dh"].ToString() orderby item.a_date ascending select item).Take(4);


            DataList2.DataSource = Query;
            DataList2.DataBind();
            foreach (DataListItem dli in DataList2.Items)

            {
                var Query4 = from item in db.doctor_availabilities where item.d_id == Session["dh"].ToString() select item;
                //foreach (var ss in Query4)
                //{
                //    DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
                //    DateTime dt2 = DateTime.Parse(ss.a_date);
                //    if (dt2 < dt1)
                //    {
                //        var Query5 = from item in db.doctor_availabilities where item.id == ss.id select item;
                //        foreach (var dd in Query5)
                //        {
                //            db.doctor_availabilities.DeleteOnSubmit(dd);
                //        }
                //        db.SubmitChanges();
                //    }
                //    else if (dt2 > dt1) { }
                //    else if (dt1 == dt2)
                //    {
                //        List<string> listTime = new List<string>();
                //        string time = DateTime.Now.AddHours(2).ToShortTimeString();
                //        var Query6 = from item in db.view_doc_available_times where item.a_date == ss.a_date select item;
                //        foreach (var tt in Query6)
                //        {

                //            DateTime dtt1 = DateTime.Parse(time);
                //            try
                //            {
                //                DateTime dtt2 = DateTime.Parse(tt.time);
                //                if (dtt2 >= dtt1)
                //                {
                //                    listTime.Add(tt.time);
                //                }
                //            }
                //            catch (Exception ex)
                //            {


                //                var qry = from item in db.doctor_availabilities where item.a_date == ss.a_date select item;
                //                foreach (var dd in qry)
                //                {
                //                    db.doctor_availabilities.DeleteOnSubmit(dd);
                //                }
                //                db.SubmitChanges();



                //            }

                //        }
                //        string ntime = "";
                //        foreach (string ttt in listTime)
                //        {
                //            if (ntime == "")
                //            {
                //                ntime = ttt;
                //            }
                //            else
                //            {
                //                ntime += "," + ttt;
                //            }
                //        }
                //        try
                //        {
                //            var Query7 = from item in db.tbl_doc_times where item.date_id == ss.id select item;
                //            foreach (var nn in Query7)
                //            {
                //                nn.time = ntime;
                //            }
                //            db.SubmitChanges();
                //        }
                //        catch (Exception ex)
                //        { }
                //    }
                //}



                Label lbl6 = dli.FindControl("Label6") as Label;
                DataList dl3 = dli.FindControl("DataList3") as DataList;
                var Query2 = from item in db.view_doc_available_times where item.a_date == lbl6.Text && item.d_hakkimid == Session["dh"].ToString() select item;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("date"), new DataColumn("time"), new DataColumn("email") });
                foreach (var ss in Query2)
                {
                    if (ss.time == "")
                    { }
                    else
                    {
                        int l = ss.time.ToString().Count();
                        string ab1 = ss.time.ToString().Substring(0, l - 2);
                        string ab2 = ss.time.ToString().Substring(l - 2, 2);
                        string timet = ab1.ToString() + " " + ab2.ToString();
                        dt.Rows.Add(ss.a_date, timet, ss.d_email);
                    }

                }
                dl3.DataSource = dt;
                dl3.DataBind();
                lbl6.Text = DateTime.Parse(lbl6.Text).ToString("yyy d MMM");
                foreach (DataListItem dl3i in dl3.Items)
                {
                    //Button btn2 = dl3i.FindControl("Button2") as Button;
                    LinkButton lnk2 = dl3i.FindControl("LinkButton2") as LinkButton;
                    string date = DateTime.Parse(lbl6.Text).ToString("yyyy-MM-dd");
                    var Query3 = from item in db.tbl_doctor_appointments
                                 where
                        item.d_id == Session["dh"].ToString() && item.a_date == date && item.a_time == lnk2.Text
                                 select item;
                    if (Query3.Count() > 0)
                    {
                        foreach (var tt in Query3)
                        {
                            if (tt.a_status == 0)
                            { lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "Waiting for confirmation"; }
                            if (tt.a_status == 1 || tt.a_status == 4)
                            {
                                lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false;
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                lnk2.ToolTip = "Booked";
                                //}
                                //else
                                //{
                                //    lnk2.ToolTip = "حجز";
                                //}
                            }
                        }

                    }
                    else
                    {
                        //lnk2.BackColor = System.Drawing.Color.Green;
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                        lnk2.ToolTip = "Available";
                        //}
                        //else
                        //{
                        //    lnk2.ToolTip = "متاح";
                        //}
                    }
                }
            }
        }
        catch (Exception ex) { }
    }
    public void Rating()
    {
        try
        {
            double Total = 0;
            SqlCommand cmddd = new SqlCommand("SELECT rate_wt,rate_bm,rate_service FROM tbl_ratingview where d_id='" + Session["dh"].ToString() + "'", con);

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
            // Literal7.Text = ratefill.ToString();
            // Literal8.Text = ratehalf.ToString();
            // Literal9.Text = rateempty.ToString();
            ////....................Rating--------------------------------------------
            //int total = 0;
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ConnectionString);

            //SqlCommand cmd6 = new SqlCommand("SELECT rate_service,rate_bm,rate_wt FROM tbl_rating where d_id='" + Session["review"].ToString() + "'", con);
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
            //    Rating1.CurrentRating = Average;

            //}
            ////----------------------Rating-----------------------------------------
        }
        catch (Exception ex) { }
    }
}