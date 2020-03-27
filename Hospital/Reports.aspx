<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Doctor_Reports" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '400px'

            });
        });
    </script>
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
            width: 1000px;
            height: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="container-fluid">
          <div class="col-md-12">
                  <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">History</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                    <div class="form-group">

                       <%-- <div class="col-md-5 col-sm-5">
                            <asp:RadioButtonList ID="rdpSelect" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" CssClass="table" RepeatLayout="Flow" OnSelectedIndexChanged="rdpSelect_SelectedIndexChanged">
                                <asp:ListItem>Date</asp:ListItem>
                                <asp:ListItem>BookDoc Id</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>

                    </div>
                        </div>
                    <div class="row">
                    <div class="form-group">
                        <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                        <div class="col-lg-3 col-md-3 ">
                            <label>From Date</label><asp:Label ID="LblValidFrom" runat="server" Text="Please select a date" Visible="False" ForeColor="Red" meta:resourcekey="LblValidFromResource1"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please select a date" ControlToValidate="TxtFrom" ValidationGroup="s" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="TxtFrom" runat="server" TextMode="Date" CssClass="form-control" ValidationGroup="a" AutoPostBack="True" OnTextChanged="TxtFrom_TextChanged" meta:resourcekey="TxtFromResource1"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-md-3 ">
                            <label>To Date</label><asp:Label ID="LblValidTo" runat="server" Text="* Please select a date" Visible="False" ForeColor="Red" meta:resourcekey="LblValidToResource1"></asp:Label>
                        <asp:TextBox ID="TxtTo" runat="server" TextMode="Date" CssClass="form-control" ValidationGroup="a" AutoPostBack="True" OnTextChanged="TxtTo_TextChanged" Enabled="False" meta:resourcekey="TxtToResource1" ></asp:TextBox>
                        </div>
                        <div class="col-lg-2 col-md-2 ">
                        <label>Diagnosis</label>
                            <asp:DropDownList ID="DdlDiagnose" runat="server" CssClass="form-control" meta:resourcekey="DdlDiagnoseResource1"></asp:DropDownList>
                        </div>
                    <div class="col-lg-2 col-md-2 ">
                        <div style="margin-top:28px;">
                            <asp:Button ID="BtnViewReport" runat="server" Text="View/Download" CssClass="btn btn-sm btn-primary"  ValidationGroup="a" OnClick="BtnViewReport_Click" meta:resourcekey="BtnViewReportResource1" />
                            </div>
                    </div>
                             <div class="col-lg-2 col-md-2">
                                <div style="margin-top:27px;">
                                    <asp:Button ID="BtnMostUser" runat="server" Text="Most Patient" CssClass="btn btn-default " ForeColor="#18BC9C" ToolTip="Click to view most visited patients" ValidationGroup="s" Width="102px" CommandName="MostPatient" OnClick="BtnMostUser_Click" meta:resourcekey="BtnMostUserResource1" />
                                    <asp:Button ID="BtnMostDisease" runat="server" Text="Most Disease" CssClass="btn btn-default " ForeColor="#18BC9C" ToolTip="Click to view most Disease the patients consulted" ValidationGroup="s" CommandName="MostDisease" OnClick="BtnMostDisease_Click" meta:resourcekey="BtnMostDiseaseResource1" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                        </div>
                    <div class="row">
                    <div class="form-group">
                        <asp:Panel ID="Panel2" runat="server" meta:resourcekey="Panel2Resource1">
                         <div class="col-lg-4 col-md-4 ">
                            <label>Hakkeem Id</label>  
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Hakkeem Id" ValidationGroup="b" ForeColor="Red" ControlToValidate="TxtBookDocId" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>                      
                        <asp:TextBox ID="TxtBookDocId" runat="server" CssClass="form-control" ValidationGroup="b" placeholder="Patient Hakkeem Id" meta:resourcekey="TxtBookDocIdResource1"></asp:TextBox>
                        </div>
                    <div class="col-lg-2 col-md-2 ">
                        <div style="margin-top:28px;">
                            <asp:Button ID="BtnViewReportId" runat="server" Text="View/Download" CssClass="btn btn-sm btn-primary"  ValidationGroup="b" OnClick="BtnViewReportId_Click" meta:resourcekey="BtnViewReportIdResource1" />
                            </div>
                    </div>
                            
                        </asp:Panel>
                </div>
                        </div>
            </div>
           </div>
              <asp:Panel ID="Panel3" runat="server" Visible="False" meta:resourcekey="Panel3Resource1">
               <div class="col-md-12">
                 
                    <div class="box box-primary">
                        <div class="box-header with-border">
                             <h3 class="box-title">Details</h3>
                            <asp:Button ID="BtnPrintDownload" runat="server" Text="Download"  CssClass="btn btn-sm btn-danger pull-right" OnClick="BtnPrintDownload_Click" meta:resourcekey="BtnPrintDownloadResource1"/>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group table-responsive">
                                <div id="Div1">
                                    <div class="table-responsive">
                                <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPageIndexChanging="GridView1_PageIndexChanging" meta:resourcekey="GridView1Resource1"  >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource1">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource1" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Time" meta:resourcekey="TemplateFieldResource2">
                                            <ItemTemplate>
                                                <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblTimeResource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Time" meta:resourcekey="TemplateFieldResource3">
                                            <ItemTemplate>
                                                <asp:Label ID="LblEndTime" runat="server" Text='<%# Eval("a_end_time") %>' meta:resourcekey="LblEndTimeResource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Patient Name" meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <asp:Label ID="LblPatientName" runat="server" Text='<%# Eval("name") %>' meta:resourcekey="LblPatientNameResource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Diagnose" meta:resourcekey="TemplateFieldResource5">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDiagnose" runat="server" Text='<%# Eval("a_doc_daignose") %>' meta:resourcekey="LblDiagnoseResource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Prescription" meta:resourcekey="TemplateFieldResource6">
                                            <ItemTemplate>
                                                <asp:Label ID="LblPrescription" runat="server" Text='<%# Eval("a_doc_prescriptions") %>' meta:resourcekey="LblPrescriptionResource1"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#DDDDDD" Font-Bold="True" ForeColor="#18BC9C" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                                        </div>
                                </div>
                            </div>
                           
                        </div>
                    </div>
             
            </div>
                  </asp:Panel>
    </div>


  <%--        <asp:Button ID="btnForAjax" runat="server" Text="" Style="display: none" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose">
            </ajaxToolkit:ModalPopupExtender>

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
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left:45%">  <asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

          <%--To date must be greater than or equal to from date--%>

<%--            <asp:Button ID="btnForAjax1" runat="server" Text="" Style="display: none" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left:45%">  <asp:Button ID="Button1" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

          <%--To date must be greater than or equal to from date--%>

          <%--No Data..--%>
      <%--           <asp:Button ID="btnForAjax2" runat="server" Text="" Style="display: none" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg2"
                TargetControlID="btnForAjax2" CancelControlID="btnclose2">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg2" runat="server" CssClass="modalPopup">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose2" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%">  <asp:Button ID="Button3" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

          <%--No Data..--%>


<%--No Data..--%>
           <%--         <asp:Button ID="btnForAjax3" runat="server" Text="" Style="display: none" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3">
            </ajaxToolkit:ModalPopupExtender>

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
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%">  <asp:Button ID="Button5" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
          <%--No Data..--%>

          <%--No Data..--%>
           <%--         <asp:Button ID="btnForAjax4" runat="server" Text="" Style="display: none" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg4"
                TargetControlID="btnForAjax4" CancelControlID="btnclose4">
            </ajaxToolkit:ModalPopupExtender>

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
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%">  <asp:Button ID="Button6" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
          <%--No Data..--%>

               <%--No Data..--%>
           <%--         <asp:Button ID="btnForAjax5" runat="server" Text="" Style="display: none" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg5"
                TargetControlID="btnForAjax5" CancelControlID="btnclose5">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg5" runat="server" CssClass="modalPopup">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose5" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%">  <asp:Button ID="Button7" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" /></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
          <%--No Data..--%>





        </div>

</asp:Content>

