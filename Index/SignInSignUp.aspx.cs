using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index_userlogin : System.Web.UI.Page
{
    MailMessage mail = new MailMessage();
    databaseDataContext db = new databaseDataContext();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //if (Session["Language"].ToString() == "ar-EG")
                //{
                //    LinkButton2.Text = "English";
                //}
                //else
                //{
                //    LinkButton2.Text = "عربى";
                //}
                //if (Session["check"].ToString() == "1")
                //{
                //    Session["check"] = "0";
                //    if (Session["Language"].ToString() == "Auto")
                //    {
                //        RegisterStartupScript("", "<Script Language=JavaScript>swal('Succesfully complete your registration. Now you can use Hakkeem.')</Script>");
                //    }
                //    else
                //    {
                //        RegisterStartupScript("", "<Script Language=JavaScript>swal('إكمال التسجيل بنجاح. الآن يمكنك استخدام حكيم.')</Script>");

                //    }
                //}


                if (Session["hakkemid_u"] != null)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        Response.Redirect("../user/search.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("../user/search.aspx?l=ar-EG");
                    //}
                }




            }
            catch (Exception ex)
            {

            }
            if (Request.Cookies["UserNameuser"] != null && Request.Cookies["Passworduser"] != null)
            {
                Email.Text = Request.Cookies["UserNameuser"].Value;
                Password.Attributes["value"] = Request.Cookies["Passworduser"].Value;
            }
        }


    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            //CancellSession();
            //if (Session["Language"].ToString() == "ar-EG")
            //{
            //    Session["Language"] = "Auto";
                Response.Redirect("signInSignUp.aspx");
            //}
            //else
            //{
            //    Session["Language"] = "ar-EG";

            //    Response.Redirect("signInSignUp.aspx.aspx?l=ar-EG");
            //}
        }
        catch (Exception ex)
        {

            //Response.Redirect("index.aspx");

        }

        //Session["Language"] = "ar-EG";

        //Response.Redirect("doctor login.aspx?l=ar-EG");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        //Panel1.Visible = true;
        //Panel2.Visible = false;

    }



    protected void Button1_Click(object sender, EventArgs e)
    {

        if (CheckBox1.Checked)
        {
            Response.Cookies["UserNameuser"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["Passworduser"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["UserNameuser"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Passworduser"].Expires = DateTime.Now.AddDays(-1);

        }
        Response.Cookies["UserNameuser"].Value = Email.Text.Trim();
        Response.Cookies["Passworduser"].Value = Password.Text.Trim();
        string eemail = Email.Text.ToLower();
        var Query11 = from item in db.tbl_signups
                    where (item.email == obj.EnryptString(Email.Text) || item.email == obj.EnryptString(eemail) || item.u_hakkimid == Email.Text || item.contact == Email.Text) && item.status == 10
                    select item;
        if (Query11.Count() > 0)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You are blocked by Admin...!')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم حظرك بواسطة المشرف')</Script>");
            //}
        }
        else
        {



            var Query = from item in db.tbl_signups
                        where (item.email == obj.EnryptString(Email.Text) || item.email == obj.EnryptString(eemail) || item.u_hakkimid == Email.Text || item.contact ==obj.EnryptString( Email.Text)) && item.status == 1
                        select item;
            if (Query.Count() > 0)
            {
                var Query1 = from item in db.tbl_signups
                             where (item.email == obj.EnryptString(Email.Text) || item.email == obj.EnryptString(eemail) || item.u_hakkimid == Email.Text || item.contact == obj.EnryptString(Email.Text)) && item.password == obj.EnryptString(Password.Text) && item.status == 1
                             select item;
                if (Query1.Count() > 0)
                {
                    foreach (var ss in Query1)
                    {
                        Session["user"] = obj.DecryptString(ss.email);
                        Session["hakkemid_u"] = ss.u_hakkimid.ToString();
                    }
                    var checkblock = from item in db.tbl_blk_users where item.user_hakkeemid == Session["hakkemid_u"].ToString() select item;
                    if (checkblock.Count() > 0)
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
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            Response.Redirect("~/User/search.aspx");
                        //}
                        //else
                        //{
                        //    Response.Redirect("~/User/search.aspx?l=ar-EG");
                        //}
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
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Given login id is incorrect...!')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('معرف تسجيل الدخول المعطى غير صحيح')</Script>");
                //}
                //Label1.Text = "Given login id is incorrect...!";
                //this.ModalPopupExtender1.Show();
            }
        }
    }

}