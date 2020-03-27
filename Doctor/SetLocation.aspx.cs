using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Doctor_SetLocation : System.Web.UI.Page
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
        //    this.MasterPageFile = "~/Doctor/ArabicMasterPage.master";
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["hakkeemid_d"] == null)
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
        if(!IsPostBack)
        {
            GetLocation();
        }
    }

    public void GetLocation()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("Latitude"), new DataColumn("Longitude"), new DataColumn("d_specialties"), new DataColumn("d_address"), new DataColumn("d_photo"), new DataColumn("d_id"), new DataColumn("id") });
        var query = from item in db.tbl_doctor_locations
                    join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                    where item1.d_hakkimid== Session["hakkeemid_d"].ToString()
                    select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
        if (query.Count() > 0)
        {
            foreach (var ss in query)
            {
                dt.Rows.Add(ss.d_name, ss.latitude, ss.longitude, ss.d_specialties, ss.d_address, ss.d_photo, ss.d_email, ss.d_id);
            }
            rptMarkers.DataSource = dt;
            rptMarkers.DataBind();
            BtnChangeLocation.Visible = true;
        }
        else
        {
            dt.Rows.Add("","24.774265","46.738586","","Riyadh,KSA","","");
            rptMarkers.DataSource = dt;
            rptMarkers.DataBind();
            BtnSetLocation.Visible = true;
            
        }
    }

    protected void BtnSetLocation_Click(object sender, EventArgs e)
    {
        try
        {
            var query = from item in db.tbl_doctors
                        where item.d_hakkimid == Session["hakkeemid_d"].ToString()
                        select item;
            foreach (var ss in query)
            {
                ss.d_address = DocAddress.Value;
                decimal LatValue = Convert.ToDecimal(Lat.Value);
                decimal LongValue = Convert.ToDecimal(Lng.Value);



                tbl_doctor_location td = new tbl_doctor_location()
                {
                    d_id = ss.d_id,
                    latitude = Convert.ToDecimal(Math.Round(LatValue, 7)),
                    longitude = Convert.ToDecimal(Math.Round(LongValue, 7)),
                };
                db.tbl_doctor_locations.InsertOnSubmit(td);
                db.SubmitChanges();

                //string[] city = DocAddress.Value.Split(',');
                //ss.d_location = city[0] /*+ "," + city[1]*/;
                //db.SubmitChanges();

            }
            //if (Session["Language"].ToString() == "Auto")
            //{
                lblModalBody.Text = "Thank you.. your location set succesfully..";
            //}
            //else
            //{
            //    lblModalBody.Text = "شكرا لك .. تم تغيير موقعك بنجاح ..";
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
        catch (Exception ex)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                lblModalBody.Text = "Please Select the Location..";
            //}
            //else
            //{
            //    lblModalBody.Text = "الرجاء تحديد الموقع ..";
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();

        }//ClientScript.RegisterStartupScript(this.GetType(),"Success","<script type='text/javascript'>swal('Thank you.. your location is set succesfully..')</script>");
        //Label7.Text = "Thank you.. your location is set succesfully..";
        //this.ModalPopupExtender4.Show();
    }
      
    
    protected void BtnChangeLocation_Click(object sender, EventArgs e)
    {
        try
        {
            var query = from item in db.tbl_doctors
                        where item.d_hakkimid == Session["hakkeemid_d"].ToString()
                        select item;
            foreach (var ss in query)
            {
                ss.d_address = DocAddress.Value;
                var query1 = from item in db.tbl_doctor_locations
                             where item.d_id == ss.d_id
                             select item;
                foreach (var s in query1)
                {
                    s.d_id = ss.d_id;
                    decimal LatValue = Convert.ToDecimal(Lat.Value);
                    decimal LongValue = Convert.ToDecimal(Lng.Value);

                    s.latitude = Convert.ToDecimal(Math.Round(LatValue, 7));
                    s.longitude = Convert.ToDecimal(Math.Round(LongValue, 7));

                }
                db.SubmitChanges();

                //string[] city = DocAddress.Value.Split(',');
                //ss.d_location = city[0] /*+ "," + city[1]*/;
                //db.SubmitChanges();
            }
            //if (Session["Language"].ToString() == "Auto")
            //{
                lblModalBody.Text = "Thank you.. your location changed succesfully..";
            //}
            //else
            //{
            //    lblModalBody.Text = "شكرا لك .. تم تغيير موقعك بنجاح ..";
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
            //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>swal('Thank you.. your location changed succesfully..')</script>");
            //Label7.Text = "Thank you.. your location changed succesfully..";
            //this.ModalPopupExtender4.Show();
        }
        catch(Exception ex)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                lblModalBody.Text = "Please select a location.";
            //}
            //else
            //{
            //    lblModalBody.Text = "الرجاء تحديد موقع.";
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
        
      
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("DoctorHome.aspx");
        //}
        //else
        //{
        //    Response.Redirect("DoctorHome.aspx?l=ar-EG");
        //}
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("doctorhome.aspx");
        //}
        //else
        //{
        //    Response.Redirect("doctorhome.aspx?l=ar-EG");
        //}
    }
}