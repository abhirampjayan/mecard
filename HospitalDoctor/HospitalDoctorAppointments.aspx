<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalDoctor/HospitalDoctorMaster.master" AutoEventWireup="true" CodeFile="HospitalDoctorAppointments.aspx.cs" Inherits="HospitalDoctor_HospitalDoctorAppointments" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
     <script src="../Design/plugins/datepicker/bootstrap-datepicker.js"></script>
   

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <% if (Session["Language"].ToString() == "Auto")
         { %>--%>
       <div class="container-fluid">
          <%-- <%}
    else
    { %>
            <div class="container-fluid" dir="rtl">
           <%} %>--%>
        <div style="margin-top: 2%">
            <div class="col-md-12">
                  <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="Label2" runat="server" Text="Search an appointment" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="form-group">
                      <%--   <% if (Session["Language"].ToString() == "Auto")
                             { %>--%>
                        <div class="col-lg-4 col-md-4 ">
                          <%--  <%}
    else
    { %>
                            <div class="col-lg-4 col-md-4 pull-right">
                            <%} %>--%>
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Date" meta:resourcekey="Label3Resource1"></asp:Label></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select a date" ControlToValidate="TxtSearchDate" ValidationGroup="a" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="TxtSearchDate" runat="server" CssClass="form-control datepicker" placeholder="dd-mm-yyyy" onkeydown="return false" onpaste="return false" ValidationGroup="a" meta:resourcekey="TxtSearchDateResource1"></asp:TextBox>
                        </div>
                           <%--  <% if (Session["Language"].ToString() == "Auto")
                                 { %>--%>
                    <div class="col-lg-2 col-md-2 ">
                       <%-- <%}
    else
    { %>
                        <div class="col-lg-2 col-md-2 pull-right">
                        <%} %>--%>
                        <div style="margin-top:28px;">
                            <asp:Button ID="BtnSearchPatient" runat="server" Text="Search" CssClass="btn btn-sm btn-primary"  ValidationGroup="a" OnClick="BtnSearchPatient_Click" meta:resourcekey="BtnSearchPatientResource1" />
                            </div>
                    </div>
                    </div>
                </div>
            </div>
                    </div>
            <div class="col-md-12">
               
                    <div class="box box-primary">

                        <div class="box-header">
                          <%--  <asp:Image ID="Image1" CssClass="direct-chat-img img-responsive" AlternateText="Image" runat="server" />
                             <h2 class="box-title pull-right"> <asp:Label ID="LblCurrentDate" runat="server" Text="Label1"></asp:Label></h2>
                            <h3 class="box-title pull-right">Dr.<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>&nbsp; </h3>--%>
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Timer ID="Timer1" runat="server" Interval="1000" ></asp:Timer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="Label4" runat="server" Text="Appointments" meta:resourcekey="Label4Resource1"></asp:Label>
                             <%--<% if (Session["Language"].ToString() == "Auto")
                                 { %>--%>
                            <div>
                                <%--<%}
    else
    { %><div class="pull-left">
                                <%} %>--%>
                            <asp:Button ID="BtnViewAll" runat="server" Text="View all" CssClass="btn btn-sm btn-primary pull-right" OnClick="BtnViewAll_Click" meta:resourcekey="BtnViewAllResource1" />
                                </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group table-responsive">
                                <div id="Div1">
                                <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowCustomPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" meta:resourcekey="GridView1Resource1" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Si.No." meta:resourcekey="TemplateFieldResource1">
                                          <ItemTemplate>
                                             <%#Container.DataItemIndex+1 %>
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource2">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource1"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource3">
                                            <ItemTemplate>
                                                <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblTimeResource1"></asp:Label>
                                                
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Patient Name" meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <asp:Label ID="LblPatientName" runat="server" Text='<%# Eval("name") %>' meta:resourcekey="LblPatientNameResource1"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason" meta:resourcekey="TemplateFieldResource5">
                                            <ItemTemplate>
                                                <asp:Label ID="LblReason" runat="server" Text='<%# Eval("a_reason") %>' meta:resourcekey="LblReasonResource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#dddddd" Font-Bold="True" ForeColor="#18bc9c" />
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

             <div>
              <%--  <asp:Button ID="btnForAjax1" runat="server" Style="display: none" meta:resourcekey="btnForAjax1Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup" Width="40%" meta:resourcekey="pnlPopupMsg1Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label5" runat="server" Text="Hakkeem" meta:resourcekey="Label5Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="BtnOk" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" meta:resourcekey="BtnOkResource1" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
            </div>


    </div>
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
            width: 1000px;
            height: 300px;
        }
    </style>
</asp:Content>

