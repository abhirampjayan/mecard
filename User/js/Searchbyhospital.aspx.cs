using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Search_by_hospital : System.Web.UI.Page
{
    secure obj = new secure();
    databaseDataContext db = new databaseDataContext();
    //SqlConnection con = new SqlConnection("Data Source=TAHCOM-1\\SQLEXPRESS;Initial Catalog=db_BookDoc;User ID=sa;Password=admin123;MultipleActiveResultSets=True");
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

    SqlCommand cmd,com;
    SqlDataReader dr, dr1, dr2;
    int pagestart = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();
        if (!IsPostBack)
        {

            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            DateTime time = new DateTime();
            time = DateTime.Now;
            string displayTime = time.ToString("hh:mm tt");
            // MessageBox.Show(displayTime.ToString());
            // MessageBox.Show(dt.ToString("yyyy-MM-dd"));
            string dat = dt.ToString("yyyy-MM-dd");
            com = new SqlCommand("select id from tbl_hos_doc_available where date<'" + dat + "'", con);
            SqlDataReader dtr = com.ExecuteReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    com = new SqlCommand("delete from tbl_hos_doc_time where date_id='" + dtr[0].ToString() + "'", con);
                    com.ExecuteNonQuery();
                }
            }
            dtr.Close();
            com = new SqlCommand("delete from tbl_hos_doc_available where date<'" + dat + "'", con);
            com.ExecuteNonQuery();

            com = new SqlCommand("select id from tbl_hos_doc_available where date='" + dat + "'", con);
            dtr = com.ExecuteReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    
                    string pmqry = "select distinct(LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))) from tbl_hos_doc_time where date_id='" + dtr[0].ToString() + "'";
                    SqlCommand compm = new SqlCommand(pmqry, con);
                    string timepm = compm.ExecuteScalar().ToString();
                    int l = timepm.ToString().Count();

                    string ab2 = timepm.ToString().Substring(l - 2, 2);
                    if (ab2 == "PM")
                    {
                        com = new SqlCommand("delete from tbl_hos_doc_time where date_id='" + dtr[0].ToString() + "' and time like '%AM'", con);
                        com.ExecuteNonQuery();
                    }

                    if (timepm.ToString().Contains("12"))
                    {
                        com = new SqlCommand("delete from tbl_hos_doc_time where date_id='" + dtr[0].ToString() + "' and time < LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7)) and time not like '1%'", con);
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        com = new SqlCommand("delete from tbl_hos_doc_time where date_id='" + dtr[0].ToString() + "' and time < LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))", con);
                        com.ExecuteNonQuery();
                    }
                }
            }

           
            com = new SqlCommand("delete from tbl_hos_doc_available where id not in (select date_id from tbl_hos_doc_time)", con);
            com.ExecuteNonQuery();


            Session["count"] = 0;
           // list_doctors("", "", "", "");
            try
            {
                try
                {
                    if (Request.QueryString["hos"].ToString() != "")
                    {
                        Response.Redirect("doctordetails.aspx?hos=" + Request.QueryString["hos"].ToString());
                    }
                }
                catch (Exception ex)
                {

                }
                if (Request.QueryString["hos1"].ToString() != "")
                {
                    // Response.Redirect("hospitaldr.aspx?hos=" + Request.QueryString["hos"].ToString());
                 string   query = "where h_name like '%" + Request.QueryString["hos1"] + "%'  and h_status = '1'";
                    list_doctors(query, "", "", "");
                }
            }
            catch(Exception ex)
            {
               

                list_doctors("", "", "", ""); }
        }

    }


    public void list_doctors(string q,string docName, string location, string hosName)
    {

        DataSet dts = new DataSet();

        SqlDataAdapter adpt;
        if (q != "")
        {
            Session["q"] = q.ToString();
            // string qqqq = q.Replace("where", " and ");
            adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time  " + q + " and h_status='1'", con);
            dts.Clear();

            adpt.Fill(dts);
        }
        else
        {
            adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time where h_status='1'", con);
            dts.Clear();

            adpt.Fill(dts);
        }

        try
        {
            if (Session["q"] != "")
            {
                adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time  " + Session["q"].ToString() + " and h_status='1'", con);
                dts.Clear();

                adpt.Fill(dts);
            }
        }
        catch (Exception ex)
        {

        }

      
        if (dts.Tables[0].Rows.Count == 0)
        {
            //RegisterStartupScript("", "<script>alert('no')</script>");
        }
        decimal c = Convert.ToDecimal(dts.Tables[0].Rows.Count);
        c = c / 5;


        decimal d = Math.Ceiling(c);






        try
        {
            if (pagestart == 0)
            {
                pagestart = 1;
            }
            else
            {

                if (Request.QueryString["page"] == null)
                {
                    pagestart = 1;


                }
                else
                {



                    pagestart = Convert.ToInt32(Request.QueryString["page"]);
                }
            }

        }
        catch (Exception ex)
        {
            pagestart = 1;
        }

        if (pagestart == 1)
        {
            //   cmd = new SqlCommand("select   distinct top 5 (hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hospital"].ToString() + "' and row_number>=1 ORDER BY hd_name, row_number", con);
            Session["q"] = "";
            if (q != "")
            {
                string qqqq = q.Replace("where", " and ");
                Session["q"] = q.ToString();
               
                    cmd = new SqlCommand("select  top 5  * from view_test_hos_doc where t>='" + pagestart + "' " + qqqq + "and h_status='1' order by t", con);
                
                //  cmd = new SqlCommand("select  top 5    * from view_test_hos_doc where d_status='1' and  t>='" + pagestart + "' " + qqqq + "order by t", con);
                //   cmd = new SqlCommand("select distinct(d_name) as d_name from view_hos_doc_available_time" + q, con);
            }
            else
            {
                cmd = new SqlCommand("select  top 5    * from view_test_hos_doc where t>='" + pagestart + "' and h_status='1' order by t", con);
                // cmd = new SqlCommand("select distinct(d_name) as d_name from view_hos_doc_available_time", con);
            }


        }
        else
        {
            //  pagestart = (pagestart + pagestart) - 2;

            pagestart = 1 + ((pagestart - 1) * 5);
            //  cmd = new SqlCommand("select   * from view_test where t>='" + pagestart + "'", con);
            //  cmd = new SqlCommand("select   * from view_test_hos_doc where t>='" + pagestart + "'", con);
            try
            {
                if (Session["q"] != "")
                {


                    string qqqq = Session["q"].ToString().Replace("where", " and ");
                    
                   
                        cmd = new SqlCommand("select  top 5  * from view_test_hos_doc where t>='" + pagestart + "' " + qqqq + "and h_status='1' order by t", con);
                    

                    //   cmd = new SqlCommand("select distinct(d_name) as d_name from view_hos_doc_available_time" + q, con);
                }
                else
                {
                    cmd = new SqlCommand("select  top 5    * from view_test_hos_doc where t>='" + pagestart + "' and h_status='1' order by t", con);
                    // cmd = new SqlCommand("select distinct(d_name) as d_name from view_hos_doc_available_time", con);
                }
            }
            catch (Exception ex)
            {
                cmd = new SqlCommand("select  top 5    * from view_test_hos_doc where t>='" + pagestart + "' and h_status='1' order by t ", con);
            }


        }

        SqlDataAdapter mapAdapter = new SqlDataAdapter(cmd);

        //DataTable dtMap = new DataTable();
        //mapAdapter.Fill(dtMap);
        //rptMarkers.DataSource = dtMap;
        //rptMarkers.DataBind();


        dr = cmd.ExecuteReader();






        //Response.Write("<span id='Label2' style='margin-bottom:1000px;'>");

        //Response.Write("<span id='Label1' class='col-md-8' style='height:100px;width:800px;Z-INDEX: 302; LEFT: 20px; POSITION: absolute; TOP: 146px;/*overflow-y:scroll*/'>");

        Response.Write("<span id='Label1' class='col-md-8'>");
       
        // Response.Write("<div id='doctors' style='overflow-y:scroll;'>");
        Response.Write("<div>");
        Response.Write("<table class='table table-responsive' style='border-collapse:separate;border-spacing:0px 10px;border-color:#ecf0f5;'>");
        //Response.Write("<div>");

        string Lat = "";
        string Long = "";
        //string docid = "";
        DataTable dtMap = new DataTable();
        dtMap.Columns.AddRange(new DataColumn[] { new DataColumn("hd_id"), new DataColumn("Latitude"), new DataColumn("Longitude"), new DataColumn("h_name"), new DataColumn("h_address"), new DataColumn("h_photo"), new DataColumn("id"), new DataColumn("h_id") });



        while (dr.Read())
        {
            string hospitalId = obj.EnryptString(dr[14].ToString());
            dtMap.Rows.Add(dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), dr[12].ToString(), dr[3].ToString(), dr[13].ToString());
            string hosId = "";
            var query1 = from item in db.tbl_hospitalregs
                         join item1 in db.tbl_hos_locations on item.h_id equals item1.h_id
                         where item.h_regno == dr[13].ToString()
                         select new { item1.latitude, item1.longitude, item.h_regno };
            if (query1.Count() > 0)
            {
                foreach (var s in query1)
                {
                    Lat = s.latitude.ToString();
                    Long = s.longitude.ToString();
                    hosId = s.h_regno.ToString();
                }
            }
            else
            {
                hosId = "";
            }


            string qry = "select * from tbl_hdoctor where hd_name='" + dr[0].ToString() + "' ";

            cmd = new SqlCommand(qry, con);
            dr2 = cmd.ExecuteReader();
            Response.Write("<tr class='box box-primary'>");
            //Response.Write("<div>");

            while (dr2.Read())
            {
                Response.Write("<td><div onmouseover='hover(" + hosId + ")' onmouseout='out(" + hosId + ")'>");
                //Response.Write("onMouseOver='hover("+dr2[0].ToString()+")' onMouseOut='out(" + dr2[0].ToString() + ")' >");
                //Response.Write("onmouseover='hover(" + dr2[0].ToString() + ")' onmouseout='out(" + dr2[0].ToString() + ")' >");

                string img;

                if (dr2[18].ToString() == "")
                {
                    img = "<img class='img-circle'  src='../doctorimages/doctor.png'>";
                }
                else
                {
                    img = "<img class='img-circle' src='" + dr2[18].ToString() + "'>";

                    //  img = "<img class='img img-responsive' src='../doctorimages/868268images (2).jpg'>";
                }

                // string img = "<img class='img img-rounded img-responsive' src='../doctorimages/doctor.png'>";

                string about = "/*<Label style='font-size:5px'>*/";
                try
                {

                    about = "<label style='font-size:small'>" + dr[11].ToString().Substring(0, 100) + "</label>";
                   // about = about + "...<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'  href=Viewhospitaldoctorreview.aspx?docid=" +obj.EnryptString(dr2[5].ToString()) + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">More </a>";

                    // about = about + "......<a href=Viewhospitaldoctorreview.aspx?docid=" + dr2[5].ToString() + "> More </a>";
                }

                catch (Exception ex)
                {
                    about = dr[11].ToString();
                }




                about = about + "</Label>";




                //  string about = "<Label style='font-size:small'>"+ dr2[25].ToString().Substring(0, 100)+"</Label>";


                Response.Write("<div class='row'><div class='col-md-2'><div class='user-panel'><div class='image'>" + img + "</div></div></div>");

                Response.Write("<div class='col-md-4'>");
                Response.Write("<p id='doctornameo'><a id='doctorname' href=Viewhospitaldoctorreview.aspx?docid=" + obj.EnryptString(dr2[5].ToString()) + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">" + dr[0].ToString() + "</a><p>");
                Response.Write(" <p><a id='hospitalname' href=?hos=" + obj.EnryptString(dr[14].ToString()) + "&docid=" + obj.EnryptString(dr2[5].ToString()) + ">" + dr[10].ToString() + " </a></p>");
                     Response.Write("<Label id='speciality1'>" + dr2[15].ToString() + "</Label></br>");
                Response.Write("<div id='marker'><i id='marker1' class='fa fa-map-marker'></i><Label id='adress'> " + about.ToString() + " </Label></div>");
                Response.Write("</div>");


              //  Response.Write("<div class='col-md-2'><div class='user-panel'><div class='image'>" + img + "</div><div class='col-md-4'>" + "<p><a id='doctorname' href=Viewhospitaldoctorreview.aspx?docid=" + obj.EnryptString(dr2[5].ToString()) + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">" + dr[0].ToString() + "</a> </p><p><a id='hospitalname' href =?hos=" + obj.EnryptString(dr[14].ToString()) + "&docid=" + obj.EnryptString(dr2[5].ToString()) + " > " + dr[10].ToString() + " </a></p><br><Label style='color:#000000;margin-top:-10%;font-weight:normal;font-size:13px;padding-top:2px;font-family:sharp-sans-semibold,Arial,sans-serif;'>" + dr2[15].ToString() + "</Label><br> <Label style = 'color:#000000;font-size:13px;font-weight:normal;font-family:sharp-sans-semibold,Arial,sans-serif;text-align:left;' > " + about.ToString() + " </Label></div>");
                // Response.Write("<td class='col-md-2'>" + img + "</td><td class='col-md-2'>" + "<a style='color:#000000;font-weight:bold;font-size:medium' href=Viewhospitaldoctorreview.aspx?docid=" +obj.EnryptString(dr2[5].ToString()) + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">" + dr[0].ToString() + "</a> <br><Label style='color:#000000;font-weight:normal;'>" + dr2[15].ToString() + "</Label><br>" + about.ToString() + "</td>");
                //Response.Write("<div style='display:inline-block;' ><div  class='col-md-2'>" + img + "</div><div class='col-md-2'>" + "<a style='color:#18bc9c;font-weight:bold;font-size:medium' href=Viewhospitaldoctorreview.aspx?docid=" + dr2[5].ToString() + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">" + dr[0].ToString() + "</a> <br><Label style='color:#18bc9c;font-weight:normal;'>" + dr2[15].ToString() + "</Label><br>" + about.ToString() + "</div> </div>");



            }
            Response.Write("<div class='col-md-6'>");
            //Response.Write("<div class='col-md-4'>");
            dr2.Close();
            DateTime dt = new DateTime();
            dt = DateTime.Now.Date;
            if (Request.QueryString["next"] == "1")
            {

                string date = Request.QueryString["docdate"];


                string datee = obj.DecryptString(date);


                DateTime dtnew = Convert.ToDateTime(datee);

                dt = dtnew;
                //list_doctorstest("", "", "", "", "", "", dtnew);
                //  Request.QueryString["next"] = "0";
            }
            int j = 0;

            Response.Write("<ul class='bxslider'>");
            Response.Write("<li>");
            for (int i = 0; i < 30; i++)
            {


                string cdate = dt.ToString("yyyy-MM-dd");

                if (j < 3)
                {



                    if (j == 0)
                    {

                       

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

                            Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
                            //   string cdate = dt.ToString("yyyy-MM-dd");
                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



                            Response.Write("</div>");
                            dt = dt.AddDays(1);
                            Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                            Response.Write("</div>");
                            dt = dt.AddDays(1);
                            Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

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
                                //Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=Hospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                //Response.Write("<br>");



                                Response.Write("<label style='margin-top:50px; margin-left:30px' ><a  class='btn btn-success' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
                                //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                                Response.Write("</div>");

                            }
                            catch (Exception ex)
                            {
                                nextdate = "";
                                Response.Write("<label  id='notavailable'><a href=?next=2  class='btn btn-success'><span id='available1'> Not Available on remaining Dates</span></a></label>");
                                //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                                Response.Write("</div>");
                                break;
                                // dt.AddDays(2);

                            }

                            //  Session["datedis"] 


                            //   Response.Write("Next availability");
                            //  Response.Write("<label style='margin-top:50px; margin-left:30px'  class='btn btn-success'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
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
                            Response.Write("<div  id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
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


                                    Response.Write("<Label  id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
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

                                                string lat = Lat.ToString();
                                                string longti = Long.ToString();


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
                                                if (a_status == 1 || a_status==3)
                                                {
                                                    Response.Write("<a title='Appointment Taken' class='btn btn-xs btn-success btn-hover' id='buttontime'>" + dr1[0].ToString() + "</a>");
                                                }
                                                else
                                                {

                                                    Response.Write("<a class='btn btn-xs btn-success btn-hover' id='buttontime' href=Hospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + "&hospitalregno="+hospitalId.ToString()+">" + dr1[0].ToString() + "</a>");

                                                }

                                                Response.Write("<br>");


                                            }
                                            catch (Exception ex)
                                            {

                                                Response.Write("<Label class='btn btn-xs btn-success btn-hover id='buttontime'>----</Label>");
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
                                            Response.Write("<Label class='btn btn-xs btn-success btn-hover' id='buttontime'>----</Label>");
                                            Response.Write("<br>");
                                        }
                                    }
                                    if (test > 3)
                                    {
                                        string docidpass = obj.EnryptString(testname.ToString());

                                        //string lat = obj.EnryptString(Lat.ToString());
                                        //string longti = obj.EnryptString(Long.ToString());

                                        string lat = Lat.ToString();
                                        string longti = Long.ToString();
                                        //  Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=Hospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                        //   Response.Write("<br>");

                                        Response.Write("<a class='btn btn-xs btn-success btn-hover' id='buttontime' href=Hospitaldoctoravailability.aspx?docid=" + docidpass + "&Lat=" + lat.ToString() + "&Long=" + longti + "&hospitalregno=" + hospitalId.ToString() + "> More</a>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                    for (int ii = 0; ii <= 4; ii++)
                                    { Response.Write("<Label class='btn btn-xs btn-success btn-hover' id='buttontime'>----</Label><br>"); }



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


















                        Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
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

                                Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
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

                                            string lat = Lat.ToString();
                                            string longti = Long.ToString();


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
                                            if (a_status == 1 || a_status==3)
                                            {
                                                Response.Write("<a title='Appointment Taken' class='btn btn-xs btn-success btn-hover' id='buttontime'>" + dr1[0].ToString() + "</a>");
                                            }
                                            else
                                            {
                                                Response.Write("<a class='btn btn-xs btn-success btn-hover'id='buttontime' href=Hospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + "&hospitalregno="+hospitalId.ToString()+">" + dr1[0].ToString() + "</a>");

                                            }

                                            Response.Write("<br>");


                                        }
                                        catch (Exception ex)
                                        {

                                            Response.Write("<Label class='btn btn-xs btn-success btn-hover' id='buttontime'>----</Label>");
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
                                        Response.Write("<Label class='btn btn-xs btn-success btn-hover' id='buttontime'>----</Label>");
                                        Response.Write("<br>");
                                    }
                                }
                                if (test > 3)
                                {
                                    string docidpass = obj.EnryptString(testname.ToString());

                                    //string lat = obj.EnryptString(Lat.ToString());
                                    //string longti = obj.EnryptString(Long.ToString());

                                    string lat = Lat.ToString();
                                    string longti = Long.ToString();
                                    //  Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=Hospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                    //   Response.Write("<br>");

                                    Response.Write("<a class='btn btn-xs btn-success btn-hover' id='buttontime' href=Hospitaldoctoravailability.aspx?docid=" + docidpass + "&Lat=" + lat.ToString() + "&Long=" + longti + "&hospitalregno=" + hospitalId.ToString() + "> More</a>");
                                }
                            }
                            else
                            {
                                Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                for (int ii = 0; ii <= 4; ii++)
                                { Response.Write("<Label class='btn btn-xs btn-success btn-hover' id='buttontime'>----</Label><br>"); }



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

                        Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
                        //   string cdate = dt.ToString("yyyy-MM-dd");
                        // DateTime  dte = Convert.ToDateTime(cdate);

                        Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



                        Response.Write("</div>");
                        dt = dt.AddDays(1);
                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                        // DateTime  dte = Convert.ToDateTime(cdate);

                        Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                        Response.Write("</div>");
                        dt = dt.AddDays(1);
                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                        // DateTime  dte = Convert.ToDateTime(cdate);

                        Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

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
                            //Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=Hospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                            //Response.Write("<br>");



                            Response.Write("<label style='margin-top:50px; margin-left:30px' ><a  class='btn btn-success' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
                            //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                            Response.Write("</div>");

                        }
                        catch (Exception ex)
                        {
                            nextdate = "";
                            Response.Write("<label id='notavailable'><a href=?next=2  class='btn btn-success'> <span id='available1'> Not Available on remaining Dates</span></a></label>");
                            //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                            Response.Write("</div>");
                            break;
                            // dt.AddDays(2);

                        }

                        //  Session["datedis"] 


                        //   Response.Write("Next availability");
                        //  Response.Write("<label style='margin-top:50px; margin-left:30px'  class='btn btn-success'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
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
            //tr close here
            Response.Write("</div>");
            //Response.Write("</div></div>");
            Response.Write("</div></div></div>");

        }
        rptMarkers.DataSource = dtMap;
        rptMarkers.DataBind();

        dr.Close();


        Response.Write("</tr>");
        Response.Write("</table>");
        Response.Write("</div>");
        Response.Write("<ul class='pagination'>");

        int pp;

        for (int p = 1; p <= d; p++)
        {
            if (Request.QueryString["page"] == p.ToString())
            {
                Response.Write("<li><a href=?page=" + p + "><font color='red'><u><b>" + p + "</b></u></font></a></li> ");
            }
            else
            {
                Response.Write("<li><a href=?page=" + p + ">" + p + "</a></li>");
            }

        }

        Response.Write("</ul>");
        //span Label1 close here
        Response.Write("</span>");
        //Response.Write("</span>");


        Response.Write("</td>");
        //Response.Write("</div></div>");
        Response.Write("</div></td></tr></table>");



    }

    //public void list_doctorstest(string q, string docName, string location, string hosName, DateTime newDate)
    //{
    //    //DataTable dt1 = this.GetDocLocations(speciality, gender, docName, location, language);
    //    //rptMarkers.DataSource = dt1;
    //    //rptMarkers.DataBind();

    //    DataSet dts = new DataSet();

    //    SqlDataAdapter adpt;
    //    if (q != "")
    //    {
    //        Session["q"] = q.ToString();
    //        // string qqqq = q.Replace("where", " and ");
    //        adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time  " + q, con);
    //    }
    //    else
    //    {
    //        adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time  ", con);
    //    }

    //    try
    //    {
    //        if (Session["q"] != "")
    //        {
    //            adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time  " + Session["q"].ToString(), con);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //    adpt.Fill(dts);
    //    decimal c = Convert.ToDecimal(dts.Tables[0].Rows.Count);
    //    c = c / 2;

    //    decimal d = Math.Ceiling(c);

    //    int pagestart = 1;
    //    try
    //    {
    //        if (Request.QueryString["page"] == null)
    //        {
    //            pagestart = 1;

    //        }
    //        else
    //        {
    //            pagestart = Convert.ToInt32(Request.QueryString["page"]);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        pagestart = 1;
    //    }

    //    if (pagestart == 1)
    //    {
    //        //   cmd = new SqlCommand("select   distinct top 5 (hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hospital"].ToString() + "' and row_number>=1 ORDER BY hd_name, row_number", con);
    //        Session["q"] = "";
    //        if (q != "")
    //        {
    //            string qqqq = q.Replace("where", " and ");
    //            Session["q"] = q.ToString();
    //            cmd = new SqlCommand("select  top 5    * from view_test_hos_doc where   t>='" + pagestart + "' " + qqqq + "order by t", con);
    //            //   cmd = new SqlCommand("select distinct(d_name) as d_name from view_hos_doc_available_time" + q, con);
    //        }
    //        else
    //        {
    //            cmd = new SqlCommand("select  top 5    * from view_test_hos_doc where   t>='" + pagestart + "' order by t", con);
    //            // cmd = new SqlCommand("select distinct(d_name) as d_name from view_hos_doc_available_time", con);
    //        }


    //    }
    //    else
    //    {
    //        //  pagestart = (pagestart + pagestart) - 2;
    //        pagestart = 1 + ((pagestart - 1) * 2);
    //        //  cmd = new SqlCommand("select   * from view_test where t>='" + pagestart + "'", con);
    //        //  cmd = new SqlCommand("select   * from view_test_hos_doc where t>='" + pagestart + "'", con);
    //        try
    //        {
    //            if (Session["q"] != "")
    //            {
    //                string qqqq = Session["q"].ToString().Replace("where", " and ");

    //                cmd = new SqlCommand("select  top 5  * from view_test_hos_doc where  t>='" + pagestart + "' " + qqqq + "order by t", con);
    //                //   cmd = new SqlCommand("select distinct(d_name) as d_name from view_hos_doc_available_time" + q, con);
    //            }
    //            else
    //            {
    //                cmd = new SqlCommand("select  top 5    * from view_test_hos_doc where   t>='" + pagestart + "' order by t", con);
    //                // cmd = new SqlCommand("select distinct(d_name) as d_name from view_hos_doc_available_time", con);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            cmd = new SqlCommand("select  top 5    * from view_test_hos_doc where t>='" + pagestart + "'  order by t ", con);
    //        }


    //    }
    //    dr = cmd.ExecuteReader();

    //    SqlDataAdapter mapAdapter = new SqlDataAdapter(cmd);

    //    //DataTable dtMap = new DataTable();
    //    //mapAdapter.Fill(dtMap);
    //    //rptMarkers.DataSource = dtMap;
    //    //rptMarkers.DataBind();
    //    //Response.Write("<span id='Label2' style='margin-bottom:1000px;'>");

    //    Response.Write("<span id='Label1' class='col-md-8' style='height:100px;width:800px;Z-INDEX: 302; LEFT: 20px; POSITION: absolute; TOP: 144px;/*overflow-y:scroll*/'>");
    //    Response.Write("<ul class='pagination'>");

    //    int pp;
    //    for (int p = 1; p <= d; p++)
    //    {

    //        Response.Write("<li><a href =searchbyhospital.aspx'?page=" + p + " > " + p + " </a></li> ");
    //    }

    //    Response.Write("</ul>");
    //    Response.Write("<table class='table table-responsive' style='border-collapse:separate;border-spacing:0px 10px;'>");
    //    //Response.Write("<div>");

    //    string Lat = "";
    //    string Long = "";
    //    DataTable dtMap = new DataTable();
    //    dtMap.Columns.AddRange(new DataColumn[] { new DataColumn("hd_id"), new DataColumn("Latitude"), new DataColumn("Longitude"), new DataColumn("h_name"), new DataColumn("h_address"), new DataColumn("h_photo"), new DataColumn("id"), new DataColumn("h_id") });


    //    while (dr.Read())
    //    {

    //        dtMap.Rows.Add(dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), dr[12].ToString(), dr[3].ToString(), dr[13].ToString());

    //        //string Lat = "";
    //        //string Long = "";
    //        string hosId = "";
    //        var query1 = from item in db.tbl_hospitalregs
    //                     join item1 in db.tbl_hos_locations on item.h_id equals item1.h_id
    //                     where item.h_regno == dr[13].ToString()
    //                     select new { item1.latitude, item1.longitude, item.h_regno };
    //        if (query1.Count() > 0)
    //        {
    //            foreach (var s in query1)
    //            {
    //                Lat = s.latitude.ToString();
    //                Long = s.longitude.ToString();
    //                hosId = s.h_regno.ToString();
    //            }
    //        }
    //        else
    //        {
    //            hosId = "";
    //        }

    //        string qry = "select * from tbl_hdoctor where hd_name='" + dr[0].ToString() + "' ";

    //        cmd = new SqlCommand(qry, con);
    //        dr2 = cmd.ExecuteReader();
    //        Response.Write("<tr class='box box-primary'>");
    //        //Response.Write("<div>");

    //        while (dr2.Read())
    //        {
    //            Response.Write("<td><div onmouseover='hover(" + hosId + ")' onmouseout='out(" + hosId + ")'><table><tr>");
    //            //Response.Write("onMouseOver='hover("+dr2[0].ToString()+")' onMouseOut='out(" + dr2[0].ToString() + ")' >");
    //            //Response.Write("onmouseover='hover(" + dr2[0].ToString() + ")' onmouseout='out(" + dr2[0].ToString() + ")' >");

    //            string img;

    //            if (dr2[18].ToString() == "")
    //            {
    //                img = "<img class='img img-responsive' src='../doctorimages/doctor.png'>";
    //            }
    //            else
    //            {
    //                img = "<img class='img img-responsive' src='" + dr2[18].ToString() + "'>";

    //                //  img = "<img class='img img-responsive' src='../doctorimages/868268images (2).jpg'>";
    //            }

    //            // string img = "<img class='img img-rounded img-responsive' src='../doctorimages/doctor.png'>";

    //            string about = "/*<Label style='font-size:5px'>*/";
    //            try
    //            {

    //                about = "<label style='font-size:small'>" + dr2[25].ToString().Substring(0, 100) + "</label>";
    //                about = about + "...<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'  href=Viewhospitaldoctorreview.aspx?docid=" + dr2[5].ToString() + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">More </a>";

    //                // about = about + "......<a href=Viewhospitaldoctorreview.aspx?docid=" + dr2[5].ToString() + "> More </a>";
    //            }

    //            catch (Exception ex)
    //            {
    //                about = dr2[25].ToString();
    //            }

    //            about = about + "</Label>";

    //            //  string about = "<Label style='font-size:small'>"+ dr2[25].ToString().Substring(0, 100)+"</Label>";
    //            Response.Write("<td class='col-md-2'>" + img + "</td><td class='col-md-2'>" + "<a style='color:#18bc9c;font-weight:bold;font-size:medium' href=Viewhospitaldoctorreview.aspx?docid=" + dr2[5].ToString() + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">" + dr[0].ToString() + "</a> <br><Label style='color:#18bc9c;font-weight:normal;'>" + dr2[15].ToString() + "</Label><br>" + about.ToString() + "</td>");
    //            //Response.Write("<div style='display:inline-block;' ><div  class='col-md-2'>" + img + "</div><div class='col-md-2'>" + "<a style='color:#18bc9c;font-weight:bold;font-size:medium' href=Viewhospitaldoctorreview.aspx?docid=" + dr2[5].ToString() + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">" + dr[0].ToString() + "</a> <br><Label style='color:#18bc9c;font-weight:normal;'>" + dr2[15].ToString() + "</Label><br>" + about.ToString() + "</div> </div>");

    //        }
    //        Response.Write("<td class='col-md-4'>");
    //        //Response.Write("<div class='col-md-4'>");
    //        dr2.Close();
    //        DateTime dt = new DateTime();
    //        dt = newDate;
    //        int j = 0;

    //        Response.Write("<ul class='bxslider'>");
    //        Response.Write("<li>");
    //        for (int i = 0; i < 30; i++)
    //        {
    //            string cdate = dt.ToString("yyyy-MM-dd");

    //            if (j < 3)
    //            {
    //                if (j == 0)
    //                {

    //                    string cdateo = dt.ToString("yyyy-MM-dd");
    //                    DateTime dt1 = dt.AddDays(1);
    //                    string cdate1 = dt1.ToString("yyyy-MM-dd");

    //                    DateTime dt2 = dt1.AddDays(1);
    //                    string cdate2 = dt2.ToString("yyyy-MM-dd");
    //                    Session["cdate2"] = cdate2.ToString();
    //                    // Session["datedis"] = dt2.ToString("ddd MMM dd");
    //                    qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdateo + "' ";

    //                    SqlDataAdapter adpt111 = new SqlDataAdapter(qry, con);
    //                    DataSet dts111 = new DataSet();
    //                    adpt111.Fill(dts111);
    //                    int count1 = dts111.Tables[0].Rows.Count;
    //                    if (count1 == 0)
    //                    {
    //                        qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate1 + "' ";


    //                        SqlDataAdapter adpt222 = new SqlDataAdapter(qry, con);
    //                        DataSet dts222 = new DataSet();
    //                        adpt222.Fill(dts222);
    //                        int count2 = dts222.Tables[0].Rows.Count;
    //                        if (count2 == 0)
    //                        {
    //                            qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate2 + "' ";


    //                            SqlDataAdapter adpt333 = new SqlDataAdapter(qry, con);
    //                            DataSet dts333 = new DataSet();
    //                            adpt333.Fill(dts333);
    //                            int count3 = dts333.Tables[0].Rows.Count;

    //                            if (count3 == 0)
    //                            {

    //                                Session["count"] = "1";
    //                            }
    //                        }
    //                    }

    //                    if (Session["count"].ToString() == "1")
    //                    {

    //                        Session["count"] = 0;

    //                        Response.Write("<div style='padding-left:42px;'><div style='display:inline-block;padding-left:8px;height:auto'>");
    //                        //   string cdate = dt.ToString("yyyy-MM-dd");
    //                        // DateTime  dte = Convert.ToDateTime(cdate);

    //                        Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

    //                        Response.Write("</div>");
    //                        dt = dt.AddDays(1);
    //                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

    //                        // DateTime  dte = Convert.ToDateTime(cdate);

    //                        Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
    //                        Response.Write("</div>");
    //                        dt = dt.AddDays(1);
    //                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

    //                        // DateTime  dte = Convert.ToDateTime(cdate);

    //                        Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

    //                        Response.Write("</div>");
    //                        //DateTime newdate = Session["cdate2"];
    //                        //     dt.ToString("ddd MMM dd")
    //                        string extqry = "select   top 1 date from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date>'" + Session["cdate2"].ToString() + "'";
    //                        SqlCommand com1 = new SqlCommand(extqry, con);
    //                        string nextdate = "";
    //                        try
    //                        {
    //                            nextdate = com1.ExecuteScalar().ToString();
    //                            Session["nextdate"] = nextdate.ToString();
    //                            DateTime datenew = Convert.ToDateTime(nextdate);

    //                            nextdate = datenew.ToString("ddd MMM dd");

    //                            string docidpass = obj.EnryptString(dr[9].ToString().ToString());
    //                            string docdate = obj.EnryptString(Session["nextdate"].ToString());
    //                            //string doctime = obj.EnryptString(t[0].ToString());
    //                            //string timeperiod = obj.EnryptString(t[1].ToString());
    //                            //string lat = obj.EnryptString(Lat.ToString());
    //                            //string longti = obj.EnryptString(Long.ToString());
    //                            //Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=HospitalHospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
    //                            //Response.Write("<br>");

    //                            Response.Write("<label style='margin-top:50px; margin-left:30px' ><a  class='btn btn-success' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
    //                            //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
    //                            Response.Write("</div>");

    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            nextdate = "";
    //                            Response.Write("<label style='margin-top:50px; margin-left:10px'  class='btn btn-success'> Not Available on remaining Dates" + nextdate.ToString() + "</label>");
    //                            //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
    //                            Response.Write("</div>");
    //                            break;
    //                            // dt.AddDays(2);

    //                        }

    //                        //  Session["datedis"] 


    //                        //   Response.Write("Next availability");
    //                        //  Response.Write("<label style='margin-top:50px; margin-left:30px'  class='btn btn-success'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
    //                        ////  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
    //                        //  Response.Write("</div>");

    //                        // dr1.Close();


    //                        // Response.Write("</li>");


    //                        Response.Write("</li>");
    //                        Response.Write("<li>");

    //                        Session["count"] = "0";

    //                        j = 0;
    //                        dt.AddDays(2);
    //                    }
    //                    else
    //                    {
    //                        Response.Write("<div style='padding-left:42px;'><div style='display:inline-block;padding-left:8px;height:auto'>");
    //                        //       string cdate = dt.ToString("yyyy-MM-dd");
    //                        // DateTime  dte = Convert.ToDateTime(cdate);
    //                        qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate + "'";

    //                        cmd = new SqlCommand(qry, con);
    //                        dr1 = cmd.ExecuteReader();
    //                        SqlDataAdapter adpt1 = new SqlDataAdapter(qry, con);
    //                        DataSet dts1 = new DataSet();
    //                        adpt1.Fill(dts1);
    //                        int count = dts1.Tables[0].Rows.Count;
    //                        int b = 0;
    //                        if (count < 4)
    //                        {
    //                            b = 4 - count;
    //                        }
    //                        try
    //                        {
    //                            int test = 0;

    //                            if (dr1.HasRows)
    //                            {
    //                                Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
    //                                string testname = "";
    //                                while (dr1.Read())
    //                                {
    //                                    testname = dr1[1].ToString();
    //                                    if (test <= 3)
    //                                    {
    //                                        try
    //                                        {
    //                                            string[] t = new string[7];

    //                                            t = dr1[0].ToString().Split(' ');
    //                                            String timet = t[0].ToString() + t[1].ToString();
    //                                            //Session["org_time"] = dr1[0].ToString();
    //                                            string docidpass = obj.EnryptString(dr1[1].ToString());
    //                                            string docdate = obj.EnryptString(cdate.ToString());
    //                                            string doctime = obj.EnryptString(t[0].ToString());
    //                                            string timeperiod = obj.EnryptString(t[1].ToString());
    //                                            //string lat = obj.EnryptString(Lat.ToString());
    //                                            //string longti = obj.EnryptString(Long.ToString());

    //                                            string lat = Lat.ToString();
    //                                            string longti = Long.ToString();


    //                                            SqlCommand com = new SqlCommand("select a_status from tbl_hos_doc_appmnt where d_id='" + dr1[1].ToString() + "' and a_time='" + dr1[0].ToString() + "' and date='" + cdate.ToString() + "'", con);

    //                                            int a_status;
    //                                            try
    //                                            {
    //                                                a_status = Convert.ToInt32(com.ExecuteScalar());
    //                                            }
    //                                            catch (Exception ex)
    //                                            {
    //                                                a_status = 2;
    //                                            }

    //                                            //if (a_status == 0)
    //                                            //{
    //                                            //    Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
    //                                            //}
    //                                            //else 
    //                                            if (a_status == 1)
    //                                            {
    //                                                Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
    //                                            }
    //                                            else
    //                                            {
    //                                                Response.Write("<a class='btn btn-xs btn-success btn-hover'  style='text-align: center; margin-top:5%;width:64px;' href=HospitalHospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");

    //                                            }

    //                                            Response.Write("<br>");


    //                                        }
    //                                        catch (Exception ex)
    //                                        {

    //                                            Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label>");
    //                                            Response.Write("<br>");
    //                                        }
    //                                        test++;
    //                                    }

    //                                }
    //                                if (b == 0)

    //                                {

    //                                }
    //                                else
    //                                {
    //                                    for (int p = 0; p <= b; p++)

    //                                    {
    //                                        Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label>");
    //                                        Response.Write("<br>");
    //                                    }
    //                                }
    //                                if (test > 3)
    //                                {
    //                                    string docidpass = obj.EnryptString(testname.ToString());

    //                                    //string lat = obj.EnryptString(Lat.ToString());
    //                                    //string longti = obj.EnryptString(Long.ToString());

    //                                    string lat = Lat.ToString();
    //                                    string longti = Long.ToString();
    //                                    //  Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=HospitalHospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
    //                                    //   Response.Write("<br>");

    //                                    Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=HospitalHospitaldoctoravailability.aspx?docid=" + docidpass + "&Lat=" + lat.ToString() + "&Long=" + longti + "> More</a>");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
    //                                for (int ii = 0; ii <= 4; ii++)
    //                                { Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label><br>"); }



    //                            }
    //                            Response.Write("</div></div");

    //                            dr1.Close();
    //                        }
    //                        catch (Exception ex)
    //                        {

    //                        }
    //                        dt = dt.AddDays(1);
    //                        // Response.Write("</li>");
    //                        j++;

    //                    }

    //                }

    //                else
    //                {

    //                    Response.Write("<div style='padding-left:42px;'><div style='display:inline-block;padding-left:8px;height:auto'>");
    //                    //       string cdate = dt.ToString("yyyy-MM-dd");
    //                    // DateTime  dte = Convert.ToDateTime(cdate);
    //                    qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate + "'";

    //                    cmd = new SqlCommand(qry, con);
    //                    dr1 = cmd.ExecuteReader();
    //                    SqlDataAdapter adpt1 = new SqlDataAdapter(qry, con);
    //                    DataSet dts1 = new DataSet();
    //                    adpt1.Fill(dts1);
    //                    int count = dts1.Tables[0].Rows.Count;
    //                    int b = 0;
    //                    if (count < 4)
    //                    {
    //                        b = 4 - count;
    //                    }
    //                    try
    //                    {
    //                        int test = 0;

    //                        if (dr1.HasRows)
    //                        {
    //                            Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
    //                            string testname = "";
    //                            while (dr1.Read())
    //                            {
    //                                testname = dr1[1].ToString();
    //                                if (test <= 3)
    //                                {
    //                                    try
    //                                    {
    //                                        string[] t = new string[7];

    //                                        t = dr1[0].ToString().Split(' ');
    //                                        String timet = t[0].ToString() + t[1].ToString();
    //                                        //Session["org_time"] = dr1[0].ToString();
    //                                        string docidpass = obj.EnryptString(dr1[1].ToString());
    //                                        string docdate = obj.EnryptString(cdate.ToString());
    //                                        string doctime = obj.EnryptString(t[0].ToString());
    //                                        string timeperiod = obj.EnryptString(t[1].ToString());
    //                                        //string lat = obj.EnryptString(Lat.ToString());
    //                                        //string longti = obj.EnryptString(Long.ToString());

    //                                        string lat = Lat.ToString();
    //                                        string longti = Long.ToString();


    //                                        SqlCommand com = new SqlCommand("select a_status from tbl_hos_doc_appmnt where d_id='" + dr1[1].ToString() + "' and a_time='" + dr1[0].ToString() + "' and date='" + cdate.ToString() + "'", con);

    //                                        int a_status;
    //                                        try
    //                                        {
    //                                            a_status = Convert.ToInt32(com.ExecuteScalar());
    //                                        }
    //                                        catch (Exception ex)
    //                                        {
    //                                            a_status = 2;
    //                                        }

    //                                        //if (a_status == 0)
    //                                        //{
    //                                        //    Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
    //                                        //}
    //                                        //else 
    //                                        if (a_status == 1)
    //                                        {
    //                                            Response.Write("<a title='Appoinement Taken' class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
    //                                        }
    //                                        else
    //                                        {
    //                                            Response.Write("<a class='btn btn-xs btn-success btn-hover'  style='text-align: center; margin-top:5%;width:64px;' href=HospitalHospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");

    //                                        }

    //                                        Response.Write("<br>");


    //                                    }
    //                                    catch (Exception ex)
    //                                    {

    //                                        Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label>");
    //                                        Response.Write("<br>");
    //                                    }
    //                                    test++;
    //                                }

    //                            }
    //                            if (b == 0)

    //                            {

    //                            }
    //                            else
    //                            {
    //                                for (int p = 0; p <= b; p++)

    //                                {
    //                                    Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label>");
    //                                    Response.Write("<br>");
    //                                }
    //                            }
    //                            if (test > 3)
    //                            {
    //                                string docidpass = obj.EnryptString(testname.ToString());

    //                                //string lat = obj.EnryptString(Lat.ToString());
    //                                //string longti = obj.EnryptString(Long.ToString());

    //                                string lat = Lat.ToString();
    //                                string longti = Long.ToString();
    //                                //  Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=HospitalHospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
    //                                //   Response.Write("<br>");

    //                                Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=HospitalHospitaldoctoravailability.aspx?docid=" + docidpass + "&Lat=" + lat.ToString() + "&Long=" + longti + "> More</a>");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
    //                            for (int ii = 0; ii <= 4; ii++)
    //                            { Response.Write("<Label class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'>----</Label><br>"); }



    //                        }
    //                        Response.Write("</div></div");

    //                        dr1.Close();
    //                    }
    //                    catch (Exception ex)
    //                    {

    //                    }
    //                    dt = dt.AddDays(1);
    //                    // Response.Write("</li>");
    //                    j++;
    //                }
    //            }
    //            if (j == 3)
    //            {

    //                Response.Write("</li>");
    //                Response.Write("<li>");

    //                string cdateo = dt.ToString("yyyy-MM-dd");
    //                DateTime dt1 = dt.AddDays(1);
    //                string cdate1 = dt1.ToString("yyyy-MM-dd");

    //                DateTime dt2 = dt1.AddDays(1);
    //                string cdate2 = dt2.ToString("yyyy-MM-dd");
    //                Session["cdate2"] = cdate2.ToString();
    //                // Session["datedis"] = dt2.ToString("ddd MMM dd");
    //                qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdateo + "' ";


    //                SqlDataAdapter adpt111 = new SqlDataAdapter(qry, con);
    //                DataSet dts111 = new DataSet();
    //                adpt111.Fill(dts111);
    //                int count1 = dts111.Tables[0].Rows.Count;
    //                if (count1 == 0)
    //                {
    //                    qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate1 + "' ";


    //                    SqlDataAdapter adpt222 = new SqlDataAdapter(qry, con);
    //                    DataSet dts222 = new DataSet();
    //                    adpt222.Fill(dts222);
    //                    int count2 = dts222.Tables[0].Rows.Count;
    //                    if (count2 == 0)
    //                    {
    //                        qry = "select time,hd_email,hd_photo from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date='" + cdate2 + "' ";


    //                        SqlDataAdapter adpt333 = new SqlDataAdapter(qry, con);
    //                        DataSet dts333 = new DataSet();
    //                        adpt333.Fill(dts333);
    //                        int count3 = dts333.Tables[0].Rows.Count;

    //                        if (count3 == 0)
    //                        {

    //                            Session["count"] = "1";
    //                        }
    //                    }
    //                }

    //                if (Session["count"].ToString() == "1")
    //                {

    //                    Session["count"] = 0;

    //                    Response.Write("<div style='padding-left:42px;'><div style='display:inline-block;padding-left:8px;height:auto'>");
    //                    //   string cdate = dt.ToString("yyyy-MM-dd");
    //                    // DateTime  dte = Convert.ToDateTime(cdate);

    //                    Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



    //                    Response.Write("</div>");
    //                    dt = dt.AddDays(1);
    //                    Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

    //                    // DateTime  dte = Convert.ToDateTime(cdate);

    //                    Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
    //                    Response.Write("</div>");
    //                    dt = dt.AddDays(1);
    //                    Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

    //                    // DateTime  dte = Convert.ToDateTime(cdate);

    //                    Response.Write("<Label style='margin-bottom:13px'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

    //                    Response.Write("</div>");


    //                    //DateTime newdate = Session["cdate2"];
    //                    //     dt.ToString("ddd MMM dd")
    //                    string extqry = "select   top 1 date from view_hos_doc_available_time where hd_name='" + dr[0].ToString() + "' and date>'" + Session["cdate2"].ToString() + "'";
    //                    SqlCommand com1 = new SqlCommand(extqry, con);
    //                    string nextdate = "";
    //                    try
    //                    {
    //                        nextdate = com1.ExecuteScalar().ToString();
    //                        Session["nextdate"] = nextdate.ToString();
    //                        DateTime datenew = Convert.ToDateTime(nextdate);

    //                        nextdate = datenew.ToString("ddd MMM dd");

    //                        string docidpass = obj.EnryptString(dr[9].ToString().ToString());
    //                        string docdate = obj.EnryptString(Session["nextdate"].ToString());
    //                        //string doctime = obj.EnryptString(t[0].ToString());
    //                        //string timeperiod = obj.EnryptString(t[1].ToString());
    //                        //string lat = obj.EnryptString(Lat.ToString());
    //                        //string longti = obj.EnryptString(Long.ToString());
    //                        //Response.Write("<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=HospitalHospitaldoctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
    //                        //Response.Write("<br>");

    //                        Response.Write("<label style='margin-top:50px; margin-left:30px' ><a  class='btn btn-success' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
    //                        //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
    //                        Response.Write("</div>");

    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        nextdate = "";
    //                        Response.Write("<label style='margin-top:50px; margin-left:10px'  class='btn btn-success'> Not Available on remaining Dates" + nextdate.ToString() + "</label>");
    //                        //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
    //                        Response.Write("</div>");
    //                        break;
    //                        // dt.AddDays(2);

    //                    }

    //                    //  Session["datedis"] 


    //                    //   Response.Write("Next availability");
    //                    //  Response.Write("<label style='margin-top:50px; margin-left:30px'  class='btn btn-success'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
    //                    ////  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
    //                    //  Response.Write("</div>");

    //                    // dr1.Close();


    //                    // Response.Write("</li>");


    //                    Response.Write("</li>");
    //                    Response.Write("<li>");

    //                    Session["count"] = "0";


    //                }

    //                j = 0;
    //                dt.AddDays(2);
    //            }


    //        }


    //        Response.Write("</ul>");
    //        //tr close here
    //        Response.Write("</td>");
    //        //Response.Write("</div></div>");
    //        Response.Write("</div></td></tr></table>");

    //    }
    //    rptMarkers.DataSource = dtMap;
    //    rptMarkers.DataBind();

    //    dr.Close();


    //    Response.Write("</tr>");
    //    Response.Write("</table>");
    //    //Response.Write("</div>");

    //    //span Label1 close here
    //    Response.Write("</span>");
    //    //Response.Write("</span>");

    //}


    protected void Button4_Click(object sender, EventArgs e)
    {
        SearchFilter();
    }

    private void SearchFilter()
    {
        string query = "";
        string docNameSpecl = "";
        string location = "";
        string hospName = "";
        string q = "";

        if (txtContactsSearch.Text != "" && txtZipCodeSearch.Text != "" && txtLangSearch.Text != "")
        {
            query = "(hd_name='" + txtContactsSearch.Text + "' or hd_specialties='" + txtContactsSearch.Text + "') and (hd_location='" + txtZipCodeSearch.Text + "' or hd_city='" + txtZipCodeSearch.Text + "') and h_name='" + txtLangSearch.Text + "'";
            docNameSpecl = txtContactsSearch.Text;
            location = txtZipCodeSearch.Text;
            hospName = txtLangSearch.Text;
            q = " where  " + query;
        }
        else if (txtContactsSearch.Text != "" && txtZipCodeSearch.Text == "" && txtLangSearch.Text == "")
        {
            query = "(hd_name like '%" + txtContactsSearch.Text + "%' or hd_specialties like '%" + txtContactsSearch.Text + "%')";
            docNameSpecl = txtContactsSearch.Text;
            q = " where  " + query;
        }
        else if (txtContactsSearch.Text == "" && txtZipCodeSearch.Text != "" && txtLangSearch.Text == "")
        {
            query = "(hd_location like '%" + txtZipCodeSearch.Text + "%' or hd_city like '%" + txtZipCodeSearch.Text + "%')";
            location = txtZipCodeSearch.Text;
            q = " where  " + query;
        }
        else if (txtContactsSearch.Text == "" && txtZipCodeSearch.Text == "" && txtLangSearch.Text != "")
        {
            query = "h_name like '%" + txtLangSearch.Text + "%'";
            hospName = txtLangSearch.Text;
            q = " where  " + query;
        }
        else if (txtContactsSearch.Text != "" && txtZipCodeSearch.Text != "" && txtLangSearch.Text == "")
        {
            query = "(hd_name='" + txtContactsSearch.Text + "' or hd_specialties='" + txtContactsSearch.Text + "') and (hd_location='" + txtZipCodeSearch.Text + "' or hd_city='" + txtZipCodeSearch.Text + "')";
            docNameSpecl = txtContactsSearch.Text;
            location = txtZipCodeSearch.Text;
            q = " where  " + query;
        }
        else if (txtContactsSearch.Text != "" && txtZipCodeSearch.Text == "" && txtLangSearch.Text != "")
        {
            query = "(hd_name='" + txtContactsSearch.Text + "' or hd_specialties='" + txtContactsSearch.Text + "') and h_name='" + txtLangSearch.Text + "'";
            docNameSpecl = txtContactsSearch.Text;
            hospName = txtLangSearch.Text;
            q = " where  " + query;
        }
        else if (txtContactsSearch.Text == "" && txtZipCodeSearch.Text != "" && txtLangSearch.Text != "")
        {
            query = "(hd_location='" + txtZipCodeSearch.Text + "' or hd_city='" + txtZipCodeSearch.Text + "') and h_name='" + txtLangSearch.Text + "'";
            location = txtZipCodeSearch.Text;
            hospName = txtLangSearch.Text;
            q = " where  " + query;
        }
        else
        {
            q = "";
        }
        //if (query != "")
        //{
        //    query = " where  " + query;
        //}

        list_doctors(q, docNameSpecl, location, hospName);
    }

    protected void txtContactsSearch_TextChanged(object sender, EventArgs e)
    {
        SearchFilter();
    }

    protected void txtZipCodeSearch_TextChanged(object sender, EventArgs e)
    {
        SearchFilter();
    }

    protected void txtLangSearch_TextChanged(object sender, EventArgs e)
    {
        SearchFilter();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["q"] = "";
        Response.Redirect("search.aspx");
    }
}