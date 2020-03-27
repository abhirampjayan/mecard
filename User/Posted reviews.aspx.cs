using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Posted_reviews : System.Web.UI.Page
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
        //if (Session["Speciality"].ToString() == "Auto")
        //{
            
        //}
        //else
        //{
        // //   Button1.Text = "تؤكد";
        //  //  Button2.Text = "تؤكد";
        //}
            if (!IsPostBack) { Feed(); }
    }
    public void Feed()
    {
        var Query = from item in db.tbl_user_feeds where item.u_email == Session["hakkemid_u"].ToString() select item;
        if (Query.Count() > 0)
        {
            Label7.Visible = false;
            GridView1.DataSource = Query;
            GridView1.DataBind();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lbl1 = gr.FindControl("Label1") as Label;
                Label lbl2 = gr.FindControl("Label2") as Label;
                Label lbl3 = gr.FindControl("Label3") as Label;
                Label lbl4 = gr.FindControl("Label4") as Label;
                Label lbl5 = gr.FindControl("Label5") as Label;
                Image img1 = gr.FindControl("Image1") as Image;
                var Query1 = from item in db.tbl_hos_appmnt_histories where item.id == int.Parse(lbl5.Text) select item;
                foreach (var ss in Query1)
                {

                    if (ss.h_id == null)
                    {
                        var Query2 = from item in db.tbl_doctors where item.d_hakkimid == lbl2.Text select item;
                        foreach (var dd in Query2) { lbl1.Text = dd.d_name;
                            if (dd.d_photo == "" || dd.d_photo == null)
                            {
                                img1.ImageUrl = "../Doctorimages/doctor.png";
                            }
                            else
                            {

                                img1.ImageUrl = dd.d_photo;
                            }
                        }
                    }
                    else
                    {
                        var Query3 = from item in db.tbl_hdoctors where item.hd_email == lbl2.Text select item;
                        foreach (var dd in Query3) { lbl1.Text = dd.hd_name;

                            if (dd.hd_photo == "" || dd.hd_photo == null)
                            {
                                img1.ImageUrl = "../Doctorimages/doctor.png";
                            }
                            else
                            {

                                img1.ImageUrl = dd.hd_photo;
                            }
                        }
                    }
                    string[] a = ss.a_date.ToString().Split(' ');
                    lbl3.Text = a[0]; lbl4.Text = ss.a_time;
                }
            }
        }
        else
        {
            Label7.Visible = true;
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        Feed();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = (GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label6") as Label).Text;
        Session["did"] = id;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#myModal2').modal();", true);
        upModal2.Update();

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = (GridView1.Rows[e.RowIndex].Cells[2].FindControl("Label6") as Label).Text;
        TextBox review = GridView1.Rows[e.RowIndex].Cells[2].FindControl("TextBox2") as TextBox;
        if (review.Text != "")
        {
            Session["comment"] = review.Text;
            Session["id"] = id;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#myModal1').modal();", true);
            upModal1.Update();

         
            //Label7.Text = "Successfully updated";
            //this.ModalPopupExtender3.Show();
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        Feed();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_user_feeds where item.id == int.Parse(Session["id"].ToString()) select item;
        foreach (var ss in Query) { ss.u_review = Session["comment"].ToString(); }
        db.SubmitChanges();
        GridView1.EditIndex = -1;
        Feed();
        Session["comment"] = "";
        Session["id"] = "";
        //if (Session["Speciality"].ToString() == "Auto")
        //{
            Response.Redirect("Posted reviews.aspx");
        //}
        //else
        //{
        //    Response.Redirect("Posted reviews.aspx?l=ar-EG");
        //}
        //RegisterStartupScript("", "<Script Language=JavaScript>swal('Successfully updated')</Script>");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        var Query = from item in db.tbl_user_feeds where item.id == int.Parse(Session["did"].ToString()) select item;
        foreach (var ss in Query) { db.tbl_user_feeds.DeleteOnSubmit(ss); }
        db.SubmitChanges();
        Feed();
        //if (Session["Speciality"].ToString() == "Auto")
        //{
            Response.Redirect("~/User/Posted reviews.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/User/Posted reviews.aspx?l=ar-EG");
        //}
    }
}