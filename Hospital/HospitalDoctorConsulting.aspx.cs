using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Hospital_HospitalDoctorConsulting : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

    string DocId = "";
    string HosId = "";


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
        if (Session["HosDocId"] != null)
        {
            DocId = Session["HosDocId"].ToString();
            HosId = Session["hakkeemid_h"].ToString();
            var selectDoc = from item in db.tbl_hdoctors
                            where item.hd_email == DocId && item.h_id == HosId
                            select item;
            foreach(var s in selectDoc)
            {
                Image1.ImageUrl = s.hd_photo;
                Label1.Text = s.hd_name;
            }

            if (!IsPostBack)
            {
                LblCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TodayAppointments();
            }
        }
        else
        {
            Response.Redirect("~/Hospital/HospitalDoctorLogin.aspx");
        }


    }

    public void TodayAppointments()
    {
        //var select = from item in db.tbl_hos_doc_appmnts
        //             join item1 in db.tbl_signups on item.u_id equals item1.name
        //             where item.a_date == LblCurrentDate.Text && item.d_id == DocId && item.h_id == HosId && item.a_status==1
                    
        //             select new {item.a_date,item.a_reason,item.a_status,item.a_time,item.d_id,item.h_id,item.id,item1.name};
        //if(select.Count() >0)
        //{
        var Query = from item in db.tbl_hos_doc_appmnts
                    join item1 in db.tbl_signups on item.u_id equals item1.email
                    where item.a_date == LblCurrentDate.Text && item.d_id == DocId && item.h_id == HosId && item.a_status == 1
                    orderby item.a_time ascending
                    select new { item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name,item.u_id };
        if(Query.Count() >0)
        {

        GridView1.DataSource = Query;
            GridView1.DataBind();
        }
        //}
    }

   
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string Id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
        Label LblReason = GridView1.Rows[e.RowIndex].FindControl("LblReason") as Label;
        Label LblPatientName = GridView1.Rows[e.RowIndex].FindControl("LblPatientName") as Label;
        Label LblId = GridView1.Rows[e.RowIndex].FindControl("LblPatId") as Label;
        //Label lbl = GridView1.Rows[e.RowIndex].FindControl("") as Label;
        LblApoId.Text = Id;
        LblPatId.Text = LblId.Text;
        TxtApointmentTime.Text = LblTime.Text;
        TxtPatientName.Text = LblPatientName.Text;
        TxtReasonToVisit.Text = LblReason.Text;
        TxtPrescription.Enabled = true;
        TxtDiagnose.Enabled = true;
        BtnComplete.Enabled = true;
        LnkPrevious.Enabled = true;
        TodayAppointments();
    }
    protected void BtnComplete_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_hos_doc_appmnts
                    where item.id == Convert.ToInt64(LblApoId.Text)
                    select item;
        if(Query.Count()>0)
        {
            foreach(var ss in Query)
            {
                string d = DateTime.Parse(ss.a_date).ToString("yyyy-MM-dd");
                tbl_hos_appmnt_history tbAp = new tbl_hos_appmnt_history()
                {
                    a_date=DateTime.Parse(d),
                    a_doc_daignose=TxtDiagnose.Text,
                    a_doc_prescriptions=TxtPrescription.Text,
                    a_payment=ss.a_payment.ToString(),
                    a_reason=ss.a_reason.ToString(),
                    a_time=ss.a_time.ToString(),
                    d_id=ss.d_id.ToString(),
                    h_id=ss.h_id.ToString(),
                    u_id=ss.u_id.ToString(),
                };
                db.tbl_hos_appmnt_histories.InsertOnSubmit(tbAp);
                db.SubmitChanges();
            }
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(Query);
            db.SubmitChanges();
            BtnComplete.Enabled = false;
            LnkPrevious.Enabled = false;
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Consulting completed. Thank you.')</Script>");
            //Label2.Text = "Consulting completed. Thank you.";
            //this.ModalPopupExtender2.Show();
        }
        TodayAppointments();
       
    }
    protected void LnkPrevious_Click(object sender, EventArgs e)
    {
        var selectQuery = from user in db.tbl_hos_appmnt_histories
                          where user.h_id == HosId && user.u_id == LblPatId.Text
                          select user;
        if (selectQuery.Count() > 0)
        {
            PatientHistory();
            //Timer mtimer = Master.FindControl("Timer1") as Timer;
            //mtimer.Enabled = false;
            Timer1.Enabled = false;
            //this.ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>swal('This patient has not previous consulting history in this hospital')</Script>");

            //Label2.Text = "This patient has not previous consulting history in this hospital";
            //this.ModalPopupExtender2.Show();
        }

    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        TodayAppointments();
    }

    public void PatientHistory()
    {
        var Query = from item in db.tbl_hos_appmnt_histories
                    join item1 in db.tbl_hdoctors on item.d_id equals item1.hd_email
                    where item.u_id == LblPatId.Text
                    orderby item.a_date ascending
                    select new {item.a_date,item.a_doc_daignose,item.a_doc_prescriptions,item.d_id,item.h_id,item.id,item1.hd_name };
        GridView2.DataSource = Query.Take(5).ToList();
        GridView2.DataBind();
        foreach(GridViewRow grow in GridView2.Rows)
        {
            Label LblHosId1 = grow.FindControl("LblHosId1") as Label;
            Label LblHospital = grow.FindControl("LblHospital") as Label;
            var query1 = from a in db.tbl_hospitalregs
                         where a.h_email == LblHosId1.Text
                         select a;
            foreach(var ss in query1)
            {
                LblHospital.Text = ss.h_name.ToString();
            }
                 
        }
   
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
    }
}