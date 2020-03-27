<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="viewemail.aspx.cs" Inherits="BookDoc_Admin_Admin_Index" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../js/app.min.js"></script>    
   
    <div class="box box-default">
        <div class="box-body">

            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" meta:resourcekey="GridView1Resource1">
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                           <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email" meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("EmailID") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Date") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk" runat="server" Text="Remove" CommandArgument='<%# Bind("id") %>' CommandName="del" meta:resourcekey="lnkResource1"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>

        </div>
    </div>
  
</asp:Content>

