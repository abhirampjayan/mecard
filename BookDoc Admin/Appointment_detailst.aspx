<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Appointment_detailst.aspx.cs" Inherits="BookDoc_Admin_Appointment_detailst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="box box-solid">
                    <div class="box-header">
                     <%-- <%   if (Session["Language"].ToString() == "Auto")
                          {%>--%>
                        <h3 class="box-title pull-left">
                           <%-- <%}
    else
    { %>
                             <h3 class="box-title pull-right">

                            <%} %>--%>
                            <asp:Label ID="Label2" runat="server" Text="Available Doctors" ></asp:Label></h3>
                       <%-- <%   if (Session["Language"].ToString() == "Auto")
                            {%>--%>
                            <div class="pull-right col-md-6">
                              <%--  <%}
    else
    { %>
                                <div class="pull-left col-md-6">
                                    <%} %>--%>
                            <div class="col-md-4">
                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control btn-xs" placeholder="Doctor name" runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged" meta:resourcekey="TextBox1Resource1"></asp:TextBox>--%>
                                <asp:DropDownList ID="DdlDoctors" AutoPostBack="True" CssClass="form-control" runat="server"  Width="150px" OnSelectedIndexChanged="DdlDoctors_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlhosdoctor" CssClass="form-control" runat="server" AutoPostBack="True"  Width="200px" OnSelectedIndexChanged="ddlhosdoctor_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <%--<asp:Button ID="Button4" CssClass="btn btn-success" runat="server" Text="Search"/>--%>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
            <!-- /.col -->

        </div>
       <div class="box-body">

                        <div class="form-group">
                            <div id="Div1" class="table-responsive">


                                <asp:DataList ID="DataList2" CssClass="table" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <div class="box box-primary box-solid">
                                            <div class="box-header with-border">
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("date") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                                <div class="box-tools pull-right">
                                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <asp:DataList ID="DataList3" runat="server" RepeatColumns="4"  meta:resourcekey="DataList3Resource2">

                                                    <ItemTemplate>

                                                        <div class="form-group">
                                                            <asp:Label ID="Label7" runat="server" Visible="False" Text='<%# Bind("date") %>' meta:resourcekey="Label7Resource2"></asp:Label>
                                                            <asp:Label ID="Label8" Visible="False" runat="server" Text='<%# Bind("time") %>' meta:resourcekey="Label8Resource2"></asp:Label>
                                                            <%--<asp:Button ID="Button2" CommandName="doc" CssClass="btn btn-sm btn-bitbucket" runat="server" Text='<%# Bind("time") %>' CommandArgument='<%#Bind("email") %>' />--%>
                                                            &nbsp;<asp:LinkButton ID="LinkButton2" CommandName="doc" CssClass="btn btn-sm btn-default" Text='<%# Bind("time") %>' CommandArgument='<%# Bind("email") %>' runat="server" meta:resourcekey="LinkButton2Resource2"></asp:LinkButton>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                               
                            </div>
                        </div>
                    </div>
      <div class="box-body">

                            <div class="form-group">

                                <div id="Div2" class="table-responsive">


                                    <asp:DataList ID="DataList4" CssClass="table" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" >
                                        <ItemTemplate>
                                            <div class="box box-primary box-solid">

                                                <div class="box-header with-border">
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                                    <div class="box-tools pull-right">
                                                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                    </div>
                                                </div>


                                                <div class="box-body">
                                                    <asp:DataList ID="DataList5" runat="server" RepeatColumns="4">

                                                        <ItemTemplate>

                                                            <div class="form-group">
                                                                <asp:Label ID="Label7" runat="server" Visible="False" Text='<%# Bind("date") %>' meta:resourcekey="Label7Resource2"></asp:Label>
                                                                <asp:Label ID="Label8" Visible="False" runat="server" Text='<%# Bind("time") %>' meta:resourcekey="Label8Resource2"></asp:Label>
                                                               
                                                                   &nbsp; <asp:LinkButton ID="LinkButton2" runat="server" CommandName="doc" CssClass="btn btn-sm btn-default" Text='<%# Bind("time") %>' CommandArgument='<%# Bind("d_hakkimid") %>' meta:resourcekey="LinkButton2Resource2"></asp:LinkButton>
                                                                   
                                                                
                                                            </div>

                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>

                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>

                                </div>
                            </div>
                        </div>
   
</asp:Content>

