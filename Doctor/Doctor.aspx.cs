using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Doctor_Doctor : System.Web.UI.Page
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

        var doctoractive = from item in db.tbl_blk_hakkeem_doctors where item.hakkeem_id == Session["hakkeemid_d"].ToString() select item;
        if (doctoractive.Count() > 0)
        {
            DisableControls(Form);
            //if (Session["Language"].ToString() == "Auto")
            //{
                Label6.Text = "Dear doctor you can't use this section. Please contact hakkeem customer care";
            //}
            //else
            //{
            //    Label6.Text = "عزيزي الطبيب لا يمكنك استخدام هذا القسم. يرجى الاتصال بخدمة العملاء حكيم";
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }


        Timer t = (Timer)Master.FindControl("Timer1");
        t.Enabled = false;

        if (!IsPostBack)
        {
            CheckLocation();
            //consultation_fee();
        }
    }




    public void CheckLocation()
    {
        try
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
        catch (Exception ex)
        {

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
                var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == TextBox4.Text select item;
                if (Query.Count() > 0)
                {
                    //if (Session["Language"].ToString() == "Auto")
                    //{
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('You already set the availability times in this date so please select another date.')</Script>");
                        TextBox4.Text = "";
                        TextBox4.Focus();
                    //}
                    //else
                    //{
                    //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد قمت بالفعل بتعيين أوقات التوفر في هذا التاريخ لذا يرجى تحديد تاريخ آخر.')</Script>");
                    //    TextBox4.Text = "";
                    //    TextBox4.Focus();
                    //}
                    //Label6.Text = "You already set the availability times in this date so please select another date.";
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
                    TextBox4.Text = "";
                    TextBox4.Focus();
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('التاريخ المحدد يكون أكبر من التاريخ الحالي.')</Script>");
                //    TextBox4.Text = "";
                //    TextBox4.Focus();
                //}
                //Label7.Text = "Selected date most be the current date or above.";
                //this.ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex)
        {
            //Label7.Text = "Selected date most be the current date or above.";
            //this.ModalPopupExtender2.Show();
        }
    }

    public static List<string> date = new List<string>();

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox4.Text != "")
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
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                        TextBox4.Text = "";
                        TextBox4.Focus();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('You selected time is less than the current time.')</Script>");
                           
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد اخترت الوقت أقل من الوقت الحالي.')</Script>");
                        //    TextBox4.Text = "";
                        //    TextBox4.Focus();
                        //}
                        //Label8.Text = "<b>Alert!</b>   <p>You selected time is less than the current time.</p>";
                        //Label8.ForeColor = System.Drawing.Color.Red;
                        //this.ModalPopupExtender3.Show();
                    }
                    else
                    {
                        List<string> dt = new List<string>();

                        DateTime t1 = DateTime.Parse(TextBox5.Text);

                        DateTime t2 = DateTime.Parse(TextBox6.Text);

                        string start_time = t1.ToShortTimeString();
                        string end_time = t2.ToShortTimeString();
                        int d = int.Parse(DropDownList1.SelectedItem.Text);
                        d = d + int.Parse(DropDownList2.SelectedItem.Text);
                        while (DateTime.Parse(start_time) < DateTime.Parse(end_time))
                        {

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
                            BtnResetDateTime.Enabled = true;
                            Button2.Enabled = false;
                        }
                        else
                        {
                            Button2.Enabled = true;
                            Button3.Enabled = false;
                            //Label8.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                            //Label8.ForeColor = System.Drawing.Color.Red;
                            //this.ModalPopupExtender3.Show();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                            TextBox5.Text = "";
                            TextBox5.Focus();
                            TextBox6.Text = "";
                            CheckBoxList3.ClearSelection();
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                                


                            //}
                            //else
                            //{
                            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد.')</Script>");
                            //    TextBox5.Text = "";
                            //    TextBox5.Focus();
                            //    TextBox6.Text = "";
                            //}
                        }
                    }
                }
                else
                {

                    List<string> dt = new List<string>();

                    DateTime t1 = DateTime.Parse(TextBox5.Text);

                    DateTime t2 = DateTime.Parse(TextBox6.Text);
                    //string[] st = TextBox5.Text.Split(':');
                    //string[]st1 = st[1].Split(' ');
                    //string[] se = TextBox6.Text.Split(':');
                    //string[] se1 = se[1].Split(' ');
                    //TimeSpan start = new TimeSpan(int.Parse(st[0]), int.Parse(st1[0]), 0); //10 o'clock
                    //TimeSpan end = new TimeSpan(int.Parse(se[0]), int.Parse(se1[0]), 0); //12 o'clock
                    //TimeSpan now = DateTime.Now.TimeOfDay;
                    //if ((now > start) && (now < end))
                    //{
                    string start_time = t1.ToShortTimeString();
                    string end_time = t2.ToShortTimeString();
                    int d = int.Parse(DropDownList1.SelectedItem.Text);
                    d = d + int.Parse(DropDownList2.SelectedItem.Text);
                    while (DateTime.Parse(start_time) < DateTime.Parse(end_time))
                    {
                        //DateTime tcheck = DateTime.Parse(start_time).AddMinutes(d);
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
                        BtnResetDateTime.Enabled = true;
                        Button2.Enabled = false;
                    }
                    else
                    {
                        Button2.Enabled = true;
                        Button3.Enabled = false;
                        //Label8.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                        //Label8.ForeColor = System.Drawing.Color.Red;
                        //this.ModalPopupExtender3.Show();
                        //if (Session["Language"].ToString() == "Auto")
                        //{
                        TextBox5.Text = "";
                        TextBox5.Focus();
                        TextBox6.Text = "";
                        CheckBoxList3.ClearSelection();
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                           
                        //}
                        //else
                        //{
                        //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد.')</Script>");
                        //    TextBox5.Text = "";
                        //    TextBox5.Focus();
                        //    TextBox6.Text = "";
                        //}
                    }


                }

            }
            if (TextBox8.Text != "")
            {
                DateTime d1 = DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime d2 = DateTime.Parse(TextBox7.Text);
                DateTime StartDate = DateTime.Parse(TextBox7.Text);
                DateTime EndDate = DateTime.Parse(TextBox8.Text);
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
                            //Label8.Text = "<b>Alert!</b>   <p>You selected time is less than the current time.</p>";
                            //Label8.ForeColor = System.Drawing.Color.Red;
                            //this.ModalPopupExtender3.Show();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                            TextBox8.Text = "";
                            TextBox8.Focus();
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('You selected time is less than the current time.')</Script>");
                               

                            //}
                            //else
                            //{
                            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد اخترت الوقت أقل من الوقت الحالي.')</Script>");
                            //    TextBox8.Text = "";
                            //    TextBox8.Focus();
                            //}
                        }
                        else
                        {
                            List<string> dt = new List<string>();

                            DateTime t1 = DateTime.Parse(TextBox5.Text);

                            DateTime t2 = DateTime.Parse(TextBox6.Text);

                            string start_time = t1.ToShortTimeString();
                            string end_time = t2.ToShortTimeString();
                            int d = int.Parse(DropDownList1.SelectedItem.Text);
                            d = d + int.Parse(DropDownList2.SelectedItem.Text);
                            while (DateTime.Parse(start_time) < DateTime.Parse(end_time))
                            {

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
                                BtnResetDateTime.Enabled = true;
                                Button2.Enabled = false;
                            }
                            else
                            {
                                Button2.Enabled = true;
                                Button3.Enabled = false;
                                //Label8.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                                //Label8.ForeColor = System.Drawing.Color.Red;
                                //this.ModalPopupExtender3.Show();
                                //if (Session["Language"].ToString() == "Auto")
                                //{
                                TextBox5.Text = "";
                                TextBox5.Focus();
                                TextBox6.Text = "";
                                CheckBoxList3.ClearSelection();
                                RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                                    
                                //}
                                //else
                                //{
                                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد.')</Script>");
                                //    TextBox5.Text = "";
                                //    TextBox5.Focus();
                                //    TextBox6.Text = "";
                                //}
                            }



                            if (RadioButtonList1.SelectedIndex == 1)
                            {
                                Button3.Enabled = true;
                                List<string> dt1 = new List<string>();
                                dt1.Clear();

                                var res = new List<DateTime>();
                                var start = DateTime.Parse(TextBox7.Text);
                                var end = DateTime.Parse(TextBox8.Text);
                                for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
                                    dt1.Add(ndate.ToShortDateString());
                                date.Clear();
                                foreach (string ss in dt1)
                                {

                                    string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
                                    var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == s select item;
                                    if (Query.Count() > 0)
                                    {

                                    }
                                    else
                                    {
                                        date.Add(s);
                                    }
                                }
                                //DropDownList3.Enabled = true;
                            }

                            CheckBoxList2.DataSource = date;
                            CheckBoxList2.DataBind();
                            Session["stime"] = TextBox7.Text;
                            Session["etime"] = TextBox8.Text;
                            TextBox7.Text = "";
                            TextBox8.Text = "";
                            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                            {
                                CheckBoxList2.Items[i].Selected = true;
                            }

                            CheckBoxList3.Items.Add("Sunday");
                            CheckBoxList3.Items.Add("Monday");
                            CheckBoxList3.Items.Add("Tuesday");
                            CheckBoxList3.Items.Add("Wednesday");
                            CheckBoxList3.Items.Add("Thursday");
                            CheckBoxList3.Items.Add("Friday");
                            CheckBoxList3.Items.Add("Saturday");
                            for (int i = 0; i < CheckBoxList3.Items.Count; i++)
                            {
                                CheckBoxList3.Items[i].Selected = true;

                            }
                            Button1.Visible = true;
                           

                            }
                    }

                    else
                    {
                        List<string> dt = new List<string>();

                        DateTime t1 = DateTime.Parse(TextBox5.Text);

                        DateTime t2 = DateTime.Parse(TextBox6.Text);

                        string start_time = t1.ToShortTimeString();
                        string end_time = t2.ToShortTimeString();
                        int d = int.Parse(DropDownList1.SelectedItem.Text);
                        d = d + int.Parse(DropDownList2.SelectedItem.Text);
                        while (DateTime.Parse(start_time) < DateTime.Parse(end_time))
                        {

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
                            BtnResetDateTime.Enabled = true;
                            Button2.Enabled = false;
                        }
                        else
                        {
                            Button2.Enabled = true;
                            Button3.Enabled = false;
                            //Label8.Text = "<b>Alert!</b>   <p>Your selected duration and break time is not apt with the given time.</p>";
                            //Label8.ForeColor = System.Drawing.Color.Red;
                            //this.ModalPopupExtender3.Show();
                            //if (Session["Language"].ToString() == "Auto")
                            //{
                            TextBox5.Text = "";
                            TextBox5.Focus();
                            TextBox6.Text = "";
                            CheckBoxList3.ClearSelection();
                            RegisterStartupScript("", "<Script Language=JavaScript>swal('Your selected duration and break time is not apt with the given time.')</Script>");
                               
                            //}
                            //else
                            //{
                            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('المدة المحددة ووقت التعطل ليست مناسبة مع الوقت المحدد.')</Script>");
                            //    TextBox5.Text = "";
                            //    TextBox5.Focus();
                            //    TextBox6.Text = "";
                            //}
                        }



                        if (RadioButtonList1.SelectedIndex == 1)
                        {
                            Button3.Enabled = true;
                            List<string> dt1 = new List<string>();
                            dt1.Clear();

                            var res = new List<DateTime>();
                            var start = DateTime.Parse(TextBox7.Text);
                            var end = DateTime.Parse(TextBox8.Text);
                            for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
                                dt1.Add(ndate.ToShortDateString());
                            date.Clear();
                            foreach (string ss in dt1)
                            {

                                string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
                                var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == s select item;
                                if (Query.Count() > 0)
                                {

                                }
                                else
                                {
                                    date.Add(s);
                                }
                            }
                            //DropDownList3.Enabled = true;
                        }

                        CheckBoxList2.DataSource = date;
                        CheckBoxList2.DataBind();
                        Session["stime"] = TextBox7.Text;
                        Session["etime"] = TextBox8.Text;
                        TextBox7.Text = "";
                        TextBox8.Text = "";

                        for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                        {
                            CheckBoxList2.Items[i].Selected = true;
                        }

                        CheckBoxList3.Items.Add("Sunday");
                        CheckBoxList3.Items.Add("Monday");
                        CheckBoxList3.Items.Add("Tuesday");
                        CheckBoxList3.Items.Add("Wednesday");
                        CheckBoxList3.Items.Add("Thursday");
                        CheckBoxList3.Items.Add("Friday");
                        CheckBoxList3.Items.Add("Saturday");
                        for (int i = 0; i < CheckBoxList3.Items.Count; i++)
                        {
                            CheckBoxList3.Items[i].Selected = true;

                        }
                        if(start_time==end_time)
                        {
                            //pnldate.Visible = false;
                            //Button3.Enabled = false;
                            //BtnResetDateTime.Enabled = false;
                            
                        }
                        Button1.Visible = true;
                    }
                }
                else
                {
                    //msg
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
            if (RadioButtonList1.SelectedIndex == 0)
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
        catch (Exception ex)
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
            TextBox8.Text = "";
            TextBox8.Focus();
            RegisterStartupScript("", "<Script Language=JavaScript>swal('You selected time is less than the current time.')</Script>");
               

            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد اخترت الوقت أقل من الوقت الحالي.')</Script>");
            //    TextBox8.Text = "";
            //    TextBox8.Focus();
            //}
        }

    }



    protected void Button3_Click(object sender, EventArgs e)
    {

        if (CheckBoxList1.SelectedIndex != -1 && CheckBoxList2.SelectedIndex != -1)//&& CheckBoxList3.SelectedIndex!=-1
        {


            var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == TextBox4.Text select item;
            if (Query.Count() > 0)
            {
                Button3.Enabled = false;
                BtnResetDateTime.Enabled = false;
                //if (Session["Language"].ToString() == "Auto")
                //{
                TextBox4.Text = "";
                TextBox4.Focus();
                RegisterStartupScript("", "<Script Language=JavaScript>swal('You already set the availability times in this date so please select another date.')</Script>");
                   

                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('لقد قمت بالفعل بتعيين أوقات التوفر في هذا التاريخ لذا يرجى تحديد تاريخ آخر.')</Script>");
                //    TextBox4.Text = "";
                //    TextBox4.Focus();
                //}
                //Label8.Text = "You already set the availability times in this date so please select another date.";
                //this.ModalPopupExtender3.Show();
            }
            else
            {
                //string time = "";

                if (TextBox4.Text != "")
                {
                    doctor_availability da = new doctor_availability()
                    {
                        d_id = Session["hakkeemid_d"].ToString(),

                        a_date = DateTime.Parse(TextBox4.Text).ToString("yyyy-MM-dd"),
                        //a_time = "",
                        status = 0,
                    };
                    db.doctor_availabilities.InsertOnSubmit(da);
                    db.SubmitChanges();
                    var QueryDate = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() select item.id;
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


                            tbl_doc_time dt = new tbl_doc_time()
                            {
                                date_id = max,
                                time = newtime,
                            };
                            db.tbl_doc_times.InsertOnSubmit(dt);
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


                            doctor_availability da = new doctor_availability()
                            {
                                d_id = Session["hakkeemid_d"].ToString(),

                                a_date = CheckBoxList2.Items[c].Text,
                                //a_time = "",
                                status = 0,
                            };
                            db.doctor_availabilities.InsertOnSubmit(da);
                            db.SubmitChanges();
                            var QueryDate = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() select item.id;
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

                                    tbl_doc_time dt = new tbl_doc_time()
                                    {
                                        date_id = max,
                                        time = newtime,
                                    };
                                    db.tbl_doc_times.InsertOnSubmit(dt);
                                    db.SubmitChanges();
                                }

                                i++;
                            }
                        }
                    }
                }
                Button3.Enabled = false;
                BtnResetDateTime.Enabled = false;
                CheckBoxList1.Items.Clear();
                date.Clear();
                CheckBoxList2.Items.Clear();
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully set your availability. Thank you doctor.')</Script>");
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('تم تعيين التوفر بنجاح. شكرا لك الطبيب.')</Script>");

                //}
                //Label9.Text = "Successfully set your availability. Thank you doctor";
                //this.ModalPopupExtender4.Show();
            }

        }
        else
        {
            //if (Session["Language"].ToString() == "Auto")
            //{
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select at least one date, time or day.')</Script>");
            //}
            //else
            //{
            //    RegisterStartupScript("", "<Script Language=JavaScript>swal('يرجى تحديد تاريخ أو وقت واحد على الأقل.')</Script>");

            //}
            //Label10.Text = "Please select at least one date or time";
            //this.ModalPopupExtender5.Show();
        }

    }


    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            TextBox4.Enabled = true;
            TextBox4.Visible = true;
            TextBox7.Visible = false;
            TextBox8.Visible = false;
            TextBox8.Enabled = false;
            TextBox7.Enabled = false;
            TextBox7.Text = "";
            TextBox8.Text = "";
            //DropDownList3.Enabled = false;
        }
        if (RadioButtonList1.SelectedIndex == 1)
        {
            //TextBox8.Enabled = true;
            TextBox7.Enabled = true;
            TextBox4.Enabled = false;
            TextBox4.Text = "";
            //DropDownList3.Enabled = true;
            TextBox4.Visible = false;
            TextBox7.Visible = true;
            TextBox8.Visible = true;
        }
    }

    protected void TextBox7_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime dt2 = DateTime.Parse(TextBox7.Text);

            if (dt2 >= dt1)
            {
                TextBox8.Enabled = true;
            }
            else
            {
                TextBox8.Enabled = false;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Selected date most be the current date or above.')</Script>");
                    TextBox7.Text = "";
                    TextBox7.Focus();
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('التاريخ المحدد يكون معظم التاريخ الحالي أو أعلى.')</Script>");
                //    TextBox7.Text = "";
                //    TextBox7.Focus();
                //}
                //Label11.Text = "Selected date most be the current date or above";
                //this.ModalPopupExtender6.Show();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void TextBox8_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = DateTime.Parse(TextBox7.Text);
            DateTime dt2 = DateTime.Parse(TextBox8.Text);

            if (dt2 > dt1)
            {
                List<string> adate = new List<string>();
                List<string> dtt1 = new List<string>();
                var res = new List<DateTime>();
                var start = DateTime.Parse(TextBox7.Text);
                var end = DateTime.Parse(TextBox8.Text);
                for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
                    dtt1.Add(ndate.ToShortDateString());
                date.Clear();
                foreach (string ss in dtt1)
                {

                    string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
                    var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == s select item;
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
                    //Label13.Text = "Appointment already fixed this date </br>" + available;
                    //ModalPopupExtender8.Show();
                    //string msg = "Appointment already fixed this date </br>" + available;
                    //RegisterStartupScript("", "<Script Language=JavaScript>swal('" + msg + "')</Script>");

                }
                Button2.Enabled = true;
            }
            else
            {
                TextBox8.Enabled = true;
                Button2.Enabled = false;
                //if (Session["Language"].ToString() == "Auto")
                //{
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Selected date most be greater than the starting date.')</Script>");
                    TextBox8.Text = "";
                    TextBox8.Focus();
                //}
                //else
                //{
                //    RegisterStartupScript("", "<Script Language=JavaScript>swal('التاريخ المحدد يكون أكبر من تاريخ البدء.')</Script>");
                //    TextBox8.Text = "";
                //    TextBox8.Focus();
                //}
                //Label12.Text = "Selected date most be greater than the starting date.";
                //this.ModalPopupExtender7.Show();
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void BtnResetDateTime_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor.aspx?l=ar-EG");
        //}
    }

    //protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (DropDownList3.SelectedIndex == 0)
    //        {
    //            List<string> dt1 = new List<string>();
    //            var res = new List<DateTime>();
    //            var start = DateTime.Parse(Session["stime"].ToString());
    //            var end = DateTime.Parse(Session["etime"].ToString());
    //            for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
    //                dt1.Add(ndate.ToShortDateString());
    //            date.Clear();
    //            foreach (string ss in dt1)
    //            {

    //                string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
    //                var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == s select item;
    //                if (Query.Count() > 0)
    //                {

    //                }
    //                else
    //                {
    //                    date.Add(s);
    //                }
    //            }
    //            CheckBoxList2.DataSource = date;
    //            CheckBoxList2.DataBind();
    //            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
    //            {
    //                CheckBoxList2.Items[i].Selected = true;
    //            }
    //        }

    //        //****************Saturday and Sunday only*****************************//
    //        if (DropDownList3.SelectedIndex == 1)
    //        {
    //            date.Clear();
    //            DateTime startDate = DateTime.Parse(Session["stime"].ToString());
    //            DateTime endDate = DateTime.Parse(Session["etime"].ToString());

    //            TimeSpan diff = endDate - startDate;
    //            int days = diff.Days;
    //            for (var i = 0; i <= days; i++)
    //            {
    //                var testDate = startDate.AddDays(i);
    //                switch (testDate.DayOfWeek)
    //                {
    //                    case DayOfWeek.Saturday:
    //                    case DayOfWeek.Sunday:
    //                        //Console.WriteLine(testDate.ToShortDateString());
    //                        date.Add(testDate.ToString("yyyy-MM-dd"));
    //                        break;
    //                }
    //            }
    //            CheckBoxList2.DataSource = date;
    //            CheckBoxList2.DataBind();
    //            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
    //            {
    //                CheckBoxList2.Items[i].Selected = true;
    //            }
    //        }
    //        //##*****************end Saturday and Sunday only*********************##//

    //        if (DropDownList3.SelectedIndex == 2)
    //        {
    //            List<string> dt1 = new List<string>();
    //            var res = new List<DateTime>();
    //            var start = DateTime.Parse(Session["stime"].ToString());
    //            var end = DateTime.Parse(Session["etime"].ToString());
    //            for (var ndate = start; ndate <= end; ndate = ndate.AddDays(1))
    //                dt1.Add(ndate.ToShortDateString());
    //            date.Clear();
    //            foreach (string ss in dt1)
    //            {

    //                string s = DateTime.Parse(ss).ToString("yyyy-MM-dd");
    //                var Query = from item in db.doctor_availabilities where item.d_id == Session["hakkeemid_d"].ToString() && item.a_date == s select item;
    //                if (Query.Count() > 0)
    //                {

    //                }
    //                else
    //                {
    //                    date.Add(s);
    //                }
    //            }
    //            CheckBoxList2.DataSource = date;
    //            CheckBoxList2.DataBind();
    //            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
    //            {
    //                CheckBoxList2.Items[i].Selected = true;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}



    protected void Button6_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Doctor/Doctor.aspx");
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        //if (Session["Language"].ToString() == "Auto")
        //{
            Response.Redirect("~/Doctor/Doctor.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/Doctor/Doctor.aspx?l=ar-EG");
        //}
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        //Button2.Enabled = true;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        List<string> list_date = new List<string>();
        int c = 0;
        for (int i = 0; i < CheckBoxList3.Items.Count; i++)
        {
            if (CheckBoxList3.Items[i].Selected)
            {
                list_date.Add(CheckBoxList3.Items[i].Text);
                c++;
            }
        }
        if (c != 0)
        {
            date.Clear();

            DateTime startDate = DateTime.Parse(Session["stime"].ToString());
            DateTime endDate = DateTime.Parse(Session["etime"].ToString());
            TimeSpan diff = endDate - startDate;
            int days = diff.Days;
            for (var i = 0; i <= days; i++)
            {
                var testDate = startDate.AddDays(i);
                foreach (string dy in list_date)
                {
                    if (dy == testDate.DayOfWeek.ToString())
                    {
                        date.Add(testDate.ToString("yyyy-MM-dd"));
                    }
                    //switch (testDate.DayOfWeek)
                    //{

                    //    case DayOfWeek.Saturday:
                    //    case DayOfWeek.Sunday:
                    //        //Console.WriteLine(testDate.ToShortDateString());
                    //        date.Add(testDate.ToString("yyyy-MM-dd"));
                    //        break;
                    //}
                }

            }
            CheckBoxList2.DataSource = date;
            CheckBoxList2.DataBind();
            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                CheckBoxList2.Items[i].Selected = true;
            }
            Button3.Enabled = true;
        }
        else
        {
            Button3.Enabled = false;
            RegisterStartupScript("", "<Script Language=JavaScript>swal('Please select your available days')</Script>");
        }

    }
}