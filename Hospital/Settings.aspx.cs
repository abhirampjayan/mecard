using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using HelperClass;
using System.Data.SqlClient;
using System.Configuration;

public partial class Hospital_Settings : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    secure obj = new secure();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    protected override void InitializeCulture()
    {
        //Session["Language"] = "";
        //string culture = "";
        //try
        //{
        //    culture = Request.QueryString["l"].ToString();
        //    Session["Language"] = culture;
        //}
        //catch (Exception ex)
        //{ }
        //// string culture = Session["Language"].ToString();
        //if (string.IsNullOrEmpty(culture))
        //{
        //    culture = "Auto";
        //    Session["Language"] = culture;
        //}
        ////Use this
        //UICulture = culture;
        //Culture = culture;
        ////OR This
        //if (culture != "Auto")
        //{

        //    System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
        //    System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
        //    System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
        //}
        //else
        //{
        //    //LinkButton1.Text = "عربى";
        //}

        //base.InitializeCulture();
    }
    protected void Fillcity()
    {
        con.Open();
        SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_cities", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DdlCity.DataSource = dt;
            DdlCity.DataTextField = "City";
            DdlCity.DataValueField = "id";
            DdlCity.DataBind();
        }
        con.Close();
    }
    void Page_PreInit(Object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{

        //}
        //else
        //{
        //    this.MasterPageFile = "~/hospital/ArabichospitalMaster.master";
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["hakkeemid_h"]==null)
        {
            Response.Redirect("~/Index/Hospita Login.aspx");
        }
        if(!IsPostBack)
        {
            try
            {
                Fillcity();
                CheckLocation();
            }
            catch (Exception ex)
            {
                Response.Redirect("../index/hospita login.aspx");
            }
            BindHospitalDetails();
        }
    }

 

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                    select new { item1.h_id, item.latitude };
        try
        {
            if (query.Count() <= 0)
            {
                // Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
                //Label8.Text = "You must set your location";
                //ModalPopupExtender4.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب تعيين موقعك')</Script>");
                //}
            }
        }
        catch (Exception ex)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("../index/hospita login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("../index/hospita login.aspx?l=ar-EG");
            //}
        }
    }

    // Method for get hospital details
    public void BindHospitalDetails()
    {
        try
        {
            var query = from item in db.tbl_hospitalregs
                        where item.h_hakkimid == Session["hakkeemid_h"].ToString()
                        select item;

            foreach(var ss in query)
            {
                TxtAbout.Text = ss.h_about;
                TxtAddress.Text = ss.h_address;
                for (int i = 0; i < DdlCity.Items.Count;i++ )
                {
                    if(DdlCity.Items[i].Text==ss.h_city)
                    {
                        DdlCity.SelectedIndex = i;
                    }
                }
                TxtContact.Text = ss.h_contact;
                TxtEmail.Text = ss.h_email;
                TxtH_Name.Text = ss.h_name;
                TxtRegNo.Text = ss.h_regno;
                TxtZip.Text = ss.h_zipcode;
                TxtHakkeemId.Text = ss.h_hakkimid.ToString();
                if (ss.h_photo == null)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        BtnChangFoto.Text = "Upload photo";
                    //}
                    //else
                    //{
                    //    BtnChangFoto.Text = "حمل الصورة";
                    //}
                    Image1.Visible = false;
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        BtnChangFoto.Text = "Change photo";
                    //}
                    //else
                    //{
                    //    BtnChangFoto.Text = "غير الصوره";
                    //}
                    Image1.Visible = true;
                    Image1.ImageUrl = ss.h_photo;
                }
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex);
        }
    }

    protected void BtnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            var query = from item in db.tbl_hospitalregs
                        where item.h_hakkimid == Session["hakkeemid_h"].ToString()
                        select item;
            foreach(var ss in query)
            {
                if (ss.h_password ==obj.EnryptString(TxtCurrentPass.Text))
                {
                    ss.h_password =obj.EnryptString(TxtConfirmNew.Text);
                    db.SubmitChanges();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('Succesfully password changed')</Script>");
                    //}
                    //else
                    //{
                    //    this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير كلمة المرور بنجاح')</Script>");

                    //}
                    //Label2.Text = "Succesfully password changed";
                    //this.ModalPopupExtender2.Show();
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('The entered current password is incorrect. Please enter correct password.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('كلمة المرور الحالية المدخلة غير صحيحة. الرجاء إدخال كلمة المرور الصحيحة')</Script>");

                    //}
                    //Label2.Text = "The entered current password is incorrect. Please enter correct password.";
                    //this.ModalPopupExtender2.Show();
                }
            }

        }
        catch(Exception ex)
        {
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        TxtAddress.ReadOnly = TxtAbout.ReadOnly = TxtContact.ReadOnly = TxtEmail.ReadOnly = TxtH_Name.ReadOnly = TxtZip.ReadOnly = false;
        DdlCity.Enabled = true;
        BtnUpdate.Visible = false;
        BtnSaveChanges.Visible = true;
        BtnCancel.Visible = true;
    }
    protected void BtnSaveChanges_Click(object sender, EventArgs e)
    {






        try
        {
            var selctHos = from item in db.tbl_hospitalregs
                           where item.h_hakkimid == Session["hakkeemid_h"].ToString()
                           select item;
            string a = "";
            foreach (var ss in selctHos)
            {

                if (ss.h_email == TxtEmail.Text)
                {
                    a = "0";


                }
                else
                {
                    a = "1";
                }

            }
            
            if (a == "0")
            {
                foreach (var ss in selctHos)
                {




                    ss.h_name = TxtH_Name.Text;
                    ss.h_email = TxtEmail.Text;
                    ss.h_contact =  TxtContact.Text;
                    ss.h_city = DdlCity.SelectedItem.Text;
                    ss.h_zipcode = TxtZip.Text;
                    ss.h_about = TxtAbout.Text;
                    ss.h_address = TxtAddress.Text;
                }
                db.SubmitChanges();
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('Profile successfully updated')</Script>");
                BtnSaveChanges.Visible = false;
                BtnCancel.Visible = false;
                BtnUpdate.Visible = true;
                DdlCity.Enabled = false;
                TxtAddress.ReadOnly = TxtAbout.ReadOnly = TxtContact.ReadOnly = TxtEmail.ReadOnly = TxtH_Name.ReadOnly = TxtZip.ReadOnly = true;
                BindHospitalDetails();
                //Label2.Text = "Profile successfully updated.";
                //this.ModalPopupExtender2.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile successfully updated')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تحديث الملف الشخصي بنجاح')</Script>");
                //}
            }
            else
            {
                Random rd = new Random();
                int i = rd.Next(000000, 999999);
                Session["hosoptp"] = i.ToString();
                MailMessage mailmsg = new MailMessage();
                mailmsg.mail(TxtEmail.Text, "Your email is successfully changed with Hakkeem for being a verified email, enter the OTP. " + i, "Hospial Updation");

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();

                //this.ModalPopupExtender1.Show();

            }
        }
        catch(Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        BtnSaveChanges.Visible = false;
        BtnCancel.Visible = false;
        BtnUpdate.Visible = true;
        DdlCity.Enabled = false;
        TxtAddress.ReadOnly = TxtAbout.ReadOnly = TxtContact.ReadOnly = TxtEmail.ReadOnly = TxtH_Name.ReadOnly = TxtZip.ReadOnly = true;
        BindHospitalDetails();
    }
    protected void BtnChangFoto_Click(object sender, EventArgs e)
    {
        try
        {
            var selctHos = from item in db.tbl_hospitalregs
                           where item.h_hakkimid == Session["hakkeemid_h"].ToString()
                           select item;
            foreach(var ss in selctHos)
            {
                if(FileUpload1.HasFile)
                {
                    string extn = Path.GetExtension(FileUpload1.FileName).ToLower();
                    if (extn == ".jpg" || extn == ".jpeg")
                    {
                        Random rd = new Random();
                        int i = rd.Next(000000, 999999);
                        //  FileUpload1.SaveAs(Server.MapPath("~/Hospital images/" + i + FileUpload1.FileName));

                        ImageCompress imgCompress = ImageCompress.GetImageCompressObject;
                        imgCompress.GetImage = new System.Drawing.Bitmap(FileUpload1.FileContent);
                        imgCompress.Height = 300;
                        imgCompress.Width = 300;
                        imgCompress.Save(i + FileUpload1.FileName, Server.MapPath("~/Hospital images/"));



                        Image1.ImageUrl = "~/Hospital images/" + i + FileUpload1.FileName;
                        Image1.Visible = true;
                        ss.h_photo = "~/Hospital images/" + i + FileUpload1.FileName;
                        db.SubmitChanges();
                        //RegisterStartupScript("", "<Script Language=JavaScript>alert('Photo updated')</Script>");
                        BindHospitalDetails();
                        //Label2.Text = "Photo updated.";
                        //this.ModalPopupExtender2.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Photo updated')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تحديث الصورة')</Script>");
                        //}
                    }
                    else
                    {
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Image format is not supported.')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تنسيق الصورة غير متوافق')</Script>");
                        //}
                        //Label2.Text = "Image format is not supported.";
                        //this.ModalPopupExtender2.Show();
                    }
                }
            }
        }
        catch(Exception ex)
        {
        }
    }

    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {
        if (Session["hosoptp"].ToString() == TxtOTP.Text)
        {
            var selctHos = from item in db.tbl_hospitalregs
                           where item.h_hakkimid == Session["hakkeemid_h"].ToString()
                           select item;
            string s = "";
            if (TxtContact.Text.StartsWith("5") == true)
            {
               s = "+966";
                s = s + TxtContact.Text;


            }
            else
            {
                s = "+91";
                s = s + TxtContact.Text;
            }

            foreach (var ss in selctHos)
            {




                ss.h_name = TxtH_Name.Text;
                ss.h_email = TxtEmail.Text;
                ss.h_contact = s.ToString();
                ss.h_city = DdlCity.SelectedItem.Text;
                ss.h_zipcode = TxtZip.Text;
                ss.h_about = TxtAbout.Text;
                ss.h_address = TxtAddress.Text;
            }
            db.SubmitChanges();
            //RegisterStartupScript("", "<Script Language=JavaScript>alert('Profile successfully updated')</Script>");
            BtnSaveChanges.Visible = false;
            BtnCancel.Visible = false;
            BtnUpdate.Visible = true;
            DdlCity.Enabled = false;
            TxtAddress.ReadOnly = TxtAbout.ReadOnly = TxtContact.ReadOnly = TxtEmail.ReadOnly = TxtH_Name.ReadOnly = TxtZip.ReadOnly = true;
            BindHospitalDetails();
            //Label2.Text = "Profile successfully updated.";
            //this.ModalPopupExtender2.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile successfully updated')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تحديث الملف الشخصي بنجاح')</Script>");
            //}
        }
        else
        {
            //Label2.Text = "Error OTP";
            //this.ModalPopupExtender2.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Error OTP')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('خطأ في مكتب المدعي العام')</Script>");
            //}

        }
    }

    protected void BtnResendOTP_Click(object sender, EventArgs e)
    {
        TxtOTP.Text = "";
        Random rd = new Random();
        int i = rd.Next(000000, 999999);
        Session["hosotp"] = i.ToString();

        MailMessage mailmsg = new MailMessage();
        mailmsg.mail(TxtEmail.Text, "Your email is successfully changed with Hakkeem for being a verified email, enter the OTP. " + i, "Hospial Updation");

        //this.ModalPopupExtender1.Show();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        upModal.Update();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/SetHospitalLocation.aspx?l=ar-EG");
        //}
    }
}