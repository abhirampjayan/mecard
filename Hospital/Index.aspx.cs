using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Hospital_Index : System.Web.UI.Page
{

    databaseDataContext db = new databaseDataContext();
    string hos_Id = "";

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
        
        if (Session["hakkeemid_h"] != null)
            {
            try
            {
                CheckLocation();
            }
            catch (Exception ex)
            {
                Response.Redirect("../index/hospita login.aspx");
            }
            hos_Id = Session["hakkeemid_h"].ToString();
                HosDoctors();
                TodatApmnt();
            }
            //todayapmnt();
            //TodayDoctors();
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
                    select new {  item1.h_id, item.latitude };
        
            if (query.Count() <= 0)
            {
            Label8.Text = "You must set your location";
            ModalPopupExtender4.Show();
            // Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
        }
        
    }
    //public void TodayDoctors()
    //{
    //    string currntDate = DateTime.Now.ToString("yyyy-MM-dd");
    //    var selctAvldoctor = from a in db.tbl_hos_doc_availables
    //                         where a.date == currntDate
    //                         select a;
    //    if (selctAvldoctor.Count() > 0)
    //    {
    //        GridView1.DataSource = selctAvldoctor;
    //        GridView1.DataBind();
    //        foreach (GridViewRow gRow in GridView1.Rows)
    //        {
    //            Label lblH_Id = (Label)gRow.FindControl("LblDocId");
    //            LinkButton lnkButn = (LinkButton)gRow.FindControl("LnkDoctor");

    //            var selectDoctrdetail = from item in db.tbl_hdoctors
    //                                    where item.hd_email == lblH_Id.Text
    //                                    select item;
    //            foreach (var s in selectDoctrdetail)
    //            {
    //                lnkButn.Text = s.hd_name;
    //                lnkButn.CommandArgument = s.hd_email;
    //            }
    //        }
    //    }
    //    else
    //    {

    //    }

    //}

    public void HosDoctors()
    {
        string currntDate = DateTime.Now.ToString("yyyy-MM-dd");
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("hd_email"), new DataColumn("hd_specialties") }); 
        
        var query = from item in db.tbl_hdoctors
                    where item.h_id == Session["hakkeemid_h"].ToString()
                    select item;
        if(query.Count()>0)
        {
        foreach (var s in query)
        {
            var query1 = from a in db.tbl_hos_doc_availables
                         where a.hd_id == s.hd_email.ToString() orderby a.date ascending
                         select a;

            foreach (var ss in query1)
            {
                dt.Rows.Add(s.hd_name, s.hd_photo,s.hd_email, s.hd_specialties);
            }
            DataView view = new DataView(dt);
            DataTable dvalue = view.ToTable(true, "hd_name", "hd_photo", "hd_email", "hd_specialties");
            DataList1.DataSource = dvalue;
            DataList1.DataBind();
            foreach (DataListItem dlItem in DataList1.Items)
            {
                Label LblDocId = dlItem.FindControl("Label1") as Label;
                DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                var query2 = from a in db.tbl_hos_doc_availables
                             where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text orderby a.date ascending
                             select a;

                dtDates.DataSource = query2.Take(8).ToList();
                dtDates.DataBind();
            }

        }
        }

    }
    public void TodatApmnt()
    {

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("a_date"), new DataColumn("d_id"), new DataColumn("hd_name") });

        var Query = from item in db.tbl_hos_doc_appmnts where item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == (DateTime.Now).ToString("yyyy-MM-dd") select item;
        if (Query.Count() > 0)
        {
            //DataList3.DataSource = Query;
            //DataList3.DataBind();
            foreach(var s in Query)
            {
                var selectQuery= from item in db.tbl_hdoctors where item.hd_email == s.d_id 
                                 select item;
                foreach(var ss in selectQuery)
                {
                   
                        dt.Rows.Add(s.a_date, s.d_id, ss.hd_name);
                     
                }
                DataView view = new DataView(dt);
                DataTable dvalue = view.ToTable(true, "a_date", "d_id", "hd_name");
                DataList3.DataSource = dvalue;
                DataList3.DataBind();
                foreach (DataListItem dl3 in DataList3.Items)
                {
                    Label lbl4 = dl3.FindControl("Label4") as Label;
                    Label lbl5 = dl3.FindControl("Label5") as Label;
                     Label lbl6 = dl3.FindControl("Label6") as Label;
                    var Query2 = from item in db.tbl_hdoctors where item.hd_email == lbl4.Text select item;
                    foreach (var n in Query2) { lbl5.Text = n.hd_name; }

                    DataList dl4 = dl3.FindControl("DataList4") as DataList;
                    var Query1 = from item in db.tbl_hos_doc_appmnts where item.d_id == lbl4.Text && item.h_id == Session["hakkeemid_h"].ToString() && item.a_date == (DateTime.Now).ToString("yyyy-MM-dd") select item;
                    dl4.DataSource = Query1;
                    dl4.DataBind();
                    foreach(DataListItem dl in dl4.Items)
                    {
                        Button Button2 = dl.FindControl("Button2") as Button;
                        Label LblDate = dl.FindControl("LblDate") as Label;
                        Label LblDocName = dl.FindControl("LblDocName") as Label;
                        Label LblDocId = dl.FindControl("LblDocId") as Label;
                        Label LblStatus = dl.FindControl("LblStatus") as Label;

                        if(LblStatus.Text=="0")
                        {

                        }
                        if (LblStatus.Text == "1")
                        {
                            Button2.ToolTip = "Confirmed";
                            Button2.BackColor = System.Drawing.Color.IndianRed;
                        }

                        Button2.CommandArgument = lbl5.Text;
                        LblDate.Text = lbl6.Text;
                        LblDocName.Text = lbl5.Text;
                        LblDocId.Text = lbl4.Text;
                    }
                }
            }
           

        }
        else
        {
        }
    }

   

    protected void BtnSearch_Click(object sender, EventArgs e)
    {

         if (TxtDocName.Text != "" && TxtDate.Text == "")
        {
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("name"), new DataColumn("image"),new DataColumn("hd_id"), new DataColumn("specialty") });

            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_name.Contains(TxtDocName.Text) ||item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text))
                       
                        select item;

            if (query.Count() > 0)
            {
                DataList1.DataSource = query;
                DataList1.DataBind();
                foreach (DataListItem dlItem in DataList1.Items)
                {
                    Label LblDocId = dlItem.FindControl("Label1") as Label;
                    DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                    var query1 = from a in db.tbl_hos_doc_availables
                                 where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text orderby a.date
                                 select a;
                    dtDates.DataSource = query1.Take(8).ToList();
                    dtDates.DataBind();
                }
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }
        if (TxtDocName.Text != "" && TxtDate.Text != "")
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("hd_email"), new DataColumn("hd_specialties") });

            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_name.Contains(TxtDocName.Text)&& item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text)
                        select item;

            if (query.Count() > 0)
            {
                foreach (var s in query)
                {
                    var query1 = from a in db.tbl_hos_doc_availables
                                 where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString() && (a.date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                                 orderby a.date ascending
                                 select a;
                    foreach (var ss in query1)
                    {
                        dt.Rows.Add(s.hd_name, s.hd_photo,s.hd_email, s.hd_specialties);
                    }

                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                    foreach (DataListItem dlItem in DataList1.Items)
                    {
                        Label LblDocId = dlItem.FindControl("Label1") as Label;
                        DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                        var query2 = from a in db.tbl_hos_doc_availables
                                     where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                     orderby a.date ascending
                                     select a;
                        dtDates.DataSource = query2.Take(8).ToList();
                        dtDates.DataBind();
                    }
                }
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }

            if(TxtDocName.Text =="" && TxtDate.Text !="")
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("hd_email"), new DataColumn("hd_specialties") });

                var select = from item in db.tbl_hdoctors
                            where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text))
                            select item;

                if(select.Count() >0)
                {
                    foreach(var s in select)
                    {
                        var query = from a in db.tbl_hos_doc_availables
                                    where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString() && (a.date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                                    orderby a.date ascending
                                    select a;
                        foreach (var ss in query)
                        {
                            dt.Rows.Add(s.hd_name, s.hd_photo,s.hd_email, s.hd_specialties);
                        }

                        DataList1.DataSource = dt;
                        DataList1.DataBind();
                        foreach (DataListItem dlItem in DataList1.Items)
                        {
                            Label LblDocId = dlItem.FindControl("Label1") as Label;
                            DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                            var query2 = from a in db.tbl_hos_doc_availables
                                         where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                         orderby a.date ascending
                                         select a;
                            dtDates.DataSource = query2.Take(8).ToList();
                            dtDates.DataBind();
                        }
                    }
                }
                else
                {
                    ClearDataList();
                    HosDoctors();
                }

            }


            if (TxtDate.Text == "" && TxtDocName.Text == "")
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("date"), new DataColumn("hd_email"), new DataColumn("hd_specialties") });

                var select = from item in db.tbl_hdoctors
                             where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text)
                             select item;

                if (select.Count() > 0)
                {
                    //foreach (var s in select)
                    //{
                    //var query = from a in db.tbl_hos_doc_availables
                    //            where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString()
                    //            select a;
                    //foreach (var ss in query)
                    //{
                    //    dt.Rows.Add(s.hd_name, s.hd_photo, ss.date, s.hd_email, s.hd_specialties);
                    //}

                    DataList1.DataSource = select;
                    DataList1.DataBind();
                    foreach (DataListItem dlItem in DataList1.Items)
                    {
                        Label LblDocId = dlItem.FindControl("Label1") as Label;
                        DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                        var query2 = from a in db.tbl_hos_doc_availables
                                     where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                     orderby a.date ascending
                                     select a;
                        dtDates.DataSource = query2.Take(8).ToList();
                        dtDates.DataBind();
                    }

                    //}
                }
                else
                {
                    ClearDataList();
                    HosDoctors();
                }

            }
    }

    protected void TxtDocName_TextChanged(object sender, EventArgs e)
    {
        if (TxtDocName.Text != "" && TxtDate.Text == "")
        {
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[5] { new DataColumn("name"), new DataColumn("image"), new DataColumn("date"), new DataColumn("hd_id"), new DataColumn("specialty") });

            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_name.Contains(TxtDocName.Text)) && ( item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text))
                        select item;

            if (query.Count() > 0)
            {
                DataList1.DataSource = query;
                DataList1.DataBind();
                foreach (DataListItem dlItem in DataList1.Items)
                {
                    Label LblDocId = dlItem.FindControl("Label1") as Label;
                    DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                    var query1 = from a in db.tbl_hos_doc_availables
                                 where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                 orderby a.date ascending
                                 select a;
                    dtDates.DataSource = query1.Take(8).ToList();
                    dtDates.DataBind();
                }
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }

        if (TxtDocName.Text != "" && TxtDate.Text != "")
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("hd_email"), new DataColumn("hd_specialties") });

            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_name.Contains(TxtDocName.Text) && item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text)
                        select item;

            if (query.Count() > 0)
            {
                foreach (var s in query)
                {
                    var query1 = from a in db.tbl_hos_doc_availables
                                 where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString() && (a.date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                                 orderby a.date ascending
                                 select a;
                    foreach (var ss in query1)
                    {
                        dt.Rows.Add(s.hd_name, s.hd_photo, s.hd_email, s.hd_specialties);
                    }

                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                    foreach (DataListItem dlItem in DataList1.Items)
                    {
                        Label LblDocId = dlItem.FindControl("Label1") as Label;
                        DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                        var query2 = from a in db.tbl_hos_doc_availables
                                     where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                     orderby a.date ascending
                                     select a;
                        dtDates.DataSource = query2.Take(8).ToList();
                        dtDates.DataBind();
                    }
                }
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }
    }

    private void ClearDataList()
    {
        DataList1.DataSource = null;
        DataList1.DataBind();
    }

    protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TxtDocName.Text != "" && TxtDate.Text == "")
        {
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("name"), new DataColumn("image"),new DataColumn("hd_id"), new DataColumn("specialty") });

            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_name.Contains(TxtDocName.Text) ||item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text))
                        select item;

            if (query.Count() > 0)
            {
                DataList1.DataSource = query;
                DataList1.DataBind();
                foreach (DataListItem dlItem in DataList1.Items)
                {
                    Label LblDocId = dlItem.FindControl("Label1") as Label;
                    DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                    var query1 = from a in db.tbl_hos_doc_availables
                                 where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                 orderby a.date ascending
                                 select a;
                    dtDates.DataSource = query1.Take(8).ToList();
                    dtDates.DataBind();
                }
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }
        if (TxtDocName.Text != "" && TxtDate.Text != "")
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("hd_email"), new DataColumn("hd_specialties") });

            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_name.Contains(TxtDocName.Text)&& item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text)
                        select item;

            if (query.Count() > 0)
            {
                foreach (var s in query)
                {
                    var query1 = from a in db.tbl_hos_doc_availables
                                 where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString() && (a.date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                                 select a;
                    foreach (var ss in query1)
                    {
                        dt.Rows.Add(s.hd_name, s.hd_photo,s.hd_email, s.hd_specialties);
                    }

                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                    foreach (DataListItem dlItem in DataList1.Items)
                    {
                        Label LblDocId = dlItem.FindControl("Label1") as Label;
                        DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                        var query2 = from a in db.tbl_hos_doc_availables
                                     where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                     orderby a.date ascending
                                     select a;
                        dtDates.DataSource = query2.Take(8).ToList();
                        dtDates.DataBind();
                    }
                }
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }

            if(TxtDocName.Text =="" && TxtDate.Text !="")
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("hd_email"), new DataColumn("hd_specialties") });

                var select = from item in db.tbl_hdoctors
                            where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text))
                            select item;

                if(select.Count() >0)
                {
                    foreach(var s in select)
                    {
                        var query = from a in db.tbl_hos_doc_availables
                                    where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString() && (a.date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                                    orderby a.date ascending
                                    select a;
                        foreach (var ss in query)
                        {
                            dt.Rows.Add(s.hd_name, s.hd_photo,s.hd_email, s.hd_specialties);
                        }

                        DataList1.DataSource = dt;
                        DataList1.DataBind();
                        foreach (DataListItem dlItem in DataList1.Items)
                        {
                            Label LblDocId = dlItem.FindControl("Label1") as Label;
                            DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                            var query2 = from a in db.tbl_hos_doc_availables
                                         where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                         orderby a.date ascending
                                         select a;
                            dtDates.DataSource = query2.Take(8).ToList();
                            dtDates.DataBind();
                        }
                    }
                }
                else
                {
                    ClearDataList();
                    HosDoctors();
                }

            }


        if(TxtDate.Text=="" && TxtDocName.Text=="")
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("date"), new DataColumn("hd_email"), new DataColumn("hd_specialties") });

            var select = from item in db.tbl_hdoctors
                         where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text)
                         select item;

            if (select.Count() > 0)
            {
                //foreach (var s in select)
                //{
                    //var query = from a in db.tbl_hos_doc_availables
                    //            where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString()
                    //            select a;
                    //foreach (var ss in query)
                    //{
                    //    dt.Rows.Add(s.hd_name, s.hd_photo, ss.date, s.hd_email, s.hd_specialties);
                    //}

                    DataList1.DataSource = select;
                    DataList1.DataBind();
                    foreach (DataListItem dlItem in DataList1.Items)
                    {
                        Label LblDocId = dlItem.FindControl("Label1") as Label;
                        DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                        var query2 = from a in db.tbl_hos_doc_availables
                                     where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                     orderby a.date ascending
                                     select a;
                        dtDates.DataSource = query2.Take(8).ToList();
                        dtDates.DataBind();
                    }

                //}
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }


        
    }

    protected void TxtDate_TextChanged(object sender, EventArgs e)
    {
        if (TxtDocName.Text == "" && TxtDate.Text != "")
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("hd_name"), new DataColumn("hd_photo"),new DataColumn("hd_email"), new DataColumn("hd_specialties") });

            var select = from item in db.tbl_hdoctors
                         where item.h_id == Session["hakkeemid_h"].ToString() && (item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text))
                         select item;

            if (select.Count() > 0)
            {
                foreach (var s in select)
                {
                    var query = from a in db.tbl_hos_doc_availables
                                where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString() && (a.date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                                orderby a.date ascending
                                select a;
                    foreach (var ss in query)
                    {
                        dt.Rows.Add(s.hd_name, s.hd_photo,s.hd_email, s.hd_specialties);
                    }

                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                    foreach (DataListItem dlItem in DataList1.Items)
                    {
                        Label LblDocId = dlItem.FindControl("Label1") as Label;
                        DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                        var query2 = from a in db.tbl_hos_doc_availables
                                     where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                     orderby a.date ascending
                                     select a;
                        dtDates.DataSource = query2.Take(8).ToList();
                        dtDates.DataBind();
                    }

                }
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }

        if (TxtDocName.Text != "" && TxtDate.Text != "")
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("hd_name"), new DataColumn("hd_photo"), new DataColumn("hd_email"), new DataColumn("hd_specialties") });

            var query = from item in db.tbl_hdoctors
                        where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_name.Contains(TxtDocName.Text) && item.hd_specialties.Contains(ddlSpeciality.SelectedItem.Text)
                        select item;

            if (query.Count() > 0)
            {
                foreach (var s in query)
                {
                    var query1 = from a in db.tbl_hos_doc_availables
                                 where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == s.hd_email.ToString() && (a.date.Contains(DateTime.Parse(TxtDate.Text).ToString("yyyy-MM-dd")))
                                 orderby a.date ascending
                                 select a;
                    foreach (var ss in query1)
                    {
                        dt.Rows.Add(s.hd_name, s.hd_photo, s.hd_email, s.hd_specialties);
                    }

                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                    foreach (DataListItem dlItem in DataList1.Items)
                    {
                        Label LblDocId = dlItem.FindControl("Label1") as Label;
                        DataList dtDates = dlItem.FindControl("DataList2") as DataList;

                        var query2 = from a in db.tbl_hos_doc_availables
                                     where a.h_id == Session["hakkeemid_h"].ToString() && a.hd_id == LblDocId.Text
                                     orderby a.date ascending
                                     select a;
                        dtDates.DataSource = query2.Take(8).ToList();
                        dtDates.DataBind();
                    }
                }
            }
            else
            {
                ClearDataList();
                HosDoctors();
            }
        }




    }




    protected void DataList2_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName=="apointment")
        {
            LinkButton lnkDate=e.Item.FindControl("LnkDate") as LinkButton;
        
        if (Session["doctor"] != null)
        {
            Session["doctor"] = null;
            Session["DoctorId"] = e.CommandArgument.ToString();
            Session["AvailableDate"] = lnkDate.Text.ToString();
            Response.Redirect("~/Hospital/Doctor available date and time.aspx");
        }
        else
        {
            Session["DoctorId"] = e.CommandArgument.ToString();
            Session["AvailableDate"] = lnkDate.Text.ToString();
            Response.Redirect("~/Hospital/Doctor available date and time.aspx");
        }
       
        }
    }

    protected void DataList4_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "AppointmentDetails")
        {
            Button Button2=e.Item.FindControl("Button2") as Button;
            Label LblDate = e.Item.FindControl("LblDate") as Label;
            Label LblDocName = e.Item.FindControl("LblDocName") as Label;
            Label LblDocId = e.Item.FindControl("LblDocId") as Label;
            Label LblStatus = e.Item.FindControl("LblStatus") as Label;

            Session["A_Time"] = Button2.Text;
            Session["A_Date"] = LblDate.Text;
            Session["Doc_Name"] = LblDocName.Text;
            Session["Doc_Id"] = LblDocId.Text;
            Session["A_Status"] = LblStatus.Text;
            Response.Redirect("~/Hospital/ApointmentDetails.aspx");
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName=="view")
        {
            Session["Hos_Doc_Id"] = e.CommandArgument.ToString();
            Response.Redirect("~/Hospital/Change doctor details.aspx");
        }
        if (e.CommandName == "check")
        {
            Session["doctor"] = e.CommandArgument.ToString();
            Response.Redirect("~/Hospital/Doctor available date and time.aspx");
        }
        if (e.CommandName == "Appointments")
        {
            SelectDoctorAppointments(e);

        }
    }

    private void SelectDoctorAppointments(DataListCommandEventArgs e)
    {
         var select = from a in db.tbl_hdoctors
                             where a.hd_email == e.CommandArgument.ToString() && a.h_id == hos_Id
                             select a;
        
                foreach(var s in select)
                {
                    LblApDocName.Text ="Dr."+" "+ s.hd_name;
                }
        

        var Query = from item in db.tbl_hos_doc_appmnts
                    join item1 in db.tbl_signups
                        on item.u_id equals item1.email
                    where item.d_id == e.CommandArgument.ToString() && item.h_id == hos_Id && item.a_status == 1
                    orderby item.a_date, item.a_time
                    select new { item.a_date, item.a_time, item1.name,item.d_id };

        //var qry = from item in db.tbl_hos_doc_appmnts
        //          where item.d_id == e.CommandArgument.ToString() && item.h_id == hos_Id && item.a_status == 1
        //          select item;
         
               
           

        if (Query.Count() > 0)
        {

            GrvApointments.DataSource = Query;
            GrvApointments.DataBind();
           
            //foreach(GridViewRow grow in GrvApointments.Rows)
            //{
            //    Label LblU_Id = grow.FindControl("LblU_Id") as Label;
            //    Label LblPatntTime = grow.FindControl("LblPatntTime") as Label;

            //    var select = from a in db.tbl_signups
            //                 where a.email == LblU_Id.Text
            //                 select a;
            //    foreach( var ss in select)
            //    {
            //        LblPatntTime.Text = ss.name.ToString();
            //    }

            //}

            Timer timer = this.Master.FindControl("Timer1") as Timer;
            timer.Enabled = false;

            this.ModalPopupExtender1.Show();
        }
        else
        {
            RegisterStartupScript("", "<Script Language=JavaScript>alert('There is no appointments')</Script>");
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
    }
}