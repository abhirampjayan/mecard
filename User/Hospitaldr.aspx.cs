using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

public partial class Hospital_Hospital : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    string qry;
    SqlCommand cmd;
    SqlDataReader dr, dr1, dr2;
    int pagestart = 1;
    int q = 0;
    secure obj = new secure();

    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();

        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
        if (!IsPostBack)
        {try
            {
                Session["hos"] = obj.DecryptString(Request.QueryString["hos"].ToString());

            }
            catch (Exception ex)
            { }
                LoadSpecialities();

            try
            {
                CheckLocation();
            }
            catch (Exception ex)
            {
               // Response.Redirect("../index/hospita login.aspx");
            }
            //if (Request.QueryString["docid"] != null)
            //{
            //    Session["did"] = obj.DecryptString(Request.QueryString["docid"].ToString());
               
            //    TxtApntmtDate.Text = obj.DecryptString(Request.QueryString["docdate"].ToString());
            //    try
            //    {
            //        TxtApointmentTime.Text = obj.DecryptString(Request.QueryString["doctime"].ToString()) + " " + obj.DecryptString(Request.QueryString["timeperiod"].ToString());
            //        this.ModalPopupExtender1.Show();
            //    }
            //    catch (Exception ex)
            //    { }
            //    }
            DataSet dts = new DataSet();
       
          
                Session["count"] = 0;
                list_doctors( "", "", "");

            }

    }
    public void LoadSpecialities()
    {
        try
        {
            var query = from item in db.tbl_specialities
                        select item;
            if (query.Count() > 0)
            {
                TextBox2.DataSource = query;
                TextBox2.DataTextField = "Specialities";
                TextBox2.DataValueField = "id";
                TextBox2.DataBind();
            }
            TextBox2.Items.Insert(0, "--Select--");

        }
        catch (Exception ex)
        {
        }
    }
    public void searchfiltr()
    {
        string query = "";
    
        string q = "";

        if (TextBox1.Text != "" && TextBox2.SelectedItem.Text != "--Select--" )
        {
            query = "hd_name='" + TextBox1.Text + "' and hd_specialties='" + TextBox2.SelectedItem.Text + "'";
           
            q = " where  " + query;
        }
        else if (TextBox1.Text != "" && TextBox2.SelectedItem.Text == "--Select--")
        {
            query = "hd_name='" + TextBox1.Text + "' ";


            q = " where  " + query;
        }
        else if (TextBox1.Text == "" && TextBox2.SelectedItem.Text != "--Select--")
        {
            query ="hd_specialties='" + TextBox2.SelectedItem.Text + "'";

            q = " where  " + query;
        }
        else
        {
            q = "";
        }
        list_doctors(q, "", "");
    }
         public void list_doctors( string docName, string location, string hosName)
    {
        DataSet dts = new DataSet();

        Session["query"] = "";
            int pagestart = 1;
            try
            {
                if (Request.QueryString["page"] == null)
                {
                    pagestart = 1;
                    q = 0;
                }
                else
                {
                    pagestart = Convert.ToInt32(Request.QueryString["page"]);
                    q = 1;
                }
            }
            catch (Exception ex)
            {
                pagestart = 1;
            }
            if (q == 0)
            {

            if (docName != "")
            {

                Session["query"] = docName.ToString();
                Session["query"] = Session["query"].ToString().Replace("where", " and ");
                SqlDataAdapter adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time where h_name='" + Session["hos"].ToString() + "'"+ Session["query"], con);


                adpt.Fill(dts);
                decimal c = Convert.ToDecimal(dts.Tables[0].Rows.Count);
                c = c / 3;


                decimal d = Math.Ceiling(c);
                Session["d"] = d.ToString();

            }
            else
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time where h_name='" + Session["hos"].ToString() + "' ", con);


                adpt.Fill(dts);
                decimal c = Convert.ToDecimal(dts.Tables[0].Rows.Count);
                c = c / 3;


                decimal d = Math.Ceiling(c);
                Session["d"] = d.ToString();
            }
               
                
            }
            if (pagestart == 1)
            {
                //   cmd = new SqlCommand("select   distinct top 3 (hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hos"].ToString() + "' and row_number>=1 ORDER BY hd_name, row_number", con);
                cmd = new SqlCommand("select top 3  * from view_test_hos_doc where h_name='" + Session["hos"].ToString() + "'  and t>='" + pagestart + "'"+ Session["query"].ToString() + " order by t", con);
            }
            else
            {
                pagestart = 1 + ((pagestart - 1) * 3);

                cmd = new SqlCommand("select top 3  * from view_test_hos_doc where h_name='" + Session["hos"].ToString() + "'  and t>'" + pagestart + "'" + Session["query"].ToString() + " order by t", con);
            }
        //            SELECT DISTINCT
        //  d_name, 
        //  DENSE_RANK() OVER(ORDER BY d_name) row_number
        //FROM view_doc_available_time
        //ORDER BY d_name, row_number
        // cmd = new SqlCommand("select   distinct top 3 (hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hos"].ToString()+ "' and row_number>='"+pagestart+"'  ORDER BY hd_name, row_number", con);
       
       
            dr = cmd.ExecuteReader();

       



        Response.Write("<div class='row'><div class='col-md-12 col-lg-12'><span id='Label1' style='height:500px;width:1300;Z-INDEX: 302; LEFT: 50px; POSITION: absolute; TOP: 150px;overflow-y:hidden;'>");


            Response.Write("<ul class='pagination'>");
        
        

            for (int p = 1; p <=Convert.ToInt32(Session["d"].ToString()); p++)
            {
                Response.Write("<li><a href =?page=" + p.ToString() + " > " + p + " </a></li> ");
            }

            Response.Write("</ul>");

            Response.Write("<div style='margin-top:17px;'>");
            while (dr.Read())
            {



                DateTime dt = new DateTime();
                dt = DateTime.Now.Date;
                int j = 0;
                Response.Write("<div style='display:inline-block;width:372px'>");
                Response.Write("<table class='table table-responsive table-hover' /*style='border-collapse: separate;border-spacing: 1px 0;'*/>");
                Response.Write("<tr class='box box-primary box-solid'><td style='font-weight:bold;'>" + dr[0].ToString() + "</td></tr><tr class='box'><td>");


                ///////////////////////////////////////////////////////

                Response.Write("<ul class='bxslider'>");
                Response.Write("<li>");
                for (int i = 0; i < 30; i++)
                {


                string cdate = dt.ToString("yyyy-MM-dd");

                if (j < 3)
                {



                    if (j == 0)
                    {

                        if (Request.QueryString["next"] == "1")
                        {

                            string date = Request.QueryString["docdate"];


                            string datee = obj.DecryptString(date);


                            DateTime dtnew = Convert.ToDateTime(datee);

                            dt = dtnew;
                            //list_doctorstest("", "", "", "", "", "", dtnew);
                            //  Request.QueryString["next"] = "0";
                        }

                        //else if(Request.QueryString["next"] == "2")
                        //{ dt = DateTime.Now.Date;
                        //}


                        string cdateo = dt.ToString("yyyy-MM-dd");
                        DateTime dt1 = dt.AddDays(1);
                        string cdate1 = dt1.ToString("yyyy-MM-dd");

                        DateTime dt2 = dt1.AddDays(1);
                        string cdate2 = dt2.ToString("yyyy-MM-dd");
                        Session["cdate2"] = cdate2.ToString();
                        // Session["datedis"] = dt2.ToString("ddd MMM dd");
                        qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdateo + "' ";


                        SqlDataAdapter adpt111 = new SqlDataAdapter(qry, con);
                        DataSet dts111 = new DataSet();
                        adpt111.Fill(dts111);
                        int count1 = dts111.Tables[0].Rows.Count;
                        if (count1 == 0)
                        {
                            qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate1 + "' ";


                            SqlDataAdapter adpt222 = new SqlDataAdapter(qry, con);
                            DataSet dts222 = new DataSet();
                            adpt222.Fill(dts222);
                            int count2 = dts222.Tables[0].Rows.Count;
                            if (count2 == 0)
                            {
                                qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate2 + "' ";


                                SqlDataAdapter adpt333 = new SqlDataAdapter(qry, con);
                                DataSet dts333 = new DataSet();
                                adpt333.Fill(dts333);
                                int count3 = dts333.Tables[0].Rows.Count;

                                if (count3 == 0)
                                {

                                    Session["count"] = "1";
                                }
                            }
                        }

                        if (Session["count"].ToString() == "1")
                        {

                            Session["count"] = 0;

                            Response.Write("<div style='padding-left:42px;'><div style='display:inline-block;padding-left:8px;height:auto'>");
                            //   string cdate = dt.ToString("yyyy-MM-dd");
                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



                            Response.Write("</div>");
                            dt = dt.AddDays(1);
                            Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                            Response.Write("</div>");
                            dt = dt.AddDays(1);
                            Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

                            Response.Write("</div>");


                            //DateTime newdate = Session["cdate2"];
                            //     dt.ToString("ddd MMM dd")
                            string extqry = "select   top 1 date from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date>'" + Session["cdate2"].ToString() + "'";
                            SqlCommand com1 = new SqlCommand(extqry, con);
                            string nextdate = "";
                            try
                            {
                                nextdate = com1.ExecuteScalar().ToString();
                                Session["nextdate"] = nextdate.ToString();
                                DateTime datenew = Convert.ToDateTime(nextdate);

                                nextdate = datenew.ToString("ddd MMM dd");

                                string docidpass = obj.EnryptString(dr[9].ToString().ToString());
                                string docdate = obj.EnryptString(Session["nextdate"].ToString());

                                //string[] t = new string[7];

                                //t = dr1[0].ToString().Split(' ');
                                //string doctime = obj.EnryptString(t[0].ToString());
                                //string timeperiod = obj.EnryptString(t[1].ToString());
                                //string lat = obj.EnryptString(Lat.ToString());
                                //string longti = obj.EnryptString(Long.ToString());
                                //Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                //Response.Write("<br>");



                                Response.Write("<label style='margin-top:35px; margin-left:30px' ><a  class='btn btn-success' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() +  "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
                                //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                                Response.Write("</div>");

                            }
                            catch (Exception ex)
                            {
                                nextdate = "";
                                Response.Write("<label style='margin-top:35px; margin-left:10px'><a href=?next=2  class='btn btn-success'> Not Available on remaining Dates</a></label>");
                                //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                                Response.Write("</div>");
                                break;
                                // dt.AddDays(2);

                            }

                            //  Session["datedis"] 


                            //   Response.Write("Next availability");
                            //  Response.Write("<label style='margin-top:35px; margin-left:30px'  class='btn btn-success'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
                            ////  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                            //  Response.Write("</div>");

                            // dr1.Close();


                            // Response.Write("</li>");


                            Response.Write("</li>");
                            Response.Write("<li>");

                            Session["count"] = "0";

                            j = 0;
                            dt.AddDays(2);
                        }
                        else
                        {
                            Response.Write("<div style='padding-left:42px;'><div style='display:inline-block;padding-left:8px;height:auto'>");
                            //       string cdate = dt.ToString("yyyy-MM-dd");
                            // DateTime  dte = Convert.ToDateTime(cdate);
                            qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate + "'";

                            cmd = new SqlCommand(qry, con);
                            dr1 = cmd.ExecuteReader();
                            SqlDataAdapter adpt1 = new SqlDataAdapter(qry, con);
                            DataSet dts1 = new DataSet();
                            adpt1.Fill(dts1);
                            int count = dts1.Tables[0].Rows.Count;
                            int b = 0;
                            if (count < 4)
                            {
                                b = 4 - count;
                            }
                            try
                            {
                                int test = 0;

                                if (dr1.HasRows)
                                {

                                    if (Request.QueryString["next"] == "1")
                                    {
                                        Response.Write("<div style='top: 82px;left: 0px;position: absolute;'><a href=?next=2><img src='images/left.png'/></a></div>");
                                    }


                                    Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                    string testname = "";
                                    while (dr1.Read())
                                    {
                                        testname = dr1[1].ToString();
                                        if (test <= 3)
                                        {
                                            try
                                            {
                                                string[] t = new string[7];

                                                t = dr1[0].ToString().Split(' ');
                                                String timet = t[0].ToString() + t[1].ToString();
                                                //Session["org_time"] = dr1[0].ToString();
                                                string docidpass = obj.EnryptString(dr1[1].ToString());
                                                string docdate = obj.EnryptString(cdate.ToString());
                                                string doctime = obj.EnryptString(t[0].ToString());
                                                string timeperiod = obj.EnryptString(t[1].ToString());
                                                //string hospitalId = obj.EnryptString(dr[13].ToString());
                                                //string lat = obj.EnryptString(Lat.ToString());
                                                //string longti = obj.EnryptString(Long.ToString());

                                           //     string lat = Lat.ToString();
                                              //  string longti = Long.ToString();


                                                SqlCommand com = new SqlCommand("select a_status from tbl_hos_doc_appmnt where d_id='" + dr1[1].ToString() + "' and a_time='" + dr1[0].ToString() + "' and date='" + cdate.ToString() + "'", con);

                                                int a_status;
                                                try
                                                {
                                                    a_status = Convert.ToInt32(com.ExecuteScalar());
                                                }
                                                catch (Exception ex)
                                                {
                                                    a_status = 2;
                                                }

                                                //if (a_status == 0)
                                                //{
                                                //    Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
                                                //}
                                                //else 
                                                if (a_status == 1)
                                                {
                                                    Response.Write("<a title='Appointment Taken' class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
                                                }
                                                else
                                                {

                                                    Response.Write("<a class='btn btn-xs btn-success btn-hover'  style='text-align: center; margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + ">" + dr1[0].ToString() + "</a>");

                                                }

                                                Response.Write("<br>");


                                            }
                                            catch (Exception ex)
                                            {

                                                Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label>");
                                                Response.Write("<br>");
                                            }
                                            test++;
                                        }

                                    }
                                    if (b == 0)

                                    {

                                    }
                                    else
                                    {
                                        for (int p = 0; p <= b; p++)

                                        {
                                            Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label>");
                                            Response.Write("<br>");
                                        }
                                    }
                                    if (test > 3)
                                    {
                                        string docidpass = obj.EnryptString(testname.ToString());

                                        //string lat = obj.EnryptString(Lat.ToString());
                                        //string longti = obj.EnryptString(Long.ToString());

                                        //string lat = Lat.ToString();
                                      //  string longti = Long.ToString();
                                        //  Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                        //   Response.Write("<br>");

                                        Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=Doctoravailabledateandtime.aspx?docid=" + docidpass  + "> More</a>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                    for (int ii = 0; ii <= 4; ii++)
                                    { Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label><br>"); }



                                }
                                Response.Write("</div></div");

                                dr1.Close();
                            }
                            catch (Exception ex)
                            {

                            }
                            dt = dt.AddDays(1);
                            // Response.Write("</li>");
                            j++;

                        }




                    }

                    else
                    {

                        Response.Write("<div style='padding-left:42px;'><div style='display:inline-block;padding-left:8px;height:auto'>");
                        //       string cdate = dt.ToString("yyyy-MM-dd");
                        // DateTime  dte = Convert.ToDateTime(cdate);
                        qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate + "'";

                        cmd = new SqlCommand(qry, con);
                        dr1 = cmd.ExecuteReader();
                        SqlDataAdapter adpt1 = new SqlDataAdapter(qry, con);
                        DataSet dts1 = new DataSet();
                        adpt1.Fill(dts1);
                        int count = dts1.Tables[0].Rows.Count;
                        int b = 0;
                        if (count < 4)
                        {
                            b = 4 - count;
                        }
                        try
                        {
                            int test = 0;

                            if (dr1.HasRows)
                            {
                                if (Request.QueryString["next"] == "1")
                                {
                                    Response.Write("<div style='top: 82px;left: 0px;position: absolute;'><a href=?next=2><img src='images/left.png'/></a></div>");
                                }

                                Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                string testname = "";
                                while (dr1.Read())
                                {
                                    testname = dr1[1].ToString();
                                    if (test <= 3)
                                    {
                                        try
                                        {
                                            string[] t = new string[7];

                                            t = dr1[0].ToString().Split(' ');
                                            String timet = t[0].ToString() + t[1].ToString();
                                            //Session["org_time"] = dr1[0].ToString();
                                            string docidpass = obj.EnryptString(dr1[1].ToString());
                                            string docdate = obj.EnryptString(cdate.ToString());
                                            string doctime = obj.EnryptString(t[0].ToString());
                                            string timeperiod = obj.EnryptString(t[1].ToString());

                                            //string lat = obj.EnryptString(Lat.ToString());
                                            //string longti = obj.EnryptString(Long.ToString());

                                          //  string lat = Lat.ToString();
                                           // string longti = Long.ToString();


                                            SqlCommand com = new SqlCommand("select a_status from tbl_hos_doc_appmnt where d_id='" + dr1[1].ToString() + "' and a_time='" + dr1[0].ToString() + "' and a_date='" + cdate.ToString() + "'", con);

                                            int a_status;
                                            try
                                            {
                                                a_status = Convert.ToInt32(com.ExecuteScalar());
                                            }
                                            catch (Exception ex)
                                            {
                                                a_status = 2;
                                            }

                                            //if (a_status == 0)
                                            //{
                                            //    Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
                                            //}
                                            //else 
                                            if (a_status == 1)
                                            {
                                                Response.Write("<a title='Appointment Taken' class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
                                            }
                                            else
                                            {
                                                Response.Write("<a class='btn btn-xs btn-success btn-hover'  style='text-align: center; margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() +  ">" + dr1[0].ToString() + "</a>");

                                            }

                                            Response.Write("<br>");


                                        }
                                        catch (Exception ex)
                                        {

                                            Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label>");
                                            Response.Write("<br>");
                                        }
                                        test++;
                                    }

                                }
                                if (b == 0)

                                {

                                }
                                else
                                {
                                    for (int p = 0; p <= b; p++)

                                    {
                                        Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label>");
                                        Response.Write("<br>");
                                    }
                                }
                                if (test > 3)
                                {
                                    string docidpass = obj.EnryptString(testname.ToString());

                                    //string lat = obj.EnryptString(Lat.ToString());
                                    //string longti = obj.EnryptString(Long.ToString());

                                    //string lat = Lat.ToString();
                                    //string longti = Long.ToString();
                                    //  Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                    //   Response.Write("<br>");

                                    Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=Doctoravailabledateandtime.aspx?docid=" + docidpass + "> More</a>");
                                }
                            }
                            else
                            {
                                Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                for (int ii = 0; ii <= 4; ii++)
                                { Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label><br>"); }



                            }
                            Response.Write("</div></div");

                            dr1.Close();
                        }
                        catch (Exception ex)
                        {

                        }
                        dt = dt.AddDays(1);
                        // Response.Write("</li>");
                        j++;
                    }
                }
                if (j == 3)
                {

                    Response.Write("</li>");
                    Response.Write("<li>");






                    string cdateo = dt.ToString("yyyy-MM-dd");
                    DateTime dt1 = dt.AddDays(1);
                    string cdate1 = dt1.ToString("yyyy-MM-dd");

                    DateTime dt2 = dt1.AddDays(1);
                    string cdate2 = dt2.ToString("yyyy-MM-dd");
                    Session["cdate2"] = cdate2.ToString();
                    // Session["datedis"] = dt2.ToString("ddd MMM dd");
                    qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdateo + "' ";


                    SqlDataAdapter adpt111 = new SqlDataAdapter(qry, con);
                    DataSet dts111 = new DataSet();
                    adpt111.Fill(dts111);
                    int count1 = dts111.Tables[0].Rows.Count;
                    if (count1 == 0)
                    {
                        qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate1 + "' ";


                        SqlDataAdapter adpt222 = new SqlDataAdapter(qry, con);
                        DataSet dts222 = new DataSet();
                        adpt222.Fill(dts222);
                        int count2 = dts222.Tables[0].Rows.Count;
                        if (count2 == 0)
                        {
                            qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate2 + "' ";


                            SqlDataAdapter adpt333 = new SqlDataAdapter(qry, con);
                            DataSet dts333 = new DataSet();
                            adpt333.Fill(dts333);
                            int count3 = dts333.Tables[0].Rows.Count;

                            if (count3 == 0)
                            {

                                Session["count"] = "1";
                            }
                        }
                    }

                    if (Session["count"].ToString() == "1")
                    {

                        Session["count"] = 0;

                        Response.Write("<div style='padding-left:42px;'><div style='display:inline-block;padding-left:8px;height:auto'>");
                        //   string cdate = dt.ToString("yyyy-MM-dd");
                        // DateTime  dte = Convert.ToDateTime(cdate);

                        Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



                        Response.Write("</div>");
                        dt = dt.AddDays(1);
                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                        // DateTime  dte = Convert.ToDateTime(cdate);

                        Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                        Response.Write("</div>");
                        dt = dt.AddDays(1);
                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                        // DateTime  dte = Convert.ToDateTime(cdate);

                        Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

                        Response.Write("</div>");


                        //DateTime newdate = Session["cdate2"];
                        //     dt.ToString("ddd MMM dd")
                        string extqry = "select   top 1 date from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date>'" + Session["cdate2"].ToString() + "'";
                        SqlCommand com1 = new SqlCommand(extqry, con);
                        string nextdate = "";
                        try
                        {
                            nextdate = com1.ExecuteScalar().ToString();
                            Session["nextdate"] = nextdate.ToString();
                            DateTime datenew = Convert.ToDateTime(nextdate);

                            nextdate = datenew.ToString("ddd MMM dd");

                            string docidpass = obj.EnryptString(dr[9].ToString().ToString());
                            string docdate = obj.EnryptString(Session["nextdate"].ToString());
                            //string doctime = obj.EnryptString(t[0].ToString());
                            //string timeperiod = obj.EnryptString(t[1].ToString());
                            //string lat = obj.EnryptString(Lat.ToString());
                            //string longti = obj.EnryptString(Long.ToString());
                            //Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                            //Response.Write("<br>");



                            Response.Write("<label style='margin-top:35px; margin-left:30px' ><a  class='btn btn-success' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
                            //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                            Response.Write("</div>");

                        }
                        catch (Exception ex)
                        {
                            nextdate = "";
                            Response.Write("<label style='margin-top:35px; margin-left:10px'><a href=?next=2  class='btn btn-success'> Not Available on remaining Dates</a></label>");
                            //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                            Response.Write("</div>");
                            break;
                            // dt.AddDays(2);

                        }

                        //  Session["datedis"] 


                        //   Response.Write("Next availability");
                        //  Response.Write("<label style='margin-top:35px; margin-left:30px'  class='btn btn-success'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
                        ////  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                        //  Response.Write("</div>");

                        // dr1.Close();


                        // Response.Write("</li>");


                        Response.Write("</li>");
                        Response.Write("<li>");

                        Session["count"] = "0";


                    }




                    j = 0;
                    dt.AddDays(2);
                }
            }
                Response.Write("</ul>");

                ////////////////////////////////////////////////////////////////////////////////////////////








                /*      Response.Write("</td></tr>");
                      Response.Write("</table>");
                      Response.Write("</div>");

      */
                //testd++;

                //}
                //else if(testd==1)
                //{
                //    Response.Write("</li>");
                //    Response.Write("<li>");
                //    testd = 0;
                //}


                Response.Write("</td></tr></table>");

                Response.Write("</div>");
            }

            Response.Write("</div>");

            dr.Close();


            //span Label1 close here
            Response.Write("</div></div></span>");
            //Response.Write("</span>");
        }

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_name == Session["hos"].ToString()
                    select new { item1.h_id, item.latitude };
        //try
        //{
            if (query.Count() <= 0)
            {
           // Label8.Text = "You must set your location";
           // ModalPopupExtender4.Show();
            // Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
        }
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("../index/hospita login.aspx");
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetHospitalLocation.aspx");
        //searchfiltr();
    }

    protected void BtnTakeAppointment_Click(object sender, EventArgs e)
    {
       
            var Query = from item in db.tbl_hos_doc_appmnts where item.d_id == Session["did"].ToString() && item.a_date == TxtApntmtDate.Text && item.u_id==TxtBookDocUserId.Text select item;
            if (Query.Count() > 0)
            {
            //RegisterStartupScript("", "<Script Language=JavaScript>alert('Already you take an appointment this day, so you please choose another day.')</Script>");
            Label1.Text = "Already you take an appointment this day, so you please choose another day.";
            this.ModalPopupExtender2.Show();
        }
            else
            {
                tbl_hos_doc_appmnt ha = new tbl_hos_doc_appmnt()
                {
                    d_id = Session["did"].ToString(),
                    a_date = TxtApntmtDate.Text,
                    u_id = TxtBookDocUserId.Text,
                    a_status = 1,
                    a_payment = DdlPayments.SelectedItem.Text,
                    a_reason = TxtReasonToVisit.SelectedItem.Text,
                    h_id = Session["hos"].ToString(),
                    a_time = TxtApointmentTime.Text,
                };
                db.tbl_hos_doc_appmnts.InsertOnSubmit(ha);
                db.SubmitChanges();
               // Availability();
                string msg = "Dear patient, your appointment is fixed. Thank you" + "<br />" + " Hakkeem Team.";
            //  Email.mail(Session["user"].ToString(), msg, "Appointment fixed");

            ///  Response.Redirect("~/User/Finish appointment.aspx");
            //RegisterStartupScript("", "<Script Language=JavaScript>alert('Your appointment is fixed')</Script>");

            Label1.Text = "Your appointment is fixed";
            this.ModalPopupExtender2.Show();



        }
       
    }

    protected void TxtBookDocUserId_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        searchfiltr();
        // Response.Redirect("SetHospitalLocation.aspx");
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        //list_doctors(TextBox1.Text, "", "");
        searchfiltr();
    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
       // searchfiltr();

    }

    protected void TextBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        searchfiltr();
    }
}

   


//public void doctorDetails()
//{
//    DataTable dt = new DataTable();
//    dt.Columns.AddRange(new DataColumn[1] { new DataColumn("doctor") });
//    var Query = (from item in db.tbl_hos_doc_availables where item.h_id == Session["hos"].ToString() select item.hd_id).Distinct();
//    foreach (var ss in Query)
//    {
//        dt.Rows.Add(ss);
//    }
//    DataList1.DataSource = dt;
//    DataList1.DataBind();
//    foreach (DataListItem dl in DataList1.Items)
//    {
//        Label lbl1 = dl.FindControl("Label1") as Label; Label lbl2 = dl.FindControl("Label2") as Label;
//        var Query1 = from item in db.tbl_hdoctors where item.h_id == Session["hos"].ToString() && item.hd_email == lbl1.Text select item;
//        foreach (var ss in Query1)
//        { lbl2.Text = "Dr."+ss.hd_name; }
//        DataTable dt2 = new DataTable();
//        dt2.Columns.AddRange(new DataColumn[1] { new DataColumn("date") });
//        var Query2 = (from item in db.tbl_hos_doc_availables where item.h_id == Session["hos"].ToString() && item.hd_id == lbl1.Text select item.date).Take(10);

//        foreach (var s in Query2)
//        {
//            dt2.Rows.Add(s);

//        }

//        DataList dl2 = dl.FindControl("DataList2") as DataList;
//        dl2.DataSource = dt2;
//        dl2.DataBind();

//        foreach (DataListItem dli2 in dl2.Items)
//        {
//            Label lbl4 = dli2.FindControl("Label4") as Label;
//            Label lbl3 = dli2.FindControl("Label3") as Label;
//            GridView gv3 = dli2.FindControl("GridView3") as GridView;
//            var Query3 = from item in db.tbl_hos_doc_availables where item.hd_id == lbl1.Text && item.date == lbl4.Text select item.hd_a_time;
//            foreach (var t in Query3)
//            {
//                string[] a = t.Split(',');
//                DataTable dt3 = new DataTable();
//                dt3.Columns.AddRange(new DataColumn[3] { new DataColumn("time"),new DataColumn("date"),new DataColumn("doctor") });
//                for (int i = 0; i < a.Count(); i++)
//                {
//                    var Query4 = from item in db.tbl_hos_doc_appmnts where item.a_date == lbl4.Text && item.a_time == a[i].ToString() && item.d_id == lbl1.Text && item.h_id == Session["hos"].ToString() select item;
//                    if (Query4.Count() > 0)
//                    {
//                        dt3.Rows.Add(" ", lbl4.Text, lbl1.Text);
//                        if (dt3.Rows.Count > 5)
//                            break;
//                    }
//                    else
//                    {
//                        dt3.Rows.Add(a[i].ToString(), lbl4.Text, lbl1.Text);
//                        if (dt3.Rows.Count > 5)
//                            break;
//                    }
//                }
//                gv3.DataSource = dt3;
//                gv3.DataBind();
//                foreach(GridViewRow gr in gv3.Rows)
//                {
//                    Button btn1 = gr.FindControl("Button1") as Button;
//                    if (btn1.Text == " ")
//                    {
//                        btn1.Enabled = false;
//                        btn1.Text="Booked";
//                    }
//                }
//            }

//        }
//    }
//}







//protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
//{
//    if(e.CommandName=="apmnt")
//    {
//        string[] commmandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
//        TxtApointmentTime.Text = commmandArgs[0];
//        TxtApntmtDate.Text = commmandArgs[1];
//        Session["did"] = commmandArgs[2];
//        this.ModalPopupExtender1.Show();
//    }
//    if(e.CommandName=="next")
//    {

//            Session["doctor"] = e.CommandArgument.ToString();

//        Response.Redirect("~/Hospital/Doctor available date and time.aspx");
//    }
//}

//protected void BtnTakeAppointment_Click(object sender, EventArgs e)
//{
//    var selct = from a in db.tbl_hos_doc_appmnts where a.a_date == TxtApntmtDate.Text && a.d_id == Session["did"].ToString() && a.h_id == Session["hos"].ToString()&&a.u_id==TxtBookDocUserId.Text select a;
//    if (selct.Count()>0)
//    {
//        RegisterStartupScript("", "<Script Language=JavaScript>alert('Already have an appointment')</Script>");
//    }
//    else
//    {
//        var selectUser = from item in db.tbl_signups
//                         where item.email == TxtBookDocUserId.Text
//                         select item;
//        if (selectUser.Count() > 0)
//        {
//            tbl_hos_doc_appmnt tbl = new tbl_hos_doc_appmnt
//            {
//                a_time = TxtApointmentTime.Text,
//                a_payment = DdlPayments.SelectedItem.Text,
//                a_date = TxtApntmtDate.Text,
//                a_reason = TxtReasonToVisit.Text,
//                a_status = 0,
//                d_id = Session["did"].ToString(),
//                h_id = Session["hos"].ToString(),
//                u_id = TxtBookDocUserId.Text,
//            };
//            db.tbl_hos_doc_appmnts.InsertOnSubmit(tbl);
//            db.SubmitChanges();
//            doctorDetails();
//            RegisterStartupScript("", "<Script Language=JavaScript>alert('Appointment set succesfully')</Script>");

//        }
//        else
//        {
//            Page.RegisterStartupScript("", "<Script Language=JavaScript>alert('The Hakkeem user id doesn't exist. Please give correct Id.')</Script>");
//        }
//    }
//}


//public void TodatApmnt()
//{

//    DataTable dt = new DataTable();
//    dt.Columns.AddRange(new DataColumn[3] { new DataColumn("a_date"), new DataColumn("d_id"), new DataColumn("hd_name") });

//    var Query = from item in db.tbl_hos_doc_appmnts where item.h_id == Session["hos"].ToString() && item.a_date == (DateTime.Now).ToString("yyyy-MM-dd") select item;
//    if (Query.Count() > 0)
//    {
//        //DataList3.DataSource = Query;
//        //DataList3.DataBind();
//        foreach (var s in Query)
//        {
//            var selectQuery = from item in db.tbl_hdoctors
//                              where item.hd_email == s.d_id
//                              select item;
//            foreach (var ss in selectQuery)
//            {

//                dt.Rows.Add(s.a_date, s.d_id, ss.hd_name);

//            }
//            DataView view = new DataView(dt);
//            DataTable dvalue = view.ToTable(true, "a_date", "d_id", "hd_name");
//            DataList3.DataSource = dvalue;
//            DataList3.DataBind();
//            foreach (DataListItem dl3 in DataList3.Items)
//            {
//                Label lbl4 = dl3.FindControl("Label4") as Label;
//                Label lbl5 = dl3.FindControl("Label5") as Label;
//                Label lbl6 = dl3.FindControl("Label6") as Label;
//                var Query2 = from item in db.tbl_hdoctors where item.hd_email == lbl4.Text select item;
//                foreach (var n in Query2) { lbl5.Text = n.hd_name; }

//                DataList dl4 = dl3.FindControl("DataList4") as DataList;
//                var Query1 = from item in db.tbl_hos_doc_appmnts where item.d_id == lbl4.Text && item.h_id == Session["hos"].ToString() && item.a_date == (DateTime.Now).ToString("yyyy-MM-dd") select item;
//                dl4.DataSource = Query1;
//                dl4.DataBind();
//                foreach (DataListItem dl in dl4.Items)
//                {
//                    Button Button2 = dl.FindControl("Button2") as Button;
//                    Label LblDate = dl.FindControl("LblDate") as Label;
//                    Label LblDocName = dl.FindControl("LblDocName") as Label;
//                    Label LblDocId = dl.FindControl("LblDocId") as Label;
//                    Label LblStatus = dl.FindControl("LblStatus") as Label;

//                    if (LblStatus.Text == "0")
//                    {

//                    }
//                    if (LblStatus.Text == "1")
//                    {
//                        Button2.ToolTip = "Confirmed";
//                        Button2.BackColor = System.Drawing.Color.IndianRed;
//                    }

//                    Button2.CommandArgument = lbl5.Text;
//                    LblDate.Text = lbl6.Text;
//                    LblDocName.Text = lbl5.Text;
//                    LblDocId.Text = lbl4.Text;
//                }
//            }
//        }


//    }
//    else
//    {
//    }
//}
//protected void DataList4_ItemCommand(object source, DataListCommandEventArgs e)
//{
//    if (e.CommandName == "AppointmentDetails")
//    {
//        Button Button2 = e.Item.FindControl("Button2") as Button;
//        Label LblDate = e.Item.FindControl("LblDate") as Label;
//        Label LblDocName = e.Item.FindControl("LblDocName") as Label;
//        Label LblDocId = e.Item.FindControl("LblDocId") as Label;
//        Label LblStatus = e.Item.FindControl("LblStatus") as Label;

//        Session["A_Time"] = Button2.Text;
//        Session["A_Date"] = LblDate.Text;
//        Session["Doc_Name"] = LblDocName.Text;
//        Session["Doc_Id"] = LblDocId.Text;
//        Session["A_Status"] = LblStatus.Text;
//        Response.Redirect("~/Hospital/ApointmentDetails.aspx");
//    }
//}
//}