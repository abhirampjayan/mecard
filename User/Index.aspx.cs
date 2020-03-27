using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hakkeem_Index : System.Web.UI.Page
{
    MailMessage Email = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

        //else
        //{
        //    LinkButton3.Text = "الإنجليزية";
        //}

        //try
        //{
        //    if (Session["Language"].ToString() == "ar-EG")
        //    {
        //        LinkButton1.Text = "الإنجليزية";
        //    }
        //}
        //catch (Exception ex)
        //{

        //}

        if (!IsPostBack)
        {
            if (LinkButton3.Text == "")
            {
                LinkButton3.Text = "عربى";


            }
            try
            {
                LinkButton3.Text = Session["la"].ToString();
            }
            catch (Exception ex)
            {

            }

            try
            {
                if (Session["searchcommon"].ToString() != "")
                {
                    // HyperLink2.NavigateUrl = "~/User/SearchCommon.aspx";
                    HyperLink2.NavigateUrl = "../default.aspx";
                    Session["searchcommon"] = "";
                    //     Response.Redirect("searchcommon.aspx");
                }
            }
            catch (Exception ex)
            {
                HyperLink2.NavigateUrl = "../default.aspx";
            }
            try
            {
                if (Session["hakkemid_u"].ToString() != "")
                {
                    HyperLink2.NavigateUrl = "~/User/Search.aspx";


                }
                else
                {

                    HyperLink2.NavigateUrl = "../default.aspx";

                }
            }
            catch (Exception ex)
            { HyperLink2.NavigateUrl = "../default.aspx"; }


        }
        user();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (LinkButton1.Text == "Patient")
        {
            Response.Redirect("~/Index/SignInSignUp.aspx");
        }
        else
        {
            //Session["hakkemid_u"] = null;
            Response.Redirect("user account.aspx");
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["hakkemid_u"] = "";
        Session["hakkemid_u"] = null;
        Session["ggggg"] = "1";
        Response.Redirect("~/Index/SignInSignUp.aspx");
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        // Server.Transfer(Request.Path + "?l=ar-EG");
        // Response.Redirect(Request.Path + "?l=ar-EG");
        try
        {
            //CancellSession();

            if (LinkButton3.Text == "عربى")
            {
                LinkButton3.Text = "الإنجليزية";
                Session["la"] = "الإنجليزية";
                Response.Redirect(Request.Path + "?l=ar-EG");

            }
            else
            {
                Session["la"] = "عربى";
                LinkButton3.Text = "عربى";
                Response.Redirect(Request.Path);
                // Response.Redirect("hospita login.aspx?l=ar-EG");
            }
        }
        catch (Exception ex)
        {

            //Response.Redirect("index.aspx");

        }

        Session["Language"] = "Auto";

        // Response.Redirect("hospita login.aspx?l=ar-EG");
        Response.Redirect(Request.Path);
    }
    public void user()
    {
        try
        {
            if (Session["hakkemid_u"].ToString() != null || Session["hakkemid_u"].ToString() != "")
            {
                var Query = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;
                foreach (var ss in Query)
                {
                    HyperLink1.Visible = true;
                    HyperLink1.NavigateUrl = "~/User/User account.aspx";

                    HyperLink3.NavigateUrl = "~/User/User review.aspx";

                    HyperLink4.NavigateUrl = "~/User/ConsultedHistory.aspx";



                    HyperLink5.NavigateUrl = "~/User/UserAppointments.aspx";

                  //  HyperLink6.NavigateUrl = "~/User/UploadTestReports.aspx";

                    string[] n = ss.name.Split(' ');
                    LinkButton1.Text = /*"Hello, "+*/n[0];
                    LinkButton2.Text = "SignOut";
                    LinkButton2.Visible = true;
                    if (ss.photo != null)
                    {
                        ImgUser.ImageUrl = ss.photo;
                    }
                    else
                    {
                        ImgUser.ImageUrl = "~/User/mapicons/user .png";
                    }
                    //Label1.Text = ss.name;
                    //Label2.Text = "SignOut";
                }
            }
            else
            {
                HyperLink1.Visible = false;
                LinkButton1.Text = "Patient";
                LinkButton1.PostBackUrl = "~/Index/SignInSignUp.aspx";
                ImgUser.Visible = false;
                LinkButton2.Visible = false;
                //LblUserName.Visible = false;
                //Label1.Visible = false;
                //Label2.Text = "SignIn";
            }
        }
        catch (Exception ex)
        {
            HyperLink1.Visible = false;
            LinkButton1.Text = "Patient";
            LinkButton1.PostBackUrl = "~/Index/SignInSignUp.aspx";
            ImgUser.Visible = false;

        }
    }


    protected void txtContactsSearch_TextChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void Illness_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}