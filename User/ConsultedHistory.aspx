<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" CodeFile="ConsultedHistory.aspx.cs" Culture="en-US" UICulture="en-US" Inherits="User_ConsultedHistory" meta:resourcekey="PageResource2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
   
   <%-- <script src="../js/jquery-1.3.2.js"></script>
   
   
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
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
        <div>
            <div class="row">
                <%-- <%if (Session["Speciality"].ToString() == "Auto")
                     { %>--%>
                <div class="col-md-12 col-lg-12 col-sm-12">
                   <%-- <%}
    else
    { %>
                    <div class="col-md-12 col-lg-12 col-sm-12" dir="rtl">
                    <%} %>--%>
                    <div class="form-group">
                        <div class="box box-primary" style="margin-top:1%">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="Label1" runat="server" Text="History" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="row">
                                    <div class="form-group">

                                       
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource2">
                                            <div class="col-lg-2 col-md-2 ">
                                                <label>
                                                    <asp:Label ID="Label3" runat="server" Text="From Date" meta:resourcekey="Label3Resource1"></asp:Label></label>
                                                <asp:TextBox ID="TxtFrom" runat="server" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" ValidationGroup="a" AutoPostBack="True" OnTextChanged="TxtFrom_TextChanged" meta:resourcekey="TxtFromResource2"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2 col-md-2 ">
                                                <label>
                                                    <asp:Label ID="Label4" runat="server" Text="To Date" meta:resourcekey="Label4Resource1"></asp:Label></label>
                                                <asp:TextBox ID="TxtTo" runat="server" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" ValidationGroup="a" AutoPostBack="True" Enabled="False" OnTextChanged="TxtTo_TextChanged" meta:resourcekey="TxtToResource2"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3 col-md-3 ">
                                                <label>
                                                    <asp:Label ID="Label5" runat="server" Text="Hospital" meta:resourcekey="Label5Resource1"></asp:Label></label>
                                                <asp:DropDownList ID="DdlHospital" runat="server" CssClass="form-control" meta:resourcekey="DdlHospitalResource2"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3 col-md-3 ">
                                                <label>
                                                    <asp:Label ID="Label6" runat="server" Text="Doctor Name" meta:resourcekey="Label6Resource1"></asp:Label></label>
                                                <asp:DropDownList ID="DdlDoctorName" runat="server" CssClass="form-control" meta:resourcekey="DdlDoctorNameResource2"></asp:DropDownList>
                                            </div>
                                           <%--   <%if (Session["Speciality"].ToString() == "Auto")
                                                  { %>--%>
                                            <div class="col-lg-2 col-md-2 ">
                                              <%--  <%}
    else
    { %>
                                                <div class="col-lg-2 col-md-2 pull-left">

                                                <%} %>--%>
                                                <div style="margin-top: 28px;">
                                                    <asp:Button ID="BtnViewReport" runat="server" Text="View/Download" CssClass="btn btn-sm btn-success" ValidationGroup="a" OnClick="BtnViewReport_Click" meta:resourcekey="BtnViewReportResource2" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel3" runat="server" Visible="False" meta:resourcekey="Panel3Resource2">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Label ID="Label2" runat="server" Text="Details" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                                   <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                         { %>--%>
                                     <div class="pull-right">
                                        <%-- <%}
    else
    { %>
                                          <div class="pull-left"><%} %>--%>
                                    <asp:Button ID="BtnPrintDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-success" OnClick="BtnPrintDownload_Click" meta:resourcekey="BtnPrintDownloadResource2" />
                                </div>
                                    </div>
                                <div class="box-body">
                                    <div class="form-group table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="6" OnPageIndexChanging="GridView2_PageIndexChanging" meta:resourcekey="GridView1Resource2">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource7">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDate" CssClass="pull-left" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource2"></asp:Label>
                                                        <asp:Label ID="LblHosId1" runat="server" Text='<%# Eval("h_id") %>' Visible="False" meta:resourcekey="LblHosId1Resource2"></asp:Label>
                                                        <asp:Label ID="LblDocId" runat="server" Text='<%# Eval("d_id") %>' Visible="False" meta:resourcekey="LblDocIdResource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource8">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTime" runat="server" CssClass="pull-left" Text='<%# Eval("a_time") %>' meta:resourcekey="LblTimeResource2" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hospital" meta:resourcekey="TemplateFieldResource9">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblHospital" CssClass="pull-left" runat="server" Text='<%# Eval("h_name") %>' meta:resourcekey="LblHospitalResource2"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doctor" meta:resourcekey="TemplateFieldResource10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDoctorName" CssClass="pull-left" runat="server" Text='<%# Eval("d_name") %>' meta:resourcekey="LblDoctorNameResource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Diagnosis" meta:resourcekey="TemplateFieldResource11">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDiagnose" CssClass="pull-left" runat="server" Text='<%# Eval("a_doc_daignose") %>' meta:resourcekey="LblDiagnoseResource2"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prescription" meta:resourcekey="TemplateFieldResource12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPrescription" CssClass="pull-left" runat="server" Text='<%# Eval("a_doc_prescriptions") %>' meta:resourcekey="LblPrescriptionResource2"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Width="100%" />
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>

                </div>
            </div>

        </div>
           <!--//model popup for alert-->

       <%-- <asp:Button ID="btnForAjax1" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        <!--//modal popup-->

    </div>
           </section>

      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />

      <link href="../Design/plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="../Design/plugins/datepicker/bootstrap-datepicker.js"></script>
      <script>
        $(function () {

           
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                //format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true,

            });

        });
       
    </script>
</asp:Content>

