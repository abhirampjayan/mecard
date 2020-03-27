using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Hospital_doctors : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

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
        if(!IsPostBack)
        {
            Doctor();
        }

    }

    public void Doctor()
    {
        var Query = from item in db.tbl_hospitalregs
                    where item.h_id == Convert.ToInt32(Session["hid"].ToString())
                    select item;
       
            DataList2.DataSource = Query;
            DataList2.DataBind();
        
    }

    protected void txtContactsSearch_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtZipCodeSearch_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtLangSearch_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void AnyGender_Click(object sender, EventArgs e)
    {

    }

    protected void Male_Click(object sender, EventArgs e)
    {

    }

    protected void Female_Click(object sender, EventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button5_Click(object sender, EventArgs e)
    {

    }

    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void Illness_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Rating1_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {

    }
}