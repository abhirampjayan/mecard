using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hospital_ArabichospitalMaster : System.Web.UI.MasterPage
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


        //if (Session["hakkeemid_d"] == null)
        //{
        //    //Label4.Visible = false;
        //    Response.Redirect("~/Index/Doctor login.aspx");
        //    LinkButton1.Text = "SignIn";
        //}
        //else
        //{
        //    LinkButton1.Text = "SignOut";

        //}
        //Timer t = (Timer)Master.FindControl("Timer1");
        //t.Enabled = false;
        if (Session["hakkeemid_h"] != null)
        {

        }
        else
        {
            if (Session["Language"].ToString() == "Auto")
            {
                Response.Redirect("~/Index/Hospita Login.aspx");
            }
            else
            {
                Response.Redirect("~/Index/Hospita Login.aspx?l=ar-EG");
            }
        }
        if (!IsPostBack)
        {
            var query = from item in db.tbl_hos_locations
                        join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                        where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                        select new { item1.h_id, item.latitude };
            //try
            //{
            if (query.Count() <= 0)
            {
                LnkAgrmntUpload.Enabled = false;
                // Label7.Enabled = false;
                LinkButton1.Enabled = false;
                LinkButton2.Enabled = false;
                LinkButton4.Enabled = false;
                LinkButton5.Enabled = false;
                LinkButton6.Enabled = false;
                LinkButton7.Enabled = false;
                LinkButton8.Enabled = false;
                LinkButton10.Enabled = false;
                LinkButton11.Enabled = false;


            }
            Hospital();
            ApointmentsCount();
            DeletePassedDate();
        }
    }

    public void Hospital()
    {
        date.Text = DateTime.Now.ToShortDateString();
        var Query = from item in db.tbl_hospitalregs where item.h_hakkimid == Session["hakkeemid_h"].ToString() select item;
        foreach (var ss in Query)
        {
            Label2.Text = ss.h_name;
            Label3.Text = ss.h_name;

            firstimage.ImageUrl = ss.h_photo;

            if (ss.h_agreement == null)
            {
                LnkAgrmntUpload.Visible = true;
            }
            else
            {
                LnkAgrmntUpload.Visible = false;
            }
        }
    }

    public void DeletePassedDate()
    {
        var select = from item in db.tbl_hos_doc_availables
                     select item;
        if (select.Count() > 0)
        {
            foreach (var s in select)
            {
                DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime dt2 = DateTime.Parse(s.date);

                if (dt2 < dt1)
                {
                    var Query = from a in db.tbl_hos_doc_availables
                                where a.id == s.id
                                select a;
                    db.tbl_hos_doc_availables.DeleteAllOnSubmit(Query);
                    db.SubmitChanges();
                }
            }
        }

        var selectApointmnts = from tt in db.tbl_hos_doc_appmnts
                               select tt;
        if (selectApointmnts.Count() > 0)
        {
            foreach (var ss in selectApointmnts)
            {
                DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime dt2 = DateTime.Parse(ss.a_date);
                if (dt2 < dt1)
                {
                    var Query1 = from item in db.tbl_hos_doc_appmnts where item.id == ss.id select item;
                    foreach (var dd in Query1)
                    { db.tbl_hos_doc_appmnts.DeleteOnSubmit(dd); }
                    db.SubmitChanges();
                }
            }
        }


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
                LblApointmentsCount.Text = query.Count().ToString();
            }
            else
            {
                LblApointmentsCount.Text = "0";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        DeletePassedDate();
        ApointmentsCount();
    }
    protected void LnkSignOut_Click(object sender, EventArgs e)
    {
        Session["hakkeemid_h"] = null;
        Session["ggggg"] = "1";
        if (Session["Language"].ToString() == "Auto")
        {
            Response.Redirect("~/index/hospita login.aspx");
        }
        else
        {
            Response.Redirect("~/index/hospita login?l=ar-EG");
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {
            //if (LinkButton3.Text == "عربى")
            //{
                //LinkButton3.Text = "الإنجليزية";
            //    Session["Language"] = "ar-EG";
            //    Response.Redirect(Request.Path + "?l=ar-EG");

            //}
            //else
            //{
                Session["Language"] = "Auto";
                //LinkButton3.Text = "عربى";
                Response.Redirect(Request.Path);

            //}
        }
        catch (Exception ex)
        {
            LinkButton3.Text = "الإنجليزية";

        }

    }
}
