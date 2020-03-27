using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_hospital_details : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            hdoc();
        }
    }

    public void hdoc()
    {
        var doc = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() select item;
        DataList1.DataSource = doc;
        DataList1.DataBind();
    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName=="open")
        {
            Session["hdoctor"] = e.CommandArgument.ToString();
            Response.Redirect("hdoctor_details");
        }
    }
}