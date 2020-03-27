using HelperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelperClass;
public partial class User_User_account : System.Web.UI.Page
{
    secure obj = new secure();
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();

    protected override void InitializeCulture()
    {
        //Session["Speciality"] = "Auto";
        //string culture = "Auto";
        //try
        //{
        //    culture = Request.QueryString["l"].ToString();
        //    Session["Speciality"] = culture;
        //}
        //catch (Exception ex)
        //{ }
        //// string culture = Session["Speciality"].ToString();
        //if (string.IsNullOrEmpty(culture))
        //    culture = "Auto";
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


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            User();
            shared_documents();

          
            //    BtnUpdateFoto.Enabled = false;
            //BtnUpdateFoto.CssClass = "btn btn-sm btn-default disabled";
          

        }

    }

    public void User()
    {
        var Query = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
        string email = "";
        string cno = "";
        foreach (var ss in Query)
        {
            email = ss.email;
            cno = ss.contact;
            if (ss.photo == null)
            {
                //PnlPhoto.Visible = false;
                //PnlChangePhoto.Visible = true;
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    BtnUpdateFoto.Text = "Upload";
                //}
                //else
                //{
                //    BtnUpdateFoto.Text = "تحميل";
                //}
            }
            else
            {
                //PnlPhoto.Visible = true;
                //PnlChangePhoto.Visible = false;
                ImgUserPhoto.ImageUrl = ss.photo;
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    BtnUpdateFoto.Text = "Update photo";
                //}
                //else
                //{
                //    BtnUpdateFoto.Text = "تحديث الصورة";
                //}
            }



        }


        DetailsView1.DataSource = Query;
        DetailsView1.DataBind();






        try
        {
            Label l = (Label)DetailsView1.FindControl("Label2");
            l.Text = obj.DecryptString(email);

            Label lc = (Label)DetailsView1.FindControl("Label3");
            lc.Text = obj.DecryptString(cno);

            Label lcc = (Label)DetailsView1.FindControl("Label19");
            if (lc.Text.StartsWith("5") == true)
            {
                lcc.Text = "+966";
            }
            else
            {
                lcc.Text = "+91";

            }


            //lc.Text = lc.Text.ToString().Substring(4, 9);

            //TextBox tc = (TextBox)DetailsView1.FindControl("TextContact");
            //tc.Text = lc.Text;

        }
        catch (Exception ex)
        {
            TextBox l = (TextBox)DetailsView1.FindControl("TextEmail");
            l.Text = obj.DecryptString(email);

            TextBox lc = (TextBox)DetailsView1.FindControl("TextContact");
            lc.Text = obj.DecryptString(cno);


            Label lcc = (Label)DetailsView1.FindControl("Label19");
            if (lc.Text.StartsWith("5") == true)
            {
                lcc.Text = "+966";
            }
            else
            {
                lcc.Text = "+91";

            }

            //lc.Text = lc.Text.ToString().Substring(4, 9);
        }
        //var Query1 = from item in db.tbl_doctor_appointments where item.c_id == Session["hakkemid_u"].ToString() select item;
        //GridView1.DataSource = Query1;
        //GridView1.DataBind();
        //foreach(GridViewRow gr in GridView1.Rows)
        //{
        //    Label lbl7 = gr.FindControl("Label7") as Label;
        //    Label lbl8 = gr.FindControl("Label8") as Label;
        //    Label lbl11 = gr.FindControl("Label11") as Label;
        //    Label lbl12 = gr.FindControl("Label12") as Label;
        //    LinkButton lnk3 = gr.FindControl("LinkButton3") as LinkButton;
        //    LinkButton lnk2 = gr.FindControl("LinkButton2") as LinkButton;
        //    var Query2 = from item in db.tbl_doctors where item.d_hakkimid == lbl7.Text select item;
        //    foreach(var ss in Query2)
        //    {
        //        lbl8.Text = ss.d_name;
        //    }
        //    if(lbl12.Text=="1")
        //    {
        //        lnk3.Text = "Confirm";
        //        lnk3.Enabled = false;
        //        lnk3.ForeColor = System.Drawing.Color.Green;
        //    }

        //}
    }


    public void shared_documents()
    {

        var History = from item in db.view_shared_histories where item.u_id == Session["hakkemid_u"].ToString() select item;
        GridView1.DataSource = History;
        GridView1.DataBind();
        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label lbl12 = gr.FindControl("Label12") as Label;
            Label lbl7 = gr.FindControl("Label7") as Label;
            Label lbl11 = gr.FindControl("Label11") as Label;
            Label lbl8 = gr.FindControl("Label8") as Label;
            Label lbl9 = gr.FindControl("Label9") as Label;
            lbl9.Text = DateTime.Parse(lbl9.Text).ToShortDateString();
            if (lbl12.Text == "")
            {
                var Query = (from item in db.tbl_doctors where item.d_hakkimid == lbl7.Text select item).First();
                lbl11.Text = Query.d_name.ToString();
                lbl8.Text = "------";
                lbl12.Visible = false;
            }
            else
            {
                var Query = (from item in db.tbl_hdoctors where item.hd_email == lbl7.Text select item).First();
                lbl11.Text = Query.hd_name.ToString();
                var Query1 = (from item in db.tbl_hospitalregs where item.h_hakkimid == lbl12.Text select item).First();
                lbl8.Text = Query1.h_name.ToString();
                lbl12.Visible = false;
            }
        }
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("doctorname"), new DataColumn("hakkeemid") });
        DataTable dt1 = new DataTable();
        dt1.Columns.AddRange(new DataColumn[] { new DataColumn("doctorname"), new DataColumn("docemail") });
        List<string> doc = new List<string>();
        List<string> hosdoc = new List<string>();

        var userapmnt = from item in db.tbl_hos_appmnt_histories where item.u_id == Session["hakkemid_u"].ToString() && item.h_id == null select item.d_id;
        foreach (var ua in userapmnt)
        {
            var doctor = from item in db.tbl_doctors where item.d_hakkimid == ua && item.d_status == 1 select item;
            foreach (var ss in doctor)
            {
                dt.Rows.Add(ss.d_name, ss.d_hakkimid);
            }
        }


        var huserapmnt = from item in db.tbl_hos_appmnt_histories where item.u_id == Session["hakkemid_u"].ToString() && item.h_id != null select item.d_id;
        foreach (var hua in huserapmnt)
        {
            var hdoctor = from item in db.tbl_hdoctors where item.hd_email == hua && item.hd_status == 1 select item;
            foreach (var ss in hdoctor)
            {
                dt1.Rows.Add(ss.hd_name, ss.hd_email);
            }
        }


        DropDownList1.DataSource = dt;
        DropDownList1.DataValueField = "hakkeemid";
        DropDownList1.DataTextField = "doctorname";
        DropDownList1.DataBind();
        //if (Session["Speciality"].ToString() == "Auto")
        //{
            DropDownList1.Items.Insert(0, "---Hakkeem doctor---");
        //}
        //else
        //{
        //    DropDownList1.Items.Insert(0, "---هكيم طبيب---");
        //}
        DropDownList2.DataSource = dt1;
        DropDownList2.DataValueField = "docemail";
        DropDownList2.DataTextField = "doctorname";
        DropDownList2.DataBind();
        //if (Session["Speciality"].ToString() == "Auto")
        //{
            DropDownList2.Items.Insert(0, "---Hospital doctor---");
        //}
        //else
        //{
        //    DropDownList2.Items.Insert(0, "---طبيب المستشفى---");
        //}
        var Report = from item in db.view_shared_reports where item.u_id == Session["hakkemid_u"].ToString() select item;
        GridView2.DataSource = Report;
        GridView2.DataBind();
        foreach (GridViewRow gr in GridView2.Rows)
        {
            Label lbl13 = gr.FindControl("Label13") as Label;
            Label lbl14 = gr.FindControl("Label14") as Label;
            Label lbl15 = gr.FindControl("Label15") as Label;
            Label lbl16 = gr.FindControl("Label16") as Label;
            Label lbl17 = gr.FindControl("Label17") as Label;
            Label lbl18 = gr.FindControl("Label18") as Label;
            Label lbl19 = gr.FindControl("Label19") as Label;
            lbl15.Text = DateTime.Parse(lbl15.Text).ToShortDateString();
            if (lbl14.Text == "" || lbl14.Text == null)
            {
                var Query = (from item in db.tbl_doctors where item.d_hakkimid == lbl13.Text select item).First();
                lbl17.Text = Query.d_name.ToString();
                lbl18.Text = "-------";
            }
            else
            {
                var Query = (from item in db.tbl_hospitalregs where item.h_hakkimid == lbl14.Text select item).First();
                lbl18.Text = Query.h_name.ToString();
                var Query1 = (from item in db.tbl_hdoctors where item.hd_email == lbl13.Text select item).First();
                lbl17.Text = Query1.hd_name.ToString();
            }



        }
        if (GridView1.Rows.Count > 0)
        {
            sharehistory.Visible = true;
        }
        else
        {
            sharehistory.Visible = false;
        }

    }



    protected void BtnUpdateFoto_Click(object sender, EventArgs e)
    {

        try
        {
            if (FileUpload1.HasFile)
            {
                string extn = Path.GetExtension(FileUpload1.FileName).ToLower();
                if (extn == ".jpg" || extn == ".jpeg" || extn == ".png")
                {
                    Random rd = new Random();
                    int i = rd.Next(000000, 999999);

                    var Query = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;

                    // FileUpload1.SaveAs(Server.MapPath("~/UserImages/" + i + FileUpload1.FileName));

                    ImageCompress imgCompress = ImageCompress.GetImageCompressObject;
                    imgCompress.GetImage = new System.Drawing.Bitmap(FileUpload1.FileContent);
                    imgCompress.Height = 300;
                    imgCompress.Width = 300;
                    imgCompress.Save(i + FileUpload1.FileName, Server.MapPath("~/UserImages/"));


                    ImgUserPhoto.ImageUrl = "~/UserImages/" + i + FileUpload1.FileName;

                    foreach (var ss in Query)
                    {
                        ss.photo = "~/UserImages/" + i + FileUpload1.FileName;
                    }
                    db.SubmitChanges();

                    //BtnChangePhoto.Visible = true;
                    User();
                    //Label2.Text = "Photo updated";
                    //this.ModalPopupExtender3.Show();
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Photo updated')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تحديث الصورة')</Script>");
                    //}
                }
                else
                {

                    //Label2.Text = "Image format is not supported.";
                    //this.ModalPopupExtender3.Show();
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Image format is not supported.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تنسيق الصورة غير متوافق.')</Script>");
                    //}
                }
            }
            else
            {
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select an image')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء تحديد صورة')</Script>");
                //}
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        //PnlChangePhoto.Visible = false;
        //PnlPhoto.Visible = true;
        //BtnChangePhoto.Visible = true;
    }
    protected void BtnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            var query = from item in db.tbl_signups
                        where item.u_hakkimid == Session["hakkemid_u"].ToString()
                        select item;
            foreach (var ss in query)
            {
                if (ss.password == obj.EnryptString(TxtCurrentPassword.Text))
                {
                    ss.password = obj.EnryptString(TxtConfirmNewPassword.Text);
                    db.SubmitChanges();
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully password changed')</Script>");
                    //}
                    //else
                    //{
                    //    this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير كلمة المرور بنجاح')</Script>");
                    //}
                    //Label2.Text = "Succesfully password changed";
                    //this.ModalPopupExtender3.Show();
                }
                else
                {
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('The current password is incorrect. Please enter correct password.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('إدخال كلمة المرور الحالية غير صحيحة. الرجاء إدخال كلمة المرور الصحيحة.')</Script>");

                    //}
                    //Label2.Text = "The entered current password is incorrect. Please enter correct password.";
                    //this.ModalPopupExtender3.Show();
                }
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        DetailsView1.ChangeMode(e.NewMode);
        User();
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
    protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        string dob = (DetailsView1.FindControl("TextDob") as TextBox).Text;

        string name = (DetailsView1.FindControl("TextName") as TextBox).Text;
        string email = (DetailsView1.FindControl("TextEmail") as TextBox).Text;
        string contact = (DetailsView1.FindControl("TextContact") as TextBox).Text;


        if (contact[0].ToString() != "5")
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('The Phone No starts with 5')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الهاتف لا يبدأ مع 5')</Script>");
            //}
        }
        else
        {
            if (contact.ToString().Count() != 9)

            {
                //if (Session["Speciality"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('The Phone No must be 9 digits')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الهاتف لا يجب أن يكون 9 أرقام')</Script>");
                //}
            }
            else
            {
                // string country = (DetailsView1.FindControl("TextCountry") as TextBox).Text;
                DropDownList DdlNationality = DetailsView1.FindControl("DdlNationality") as DropDownList;
                string address = (DetailsView1.FindControl("TextAddress") as TextBox).Text;

                //var QueryEmail = from item in db.tbl_signups where item.email == obj.EnryptString(email) && item.u_hakkimid != Session["hakkemid_u"].ToString() select item;
                //if (QueryEmail.Count() > 0)
                //{
                //    //Label2.Text = "Given email id exist so please choose another one..";
                //    //ModalPopupExtender3.Show();
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('Given email id exist so please choose another one..')</Script>");

                //}
                //else
                //{
                var Query = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                foreach (var ss in Query)
                {
                    //  contact = "+966" + contact.ToString();
                    contact = contact.ToString();
                    ss.name = name;
                    ss.email = obj.EnryptString(email);
                    ss.contact = obj.EnryptString(contact);
                    ss.country = DdlNationality.Text; 
                    ss.address = address;
                }
                db.SubmitChanges();
                DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
                User();
                //}
            }
        }
    }

    



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        shared_documents();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        shared_documents();
    }

    //protected void Button2_Click(object sender, EventArgs e)
    //{

    //}



    protected void sharehistory_Click(object sender, EventArgs e)
    {
        List<string> hisid = new List<string>();
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox chk = gr.FindControl("CheckBox2") as CheckBox;
            Label lbl16 = gr.FindControl("Label16") as Label;
            if (chk.Checked)
            {
                hisid.Add(lbl16.Text);
            }
        }

        if (hisid.Count() > 0)
        {

            if (DropDownList1.SelectedIndex != 0 || DropDownList2.SelectedIndex != 0)
            {
                if (DropDownList1.SelectedIndex != 0 && DropDownList2.SelectedIndex != 0)
                {
                    foreach (var ss in hisid)
                    {
                        var his = (from item in db.tbl_share_histories where item.history_id == Convert.ToInt32(ss.ToString()) select item).First();

                        tbl_share_history td = new tbl_share_history()
                        {
                            appointment_id = his.appointment_id,
                            history_id = int.Parse(his.history_id.ToString()),
                            share_h_id = null,
                            share_d_id = DropDownList1.SelectedItem.Value,
                        };
                        db.tbl_share_histories.InsertOnSubmit(td);
                        db.SubmitChanges();

                        try
                        {
                            var hospital = (from item in db.tbl_hdoctors where item.hd_email == DropDownList2.SelectedItem.Value select item).First();

                            tbl_share_history td1 = new tbl_share_history()
                            {
                                appointment_id = his.appointment_id,
                                history_id = int.Parse(his.history_id.ToString()),
                                share_h_id = hospital.h_id,
                                share_d_id = DropDownList2.SelectedItem.Value,
                            };
                            db.tbl_share_histories.InsertOnSubmit(td1);
                            db.SubmitChanges();
                        }
                        catch (Exception ex) { }

                    }
                    hisid.Clear();
                    shared_documents();
                }
                else if (DropDownList1.SelectedIndex != 0)
                {
                    foreach (var ss in hisid)
                    {
                        var his = (from item in db.tbl_share_histories where item.history_id == Convert.ToInt32(ss.ToString()) select item).First();

                        tbl_share_history td = new tbl_share_history()
                        {
                            appointment_id = his.appointment_id,
                            history_id = int.Parse(his.history_id.ToString()),
                            share_h_id = null,
                            share_d_id = DropDownList1.SelectedItem.Value,
                        };
                        db.tbl_share_histories.InsertOnSubmit(td);
                        db.SubmitChanges();


                    }
                    hisid.Clear();
                    shared_documents();
                }
                else if (DropDownList2.SelectedIndex != 0)
                {
                    foreach (var ss in hisid)
                    {
                        var his = (from item in db.tbl_share_histories where item.history_id == Convert.ToInt32(ss.ToString()) select item).First();

                        try
                        {
                            var hospital = (from item in db.tbl_hdoctors where item.hd_email == DropDownList2.SelectedItem.Value select item).First();

                            tbl_share_history td1 = new tbl_share_history()
                            {
                                appointment_id = his.appointment_id,
                                history_id = int.Parse(his.history_id.ToString()),
                                share_h_id = hospital.h_id,
                                share_d_id = DropDownList2.SelectedItem.Value,
                            };
                            db.tbl_share_histories.InsertOnSubmit(td1);
                            db.SubmitChanges();
                        }
                        catch (Exception ex) { }


                    }
                    shared_documents();
                    hisid.Clear();
                }
                else
                {
                    //if (Session["Speciality"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select a doctor')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء اختيار طبيب')</Script>");
                    //}
                }
            }
        }
        else
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select a history')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الرجاء تحديد سجل')</Script>");
            //}
        }
    }

    protected void DetailsView1_DataBound1(object sender, EventArgs e)
    {
       
        if (DetailsView1.CurrentMode == DetailsViewMode.Edit)
        {

            DropDownList DdlNationality = DetailsView1.FindControl("DdlNationality") as DropDownList;

            if (DdlNationality != null)
            {
                DdlNationality.DataSource = CountryList();
                DdlNationality.DataBind();
                try
                {
                    if (DdlNationality.Items.Contains(DdlNationality.Items.FindByValue(DataBinder.Eval(
                         DetailsView1.DataItem, "country").ToString())))
                    {
                        DdlNationality.SelectedIndex = DdlNationality.Items.IndexOf(
                          DdlNationality.Items.FindByValue(DataBinder.Eval(DetailsView1.DataItem,
                          "country").ToString()));
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    if(FileUpload1.HasFile)
    //    {
    //        BtnUpdateFoto.Enabled = true;
    //        BtnUpdateFoto.CssClass = "btn btn-sm btn-success";
    //    }
    //}

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        List<string> files = new List<string>();
        string report_id = (sender as LinkButton).CommandArgument;
        var report = from item in db.tbl_test_reports where item.id == int.Parse(report_id) select item;
        foreach (var ss in report)
        {
            if (ss.blood_test_report != null) { files.Add(ss.blood_test_report); }
            if (ss.other_test_report != null) { files.Add(ss.other_test_report); }
            if (ss.scan_test_report != null) { files.Add(ss.scan_test_report); }
            if (ss.urine_test_report != null) { files.Add(ss.urine_test_report); }
            if (ss.xray_test_report != null) { files.Add(ss.xray_test_report); }
        }
      
        int i = 0;
        while (i < files.Count())
        {
            string filePath = files[i];

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        i++;
        

    }
}