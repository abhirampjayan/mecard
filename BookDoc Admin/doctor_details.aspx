<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="doctor_details.aspx.cs" Inherits="BookDoc_Admin_doctor_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script>
        $(function () {

            //Timepicker
            //$(".timepicker").timepicker({
            //    showInputs: false
            //});

            //Date range picker
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                //format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true,

            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">

        <div class="row">
            <div class="col-md-4">
              
                <!-- Widget: user widget style 1 -->
                <div class="box box-widget widget-user">
                    <!-- Add the bg color to the header using any of the bg-* classes -->
                    <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                        <h3 class="widget-user-username">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                    
                    </div>
                    <div class="widget-user-image">
                        <%
                            string path = "";
                            string spec = "";
                            string loc = "";
                            string dt = "";
                            string cb = "";
                            databaseDataContext db = new databaseDataContext();
                            var doc = from item in db.tbl_doctors where item.d_hakkimid == Session["hakkeemid"].ToString() select item;
                            foreach (var ss in doc)
                            {
                                if(ss.d_photo!=null)
                                {
                                    path = ss.d_photo;
                                }
                                else
                                {
                                 path = "../Doctorimages/doctor.png";
                                 }
                                

                                spec = ss.d_specialties;
                                loc = ss.d_location;
                                dt = ss.d_date_time;
                                if (ss.d_otp != null)
                                {
                                    cb = "Created by own";
                                }
                                else
                                {
                                    cb = "Created by Hakkeem Authority";
                                }
                            }
                        %>
                        <img class="img-circle" src='<%= path %>' alt="User Avatar">
                    </div>
                    <div class="box-footer">
                        <div class="row">
                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header">
                                        <asp:Label ID="Label27" runat="server" Text="Hakkeem Id"></asp:Label></h5>
                                    <p class="description-text"><%=Session["hakkeemid"].ToString() %></p>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-4 border-right">
                                <div class="description-block">
                                    <h5 class="description-header">
                                        <asp:Label ID="Label28" runat="server" Text="Speciality"></asp:Label></h5>
                                    <p class="description-text"><%=spec %></p>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                            <div class="col-sm-4">
                                <div class="description-block">
                                    <h5 class="description-header">
                                        <asp:Label ID="Label29" runat="server" Text="Location"></asp:Label></h5>
                                    <p class="description-text"><%=loc %></p>
                                </div>
                                <!-- /.description-block -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </div>
                    <div class="box-footer">
                        <p>Joining date and time: <%=dt %></p>
                        <p>Account <%=cb %></p>
                    </div>
                </div>
                <!-- /.widget-user -->
            </div>
            <!-- /.col -->

            <!-- appointments -->
            <div class="col-md-8">
                <div class="box box-default">
                    <div class="box-header">
                        <h3 class="box-title">Appointments</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" PageSize="6" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Patient Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label30" Visible="false" runat="server" Text='<%# Bind("c_id") %>'></asp:Label>
                                            <asp:Label ID="Label31" runat="server" Text="Label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appointment date and time">
                                        <ItemTemplate>
                                            <asp:Label ID="Label32" runat="server" Text='<%# Bind("app_date") %>'></asp:Label>
                                            <asp:Label ID="Label34" runat="server" Text='<%# Bind("app_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reason">
                                        <ItemTemplate>
                                            <asp:Label ID="Label33" runat="server" Text='<%# Bind("a_reason") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#F3F3F3" />
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
<h3 class="box-title">Doctor availability</h3>
                        <div class="pull-right">
                        <asp:TextBox ID="TextBox1" placeholder="Search by date" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" AutoPostBack="True" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                    </div>
                        </div>
                     <div class="table-responsive">
                    <asp:DataList ID="DataList2" CssClass="table" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" meta:resourcekey="DataList2Resource1">
                        <ItemTemplate>
                            <div class="box box-default box-solid collapsed-box">
                                <div class="box-header with-border">

                                    <h3 class="box-title">

                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                    </h3>

                                    <div class="box-tools pull-right">

                                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <asp:DataList ID="DataList3" runat="server" RepeatColumns="4" OnItemCommand="DataList3_ItemCommand">

                                        <ItemTemplate>

                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server" Visible="False" Text='<%# Bind("date") %>' meta:resourcekey="Label7Resource1"></asp:Label>
                                                <asp:Label ID="Label8" Visible="False" runat="server" Text='<%# Bind("time") %>' meta:resourcekey="Label8Resource1"></asp:Label>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="doc" CssClass="btn btn-sm btn-primary" Text='<%# Bind("time") %>' CommandArgument='<%# Bind("email") %>'></asp:LinkButton>
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

            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">

                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                                    <h4 class="modal-title">

                                        <asp:Label ID="Label4" runat="server" Text="Make an appointment" meta:resourcekey="Label4Resource1"></asp:Label>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" Text="Date" meta:resourcekey="Label5Resource1"></asp:Label>
                                        <asp:TextBox ID="TxtApntmtDate" ValidationGroup="aaa" CssClass="form-control" runat="server" ReadOnly="True" placeholder="Appointment date" meta:resourcekey="TxtApntmtDateResource1"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" Text="Appointment time" meta:resourcekey="Label10Resource1"></asp:Label>
                                        <asp:TextBox ID="TxtApointmentTime" ValidationGroup="aaa" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Appointment Time" meta:resourcekey="TxtApointmentTimeResource1"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" Text="Reason to visit" meta:resourcekey="Label11Resource1"></asp:Label>
                                        <asp:DropDownList ID="TxtReasonToVisit" ValidationGroup="aaa" runat="server" CssClass="form-control" meta:resourcekey="TxtReasonToVisitResource1">
                                            <asp:ListItem meta:resourcekey="ListItemResource1">General</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource2">Illness</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" Text="Payment option" meta:resourcekey="Label12Resource1"></asp:Label>
                                        <asp:DropDownList ID="DdlPayments" ValidationGroup="aaa" runat="server" CssClass="form-control" meta:resourcekey="DdlPaymentsResource1">
                                            <asp:ListItem meta:resourcekey="ListItemResource3">Payment my self</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server" Text="User Hakkeem Id"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBookDocUserId" ForeColor="Red" ErrorMessage="Please Fill this field" ValidationGroup="aaa" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="TxtBookDocUserId" runat="server" CssClass="form-control" ValidationGroup="aaa" placeholder="User Hakkeem Id"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="modal-footer">

                                    <div>

                                        <asp:Button ID="BtnTakeAppointment" runat="server" Text="Take Appointment" ValidationGroup="aaa" CssClass="btn btn-primary" UseSubmitBehavior="False" OnClick="BtnTakeAppointment_Click" />

                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnTakeAppointment" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>

    <script src="../js/app.min.js"></script>

</asp:Content>

