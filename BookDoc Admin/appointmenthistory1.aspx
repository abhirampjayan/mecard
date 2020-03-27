<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="appointmenthistory1.aspx.cs" Inherits="BookDoc_Admin_appointmenthistory" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
                   <h3 class="box-title"> <asp:Label ID="Label8" runat="server" Text="Appointment histories" meta:resourcekey="Label8Resource1"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" meta:resourcekey="GridView1Resource1">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource7"><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Doctor" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1"  runat="server" Text='<%# Bind("d_id") %>'></asp:Label>
                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>

                              

                                <asp:TemplateField HeaderText="Hospital" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("h_id") %>' ></asp:Label>
                                        <asp:Label ID="Label4" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Patient" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5"  runat="server" Text='<%# Bind("c_id") %>'></asp:Label>
                                        <asp:Label ID="Label6" runat="server" ></asp:Label>
                                    </ItemTemplate> <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("a_date") %>'></asp:Label>
                                        
                                    </ItemTemplate> <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("a_time") %>'></asp:Label>
                                      
                                    </ItemTemplate> <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource6">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" Visible="False" runat="server" Text='<%# Bind("a_status") %>' ></asp:Label>
                                        <asp:Label ID="Label12" runat="server"></asp:Label>
                                    </ItemTemplate> <HeaderStyle BackColor="#F0F0F0" />
                                </asp:TemplateField>
                            </Columns>
                           <HeaderStyle BackColor="#F0F0F0" />
                        </asp:GridView>
                    </div>
                </div>
            </div>


        </div>
    </div>
</asp:Content>

