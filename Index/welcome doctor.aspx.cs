using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index_welcome_doctor : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["OTP"] == null || Session["name"] == null)
        {
            Response.Redirect("~/Index/Index.aspx");
        }
        Label1.Text = Session["name"].ToString();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
        var Query = from item in db.tbl_doctors
                    where item.d_email == Session["OTP"].ToString() && item.d_status == 2
                    select item;

        if(Query.Count()>0)
        {
            foreach (var ss in Query)
            {
                if (TextBox1.Text == ss.d_otp.ToString())
                {
                    ss.d_status = 0;
                    db.SubmitChanges();
                    //RegisterStartupScript("", "<Script Language=JavaScript>alert('Successfully completed your registration...we will contact you with one business day. Thank you')</Script>");
                    //Response.Redirect("~/Index/Index.aspx");
                    this.Page.RegisterStartupScript("", "<Script Language=JavaScript>alert('Thank you for registering with BookDok.Please check your mail and upload the signed agreement. We will contact you with in one business day.');window.location='Index.aspx'</Script>");
                }
                else
                {
                    RegisterStartupScript("", "<Script Language=JavaScript>alert('You entered OTP is not valid...please check given email')</Script>");
                }
            }
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>alert('User not exist....!')</Script>");
        }


        
    }
}