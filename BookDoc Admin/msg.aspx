<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="msg.aspx.cs" Inherits="BookDoc_Admin_msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content">
           <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">
                    <asp:Label ID="Label1" runat="server" Text="Messages from doctors" ></asp:Label>

                </h3>
                </div>
                <div class="box-body">
    <asp:GridView ID="GridView2" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="13" OnPageIndexChanging="GridView2_PageIndexChanging"  OnRowCommand="GridView2_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource8">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Doctor HakkeemID" meta:resourcekey="TemplateFieldResource1">
                <ItemTemplate>
                   
                    <asp:LinkButton ID="LinkButton6" Text='<%# Bind("hakkimid") %>' CommandArgument='<%# Bind("hakkimid") %>' CommandName="open1" runat="server"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Message" meta:resourcekey="TemplateFieldResource2">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("message") %>' meta:resourcekey="Label13Resource1"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Posted On" meta:resourcekey="TemplateFieldResource3">
                <ItemTemplate>
                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("date") %>' meta:resourcekey="Label10Resource1"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
         
            <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource6">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton9" runat="server"  CommandName="del" CommandArgument='<%# Bind("id") %>'  Text="Remove"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#F0F0F0" />
        <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
    </asp:GridView>
</div>
               </div></div>
</asp:Content>

