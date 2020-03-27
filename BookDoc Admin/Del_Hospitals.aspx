<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Del_Hospitals.aspx.cs" Inherits="BookDoc_Admin_Del_Hospitals" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../js/app.min.js"></script>
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

     <script type="text/javascript">
        function getConfirmation(sender, title, message) {
            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>

       <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="content">

         



        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">
                    <asp:Label ID="Label1" runat="server" Text="Removed Hospitals" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                
            </div>
            <div class="box-body">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"  AllowPaging="True" PageSize="20" meta:resourcekey="GridView1Resource1" >
                        <Columns>
                             <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource9"><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Hospital name" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("h_name") %>' meta:resourcekey="Label9Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hakkeem id" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("h_hakkimid") %>' meta:resourcekey="Label10Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reg.No" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("h_regno") %>' meta:resourcekey="Label11Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("h_contact") %>' meta:resourcekey="Label12Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("h_email") %>' meta:resourcekey="Label13Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City" meta:resourcekey="TemplateFieldResource6">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("h_city") %>' meta:resourcekey="Label14Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Removed Date and Time" meta:resourcekey="TemplateFieldResource7" >
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("date") %>' meta:resourcekey="Label15Resource1" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" meta:resourcekey="TemplateFieldResource8" >
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("reason") %>' meta:resourcekey="Label16Resource1" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#F0F0F0" />
                        <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
                    </asp:GridView>
                </div>
            </div>
        </div>
          <div>

        


            </div>
    </div>
    
    <style>
        .ui-autocomplete {
            width: 72.5% !important;
            height:auto;
        }
    </style>
</asp:Content>

