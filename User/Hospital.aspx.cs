using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Hospital : System.Web.UI.Page
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
        if(Session["hospital"]==null)
        {
            Response.Redirect("~/User/Search.aspx");
        }

        if(!IsPostBack)
        {
            hospital();
        }
    }

    public void hospital()
    {
        var Query = from item in db.tbl_hospitalregs where item.h_hakkimid == Session["hakkeemid_h"].ToString() select item;
        foreach(var ss in Query)
        {
            hname.Text = ss.h_name;
            Image1.ImageUrl = ss.h_photo;
            Label6.Text = ss.h_name;
        }
        DetailsView1.DataSource = Query;
        DetailsView1.DataBind();
    }
}