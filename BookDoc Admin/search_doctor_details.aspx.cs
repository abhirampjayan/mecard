using System;
using System.Collections.Generic;
using System.Linq;
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
public partial class BookDoc_Admin_search_doctor_details : System.Web.UI.Page
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
            LoadSpecialities();
            GetDocDetails();
        }

    }

    protected void GetDocDetails()
    {
        var doctor1 = from item in db.tbl_doctors where item.d_status == 1  orderby item.d_id descending select item;
        if (doctor1.Count() > 0)
        {

            DataList2.Visible = true;
            DataList2.DataSource = doctor1;
            DataList2.DataBind();
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            DataList2.Visible = false;
        }
    }

    public void LoadSpecialities()
    {
        try
        {
            var query = from item in db.tbl_specialities
                        select item;
            if (query.Count() > 0)
            {
                dl_speciality.DataSource = query;
                dl_speciality.DataTextField = "Specialities";
                dl_speciality.DataValueField = "id";
                dl_speciality.DataBind();
                dl_speciality.Items.Insert(0, "--Select Specialty--");
            }

        }
        catch (Exception ex)
        {
        }
    }

    //protected void dl_speciality_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //foreach (DataListItem dli in DataList2.Items)

    //{

    //    LinkButton lnk2 = dli.FindControl("LinkButton2") as LinkButton;
    //    Image img1 = dli.FindControl("img_doc") as Image;
    //    //Object objImage = dli.FindControl("Image1");
    //    //if (objImage != null)
    //    //{

    //    //}
    //}
    // }

    protected void btn_search_Click(object sender, EventArgs e)
    {
      
        var doctor1 = from item in db.tbl_doctors where item.d_status == 1 && item.d_specialties == dl_speciality.SelectedItem.Text || item.d_status == 1 && item.d_name == txtsearch.Text && item.d_specialties == dl_speciality.SelectedItem.Text || item.d_status == 1 && item.d_hakkimid == txtsearch.Text && item.d_specialties == dl_speciality.SelectedItem.Text || item.d_status == 1 && item.d_contact == obj.EnryptString(txtsearch.Text) && item.d_specialties == dl_speciality.SelectedItem.Text || item.d_status == 1 && item.d_name == txtsearch.Text || item.d_status == 1 && item.d_contact == obj.EnryptString(txtsearch.Text) || item.d_status == 1 && item.d_hakkimid == txtsearch.Text orderby item.d_id descending select item;
        if (doctor1.Count() > 0)
        {

            DataList2.Visible = true;
            DataList2.DataSource = doctor1;
            DataList2.DataBind();
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            DataList2.Visible = false;
        }
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
    protected void DataList2_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "doc")
        {
            string docid = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
            Response.Redirect("doctor_details.aspx?doctid=" + docid);
        }
        else if (e.CommandName == "rev")
        {
            string docid = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
            Response.Redirect("doctor_review.aspx?doctid=" + docid);
        }
        else
        {

        }
    }

    protected void dl_speciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        var doctor1 = from item in db.tbl_doctors where item.d_status == 1 && item.d_specialties == dl_speciality.SelectedItem.Text  orderby item.d_id descending select item;
        if (doctor1.Count() > 0)
        {

            DataList2.Visible = true;
            DataList2.DataSource = doctor1;
            DataList2.DataBind();
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            DataList2.Visible = false;
        }
    }
}