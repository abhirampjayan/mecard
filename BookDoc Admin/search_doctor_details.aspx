<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="search_doctor_details.aspx.cs" Inherits="BookDoc_Admin_search_doctor_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <div class="box box-solid">
                <div class="box-header">

                    <h3 class="box-title pull-left">

                        <asp:Label ID="Label2" runat="server" Text="Doctors"></asp:Label></h3>

                    <div class="col-md-6">

                        <div class="col-md-4">
                            <%--<asp:TextBox ID="TextBox1" CssClass="form-control btn-xs" placeholder="Doctor name" runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged" meta:resourcekey="TextBox1Resource1"></asp:TextBox>--%>
                            <asp:DropDownList ID="dl_speciality" AutoPostBack="True" CssClass="form-control" runat="server" Width="150px" OnSelectedIndexChanged="dl_speciality_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtsearch" CssClass="form-control" placeholder=" Doctor Name or Hakkeem id or Phone number" Style="width: 308px;" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btn_search" CssClass="btn btn-primary" runat="server" Style="margin-left: 233px;" Text="Search" OnClick="btn_search_Click" />
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


                <asp:DataList ID="DataList2" CssClass="table" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" OnItemCommand="DataList2_ItemCommand">
                    <ItemTemplate>
                 <%--       <div class="box box-primary box-solid">
                            <div class="box-header with-border">
                                <h3 class="box-title">Dr.
                                    

                                </h3>
                            </div>
                            <div class="box-body">
                               
                            </div>

                        </div>--%>
                          <div class="col-md-12">

                    <!-- Widget: user widget style 1 -->
                    <div class="box box-widget widget-user">
                        <!-- Add the bg color to the header using any of the bg-* classes -->
                        <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                            <h3 class="widget-user-username" style="font-weight:bold">
                                <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                                Dr. <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" CommandName="doc" Text='<%# Bind("d_name") %>' CommandArgument='<%# Bind("d_hakkimid") %>'></asp:LinkButton>
                            </h3>
                             <h5 class="widget-user-desc">
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("d_specialties") %>'></asp:Label></h5>
                        </div>
                        <div class="widget-user-image">
                          <asp:Image ID="img_doc" AlternateText="Photo" CssClass="img-circle img-responsive img-md" runat="server" ImageUrl='<%# (Eval("d_photo") ?? "../Doctorimages/doctor.png") %>' />
                            <%--<img class="img-circle" src='<%= path %>' alt="User Avatar">--%>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-sm-4 border-right">
                                    <div class="description-block">
                                        <h5 class="description-header">
                                            <%--<asp:Label ID="Label27" runat="server" Text="Hakkeem Id"></asp:Label></h5>--%>
                                        <%--<p class="description-text"><%=Session["hakkeemid"].ToString() %></p>--%>
                                    </div>
                                    <!-- /.description-block -->
                                </div>
                                <!-- /.col -->
                                <div class="col-sm-4 border-right">
                                    <div class="description-block">
                                       <br />
                                        <h5 class="description-header">
                                            <%--<asp:Label ID="Label28" runat="server" Text="Speciality"></asp:Label></h5>--%>
                                        <%--<p class="description-text"><%=spec %></p>--%>
               <asp:LinkButton ID="LinkButton1" runat="server" CommandName="rev" Text="Review" CommandArgument='<%# Bind("d_hakkimid") %>'></asp:LinkButton>

                                    </div>
                                    <!-- /.description-block -->
                                </div>
                                <!-- /.col -->
                                <div class="col-sm-4">
                                    <div class="description-block">
                                        <h5 class="description-header">
                                            <%--<asp:Label ID="Label29" runat="server" Text="Location"></asp:Label></h5>--%>
                                        <%--<p class="description-text"><%=loc %></p>--%>
                                    </div>
                                    <!-- /.description-block -->
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->
                        </div>
                      
                    </div>
                    <!-- /.widget-user -->
                </div>
                    </ItemTemplate>
                </asp:DataList>


              





            </div>
        </div>
    </div>
    <script src="../js/app.min.js"></script>
</asp:Content>

