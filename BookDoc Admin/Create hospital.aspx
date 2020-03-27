<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Create hospital.aspx.cs" Inherits="BookDoc_Admin_Create_hospital" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /*#ContentPlaceHolder1_Label8 {
            margin-left: -15px;
        }*/

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
    <script src="../js/app.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">

        <!-- Bootstrap Modal Dialog -->
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                              <%--   <%if (Session["Language"].ToString() == "Auto")
                                     { %>--%>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                             <%--   <%}
    else
    { %>
                                 <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <%} %>--%>
                                <h4 class="modal-title">
                                   <%--  <%if (Session["Language"].ToString() == "Auto")
                                         { %>--%>
                                    <span>
                                      <%--  <%}
    else
    { %>
                                        <span class="">
                                        <%} %>--%>
                                    <asp:Label ID="lblModalTitle" runat="server" Text="Hakkeem" meta:resourcekey="lblModalTitleResource1"></asp:Label>
                                        </span>
                                        </h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                  <%-- <%if (Session["Language"].ToString() == "Auto")
                                       { %>--%>
                                <div>
                                  <%--  <%}
    else
    { %>
                                      <div class="pull-left>
                                    <%} %>--%>
                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="Button2_Click" UseSubmitBehavior="False" meta:resourcekey="Button2Resource1" />
                            </div>
                                </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>




        <%-- <%if (Session["Language"].ToString() == "Auto")
             { %>--%>
        <div class="col-md-12">
          <%--  <%}
    else
    { %>
            <div class="col-md-12 pull-right">
            <%} %>--%>
            <div class="box box-default">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label ID="Label3" runat="server" Text="Create hospital" meta:resourcekey="Label3Resource1"></asp:Label>
                        <asp:LinkButton ID="LinkCreate" runat="server" Visible="False" OnClick="LinkCreate_Click" meta:resourcekey="LinkCreateResource1">Or Create a new hospital</asp:LinkButton></h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="Hospital name" meta:resourcekey="Label4Resource1"></asp:Label></label><asp:RequiredFieldValidator ControlToValidate="hname" ID="RequiredFieldValidator1" ValidationGroup="a" runat="server" ErrorMessage="* Required" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="Hname" onkeyup="javascript:capitalize(this.id, this.value);" CssClass="form-control" placeholder="Hospital name" ValidationGroup="a" runat="server" meta:resourcekey="HnameResource1"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="Hospital registration number" meta:resourcekey="Label5Resource1"></asp:Label></label><asp:RequiredFieldValidator ControlToValidate="hregno" ForeColor="Red" ID="RequiredFieldValidator2" ValidationGroup="a" runat="server" ErrorMessage="* Required" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="hregno" CssClass="form-control" placeholder="Hospital registration number" ValidationGroup="a" runat="server" meta:resourcekey="hregnoResource1"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label6" runat="server" Text="Hospital address" meta:resourcekey="Label6Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="haddrs" ForeColor="Red" ValidationGroup="a" runat="server" ErrorMessage="* Required" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="haddrs" CssClass="form-control" TextMode="MultiLine" placeholder="Hospital address" ValidationGroup="a" runat="server" meta:resourcekey="haddrsResource1"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label7" runat="server" Text="Hospital contact number" meta:resourcekey="Label7Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="hcontact" ForeColor="Red" ValidationGroup="a" runat="server" ErrorMessage="* Required" Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ValidationExpression="^[0-9]+$" ID="RegularExpressionValidator2" ControlToValidate="hcontact" ForeColor="Red" ValidationGroup="a" runat="server" ErrorMessage="* Enter valid phone number" Display="Dynamic" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                        <div class="input-group">
                            <div class="input-group-addon">
                                  <i class="fa fa-contao"></i>
                            </div>
                            <asp:TextBox ID="hcontact" CssClass="form-control" placeholder="Enter a number" ValidationGroup="a" runat="server" meta:resourcekey="hcontactResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Hospital email address" meta:resourcekey="Label9Resource1"></asp:Label></label><asp:RegularExpressionValidator ControlToValidate="hemail" ValidationGroup="a" ID="RegularExpressionValidator3" runat="server" ErrorMessage="* Enter valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="hemail" CssClass="form-control" ValidationGroup="a" placeholder="Hospital email address" runat="server" meta:resourcekey="hemailResource1"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="About Hospital" meta:resourcekey="Label10Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="habout" ForeColor="Red" ValidationGroup="a" runat="server" ErrorMessage="* Required" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="habout" TextMode="MultiLine" Rows="3" CssClass="form-control" ValidationGroup="a" placeholder="About hospital" runat="server" meta:resourcekey="haboutResource1"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Hospital photo" meta:resourcekey="Label11Resource1"></asp:Label></label><asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" meta:resourcekey="FileUpload1Resource1" />
                    </div>
                    <div class="form-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" Visible="false" Text="Agreement" Target="_blank" meta:resourcekey="HyperLink1Resource1"></asp:HyperLink>
                        <asp:Panel ID="Panel1" runat="server" Visible="False" meta:resourcekey="Panel1Resource1">
                            <label>
                                <asp:Label ID="Label12" runat="server" Text="Agreement File" meta:resourcekey="Label12Resource1"></asp:Label><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Please upload agreement file" ForeColor="Red" ControlToValidate="FileUpload2" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>--%></label>
                            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control"  meta:resourcekey="FileUpload2Resource1" />
                        </asp:Panel>

                    </div>
                </div>
                <div class="box-footer">
                   <%--   <%if (Session["Language"].ToString() == "Auto")
                          { %>--%>
                    <div class="pull-right">
                       <%-- <%}
    else
    { %>
                         <div class="pull-left">
                        <%} %>--%>
                              <asp:Button ID="btn_cancel" runat="server"  CssClass="btn btn-primary" Style="margin:5px;" Text="Cancel" meta:resourcekey="btn_cancelResource1" OnClick="btn_cancel_Click"  />
                    <asp:Button ID="Button1" CssClass="btn btn-primary" ValidationGroup="a" runat="server" Text="Create hospital" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
                    </div>
                </div>
            </div>
        </div>

        <%--<div class="col-md-6">
            <div class="box box-default">
                <div class="box-body">
                    <div class="form-group">
                        <asp:Image ID="Image1" CssClass="img-responsive img-bordered" runat="server" meta:resourcekey="Image1Resource1" />
                    </div>
                </div>
            </div>
        </div>--%>



        <div>
        </div>

    </div>

    <script type="text/javascript">
        function capitalize(textboxid, str) {
            // string with alteast one character
            if (str && str.length >= 1) {
                var firstChar = str.charAt(0);
                var remainingStr = str.slice(1);
                str = firstChar.toUpperCase() + remainingStr;
            }
            document.getElementById(textboxid).value = str;
        }
    </script>
</asp:Content>

