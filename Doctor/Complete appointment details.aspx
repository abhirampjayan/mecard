<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Complete appointment details.aspx.cs" Inherits="Doctor_Complete_appointment_details" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="//fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet">
    <link href="//fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet">
   <%-- <link rel="stylesheet" href="../css/bootstrap.min.css" />--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
    <%--<link rel="stylesheet" href="../css/AdminLTE.min.css" />--%>

    <%--<link rel="stylesheet" href="../css/_all-skins.min.css" />--%>

    <link href="../css/intlTelInput.css" rel="stylesheet" />
    <!-- //web font -->
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

        #a1:hover {
            color: #4aa9af;
        }
    </style>
  <%--  <link href="../css/datepicker3.css" rel="stylesheet" />
    <link href="../css/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="../js/bootstrap-datepicker.js"></script>--%>
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
   <%-- <%if (Session["Language"].ToString() == "Auto")
        { %>--%>
    <div class="container-fluid">
     <%--   <%}
            else
            { %>
        <div class="container-fluid" dir="rtl">
            <%} %>--%>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="Label2" runat="server" Text="Search an Appointment" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 ">
                                        <asp:Label ID="Label3" runat="server" Text="Date" meta:resourcekey="Label3Resource1"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select a date" ControlToValidate="TxtSearchDate" ValidationGroup="a" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                        <div class="input-group">


                                            <asp:TextBox ID="TxtSearchDate" runat="server" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" ValidationGroup="a" meta:resourcekey="TxtSearchDateResource1"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button ID="BtnSearchPatient" runat="server" Text="Search" CssClass="btn btn-primary" ValidationGroup="a" OnClick="BtnSearchPatient_Click" meta:resourcekey="BtnSearchPatientResource1" />

                                            </span>
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
                                <asp:Label ID="Label10" runat="server" Text="Complete appointment details" meta:resourcekey="Label10Resource1"></asp:Label></h3>
                           <%--   <%if (Session["Language"].ToString() == "Auto")
                                  { %>--%>
                            <div class="pull-right">
                              <%--  <%}
    else
    { %>
                                <div class="pull-left">
                                <%} %>--%>
                            <asp:Button ID="btnViewAll" runat="server" Text="View All" CssClass="btn btn-sm btn-primary" OnClick="btnViewAll_Click" meta:resourcekey="btnViewAllResource1" />
                                </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" CssClass="table table-hover table-bordered" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" meta:resourcekey="GridView1Resource1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Patient name" meta:resourcekey="TemplateFieldResource1">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" Visible="False" runat="server" Text='<%# Bind("c_id") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                                    <asp:Label ID="Label5" runat="server" Text="Label" meta:resourcekey="Label5Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Appointment date" meta:resourcekey="TemplateFieldResource2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Appointment time" meta:resourcekey="TemplateFieldResource3">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("a_time") %>' meta:resourcekey="Label7Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource4">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Visible="False" Text='<%# Bind("a_status") %>' meta:resourcekey="Label8Resource1"></asp:Label>
                                                    <asp:Label ID="Label9" runat="server" Text="Label" meta:resourcekey="Label9Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="###">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server">Finish</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#dddddd" Font-Bold="True" ForeColor="#4aa9af" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
        <script src="../js/app.min.js"></script>
</asp:Content>

