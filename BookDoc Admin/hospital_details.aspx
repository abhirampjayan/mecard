<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="hospital_details.aspx.cs" Inherits="BookDoc_Admin_hospital_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="">
        <div class="row">
            <%
                string hname = "";
                string email = "";
                string con = "";
                string cb = "";
                string dt = "";
                string adrs = "";
                databaseDataContext db = new databaseDataContext();
                var hos = from item in db.tbl_hospitalregs where item.h_hakkimid == Session["hakkeemid_h"].ToString() select item;
                foreach (var ss in hos)
                {
                    hname = ss.h_name;
                    email = ss.h_email;
                    con = ss.h_contact;
                    dt = ss.h_date_time;
                    adrs = ss.h_address;
                    if (ss.h_otp != null)
                    {
                        cb = "Created by hospital authority";
                    }
                    else
                    {
                        cb = "Created by hakkeem authority";
                    }

                }
            %>
            <div class="col-md-12">
                <!-- Widget: user widget style 1 -->
                <div class="box box-widget widget-user">
                    <!-- Add the bg color to the header using any of the bg-* classes -->
                    <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                        <h3 class="widget-user-username"><%=hname %></h3>
                        <h5 class="widget-user-desc"><%=adrs %></h5>
                    </div>
                    <div class="widget-user-image">
                        <img class="img-circle" src="images/hospital.svg" alt="User Avatar">
                    </div>
                    <div class="box-footer">
                        <div class="row">
                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header">Hakkeem Id</h5>
                                    <p class="description-text"><%=Session["hakkeemid_h"] %></p>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header">Email</h5>
                                    <p class="description-text"><%=email %></p>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-4">
                                <div class="description-block">
                                    <h5 class="description-header">Contact</h5>
                                    <p class="description-text"><%=con %></p>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </div>
                    <div class="box-footer">
                        <p>Account <%=cb %></p>
                        <p>Account created date <%=dt %></p>
                    </div>
                </div>
                <!-- /.widget-user -->
            </div>
            <!-- /.col -->
        </div>

        <div class="row">
            <div class="col-md-12">
                <h3>Available doctors</h3>

                <div class="table-responsive">
                    <asp:DataList ID="DataList1" CssClass="table table-bordered" runat="server" RepeatColumns="3" OnItemCommand="DataList1_ItemCommand">
                        <ItemTemplate>
                            <div class="col-md-12">
                                <!-- Widget: user widget style 1 -->
                                <div class="box box-widget widget-user">
                                    <!-- Add the bg color to the header using any of the bg-* classes -->
                                    <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                                        <h3 class="widget-user-username">
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("hd_name") %>'></asp:Label></h3>--%>
                                            <asp:LinkButton ID="LinkButton1" CommandName="open" Font-Bold="true" ForeColor="White" CommandArgument='<%# Bind("hd_email") %>' Text='<%# Bind("hd_name") %>' runat="server"></asp:LinkButton>
                                         <h5 class="widget-user-desc">
                                             <asp:Label ID="Label2" runat="server" Text='<%# Bind("hd_specialties") %>'></asp:Label></h5>
                                    </div>
                                    <div class="widget-user-image">
                                        <%--<img class="img-circle" src="images/people.png" alt="User Avatar">--%>
                                        <asp:Image ID="Image1" CssClass="img-circle img-md" runat="server" ImageUrl='<%# (Eval("hd_photo") ?? "../Doctorimages/doctor.png") %>' />
                                    </div>
                                    <div class="box-footer">
                                        <div class="row">
                                            <div class="col-sm-4 border-right">
                                                <div class="description-block">
                                                  <%--  <h6 class="description-header">
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Bind("hd_email") %>'></asp:Label></h6>--%>
                                                   <%-- <a href="users.aspx"><span class="description-text">Active</span></a>--%>
                                                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_email") %>'></asp:Label>
                                                </div>
                                                <!-- /.description-block -->
                                            </div>
                                            <!-- /.col -->
                                            <div class="col-sm-4 border-right">
                                                <div class="description-block">
                                                 <%--   <h5 class="description-header">
                                                        <asp:Label ID="Label28" runat="server" Text="Label" meta:resourcekey="Label28Resource1"></asp:Label></h5>
                                                    <a href="users.aspx"><span class="description-text">Inactive</span></a>--%>
                                                    
                                                </div>
                                                <!-- /.description-block -->
                                            </div>
                                            <!-- /.col -->
                                            <div class="col-sm-4">
                                                <div class="description-block">
                                                   <%-- <h5 class="description-header">
                                                        <asp:Label ID="Label29" runat="server" Text="Label" meta:resourcekey="Label29Resource1"></asp:Label></h5>--%>
                                                  <%--  <a href="Del_users.aspx"><span class="description-text">Removed</span></a>--%>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("hd_contact") %>'></asp:Label>
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
                            <!-- /.col -->
                        </ItemTemplate>
                    </asp:DataList>
                </div>

            </div>

        </div>

    </div>
    <script src="../js/app.min.js"></script>
</asp:Content>

