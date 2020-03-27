using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hospital_Doctor_details : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();
    secure obj = new secure();

    protected override void InitializeCulture()
    {
        //Session["Speciality"] = "Auto";
        //string culture = "Auto";
        //try
        //{
        //    culture = Request.QueryString["l"].ToString();
        //    Session["Speciality"] = culture;
        //}
        //catch (Exception ex)
        //{ }
        //// string culture = Session["Speciality"].ToString();
        //if (string.IsNullOrEmpty(culture))
        //    culture = "Auto";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
        if (!IsPostBack)
        {
            try
            {
                //CheckLocation();
            }
            catch (Exception ex)
            {
                Response.Redirect("../index/hospita login.aspx");
            }
            Session["hname"] =obj.DecryptString(Request.QueryString["hos"].ToString());
            //Response.Write(Session["hname"].ToString());
            doctor();

            var query = from item in db.tbl_hospitalregs
                        where item.h_hakkimid == Session["hname"].ToString()
                        select item;

            foreach (var ss in query)
            {
                if (ss.h_photo == null)
                {
                  
                    Image1.Visible = false;
                }
                else
                {
                   
                    Image1.Visible = true;
                    Image1.ImageUrl = ss.h_photo;
                }
            }

            }
        }

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_hakkimid == Session["hname"].ToString()
                    select new { item1.h_id, item.latitude };
       
            if (query.Count() <= 0)
            {
            // Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
            //Label8.Text = "You must set your location";
            //ModalPopupExtender4.Show();
            RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location.')</Script>");
        }
        
    }

    public void doctor()
    {
        var Query = from item in db.tbl_hdoctors where item.h_id ==Session["hname"].ToString() && item.hd_status == 1 select item;
        foreach (var s in Query)
        {
            Label9.Text = s.h_name;
        }

        GridView1.DataSource = Query;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_hdoctors where item.h_id == Session["hname"].ToString() && item.hd_status == 1  && (item.hd_name.Contains(TextBox1.Text) || item.hd_id_number.Contains(TextBox1.Text)) select item;
        foreach (var s in Query)
        {
            Label9.Text = s.h_name;
        }

        GridView1.DataSource = Query;
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="check")
        {
            if (Session["AvailableDate"] != null)
            {
                Session["doctor"] = obj.EnryptString(e.CommandArgument.ToString());
                Session["AvailableDate"] = null;
                Response.Redirect("Hospitaldoctoravailability.aspx");
            }
            else
            {
                //Session["doctor"] = e.CommandArgument.ToString();
                Response.Redirect("Hospitaldoctoravailability.aspx?docid=" + obj.EnryptString(e.CommandArgument.ToString()) + "");
            }

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_hdoctors where item.h_id == Session["hname"].ToString() && item.hd_status == 1 && (item.hd_name.Contains(TextBox1.Text) || item.hd_id_number.Contains(TextBox1.Text)) select item;
        GridView1.DataSource = Query;
        GridView1.DataBind();
    }
}