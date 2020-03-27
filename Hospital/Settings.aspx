<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Hospital_Settings" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <% if (Session["Language"].ToString() == "Auto")
        {%>--%>
    <div class="container-fluid">
       <%-- <%}
            else
            {
        %>
        <div class="container-fluid" dir="rtl">
            <%} %>--%>
            <div style="margin-top: 2%">

                <div class="col-md-12">
                    <div class="row">
                        <div class="box box-primary">
                            <div class="box-header">
                                <h3 class="box-title">
                                    <asp:Label ID="Label1" runat="server" Text="Details" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <%--  <% if (Session["Language"].ToString() == "Auto")
                                          {%>--%>
                                    <div class="col-md-6">
                                       <%-- <%}
    else
    { %> <div class="col-md-6 pull-right">

                                        <%} %>--%>
                                        <div class="form-group table-responsive">

                                            <div class="form-group">
                                                <div class="col-md-6" style="padding-left: 0px;">
                                                    <label>
                                                        <asp:Label ID="Label3" runat="server" Text="Registration Number" meta:resourcekey="Label3Resource1"></asp:Label></label>
                                                    <asp:TextBox ID="TxtRegNo" runat="server" CssClass="form-control" ValidationGroup="a" ReadOnly="True" meta:resourcekey="TxtRegNoResource1"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6" style="padding-right: 0px;">
                                                    <label>
                                                        <asp:Label ID="Label4" runat="server" Text="Hakkeem Id" meta:resourcekey="Label4Resource1"></asp:Label></label>
                                                    <asp:TextBox ID="TxtHakkeemId" runat="server" CssClass="form-control" ValidationGroup="a" ReadOnly="True" meta:resourcekey="TxtHakkeemIdResource1"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label5" runat="server" Text="Hospital Name" meta:resourcekey="Label5Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please fill thid field" ForeColor="Red" ValidationGroup="a" ControlToValidate="TxtH_Name" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="TxtH_Name" runat="server" CssClass="form-control" ValidationGroup="a" ReadOnly="True" meta:resourcekey="TxtH_NameResource1"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label6" runat="server" Text="Email" meta:resourcekey="Label6Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Please fill thid field" ForeColor="Red" ValidationGroup="a" ControlToValidate="TxtEmail" Display="Dynamic" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" ControlToValidate="TxtEmail" runat="server" ErrorMessage="* Enter valid email" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator4Resource1"></asp:RegularExpressionValidator>
                                                </label>
                                                <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" ValidationGroup="a" ReadOnly="True" meta:resourcekey="TxtEmailResource1"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label7" runat="server" Text="Contact" meta:resourcekey="Label7Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Please fill thid field" Display="Dynamic" ForeColor="Red" ValidationGroup="a" ControlToValidate="TxtContact" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ControlToValidate="TxtContact" runat="server" ErrorMessage="* Enter valid number" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]+$" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                                </label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa"></i>
                                                    </div>
                                                    <asp:TextBox ID="TxtContact" runat="server" CssClass="form-control" ValidationGroup="a" ReadOnly="True"  meta:resourcekey="TxtContactResource1"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label9" runat="server" Text="Zip Code" meta:resourcekey="Label9Resource1"></asp:Label></label>
                                                <asp:TextBox ID="TxtZip" runat="server" CssClass="form-control" ValidationGroup="a" ReadOnly="True" meta:resourcekey="TxtZipResource1"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label10" runat="server" Text="City" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                                <asp:DropDownList ID="DdlCity" runat="server" CssClass="form-control" Enabled="False" meta:resourcekey="DdlCityResource1">
                                                   <%-- <asp:ListItem meta:resourcekey="ListItemResource1">Madhina</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource2">Makkah</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource3">Jiddah</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource4">Riyadh</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource5">Dammam</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label11" runat="server" Text="Address" meta:resourcekey="Label11Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Please fill thid field" ForeColor="Red" ValidationGroup="a" ControlToValidate="TxtAddress" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" CssClass="form-control" ValidationGroup="a" ReadOnly="True" meta:resourcekey="TxtAddressResource1"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="About" meta:resourcekey="Label12Resource1"></asp:Label></label>
                                                <asp:TextBox ID="TxtAbout" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine" ValidationGroup="a" ReadOnly="True" meta:resourcekey="TxtAboutResource1"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <div>
                                                    <asp:Button ID="BtnUpdate" runat="server" Text="Edit Details" CssClass="btn btn-sm btn-primary pull-right" OnClick="BtnUpdate_Click" meta:resourcekey="BtnUpdateResource1" />
                                                    <asp:Button ID="BtnSaveChanges" runat="server" Text="Save Changes" ValidationGroup="a" Visible="False" CssClass="btn btn-sm btn-primary" OnClick="BtnSaveChanges_Click" meta:resourcekey="BtnSaveChangesResource1" />
                                                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Visible="False" CssClass="btn btn-sm btn-default" ForeColor="#4AA9AF" OnClick="BtnCancel_Click" meta:resourcekey="BtnCancelResource1" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                                                 <%--  <% if (Session["Language"].ToString() == "Auto")
                                                       {%>--%>
                                                <div class="col-md-4">
                                                   <%-- <%}
    else
    { %>
                                                     <div class="col-md-4 pull-right">
                                                    <%} %>--%>
                                                    <div class="form-group">
                                                        <label>
                                                            <asp:Label ID="Label13" runat="server" Text="Hospital photo" meta:resourcekey="Label13Resource1"></asp:Label></label>
                                                        <asp:Image ID="Image1" AlternateText="Hospital Image" CssClass="img-responsive img-thumbnail" runat="server" meta:resourcekey="Image1Resource1" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" meta:resourcekey="FileUpload1Resource1" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="BtnChangFoto" CssClass="btn btn-sm btn-primary pull-right" runat="server" Text="Change photo" OnClick="BtnChangFoto_Click" meta:resourcekey="BtnChangFotoResource1" />
                                                </div>
                                            </asp:Panel>

                                        </div>
                                        <div class="col-md-12" style="margin-top: 2%">


                                            <asp:Panel ID="Panel2" runat="server" meta:resourcekey="Panel2Resource1">


                                                <div class="box box-primary">
                                                    <div class="box-header">
                                                        <h3 class="box-title">
                                                            <asp:Label ID="Label14" runat="server" Text="Change password" meta:resourcekey="Label14Resource1"></asp:Label>
                                                            <asp:Label ID="LblDocId" runat="server" Visible="False" Text="Label" meta:resourcekey="LblDocIdResource1"></asp:Label>

                                                        </h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="form-group">
                                                            <label>
                                                                <asp:Label ID="Label15" runat="server" Text="Current password" meta:resourcekey="Label15Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredValidator2" runat="server" ErrorMessage="* Please fill this field" ValidationGroup="b" ControlToValidate="TxtCurrentPass" ForeColor="Red" meta:resourcekey="RequiredValidator2Resource1"></asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="TxtCurrentPass" runat="server" CssClass="form-control" placeholder="Enter current password" TextMode="Password" meta:resourcekey="TxtCurrentPassResource1"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>
                                                                <asp:Label ID="Label16" runat="server" Text="New password" meta:resourcekey="Label16Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredValidator3" runat="server" ErrorMessage="* Please fill this field" ValidationGroup="b" ControlToValidate="TxtNewPass" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredValidator3Resource1"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="* Minimum 6 characters required" ControlToValidate="TxtNewPass" ForeColor="Red" ValidationGroup="b" Display="Dynamic" ValidationExpression="^[\s\S]{6,}$" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                                            <asp:TextBox ID="TxtNewPass" runat="server" CssClass="form-control" placeholder="Enter new password" TextMode="Password" meta:resourcekey="TxtNewPassResource1"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>
                                                                <asp:Label ID="Label17" runat="server" Text="Confirm new password" meta:resourcekey="Label17Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredValidator1" runat="server" ValidationGroup="b" ErrorMessage="* Please fill this field" ControlToValidate="TxtConfirmNew" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredValidator1Resource1"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password mismatched" ValidationGroup="b" ControlToValidate="TxtConfirmNew" ControlToCompare="TxtNewPass" ForeColor="Red" Display="Dynamic" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                                                            <asp:TextBox ID="TxtConfirmNew" runat="server" CssClass="form-control" placeholder="Enter new password" TextMode="Password" meta:resourcekey="TxtConfirmNewResource1"></asp:TextBox>


                                                        </div>

                                                        <div class="form-group">
                                                            <asp:Button ID="BtnChangePassword" runat="server" Text="Change password" ValidationGroup="b" CssClass="btn btn-sm btn-primary pull-right" OnClick="BtnChangePassword_Click" meta:resourcekey="BtnChangePasswordResource1" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>


                            </div>

                        </div>
                    </div>

                </div>


            </div>

         
            <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                     <%--<% if (Session["Language"].ToString() == "Auto")
                                         {%>--%>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                   <%-- <%}
    else
    { %>
                                     <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <%} %>--%>
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text="Hakkeem" meta:resourcekey="lblModalTitleResource1"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                      <%--   <% if (Session["Language"].ToString() == "Auto")
                                             {%>--%>
                                        <label>Please check your new email and enter your OTP to continue your updation process..</label>
                                       <%-- <%}
    else
    { %>
                                          <label>يرجى التحقق من بريدك الإلكتروني الجديد وإدخال مكتب المدعي العام لمتابعة عملية التحديث</label>
                                        <%} %>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="* Please enter OTP." ForeColor="Red" ControlToValidate="TxtOTP" ValidationGroup="cc" meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="TxtOTP" runat="server" CssClass="form-control" placeholder="Enter OTP here" meta:resourcekey="TxtOTPResource1"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                      <%--  <% if (Session["Language"].ToString() == "Auto")
                                            {%>--%>
                                        <div>
                                            <%--<%}
    else
    { %>
<div class="pull-left">
                                            <%} %>--%>
                                        <asp:Button ID="Button2" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmitOTP_Click" ValidationGroup="cc" meta:resourcekey="Button1Resource1" />
                                        <asp:Button ID="BtnResendOTP" runat="server" Text="Resend" CssClass="btn btn-primary" OnClick="BtnResendOTP_Click" meta:resourcekey="BtnResendOTPResource1" />
                                    </div>
                                        </div>
                                    <div class="text-center">
                                         <%-- <% if (Session["Language"].ToString() == "Auto")
                                              {%>--%>
                                        <a href="hospital.aspx">Or goto Home</a>
                                       <%-- <%}
    else
    { %>
                                          <a href="hospital.aspx?l=ar-EG">أو اذهب إلى الصفحة الرئيسية</a>
                                        <%} %>--%>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnResendOTP" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>



        </div>
</asp:Content>

