<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="ApointmentDetails.aspx.cs" Inherits="Hospital_ApointmentDetails" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
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
        <div style="margin-top: 2%">
                <div>
                 <div class="col-md-12 col-lg-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title" style="color:#18bc9c">
                                <asp:Label ID="Label2" runat="server" Text="Appointment Details" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <%--<asp:DataList ID="DataList1" CssClass="table" runat="server" RepeatColumns="5"></asp:DataList>--%>
                                <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label3" runat="server" Text="Doctor Name" meta:resourcekey="Label3Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtDocName" runat="server" CssClass="form-control" ReadOnly="True" meta:resourcekey="TxtDocNameResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Date" meta:resourcekey="Label4Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtApntmtDate" ValidationGroup="bb" CssClass="form-control" runat="server"  ReadOnly="True" meta:resourcekey="TxtApntmtDateResource1"></asp:TextBox>
                                    <%--<span class="input-group-btn">
                                        <asp:Button ID="Button2" ValidationGroup="bb" CssClass="btn btn-success" runat="server" Text="Check availability" OnClick="Button1_Click" />
                                    </span>--%>
                                </div>
                                <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label5" runat="server" Text="Appointment time" meta:resourcekey="Label5Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtApointmentTime" runat="server" CssClass="form-control" ReadOnly="True" meta:resourcekey="TxtApointmentTimeResource1" ></asp:TextBox>
                                </div>
                                <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label6" runat="server" Text="Reason to visit" meta:resourcekey="Label6Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtReasonToVisit" runat="server" CssClass="form-control"  ReadOnly="True" meta:resourcekey="TxtReasonToVisitResource1"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label7" runat="server" Text="Patient Name" meta:resourcekey="Label7Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtPatient" runat="server" CssClass="form-control"  ReadOnly="True" meta:resourcekey="TxtPatientResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                     <label>
                                         <asp:Label ID="Label9" runat="server" Text="Payment option" meta:resourcekey="Label9Resource1"></asp:Label></label>
                                      <asp:TextBox ID="TxtPayment" runat="server" CssClass="form-control"  ReadOnly="True" meta:resourcekey="TxtPaymentResource1"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                  <div class="pull-right">
                                       <asp:Button ID="BtnCancelApointment" runat="server" Text="Cancel Appointment" CssClass="btn btn-primary" OnClick="BtnCancelApointment_Click" meta:resourcekey="BtnCancelApointmentResource1"/>
                                      <asp:Button ID="BtnConfirm" runat="server" Text="Confirm" CssClass="btn btn-primary" OnClick="BtnConfirm_Click" meta:resourcekey="BtnConfirmResource1"/>
                                  </div>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
               
        </div>
         <!--//model popup for alert-->

       <%-- <asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
              <Animations>
                    <OnShowing>
                        <FadeIn ForceLayoutInIE="false" Duration=".1" Fps="10" />
                    </OnShowing>
                    <OnShown>
                        <FadeIn ForceLayoutInIE="false" Duration=".1" Fps="10" />
                    </OnShown>
                    <OnHiding>
                        <FadeOut ForceLayoutInIE="false" Duration=".2" Fps="20" />
                    </OnHiding>
                    <OnHidden>
                        <FadeOut ForceLayoutInIE="false" Duration=".2" Fps="10" />
                    </OnHidden>

                    </Animations>  
          
          </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label10" runat="server" Text="Hakkeem" meta:resourcekey="Label10Resource1"></asp:Label></h3>
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

        
             <%--    <asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender4" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label11" runat="server" Text="Hakkeem" meta:resourcekey="Label11Resource1"></asp:Label></h3>
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


    </div>
</asp:Content>

