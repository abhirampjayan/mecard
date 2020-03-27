<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="Hospital.aspx.cs" Inherits="User_Hospital" %>

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
      <section class="content">
    <div class="container-fluid">
        <div style="margin-top: 2%">


            <div class="row">
                <div class="col-md-6">
                    <div class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">
                                <asp:Label ID="hname" runat="server" Text="Label"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateRows="False">
                                    <Fields>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("h_address") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email address">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("h_email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("h_city") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact number">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("h_contact") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="About hospital">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("h_about") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Fields>
                                </asp:DetailsView>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="Button1" CssClass="btn btn-sm btn-warning" runat="server" Text="Check doctor availability" />
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">
                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <asp:Image ID="Image1" CssClass="img-responsive" runat="server" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
            </section>
</asp:Content>

