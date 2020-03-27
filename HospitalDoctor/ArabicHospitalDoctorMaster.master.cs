using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Hospital_HospitalDoctorMaster : System.Web.UI.MasterPage
{
    databaseDataContext db = new databaseDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Language"].ToString() == "Auto" || Session["Language"].ToString() == "")
        {
            LinkButton3.Text = "English";
        }
        else
        {
            LinkButton3.Text = "English";

        }
        if (Session["HosDocId"] != null)
        {
            HospitalDetails();
            DoctorDetails();
            DeletePassedAppointments();
            if (!IsPostBack)
            {
                AppointmentsCount();
            }
        }
        else
        {
            Response.Redirect("~/Index/HospitalDoctorLogin.aspx");
        }
    }

    public void HospitalDetails()
    {
        var Query = from item in db.tbl_hospitalregs
                    where item.h_hakkimid == Session["HospitalId"].ToString()
                    select item;
        foreach (var ss in Query)
        {
            Label3.Text = ss.h_name;
        }
    }

    public void DoctorDetails()
    {
        var select = from a in db.tbl_hdoctors
                     where a.h_id == Session["HospitalId"].ToString() && a.hd_email == Session["HosDocId"].ToString()
                     select a;
        foreach (var ss in select)
        {
            LblDoctorName.Text = "Dr. " + ss.hd_name;
            if (ss.hd_photo == null)
            {
                Image1.ImageUrl = "/Clinic/User/mapicons/user .png";
            }
            else
            {
                Image1.ImageUrl = ss.hd_photo;
            }

        }

    }

    public void AppointmentsCount()
    {
        var Query = from item in db.tbl_hos_doc_appmnts
                    where item.d_id == Session["HosDocId"].ToString() && item.h_id == Session["HospitalId"].ToString() && item.a_status == 1
                    select item;
        if (Query.Count() > 0)
        {
            LblAppointmentsCount.Text = Query.Count().ToString();
        }
        else
        {
            LblAppointmentsCount.Text = "0";
        }

    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        AppointmentsCount();
        DeletePassedAppointments();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["HosDocId"] = null;
        Session["ggggg"] = "1";
        Response.Redirect("~/Index/HospitalDoctorLogin.aspx");
    }

    public void DeletePassedAppointments()
    {
        try
        {
            string currentTime = DateTime.Now.AddHours(-1).ToShortTimeString();
            var query = from item in db.tbl_hos_doc_appmnts
                        where item.h_id == Session["HospitalId"].ToString() && item.d_id == Session["HosDocId"].ToString() && item.a_date == DateTime.Now.ToString("yyyy-MM-dd")
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    string a_time = DateTime.Parse(ss.a_time).ToShortTimeString();
                    DateTime curnTime = DateTime.Parse(currentTime);
                    if (DateTime.Parse(a_time) <= curnTime)
                    {
                        var query1 = from item in db.tbl_hos_doc_appmnts
                                     where item.id == ss.id
                                     select item;
                        db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(query1);
                    }
                    db.SubmitChanges();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {
            if (LinkButton3.Text == "عربى")
            {
                LinkButton3.Text = "الإنجليزية";
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

        //Session["Language"] = "Auto";

        //Response.Redirect(Request.Path);
    }
}
