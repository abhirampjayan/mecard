<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="Doctoravailabledateandtime.aspx.cs" Inherits="Hospital_Doctor_available_date_and_time" %>
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

    </style>
    <link href="../Design/plugins/datepicker/datepicker3.css" rel="stylesheet" />
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
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>--%>
    <script src="../Design/plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '500px'

            });
        });
    </script>
      <section class="content">
    <div class="container-fluid">
        
        <div style="margin-top: 2%">
            <div class="col-md-7">
                 <div class="col-md-12 col-lg-12">
                    <div class="box box-primary box-solid">

                        <div class="box-header">
                            <asp:Image ID="Image1" CssClass="direct-chat-img img-responsive" AlternateText="Image" runat="server" />
                            <h3 class="box-title pull-right">Dr.<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                            <asp:Label ID="LblDoctorId" runat="server" Text="Label" Visible="false"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please choose a date" ForeColor="Red" ValidationGroup="aa"></asp:RequiredFieldValidator>
                                </div>
                                <div class="input-group">         
                                    <asp:TextBox ID="TextBox1" ValidationGroup="aa" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" runat="server"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="Button1" ValidationGroup="aa" CssClass="btn btn-success" runat="server" Text="Check availability" OnClick="Button1_Click" />
                                    </span>
                                </div>

                            </div>
                            <div class="form-group">

                            </div>
                            <div class="form-group">
                                <div class="box-header">
                                    <label>Doctor Available Times</label> 
                                </div>
                                <div class="box-body">
                                    <asp:RadioButtonList ID="RdbAvlTimes" runat="server"  AutoPostBack="True" RepeatColumns="10" RepeatDirection="Horizontal" RepeatLayout="table" OnSelectedIndexChanged="RdbAvlTimes_SelectedIndexChanged"></asp:RadioButtonList>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="col-md-12 col-lg-12">
                <div class="box box-primary box-solid">
                    <div class="form-group">
                    <div class="box-header">
                    <h3 class="box-title">Other Available Dates and Times</h3>
                    </div>

                    <div class="box-body">
                        <div id="Div1">
                            <asp:DataList ID="DataList3" runat="server" CssClass="table" RepeatColumns="2" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <div class="box box-primary box-solid <%--collapsed-box--%>">
                                        <div class="box-header with-border">
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                                            <asp:Label ID="Label4" Visible="false" runat="server" Text='<%# Bind("hd_id") %>'></asp:Label>
                                            &nbsp; Dr.<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                            <div class="box-tools pull-right">
                                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                            </div>
                                        </div>
                                        <div class="box-body ">
                                          
                                            <asp:DataList ID="DataList4" runat="server" RepeatColumns="3" OnItemCommand="DataList4_ItemCommand">

                                                <ItemTemplate>

                                                    <div class="form-group">
                                                        <%-- <asp:Label ID="Label7" runat="server" Visible="false" Text='<%# Bind("date") %>'></asp:Label>
                                                        <asp:Label ID="Label8" Visible="false" runat="server" Text='<%# Bind("time") %>'></asp:Label>--%>
                                                        <asp:Button ID="Button2" CommandName="Appointment" CssClass="btn btn-sm btn-default" runat="server" Text='<%# Bind("time") %>' CommandArgument='<%#Eval("date") %>'/>
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
                            <h3 class="box-title">Set an Appointment</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <%--<asp:DataList ID="DataList1" CssClass="table" runat="server" RepeatColumns="5"></asp:DataList>--%>

                                <div class="form-group">
                                    <label>Date</label>
                                    <asp:TextBox ID="TxtApntmtDate" ValidationGroup="bb" CssClass="form-control" runat="server" ReadOnly="true" placeholder="Appointment date"></asp:TextBox>
                                    <%--<span class="input-group-btn">
                                        <asp:Button ID="Button2" ValidationGroup="bb" CssClass="btn btn-success" runat="server" Text="Check availability" OnClick="Button1_Click" />
                                    </span>--%>
                                </div>
                                <div class="form-group">
                                     <label>Appointment time</label>
                                    <asp:TextBox ID="TxtApointmentTime" runat="server" CssClass="form-control" ReadOnly="true" placeholder="Appointment Time"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                     <label>Reason to visit</label>
                                    <%--<asp:TextBox ID="TxtReasonToVisit" runat="server" CssClass="form-control" Enabled="False" placeholder=" Reason"></asp:TextBox>--%>
                                    <asp:DropDownList ID="TxtReasonToVisit" runat="server" CssClass="form-control" Enabled="false">
                                        <asp:ListItem>General</asp:ListItem>
                                        <asp:ListItem>Illness</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group">
                                     <label>Payment option</label>
                                    <asp:DropDownList ID="DdlPayments" runat="server" CssClass="form-control" Enabled="false">
                                        <asp:ListItem>Payment my self</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                     <label>Hakkeem user id</label>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBookDocUserId" ForeColor="Red" ErrorMessage="Please Fill this field" ValidationGroup="b"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtBookDocUserId" runat="server" CssClass="form-control" Enabled="false" placeholder="Hakkeem Id"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="BtnTakeAppointment" runat="server" Text="Take Appointment" ValidationGroup="b" CssClass="btn btn-success pull-right" Enabled="false" OnClick="BtnTakeAppointment_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
               


            
           
        </div>

            <!--//model popup for alert-->

    <%--    <asp:Button ID="btnForAjax" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs"/></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        <!--//modal popup-->


         <%-- <asp:Button ID="btnForAjax3" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button3" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button1_Click"/></span>
                            </div>
                        </div>
                    </div>

              
            </asp:Panel>--%>


    </div>
          </section>
</asp:Content>

