<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="hospital_doctor.aspx.cs" Inherits="BookDoc_Admin_hospital_doctor" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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


        <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
   <%-- <script type="text/javascript">

        //function CheckParts() {
        //    __doPostBack('', '');
        //};
        $(document).ready(function () {

            $("#<%=TextBox1.ClientID %>").autocomplete({


                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("../Service.asmx/GetHospitalDoctorName") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        width: '5%',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))

                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });

                },
                select: function (e, i) {
                    $("#<%=HiddenField1.ClientID %>").val(i.item.val);
                    $("#<%=TextBox1.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


            //

        });
    </script>--%>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="Label5" runat="server" Text="Hospital doctor" meta:resourcekey="Label5Resource1"></asp:Label>
            </h3>
            <div class="form-group">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <div class="input-group">
                    <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Hospital Hakkeem id or Doctor phone number" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" meta:resourcekey="Button1Resource1" OnClick="Button1_Click" />
                    </span>
                </div>
            </div>
             <div class="form-group">
                  
                    <div class="input-group">
                        <asp:RadioButtonList ID="rdb_status" CssClass="table-condensed" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rdb_statusResource1" OnSelectedIndexChanged="rdb_status_SelectedIndexChanged" AutoPostBack="True">
                             <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource7"></asp:ListItem>
                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource8"></asp:ListItem>
                        </asp:RadioButtonList>
                        <span class="input-group-btn">
                           
                        </span>
                    </div>
                    
               </div>
        </div>
        <div class="box-body">

             <div class="table-responsive">
            <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" AllowPaging="True" PageSize="13" runat="server" AutoGenerateColumns="False" BackColor="White" OnPageIndexChanging="GridView1_PageIndexChanging" OnDataBound="GridView1_DataBound" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" meta:resourcekey="GridView1Resource1">
                <Columns>
                     <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource6"><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="Doctor name" meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("hd_name") %>' meta:resourcekey="Label1Resource1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hospital name" meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("h_name") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hospital Hakkeem id" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("h_id") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact" meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_contact") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active / Inactive" meta:resourcekey="TemplateFieldResource5">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("hd_email") %>' Visible="False" meta:resourcekey="Label6Resource1"></asp:Label>
                            <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                  <HeaderStyle BackColor="#F0F0F0" />
                        <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
            </asp:GridView></div>
        </div>
    </div>
    <style>
        .ui-autocomplete {
            width: 74.5% !important;
            height:auto;
        }
    </style>
</asp:Content>

