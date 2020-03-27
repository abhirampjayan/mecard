using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class HospitalDoctor_ChangePassword : System.Web.UI.Page
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
        //    this.MasterPageFile = "~/HospitalDoctor/ArabicHospitalDoctorMaster.master";
        //}
    }
    string HosId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HosDocId"] != null)
        {
            LblDocId.Text = Session["HosDocId"].ToString();
            HosId = Session["HospitalId"].ToString();
            if (!IsPostBack)
            {
                
            }
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{

                Response.Redirect("~/Index/HospitalDoctorLogin.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/HospitalDoctorLogin.aspx?l=ar-EG");

            //}
        }
    }
    protected void BtnChange_Click(object sender, EventArgs e)
    {
        try
        {
            var query = from item in db.tbl_hdoctors
                        where item.h_id == HosId && item.hd_email == LblDocId.Text
                        select item;
            foreach(var ss in query)
            {
                if (ss.hd_password == TxtCurrentPass.Text)
                {
                    ss.hd_password = TxtConfirmNew.Text;
                    db.SubmitChanges();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('Succesfully password changed')</Script>");
                    //}
                    //else
                    //{
                    //    this.Page.RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تغيير كلمة المرور بنجاح')</Script>");

                    //}
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swaal('Current password is incorrect. Please enter correct password.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swaal('كلمة المرور الحالية غير صحيحة. الرجاء إدخال كلمة المرور الصحيحة.')</Script>");

                    //}
                }
            }

        }
        catch(Exception ex)
        {
            //Response.Write(ex);
        }

    }
}