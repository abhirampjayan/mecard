using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_test : System.Web.UI.Page
{
    

        SqlConnection con = new SqlConnection("Data Source=TAHCOM-1\\SQLEXPRESS;Initial Catalog=db_BookDoc;User ID=sa;Password=admin123;MultipleActiveResultSets=True");
        SqlCommand cmd;
        SqlDataReader dr,dr1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            con.Open();

            cmd = new SqlCommand("select distinct(d_name) as d_name from view_dt_time", con);

            dr = cmd.ExecuteReader();


            Response.Write("<table class='table table-responsive table-bordered table-hover'>");

            while (dr.Read())
            {
                Response.Write("<tr><td width='30px'>" + dr[0].ToString() + "</td>");


                Response.Write("<td>");

                DateTime dt = new DateTime();
                dt = DateTime.Now.Date;
                int j = 0;

                Response.Write("<ul class='bxslider'>");
                Response.Write("<li>");
                for (int i = 0; i < 30; i++)
                {

                 //   Response.Write("<li>");
                    if (j < 3)
                    {
                    
                      
                        Response.Write("<div><div style='display:inline-block'>" + dt.ToString("ddd MMM dd" )+"</br>");
                        string cdate = dt.ToString("yyyy-MM-dd");
                     // DateTime  dte = Convert.ToDateTime(cdate);
                        string qry = "select time from view_dt_time where d_name='" + dr[0].ToString() + "' and date='" +cdate + "'";

                        cmd = new SqlCommand(qry, con);
                        dr1 = cmd.ExecuteReader();
                        while (dr1.Read())
                        {
                            Response.Write( dr1[0].ToString()+"<br>");

                        }
                        Response.Write("</div></div");
                      
                        dr1.Close();

                       // Response.Write("</li>");
                        j++;
                    }
                    if (j == 3)
                    {

                        Response.Write("</li>");
                        Response.Write("<li>");
                        j = 0;
                    }

                    dt = dt.AddDays(1);
                }
                Response.Write("</ul>");

                Response.Write("</td></tr>");

            }

            dr.Close();
            Response.Write("</table>");

        }


    }
}
