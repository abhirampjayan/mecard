<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="HospitalReports.aspx.cs" Inherits="Hospital_HospitalReports" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

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
    <script src="../js/bootstrap-datepicker.js"></script>
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
   <%-- <% if (Session["Language"].ToString() == "Auto")
        {%>--%>
    <div class="container-fluid">
        <%--<%}
            else
            { %>
        <div class="container-fluid" dir="rtl">
            <%} %>--%>
            <div style="margin-top: 2%">

                <div class="box box-primary">
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
                                <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                                   <%--  <% if (Session["Language"].ToString() == "Auto")
                                         {%>--%>
                                    <div class="col-lg-3 col-md-3 ">
                                       <%-- <%}
    else
    { %>
                                        <div class="col-lg-3 col-md-3 pull-right">
                                        <%} %>--%>
                                        <label>
                                            <asp:Label ID="Label3" runat="server" Text="From Date" meta:resourceKey="Label3Resource1"></asp:Label></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please select a date" ControlToValidate="TxtFrom" ValidationGroup="s" ForeColor="Red" meta:resourceKey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="TxtFrom" runat="server" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" ValidationGroup="a" AutoPostBack="True" OnTextChanged="TxtFrom_TextChanged" meta:resourceKey="TxtFromResource1"></asp:TextBox>
                                    </div>
                                         <%--  <% if (Session["Language"].ToString() == "Auto")
                                               {%>--%>
                                    <div class="col-lg-3 col-md-3 ">
                                        <%--<%}
    else
    { %>
                                         <div class="col-lg-3 col-md-3 pull-right">
                                        <%} %>--%>
                                        <label>
                                            <asp:Label ID="Label4" runat="server" Text="To Date" meta:resourceKey="Label4Resource1"></asp:Label></label>
                                        <asp:TextBox ID="TxtTo" runat="server" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" ValidationGroup="a" AutoPostBack="True" Enabled="False" OnTextChanged="TxtTo_TextChanged" meta:resourceKey="TxtToResource1"></asp:TextBox>
                                    </div>
                                        <%--  <% if (Session["Language"].ToString() == "Auto")
                                              {%>--%>
                                    <div class="col-lg-2 col-md-2 ">
                                       <%-- <%}
    else
    { %>
                                        <div class="col-lg-2 col-md-2 pull-right">
                                        <%} %>--%>
                                        <label>
                                            <asp:Label ID="Label5" runat="server" Text="Diagnosis" meta:resourceKey="Label5Resource1"></asp:Label></label>
                                        <asp:DropDownList ID="DdlDiagnose" runat="server" CssClass="form-control" meta:resourceKey="DdlDiagnoseResource1" AutoPostBack="True" OnSelectedIndexChanged="DdlDiagnose_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                        <%-- <% if (Session["Language"].ToString() == "Auto")
                                             {%>--%>
                                    <div class="col-lg-2 col-md-2 ">
                                       <%-- <%}
    else
    { %> <div class="col-lg-2 col-md-2 pull-right">

                                        <%} %>--%>
                                        <label>
                                            <asp:Label ID="Label6" runat="server" Text="Doctor Name" meta:resourceKey="Label6Resource1"></asp:Label></label>
                                        <asp:DropDownList ID="DdlDoctorName" runat="server" CssClass="form-control" meta:resourceKey="DdlDoctorNameResource1" AutoPostBack="True" OnSelectedIndexChanged="DdlDoctorName_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-lg-2 col-md-2 ">
                                        <div style="margin-top: 28px;">
                                            <asp:Button ID="BtnViewReport" runat="server" Text="View/Download" CssClass="btn btn-sm btn-primary" ValidationGroup="a" OnClick="BtnViewReport_Click" meta:resourceKey="BtnViewReportResource1" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Panel ID="Panel2" runat="server" meta:resourcekey="Panel2Resource1">
                                   <%--  <% if (Session["Language"].ToString() == "Auto")
                                         {%>--%>
                                    <div class="col-lg-4 col-md-4 ">
                                        <%--<%}
    else
    { %> <div class="col-lg-4 col-md-4 pull-right">
                                        <%} %>--%>
                                        <label>
                                            <asp:Label ID="Label7" runat="server" Text="Hakkeem Id" meta:resourceKey="Label7Resource1"></asp:Label></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Please fill this field" ValidationGroup="b" ForeColor="Red" ControlToValidate="TxtBookDocId" meta:resourceKey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="TxtBookDocId" runat="server" CssClass="form-control" ValidationGroup="b" placeholder="Patient Hakkeem Id" meta:resourceKey="TxtBookDocIdResource1"></asp:TextBox>
                                    </div>
                                          <%-- <% if (Session["Language"].ToString() == "Auto")
                                               {%>--%>
                                    <div class="col-lg-2 col-md-2 ">
                                       <%-- <%}
    else
    { %>
                                        <div class="col-lg-2 col-md-2 pull-right">
                                        <%} %>--%>
                                        <div style="margin-top: 28px;">
                                            <asp:Button ID="BtnViewReportId" runat="server" Text="View/Download" CssClass="btn btn-sm btn-primary" ValidationGroup="b" OnClick="BtnViewReportId_Click" meta:resourceKey="BtnViewReportIdResource1" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 ">
                                        <div style="margin-top: 28px;">
                                            <div class="col-md-3">
                                                <asp:Button ID="BtnMostUser" ForeColor="#4AA9AF" runat="server" Text="Most Patient" CssClass="btn btn-default pull-right" ToolTip="Click to view most visited patients" ValidationGroup="s" Width="102px" CommandName="MostPatient" OnClick="BtnMostUser_Click" meta:resourceKey="BtnMostUserResource1" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="BtnMostDisease" ForeColor="#4AA9AF" runat="server" Text="Most Disease" CssClass="btn btn-default pull-right" ToolTip="Click to view disease patients most visited" ValidationGroup="s" Width="102px" CommandName="MostDisease" OnClick="BtnMostDisease_Click" meta:resourceKey="BtnMostDiseaseResource1" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="BtnMostDoctor" ForeColor="#4AA9AF" runat="server" Text="Most Doctor" CssClass="btn btn-default pull-right" ToolTip="Click to view doctor patients most visited" ValidationGroup="s" Width="102px" CommandName="MostDoctor" OnClick="BtnMostDoctor_Click" meta:resourceKey="BtnMostDoctorResource1" />
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="Panel3" runat="server" Visible="False" meta:resourcekey="Panel3Resource1">
                    <div>

                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="Label9" runat="server" Text="Details" meta:resourceKey="Label9Resource1"></asp:Label></h3>
                                <asp:Button ID="BtnPrintDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary pull-right" OnClick="BtnPrintDownload_Click" meta:resourceKey="BtnPrintDownloadResource1" />
                            </div>
                            <div class="box-body">

                                <div class="form-group table-responsive">
                                    <div id="Div1">
                                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowCustomPaging="True" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" meta:resourceKey="GridView1Resource1">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Date" meta:resourceKey="TemplateFieldResource1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourceKey="LblDateResource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Time" meta:resourceKey="TemplateFieldResource2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourceKey="LblTimeResource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Time" meta:resourceKey="TemplateFieldResource3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblEndTime" runat="server" Text='<%# Eval("a_end_time") %>' meta:resourceKey="LblEndTimeResource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doctor Name" meta:resourceKey="TemplateFieldResource4">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDocName" runat="server" Text='<%# Eval("doc_name") %>' meta:resourceKey="LblDocNameResource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Patient Name" meta:resourceKey="TemplateFieldResource5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPatientName" runat="server" Text='<%# Eval("name") %>' meta:resourceKey="LblPatientNameResource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Diagnose" meta:resourceKey="TemplateFieldResource6">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDiagnose" runat="server" Text='<%# Eval("a_doc_daignose") %>' meta:resourceKey="LblDiagnoseResource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prescription" meta:resourceKey="TemplateFieldResource7">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPrescription" runat="server" Text='<%# Eval("a_doc_prescriptions") %>' meta:resourceKey="LblPrescriptionResource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <HeaderStyle BackColor="#DDDDDD" Font-Bold="True" ForeColor="#4AA9AF" />
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
</asp:Content>

