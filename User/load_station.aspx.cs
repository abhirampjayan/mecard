using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class load_stationadmin : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd, cmd1;
    SqlDataReader dr, dr1;
    protected void Page_Load(object sender, EventArgs e)
    {
       con = new SqlConnection("server=.;uid=sa;pwd=admin123;database=online_crime;MultipleActiveResultSets=True");
        string st = Request.QueryString["st"].ToString();
        con.Open();
        cmd = new SqlCommand("select * from station_info where dis='" + st + "'", con);
        dr = cmd.ExecuteReader();

        if (dr.HasRows)
        {
            int i = 1;
            Response.Write("<table class='table table-responsive table-bordered table-hover'><tr><td>#</td><td>Station Name</td><td>Contact</td><td>More</td></tr>");
            while (dr.Read())
            {
                Response.Write("<tr><td>"+i+"</td><td>"+dr[2]+"</td><td>"+dr[4]+"</td><td><a href='more_view.aspx?t=1&stid="+dr[0]+"'><img src='logo/view.png' title='View Info' /></a> |<a href='more_view.aspx?t=2&stid="+dr[0]+"'> <img src='logo/edit.png' title='Edit Info' /></a></td></tr>");
                i++;
            }
            Response.Write("</table>");
        }
        else
        {
            Response.Write("No Information Found");
        }
        con.Close();
        
    }
}