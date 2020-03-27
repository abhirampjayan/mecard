using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;

public partial class Hospital_ApointmentDetails : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    MailMessage Email = new MailMessage();

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
        //    this.MasterPageFile = "~/hospital/ArabichospitalMaster.master";
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["A_Status"].ToString() =="1")
        {
            BtnConfirm.Enabled = false;
            BtnConfirm.ToolTip = "Already confirmed";
            BtnConfirm.BackColor = System.Drawing.Color.IndianRed;
        }
        if (Session["hakkeemid_h"] != null)
        {
            if(!IsPostBack)
            {
                try
                {
                    CheckLocation();
                }
                catch (Exception ex)
                {
                    Response.Redirect("../index/hospita login.aspx");
                }
            }
            TxtApntmtDate.Text=Session["A_Date"].ToString();
            TxtApointmentTime.Text = Session["A_Time"].ToString();
            TxtDocName.Text = "Dr."+" "+Session["Doc_Name"].ToString();


            var Query = from item in db.tbl_hos_doc_appmnts
                        where item.a_date == DateTime.Parse(TxtApntmtDate.Text).ToString("yyyy-MM-dd") && item.a_time == TxtApointmentTime.Text && item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == Session["Doc_Id"].ToString()
                        select item;
            if(Query.Count()>0)
            {

            foreach( var ss in Query)
            {
                TxtReasonToVisit.Text = ss.a_reason.ToString();
                TxtPayment.Text = ss.a_payment.ToString();
                TxtPatient.Text = ss.u_id.ToString();
            }
            }
        }
        else
        {
            Response.Redirect("~/Index/Hospita Login.aspx");
        }
    }

    public void CheckLocation()
    {
        var query = from item in db.tbl_hos_locations
                    join item1 in db.tbl_hospitalregs on item.h_id equals item1.h_id
                    where item1.h_hakkimid == Session["hakkeemid_h"].ToString()
                    select new { item1.h_id, item.latitude };
       
            if (query.Count() <= 0)
            {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location')</Script>");
            //Label8.Text = "You must set your location";
            //ModalPopupExtender4.Show();
        }
       
    }
    protected void BtnCancelApointment_Click(object sender, EventArgs e)
    {
        string user = "";
        var Query = from item in db.tbl_hos_doc_appmnts
                    where item.a_date == DateTime.Parse(TxtApntmtDate.Text).ToString("yyyy-MM-dd") && item.a_time == TxtApointmentTime.Text && item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == Session["Doc_Id"].ToString()
                    select item;
        if(Query.Count() >0)
        {

        db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(Query);
        db.SubmitChanges();
        Session["A_Date"] = null;
        Session["A_Time"] = null;
        Session["Doc_Name"] = null;
            foreach(var ss in Query)
            {
                user = ss.u_id;
            }
        string mailmessage = "Your Appointment(" + " " + TxtApntmtDate.Text + "," + TxtApointmentTime.Text + " ) for "+ TxtDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
        Email.mail(user, mailmessage,"Appointment canceled");
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Appointment Canceled')</Script>");
            //Label1.Text = "Appointment Canceled";
            //this.ModalPopupExtender1.Show();
        }
    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_hos_doc_appmnts
                    where item.a_date == DateTime.Parse(TxtApntmtDate.Text).ToString("yyyy-MM-dd") && item.a_status==0 && item.a_time == TxtApointmentTime.Text && item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == Session["Doc_Id"].ToString()
                    select item;
        if (Query.Count() > 0)
        {
            foreach (var ss in Query)
            {
                ss.a_status = 1;
                db.SubmitChanges();
            }
            BtnCancelApointment.Enabled = false;
            BtnConfirm.Enabled = false;
            Response.Redirect("~/Hospital/Hospital.aspx");
        }
        else
        {
            
        }
    }

    //public void Email(string email, string msg)
    //{
    //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //    mail.To.Add(email);
    //    mail.From = new MailAddress("bookdoc2017@gmail.com", "Hakkeem", System.Text.Encoding.UTF8);
    //    mail.Subject = "Account creation";
    //    mail.SubjectEncoding = System.Text.Encoding.UTF8;
    //    mail.Body = msg;
    //    //mail.Attachment = "attachment path";
    //    mail.BodyEncoding = System.Text.Encoding.UTF8;
    //    mail.IsBodyHtml = true;
    //    mail.Priority = MailPriority.High;
    //    SmtpClient client = new SmtpClient();
    //    client.Credentials = new System.Net.NetworkCredential("bookdoc2017", "bookdoc12345");
    //    client.Port = 587;
    //    client.Host = "smtp.gmail.com";
    //    client.EnableSsl = true;
    //    try
    //    {
    //        client.Send(mail);
    //        //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        //Exception ex2 = ex;
    //        //string errorMessage = string.Empty;
    //        //while (ex2 != null)
    //        //{
    //        //    errorMessage += ex2.ToString();
    //        //    ex2 = ex2.InnerException;
    //        //}
    //        //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
    //    }
    //}

    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Hospital/Hospital.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
    }
}