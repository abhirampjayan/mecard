<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="AppointConfirmation.aspx.cs" Inherits="Hospital_AppointConfirmation" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />



    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

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

        #a1:hover{
            color:#18bc9c;
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


    <%-- <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>
<link href="../css/sweetalert.css" rel="stylesheet" />

    <script src="../js/sweetalert.min.js"></script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../Design/plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '300px'

            });
        });
        
       function Myconfirm() {
           var reslt = confirm('Do yo Want to Cancel this Appointment ?');
           if (reslt) {
               return true;
           }
           else {
               return false;
           }
       }
    </script>

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
         <div style="margin-top:2%">
        <div class="row">
             <div class="col-md-12">
                  <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="Label2" runat="server" Text="Search a patient" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="form-group">
                       <%--  <% if (Session["Language"].ToString() == "Auto")
                             {%>--%>
                        <div class="col-lg-3 col-md-3 ">
                           <%-- <%}
    else
    { %>
                            <div class="col-lg-3 col-md-3 pull-right">
                            <%} %>--%>
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Patient's Hakkeem Id or Name" meta:resourcekey="Label3Resource1"></asp:Label></label>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please fill this field" ControlToValidate="TxtSearchPatient" ValidationGroup="a" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox ID="TxtSearchPatient" runat="server" placeholder="Name or Id" CssClass="form-control" ValidationGroup="a" meta:resourcekey="TxtSearchPatientResource1"></asp:TextBox>
                        </div>
  <%--<% if (Session["Language"].ToString() == "Auto")
      {%>--%>
                        <div class="col-lg-3 col-md-3 ">
                          <%--  <%}
    else
    { %>
                             <div class="col-lg-3 col-md-3 pull-right">

                            <%} %>--%>
                             <label>
                                 <asp:Label ID="Label4" runat="server" Text="Doctor Name" meta:resourcekey="Label4Resource1"></asp:Label></label>
                            <asp:TextBox ID="TxtDoctorName" runat="server" placeholder="Doctor name" CssClass="form-control" meta:resourcekey="TxtDoctorNameResource1"></asp:TextBox>
                            </div>
                       <%-- <% if (Session["Language"].ToString() == "Auto")
                            {%>--%>
                        <div class="col-lg-3 col-md-3 ">
                            <%--<%}
    else
    { %>
                            <div class="col-lg-3 col-md-3 pull-right">
                            <%} %>--%>
                             <label>
                                 <asp:Label ID="Label5" runat="server" Text="Appointment Date" meta:resourcekey="Label5Resource1"></asp:Label></label>
                            <asp:TextBox ID="TxtDate" runat="server" CssClass="form-control datepicker" placeholder="dd-mm-yyyy" onkeydown="return false" onpaste="return false" meta:resourcekey="TxtDateResource1" ></asp:TextBox>
                            </div>

                       <%-- <% if (Session["Language"].ToString() == "Auto")
                            {%>--%>
                    <div class="col-lg-3 col-md-3 ">
                       <%-- <%}
    else
    { %>
                        <div class="col-lg-3 col-md-3 pull-left">
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
                      <%--  <% if (Session["Language"].ToString() == "Auto")
                            {%>--%>
                        <h3 class="box-title">
                          <%--  <%}
    else
    { %><h3 class="box-title pull-right">

                            <%} %>--%>
                            <asp:Label ID="Label6" runat="server" Text="Appointments" meta:resourcekey="Label6Resource1"></asp:Label></h3>
                        <%--   <% if (Session["Language"].ToString() == "Auto")
                               {%>--%>
                             <div class="pull-right">
                                <%-- <%}
    else
    { %>
                                   <div class="pull-left">
                                 <%} %>--%>
                        <asp:Button ID="BtnViewAll" runat="server" Text="View all" CssClass="btn btn-sm btn-primary" OnClick="BtnViewAll_Click" meta:resourcekey="BtnViewAllResource1" />
                               </div>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div id="Div1">
                                <div class="table-responsive">
                            <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server"  AutoGenerateColumns="False" DataKeyNames="id" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" meta:resourcekey="GridView1Resource1" OnRowCommand="GridView1_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Bind("id") %>' Text='<%# Bind("a_date") %>' Enabled="False" meta:resourcekey="LinkButton2Resource1"></asp:LinkButton>
                                            <asp:Label ID="LblDocId" Visible="False" runat="server" Text='<%# Eval("d_id") %>' meta:resourcekey="LblDocIdResource1"></asp:Label>
                                            <asp:Label ID="LblUserId" runat="server" Visible="False" Text='<%# Eval("u_id") %>' meta:resourcekey="LblUserIdResource1"></asp:Label>
                                            <asp:Label ID="LblStatus" runat="server" Visible="False"  Text='<%# Eval("a_status") %>' meta:resourcekey="LblStatusResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doctor Name" meta:resourcekey="TemplateFieldResource2">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDocName" runat="server" meta:resourcekey="LblDocNameResource1"></asp:Label>
                                       
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Patient Name" meta:resourcekey="TemplateFieldResource3">
                                        <ItemTemplate>
                                            <asp:Label ID="LblPatientName" runat="server" Text='<%# Eval("name") %>' meta:resourcekey="LblPatientNameResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Hakkeem ID" meta:resourcekey="TemplateFieldResource4">
                                        <ItemTemplate>
                                            <asp:Label ID="LblH" runat="server" Text='<%# Eval("u_hakkimid") %>' meta:resourcekey="LblHResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource5">
                                        <ItemTemplate>
                                            <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblTimeResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reason" meta:resourcekey="TemplateFieldResource6">
                                         <ItemTemplate>
                                            <asp:Label ID="LblReason" runat="server" Text='<%# Eval("a_reason") %>' meta:resourcekey="LblReasonResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource7">
                                       
                                        <ItemTemplate>
                                            <div dir="ltr">
                                            <asp:LinkButton ID="LnkConfirm" runat="server" CommandName="update" CommandArgument='<%# Bind("id") %>' meta:resourcekey="LnkConfirmResource1"></asp:LinkButton>
                                            &nbsp;or

                                            <asp:LinkButton ID="LnkCancel" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="apmtcancel" meta:resourcekey="LnkCancelResource1">Cancel</asp:LinkButton>
                                                </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                    </div>
                                </div>
                        </div>

                    </div>
                </div>
            </div>
        
        </div>
             </div>

         
<%--  <% if (Session["Language"].ToString() == "Auto")
      {%>--%>
          <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
                     <link href="../css/AdminLTE.min.css" rel="stylesheet" />
                  <%--  <%}
    else
    { %>
                      <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
                    <link href="arabicdesign/dist/css/AdminLTE.min.css" rel="stylesheet" />

                    <%} %>--%>
          <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                   <%--  <% if (Session["Language"].ToString() == "Auto")
                                         {%>--%>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                   <%-- <%}
    else
    { %>
                                    <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>

                                    <%} %>--%>
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text="Confirmation" meta:resourcekey="lblModalTitleResource1"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="lblModalBody" runat="server" Text="Do you want cancel this appointment..?" meta:resourcekey="lblModalBodyResource1"></asp:Label>
                                  
                                  
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button4" OnClientClick="this.value = 'Running Process...'" runat="server" CssClass="btn btn-primary pull-right" Text="Confirm" UseSubmitBehavior="False" OnClick="Button4_Click" meta:resourcekey="Button4Resource1" />
                                   
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button4" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>







    </div>
</asp:Content>

