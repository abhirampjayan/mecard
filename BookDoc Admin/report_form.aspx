<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="report_form.aspx.cs" Inherits="BookDoc_Admin_report_form" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">
                    <asp:Label ID="Label1" runat="server" Text="Patient reports" meta:resourcekey="Label1Resource1" ></asp:Label>
                </h3>
            </div>
            <div class="box-body">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" meta:resourcekey="GridView1Resource1">
                        <Columns>
                             <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource5"><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Patient name" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text="Label" meta:resourcekey="Label8Resource1"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Visible="False" Text='<%# Bind("u_id") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Visible="False" Text='<%# Bind("apmnt_id") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doctor name" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text="Label" meta:resourcekey="Label4Resource1"></asp:Label>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Hospital name" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Visible="False" Text='<%# Bind("h_id") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                    <asp:Label ID="Label7" runat="server" Text="Label" meta:resourcekey="Label7Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Report" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="open" CommandArgument='<%# Bind("id") %>' meta:resourcekey="LinkButton4Resource1" Text="Open"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#F0F0F0" />
                    </asp:GridView>
                </div>
            </div>
        </div>
       
    </section>
</asp:Content>

