<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Hospital_Index" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../js/jquery.slimscroll.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '450px'

            });
        });
    </script>
    <div class="container-fluid">
        <div style="margin-top: 3%">

            <div class="col-md-8">
                <div class="col-md-12">
                    <div class="box box-primary">

                        <div class="box-header">
                            <h3 class="box-title">Find a Doctor
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">

                                <div class="col-lg-3">
                                    <asp:TextBox ID="TxtDocName" runat="server" CssClass="form-control" placeholder="Doctor Name" OnTextChanged="TxtDocName_TextChanged" AutoPostBack="True" meta:resourcekey="TxtDocNameResource1"></asp:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="ddlSpeciality" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSpeciality_SelectedIndexChanged" meta:resourcekey="ddlSpecialityResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource1">General</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource2">Surgen</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource3">Ortho</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource4">Gynac</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="TxtDate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="True" OnTextChanged="TxtDate_TextChanged" meta:resourcekey="TxtDateResource1"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button ID="BtnSearch" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="BtnSearch_Click" meta:resourcekey="BtnSearchResource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="col-md-12">
                        <div class="box-body">
                            <div class="form-group table-responsive">
                                <asp:DataList ID="DataList1" runat="server" CssClass="table table-hover" OnItemCommand="DataList1_ItemCommand" meta:resourcekey="DataList1Resource1" >
                                    <ItemTemplate>
                                        <div class="form-group">
                                            <div class="box box-primary">
                                                <div class="box-header with-border">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <h3 class="box-title"><b>Dr.
                                                     <asp:LinkButton ID="LinkButton2" Text='<%# Bind("hd_name") %>' CommandName="view" runat="server" CommandArgument='<%# Bind("hd_email") %>' meta:resourcekey="LinkButton2Resource1" ></asp:LinkButton></b></h3>
                                                            <asp:Label ID="Label1" Visible="False" runat="server" Text='<%# Bind("hd_email") %>' meta:resourcekey="Label1Resource1"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <b>
                                                                <asp:Label ID="Label2" runat="server" Text="Available Dates" meta:resourcekey="Label2Resource1"></asp:Label></b>
                                                        </div>
                                                        <div class="col-md-4">
                                                        </div>
                                                    </div>
                                                    <div class="box-tools pull-right">
                                                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                        <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Image ID="Image1" CssClass="img-circle img-responsive img-lg" AlternateText="Doctor photo" runat="server" ImageUrl='<%# Bind("hd_photo") %>' meta:resourcekey="Image1Resource1" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-9">
                                                            <div class="form-group table-responsive">
                                                               
                                                                <asp:DataList ID="DataList2" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" OnItemCommand="DataList2_ItemCommand" meta:resourcekey="DataList2Resource1">
                                                                    <ItemTemplate>
                                                                        <div class="form-group">
                                                                            <asp:LinkButton ID="LnkDate" runat="server" CssClass="btn btn-sm btn-bitbucket" CommandName="apointment" Text='<%# Eval("date") %>' CommandArgument='<%# Eval("hd_id") %>' meta:resourcekey="LnkDateResource1"></asp:LinkButton>
                                                                        </div>

                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                                  
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <div class="col-sm-4">
                                                        Specialty : 
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("hd_specialties") %>' meta:resourcekey="Label3Resource1" ></asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                         <asp:LinkButton ID="LnkCheckApointments" runat="server" CssClass="center-block" CommandName="Appointments" CommandArgument='<%# Eval("hd_email") %>' meta:resourcekey="LnkCheckApointmentsResource1">Check Appointments</asp:LinkButton>
                                                    </div>
                                                   <div class="col-sm-4">
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-xs btn-default pull-right" CommandName="check" CommandArgument='<%# Bind("hd_email") %>' meta:resourcekey="LinkButton1Resource1" >More Dates</asp:LinkButton>
                                                   </div>
                                                   
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

                <div class="col-md-12">
                    <div class=" box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Today's Appointments
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group table-responsive">
                                <div id="Div1">
                                <asp:DataList ID="DataList3" CssClass="table table-hover" runat="server" meta:resourcekey="DataList3Resource1">
                                    <ItemTemplate>
                                        <div class="box box-primary box-solid collapsed-box">
                                            <div class="box-header with-border">
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                                <asp:Label ID="Label4" Visible="False" runat="server" Text='<%# Bind("d_id") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                                &nbsp; Dr.<asp:Label ID="Label5" runat="server" Text="Label" meta:resourcekey="Label5Resource1"></asp:Label>
                                                
                                                <div class="box-tools pull-right">
                                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <asp:DataList ID="DataList4" CssClass="table" runat="server" RepeatColumns="3" OnItemCommand="DataList4_ItemCommand" meta:resourcekey="DataList4Resource1">

                                                    <ItemTemplate>

                                                        <div class="form-group">
                                                            <asp:Button ID="Button2" CommandName="AppointmentDetails" CssClass="btn btn-sm btn-bitbucket" runat="server" Text='<%# Bind("a_time") %>' meta:resourcekey="Button2Resource2"/>
                                                            <asp:Label ID="LblDate" runat="server" Text="Label" Visible="False" meta:resourcekey="LblDateResource1"></asp:Label>
                                                            <asp:Label ID="LblDocName" runat="server" Text="Label" Visible="False" meta:resourcekey="LblDocNameResource1"></asp:Label>
                                                            <asp:Label ID="LblDocId" runat="server" Text="Label" Visible="False" meta:resourcekey="LblDocIdResource1"></asp:Label>
                                                            <asp:Label ID="LblStatus" runat="server" Text='<%# Eval("a_status") %>' Visible="False" meta:resourcekey="LblStatusResource1"></asp:Label>
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

             <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
                            
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg" TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>

                          

                            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
                                
                                <div class="col-md-12">
                                    <div class="box box-success box-solid">
                                        <div class="box-header">
                                            <h3 class="box-title">
                                                <asp:Label ID="LblApDocName" runat="server" Text="Label" meta:resourcekey="LblApDocNameResource1"></asp:Label> </h3>
                                            <div class="box-tools pull-right">
                                               
                                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div class="form-group">
                                                <asp:GridView ID="GrvApointments" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" meta:resourcekey="GrvApointmentsResource1">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource2" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Appointment Time" meta:resourcekey="TemplateFieldResource2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblApoTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblApoTimeResource1" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Patient Name" meta:resourcekey="TemplateFieldResource3">
                                                            <ItemTemplate>
                                                                 <asp:Label ID="LblPatntTime" runat="server" Text='<%# Eval("name") %>' meta:resourcekey="LblPatntTimeResource1" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                            
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
              <asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender4" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label8" runat="server" Text="Label" meta:resourcekey="Label8Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" OnClick="Button1_Click" meta:resourcekey="Button2Resource3"/></span>
                            </div>
                        </div>
                    </div>

              
            </asp:Panel>



        </div>
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

</asp:Content>

