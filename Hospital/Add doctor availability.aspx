<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Add doctor availability.aspx.cs" Inherits="Hospital_Add_doctor_availability" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
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
 
     <link href="../css/datepicker3.css" rel="stylesheet" />
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

     <%-- <% if (Session["Language"].ToString() == "Auto")
          {%>--%>
      <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <%--  <%}
    else
    { %>
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <%} %>--%>
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <%-- <% if (Session["Language"].ToString() == "Auto")
                                     {%>--%>
                                  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title"> 
                                  <%--  <%}
    else
    { %>
                                         <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title pull-right"> 
                                    <%} %>--%>
                                     <asp:Label ID="Label7" runat="server" Text="Hakkeem" meta:resourcekey="Label7Resource2"></asp:Label>
                                </h4>
                                   </div>
                            <div class="modal-body">
                                <asp:Label ID="Label8" runat="server" Text="Label" meta:resourcekey="Label8Resource2"></asp:Label>
                                 </div>
                            <div class="modal-footer">
                            </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

   






    <%--  <% if (Session["Language"].ToString() == "Auto")
          {%>--%>
    <div class="container-fluid">
       <%-- <%}
    else
    { %>
          <div class="container-fluid" dir="rtl">
        <%} %>--%>
        <div style="margin-top: 2%">
            <div class="row">
                <%-- <% if (Session["Language"].ToString() == "Auto")
                     {%>--%>
                <div class="col-md-4">
                  <%--  <%}
    else
    { %>
 <div class="col-md-4 pull-right">
                    <%} %>--%>
                    <div class="box box-primary">
                        <div class="box-header with-border">
                          <%--   <% if (Session["Language"].ToString() == "Auto")
                                 {%>--%>
                            <h3 class="box-title">
                              <%--  <%}
    else
    { %>
                                <h3 class="box-title pull-right">
                                <%} %>--%>
                                <asp:Label ID="Label10" runat="server" Text="Set doctor availabilty" meta:resourcekey="Label10Resource1"></asp:Label></h3>
                           <%--  <% if (Session["Language"].ToString() == "Auto")
                                 {%>--%>
                            <div class="box-tools pull-right">
                               <%-- <%}
    else
    { %>
                                <div class="pull-left" style="left: 1%;position:absolute;top:5px;">
                                <%} %>--%>
                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Select doctor" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                <asp:DropDownList ID="DropDownList1" AutoPostBack="True" CssClass="form-control" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" meta:resourcekey="DropDownList1Resource1"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:RadioButtonList ID="RdbDateSelect" runat="server" CssClass="table" RepeatDirection="Horizontal" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="RdbDateSelect_SelectedIndexChanged" meta:resourcekey="RdbDateSelectResource1">
                                    <asp:ListItem meta:resourcekey="ListItemResource1">Select single date</asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource2">Select multiple dates</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="form-group">
                               
                                <%--<asp:TextBox ID="TextBox4" TextMode="Date" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>--%>
                                <asp:TextBox ID="TextBox4" CssClass="form-control datepicker" placeholder="dd-mm-yyyy" onkeydown="return false" onpaste="return false" runat="server" AutoPostBack="True" OnTextChanged="TextBox4_TextChanged" Visible="False" meta:resourcekey="TextBox4Resource1"></asp:TextBox>
                                <div>

                                    <div class="form-group">
                                    </div>

                                    <div class="row">
                                         <%--  <% if (Session["Language"].ToString() == "Auto")
                                               {%>--%>
                                        <div class="col-md-6">
                                           <%-- <%}
    else
    { %>  <div class="col-md-6 pull-right">

                                            <%} %>--%>
                                            <%--<label>From</label>--%><asp:Label ID="Label5" runat="server" Visible="False" Text="From" meta:resourcekey="Label5Resource1"></asp:Label>
                                            <asp:TextBox ID="TxtFromDate" Enabled="False" placeholder="dd-mm-yyyy" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" runat="server" Visible="False" OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True" meta:resourcekey="TxtFromDateResource1"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <%--<label>To</label>--%><asp:Label ID="Label6" runat="server" Visible="False" Text="To" meta:resourcekey="Label6Resource1"></asp:Label>
                                            <asp:TextBox ID="TxtToDate" Enabled="False" placeholder="dd-mm-yyyy" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" runat="server" Visible="False" OnTextChanged="TxtToDate_TextChanged" AutoPostBack="True" meta:resourcekey="TxtToDateResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                                    <div class="form-group">
                                        <div class="row">
                                             <%--  <% if (Session["Language"].ToString() == "Auto")
                                                   {%>--%>
                                            <div class="col-md-6">
                                               <%-- <%}else { %>
                                                 <div class="col-md-6 pull-right">
                                                <%} %>--%>
                                                <div class="bootstrap-timepicker">
                                                    <div class="form-group">
                                                        <label>
                                                            <asp:Label ID="Label12" runat="server" Text="Time From:" meta:resourcekey="Label12Resource1"></asp:Label></label>
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
                                                            <asp:Label ID="Label13" runat="server" Text="Time To:" meta:resourcekey="Label13Resource1"></asp:Label></label>
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
                                                <asp:Label ID="Label14" runat="server" Text="Appointment duration" meta:resourcekey="Label14Resource1"></asp:Label></label>
                                            <div class="input-group">
                                                <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" meta:resourcekey="DropDownList2Resource1">
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
                                                    <asp:Label ID="Label15" runat="server" Text="Minute" meta:resourcekey="Label15Resource1"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label1" runat="server" Text="Break time" meta:resourcekey="Label1Resource1"></asp:Label></label>
                                            <div class="input-group">
                                                <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server" meta:resourcekey="DropDownList3Resource1">
                                                    <asp:ListItem meta:resourcekey="ListItemResource14">0</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource15">5</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource16">10</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource17">15</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource18">20</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="input-group-addon">
                                                    <asp:Label ID="Label16" runat="server" Text="Minute" meta:resourcekey="Label16Resource1"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="box-footer">
                                  <%-- <% if (Session["Language"].ToString() == "Auto")
                                       {%>--%>
                                <div>
                                    <%--<%}
    else
    { %>

                                    <div class="pull-left"><%} %>--%>
                                <asp:Button ID="Button2" CssClass="btn btn-sm btn-primary pull-right" runat="server" Enabled="False" Text="Set availability" OnClick="Button2_Click" meta:resourcekey="Button2Resource1" />
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-8">

                    <div class="box box-primary collapsed-box">
                        <div class="box-header with-border">
                            <%--   <% if (Session["Language"].ToString() == "Auto")
                                   {%>--%>
                            <h3 class="box-title">
                             <%--   <%}
    else
    { %>
                                  <h3 class="box-title pull-right">
                                <%} %>--%>
                                <asp:Label ID="Label17" runat="server" Text="Doctor details" meta:resourcekey="Label17Resource1"></asp:Label></h3>
                           <%--  <% if (Session["Language"].ToString() == "Auto")
                                 {%>--%>
                                <div class="box-tools pull-right">
                                   <%-- <%}
    else
    { %>
                                      <div class="pull-left" style="left: 1%;position:absolute;top:5px;" >
                                    <%} %>--%>
                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="form-group">
                                <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateRows="False" meta:resourcekey="DetailsView1Resource1">
                                    <Fields>
                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                            <ItemTemplate>
                                                <asp:Image ID="Image3" runat="server" AlternateText="Doctor image" CssClass="img-responsive img-circle img-lg" ImageUrl='<%# Bind("hd_photo") %>' meta:resourcekey="Image3Resource1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource2">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("hd_name") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specialty" meta:resourcekey="TemplateFieldResource3">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("hd_specialties") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor identification" meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_id_number") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Fields>
                                </asp:DetailsView>
                            </div>
                        </div>

                    </div>

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="Label18" runat="server" Text="Set available date and time" meta:resourcekey="Label18Resource1"></asp:Label></h3>
                            <%-- <% if (Session["Language"].ToString() == "Auto")
                                 {%>--%>
                             <div class="box-tools pull-right">
                                <%-- <%}
    else
    { %>
                                 <div class="pull-left" style="left: 1%;position:absolute;top:5px;">

                                 <%} %>--%>
                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                               <%--  <% if (Session["Language"].ToString() == "Auto")
                                     {%>--%>
                                <div class="col-md-4">
                                  <%--  <%}
    else
    { %> <div class="col-md-4 pull-right">

                                    <%} %>--%>
                                    <div class="box box-solid box-primary">
                                        <div class="box-header">
                                            <asp:Label ID="Label19" runat="server" Text="Selected times" meta:resourcekey="Label19Resource1"></asp:Label>
                                        </div>
                                        <div class="box-body">
                                            <div class="form-group">

                                                <asp:CheckBoxList ID="CheckBoxList1" CssClass="table" RepeatColumns="2" runat="server" meta:resourcekey="CheckBoxList1Resource1"></asp:CheckBoxList>

                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-8">
                                    <div class="box box-primary box-solid">
                                        <div class="box-header">
                                            <asp:DropDownList ID="DropDownList4" ForeColor="#4AA9AF" Enabled="False" CssClass="btn-xs" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" meta:resourcekey="DropDownList4Resource1">
                                                <asp:ListItem meta:resourcekey="ListItemResource19">-----Choose date-----</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource20">Saturday and Sunday Only</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource21">Select available date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="box-body">
                                            <asp:CheckBoxList ID="CheckBoxList2" CssClass="table" runat="server" RepeatColumns="3" meta:resourcekey="CheckBoxList2Resource1"></asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="box-footer">
                             <%-- <% if (Session["Language"].ToString() == "Auto")
                                  {%>--%>
                            <div class="col-md-4 pull-right">
                               <%-- <%}
    else
    { %>
                                 <div class="pull-left">
                                <%} %>--%>
                                <asp:Button ID="Button3" CssClass="btn btn-sm btn-primary" Enabled="False" runat="server" Text="Submit availability" OnClick="Button3_Click" meta:resourcekey="Button3Resource1" />
                                <asp:Button ID="BtnResetDate" CssClass="btn btn-sm btn-default pull-right" runat="server" Enabled="False" Text="Reset Date" OnClick="BtnResetDate_Click" meta:resourcekey="BtnResetDateResource1" />
                            </div>
                        </div>
                    </div>

                </div>

                
         <!--//model popup for alert-->

       <%-- <asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label20" runat="server" Text="Hakkeem" meta:resourcekey="Label20Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" ValidationGroup="cc" meta:resourcekey="BtnSubmitOTPResource1" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        <!--//modal popup-->

                 <!--//model popup for alert-->

       <%-- <asp:Button ID="btnForAjax1" runat="server" Style="display: none" meta:resourcekey="btnForAjax1Resource1" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg1Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize"><asp:Label ID="Label21" runat="server" Text="Hakkeem" meta:resourcekey="Label21Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label7" runat="server" Text="Label" meta:resourcekey="Label7Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="Button4" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" ValidationGroup="cc" OnClick="Button4_Click" meta:resourcekey="Button4Resource1" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        <!--//modal popup-->

              <%--   <asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender4" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize"><asp:Label ID="Label22" runat="server" Text="Hakkeem" meta:resourcekey="Label22Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label8" runat="server" Text="Label" meta:resourcekey="Label8Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button1" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" OnClick="Button1_Click" meta:resourcekey="Button1Resource1"/></span>
                            </div>
                        </div>
                    </div>

              
            </asp:Panel>--%>
            </div>

            <%-- <asp:Button ID="btnForAjax4" runat="server" Style="display: none" meta:resourcekey="btnForAjax4Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg4"
                TargetControlID="btnForAjax4" CancelControlID="btnclose4" RepositionMode="RepositionOnWindowResizeAndScroll" BehaviorID="ModalPopupExtender3" DynamicServicePath="">

               <Animations>
                    <OnShowing>
                        <FadeIn ForceLayoutInIE="false" Duration=".5" Fps="10" />
                    </OnShowing>
                    <OnShown>
                        <FadeIn ForceLayoutInIE="false" Duration=".5" Fps="10" />
                    </OnShown>
                    <OnHiding>
                        <FadeOut ForceLayoutInIE="false" Duration=".5" Fps="20" />
                    </OnHiding>
                    <OnHidden>
                        <FadeOut ForceLayoutInIE="false" Duration=".5" Fps="10" />
                    </OnHidden>

                    </Animations>  

            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg4" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg4Resource1">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize"><asp:Label ID="Label23" runat="server" Text="Hakkeem" meta:resourcekey="Label23Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose4" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                  <asp:Label ID="Label9" runat="server" Text="Label" meta:resourcekey="Label9Resource1"></asp:Label>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button6" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" meta:resourcekey="Button6Resource1"/></span>
                            </div>
                        </div>
                    </div>

              
            </asp:Panel>--%>



        </div>







    </div>
    <script>
        $(function () {
            //Initialize Select2 Elements
            $(".select2").select2();

            //Datemask dd/mm/yyyy
            $("#datemask").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            //Datemask2 mm/dd/yyyy
            $("#datemask2").inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy" });
            //Money Euro
            $("[data-mask]").inputmask();

            //Date range picker
            $('#reservation').daterangepicker();
            //Date range picker with time picker
            $('#reservationtime').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'MM/DD/YYYY h:mm A' });
            //Date range as a button
            $('#daterange-btn').daterangepicker(
                {
                    ranges: {
                        'Today': [moment(), moment()],
                        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        'This Month': [moment().startOf('month'), moment().endOf('month')],
                        'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                    },
                    startDate: moment().subtract(29, 'days'),
                    endDate: moment()
                },
            function (start, end) {
                $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
            }
            );

            //iCheck for checkbox and radio inputs
            $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue'
            });
            //Red color scheme for iCheck
            $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
                checkboxClass: 'icheckbox_minimal-red',
                radioClass: 'iradio_minimal-red'
            });
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });

            //Colorpicker
            $(".my-colorpicker1").colorpicker();
            //color picker with addon
            $(".my-colorpicker2").colorpicker();

            //Timepicker
            $(".timepicker").timepicker({
                showInputs: false
            });
        });
    </script>

</asp:Content>

