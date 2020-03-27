<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Doctor.aspx.cs" Inherits="Doctor_Doctor" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script src="../css/bootstrap-datepicker.js"></script>
    
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
    <!-- /.box -->
      <%--<%if (Session["Language"].ToString() == "Auto")
          { %>--%>
    <section class=""><%--<%}
    else
    { %>
         <section class="" dir="rtl">
        <%} %>--%>

               <%--<%if (Session["Language"].ToString() == "Auto")
                   { %>--%>

        <div class="col-md-4">
           <%-- <%}
    else
    { %>
              <div class="col-md-4 pull-right">
            <%} %>--%>
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="Label1" runat="server" Text="Set your own availabilty" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                 <%--<%if (Session["Language"].ToString() == "Auto")
                     { %>--%>
                <div class="box-body">
                   <%-- <%}
    else
    { %>
                    <div class="box-body" dir="rtl">
                    <%} %>--%>
                    <div class="form-group">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="table" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" meta:resourcekey="RadioButtonList1Resource1">
                            <asp:ListItem meta:resourcekey="ListItemResource1">Select single date</asp:ListItem>
                            <asp:ListItem meta:resourcekey="ListItemResource2">Select multiple dates</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="form-group">

                        <div>

                            <div class="form-group">
                               
                                <asp:TextBox ID="TextBox4" Enabled="False" placeholder="dd-mm-yyyy" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" runat="server" AutoPostBack="True" OnTextChanged="TextBox4_TextChanged" Visible="False" meta:resourcekey="TextBox4Resource1"></asp:TextBox>

                            </div>

                            <div class="row">
                                 <%-- <%if (Session["Language"].ToString() == "Auto")
                                      { %>--%>
                                <div class="col-md-6">
                                  <%--  <%}
    else
    { %>
                                    <div class="col-md-6 col-md-push-6">
                                    <%} %>--%>
                                    <asp:TextBox ID="TextBox7" Enabled="False" CssClass="form-control datepicker" placeholder="dd-mm-yyyy" onkeydown="return false" onpaste="return false" runat="server" AutoPostBack="True" OnTextChanged="TextBox7_TextChanged" Visible="False" meta:resourcekey="TextBox7Resource1"></asp:TextBox>
                                </div>
                                     <%-- <%if (Session["Language"].ToString() == "Auto")
                                          { %>--%>
                                <div class="col-md-6">
                                   <%-- <%}
    else
    { %>
                                     <div class="col-md-6 col-md-pull-6">
                                    <%} %>--%>
                                    <asp:TextBox ID="TextBox8" Enabled="False" CssClass="form-control datepicker" placeholder="dd-mm-yyyy" onkeydown="return false" onpaste="return false" runat="server" AutoPostBack="True" OnTextChanged="TextBox8_TextChanged" Visible="False" meta:resourcekey="TextBox8Resource1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="bootstrap-timepicker">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label2" runat="server" Text="Time From:" meta:resourcekey="Label2Resource1"></asp:Label></label>
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
                                    <div class="col-md-6">
                                        <div class="bootstrap-timepicker">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label3" runat="server" Text="Time To:" meta:resourcekey="Label3Resource1"></asp:Label></label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="TextBox6" CssClass="form-control timepicker" runat="server" meta:resourcekey="TextBox6Resource1"></asp:TextBox>
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
                                <!-- time Picker -->

                            </div>

                        </asp:Panel>

                    </div>

                    <div class="form-group">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Appointment duration" meta:resourcekey="Label4Resource1"></asp:Label></label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" meta:resourcekey="DropDownList1Resource1">
                                            <asp:ListItem meta:resourcekey="ListItemResource3">60</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource4">55</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource5">50</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource6">45</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource7">40</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource8">30</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource9">25</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource10">20</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource11">15</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource12">10</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource13">5</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="input-group-addon">
                                            <asp:Label ID="Label16" runat="server" Text=" Minute" meta:resourcekey="Label16Resource1"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Break time" meta:resourcekey="Label5Resource1"></asp:Label></label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" meta:resourcekey="DropDownList2Resource1">
                                            <asp:ListItem meta:resourcekey="ListItemResource14">0</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource15">5</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource16">10</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource17">15</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource18">20</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="input-group-addon">
                                            <asp:Label ID="Label15" runat="server" Text="Minute" meta:resourcekey="Label15Resource1"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="box-footer">
                        <asp:Button ID="Button2" CssClass="btn btn-sm btn-primary pull-right" runat="server" Enabled="False" Text="Set availability" OnClick="Button2_Click" meta:resourcekey="Button2Resource1" />
                    </div>
                </div>
            </div>
          <%--  <div class="box box-primary ">
                <div class="box-header with-border">
                   <h3 class="box-title">
                       <asp:Label ID="Label7" runat="server" Text="Cosultation fee" meta:resourcekey="Label7Resource1"></asp:Label></h3>
                </div>
                <div class="box-body">
                   <p style="margin:7%;font-size:200%;font-weight:bold"><asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1"></asp:Label></p>
                </div>
                 <div class="box-footer">
                <a class="btn btn-sm btn-primary pull-right" href="Doctor profile.aspx">
                    <asp:Label ID="Label8" runat="server" Text="Edit consultation fee" meta:resourcekey="Label8Resource1"></asp:Label></a>
            </div>
            </div>--%>
           
        </div>
        <%--<asp:Panel ID="Panel2" CssClass="panel" runat="server">--%>

        <div class="col-md-8">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="Label17" runat="server" Text="Set available time and date" meta:resourcekey="Label17Resource1"></asp:Label></h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                      
                        <div class="col-md-4">
                         
                            <div class="box box-solid box-primary">
                                <div class="box-header">
                                    <asp:Label ID="Label18" runat="server" Text="Selected times" meta:resourcekey="Label18Resource1"></asp:Label>
                                </div>
                                <div class="box-body">

                                    <asp:CheckBoxList ID="CheckBoxList1" RepeatColumns="2" CssClass="table" runat="server" meta:resourcekey="CheckBoxList1Resource1"></asp:CheckBoxList>

                                </div>
                            </div>

                        </div>
                        <div class="col-md-8">
                            <div class="box box-primary box-solid">
                                <div class="box-header">
                                  Selected date
                                   <%-- <asp:DropDownList ID="DropDownList3" ForeColor="#4AA9AF" Enabled="False" CssClass="btn-xs" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" meta:resourcekey="DropDownList3Resource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource19">-----Choose date-----</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource20">Saturday and Sunday Only</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource21">Select available date</asp:ListItem>
                                    </asp:DropDownList>--%>

                                </div>
                               
                                <div class="box-body"> 
                                    <asp:Panel ID="pnldate" runat="server">
                                    <div class="form-group">
                                          <asp:CheckBoxList ID="CheckBoxList3" CssClass="pull-left btn-xs" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                    <asp:Button ID="Button1" CssClass="btn btn-xs btn-default pull-right" Visible="false" runat="server" Text="Submit your days" OnClick="Button1_Click" />
                                    </div>
                                   
                                    <div class="form-group" style="margin-top:1cm;">
                                    <asp:CheckBoxList ID="CheckBoxList2" CssClass="table" runat="server" RepeatColumns="3" meta:resourcekey="CheckBoxList2Resource1"></asp:CheckBoxList>
                                    </div>

                                    </asp:Panel>
                                </div>
                            </div>


                        </div>
                    </div>

                </div>
                <div class="box-footer">
                  
                    <div class="pull-right">
                     
                        <asp:Button ID="Button3" CssClass="btn btn-sm btn-primary" Enabled="False" runat="server" Text="Submit availability" OnClick="Button3_Click" meta:resourcekey="Button3Resource1" />
                        <asp:Button ID="BtnResetDateTime" CssClass="btn btn-sm btn-default" Enabled="False" runat="server" Text="Reset Date" OnClick="BtnResetDateTime_Click" meta:resourcekey="BtnResetDateTimeResource1" />
                    </div>
                </div>
            </div>
        </div>
        <%--</asp:Panel>--%>
    </section>

         <%-- <%if (Session["Language"].ToString() == "Auto")
              { %>--%>
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
           <%-- <%}
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
                                <h4 class="modal-title">
                                   <%-- <%}
    else
    { %>
                                    <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title pull-right">
                                    <%} %>--%>
                                    <asp:Label ID="Label7" runat="server" Text="Hakkeem" meta:resourcekey="Label7Resource2"></asp:Label>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="Label6" runat="server" Text="Label" meta:resourcekey="Label6Resource2"></asp:Label>
                            </div>
                            <div class="modal-footer">
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>









        <script src="../js/app.min.js"></script>

    <%-- c pass--%>
</asp:Content>

