<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Doctor details.aspx.cs" Inherits="Hospital_Doctor_details" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
    <script src="../js/jquery.slimscroll.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '450px'

            });
        });
    </script>
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
    </style>
    <script type="text/javascript">
        function CheckParts() {
            __doPostBack('', '');
        };
        $(document).ready(function () {
            $("#<%=TextBox1.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetCustomers1") %>',
                    data: "{ 'prefix': '" + request.term + "'}",

                    dataType: "json",
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
                    $("#<%=hfCustomerId.ClientID %>").val(i.item.val);
                $("#<%=TextBox1.ClientID %>").val(i.item.label);
                CheckParts();
            },
                minLength: 1
            });




        });
    </script>
   <section class="content">
    <div class="container-fluid">
      
            <div style="margin-top: 2%">


                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header">
                                <h3 class="box-title">
                                    <asp:Label ID="Label3" runat="server" Text="Doctors" meta:resourcekey="Label3Resource1"></asp:Label></h3>
                            </div>
                            <div class="box-body">
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Doctor name or Identification numer" runat="server" AutoPostBack="True" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
                                            <asp:HiddenField ID="hfCustomerId" runat="server" />
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div id="Div1">
                                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" meta:resourcekey="GridView1Resource1">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Doctor image" meta:resourcekey="TemplateFieldResource1">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" CssClass="img-circle profile-user-img img-md img-responsive" ImageUrl='<%# (Eval("hd_photo") ?? "../Doctorimages/doctor.png") %>' meta:resourcekey="Image1Resource1" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource2">
                                                    <ItemTemplate>
                                                        Dr.
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("hd_name") %>' meta:resourcekey="Label1Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specialty" meta:resourcekey="TemplateFieldResource3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("hd_specialties") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="####" meta:resourcekey="TemplateFieldResource4">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Bind("hd_email") %>' CommandName="check" meta:resourcekey="LinkButton2Resource1" Text="Check"></asp:LinkButton>&nbsp;
                                      <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Bind("hd_email") %>' CommandName="del" meta:resourcekey="LinkButton3Resource1" Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


         


        </div>
       <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">

                                  
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">
                                        <p>
                                            Please confirm</h4>
                                   
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtreason" CssClass="form-control" TextMode="MultiLine" placeholder="Enter reason for remove" runat="server" meta:resourcekey="txtreasonResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="Required" ValidationGroup="reg" ControlToValidate="txtreason" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnclick" CssClass="btn btn-sm btn-primary" UseSubmitBehavior="False" ValidationGroup="reg" runat="server" Text="Remove" OnClick="btnclick_Click" meta:resourcekey="btnclickResource1" />
                                    </div>
                                </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnclick" EventName="Click" />

                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
       </section>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

        <!-- Latest compiled JavaScript -->
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
      
        <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />
      
</asp:Content>

