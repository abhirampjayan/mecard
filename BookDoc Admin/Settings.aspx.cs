using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class BookDoc_Admin_Settings : System.Web.UI.Page
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
        //    this.MasterPageFile = "~/BookDoc Admin/AdminArabicMasterPage.master";
        //}
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGrvCities();
            LoadGrvSpecialities();
        }
    }
    protected void BtnAddCity_Click(object sender, EventArgs e)
    {
        try
        {
            var city = from item in db.tbl_cities where item.City == TxtCity.Text select item;
            if (city.Count() > 0)
            {
                TxtCity.Text = "";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Already exist')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('موجود مسبقا')</Script>");
                //}
            }
            else
            {
               
                tbl_city t_city = new tbl_city()
                {
                    City = TxtCity.Text,
                };
                db.tbl_cities.InsertOnSubmit(t_city);
                db.SubmitChanges();
                //TxtCity.Text = "";
                LoadGrvCities();
            }
         
        }
        catch (Exception ex)
        {
        }
    }
    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    tbl_city t = new tbl_city()
    //    {
    //        City = TextBoxcity.Text,
    //    };
    //    db.tbl_cities.InsertOnSubmit(t);
    //    db.SubmitChanges();
    //    LoadGrvCities();
    //}


    public void LoadGrvCities()
    {
        try
        {
            var query = from item in db.tbl_cities
                        select item;
            if (query.Count() > 0)
            {
                GrvCities.DataSource = query;
                GrvCities.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }

    public void LoadGrvSpecialities()
    {
        try
        {
            var query = from item in db.tbl_specialities
                        select item;
            if (query.Count() > 0)
            {
                GrvSpeciality.DataSource = query;
                GrvSpeciality.DataBind();
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void GrvCities_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GrvCities.DataKeys[e.RowIndex].Values["id"].ToString();

        try
        {
            var query = from item in db.tbl_cities
                        where item.id == Convert.ToInt64(id)
                        select item;
            db.tbl_cities.DeleteAllOnSubmit(query);
            db.SubmitChanges();
        }
        catch (Exception ex)
        {

        }
        LoadGrvCities();
    }
    protected void GrvCities_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvCities.PageIndex = e.NewPageIndex;
        LoadGrvCities();
    }
    protected void BtnAddSpeciality_Click(object sender, EventArgs e)
    {
        try
        {
            var spec = from item in db.tbl_specialities where item.Specialities == TxtSpecialities.Text select item;
            if (spec.Count() > 0)
            {
                TxtSpecialities.Text = "";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Already exist')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('موجود مسبقا')</Script>");
                //}
            }
            else
            {
               
                tbl_speciality t_city = new tbl_speciality()
                {
                    Specialities = TxtSpecialities.Text,
                };
                db.tbl_specialities.InsertOnSubmit(t_city);
                db.SubmitChanges();
                //TxtSpecialities.Text = "";
                LoadGrvSpecialities();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void GrvSpeciality_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GrvSpeciality.DataKeys[e.RowIndex].Values["id"].ToString();

        try
        {
            var query = from item in db.tbl_specialities
                        where item.id == Convert.ToInt64(id)
                        select item;
            db.tbl_specialities.DeleteAllOnSubmit(query);
            db.SubmitChanges();
        }
        catch (Exception ex)
        {

        }
        LoadGrvSpecialities();
    }
    protected void GrvSpeciality_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrvSpeciality.PageIndex = e.NewPageIndex;
        LoadGrvSpecialities();
    }

    protected void GrvSpeciality_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrvSpeciality.EditIndex = e.NewEditIndex;
        LoadGrvSpecialities();
    }

    protected void GrvSpeciality_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrvSpeciality.EditIndex = -1;
        LoadGrvSpecialities();
    }

    protected void GrvSpeciality_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = (GrvSpeciality.Rows[e.RowIndex].Cells[0].FindControl("Label3") as Label).Text;
        string txtspec = (GrvSpeciality.Rows[e.RowIndex].Cells[0].FindControl("TextBox1") as TextBox).Text;
        var spec = from item in db.tbl_specialities where item.id == int.Parse(id) select item;
        foreach (var ss in spec)
        {
            ss.Specialities = txtspec;
        }
        db.SubmitChanges();
        GrvSpeciality.EditIndex = -1;
        LoadGrvSpecialities();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        var spec = from item in db.tbl_specialities where item.Specialities.Contains(TextBox2.Text) select item;
        GrvSpeciality.DataSource = spec;
        GrvSpeciality.DataBind();
    }

    protected void GrvCities_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrvCities.EditIndex = e.NewEditIndex;
        LoadGrvCities();
    }

    protected void GrvCities_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = (GrvCities.Rows[e.RowIndex].Cells[0].FindControl("Label4") as Label).Text;
        string txtspec = (GrvCities.Rows[e.RowIndex].Cells[0].FindControl("TextBox3") as TextBox).Text;
        var city = from item in db.tbl_cities where item.id == int.Parse(id) select item;
        foreach (var ss in city)
        {
            ss.City = txtspec;
        }
        db.SubmitChanges();
        GrvCities.EditIndex = -1;
        LoadGrvCities();
    }

    protected void GrvCities_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrvCities.EditIndex = -1;
        LoadGrvCities();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        var scity = from item in db.tbl_cities where item.City.Contains(TextBox4.Text) select item;
        GrvCities.DataSource = scity;
        GrvCities.DataBind();
    }



    protected void TextBox4_TextChanged(object sender, EventArgs e)
   {
      
        var scity = from item in db.tbl_cities where item.City.Contains(TextBox4.Text) select item;
        GrvCities.DataSource = scity;
        GrvCities.DataBind();
    }
}