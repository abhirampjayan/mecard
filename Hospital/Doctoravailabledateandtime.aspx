<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Doctoravailabledateandtime.aspx.cs" Inherits="Hospital_Doctor_available_date_and_time" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
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

        #a1:hover{
            color:#18bc9c;
       
         }
       #BtnTakeAppointment{
           background-color:#4aa9af;
           color:white;
       }
        #BtnTakeAppointment:hover{
           background-color:lightgrey;
           color:#4aa9af;
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
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../js/jquery.slimscroll.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '500px'

            });
        });
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
        <div style="margin-top: 2%">
            <%--  <% if (Session["Language"].ToString() == "Auto")
                  {%>--%>
            <div class="col-md-7">
               <%-- <%}
    else
    { %>
                <div class="col-md-7 pull-right">
                <%} %>--%>
                 <div class="col-md-12 col-lg-12">
                    <div class="box box-primary box-solid">

                        <div class="box-header">
                           <%-- <% if (Session["Language"].ToString() == "Auto")
                                {%>--%>
                            <div>
                                <%--<%}
    else
    { %>
                                <div class="pull-right">
                                    <%} %>--%>
                            <asp:Image ID="Image1" CssClass="direct-chat-img img-responsive" AlternateText="Image" runat="server" meta:resourcekey="Image1Resource1" />
                                </div>
                                <%-- <% if (Session["Language"].ToString() == "Auto")
                                     {%>--%>
                            <h3 class="box-title pull-right">
                               <%-- <%}
    else
    { %>
                                 <h3 class="box-title pull-left">
                                <%} %>--%>
                                <asp:Label ID="Label3" runat="server" Text="Dr." meta:resourcekey="Label3Resource1"></asp:Label><asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                            <asp:Label ID="LblDoctorId" runat="server" Text="Label" Visible="False" meta:resourcekey="LblDoctorIdResource1"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please choose a date" ForeColor="Red" ValidationGroup="aa" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="input-group">         
                                    <asp:TextBox ID="TextBox1" ValidationGroup="aa" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="Button1" ValidationGroup="aa" CssClass="btn btn-primary" runat="server" Text="Check availability" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
                                    </span>
                                </div>

                            </div>
                            <div class="form-group">

                            </div>
                            <div class="form-group">
                                <div class="box-header">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Doctor Available Times" meta:resourcekey="Label7Resource1"></asp:Label></label> 
                                </div>
                                <div class="box-body">
                                    <asp:RadioButtonList ID="RdbAvlTimes" runat="server"  AutoPostBack="True" RepeatColumns="10" RepeatDirection="Horizontal" OnSelectedIndexChanged="RdbAvlTimes_SelectedIndexChanged" meta:resourcekey="RdbAvlTimesResource1"></asp:RadioButtonList>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="col-md-12 col-lg-12">
                <div class="box box-primary box-solid">
                    <div class="form-group">
                    <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label ID="Label9" runat="server" Text="Other Available Dates and Times" meta:resourcekey="Label9Resource1"></asp:Label></h3>
                    </div>

                    <div class="box-body">
                        <div id="Div1">
                            <asp:DataList ID="DataList3" runat="server" CssClass="table" RepeatColumns="2" RepeatDirection="Horizontal" meta:resourcekey="DataList3Resource1">
                                <ItemTemplate>
                                    <div class="box box-primary box-solid ">
                                        <div class="box-header with-border">
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("date") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                            <asp:Label ID="Label4" Visible="False" runat="server" Text='<%# Bind("hd_id") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                            &nbsp; Dr.<asp:Label ID="Label5" runat="server" Text="Label" meta:resourcekey="Label5Resource1"></asp:Label>
                                           <%--  <% if (Session["Language"].ToString() == "Auto")
                                                 {%>--%>
                                             <div class="box-tools pull-right">
                                               <%--  <%}
    else
    { %>
                                                  <div class="pull-left">
                                                 <%} %>--%>
                                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                            </div>
                                        </div>
                                        <div class="box-body ">
                                          
                                            <asp:DataList ID="DataList4" runat="server" RepeatColumns="3" OnItemCommand="DataList4_ItemCommand" meta:resourcekey="DataList4Resource1">

                                                <ItemTemplate>

                                                    <div class="form-group">
                                                        <asp:Button ID="Button2" CommandName="Appointment" CssClass="btn btn-sm btn-default" runat="server" Text='<%# Bind("time") %>' CommandArgument='<%# Eval("date") %>' meta:resourcekey="Button2Resource1"/>
                                                        &nbsp;
                                                    </div>

                                                </ItemTemplate>
                                            </asp:DataList>
                                          
                                        </div>

                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            </div>
               


            <div class="col-md-5">
                 <div class="col-md-12 col-lg-12">
                    <div class="box box-default box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                <asp:Label ID="Label10" runat="server" Text="Set an Appointment" meta:resourcekey="Label10Resource1"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <%--<asp:DataList ID="DataList1" CssClass="table" runat="server" RepeatColumns="5"></asp:DataList>--%>

                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label11" runat="server" Text="Date" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtApntmtDate" ValidationGroup="bb" CssClass="form-control" runat="server" ReadOnly="True" placeholder="Appointment date" meta:resourcekey="TxtApntmtDateResource1"></asp:TextBox>
                                    <%--<span class="input-group-btn">
                                        <asp:Button ID="Button2" ValidationGroup="bb" CssClass="btn btn-success" runat="server" Text="Check availability" OnClick="Button1_Click" />
                                    </span>--%>
                                </div>
                                <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label12" runat="server" Text="Appointment time" meta:resourcekey="Label12Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtApointmentTime" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Appointment Time" meta:resourcekey="TxtApointmentTimeResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label13" runat="server" Text="Reason to visit" meta:resourcekey="Label13Resource1"></asp:Label></label>
                                    <%--<asp:TextBox ID="TxtReasonToVisit" runat="server" CssClass="form-control" Enabled="False" placeholder=" Reason"></asp:TextBox>--%>
                                    <asp:DropDownList ID="TxtReasonToVisit" runat="server" CssClass="form-control" Enabled="False" meta:resourcekey="TxtReasonToVisitResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource1">General</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource2">Illness</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label14" runat="server" Text="Payment option" meta:resourcekey="Label14Resource1"></asp:Label></label>
                                    <asp:DropDownList ID="DdlPayments" runat="server" CssClass="form-control" Enabled="False" meta:resourcekey="DdlPaymentsResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource3">Payment my self</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label15" runat="server" Text="Hakkeem Id" meta:resourcekey="Label15Resource1"></asp:Label></label>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBookDocUserId" ForeColor="Red" ErrorMessage="Please Fill this field" ValidationGroup="b" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtBookDocUserId" runat="server" CssClass="form-control" Enabled="False" placeholder="Hakkeem Id" meta:resourcekey="TxtBookDocUserIdResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="BtnTakeAppointment" runat="server" Text="Take Appointment" ValidationGroup="b" CssClass="btn btn-primary pull-right"  Enabled="False" OnClick="BtnTakeAppointment_Click" meta:resourcekey="BtnTakeAppointmentResource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
               


            
           
        </div>

            <!--//model popup for alert-->


    </div>
</asp:Content>

