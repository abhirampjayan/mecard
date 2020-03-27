<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Hospital.aspx.cs" Inherits="BookDoc_Admin_Hospital" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
    <%--<script type="text/javascript">

        //function CheckParts() {
        //    __doPostBack('', '');
        //};
        $(document).ready(function () {

            $("#<%=TextBox1.ClientID %>").autocomplete({


                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("../Service.asmx/GetHospitalName") %>',
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content">

         <div id="modalPopUp" class="modal fade" role="dialog">
            <div class="modal-dialog">
                     <div class="modal-content">
                    <div class="modal-header">
                       <%--   <%if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                       <%-- <%}
    else
    { %>
                         <button type="button" class="close pull-left" data-dismiss="modal">&times;</button>
                        <%} %>--%>
                        <h4 class="modal-title">
                            <span id="spnTitle"></span>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <span id="spnMsg"></span>.
                        </p>
                    </div>
                    <div class="modal-footer">
                      <%--  <%if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="btnConfirm" class="btn btn-primary">
                            Yes, please</button>
                     <%--   <%}
    else
    { %>
                       <div class="pull-left">
                        <button type="button" id="btnConfirm" class="btn btn-primary">  نعم فعلا, رجاء</button>
                             <button type="button" class="btn btn-default" data-dismiss="modal">قريب</button>
                          </div>
                        <%} %>--%>
                    </div>
                </div>
            </div>
        </div>



        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">
                    <asp:Label ID="Label1" runat="server" Text="Hospitals" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                <div class="form-group">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <div class="input-group">
                        <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Hakkeem id or Phone number" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search" meta:resourcekey="Button1Resource2" OnClick="Button1_Click1" />
                        </span>
                    </div>
                </div>
                 <div class="form-group">
                  
                    <div class="input-group">
                        <asp:RadioButtonList ID="rdb_status" CssClass="table-condensed"  runat="server" RepeatDirection="Horizontal" meta:resourcekey="rdb_statusResource1" OnSelectedIndexChanged="rdb_status_SelectedIndexChanged" AutoPostBack="True">
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
                    <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True" PageSize="13" meta:resourcekey="GridView1Resource1" OnDataBound="GridView1_DataBound" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                             <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource9"><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Hospital name" meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Visible="false" Text='<%# Bind("h_name") %>' meta:resourcekey="Label9Resource1"></asp:Label>
                                    <asp:LinkButton ID="LinkButton2" Text='<%# Bind("h_name") %>' CommandName="open" CommandArgument='<%# Bind("h_hakkimid") %>' runat="server"></asp:LinkButton>
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
                            <asp:TemplateField HeaderText="Active / Inactive" meta:resourcekey="TemplateFieldResource8">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="blk" CommandArgument='<%# Bind("h_hakkimid") %>' meta:resourcekey="LinkButton4Resource1" Text="Inactive"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="ublk" CommandArgument='<%# Bind("h_hakkimid") %>' meta:resourcekey="LinkButton5Resource1" Text="Active"></asp:LinkButton>
                                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("h_hakkimid") %>' Visible="False" meta:resourcekey="Label15Resource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource7">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Bind("h_hakkimid") %>'   CommandName="delete" meta:resourcekey="LinkButton1Resource1" Text="Remove"></asp:LinkButton>
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

               <%-- <asp:Button ID="btnForAjax5" runat="server" Style="display: none" meta:resourcekey="btnForAjax5Resource1" />
                <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg5"
                    TargetControlID="btnForAjax5" CancelControlID="btnclose5" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
                </ajaxtoolkit:modalpopupextender>

                <asp:Panel ID="pnlPopupMsg5" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg5Resource1">

                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform: capitalize">
                                <asp:Label ID="Label23" runat="server" Text="Hakkeem" meta:resourcekey="Label23Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose5" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <span style="text-transform: initial">
                                    <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left: 45%">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-xs" Text="Confirm" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" /></span>
                                <span>
                                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btn btn-success btn-xs" OnClick="BtnCancel_Click" meta:resourcekey="BtnCancelResource1" /></span>
                            </div>
                        </div>
                    </div>


                </asp:Panel>--%>


            </div>
    </div>
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                           
                          <%--   <%if (Session["Language"].ToString() == "Auto")
                                         { %>--%>
                             <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                     <h4 class="modal-title">
                                        <p>Please confirm</h4>
                                      <%--  <%}
    else
    { %> <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                       <h4 class="modal-title" dir="rtl">
                                            <p>يرجى تأكيد</h4>
                                            <%} %>--%>



                           
                                                          
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:TextBox ID="TextBox2" CssClass="form-control" TextMode="MultiLine" placeholder="Enter reason for remove" runat="server" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="Required" ValidationGroup="reg" ControlToValidate="TextBox2" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="Button2" CssClass="btn btn-sm btn-primary" UseSubmitBehavior="False"  ValidationGroup="reg" runat="server" Text="Remove" OnClick="Button2_Click" meta:resourcekey="Button2Resource1"  />
                            </div>
                        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />

                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <style>
        .ui-autocomplete {
            width: 72.5% !important;
            height:auto;
        }
    </style>
</asp:Content>

