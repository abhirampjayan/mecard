using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctor_msg : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        con.Open();
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
            upModal1.Update();
        }
    }
    private static DateTime ConvertToEngCal(string hijri)
    {
        CultureInfo arSA = new CultureInfo("ar-SA");
        arSA.DateTimeFormat.Calendar = new HijriCalendar();
        return DateTime.ParseExact(hijri, "dd/MM/yy", arSA);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string dd = System.DateTime.Now.ToString();
        string[] dd1 = new string[7];

        dd1 = dd.Split('-');
        string[] dd2 = new string[7];
        dd2 = dd1[2].Split(' ');

        //    Response.Write("<br>" + dd2[0]);
        string dttime = System.DateTime.Now.ToString();

        if (dd2[0] == "2018" || dd2[0] == "2019" || dd2[0] == "2020")
        {

        }
        else
        {
            dttime = ConvertToEngCal(dttime).ToString("dd-MM-yyyy");
        }



        SqlCommand com = new SqlCommand("insert into tbl_msg values('" + TextBox1.Text + "','"+ Session["hakkeemid_d"].ToString()  + "','" + dttime.ToString() + "')", con);
         int id=Convert.ToInt32(com.ExecuteNonQuery());
       Label19.Text = "Message send to Hakkeem Admin";
       // Label19.Text = com.CommandText.ToString();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("doctorhome.aspx");
    }
    }