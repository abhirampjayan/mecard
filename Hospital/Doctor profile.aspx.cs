using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using HelperClass;

public partial class Doctor_Doctor_profile : System.Web.UI.Page
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
        
        if(!IsPostBack)
        {
            LoadSpecialities();
            CheckLocation();
            profile();
        }

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
                    where item1.d_email == Session["doctor"].ToString()
                    select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
        if (query.Count() <= 0)
        {
            Response.Redirect("~/Doctor/SetLocation.aspx");
        }
    }

    public void profile()
    {
        var Query = from item in db.tbl_doctors where item.d_email == Session["doctor"].ToString() select item;

        DetailsView1.DataSource = Query;
        DetailsView1.DataBind();

        //LanguageLoad();

        foreach (var ss in Query)
        {
            LblDocPId.Text = ss.d_id.ToString();
            if (ss.d_photo == null)
            {
                Image1.ImageUrl = "~/Doctorimages/doctor.png";
                Button1.Text = "Upload photo";
            }
            else
            {
                Image1.ImageUrl = ss.d_photo;
                Button1.Text = "Change photo";
            }
        }
    }

    //private void LanguageLoad()
    //{
    //    Label Language = DetailsView1.FindControl("Language") as Label;
    //    Language.Text = "";
    //    List<string> langs = Languages();
    //    foreach (string ss in langs)
    //    {
    //        Language.Text += ss+",";
            
    //    }
    //}

    //public DataTable LoadNationality()
    //{

    //    DataTable dt = new DataTable();
    //    dt.Columns.AddRange(new DataColumn[] { new DataColumn("country")});

    //    foreach(var ss in query)
    //    {
    //        dt.Rows.Add(ss.d_country);
    //    }
    //    return dt;
    //}

    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        DetailsView1.ChangeMode(e.NewMode);
        
        profile();
    }

    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        var query = from item in db.tbl_doc_languages
                    where item.doc_id == LblDocPId.Text
                   
                    select item;
        db.tbl_doc_languages.DeleteAllOnSubmit(query);
        db.SubmitChanges();

        //string[] Langs2 = null;
        List<string> Langs = new List<string>();
        List<string> Langs2 = new List<string>();
        string name = (DetailsView1.FindControl("TextBoxname") as TextBox).Text;
        string clg = (DetailsView1.FindControl("TextBoxClg") as TextBox).Text;
        string edu = (DetailsView1.FindControl("TextBoxEdu") as TextBox).Text;
        string loc = (DetailsView1.FindControl("TextBoxLoc") as TextBox).Text;
        string exp = (DetailsView1.FindControl("TextBoxExp") as TextBox).Text;
        string spe = (DetailsView1.FindControl("DropDownListSpec") as DropDownList).SelectedItem.Text;
        //string lang = (DetailsView1.FindControl("TextBoxLang") as TextBox).Text;
        string hosafl = (DetailsView1.FindControl("TextBoxHosAfl") as TextBox).Text;
        string about = (DetailsView1.FindControl("TextBoxAbout") as TextBox).Text;
        string adrs1 = (DetailsView1.FindControl("TextBoxAdrs1") as TextBox).Text;
        string adrs2 = (DetailsView1.FindControl("TextBoxAdrs2") as TextBox).Text;
        TextBox dob = DetailsView1.FindControl("TextBoxDob") as TextBox;
        DropDownList DdlNationality = DetailsView1.FindControl("DdlNationality") as DropDownList;
        TextBox TextBoxLang = DetailsView1.FindControl("TextBoxLang") as TextBox;
        CheckBoxList Ckblanguages = DetailsView1.FindControl("Ckblanguages") as CheckBoxList;
        ListBox LsbLanguages = DetailsView1.FindControl("LsbLanguages") as ListBox;


        for (int i = 0; i < Ckblanguages.Items.Count; i++)
        {
            if (Ckblanguages.Items[i].Selected)
            {
                Langs.Add(Ckblanguages.Items[i].Text);
            }

        }
        for (int i = 0; i < LsbLanguages.Items.Count; i++)
        {
            if (LsbLanguages.Items[i].Selected)
            {
                if(!Langs.Contains(LsbLanguages.Items[i].Text))
                {
                    Langs.Add(LsbLanguages.Items[i].Text);
                }
            }

        }
        //}
        //if (TextBoxLang.Text !="")
        //{
        //    Langs2 = null;
        //    //Langs.Add(TextBoxLang.Text.Split(','));
        //    Langs2= TextBoxLang.Text.Split(',');
        //    foreach(string lan in Langs2)
        //    {
        //        Langs.Add(lan);
        //    }
        //}
        //else
        //{
        //    Langs2 = null;
        //}

        if (dob.Text!="")
        {
            int age = new DateTime((DateTime.Now - Convert.ToDateTime(dob.Text)).Ticks).Year;
            if(age>=18)
            {
                var Query = from item in db.tbl_doctors where item.d_email == Session["doctor"].ToString() select item;
                foreach (var ss in Query)
                {
                    ss.d_name = name;
                    ss.d_college = clg;
                    ss.d_education = edu;
                    ss.d_location = loc;
                    ss.d_experience = exp + " " + "Years";
                    ss.d_specialties = spe;
                    //ss.d_language = lang;
                    ss.d_hospital_afili = hosafl;
                    ss.d_about_you = about;
                    ss.d_address = adrs1;
                    ss.d_address2 = adrs2;
                    ss.d_dob = dob.Text;
                    ss.d_age = age;
                    ss.d_country = DdlNationality.Text;
                    foreach (var langs in Langs)
                    {
                            tbl_doc_language tdl = new tbl_doc_language()
                            {
                               d_Language= langs,
                                doc_id = ss.d_id.ToString()
                            };
                            db.tbl_doc_languages.InsertOnSubmit(tdl);    
                    }
                }
                db.SubmitChanges();
                DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
                profile();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile successfully updated')</Script>");
                //Label7.Text = "Profile successfully updated";
                //this.ModalPopupExtender4.Show();
            }
            else
            {
                //Label7.Text = "Your age must be 18 years and above !";
                //Label7.ForeColor = System.Drawing.Color.Red;
                //this.ModalPopupExtender4.Show();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Your age must be 18 years and above !')</Script>");
            }
            
        }
        else
        {
            var Query = from item in db.tbl_doctors where item.d_email == Session["doctor"].ToString() select item;
            foreach (var ss in Query)
            {
                ss.d_name = name;
                ss.d_college = clg;
                ss.d_education = edu;
                ss.d_location = loc;
                //ss.d_experience = exp + " " + "Years";
                ss.d_specialties = spe;
                //ss.d_language = lang;
                ss.d_hospital_afili = hosafl;
                ss.d_about_you = about;
                ss.d_address = adrs1;
                ss.d_address2 = adrs2;
                ss.d_dob = dob.Text;
                //ss.d_age = age;
                ss.d_country = DdlNationality.Text;
                foreach (var langs in Langs)
                {
                   
                        tbl_doc_language tdl = new tbl_doc_language()
                        {
                            d_Language = langs,
                            doc_id = ss.d_id.ToString()
                        };
                        db.tbl_doc_languages.InsertOnSubmit(tdl);
                    

                }
                //db.SubmitChanges();
            }
            db.SubmitChanges();
            DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
            profile();
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile successfully updated')</Script>");
            //Label1.Text = "Profile successfully updated";
            //this.ModalPopupExtender1.Show();
        }
        Langs.Clear();
        Langs2 = null;
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {


            var Query = from item in db.tbl_doctors where item.d_email == Session["doctor"].ToString() select item;
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
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Profile photo updated')</Script>");
                //Label2.Text = "Profile photo updated";
                //this.ModalPopupExtender2.Show();
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
            RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);
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
                        where item.doc_id == LblDocPId.Text
                        select item;
            if(query.Count() >0)
            {
                foreach(var ss in query)
                {
                    Languages.Add(ss.d_Language);
                }
            }
        }
        catch(Exception ex)
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
            if (Language !=null)
            { 
                Language.Text = "";
            List<string> langs = Languages();
            foreach (string ss in langs)
            {
                Language.Text += ss + ",";
            }
        }
        }
        if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
        {
            ListBox LsbLanguages = DetailsView1.FindControl("LsbLanguages") as ListBox;
            DropDownList DdlNationality = DetailsView1.FindControl("DdlNationality") as DropDownList;
            CheckBoxList Ckblanguages = DetailsView1.FindControl("Ckblanguages") as CheckBoxList;
            if(Ckblanguages !=null)
            {
                Ckblanguages.DataSource = Languages();
                Ckblanguages.DataBind();
                for(int i=0;i <Ckblanguages.Items.Count; i++)
                {
                    Ckblanguages.Items[i].Selected = true;
                }
            }
            if(LsbLanguages !=null)
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
                catch(Exception ex)
                {

                }
            }
        }
        
    }


    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    string names = "";
    //    for (int i = 0; i < drpDemo.Items.Count;i++)
    //    {
    //        if(drpDemo.Items[i].Selected)
    //        {
    //             names += drpDemo.Items[i].Text + ",";
    //        }
            
    //    }
    //    //string demo = drpDemo.Value;
    //    //RegisterStartupScript("", "<Script Language=JavaScript>alert("+drpDemo.Text+")</Script>");
    //}
}