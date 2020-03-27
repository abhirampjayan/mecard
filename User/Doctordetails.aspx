<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="Doctordetails.aspx.cs" Inherits="Hospital_Doctor_details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <%-- <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../Design/plugins/slimScroll/jquery.slimscroll.min.js"></script>--%>
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
        <div style="margin-top:2%">

       
        <div class="row">
            <div class="col-md-12">
              
                <div class="box box-primary">
                <div class="box-header">
                      <div class="row">
                          <div class="col-lg-3">
                    <h3 class="box-title">Availabile Doctors in
                        <asp:Label ID="Label9" runat="server"></asp:Label>
&nbsp;</h3></div>
                            <div class="col-lg-6">
                     <asp:Image ID="Image1" runat="server" CssClass="img-bordered img-thumbnail  img-lg img-responsive" />
               </div>
                            </div>
                           </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Doctor name or Identification numer" runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Search" OnClick="Button1_Click" />
                            <asp:HiddenField ID="hfCustomerId" runat="server" />
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <div id="Div1">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Doctor image">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" CssClass="img-circle profile-user-img img-md img-responsive" ImageUrl='<%# Bind("hd_photo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                       Dr. <asp:Label ID="Label1" runat="server" Text='<%# Bind("hd_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specialty">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("hd_specialties") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="####">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Bind("hd_email") %>' CommandName="check">Check Availability</asp:LinkButton>
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
     <%-- <asp:Button ID="btnForAjax3" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button2_Click"/></span>
                            </div>
                        </div>
                    </div>

              
            </asp:Panel>--%>
            </section>
</asp:Content>

