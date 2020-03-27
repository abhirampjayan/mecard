<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Doctor availability.aspx.cs" Inherits="Doctor_Doctor_availability" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

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

        });
    </script>

    <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
    <script type="text/javascript">
        function getConfirmation(sender, title, message) {

            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>

    <style type="text/css">
        .messagealert {
            width: 50%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
            margin-top: 6%;
            margin-left: 15%;
            background-color: white;
            /*margin-right:30%;*/
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
    </script>
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>


    <%-- <script type="text/javascript">

        $(function () {
            $("#confirm-dialog").dialog({
                autoOpen: false,
                modal: true,
                buttons: {
                    "OK": function (e) {
                        $(this).dialog("close");
                        <% //GridView gr = (GridView)this.FindControl("GridView1");
        LinkButton l = new LinkButton();
        l = ((LinkButton)this.GridView1.Rows[0].Cells[2].FindControl("LinkButton6"));%>

                        <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(l)) %>
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                        return false;
                    }
                }
            });

            $('#<%=l.ClientID%>').click(function (e) {
                e.preventDefault();
                $("#confirm-dialog").dialog("open");
            });
        });

        function plain_confirm() {
            return confirm('Are you sure?');
        }
    </script>--%>
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
    <div class="container-fluid">
       <%-- <%}
            else
            { %>
        <div class="container-fluid" dir="rtl">

            <%} %>--%>
            <div class="messagealert" id="alert_container">
            </div>


            <div id="modalPopUp" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                          <%--  <%if (Session["Language"].ToString() == "Auto")
                                { %>--%>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                          <%--  <%}
                            else
                            { %>
                            <button type="button" class="close pull-left" data-dismiss="modal">&times;</button>
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
                           <%-- <%if (Session["Language"].ToString() == "Auto")
                                { %>--%>
                            <div>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <button type="button" id="btnConfirm" class="btn btn-primary">
                                    Yes, please</button>
                               <%-- <%}
                      else
                      { %><div class="pull-left">
        <button type="button" class="btn btn-default" data-dismiss="modal">قريب</button>
        <button type="button" id="btnConfirm" class="btn btn-primary">
            نعم من فضلك</button>
        <%} %>--%>
    </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-md-12">
                        <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                            <div class="box box-primary">
                                <div class="box-header">
                                  <%--  <%if (Session["Language"].ToString() == "Auto")
                                        { %>--%>
                                    <h3 class="box-title">
                                       <%-- <%}
                                        else
                                        { %>
                                        <h3 class="box-title pull-right">

                                            <%} %>--%>
                                            <asp:Label ID="Label13" runat="server" Text="Aavailability" meta:resourcekey="Label13Resource1"></asp:Label></h3>
                                        <%--<%if (Session["Language"].ToString() == "Auto")
                                            { %>--%>
                                        <div class="pull-right">
                                            <%--<%}
                                            else
                                            { %>
                                            <div class="pull-left">
                                                <%} %>--%>
                                                <div class="col-md-12">
                                                  <%--  <%if (Session["Language"].ToString() == "Auto")
                                                        { %>--%>
                                                    <div class="row">
                                                      <%--  <%}
                                                        else
                                                        { %>
                                                        <div class="row" dir="rtl">
                                                            <%} %>--%>

                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TextBox1" placeholder="Start date" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" AutoPostBack="True" runat="server" OnTextChanged="TextBox1_TextChanged" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TextBox2" placeholder="End date" Enabled="False" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" AutoPostBack="True" runat="server" OnTextChanged="TextBox2_TextChanged" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <div class="form-group">

                                                    <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowDeleting="GridView1_RowDeleting" meta:resourcekey="GridView1Resource1" OnRowCommand="GridView1_RowCommand">
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
                                                                    <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# Bind("time") %>' Enabled="False" meta:resourcekey="LinkButton5Resource1"></asp:LinkButton>
                                                                    <asp:Label ID="Label4" runat="server" Visible="False" meta:resourcekey="Label4Resource1"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource3">
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-xs btn-primary" OnClientClick="return getConfirmation(this, 'Hakkeem','Are you sure, you want to change the availability?');" CommandName="update" meta:resourcekey="LinkButton6Resource1">Update</asp:LinkButton>
                                                                    &nbsp;<asp:LinkButton ID="LinkButton1" CssClass="btn btn-xs btn-primary" CommandName="engaged" runat="server">Engaged</asp:LinkButton>
                                                                    &nbsp;<asp:LinkButton ID="LinkButton7" CssClass="btn btn-xs btn-primary" runat="server" CommandName="cancel" meta:resourcekey="LinkButton7Resource1">Cancel</asp:LinkButton>
                                                                    
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("date") %>' Visible="False" meta:resourcekey="Label6Resource1"></asp:Label>
                                                                    <asp:Label ID="Label7" runat="server" Visible="False" meta:resourcekey="Label7Resource1"></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="edit" meta:resourcekey="LinkButton3Resource1">Edit</asp:LinkButton>
                                                                    &nbsp;or
                                                <asp:LinkButton ID="LinkButton4" runat="server" OnClientClick="return getConfirmation(this, 'Hakkeem','Are you sure, you want to remove this availabilities?');" CommandName="delete" meta:resourcekey="LinkButton4Resource1">Remove</asp:LinkButton>
                                                                    <asp:Label ID="Label8" runat="server" Visible="False" meta:resourcekey="Label8Resource1"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#F0F0F0" />
                                                        <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
                                                    </asp:GridView>


                                                </div>

                                            </div>
                                        </div>
                        </asp:Panel>

                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="Panel2" Visible="False" runat="server" meta:resourcekey="Panel2Resource1">
                            <div class="box box-primary">
                                <div class="box-header">
                                  <%--  <%if (Session["Language"].ToString() == "Auto")
                                        { %>--%>
                                    <h3 class="box-title">
                                      <%--  <%}
                                        else
                                        { %>
                                        <h3 class="box-title pull-right">
                                            <%} %>--%>
                                            <asp:Label ID="Label14" runat="server" Text="Availability" meta:resourcekey="Label14Resource1"></asp:Label></h3>
                                     <%--   <%if (Session["Language"].ToString() == "Auto")
                                            { %>--%>
                                        <div class="pull-right">
                                           <%-- <%}
                                            else
                                            { %>
                                            <div class="pull-left">
                                                <%} %>--%>
                                                <div class="col-md-12">

                                                    <div class="row">

                                                        <div>
                                                            <asp:TextBox ID="TextBox3" placeholder="Search by date" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" AutoPostBack="True" runat="server" OnTextChanged="TextBox1_TextChanged" meta:resourcekey="TextBox3Resource1"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div class="form-group">
                                                <div class="col-md-8">


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
                                                                    <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# Bind("time") %>' Enabled="False" meta:resourcekey="LinkButton5Resource2"></asp:LinkButton>
                                                                    <asp:Label ID="Label4" runat="server" Visible="False" meta:resourcekey="Label4Resource2"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#F0F0F0" />
                                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="RemoveAll" CssClass="btn btn-flat btn-primary" runat="server" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to remove this selected availabilities?');" OnClick="RemoveAll_Click" meta:resourcekey="RemoveAllResource1">Remove all</asp:LinkButton>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="bootstrap-timepicker">
                                                                    <div class="form-group">
                                                                        <label>
                                                                            <asp:Label ID="Label15" runat="server" Text="Time From:" meta:resourcekey="Label15Resource1"></asp:Label></label>
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
                                                                            <asp:Label ID="Label16" runat="server" Text="Time To:" meta:resourcekey="Label16Resource1"></asp:Label></label>
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
                                                                        <asp:Label ID="Label17" runat="server" Text="Appointment duration" meta:resourcekey="Label17Resource1"></asp:Label></label>
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
                                                                            <asp:Label ID="Label19" runat="server" Text="Minute" meta:resourcekey="Label19Resource1"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="Label18" runat="server" Text="Break time" meta:resourcekey="Label18Resource1"></asp:Label></label>
                                                                    <div class="input-group">
                                                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" meta:resourcekey="DropDownList2Resource1">
                                                                            <asp:ListItem meta:resourcekey="ListItemResource12">0</asp:ListItem>
                                                                            <asp:ListItem meta:resourcekey="ListItemResource13">5</asp:ListItem>
                                                                            <asp:ListItem meta:resourcekey="ListItemResource14">10</asp:ListItem>
                                                                            <asp:ListItem meta:resourcekey="ListItemResource15">15</asp:ListItem>
                                                                            <asp:ListItem meta:resourcekey="ListItemResource16">20</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="input-group-addon">
                                                                            <asp:Label ID="Label20" runat="server" Text="Minute" meta:resourcekey="Label20Resource1"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="ChangeAllTime" CssClass="btn btn-flat btn-primary" runat="server" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to change this selected availabilities?');" OnClick="ChangeAllTime_Click">Change time</asp:LinkButton>&nbsp;
                                                                                    <asp:Button ID="Button9" CssClass="btn btn-flat btn-primary" runat="server" Text="Cancel" OnClick="Button9_Click" meta:resourcekey="Button9Resource1" />

                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                </div>
                        </asp:Panel>
                    </div>
                </div>

                <%-- <div id="confirm-dialog">
            <asp:Label ID="Label29" runat="server" Text="Label"></asp:Label>
        </div>--%>


                <%--  <asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
            TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender4" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">

            <div class="box box-primary box-solid">
                <div class="box-header">
                    <h3 class="box-title" style="text-transform: capitalize">
                        <asp:Label ID="Label21" runat="server" Text="Hakkeem" meta:resourcekey="Label21Resource1"></asp:Label></h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <div class="box-body">

                    <div class="form-group">

                        <span style="text-transform: initial">
                            <asp:Label ID="Label7" runat="server" Text="Label" meta:resourcekey="Label7Resource2"></asp:Label></span>

                    </div>
                    <div class="form-group">
                        <span style="margin-left: 45%">
                            <asp:Button ID="Button4" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" meta:resourcekey="Button4Resource2" /></span>
                    </div>
                </div>
            </div>


        </asp:Panel>

        <div>

            <asp:Button ID="btnForAjax4" runat="server" Style="display: none" meta:resourcekey="btnForAjax4Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg4"
                TargetControlID="btnForAjax4" CancelControlID="btnclose4" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg4" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg4Resource1">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label22" runat="server" Text="Hakkeem" meta:resourcekey="Label22Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose4" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" OnClick="Button2_Click" meta:resourcekey="Button2Resource5" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>


        </div>

        <div>

            <asp:Button ID="btnForAjax5" runat="server" Style="display: none" meta:resourcekey="btnForAjax5Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg5"
                TargetControlID="btnForAjax5" CancelControlID="btnclose5" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg5" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg5Resource1">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label23" runat="server" Text="Hakkeem" meta:resourcekey="Label23Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose5" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-xs" Text="Confirm" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" /></span>
                            <span>
                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-xs" OnClick="BtnCancel_Click" meta:resourcekey="BtnCancelResource1" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>


        </div>


        <div>

            <asp:Button ID="btnForAjax6" runat="server" Style="display: none" meta:resourcekey="btnForAjax6Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg6"
                TargetControlID="btnForAjax6" BehaviorID="ModalPopupExtender3" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg6" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg6Resource1">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label24" runat="server" Text="Hakkeem" meta:resourcekey="Label24Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label3" runat="server" Text="Label" meta:resourcekey="Label3Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary btn-xs" Text="Confirm" OnClick="Button5_Click" meta:resourcekey="Button5Resource3" /></span>
                            <span>
                                <asp:Button ID="Button6" runat="server" Text="Cancel" CssClass="btn btn-primary btn-xs" OnClick="Button6_Click" meta:resourcekey="Button6Resource2" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>


        </div>


        <div>

            <asp:Button ID="btnForAjax7" runat="server" Style="display: none" meta:resourcekey="btnForAjax7Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg7"
                TargetControlID="btnForAjax7" BehaviorID="ModalPopupExtender5" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg7" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg7Resource1">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label25" runat="server" Text="Hakkeem" meta:resourcekey="Label25Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label9" runat="server" Text="Label" meta:resourcekey="Label9Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button7" runat="server" CssClass="btn btn-primary btn-xs" Text="Confirm" OnClick="Button7_Click" meta:resourcekey="Button7Resource2" /></span>
                            <span>
                                <asp:Button ID="Button8" runat="server" Text="Cancel" CssClass="btn btn-primary btn-xs" OnClick="Button8_Click" meta:resourcekey="Button8Resource1" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>


        </div>--%>

                <div>

                    <asp:Button ID="btnForAjax8" runat="server" Style="display: none" meta:resourcekey="btnForAjax8Resource1" />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg8"
                        TargetControlID="btnForAjax8" BehaviorID="ModalPopupExtender6" DynamicServicePath="">
                    </ajaxToolkit:ModalPopupExtender>

                    <asp:Panel ID="pnlPopupMsg8" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg8Resource1" Visible="false">

                        <div class="box box-primary box-solid">
                            <div class="box-header">
                                <h3 class="box-title" style="text-transform: capitalize">
                                    <asp:Label ID="Label26" runat="server" Text="Hakkeem" meta:resourcekey="Label26Resource1"></asp:Label></h3>
                                <div class="box-tools pull-right">
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="form-group">
                                    <asp:CheckBoxList ID="CheckBoxList2" RepeatColumns="5" CssClass="table" runat="server" meta:resourcekey="CheckBoxList2Resource1"></asp:CheckBoxList>
                                </div>
                                <div class="form-group">

                                    <span style="text-transform: initial">
                                        <asp:Label ID="Label10" runat="server" Text="Label" meta:resourcekey="Label10Resource1"></asp:Label></span>

                                </div>
                                <div class="form-group">
                                    <span style="margin-left: 45%">
                                        <asp:Button ID="Button10" runat="server" CssClass="btn btn-primary btn-xs" Text="Confirm" OnClick="Button10_Click" meta:resourcekey="Button10Resource1" /></span>
                                    <span>
                                        <asp:Button ID="Button11" runat="server" Text="Cancel" CssClass="btn btn-primary btn-xs" meta:resourcekey="Button11Resource1" /></span>
                                </div>
                            </div>
                        </div>


                    </asp:Panel>


                </div>


                <%--  <div>

            <asp:Button ID="btnForAjax41" runat="server" Style="display: none" meta:resourcekey="btnForAjax41Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender7" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg41"
                TargetControlID="btnForAjax41" BehaviorID="ModalPopupExtender7" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg41" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg41Resource1">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label27" runat="server" Text="Hakkeem" meta:resourcekey="Label27Resource1"></asp:Label></h3>

                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label11" runat="server" Text="Label" meta:resourcekey="Label11Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button13" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" OnClick="Button13_Click" meta:resourcekey="Button13Resource1" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>


        </div>

        <div>

            <asp:Button ID="btnForAjax412" runat="server" Style="display: none" meta:resourcekey="btnForAjax412Resource1" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender8" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg412"
                TargetControlID="btnForAjax412" BehaviorID="ModalPopupExtender8" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg412" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg412Resource1">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label28" runat="server" Text="Hakkeem" meta:resourcekey="Label28Resource1"></asp:Label></h3>

                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label12" runat="server" Text="Label" meta:resourcekey="Label12Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button14" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" OnClick="Button14_Click" meta:resourcekey="Button14Resource1" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>


       </div>--%>
            </div>
            <script src="../js/app.min.js"></script>
</asp:Content>

