<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="AgreementUpload.aspx.cs" Inherits="Hospital_AgreementUpload" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <% if (Session["Language"].ToString() == "Auto")
         {%>--%>
     <div class="container-fluid">
      <%--   <%}
    else
    { %>
         <div class="container-fluid" dir="rtl">

         <%} %>--%>
    
     
        <div style="margin-top: 2%">


            <div class="col-md-12">
                <div class="row">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">
                                <asp:Label ID="Label5" runat="server" Text="Upload Signed Agreement" meta:resourcekey="Label5Resource1"></asp:Label>
                            </h3>
                            <asp:Label ID="LblH_RegnNo" runat="server" Text="Label" Visible="False" meta:resourcekey="LblH_RegnNoResource1"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" meta:resourcekey="FileUpload1Resource1" />

                            </div>
                            <div class="form-group">
                                <asp:Button ID="BtnUpload" runat="server" Text="Upload" CssClass="btn btn-sm btn-primary pull-right" OnClick="BtnUpload_Click" meta:resourcekey="BtnUploadResource1" />

                            </div>

                        </div>

                    </div>
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
                            <h3 class="box-title" style="text-transform:capitalize"><asp:Label ID="Label20" runat="server" Text="Hakkeem" meta:resourcekey="Label20Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" OnClick="BtnSubmitOTP_Click" meta:resourcekey="BtnSubmitOTPResource1" /></span> 
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
                            <h3 class="box-title" style="text-transform:capitalize"><asp:Label ID="Label3" runat="server" Text="Hakkeem" meta:resourcekey="Label3Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" meta:resourcekey="Button2Resource1" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>


        
              <%--   <asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender4" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize"><asp:Label ID="Label4" runat="server" Text="Hakkeem" meta:resourcekey="Label4Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label8" runat="server" Text="Label" meta:resourcekey="Label8Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button1" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button1_Click" meta:resourcekey="Button1Resource1"/></span>
                            </div>
                        </div>
                    </div>

              
            </asp:Panel>--%>
        <!--//modal popup-->
    </div>
</asp:Content>

