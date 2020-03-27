using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index_HospitaLogin : System.Web.UI.Page
{
    secure obj = new secure();
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            //CancellSession();
            //if (Session["Language"].ToString() == "ar-EG")
            //{
            //    Session["Language"] = "Auto";
                Response.Redirect("hospita login.aspx");
            //}
            //else
            //{
            //    Session["Language"] = "ar-EG";

            //    Response.Redirect("hospita login.aspx?l=ar-EG");
            //}
        }
        catch (Exception ex)
        {

            //Response.Redirect("index.aspx");

        }

        //Session["Language"] = "ar-EG";

        //Response.Redirect("doctor login.aspx?l=ar-EG");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (Session["Language"].ToString() == "ar-EG")
            //{
                LinkButton2.Text = "English";
            //}
            //else
            //{
            //    LinkButton2.Text = "عربى";
            //}
            if (Session["check"].ToString() == "1")
            {
                Session["check"] = "0";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Thank you for registering with Hakkeem. Please check your mail. We will contact you with in one business day.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('شكرا لك على التسجيل مع هكيم. يرجى التحقق من بريدك وتحميل الاتفاقية الموقعة. سوف نتصل بك في يوم عمل واحد.')</Script>");

                //}
            }
        }
        catch (Exception ex)
        {

        }
        if (!IsPostBack)
        {
            if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            {
                login.Text = Request.Cookies["UserName"].Value;
                Password.Attributes["value"] = Request.Cookies["Password"].Value;
            }
        }
      
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
        if (CheckBox1.Checked)
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

        }
        Response.Cookies["UserName"].Value = login.Text.Trim();
        Response.Cookies["Password"].Value = Password.Text.Trim();
        var Query = from item in db.tbl_hospitalregs where (item.h_regno == login.Text || item.h_hakkimid == login.Text) select item;
        if (Query.Count() > 0)
        {
            var Query1 = from item in db.tbl_hospitalregs where (item.h_regno == login.Text || item.h_hakkimid == login.Text) && item.h_password == obj.EnryptString(Password.Text) select item;
            if (Query1.Count() > 0)
            {
                foreach (var ss in Query1)
                {
                    Session["hospital"] = ss.h_regno;
                    Session["hakkeemid_h"] = ss.h_hakkimid;
                }
                var hsptlblk = from item in db.tbl_blk_hospitals where item.hospital_hakkeem_id == Session["hakkeemid_h"].ToString() select item;
                if (hsptlblk.Count() > 0)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('You are blocked, contact Hakkeem admin')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('أنت محظورة. الاتصال هكيم المشرف')</Script>");
                    //}
                }
                else
                {
                    var query = from item in db.tbl_hos_locations
                                join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                                where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                                select new { item1.h_id, item.latitude };
                    //try
                    //{
                    if (query.Count() <= 0)
                    {
                        //Label8.Text = "You must set your location";
                        //ModalPopupExtender4.Show();
                        // Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
                        //}
                        //else
                        //{
                        //    Response.Redirect("~/Hospital/SetHospitalLocation.aspx?l=ar-EG");
                        //}
                    }

                    else
                    {


                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            Response.Redirect("~/Hospital/hospital.aspx");
                        //}
                        //else
                        //{
                        //    Response.Redirect("~/Hospital/hospital.aspx?l=ar-EG");
                        //}
                    }
                }
            }
            else
            {
                //Label1.Text = "Given password is incorrect...!";
                //this.ModalPopupExtender1.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Given password is incorrect...!')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('كلمة المرور غير صحيحة')</Script>");

                //}
            }
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Login id or password incorrect')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('معرف تسجيل الدخول أو كلمة المرور غير صحيحة')</Script>");
            //}
            //Label1.Text = "Given login id is incorrect...!";
            //this.ModalPopupExtender1.Show();
        }
    }
}