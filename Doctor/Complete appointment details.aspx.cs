using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctor_Complete_appointment_details : System.Web.UI.Page
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
        //    this.MasterPageFile = "~/Doctor/ArabicMasterPage.master";
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if(!IsPostBack)
        {
            CheckLocation();
            today();
        }
    }
    public void CheckLocation()
    {
        var query = from item in db.tbl_doctor_locations
                    join item1 in db.tbl_doctors on item.d_id equals item1.d_id
                    where item1.d_hakkimid == Session["hakkeemid_d"].ToString()
                    select new { item1.d_name, item.latitude, item.longitude, item1.d_specialties, item1.d_address, item1.d_photo, item1.d_email, item1.d_id };
        if (query.Count() <= 0)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Doctor/SetLocation.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Doctor/SetLocation.aspx?l=ar-EG");
            //}
        }
    }
    public void today()
    {
        var Query = from item in db.tbl_doctor_appointments where item.d_id == Session["hakkeemid_d"].ToString()&&item.a_status==1 orderby item.id descending select item;

        if (Query.Count() > 0)
        {

            GridView1.DataSource = Query;
            GridView1.DataBind();

            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lbl4 = new Label();
                lbl4 = gr.FindControl("Label4") as Label;
                Label lbl5 = new Label();
                lbl5 = gr.FindControl("Label5") as Label;
                Label lbl8 = new Label();
                lbl8 = gr.FindControl("Label8") as Label;
                Label lbl9 = new Label();
                lbl9 = gr.FindControl("Label9") as Label;

                var Query1 = from item in db.tbl_signups where item.u_hakkimid == lbl4.Text select item;
                foreach (var s in Query1)
                {
                    lbl5.Text = s.name;
                }
                if (lbl8.Text == "0")
                {
                    lbl9.Text = "Waiting";
                    gr.Cells[2].BackColor = System.Drawing.Color.Orange;
                }
                else if (lbl8.Text == "1")
                {
                    lbl9.Text = "Confirm";
                    //gr.Cells[2].BackColor = System.Drawing.Color.LimeGreen;
                    gr.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#4aa9af");
                }
                else
                {
                    lbl9.Text = "Rejected";
                    //lbl9.ForeColor = System.Drawing.Color.Red;
                    gr.Cells[2].BackColor = System.Drawing.Color.IndianRed;
                }
            }
            BtnSearchPatient.Visible = true;
            btnViewAll.Visible = true;
            TxtSearchDate.Visible = true;
            Label3.Visible = true;
            Label2.Visible = true;
            Label10.Visible = true;
        }
        else
        {
            // TextBox1.Visible = false;
            BtnSearchPatient.Visible = false;
            btnViewAll.Visible = false;
            TxtSearchDate.Visible = false;
            Label3.Visible = false;
            Label2.Visible = false;
            Label10.Visible = false;
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Appointments Found')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لم يتم العثور على تعيينات')</Script>");
            //}

        }
    }
    public void SelectApointmentByDate()
    {
        var Query = from item in db.tbl_doctor_appointments where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date==DateTime.Parse(TxtSearchDate.Text).ToString("yyyy-MM-dd")&&item.a_status==1 orderby item.id descending select item;
        GridView1.DataSource = Query;
        if(Query.Count() >0)
        {
        GridView1.DataBind();

        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label lbl4 = new Label();
            lbl4 = gr.FindControl("Label4") as Label;
            Label lbl5 = new Label();
            lbl5 = gr.FindControl("Label5") as Label;
            Label lbl8 = new Label();
            lbl8 = gr.FindControl("Label8") as Label;
            Label lbl9 = new Label();
            lbl9 = gr.FindControl("Label9") as Label;

            var Query1 = from item in db.tbl_signups where item.u_hakkimid == lbl4.Text select item;
            foreach (var s in Query1)
            {
                lbl5.Text = s.name;
            }
            if (lbl8.Text == "0")
            {
                lbl9.Text = "Waiting";
                gr.Cells[2].BackColor = System.Drawing.Color.Orange;
            }
            else if (lbl8.Text == "1")
            {
                lbl9.Text = "Confirm";
                gr.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#4aa9af");
            }
            else
            {
                lbl9.Text = "Rejected";
                //lbl9.ForeColor = System.Drawing.Color.Red;
                gr.Cells[2].BackColor = System.Drawing.Color.IndianRed;
            }
        }
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Appointments on this date..')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا تعيينات في هذا التاريخ..')</Script>");
            //}
            //Label1.Text = "No Appoitments on this date..!";
            //this.ModalPopupExtender1.Show();
            today();
        }
    }
    protected void BtnSearchPatient_Click(object sender, EventArgs e)
    {
        SelectApointmentByDate();
    }
    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        today();
    }
}