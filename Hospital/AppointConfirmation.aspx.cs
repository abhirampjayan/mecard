using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

public partial class Hospital_AppointConfirmation : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    MailMessage Email = new MailMessage();
    secure obj = new secure();
    SMS m = new SMS();
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
        //    this.MasterPageFile = "~/hospital/ArabichospitalMaster.master";
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;
        if (Session["hakkeemid_h"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    CheckLocation();
                }
                catch (Exception ex)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{

                        Response.Redirect("../index/hospita login.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("../index/hospita login.aspx?l=ar-EG");
                    //}
                }
                selectApointments();
            }
        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/Hospita Login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/Hospita Login.aspx?l=ar-EG");
            //}
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
            //Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
            //Label8.Text = "You must set your location";
            //ModalPopupExtender4.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب تعيين موقعك')</Script>");
            //}
        }
       
    }

    public void selectApointments()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[8] { new DataColumn("id"), new DataColumn("date"), new DataColumn("d_id"), new DataColumn("u_id"), new DataColumn("hd_name"), new DataColumn("name"), new DataColumn("a_time"), new DataColumn("a_reason") });

        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        var Query = from item in db.tbl_hos_doc_appmnts
                    from item1 in db.tbl_signups
                    where   (item.u_id ==  item1.email || item.u_id == item1.u_hakkimid) && item.h_id == Session["hakkeemid_h"].ToString() && (item.u_id.Contains(TxtSearchPatient.Text) || item1.name.Contains(TxtSearchPatient.Text))
                    &&item.a_status!=2
                    orderby item.a_date, item.a_time ascending

                    select new {item.u_id, item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name,item1.u_hakkimid };

        if (Query.Count() > 0)
        {
            GridView1.DataSource = Query;
            GridView1.DataBind();
            foreach (GridViewRow grow in GridView1.Rows)
            {
                Label LblDocId = grow.FindControl("LblDocId") as Label;
                Label LblUserId = grow.FindControl("LblUserId") as Label;
                Label LblDocName = grow.FindControl("LblDocName") as Label;
                Label LblH = grow.FindControl("LblH") as Label;
                Label LblPatientName = grow.FindControl("LblPatientName") as Label;
                Label LblStatus = grow.FindControl("LblStatus") as Label;
                LinkButton LnkConfirm = grow.FindControl("LnkConfirm") as LinkButton;
                LinkButton LinkButton2 = grow.FindControl("LinkButton2") as LinkButton;

                //DateTime dte=DateTime.Parse(LinkButton2.Text);
                //DateTime dte1=DateTime.Parse(currentDate);


                //if (dte == dte1)
                //{
                    if (LblStatus.Text == "0")
                    {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        LnkConfirm.Text = "Confirm";
                    //}else
                    //{
                    //    LnkConfirm.Text = "تؤكد";
                    //}
                        LnkConfirm.ForeColor = System.Drawing.Color.Green;
                    }
                    else if(LblStatus.Text=="1")
                    {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        LnkConfirm.Text = "Confirmed";
                    //}
                    //else
                    //{
                    //    LnkConfirm.Text = "تم تأكيد";
                    //}
                        LnkConfirm.ForeColor = System.Drawing.Color.Red;
                        LnkConfirm.Enabled = false;
                    }
                   
                //}
                //else
                //{
                //    if (LblStatus.Text == "0")
                //    {
                //        LnkConfirm.Text = "Confirm";
                //        LnkConfirm.ForeColor = System.Drawing.Color.Green;
                //        LnkConfirm.Enabled = false;
                //    }
                //    else
                //    {
                //        LnkConfirm.Text = "Confirmed";
                //        LnkConfirm.ForeColor = System.Drawing.Color.Red;
                //        LnkConfirm.Enabled = false;
                //    }
                //}
                

                var query1 = from a in db.tbl_hdoctors
                             where a.hd_email == LblDocId.Text
                             select a;
                foreach (var s in query1)
                {
                    LblDocName.Text = s.hd_name.ToString();
                }
                //var query2 = from aa in db.tbl_signups
                //             where aa.email == LblUserId.Text
                //             select aa;
                //foreach (var ss in query2)
                //{
                //    LblPatientName.Text = ss.name.ToString();
                //}
            }
        }
        else
        {

            //Label1.Text = "No appointments";
            //this.ModalPopupExtender1.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointments')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا التعيينات')</Script>");
            //}
        }

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      
		 #region ByUserName
        if (TxtSearchPatient.Text != "" && TxtDate.Text == "" && TxtDoctorName.Text == "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            LinkButton LnkConfirm = GridView1.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
            if (LnkConfirm.Text == "Confirm")
            {
                var select = from item in db.tbl_hos_doc_appmnts
                             where item.id == Convert.ToInt64(id)
                             select item;
                if (select.Count() > 0)
                {
                    foreach (var ss in select)
                    {
                        ss.a_status = 1;
                        db.SubmitChanges();
                    }
                }
                SelectByUserIdName();
            }
            else
            {
                SelectByUserIdName();
            }
        } 
        #endregion

        #region ByDocName
        else if (TxtSearchPatient.Text == "" && TxtDate.Text == "" && TxtDoctorName.Text != "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            LinkButton LnkConfirm = GridView1.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
            if (LnkConfirm.Text == "Confirm")
            {
                var select = from item in db.tbl_hos_doc_appmnts
                             where item.id == Convert.ToInt64(id)
                             select item;
                if (select.Count() > 0)
                {
                    foreach (var ss in select)
                    {
                        ss.a_status = 1;
                        db.SubmitChanges();
                    }
                }
                SelectByDocName();
            }
            else
            {
                SelectByDocName();
            }
        } 
        #endregion

        #region ByDate
        else if (TxtSearchPatient.Text == "" && TxtDate.Text != "" && TxtDoctorName.Text == "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            LinkButton LnkConfirm = GridView1.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
            if (LnkConfirm.Text == "Confirm")
            {
                var select = from item in db.tbl_hos_doc_appmnts
                             where item.id == Convert.ToInt64(id)
                             select item;
                if (select.Count() > 0)
                {
                    foreach (var ss in select)
                    {
                        ss.a_status = 1;
                        db.SubmitChanges();
                    }
                }
                SelectByDate();
            }
            else
            {
                SelectByDate();
            }
        }
        #endregion

        #region ByNameDocName
        else if (TxtSearchPatient.Text != "" && TxtDate.Text == "" && TxtDoctorName.Text != "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            LinkButton LnkConfirm = GridView1.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
            if (LnkConfirm.Text == "Confirm")
            {
                var select = from item in db.tbl_hos_doc_appmnts
                             where item.id == Convert.ToInt64(id)
                             select item;
                if (select.Count() > 0)
                {
                    foreach (var ss in select)
                    {
                        ss.a_status = 1;
                        db.SubmitChanges();
                    }
                }
                SelectByUserIdNameDocName();
            }
            else
            {
                SelectByUserIdNameDocName();
            }
        }
        #endregion

        #region ByNameDate
        else if (TxtSearchPatient.Text != "" && TxtDate.Text != "" && TxtDoctorName.Text == "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            LinkButton LnkConfirm = GridView1.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
            if (LnkConfirm.Text == "Confirm")
            {
                var select = from item in db.tbl_hos_doc_appmnts
                             where item.id == Convert.ToInt64(id)
                             select item;
                if (select.Count() > 0)
                {
                    foreach (var ss in select)
                    {
                        ss.a_status = 1;
                        db.SubmitChanges();
                    }
                }
                SelectByUserIdNameDate();
            }
            else
            {
                SelectByUserIdNameDate();
            }
        }
        #endregion

        #region ByDocDate
        else if (TxtSearchPatient.Text == "" && TxtDate.Text != "" && TxtDoctorName.Text != "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            LinkButton LnkConfirm = GridView1.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
            if (LnkConfirm.Text == "Confirm")
            {
                var select = from item in db.tbl_hos_doc_appmnts
                             where item.id == Convert.ToInt64(id)
                             select item;
                if (select.Count() > 0)
                {
                    foreach (var ss in select)
                    {
                        ss.a_status = 1;
                        db.SubmitChanges();
                    }
                }
                SelectByDocNameDate();
            }
            else
            {
                SelectByDocNameDate();
            }
        }
        #endregion

        #region ByALL
        else if (TxtSearchPatient.Text != "" && TxtDate.Text != "" && TxtDoctorName.Text != "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            LinkButton LnkConfirm = GridView1.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
            if (LnkConfirm.Text == "Confirm")
            {
                var select = from item in db.tbl_hos_doc_appmnts
                             where item.id == Convert.ToInt64(id)
                             select item;
                if (select.Count() > 0)
                {
                    foreach (var ss in select)
                    {
                        ss.a_status = 1;
                        db.SubmitChanges();
                    }
                }
                SelectByDocNameDateUserIdName();
            }
            else
            {
                SelectByDocNameDateUserIdName();
            }
        }
        #endregion

        else
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            LinkButton LnkConfirm = GridView1.Rows[e.RowIndex].FindControl("LnkConfirm") as LinkButton;
            if (LnkConfirm.Text == "Confirm"||LnkConfirm.Text== "تؤكد")
            {
                var select = from item in db.tbl_hos_doc_appmnts
                             where item.id == Convert.ToInt64(id)
                             select item;
                if (select.Count() > 0)
                {
                    foreach (var ss in select)
                    {
                        ss.a_status = 1;
                        db.SubmitChanges();
                    }
                }
                selectApointments();
            }
            else
            {
                selectApointments();
            }
        }

    }
    protected void BtnSearchPatient_Click(object sender, EventArgs e)
    {
         if(TxtSearchPatient.Text !="" && TxtDoctorName.Text =="" && TxtDate.Text=="")
        {
            SelectByUserIdName();
        }
        else if (TxtSearchPatient.Text != "" && TxtDoctorName.Text != "" && TxtDate.Text == "")
        {
            SelectByUserIdNameDocName();
        }
        else if (TxtSearchPatient.Text != "" && TxtDoctorName.Text == "" && TxtDate.Text != "")
        {
            SelectByUserIdNameDate();
        }
        else if (TxtSearchPatient.Text != "" && TxtDoctorName.Text != "" && TxtDate.Text != "")
        {
            SelectByDocNameDateUserIdName();
        }
        else if (TxtSearchPatient.Text == "" && TxtDoctorName.Text != "" && TxtDate.Text != "")
        {
            SelectByDocNameDate();
        }
             else if(TxtSearchPatient.Text == "" && TxtDoctorName.Text != "" && TxtDate.Text == "")
         {
             SelectByDocName();
         }
             else if(TxtSearchPatient.Text == "" && TxtDoctorName.Text == "" && TxtDate.Text != "")
         {
             SelectByDate();
         }
        else
        {
            selectApointments();
        }

    }

    public void ClearData()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
    }

    protected void BtnViewAll_Click(object sender, EventArgs e)
    {
        ClearData();
        TxtDoctorName.Text = "";
        TxtDate.Text = "";
        TxtSearchPatient.Text = "";
        selectApointments();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        #region ByUserName
        string docph = "";
        if (TxtSearchPatient.Text != "" && TxtDate.Text == "" && TxtDoctorName.Text == "")
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            Label LblUserId = GridView1.Rows[e.RowIndex].FindControl("LblUserId") as Label;
            Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
            Label LblDocName = GridView1.Rows[e.RowIndex].FindControl("LblDocName") as Label;
            LinkButton LinkButton2 = GridView1.Rows[e.RowIndex].FindControl("LinkButton2") as LinkButton;

            var select = from item in db.tbl_hos_doc_appmnts
                         where item.id == Convert.ToInt32(id)
                         select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(select);
            db.SubmitChanges();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + LinkButton2.Text + "'", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
            }
            string mailmessage = "Your Appointment(" + " " + LinkButton2.Text + "," + LblTime.Text + " ) for Dr." + " " + LblDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
            //  Email.mail(LblUserId.Text, mailmessage, "Appointment canceled");
            Email_To_AppoinmentCancilationPatient(LblUserId.Text, LblDocName.Text, LinkButton2.Text, LblTime.Text,Session["CDay"].ToString());
            try
            {
                con.Open();
                DataTable dff = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='"+ LblUserId.Text+"'",con);
                sda1.Fill(dff);
                if(dff.Rows.Count>0)
                {
                    docph = dff.Rows[0]["hd_contact"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            
            string   docmsg = "Your Appointment has been Cancelled with Patient " + LblDocName.Text + " on " + LinkButton2.Text + " and " + LblTime.Text + " Thank you Hakkeem Team";
            string ph = "+966" + docph;
            m.Message(ph, docmsg);
            string ph1 = "+91" + docph;
            m.Message(ph1, docmsg);
            SelectByUserIdName();
        }
        #endregion

        #region ByDocName
        else if (TxtSearchPatient.Text == "" && TxtDate.Text == "" && TxtDoctorName.Text != "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            Label LblUserId = GridView1.Rows[e.RowIndex].FindControl("LblUserId") as Label;
            Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
            Label LblDocName = GridView1.Rows[e.RowIndex].FindControl("LblDocName") as Label;
            LinkButton LinkButton2 = GridView1.Rows[e.RowIndex].FindControl("LinkButton2") as LinkButton;

            var select = from item in db.tbl_hos_doc_appmnts
                         where item.id == Convert.ToInt32(id)
                         select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(select);
            db.SubmitChanges();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + LinkButton2.Text + "'", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
            }
            string mailmessage = "Your Appointment(" + " " + LinkButton2.Text + "," + LblTime.Text + " ) for Dr." + " " + LblDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
            Email_To_AppoinmentCancilationPatient(LblUserId.Text, LblDocName.Text, LinkButton2.Text, LblTime.Text,Session["CDay"].ToString());
            //Email.mail(LblUserId.Text, mailmessage,"Appointment canceled");
            try
            {
                con.Open();
                DataTable dff = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + LblUserId.Text + "'", con);
                sda1.Fill(dff);
                if (dff.Rows.Count > 0)
                {
                    docph = dff.Rows[0]["hd_contact"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            string docmsg = "Your Appointment has been Cancelled with Patient " + LblDocName.Text + " on " + LinkButton2.Text + " and " + LblTime.Text + " Thank you Hakkeem Team";
            string ph = "+966" + docph;
            m.Message(ph, docmsg);
            string ph1 = "+91" + docph;
            m.Message(ph1, docmsg);
            SelectByDocName();

        }
        #endregion

        #region ByDate
        else if (TxtSearchPatient.Text == "" && TxtDate.Text != "" && TxtDoctorName.Text == "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            Label LblUserId = GridView1.Rows[e.RowIndex].FindControl("LblUserId") as Label;
            Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
            Label LblDocName = GridView1.Rows[e.RowIndex].FindControl("LblDocName") as Label;
            LinkButton LinkButton2 = GridView1.Rows[e.RowIndex].FindControl("LinkButton2") as LinkButton;

            var select = from item in db.tbl_hos_doc_appmnts
                         where item.id == Convert.ToInt32(id)
                         select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(select);
            db.SubmitChanges();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + LinkButton2.Text + "'", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
            }
            string mailmessage = "Your Appointment(" + " " + LinkButton2.Text + "," + LblTime.Text + " ) for Dr." + " " + LblDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
            Email_To_AppoinmentCancilationPatient(LblUserId.Text, LblDocName.Text, LinkButton2.Text, LblTime.Text,Session["CDay"].ToString());
            // Email.mail(LblUserId.Text, mailmessage, "Appointment canceled");
            try
            {
                con.Open();
                DataTable dff = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + LblUserId.Text + "'", con);
                sda1.Fill(dff);
                if (dff.Rows.Count > 0)
                {
                    docph = dff.Rows[0]["hd_contact"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            string docmsg = "Your Appointment has been Cancelled with Patient " + LblDocName.Text + " on " + LinkButton2.Text + " and " + LblTime.Text + " Thank you Hakkeem Team";
            string ph = "+966" + docph;
            m.Message(ph, docmsg);
            string ph1 = "+91" + docph;
            m.Message(ph1, docmsg);
            SelectByDate();

        }
        #endregion

        #region ByNameDocName
        else if (TxtSearchPatient.Text != "" && TxtDate.Text == "" && TxtDoctorName.Text != "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            Label LblUserId = GridView1.Rows[e.RowIndex].FindControl("LblUserId") as Label;
            Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
            Label LblDocName = GridView1.Rows[e.RowIndex].FindControl("LblDocName") as Label;
            LinkButton LinkButton2 = GridView1.Rows[e.RowIndex].FindControl("LinkButton2") as LinkButton;

            var select = from item in db.tbl_hos_doc_appmnts
                         where item.id == Convert.ToInt32(id)
                         select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(select);
            db.SubmitChanges();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + LinkButton2.Text + "'", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
            }
            string mailmessage = "Your Appointment(" + " " + LinkButton2.Text + "," + LblTime.Text + " ) for Dr." + " " + LblDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
            Email_To_AppoinmentCancilationPatient(LblUserId.Text, LblDocName.Text, LinkButton2.Text, LblTime.Text,Session["CDay"].ToString());
            //Email.mail(LblUserId.Text, mailmessage,"Appointment canceled");
            try
            {
                con.Open();
                DataTable dff = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + LblUserId.Text + "'", con);
                sda1.Fill(dff);
                if (dff.Rows.Count > 0)
                {
                    docph = dff.Rows[0]["hd_contact"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            string docmsg = "Your Appointment has been Cancelled with Patient " + LblDocName.Text + " on " + LinkButton2.Text + " and " + LblTime.Text + " Thank you Hakkeem Team";
            string ph = "+966" + docph;
            m.Message(ph, docmsg);
            string ph1 = "+91" + docph;
            m.Message(ph1, docmsg);
            SelectByUserIdNameDocName();

        }
        #endregion

        #region ByNameDate
        else if (TxtSearchPatient.Text != "" && TxtDate.Text != "" && TxtDoctorName.Text == "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            Label LblUserId = GridView1.Rows[e.RowIndex].FindControl("LblUserId") as Label;
            Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
            Label LblDocName = GridView1.Rows[e.RowIndex].FindControl("LblDocName") as Label;
            LinkButton LinkButton2 = GridView1.Rows[e.RowIndex].FindControl("LinkButton2") as LinkButton;

            var select = from item in db.tbl_hos_doc_appmnts
                         where item.id == Convert.ToInt32(id)
                         select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(select);
            db.SubmitChanges();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + LinkButton2.Text + "'", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
            }
            string mailmessage = "Your Appointment(" + " " + LinkButton2.Text + "," + LblTime.Text + " ) for Dr." + " " + LblDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
            Email_To_AppoinmentCancilationPatient(LblUserId.Text, LblDocName.Text, LinkButton2.Text, LblTime.Text,Session["CDay"].ToString());
            //Email.mail(LblUserId.Text, mailmessage,"Appointment canceled");
            try
            {
                con.Open();
                DataTable dff = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + LblUserId.Text + "'", con);
                sda1.Fill(dff);
                if (dff.Rows.Count > 0)
                {
                    docph = dff.Rows[0]["hd_contact"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            string docmsg = "Your Appointment has been Cancelled with Patient " + LblDocName.Text + " on " + LinkButton2.Text + " and " + LblTime.Text + " Thank you Hakkeem Team";
            string ph = "+966" + docph;
            m.Message(ph, docmsg);
            string ph1 = "+91" + docph;
            m.Message(ph1, docmsg);
            SelectByUserIdNameDate();

        }
        #endregion

        #region ByDocDate
        else if (TxtSearchPatient.Text == "" && TxtDate.Text != "" && TxtDoctorName.Text != "")
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            Label LblUserId = GridView1.Rows[e.RowIndex].FindControl("LblUserId") as Label;
            Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
            Label LblDocName = GridView1.Rows[e.RowIndex].FindControl("LblDocName") as Label;
            LinkButton LinkButton2 = GridView1.Rows[e.RowIndex].FindControl("LinkButton2") as LinkButton;

            var select = from item in db.tbl_hos_doc_appmnts
                         where item.id == Convert.ToInt32(id)
                         select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(select);
            db.SubmitChanges();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + LinkButton2.Text + "'", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
            }
            string mailmessage = "Your Appointment(" + " " + LinkButton2.Text + "," + LblTime.Text + " ) for Dr." + " " + LblDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
            Email_To_AppoinmentCancilationPatient(LblUserId.Text, LblDocName.Text, LinkButton2.Text, LblTime.Text,Session["CDay"].ToString());
            // Email.mail(LblUserId.Text, mailmessage,"Appointment canceled");
            try
            {
                con.Open();
                DataTable dff = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + LblUserId.Text + "'", con);
                sda1.Fill(dff);
                if (dff.Rows.Count > 0)
                {
                    docph = dff.Rows[0]["hd_contact"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            string docmsg = "Your Appointment has been Cancelled with Patient " + LblDocName.Text + " on " + LinkButton2.Text + " and " + LblTime.Text + " Thank you Hakkeem Team";
            string ph = "+966" + docph;
            m.Message(ph, docmsg);
            string ph1 = "+91" + docph;
            m.Message(ph1, docmsg);
            SelectByDocNameDate();

        }
        #endregion

        #region ByALL
        else if (TxtSearchPatient.Text != "" && TxtDate.Text != "" && TxtDoctorName.Text != "")
        {

            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            Label LblUserId = GridView1.Rows[e.RowIndex].FindControl("LblUserId") as Label;
            Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
            Label LblDocName = GridView1.Rows[e.RowIndex].FindControl("LblDocName") as Label;
            LinkButton LinkButton2 = GridView1.Rows[e.RowIndex].FindControl("LinkButton2") as LinkButton;

            var select = from item in db.tbl_hos_doc_appmnts
                         where item.id == Convert.ToInt32(id)
                         select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(select);
            db.SubmitChanges();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + LinkButton2.Text + "'", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
            }
            string mailmessage = "Your Appointment(" + " " + LinkButton2.Text + "," + LblTime.Text + " ) for Dr." + " " + LblDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
            Email_To_AppoinmentCancilationPatient(LblUserId.Text, LblDocName.Text, LinkButton2.Text, LblTime.Text,Session["CDay"].ToString());
            // Email.mail(LblUserId.Text, mailmessage,"Appointment canceled");
            try
            {
                con.Open();
                DataTable dff = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + LblUserId.Text + "'", con);
                sda1.Fill(dff);
                if (dff.Rows.Count > 0)
                {
                    docph = dff.Rows[0]["hd_contact"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            string docmsg = "Your Appointment has been Cancelled with Patient " + LblDocName.Text + " on " + LinkButton2.Text + " and " + LblTime.Text + " Thank you Hakkeem Team";
            string ph = "+966" + docph;
            m.Message(ph, docmsg);
            string ph1 = "+91" + docph;
            m.Message(ph1, docmsg);
            SelectByDocNameDateUserIdName();
        }
        #endregion
        else
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            Label LblUserId = GridView1.Rows[e.RowIndex].FindControl("LblUserId") as Label;
            Label LblTime = GridView1.Rows[e.RowIndex].FindControl("LblTime") as Label;
            Label LblDocName = GridView1.Rows[e.RowIndex].FindControl("LblDocName") as Label;
            LinkButton LinkButton2 = GridView1.Rows[e.RowIndex].FindControl("LinkButton2") as LinkButton;

            var select = from item in db.tbl_hos_doc_appmnts
                         where item.id == Convert.ToInt32(id)
                         select item;
            db.tbl_hos_doc_appmnts.DeleteAllOnSubmit(select);
            db.SubmitChanges();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + LinkButton2.Text + "'", con);
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
            }
            string mailmessage = "Your Appointment(" + " " + LinkButton2.Text + "," + LblTime.Text + " ) for Dr." + " " + LblDocName.Text + " is canceled by" + " " + Session["hakkeemid_h"].ToString();
            Email_To_AppoinmentCancilationPatient(LblUserId.Text, LblDocName.Text, LinkButton2.Text, LblTime.Text, Session["CDay"].ToString());
            //Email.mail(LblUserId.Text, mailmessage, "Appointment canceled");
            try
            {
                con.Open();
                DataTable dff = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + LblUserId.Text + "'", con);
                sda1.Fill(dff);
                if (dff.Rows.Count > 0)
                {
                    docph = dff.Rows[0]["hd_contact"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            string docmsg = "Your Appointment has been Cancelled with Patient " + LblDocName.Text + " on " + LinkButton2.Text + " and " + LblTime.Text + " Thank you Hakkeem Team";
            string ph = "+966" + docph;
            m.Message(ph, docmsg);
            string ph1 = "+91" + docph;
            m.Message(ph1, docmsg);
            selectApointments();
            TxtSearchPatient.Text = TxtDate.Text = TxtDoctorName.Text = "";
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
    //    client.Port = 25;
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

    private void SelectByUserIdName()
    {
        string curntDate = DateTime.Now.ToString("yyyy-MM-dd");

        var select = from item in db.tbl_hos_doc_appmnts
                     join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                     where item.h_id == Session["hakkeemid_h"].ToString() && (item1.u_hakkimid.Contains(TxtSearchPatient.Text) || item1.name.Contains(TxtSearchPatient.Text))
                     orderby item.a_date, item.a_time ascending
                    
                     select new { item.u_id, item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name,item1.u_hakkimid };

        if (select.Count() > 0)
        {
            GridView1.DataSource = select.Distinct().ToList();
            GridView1.DataBind();
            foreach (GridViewRow grow in GridView1.Rows)
            {
                Label LblDocName = grow.FindControl("LblDocName") as Label;
                Label LblPatientName = grow.FindControl("LblPatientName") as Label;
                Label LblDocId = grow.FindControl("LblDocId") as Label;
                Label LblStatus = grow.FindControl("LblStatus") as Label;
                LinkButton LnkConfirm = grow.FindControl("LnkConfirm") as LinkButton;
                LinkButton LinkButton2 = grow.FindControl("LinkButton2") as LinkButton;

               
                if (LblStatus.Text == "0")
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        LnkConfirm.Text = "Confirm";
                    //}
                    //else
                    //{
                    //    LnkConfirm.Text = "تؤكد";
                    //}
                    LnkConfirm.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        LnkConfirm.Text = "Confirmed";
                    //}
                    //else
                    //{
                    //    LnkConfirm.Text = "تم تأكيد";
                    //}
                    LnkConfirm.ForeColor = System.Drawing.Color.Red;
                    LnkConfirm.Enabled = false;
                }
              
                
                //foreach (var ss in select)
                //{
                //    LblPatientName.Text = ss.name;
                //}

                var query = from a in db.tbl_hdoctors
                            where a.hd_email == LblDocId.Text && a.h_id == Session["hakkeemid_h"].ToString()
                            select a;
                foreach (var s in query)
                {
                    LblDocName.Text = s.hd_name.ToString();
                }
            }
        }
        else
        {
            
           
            selectApointments();
            //Label1.Text = "No appointments";
            //this.ModalPopupExtender1.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointment')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا يوجد موعد')</Script>");
            //}
        }
    }
    public void SelectByUserIdNameDocName()
    {
        List<string> DocEmId = new List<string>();
        var query = from item in db.tbl_hdoctors
                    where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_name.Contains(TxtDoctorName.Text))
                    select item;
        if (query.Count() > 0)
        {
            foreach (var ss in query)
            {
                DocEmId.Add(ss.hd_email.ToString());
            }
            foreach (var id in DocEmId)
            {
                #region NotRec
                var select = from item in db.tbl_hos_doc_appmnts
                             join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                             where item.h_id == Session["hakkeemid_h"].ToString() && (item1.u_hakkimid.Contains(TxtSearchPatient.Text) || item1.name.Contains(TxtSearchPatient.Text)) && item.d_id == id.ToString()
                             orderby item.a_date, item.a_time ascending
                             select new { item.u_id, item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name,item1.u_hakkimid };

                if (select.Count() > 0)
                {
                    GridView1.DataSource = select.Distinct().ToList();
                    GridView1.DataBind();
                    foreach (GridViewRow grow in GridView1.Rows)
                    {
                        Label LblDocName = grow.FindControl("LblDocName") as Label;
                        Label LblPatientName = grow.FindControl("LblPatientName") as Label;
                        Label LblDocId = grow.FindControl("LblDocId") as Label;
                        Label LblStatus = grow.FindControl("LblStatus") as Label;
                        LinkButton LnkConfirm = grow.FindControl("LnkConfirm") as LinkButton;
                        LinkButton LinkButton2 = grow.FindControl("LinkButton2") as LinkButton;


                        if (LblStatus.Text == "0")
                        {
                            LnkConfirm.Text = "Confirm";
                            LnkConfirm.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            LnkConfirm.Text = "Confirmed";
                            LnkConfirm.ForeColor = System.Drawing.Color.Red;
                            LnkConfirm.Enabled = false;
                        }


                        //foreach (var ss in select)
                        //{
                        //    LblPatientName.Text = ss.name;
                        //}

                        var query1 = from a in db.tbl_hdoctors
                                     where a.hd_email == LblDocId.Text && a.h_id == Session["hakkeemid_h"].ToString()
                                     select a;
                        foreach (var s in query1)
                        {
                            LblDocName.Text = s.hd_name.ToString();
                        }
                    }
                }
                else
                {
                    selectApointments();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointment')</Script>");
                    //Label1.Text = "No appointments";
                    //this.ModalPopupExtender1.Show();

                }
                #endregion
            }
        }
        else
        {
            selectApointments();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor not found')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لم يتم العثور على الطبيب')</Script>");
            //}
            //Label1.Text = "Doctor not found";
            //this.ModalPopupExtender1.Show();
        }
    }

    public void SelectByDocNameDate()
    {
        List<string> DocEmId = new List<string>();
        var selectDate = from item in db.tbl_hos_doc_appmnts
                         where item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")
                         select item;
        if (selectDate.Count() > 0)
        {
            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_name.Contains(TxtDoctorName.Text))
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    DocEmId.Add(ss.hd_email.ToString());
                }
                foreach (var id in DocEmId)
                {
                    #region NotRec
                    var select = from item in db.tbl_hos_doc_appmnts
                                 join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                                 where item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == id.ToString() && (item.a_date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                                 orderby item.a_date, item.a_time ascending
                                 select new { item.u_id, item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name,item1.u_hakkimid };

                    if (select.Count() > 0)
                    {
                        GridView1.DataSource = select.Distinct().ToList();
                        GridView1.DataBind();
                        foreach (GridViewRow grow in GridView1.Rows)
                        {
                            Label LblDocName = grow.FindControl("LblDocName") as Label;
                            Label LblPatientName = grow.FindControl("LblPatientName") as Label;
                            Label LblDocId = grow.FindControl("LblDocId") as Label;
                            Label LblStatus = grow.FindControl("LblStatus") as Label;
                            LinkButton LnkConfirm = grow.FindControl("LnkConfirm") as LinkButton;
                            LinkButton LinkButton2 = grow.FindControl("LinkButton2") as LinkButton;


                            if (LblStatus.Text == "0")
                            {
                                LnkConfirm.Text = "Confirm";
                                LnkConfirm.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                LnkConfirm.Text = "Confirmed";
                                LnkConfirm.ForeColor = System.Drawing.Color.Red;
                                LnkConfirm.Enabled = false;
                            }


                            //foreach (var ss in select)
                            //{
                            //    LblPatientName.Text = ss.name;
                            //}

                            var query1 = from a in db.tbl_hdoctors
                                         where a.hd_email == LblDocId.Text && a.h_id == Session["hakkeemid_h"].ToString()
                                         select a;
                            foreach (var s in query1)
                            {
                                LblDocName.Text = s.hd_name.ToString();
                            }
                        }
                    }
                    else
                    {


                        selectApointments();
                        //Label1.Text = "No appointments";
                        //this.ModalPopupExtender1.Show();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointment')</Script>");

                    }
                    #endregion
                }

            }
            else
            {



                selectApointments();

                //Label1.Text = "Doctor not found";
                //this.ModalPopupExtender1.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor Not Found')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لم يتم العثور على الطبيب')</Script>");
                //}
            }
        }
        else
        {


            selectApointments();
            //Label1.Text = "No appointments";
            //this.ModalPopupExtender1.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Appointments')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا تعيينات')</Script>");
            //}
        }
       
    }

    public void SelectByDocNameDateUserIdName()
    {
        List<string> DocEmId = new List<string>();
        var selectDate = from item in db.tbl_hos_doc_appmnts
                         where item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd") 
                         select item;
        if (selectDate.Count() > 0)
        {
            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_name.Contains(TxtDoctorName.Text)) 
                        select item;
            if (query.Count() > 0)
            {
                foreach (var ss in query)
                {
                    DocEmId.Add(ss.hd_email.ToString());
                }
                foreach (var id in DocEmId)
                {
                    #region NotRec
                    var select = from item in db.tbl_hos_doc_appmnts
                                 join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                                 where item.h_id == Session["hakkeemid_h"].ToString() && item.d_id == id.ToString() && (item.a_date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd"))) && (item.u_id.Contains(TxtSearchPatient.Text) || item1.name.Contains(TxtSearchPatient.Text))
                                 orderby item.a_date, item.a_time ascending
                                 select new { item.u_id, item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name,item1.u_hakkimid };

                    if (select.Count() > 0)
                    {
                        GridView1.DataSource = select.Distinct().ToList();
                        GridView1.DataBind();
                        foreach (GridViewRow grow in GridView1.Rows)
                        {
                            Label LblDocName = grow.FindControl("LblDocName") as Label;
                            Label LblPatientName = grow.FindControl("LblPatientName") as Label;
                            Label LblDocId = grow.FindControl("LblDocId") as Label;
                            Label LblStatus = grow.FindControl("LblStatus") as Label;
                            LinkButton LnkConfirm = grow.FindControl("LnkConfirm") as LinkButton;
                            LinkButton LinkButton2 = grow.FindControl("LinkButton2") as LinkButton;


                            if (LblStatus.Text == "0")
                            {
                                LnkConfirm.Text = "Confirm";
                                LnkConfirm.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                LnkConfirm.Text = "Confirmed";
                                LnkConfirm.ForeColor = System.Drawing.Color.Red;
                                LnkConfirm.Enabled = false;
                            }


                            //foreach (var ss in select)
                            //{
                            //    LblPatientName.Text = ss.name;
                            //}

                            var query1 = from a in db.tbl_hdoctors
                                         where a.hd_email == LblDocId.Text && a.h_id == Session["hakkeemid_h"].ToString()
                                         select a;
                            foreach (var s in query1)
                            {
                                LblDocName.Text = s.hd_name.ToString();
                            }
                        }
                    }
                    else
                    {
                       
                    
                        selectApointments();
                        //Label1.Text = "No appointments";
                        //this.ModalPopupExtender1.Show();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointment')</Script>");

                    }
                    #endregion
                }

            }
            else
            {
               
                selectApointments();
                //Label1.Text = "Doctor not found";
                //this.ModalPopupExtender1.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor Not Found')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لم يتم العثور على الطبيب')</Script>");
                //}
            }
        }
        else
        {
            
            
            selectApointments();
            //Label1.Text = "No appointments";
            //this.ModalPopupExtender1.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No Appointments')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا تعيينات')</Script>");
            //}
        }

    }

    public void SelectByUserIdNameDate()
    {
        //string curntDate = DateTime.Now.ToString("yyyy-MM-dd");

        var select = from item in db.tbl_hos_doc_appmnts
                     join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                     where item.h_id == Session["hakkeemid_h"].ToString() && (item1.u_hakkimid.Contains(TxtSearchPatient.Text) || item1.name.Contains(TxtSearchPatient.Text)) && (item.a_date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                     orderby item.a_date, item.a_time ascending
                     select new { item.u_id, item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name,item1.u_hakkimid };

        if (select.Count() > 0)
        {
            GridView1.DataSource = select.Distinct().ToList();
            GridView1.DataBind();
            foreach (GridViewRow grow in GridView1.Rows)
            {
                Label LblDocName = grow.FindControl("LblDocName") as Label;
                Label LblPatientName = grow.FindControl("LblPatientName") as Label;
                Label LblDocId = grow.FindControl("LblDocId") as Label;
                Label LblStatus = grow.FindControl("LblStatus") as Label;
                LinkButton LnkConfirm = grow.FindControl("LnkConfirm") as LinkButton;
                LinkButton LinkButton2 = grow.FindControl("LinkButton2") as LinkButton;

               
                if (LblStatus.Text == "0")
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        LnkConfirm.Text = "Confirm";
                    //}
                    //else
                    //{
                    //    LnkConfirm.Text = "تؤكد";
                    //}
                    LnkConfirm.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        LnkConfirm.Text = "Confirmed";
                    //}
                    //else
                    //{
                    //    LnkConfirm.Text = "تم تأكيد";
                    //}
                    LnkConfirm.ForeColor = System.Drawing.Color.Red;
                    LnkConfirm.Enabled = false;
                }
              

                //foreach (var ss in select)
                //{
                //    LblPatientName.Text = ss.name;
                //}

                var query = from a in db.tbl_hdoctors
                            where a.hd_email == LblDocId.Text && a.h_id == Session["hakkeemid_h"].ToString()
                            select a;
                foreach (var s in query)
                {
                    LblDocName.Text = s.hd_name.ToString();
                }
            }
        }
        else
        {
            
         
            selectApointments();
            //Label1.Text = "No appointments";
            //this.ModalPopupExtender1.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointment')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا يوجد موعد')</Script>");
            //}

        }
    }

    public void SelectByDate()
    {
        var select = from item in db.tbl_hos_doc_appmnts
                     join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                     where item.h_id == Session["hakkeemid_h"].ToString() && (item.a_date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                     orderby item.a_date, item.a_time ascending
                     select new { item.u_id, item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name ,item1.u_hakkimid};

        if (select.Count() > 0)
        {
            GridView1.DataSource = select.Distinct().ToList();
            GridView1.DataBind();
            foreach (GridViewRow grow in GridView1.Rows)
            {
                Label LblDocName = grow.FindControl("LblDocName") as Label;
                Label LblPatientName = grow.FindControl("LblPatientName") as Label;
                Label LblDocId = grow.FindControl("LblDocId") as Label;
                Label LblStatus = grow.FindControl("LblStatus") as Label;
                LinkButton LnkConfirm = grow.FindControl("LnkConfirm") as LinkButton;
                LinkButton LinkButton2 = grow.FindControl("LinkButton2") as LinkButton;


                if (LblStatus.Text == "0")
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        LnkConfirm.Text = "Confirm";
                    //}
                    //else
                    //{
                    //    LnkConfirm.Text = "تؤكد";
                    //}
                    LnkConfirm.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        LnkConfirm.Text = "Confirmed";
                    //}
                    //else
                    //{
                    //    LnkConfirm.Text = "تم تأكيد";
                    //}
                    LnkConfirm.ForeColor = System.Drawing.Color.Red;
                    LnkConfirm.Enabled = false;
                }


                //foreach (var ss in select)
                //{
                //    LblPatientName.Text = ss.name;
                //}

                var query = from a in db.tbl_hdoctors
                            where a.hd_email == LblDocId.Text && a.h_id == Session["hakkeemid_h"].ToString()
                            select a;
                foreach (var s in query)
                {
                    LblDocName.Text = s.hd_name.ToString();
                }
            }
        }
        else
        {
            //RegisterStartupScript("", "<Script Language=JavaScript>alert('No appointment')</Script>");
          
            selectApointments();
            //Label1.Text = "No appointments";
            //this.ModalPopupExtender1.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointment')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا يوجد موعد')</Script>");
            //}
        }
    }

    public void SelectByDocName()
    {
        List<string> DocEmId = new List<string>();
        //DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[9] { new DataColumn("u_id"), new DataColumn("a_date"), new DataColumn("a_reason"), new DataColumn("a_status"), new DataColumn("a_time"), new DataColumn("d_id"), new DataColumn("h_id"), new DataColumn("id"), new DataColumn("name")});

        var Query = from item in db.tbl_hdoctors
                    where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_name.Contains(TxtDoctorName.Text))
                    select item;
        if (Query.Count() > 0)
        {
            foreach(var ss in Query)
            {
                DocEmId.Add(ss.hd_email.ToString());
            }
            foreach(var id in DocEmId)
            {
            var select = from item in db.tbl_hos_doc_appmnts
                         join item1 in db.tbl_signups on item.u_id equals item1.u_hakkimid
                         where item.h_id == Session["hakkeemid_h"].ToString() && item.d_id ==id.ToString()
                         orderby item.a_date, item.a_time ascending
                         select new { item.u_id, item.a_date, item.a_reason, item.a_status, item.a_time, item.d_id, item.h_id, item.id, item1.name,item1.u_hakkimid };
        
                if(select.Count() >0)
                {
                      GridView1.DataSource = select.Distinct().ToList();
                       GridView1.DataBind();
                       foreach (GridViewRow grow in GridView1.Rows)
                       {
                           Label LblDocName = grow.FindControl("LblDocName") as Label;
                           Label LblPatientName = grow.FindControl("LblPatientName") as Label;
                           Label LblDocId = grow.FindControl("LblDocId") as Label;
                           Label LblStatus = grow.FindControl("LblStatus") as Label;
                           LinkButton LnkConfirm = grow.FindControl("LnkConfirm") as LinkButton;
                           LinkButton LinkButton2 = grow.FindControl("LinkButton2") as LinkButton;


                           if (LblStatus.Text == "0")
                        {
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                LnkConfirm.Text = "Confirm";
                            //}
                            //else
                            //{
                            //    LnkConfirm.Text = "تؤكد";
                            //}
                               LnkConfirm.ForeColor = System.Drawing.Color.Green;
                           }
                           else
                           {
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                LnkConfirm.Text = "Confirmed";
                            //}
                            //else
                            //{
                            //    LnkConfirm.Text = "تم تأكيد";
                            //}
                               LnkConfirm.ForeColor = System.Drawing.Color.Red;
                               LnkConfirm.Enabled = false;
                           }

                           //var query1= from  t in db.tbl_signups 
                           //            where t.email==

                           //foreach (var ss in select)
                           //{
                           //    LblPatientName.Text = ss.name;
                           //}

                           var query = from a in db.tbl_hdoctors
                                       where a.hd_email == LblDocId.Text && a.h_id == Session["hakkeemid_h"].ToString()
                                       select a;
                           foreach (var s in query)
                           {
                               LblDocName.Text = s.hd_name.ToString();
                           }
                       }

                }
                else
                {
                   
                   
                    selectApointments();
                    //Label1.Text = "No appointments";
                    //this.ModalPopupExtender1.Show();
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('No appointment')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لا يوجد موعد')</Script>");
                    //}
                }
            }
        }
        else
        {
            
          
            selectApointments();
            //Label1.Text = "Doctor not exist";
            //this.ModalPopupExtender1.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor not exist')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('الطبيب غير موجود')</Script>");
            //}
        }

    }


    #region NotUsed
    //protected void TxtSearchPatient_TextChanged(object sender, EventArgs e)
    //{
    //    if (TxtSearchPatient.Text != "" && TxtDoctorName.Text == "" && TxtDate.Text == "")
    //    {
    //        ClearData();
    //        SelectByUserIdName();
    //    }
    //    else if (TxtSearchPatient.Text != "" && TxtDoctorName.Text != "" && TxtDate.Text == "")
    //    {
    //        ClearData();
    //        SelectByUserIdNameDocName();
    //    }
    //    else if (TxtSearchPatient.Text != "" && TxtDoctorName.Text == "" && TxtDate.Text != "")
    //    {
    //        ClearData();
    //        SelectByUserIdNameDate();
    //    }
    //    else if (TxtSearchPatient.Text != "" && TxtDoctorName.Text != "" && TxtDate.Text != "")
    //    {
    //        ClearData();
    //        SelectByDocNameDateUserIdName();
    //    }
    //    else if (TxtSearchPatient.Text == "" && TxtDoctorName.Text != "" && TxtDate.Text != "")
    //    {
    //        ClearData();
    //        SelectByDocNameDate();
    //    }
    //    else
    //    {
    //        ClearData();
    //        selectApointments();
    //    }
    //}
    //protected void TxtDate_TextChanged(object sender, EventArgs e)
    //{
    //    if (TxtDate.Text != "" && TxtSearchPatient.Text != "" && TxtDoctorName.Text == "")
    //    {
    //        ClearData();
    //        SelectByUserIdNameDate();
    //    }
    //    else if (TxtDate.Text != "" && TxtSearchPatient.Text == "" && TxtDoctorName.Text == "")
    //    {
    //        ClearData();
    //        SelectByDate();
    //    }
    //    else if (TxtDate.Text != "" && TxtSearchPatient.Text == "" && TxtDoctorName.Text != "")
    //    {
    //        ClearData();
    //        SelectByDocNameDate();
    //    }
    //    else if (TxtDate.Text != "" && TxtSearchPatient.Text != "" && TxtDoctorName.Text != "")
    //    {
    //        ClearData();
    //        SelectByDocNameDateUserIdName();
    //    }
    //    else if (TxtDate.Text == "" && TxtSearchPatient.Text != "" && TxtDoctorName.Text != "")
    //    {
    //        ClearData();
    //        SelectByUserIdNameDocName();
    //    }
    //    else
    //    {
    //        ClearData();
    //        selectApointments();
    //    }

    //}
    //protected void TxtDoctorName_TextChanged(object sender, EventArgs e)
    //{
    //    if (TxtDate.Text == "" && TxtSearchPatient.Text == "" && TxtDoctorName.Text != "")
    //    {
    //        ClearData();
    //        SelectByDocName();
    //    }
    //    else if (TxtDate.Text != "" && TxtSearchPatient.Text == "" && TxtDoctorName.Text != "")
    //    {
    //        ClearData();
    //        SelectByDocNameDate();
    //    }
    //    else if (TxtDate.Text == "" && TxtSearchPatient.Text != "" && TxtDoctorName.Text != "")
    //    {
    //        ClearData();
    //        SelectByUserIdNameDocName();
    //    }
    //    else if (TxtDate.Text != "" && TxtSearchPatient.Text != "" && TxtDoctorName.Text != "")
    //    {
    //        ClearData();
    //        SelectByDocNameDateUserIdName();
    //    }
    //    else if (TxtDate.Text != "" && TxtSearchPatient.Text != "" && TxtDoctorName.Text == "")
    //    {
    //        ClearData();
    //        SelectByUserIdNameDate();
    //    }
    //    else
    //    {
    //        ClearData();
    //        selectApointments();
    //    }
    //}

    #endregion


    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/SetHospitalLocation.aspx?l = ar - EG");
            
        //}
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName== "apmtcancel")
        {
            Session["apmtid"] = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();

        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            var apmnt = from item in db.tbl_hos_doc_appmnts where item.id == int.Parse(Session["apmtid"].ToString()) select item;
            foreach (var ss in apmnt)
            {
                var user = (from item in db.tbl_signups where item.u_hakkimid == ss.u_id select item).First();

                string msg = "Your appointment ("+ss.a_date+" / "+ss.a_time+") is cancelled because the doctor is not available at that moment";
                try
                {
                    // Email.mail(obj.DecryptString(user.email), msg, "Appointment cancelled");
                    // Email.mail(ss.d_id, "Appointment cancelled successfully", "Appointment cancelled");
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter sda1 = new SqlDataAdapter("Select DATENAME(dw,a_date) as dayname from tbl_hos_doc_appmnt where a_date='" + ss.a_date + "'", con);
                    sda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Session["CDay"] = dt1.Rows[0]["dayname"].ToString();
                    }
                    Email_To_AppoinmentCancilation(obj.DecryptString(user.email),ss.a_date, ss.a_time,Session["CDay"].ToString());
                    Email_To_AppoinmentCancilation(ss.d_id,ss.a_date, ss.a_time, Session["CDay"].ToString());

                    //string userph = obj.DecryptString(user.contact);
                    //string docph = "";
                    //string umsg = "";
                    //con.Open();
                    //SqlDataAdapter sda = new SqlDataAdapter("Select * from tbl_hdoctor where hd_email='" + ss.d_id + "' ", con);
                    //DataTable dtt = new DataTable();
                    //sda.Fill(dtt);
                    //if (dtt.Rows.Count > 0)
                    //{
                    //    docph = dtt.Rows[0]["hd_contact"].ToString();
                    //}


                    //umsg = "your appointment cancellation. Time: " + ss.a_time + ", Date: " + ss.a_date + "";
                    //string ph = "+966" + docph;
                    //m.Message(ph, umsg);
                    //string uph = "+966" + userph;
                    //m.Message(uph, umsg);


                }
                catch (Exception ex)
                {

                }
                try
                {
                    m.Message(obj.DecryptString(user.contact), msg);
                }
                catch(Exception ex)
                {

                }
                ss.a_status = 2;
            }
            db.SubmitChanges();
            selectApointments();
            //if (Session["Language"].ToString() == "Auto")
            //{
                Response.Redirect("~/Hospital/AppointConfirmation.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Hospital/AppointConfirmation.aspx?l=ar-EG");
            //}
        }
        catch(Exception ex)
        {

        }
    }

    public bool Email_To_AppoinmentCancilationPatient(string email,string name, string date, string time,string day)
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
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>Appointment Cancellation</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "your appointment cancellation." + name + ", Time:" + time + " "+day+", Date:" + date + "";
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
            mail.Subject = "Hakkeem Appointment Cancellation";
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
    public bool Email_To_AppoinmentCancilation(string email,string date, string time,string day)
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
            messagestr = messagestr + "<tr><td width='100%' style='text-align:center;font-size:20px;padding:20px 20px;background-color:#fff;color:#4aa9af;font-weight:bold'>Appointment Cancellation</td></tr>";
            messagestr = messagestr + "<tr><td width='100%' colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px;color:#4aa9af;text-align:center;'>";
            messagestr = messagestr + "your appointment cancellation. Time:" + time + " "+day+", Date:" + date + "";
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
            mail.Subject = "Hakkeem Appointment Cancellation";
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