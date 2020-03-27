<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="read_report_form.aspx.cs" Inherits="BookDoc_Admin_read_report_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
      <script src="../Design/dist/js/app.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-header">
                       <h3 class="box-title"> <asp:Label ID="Label2" runat="server" Text="Patient and doctor details"></asp:Label></h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered" AutoGenerateRows="False">
                                <Fields>
                                    <asp:TemplateField HeaderText="Patient name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Patient hakkeem id">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("u_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doctor name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doctor hakkeem id">
                                        <ItemTemplate>
                                            <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hospital name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hospital hakkeem id">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("h_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Fields>
                            </asp:DetailsView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title"><asp:Label ID="Label1" runat="server" Text="Appointment details"></asp:Label></h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:DetailsView ID="DetailsView2" runat="server" CssClass="table table-bordered" AutoGenerateRows="False">
                                <Fields>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" Visible="false" runat="server" Text='<%# Bind("h_id") %>'></asp:Label>
                                            <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time">
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header">
                       <h3 class="box-title"><asp:Label ID="Label3" runat="server" Text="Report details"></asp:Label></h3> 
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <h4 style="color:black;font-weight:bold;">
                                <asp:Label ID="Label4" runat="server" Text="Reason"></asp:Label>

                            </h4>
                            <p>
                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="form-group">
                            <h4 style="color:black;font-weight:bold;">
                                <asp:Label ID="Label6" runat="server" Text="Description"></asp:Label>
                            </h4>
                            <p>
                                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                       <div class="form-group pull-right">
                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-sm btn-primary" runat="server">Verified</asp:LinkButton>
                        </div>
                    
                    </div>
                   
                </div>
            </div>
        </div>
    </div>
</asp:Content>

