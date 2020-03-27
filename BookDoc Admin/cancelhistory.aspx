<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="cancelhistory.aspx.cs" Inherits="BookDoc_Admin_appointmenthistory" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
    <div class="container-fluid">
        <div class="row">
            <div class="box">
                <div class="box-header">
                   <h3 class="box-title"> <asp:Label ID="Label8" runat="server" Text="Cancelled Histories" meta:resourcekey="Label8Resource1"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" meta:resourcekey="GridView1Resource1" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="17">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource7"><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate>
                                    <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Doctor" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" Visible="False" runat="server" Text='<%# Bind("doctor") %>' meta:resourcekey="Label1Resource1"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hospital" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" Visible="False" runat="server" Text='<%# Bind("hospital") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1"></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Patient" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" Visible="False" runat="server" Text='<%# Bind("patient") %>' meta:resourcekey="Label5Resource1"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Text="Label" meta:resourcekey="Label6Resource1"></asp:Label>
                                    </ItemTemplate> <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Appointment date&amp;time" meta:resourcekey="TemplateFieldResource9">
                                   
                                    <ItemTemplate>
                                         <asp:Label ID="Label10" runat="server" meta:resourcekey="Label10Resource1" Text='<%# Bind("apmntid") %>' Visible="False"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" meta:resourcekey="Label15Resource1"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                               
                               
                                <asp:TemplateField HeaderText="Reason" meta:resourcekey="TemplateFieldResource8">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("reason") %>' meta:resourcekey="Label13Resource1"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Appointment cancel date&time" meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("date") %>' meta:resourcekey="Label7Resource1"></asp:Label>
                                       <asp:Label ID="Label9" runat="server" Text='<%# Bind("time") %>' meta:resourcekey="Label9Resource1"></asp:Label>

                                    </ItemTemplate> <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Canceled by" meta:resourceKey="TemplateFieldResource6">
                                    <ItemTemplate>
                                       
                                        <asp:Label ID="Label11" runat="server" meta:resourcekey="Label11Resource1" Visible="False"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" meta:resourcekey="Label12Resource1"></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Visible="False" Text='<%# Bind("usertype") %>' meta:resourcekey="Label14Resource1"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                            </Columns>
                              <HeaderStyle BackColor="#F0F0F0" />
                        <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>


        </div>
    </div>
</asp:Content>

