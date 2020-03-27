<%@ WebHandler Language="C#" Class="Search" %>

using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;
    using System.Configuration;
public class Search : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)
    {
        string searchText = context.Request.QueryString["q"];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand("select id,name,photo from tbl_signup where name Like @Search + '%' ", con);
        cmd.Parameters.AddWithValue("@Search",searchText);
        StringBuilder sb = new StringBuilder();
        using(SqlDataReader dr=cmd.ExecuteReader())
        {
            while(dr.Read())
            {
                sb.Append(string.Format("{0},{1}{2}",dr["name"],dr["photo"],Environment.NewLine));
            }
        }
        con.Close();
        context.Response.Write(sb.ToString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

