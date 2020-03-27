using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using HelperClass;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class Doctor_Doctor_profile : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    DataSet dts = new DataSet();
    secure obj = new secure();

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
        //    this.MasterPageFile = "~/Doctor/ArabicMasterPage.master";
        //}
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
        con.Open();
        if (!IsPostBack)
        {

            LoadSpecialities();
            CheckLocation();
            profile();
            fill();
            consultation_fee();
           // FillCity();
        }
       
    }

   

    public void fill()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select certi,hakkimid,id,certi as certiname from tbl_doctor_certificate where hakkimid='" + Session["hakkeemid_d"].ToString() + "'", con);

        dts.Clear();
        adpt.Fill(dts);
        DataList1.DataSource = dts;
        DataList1.DataBind();

    }
    public void LoadSpecialities()
    {
        //try
        //{
        //    var query = from item in db.tbl_specialities
        //                select item;
        //    if (query.Count() > 0)
        //    {
        //        specialty.DataSource = query;
        //        specialty.DataTextField = "Specialities";
        //        specialty.DataValueField = "id";
        //        specialty.DataBind();
        //    }

        //}
        //catch (Exception ex)
        //{
        //}
    }
    public void CheckLocation()
    {
        var query = from item in db.tbl_doctor_locations
                    join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                    where item1.d_hakkimid == Session["hakkeemid_d"].ToString()
                    select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
        if (query.Count() <= 0)
        {
            Response.Redirect("~/Doctor/SetLocation.aspx");
        }
    }

    public void profile()
    {
        var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;

        DetailsView1.DataSource = Query;
        DetailsView1.DataBind();


        //LanguageLoad();

        foreach (var ss in Query)
        {
            LblDocPId.Text = ss.d_id.ToString();
            if (ss.d_photo == null)
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
                Image1.ImageUrl = ss.d_photo;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Button1.Text = "Change photo";
                //}
                //else
                //{
                //    Button1.Text = "تغيير الصورة";
                //}
            }
        }
       
    }
    public void consultation_fee()
    {
        var fee1 = from item in db.tbl_fees where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
        if (fee1.Count() > 0)
        {
            foreach (var f in fee1)
            {
                Label2.Text = f.rate;
                fee.Text = f.rate;
            }
        }
        else
        {
            Label2.Text = "00.00";
        }
        
    }


    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        DetailsView1.ChangeMode(e.NewMode);

        profile();
    }

    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {

        var query = from item in db.tbl_doc_languages
                    where item.doc_id == Session["hakkeemid_d"].ToString()
                    select item;
        db.tbl_doc_languages.DeleteAllOnSubmit(query);
        db.SubmitChanges();

        //string[] Langs2 = null;
        List<string> Langs = new List<string>();
        List<string> Langs2 = new List<string>();
        string name = (DetailsView1.FindControl("TextBoxname") as TextBox).Text;
        //  name = "N" + "'" + (DetailsView1.FindControl("TextBoxname") as TextBox).Text+"'" ;
        string clg = (DetailsView1.FindControl("TextBoxClg") as TextBox).Text;
        string edu = (DetailsView1.FindControl("TextBoxEdu") as TextBox).Text;
        // string loc = (DetailsView1.FindControl("TextBoxLoc") as TextBox).Text;
        // DropDownList dl_city = DetailsView1.FindControl("dl_city") as DropDownList;
        string loc = (DetailsView1.FindControl("dl_city") as DropDownList).SelectedItem.Text;
        string exp = (DetailsView1.FindControl("TextBoxExp") as TextBox).Text;
        DropDownList ddl1 = DetailsView1.FindControl("DropDownList1") as DropDownList;
        string spe = (DetailsView1.FindControl("DropDownListSpec") as DropDownList).SelectedItem.Text;
        //string lang = (DetailsView1.FindControl("TextBoxLang") as TextBox).Text;
        string hosafl = (DetailsView1.FindControl("TextBoxHosAfl") as TextBox).Text;
        string about = (DetailsView1.FindControl("TextBoxAbout") as TextBox).Text;
        string adrs1 = (DetailsView1.FindControl("TextBoxAdrs1") as TextBox).Text;
        //string adrs1 = (DetailsView1.FindControl("TextBoxname") as TextBox).Text; 
        //string adrs2 = (DetailsView1.FindControl("TextBoxAdrs2") as TextBox).Text;
        TextBox dob = DetailsView1.FindControl("TextBoxDob") as TextBox;
        DropDownList DdlNationality = DetailsView1.FindControl("DdlNationality") as DropDownList;
        TextBox TextBoxLang = DetailsView1.FindControl("TextBoxLang") as TextBox;
        CheckBoxList Ckblanguages = DetailsView1.FindControl("Ckblanguages") as CheckBoxList;
        ListBox LsbLanguages = DetailsView1.FindControl("LsbLanguages") as ListBox;


        for (int i = 0; i < Ckblanguages.Items.Count; i++)
        {
            if (Ckblanguages.Items[i].Selected)
            {
                //var output = input.Remove(input.Length - 1);
                if (Langs.Contains(Ckblanguages.Items[i].Text.Remove(Ckblanguages.Items[i].Text.Length-1)))
                { }
                else
                {
                    Langs.Add(Ckblanguages.Items[i].Text.Remove(Ckblanguages.Items[i].Text.Length - 1));
                }
            }

        }
        for (int i = 0; i < LsbLanguages.Items.Count; i++)
        {
            if (LsbLanguages.Items[i].Selected)
            {
                //if (!Langs.Contains(LsbLanguages.Items[i].Text))
                //{
                if (Langs.Contains(LsbLanguages.Items[i].Text))
                { }
                else
                {
                    Langs.Add(LsbLanguages.Items[i].Text);
                }
                //}
            }

        }
        string language = "";
     for(int i=0;i<Langs.Count();i++)
        {
            if(language=="")
            {
                language = Langs[i].ToString();
            }
            else
            {
                language += ","+Langs[i].ToString();
            }
        }
        //language = Langs.Distinct();


        if (dob.Text != "")
        {
            try
            {
                int age = new DateTime((DateTime.Now - Convert.ToDateTime(dob.Text)).Ticks).Year;
                //string dateAsString = DateTime.Now.ToString("yyyy-MM-dd");
                //DateTime TDate = Convert.ToDateTime(dateAsString);
                //DateTime cuDate = Convert.ToDateTime(dob.Text);
                //if (TDate >= cuDate)
                //{
                    if (age >= 18)
                    {
                        var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
                        foreach (var ss in Query)
                        {

                            ss.d_name = name;

                            ss.d_college = clg;
                            ss.d_education = edu;
                            ss.d_location = loc;
                            ss.d_experience = exp /*+ " " + ddl1.SelectedItem.Text*/;
                            ss.d_specialties = spe;
                            ss.d_language = language;
                            ss.d_hospital_afili = hosafl;
                            ss.d_about_you = about;
                            ss.d_address = adrs1;
                            //ss.d_address2 = adrs2;
                            ss.d_dob = dob.Text;
                            ss.d_age = age;
                            ss.d_country = DdlNationality.Text;
                            foreach (var langs in Langs)
                            {
                                tbl_doc_language tdl = new tbl_doc_language()
                                {
                                    d_Language = langs,
                                    doc_id = Session["hakkeemid_d"].ToString(),
                                };
                                db.tbl_doc_languages.InsertOnSubmit(tdl);
                                db.SubmitChanges();
                            }
                        }
                        db.SubmitChanges();
                        Langs.Clear();
                        DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
                        profile();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile successfully updated')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تحديث الملف الشخصي بنجاح')</Script>");
                        //}
                        //Label7.Text = "Profile successfully updated";
                        //this.ModalPopupExtender4.Show();
                    }
                    
                    else
                    {
                        //Label7.Text = "Your age must be 18 years and above !";
                        //Label7.ForeColor = System.Drawing.Color.Red;
                        //this.ModalPopupExtender4.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be 18 years and above !')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك 18 عاما فما فوق!')</Script>");

                        //}
                    }
                //}
                //else
                //{
                //    //Label7.Text = "Your age must be 18 years and above !";
                //    //Label7.ForeColor = System.Drawing.Color.Red;
                //    //this.ModalPopupExtender4.Show();
                //    if (Session["Language"].ToString() == "Auto")
                //    {
                //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be 18 years and above !')</Script>");
                //    }
                //    else
                //    {
                //        RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك 18 عاما فما فوق!')</Script>");

                //    }
                //}
            }
            catch (Exception ex)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be 18 years and above !')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب أن يكون عمرك 18 عاما فما فوق!')</Script>");

                //}
            }

        }
        else
        {
            var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
            foreach (var ss in Query)
            {
                ss.d_name = name;
                ss.d_college = clg;
                ss.d_education = edu;
                ss.d_location = loc; 
                ss.d_experience = exp /*+ " " + ddl1.SelectedItem.Text*/;
                ss.d_specialties = spe;
                ss.d_language = language;
                ss.d_hospital_afili = hosafl;
                ss.d_about_you = about;
                ss.d_address = adrs1;
                //ss.d_address2 = adrs2;
                ss.d_dob = dob.Text;
                //ss.d_age = age;
                ss.d_country = DdlNationality.Text;
                foreach (var langs in Langs)
                {

                    tbl_doc_language tdl = new tbl_doc_language()
                    {
                        d_Language = langs,
                        doc_id = Session["hakkeemid_d"].ToString(),
                    };
                    db.tbl_doc_languages.InsertOnSubmit(tdl);

                    db.SubmitChanges();
                }

            }
            db.SubmitChanges();
            Langs.Clear();
            DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile successfully updated')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تحديث الملف الشخصي بنجاح')</Script>");
            //}
            //Label1.Text = "Profile successfully updated";
            //this.ModalPopupExtender1.Show();
        }
        Langs.Clear();
        Langs2 = null;
        profile();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string ext = (Path.GetExtension(FileUpload1.FileName)).ToLower();
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" )
            {
                var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
                foreach (var ss in Query)
                {
                    Random rd = new Random();
                    int i = rd.Next(000000, 999999);
                    // FileUpload1.SaveAs(Server.MapPath("~/doctorimages/" + i + FileUpload1.FileName));

                    ImageCompress imgCompress = ImageCompress.GetImageCompressObject;
                    imgCompress.GetImage = new System.Drawing.Bitmap(FileUpload1.FileContent);
                    imgCompress.Height = 300;
                    imgCompress.Width = 300;
                    imgCompress.Save(i + FileUpload1.FileName, Server.MapPath("~/doctorimages/"));




                    Image1.ImageUrl = "../doctorimages/" + i + FileUpload1.FileName;
                    ss.d_photo = "../doctorimages/" + i + FileUpload1.FileName;
                    db.SubmitChanges();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile photo updated')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تحديث صورة الملف الشخصي')</Script>");
                    //}
                    //Label2.Text = "Profile photo updated";
                    //this.ModalPopupExtender2.Show();
                }
                Response.Redirect("doctor profile.aspx");
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Must Enter a Valid photo')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal(' يجب إدخال صورة صالحة')</Script>");
                //}
               
                //RegisterStartupScript("", "<Script Language=JavaScript>swal('Must Enter a Valid photo')</Script>");
            }
        }
    }

    public static List<string> CountryList()
    {
        //Creating list
        List<string> CultureList = new List<string>();

        //getting  the specific  CultureInfo from CultureInfo class
        CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        foreach (CultureInfo getCulture in getCultureInfo)
        {
            //creating the object of RegionInfo class
            RegionInfo GetRegionInfo = new RegionInfo(getCulture.Name);
            //adding each county Name into the arraylist
            if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
            {
                CultureList.Add(GetRegionInfo.EnglishName);
            }
        }
        //sorting array by using sort method to get countries in order
        CultureList.Sort();
        //returning country list
        return CultureList;
    }

    public List<string> Languages()
    {
        List<string> Languages = new List<string>();
        try
        {
            var query = from item in db.tbl_doc_languages
                        where item.doc_id == Session["hakkeemid_d"].ToString()
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    Languages.Add(ss.d_Language + " ");
                }
            }
        }
        catch (Exception ex)
        {

        }
        return Languages;
    }

    protected void DetailsView1_DataBound1(object sender, EventArgs e)
    {
        if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
        {
            //LanguageLoad();
            Label Language = DetailsView1.FindControl("Language") as Label;
            if (Language != null)
            {
                Language.Text = "";
                List<string> langs = Languages();

                foreach (string ss in langs)
                {
                    Language.Text += ss;
                }
            }
        }
        if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
        {
            DropDownList docspec = DetailsView1.FindControl("DropDownListSpec") as DropDownList;
            var Query1 = from item in db.tbl_specialities select item.Specialities;
            docspec.DataSource = Query1;
            docspec.DataBind();

            DropDownList doccity = DetailsView1.FindControl("dl_city") as DropDownList;

            SqlDataAdapter adpt = new SqlDataAdapter("select * from tbl_cities order by city", con);
            DataSet dts = new DataSet();
            dts.Clear();
            adpt.Fill(dts);

            doccity.DataSource = dts;
            doccity.DataTextField = "City";
            doccity.DataValueField = "id";
            doccity.DataBind();


            //var Query2 = from item in db.tbl_cities select item.City;



            //doccity.DataSource = Query2;
            //doccity.DataBind();

            try
            {
                if (docspec.Items.Contains(docspec.Items.FindByValue(DataBinder.Eval(
                     DetailsView1.DataItem, "d_specialties").ToString())))
                {
                    docspec.SelectedIndex = docspec.Items.IndexOf(
                      docspec.Items.FindByValue(DataBinder.Eval(DetailsView1.DataItem,
                      "d_specialties").ToString()));
                }
                if (doccity.Items.Contains(doccity.Items.FindByText(DataBinder.Eval(
                    DetailsView1.DataItem, "d_location").ToString())))
                {
                    doccity.SelectedIndex = doccity.Items.IndexOf(
                      doccity.Items.FindByText(DataBinder.Eval(DetailsView1.DataItem,
                      "d_location").ToString()));
                }
            }
            catch (Exception ex)
            {

            }





            ListBox LsbLanguages = DetailsView1.FindControl("LsbLanguages") as ListBox;
            DropDownList DdlNationality = DetailsView1.FindControl("DdlNationality") as DropDownList;
            CheckBoxList Ckblanguages = DetailsView1.FindControl("Ckblanguages") as CheckBoxList;
            if (Ckblanguages != null)
            {
                Ckblanguages.DataSource = Languages();
                Ckblanguages.DataBind();
                for (int i = 0; i < Ckblanguages.Items.Count; i++)
                {
                    Ckblanguages.Items[i].Selected = true;
                }
            }
            if (LsbLanguages != null)
            {
                LsbLanguages.Attributes.Add("data-placeholder", "Choose languages");
                LsbLanguages.CssClass = "form-control select2";

            }

            if (DdlNationality != null)
            {
                DdlNationality.DataSource = CountryList();
                DdlNationality.DataBind();
                try
                {
                    if (DdlNationality.Items.Contains(DdlNationality.Items.FindByValue(DataBinder.Eval(
                         DetailsView1.DataItem, "d_country").ToString())))
                    {
                        DdlNationality.SelectedIndex = DdlNationality.Items.IndexOf(
                          DdlNationality.Items.FindByValue(DataBinder.Eval(DetailsView1.DataItem,
                          "d_country").ToString()));
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

    }



    protected void Button2_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
        foreach (var ss in Query)
        {
            if (ss.d_password == obj.EnryptString(TextBox1.Text))
            {
                ss.d_password = obj.EnryptString(TextBox3.Text);
                db.SubmitChanges();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Password changed successfully')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير الرقم السري بنجاح')</Script>");
                //}
                //Label13.Text = "Password changed successfully";
                //this.ModalPopupExtender8.Show();
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Current password does not match')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('كلمة المرور الحالية غير متطابقة')</Script>");
                //}

                //Label14.Text = "Current password doesn't match";
                //this.ModalPopupExtender9.Show();

            }
        }
    }



    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    //if (FileUpload2.HasFiles)
    //    //{



    //                HttpFileCollection hfc = Request.Files;
    //                for (int i = 0; i < hfc.Count; i++)
    //                {
    //                    HttpPostedFile hpf = hfc[i];
    //                    if (hpf.ContentLength > 0)
    //        {
    //            Random rd = new Random();
    //            int ri = rd.Next(000000, 999999);
    //            hpf.SaveAs(Server.MapPath("..\\doctorcerti") + "\\" +ri+ System.IO.Path.GetFileName(hpf.FileName));
    //            //var Query = from item in db.tbl_doctor_certificates where item.hakkimid == Session["hakkeemid_d"].ToString() select item;
    //            //if (Query.Count() > 0)
    //            //{
    //            //    foreach (var ss in Query)
    //            //    {
    //            //        ss.certi = "../doctorcerti/" + ri + System.IO.Path.GetFileName(hpf.FileName);
    //            //        db.SubmitChanges();

    //            //    }
    //            //}
    //            //else
    //            //{


    //                tbl_doctor_certificate tb = new tbl_doctor_certificate()
    //                {
    //                    hakkimid = Session["hakkeemid_d"].ToString(),
    //                    certi = ri + hpf.FileName,

    //                };
    //                db.tbl_doctor_certificates.InsertOnSubmit(tb);
    //                db.SubmitChanges();
    //                //Label2.Text = "Profile photo updated";
    //                //this.ModalPopupExtender2.Show();
    //            //}
    //        }

    //    }
    //    //Label2.Text = "Profile photo updated";
    //    //this.ModalPopupExtender2.Show();
    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile photo updated')</Script>");

    //    fill();
    //    //}
    //}

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            SqlCommand com = new SqlCommand("delete from tbl_doctor_certificate where id='" + e.CommandArgument.ToString() + "'", con);
            com.ExecuteNonQuery();
        }
        if (e.CommandName == "view")
        {
            Response.Redirect("../doctorcerti/" + e.CommandArgument.ToString());
        }
        fill();
    }

    protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        string fileName = Path.GetFileName(e.FileName);
        string filepath = "..\\doctorcerti" + "\\" + fileName;




        tbl_doctor_certificate tb = new tbl_doctor_certificate()
        {
            hakkimid = Session["hakkeemid_d"].ToString(),
            certi = fileName,

        };
        db.tbl_doctor_certificates.InsertOnSubmit(tb);
        db.SubmitChanges();

        AjaxFileUpload1.SaveAs(Server.MapPath(filepath));

        //RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile photo updated')</Script>");


    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor profile.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor profile.aspx?l=ar-EG");
        //}
    }


    protected void Button3_Click(object sender, EventArgs e)
    {

        var fees = from item in db.tbl_fees where item.d_hakkimid == Session["hakkeemid_d"].ToString() select item;
        if (fees.Count() > 0)
        {
            foreach (var ff in fees)
            {
                ff.rate = fee.Text + " " + DropDownList1.SelectedItem.Text;
            }
            db.SubmitChanges();
        }
        else
        {
            tbl_fee tf = new tbl_fee()
            {
                d_hakkimid = Session["hakkeemid_d"].ToString(),
                rate = fee.Text + " " + DropDownList1.SelectedItem.Text,
            };
            db.tbl_fees.InsertOnSubmit(tf);
            db.SubmitChanges();
           
        }
        consultation_fee();
        //if (Session["Language"].ToString() == "Auto")
        //{
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully set your consultation fee. Thank you!')</Script>");
        //}
        //else
        //{
        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تعيين رسوم الاستشارة بنجاح. شكرا!')</Script>");
        //}
    }
}