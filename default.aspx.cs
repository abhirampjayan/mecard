using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hakkeem_Index : System.Web.UI.Page
{
    int t;
    SMS ob = new SMS();
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

    SqlCommand cmd;
    protected override void InitializeCulture()
    {
        Session["Speciality"] = "Auto";
        Session["Language"] = "Auto";
        string culture = "Auto";
        try
        {
            culture = Request.QueryString["l"].ToString();
            Session["Speciality"] = culture;
        }
        catch (Exception ex)
        { }
        // string culture = Session["Speciality"].ToString();
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
        else
        {
            //LinkButton1.Text = "عربى";
        }

        base.InitializeCulture();
    }
    protected void Page_Init(object sender, EventArgs e)
    {

        // Response.Redirect(Request.RawUrl);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["dne"].ToString() == "1")
            {
                Session["dne"] = "";
                if (Session["Speciality"].ToString() == "Auto")
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctors not exist')</Script>");
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('الطبيب غير موجود')</Script>");
                }
            }
        }
        catch (Exception ex) { }
        ////////////////////////////////
        try
        {
            if (Session["Speciality"].ToString() == "ar-EG")
            {
                //LinkButton1.Text = "الإنجليزية";
            }
        }
        catch (Exception ex)
        {

        }


        /////////////////////////////
        con.Open();
        //Response.Write(ViewState["a"].ToString());
        //ViewState["a"] = 2;

        if (!IsPostBack)
        {
            try
            {
                if (Session["hakkemid_u"].ToString() != "")
                {
                    cmd = new SqlCommand("select name from tbl_signup where u_hakkimid='" + Session["hakkemid_u"].ToString() + "'", con);
                    string name = cmd.ExecuteScalar().ToString();
                    Session["ggggg"] = "0";
                    HyperLink5.Text = "Hello, " + name.ToString();
                    HyperLink5.NavigateUrl = "../user/User account.aspx";
                    HyperLink6.Text = "Hello, " + name.ToString();
                    HyperLink6.NavigateUrl = "../user/User account.aspx?l=ar-EG";
                    //HyperLink2.Visible = false;
                    //HyperLink3.Visible = false;
                    //HyperLink4.Visible = false;
                }
            }
            catch (Exception ex)
            { }
            try
            {
                if (Session["hakkeemid_h"].ToString() != "")
                {
                    cmd = new SqlCommand("select h_name from tbl_hospitalreg where h_hakkimid='" + Session["hakkemid_h"].ToString() + "'", con);
                    string name = cmd.ExecuteScalar().ToString();


                    HyperLink5.Text = name.ToString();
                    HyperLink5.NavigateUrl = "../hospital/settings.aspx";
                    HyperLink6.Text = name.ToString();
                    HyperLink6.NavigateUrl = "../hospital/settings.aspx?l=ar-EG";
                    Session["ggggg"] = "0";
                    //HyperLink2.Visible = false;
                    //HyperLink3.Visible = false;
                    //HyperLink4.Visible = false;
                }
            }
            catch (Exception ex)
            { }
            try
            {
                if (Session["hakkeemid_d"].ToString() != "")
                {
                    cmd = new SqlCommand("select d_name from tbl_doctor where d_hakkimid='" + Session["hakkeemid_d"].ToString() + "'", con);
                    string name = cmd.ExecuteScalar().ToString();

                    Session["ggggg"] = "0";
                    HyperLink5.Text = "Hello, " + name.ToString();
                    HyperLink5.NavigateUrl = "../Doctor/Doctor profile.aspx";
                    HyperLink6.Text = "Hello, " + name.ToString();
                    HyperLink6.NavigateUrl = "../Doctor/Doctor profile.aspx?l=ar-EG";
                    //HyperLink2.Visible = false;
                    //HyperLink3.Visible = false;
                    //HyperLink4.Visible = false;
                }
            }
            catch (Exception ex)
            { }




            LoadSpecialities();
            city();
            try
            {
                if (Session["a"].ToString() == "1")
                {
                    ViewState["a"] = "2";
                    Session["a"] = "2";
                }
                else
                {
                    t = Convert.ToInt32(Session["a"]) + 1;
                    ViewState["a"] = t;

                    Session["a"] = t.ToString();
                    if (t == 3)
                    {
                        Session["a"] = 1;
                        ViewState["a"] = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewState["a"] = 1;
                Session["a"] = "1";
            }

        }


    }
    public void city()
    {
        SqlCommand comdropcity = new SqlCommand("select distinct(d_location)as lang from view_doc_available_time where d_status=1", con);
        SqlDataReader dtrdropcity = comdropcity.ExecuteReader();
        Speciality0.Items.Clear();
        if (dtrdropcity.HasRows)
        {
            while (dtrdropcity.Read())
            {if (dtrdropcity[0].ToString() == "")
                { }
            else
                {
                Speciality0.Items.Add(dtrdropcity[0].ToString());
                DropDownList1.Items.Add(dtrdropcity[0].ToString());
                }
            }
        }
        if (Session["Speciality"].ToString() == "Auto")
        {
            Speciality0.Items.Insert(0, "--Location--");
            DropDownList1.Items.Insert(0, "--Location--");
        }
        else
        {
            Speciality0.Items.Insert(0, "--موقعك--");
            DropDownList1.Items.Insert(0, "--موقعك--");
        }
    }
    public void LoadSpecialities()
    {


        SqlCommand comdropcity = new SqlCommand("select distinct(d_specialties)as lang from view_doc_available_time", con);
        SqlDataReader dtrdropcity = comdropcity.ExecuteReader();
        Speciality0.Items.Clear();
        if (dtrdropcity.HasRows)
        {
            while (dtrdropcity.Read())
            {
                if (dtrdropcity[0].ToString() == "")
                { }
                else
                {
                    Speciality.Items.Add(dtrdropcity[0].ToString());
                    DropDownList2.Items.Add(dtrdropcity[0].ToString());
                }
            }
        }
        
       

   
            if (Session["Speciality"].ToString() == "Auto")
            {
                Speciality.Items.Insert(0, "--Speciality--");
                DropDownList2.Items.Insert(0, "--Speciality--");
            }
            else
            {
                Speciality.Items.Insert(0, "--تخصص--");
                DropDownList2.Items.Insert(0, "--تخصص--");
            }
       

    }

    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        Session["q"] = "";


        cmd = new SqlCommand("select h_name from tbl_hospitalreg where h_name like '%" + txtSearch.Text + "'%", con);
        string hName = "";
        try
        {
            hName = cmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            hName = "";
        }
        if (hName.ToString() != "")
        {
            // Session["shname"] = hName.Text;
            Response.Redirect("../user/searchbyhospital.aspx?hos1=" + hName.ToString());
        }

        if (Session["Speciality"].ToString() == "Auto")

        {


            if (txtSearch1.Text != "" || DropDownList2.SelectedItem.Text != "--Speciality--" || DropDownList1.SelectedItem.Text != "--Location--")
            {




                //   Session["searchcommon"] = "1";

                string str = "", qq = "";


                if (txtSearch1.Text != "" && DropDownList1.SelectedItem.Text != "--Location--" && DropDownList2.SelectedItem.Text != "--Speciality--")
                {



                    str = "d_name like '%" + txtSearch1.Text + "%'   and  d_specialties ='" + DropDownList2.SelectedItem.Text + "' and (d_city like '%" + DropDownList1.SelectedItem.Text + "%' or d_location = '" + DropDownList1.SelectedItem.Text + "')";

                    qq = " where  " + str;
                }
                else if (txtSearch1.Text != "" && DropDownList1.SelectedItem.Text != "--Location--")
                {
                    str = "d_name like '%" + txtSearch1.Text + "%'  and (d_city like '%" + DropDownList1.SelectedItem.Text + "%' or d_location = '" + DropDownList1.SelectedItem.Text + "') ";

                    qq = " where  " + str;
                }
                else if (txtSearch1.Text != "" && DropDownList2.SelectedItem.Text != "--Speciality--")
                {
                    str = "d_name like '%" + txtSearch1.Text + "%' and  d_specialties like '%" + DropDownList2.SelectedItem.Text + "%' ";

                    qq = " where  " + str;
                }
                else if (DropDownList1.SelectedItem.Text != "--Location--" && DropDownList2.SelectedItem.Text != "--Speciality--")
                {
                    str = " d_location = '" + DropDownList1.SelectedItem.Text + "' and  d_specialties like '%" + DropDownList2.SelectedItem.Text + "%' ";

                    qq = " where  " + str;
                }
                else if (txtSearch1.Text != "")
                {
                    str = "d_name like '%" + txtSearch1.Text + "%' ";

                    qq = " where  " + str;
                }
                else if (DropDownList1.SelectedItem.Text != "--Location--")
                {
                    str = "(d_city like '%" + DropDownList1.SelectedItem.Text + "%' or d_location = '" + DropDownList1.SelectedItem.Text + "') ";

                    qq = " where  " + str;

                }
                else if (DropDownList2.SelectedItem.Text != "--Speciality--")
                {
                    str = " d_specialties = '" + DropDownList2.SelectedItem.Text + "'";


                    qq = " where  " + str;
                }
                else
                {
                    qq = "";
                }

                Session["q"] = qq;

                Session["pq"] = qq;



                Response.Redirect("~/User/Search.aspx");
            }
            else
            {


                Response.Redirect("~/User/Search.aspx");
            }
        }
        else
        {
            if (txtSearch.Text != "" || Speciality.SelectedItem.Text != "--تخصص--" || Speciality0.SelectedItem.Text != "--موقعك--")
            {




                //   Session["searchcommon"] = "1";

                string str = "", qq = "";


                if (txtSearch.Text != "" && Speciality0.SelectedItem.Text != "--موقعك--" && Speciality.SelectedItem.Text != "--تخصص--")
                {



                    str = "d_name like '%" + txtSearch.Text + "%'   and  d_specialties ='" + Speciality.SelectedItem.Text + "' and (d_city like '%" + Speciality0.SelectedItem.Text + "%' or d_location = '" + Speciality0.SelectedItem.Text + "')";

                    qq = " where  " + str;
                }
                else if (txtSearch.Text != "" && Speciality0.SelectedItem.Text != "--موقعك--")
                {
                    str = "d_name like '%" + txtSearch.Text + "%'  and (d_city like '%" + Speciality0.SelectedItem.Text + "%' or d_location = '" + Speciality0.SelectedItem.Text + "') ";

                    qq = " where  " + str;
                }
                else if (txtSearch.Text != "" && Speciality.SelectedItem.Text != "--تخصص--")
                {
                    str = "d_name like '%" + txtSearch.Text + "%' and  d_specialties like '%" + Speciality.SelectedItem.Text + "%' ";

                    qq = " where  " + str;
                }
                else if (Speciality0.SelectedItem.Text != "--موقعك--" && Speciality.SelectedItem.Text != "--تخصص--")
                {
                    str = " d_location = '" + Speciality0.SelectedItem.Text + "' and  d_specialties like '%" + Speciality.SelectedItem.Text + "%' ";

                    qq = " where  " + str;
                }
                else if (txtSearch.Text != "")
                {
                    str = "d_name like '%" + txtSearch.Text + "%' ";

                    qq = " where  " + str;
                }
                else if (Speciality0.SelectedItem.Text != "--موقعك--")
                {
                    str = "(d_city like '%" + Speciality0.SelectedItem.Text + "%' or d_location = '" + Speciality0.SelectedItem.Text + "') ";

                    qq = " where  " + str;

                }
                else if (Speciality.SelectedItem.Text != "--تخصص--")
                {
                    str = " d_specialties = '" + Speciality.SelectedItem.Text + "'";


                    qq = " where  " + str;
                }
                else
                {
                    qq = "";
                }

                Session["q"] = qq;

                Session["pq"] = qq;



                Response.Redirect("~/User/Search.aspx?l=ar-EG");
            }
            else
            {


                Response.Redirect("~/User/Search.aspx?l=ar-EG");
            }
        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {

    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        //Session["q"] = "";
        //Session["hodn"] = txtSearch.Text;
        //Session["speciality"] = Speciality.SelectedItem.Text;
        //Session["location"] = Speciality0.SelectedItem.Text;

        cmd = new SqlCommand("select d_hakkimid,d_id  from tbl_doctor where d_name='" + txtSearch.Text + "'", con);
        string d_id = "", d_hid = "", lat = "", longti = "";
        SqlDataReader dtr = cmd.ExecuteReader();
        while (dtr.Read())
        {
            d_id = dtr[1].ToString();
            d_hid = dtr[0].ToString();
        }

        cmd = new SqlCommand("select latitude,longitude  from tbl_doctor_location where d_id='" + d_id.ToString() + "'", con);

        SqlDataReader dtr1 = cmd.ExecuteReader();
        while (dtr1.Read())
        {
            longti = dtr1[1].ToString();
            lat = dtr1[0].ToString();
        }

        secure obj = new secure();
    //    Response.Redirect("../user/viewdoctorsreview.aspx?docid=" + obj.EnryptString(d_hid.ToString()) + "&Lat=" + lat + "&Long=" + longti);
        //if (hName.ToString() != "")
        //{
        //    // Session["shname"] = hName.Text;
        //    Response.Redirect("../user/searchbyhospital.aspx?hos1=" + hName.ToString());
        //}

        //if (txtSearch.Text != "" || Speciality.SelectedItem.Text != "--Speciality--" || Speciality0.SelectedItem.Text != "--Location--")
        //{




        //    Session["searchcommon"] = "1";











        //    Response.Redirect("~/User/SearchCommon.aspx");
        //}
        //else
        //{


        //    Response.Redirect("~/User/Search.aspx");
        //}
    }

    protected void submit_Click1(object sender, EventArgs e)
    {
        applink.Text = "";
    }

    protected void applink_TextChanged(object sender, EventArgs e)
    {
        //Response.Write("dfh hdfgk hdfkj ghdfkj ghjdf gkjdfg");
    }

    protected void hName_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Speciality_SelectedIndexChanged(object sender, EventArgs e)
    {

        

        SqlCommand comdropcity = new SqlCommand();
        comdropcity.Connection = con;
        if (Session["Speciality"].ToString() != "Auto")
        {


            if (Speciality.SelectedItem.Text == "--تخصص--")
            {
                comdropcity.CommandText = "select distinct(d_location)as lang from view_doc_available_time where d_status=1 ";
            }
            else
            {
                comdropcity.CommandText = "select distinct(d_location)as lang from view_doc_available_time where d_status=1 and d_specialties='" + Speciality.SelectedItem.Text + "'";
            }
        }
        else
        { if (DropDownList2.SelectedItem.Text == "--Speciality--")
            {
                comdropcity.CommandText = "select distinct(d_location)as lang from view_doc_available_time where d_status=1 ";
            }
            else
            {
                comdropcity.CommandText = "select distinct(d_location)as lang from view_doc_available_time where d_status=1 and d_specialties='" + DropDownList2.SelectedItem.Text + "'";
            }
        }
        SqlDataReader dtrdropcity = comdropcity.ExecuteReader();
        Speciality0.Items.Clear();
        DropDownList1.Items.Clear();
        if (dtrdropcity.HasRows)
        {
            while (dtrdropcity.Read())
            {
                if (dtrdropcity[0].ToString() == "")
                { }
                else
                {
                    Speciality0.Items.Add(dtrdropcity[0].ToString());
                    DropDownList1.Items.Add(dtrdropcity[0].ToString());
                }
            }
        }

        if (Session["Speciality"].ToString() == "Auto")
        {
            Speciality0.Items.Insert(0, "--Location--");
            DropDownList1.Items.Insert(0, "--Location--");
        }
        else
        {
            Speciality0.Items.Insert(0, "--موقعك--");
            DropDownList1.Items.Insert(0, "--موقعك--");
        }
    }

    protected void Speciality0_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SqlCommand comdropcity = new SqlCommand();
        //comdropcity.Connection = con;
        //if (Session["Speciality"].ToString() != "Auto")
        //{


        //    if (Speciality0.SelectedItem.Text == "--موقعك--")
        //    {
        //        comdropcity.CommandText = "select distinct(d_specialties)as lang from view_doc_available_time where d_status=1 ";
        //    }
        //    else
        //    {
        //        comdropcity.CommandText = "select distinct(d_specialties)as lang from view_doc_available_time where d_status=1 and d_location='" + Speciality0.SelectedItem.Text + "'";
        //    }
        //}
        //else
        //{
        //    if (DropDownList1.SelectedItem.Text == "--Location--")
        //    {
        //        comdropcity.CommandText = "select distinct(d_specialties)as lang from view_doc_available_time where d_status=1 ";
        //    }
        //    else
        //    {
        //        comdropcity.CommandText = "select distinct(d_specialties)as lang from view_doc_available_time where d_status=1 and d_location='" + DropDownList1.SelectedItem.Text + "'";
        //    }
        //}
        //SqlDataReader dtrdropcity = comdropcity.ExecuteReader();
        //Speciality.Items.Clear();
        //DropDownList2.Items.Clear();
        //if (dtrdropcity.HasRows)
        //{
        //    while (dtrdropcity.Read())
        //    {
        //        if (dtrdropcity[0].ToString() == "")
        //        { }
        //        else
        //        {
        //            Speciality.Items.Add(dtrdropcity[0].ToString());
        //            DropDownList2.Items.Add(dtrdropcity[0].ToString());
        //        }
        //    }
        //}

        //if (Session["Speciality"].ToString() == "Auto")
        //{
        //    Speciality.Items.Insert(0, "--Speciality--");
        //    DropDownList2.Items.Insert(0, "--Speciality--");
        //}
        //else
        //{
        //    Speciality.Items.Insert(0, "--تخصص--");
        //    DropDownList2.Items.Insert(0, "--تخصص--");
        //}

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        //////////////////////


        try
        {
            //CancellSession();
            if (Session["Speciality"].ToString() == "ar-EG")
            {
                Session["Speciality"] = "Auto";
                Response.Redirect("default.aspx");
            }
            else
            {
                Session["Speciality"] = "ar-EG";

                Response.Redirect("default.aspx?l=ar-EG");
            }
        }
        catch (Exception ex)
        {

            //Response.Redirect("index.aspx");

        }

        Session["Speciality"] = "ar-EG";

        Response.Redirect("default.aspx?l=ar-EG");

        /////////////////////////

    }
    public void CancellSession()
    {
        Session["Speciality"] = "";
        Session.Abandon();
    }

    protected void submit123_Click(object sender, EventArgs e)
    {
        //ob.Message("+966" + applink.Text, "Hakkeem app download link");
        //ob.Message("+919539395281", "Hakkeem app download link");
        ob.Message("+966" + applink.Text, "Applications under construction Coming Soon");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //Response.Redirect("index.aspx?l=ar-EG");

        try
        {
            //CancellSession();
            if (Session["Speciality"].ToString() == "ar-EG")
            {
                Session["Speciality"] = "Auto";
                //Response.Redirect("index.aspx");
                Response.Redirect("default.aspx?l=ar-EG");
            }
            else
            {
                Session["Speciality"] = "ar-EG";

                Response.Redirect("default.aspx?l=ar-EG");
            }
        }
        catch (Exception ex)
        {

            //Response.Redirect("index.aspx");

        }

        //Session["Speciality"] = "ar-EG";

        //Response.Redirect("index.aspx?l=ar-EG");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            //CancellSession();
            if (Session["Speciality"].ToString() == "ar-EG")
            {
                Session["Speciality"] = "Auto";
                Response.Redirect("default.aspx?l=Auto");
            }
            else
            {
                Session["Speciality"] = "Auto";

                Response.Redirect("default.aspx?l=ar-EG");
            }
        }
        catch (Exception ex)
        {

            //Response.Redirect("index.aspx");

        }

        //Session["Speciality"] = "Auto";

        //Response.Redirect("index.aspx?l=Auto");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string name = "";
        if (Session["Speciality"].ToString() == "Auto")
        {
            name = TextBox4.Text;
        }
        else
        {
            name = TextBox1.Text;

        }
        string dt = System.DateTime.Now.ToString();

        SqlCommand com1 = new SqlCommand("select id from tbl_newsletter where email='" + name.ToString() + "'", con);
        int tt=0;
        try

        { tt = Convert.ToInt32(com1.ExecuteScalar()); }
        catch (Exception ex)
        {
            tt = 0;
        }
        if (Session["Speciality"].ToString() == "Auto")
        {
            if (tt == 0)
            {
                SqlCommand com = new SqlCommand("insert into tbl_newsletter values('" + name.ToString() + "','" + dt.ToString() + "')", con);
                int id = Convert.ToInt32(com.ExecuteNonQuery());
                if (id != 0)
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Thank you for subscription')</Script>");
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Error')</Script>");
                }
            }
            else
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You are already subscribed')</Script>");
            }
        }
        else
        {
            if (tt == 0)
            {
                SqlCommand com = new SqlCommand("insert into tbl_newsletter values('" + name.ToString() + "','" + dt.ToString() + "')", con);
                int id = Convert.ToInt32(com.ExecuteNonQuery());
                if (id != 0)
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('شكرا لك على الاشتراك')</Script>");
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('خطأ')</Script>");
                }
                
            }
            else
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('أنك بالفعل مشترك')</Script>");
            }
        }
    }

    protected void Button3_Click1(object sender, EventArgs e)
    {
     //   RegisterStartupScript("", "<Script Language=JavaScript>swal('You are already registered')</Script>");
    }
}