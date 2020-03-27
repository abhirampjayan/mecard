using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_UserIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["db_BookDocConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct d_name,d_specialties from tbl_doctor where " +
                "d_name like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["d_name"].ToString());
                        customers.Add(sdr["d_specialties"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    //......................................................


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchZipCode(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["db_BookDocConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct d_location from tbl_doctor where " +
                "d_location like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["d_location"].ToString());
                        
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    ////..................................................////////////



    //........................................................................

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchLanguage(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["db_BookDocConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct d_language from tbl_doctor where " +
                "d_language like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["d_language"].ToString());

                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    ////.......................................................................


    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["dname_specialty"] = txtContactsSearch.Text;
        Session["zipcode"] = txtZipCodeSearch.Text;
        Session["language"] = txtLangSearch.Text;
        Response.Redirect("~/User/Search.aspx");
    }
}