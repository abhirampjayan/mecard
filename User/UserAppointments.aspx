<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" CodeFile="UserAppointments.aspx.cs" Inherits="User_UserAppointments" Culture="en-US" meta:resourcekey="PageResource2" UICulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>
    <%--   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>
    <style type="text/css">
        .gridview {
            background-color: #fff;
            padding: 2px;
            margin: 2% auto;
        }

            .gridview a {
                margin: auto 1%;
                border-radius: 50%;
                background-color: #4aa9af;
                padding: 5px 10px 5px 10px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: rgba(199, 198, 198, 0.28);
                    color: #4aa9af;
                }

            .gridview span {
                background-color: #000;
                color: #fff;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
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
    <script type="text/javascript">
        function getConfirmation(sender, title, message) {
            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>
    <section class="content" style="margin-top: 1.5cm; margin-bottom: 1cm;">
        <div class="container-fluid">
            <div>

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


               <%-- <%if (Session["Speciality"].ToString() == "Auto")
                    { %>--%>
                <div class="col-lg-12 col-md-12 col-sm-12">
                   <%-- <%}
                        else
                        { %>
                    <div class="col-lg-12 col-md-12 col-sm-12" dir="rtl">
                        <%} %>--%>
                        <div class="box box-primary" style="margin-top: 1%">
                            <div class="box-header">
                                <h3 class="box-title">
                                    <asp:Label ID="Label1" runat="server" Text="Your Appointments" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                            </div>
                            <div class="box-body">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="GrvAppointments" runat="server" CssClass="table table-bordered table-hover" HeaderStyle-BackColor="#4aa9af" HeaderStyle-ForeColor="white" AutoGenerateColumns="False" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" DataKeyNames="id" OnPageIndexChanging="GrvAppointments_PageIndexChanging" OnRowUpdating="GrvAppointments_RowUpdating" OnRowCancelingEdit="GrvAppointments_RowCancelingEdit" OnRowDeleting="GrvAppointments_RowDeleting" meta:resourcekey="GrvAppointmentsResource2" OnRowCommand="GrvAppointments_RowCommand" OnDataBound="GrvAppointments_DataBound">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource16">
                                                <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource8">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource2"></asp:Label>
                                                    <asp:Label ID="LblHosId1" runat="server" Text='<%# Eval("h_id") %>' Visible="False" meta:resourcekey="LblHosId1Resource2"></asp:Label>
                                                    <asp:Label ID="LblDocId" runat="server" Text='<%# Eval("d_id") %>' Visible="False" meta:resourcekey="LblDocIdResource2"></asp:Label>
                                                    <asp:Label ID="LblSta" runat="server" Text='<%# Eval("a_status") %>' Visible="False" meta:resourcekey="LblStaResource2"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource9">
                                                <ItemTemplate>
                                                    <div dir="ltr">
                                                        <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblTimeResource2"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hospital" meta:resourcekey="TemplateFieldResource10">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblHospital" runat="server" Text='<%# Eval("h_name") %>' meta:resourcekey="LblHospitalResource2"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor" meta:resourcekey="TemplateFieldResource11">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDoctorName" runat="server" Text='<%# Eval("d_name") %>' meta:resourcekey="LblDoctorNameResource2"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason" meta:resourcekey="TemplateFieldResource12">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDiagnose" runat="server" Text='<%# Eval("a_reason") %>' meta:resourcekey="LblDiagnoseResource2"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblStatus" runat="server" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="####" meta:resourcekey="TemplateFieldResource13">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkConfirm" runat="server" CommandName="Update" meta:resourcekey="LnkConfirmResource2" Text="Confirm"></asp:LinkButton>&nbsp;
                                                    <asp:LinkButton ID="LnkCancel" runat="server" CssClass="btn btn-xs btn-default" CommandName="Cancel" CommandArgument='<%# Eval("d_id") %>' OnClientClick="return getConfirmation(this,Hakkeem,'Are you sure, you want to cancel this appointment?');" meta:resourcekey="LnkCancelResource2" Text="Cancel"></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="History" meta:resourcekey="TemplateFieldResource14">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkShare" runat="server" CommandName="delete" meta:resourcekey="LnkShareResource2" Text="Share"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Report to Hakkeem" meta:resourcekey="TemplateFieldResource15">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" Visible="False" runat="server" Text='<%# Bind("h_id") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                                                    <asp:Label ID="Label2" Visible="False" runat="server" Text='<%# Bind("id") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton7" CssClass="btn btn-xs btn-danger text-center" CommandName="report" CommandArgument='<%# Eval("id") %>' runat="server" meta:resourcekey="LinkButton7Resource1" Text="Report!"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                        <HeaderStyle BackColor="#4AA9AF" ForeColor="White"></HeaderStyle>
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--//model popup for alert-->

                <%--   <asp:Button ID="btnForAjax3" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
                <!--//modal popup-->
                <div>


                    <%-- <asp:Button ID="btnForAjax4" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg4"
                TargetControlID="btnForAjax4" CancelControlID="btnclose4">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg4" runat="server" CssClass="modalPopup">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose4" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> 
                                  <asp:Button ID="Button2" runat="server" Text="Confirm" CssClass="btn btn-success btn-xs" ValidationGroup="cc" OnClick="Button2_Click" />
                                  <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-success btn-xs" ValidationGroup="cc" />
                              </span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
                </div>

                <!-- Bootstrap Modal Dialog -->
                <div class="modal fade" id="myModal1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <asp:UpdatePanel ID="upModal1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                             { %>--%>
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title">Hakkeem</h4>
                                      <%--  <%}
    else
    { %>
                                        <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title pull-right">حكيم</h4>
                                        <%} %>--%>
                                    </div>
                                   <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                         { %>--%>
                                    <div class="modal-body">
                                        <p>Please choose your reason</p>
                                       <%-- <%}
    else
    { %>
                                        <div class="modal-body" dir="rtl">
                                            <p>الرجاء اختيار السبب</p>
                                            <%} %>--%>
                                            <asp:RadioButtonList ID="RadioButtonList1" CssClass="table table-bordered table-responsive" runat="server" ValidationGroup="bb" meta:resourcekey="RadioButtonList1Resource1">
                                                <asp:ListItem meta:resourcekey="ListItemResource1" Text="Not satisfied with this doctor"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource2" Text="Engaged with personal purpose"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource3" Text="Others"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="form-group">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="RadioButtonList1" ValidationGroup="bb" ErrorMessage="Please choose a reason" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Button ID="Button1" CssClass="btn btn-sm btn-primary" runat="server" Text="Confirm" meta:resourcekey="Button1Resource3" ValidationGroup="bb" UseSubmitBehavior="False" OnClick="Button1_Click" />

                                        </div>
                                    </div>
                            </ContentTemplate>
                             <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                             
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>





                <!-- Bootstrap Modal Dialog -->
                <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title">
                                            <asp:Label ID="lblModalTitle" runat="server" Text="Confirm" meta:resourcekey="lblModalTitleResource2"></asp:Label></h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblModalBody" runat="server" Text="Do you want cancel this appointment?" meta:resourcekey="lblModalBodyResource2"></asp:Label>
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" UseSubmitBehavior="false" meta:resourcekey="LinkButton1Resource2" Text="LinkButton"></asp:LinkButton>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="Button2" runat="server" Text="Confirm" CssClass="btn btn-success btn-xs" ValidationGroup="cc" OnClick="Button2_Click" UseSubmitBehavior="False" meta:resourcekey="Button2Resource2" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>



            <%--re-book hakkeem doctor--%>

                     <div class="modal fade" id="myModal3" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal3" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                   <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                        { %>--%>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">Hakkeem</h4>
                                  <%--  <%}
                                    else
                                    { %>
                                    <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title pull-right">حكيم</h4>
                                    <%} %>--%>
                                </div>
                               <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <div class="modal-body">

                                    <%--<%}
                                    else
                                    { %>
                                    <div class="modal-body" dir="rtl">

                                        <%} %>--%>
                                        <div class="form-group">
                                            <p>
                                                 <%--<%if (Session["Speciality"].ToString() == "Auto")
                                                     { %>--%>
                                                Do you want take an appointment this same date and time?
                                               <%-- <%}
    else
    { %>
                                               هل تريد الحصول على موعد في نفس التاريخ والوقت؟
                                                <%} %>--%>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="Button3" CssClass="btn btn-sm btn-primary" UseSubmitBehavior="false" runat="server" Text="Confirm" OnClick="Button3_Click"/>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label4" ForeColor="Red" runat="server"></asp:Label>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </ContentTemplate>
                         <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
                             
                            </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>


                 <%--re-book hospital doctor--%>


                    <div class="modal fade" id="myModal4" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal4" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                 <%--   <%if (Session["Speciality"].ToString() == "Auto")
                                        { %>--%>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">Hakkeem</h4>
                                 <%--   <%}
                                    else
                                    { %>
                                    <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title pull-right">حكيم</h4>
                                    <%} %>--%>
                                </div>
                              <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <div class="modal-body">

                                  <%--  <%}
                                    else
                                    { %>
                                    <div class="modal-body" dir="rtl">

                                        <%} %>--%>
                                        <div class="form-group">
                                            <p>
                                                <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                                     { %>--%>
                                                Do you want take an appointment this same date and time?
                                               <%-- <%}
    else
    { %>
                                               هل تريد الحصول على موعد في نفس التاريخ والوقت؟
                                                <%} %>--%>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="Button4" CssClass="btn btn-sm btn-primary" UseSubmitBehavior="false" runat="server" Text="Confirm" OnClick="Button4_Click"/>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label5" ForeColor="Red" runat="server"></asp:Label>
                                        </div>
                                    </div>


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


          
    </section>
   
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  
    <script src="../Design/dist/js/app.min.js"></script>
  
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>

