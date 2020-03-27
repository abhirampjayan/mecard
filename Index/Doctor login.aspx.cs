using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Index_doctorlogin : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (Session["Language"].ToString() == "ar-EG")
            //{
            //    LinkButton1.Text = "English";
            //}
            //else
            //{
            //    LinkButton1.Text = "عربى";
            //}
            if (Session["check"].ToString() == "1")
            {
                Session["check"] = "0";
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Thank you for registering with Hakkeem.Please check your mail.We will contact you with in one business day.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('شكرا لتسجيلك مع هكيم. يرجى التحقق من البريد الخاص بك وتحميل الاتفاقية الموقعة. وسوف نتصل بك مع في يوم عمل واحد')</Script>");
                //}
            }
        }
        catch (Exception ex)
        {

        }

        if (!IsPostBack)
        {
            if (Request.Cookies["UserNamehos"] != null && Request.Cookies["Passwordhos"] != null)
            {
                input1.Text = Request.Cookies["UserNamehos"].Value;
                input2.Attributes["value"] = Request.Cookies["Passwordhos"].Value;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            Response.Cookies["UserNamehos"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["Passwordhos"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["UserNamehos"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Passwordhos"].Expires = DateTime.Now.AddDays(-1);

        }
        Response.Cookies["UserNamehos"].Value = input1.Text.Trim();
        Response.Cookies["Passwordhos"].Value = input2.Text.Trim();

        string user = input1.Text.ToLower();

        var Query = from item in db.tbl_doctors where (item.d_email == obj.EnryptString(input1.Text) || item.d_email == obj.EnryptString(user) || item.d_hakkimid == input1.Text) && (item.d_status == 1 || item.d_status == 0) select item;
        if (Query.Count() > 0)
        {
            var Query1 = from item in db.tbl_doctors where (item.d_email == obj.EnryptString(input1.Text) || item.d_email == obj.EnryptString(user) || item.d_hakkimid == input1.Text) && item.d_password == obj.EnryptString(input2.Text) && (item.d_status == 1 || item.d_status == 0) select item;
            if (Query1.Count() > 0)
            {



                foreach (var ss in Query1)
                {

                    var Query11 = from item in db.tbl_blk_hakkeem_doctors where item.hakkeem_id == ss.d_hakkimid select item;
                    if (Query11.Count() > 0)
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

                        if (ss.d_id_expire == null)
                        {
                            Session["doctor"] = ss.d_email.ToString();
                            Session["hakkeemid_d"] = ss.d_hakkimid.ToString();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                Response.Redirect("~/Doctor/DoctorHome.aspx");
                            //}
                            //else
                            //{
                            //    Response.Redirect("~/Doctor/DoctorHome.aspx?l=ar-EG");
                            //}
                        }
                        else
                        {
                            try
                            {
                                DateTime currentDate = DateTime.Now;
                                DateTime compareDate = Convert.ToDateTime(ss.d_id_expire);
                                if (currentDate >= compareDate)
                                {
                                    //Label2.Text = "Doctor identification number is expired...!";
                                    //ModalPopupExtender2.Show();
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number is expired...!')</Script>");
                                    //}
                                    //else
                                    //{
                                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('انتهت صلاحية رقم تعريف الطبيب')</Script>");
                                    //}
                                }
                                else
                                {
                                    Session["doctor"] = ss.d_email.ToString();
                                    Session["hakkeemid_d"] = ss.d_hakkimid.ToString();
                                    //if (Session["Language"].ToString() == "Auto")
                                    //{
                                    Response.Redirect("~/Doctor/DoctorHome.aspx");
                                    //}
                                    //else
                                    //{
                                    //    Response.Redirect("~/Doctor/DoctorHome.aspx?l=ar-EG");
                                    //}
                                }
                            }
                            catch (Exception ex)
                            {
                                Session["doctor"] = ss.d_email.ToString();
                                Session["hakkeemid_d"] = ss.d_hakkimid.ToString();
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                Response.Redirect("~/Doctor/DoctorHome.aspx"); 

                            }
                        }
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            //CancellSession();
            //if (Session["Language"].ToString() == "ar-EG")
            //{
            //    Session["Language"] = "Auto";
                Response.Redirect("doctor login.aspx");
            //}
            //else
            //{
            //    Session["Language"] = "ar-EG";

            //    Response.Redirect("doctor login.aspx?l=ar-EG");
            //}
        }
        catch (Exception ex)
        {

            //Response.Redirect("index.aspx");

        }

        //Session["Language"] = "ar-EG";

        //Response.Redirect("doctor login.aspx?l=ar-EG");
    }

}