<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Del_Doctors.aspx.cs" Inherits="Hospital_Del_Doctors" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

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


        <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
  


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content">
     <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="Label5" runat="server" Text="Removed Doctors" meta:resourcekey="Label5Resource1"></asp:Label>
            </h3>
            
        </div>
        <div class="box-body">

             <div class="table-responsive">
            <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" AllowPaging="True" PageSize="20" runat="server" AutoGenerateColumns="False" BackColor="White" OnPageIndexChanging="GridView1_PageIndexChanging" meta:resourcekey="GridView1Resource1" >
                <Columns>
                     <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource7"  ><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Doctor name" meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("hd_name") %>' meta:resourcekey="Label1Resource1" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact" meta:resourcekey="TemplateFieldResource4" >
                        <ItemTemplate>
                          <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_contact") %>' meta:resourcekey="Label4Resource1" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Removed Date and Time" meta:resourcekey="TemplateFieldResource5" >
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("delete_date_and_time") %>' meta:resourcekey="Label5Resource2" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Reason" meta:resourcekey="TemplateFieldResource6"  >
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("reason") %>' meta:resourcekey="Label6Resource1" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                  <HeaderStyle BackColor="#F0F0F0" />
                        <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
            </asp:GridView></div>
        </div>
    </div>
        </section>
    <style>
        .ui-autocomplete {
            width: 74.5% !important;
            height:auto;
        }
    </style>
</asp:Content>



