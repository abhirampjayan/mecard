using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookDoc_Admin_report_form : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            report();
        }
    }
    public void report()
    {
        var report = from item in db.tbl_report_forms select item;
        GridView1.DataSource = report;
        GridView1.DataBind();

        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label lbl6 = gr.FindControl("Label6") as Label;
            Label lbl2 = gr.FindControl("Label2") as Label;
            Label lbl3 = gr.FindControl("Label3") as Label;
            Label lbl4 = gr.FindControl("Label4") as Label;
            Label lbl8 = gr.FindControl("Label8") as Label;
            Label lbl7 = gr.FindControl("Label7") as Label;
            if (lbl6.Text == "")
            {
                lbl7.Text = "----";
                var doctor = from item in db.tbl_doctor_appointments where item.id == int.Parse(lbl3.Text) select item;
                foreach (var ss in doctor)
                {
                    var query = from item in db.tbl_doctors where item.d_hakkimid == ss.d_id select item;
                    foreach (var d in query)
                    {
                        lbl4.Text = d.d_name;
                    }
                    var user = from item in db.tbl_signups where item.u_hakkimid == lbl2.Text select item;
                    foreach (var u in user)
                    {
                        lbl8.Text = u.name;
                    }
                }
            }
            else
            {
                var doctor = from item in db.tbl_hos_doc_appmnts where item.id == int.Parse(lbl3.Text) select item;
                foreach (var ss in doctor)
                {
                    var query = from item in db.tbl_hdoctors where item.hd_email == ss.d_id&&item.h_id==lbl6.Text select item;
                    foreach (var d in query)
                    {
                        lbl4.Text = d.hd_name;
                    }
                }
                var user = from item in db.tbl_signups where item.u_hakkimid == lbl2.Text select item;
                foreach (var u in user)
                {
                    lbl8.Text = u.name;
                }
                var hospital = from item in db.tbl_hospitalregs where item.h_hakkimid == lbl6.Text select item;
                foreach(var h in hospital)
                {
                    lbl7.Text = h.h_name;
                }
            }

        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="open")
        {
            Session["apmntid"] = e.CommandArgument.ToString();
            Response.Redirect("~/BookDoc Admin/read_report_form.aspx");
        }
    }
}