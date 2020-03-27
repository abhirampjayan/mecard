<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" CodeFile="UploadTestReports.aspx.cs" Culture="en-US" UICulture="en-US" Inherits="User_UploadTestReports" meta:resourcekey="PageResource2" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </style>
      <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>

    <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <section class="content" style="margin-top:1.5cm;margin-bottom:1cm;">
    <div class="container-fluid">
        <div class="row">

            <div class="col-lg-6 col-md-6">

                <div class="box box-primary" style="margin-top: 1%">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="Label1" runat="server" Text="Submit Your Test Records" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Blood test report" meta:resourcekey="Label5Resource1"></asp:Label></label>
                                <asp:FileUpload ID="FupBloodTest" runat="server" CssClass="form-control" accept="image/png, image/jpeg, .pdf" meta:resourcekey="FupBloodTestResource2"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text="Urine test report" meta:resourcekey="Label6Resource1"></asp:Label></label>
                                <asp:FileUpload ID="FupUrineTest" runat="server" CssClass="form-control" meta:resourcekey="FupUrineTestResource2" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="Label7" runat="server" Text="Scan report" meta:resourcekey="Label7Resource1"></asp:Label></label>
                                <asp:FileUpload ID="FupScanRep" runat="server" CssClass="form-control" meta:resourcekey="FupScanRepResource2" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="X-ray report" meta:resourcekey="Label8Resource1"></asp:Label></label>
                                <asp:FileUpload ID="FupXrayRep" runat="server" CssClass="form-control" meta:resourcekey="FupXrayRepResource2" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Other" meta:resourcekey="Label9Resource1"></asp:Label></label>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:TextBox ID="TxtOtherFileName" runat="server" CssClass="form-control" placeholder="Enter test name" meta:resourcekey="TxtOtherFileNameResource2"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:FileUpload ID="FupOtherReport" runat="server" CssClass="form-control" meta:resourcekey="FupOtherReportResource2" />
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 5%">
                            <div class="col-md-6">
                                <label></label>
                                <asp:Button ID="BtnUpload" runat="server" Text="Submit" CssClass="btn btn-file btn-success pull-right" OnClick="BtnUpload_Click" meta:resourcekey="BtnUploadResource2" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-md-6">
                <div class="box box-primary" style="margin-top: 1%">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="Label2" runat="server" Text="Uploaded reports" meta:resourcekey="Label2Resource1"></asp:Label>
                        </h3>
                        <%-- <%if (Session["Speciality"].ToString() == "Auto")
                             { %>--%>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                           <%-- <%}
    else
    { %> <div class="box-tools pull-left">
        <button class="btn btn-box-tool pull-left" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <%} %>--%>
                            
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form-group">

                          
                                     <asp:GridView ID="GridView2" CssClass="table table-responsive table-bordered" runat="server" AutoGenerateColumns="False" meta:resourcekey="GridView2Resource2">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource7">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("date") %>' Visible="False" meta:resourcekey="Label3Resource2"></asp:Label>
                                            <asp:Label ID="Label4" runat="server" Text="Label" meta:resourcekey="Label4Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Blood test" meta:resourcekey="TemplateFieldResource8">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="blood" runat="server" CommandArgument='<%# Bind("blood_test_report") %>' OnClick="blood_Click" meta:resourcekey="bloodResource2">Download</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Urine test" meta:resourcekey="TemplateFieldResource9">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="urine" runat="server" CommandArgument='<%# Bind("urine_test_report") %>' OnClick="urine_Click" meta:resourcekey="urineResource2">Download</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scaning reports" meta:resourcekey="TemplateFieldResource10">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="scan" runat="server" CommandArgument='<%# Bind("scan_test_report") %>' OnClick="scan_Click" meta:resourcekey="scanResource2">Download</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="X-Rays reports" meta:resourcekey="TemplateFieldResource11">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="xray" runat="server" CommandArgument='<%# Bind("xray_test_report") %>' OnClick="xray_Click" meta:resourcekey="xrayResource2">Download</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other test reports" meta:resourcekey="TemplateFieldResource12">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Other" runat="server" CommandArgument='<%# Bind("other_test_report") %>' OnClick="Other_Click" meta:resourcekey="OtherResource2">Download</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                          
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <!--//model popup for alert-->

       <%-- <asp:Button ID="btnForAjax2" runat="server" Text="" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg2"
            TargetControlID="btnForAjax2" CancelControlID="btnclose2">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg2" runat="server" CssClass="modalPopup">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">Hakkeem</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose2" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" OnClick="BtnSubmitOTP_Click" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>--%>
        <!--//modal popup-->
    </div>
  </section>
   <%-- <style type="text/css">
        #copy {
            width: 100%;
            padding: 20px 0;
            position: absolute;
            z-index: 1000000;
            color: #fff;
            background: #313131;
            /* margin-top: 6cm; */
            bottom: 0;
        }
    </style>--%>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>

