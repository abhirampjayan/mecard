<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" UICulture="en-US" Culture="en-US" AutoEventWireup="true" CodeFile="Posted reviews.aspx.cs" Inherits="User_Posted_reviews" meta:resourcekey="PageResource2" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

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

    <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content" style="margin-bottom: 1cm; margin-top: 1.5cm;">
        <div class="container">
            <div class="row">
                <div style="margin-top: 1%">

                    <div class="box box-primary">
                        <div class="box-header">
                            <div class="form-group">


                                <%--<div class="input-group">
                            <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Enter doctor name" runat="server"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="Button1" CssClass="btn btn-warning" runat="server" Text="Search" OnClick="Button1_Click" />
                            </span>
                        </div>--%>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="form-group">
                                <asp:GridView ID="GridView1" CssClass="table table-responsive table-hover table-bordered" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" meta:resourcekey="GridView1Resource2">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Doctor details" meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>

                                                <div class="form-group">
                                                    <asp:Image ID="Image1" runat="server" AlternateText="Photo" CssClass="img-circle img-responsive img-md" meta:resourcekey="Image1Resource2" />
                                                </div>


                                                <div class="form-group pull-left" style="margin-left: 10px; font-weight: bold">
                                                    Dr.
                                          <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource2"></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("d_email") %>' Visible="False" meta:resourcekey="Label2Resource2"></asp:Label>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("his_id") %>' Visible="False" meta:resourcekey="Label5Resource2"></asp:Label>
                                                </div>




                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Consulting details" meta:resourcekey="TemplateFieldResource5">
                                            <ItemTemplate>
                                                <div class="pull-left">
                                                    <asp:Label ID="Label8" runat="server" Text="Date:" meta:resourcekey="Label8Resource1"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource2"></asp:Label><br />
                                                    <asp:Label ID="Label9" runat="server" Text="Time:" meta:resourcekey="Label9Resource1"></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource2"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feedback" meta:resourcekey="TemplateFieldResource6">
                                            <EditItemTemplate>
                                                <div class="pull-left">
                                                    <asp:TextBox ID="TextBox2" CssClass="form-control" Rows="3" TextMode="MultiLine" Text='<%# Bind("u_review") %>' runat="server" meta:resourcekey="TextBox2Resource2"></asp:TextBox>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" meta:resourcekey="LinkButton2Resource3">Cancel</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" meta:resourcekey="LinkButton1Resource3">Update</asp:LinkButton>
                                                    <asp:Label ID="Label6" runat="server" Visible="False" Text='<%# Bind("id") %>' meta:resourcekey="Label6Resource3"></asp:Label>
                                                </div>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <div class="pull-left">
                                                    <div>
                                                        <p>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("u_review") %>' meta:resourcekey="Label7Resource3"></asp:Label>
                                                        </p>

                                                    </div>
                                                    <asp:Label ID="Label6" runat="server" Visible="False" Text='<%# Bind("id") %>' meta:resourcekey="Label6Resource4"></asp:Label>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" meta:resourcekey="LinkButton2Resource4">Edit</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" meta:resourcekey="LinkButton1Resource4">Delete</asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#f7f6f3" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#4aa9af" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <h3>
                                    <asp:Label ID="Label7" runat="server" ForeColor="#4AA9AF" Text="---No posted reviews yet---" meta:resourcekey="Label7Resource4"></asp:Label></h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--//model popup for alert-->

            <div class="modal fade" id="myModal1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                  <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                        { %>--%>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">Hakkeem</h4>
                                   <%-- <%}
                                    else
                                    { %>
                                    <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title pull-right">حكيم</h4>
                                    <%} %>--%>
                                </div>
                              <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <div class="modal-body">

                                   <%-- <%}
                                    else
                                    { %>
                                    <div class="modal-body" dir="rtl">

                                        <%} %>--%>
                                        <div class="form-group">
                                            <p>
                                               <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                                     { %>--%>
                                                Do you want update this feedback?
                                               <%-- <%}
    else
    { %>
                                                هل تريد تحديث هذه التعليقات؟
                                                <%} %>--%>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="Button1" CssClass="btn btn-sm btn-primary" UseSubmitBehavior="false" runat="server" Text="Confirm" OnClick="Button1_Click" />
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </ContentTemplate>
                         <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                             
                            </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>


             <div class="modal fade" id="myModal2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal2" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                   <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                        { %>--%>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">Hakkeem</h4>
                                 <%--   <%}
                                    else
                                    { %>
                                    <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title pull-right">حكيم</h4>
                                    <%} %>--%>
                                </div>
                              <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <div class="modal-body">

                                  <%--  <%}
                                    else
                                    { %>
                                    <div class="modal-body" dir="rtl">

                                        <%} %>--%>
                                        <div class="form-group">
                                            <p>
                                                <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                                     { %>--%>
                                                Do you want delete this feedback?
                                               <%-- <%}
    else
    { %>
                                                هل تريد حذف هذه التعليقات؟
                                                <%} %>--%>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="Button2" CssClass="btn btn-sm btn-primary" UseSubmitBehavior="false" runat="server" Text="Confirm" OnClick="Button2_Click" />
                                        </div>
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

            <%-- <asp:Button ID="btnForAjax3" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3">
            </ajaxtoolkit:modalpopupextender>

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
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
            <!--//modal popup-->
        </div>
    </section>
    <%-- <style type="text/css">
        #copy {
            width: 100%;
            padding: 20px 0;
            position: absolute;
            z-index: 1000000;
            color: #fff;
            background: #313131;
            /* margin-top: 6cm; */
            bottom: 0;
        }
          .main-header ul li a {
    text-decoration: none;
    text-transform: uppercase;
    color: #000;
    font-size: 17px;
    font-weight: 500;
    text-shadow: none;
    padding: 9px 15px;
    display: block;
    outline: none;
    letter-spacing: 0.1px;
}
    </style>--%>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />


</asp:Content>

