using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hospital_Add_doctor_availability : System.Web.UI.Page
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
        //    this.MasterPageFile = "~/hospital/ArabichospitalMaster.master";
        //}
    }


    private void DisableControls(System.Web.UI.Control control)
    {
        foreach (System.Web.UI.Control c in control.Controls)
        {
            // Get the Enabled property by reflection.
            Type type = c.GetType();
            PropertyInfo prop = type.GetProperty("Enabled");

            // Set it to False to disable the control.
            if (prop != null)
            {
                prop.SetValue(c, false, null);
            }

            // Recurse into child controls.
            if (c.Controls.Count > 0)
            {
                this.DisableControls(c);
            }
        }
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
                doctor();
                DropDownList1.Items.Insert(0, "----Select doctor----");

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
            //Label8.Text = "You must set your location";
            //ModalPopupExtender4.Show();
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You must set your location.')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يجب تعيين موقعك.')</Script>");
            //}
            //
        }

    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime dt2 = DateTime.Parse(TextBox4.Text);

            if (dt2 >= dt1)
            {
                var Query = from item in db.tbl_hos_doc_availables where item.hd_id == DropDownList1.SelectedValue.ToString() && item.date == TextBox4.Text select item;
                if (Query.Count() > 0)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Already set the availability times in this date so please select another date.')</Script>");
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('بالفعل تعيين أوقات توافر في هذا التاريخ لذا يرجى تحديد تاريخ آخر')</Script>");

                    //}
                    //Label1.Text = "Already set the availability times in this date so please select another date.";
                    //this.ModalPopupExtender1.Show();
                }
                else
                {
                    Button2.Enabled = true;
                }
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Selected date most be greater than the current date.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('التاريخ المحدد يكون أكبر من التاريخ الحالي')</Script>");

                //}
                //Label7.Text = "Selected date most be the current date or above.";
                //this.ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex)
        {

        }

    }

    public static List<string> date = new List<string>();

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (TextBox4.Text != "")
        {

            var checkDate = from item in db.tbl_hos_doc_availables
                            where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == DropDownList1.SelectedValue.ToString() && item.date == DateTime.Parse(TextBox4.Text).ToString("yyyy-MM-dd")
                            select item;
            if (checkDate.Count() > 0)
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Already set appointments for the doctor in this date. Please check and edit')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تعيين المواعيد بالفعل للطبيب في هذا التاريخ. يرجى التحقق والتعديل')</Script>");

                //}
                //Label1.Text = "Already set appointments for the doctor in this date. Please check and edit";
                //this.ModalPopupExtender1.Show();
            }
            else
            {
                DateTime d1 = DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime d2 = DateTime.Parse(TextBox4.Text);
                if (d1 == d2)
                {
                    string ts = DateTime.Parse(TextBox5.Text).ToShortTimeString();
                    string te = DateTime.Parse(TextBox6.Text).ToShortTimeString();
                    string tn = DateTime.Now.ToShortTimeString();
                    if ((DateTime.Parse(tn) > DateTime.Parse(ts)) || (DateTime.Parse(tn) > DateTime.Parse(te)))
                    {
                        //Label1.Text = "<b>Alert!</b>   <p>You selected time is less than the current time.</p>";
                        //Label1.ForeColor = System.Drawing.Color.Red;
                        //this.ModalPopupExtender1.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('You selected time is less than the current time.')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد اخترت الوقت أقل من الوقت الحالي')</Script>");

                        //}
                    }
                    else
                    {
                        List<string> dt = new List<string>();

                        DateTime t1 = DateTime.Parse(TextBox5.Text);

                        DateTime t2 = DateTime.Parse(TextBox6.Text);

                        string start_time = t1.ToShortTimeString();
                        string end_time = t2.ToShortTimeString();
                        int d = int.Parse(DropDownList2.SelectedItem.Text);
                        d = d + int.Parse(DropDownList3.SelectedItem.Text);

                        while (DateTime.Parse(start_time) < DateTime.Parse(end_time))
                        {
                            //dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));

                            //start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                            if (DateTime.Parse(start_time).AddMinutes(d) <= DateTime.Parse(end_time))
                            {
                                dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));
                                start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                            }
                            else
                            {
                                break;
                                //message
                            }

                        }
                        if (dt.Count() > 0)
                        {
                            CheckBoxList1.DataSource = dt;
                            CheckBoxList1.DataBind();
                            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                            {
                                CheckBoxList1.Items[i].Selected = true;
                            }
                            CheckBoxList1.Visible = true;
                            CheckBoxList1.Enabled = true;
                            Button3.Enabled = true;
                            BtnResetDate.Enabled = true;
                            Button2.Enabled = false;
                        }
                        else
                        {
                            Button2.Enabled = true;
                            Button3.Enabled = false;
                            //Label1.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                            //Label1.ForeColor = System.Drawing.Color.Red;
                            //this.ModalPopupExtender1.Show();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                            //}
                            //else
                            //{
                            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد')</Script>");

                            //}
                        }
                    }
                }
                else
                {
                    List<string> dt = new List<string>();

                    DateTime t1 = DateTime.Parse(TextBox5.Text);

                    DateTime t2 = DateTime.Parse(TextBox6.Text);

                    string start_time = t1.ToShortTimeString();
                    string end_time = t2.ToShortTimeString();
                    int d = int.Parse(DropDownList2.SelectedItem.Text);
                    d = d + int.Parse(DropDownList3.SelectedItem.Text);

                    while (DateTime.Parse(start_time) < DateTime.Parse(end_time))
                    {
                        //dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));

                        //start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                        if (DateTime.Parse(start_time).AddMinutes(d) <= DateTime.Parse(end_time))
                        {
                            dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));
                            start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                        }
                        else
                        {
                            break;
                            //message
                        }

                    }
                    if (dt.Count() > 0)
                    {
                        CheckBoxList1.DataSource = dt;
                        CheckBoxList1.DataBind();
                        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                        {
                            CheckBoxList1.Items[i].Selected = true;
                        }
                        CheckBoxList1.Visible = true;
                        CheckBoxList1.Enabled = true;
                        Button3.Enabled = true;
                        BtnResetDate.Enabled = true;
                        Button2.Enabled = false;
                    }
                    else
                    {
                        Button2.Enabled = true;
                        Button3.Enabled = false;
                        //Label1.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                        //Label1.ForeColor = System.Drawing.Color.Red;
                        //this.ModalPopupExtender1.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");

                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد')</Script>");
                        //}
                    }
                }
            }
            //}
        }

        if (TxtToDate.Text != "")
        {
            DateTime d1 = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime d2 = DateTime.Parse(TxtFromDate.Text);
            DateTime StartDate = DateTime.Parse(TxtFromDate.Text);
            DateTime EndDate = DateTime.Parse(TxtToDate.Text);
            double total_days = (EndDate - StartDate).TotalDays;
            if (60 > total_days)
            {
                if (d1 == d2)
                {
                    string ts = DateTime.Parse(TextBox5.Text).ToShortTimeString();
                    string te = DateTime.Parse(TextBox6.Text).ToShortTimeString();
                    string tn = DateTime.Now.ToShortTimeString();
                    if ((DateTime.Parse(tn) > DateTime.Parse(ts)) || (DateTime.Parse(tn) > DateTime.Parse(te)))
                    {
                        //Label1.Text = "<b>Alert!</b>   <p>You selected time is less than the current time.</p>";
                        //Label1.ForeColor = System.Drawing.Color.Red;
                        //this.ModalPopupExtender1.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('You selected time is less than the current time.')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد اخترت الوقت أقل من الوقت الحالي')</Script>");

                        //}
                    }
                    else
                    {

                        List<string> dt = new List<string>();
                        DateTime dtNow = DateTime.Parse(DateTime.Now.ToShortDateString());


                        DateTime t1 = DateTime.Parse(TextBox5.Text);

                        DateTime t2 = DateTime.Parse(TextBox6.Text);

                        string start_time = t1.ToShortTimeString();
                        string end_time = t2.ToShortTimeString();
                        int d = int.Parse(DropDownList2.SelectedItem.Text);
                        d = d + int.Parse(DropDownList3.SelectedItem.Text);
                        while (DateTime.Parse(start_time) <= DateTime.Parse(end_time))
                        {
                            //dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));

                            //start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                            if (DateTime.Parse(start_time).AddMinutes(d) <= DateTime.Parse(end_time))
                            {
                                dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));
                                start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                            }
                            else
                            {
                                break;
                                //message
                            }
                        }
                        if (dt.Count() > 0)
                        {
                            CheckBoxList1.DataSource = dt;
                            CheckBoxList1.DataBind();
                            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                            {
                                CheckBoxList1.Items[i].Selected = true;
                            }
                            Button3.Enabled = true;
                            BtnResetDate.Enabled = true;
                            CheckBoxList1.Enabled = true;
                            CheckBoxList1.Visible = true;
                            Button2.Enabled = false;
                        }
                        else
                        {
                            Button2.Enabled = true;
                            Button3.Enabled = false;
                            //Label1.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                            //Label1.ForeColor = System.Drawing.Color.Red;
                            //this.ModalPopupExtender1.Show();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                                RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                            //}
                            //else
                            //{
                            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد')</Script>");

                            //}
                        }
                        if (RdbDateSelect.SelectedIndex == 1)
                        {
                            Button3.Enabled = true;

                            List<string> dt1 = new List<string>();
                            dt1.Clear();
                            var res = new List<DateTime>();
                            var start = DateTime.Parse(TxtFromDate.Text);
                            var end = DateTime.Parse(TxtToDate.Text);
                            for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
                                dt1.Add(ndate.ToShortDateString());
                            date.Clear();
                            foreach (string ss in dt1)
                            {
                                string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
                                var Query = from item in db.tbl_hos_doc_availables where item.hd_id == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == s select item;
                                if (Query.Count() > 0)
                                {

                                }
                                else
                                {
                                    date.Add(s);
                                }
                            }
                            DropDownList4.Enabled = true;
                        }

                        CheckBoxList2.DataSource = date;
                        CheckBoxList2.DataBind();
                        Session["stime"] = TxtFromDate.Text;
                        Session["etime"] = TxtToDate.Text;
                        TxtFromDate.Text = "";
                        TxtToDate.Text = "";
                        for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                        {
                            CheckBoxList2.Items[i].Selected = true;
                        }
                    }
                }
                else
                {
                    List<string> dt = new List<string>();
                    DateTime dtNow = DateTime.Parse(DateTime.Now.ToShortDateString());


                    DateTime t1 = DateTime.Parse(TextBox5.Text);

                    DateTime t2 = DateTime.Parse(TextBox6.Text);

                    string start_time = t1.ToShortTimeString();
                    string end_time = t2.ToShortTimeString();
                    int d = int.Parse(DropDownList2.SelectedItem.Text);
                    d = d + int.Parse(DropDownList3.SelectedItem.Text);
                    while (DateTime.Parse(start_time) <= DateTime.Parse(end_time))
                    {
                        //dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));

                        //start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                        if (DateTime.Parse(start_time).AddMinutes(d) <= DateTime.Parse(end_time))
                        {
                            dt.Add(DateTime.Parse(start_time).ToString("hh:mm tt"));
                            start_time = DateTime.Parse(start_time).AddMinutes(d).ToShortTimeString();
                        }
                        else
                        {
                            break;
                            //message
                        }
                    }
                    if (dt.Count() > 0)
                    {
                        CheckBoxList1.DataSource = dt;
                        CheckBoxList1.DataBind();
                        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                        {
                            CheckBoxList1.Items[i].Selected = true;
                        }
                        Button3.Enabled = true;
                        BtnResetDate.Enabled = true;
                        CheckBoxList1.Enabled = true;
                        CheckBoxList1.Visible = true;
                        Button2.Enabled = false;
                    }
                    else
                    {
                        Button2.Enabled = true;
                        Button3.Enabled = false;
                        //Label1.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                        //Label1.ForeColor = System.Drawing.Color.Red;
                        //this.ModalPopupExtender1.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد')</Script>");

                        //}
                    }
                    if (RdbDateSelect.SelectedIndex == 1)
                    {
                        Button3.Enabled = true;

                        List<string> dt1 = new List<string>();
                        dt1.Clear();
                        var res = new List<DateTime>();
                        var start = DateTime.Parse(TxtFromDate.Text);
                        var end = DateTime.Parse(TxtToDate.Text);
                        for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
                            dt1.Add(ndate.ToShortDateString());
                        date.Clear();
                        foreach (string ss in dt1)
                        {
                            string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
                            var Query = from item in db.tbl_hos_doc_availables where item.hd_id == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == s select item;
                            if (Query.Count() > 0)
                            {

                            }
                            else
                            {
                                date.Add(s);
                            }
                        }
                        DropDownList4.Enabled = true;
                    }

                    CheckBoxList2.DataSource = date;
                    CheckBoxList2.DataBind();
                    Session["stime"] = TxtFromDate.Text;
                    Session["etime"] = TxtToDate.Text;
                    TxtFromDate.Text = "";
                    TxtToDate.Text = "";
                    for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                    {
                        CheckBoxList2.Items[i].Selected = true;
                    }
                }
            }
            else
            {
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You can set your availabilities only next 60 days.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يمكنك تعيين توافر الخاص بك فقط 60 يوما القادمة.')</Script>");

                //}
            }

        }
        if (RdbDateSelect.SelectedIndex == 0)
        {

            date.Clear();
            Button3.Enabled = true;
            var start = DateTime.Parse(TextBox4.Text);
            string s = start.ToString("yyyy-MM-dd");
            date.Add(s);
            CheckBoxList2.DataSource = date;
            CheckBoxList2.DataBind();
            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                CheckBoxList2.Items[i].Selected = true;
            }
        }

    }

    public void doctor()
    {

        var Query = from item in db.tbl_hdoctors where item.h_id == Session["hakkeemid_h"].ToString() select item;
        DropDownList1.DataSource = Query;
        DropDownList1.DataValueField = "hd_email";
        DropDownList1.DataTextField = "hd_name";
        DropDownList1.DataBind();

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex > 0)
        {

            var doctoractive = from item in db.tbl_blk_hos_doctors where item.doctor_id == DropDownList1.SelectedItem.Value && item.hos_hakkeem_id == Session["hakkeemid_h"].ToString() select item;
            if (doctoractive.Count() > 0)
            {
                DisableControls(Form);
                //if (Session["Language"].ToString() == "Auto")
                //{
                    Label8.Text = "Dear, You selected doctor is blocked by Administrator. Please contact hakkeem customer care";
                //}
                //else
                //{
                //    Label8.Text = "اخترت الطبيب تم حظره من قبل المسؤول. يرجى الاتصال بخدمة العملاء حكيم";
                //}
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            else
            {
                var Query = from item in db.tbl_hdoctors where item.hd_email == DropDownList1.SelectedItem.Value select item;





                foreach (var ss in Query)
                {
                    DateTime currentDate = DateTime.Now;
                    DateTime compareDate = Convert.ToDateTime(ss.hd_id_expire);
                    if (currentDate >= compareDate)
                    {
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Doctor identification number is expired...!')</Script>");
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('انتهت صلاحية رقم تعريف الطبيب')</Script>");

                        //}
                    }
                    else
                    {
                        DetailsView1.DataSource = Query;
                        DetailsView1.DataBind();
                        RdbDateSelect.Enabled = true;
                        CheckBoxList1.Visible = false;
                        Button3.Enabled = false;
                    }
                }
            }
        }

    }

    protected void RdbDateSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdbDateSelect.SelectedIndex == 0)
        {
            TextBox4.Enabled = true;
            TxtFromDate.Enabled = false;
            TxtToDate.Enabled = false;
            TxtFromDate.Text = "";
            TxtToDate.Text = "";
            Button3.Enabled = false;
            CheckBoxList1.Enabled = false;
            DropDownList4.Enabled = false;
            TextBox4.Visible = true;
            TxtFromDate.Visible = false;
            TxtToDate.Visible = false;
            Label5.Visible = false;
            Label6.Visible = false;
        }
        if (RdbDateSelect.SelectedIndex == 1)
        {
            Label5.Visible = true;
            Label6.Visible = true;
            TextBox4.Enabled = false;
            //TextBox8.Enabled = true;
            TxtFromDate.Enabled = true;
            TxtToDate.Enabled = false;
            TextBox4.Text = "";
            Button3.Enabled = false;
            CheckBoxList1.Enabled = false;
            DropDownList4.Enabled = true;
            TextBox4.Visible = false;
            TxtFromDate.Visible = true;
            TxtToDate.Visible = true;
        }
    }

    //protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
    //    DateTime dt2 = DateTime.Parse(TxtFromDate.Text);

    //    if (dt2 > dt1)
    //    {
    //        TxtToDate.Enabled = true;
    //    }
    //    else
    //    {
    //        RegisterStartupScript("", "<Script Language=JavaScript>alert('Selected date most be greater than the current date.')</Script>");

    //    }
    //}

    ////protected void TxtToDate_TextChanged(object sender, EventArgs e)
    ////{
    ////    DateTime dt1 = DateTime.Parse(TxtFromDate.Text);
    ////    DateTime dt2 = DateTime.Parse(TxtToDate.Text);

    ////    if (dt2 > dt1)
    ////    {
    ////        Button2.Enabled = true;
    ////    }
    ////    else
    ////    {
    ////        //TxtToDate.Enabled = false;
    ////        Button2.Enabled = false;
    ////        RegisterStartupScript("", "<Script Language=JavaScript>alert('Selected date most be greater than the starting date.')</Script>");

    ////    }
    ////}

    protected void Button3_Click(object sender, EventArgs e)
    {
        //if (CheckBoxList1.SelectedIndex != -1)
        if (CheckBoxList1.SelectedIndex!=-1 &&  CheckBoxList2.SelectedIndex != -1)
        {
            var Query = from item in db.tbl_hos_doc_availables where item.hd_id == DropDownList1.SelectedValue.ToString() && item.date == TextBox4.Text select item;
            if (Query.Count() > 0)
            {
                //RegisterStartupScript("", "<Script Language=JavaScript>alert('You already set the availability times in this date so please select another date.')</Script>");

                Button3.Enabled = false;
                BtnResetDate.Enabled = false;
                //Label1.Text = "You already set the availability times in this date so please select another date.";
                //this.ModalPopupExtender1.Show();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('You already set the availability times in this date so please select another date.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد قمت بالفعل بتعيين أوقات التوفر في هذا التاريخ لذا يرجى تحديد تاريخ آخر')</Script>");
                //}
            }
            else
            {
                //string time = "";
                //int i = 0;
                //while (i < CheckBoxList1.Items.Count)
                //{
                //    if (time == "")
                //    {
                //        if (CheckBoxList1.Items[i].Selected)
                //        {
                //            time = CheckBoxList1.Items[i].Text;
                //        }
                //    }
                //    else
                //    {
                //        if (CheckBoxList1.Items[i].Selected)
                //        {
                //            time += "," + CheckBoxList1.Items[i].Text;
                //        }
                //    }
                //    i++;
                //}
                if (TextBox4.Text != "")
                {
                    tbl_hos_doc_available da = new tbl_hos_doc_available()
                    {
                        hd_id = DropDownList1.SelectedValue.ToString(),
                        h_id = Session["hakkeemid_h"].ToString(),
                        date = DateTime.Parse(TextBox4.Text).ToString("yyyy-MM-dd"),
                        //hd_a_time=time,
                        status = 0,
                    };
                    db.tbl_hos_doc_availables.InsertOnSubmit(da);
                    db.SubmitChanges();
                    var QueryDate = from item in db.tbl_hos_doc_availables where item.hd_id == DropDownList1.SelectedItem.Value && item.h_id == Session["hakkeemid_h"].ToString() select item.id;
                    int max = Convert.ToInt32(QueryDate.Max());
                    int i = 0;
                    while (i < CheckBoxList1.Items.Count)
                    {

                        if (CheckBoxList1.Items[i].Selected)
                        {

                            string newtime = CheckBoxList1.Items[i].Text;
                            int l = CheckBoxList1.Items[i].Text.Count();
                            if (newtime[0].ToString() == "0")
                            {
                                newtime = newtime.Substring(1, l - 1);
                            }
                            newtime = newtime.Replace(" ", "");



                            tbl_hos_doc_time dt = new tbl_hos_doc_time()
                            {
                                date_id = max,
                                time = newtime,
                            };
                            db.tbl_hos_doc_times.InsertOnSubmit(dt);
                            db.SubmitChanges();
                        }

                        i++;
                    }
                }
                else
                {
                    //foreach (string ss in date)
                    for (int c = 0; c < CheckBoxList2.Items.Count; c++)
                    {
                        if (CheckBoxList2.Items[c].Selected)
                        {
                            tbl_hos_doc_available da = new tbl_hos_doc_available()
                            {
                                hd_id = DropDownList1.SelectedValue.ToString(),
                                h_id = Session["hakkeemid_h"].ToString(),
                                date = CheckBoxList2.Items[c].Text,
                                //hd_a_time = time,
                                status = 0,
                            };
                            db.tbl_hos_doc_availables.InsertOnSubmit(da);
                            db.SubmitChanges();
                            var QueryDate = from item in db.tbl_hos_doc_availables where item.hd_id == DropDownList1.SelectedItem.Value && item.h_id == Session["hakkeemid_h"].ToString() select item.id;
                            int max = Convert.ToInt32(QueryDate.Max());
                            int i = 0;
                            while (i < CheckBoxList1.Items.Count)
                            {

                                if (CheckBoxList1.Items[i].Selected)
                                {
                                    string newtime = CheckBoxList1.Items[i].Text;
                                    int l = CheckBoxList1.Items[i].Text.Count();
                                    if (newtime[0].ToString() == "0")
                                    {
                                        newtime = newtime.Substring(1, l - 1);
                                    }
                                    newtime = newtime.Replace(" ", "");
                                    tbl_hos_doc_time dt = new tbl_hos_doc_time()
                                    {
                                        date_id = max,
                                        time = newtime.ToString(),
                                    };
                                    db.tbl_hos_doc_times.InsertOnSubmit(dt);
                                    db.SubmitChanges();
                                }

                                i++;
                            }
                        }
                    }
                }
                Button3.Enabled = false;
                BtnResetDate.Enabled = false;
                CheckBoxList1.Items.Clear();
                CheckBoxList2.Items.Clear();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully set your availability. Thank you doctor.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تعيين التوفر بنجاح. شكرا لك الطبيب')</Script>");

                //}
                //Label1.Text = "Successfully set your availability. Thank you doctor.";
                //this.ModalPopupExtender1.Show();
            }

        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select at least one from the checkboxlist.')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد واحد على الأقل من قائمة الاختيار')</Script>");

            //}
            //Label1.Text = "Please select at least one from the checkboxlist.";
            //this.ModalPopupExtender1.Show();
        }

    }

    protected void BtnResetDate_Click(object sender, EventArgs e)
    {
        BtnResetDate.Enabled = Button3.Enabled = false;
        Button2.Enabled = true;
        CheckBoxList1.Items.Clear();
    }


    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList4.SelectedIndex == 0)
            {
                List<string> dt1 = new List<string>();
                var res = new List<DateTime>();
                var start = DateTime.Parse(Session["stime"].ToString());
                var end = DateTime.Parse(Session["etime"].ToString());
                for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
                    dt1.Add(ndate.ToShortDateString());
                date.Clear();
                foreach (string ss in dt1)
                {

                    string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
                    var Query = from item in db.tbl_hos_doc_availables where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == DropDownList1.SelectedItem.Value && item.date == s select item;
                    if (Query.Count() > 0)
                    {

                    }
                    else
                    {
                        date.Add(s);
                    }
                }
                CheckBoxList2.DataSource = date;
                CheckBoxList2.DataBind();
                for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                {
                    CheckBoxList2.Items[i].Selected = true;
                }
            }

            //Saturday and Sunday only-------------------------------------------------
            if (DropDownList4.SelectedIndex == 1)
            {
                date.Clear();
                DateTime startDate = DateTime.Parse(Session["stime"].ToString());
                DateTime endDate = DateTime.Parse(Session["etime"].ToString());

                TimeSpan diff = endDate - startDate;
                int days = diff.Days;
                for (var i = 0; i <= days; i++)
                {
                    var testDate = startDate.AddDays(i);
                    switch (testDate.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                        case DayOfWeek.Sunday:
                            //Console.WriteLine(testDate.ToShortDateString());
                            date.Add(testDate.ToString("yyyy-MM-dd"));
                            break;
                    }
                }
                CheckBoxList2.DataSource = date;
                CheckBoxList2.DataBind();
                for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                {
                    CheckBoxList2.Items[i].Selected = true;
                }
            }
            //##*****************Saturday and Sunday only*********************##//

            if (DropDownList4.SelectedIndex == 2)
            {
                List<string> dt1 = new List<string>();
                var res = new List<DateTime>();
                var start = DateTime.Parse(Session["stime"].ToString());
                var end = DateTime.Parse(Session["etime"].ToString());
                for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
                    dt1.Add(ndate.ToShortDateString());
                date.Clear();
                foreach (string ss in dt1)
                {

                    string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
                    var Query = from item in db.tbl_hos_doc_availables where item.h_id == Session["hakkeemid_h"].ToString() && item.hd_id == DropDownList1.SelectedItem.Value && item.date == s select item;
                    if (Query.Count() > 0)
                    {

                    }
                    else
                    {
                        date.Add(s);
                    }
                }
                CheckBoxList2.DataSource = date;
                CheckBoxList2.DataBind();
                for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                {
                    CheckBoxList2.Items[i].Selected = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime dt2 = DateTime.Parse(TxtFromDate.Text);

            if (dt2 >= dt1)
            {
                TxtToDate.Enabled = true;
            }
            else
            {
                TxtToDate.Enabled = false;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Selected date most be greater than the current date or above.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('التاريخ المحدد يكون أكبر من التاريخ الحالي أو أعلى')</Script>");

                //}
                //Label1.Text = "Selected date most be the current date or above";
                //this.ModalPopupExtender1.Show();

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = DateTime.Parse(TxtFromDate.Text);
            DateTime dt2 = DateTime.Parse(TxtToDate.Text);

            if (dt2 > dt1)
            {
                List<string> adate = new List<string>();
                List<string> dtt1 = new List<string>();
                //dt1.Clear();
                var res = new List<DateTime>();
                var start = DateTime.Parse(TxtFromDate.Text);
                var end = DateTime.Parse(TxtToDate.Text);
                for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
                    dtt1.Add(ndate.ToShortDateString());
                date.Clear();
                foreach (string ss in dtt1)
                {
                    string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
                    var Query = from item in db.tbl_hos_doc_availables where item.hd_id == DropDownList1.SelectedValue.ToString() && item.h_id == Session["hakkeemid_h"].ToString() && item.date == s select item;
                    if (Query.Count() > 0)
                    {
                        adate.Add(s);
                    }
                    //else
                    //{
                    //    date.Add(s);
                    //}
                }
                if (adate.Count() > 0)
                {
                    string available = "";
                    for (int i = 0; i < adate.Count(); i++)
                    {
                        if (available == "")
                        {
                            available = "<span class='btn btn-xs btn-default'>" + adate[i].ToString() + "</span>";
                        }
                        else
                        {
                            available += " <span class='btn btn-xs btn-default'>" + adate[i].ToString() + "</span>";
                        }
                    }

                    //Label9.Text = "Appointment already fixed this date </br>" + available;
                    //ModalPopupExtender3.Show();
                    //string msg = "Appointment already fixed this date </br>" + available;
                    //RegisterStartupScript("", "<Script Language=JavaScript>swal({html:true, text:'<b>TEXT</b>'})</Script>");
                }
                Button2.Enabled = true;
            }
            else
            {
                TxtToDate.Enabled = false;
                Button2.Enabled = false;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Selected date most be greater than the starting date.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('التاريخ المحدد يكون أكبر من تاريخ البدء')</Script>");

                //}
                //Label7.Text = "Selected date most be greater than the starting date.";
                //this.ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/Add doctor availability.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Hospital/Add doctor availability.aspx?l=ar-EG");
        //}
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Hospital/SetHospitalLocation.aspx");
        //}
        //else

        //{
        //    Response.Redirect("~/Hospital/SetHospitalLocation.aspx?l=ar-EG");
        //}
    }
}