<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="Hospital doctors.aspx.cs" Inherits="User_Hospital_doctors" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
      <section class="content">
    <div class="container-fluid">
        <div class="row">
            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">

                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox ID="txtContactsSearch" CssClass="form-control" placeholder="Doctor name or Specialty" runat="server" ValidationGroup="cc" AutoPostBack="True" OnTextChanged="txtContactsSearch_TextChanged"></asp:TextBox>

                                <ajaxToolkit:AutoCompleteExtender runat="server" ServiceMethod="SearchCustomers"
                                    MinimumPrefixLength="2"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" BehaviorID="txtContactsSearch_AutoCompleteExtender" TargetControlID="txtContactsSearch" ID="txtContactsSearch_AutoCompleteExtender">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox ID="txtZipCodeSearch" CssClass="form-control" placeholder="City or Zipcode" runat="server" ValidationGroup="cc" AutoPostBack="True" OnTextChanged="txtZipCodeSearch_TextChanged"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender runat="server" ServiceMethod="SearchCity" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" BehaviorID="txtZipCodeSearch_AutoCompleteExtender" TargetControlID="txtZipCodeSearch" ID="txtZipCodeSearch_AutoCompleteExtender"></ajaxToolkit:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtLangSearch" AutoPostBack="true" CssClass="form-control" placeholder="Language" runat="server" ValidationGroup="cc" OnTextChanged="txtLangSearch_TextChanged"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender runat="server" MinimumPrefixLength="2"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" ServiceMethod="SearchLanguage" ServicePath="" DelimiterCharacters="" BehaviorID="txtLangSearch_AutoCompleteExtender" TargetControlID="txtLangSearch" ID="txtLangSearch_AutoCompleteExtender">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="Button4" CssClass="btn btn-sm btn-flat btn-github" runat="server" Text="Find" OnClick="Button4_Click" ValidationGroup="cc" />
                        </div>
                    </div>

                    <%--<h3 class="box-title">Find a Doctor</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>--%>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="Illness" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="Illness_SelectedIndexChanged">
                                    <asp:ListItem>Illness</asp:ListItem>
                                    <asp:ListItem Value="General">General consultation</asp:ListItem>
                                    <asp:ListItem Value="General Follow Up">General Follow Up</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <!-- /.form-group -->

                        </div>
                        <!-- /.col -->
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="AnyGender" CssClass="form-control" runat="server" Text="Any gender" OnClick="AnyGender_Click" />
                            </div>
                            <!-- /.form-group -->

                        </div>
                        <!-- /.col -->
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="Male" CssClass="form-control" runat="server" Text="Male" OnClick="Male_Click" />
                            </div>
                            <!-- /.form-group -->

                        </div>
                        <!-- /.col -->
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="Female" CssClass="form-control" runat="server" Text="Female" OnClick="Female_Click" />
                            </div>
                            <!-- /.form-group -->

                        </div>
                        <!-- /.col -->
                        <div class="col-md-3">
                            <div class="form-group">


                                <div class="box box-warning collapsed-box box-solid">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">More</h3>
                                        <div class="box-tools pull-right">
                                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                                        </div>
                                        <!-- /.box-tools -->
                                    </div>
                                    <!-- /.box-header -->
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    Languages:
                                                            <asp:DropDownList ID="DropDownList1" CssClass="" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                                <asp:ListItem>Any Language</asp:ListItem>
                                                                <asp:ListItem>English</asp:ListItem>
                                                                <asp:ListItem>Arabic</asp:ListItem>
                                                                <asp:ListItem>Malayalam</asp:ListItem>
                                                            </asp:DropDownList>
                                                </div>
                                                <div class="divider"></div>
                                                <div class="form-group">

                                                    <asp:Button ID="Button5" runat="server" CssClass="btn btn-flat btn-sm btn-warning" Text="Take appointment through hospital" OnClick="Button5_Click" />
                                                </div>
                                                <div class="divider"></div>
                                                <div class="form-group">
                                                    Day:
                                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-flat btn-sm btn-primary" Text="Any day" /><asp:Button ID="Button2" runat="server" CssClass="btn btn-flat btn-sm" Text="Today" /><asp:Button ID="Button3" runat="server" CssClass="btn btn-sm btn-flat btn-primary" Text="Next 3 days" />
                                                </div>
                                                <div class="divider"></div>
                                                <div class="form-group">
                                                    Other:
                                                            <asp:CheckBox ID="CheckBox3" AutoPostBack="true" Text="Sees Children" runat="server" OnCheckedChanged="CheckBox3_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <!-- /.box -->

                                <!-- /.col -->

                            </div>
                            <!-- /.form-group -->

                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.box-body -->
                <%--<div class="box-footer">
                        Visit <a href="https://select2.github.io/">Select2 documentation</a> for more examples and information about the plugin.
                    </div>--%>
            </div>
            <!-- /.box -->
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>


    <div>
        <div class="box box-warning">
            <%--<div class="box-header with-border">
                     
                        <h3 class="box-title">Find a Doctor</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>--%>
            <!-- /.box-header -->
            <div class="box-body" <%--style="background-color:lightgray"--%>>
                <div class="col-md-6">
                    <div class="form-group">Hospital doctors</div>
                    <div class="form-group">
                        <%-- <h2>
                                 <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h2>--%>
                        <asp:DataList ID="DataList2" CssClass="table table-hover" runat="server">
                            <ItemTemplate>
                                <div class="form-group">
                                    <div class="box box-warning box-solid">
                                        <div class="box-header with-border">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <h3 class="box-title"><b>Dr.
                                                   
                                                        <asp:LinkButton ID="LinkButton2" Text='<%# Bind("hd_name") %>' runat="server" CommandArgument='<%# Bind("hd_email") %>' CommandName="view"></asp:LinkButton></b></h3>
                                                </div>
                                                <div class="col-md-4">
                                                    <b>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("hd_specialties") %>'></asp:Label></b>
                                                </div>
                                                <div class="col-md-4">
                                                    <ajaxToolkit:Rating ID="Rating1" AutoPostBack="true" StarCssClass="glyphicon-star" WaitingStarCssClass="fa-star-half" EmptyStarCssClass="glyphicon-star-empty" FilledStarCssClass="glyphicon-star" runat="server" OnChanged="Rating1_Changed"></ajaxToolkit:Rating>

                                                </div>
                                            </div>
                                            <div class="box-tools pull-right">
                                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Image ID="Image1" CssClass="img-circle img-responsive img-lg" AlternateText="Doctor photo" runat="server" ImageUrl='<%# Bind("hd_photo") %>' />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>About doctor</label>
                                                        "<asp:Label ID="Label3" runat="server" Text='<%# Bind("hd_about_you") %>'></asp:Label>"<asp:LinkButton ID="LinkButton1" CommandName="view" CommandArgument='<%# Bind("hd_email") %>' runat="server">More</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                    </div>
                                                    <div class="form-group">
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Button ID="Button4" runat="server" CssClass="btn btn-warning pull-right" Text="Check availabilty" CommandName="doc" CommandArgument='<%# Bind("hd_email") %>' />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <asp:Label ID="Label4" CssClass="label label-success pull-right" runat="server" Text='<%# Bind("hd_location") %>'></asp:Label><asp:Label ID="Label5" runat="server" CssClass="label label-danger pull-right" Text='<%# Bind("hd_location") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
    </div>
          </section>
</asp:Content>

