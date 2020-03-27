<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Doctor_Reports" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '400px'

            });
        });
    </script>
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
    <link href="../css/datepicker3.css" rel="stylesheet" />
    <script src="../css/bootstrap-datepicker.js"></script>
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
    <div class="container-fluid"><%--<%}
    else
    { %>
        <div class="container-fluid" dir="rtl">
        <%} %>--%>
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="Label7" runat="server" Text="History" meta:resourcekey="Label7Resource1"></asp:Label></h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="form-group">

                            <%-- <div class="col-md-5 col-sm-5">
                            <asp:RadioButtonList ID="rdpSelect" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" CssClass="table" RepeatLayout="Flow" OnSelectedIndexChanged="rdpSelect_SelectedIndexChanged">
                                <asp:ListItem>Date</asp:ListItem>
                                <asp:ListItem>BookDoc Id</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                                <%--  <%if (Session["Language"].ToString() == "Auto")
                                      { %>--%>
                                <div class="col-lg-3 col-md-3 ">
                                  <%--  <%}
    else
    { %>
                                    <div class="col-lg-3 col-md-3 pull-right">
                                        <%} %>--%>
                                    <label>
                                        <asp:Label ID="Label8" runat="server" Text="From Date" meta:resourcekey="Label8Resource1"></asp:Label></label><asp:Label ID="LblValidFrom" runat="server" Text="Please select a date" Visible="False" ForeColor="Red" meta:resourcekey="LblValidFromResource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please select a date" ControlToValidate="TxtFrom" ValidationGroup="s" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtFrom" runat="server" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" ValidationGroup="a" AutoPostBack="True" OnTextChanged="TxtFrom_TextChanged" meta:resourcekey="TxtFromResource1"></asp:TextBox>
                                </div>
                                    <%--  <%if (Session["Language"].ToString() == "Auto")
                                          { %>--%>
                                <div class="col-lg-3 col-md-3 ">
                                  <%--  <%}
    else
    { %>
                                    <div class="col-lg-3 col-md-3 pull-right">
                                    <%} %>--%>
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="To Date" meta:resourcekey="Label9Resource1"></asp:Label></label><asp:Label ID="LblValidTo" runat="server" Text="* Please select a date" Visible="False" ForeColor="Red" meta:resourcekey="LblValidToResource1"></asp:Label>
                                    <asp:TextBox ID="TxtTo" runat="server" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" ValidationGroup="a" AutoPostBack="True" OnTextChanged="TxtTo_TextChanged" Enabled="False" meta:resourcekey="TxtToResource1"></asp:TextBox>
                                </div>
                                    <%--  <%if (Session["Language"].ToString() == "Auto")
                                          { %>--%>
                                <div class="col-lg-2 col-md-2 "><%--<%}
    else
    { %>
                                    <div class="col-lg-2 col-md-2 pull-right">
                                    <%} %>--%>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Diagnosis" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                    <asp:DropDownList ID="DdlDiagnose" runat="server" CssClass="form-control" meta:resourcekey="DdlDiagnoseResource1"></asp:DropDownList>
                                </div>
                                     <%--<%if (Session["Language"].ToString() == "Auto")
                                         { %>--%>
                                <div class="col-lg-2 col-md-2 ">
                                   <%-- <%}
    else
    { %>
                                    <div class="col-lg-2 col-md-2 pull-right">
                                        <%} %>--%>
                                    <div style="margin-top: 28px;">
                                        <asp:Button ID="BtnViewReport" runat="server" Text="View/Download" CssClass="btn btn-sm btn-primary" ValidationGroup="a" OnClick="BtnViewReport_Click" meta:resourcekey="BtnViewReportResource1" />
                                    </div>
                                </div>
                                   <%--  <%if (Session["Language"].ToString() == "Auto")
                                         { %>--%>
                                <div class="col-lg-2 col-md-2"><%--<%}
    else
    { %>
                                    <div class="col-lg-2 col-md-2 pull-left"><%} %>--%>

                                    <div style="margin-top: 27px;">
                                        <div class="form-group">
                                        <asp:Button ID="BtnMostUser" runat="server" Text="Most Patient" CssClass="btn btn-default " ForeColor="#4AA9AF" ToolTip="Click to view most visited patients" ValidationGroup="s" Width="102px" CommandName="MostPatient" OnClick="BtnMostUser_Click" meta:resourcekey="BtnMostUserResource1" />
                                            </div>
                                        <div class="form-group">
                                        <asp:Button ID="BtnMostDisease" runat="server" Text="Most Disease" CssClass="btn btn-default " ForeColor="#4AA9AF" ToolTip="Click to view most Disease the patients consulted" ValidationGroup="s" CommandName="MostDisease" OnClick="BtnMostDisease_Click" meta:resourcekey="BtnMostDiseaseResource1" />
                                    </div>
                                        </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <asp:Panel ID="Panel2" runat="server" meta:resourcekey="Panel2Resource1">
                               <%--  <%if (Session["Language"].ToString() == "Auto")
                                     { %>--%>
                                <div class="col-lg-4 col-md-4 "><%--<%}
    else
    { %>
<div class="col-lg-4 col-md-4 pull-right">
                                    <%} %>--%>
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="MediFi Id" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter MediFi Id" ValidationGroup="b" ForeColor="Red" ControlToValidate="TxtBookDocId" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtBookDocId" runat="server" CssClass="form-control" ValidationGroup="b" placeholder="Patient MediFi Id" meta:resourcekey="TxtBookDocIdResource1"></asp:TextBox>
                                </div>
                                      <%--<%if (Session["Language"].ToString() == "Auto")
                                          { %>--%>
                                <div class="col-lg-2 col-md-2">
                                   <%-- <%}
    else
    { %>
                                    <div class="col-lg-2 col-md-2 pull-right">

                                    <%} %>--%>
                                    <div style="margin-top: 28px;">
                                        <asp:Button ID="BtnViewReportId" runat="server" Text="View/Download" CssClass="btn btn-sm btn-primary" ValidationGroup="b" OnClick="BtnViewReportId_Click" meta:resourcekey="BtnViewReportIdResource1" />
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="Panel3" runat="server" Visible="False" meta:resourcekey="Panel3Resource1">
                <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%-- <%if (Session["Language"].ToString() == "Auto")
                                 { %>--%>
                            <h3 class="box-title">
                               <%-- <%}
    else
    { %>
 <h3 class="box-title pull-right">
                                <%} %>--%>
                                <asp:Label ID="Label12" runat="server" Text="Details" meta:resourcekey="Label12Resource1"></asp:Label></h3>
                            <asp:Button ID="BtnPrintDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary pull-right" OnClick="BtnPrintDownload_Click" meta:resourcekey="BtnPrintDownloadResource1" />
                        </div>
                        <div class="box-body">

                            <div class="form-group table-responsive">
                                <div id="Div1">
                                    <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPageIndexChanging="GridView1_PageIndexChanging" meta:resourcekey="GridView1Resource1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource1">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Time" meta:resourcekey="TemplateFieldResource2">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblTimeResource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Time" meta:resourcekey="TemplateFieldResource3">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblEndTime" runat="server" Text='<%# Eval("a_end_time") %>' meta:resourcekey="LblEndTimeResource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Patient Name" meta:resourcekey="TemplateFieldResource4">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPatientName" runat="server" Text='<%# Eval("name") %>' meta:resourcekey="LblPatientNameResource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diagnose" meta:resourcekey="TemplateFieldResource5">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDiagnose" runat="server" Text='<%# Eval("a_doc_daignose") %>' meta:resourcekey="LblDiagnoseResource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prescription" meta:resourcekey="TemplateFieldResource6">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPrescription" runat="server" Text='<%# Eval("a_doc_prescriptions") %>' meta:resourcekey="LblPrescriptionResource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#DDDDDD" Font-Bold="True" ForeColor="#4aa9af" />
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
            </asp:Panel>
        </div>


    </div>
     <script src="../js/app.min.js"></script>
</asp:Content>

