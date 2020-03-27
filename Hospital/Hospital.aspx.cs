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
using System.Net.Mail;

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
    MailMessage Email = new MailMessage();
    SMS ob = new SMS();
    protected override void InitializeCulture()
    {
        //Session["Language"] = "Auto";
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
        //    this.MasterPageFile = "~/hospital/ArabichospitalMaster.master";
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
        if (!IsPostBack)
        {
           
            LoadSpecialities();
            ApointmentsCount();
            doctor_count();
            try
            {
                CheckLocation();
            }
            catch (Exception ex)
            {
                Response.Redirect("../index/hospita login.aspx");
            }
            if (Request.QueryString["docid"] != null)
            {
                Session["did"] = obj.DecryptString(Request.QueryString["docid"].ToString());

                TxtApntmtDate.Text = obj.DecryptString(Request.QueryString["docdate"].ToString());
                try
                {
                    TxtApointmentTime.Text = obj.DecryptString(Request.QueryString["doctime"].ToString()) + " " + obj.DecryptString(Request.QueryString["timeperiod"].ToString());
                    //this.ModalPopupExtender1.Show();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                catch (Exception ex)
                { }
            }
            DataSet dts = new DataSet();


            Session["count"] = 0;
           // list_doctors("", "", "");
            SelectDoctors();
        }
       
    }
    public void doctor_count()
    {
        try
        {
            var Query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_status == 1
                        select item;
            Label11.Text = Query.Count().ToString();
        }catch(Exception ex) { }

        con.Open();
        SqlDataAdapter Sda = new SqlDataAdapter("Select count(id) as tid from tbl_temp_hdoctor where h_id='"+ Session["hakkeemid_h"].ToString()+"'", con);
        DataTable dt = new DataTable();
        Sda.Fill(dt);

        lblrdoc.Text = dt.Rows[0]["tid"].ToString();
        con.Close();

    }

    private void ApointmentsCount()
    {
        try
        {
            var query = from item in db.tbl_hos_doc_appmnts
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.a_status != 2
                        select item;
            if (query.Count() > 0)
            {
                Label10.Text = query.Count().ToString();
            }
            else
            {
                Label10.Text = "0";
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void SelectDoctors()
    {
        var Query = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() select item;
        DdlDoctors.DataSource = Query;
        DdlDoctors.DataValueField = "hd_email";
        DdlDoctors.DataTextField = "hd_name";
        DdlDoctors.DataBind();
        DdlDoctors.Items.Insert(0, "---Select doctor---");
        //if (Session["Language"].ToString() == "Auto")
        //{

        //}
        //else
        //{
        //    DdlDoctors.Items.Insert(0, "---حدد الطبيب---");
        //}
        //if (Session["selecteddoctor"] != null)
        //{
        //    var Query1 = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
        //    foreach(var name in Query1)
        //    DdlDoctors.Items.Insert(0, name);
        //}
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
                TextBox2.Items.Insert(0, "--Select Specialty--");
            }
            //if (Session["Language"].ToString() == "Auto")
            //{
               
            //}
            //else
            //{
            //    TextBox2.Items.Insert(0, "-- اختيار التخصص--");
            //}
        }
        catch (Exception ex)
        {
        }
    }
    public void searchfiltr()
    {
        string query = "";

        string q = "";

        if (DdlDoctors.SelectedItem.Text != "---Select doctor---" && TextBox2.SelectedItem.Text != "--Select Specialty--")
        {
            query = "hd_name='" + DdlDoctors.SelectedItem.Text + "' and hd_specialties='" + TextBox2.SelectedItem.Text + "'";

            q = " where  " + query;
        }
        else if (DdlDoctors.SelectedItem.Text != "---Select doctor---" && TextBox2.SelectedItem.Text == "--Select Specialty--")
        {
            query = "hd_name='" + DdlDoctors.SelectedItem.Text + "' ";


            q = " where  " + query;
        }
        else if (DdlDoctors.SelectedItem.Text == "---Select doctor---" && TextBox2.SelectedItem.Text != "--Select Specialty--")
        {
            query = "hd_specialties='" + TextBox2.SelectedItem.Text + "'";

            q = " where  " + query;
        }
    

        //else    if (DdlDoctors.SelectedItem.Text != "" && TextBox2.SelectedItem.Text != "--تحديد--")
        //{
        //    query = "hd_name='" + DdlDoctors.SelectedItem.Text + "' and hd_specialties='" + TextBox2.SelectedItem.Text + "'";

        //    q = " where  " + query;
        //}
        //else if (DdlDoctors.SelectedItem.Text != "" && TextBox2.SelectedItem.Text == "--تحديد--")
        //{
        //    query = "hd_name='" + DdlDoctors.SelectedItem.Text + "' ";


        //    q = " where  " + query;
        //}
        //else if (DdlDoctors.SelectedItem.Text == "" && TextBox2.SelectedItem.Text != "--تحديد--")
        //{
        //    query = "hd_specialties='" + TextBox2.SelectedItem.Text + "'";

        //    q = " where  " + query;
        //}


        else
        {
            q = "";
        }
        list_doctors(q, "", "");


        //if (q == "")
        //{
        //    if (Session["Language"].ToString() == "Auto")
        //    {
        //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctors or Availability not updated.')</Script>");
        //        // Response.Redirect("sethospitalLocation.aspx");
        //    }
        //    else
        //    {
        //        RegisterStartupScript("", "<Script Language=JavaScript>swal('لا الأطباء أفايلابيل.')</Script>");
        //        // Response.Redirect("sethospitalLocation.aspx?l=ar-EG");
        //    }
        //}
    }
    public void list_doctors(string docName, string location, string hosName)
    {
        if(con.State.ToString()=="Closed")
        {
           con.Open();
        }
        
        //if (Session["Language"].ToString() == "Auto")
        //{


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
                    SqlDataAdapter adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'" + Session["query"], con);


                    adpt.Fill(dts);
                    decimal c = Convert.ToDecimal(dts.Tables[0].Rows.Count);
                    c = c / 3;


                    decimal d = Math.Ceiling(c);
                    Session["d"] = d.ToString();

                }
                else
                {
                    SqlDataAdapter adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "' ", con);


                    adpt.Fill(dts);
                    decimal c = Convert.ToDecimal(dts.Tables[0].Rows.Count);
                    c = c / 3;


                    decimal d = Math.Ceiling(c);
                    Session["d"] = d.ToString();
                }


            }
            if (pagestart == 1)
            {
                //   cmd = new SqlCommand("select   distinct top 3 (hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hakkeemid_h"].ToString() + "' and row_number>=1 ORDER BY hd_name, row_number", con);
                cmd = new SqlCommand("select top 3  * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'  and t>='" + pagestart + "'" + Session["query"].ToString() + " order by t", con);
            }
            else
            {
                pagestart = 1 + ((pagestart - 1) * 3);

                cmd = new SqlCommand("select top 3  * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'  and t>'" + pagestart + "'" + Session["query"].ToString() + " order by t", con);
            }
            //            SELECT DISTINCT
            //  d_name, 
            //  DENSE_RANK() OVER(ORDER BY d_name) row_number
            //FROM view_doc_available_time
            //ORDER BY d_name, row_number
            // cmd = new SqlCommand("select   distinct top 3 (hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hakkeemid_h"].ToString()+ "' and row_number>='"+pagestart+"'  ORDER BY hd_name, row_number", con);


            dr = cmd.ExecuteReader();





            Response.Write("<div class='row'><div class='col-md-12 col-lg-12'><span id='Label1'>");


            Response.Write("<ul class='pagination'>");



            for (int p = 1; p <= Convert.ToInt32(Session["d"].ToString()); p++)
            {
                Response.Write("<li><a href =?page=" + p.ToString() + " > " + p + " </a></li> ");
            }

            Response.Write("</ul>");

            Response.Write("<div>");
            while (dr.Read())
            {



                DateTime dt = new DateTime();
                dt = DateTime.Now.Date;
                int j = 0;
                Response.Write("<div id='box1'>");
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

                                Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
                                //   string cdate = dt.ToString("yyyy-MM-dd");
                                // DateTime  dte = Convert.ToDateTime(cdate);

                                Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



                                Response.Write("</div>");
                                dt = dt.AddDays(1);
                                Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                                // DateTime  dte = Convert.ToDateTime(cdate);

                                Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                Response.Write("</div>");
                                dt = dt.AddDays(1);
                                Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                                // DateTime  dte = Convert.ToDateTime(cdate);

                                Response.Write("<Label  id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

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
                                    //Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                    //Response.Write("<br>");



                                    Response.Write("<label style='margin-top:50px; margin-left:30px'><a  class='btn btn-primary' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
                                    //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                                    Response.Write("</div>");

                                }
                                catch (Exception ex)
                                {
                                    nextdate = "";
                                    Response.Write("<label style='margin-top:16%;'><a href=?next=2  class='btn btn-primary'> Not Available on remaining Dates</a></label>");
                                    //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                                    Response.Write("</div>");
                                    break;
                                    // dt.AddDays(2);

                                }

                                //  Session["datedis"] 


                                //   Response.Write("Next availability");
                                //  Response.Write("<label style='margin-top:35px; margin-left:30px'  class='btn btn-primary'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
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
                                            Response.Write("<div style='top: 82px;left: 0px;position: absolute;'><a href=?next=2><img src='../images/left.png'/></a></div>");
                                        }


                                        Response.Write("<Label  id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
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

                                                    //     string lat = Lat.ToString();
                                                    //  string longti = Long.ToString();


                                                    SqlCommand com = new SqlCommand("select a_status from tbl_hos_doc_appmnt where d_id='" + dr1[1].ToString() + "' and a_time='" + timet.ToString() + "' and a_date='" + cdate.ToString() + "'", con);

                                                    int a_status = 2;
                                                    int f = 0;
                                                    try
                                                    {
                                                        if (com.ExecuteScalar() == null)
                                                        {
                                                            a_status = 2;
                                                            f = 1;
                                                        }
                                                        if (f == 0)
                                                        {
                                                            a_status = Convert.ToInt32(com.ExecuteScalar());
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        a_status = 2;
                                                    }
                                                    //if (a_status == 0)
                                                    //{
                                                    //    Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
                                                    //}
                                                    //else 
                                                    if (a_status == 0 || a_status==1 || a_status==3 || a_status==4)
                                                    {
                                                        Response.Write("<a id='buttontime' title='Appointment Taken' class='btn btn-xs btn-primary btn-hover'>----</a>");
                                                    }
                                                    else
                                                    {

                                                        Response.Write("<a class='btn btn-xs btn-primary btn-hover'  id='buttontime' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + ">" + timet.ToString() + "</a>");

                                                    }

                                                    Response.Write("<br>");


                                                }
                                                catch (Exception ex)
                                                {

                                                    Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label>");
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
                                                Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label>");
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
                                            //  Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                            //   Response.Write("<br>");

                                            Response.Write("<a class='btn btn-xs btn-primary btn-hover' id='buttontime' href=Doctoravailabledateandtime.aspx?docid=" + docidpass + "> More</a>");
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("<Label  id='datedisplay''>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                        for (int ii = 0; ii <= 4; ii++)
                                        { Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label><br>"); }



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
                                        Response.Write("<div style='top: 82px;left: 0px;position: absolute;'><a href=?next=2><img src='../images/left.png'/></a></div>");
                                    }

                                    Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                    string testname = "";
                                    while (dr1.Read())
                                    {
                                        testname = dr1[1].ToString();
                                        if (test <= 3)
                                        {
                                            try
                                            {
                                                /* string[] t = new string[7];

                                                 t = dr1[0].ToString().Split(' ');
                                                 String 
                                                 
                                                
                                                
                                                
                                                = t[0].ToString() + t[1].ToString();
                                                 */
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

                                                //string lat = obj.EnryptString(Lat.ToString());
                                                //string longti = obj.EnryptString(Long.ToString());

                                                //  string lat = Lat.ToString();
                                                // string longti = Long.ToString();


                                                SqlCommand com = new SqlCommand("select a_status from tbl_hos_doc_appmnt where d_id='" + dr1[1].ToString() + "' and a_time='" +timet.ToString() + "' and a_date='" + cdate.ToString() + "'", con);

                                                int a_status = 2;
                                                int f = 0;
                                                try
                                                {
                                                    if (com.ExecuteScalar() == null)
                                                    {
                                                        a_status = 2;
                                                        f = 1;
                                                    }
                                                    if (f == 0)
                                                    {
                                                        a_status = Convert.ToInt32(com.ExecuteScalar());
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    a_status = 2;
                                                }
                                                //if (a_status == 0)
                                                //{
                                                //    Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
                                                //}
                                                //else 
                                                if (a_status==0 || a_status == 1 || a_status==3 ||a_status==4)
                                                {
                                                    Response.Write("<a title='Appointment Taken' class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</a>");
                                                }
                                                else
                                                {
                                                    Response.Write("<a class='btn btn-xs btn-primary btn-hover' id='buttontime' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + ">" + dr1[0].ToString() + "</a>");

                                                }

                                                Response.Write("<br>");


                                            }
                                            catch (Exception ex)
                                            {

                                                Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label>");
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
                                            Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label>");
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
                                        //  Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                        //   Response.Write("<br>");

                                        Response.Write("<a class='btn btn-xs btn-primary btn-hover' id='buttontime' href=Doctoravailabledateandtime.aspx?docid=" + docidpass + "> More</a>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                                    for (int ii = 0; ii <= 4; ii++)
                                    { Response.Write("<Label class='btn btn-xs btn-primary btn-hover'id='buttontime'>----</Label><br>"); }



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

                            Response.Write("<Label  id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



                            Response.Write("</div>");
                            dt = dt.AddDays(1);
                            Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
                            Response.Write("</div>");
                            dt = dt.AddDays(1);
                            Response.Write("<div style='display:inline-block;padding-left:;height:auto'>");

                            // DateTime  dte = Convert.ToDateTime(cdate);

                            Response.Write("<Label  id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

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
                                //Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
                                //Response.Write("<br>");



                                Response.Write("<label id='sliderp'><a style='margin-top:50px; margin-left:30px' class='btn btn-primary' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
                                //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                                Response.Write("</div>");

                            }
                            catch (Exception ex)
                            {
                                nextdate = "";
                                Response.Write("<label style='margin-top:16%;'><a href=?next=2  class='btn btn-primary'> Not Available on remaining Dates</a></label>");
                                //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
                                Response.Write("</div>");
                                break;
                                // dt.AddDays(2);

                            }

                            //  Session["datedis"] 


                            //   Response.Write("Next availability");
                            //  Response.Write("<label style='margin-top:35px; margin-left:30px'  class='btn btn-primary'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
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
        //}//End Auto..................English---------------------------------------
        //else//..................Start Arabic---------------------------------------
        //{
        //    DataSet dts = new DataSet();

        //    Session["query"] = "";
        //    int pagestart = 1;
        //    try
        //    {
        //        if (Request.QueryString["page"] == null)
        //        {
        //            pagestart = 1;
        //            q = 0;
        //        }
        //        else
        //        {
        //            pagestart = Convert.ToInt32(Request.QueryString["page"]);
        //            q = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        pagestart = 1;
        //    }
        //    if (q == 0)
        //    {

        //        if (docName != "")
        //        {

        //            Session["query"] = docName.ToString();
        //            Session["query"] = Session["query"].ToString().Replace("where", " and ");
        //            SqlDataAdapter adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'" + Session["query"], con);


        //            adpt.Fill(dts);
        //            decimal c = Convert.ToDecimal(dts.Tables[0].Rows.Count);
        //            c = c / 3;


        //            decimal d = Math.Ceiling(c);
        //            Session["d"] = d.ToString();

        //        }
        //        else
        //        {
        //            SqlDataAdapter adpt = new SqlDataAdapter("select  distinct(hd_name) as d_name from  view_hos_doc_available_time where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "' ", con);


        //            adpt.Fill(dts);
        //            decimal c = Convert.ToDecimal(dts.Tables[0].Rows.Count);
        //            c = c / 3;


        //            decimal d = Math.Ceiling(c);
        //            Session["d"] = d.ToString();
        //        }


        //    }
        //    if (pagestart == 1)
        //    {
        //        //   cmd = new SqlCommand("select   distinct top 3 (hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hakkeemid_h"].ToString() + "' and row_number>=1 ORDER BY hd_name, row_number", con);
        //        cmd = new SqlCommand("select top 3  * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'  and t>='" + pagestart + "'" + Session["query"].ToString() + " order by t", con);
        //    }
        //    else
        //    {
        //        pagestart = 1 + ((pagestart - 1) * 3);

        //        cmd = new SqlCommand("select top 3  * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'  and t>'" + pagestart + "'" + Session["query"].ToString() + " order by t", con);
        //    }
        //    //            SELECT DISTINCT
        //    //  d_name, 
        //    //  DENSE_RANK() OVER(ORDER BY d_name) row_number
        //    //FROM view_doc_available_time
        //    //ORDER BY d_name, row_number
        //    // cmd = new SqlCommand("select   distinct top 3 (hd_name) as d_name,DENSE_RANK() OVER(ORDER BY hd_name) row_number from view_hos_doc_available_time where h_id='" + Session["hakkeemid_h"].ToString()+ "' and row_number>='"+pagestart+"'  ORDER BY hd_name, row_number", con);


        //    dr = cmd.ExecuteReader();





        //    Response.Write("<div class='row'><div class='col-md-12 col-lg-12'><span id='Label2'>");


        //    Response.Write("<ul class='pagination'>");



        //    for (int p = 1; p <= Convert.ToInt32(Session["d"].ToString()); p++)
        //    {
        //        Response.Write("<li><a href =?page=" + p.ToString() + " > " + p + " </a></li> ");
        //    }

        //    Response.Write("</ul>");

        //    Response.Write("<div>");
        //    while (dr.Read())
        //    {



        //        DateTime dt = new DateTime();
        //        dt = DateTime.Now.Date;
        //        int j = 0;
        //        Response.Write("<div id='box1'>");
        //        Response.Write("<table class='table table-responsive table-hover' /*style='border-collapse: separate;border-spacing: 1px 0;'*/>");
        //        Response.Write("<tr class='box box-primary box-solid'><td style='font-weight:bold;'>" + dr[0].ToString() + "</td></tr><tr class='box'><td>");


        //        ///////////////////////////////////////////////////////

        //        Response.Write("<ul class='bxslider'>");
        //        Response.Write("<li>");
        //        for (int i = 0; i < 30; i++)
        //        {


        //            string cdate = dt.ToString("yyyy-MM-dd");

        //            if (j < 3)
        //            {



        //                if (j == 0)
        //                {

        //                    if (Request.QueryString["next"] == "1")
        //                    {

        //                        string date = Request.QueryString["docdate"];


        //                        string datee = obj.DecryptString(date);


        //                        DateTime dtnew = Convert.ToDateTime(datee);

        //                        dt = dtnew;
        //                        //list_doctorstest("", "", "", "", "", "", dtnew);
        //                        //  Request.QueryString["next"] = "0";
        //                    }

        //                    //else if(Request.QueryString["next"] == "2")
        //                    //{ dt = DateTime.Now.Date;
        //                    //}


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

        //                        Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
        //                        //   string cdate = dt.ToString("yyyy-MM-dd");
        //                        // DateTime  dte = Convert.ToDateTime(cdate);

        //                        Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



        //                        Response.Write("</div>");
        //                        dt = dt.AddDays(1);
        //                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

        //                        // DateTime  dte = Convert.ToDateTime(cdate);

        //                        Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
        //                        Response.Write("</div>");
        //                        dt = dt.AddDays(1);
        //                        Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

        //                        // DateTime  dte = Convert.ToDateTime(cdate);

        //                        Response.Write("<Label  id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

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

        //                            //string[] t = new string[7];

        //                            //t = dr1[0].ToString().Split(' ');
        //                            //string doctime = obj.EnryptString(t[0].ToString());
        //                            //string timeperiod = obj.EnryptString(t[1].ToString());
        //                            //string lat = obj.EnryptString(Lat.ToString());
        //                            //string longti = obj.EnryptString(Long.ToString());
        //                            //Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
        //                            //Response.Write("<br>");



        //                            Response.Write("<label style='margin-top:50px; margin-left:30px'><a  class='btn btn-primary' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
        //                            //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
        //                            Response.Write("</div>");

        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            nextdate = "";
        //                            Response.Write("<label style='margin-top:16%;'><a href=?next=2  class='btn btn-primary'> Not Available on remaining Dates</a></label>");
        //                            //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
        //                            Response.Write("</div>");
        //                            break;
        //                            // dt.AddDays(2);

        //                        }

        //                        //  Session["datedis"] 


        //                        //   Response.Write("Next availability");
        //                        //  Response.Write("<label style='margin-top:35px; margin-left:30px'  class='btn btn-primary'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
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
        //                        Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
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

        //                                if (Request.QueryString["next"] == "1")
        //                                {
        //                                    Response.Write("<div style='top: 82px;left: 0px;position: absolute;'><a href=?next=2><img src='../images/left.png'/></a></div>");
        //                                }


        //                                Response.Write("<Label  id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
        //                                string testname = "";
        //                                while (dr1.Read())
        //                                {
        //                                    testname = dr1[1].ToString();
        //                                    if (test <= 3)
        //                                    {
        //                                        try
        //                                        {
        //                                            String timet = "";
        //                                            string[] t = new string[7];
        //                                            string ab1 = "", ab2 = "";
        //                                            try
        //                                            {
        //                                                t = dr1[0].ToString().Split(' ');


        //                                                ab1 = t[0].ToString();
        //                                                ab2 = t[1].ToString();
        //                                                timet = ab1.ToString() + " " + ab2.ToString();
        //                                            }
        //                                            catch (Exception ex)
        //                                            {
        //                                                int l = dr1[0].ToString().Count();
        //                                                ab1 = dr1[0].ToString().Substring(0, l - 2);
        //                                                ab2 = dr1[0].ToString().Substring(l - 2, 2);
        //                                                timet = ab1.ToString() + " " + ab2.ToString();
        //                                            }



        //                                            //Session["org_time"] = dr1[0].ToString();
        //                                            string docidpass = obj.EnryptString(dr1[1].ToString());
        //                                            string docdate = obj.EnryptString(cdate.ToString());
        //                                            string doctime = obj.EnryptString(ab1.ToString());
        //                                            string timeperiod = obj.EnryptString(ab2.ToString());
        //                                            //string hospitalId = obj.EnryptString(dr[13].ToString());
        //                                            //string lat = obj.EnryptString(Lat.ToString());
        //                                            //string longti = obj.EnryptString(Long.ToString());

        //                                            //     string lat = Lat.ToString();
        //                                            //  string longti = Long.ToString();


        //                                            SqlCommand com = new SqlCommand("select a_status from tbl_hos_doc_appmnt where d_id='" + dr1[1].ToString() + "' and a_time='" + timet.ToString() + "' and a_date='" + cdate.ToString() + "'", con);

        //                                            int a_status = 2;
        //                                            int f = 0;
        //                                            try
        //                                            {
        //                                                if (com.ExecuteScalar() == null)
        //                                                {
        //                                                    a_status = 2;
        //                                                    f = 1;
        //                                                }
        //                                                if (f == 0)
        //                                                {
        //                                                    a_status = Convert.ToInt32(com.ExecuteScalar());
        //                                                }
        //                                            }
        //                                            catch (Exception ex)
        //                                            {
        //                                                a_status = 2;
        //                                            }

        //                                            //if (a_status == 0)
        //                                            //{
        //                                            //    Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
        //                                            //}
        //                                            //else 
        //                                            if (a_status == 0 || a_status == 1 || a_status == 3 || a_status == 4)
        //                                            {
        //                                                Response.Write("<a id='buttontime' title='Appointment Taken' class='btn btn-xs btn-primary btn-hover'>----</a>");
        //                                            }
        //                                            else
        //                                            {

        //                                                Response.Write("<a class='btn btn-xs btn-primary btn-hover'  id='buttontime' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + ">" + timet.ToString() + "</a>");

        //                                            }

        //                                            Response.Write("<br>");


        //                                        }
        //                                        catch (Exception ex)
        //                                        {

        //                                            Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label>");
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
        //                                        Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label>");
        //                                        Response.Write("<br>");
        //                                    }
        //                                }
        //                                if (test > 3)
        //                                {
        //                                    string docidpass = obj.EnryptString(testname.ToString());

        //                                    //string lat = obj.EnryptString(Lat.ToString());
        //                                    //string longti = obj.EnryptString(Long.ToString());

        //                                    //string lat = Lat.ToString();
        //                                    //  string longti = Long.ToString();
        //                                    //  Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
        //                                    //   Response.Write("<br>");

        //                                    Response.Write("<a class='btn btn-xs btn-primary btn-hover' id='buttontime' href=Doctoravailabledateandtime.aspx?docid=" + docidpass + "> More</a>");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Response.Write("<Label  id='datedisplay''>" + dt.ToString("ddd MMM dd") + "</Label></br>");
        //                                for (int ii = 0; ii <= 4; ii++)
        //                                { Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label><br>"); }



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

        //                    Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
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
        //                            if (Request.QueryString["next"] == "1")
        //                            {
        //                                Response.Write("<div style='top: 82px;left: 0px;position: absolute;'><a href=?next=2><img src='../images/left.png'/></a></div>");
        //                            }

        //                            Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
        //                            string testname = "";
        //                            while (dr1.Read())
        //                            {
        //                                testname = dr1[1].ToString();
        //                                if (test <= 3)
        //                                {
        //                                    try
        //                                    {

        //                                        String timet = "";
        //                                        string[] t = new string[7];
        //                                        string ab1 = "", ab2 = "";
        //                                        try
        //                                        {
        //                                            t = dr1[0].ToString().Split(' ');


        //                                            ab1 = t[0].ToString();
        //                                            ab2 = t[1].ToString();
        //                                            timet = ab1.ToString() + " " + ab2.ToString();
        //                                        }
        //                                        catch (Exception ex)
        //                                        {
        //                                            int l = dr1[0].ToString().Count();
        //                                            ab1 = dr1[0].ToString().Substring(0, l - 2);
        //                                            ab2 = dr1[0].ToString().Substring(l - 2, 2);
        //                                            timet = ab1.ToString() + " " + ab2.ToString();
        //                                        }

        //                                        string docidpass = obj.EnryptString(dr1[1].ToString());
        //                                        string docdate = obj.EnryptString(cdate.ToString());
        //                                        string doctime = obj.EnryptString(ab1.ToString());
        //                                        string timeperiod = obj.EnryptString(ab2.ToString());

        //                                        SqlCommand com = new SqlCommand("select a_status from tbl_hos_doc_appmnt where d_id='" + dr1[1].ToString() + "' and a_time='" + timet.ToString() + "' and a_date='" + cdate.ToString() + "'", con);

        //                                        //int a_status;
        //                                        //try
        //                                        //{
        //                                        //    a_status = Convert.ToInt32(com.ExecuteScalar());
        //                                        //}
        //                                        //catch (Exception ex)
        //                                        //{
        //                                        //    a_status = 2;
        //                                        //}
        //                                        int a_status = 2;
        //                                        int f = 0;
        //                                        try
        //                                        {
        //                                            if (com.ExecuteScalar() == null)
        //                                            {
        //                                                a_status = 2;
        //                                                f = 1;
        //                                            }
        //                                            if (f == 0)
        //                                            {
        //                                                a_status = Convert.ToInt32(com.ExecuteScalar());
        //                                            }
        //                                        }
        //                                        catch (Exception ex)
        //                                        {
        //                                            a_status = 2;
        //                                        }
        //                                        //if (a_status == 0)
        //                                        //{
        //                                        //    Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' >" + dr1[0].ToString() + "</a>");
        //                                        //}
        //                                        //else 
        //                                        if (a_status == 0 || a_status == 1 || a_status == 3 || a_status == 4)
        //                                        {
        //                                            Response.Write("<a title='Appointment Taken' class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</a>");
        //                                        }
        //                                        else
        //                                        {
        //                                            Response.Write("<a class='btn btn-xs btn-primary btn-hover' id='buttontime' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + ">" + dr1[0].ToString() + "</a>");

        //                                        }

        //                                        Response.Write("<br>");


        //                                    }
        //                                    catch (Exception ex)
        //                                    {

        //                                        Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label>");
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
        //                                    Response.Write("<Label class='btn btn-xs btn-primary btn-hover' id='buttontime'>----</Label>");
        //                                    Response.Write("<br>");
        //                                }
        //                            }
        //                            if (test > 3)
        //                            {
        //                                string docidpass = obj.EnryptString(testname.ToString());

        //                                //string lat = obj.EnryptString(Lat.ToString());
        //                                //string longti = obj.EnryptString(Long.ToString());

        //                                //string lat = Lat.ToString();
        //                                //string longti = Long.ToString();
        //                                //  Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
        //                                //   Response.Write("<br>");

        //                                Response.Write("<a class='btn btn-xs btn-primary btn-hover' id='buttontime' href=Doctoravailabledateandtime.aspx?docid=" + docidpass + "> More</a>");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
        //                            for (int ii = 0; ii <= 4; ii++)
        //                            { Response.Write("<Label class='btn btn-xs btn-primary btn-hover'id='buttontime'>----</Label><br>"); }



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

        //                    Response.Write("<div id='sliderp'><div style='display:inline-block;padding-left:8px;height:auto'>");
        //                    //   string cdate = dt.ToString("yyyy-MM-dd");
        //                    // DateTime  dte = Convert.ToDateTime(cdate);

        //                    Response.Write("<Label  id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");



        //                    Response.Write("</div>");
        //                    dt = dt.AddDays(1);
        //                    Response.Write("<div style='display:inline-block;padding-left:8px;height:auto'>");

        //                    // DateTime  dte = Convert.ToDateTime(cdate);

        //                    Response.Write("<Label id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");
        //                    Response.Write("</div>");
        //                    dt = dt.AddDays(1);
        //                    Response.Write("<div style='display:inline-block;padding-left:;height:auto'>");

        //                    // DateTime  dte = Convert.ToDateTime(cdate);

        //                    Response.Write("<Label  id='datedisplay'>" + dt.ToString("ddd MMM dd") + "</Label></br>");

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
        //                        //Response.Write("<a class='btn btn-xs btn-primary btn-hover' style='text-align: center;margin-top:5%;width:64px;' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&doctime=" + doctime.ToString() + "&timeperiod=" + timeperiod.ToString() + "&Lat=" + lat.ToString() + "&Long=" + longti.ToString() + ">" + dr1[0].ToString() + "</a>");
        //                        //Response.Write("<br>");



        //                        Response.Write("<label id='sliderp'><a style='margin-top:50px; margin-left:30px' class='btn btn-primary' href=?docid=" + docidpass.ToString() + "&docdate=" + docdate.ToString() + "&next=1> Next Availability " + nextdate.ToString() + "</a></label>");
        //                        //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
        //                        Response.Write("</div>");

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        nextdate = "";
        //                        Response.Write("<label style='margin-top:16%;'><a href=?next=2  class='btn btn-primary'> Not Available on remaining Dates</a></label>");
        //                        //  Response.Write("<a href=?docid=" + dr[0].ToString() + "&docdate=" + Session["cdate2"].ToString() + " >Next availability</a>");
        //                        Response.Write("</div>");
        //                        break;
        //                        // dt.AddDays(2);

        //                    }

        //                    //  Session["datedis"] 


        //                    //   Response.Write("Next availability");
        //                    //  Response.Write("<label style='margin-top:35px; margin-left:30px'  class='btn btn-primary'><a href=?docid=" + dr[9].ToString() + "&docdate=" + Session["cdate2"].ToString() + " &next=1> Next Availability "+ nextdate.ToString() + "</a></label>");
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

        //        ////////////////////////////////////////////////////////////////////////////////////////////



        //        Response.Write("</td></tr></table>");

        //        Response.Write("</div>");
        //    }

        //    Response.Write("</div>");

        //    dr.Close();


        //    //span Label1 close here
        //    Response.Write("</div></div></span>");
        //}
        con.Close();
    }

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                    select new { item1.h_id, item.latitude };
        //try
        //{
        if (query.Count() <= 0)
        {
            //Label8.Text = "You must set your location";
            //ModalPopupExtender4.Show();
            // Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location.')</Script>");
               // Response.Redirect("sethospitalLocation.aspx");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب تعيين موقعك')</Script>");
            //   // Response.Redirect("sethospitalLocation.aspx?l=ar-EG");
            //}
        }
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("../index/hospita login.aspx");
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("SetHospitalLocation.aspx");
        //}
        //else
        //{
        //    Response.Redirect("SetHospitalLocation.aspx?l=ar-EG");
        //}
        //searchfiltr();
    }

    protected void BtnTakeAppointment_Click(object sender, EventArgs e)
    {

        var selct = from a in db.tbl_hos_doc_appmnts
                    where a.a_date == TxtApntmtDate.Text && a.a_time == TxtApointmentTime.Text && a.d_id == Session["did"].ToString() && a.h_id == Session["hakkeemid_h"].ToString()
                    select a;
        if (selct.Count() > 0)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Label1.Text = "Already have an appointment.";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
                upModal1.Update();
                //RegisterStartupScript("", "<Script Language=JavaScript>swal('Already have an appointment')</Script>");
            //}
            //else
            //{
            //    Label1.Text = "لديك بالفعل موعد";

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
            //    upModal1.Update();
            //  //  RegisterStartupScript("", "<Script Language=JavaScript>swal('لديك بالفعل موعد')</Script>");
            //}
        }

        //Label2.Text = "Already have an appointment";
        //this.ModalPopupExtender1.Show();

        //else
        //{


        //    var selct1 = from a in db.tbl_hos_doc_appmnts
        //                 where a.a_date == TxtApntmtDate.Text && a.a_time == TxtApointmentTime.Text && a.d_id == Session["did"].ToString() && a.h_id == Session["hakkeemid_h"].ToString()
        //                 select a;
        //    if (selct1.Count() > 0)
        //    {
        //        if (Session["Language"].ToString() == "Auto")
        //        {
        //            Label1.Text = "You were already took an appointment on this day.";

        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
        //            upModal1.Update();

        //           // RegisterStartupScript("", "<Script Language=JavaScript>swal('You were already took an appointment on this day')</Script>");
        //        }
        //        else
        //        {
        //            Label1.Text = "كنت قد اتخذت بالفعل موعد في هذا اليوم";

        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
        //            upModal1.Update();
        //          //  RegisterStartupScript("", "<Script Language=JavaScript>swal('كنت قد اتخذت بالفعل موعد في هذا اليوم')</Script>");
        //        }
        //    }

            else
            {









                var selectUser = from item in db.tbl_signups
                                 where (item.email == TxtBookDocUserId.Text || item.u_hakkimid == TxtBookDocUserId.Text)
                                 select item;
                if (selectUser.Count() > 0)
                {
                    foreach (var ss in selectUser)
                    {
                        tbl_hos_doc_appmnt tbl = new tbl_hos_doc_appmnt
                        {
                            a_time = TxtApointmentTime.Text,
                            a_payment = DdlPayments.SelectedItem.Text,
                            a_date = TxtApntmtDate.Text,
                            a_reason = TxtReasonToVisit.Text,
                            a_status = 0,
                            d_id = Session["did"].ToString(),
                            h_id = Session["hakkeemid_h"].ToString(),
                            u_id = ss.u_hakkimid,
                        };
                        db.tbl_hos_doc_appmnts.InsertOnSubmit(tbl);
                        db.SubmitChanges();
                    }
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + TxtApntmtDate.Text + "'", con);
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    Session["Day"] = dt1.Rows[0]["dayname"].ToString();
                }
                //if (Session["Language"].ToString() == "Auto")
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Appointment set succesfully')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تعيين تعيين بنجاح')</Script>");
                //}
                try
                    {
                        //  Email.mail(Session["user"].ToString(), msg, "Appointment fixed");
                        string doctorname = "";
                        string username = "";
                        var user1 = from item in db.tbl_signups where item.u_hakkimid == TxtBookDocUserId.Text select item;
                        foreach (var u in user1) { username = u.name; }
                        var doctor = from item in db.tbl_hdoctors where item.hd_email == Session["did"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() select item;
                        foreach (var d in doctor)
                        {
                            string m = "Your Appointment has been fixed with patient " + username + " on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + ". Thank you!";
                            try {
                            //Email.mail(d.hd_email, m, "Hakkeem appointment");
                            Email_To_Doctorappoinment(d.hd_email, username, TxtApntmtDate.Text, TxtApointmentTime.Text,Session["Day"].ToString());
                        } catch (Exception ex) { }
                            doctorname = d.hd_name;
                        }
                        var user = from item in db.tbl_signups where item.u_hakkimid == TxtBookDocUserId.Text select item;
                        foreach (var ss in user)
                        {
                            string m = "Your Appointment has been fixed with Doctor " + doctorname + " on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + ". Thank you!";
                            try
                            {
                            //  Email.mail(obj.DecryptString(ss.email), m, "Hakkeem appointment");
                            Email_To_userappoinment(obj.DecryptString(ss.email), doctorname, TxtApntmtDate.Text, TxtApointmentTime.Text,Session["Day"].ToString());
                            }
                            catch (Exception ex) { }
                            try { ob.Message(obj.DecryptString(ss.contact), m); } catch (Exception ex) { }
                        }
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            Session["msg"] = "";
                            Session["msg"] = "Appointment has been fixed with Doctor: <b>" + doctorname + "</b> and Patient: <b>" + username + "</b> on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + "!";
                           
                        //}
                        //else
                        //{
                        //    Session["msg"] = "";
                        //    Session["msg"] = "تم إصلاح موعد مع الطبيب " + doctorname + "والمريض " + username + " على " + TxtApntmtDate.Text + " و " + TxtApointmentTime.Text + "!";
                        //}
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        //if (Session["Language"].ToString() == "Auto")
                        //{

                            Response.Redirect("Finish appointment.aspx");
                        //}
                        //else
                        //{
                        //    Response.Redirect("Finish appointment.aspx?l=ar-EG");
                        //}
                    }
                    catch (Exception ex)
                    {
                        //if (Session["Language"].ToString() == "Auto")
                        //{

                            Response.Redirect("Finish appointment.aspx");
                        //}
                        //else
                        //{
                        //    Response.Redirect("Finish appointment.aspx?l=ar-EG");
                        //}
                    }
                
                    TxtApntmtDate.Text = TxtApointmentTime.Text = TxtBookDocUserId.Text = "";
                }
                else
                {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Label8.Text = "The Hakkeem user id doesn't exist. Please give correct Id.";
                    // RegisterStartupScript("", "<Script Language=JavaScript>swal('The Hakkeem user id doesn't exist. Please give correct Id.')</Script>");
                //}
                //else
                //{
                //    Label8.Text = "معرف المستخدم هكيم غير موجود. يرجى إعطاء الرقم التعريفي الصحيح";
                    // RegisterStartupScript("", "<Script Language=JavaScript>swal('معرف المستخدم هكيم غير موجود. يرجى إعطاء الرقم التعريفي الصحيح')</Script>");

              //  }
                //Label2.Text = "The Hakkeem user id doesn't exist. Please give correct Id.";

                //this.ModalPopupExtender1.Show();
            }
            }
        }
    public bool Email_To_userappoinment(string email, string doctorname, string date, string time,string day)
    {
        try
        {
            String messagestr = "";
            bool flag = true;
            string bg = "http://www.hakkeem.com/head1.png";
            string follw = "http://www.hakkeem.com/followus.png";
            string face = "http://www.hakkeem.com/facebook.png";
            string twitter = "http://www.hakkeem.com/twitter.png";
            string insta = "http://www.hakkeem.com/instagram.png";
            string sthetho = "http://www.hakkeem.com/stethoscope1.png";
            string timepath = "http://www.hakkeem.com/time1.png";
            string calender = "http://www.hakkeem.com/calendar1.png";
            string contact = "http://hakkeem.com/ContactUs.aspx";
            string privacy = "http://hakkeem.com/privacy%20policy.aspx";
            messagestr = messagestr + "<body  style='text-align:center;width=100%'>";

            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto; background:#f2f2f2;padding:60px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + "<tr>";
            messagestr = messagestr + " <td>";
            messagestr = messagestr + " <table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + " <tr>";
            messagestr = messagestr + "<td  colspan='2'  style='padding:20px 20px;background-color:#fff;line-height:3.5em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%'></td>";

            messagestr = messagestr + "</tr>";
            messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:20px;text-align:center;font-weight:bold;'>";
            messagestr = messagestr + "Hakkeem Appointment</td></tr>";
            messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:11px;line-height:1.5em;text-align:center;padding-top:10px'>";
            messagestr = messagestr + "'We believe that the greatest gift you can give your family and the world is a healthy you' </td></tr>";

            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td  style='color:#4aa9af;font-weight:bold;'><img src='" + sthetho + "' ></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>Dr." + doctorname + "</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + timepath + "'></td><td style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + time + " "+day+"</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + calender + "'></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + date + "</td></tr></tbody></table></td></tr>";


            messagestr = messagestr + "<tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
            messagestr = messagestr + " <tbody><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<span style='color:#4aa9af'><a href='"+privacy+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='"+contact+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
            messagestr = messagestr + "</span></td></tr><br><br><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<img src='" + follw + "'>";
            messagestr = messagestr + "<a href='https://www.facebook.com/hakkeem.etqan.1' style='text-decoration:none'><img src='" + face + "' title='Facbook'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://twitter.com/Hakkeem_1' style='text-decoration:none'><img src='" + twitter + "'  title='Twitter'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://www.instagram.com/hakkeem_1/' style='text-decoration:none'><img src='" + insta + "'  title='Instagram'>&nbsp;</a>";
            messagestr = messagestr + "</td></tr></tbody></table></td></tr>";

            messagestr = messagestr + "<tr><td  colspan='2' style='background-color:#fff;padding:20px 20px'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:25px'>";
            messagestr = messagestr + " <tbody><tr>";
            messagestr = messagestr + "<td width='100%' style='color:#4aa9af;font-family:sans-serif; font-size:10px'>";
            messagestr = messagestr + " <span style='font-weight:bold;font-size:10px'> Discliamer</span> : Please do not print this email unless it is necessary. Every unprinted email helps the environment.";
            messagestr = messagestr + "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body>";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
            mail.Subject = "Hakkeem Appointment";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = messagestr.ToString();
            // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("mail@hakkeem.com", "Hakkeem2018!!");
            client.Port = 25;
            client.Host = "smtp.goldenetqan.com";
            client.EnableSsl = false;
            try
            {
                client.Send(mail);

            }
            catch (Exception ex)
            {

            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return flag;
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            throw ex;
        }
    }
    public bool Email_To_Doctorappoinment(string email, string username, string date, string time,string day)
    {
        try
        {
            String messagestr = "";
            bool flag = true;
            string bg = "http://www.hakkeem.com/head1.png";
            string follw = "http://www.hakkeem.com/followus.png";
            string face = "http://www.hakkeem.com/facebook.png";
            string twitter = "http://www.hakkeem.com/twitter.png";
            string insta = "http://www.hakkeem.com/instagram.png";
            string sthetho = "http://www.hakkeem.com/stethoscope1.png";
            string timepath = "http://www.hakkeem.com/time1.png";
            string calender = "http://www.hakkeem.com/calendar1.png";
            string contact = "http://hakkeem.com/ContactUs.aspx";
            string privacy = "http://hakkeem.com/privacy%20policy.aspx";
            messagestr = messagestr + "<body  style='text-align:center;width=100%'>";

            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto; background:#f2f2f2;padding:60px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + "<tr>";
            messagestr = messagestr + " <td>";
            messagestr = messagestr + " <table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + " <tr>";
            messagestr = messagestr + "<td  colspan='2'  style='padding:20px 20px;background-color:#fff;line-height:3.5em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%'></td>";

            messagestr = messagestr + "</tr>";
            messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:20px;text-align:center;font-weight:bold;'>";
            messagestr = messagestr + "Hakkeem Appointment</td></tr>";
            messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:11px;line-height:1.5em;text-align:center;padding-top:10px'>";
            messagestr = messagestr + "'We believe that the greatest gift you can give your family and the world is a healthy you' </td></tr>";

            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td  style='color:#4aa9af;font-weight:bold;'><img src='" + sthetho + "' ></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + username + "</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + timepath + "'></td><td style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + time + " "+day+"</td></tr></tbody></table></td></tr>";
            messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin:auto !important;margin-left:85px;'><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + calender + "'></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>" + date + "</td></tr></tbody></table></td></tr>";


            messagestr = messagestr + "<tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
            messagestr = messagestr + " <tbody><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<span style='color:#4aa9af'><a href='"+privacy+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='"+contact+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
            messagestr = messagestr + "</span></td></tr><br><br><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<img src='" + follw + "'>";
            messagestr = messagestr + "<a href='https://www.facebook.com/hakkeem.etqan.1' style='text-decoration:none'><img src='" + face + "' title='Facbook'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://twitter.com/Hakkeem_1' style='text-decoration:none'><img src='" + twitter + "'  title='Twitter'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://www.instagram.com/hakkeem_1/' style='text-decoration:none'><img src='" + insta + "'  title='Instagram'>&nbsp;</a>";
            messagestr = messagestr + "</td></tr></tbody></table></td></tr>";

            messagestr = messagestr + "<tr><td  colspan='2' style='background-color:#fff;padding:20px 20px'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:25px'>";
            messagestr = messagestr + " <tbody><tr>";
            messagestr = messagestr + "<td width='100%' style='color:#4aa9af;font-family:sans-serif; font-size:10px'>";
            messagestr = messagestr + " <span style='font-weight:bold;font-size:10px'> Discliamer</span> : Please do not print this email unless it is necessary. Every unprinted email helps the environment.";
            messagestr = messagestr + "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body>";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
            mail.Subject = "Hakkeem Appointment";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = messagestr.ToString();
            // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("mail@hakkeem.com", "Hakkeem2018!!");
            client.Port = 25;
            client.Host = "smtp.goldenetqan.com";
            client.EnableSsl = false;
            try
            {
                client.Send(mail);

            }
            catch (Exception ex)
            {

            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return flag;
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            throw ex;
        }
    }























    //try
    //{
    //    int i = 0;
    //    var qq = from itemqq in db.tbl_signups where itemqq.u_hakkimid == TxtBookDocUserId.Text select itemqq;
    //    if (qq.Count() > 0)
    //    {
    //        var Query = from item in db.tbl_hos_doc_appmnts where item.a_date == TxtApntmtDate.Text && item.u_id == TxtBookDocUserId.Text && (item.a_status == 0) || (item.a_status == 1) select item;
    //        if (Query.Count() > 0)
    //        {
    //            i = 1;
    //        }
    //        else
    //        {
    //            var doctor = from item in db.tbl_doctor_appointments where item.c_id == TxtBookDocUserId.Text && item.a_date == TxtApntmtDate.Text && item.a_status == 1 select item;
    //            if (Query.Count() > 0)
    //            {
    //                i = 1;
    //            }
    //        }
    //        if (i == 1)
    //        {
    //            if (Session["Language"].ToString() == "Auto")
    //            {
    //                Label1.Text = "Already you take an appointment this day, so you please choose another day.";
    //                //RegisterStartupScript("", "<Script Language=JavaScript>swal('Already you take an appointment this day, so you please choose another day.')</Script>");
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
    //                upModal1.Update();
    //            }
    //            else
    //            {
    //                //RegisterStartupScript("", "<Script Language=JavaScript>swal('إذا كنت تأخذ موعد هذا اليوم، لذلك يرجى اختيار يوم آخر')</Script>");
    //                Label1.Text = "إذا كنت تأخذ موعد هذا اليوم، لذلك يرجى اختيار يوم آخر";
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
    //                upModal1.Update();

    //            }
    //            //Label1.Text = "Already you take an appointment this day, so you please choose another day.";
    //            //this.ModalPopupExtender2.Show();
    //        }
    //        else
    //        {
    //            tbl_hos_doc_appmnt ha = new tbl_hos_doc_appmnt()
    //            {
    //                d_id = Session["did"].ToString(),
    //                a_date = TxtApntmtDate.Text,
    //                u_id = TxtBookDocUserId.Text,
    //                a_status = 1,
    //                a_payment = DdlPayments.SelectedItem.Text,
    //                a_reason = TxtReasonToVisit.SelectedItem.Text,
    //                h_id = Session["hakkeemid_h"].ToString(),
    //                a_time = TxtApointmentTime.Text,
    //            };
    //            db.tbl_hos_doc_appmnts.InsertOnSubmit(ha);
    //            db.SubmitChanges();
    //            // Availability();
    //            //string msg = "Dear patient, your appointment is fixed. Thank you" + "<br />" + " Hakkeem Team.";
    //            try
    //            {
    //                //  Email.mail(Session["user"].ToString(), msg, "Appointment fixed");
    //                string doctorname = "";
    //                string username = "";
    //                var user1 = from item in db.tbl_signups where item.u_hakkimid == TxtBookDocUserId.Text select item;
    //                foreach (var u in user1) { username = u.name; }
    //                var doctor = from item in db.tbl_hdoctors where item.hd_email == Session["did"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() select item;
    //                foreach(var d in doctor)
    //                {
    //                    string m = "Your Appointment has been fixed with patient " + username + " on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + ". Thank you!";
    //                    try { Email.mail(d.hd_email, m, "Hakkeem appointment"); } catch (Exception ex) { }
    //                    doctorname = d.hd_name;
    //                }
    //                var user = from item in db.tbl_signups where item.u_hakkimid == TxtBookDocUserId.Text select item;
    //                foreach(var ss in user)
    //                {
    //                    string m = "Your Appointment has been fixed with Doctor " + doctorname + " on " + TxtApntmtDate.Text + " and " + TxtApointmentTime.Text + ". Thank you!";
    //                    try
    //                    {
    //                        Email.mail(obj.DecryptString(ss.email), m, "Hakkeem appointment");
    //                    }
    //                    catch (Exception ex) { }
    //                    try { ob.Message(obj.DecryptString(ss.contact), m); } catch (Exception ex) { }
    //                }
    //            }
    //            catch(Exception ex)
    //            {

    //            }


    //            ///  Response.Redirect("~/User/Finish appointment.aspx");
    //            ///  
    //            if (Session["Language"].ToString() == "Auto")
    //            {
    //                Label1.Text = "Your appointment is fixed.";

    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
    //                upModal1.Update();
    //            }
    //            else
    //            {
    //                //RegisterStartupScript("", "<Script Language=JavaScript>swal('إذا كنت تأخذ موعد هذا اليوم، لذلك يرجى اختيار يوم آخر')</Script>");
    //                Label1.Text = "تم إصلاح موعدك.";
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
    //                upModal1.Update();

    //            }

    //            //Label1.Text = "Your appointment is fixed";
    //            //this.ModalPopupExtender2.Show();



    //        }
    //    }
    //    else
    //    {
    //        //Label1.Text = "Please use a valid hakkeemid";
    //        //this.ModalPopupExtender2.Show();
    //        if (Session["Language"].ToString() == "Auto")
    //        {
    //            Label1.Text = "Please use a valid user id";

    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
    //            upModal1.Update();
    //        }
    //        else
    //        {
    //            Label1.Text = "الرجاء استخدام معرف مستخدم صالح";

    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
    //            upModal1.Update();
    //        }
    //    }

    //}
    //catch (Exception ex)
    //{
    //    //Label1.Text = "Please use a valid hakkeemid";
    //    //this.ModalPopupExtender2.Show();
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        //RegisterStartupScript("", "<Script Language=JavaScript>swal('Please use a valid hakkeemid.')</Script>");
    //        Label1.Text = "Please use a valid user id";

    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
    //        upModal1.Update();
    //    }
    //    else
    //    {
    //        //RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى استخدام هاكيميد صالح')</Script>");
    //        Label1.Text = "الرجاء استخدام معرف مستخدم صالح";

    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
    //        upModal1.Update();
    //    }
    //}



    protected void TxtBookDocUserId_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    var qq = from itemqq in db.tbl_signups where itemqq.u_hakkimid == TxtBookDocUserId.Text select itemqq;
        //    if (qq.Count() > 0)
        //    {
        //        var Query = from item in db.tbl_hos_doc_appmnts where item.d_id == Session["did"].ToString() && item.a_date == TxtApntmtDate.Text && item.u_id == TxtBookDocUserId.Text select item;
        //        if (Query.Count() > 0)
        //        {
        //            //RegisterStartupScript("", "<Script Language=JavaScript>alert('Already you take an appointment this day, so you please choose another day.')</Script>");
        //            Label1.Text = "Already you take an appointment this day, so you please choose another day.";
        //            this.ModalPopupExtender2.Show();
        //        }
        //        else
        //        {
        //            tbl_hos_doc_appmnt ha = new tbl_hos_doc_appmnt()
        //            {
        //                d_id = Session["did"].ToString(),
        //                a_date = TxtApntmtDate.Text,
        //                u_id = TxtBookDocUserId.Text,
        //                a_status = 1,
        //                a_payment = DdlPayments.SelectedItem.Text,
        //                a_reason = TxtReasonToVisit.SelectedItem.Text,
        //                h_id = Session["hakkeemid_h"].ToString(),
        //                a_time = TxtApointmentTime.Text,
        //            };
        //            db.tbl_hos_doc_appmnts.InsertOnSubmit(ha);
        //            db.SubmitChanges();
        //            // Availability();
        //            string msg = "Dear patient, your appointment is fixed. Thank you" + "<br />" + " Hakkeem Team.";
        //            //  Email.mail(Session["user"].ToString(), msg, "Appointment fixed");

        //            ///  Response.Redirect("~/User/Finish appointment.aspx");
        //            //RegisterStartupScript("", "<Script Language=JavaScript>alert('Your appointment is fixed')</Script>");

        //            Label1.Text = "Your appointment is fixed";
        //            this.ModalPopupExtender2.Show();



        //        }
        //    }
        //    else
        //    {
        //        Label1.Text = "Please use a valid hakkeemid";
        //        this.ModalPopupExtender2.Show();
        //    }

        //}
        //catch (Exception ex)
        //{
        //    Label1.Text = "Please use a valid hakkeemid";
        //    this.ModalPopupExtender2.Show();
        //}
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        searchfiltr();
        // Response.Redirect("SetHospitalLocation.aspx");
    }
    protected void DdlDoctors_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DdlDoctors.SelectedIndex > 0)
        //{

       // Session["query"] = DdlDoctors.SelectedItem.Text;
        //cmd = new SqlCommand("select * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'" + Session["query"].ToString(), con);
        //SqlDataReader dr = cmd.ExecuteReader();
        //if (dr.HasRows)
        //{
            searchfiltr();
        //}
        //else
        //{
        //    if (Session["Language"].ToString() == "Auto")
        //    {
        //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctors or Availability not updated.')</Script>");
        //        // Response.Redirect("sethospitalLocation.aspx");
        //    }
        //    else
        //    {
        //        RegisterStartupScript("", "<Script Language=JavaScript>swal('لا الأطباء أفايلابيل.')</Script>");
        //        // Response.Redirect("sethospitalLocation.aspx?l=ar-EG");
        //    }
        //}
        //}
    }
    //protected void TextBox1_TextChanged(object sender, EventArgs e)
    //{
    //    //list_doctors(TextBox1.Text, "", "");

    //    cmd = new SqlCommand("select * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'" + Session["query"].ToString() , con);
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    if (dr.HasRows)
    //    {
    //        searchfiltr();
    //    }
    //    else
    //    {
    //        if (Session["Language"].ToString() == "Auto")
    //        {
    //            RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctors or Availability not updated.')</Script>");
    //            // Response.Redirect("sethospitalLocation.aspx");
    //        }
    //        else
    //        {
    //            RegisterStartupScript("", "<Script Language=JavaScript>swal('لا الأطباء أفايلابيل.')</Script>");
    //            // Response.Redirect("sethospitalLocation.aspx?l=ar-EG");
    //        }
    //    }
    //}

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        // searchfiltr();

    }

    protected void TextBox2_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (con.State.ToString() == "Closed")
        {
            con.Open();
        }
        string hosid = Session["hakkeemid_h"].ToString();
        string queries = "";


     if (HttpContext.Current.Session["query"] != null)
        { queries = Session["query"].ToString(); }
       
            
        if (hosid == null || hosid == ""   && queries == null || queries == "")
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('No Doctors Availability.')</Script>");
        }
        else
        {
            //cmd = new SqlCommand("select * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'" + Session["query"].ToString(), con);
            /////cmd = new SqlCommand("select * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "' and hd_name='"+DdlDoctors.SelectedItem.Text+ "' and  hd_specialties='"+TextBox2.SelectedItem.Text+"'" , con);
            cmd = new SqlCommand("select * from view_test_hos_doc where h_hakkimid='" + Session["hakkeemid_h"].ToString() + "'" + Session["query"].ToString(), con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                searchfiltr();
            }
            else
            {

                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Doctors Availability.')</Script>");

            }
        }
       
    }
}




//public void doctorDetails()
//{
//    DataTable dt = new DataTable();
//    dt.Columns.AddRange(new DataColumn[1] { new DataColumn("doctor") });
//    var Query = (from item in db.tbl_hos_doc_availables where item.h_id == Session["hakkeemid_h"].ToString() select item.hd_id).Distinct();
//    foreach (var ss in Query)
//    {
//        dt.Rows.Add(ss);
//    }
//    DataList1.DataSource = dt;
//    DataList1.DataBind();
//    foreach (DataListItem dl in DataList1.Items)
//    {
//        Label lbl1 = dl.FindControl("Label1") as Label; Label lbl2 = dl.FindControl("Label2") as Label;
//        var Query1 = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_email == lbl1.Text select item;
//        foreach (var ss in Query1)
//        { lbl2.Text = "Dr."+ss.hd_name; }
//        DataTable dt2 = new DataTable();
//        dt2.Columns.AddRange(new DataColumn[1] { new DataColumn("date") });
//        var Query2 = (from item in db.tbl_hos_doc_availables where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == lbl1.Text select item.date).Take(10);

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
//                    var Query4 = from item in db.tbl_hos_doc_appmnts where item.a_date == lbl4.Text && item.a_time == a[i].ToString() && item.d_id == lbl1.Text && item.h_id == Session["hakkeemid_h"].ToString() select item;
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
//    var selct = from a in db.tbl_hos_doc_appmnts where a.a_date == TxtApntmtDate.Text && a.d_id == Session["did"].ToString() && a.h_id == Session["hakkeemid_h"].ToString()&&a.u_id==TxtBookDocUserId.Text select a;
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
//                h_id = Session["hakkeemid_h"].ToString(),
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

//    var Query = from item in db.tbl_hos_doc_appmnts where item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == (DateTime.Now).ToString("yyyy-MM-dd") select item;
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
//                var Query1 = from item in db.tbl_hos_doc_appmnts where item.d_id == lbl4.Text && item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == (DateTime.Now).ToString("yyyy-MM-dd") select item;
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