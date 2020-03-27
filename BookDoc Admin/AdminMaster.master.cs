using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_AdminMaster : System.Web.UI.MasterPage
{

    databaseDataContext db = new databaseDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            if (Session["aduser"].ToString() == "")
           {
                Response.Redirect("index.aspx");
           }
        }
        catch (Exception ex)
        {
            Response.Redirect("index.aspx");
        }
        //if (Session["Language"].ToString() == "Auto" || Session["Language"].ToString() == "")
        //{
        //    LinkButton3.Text = "عربى";
        //}
        //else
        //{
        //    LinkButton3.Text = "الإنجليزية";

        //}
        if (!IsPostBack)
        {
            Expire();
            HosReqstNo();
            DoctorReqstNo();
        }
    }

    #region Hospital and Doctor reqstCount

    public void HosReqstNo()
    {
        var query = from item in db.tbl_hospitalregs
                    where item.h_status == 0
                    select item;
        if (query.Count() > 0)
        {
            LblHosReqstNo.Text = query.Count().ToString();
        }
        else
        {
            LblHosReqstNo.Text = "0";
        }
    }
    public void DoctorReqstNo()
    {
        var query = from item in db.tbl_doctors
                    where item.d_status == 0
                    select item;
        if (query.Count() > 0)
        {
            LblDoctorReqstNo.Text = query.Count().ToString();
        }
        else
        {
            LblDoctorReqstNo.Text = "0";
        }
    } 
    #endregion

    public void Expire()
    {
        var Query = from item in db.tbl_doctors where item.d_status == 1 select item;
        foreach(var ss in Query)
        {
            //DateTime dt = DateTime.Parse(ss.d_id_expire);

        }
    }




    protected void Timer1_Tick(object sender, EventArgs e)
    {
        HosReqstNo();
        DoctorReqstNo();
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
          //  LinkButton3.Text = "الإنجليزية";

        }

        //Session["Language"] = "Auto";

        //Response.Redirect(Request.Path);
    }
}
