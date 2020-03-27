using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index_userlogin : System.Web.UI.Page
{
    MailMessage mail = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    secure obj = new secure();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    protected override void InitializeCulture()
    {
        //Session["Language"] = "Auto";
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
        con.Open();
        
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
                

               




            }
            catch (Exception ex)
            {

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
                Response.Redirect("index.aspx");
            //}
            //else
            //{
            //    Session["Language"] = "ar-EG";

            //    Response.Redirect("index.aspx.aspx?l=ar-EG");
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

       

        SqlCommand com = new SqlCommand("select id from tbl_admin where username='" + obj.EnryptString(Email.Text) + "' and password='" +obj.EnryptString(Password.Text) + "'", con);
        int id;
        try
        {
            id = Convert.ToInt32(com.ExecuteScalar());
        }
        catch (Exception ex)
        {
            id = 0;
        }
        if (id!= 0)
        {
           
                    Session["aduser"] = obj.EnryptString(Email.Text);
                
               

                //if (Session["Language"].ToString() == "Auto")
                //{
                    Response.Redirect("admin index.aspx");
                //}
                //else
                //{
                //    Response.Redirect("admin index.aspx?l=ar-EG");
                //}
            }
            else
            {
                //Label1.Text = "Given password is incorrect...!";
                //this.ModalPopupExtender1.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Given username or password is incorrect...!')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('كلمة المرور غير صحيحة')</Script>");
                //}
            }
        }




    protected void Email_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Email_TextChanged1(object sender, EventArgs e)
    {

    }
}