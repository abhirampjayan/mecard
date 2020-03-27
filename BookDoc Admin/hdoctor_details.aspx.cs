using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_hdoctor_details : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.QueryString["doctid"] != "" && Request.QueryString["doctid"] != null)
            {
                string docid = HttpUtility.UrlDecode(Request.QueryString["doctid"]);
                string doctorid = Decrypt(docid);
                Session["hdoctor"] = doctorid.ToString();
            }
            hdoctor();
            SelectApointments();
            TodayAviablDoctrs();
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
    public void hdoctor()
    {
        var doc = from item in db.tbl_hdoctors where item.hd_email == Session["hdoctor"].ToString() select item;
        foreach(var ss in doc)
        {
            Label1.Text = ss.hd_name;
            Label2.Text = ss.hd_specialties;
            Label4.Text = ss.h_id;
            Label3.Text = ss.hd_contact;
            Session["hakkeemid_h"] = ss.h_id;
        }
    }
    public void SelectApointments()
    {
        try
        {
            var query = from item in db.tbl_hos_doc_appmnts
                        join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid

                        where item.d_id == Session["hdoctor"].ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.a_status == 1
                        orderby item.a_date, item.a_time ascending
                        select new { item.a_time, item.a_date, item.a_reason, item1.name };
            if (query.Count() > 0)
            {
                GridView1.DataSource = query.ToList();
                GridView1.DataBind();
            }
            else
            {
                //Label1.Text = "You have no appointments";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                //RegisterStartupScript("", "<Script Language=JavaScript>swal('No Appoitments')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا تعيينات')</Script>");
                //}
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        SelectApointments();
    }
    public void TodayAviablDoctrs()
    {

        var Query = from item in db.tbl_hos_doc_availables
                    where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == Session["hdoctor"].ToString()
                    orderby item.date ascending
                    select item;
        if (Query.Count() > 0)
        {
            DataList3.DataSource = Query;
            DataList3.DataBind();
            foreach (DataListItem dl3 in DataList3.Items)
            {
                Label lbl4 = dl3.FindControl("Label4") as Label;
                Label lbl5 = dl3.FindControl("Label5") as Label;
                Label Lbl6 = dl3.FindControl("Label6") as Label;




                // SqlCommand com = new SqlCommand("select date_id from tbl_hos_doc_available where date='"+Lbl6.Text+"'",con); ;
                //  int idd = Convert.ToInt32(com.ExecuteScalar());




                var Query2 = from item in db.tbl_hdoctors where item.hd_email == lbl4.Text select item;
                foreach (var n in Query2) { lbl5.Text = n.hd_name; }


                DataList dl4 = dl3.FindControl("DataList4") as DataList;
                var Query1 = from item in db.view_hos_doc_available_times
                             where item.h_hakkimid == Session["hakkeemid_h"].ToString() && item.date == Lbl6.Text
                             orderby item.date ascending
                             select item;
                List<string> lst = new List<string>();
                string time = "";
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("time"), new DataColumn("date") });
                //dt.Columns.AddRange(new DataColumn[1] { new DataColumn("time") });
                foreach (var tt in Query1)
                {
                    //string[] a = tt.hd_a_time.Split(',');
                    //for (int i = 0; i < a.Count(); i++)
                    //{try
                    try
                    {
                        dt.Rows.Add(tt.time.ToString(), Lbl6.Text);
                    }
                    catch (Exception ex)
                    { }
                    //}

                }

                DataView view = new DataView(dt);
                DataTable dvalue = view.ToTable(true, "time", "date");
                dl4.DataSource = dvalue;
                dl4.DataBind();


                foreach (DataListItem dl in dl4.Items)
                {
                    string date = DateTime.Parse(Lbl6.Text).ToString("yyyy-MM-dd");
                    Button button2 = dl.FindControl("Button2") as Button;
                    String timet = "";
                    string[] t = new string[7];
                    string ab1 = "", ab2 = "";
                    try
                    {

                        int l = button2.Text.Count();
                        ab1 = button2.Text.Substring(0, l - 2);
                        ab2 = button2.Text.Substring(l - 2, 2);
                        timet = ab1.ToString() + " " + ab2.ToString();

                    }
                    catch (Exception ex)
                    {

                    }





                    var selctAvl = from avl in db.tbl_hos_doc_appmnts
                                   where avl.a_date == date && avl.a_time == timet.ToString() && avl.d_id == Session["hdoctor"].ToString() && avl.h_id == Session["hakkeemid_h"].ToString()
                                   select avl;
                    if (selctAvl.Count() > 0)
                    {
                        foreach (var tt in selctAvl)
                        {
                            if (tt.a_status == 0)
                            {
                                button2.BackColor = System.Drawing.Color.Orange;
                                button2.Enabled = false;
                                button2.ToolTip = "Waiting for confirmation";
                            }
                            if (tt.a_status == 1)
                            {
                                button2.BackColor = System.Drawing.Color.IndianRed;
                                button2.Enabled = false;
                                button2.ToolTip = "Booked";
                            }
                        }
                    }
                    else
                    {
                        //button2.BackColor = System.Drawing.Color.Green;
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                        button2.ToolTip = "Available";
                        //}
                        //else
                        //{
                        //    button2.ToolTip = "متاح";
                        //}
                    }

                }

            }

        }
        else
        {
        }
    }
}