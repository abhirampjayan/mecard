using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_reporttohakkeem : System.Web.UI.Page
{
    secure obj = new secure();
    SMS ob = new SMS();
    MailMessage mail = new MailMessage();
    databaseDataContext db = new databaseDataContext();
    protected override void InitializeCulture()
    {
        Session["Speciality"] = "Auto";
        string culture = "Auto";
        try
        {
            culture = Request.QueryString["l"].ToString();
            Session["Speciality"] = culture;
        }
        catch (Exception ex)
        { }
        // string culture = Session["Speciality"].ToString();
        if (string.IsNullOrEmpty(culture))
            culture = "Auto";
        //Use this
        UICulture = culture;
        Culture = culture;
        //OR This
        if (culture != "Auto")
        {

            System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
            System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
        }
        else
        {
            //LinkButton1.Text = "عربى";
        }

        base.InitializeCulture();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }




    protected void Button1_Click(object sender, EventArgs e)
    {
       try
        {
            string reason = "";
            for(int i=0;i<CheckBoxList1.Items.Count;i++)
            {
                if(CheckBoxList1.Items[i].Selected)
                {
                    if(reason=="")
                    {
                        reason = CheckBoxList1.Items[i].Text;
                    }
                    else
                    {
                        reason += "," + CheckBoxList1.Items[i].Text;
                    }
                }
            }
            if (Session["hid"] != null)
            {
                tbl_report_form trf = new tbl_report_form()
                {
                    reason = reason,
                    description = TextBox1.Text,
                    apmnt_id = int.Parse(Session["apmntid"].ToString()),
                    h_id=Session["hid"].ToString(),
                    time = DateTime.Now.ToShortTimeString(),
                    date = DateTime.Now.ToShortDateString(),
                    u_id = Session["hakkemid_u"].ToString(),
                };
                db.tbl_report_forms.InsertOnSubmit(trf);
                db.SubmitChanges();
                //var doctor=from item in db.tbl_hdoctors where item
                var user = from item in db.tbl_signups where item.u_hakkimid == Session["hakkemid_u"].ToString() select item;

                //string mailmessage="Mr."
            }
            else
            {
                tbl_report_form trf = new tbl_report_form()
                {
                    reason = reason,
                    description = TextBox1.Text,
                    apmnt_id = int.Parse(Session["apmntid"].ToString()),
                    //h_id = Session["hid"].ToString(),
                    time = DateTime.Now.ToShortTimeString(),
                    date = DateTime.Now.ToShortDateString(),
                    u_id = Session["hakkemid_u"].ToString(),
                };
                db.tbl_report_forms.InsertOnSubmit(trf);
                db.SubmitChanges();
            }
           
            Response.Redirect("~/User/UserAppointments.aspx");
        }
        catch(Exception ex)
        {

        }
    }
}