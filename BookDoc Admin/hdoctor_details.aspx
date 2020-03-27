<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="hdoctor_details.aspx.cs" Inherits="BookDoc_Admin_hdoctor_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <div class="row">
            <%
                databaseDataContext db = new databaseDataContext();
                string img = "";
                string hname = "";
                var doc = from item in db.tbl_hdoctors where item.hd_email == Session["hdoctor"].ToString() select item;
                foreach (var ss in doc)
                {
                    if (ss.hd_photo != null)
                    {

                        img = ss.hd_photo;
                    }
                    else
                    {
                        img = "../Doctorimages/doctor.png";
                    }
                    hname = ss.h_name;
                }
            %>

            <div class="col-md-4">
                <!-- Widget: user widget style 1 -->
                <div class="box box-widget widget-user">
                    <!-- Add the bg color to the header using any of the bg-* classes -->
                    <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                        <h3 class="widget-user-username">
                            <asp:Label ID="Label1" runat="server"></asp:Label></h3>

                        <h5 class="widget-user-desc">
                            <asp:Label ID="Label2" runat="server"></asp:Label></h5>
                    </div>
                    <div class="widget-user-image">
                        <img class="img-circle" src="<%=img %>" alt="User Avatar">
                        <%--  <asp:Image ID="Image1" CssClass="img-circle img-md" sr runat="server"  />--%>
                    </div>
                    <div class="box-footer">
                        <div class="row">
                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <%--  <h6 class="description-header">
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Bind("hd_email") %>'></asp:Label></h6>--%>
                                    <%-- <a href="users.aspx"><span class="description-text">Active</span></a>--%>
                                    <asp:Label ID="Label4" runat="server"></asp:Label>
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
                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </div>
                    <div class="box-footer">
                        <p>Hospital Name : <%= hname%></p>
                        <p>Doctor Email : <%=Session["hdoctor"].ToString() %></p>
                    </div>
                </div>
                <!-- /.widget-user -->
            </div>

            <div class="col-md-8">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Appointments</h3>
                    </div>
                    <div class="box-body">

                   
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowCustomPaging="True" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="6">
                        <Columns>
                            <asp:TemplateField HeaderText="Si.No." meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource1"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblTimeResource1"></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Patient Name" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <asp:Label ID="LblPatientName" runat="server" Text='<%# Eval("name") %>' meta:resourcekey="LblPatientNameResource1"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <asp:Label ID="LblReason" runat="server" Text='<%# Eval("a_reason") %>' meta:resourcekey="LblReasonResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#dddddd" Font-Bold="True" ForeColor="#18bc9c" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </div>
                         </div>
                    </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Availabilities</h3>
                    </div>
               <div class="box-body">
                    <div class="table-responsive">
                <asp:DataList ID="DataList3" runat="server" CssClass="table" RepeatColumns="2" RepeatDirection="Horizontal" meta:resourcekey="DataList3Resource1">
                    <ItemTemplate>
                        <div class="box box-primary box-solid ">
                            <div class="box-header with-border">
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("date") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                <asp:Label ID="Label4" Visible="False" runat="server" Text='<%# Bind("hd_id") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                &nbsp; Dr.<asp:Label ID="Label5" runat="server" Text="Label" meta:resourcekey="Label5Resource1"></asp:Label>
                              
                                <div class="box-tools pull-right">
                                  
                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>
                            <div class="box-body ">

                                <asp:DataList ID="DataList4" runat="server" RepeatColumns="3"  meta:resourcekey="DataList4Resource1">

                                    <ItemTemplate>

                                        <div class="form-group">
                                            <asp:Button ID="Button2" CommandName="Appointment" CssClass="btn btn-sm btn-default" runat="server" Text='<%# Bind("time") %>' CommandArgument='<%# Eval("date") %>' meta:resourcekey="Button2Resource1" />
                                            &nbsp;
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
            </div>
        </div>
    </div>
    <script src="../js/app.min.js"></script>
</asp:Content>

