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

public partial class BookDoc_Admin_Doctor_request : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    secure obj = new secure();
    MailMessage msg = new MailMessage();
    SMS ob1 = new SMS();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
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
        //    this.MasterPageFile = "~/BookDoc Admin/AdminArabicMasterPage.master";
        //}
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            doctor_rqst();
        }
        doctor_rqst();
    }

    public void doctor_rqst()
    {
        var Query = from item in db.tbl_doctors where item.d_status == 0 orderby item.d_id descending select item;
        if(Query.Count()>0)
        {
            GridView1.DataSource = Query;
            GridView1.DataBind();
            foreach(GridViewRow grow in GridView1.Rows)
            {
                Label lblAgrmnt = (Label)grow.FindControl("LblAgreement");
                if (lblAgrmnt.Text == "")
                {
                    grow.BackColor = System.Drawing.Color.IndianRed;
                    grow.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fff");
                    //ColorTranslator.FromHtml("#FF00FF");
                }
                else
                {
                    grow.BackColor = System.Drawing.ColorTranslator.FromHtml("#4aa9af");
                    grow.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fff");
                }
            }
        }
        else
        {

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            Session["d_id"] = e.CommandArgument.ToString();
            //if (Session["Language"].ToString() == "Auto")
            //{

                Response.Redirect("~/BookDoc Admin/Create Doctor.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/BookDoc Admin/Create Doctor.aspx?l=ar-EG");
            //}
        }
        
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        con.Open();
        String email = "";
        string name = "";
        string ph = "";
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_doctor where d_id='" + id + "'", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if(dt.Rows.Count>0)
        {
            email = obj.DecryptString(dt.Rows[0]["d_email"].ToString());
            name= dt.Rows[0]["d_name"].ToString();
            ph= dt.Rows[0]["d_contact"].ToString();
        }
        string s = obj.DecryptString(ph);
        string dph = "+966" + s.ToString();
        ob1.Message(dph.ToString(), "sorry!!  " + name + " your account rejected from Hakkeem");


        string dph1 = "+91" + s.ToString();
        ob1.Message(dph1.ToString(), "sorry!!  " + name + " your account rejected from Hakkeem");
        Email_To_AccountRejection(email);
        // Email(email, "sorry!!  " + name + " your account rejected from Hakkeem");
      
        if(con.State.ToString()=="Closed")
        {
            con.Open();
        }
        //SqlCommand cmd8 = new SqlCommand("Delete from tbl_doctor_location where d_id='" + id + "'", con);
        //cmd8.ExecuteNonQuery();
        SqlCommand cmd = new SqlCommand("Delete from tbl_doctor where d_id='" + id + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        doctor_rqst();
        //if (Session["Language"].ToString() == "Auto")
        //{

            Response.Redirect("~/BookDoc Admin/Doctor request.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/BookDoc Admin/Doctor request.aspx?l=ar-EG");
        //}
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
    public void Email(string email, string msg)
    {
       
               

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.To.Add(email);
                    mail.From = new MailAddress("mail@hakkeem.com", "Hakkeem", System.Text.Encoding.UTF8);
                    mail.Subject = "Account Rejection";
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
          
    }
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (Session["Language"].ToString() == "Auto")
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            LinkButton buttonCommandField = e.Row.Cells[5].Controls[0] as LinkButton;
    //            buttonCommandField.Attributes["onClick"] =
    //                   string.Format("return confirm('Are you want delete ')");
    //        }
    //    }
    //    else
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            LinkButton buttonCommandField = e.Row.Cells[5].Controls[0] as LinkButton;
    //            buttonCommandField.Attributes["onClick"] =
    //                   string.Format("return confirm('هل تريد حذف')");
    //        }
    //    }
    //}
