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

public partial class BookDoc_Admin_Search_hospital_details : System.Web.UI.Page
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
            SelectHospitals();
            gethosdocdetails();
        }
    }

    protected void gethosdocdetails()
    {
        if (con.State.ToString() == "Closed")
        {
            con.Open();
        }
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(" select hs.*, hd.* from tbl_hospitalreg hs inner join tbl_hdoctor hd on hd.h_id = hs.h_hakkimid where   hs.h_status = '1'  ", con);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            DataList2.Visible = true;
            DataList2.DataSource = dt;
            DataList2.DataBind();


        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            DataList2.Visible = false;
        }
        con.Close();
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
    public void SelectHospitals()
    {

        con.Open();
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_hospitalreg  where h_status='1'", con);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dl_hospital.DataSource = dt;
            dl_hospital.DataValueField = "h_hakkimid";
            dl_hospital.DataTextField = "h_name";
            dl_hospital.DataBind();
            dl_hospital.Items.Insert(0, "--Select Hospital--");
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
            Response.Redirect("hdoctor_details.aspx?doctid=" + docid);
        }
        else if (e.CommandName == "rev")
        {
            string docid = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
            Response.Redirect("hospitaldoctor_review.aspx?doctid=" + docid);
        }
        else
        {

        }
    }

    protected void dl_hospital_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State.ToString() == "Closed")
        {
            con.Open();
        }
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter("Select hd.*  from tbl_hdoctor hd where  hd.h_id='" + dl_hospital.SelectedValue.ToString() + "' ", con);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            DataList2.Visible = true;
            DataList2.DataSource = dt;
            DataList2.DataBind();


        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            DataList2.Visible = false;
        }
        con.Close();
        //if (con.State.ToString() == "Closed")
        //{
        //    con.Open();
        //}
        //DataTable dt = new DataTable();
        //DataTable dt1 = new DataTable();
        //SqlDataAdapter sda = new SqlDataAdapter("Select hs.*,hd.*  from tbl_hospitalreg hs inner join tbl_hdoctor hd on hd.h_id=hs.h_hakkimid where hs.h_status='1' and hs.h_name='" + dl_hospital.SelectedItem.Text + "' ", con);
        //sda.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    DataList2.Visible = true;
        //    DataList2.DataSource = dt;
        //    DataList2.DataBind();
        //    foreach (DataListItem dli in DataList2.Items)
        //    {
        //        Image img1 = dli.FindControl("img_doc") as Image;
        //        Label lbl1 = dli.FindControl("Label1") as Label;

        //        var Query1 = from item in db.tbl_hdoctors where item.hd_email == lbl1.Text select item;
        //        foreach (var ss in Query1)
        //        {
        //            if(ss.hd_photo!="" || ss.hd_photo != null)
        //            {
        //                img1.ImageUrl = ss.hd_photo;
        //            }
        //            else
        //            {
        //                img1.ImageUrl = "../Doctorimages/doctor.png";
        //            }
                   
        //        }
              
        //    }
           
        //}
        //else
        //{
        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
        //    DataList2.Visible = false;
        //}
        //con.Close();
    }

    protected void dl_speciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (con.State.ToString() == "Closed")
        {
            con.Open();
        }
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter("Select hd.*  from  tbl_hdoctor hd  where  hd.hd_specialties='" + dl_speciality.SelectedItem.Text + "' and hd.h_id='" + dl_hospital.SelectedValue.ToString() + "' or  hd.hd_specialties='" + dl_speciality.SelectedItem.Text + "' ", con);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            DataList2.Visible = true;
            DataList2.DataSource = dt;
            DataList2.DataBind();


        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            DataList2.Visible = false;
        }
        con.Close();
    }
  
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (con.State.ToString() == "Closed")
        {
            con.Open();
        }
        DataTable dt = new DataTable();
        if (txtsearch.Text != "" || dl_hospital.SelectedIndex > 0 && txtsearch.Text != "" || dl_hospital.SelectedIndex > 0 && dl_speciality.SelectedIndex > 0 || dl_hospital.SelectedIndex > 0 && dl_hospital.SelectedIndex > 0 && txtsearch.Text != "" || dl_hospital.SelectedIndex > 0 || dl_speciality.SelectedIndex > 0)
        {


            if (txtsearch.Text != "")
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select hs.*, hd.* from tbl_hospitalreg hs inner join tbl_hdoctor hd on hd.h_id = hs.h_hakkimid where   hs.h_status = '1' and hd.hd_contact='" + txtsearch.Text + "' or hs.h_status = '1' and hd.h_id='" + txtsearch.Text + "'  ", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataList2.Visible = true;
                    DataList2.DataSource = dt;
                    DataList2.DataBind();


                }
            }
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            //    DataList2.Visible = false;
            //}
            if (dl_hospital.SelectedIndex > 0 && txtsearch.Text != "")
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select hs.*, hd.* from tbl_hospitalreg hs inner join tbl_hdoctor hd on hd.h_id = hs.h_hakkimid where   hs.h_status = '1' and hd.h_id='" + txtsearch.Text + "' and hs.h_name = '" + dl_hospital.SelectedItem.Text + "' or  hs.h_status = '1' and hd.hd_contact='" + txtsearch.Text + "' and hs.h_name = '" + dl_hospital.SelectedItem.Text + "'  ", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataList2.Visible = true;
                    DataList2.DataSource = dt;
                    DataList2.DataBind();


                }
            }
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            //    DataList2.Visible = false;
            //}
            if (dl_hospital.SelectedIndex > 0 && dl_speciality.SelectedIndex > 0)
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select hs.*, hd.* from tbl_hospitalreg hs inner join tbl_hdoctor hd on hd.h_id = hs.h_hakkimid where hs.h_status = '1' and hs.h_name = '" + dl_hospital.SelectedItem.Text + "'  and hd.hd_specialties='" + dl_speciality.SelectedItem.Text + "' ", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataList2.Visible = true;
                    DataList2.DataSource = dt;
                    DataList2.DataBind();


                }
            }
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            //    DataList2.Visible = false;
            //}
            if (dl_hospital.SelectedIndex > 0 && dl_hospital.SelectedIndex > 0 && txtsearch.Text != "")
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select hs.*, hd.* from tbl_hospitalreg hs inner join tbl_hdoctor hd on hd.h_id = hs.h_hakkimid where  hs.h_status = '1' and hd.h_id='" + txtsearch.Text + "' and hs.h_name = '" + dl_hospital.SelectedItem.Text + "' and hd.hd_specialties='" + dl_speciality.SelectedItem.Text + "' or  hs.h_status = '1' and hd.hd_contact='" + txtsearch.Text + "' and hs.h_name = '" + dl_hospital.SelectedItem.Text + "' and hd.hd_specialties='" + dl_speciality.SelectedItem.Text + "'  ", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataList2.Visible = true;
                    DataList2.DataSource = dt;
                    DataList2.DataBind();


                }
            }
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            //    DataList2.Visible = false;
            //}
            if (dl_hospital.SelectedIndex > 0)
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select hs.*, hd.* from tbl_hospitalreg hs inner join tbl_hdoctor hd on hd.h_id = hs.h_hakkimid where  hs.h_status = '1' and hs.h_name = '" + dl_hospital.SelectedItem.Text + "'  ", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataList2.Visible = true;
                    DataList2.DataSource = dt;
                    DataList2.DataBind();


                }
            }
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            //    DataList2.Visible = false;
            //}
            if (dl_speciality.SelectedIndex > 0)
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select hs.*, hd.* from tbl_hospitalreg hs inner join tbl_hdoctor hd on hd.h_id = hs.h_hakkimid where  hs.h_status = '1' and hd.hd_specialties = '" + dl_speciality.SelectedItem.Text + "'  ", con);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataList2.Visible = true;
                    DataList2.DataSource = dt;
                    DataList2.DataBind();


                }
            }
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            //    DataList2.Visible = false;
            //}
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Sorry! not found')</Script>");
            DataList2.Visible = false;
        }
        con.Close();
    }
}