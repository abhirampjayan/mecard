<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" Culture="en-US" UICulture="en-US" CodeFile="User account.aspx.cs" Inherits="User_User_account" meta:resourcekey="PageResource2" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Design/plugins/datepicker/bootstrap-datepicker.js"></script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.4;
        }

        .modalPopup {
            /*background-color: #FFFFFF;*/
            border-width: 0px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 3px;
            width: 500px;
            height: 300px;
        }

        .gridview {
            background-color: #fff;
            padding: 2px;
            margin: 2% auto;
        }

            .gridview a {
                margin: auto 1%;
                border-radius: 50%;
                background-color: #4aa9af;
                padding: 5px 10px 5px 10px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: rgba(199, 198, 198, 0.28);
                    color: #4aa9af;
                }

            .gridview span {
                background-color: #000;
                color: #fff;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }

           /* ----------- iPhone 4 and 4S ----------- */

/* Portrait and Landscape */
@media only screen 
  and (min-device-width: 320px) 
  and (max-device-width: 480px)
  and (-webkit-min-device-pixel-ratio: 2) {
        td, .form-control{
        font-size:9px;
    }
    .box-title{
        font-size:15px !important;
    }
}

/* Portrait */
@media only screen 
  and (min-device-width: 320px) 
  and (max-device-width: 480px)
  and (-webkit-min-device-pixel-ratio: 2)
  and (orientation: portrait) {

    td, .form-control{
        font-size:9px;
    }
       .box-title{
        font-size:15px !important;
    }
}

/* Landscape */
@media only screen 
  and (min-device-width: 320px) 
  and (max-device-width: 480px)
  and (-webkit-min-device-pixel-ratio: 2)
  and (orientation: landscape) {

        td , .form-control{
        font-size:9px;
    }
           .box-title{
        font-size:15px !important;
    }
}

/* ----------- iPhone 5, 5S, 5C and 5SE ----------- */

/* Portrait and Landscape */
@media only screen 
  and (min-device-width: 320px) 
  and (max-device-width: 568px)
  and (-webkit-min-device-pixel-ratio: 2) {
        td, .form-control{
        font-size:9px;
    }
           .box-title{
        font-size:15px !important;
    }

}

    </style>
    <script>
        $(function () {

            //Timepicker
            //$(".timepicker").timepicker({
            //    showInputs: false
            //});

            //Date range picker
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                //format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true,

            });

        });
    </script>
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>
     <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>

  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script>
        $(function () {

            //Timepicker
            //$(".timepicker").timepicker({
            //    showInputs: false
            //});

            //Date range picker
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                //format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true,

            });

        });
    </script>
    <section class="content" style="margin-top:1.5cm;margin-bottom:1cm;">
        <div class="container-fluid">
            <div>
                <div class="row">
                 <%--  <%  if (Session["Speciality"].ToString() == "Auto")
                       { %>--%>

                     <div class="col-md-6">
                    <%--<%}
    else
    { %>

                          <div class="col-md-6 pull-right">

                    <%} %>--%>
                   
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="box box-primary" style="margin-top: 1%">
                                    <div class="box-header">
                                        <h3 class="box-title">
                                            <asp:Label ID="Label20" runat="server" Text="Account details" meta:resourcekey="Label20Resource1"></asp:Label></h3>
                                    </div>
                                    <div class="box-body">
                                        <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateRows="False" OnModeChanging="DetailsView1_ModeChanging" OnItemUpdating="DetailsView1_ItemUpdating" meta:resourcekey="DetailsView1Resource2" OnDataBound="DetailsView1_DataBound1">
                                            <Fields>
                                                <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource17">
                                                    <EditItemTemplate>

                                                        <asp:TextBox ID="TextName" CssClass="form-control" ValidationGroup="a" runat="server" Text='<%# Bind("name") %>' meta:resourcekey="TextNameResource2"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextName" ValidationGroup="a" runat="server" ErrorMessage="* Required" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("name") %>' meta:resourcekey="Label1Resource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email" meta:resourcekey="TemplateFieldResource18">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextEmail" ValidationGroup="a" Enabled="false" CssClass="form-control" runat="server" Text='<%# Bind("email") %>' meta:resourcekey="TextEmailResource2"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="TextEmail" runat="server" ErrorMessage="* Enter valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="a" meta:resourcekey="RegularExpressionValidator2Resource2"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextEmail" ValidationGroup="a" runat="server" ErrorMessage="* Required" ForeColor="Red" meta:resourcekey="RequiredFieldValidator2Resource2"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("email") %>' meta:resourcekey="Label2Resource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact number" meta:resourcekey="TemplateFieldResource19">
                                                    <EditItemTemplate>
                                                        <%--<asp:Label ID="Label19" runat="server" Text="+966"></asp:Label>--%>
                                                         <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa">
                            <asp:Label ID="Label19" runat="server" Text=""></asp:Label></i>
                      </div>
                        <asp:TextBox ID="TextContact" CssClass="form-control" ValidationGroup="a" runat="server" meta:resourcekey="TextContactResource2" Text='<%# Bind("contact") %>'  MaxLength="9"></asp:TextBox>
                    </div>
                            
                                                     <%--  <asp:TextBox ID="TextContact" ValidationGroup="a" CssClass="form-control" runat="server" Text='<%# Bind("contact") %>' meta:resourcekey="TextContactResource2" MaxLength="9"></asp:TextBox>--%>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label19" runat="server" Text="" meta:resourcekey="Label19Resource4"></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("contact") %>' meta:resourcekey="Label3Resource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country" meta:resourcekey="TemplateFieldResource20">
                                                    <EditItemTemplate>
                                                       <%-- <asp:TextBox ID="TextCountry" ValidationGroup="a" CssClass="form-control" runat="server" Text='<%# Bind("country") %>' meta:resourcekey="TextCountryResource2"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="DdlNationality" runat="server" CssClass="form-control" placeholder="choose to change country" meta:resourcekey="DdlNationalityResource1"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("country") %>' meta:resourcekey="Label4Resource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address" meta:resourcekey="TemplateFieldResource21">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextAddress" ValidationGroup="a" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" Text='<%# Bind("address") %>' meta:resourcekey="TextAddressResource2"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("address") %>' meta:resourcekey="Label5Resource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of birth" meta:resourcekey="TemplateFieldResource22">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextDob" ValidationGroup="a" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" runat="server" Text='<%# Bind("dob") %>' meta:resourcekey="TextDobResource2"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("dob") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource23">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton6" ValidationGroup="a" runat="server" CommandName="update" CssClass="btn btn-flat btn-success btn-xs" meta:resourcekey="LinkButton6Resource2">Save changes</asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton7" runat="server" CommandName="cancel" CssClass="btn btn-flat btn-default btn-xs" meta:resourcekey="LinkButton7Resource2">Cancel</asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton4" CommandName="edit" CssClass="btn btn-flat btn-success btn-xs" runat="server" meta:resourcekey="LinkButton4Resource2">Edit profile</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#F0F0F0" />
                                                    <ItemStyle BackColor="#F0F0F0" />
                                                </asp:TemplateField>
                                            </Fields>
                                        </asp:DetailsView>

                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Label ID="Label21" runat="server" Text="User photo" meta:resourcekey="Label21Resource1"></asp:Label></h3>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <%--  <%  if (Session["Speciality"].ToString() == "Auto")
                                              { %>--%>

                                        <div class="col-md-6">
                                         <%--   <%}
    else
    { %>
                                             <div class="col-md-6 pull-right">

                                            <%} %>--%>
                                            <div class="form-group">
                                                <asp:Image ID="ImgUserPhoto" runat="server" Height="50%" Width="50%" CssClass="img-responsive" meta:resourcekey="ImgUserPhotoResource2" />
                                            </div>
                                        </div>



                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" meta:resourcekey="FileUpload1Resource2" />
                                            </div>

                                         <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
                                                    </asp:Timer>

                                                   


                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                             <div class="form-group">
                                                <div class="pull-right">
                                                    <asp:Button ID="BtnUpdateFoto" CssClass="btn btn-sm btn-success" runat="server" Text="Update photo" OnClick="BtnUpdateFoto_Click" meta:resourcekey="BtnUpdateFotoResource2" />&nbsp;
                                         <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-default" OnClick="BtnCancel_Click" meta:resourcekey="BtnCancelResource2" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>




                        </div>
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Label ID="Label22" runat="server" Text="Change password" meta:resourcekey="Label22Resource1"></asp:Label></h3>
                                </div>
                                <div class="box-body">
                                    <div class="form-group">
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequiredValidator2" runat="server" ErrorMessage="Please fill this field" ValidationGroup="b" ControlToValidate="TxtCurrentPassword" ForeColor="Red" meta:resourcekey="RequiredValidator2Resource2"></asp:RequiredFieldValidator></label>

                                        <asp:TextBox ID="TxtCurrentPassword" runat="server" placeholder="Enter current password" CssClass="form-control" ValidationGroup="b" TextMode="Password" meta:resourcekey="TxtCurrentPasswordResource2"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequiredValidator3" runat="server" ErrorMessage="Please fill this field" ValidationGroup="b" ControlToValidate="TxtNewPassword" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredValidator3Resource2"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="* Minimum 6 characters required" ControlToValidate="TxtNewPassword" ForeColor="Red" ValidationGroup="b" Display="Dynamic" ValidationExpression="^[\s\S]{6,}$" meta:resourcekey="RegularExpressionValidator1Resource2"></asp:RegularExpressionValidator>
                                        </label>
                                        <asp:TextBox ID="TxtNewPassword" runat="server" placeholder="Enter new password" CssClass="form-control" ValidationGroup="b" TextMode="Password" meta:resourcekey="TxtNewPasswordResource2"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequiredValidator1" runat="server" ValidationGroup="b" ErrorMessage="Please fill this field" ControlToValidate="TxtConfirmNewPassword" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredValidator1Resource2"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password mismatched" ValidationGroup="b" ControlToValidate="TxtConfirmNewPassword" ControlToCompare="TxtNewPassword" ForeColor="Red" Display="Dynamic" meta:resourcekey="CompareValidator1Resource2"></asp:CompareValidator></label>
                                        <asp:TextBox ID="TxtConfirmNewPassword" runat="server" placeholder="Enter confirm password" CssClass="form-control" ValidationGroup="b" TextMode="Password" meta:resourcekey="TxtConfirmNewPasswordResource2"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="BtnChangePassword" runat="server" Text="Change password" CssClass="btn btn-success pull-right" ValidationGroup="b" OnClick="BtnChangePassword_Click" meta:resourcekey="BtnChangePasswordResource2" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                     <%-- <%  if (Session["Speciality"].ToString() == "Auto")
                                              { %>--%>

                                        <div class="col-md-6">
                                          <%--  <%}
    else
    { %>
                                             <div class="col-md-6 pull-right">

                                            <%} %>--%>






                        <div class="row">

                       







                            <div class="col-md-12">
                                <div class="box box-primary  title-top-history" >
                                    <div class="box-header">
                                        <h3 class="box-title">
                                            <asp:Label ID="Label23" runat="server" Text="Your shared history" meta:resourcekey="Label23Resource1"></asp:Label></h3>
                                        <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                             { %>--%>
                                        <div id="drop" class="pull-right">
                                          <%--  <%}
    else
    {%> <div id="drop" class="pull-left">
                                            <%} %>--%>
                                            <asp:DropDownList ID="DropDownList1" CssClass="btn-xs" runat="server" meta:resourcekey="DropDownList1Resource2"></asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList2" CssClass="btn-xs" runat="server" meta:resourcekey="DropDownList2Resource2"></asp:DropDownList>
                                            <%-- <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="9" meta:resourcekey="GridView1Resource2">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Doctor name" meta:resourcekey="TemplateFieldResource24">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Visible="False" Text='<%# Bind("share_d_id") %>' meta:resourcekey="Label7Resource2"></asp:Label>
                                                                <asp:Label ID="Label11" runat="server" Text="Label" meta:resourcekey="Label11Resource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hospital" meta:resourcekey="TemplateFieldResource25">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label8" runat="server" Text="Label" meta:resourcekey="Label8Resource2"></asp:Label>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("share_h_id") %>' meta:resourcekey="Label12Resource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource26">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label9Resource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource27">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("a_time") %>' meta:resourcekey="Label10Resource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="select" meta:resourcekey="TemplateFieldResource28">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox2" runat="server" meta:resourcekey="CheckBox2Resource2" />
                                                                <asp:Label ID="Label16" Visible="False" runat="server" Text='<%# Bind("id") %>' meta:resourcekey="Label16Resource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#F0F0F0" />
                                                    <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                      <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                            { %>--%>
                                          <div class="box-footer" dir="ltr">
                                       <%-- <%}
                                        else
                                        { %>
                                          <div class="box-footer" dir="ltr"> 
                                      
                                        <%} %>--%>
                                
                                        <asp:LinkButton ID="sharehistory" runat="server" OnClick="sharehistory_Click" meta:resourcekey="sharehistoryResource2">Share</asp:LinkButton>
                                    
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="box box-primary" style="margin-top: 1%">
                                    <div class="box-header">
                                        <h3 class="box-title">
                                            <asp:Label ID="Label24" runat="server" Text="Your shared reports" meta:resourcekey="Label24Resource1"></asp:Label></h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView2" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="9" meta:resourcekey="GridView2Resource2" OnRowCommand="GridView2_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Doctor name" meta:resourcekey="TemplateFieldResource29">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label17" runat="server" Text="Label" meta:resourcekey="Label17Resource2"></asp:Label>
                                                                <asp:Label ID="Label13" runat="server" Visible="False" Text='<%# Bind("share_d_id") %>' meta:resourcekey="Label13Resource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hospital name" meta:resourcekey="TemplateFieldResource30">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label18" runat="server" Text="Label" meta:resourcekey="Label18Resource2"></asp:Label>
                                                                <asp:Label ID="Label14" runat="server" Visible="False" Text='<%# Bind("share_h_id") %>' meta:resourcekey="Label14Resource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Uploaded date" meta:resourcekey="TemplateFieldResource31">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label15" runat="server" Text='<%# Bind("date") %>' meta:resourcekey="Label15Resource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reports" meta:resourcekey="TemplateFieldResource32">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" meta:resourcekey="LinkButton1Resource2" CommandArgument='<%# Bind("id") %>' Text="View" OnClick="DownloadFile"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#F0F0F0" />
                                                    <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <!--//model popup for alert-->
            <div>
             
            </div>
            <div>


            </div>
        </div>
    </section>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />


      <script src="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/js/jquery.countrySelector.js"></script>
    <script>$('.tokenize-demo').tokenize2();</script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $("#DdlNationality").countrySelector({ value: 'FRA' });
        $('.multipleInputDynamic').fastselect();
        //});


    </script>
  
</asp:Content>

