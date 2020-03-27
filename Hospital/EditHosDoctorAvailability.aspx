<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="EditHosDoctorAvailability.aspx.cs" Inherits="Hospital_EditHosDoctorAvailability" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/bootstrap-datepicker.js"></script>
   
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
            //Timepicker
            $(".timepicker").timepicker({
                showInputs: false
            });

        });
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
            //Timepicker
            $(".timepicker").timepicker({
                showInputs: false
            });

        });
    </script>

     <%-- <% if (Session["Language"].ToString() == "Auto")
          {%>--%>
    <div class="container-fluid">
       <%-- <%}
    else
    { %>
          <div class="container-fluid" dir="rtl">
        <%} %>--%>
            <div id="modalPopUp" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                       <%--  <% if (Session["Language"].ToString() == "Auto")
                             {%>--%>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                       <%-- <%}
    else
    { %>

                        <%} %>--%>
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
                        <%-- <% if (Session["Language"].ToString() == "Auto")
                             {%>--%>
                        <div>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="btnConfirm" class="btn btn-primary">
                            Yes, please</button>
                            </div><%--<%}
    else
    { %>
                        <div class="pull-left">
                            
                        <button type="button" id="btnConfirm" class="btn btn-primary">
                           نعم من فضلك</button>
                             <button type="button" class="btn btn-default" data-dismiss="modal">قريب</button>
                        </div>
                        <%} %>--%>
                    </div>
                </div>
            </div>
        </div>
        <div style="margin-top: 2%">


            <div class="row">
                <div class="col-md-12">
                    <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                        <div class="box box-primary">
                            <div class="box-header">
                                <h3 class="box-title">
                                    <asp:Label ID="Label12" runat="server" Text="availability"></asp:Label></h3>
                                 <%-- <% if (Session["Language"].ToString() == "Auto")
                                      {%>--%>
                                <div class="pull-right">
                                   <%-- <%}
    else
    { %>
                                      <div class="pull-left">
                                    <%} %>--%>
                                    <div class="col-md-12">
                                        <%-- <% if (Session["Language"].ToString() == "Auto")
                                             {%>--%>
                                        <div class="col-md-4">
                                          <%--  <%}
    else
    { %><div class="col-md-4 pull-right">
                                            <%} %>--%>
                                            <asp:DropDownList ID="DdlDoctors" AutoPostBack="True" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlDoctors_SelectedIndexChanged" meta:resourcekey="DdlDoctorsResource1"></asp:DropDownList>
                                        </div>
                                        <%--    <% if (Session["Language"].ToString() == "Auto")
                                                {%>--%>
                                        <div class="col-md-4">
                                           <%-- <%}
    else
    { %>
                                             <div class="col-md-4 pull-right">
                                            <%} %>--%>
                                            <asp:TextBox ID="TextBox1" placeholder="Start date" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" AutoPostBack="True" runat="server" OnTextChanged="TextBox1_TextChanged" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="TextBox2" placeholder="End date" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" AutoPostBack="True" runat="server" OnTextChanged="TextBox2_TextChanged" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="box-body">
                                <div class="form-group">
                                    <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" PageSize="8" meta:resourcekey="GridView1Resource1" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource1">
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Enabled="False" Text='<%# Bind("date") %>' meta:resourcekey="LinkButton2Resource1"></asp:LinkButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Bind("date") %>' Enabled="False" meta:resourcekey="LinkButton2Resource2"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available Time" meta:resourcekey="TemplateFieldResource2">
                                                <EditItemTemplate>
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="11" RepeatDirection="Horizontal" RepeatLayout="Flow" meta:resourcekey="CheckBoxList1Resource1">
                                                    </asp:CheckBoxList>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("time") %>' Visible="False" meta:resourcekey="Label5Resource1"></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton5" runat="server" Enabled="False" Text='<%# Bind("time") %>' meta:resourcekey="LinkButton5Resource1"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource3">
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-xs btn-primary" CommandName="update" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to remove this selected availabilities?');" meta:resourcekey="LinkButton6Resource1">Update</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton1" CommandName="engage" CssClass="btn btn-xs btn-primary" runat="server">Engage</asp:LinkButton>
                                                    &nbsp;<asp:LinkButton ID="LinkButton7" CssClass="btn btn-xs btn-primary" runat="server" CommandName="cancel" meta:resourcekey="LinkButton7Resource1">Cancel</asp:LinkButton>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("date") %>' Visible="False" meta:resourcekey="Label6Resource1"></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="edit" meta:resourcekey="LinkButton3Resource1">Edit</asp:LinkButton>
                                                    &nbsp;or
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="delete" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to remove this selected availabilities?');" meta:resourcekey="LinkButton4Resource1">Remove</asp:LinkButton>
                                                    <asp:Label ID="Label8" runat="server" Visible="False" meta:resourcekey="Label8Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                    </asp:GridView>
                                </div>

                                <asp:CheckBoxList ID="CheckBoxList2" Visible="False" RepeatColumns="5" CssClass="table" runat="server" meta:resourcekey="CheckBoxList2Resource1"></asp:CheckBoxList>

                            </div>
                        </div>
                    </asp:Panel>
                </div>

                <div class="col-md-12">
                    <asp:Panel ID="Panel2" Visible="False" runat="server" meta:resourcekey="Panel2Resource1">
                        <div class="box box-primary">
                            <div class="box-header">
                                <h3 class="box-title">
                                    <asp:Label ID="Label13" runat="server" Text="Availability" meta:resourcekey="Label13Resource1"></asp:Label>

                                </h3>
                                <div class="pull-right">
                                </div>

                            </div>
                            <div class="box-body">
                                <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <asp:GridView ID="GridView2" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" meta:resourcekey="GridView2Resource1">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource4">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" Enabled="False" Text='<%# Bind("date") %>' meta:resourcekey="LinkButton2Resource3"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Bind("date") %>' Enabled="False" meta:resourcekey="LinkButton2Resource4"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Available Time" meta:resourcekey="TemplateFieldResource5">
                                                    <EditItemTemplate>
                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="11" RepeatDirection="Horizontal" RepeatLayout="Flow" meta:resourcekey="CheckBoxList1Resource2">
                                                        </asp:CheckBoxList>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("time") %>' Visible="False" meta:resourcekey="Label5Resource2"></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton5" runat="server" Enabled="False" Text='<%# Bind("time") %>' meta:resourcekey="LinkButton5Resource2"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="gridview"></PagerStyle>
                                        </asp:GridView>
                                    </div>

                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="RemoveAll" CssClass="btn btn-flat btn-primary" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to remove this selected availabilities?');" runat="server" OnClick="RemoveAll_Click" meta:resourcekey="RemoveAllResource1">Remove all</asp:LinkButton>
                                    </div>

                                      <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6">
                                               <div class="bootstrap-timepicker">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text="Time From:" meta:resourcekey="Label14Resource1"></asp:Label></label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="TextBox4" CssClass="form-control timepicker" runat="server" meta:resourcekey="TextBox4Resource1"></asp:TextBox>
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-clock-o"></i>
                                                    </div>
                                                </div>
                                                <!-- /.input group -->
                                            </div>
                                            <!-- /.form group -->
                                        </div>
                                            </div>
                                            <div class="col-md-6">
                                               <div class="bootstrap-timepicker">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label15" runat="server" Text="Time To:" meta:resourcekey="Label15Resource1"></asp:Label></label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="TextBox5" CssClass="form-control timepicker" runat="server" meta:resourcekey="TextBox5Resource1"></asp:TextBox>
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-clock-o"></i>
                                                    </div>
                                                </div>
                                                <!-- /.input group -->
                                            </div>
                                            <!-- /.form group -->
                                        </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Appointment duration" meta:resourcekey="Label16Resource1"></asp:Label></label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" meta:resourcekey="DropDownList1Resource1">
                                            <asp:ListItem meta:resourcekey="ListItemResource1">60</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource2">55</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource3">50</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource4">45</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource5">40</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource6">30</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource7">25</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource8">20</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource9">15</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource10">10</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource11">5</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="input-group-addon">
                                            <asp:Label ID="Label17" runat="server" Text="Minute" meta:resourcekey="Label17Resource1"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label19" runat="server" Text="Break time" meta:resourcekey="Label19Resource1"></asp:Label></label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" meta:resourcekey="DropDownList2Resource1">
                                            <asp:ListItem meta:resourcekey="ListItemResource12">0</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource13">5</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource14">10</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource15">15</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource16">20</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="input-group-addon">
                                            <asp:Label ID="Label18" runat="server" Text="Minute" meta:resourcekey="Label18Resource1"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                                    <div class="form-group">
                                        <asp:LinkButton ID="ChangeTime" CssClass="btn btn-flat btn-primary" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to remove this selected availabilities?');" runat="server" OnClick="ChangeTime_Click" meta:resourcekey="ChangeTimeResource1">Change time</asp:LinkButton>
                                        &nbsp;<asp:Button ID="Button10" CssClass="btn btn-flat btn-default" runat="server" Text="Cancel" OnClick="Button10_Click" meta:resourcekey="Button10Resource1" />
                                    </div>


                                </div>

                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                </div>

            </div>
        </div>

        <!--//model popup for alert-->

        <%--<asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
            TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label20" runat="server" Text="Hakkeem" meta:resourcekey="Label20Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" meta:resourcekey="BtnSubmitOTPResource1" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>--%>
        <!--//modal popup-->

        <div>
            <!--//model popup for alert-->

           <%-- <asp:Button ID="btnForAjax1" runat="server" Style="display: none" meta:resourcekey="btnForAjax1Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg1Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform: capitalize">
                                <asp:Label ID="Label21" runat="server" Text="Hakkeem" meta:resourcekey="Label21Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <span style="text-transform: initial">
                                    <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left: 45%">
                                    <asp:Button ID="Button2" runat="server" Text="Confirm" CssClass="btn btn-success btn-xs" OnClick="Button2_Click" meta:resourcekey="Button2Resource5" /></span>
                                <span>
                                    <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-success btn-xs" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
            <%--<asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender4" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label22" runat="server" Text="Hakkeem" meta:resourcekey="Label22Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label8" runat="server" Text="Label" meta:resourcekey="Label8Resource2"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button3" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button1_Click" meta:resourcekey="Button3Resource2" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>--%>
            <!--//modal popup-->



            <%--<asp:Button ID="btnForAjax2" runat="server" Style="display: none" meta:resourcekey="btnForAjax2Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg2"
                TargetControlID="btnForAjax2" CancelControlID="btnclose2" BehaviorID="ModalPopupExtender3" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg2" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg2Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform: capitalize">
                                <asp:Label ID="Label23" runat="server" Text="Hakkeem" meta:resourcekey="Label23Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose2" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <span style="text-transform: initial">
                                    <asp:Label ID="Label3" runat="server" Text="Label" meta:resourcekey="Label3Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left: 45%">
                                    <asp:Button ID="Button5" runat="server" Text="Confirm" CssClass="btn btn-success btn-xs" OnClick="Button5_Click" meta:resourcekey="Button5Resource3" /></span>
                                <span>
                                    <asp:Button ID="Button6" runat="server" Text="Cancel" CssClass="btn btn-success btn-xs" OnClick="Button6_Click" meta:resourcekey="Button6Resource2" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>






        </div>


        <%--Confirmpopup for remove--%>
        <div>
            <%--<asp:Button ID="btnForAjax31" runat="server" Style="display: none" meta:resourcekey="btnForAjax31Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg31"
                TargetControlID="btnForAjax31" CancelControlID="btnclose2" BehaviorID="ModalPopupExtender5" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg31" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg31Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform: capitalize">
                                <asp:Label ID="Label24" runat="server" Text="Hakkeem" meta:resourcekey="Label24Resource1"></asp:Label></h3>
                            
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <span style="text-transform: initial">
                                    <asp:Label ID="Label4" runat="server" Text="Label" meta:resourcekey="Label4Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left: 45%">
                                    <asp:Button ID="Button7" runat="server" Text="Confirm" CssClass="btn btn-success btn-xs" OnClick="Button7_Click" meta:resourcekey="Button7Resource2" /></span>
                                <span>
                                    <asp:Button ID="Button8" runat="server" Text="Cancel" CssClass="btn btn-success btn-xs" OnClick="Button6_Click" meta:resourcekey="Button8Resource1" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        </div>

        <%--popup for can't choose current date--%>
         <div>
            <%--<asp:Button ID="btnForAjax32" runat="server" Style="display: none" meta:resourcekey="btnForAjax32Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg32"
                TargetControlID="btnForAjax32" CancelControlID="btnclose2" BehaviorID="ModalPopupExtender6" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg32" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg32Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform: capitalize">
                                <asp:Label ID="Label25" runat="server" Text="Hakkeem" meta:resourcekey="Label25Resource1"></asp:Label></h3>
                            
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <span style="text-transform: initial">
                                    <asp:Label ID="Label7" runat="server" Text="Label" meta:resourcekey="Label7Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left: 45%">
                                    <asp:Button ID="Button12" runat="server" Text="Ok" CssClass="btn btn-success btn-xs" OnClick="Button12_Click" meta:resourcekey="Button12Resource1"/></span>
                                
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        </div>


        <%--selected time popup confirm--%>
         <div>

           <%-- <asp:Button ID="btnForAjax8" runat="server" Style="display: none" meta:resourcekey="btnForAjax8Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender7" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg8"
                TargetControlID="btnForAjax8" BehaviorID="ModalPopupExtender7" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg8" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg8Resource1">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label26" runat="server" Text="Hakkeem" meta:resourcekey="Label26Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            
                        </div>
                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label10" runat="server" Text="Label" meta:resourcekey="Label10Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button11" runat="server" CssClass="btn btn-success btn-xs" Text="Confirm" OnClick="Button11_Click" meta:resourcekey="Button11Resource1" /></span>
                            <span>
                                <asp:Button ID="Button13" runat="server" Text="Cancel" CssClass="btn btn-success btn-xs" meta:resourcekey="Button13Resource1" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>--%>


        </div>


           <div>
            <%--<asp:Button ID="btnForAjax33" runat="server" Style="display: none" meta:resourcekey="btnForAjax33Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender8" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg33"
                TargetControlID="btnForAjax33" CancelControlID="btnclose2" BehaviorID="ModalPopupExtender8" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg33" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg33Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform: capitalize">
                                <asp:Label ID="Label27" runat="server" Text="Hakkeem" meta:resourcekey="Label27Resource1"></asp:Label></h3>
                            
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <span style="text-transform: initial">
                                    <asp:Label ID="Label9" runat="server" Text="Label" meta:resourcekey="Label9Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left: 45%">
                                    <asp:Button ID="Button15" runat="server" Text="Ok" CssClass="btn btn-success btn-xs" OnClick="Button15_Click" meta:resourcekey="Button15Resource1"/></span>
                                
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        </div>

         <div>
            <%--<asp:Button ID="btnForAjax34" runat="server" Style="display: none" meta:resourcekey="btnForAjax34Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg34"
                TargetControlID="btnForAjax34" CancelControlID="btnclose2" BehaviorID="ModalPopupExtender9" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg34" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg34Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform: capitalize">
                                <asp:Label ID="Label28" runat="server" Text="Hakkeem" meta:resourcekey="Label28Resource1"></asp:Label></h3>
                            
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <span style="text-transform: initial">
                                    <asp:Label ID="Label11" runat="server" Text="Label" meta:resourcekey="Label11Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left: 45%">
                                    <asp:Button ID="Button16" runat="server" Text="Ok" CssClass="btn btn-success btn-xs" OnClick="Button16_Click" meta:resourcekey="Button16Resource1"/></span>
                                
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        </div>


    </div>
</asp:Content>

