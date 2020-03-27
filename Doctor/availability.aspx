<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="availability.aspx.cs" Inherits="Doctor_Doctoravailability" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

   <%-- <script type="text/javascript">
        function getConfirmation(sender, title, message) {
            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>--%>
    <%--<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>--%>
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
    <div class="container">


        <div id="modalPopUp" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">
                            <span id="spnTitle"></span>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <span id="spnMsg"></span>.
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="btnConfirm" class="btn btn-primary">
                            Yes, please</button>
                    </div>
                </div>
            </div>
        </div>
       <%--  <%if (Session["Language"].ToString() == "Auto")
             { %>--%>
         <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
           <%--  <%}
    else
    { %>
              <div class="modal fade" id="myModal" dir="rtl" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

             <%} %>--%>
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                      <%-- <%if (Session["Language"].ToString() == "Auto")
                                           { %>--%>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <%--<%}
    else
    { %>
                                    <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <%} %>--%>
                                    <h4 class="modal-title">
                                       
                                        <asp:Label ID="Label4" runat="server" Text="Make an appointment" meta:resourcekey="Label4Resource1"></asp:Label>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Date" meta:resourcekey="Label5Resource1"></asp:Label>
                                    <asp:TextBox ID="TxtApntmtDate" ValidationGroup="aaa" CssClass="form-control" runat="server" ReadOnly="True" placeholder="Appointment date" meta:resourcekey="TxtApntmtDateResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" Text="Appointment time" meta:resourcekey="Label10Resource1"></asp:Label>
                                    <asp:TextBox ID="TxtApointmentTime" ValidationGroup="aaa" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Appointment Time" meta:resourcekey="TxtApointmentTimeResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" Text="Reason to visit" meta:resourcekey="Label11Resource1"></asp:Label>
                                    <asp:DropDownList ID="TxtReasonToVisit" ValidationGroup="aaa" runat="server" CssClass="form-control" meta:resourcekey="TxtReasonToVisitResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource1">General</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource2">Illness</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" Text="Payment option" meta:resourcekey="Label12Resource1"></asp:Label>
                                    <asp:DropDownList ID="DdlPayments" ValidationGroup="aaa" runat="server" CssClass="form-control" meta:resourcekey="DdlPaymentsResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource3">Payment my self</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" Text="User Hakkeem Id" ></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBookDocUserId" ForeColor="Red" ErrorMessage="Please Fill this field" ValidationGroup="aaa" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtBookDocUserId" runat="server" CssClass="form-control" ValidationGroup="aaa" placeholder="User Hakkeem Id" OnTextChanged="TxtBookDocUserId_TextChanged"></asp:TextBox>
                                </div>
                                </div>
                                <div class="modal-footer">
                                     <%-- <%if (Session["Language"].ToString() == "Auto")
                                             { %>--%>
                                    <div>
                                        <%-- <%}
    else
    { %>  <div class="pull-left">

                                        <%} %>--%>
                                    <asp:Button ID="BtnTakeAppointment" runat="server" Text="Take Appointment" ValidationGroup="aaa" CssClass="btn btn-primary" OnClick="BtnTakeAppointment_Click" meta:resourcekey="BtnTakeAppointmentResource1" UseSubmitBehavior="False" />
                              
                                    </div>
                                    </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnTakeAppointment" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>




        <div class="row">
            <div class="col-md-11">
                <%-- <%if (Session["Language"].ToString() == "Auto")
                     { %>--%>
                <div class="box box-primary">
                   <%-- <%}
    else
    { %>
                    <div class="box box-primary" dir="rtl">
                    <%} %>--%>
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Label ID="Label14" runat="server" Text="Availability" meta:resourcekey="Label14Resource1"></asp:Label></h3>
                         <%-- <%if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <div class="pull-right"><%--<%}
    else
    { %> <div class="pull-left">
                            <%} %>--%>
                            <asp:TextBox ID="TextBox1" placeholder="Search by date" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" AutoPostBack="True" runat="server" OnTextChanged="TextBox1_TextChanged" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form-group">

                            <div class="table-responsive">


                                <asp:DataList ID="DataList2" CssClass="table" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" meta:resourcekey="DataList2Resource1">
                                    <ItemTemplate>
                                        <div class="box box-default box-solid">
                                            <div class="box-header with-border">
                                                <%--   <%if (Session["Language"].ToString() == "Auto")
                                                       { %>--%>
                                                <h3 class="box-title">
                                                  <%--  <%}
    else
    { %> <h3 class="box-title" dir="rtl">

                                                    <%} %>--%>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                                    </h3>
                                                      <%--  <%if (Session["Language"].ToString() == "Auto")
                                                            { %>--%>
                                                <div class="box-tools pull-right">
                                                <%--    <%}
    else
    { %> <div class="pull-left" style="left: 1%;position:absolute;top:5px;">

                                                    <%} %>--%>
                                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <asp:DataList ID="DataList3" runat="server" RepeatColumns="4" OnItemCommand="DataList3_ItemCommand" meta:resourcekey="DataList3Resource1">

                                                    <ItemTemplate>

                                                        <div class="form-group">
                                                            <asp:Label ID="Label7" runat="server" Visible="False" Text='<%# Bind("date") %>' meta:resourcekey="Label7Resource1"></asp:Label>
                                                            <asp:Label ID="Label8" Visible="False" runat="server" Text='<%# Bind("time") %>' meta:resourcekey="Label8Resource1"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="doc" CssClass="btn btn-sm btn-primary" Text='<%# Bind("time") %>' CommandArgument='<%# Bind("email") %>' meta:resourcekey="LinkButton2Resource1"></asp:LinkButton>
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

       <%-- <div>
            <asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg" runat="server" DefaultButton="BtnTakeAppointment" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">
                                <asp:Label ID="Label4" runat="server" Text="Make an appointment" meta:resourcekey="Label4Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">

                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Date" meta:resourcekey="Label5Resource1"></asp:Label>
                                    <asp:TextBox ID="TxtApntmtDate" ValidationGroup="aaa" CssClass="form-control" runat="server" ReadOnly="True" placeholder="Appointment date" meta:resourcekey="TxtApntmtDateResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" Text="Appointment time" meta:resourcekey="Label10Resource1"></asp:Label>
                                    <asp:TextBox ID="TxtApointmentTime" ValidationGroup="aaa" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Appointment Time" meta:resourcekey="TxtApointmentTimeResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" Text="Reason to visit" meta:resourcekey="Label11Resource1"></asp:Label>
                                    <asp:DropDownList ID="TxtReasonToVisit" ValidationGroup="aaa" runat="server" CssClass="form-control" meta:resourcekey="TxtReasonToVisitResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource1">General</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource2">Illness</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" Text="Payment option" meta:resourcekey="Label12Resource1"></asp:Label>
                                    <asp:DropDownList ID="DdlPayments" ValidationGroup="aaa" runat="server" CssClass="form-control" meta:resourcekey="DdlPaymentsResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource3">Payment my self</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" Text="Hakkeem user id" meta:resourcekey="Label13Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBookDocUserId" ForeColor="Red" ErrorMessage="Please Fill this field" ValidationGroup="aaa" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtBookDocUserId" runat="server" CssClass="form-control" ValidationGroup="aaa" placeholder="Hakkeem user id" OnTextChanged="TxtBookDocUserId_TextChanged" meta:resourcekey="TxtBookDocUserIdResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="BtnTakeAppointment" runat="server" Text="Take Appointment" ValidationGroup="aaa" CssClass="btn btn-primary pull-right" OnClick="BtnTakeAppointment_Click" meta:resourcekey="BtnTakeAppointmentResource1" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </asp:Panel>
        </div>--%>

        <div>

            <asp:Button ID="btnForAjax2" runat="server" Style="display: none" meta:resourcekey="btnForAjax2Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg2"
                TargetControlID="btnForAjax2" CancelControlID="btnclose2" BehaviorID="ModalPopupExtender3" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg2" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg2Resource1" Visible="false">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform: capitalize">
                                <asp:Label ID="Label3" runat="server" Text="Hakkeem" meta:resourcekey="Label3Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose2" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label>

                            </div>
                            <div class="form-group">
                                <span style="margin-left: 45%">
                                    <asp:Button ID="BtnError" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" ValidationGroup="cc" OnClick="BtnError_Click" meta:resourcekey="BtnErrorResource1" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>


        </div>
        <asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
            TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1" Visible="false">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label9" runat="server" Text="Hakkeem" meta:resourcekey="Label9Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" ValidationGroup="cc" meta:resourcekey="Button2Resource2" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>

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
            width: 500px;
            height: 300px;
        }
    </style>
    <script src="../js/app.min.js"></script>
</asp:Content>

