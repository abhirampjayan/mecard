using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctor_DoctorMasterPage : System.Web.UI.MasterPage
{

    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());

    protected void Page_Init(object sender, EventArgs e)
    {
       //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
       // trigger.EventName = "Click";
       // trigger.ControlID = Button1.UniqueID.ToString();
       // upModal1.Triggers.Add(trigger);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();
        try
        {
            if (Session["doctor"].ToString() != null)
            {
               
            }
            else
            {
                Response.Redirect("../index/Doctor login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../index/Doctor login.aspx");
        }
        //if (Session["Language"].ToString() == "Auto" || Session["Language"].ToString()=="")
        //{
        //     LinkButton3.Text = "عربى";
        //}
        //else
        //{
        //    LinkButton3.Text = "English";

        //    }
    

        if(Session["hakkeemid_d"]==null)
        {
            //Label4.Visible = false;
            Response.Redirect("~/Index/Doctor login.aspx");
            LinkButton1.Text = "SignIn";
        }
        else
        {
            LinkButton1.Text = "SignOut";
            
        }

        if(!IsPostBack)
        {
            data();
            AppointmentsCount();
            AllAppointmentsCount();
        }
       // data();
    }

   
    public void data()
    {
        var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
        foreach(var ss in Query)
        {
            Label1.Text = "Dr."+ss.d_name;
           
            if (ss.d_photo == null)
            {
                Image1.ImageUrl = "../Doctorimages/doctor.png";
               
            }
            else
            {
                Image1.ImageUrl = ss.d_photo;
               
            }
           
            if (ss.d_agreement==null)
            {
                //  LnkAgrmntUpload.Visible = true;
                // LnkAgrmntUpload.Text = "Upload agreement";


                LnkAgrmntUpload.Visible = false;


            }
            else
            {
                LnkAgrmntUpload.Visible = false;
            }
        }

    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if(LinkButton1.Text=="SignIn")
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/Doctor login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/Doctor login.aspx?l=ar-EG");
            //}
        }
        else
        {
            Session["doctor"] = "";
            Session["hakkeemid_d"] = null;
            Session["ggggg"] = "1";
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/default.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/default.aspx?l=ar-EG");
            //}
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        AppointmentsCount();
        AllAppointmentsCount();
       // data();



    }

    public void AppointmentsCount()
    {
        var query = from item in db.tbl_doctor_appointments
                    where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == DateTime.Now.ToString("yyyy-MM-dd")&&item.a_status==1
                    orderby item.a_time ascending
                    select item;
        if(query.Count() >0)
        {
            LblAppointmentsCount.Text = query.Count().ToString();
        }
        else
        {
            LblAppointmentsCount.Text = "0";
        }
                   
    }

    public void AllAppointmentsCount()
    {
        var query = from item in db.tbl_doctor_appointments
                    where item.d_id == Session["hakkeemid_d"].ToString()&&item.a_status==1
                    orderby item.a_time ascending
                    select item;
        if (query.Count() > 0)
        {
            LblAllApntCount.Text = query.Count().ToString();
        }
        else
        {
            LblAllApntCount.Text = "0";
        }

    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        try
        {
            //if (LinkButton3.Text == "عربى")
            //{
            //    LinkButton3.Text = "الإنجليزية";
            //    Session["Language"] = "ar-EG";
            //    Response.Redirect(Request.Path + "?l=ar-EG");

            //}
            //else
            //{
            //    Session["Language"] = "Auto";
            //    LinkButton3.Text = "عربى";
            //    Response.Redirect(Request.Path);

            //}
        }
        catch (Exception ex)
        {
           // LinkButton3.Text = "الإنجليزية";

        }

        //Session["Language"] = "Auto";

        //Response.Redirect(Request.Path);
    }




    protected void LinkButton2_Click(object sender, EventArgs e)
    {//
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
        upModal1.Update();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
        upModal1.Update();
    }
    private static DateTime ConvertToEngCal(string hijri)
    {
        CultureInfo arSA = new CultureInfo("ar-SA");
        arSA.DateTimeFormat.Calendar = new HijriCalendar();
        return DateTime.ParseExact(hijri, "dd/MM/yy", arSA);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        //string dd = System.DateTime.Now.ToString();
        //string[] dd1 = new string[7];

        //dd1 = dd.Split('-');
        //string[] dd2 = new string[7];
        //dd2 = dd1[2].Split(' ');

        //    Response.Write("<br>" + dd2[0]);
        string dttime = System.DateTime.Now.ToString();

        //if (dd2[0] == "2018" || dd2[0] == "2019" || dd2[0] == "2020")
        //{

        //}
        //else
        //{
        //    dttime = ConvertToEngCal(dttime).ToString("dd-MM-yyyy");
        //}







        SqlCommand com = new SqlCommand("insert into tbl_msg values('" + TextBox1.Text + "','" + Session["hakkeemid_d"].ToString() + "','" + dttime.ToString() + "')", con);
        int id = Convert.ToInt32(com.ExecuteNonQuery());
        Label19.Text = "Message send to Hakkeem Admin";
    }
   
}
