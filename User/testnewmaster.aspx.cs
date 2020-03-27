using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_testnewmaster : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();
    //SqlConnection con = new SqlConnection("Data Source=TAHCOM-1\\SQLEXPRESS;Initial Catalog=db_BookDoc;User ID=sa;Password=admin123;MultipleActiveResultSets=True");
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

    SqlCommand cmd, com;
    SqlDataReader dr, dr1, dr2;
    int pagestart = 1;
    string filter = "";
    secure obj = new secure();
    string ratefill = "";
    string rateempty = "";
    string ratehalf = "";

    protected override void InitializeCulture()
    {
        string culture = "Auto";
        try
        {
            culture = Request.QueryString["l"].ToString();
        }
        catch (Exception ex)
        { }
        try
        {
            culture = Session["Language"].ToString();
        }
        catch (Exception ex)
        {

        }
        // string culture = Session["Language"].ToString();
        if (string.IsNullOrEmpty(culture))
            culture = "Auto";
        //Use this
        UICulture = culture;
        Culture = culture;
        //OR This
        if (culture != "Auto")
        {
            System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
            System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
        }

        base.InitializeCulture();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();


        if (!IsPostBack)
        {/////service


            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            DateTime time = new DateTime();
            time = DateTime.Now;
            string displayTime = time.ToString("hh:mm tt");
            // MessageBox.Show(displayTime.ToString());
            // MessageBox.Show(dt.ToString("yyyy-MM-dd"));
            string dat = dt.ToString("yyyy-MM-dd");
            com = new SqlCommand("select id from doctor_availability where a_date<'" + dat + "'", con);
            SqlDataReader dtr = com.ExecuteReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    com = new SqlCommand("delete from tbl_doc_time where date_id='" + dtr[0].ToString() + "'", con);
                    com.ExecuteNonQuery();
                }
            }
            dtr.Close();
            com = new SqlCommand("delete from doctor_availability where a_date<'" + dat + "'", con);
            com.ExecuteNonQuery();

            com = new SqlCommand("select id from doctor_availability where a_date='" + dat + "'", con);
            dtr = com.ExecuteReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    // time > LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))

                    //  com = new SqlCommand("delete from tbl_doc_time where date_id='" + dtr[0].ToString() + "' and time > LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))", con);
                    string pmqry = "select distinct(LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))) from tbl_doc_time where date_id='" + dtr[0].ToString() + "'";
                    SqlCommand compm = new SqlCommand(pmqry, con);
                    string timepm = compm.ExecuteScalar().ToString();
                    int l = timepm.ToString().Count();

                    string ab2 = timepm.ToString().Substring(l - 2, 2);
                    if (ab2 == "PM")
                    {
                        com = new SqlCommand("delete from tbl_doc_time where date_id='" + dtr[0].ToString() + "' and time like '%AM'", con);
                        com.ExecuteNonQuery();
                    }

                    if (timepm.ToString().Contains("12"))
                    {
                        com = new SqlCommand("delete from tbl_doc_time where date_id='" + dtr[0].ToString() + "' and time < LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7)) and time not like '1%'", con);
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        com = new SqlCommand("delete from tbl_doc_time where date_id='" + dtr[0].ToString() + "' and time < LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7))", con);
                        com.ExecuteNonQuery();
                    }
                }
            }
            com = new SqlCommand("delete from doctor_availability where id not in (select date_id from tbl_doc_time)", con);
            com.ExecuteNonQuery();
            //service



            SqlCommand comdrop = new SqlCommand("select distinct(d_language)as lang from tbl_doc_language", con);
            SqlDataReader dtrdrop = comdrop.ExecuteReader();
            if (dtrdrop.HasRows)
            {
                while (dtrdrop.Read())
                {
                    if (dtrdrop[0].ToString() == "")
                    {

                    }
                    else
                    {

                        DropDownList1.Items.Add(dtrdrop[0].ToString());
                    }
                }
            }
            DropDownList1.Items.Insert(0, "--Language--");


            SqlCommand comdropcity = new SqlCommand("select distinct(d_location)as lang from tbl_doctor", con);
            SqlDataReader dtrdropcity = comdropcity.ExecuteReader();
            if (dtrdropcity.HasRows)
            {
                while (dtrdropcity.Read())
                {
                    if (dtrdropcity[0].ToString() == "")
                    {

                    }
                    else
                    {
                        DropDownList2.Items.Add(dtrdropcity[0].ToString());
                    }
                }
            }
            DropDownList2.Items.Insert(0, "--Location--");

            Session["count"] = 0;


            try
            {
                //txtContactsSearch.Text = Request.QueryString["did"].ToString();
                string ids = Request.QueryString["did"].ToString();
                if (ids != "")
                {
                    List<string> id = new List<string>(Request.QueryString["did"].ToString().Split(','));

                    //CheckBox1.Checked = true;
                    string q = "where d_id in(";
                    string qq = "";
                    for (int i = 1; i < id.Count; i++)
                    {
                        qq = qq + id[i].ToString() + ",";
                    }
                    qq = qq.Substring(0, qq.LastIndexOf(','));
                    q = q + qq + ")";
                    list_doctors(q, "", "", "", "", "");
                    id.Clear();


                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    //upModal.Update();
                }
            }
            catch (Exception ex)
            {


                list_doctors("", "", "", "", "", "");
            }


            LoadSpecialities();
            try
            {
                //cmd = new SqlCommand("select d_language from tbl_doctor where d_id='" + DropDownList1.SelectedItem.Text + "'", con);
                //DropDownList1.SelectedItem.Text = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            { }

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
                Illness.DataSource = query;
                Illness.DataTextField = "Specialities";
                Illness.DataValueField = "id";
                Illness.DataBind();

            }
            Illness.Items.Insert(0, "--Speciality--");

        }
        catch (Exception ex)
        {
        }
    }
    public void list_doctors(string q, string speciality, string gender, string docName, string location, string language)
    {

        DataSet dts = new DataSet();

        SqlDataAdapter adpt;
        if (q != "")
        {

            Session["q"] = q.ToString();
            // string qqqq = q.Replace("where", " and ");
            adpt = new SqlDataAdapter("select  distinct(d_name) as d_name from   view_doc_available_time v  " + q, con);
            dts.Clear();
            adpt.Fill(dts);
        }
        else
        {
            adpt = new SqlDataAdapter("select  distinct(d_name) as d_name from   view_doc_available_time v where d_status='1' ", con);
            dts.Clear();
            adpt.Fill(dts);
        }

        try
        {
            if (Session["q"] != "")
            {
                adpt = new SqlDataAdapter("select  distinct(d_name) as d_name from   view_doc_available_time v  " + Session["q"].ToString(), con);
                dts.Clear();
                adpt.Fill(dts);
            }
        }
        catch (Exception ex)
        {

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
        int chkno = 1, chknoj = 1;
        if (pagestart == 1)
        {
            //   cmd = new SqlCommand("select   distinct top 5(hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hospital"].ToString() + "' and row_number>=1 ORDER BY hd_name, row_number", con);
            Session["q"] = "";

            try
            {
                q = Session["pq"].ToString();

            }
            catch (Exception ex)
            {

            }
            Session["pq"] = "";
            if (q != "")
            {


                string qqqq = q.Replace("where", " and ");
                Session["q"] = q.ToString();
                if (Session["filter"] == "male")
                {
                    cmd = new SqlCommand("select top 5 * from view_gg_male where  d_status='1' and t>='" + pagestart + "' " + qqqq + "order by rate desc", con);

                }
                else if (Session["filter"] == "female")
                {
                    cmd = new SqlCommand("select top 5 * from view_gg_female where  d_status='1' and t>='" + pagestart + "' " + qqqq + "order by rate desc", con);

                }
                else
                {
                    cmd = new SqlCommand("select top 5 * from view_gg where  d_status='1' and t>='" + pagestart + "' " + qqqq + "order by rate desc", con);
                }
                //  cmd = new SqlCommand("select top 5   * from view_gg where d_status='1' and  t>='" + pagestart + "' " + qqqq + "order by t", con);
                //   cmd = new SqlCommand("select distinct(d_name) as d_name from view_doc_available_time" + q, con);
            }
            else
            {
                cmd = new SqlCommand("select top 5   * from view_gg where  d_status='1' and t>='" + pagestart + "' order by rate desc", con);
                // cmd = new SqlCommand("select distinct(d_name) as d_name from view_doc_available_time", con);
            }


        }
        else
        {
            //  pagestart = (pagestart + pagestart) - 2;
            pagestart = 1 + ((pagestart - 1) * 5);
            chkno = 0;
            chknoj = 0;
            //  cmd = new SqlCommand("select   * from view_test where t>='" + pagestart + "'", con);
            //  cmd = new SqlCommand("select   * from view_gg where t>='" + pagestart + "'", con);
            try
            {
                if (Session["q"] != "")
                {


                    string qqqq = Session["q"].ToString().Replace("where", " and ");
                    if (Session["filter"] == "male")
                    {
                        cmd = new SqlCommand("select top 5* from view_gg_male where  d_status='1' and t>='" + pagestart + "' " + qqqq + "order by rate desc ", con);

                    }
                    else if (Session["filter"] == "female")
                    {
                        cmd = new SqlCommand("select top 5 * from view_gg_female where  d_status='1' and t>='" + pagestart + "' " + qqqq + "order by rate desc", con);

                    }
                    else
                    {
                        cmd = new SqlCommand("select top 5 * from view_gg where  d_status='1' and t>='" + pagestart + "' " + qqqq + "order by rate desc", con);
                    }

                    //   cmd = new SqlCommand("select distinct(d_name) as d_name from view_doc_available_time" + q, con);
                }
                else
                {
                    cmd = new SqlCommand("select top 5   * from view_gg where  d_status='1' and t>='" + pagestart + "'  order by rate desc", con);
                    // cmd = new SqlCommand("select distinct(d_name) as d_name from view_doc_available_time", con);
                }
            }
            catch (Exception ex)
            {
                cmd = new SqlCommand("select top 5   * from view_gg where  d_status='1' and t>='" + pagestart + "'  order by rate desc ", con);
            }


        }
        SqlDataAdapter mapAdapter = new SqlDataAdapter(cmd);
        DataTable dtMap = new DataTable();
        dtMap.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("Latitude"), new DataColumn("Longitude"), new DataColumn("d_specialties"), new DataColumn("d_address"), new DataColumn("d_photo"), new DataColumn("d_id"), new DataColumn("id"), new DataColumn("d_hakkimid") });


        //DataTable dtMap = new DataTable();
        //mapAdapter.Fill(dtMap);
        //rptMarkers.DataSource = dtMap;
        //rptMarkers.DataBind();


        dr = cmd.ExecuteReader();






        //Response.Write("<span id='Label2' style='margin-bottom:1000px;'>");
        //  Response.Write("<span id='Label1' class='col-md-7' style='LEFT: 10px; POSITION: absolute; TOP: 180px;/*overflow-y:scroll*/'>");
        Response.Write("<span id='Label1' class='col-md-8'>");
        //  Response.Write("<span id='Label1' class='col-md-8' style='height:100px;width:800px;Z-INDEX: 302; LEFT: 20px; POSITION: absolute; TOP: 200px;'>");

        //  Response.Write("<div id='doctors' style='overflow-y:scroll;'>");
        Response.Write("<div>");
        Response.Write("<table id='table1' class='table table-responsive'>");



        string Lat = "";
        string Long = "";
        string docid = "";

        while (dr.Read())
        {
            dtMap.Rows.Add(dr[0].ToString(), dr[7].ToString(), dr[8].ToString(), dr[3].ToString(), dr[11].ToString(), dr[10].ToString(), dr[2].ToString(), dr[9].ToString(), dr[13].ToString());
            try
            {
                var quer = from item in db.tbl_doctors
                           join item1 in db.tbl_doctor_locations on item.d_id equals item1.d_id
                           where item.d_name == dr[0].ToString()
                           select new { item1.latitude, item1.longitude, item.d_id };
                if (quer.Count() > 0)
                {
                    foreach (var ss in quer)
                    {
                        Lat = ss.latitude.ToString();
                        Long = ss.longitude.ToString();
                        docid = ss.d_id.ToString();
                    }
                }
                else
                {
                    docid = "";
                }

            }
            catch (Exception ex)
            {
            }

            string qry = "select * from tbl_doctor where d_id='" + dr[9].ToString() + "' ";

            cmd = new SqlCommand(qry, con);
            dr2 = cmd.ExecuteReader();
            Response.Write("<tr class='box box-primary'>");
            //Response.Write("<div>");

            while (dr2.Read())
            {
                Response.Write("<td><div id='btnover' onmouseover='hover(" + docid + ")' onmouseout='out(" + docid + ")'>");

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

                    about = "<label style='font-size:small'>" + dr2[7].ToString().Substring(0, 100) + "</label>";
                    //    about = about + "...<a class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;'  href=Viewdoctorsreview.aspx?docid=" + obj.EnryptString(dr2[32].ToString())+ "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">More </a>";

                    // about = about + "......<a href=Viewdoctorsreview.aspx?docid=" + dr2[32].ToString() + "> More </a>";
                }

                catch (Exception ex)
                {
                    about = dr2[7].ToString();
                }




                about = about + "</Label>";




                //  string about = "<Label style='font-size:small'>"+ dr2[25].ToString().Substring(0, 100)+"</Label>";



                double Total = 0;
                SqlCommand cmddd = new SqlCommand("SELECT rate_wt,rate_bm,rate_service FROM tbl_ratingview where d_id='" + dr2[32].ToString() + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmddd);
                DataTable dtt = new DataTable();
                da.Fill(dtt);
                double Average = 0;
                string avgg = "";
                int wt = 0, bm = 0, ser = 0;
                string ratefill, rateempty, ratehalf;
                if (dtt.Rows.Count > 0)
                {
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        wt += Convert.ToInt32(dtt.Rows[i][0].ToString());
                        bm += Convert.ToInt32(dtt.Rows[i][1].ToString());
                        ser += Convert.ToInt32(dtt.Rows[i][2].ToString());
                        Total += Convert.ToInt32(dtt.Rows[i][0].ToString()) + Convert.ToInt32(dtt.Rows[i][1].ToString()) + Convert.ToInt32(dtt.Rows[i][2].ToString());

                        Average = Total / 3;
                    }
                    wt = wt / (dtt.Rows.Count);
                    bm = bm / (dtt.Rows.Count);
                    ser = ser / (dtt.Rows.Count);
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
                SqlCommand comrate = new SqlCommand("select top 1 u_review from tbl_user_feed where d_email='" + dr2[32].ToString() + "'  order by id desc", con);
                string comm = "";
                try
                {
                    comm = comrate.ExecuteScalar().ToString();

                    comm = "\"" + comm.ToString() + "\"";


                }
                catch (Exception ex)
                {
                    comm = "";
                }

                Response.Write("<div class='row'><div class='col-md-2'><div class='user-panel'><div class='image'>" + img + "</div></div></div>");

                Response.Write("<div class='col-md-4'>");
                Response.Write("<p id='doctornameo'><a id='doctorname' href=Viewdoctorsreview.aspx?docid=" + obj.EnryptString(dr2[32].ToString()) + "&Lat=" + Lat.ToString() + "&Long=" + Long.ToString() + ">" + dr[0].ToString() + "</a><br>");
                Response.Write("<Label id='speciality1'>" + dr2[15].ToString() + "</Label></br>");
                //personalProgress.Style["width"] = pervale.ToString() + "%";
                //personalProgress.Attributes.Add("aria-valuenow", pervale.ToString());
                //  string testt = "<div class='progress'><div id ='personalProgress' width='100%'  class='progress-bar progress-bar-primary progress-bar-striped' role='progressbar' aria-valuemin='0' aria-valuemax='100' aria-valuenow='75'><span class='sr-only'>40% Complete(success)</span></div></div>";
                // string testt = "<div class='progress pink'><div class='progress-bar' style='width:"+wt.ToString()+"%; background:#ff4b7d;'><div class='progress-value'>90%</div></div></div>";
                // string testt = "<div class='progress pink'><div class='progress-bar' style='width:" + wt.ToString() + "%; background:#ff4b7d;'></div></div>";
                wt = wt * 20;
                // string colo = "#ff4b7d";
                string testt = "<div class=progress pink style=width:180px;height:8px;><div class=progress-bar style=background:#4aa9af;margin-bottom:10%;width:" + wt.ToString() + "%; ></div></div>";
                bm = bm * 20;
                string testtt = "<div class=progress pink style=width:180px;height:8px;><div class=progress-bar style=background:#4aa9af;margin-bottom:10%;width:" + bm.ToString() + "%; background:#ff4b7d;></div></div>";
                ser = ser * 20;
                string testttt = "<div class=progress pink style=width:180px;height:8px;><div class=progress-bar style=background:#4aa9af;margin-bottom:10%;width:" + ser.ToString() + "%; background:#ff4b7d;></div></div>";
                //  Response.Write(testt);

                Response.Write("<a class='function' id='star' data-content='Waiting Time " + testt.ToString() + "Beside Manner " + testtt.ToString() + "Service " + testttt.ToString() + "' data-title=''>" + ratefill + ratehalf + rateempty + "</a><br>");
                Response.Write("<a><Label id='latestcomment' > " + comm.ToString() + " </Label ></a></p><br>");

                DateTime dateForButton = DateTime.Now;
                dateForButton = dateForButton.AddDays(-1);
                DateTime dateForButton48 = DateTime.Now;
                dateForButton48 = dateForButton48.AddDays(-2);
                string dateTime = DateTime.Now.ToString();
                string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
                string createddatediff = Convert.ToDateTime(dateForButton).ToString("yyyy-MM-dd");
                string createddatediff48 = Convert.ToDateTime(dateForButton48).ToString("yyyy-MM-dd");



                SqlCommand comho = new SqlCommand("select count(*) as count from tbl_doctor_appointment where d_id='" + dr2[32].ToString() + "' and app_date in ('" + createddate + "' , '" + createddatediff + "')", con);
                int countb = 0, count48 = 0;
                int hours = 0;
                try
                {
                    countb = Convert.ToInt32(comho.ExecuteScalar());

                    SqlCommand comhour = new SqlCommand("select max(id) as id from tbl_doctor_appointment where d_id='" + dr2[32].ToString() + "' and app_date in ('" + createddate + "' , '" + createddatediff + "')", con);
                    int maxid = 0;
                    maxid = Convert.ToInt32(comhour.ExecuteScalar());

                    SqlCommand comhourtime = new SqlCommand("select app_date,app_time from tbl_doctor_appointment where id='" + maxid.ToString() + "' ", con);
                    SqlDataReader dtr = comhourtime.ExecuteReader();
                    string dttimee = "";
                    while (dtr.Read())
                    {
                        dttimee = dtr[0].ToString() + " " + dtr[1].ToString();

                    }

                    DateTime date1 = DateTime.Now;
                    DateTime date2 = Convert.ToDateTime(dttimee);

                    hours = Convert.ToInt32(date1.Subtract(date2).TotalHours);


                    //TimeSpan diff = secondDate - firstDate;
                    //double hours = diff.TotalHours;



                }
                catch (Exception ex)
                {
                    countb = 0;

                    SqlCommand comho48 = new SqlCommand("select count(*) as count from tbl_doctor_appointment where d_id='" + dr2[32].ToString() + "' and app_date in ('" + createddate + "' , '" + createddatediff48 + "')", con);


                    try
                    {
                        count48 = Convert.ToInt32(comho48.ExecuteScalar());
                    }
                    catch (Exception ex1)
                    {
                        count48 = 0;
                    }


                }
                if (countb != 0)
                {
                    string msg = "";
                    if (countb > 2)
                    {
                        msg = "InHighDemand";
                    }
                    if (hours == 0)
                    {
                        string chk = "check0" + chknoj.ToString();
                        //string output = msg.ToString() + " Booked " + countb.ToString() + " times in last 24 hours";
                        //Response.Write("<a  id='check12' data-replace='Just Booked' title='"+output+"'>"+output+"</a><br> ");
                        Response.Write("<a style='font-size:12px'  id='" + chk + "' data-replace='Just Booked' title='" + msg + " Booked " + countb.ToString() + " times in last 24 hours'><span>" + msg + " Booked " + countb.ToString() + " times in last 24 hours<span></a><br> ");
                        chknoj = chknoj + 1;
                    }
                    else
                    {
                        string chk = "check" + chkno.ToString();

                        Response.Write("<a style='font-size:12px;' id='" + chk + "' data-replace='Latest Booking: " + hours + " hours ago' title='" + msg + " Booked " + countb.ToString() + " times in last 24 hours'><span>" + msg + "Booked " + countb.ToString() + " times in last 24 hours<span></a><br> ");
                        chkno = chkno + 1;
                    }
                }
                else
                {
                    if (count48 != 0)
                    {
                        Response.Write("<a id='comment' style='color:#6BD904;fontsize:12px;'>Booked " + count48 + " times in the last 48 hours<br> </a>");
                    }

                }

                Response.Write("<div id='marker'><i id='marker1' class='fa fa-map-marker'></i><Label id='adress'> " + about.ToString() + " </Label></div>");
                Response.Write("</div>");


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
                        qry = "select time,d_hakkimid,d_photo from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date='" + cdateo + "' and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time) ";


                        SqlDataAdapter adpt111 = new SqlDataAdapter(qry, con);
                        DataSet dts111 = new DataSet();
                        adpt111.Fill(dts111);
                        int count1 = dts111.Tables[0].Rows.Count;
                        if (count1 == 0)
                        {

                            qry = "select time,d_hakkimid,d_photo from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date='" + cdate1 + "' and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time) ";


                            SqlDataAdapter adpt222 = new SqlDataAdapter(qry, con);
                            DataSet dts222 = new DataSet();
                            adpt222.Fill(dts222);
                            int count2 = dts222.Tables[0].Rows.Count;
                            if (count2 == 0)
                            {
                                qry = "select time,d_hakkimid,d_photo from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date='" + cdate2 + "' and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time) ";


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

                            Response.Write("<Label  id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



                            Response.Write("</div>");
                            dt = dt.AddDays(1);
                            Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                            Response.Write("</div>");
                            dt = dt.AddDays(1);
                            Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label  id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

                            Response.Write("</div>");


                            //DateTime newdate = Session["cdate2"];
                            //     dt.ToString("ddd MMM dd")
                            string extqry = "select   top 1 a_date from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date>'" + Session["cdate2"].ToString() + "' and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time)";

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
                                Response.Write("<label id='notavailable'><a href=?next=2  class='btn btn-success'><span id='available1'> Not Available on remaining Dates</span></a></label>");
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
                            Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
                            //       string cdate = dt.ToString("yyyy-MM-dd");
                            // DateTime  dte = Convert.ToDateTime(cdate);
                            qry = "select time,d_hakkimid,d_photo from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date='" + cdate + "'and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time)";

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
                                                String timet = "";
                                                string[] t = new string[7];
                                                string ab1 = "", ab2 = "";
                                                try
                                                {
                                                    t = dr1[0].ToString().Split(' ');


                                                    ab1 = t[0].ToString();
                                                    ab2 = t[1].ToString();
                                                    timet = ab1.ToString() + " " + ab2.ToString();
                                                }
                                                catch (Exception ex)
                                                {
                                                    int l = dr1[0].ToString().Count();
                                                    ab1 = dr1[0].ToString().Substring(0, l - 2);
                                                    ab2 = dr1[0].ToString().Substring(l - 2, 2);
                                                    timet = ab1.ToString() + " " + ab2.ToString();
                                                }
                                                //Session["org_time"] = dr1[0].ToString();
                                                string docidpass = obj.EnryptString(dr1[1].ToString());
                                                string docdate = obj.EnryptString(cdate.ToString());
                                                string doctime = obj.EnryptString(ab1.ToString());
                                                string timeperiod = obj.EnryptString(ab2.ToString());
                                                //string hospitalId = obj.EnryptString(dr[13].ToString());
                                                //string lat = obj.EnryptString(Lat.ToString());
                                                //string longti = obj.EnryptString(Long.ToString());

                                                string lat = Lat.ToString();
                                                string longti = Long.ToString();


                                                SqlCommand com = new SqlCommand("select a_status from tbl_doctor_appointment where d_id='" + dr1[1].ToString() + "' and a_time='" + timet.ToString() + "' and a_date='" + cdate.ToString() + "'", con);

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
                                                if (a_status == 1 || a_status == 3)
                                                {
                                                    //Response.Write("<a title='Appointment Taken' class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
                                                    Response.Write("<Label class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()' > ----</Label>");
                                                }
                                                else
                                                {
                                                    Response.Write("<a class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()' href=doctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + timet.ToString() + "</a>");

                                                }

                                                Response.Write("<br>");


                                            }
                                            catch (Exception ex)
                                            {

                                                Response.Write("<Label class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()'>----</Label>");
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
                                            Response.Write("<Label class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()'>----</Label>");
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

                                        Response.Write("<a class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()' href=doctoravailability.aspx?docid=" + docidpass + "&Lat=" + lat.ToString() + "&Long=" + longti + "> More</a>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<Label  id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                    for (int ii = 0; ii <= 4; ii++)
                                    { Response.Write("<Label class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()'>----</Label><br>"); }



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
                        qry = "select time,d_hakkimid,d_photo from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date='" + cdate + "' and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time)";

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

                                            String timet = "";

                                            string ab1 = "", ab2 = "";
                                            try
                                            {
                                                t = dr1[0].ToString().Split(' ');

                                                ab1 = t[0].ToString();
                                                ab2 = t[1].ToString();
                                                timet = ab1.ToString() + " " + ab2.ToString();
                                            }
                                            catch (Exception ex)
                                            {
                                                int l = dr1[0].ToString().Count();
                                                ab1 = dr1[0].ToString().Substring(0, l - 2);
                                                ab2 = dr1[0].ToString().Substring(l - 2, 2);
                                                timet = ab1.ToString() + " " + ab2.ToString();



                                            }
                                            //Session["org_time"] = dr1[0].ToString();
                                            string docidpass = obj.EnryptString(dr1[1].ToString());
                                            string docdate = obj.EnryptString(cdate.ToString());
                                            string doctime = obj.EnryptString(ab1.ToString());
                                            string timeperiod = obj.EnryptString(ab2.ToString());

                                            //string lat = obj.EnryptString(Lat.ToString());
                                            //string longti = obj.EnryptString(Long.ToString());

                                            string lat = Lat.ToString();
                                            string longti = Long.ToString();


                                            SqlCommand com = new SqlCommand("select a_status from tbl_doctor_appointment where d_id='" + dr1[1].ToString() + "' and a_time='" + timet.ToString() + "' and a_date='" + cdate.ToString() + "'", con);

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
                                            if (a_status == 1 || a_status == 3)
                                            {
                                                // Response.Write("<a title='Appointment Taken' class='btn btn-xs btn-success btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
                                                Response.Write("<Label class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()'>----</Label>");
                                            }
                                            else
                                            {

                                                Response.Write("<a class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()' href=doctoravailability.aspx?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + timet.ToString() + "</a>");

                                            }
                                            Response.Write("<br>");


                                        }
                                        catch (Exception ex)
                                        {

                                            Response.Write("<Label class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()'>----</Label>");
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
                                        Response.Write("<Label class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()'>----</Label>");
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

                                    Response.Write("<a class='btn btn-xs btn-success btn-hover btn' id='buttontime'  onmouseover='mouseOver()' onmouseout='mouseOut()'href=doctoravailability.aspx?docid=" + docidpass + "&Lat=" + lat.ToString() + "&Long=" + longti + "> More</a>");
                                }
                            }
                            else
                            {
                                Response.Write("<Label id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                for (int ii = 0; ii <= 4; ii++)
                                { Response.Write("<Label class='btn btn-xs btn-success btn-hover btn' id='buttontime' onmouseover='mouseOver()' onmouseout='mouseOut()'>----</Label><br>"); }



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
                    qry = "select time,d_hakkimid,d_photo from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date='" + cdateo + "' and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time)";



                    SqlDataAdapter adpt111 = new SqlDataAdapter(qry, con);
                    DataSet dts111 = new DataSet();
                    adpt111.Fill(dts111);
                    int count1 = dts111.Tables[0].Rows.Count;
                    if (count1 == 0)
                    {
                        qry = "select time,d_hakkimid,d_photo from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date='" + cdate1 + "'and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time) ";


                        SqlDataAdapter adpt222 = new SqlDataAdapter(qry, con);
                        DataSet dts222 = new DataSet();
                        adpt222.Fill(dts222);
                        int count2 = dts222.Tables[0].Rows.Count;
                        if (count2 == 0)
                        {
                            qry = "select time,d_hakkimid,d_photo from  view_doc_available_time v where d_name='" + dr[0].ToString() + "' and a_date='" + cdate2 + "' and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time)";


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

                        Response.Write("<Label  id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                        Response.Write("</div>");
                        dt = dt.AddDays(1);
                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                        // DateTime  dte = Convert.ToDateTime(cdate);

                        Response.Write("<Label  id='Datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

                        Response.Write("</div>");


                        //DateTime newdate = Session["cdate2"];
                        //     dt.ToString("ddd MMM dd")
                        string extqry = "select   top 1 a_date from  view_doc_available_time v where d_hakkimid='" + dr[13].ToString() + "' and a_date>'" + Session["cdate2"].ToString() + "' and d_hakkimid not in (select d_id from tbl_doctor_appointment where a_date = v.a_date and a_time = v.time)";
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
                            Response.Write("<label id='notavailable'><a href=?next=2  class='btn btn-success' ><span id='available1'> Not Available on remaining Dates</span></a></label>");
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
                Response.Write("<li><a href=search.aspx'?page=" + p + " ><font color='red'><u><b> " + p + "</b></u></font> </a></li> ");
            }
            else
            {
                Response.Write("<li><a href=search.aspx'?page=" + p + " > " + p + "</a></li> ");
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

    public DataTable GetDocLocations(string speciality, string gender, string docName, string location, string language)
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("Latitude"), new DataColumn("Longitude"), new DataColumn("d_specialties"), new DataColumn("d_address"), new DataColumn("d_photo"), new DataColumn("d_id"), new DataColumn("id") });
        try
        {
            var query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            // if speciality selected
            if (speciality != "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && item1.d_specialties == speciality
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }
            // if male selected
            else if (gender != "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && item1.d_sex == gender
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }
            else if (docName != "" && location != "" && language != "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && item1.d_name == docName && (item1.d_location == location || item1.d_city == location) && item1.d_language == language
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }
            else if (docName != "" && location == "" && language == "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && item1.d_name == docName
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }
            else if (docName != "" && location != "" && language == "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && item1.d_name == docName && (item1.d_location == location || item1.d_city == location)
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }
            else if (docName != "" && location == "" && language != "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && item1.d_name == docName && item1.d_language == language
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }
            else if (docName == "" && location != "" && language != "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && item1.d_language == language && (item1.d_location == location || item1.d_city == location)
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }
            else if (docName == "" && location != "" && language == "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && (item1.d_location == location || item1.d_city == location)
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }
            else if (docName == "" && location == "" && language != "")
            {
                query = from item in db.tbl_doctor_locations
                        join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                        where item1.d_status == 1 && item1.d_language == language
                        select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_hakkimid, item1.d_id };
            }

            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    var query1 = from item in db.doctor_availabilities
                                 where item.d_id == ss.d_hakkimid
                                 select item;
                    if (query1.Count() > 0)
                    {
                        dt.Rows.Add(ss.d_name, ss.latitude, ss.longitude, ss.d_specialties, ss.d_address, ss.d_photo, ss.d_hakkimid, ss.d_id);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }

        return dt;

    }
}