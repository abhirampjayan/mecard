using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctor_ArabicMasterPage : System.Web.UI.MasterPage
{
    databaseDataContext db = new databaseDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Language"].ToString() == "Auto" || Session["Language"].ToString() == "")
        {
            LinkButton3.Text = "عربى";
        }
        else
        {
            LinkButton3.Text = "English";

        }
        try
        {
            if (Session["doctor"].ToString() != null)
            {

            }
            else
            {
                Response.Redirect("../index/Doctor login.aspx?l=ar-EG");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../index/Doctor login.aspx?l=ar-EG");
        }

        if (Session["hakkeemid_d"] == null)
        {
            //Label4.Visible = false;
            Response.Redirect("~/Index/Doctor login.aspx");
            if (Session["Language"].ToString() == "Auto")
            {
                LinkButton1.Text = "SignIn";
            }
            else
            {
                LinkButton1.Text = "تسجيل الدخول";
            }
        }
        else
        {
            if (Session["Language"].ToString() == "Auto")
            {
                LinkButton1.Text = "SignOut";
            }
            else
            {
                LinkButton1.Text = "خروج";
            }

        }

        if (!IsPostBack)
        {
            data();
            AppointmentsCount();
            AllAppointmentsCount();
        }
        data();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        AppointmentsCount();
        AllAppointmentsCount();
    }
    public void data()
    {
        var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
        foreach (var ss in Query)
        {
            Label1.Text = "Dr." + ss.d_name;

            if (ss.d_photo == null)
            {
                Image1.ImageUrl = "~/Doctorimages/doctor.png";

            }
            else
            {
                Image1.ImageUrl = ss.d_photo;

            }

            if (ss.d_agreement == null)
            {
                //  LnkAgrmntUpload.Visible = true;
                //   LnkAgrmntUpload.Text = "Upload agreement";
                LnkAgrmntUpload.Visible = false;

            }
            else
            {
                LnkAgrmntUpload.Visible = false;
            }
        }

    }

    public void AppointmentsCount()
    {
        var query = from item in db.tbl_doctor_appointments
                    where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == DateTime.Now.ToString("yyyy-MM-dd") && item.a_status == 1
                    orderby item.a_time ascending
                    select item;
        if (query.Count() > 0)
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
                    where item.d_id == Session["hakkeemid_d"].ToString() && item.a_status == 1
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
            if (LinkButton3.Text == "عربى")
            {
                LinkButton3.Text = "English";
                Session["Language"] = "ar-EG";
                Response.Redirect(Request.Path + "?l=ar-EG");

            }
            else
            {
                Session["Language"] = "Auto";
                LinkButton3.Text = "عربى";
                Response.Redirect(Request.Path);

            }
        }
        catch (Exception ex)
        {
            LinkButton3.Text = "English";

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (LinkButton1.Text == "SignIn")
        {
            if (Session["Language"].ToString() == "Auto")
            {
                Response.Redirect("~/Index/Doctor login.aspx");
            }
            else
            {
                Response.Redirect("~/Index/Doctor login.aspx?l=ar-EG");
            }
        }
        else
        {
            Session["hakkeemid_d"] = null;
            Session["ggggg"] = "1";
            if (Session["Language"].ToString() == "Auto")
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Response.Redirect("~/default.aspx?l=ar-EG");
            }
        }
    }
}
