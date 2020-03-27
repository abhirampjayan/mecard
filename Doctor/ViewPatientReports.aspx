<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="ViewPatientReports.aspx.cs" Inherits="Doctor_ViewPatientReports" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="box box-primary">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                      <label style="margin-top:12px;font-size:22px;">
                          <asp:Label ID="Label3" runat="server" Text="Patient Name:" meta:resourcekey="Label3Resource1"></asp:Label> <asp:Label ID="LblUserName" runat="server" ForeColor="#503009" Font-Bold="True" meta:resourcekey="LblUserNameResource1"></asp:Label></label>  
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="form-group table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" HeaderStyle-BackColor="lightgray" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" meta:resourcekey="GridView1Resource1">
                            <Columns>
                                <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="LblDate" runat="server" Text='<%# Eval("date") %>' Visible="False" meta:resourcekey="LblDateResource1"></asp:Label>
                                        <asp:Label ID="LblDate1" runat="server" Text="Label" meta:resourcekey="LblDate1Resource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Blood Test" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HlnkBloodTest" runat="server" NavigateUrl='<%# Eval("blood_test_report") %>' Target="_blank" meta:resourcekey="HlnkBloodTestResource1">View Report</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Urine Test" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HlnkUrineTest" runat="server" NavigateUrl='<%# Eval("urine_test_report") %>' Target="_blank" meta:resourcekey="HlnkUrineTestResource1">View Report</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField >
                                <asp:TemplateField HeaderText="Scan Report" meta:resourcekey="TemplateFieldResource4" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="LnkScanReport" runat="server" NavigateUrl='<%# Eval("scan_test_report") %>' Target="_blank" meta:resourcekey="LnkScanReportResource1">View Report</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="X-ray Report" meta:resourcekey="TemplateFieldResource5" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="LnkXrayReport" runat="server" NavigateUrl='<%# Eval("xray_test_report") %>' Target="_blank" meta:resourcekey="LnkXrayReportResource1">View Report</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Report" meta:resourcekey="TemplateFieldResource6" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="LblOther" runat="server" Text='<%# Eval("other_test_name") %>' meta:resourcekey="LblOtherResource1"></asp:Label>
                                        </div>
                                        <div>
                                             <asp:HyperLink ID="LnkOtherReport" runat="server" NavigateUrl='<%# Eval("other_test_report") %>' Target="_blank" meta:resourcekey="LnkOtherReportResource1">View Report</asp:HyperLink>
                                        </div>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#dddddd" Font-Bold="True" ForeColor="#18bc9c" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="BtnComplete" runat="server" Text="Finish" CssClass="btn btn-primary pull-right" OnClick="BtnComplete_Click" style="margin-right:24px;margin-bottom:14px;" meta:resourcekey="BtnCompleteResource1"/>
                    </div>
                </div>
            </div>

        </div>

        <%--This patient haven't a nye shared reports.--%>

        <%--<asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label4" runat="server" Text="Hakkeem" meta:resourcekey="Label4Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                               <span style="margin-left:45%"><asp:Button ID="Button1" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" ValidationGroup="cc" meta:resourcekey="Button1Resource1" /></span>  
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        <%--This patient haven't a nye shared reports.--%>
        <%--<asp:Button ID="btnForAjax1" runat="server" Style="display: none" meta:resourcekey="btnForAjax1Resource1" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg1Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label5" runat="server" Text="Hakkeem" meta:resourcekey="Label5Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"><asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" ValidationGroup="cc" meta:resourcekey="Button2Resource1" /></span>  
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
    </div>
     <script src="../js/app.min.js"></script>
</asp:Content>

