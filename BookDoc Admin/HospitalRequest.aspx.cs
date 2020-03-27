using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Net.Mail;
public partial class BookDoc_Admin_HospitalRequest : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    secure obj = new secure();
    MailMessage msg = new MailMessage();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    SMS ob1 = new SMS();
    protected override void InitializeCulture()
    {
    //    Session["Language"] = "";
    //    string culture = "";
    //    try
    //    {
    //        culture = Request.QueryString["l"].ToString();
    //        Session["Language"] = culture;
    //    }
    //    catch (Exception ex)
    //    { }
    //    // string culture = Session["Language"].ToString();
    //    if (string.IsNullOrEmpty(culture))
    //    {
    //        culture = "Auto";
    //        Session["Language"] = culture;
    //    }
    //    //Use this
    //    UICulture = culture;
    //    Culture = culture;
    //    //OR This
    //    if (culture != "Auto")
    //    {

    //        System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
    //        System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
    //        System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
    //    }
    //    else
    //    {
    //        //LinkButton1.Text = "عربى";
    //    }

    //    base.InitializeCulture();
    }

    void Page_PreInit(Object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{

        //}
        //else
        //{
        //    this.MasterPageFile = "~/BookDoc Admin/AdminArabicMasterPage.master";
        //}
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetHospitals();
        }
        GetHospitals();
    }

    private void GetHospitals()
    {
        var selectHospitals = from hospitals in db.tbl_hospitalregs
                              where hospitals.h_status == 0
                              orderby hospitals.h_id descending
                              select hospitals;

        if (selectHospitals.Count() > 0)
        {
            grvHospitals.DataSource = selectHospitals;
            grvHospitals.DataBind();
            foreach (GridViewRow gr in grvHospitals.Rows)
            {
                Label lblAgrmnt = (Label)gr.FindControl("LblAgrementFile");
                if(lblAgrmnt.Text=="")
                {
                    gr.BackColor = System.Drawing.Color.IndianRed;
                    gr.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fff");
                }
                else
                {
                   
                    gr.BackColor = System.Drawing.ColorTranslator.FromHtml("#4aa9af");
                    gr.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fff");
                }
            }
        }
        else
        {
            //Response.Write("<script>alert('No Hospitals are requested')</script>");
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No hospitals are requested')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('ولا يطلب من المستشفيات')</Script>");
            //}
        }
    }
    protected void grvHospitals_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    //    if(e.CommandName=="view")
    //    {
    //        int intex = int.Parse(e.CommandArgument.ToString());
    //            GridViewRow row = grvHospitals.Rows[intex];
 
    ////Fetch value of Name.
    //string name = (row.FindControl("LblAgrementFile") as TextBox).Text;

    //        //Label lblAgrement = (Label)grvHospitals.Rows[intex].FindControl("LblAgrementFile");
    //        Session["hosptal_Id"] = e.CommandArgument.ToString();
    //        Session["Agrement"] = name;
    //        Response.Redirect("Create hospital.aspx");
    //    }
    }
    protected void grvHospitals_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        Label lblAgrement = (Label)grvHospitals.Rows[e.RowIndex].FindControl("LblAgrementFile");
        if (lblAgrement.Text != "")
        {
            Session["hosptal_Id"] = grvHospitals.DataKeys[e.RowIndex].Values["h_id"].ToString();
            Session["Agrement"] = lblAgrement.Text;
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("Create hospital.aspx");
            //}
            //else
            //{
            //    Response.Redirect("Create hospital.aspx?l=ar-EG");
            //}
        }
        else
        {
            Session["hosptal_Id"] = grvHospitals.DataKeys[e.RowIndex].Values["h_id"].ToString();
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("Create hospital.aspx");
            //}
            //else
            //{
            //    Response.Redirect("Create hospital.aspx?l=ar-EG");
            //}
        }
        
        
        
    }

    protected void grvHospitals_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        con.Open();
        String email = "";
        string name = "";
        string ph = "";
        int id = Convert.ToInt32(grvHospitals.DataKeys[e.RowIndex].Value.ToString());
        SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_hospitalreg where h_id='" + id + "'", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            email = dt.Rows[0]["h_email"].ToString();
            name = dt.Rows[0]["h_name"].ToString();
            ph = dt.Rows[0]["h_contact"].ToString();
        }
      
        string dph = "+966" + ph.ToString();
        ob1.Message(dph.ToString(), "sorry!!  " + name + " your account rejected from Hakkeem");

        string dph1 = "+91" + ph.ToString();
        ob1.Message(dph1.ToString(), "sorry!!  " + name + " your account rejected from Hakkeem");
        Email_To_AccountRejection(email);
       // EmailString(email, "sorry!!  " + name + " your account rejected from Hakkeem");
       
        if(con.State.ToString()=="Closed")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("Delete from tbl_hospitalreg where h_id='" + id + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        GetHospitals();
        //if (Session["Language"].ToString() == "Auto")
        //{

            Response.Redirect("~/BookDoc Admin/HospitalRequest.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/BookDoc Admin/HospitalRequest.aspx?l=ar-EG");
        //}
    }
    public void EmailString(string email, string msg)
    {



        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(email);
        mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
        mail.Subject = "Account Deletion";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = msg;
        // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("mail@hakkeem.com", "Hakkeem2018!!");
        client.Port = 25;
        client.Host = "smtp.goldenetqan.com";
        client.EnableSsl = false;
        try
        {
            client.Send(mail);





            //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
        }
        catch (Exception ex)
        {
            //Exception ex2 = ex;
            //string errorMessage = string.Empty;
            //while (ex2 != null)
            //{
            //    errorMessage += ex2.ToString();
            //    ex2 = ex2.InnerException;
            //}
            //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
        }
    }
    public bool Email_To_AccountRejection(string email)
    {
        string cmpnyemail = "";
        string number = "";
        bool flag = true;
        string msgsubject, msgbody;

        MailMessage message = new MailMessage();


        int id;
        try
        {

            //con.Open();



            MailMessage msgMail = new MailMessage();
            MailMessage myMessage = new MailMessage();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbtitle = new StringBuilder();
            String messagestr = "";
            string bg = "http://www.hakkeem.com/head1.png";
            string follw = "http://www.hakkeem.com/followus.png";
            string face = "http://www.hakkeem.com/facebook.png";
            string twitter = "http://www.hakkeem.com/twitter.png";
            string insta = "http://www.hakkeem.com/instagram.png";
            string contact = "http://hakkeem.com/ContactUs.aspx";
            string privacy = "http://hakkeem.com/privacy%20policy.aspx";
            messagestr = messagestr + "<body style='text-align:center;width=100%'>";

            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto; background:#f2f2f2;padding:60px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + "<tr>";
            messagestr = messagestr + " <td>";
            messagestr = messagestr + " <table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
            messagestr = messagestr + "<tbody>";
            messagestr = messagestr + " <tr>";
            messagestr = messagestr + "<td  colspan='2' style='padding:20px 20px;background-color:#fff;line-height:2.2em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%' ></td>";

            messagestr = messagestr + "</tr>";
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>Account Deletion</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "Your account has been rejected by Hakkeem authority";
            messagestr = messagestr + " </td></tr><tr><td style='text-align:center;padding:20px 20px;background-color: #fff;text-align:center'>";
            messagestr = messagestr + "</td></tr><tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
            messagestr = messagestr + " <tbody><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<span style='color:#4aa9af'><a href='"+privacy+"' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='" + contact + "' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
            messagestr = messagestr + "</span></td></tr><br><br><tr><td style='text-align:center;'>";
            messagestr = messagestr + "<img src='" + follw + "'>";
            messagestr = messagestr + "<a href='https://www.facebook.com/hakkeem.etqan.1' style='text-decoration:none'><img src='" + face + "' title='Facbook'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://twitter.com/Hakkeem_1' style='text-decoration:none'><img src='" + twitter + "'  title='Twitter'>&nbsp;</a>";
            messagestr = messagestr + "<a href='https://www.instagram.com/hakkeem_1/' style='text-decoration:none'><img src='" + insta + "'  title='Instagram'>&nbsp;</a>";
            messagestr = messagestr + "</td></tr></tbody></table></td></tr>";

            messagestr = messagestr + "<tr><td  colspan='2' style='background-color:#fff;padding:20px 20px'>";
            messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:25px'>";
            messagestr = messagestr + " <tbody><tr>";
            messagestr = messagestr + "<td width='100%' style='color:#4aa9af;font-family:sans-serif; font-size:10px'>";
            messagestr = messagestr + " <span style='font-weight:bold;font-size:10px'> Discliamer</span> : Please do not print this email unless it is necessary. Every unprinted email helps the environment.";
            messagestr = messagestr + "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body>";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
            mail.Subject = "Hakkeem Account Rejection";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = messagestr.ToString();
            // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("mail@hakkeem.com", "Hakkeem2018!!");
            client.Port = 25;
            client.Host = "smtp.goldenetqan.com";
            client.EnableSsl = false;
            try
            {
                client.Send(mail);

            }
            catch (Exception ex)
            {

            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return flag;
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            throw ex;
        }
    }
}