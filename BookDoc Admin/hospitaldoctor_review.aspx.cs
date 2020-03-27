using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography;
using System;


public partial class BookDoc_Admin_hospitaldoctor_review : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    string qry;
    SqlCommand cmd;
    SqlDataReader dr, dr1, dr2;
    int pagestart = 1;
    int q = 0;
    secure obj = new secure();
    MailMessage Email = new MailMessage();
    SMS ob = new SMS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string docid = "";
            if (Request.QueryString["doctid"] != "" && Request.QueryString["doctid"] != null)
            {
                docid = HttpUtility.UrlDecode(Request.QueryString["doctid"]);
                GetUserReview();
            }
        }
    }
    protected void GetUserReview()
    {
        if (con.State.ToString() == "Closed")
        {
            con.Open();
        }
        string docid = "";
        DataTable dt = new DataTable();
        docid = HttpUtility.UrlDecode(Request.QueryString["doctid"]);
        string doctorid = Decrypt(docid);
        SqlDataAdapter sda = new SqlDataAdapter("Select hs.*,hd.*,re.*,us.*,re.id as reid from tbl_hdoctor hd inner join tbl_user_feed re on re.d_email=hd.hd_email inner join tbl_hospitalreg hs on hs.h_hakkimid=hd.h_id inner join tbl_signup us on  re.u_email=us.u_hakkimid  where re.d_email='" + doctorid + "'", con);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            GridView1.Visible = false;
        }
        con.Close();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GetUserReview();
    }

    public static string Encrypt(string inputText)
    {
        string encryptionkey = "SAUW193BX628TD57";
        byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
        using (ICryptoTransform encryptrans = rijndaelCipher.CreateEncryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
        {
            using (MemoryStream mstrm = new MemoryStream())
            {
                using (CryptoStream cryptstm = new CryptoStream(mstrm, encryptrans, CryptoStreamMode.Write))
                {
                    cryptstm.Write(plainText, 0, plainText.Length);
                    cryptstm.Close();
                    return Convert.ToBase64String(mstrm.ToArray());
                }
            }
        }
    }
    public static string Decrypt(string encryptText)
    {
        string encryptionkey = "SAUW193BX628TD57";
        byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] encryptedData = Convert.FromBase64String(encryptText.Replace(" ", "+"));
        PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
        using (ICryptoTransform decryptrans = rijndaelCipher.CreateDecryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
        {
            using (MemoryStream mstrm = new MemoryStream(encryptedData))
            {
                using (CryptoStream cryptstm = new CryptoStream(mstrm, decryptrans, CryptoStreamMode.Read))
                {
                    byte[] plainText = new byte[encryptedData.Length];
                    int decryptedCount = cryptstm.Read(plainText, 0, plainText.Length);
                    return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                }
            }
        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (con.State.ToString() == "Closed")
        {
            con.Open();
        }
        string docid = "";
        DataTable dt = new DataTable();
        docid = HttpUtility.UrlDecode(Request.QueryString["doctid"]);
        string doctorid = Decrypt(docid);
        foreach (GridViewRow gr in GridView1.Rows)
        {

            LinkButton lnk4 = gr.FindControl("LinkButton4") as LinkButton;
            LinkButton lnk5 = gr.FindControl("LinkButton5") as LinkButton;
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_user_feed where d_email='" + doctorid + "' and status='0'", con);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lnk4.Visible = true;
                lnk5.Visible = false;
                lnk4.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lnk4.Visible = false;
                lnk5.Visible = true;
            }

        }
        con.Close();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (con.State.ToString() == "Closed")
        {
            con.Open();
        }

        if (e.CommandName == "blk")
        {
            int tid = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_user_feed where  id='" + tid + "'", con);
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                int flag = Convert.ToInt32(dt.Rows[0]["status"].ToString());

                if (flag == 0)
                {
                    //str = "update tbl_homebanner set IsActive='" + false + "' where banner_id=" + tid + "";
                    int userfeid = tid;
                    SqlCommand cmd = new SqlCommand("update tbl_user_feed set status='1' where id='" + tid + "'", con);
                    cmd.ExecuteNonQuery();
                    GetUserReview();
                }

            }
        }
        if (e.CommandName == "ublk")
        {
            int tid = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_user_feed where  id='" + tid + "'", con);
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                int flag = Convert.ToInt32(dt.Rows[0]["status"].ToString());

                if (flag == 1)
                {
                    //str = "update tbl_homebanner set IsActive='" + false + "' where banner_id=" + tid + "";
                    int userfeid = tid;
                    SqlCommand cmd = new SqlCommand("update tbl_user_feed set status='0' where id='" + tid + "'", con);
                    cmd.ExecuteNonQuery();
                    GetUserReview();
                }

            }
        }
        con.Close();
    }
}