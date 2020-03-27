﻿<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="HospitalRequest.aspx.cs" Inherits="BookDoc_Admin_HospitalRequest" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
        function getConfirmation(sender, title, message) {
            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>
    <style>
        .gridview {
            background-color: #fff;
            padding: 2px;
            margin: 2% auto;
        }

            .gridview a {
                margin: auto 1%;
                border-radius: 50%;
                background-color: #4aa9af;
                padding: 5px 10px 5px 10px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: rgba(199, 198, 198, 0.28);
                    color: #4aa9af;
                }

            .gridview span {
                background-color: #fff;
                color: #4aa9af;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../js/app.min.js"></script>
            <div id="modalPopUp" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                         <%-- <%if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                       <%-- <%}
    else
    { %>
                         <button type="button" class="close pull-left" data-dismiss="modal">&times;</button>
                        <%} %>--%>
                        <h4 class="modal-title">
                            <span id="spnTitle"></span>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <span id="spnMsg"></span>.
                        </p>
                    </div>
                    <div class="modal-footer">
                      <%--  <%if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="btnConfirm" class="btn btn-primary">
                            Yes, please</button>
                      <%--  <%}
    else
    { %>
                       <div class="pull-left">
                        <button type="button" id="btnConfirm" class="btn btn-primary">  نعم فعلا, رجاء</button>
                             <button type="button" class="btn btn-default" data-dismiss="modal">قريب</button>
                          </div>
                        <%} %>--%>
                    </div>
                </div>
            </div>
        </div>
     <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">
                      <asp:Label ID="Label5" runat="server" Text="Hospital Requests" meta:resourcekey="Label5Resource1"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                         <div class="table-responsive">
                        <asp:GridView ID="grvHospitals" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" OnRowCommand="grvHospitals_RowCommand" DataKeyNames="h_id" OnRowUpdating="grvHospitals_RowUpdating" meta:resourcekey="grvHospitalsResource1" OnRowDeleting="grvHospitals_RowDeleting"  >
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hospital Name" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("h_name") %>' meta:resourcekey="Label1Resource1"></asp:Label>
                                        <asp:Label ID="LblAgrementFile" runat="server" Text='<%# Eval("h_agreement") %>' Visible="False" meta:resourcekey="LblAgrementFileResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Registration No." meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("h_regno") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date and Time" meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("h_date_time") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("h_address") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Bind("h_id") %>' ForeColor="White" CommandName="update" meta:resourcekey="LinkButton1Resource1">Open</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource6">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to remove this Doctor?');" CommandName="delete" meta:resourcekey="LinkButton2Resource1" Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                             </div>
                    </div>
                    </div>

         </div>
</asp:Content>
