using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class User_UploadTestReports : System.Web.UI.Page
{
    databaseDataContext db = new databaseDataContext();

    protected override void InitializeCulture()
    {
        //Session["Speciality"] = "Auto";
        //string culture = "Auto";
        //try
        //{
        //    culture = Request.QueryString["l"].ToString();
        //    Session["Speciality"] = culture;
        //}
        //catch (Exception ex)
        //{ }
        //// string culture = Session["Speciality"].ToString();
        //if (string.IsNullOrEmpty(culture))
        //    culture = "Auto";
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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["hakkemid_u"] == null)
        {
            //if (Session["Speciality"].ToString() == "Auto")
            //{
                Response.Redirect("~/Index/SignInSignUp.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/Index/SignInSignUp.aspx?l=ar-EG");
            //}
        }
        if (!IsPostBack)
        {
            Reports();
        }
    }

    public void Reports()
    {
        var Query = from item in db.tbl_test_reports where item.u_id == Session["hakkemid_u"].ToString() select item;
        GridView2.DataSource = Query;
        GridView2.DataBind();
        foreach(GridViewRow gr in GridView2.Rows)
        {
            Label lbl3 = gr.FindControl("Label3") as Label;
            string[] a = lbl3.Text.Split(' ');
            Label lbl4 = gr.FindControl("Label4") as Label;
            lbl4.Text = a[0].ToString();
            LinkButton lb = gr.FindControl("blood") as LinkButton;
            LinkButton lu = gr.FindControl("urine") as LinkButton;
            LinkButton ls = gr.FindControl("scan") as LinkButton;
            LinkButton lx = gr.FindControl("xray") as LinkButton;
            LinkButton lo = gr.FindControl("Other") as LinkButton;
            if (lb.CommandArgument != "")
            { }
            else { lb.Text = "------";
                lb.Enabled = false;
            }
            if (lu.CommandArgument != "")
            { }
            else { lu.Text = "------";
                lu.Enabled = false;
            }
            if (ls.CommandArgument != "")
            { }
            else { ls.Text = "------";

                ls.Enabled = false;
            }
            if (lx.CommandArgument != "")
            { }
            else { lx.Text = "------";
                lx.Enabled = false;
            }
            if (lo.CommandArgument != "")
            { }
            else { lo.Text = "------";
                lo.Enabled = false;
            }
        }
    }

    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        
        string id = "";
        try
        {
            if (FupBloodTest.HasFile || FupScanRep.HasFile || FupUrineTest.HasFile || FupXrayRep.HasFile || FupOtherReport.HasFile )
            {
                var query = from item in db.tbl_test_reports
                            select item;
                if (query.Count() > 0)
                {
                    var query1 = (from item in db.tbl_test_reports
                                  select item.id).Max();
                    id = (query1 + 1).ToString();
                }
                else
                {
                    id = 0.ToString();
                }
                int test = 0;


                tbl_test_report td = new tbl_test_report();

                td.u_id = Session["hakkemid_u"].ToString();
                td.date = DateTime.Now.Date;

                if (FupBloodTest.HasFile)
                {
                    string extn = Path.GetExtension(FupBloodTest.FileName).ToLower();
                    if (extn == ".jpg" || extn == ".jpeg" || extn == ".pdf" || extn == ".png" || extn == ".bmp")
                    {
                        FupBloodTest.SaveAs(Server.MapPath("../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 1 + FupBloodTest.FileName));
                        td.blood_test_report = "../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 1 + FupBloodTest.FileName;
                    }
                    else
                    {
                        test = 1;
                       
                    }
                }
                if (FupUrineTest.HasFile)
                {
                    string extn = Path.GetExtension(FupUrineTest.FileName).ToLower();
                    if (extn == ".jpg" || extn == ".jpeg" || extn == ".pdf" || extn == ".png" || extn == ".bmp")
                    {
                        FupUrineTest.SaveAs(Server.MapPath("../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 2 + FupUrineTest.FileName));
                        td.urine_test_report = "../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 2 + FupUrineTest.FileName;
                    }
                    else
                    {
                        test = 1;
                        
                    }
                }
                if (FupScanRep.HasFile)
                {
                    string extn = Path.GetExtension(FupScanRep.FileName).ToLower();
                    if (extn == ".jpg" || extn == ".jpeg" || extn == ".pdf" || extn == ".png" || extn == ".bmp")
                    {
                        FupScanRep.SaveAs(Server.MapPath("../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 3 + FupScanRep.FileName));
                        td.scan_test_report = "../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 3 + FupScanRep.FileName;
                    }
                    else
                    {
                        test = 1;
                     
                    }
                }
                if (FupXrayRep.HasFile)
                {
                    string extn = Path.GetExtension(FupXrayRep.FileName).ToLower();
                    if (extn == ".jpg" || extn == ".jpeg" || extn == ".pdf" || extn == ".png" || extn == ".bmp")
                    {
                        FupXrayRep.SaveAs(Server.MapPath("../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 4 + FupXrayRep.FileName));
                        td.xray_test_report = "../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 4 + FupXrayRep.FileName;
                    }
                    else
                    {
                        test = 1;
                       
                    }
                }

              
                    if (FupOtherReport.HasFile)
                    {
                    if (TxtOtherFileName.Text != "")
                    {
                        string extn = Path.GetExtension(FupOtherReport.FileName).ToLower();
                        if (extn == ".jpg" || extn == ".jpeg" || extn == ".pdf" || extn == ".png" || extn == ".bmp")
                        {

                            test = 0;
                            FupOtherReport.SaveAs(Server.MapPath("../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 5 + FupOtherReport.FileName));
                            td.other_test_name = TxtOtherFileName.Text;
                            td.other_test_report = "../PatientsTestReports/" + Session["hakkemid_u"].ToString() + id + 5 + FupOtherReport.FileName;
                        }
                       
                    }
                    else
                    {
                        test = 1;
                        RegisterStartupScript("", "<Script Language=JavaScript>swal('Please fill the name of report')</Script>");

                    }
                   
                }


                //RegisterStartupScript("", "<Script Language=JavaScript>alert('Uploaded succesfully ')</Script>");
                TxtOtherFileName.Text = "";

                if (test == 0)
                {
                    db.tbl_test_reports.InsertOnSubmit(td);
                    db.SubmitChanges();
                    //Label2.Text = "Uploaded succesfully";
                    //this.ModalPopupExtender3.Show();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Uploaded succesfully ')</Script>");
                }
                else
                {

                    //Label2.Text = "Reports only image or PDF format";
                    //this.ModalPopupExtender3.Show();
                    RegisterStartupScript("", "<Script Language=JavaScript>swal('Reports only image or PDF format')</Script>");
                }
            }
            else
            {
                RegisterStartupScript("", "<Script Language=JavaScript>swal('Please choose atleast one report')</Script>");
                //Label2.Text = "Please enter a file name for other reports";
                //this.ModalPopupExtender3.Show();
            }
        }
        catch (Exception ex)
        {
        }
        Reports();
    }
        




    protected void blood_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void urine_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void scan_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void xray_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void Other_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void BtnSubmitOTP_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/User/UploadTestReports.aspx");
    }
}