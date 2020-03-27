using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index_hdoclogin : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
        //    LinkButton1.Text = "عربى";
        //}
        //else
        //{
        //    LinkButton1.Text = "English";
        //}
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        

        var Query = from item in db.tbl_hdoctors
                    where item.hd_email == login.Text && item.hd_password == Password.Text && item.h_id == TxtHospital.Text
                    select item;
        if (Query.Count() > 0)
        {

            foreach (var ss in Query)
            {

                var Query11 = from item in db.tbl_blk_hos_doctors where item.doctor_id == ss.hd_email select item;
                if (Query11.Count() > 0)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('You are blocked, contact Hospital admin')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يتم حظر لك، اتصل مستشفى المشرف')</Script>");
                    //}

                }

                else
                {

                    if (ss.hd_id_expire == null)
                    {
                        Session["HosDocId"] = ss.hd_email;
                        Session["HospitalId"] = ss.h_id;

                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            Response.Redirect("~/HospitalDoctor/HospitalDoctorConsulting.aspx");
                        //}
                        //else
                        //{
                        //    Response.Redirect("~/HospitalDoctor/HospitalDoctorConsulting.aspx?l=ar-EG");
                        //}
                    }
                    else
                    {
                        DateTime currentDate = DateTime.Now;
                        DateTime compareDate = Convert.ToDateTime(ss.hd_id_expire);
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
                            Session["HosDocId"] = ss.hd_email;
                            Session["HospitalId"] = ss.h_id;

                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                Response.Redirect("~/HospitalDoctor/HospitalDoctorConsulting.aspx");
                            //}
                            //else
                            //{
                            //    Response.Redirect("~/HospitalDoctor/HospitalDoctorConsulting.aspx?l=ar-EG");
                            //}
                        }


                    }

                }

            }
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Username or password is incorrect')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('اسم المستخدم أو كلمة المرور غير صحيحة')</Script>");
            //}
            ////Label1.Text = "Invalid login";
            ////this.ModalPopupExtender1.Show();
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
            //    Response.Redirect("hospitaldoctorlogin.aspx");
            //}
            //else
            //{
            //    Session["Language"] = "ar-EG";

            //    Response.Redirect("hospitaldoctorlogin.aspx?l=ar-EG");
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