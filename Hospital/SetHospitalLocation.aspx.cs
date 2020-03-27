using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Hospital_SetHospitalLocation : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();


    protected override void InitializeCulture()
    {
        //Session["Language"] = "";
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
        if (Session["hakkeemid_h"] == null)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/Hospita Login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/Hospita Login.aspx?l=ar-EG");
            //}
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
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location.')</Script>");
                    // Response.Redirect("sethospitalLocation.aspx");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب تعيين موقعك')</Script>");
                //    // Response.Redirect("sethospitalLocation.aspx?l=ar-EG");
                //}
            }

            GetLocation();
        }
    }

    public void GetLocation()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("Latitude"), new DataColumn("Longitude"), new DataColumn("h_address"), new DataColumn("h_photo"), new DataColumn("id") });
        var query = from item in db.tbl_hos_locations join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                    select new { item1.h_name, item.latitude, item.longitude, item1.h_address, item1.h_photo, item1.h_id };
        if (query.Count() > 0)
        {
            foreach (var ss in query)
            {
                dt.Rows.Add(ss.h_name, ss.latitude, ss.longitude, ss.h_address, ss.h_photo, ss.h_id);
            }
            rptMarkers.DataSource = dt;
            rptMarkers.DataBind();
            BtnChangeLocation.Visible = true;
        }
        else
        {
            dt.Rows.Add("", "24.774265", "46.738586","Riyadh,KSA", "", "");
            rptMarkers.DataSource = dt;
            rptMarkers.DataBind();
            BtnSetLocation.Visible = true;
        }
    }



    protected void BtnChangeLocation_Click(object sender, EventArgs e)
    {
        try
        {
            var query = from item in db.tbl_hospitalregs
                        where item.h_hakkimid == Session["hakkeemid_h"].ToString()
                        select item;
            foreach (var ss in query)
            {
                ss.h_address = HosAddress.Value;
                var query1 = from item in db.tbl_hos_locations
                             where item.h_id == ss.h_id
                             select item;
                foreach (var s in query1)
                {
                    s.h_id = ss.h_id;
                    decimal LatValue = Convert.ToDecimal(Lat.Value);
                    decimal LongValue = Convert.ToDecimal(Lng.Value);

                    s.latitude = Convert.ToDecimal(Math.Round(LatValue, 7));
                    s.longitude = Convert.ToDecimal(Math.Round(LongValue, 7));

                }
                db.SubmitChanges();
            }
            //     ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Thank you.. your location changed succesfully..');window.location='Hospital.aspx';</script>");
            //if (Session["Language"].ToString() == "Auto")
            //{
                Label7.Text = "Thank you.. your location changed succesfully..";
            //}
            //else
            //{
            //    Label7.Text = "شكرا لك .. تم تغيير موقعك بنجاح ";
            //}
            //this.ModalPopupExtender4.Show();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
            //RegisterStartupScript("", "<Script Language=JavaScript>swal('Thank you.. your location changed succesfully..')</Script>");

        }
        catch (Exception ex)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal('Please click on the marker for confirmation..')</script>");
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "نجاح", "<script type='text/javascript'>swal('يرجى النقر على علامة التأكيد.')</script>");

            //}
        }
    }
    protected void BtnSetLocation_Click(object sender, EventArgs e)
    {
        try
        {
            var query = from item in db.tbl_hospitalregs
                        where item.h_hakkimid == Session["hakkeemid_h"].ToString()
                        select item;
            foreach (var ss in query)
            {

                var queryy = from itemm in db.tbl_hos_locations
                            where itemm.h_id == ss.h_id
                            select itemm;

                if (queryy.Count() > 0)
                {

                }
                else
                {
                    ss.h_address = HosAddress.Value;

                    decimal LatValue = Convert.ToDecimal(Lat.Value);
                    decimal LongValue = Convert.ToDecimal(Lng.Value);

                    tbl_hos_location td = new tbl_hos_location()
                    {
                        h_id = ss.h_id,
                        latitude = Convert.ToDecimal(Math.Round(LatValue, 7)),
                        longitude = Convert.ToDecimal(Math.Round(LongValue, 7)),
                    };
                    db.tbl_hos_locations.InsertOnSubmit(td);
                    db.SubmitChanges();
                }
            }
            //   ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Thank you.. your location is set succesfully..');window.location='Hospital.aspx';</script>");
            //if (Session["Language"].ToString() == "Auto")
            //{
                Label7.Text = "Thank you.. your location added succesfully..";
            //}
            //else
            //{
            //    Label7.Text = "شكرا لك .. تم تغيير موقعك بنجاح ";
            //}
            //this.ModalPopupExtender4.Show();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();

            //RegisterStartupScript("", "<Script Language=JavaScript>swal('Thank you.. your location changed succesfully..')</Script>");

        }
        catch (Exception ex)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal('Please click on the marker for confirmation..')</script>");
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "نجاح", "<script type='text/javascript'>swal('يرجى النقر على علامة التأكيد.')</script>");

            //}
        }
    }

    //protected void Button4_Click(object sender, EventArgs e)
    //{
        
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("hospital.aspx");
        //}
        //else
        //{
        //    Response.Redirect("hospital.aspx?l=ar-EG");
        //}
    }
}