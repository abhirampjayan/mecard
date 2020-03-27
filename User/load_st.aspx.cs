using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class User_load_st : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("Data Source=TAHCOM-1\\SQLEXPRESS;Initial Catalog=db_BookDoc;User ID=sa;Password=admin123;MultipleActiveResultSets=True");
    SqlCommand cmd;
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Response.Write("jh");


        //cmd = new SqlCommand("select * from station_info where dis='" + st + "'", con);
        //dr = cmd.ExecuteReader();

        //if (dr.HasRows)
        //{
           

        DateTime dt = new DateTime();
        dt = DateTime.Now.Date;
      

        Response.Write("<table class='table table-responsive table-bordered table-hover'><tr>");
           for(int i=0;i<30;i++)
            {
                Response.Write("<td>" + dt + "</td>");
            dt = dt.AddDays(1);
            }

            Response.Write("</tr></table>");
        //}
        //else
        //{
        //    Response.Write("No Information Found");
        //}
    }
}