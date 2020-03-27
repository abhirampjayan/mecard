<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="DoctorHome.aspx.cs" Inherits="Doctor_DoctorHome" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery.slimscroll.min.js"></script>


    <script type="text/javascript">

        $(function () {
            $('#Div1').slimScroll({
                height: '393px'

            });
        });
    </script>
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
    <script src="../js/jquery.slimscroll.min.js"></script>


    <script type="text/javascript">

        $(function () {
            $('#Div1').slimScroll({
                height: '393px'

            });
        });
    </script>
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

     <style type="text/css">
    .glow {
        -webkit-animation-duration: 1s;
        -webkit-animation-name: glow;
        -webkit-animation-direction: alternate;
        -webkit-animation-iteration-count: infinite;
        animation-duration: 1s;
        animation-name: glow;
        animation-direction: alternate;
        animation-iteration-count: infinite;
    
    }
    
    @-webkit-keyframes glow {
        from { text-shadow: 0 0 5px yellow;
               font-size:1cm;
        }
        to { text-shadow: 0 0 20px yellow; }
    }

  </style>


    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>


    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label16" runat="server" Text="Make an appointment" meta:resourcekey="Label16Resource1"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="Label17" runat="server" Text="Date" meta:resourcekey="Label17Resource1"></asp:Label>
                                <asp:TextBox ID="TxtApntmtDate" ValidationGroup="aaa" CssClass="form-control" runat="server" ReadOnly="True" placeholder="Appointment date" meta:resourcekey="TxtApntmtDateResource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label18" runat="server" Text="Appointment time" meta:resourcekey="Label18Resource1"></asp:Label>
                                <asp:TextBox ID="TxtApointmentTime" ValidationGroup="aaa" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Appointment Time" meta:resourcekey="TxtApointmentTimeResource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label19" runat="server" Text="Reason to visit" meta:resourcekey="Label19Resource1"></asp:Label>
                                <asp:DropDownList ID="TxtReasonToVisit" ValidationGroup="aaa" runat="server" CssClass="form-control" meta:resourcekey="TxtReasonToVisitResource1">
                                    <asp:ListItem meta:resourcekey="ListItemResource1">General</asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource2">Illness</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label20" runat="server" Text="Payment option" meta:resourcekey="Label20Resource1"></asp:Label>
                                <asp:DropDownList ID="DdlPayments" ValidationGroup="aaa" runat="server" CssClass="form-control" meta:resourcekey="DdlPaymentsResource1">
                                    <asp:ListItem meta:resourcekey="ListItemResource3">Payment my self</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label21" runat="server" Text="Hakkeem user id" meta:resourcekey="Label21Resource1"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBookDocUserId" ForeColor="Red" ErrorMessage="Please Fill this field" ValidationGroup="aaa" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="TxtBookDocUserId" runat="server" CssClass="form-control" ValidationGroup="aaa" placeholder="Hakkeem user id" OnTextChanged="TxtBookDocUserId_TextChanged" meta:resourcekey="TxtBookDocUserIdResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnTakeAppointment" runat="server" OnClientClick="this.value = 'Running Process...'" Text="Take Appointment" ValidationGroup="aaa" CssClass="btn btn-primary pull-right" OnClick="BtnTakeAppointment_Click" meta:resourcekey="BtnTakeAppointmentResource1" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnTakeAppointment" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>


    <section class="content">

        <div class="row">
            <div class="col-md-8">
                <!-- Widget: user widget style 1 -->
                <div class="box box-widget widget-user">
                    <!-- Add the bg color to the header using any of the bg-* classes -->
                    <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                        <h3 class="widget-user-username">Dr.<asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                       <h5> <asp:Label ID="Label14" runat="server"></asp:Label></h5>
                        <%-- <h5 class="widget-user-desc">Web Designer</h5>--%>
                    </div>
                    <div class="widget-user-image">
                        <%--<img class="img-circle" src="images/doctor.svg" alt="User Avatar">--%>
                        <asp:Image ID="Image1" CssClass="img-circle" runat="server" meta:resourcekey="Image1Resource1" />
                    </div>
                    <div class="box-footer">
                        <div class="row">
                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header">Qualification</h5>
                                    <span class="description-text" style="color: #4aa9af">
                                        <asp:Label ID="Label4" runat="server" Text="Rating" meta:resourcekey="Label4Resource1"></asp:Label></span>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header">Speciality</h5>
                                    <span class="description-text" style="color: #4aa9af">
                                        <asp:Label ID="Label2" runat="server"></asp:Label></span>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-4">
                                <div class="description-block">
                                    <h5 class="description-header">Languages</h5>
                                    <span class="description-text" style="color: #4aa9af">
                                        <asp:Label ID="Label5" runat="server"></asp:Label></span>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </div>
                </div>
                <!-- /.widget-user -->
            </div>
            <!-- /.col -->

            <div class="col-md-4">

                <div class="info-box bg-yellow">
                    <span class="info-box-icon"><i class="fa fa-calendar"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">Today appointments</span>
                        <span class="info-box-number">
                            <asp:Label ID="Label11" runat="server"></asp:Label></span>
                        <div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="progress-description"><a href="Today appointments.aspx" style="color:white">Today Appointments</a> 
                  </span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->


                <div class="info-box bg-yellow">
                    <span class="info-box-icon"><i class="fa fa-calendar"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">Up-comming appointments</span>
                        <span class="info-box-number">
                            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></span>
                        <div class="progress">
                            <div class="progress-bar" style="width: 100%"></div>
                        </div>
                        <span class="progress-description"><a href="Complete appointment details.aspx" style="color:white">View your appointments</a>
                  </span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->


            </div>

        </div>

        <div class="row">
            <div class="col-md-8">

                <div class="box box-primary">




                    <div class="box-tools pull-right">

                        <asp:Button ID="BtnUpdateProfile" runat="server" Text="Update profile" CssClass="btn btn-xs btn-flat btn-primary" OnClick="BtnUpdateProfile_Click" meta:resourcekey="BtnUpdateProfileResource1" />
                    </div>

                    <div class="box-body">
                        <div class="row">
                            <div class="form-group" style="margin-left: 16px">
                                <asp:Label ID="Label13" runat="server" Text="Personal" meta:resourcekey="Label13Resource1"></asp:Label>
                            </div>
                            <%--  <%if (Session["Language"].ToString() == "Auto")
                                                         { %>--%>
                            <div class="col-md-10">
                                <%--   <%}
    else
    { %>
                                                     <div class="col-md-10 pull-right">
                                                    <%} %>--%>

                                <div class="progress">

                                    <div id="personalProgress" runat="server" class="progress-bar progress-bar-primary progress-bar-striped" role="progressbar" aria-valuemin="0" aria-valuemax="100">
                                        <span class="sr-only">40% Complete (success)</span>
                                    </div>
                                </div>
                            </div>
                            <%-- <%if (Session["Language"].ToString() == "Auto")
                                                         { %>--%>
                            <div class="col-md-2">
                                <%-- <%}
    else
    { %>
                                                    <div class="col-md-2 pull-right">

                                                    <%} %>--%>
                                <asp:Label ID="LblPersonalStatus" runat="server" Text="Label" meta:resourcekey="LblPersonalStatusResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group" style="margin-left: 16px">

                                <asp:Label ID="Label26" runat="server" Text="Professional" meta:resourcekey="Label14Resource1"></asp:Label>

                            </div>

                            <%--   <%if (Session["Language"].ToString() == "Auto")
                                                         { %>--%>
                            <div class="col-md-10">
                                <%--   <%}
    else
    { %>
                                                     <div class="col-md-10 pull-right">
                                                    <%} %>--%>
                                <div class="progress">
                                    <div id="qualfProgress" dir="rtl" runat="server" class="progress-bar progress-bar-primary progress-bar-striped" role="progressbar" aria-valuemin="0" aria-valuemax="100">
                                        <span class="sr-only">40% Complete (success)</span>
                                    </div>
                                </div>
                            </div>
                            <%--   <%if (Session["Language"].ToString() == "Auto")
                                                         { %>--%>
                            <div class="col-md-2">
                                <%--  <%}
    else
    { %>

                                                    <div class="col-md-2 pull-right">
                                                    <%} %>--%>
                                <asp:Label ID="LblQualfStatus" runat="server" Text="Label" meta:resourcekey="LblQualfStatusResource1"></asp:Label>
                            </div>
                        </div>
                    </div>



                </div>
                <div id="div1">




                    <div class="box box-primary">
                        <%--   <%if (Session["Language"].ToString() == "Auto")
                                    { %>--%>
                        <div class="box-header">
                            <%-- <%}
                                        else
                                        { %>
                                    <div class="box-header" dir="rtl">
                                        <%} %>--%>
                            <h3 style="color: #000000; font-size: large; font-weight: bold;">
                                <asp:Label ID="Label24" runat="server" Text="Verified patient reviews" meta:resourcekey="Label24Resource1"></asp:Label></h3>
                        </div>
                        <div>
                            <div class="form-group">
                                <asp:DataList ID="DataList1" CssClass="table" runat="server" meta:resourcekey="DataList1Resource1">
                                    <ItemTemplate>
                                        <%--   <%if (Session["Language"].ToString() == "Auto")
                                                        { %>--%>
                                        <div>
                                            <%--  <%}
                                                            else
                                                            { %>
                                                        <div dir="rtl">
                                                            <%} %>--%>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("u_email") %>' Visible="False" meta:resourcekey="Label3Resource2"></asp:Label>
                                                    <b>
                                                        <asp:Label ID="Label9" Font-Size="16px" Text='<%# Bind("date") %>' runat="server" meta:resourcekey="Label9Resource2"></asp:Label>
                                                    </b>
                                                    <div style="margin-top: 2%">

                                                        <asp:Label ID="Label1" Font-Size="16px" runat="server" Text="Label" meta:resourcekey="Label1Resource2"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="Label10" Text="(Verified patient)" runat="server" meta:resourcekey="Label10Resource2"></asp:Label>
                                                    </div>
                                                </div>



                                                <div class="col-md-2">
                                                    <span style="font-weight: bold; font-size: 16px">Waiting time</span>
                                                    <div style="margin-top: 2%">
                                                        <asp:Literal ID="Literal4" runat="server" meta:resourcekey="Literal4Resource1"></asp:Literal>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <span style="font-weight: bold; font-size: 16px">Beside manner</span>
                                                    <div style="margin-top: 2%">
                                                        <asp:Literal ID="Literal5" runat="server" meta:resourcekey="Literal5Resource1"></asp:Literal>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <span style="font-weight: bold; font-size: 16px">Service</span>
                                                    <div style="margin-top: 2%">
                                                        <asp:Literal ID="Literal6" runat="server" meta:resourcekey="Literal6Resource1"></asp:Literal>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">

                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("u_review") %>' meta:resourcekey="Label2Resource2"></asp:Label>
                                                </div>
                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-md-4">
                <%--    <%}
            else
            { %>
        <div class="col-md-4 pull-left">
            <%} %>--%>
                <div class="box box-primary">
                    <div class="box-header">
                        <%--   <%if (Session["Language"].ToString() == "Auto")
                        { %>--%>
                        <h3 class="box-title">
                            <%--   <%}
                            else
                            { %>
                        <h3 class="box-title pull-right">
                            <%} %>--%>
                            <asp:Label ID="Label15" runat="server" Text="Availability" meta:resourcekey="Label15Resource1"></asp:Label>

                        </h3>

                        <%--   <%if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <div class="pull-right">
                            <%-- <%}
                                else
                                { %>
                            <div class="pull-left">

                                <%} %>--%>
                            <asp:TextBox ID="TextBox1" placeholder="Search by date" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" AutoPostBack="True" runat="server" OnTextChanged="TextBox1_TextChanged" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form-group">

                            <div id="Div1">
                                <h1>
                                    <asp:Label ID="Label23" runat="server" Visible="False" meta:resourcekey="Label23Resource1"></asp:Label></h1>

                                <asp:DataList ID="DataList2" CssClass="table" runat="server" meta:resourcekey="DataList2Resource1">
                                    <ItemTemplate>
                                        <div class="box box-primary box-solid collapsed-box">
                                            <%--  <%if (Session["Language"].ToString() == "Auto")
                                                    { %>--%>
                                            <div class="box-header with-border">
                                                <%--  <%}
                                                        else
                                                        { %>
                                                    <div class="box-header with-border" dir="rtl">
                                                        <%} %>--%>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                                <%--  <%if (Session["Language"].ToString() == "Auto")
                                                            { %>--%>
                                                <div class="box-tools pull-right">
                                                    <%--  <%}
                                                                else
                                                                { %>
                                                            <div class="box-tools pull-left" style="right: 278px">

                                                                <%} %>--%>
                                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <div class="table-responsive">
                                                    <asp:DataList ID="DataList3" runat="server" RepeatColumns="3" OnItemCommand="DataList3_ItemCommand" meta:resourcekey="DataList3Resource1">

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

                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>

                        </div>
                    </div>
                </div>



            </div>




        </div>



    </section>






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

