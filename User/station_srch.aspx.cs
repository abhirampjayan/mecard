using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class zonehome : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd,cmd1;
    SqlDataReader dr;
    string zid;
    protected void Page_Load(object sender, EventArgs e)
    {
       con = new SqlConnection("server=.;uid=sa;pwd=admin123;database=online_crime;MultipleActiveResultSets=True");
        string zadmin = Session["zadm"].ToString();
        con.Open();
        cmd = new SqlCommand("select * from zone_info where uid='"+zadmin+"'",con);
        dr = cmd.ExecuteReader();
        dr.Read();
        Label1.Text = dr[2].ToString();
        Label2.Text = dr[1].ToString();
        zoid.Text = dr[0].ToString();
        zid = dr[0].ToString();
        con.Close();

        con.Open();
        cmd = new SqlCommand("select * from zone_dist where zid='"+zid+"'",con);
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                Label3.Text += "<span class='btn btn-sm btn-default btn-block'>"+dr[2]+"</span>";
            }
        }
        else
        {
            Label3.Text = "<span class='btn btn-sm btn-default btn-block'>No List Found</span>";
        }
        con.Close();
    }
    
}