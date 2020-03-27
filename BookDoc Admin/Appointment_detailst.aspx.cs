using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

public partial class BookDoc_Admin_Appointment_detailst : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_BookDocConnectionString1"].ToString());
    string qry;
    SqlCommand cmd;
    SqlDataReader dr, dr1, dr2;
    int pagestart = 1;
    int q = 0;
    secure obj = new secure();
    MailMessage Email = new MailMessage();
    SMS ob = new SMS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelectDoctors();
            SelectHospitalDoctors();
        }
    }
    public void SelectDoctors()
    {
        var Query = from item in db.tbl_doctors where item.d_status == 1 select item;
        //con.Open();
        //DataTable dt = new DataTable();
        //SqlDataAdapter sda=new SqlDataAdapter ("Select * from tbl_doctor where '")
        DdlDoctors.DataSource = Query;
        DdlDoctors.DataValueField = "d_hakkimid";
        DdlDoctors.DataTextField = "d_name";
        DdlDoctors.DataBind();
        DdlDoctors.Items.Insert(0, "---Select Doctor---");
        //if (Session["Language"].ToString() == "Auto")
        //{

        //}
        //else
        //{
        //    DdlDoctors.Items.Insert(0, "---حدد الطبيب---");
        //}
        //if (Session["selecteddoctor"] != null)
        //{
        //    var Query1 = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_email == Session["selecteddoctor"].ToString() select item.hd_name;
        //    foreach(var name in Query1)
        //    DdlDoctors.Items.Insert(0, name);
        //}
    }
    public void SelectHospitalDoctors()
    {

        con.Open();
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter("Select hosreg.*,hdreg.* from tbl_hospitalreg hosreg inner join tbl_hdoctor hdreg on hdreg.h_id=hosreg.h_hakkimid where hosreg.h_status='1'", con);
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlhosdoctor.DataSource = dt;
            ddlhosdoctor.DataValueField = "hd_email";
            ddlhosdoctor.DataTextField = "hd_name";
            ddlhosdoctor.DataBind();
            ddlhosdoctor.Items.Insert(0, "---Select Hospital Doctor---");
        }

    }

    protected void DdlDoctors_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataList2.Visible = false;
        DataList4.Visible = true;



        try
        {
            var Query = from item in db.doctor_availabilities where item.d_id == DdlDoctors.SelectedValue.ToString() orderby item.a_date ascending select item;


            DataList4.DataSource = Query;
            DataList4.DataBind();
            foreach (DataListItem dli in DataList4.Items)
            {
                var Query4 = from item in db.doctor_availabilities where item.d_id == DdlDoctors.SelectedValue.ToString() select item;



                Label lbl6 = dli.FindControl("Label6") as Label;
                DataList dl3 = dli.FindControl("DataList5") as DataList;
                var Query2 = from item in db.view_doc_available_times where item.a_date == lbl6.Text && item.d_hakkimid == DdlDoctors.SelectedValue.ToString() select item;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("date"), new DataColumn("time"), new DataColumn("d_hakkimid") });
                foreach (var ss in Query2)
                {
                    if (ss.time == "")
                    { }
                    else
                    {
                        string ab1 = "", ab2 = "", timet = "";
                        int l = ss.time.ToString().Count();
                        ab1 = ss.time.ToString().Substring(0, l - 2);
                        ab2 = ss.time.ToString().Substring(l - 2, 2);
                        timet = ab1.ToString() + " " + ab2.ToString();

                        dt.Rows.Add(ss.a_date, timet, ss.d_hakkimid);
                    }

                }
                dl3.DataSource = dt;
                dl3.DataBind();
                lbl6.Text = DateTime.Parse(lbl6.Text).ToString("yyy d MMM");
                foreach (DataListItem dl3i in dl3.Items)
                {
                    //Button btn2 = dl3i.FindControl("Button2") as Button;
                    LinkButton lnk2 = dl3i.FindControl("LinkButton2") as LinkButton;
                    string date = DateTime.Parse(lbl6.Text).ToString("yyyy-MM-dd");
                    var Query3 = from item in db.tbl_doctor_appointments
                                 where
                        item.d_id == DdlDoctors.SelectedValue.ToString() && item.a_date == date && item.a_time == lnk2.Text
                                 select item;
                    if (Query3.Count() > 0)
                    {
                        foreach (var tt in Query3)
                        {
                            if (tt.a_status == 0)
                            {
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "Waiting for confirmation";
                                //}
                                //else
                                //{
                                //    lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "في انتظار التأكيد";
                                //}
                            }
                            if (tt.a_status == 1 || tt.a_status == 4)
                            {
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false; lnk2.ToolTip = "Booked";
                                //}
                                //else
                                //{
                                //    lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false; lnk2.ToolTip = "حجز";
                                //}
                            }
                        }

                    }
                    else
                    {
                        //lnk2.BackColor = System.Drawing.Color.Green;
                        //if (Session["Speciality"].ToString() == "Auto")
                        //{
                        lnk2.ToolTip = "Available";
                        //}
                        //else
                        //{
                        //    lnk2.ToolTip = "متاح";
                        //}
                    }
                }
            }
        }
        catch (Exception ex) { }

    }




    


    protected void ddlhosdoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        try
        {
            DataList2.Visible = true;
            DataList4.Visible = false;
            var Query = from item in db.tbl_hos_doc_availables where item.hd_id == ddlhosdoctor.SelectedValue.ToString() orderby item.date ascending select item;


            DataList2.DataSource = Query;
            DataList2.DataBind();
            foreach (DataListItem dli in DataList2.Items)

            {
                var Query4 = from item in db.tbl_hos_doc_availables where item.hd_id == ddlhosdoctor.SelectedValue.ToString() select item;




                Label lbl6 = dli.FindControl("Label6") as Label;
                DataList dl3 = dli.FindControl("DataList3") as DataList;
                var Query2 = from item in db.view_hos_doc_available_times where item.date == lbl6.Text && item.hd_email == ddlhosdoctor.SelectedValue.ToString() select item;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("date"), new DataColumn("time"), new DataColumn("email") });
                foreach (var ss in Query2)
                {
                    if (ss.time == "")
                    { }
                    else
                    {
                        int l = ss.time.ToString().Count();
                        string ab1 = ss.time.ToString().Substring(0, l - 2);
                        string ab2 = ss.time.ToString().Substring(l - 2, 2);
                        string timet = ab1.ToString() + " " + ab2.ToString();


                        dt.Rows.Add(ss.date, timet, ss.hd_email);
                    }

                }
                dl3.DataSource = dt;
                dl3.DataBind();
                lbl6.Text = DateTime.Parse(lbl6.Text).ToString("ddd d MMM");
                foreach (DataListItem dl3i in dl3.Items)
                {
                    //Button btn2 = dl3i.FindControl("Button2") as Button;
                    LinkButton lnk2 = dl3i.FindControl("LinkButton2") as LinkButton;
                    string date = DateTime.Parse(lbl6.Text).ToString("yyyy-MM-dd");
                    var Query3 = from item in db.tbl_hos_doc_appmnts
                                 where
                        item.d_id == ddlhosdoctor.SelectedValue.ToString() && item.a_date == date && item.a_time == lnk2.Text
                                 select item;
                    if (Query3.Count() > 0)
                    {
                        foreach (var tt in Query3)
                        {
                            if (tt.a_status == 0)
                            {
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "Waiting for confirmation";
                                //}
                                //else
                                //{
                                //    lnk2.BackColor = System.Drawing.Color.Orange; lnk2.Enabled = false; lnk2.ToolTip = "في انتظار التأكيد";

                                //}
                            }
                            if (tt.a_status == 1)
                            {
                                //if (Session["Speciality"].ToString() == "Auto")
                                //{
                                lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false; lnk2.ToolTip = "Booked";
                                //}
                                //else
                                //{
                                //    lnk2.BackColor = System.Drawing.Color.IndianRed; lnk2.Enabled = false; lnk2.ToolTip = "حجز";
                                //}
                            }
                        }

                    }
                    else
                    {
                       
                        lnk2.ToolTip = "Available";
                       
                    }
                }
            }
           

       

        }
        catch (Exception ex) { }
    

}
}