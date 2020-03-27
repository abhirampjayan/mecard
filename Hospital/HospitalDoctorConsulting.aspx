<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="HospitalDoctorConsulting.aspx.cs" Inherits="Hospital_HospitalDoctorConsulting" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="container-fluid">
        <div style="margin-top: 2%">
            <div class="col-md-6">
                 <div class="col-md-12 col-lg-12">
                    <div class="box box-success box-solid">

                        <div class="box-header">
                            <asp:Image ID="Image1" CssClass="direct-chat-img img-responsive" AlternateText="Image" runat="server" meta:resourcekey="Image1Resource1" />
                             <h2 class="box-title pull-right"> <asp:Label ID="LblCurrentDate" runat="server" Text="Label1" meta:resourcekey="LblCurrentDateResource1"></asp:Label></h2>
                            <h3 class="box-title pull-right">Dr.<asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label>&nbsp; </h3>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick"></asp:Timer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="id" OnRowUpdating="GridView1_RowUpdating" meta:resourcekey="GridView1Resource1" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Si.No." meta:resourcekey="TemplateFieldResource1">
                                          <ItemTemplate>
                                             <%#Container.DataItemIndex+1 %>
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource2">
                                            <ItemTemplate>
                                                <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblTimeResource1"></asp:Label>
                                                <asp:Label ID="LblPatId" runat="server" Text='<%# Eval("u_id") %>' Visible="False" meta:resourcekey="LblPatIdResource1"></asp:Label>
                                              <asp:Label ID="LblReason" runat="server" Text='<%# Eval("a_reason") %>' Visible="False" meta:resourcekey="LblReasonResource1"></asp:Label>
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Patient Name" meta:resourcekey="TemplateFieldResource3">
                                            <ItemTemplate>
                                                <asp:Label ID="LblPatientName" runat="server" Text='<%# Eval("name") %>' meta:resourcekey="LblPatientNameResource1"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkConsult" runat="server" CommandName="update" CommandArgument='<%# Eval("u_id") %>' meta:resourcekey="LnkConsultResource1">Consult</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
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
               


            <div class="col-md-6">
                 <div class="col-md-12 col-lg-12">
                    <div class="box box-warning box-solid">
                        <div class="box-header">
                            <h3 class="box-title">Patient <asp:Label ID="LblApoId" runat="server" Visible="False" Text="Label" meta:resourcekey="LblApoIdResource1"></asp:Label>
                                <asp:Label ID="LblPatId" runat="server" Text="Label" Visible="False" meta:resourcekey="LblPatIdResource2"></asp:Label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <%--<asp:DataList ID="DataList1" CssClass="table" runat="server" RepeatColumns="5"></asp:DataList>--%>
                                <div class="form-group">
                                     <label>Appointment time</label>
                                    <asp:TextBox ID="TxtApointmentTime" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Appointment Time" meta:resourcekey="TxtApointmentTimeResource1"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                     <label>Patient Name</label>
                                    <asp:TextBox ID="TxtPatientName" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Patient Name" meta:resourcekey="TxtPatientNameResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                     <label>Reason to visit</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please fill this field" ControlToValidate="TxtReasonToVisit" ValidationGroup="b" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtReasonToVisit" runat="server" CssClass="form-control" ReadOnly="True" placeholder=" Reason" meta:resourcekey="TxtReasonToVisitResource1"></asp:TextBox>
                                    <%--<asp:DropDownList ID="TxtReasonToVisit" runat="server" CssClass="form-control" Enabled="false">
                                        <asp:ListItem>General</asp:ListItem>
                                        <asp:ListItem>Illness</asp:ListItem>
                                    </asp:DropDownList>--%>

                                </div>
                                <div class="form-group">
                                     <label>Diagnose</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please fill this field" ControlToValidate="TxtDiagnose" ForeColor="Red"  ValidationGroup="b" meta:resourcekey="RequiredFieldValidator2Resource1" ></asp:RequiredFieldValidator>
                                    <%--<asp:DropDownList ID="DdlPayments" runat="server" CssClass="form-control" Enabled="false">
                                        <asp:ListItem>Payment my self</asp:ListItem>
                                    </asp:DropDownList>--%>
                                     <asp:TextBox ID="TxtDiagnose" runat="server" CssClass="form-control" Enabled="False"  placeholder=" Diagnosis" meta:resourcekey="TxtDiagnoseResource1"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                     <label>Prescription</label>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please fill this field" ValidationGroup="b" ControlToValidate="TxtPrescription" ForeColor="Red" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                     <asp:TextBox ID="TxtPrescription" runat="server" CssClass="form-control" Enabled="False"  placeholder="Prescription" TextMode="MultiLine" meta:resourcekey="TxtPrescriptionResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:LinkButton ID="LnkPrevious" runat="server" Enabled="False" CssClass="pull-left" OnClick="LnkPrevious_Click" meta:resourcekey="LnkPreviousResource1" >View previous history</asp:LinkButton>
                                      <asp:Button ID="BtnComplete" runat="server" Text="Complete" ValidationGroup="b" CssClass="btn btn-sm btn-primary pull-right" Enabled="False" OnClick="BtnComplete_Click" meta:resourcekey="BtnCompleteResource1"  />
                                
                                </div>
                              
                                  </div>
                        </div>
                    </div>
                </div>
            </div>
               
           <%--   <div>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnForAjax" runat="server" Text="" Style="display: none" />
                          
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg" 
                               TargetControlID="btnForAjax" CancelControlID="btnclose" >

                            </ajaxToolkit:ModalPopupExtender>

                            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup">
                              
                                <div class="col-md-12">
                                    <div class="box box-success box-solid">
                                        <div class="box-header">
                                            <h3 class="box-title">Patient history</h3>
                                            <div class="box-tools pull-right">
                                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div class="form-group">
                                                
                                            </div>
                                            
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>--%>

            
           
        </div>

                  <!--//model popup for alert-->

      <%--  <asp:Button ID="btnForAjax1" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1">
            </ajaxtoolkit:modalpopupextender>

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
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs"/></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        <!--//modal popup-->





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
     <%-- <asp:Button ID="btnForAjax3" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button1" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button1_Click"/></span>
                            </div>
                        </div>
                    </div>

              
            </asp:Panel>--%>

        <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text="Patient history" meta:resourcekey="lblModalTitleResource1"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" meta:resourcekey="GridView2Resource1">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource5">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource1"></asp:Label>
                                                                <asp:Label ID="LblHosId1" runat="server" Text='<%# Eval("h_id") %>' Visible="False" meta:resourcekey="LblHosId1Resource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hospital" meta:resourcekey="TemplateFieldResource6">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblHospital" runat="server" Text="Label" meta:resourcekey="LblHospitalResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doctor" meta:resourcekey="TemplateFieldResource7">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblDoctorName" runat="server" Text='<%# Eval("hd_name") %>' meta:resourcekey="LblDoctorNameResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Diagnosis" meta:resourcekey="TemplateFieldResource8">
                                                            <ItemTemplate>
                                                                 <asp:Label ID="LblDiagnose" runat="server" Text='<%# Eval("a_doc_daignose") %>' meta:resourcekey="LblDiagnoseResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prescription" meta:resourcekey="TemplateFieldResource9">
                                                            <ItemTemplate>
                                                                 <asp:Label ID="LblPrescription" runat="server" Text='<%# Eval("a_doc_prescriptions") %>' meta:resourcekey="LblPrescriptionResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button1_Click" UseSubmitBehavior="False" meta:resourcekey="Button2Resource1" />
                                </div>
                            </div>
                        </ContentTemplate>
                     <%--   <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
            </div>



</asp:Content>

