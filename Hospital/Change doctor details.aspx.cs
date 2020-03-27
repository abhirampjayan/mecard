using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hospital_Today_appointments : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

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
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;

     
        if(!IsPostBack)
        {
            try
            {
                CheckLocation();
            }
            catch (Exception ex)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("~/Index/Hospita Login.aspx");
                //}
                //else
                //{
                //    Response.Redirect("~/Index/Hospita Login.aspx?l=ar-EG");
                //}
            }
            if (Session["Hos_Doc_Id"] == null)
            {
                Image1.Visible = false;
                FileUpload1.Visible = false;
                //    LinkButton4.Visible = false;
                Button1.Visible = false;
                doctor();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    DropDownList1.Items.Insert(0, "----Select doctor----");
                //}
                //else
                //{
                //    DropDownList1.Items.Insert(0, "----حدد الطبيب----");
                //}
            }
            else
            {
                LblDoctor.Text = Session["Hos_Doc_Id"].ToString();
                Image1.Visible = true;
                FileUpload1.Visible = true;
                doctor();
                selectedDoctor();
                data();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    DropDownList1.Items.Insert(0, "----Select doctor----");
                //}
                //else
                //{
                //    DropDownList1.Items.Insert(0, "----حدد الطبيب----");
                //}

            }
            if (Session["hakkeemid_h"] != null)
            {
            }

            else {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("~/Index/Hospita Login.aspx");
                //}
                //else
                //{
                //    Response.Redirect("~/Index/Hospita Login.aspx?l=ar-EG");
                //}
            }
        }
    }

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                    select new { item1.h_id, item.latitude };
       
            if (query.Count() <= 0)
            {
            //Label12.Text = "You must set your location";
            //ModalPopupExtender5.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب تعيين موقعك')</Script>");
            //}
        }
        //Label8.Text = "You must set your location";
        //ModalPopupExtender5.Show();


    }
    public void selectedDoctor()
    {
        var Query = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_email==Session["Hos_Doc_Id"].ToString() select item;
       
       foreach(var ss in Query)
       {
           for (int i = 0; i < DropDownList1.Items.Count; i++ )
           {
               if(DropDownList1.Items[i].Value.ToString()==ss.hd_email.ToString())
               {
                   DropDownList1.SelectedIndex = i;
               }
           }
       }
    }

    public void doctor()
    {
       

            var Query = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() select item;

        if (Query.Count() > 0)
        {
            DropDownList1.DataSource = Query;
            DropDownList1.DataValueField = "hd_email";
            DropDownList1.DataTextField = "hd_name";
            DropDownList1.DataBind();
        }
        else
        {
            DropDownList1.Visible = false;
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctors not Availabile')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الأطباء لا أفيلابيل')</Script>");
            //}
        }

    }
    public void data()
    {
        var Query = from item in db.tbl_hdoctors where item.hd_email == DropDownList1.SelectedItem.Value select item;
        DetailsView1.DataSource = Query;
        DetailsView1.DataBind();
        foreach (var ss in Query)
        {
            if (ss.hd_photo==null)
            {
                Image1.ImageUrl = "~/Doctorimages/doctor.png";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Button1.Text = "Upload photo";
                //}
                //else
                //{
                //    Button1.Text = "حمل الصورة";
                //}
            }
            else
            {
                Image1.ImageUrl = ss.hd_photo;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Button1.Text = "Change photo";
                //}
                //else
                //{
                //    Button1.Text = "غير الصوره";
                //}
            }
            
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DetailsView1.CurrentMode==DetailsViewMode.ReadOnly)
        {
            LblDoctor.Text = "";
            Image1.Visible = true;
            FileUpload1.Visible = true;
            Button1.Visible = true;
//           LinkButton4.Visible = true;
            DetailsView1.DataSource = null;
            DetailsView1.DataBind();
            data();
        }
        if(DetailsView1.CurrentMode == DetailsViewMode.Edit)
        {
            DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
            LblDoctor.Text = "";
            Image1.Visible = true;
            FileUpload1.Visible = true;
            Button1.Visible = true;
//           LinkButton4.Visible = true;
            DetailsView1.DataSource = null;
            DetailsView1.DataBind();
            data();
        }
      
       
       
    }

    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        DetailsView1.ChangeMode(e.NewMode);
        data();
    }

    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        string dqlfcn = (DetailsView1.FindControl("dqlfcn") as TextBox).Text;
        string dclg = (DetailsView1.FindControl("dclg") as TextBox).Text;
        string dabout = (DetailsView1.FindControl("dabout") as TextBox).Text;
        string dspec = (DetailsView1.FindControl("dspec") as DropDownList).SelectedItem.Text;
        string loc = (DetailsView1.FindControl("dl_city") as DropDownList).SelectedItem.Text;
        string dcontact = (DetailsView1.FindControl("dcontact") as TextBox).Text;
        string dadrs1 = (DetailsView1.FindControl("dpadrs") as TextBox).Text;
        string dadrs2 = (DetailsView1.FindControl("dtadrs") as TextBox).Text;
        TextBox dob = DetailsView1.FindControl("ddob") as TextBox;
        if(dob.Text!="")
        {
            try
            {
                int age = new DateTime((DateTime.Now - Convert.ToDateTime(dob.Text)).Ticks).Year;
                if (age >= 18)
                {
                    var Query = from item in db.tbl_hdoctors where item.hd_email == DropDownList1.SelectedItem.Value select item;

                    if (dcontact.StartsWith("5") == true)
                    {
                     dcontact=   "+966" + dcontact;
                    }
                    else
                    {
                        dcontact = "+91" + dcontact;
                    }
                    foreach (var ss in Query)
                    {
                        ss.hd_education = dqlfcn;
                        ss.hd_college = dclg;
                        ss.hd_about_you = dabout;
                        ss.hd_specialties = dspec;
                        ss.hd_location = loc;
                        ss.hd_contact = dcontact;
                        ss.hd_address = dadrs1;
                        ss.hd_address2 = dadrs2;
                        ss.hd_dob = dob.Text;
                        ss.hd_age = age;
                    }
                    db.SubmitChanges();
                    DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
                    data();
                }
                else
                {
                    //Label1.Text = "Your age must be 18 years and above";
                    //this.ModalPopupExtender1.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be 18 years and above')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك 18 سنة فأكثر')</Script>");
                    //}
                }
            }
            catch(Exception ex)
            {
                //Label1.Text = "Your age must be 18 years and above";
                //this.ModalPopupExtender1.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be 18 years and above')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك 18 سنة فأكثر')</Script>");
                //}
            }
           
        }
        else
        {
            var Query = from item in db.tbl_hdoctors where item.hd_email == DropDownList1.SelectedItem.Value select item;
            if (dcontact.StartsWith("5") == true)
            {
                dcontact = "+966" + dcontact;
            }
            else
            {
                dcontact = "+91" + dcontact;
            }
            foreach (var ss in Query)
            {
                ss.hd_education = dqlfcn;
                ss.hd_college = dclg;
                ss.hd_about_you = dabout;
                ss.hd_specialties = dspec;
                ss.hd_location = loc;
                ss.hd_contact = dcontact;
                ss.hd_address = dadrs1;
                ss.hd_address2 = dadrs2;
                ss.hd_dob = dob.Text;
                //ss.hd_age = age;
            }
            db.SubmitChanges();
            
            DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
            data();
        }
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (LblDoctor.Text =="")
        {
            if(DropDownList1.SelectedIndex >0)
            {
                if (FileUpload1.HasFile)
                {


                    string ext = (Path.GetExtension(FileUpload1.FileName)).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp")
                    {
                        var Query = from item in db.tbl_hdoctors where item.hd_email == DropDownList1.SelectedItem.Value select item;
                        foreach (var ss in Query)
                        {
                            Random rd = new Random();
                            int i = rd.Next(000000, 999999);
                            FileUpload1.SaveAs(Server.MapPath("../doctorimages/" + i + FileUpload1.FileName));
                            Image1.ImageUrl = "../doctorimages/" + i + FileUpload1.FileName;
                            ss.hd_photo = "../doctorimages/" + i + FileUpload1.FileName;
                            db.SubmitChanges();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile photo updated')</Script>");
                            //}
                            //else
                            //{
                            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تحديث صورة الملف الشخصي')</Script>");
                            //}
                            //Label1.Text = "Profile photo updated";
                            //this.ModalPopupExtender1.Show();

                        }
                    }
                    else
                    {
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Please choose a valid photo')</Script>");
                    }
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Please choose a photo')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء اختيار صورة')</Script>");
                    //}
                    //Label1.Text = "Please choose a photo";
                    //this.ModalPopupExtender1.Show();
                }
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select a doctor')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء اختيار طبيب')</Script>");
                //}
                //Label1.Text = "Please select a doctor";
                //this.ModalPopupExtender1.Show();
            }
        }
        else
        {
            if(FileUpload1.HasFile)
            {
            var Query = from item in db.tbl_hdoctors where item.hd_email == DropDownList1.SelectedItem.Value select item;
            foreach (var ss in Query)
            {
                Random rd = new Random();
                int i = rd.Next(000000, 999999);
                FileUpload1.SaveAs(Server.MapPath("../doctorimages/" + i + FileUpload1.FileName));
                Image1.ImageUrl = "../doctorimages/" + i + FileUpload1.FileName;
                ss.hd_photo = "../doctorimages/" + i + FileUpload1.FileName;
                db.SubmitChanges();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile photo updated')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تحديث صورة الملف الشخصي')</Script>");
                    //}
                    //Label1.Text = "Profile photo updated";
                    //this.ModalPopupExtender1.Show();
                }
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Please choose a photo')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء اختيار صورة')</Script>");
                //}
                //Label1.Text = "Please choose a photo";
                //this.ModalPopupExtender1.Show();
            }
        }
    }
    protected void BtnDeleteDoc_Click(object sender, EventArgs e)
    {
        //this.ModalPopupExtender4.Show();
        if (LblDoctor.Text == "")
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                var delt = from item in db.tbl_hdoctors
                           where item.hd_email == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString()
                           select item;
                db.tbl_hdoctors.DeleteAllOnSubmit(delt);
                db.SubmitChanges();
                Session["Hos_Doc_Id"] = null;
                Image1.Visible = false;
                FileUpload1.Visible = false;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor deleted sucsessfully')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم حذف الطبيب سوكسسفولي')</Script>");
                //}
                //Label10.Text = "Doctor deleted sucsessfully";
                //this.ModalPopupExtender2.Show();

            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select a doctor')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء اختيار طبيب')</Script>");
                //}
                //Label1.Text = "Please select a doctor";
                //this.ModalPopupExtender1.Show();
            }
        }
        else
        {
            var delt = from item in db.tbl_hdoctors
                       where item.hd_email == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString()
                       select item;
            db.tbl_hdoctors.DeleteAllOnSubmit(delt);
            db.SubmitChanges();
            Session["Hos_Doc_Id"] = null;
            Image1.Visible = false;
            FileUpload1.Visible = false;
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor deleted sucsessfully')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم حذف الطبيب سوكسسفولي')</Script>");
            //}
            //Label11.Text = "Doctor deleted sucessfully";
            //this.ModalPopupExtender3.Show();
        }

    }

    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/Change doctor details.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/Change doctor details.aspx?l=ar-EG");
        //}
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/Change doctor details.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/Change doctor details.aspx?l=ar-EG");
        //}
    }
    public List<string> LoadSpecialities()
    {
        List<string> specialities = new List<string>();
        try
        {
            var query = from item in db.tbl_specialities
                        select item;
            if (query.Count() > 0)
            {
               foreach(var ss in query)
                {
                    specialities.Add(ss.Specialities);
                }
            }

        }
        catch (Exception ex)
        {
        }
        return specialities;
    }


    protected void DetailsView1_DataBound(object sender, EventArgs e)
    {
        if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
        {
            DropDownList dspec = DetailsView1.FindControl("dspec") as DropDownList;
            DropDownList dcity = DetailsView1.FindControl("dl_city") as DropDownList;
            TextBox dcontact = DetailsView1.FindControl("dcontact") as TextBox;
            if(dcontact !=null)
            {
                try
                {
                    var Query = from item in db.tbl_hdoctors where item.hd_email == DropDownList1.SelectedItem.Value select item;
                    foreach (var ss in Query)
                    {
                        dcontact.Text = ss.hd_contact.Substring(4, 9);
                    }
                }
                catch(Exception ex)
                {

                }
            }

            if (dspec !=null)
            {
                dspec.CssClass = "form-control";
                dspec.DataSource = LoadSpecialities();
                dspec.DataBind();
                try
                {
                    if (dspec.Items.Contains(dspec.Items.FindByValue(DataBinder.Eval(
                           DetailsView1.DataItem, "hd_specialties").ToString())))
                    {
                        dspec.SelectedIndex = dspec.Items.IndexOf(
                          dspec.Items.FindByValue(DataBinder.Eval(DetailsView1.DataItem,
                          "hd_specialties").ToString()));
                    }
                }
                catch (Exception ex)
                {

                }
            }
            if (dcity != null)
            {
                dcity.CssClass = "form-control";
                dcity.DataSource = LoadCities();
                dcity.DataBind();
                try
                {
                    if (dcity.Items.Contains(dcity.Items.FindByValue(DataBinder.Eval(
                           DetailsView1.DataItem, "hd_location").ToString())))
                    {
                        dcity.SelectedIndex = dcity.Items.IndexOf(
                          dcity.Items.FindByValue(DataBinder.Eval(DetailsView1.DataItem,
                          "hd_location").ToString()));
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    protected void BtnResendOTP_Click(object sender, EventArgs e)
    {

    }
    public List<string> LoadCities()
    {
        List<string> citys = new List<string>();
        try
        {
            var query = from item in db.tbl_cities
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    citys.Add(ss.City);
                }
            }

        }
        catch (Exception ex)
        {
        }
        return citys;
    }
    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    //if (LblDoctor.Text == "")
    //    //{
    //    //    if (DropDownList1.SelectedIndex > 0)
    //    //    {
    //    //        var delt = from item in db.tbl_hdoctors
    //    //                   where item.hd_email == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString()
    //    //                   select item;
    //    //        db.tbl_hdoctors.DeleteAllOnSubmit(delt);
    //    //        db.SubmitChanges();
    //    //        Session["Hos_Doc_Id"] = null;
    //    //        Image1.Visible = false;
    //    //        FileUpload1.Visible = false;
    //    //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor deleted sucsessfully')</Script>");
    //    //        //Label10.Text = "Doctor deleted sucsessfully";
    //    //        //this.ModalPopupExtender2.Show();

    //    //    }
    //    //    else
    //    //    {
    //    //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select a doctor')</Script>");
    //    //        //Label1.Text = "Please select a doctor";
    //    //        //this.ModalPopupExtender1.Show();
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    var delt = from item in db.tbl_hdoctors
    //    //               where item.hd_email == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString()
    //    //               select item;
    //    //    db.tbl_hdoctors.DeleteAllOnSubmit(delt);
    //    //    db.SubmitChanges();
    //    //    Session["Hos_Doc_Id"] = null;
    //    //    Image1.Visible = false;
    //    //    FileUpload1.Visible = false;
    //    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor deleted sucsessfully')</Script>");
    //    //    //Label11.Text = "Doctor deleted sucessfully";
    //    //    //this.ModalPopupExtender3.Show();
    //    //}
    //}

    protected void BtnResendOTP_Click1(object sender, EventArgs e)
    {

    }

    protected void Button6_Click(object sender, EventArgs e)
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

    protected void Button7_Click(object sender, EventArgs e)
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

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        //this.ModalPopupExtender4.Show();
        if (LblDoctor.Text == "")
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                var delt = from item in db.tbl_hdoctors
                           where item.hd_email == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString()
                           select item;
                db.tbl_hdoctors.DeleteAllOnSubmit(delt);
                db.SubmitChanges();
                Session["Hos_Doc_Id"] = null;
                Image1.Visible = false;
                FileUpload1.Visible = false;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor deleted sucsessfully')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم حذف الطبيب سوكسسفولي')</Script>");
                //}
                //Label10.Text = "Doctor deleted sucsessfully";
                //this.ModalPopupExtender2.Show();

            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select a doctor')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء اختيار طبيب')</Script>");
                //}
                //Label1.Text = "Please select a doctor";
                //this.ModalPopupExtender1.Show();
            }
        }
        else
        {
            var delt = from item in db.tbl_hdoctors
                       where item.hd_email == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString()
                       select item;
            db.tbl_hdoctors.DeleteAllOnSubmit(delt);
            db.SubmitChanges();
            Session["Hos_Doc_Id"] = null;
            Image1.Visible = false;
            FileUpload1.Visible = false;
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor deleted sucsessfully')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم حذف الطبيب سوكسسفولي')</Script>");
            //}
            //Label11.Text = "Doctor deleted sucessfully";
            //this.ModalPopupExtender3.Show();
        }
    }
}