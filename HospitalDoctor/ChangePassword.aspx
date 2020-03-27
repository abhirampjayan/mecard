<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalDoctor/HospitalDoctorMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="HospitalDoctor_ChangePassword" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <%-- <% if (Session["Language"].ToString() == "Auto")
             { %>--%>
        <div class="col-md-8">
          <%--  <%}
    else
    { %>
            <div class="col-md-8 pull-right" dir="rtl">
            <%} %>--%>
            <div style="margin-top: 10px;">
               
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Label ID="Label1" runat="server" Text="Change password" meta:resourcekey="Label1Resource1"></asp:Label>
                            <asp:label id="LblDocId" runat="server" visible="False" text="Label" meta:resourcekey="LblDocIdResource1"></asp:label>

                        </h3>
                    </div>
                    <div class="box-body">
                        <div class="form-group">

                            <div class="form-group">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Current password" meta:resourcekey="Label2Resource1"></asp:Label></label><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" errormessage="Please fill this field" validationgroup="b" controltovalidate="TxtCurrentPass" forecolor="Red" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:requiredfieldvalidator>
                                <asp:textbox id="TxtCurrentPass" runat="server" cssclass="form-control" placeholder="Enter current password" textmode="Password" meta:resourcekey="TxtCurrentPassResource1"></asp:textbox>
                            </div>
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="New password" meta:resourcekey="Label3Resource1"></asp:Label></label><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" errormessage="Please fill this field" validationgroup="b" controltovalidate="TxtNewPass" forecolor="Red" display="Dynamic" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:requiredfieldvalidator>
                                <asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" errormessage="* Minimum 6 characters required" controltovalidate="TxtNewPass" forecolor="Red" validationgroup="b" display="Dynamic" validationexpression="^[\s\S]{6,}$" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:regularexpressionvalidator>
                                <asp:textbox id="TxtNewPass" runat="server" cssclass="form-control" placeholder="Enter new password" textmode="Password" meta:resourcekey="TxtNewPassResource1"></asp:textbox>
                            </div>
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Confirm new password" meta:resourcekey="Label4Resource1"></asp:Label></label><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" validationgroup="b" errormessage="Please fill this field" controltovalidate="TxtConfirmNew" forecolor="Red" display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:requiredfieldvalidator>
                                <asp:comparevalidator id="CompareValidator1" runat="server" errormessage="Password mismatched" validationgroup="b" controltovalidate="TxtConfirmNew" controltocompare="TxtNewPass" forecolor="Red" display="Dynamic" meta:resourcekey="CompareValidator1Resource1"></asp:comparevalidator>
                                <asp:textbox id="TxtConfirmNew" runat="server" cssclass="form-control" placeholder="Enter new password" textmode="Password" meta:resourcekey="TxtConfirmNewResource1"></asp:textbox>
                            </div>

                            <div class="form-group">
                                <asp:button id="BtnChange" runat="server" text="Change password" validationgroup="b" cssclass="btn btn-success pull-right" onclick="BtnChange_Click" meta:resourcekey="BtnChangeResource1" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>

