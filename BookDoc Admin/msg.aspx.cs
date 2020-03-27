using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_msg : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();
        if (!IsPostBack)
        {
            fill();
        }

    }
    public void fill()
    {

        SqlDataAdapter sda1 = new SqlDataAdapter("Select * from tbl_msg order by id desc ", con);
        DataSet dts = new DataSet();
        dts.Clear();
        sda1.Fill(dts);
        //  var doctor1 = from item in db.tbl_doctors where item.d_status == 1 && item.d_id_expire>= GETDATE() orderby item.d_id descending select item;
        GridView2.DataSource = dts;
        GridView2.DataBind();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        fill();
    }


    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string id = e.CommandArgument.ToString();
            SqlCommand cmd8 = new SqlCommand("Delete from tbl_mail where id='" + id.ToString() + "'", con);
            cmd8.ExecuteNonQuery();
            fill();
        }
        if (e.CommandName == "open1")
        {
            Session["dh"] = e.CommandArgument.ToString();
            Response.Redirect("doctordetails.aspx");
        }
    }
}