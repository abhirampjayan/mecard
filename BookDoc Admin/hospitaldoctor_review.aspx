<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="hospitaldoctor_review.aspx.cs" Inherits="BookDoc_Admin_hospitaldoctor_review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="box box-primary">
    <div class="box-body">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="13" OnDataBound="GridView1_DataBound" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" >
                        <Columns>
                             <asp:TemplateField HeaderText="Sl.No" ><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                             <asp:TemplateField HeaderText="Hospital Name" >
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("h_name") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doctor Name" >
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("hd_name") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Patient Name">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Review" >
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("u_review") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active / Inactive" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Bind("reid") %>' CommandName="blk"  Text="Inactive"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument='<%# Bind("reid") %>' CommandName="ublk"  Text="Active"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                        </Columns>
                        <HeaderStyle BackColor="#F0F0F0" />
                        <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
                    </asp:GridView>
                </div>
            </div></div>
     <script src="../js/app.min.js"></script>
</asp:Content>


