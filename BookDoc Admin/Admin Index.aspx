<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin Index.aspx.cs" Inherits="BookDoc_Admin_Admin_Index" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/app.min.js"></script>
    <section class="content">


              <div class="row">
              <div class="col-md-4">
              <!-- Widget: user widget style 1 -->
              <div class="box box-widget widget-user">
                <!-- Add the bg color to the header using any of the bg-* classes -->
                <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                  <h3 class="widget-user-username">MediFi Users</h3>
                 <%-- <h5 class="widget-user-desc">Web Designer</h5>--%>
                </div>
                <div class="widget-user-image">
                  <img class="img-circle" src="images/people.png" alt="User Avatar">
                </div>
                <div class="box-footer">
                  <div class="row">
                    <div class="col-sm-4 border-right">
                      <div class="description-block">
                        <h5 class="description-header">
                  <asp:Label ID="Label27" runat="server" Text="Label" meta:resourcekey="Label27Resource1"></asp:Label></h5>
                       <a href="users.aspx"> <span class="description-text">Active</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                    <div class="col-sm-4 border-right">
                      <div class="description-block">
                        <h5 class="description-header">
                      <asp:Label ID="Label28" runat="server" Text="Label" meta:resourcekey="Label28Resource1"></asp:Label></h5>
                        <a href="users.aspx"> <span class="description-text">Inactive</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                    <div class="col-sm-4">
                      <div class="description-block">
                        <h5 class="description-header">
                          <asp:Label ID="Label29" runat="server" Text="Label" meta:resourcekey="Label29Resource1"></asp:Label></h5>
                       <a href="Del_users.aspx"><span class="description-text">Removed</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                  </div><!-- /.row -->
                </div>
              </div><!-- /.widget-user -->
            </div><!-- /.col -->
             <div class="col-md-4">
              <!-- Widget: user widget style 1 -->
              <div class="box box-widget widget-user">
                <!-- Add the bg color to the header using any of the bg-* classes -->
                <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                  <h3 class="widget-user-username">MediFi Doctors</h3>
                 <%-- <h5 class="widget-user-desc">Web Designer</h5>--%>
                </div>
                <div class="widget-user-image">
              
                  <img class="img-circle" src="images/doctor.svg" alt="User Avatar">
                </div>
                <div class="box-footer">
                  <div class="row">
                    <div class="col-sm-4 border-right">
                      <div class="description-block">
                        <h5 class="description-header">
                  <asp:Label ID="Label30" runat="server" Text="Label" meta:resourcekey="Label30Resource1"></asp:Label></h5>
                       <a href="Doctor.aspx"> <span class="description-text">Active</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                    <div class="col-sm-4 border-right">
                      <div class="description-block">
                        <h5 class="description-header">
                      <asp:Label ID="Label31" runat="server" Text="Label" meta:resourcekey="Label31Resource1"></asp:Label></h5>
                        <a href="Doctor.aspx"> <span class="description-text">Inactive</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                    <div class="col-sm-4">
                      <div class="description-block">
                        <h5 class="description-header">
                          <asp:Label ID="Label32" runat="server" Text="Label" meta:resourcekey="Label32Resource1"></asp:Label></h5>
                       <a href="Del_Doctors.aspx"><span class="description-text">Removed</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                  </div><!-- /.row -->
                </div>
              </div><!-- /.widget-user -->
            </div><!-- /.col -->
             <div class="col-md-4">
              <!-- Widget: user widget style 1 -->
              <div class="box box-widget widget-user">
                <!-- Add the bg color to the header using any of the bg-* classes -->
                <div class="widget-user-header bg-black" style="background: url('../images/titlelogo.png') center center;">
                  <h3 class="widget-user-username">MediFi Hospitals</h3>
                 <%-- <h5 class="widget-user-desc">Web Designer</h5>--%>
                </div>
                <div class="widget-user-image">
             
                  <img class="img-circle" src="images/hospital.svg" alt="User Avatar">
                </div>
                <div class="box-footer">
                  <div class="row">
                    <div class="col-sm-4 border-right">
                      <div class="description-block">
                        <h5 class="description-header">
                  <asp:Label ID="Label33" runat="server" Text="Label" meta:resourcekey="Label33Resource1"></asp:Label></h5>
                       <a href="Hospital.aspx"> <span class="description-text">Active</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                    <div class="col-sm-4 border-right">
                      <div class="description-block">
                        <h5 class="description-header">
                      <asp:Label ID="Label34" runat="server" Text="Label" meta:resourcekey="Label34Resource1"></asp:Label></h5>
                        <a href="Hospital.aspx"> <span class="description-text">Inactive</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                    <div class="col-sm-4">
                      <div class="description-block">
                        <h5 class="description-header">
                          <asp:Label ID="Label35" runat="server" Text="Label" meta:resourcekey="Label35Resource1"></asp:Label></h5>
                       <a href="Del_Hospitals.aspx"><span class="description-text">Removed</span></a>
                      </div><!-- /.description-block -->
                    </div><!-- /.col -->
                  </div><!-- /.row -->
                </div>
              </div><!-- /.widget-user -->
            </div><!-- /.col -->
        </div>

        <!-- Users count -->

        <div class="row">
            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-aqua"><i class="ion ion-ios-people-outline"></i></span>
                    <div class="info-box-content">
                      <%--  <% if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <span class="info-box-text">Members</span>
                      <%--  <%}
    else
    { %>
                        <span class="info-box-text">أفراد</span>
                        <%} %>--%>
                        <span class="info-box-number">
                            <asp:Label ID="Label4" runat="server" Text="Label" meta:resourcekey="Label4Resource2"></asp:Label>

                        </span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
            <!-- /.col -->
            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-aqua"><i class="fa fa-stethoscope" aria-hidden="true"></i></span>
                    <div class="info-box-content">
                        <%-- <% if (Session["Language"].ToString() == "Auto")
                             { %>--%>
                        <span class="info-box-text">Doctors</span>
                       <%-- <%}
    else
    { %>
                         <span class="info-box-text">الأطباء</span>
                        <%} %>--%>
                        <span class="info-box-number">
                            <asp:Label ID="Label5" runat="server" Text="Label" meta:resourcekey="Label5Resource2"></asp:Label>

                        </span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
            <!-- /.col -->
            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-aqua"><i class="fa fa-hospital-o"></i></span>
                    <div class="info-box-content">
                        <%-- <% if (Session["Language"].ToString() == "Auto")
                             { %>--%>
                        <span class="info-box-text">Hospital</span>
                       <%-- <%}
    else
    { %>
                        <span class="info-box-text">مستشفى</span>
                        <%} %>--%>
                        <span class="info-box-number">
                            <asp:Label ID="Label20" runat="server" Text="Label" meta:resourcekey="Label20Resource1"></asp:Label>
                        </span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
            <!-- /.col -->

            <!-- fix for small devices only -->
            <div class="clearfix visible-sm-block"></div>

            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-aqua"><i class="fa fa-stethoscope"></i></span>
                    <div class="info-box-content">
                        <%-- <% if (Session["Language"].ToString() == "Auto")
                             { %>--%>
                        <span class="info-box-text">Hospital doctors</span>
                       <%-- <%}
    else
    { %>
                         <span class="info-box-text">أطباء المستشفى</span>
                        <%} %>--%>
                        <span class="info-box-number">
                            <asp:Label ID="Label21" runat="server" Text="Label" meta:resourcekey="Label21Resource1"></asp:Label>

                        </span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
            <!-- /.col -->

        </div>

        <!--End Users count -->

      

        <%--   --------------------End first section----------------%>

        
        <div class="row">
            <%-- user--%>
            <div class="col-md-3">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <%-- <% if (Session["Language"].ToString() == "Auto")
                             { %>--%>
                        <h3 class="box-title">Latest members</h3>
                      <%--  <%}
    else
    { %>
                        <h3 class="box-title">أحدث الأعضاء</h3>
                        <%} %>--%>
                        <div class="box-tools pull-right">

                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">

                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="False" ShowHeader="False" meta:resourcekey="GridView1Resource1">
                            <Columns>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <ul class="users-list clearfix">
                                            <li>
                                                <asp:Image ID="Image1" CssClass="img-circle img-sm" runat="server" ImageUrl='<%# Bind("photo") %>' meta:resourcekey="Image1Resource1" />
                                            </li>
                                            <li>
                                            <a class="users-list-name">
                                                <asp:Label ID="Label22" runat="server" Text='<%# Bind("name") %>' meta:resourcekey="Label22Resource1"></asp:Label></a>
                                                </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="form-group text-center">
                           <%--  <% if (Session["Language"].ToString() == "Auto")
                                 { %>--%>
                            <a href="users.aspx" class="uppercase">View all users</a>
                          <%--  <%}
    else
    { %>
                            <a href="users.aspx?l=ar-EG" class="uppercase">عرض جميع المستخدمين</a>
                            <%} %>--%>
                        </div>

                    </div>
                  
                </div>
            </div>
            <%-- doctor--%>
            <div class="col-md-3">
                 <div class="box box-primary">
                    <div class="box-header with-border">
                     <%--  <% if (Session["Language"].ToString() == "Auto")
                             { %>--%>
                        <h3 class="box-title">Latest doctors</h3>
                       <%-- <%}
    else
    { %>
                        <h3 class="box-title">أحدث الأطباء</h3>
                        <%} %>--%>

                        <div class="box-tools pull-right">

                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                  <asp:GridView ID="GridView2" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="False" ShowHeader="False" meta:resourcekey="GridView2Resource1">
                            <Columns>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <ul class="users-list clearfix">
                                            <li>
                                                <asp:Image ID="Image1" CssClass="img-circle img-sm" runat="server" ImageUrl='<%# Bind("d_photo") %>' meta:resourcekey="Image1Resource2" />
                                            </li>
                                            <li>
                                            <a class="users-list-name">
                                                <asp:Label ID="Label22" runat="server" Text='<%# Bind("d_name") %>' meta:resourcekey="Label22Resource2"></asp:Label></a>
                                                </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="form-group text-center">
                            <%-- <% if (Session["Language"].ToString() == "Auto")
                                 { %>--%>
                            <a href="Doctor.aspx" class="uppercase">View all doctors</a>
                         <%--   <%}
    else
    { %>
                            <a href="Doctor.aspx" class="uppercase">عرض جميع الأطباء</a>
                            <%} %>--%>
                        </div>
                   
                    </div>
                  
                </div>
            </div>
            <%-- hospital--%>
            <div class="col-md-3">
                  <div class="box box-primary">
                    <div class="box-header with-border">
                         <%-- <% if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <h3 class="box-title">Latest hospitals</h3>
                      <%--  <%}
    else
    { %>
                          <h3 class="box-title">أحدث المستشفيات</h3>
                        <%} %>--%>
                        <div class="box-tools pull-right">

                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                  <asp:GridView ID="GridView3" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="False" ShowHeader="False" meta:resourcekey="GridView3Resource1">
                            <Columns>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <ul class="users-list clearfix">
                                            <li>
                                                <asp:Image ID="Image1" CssClass="img-circle img-sm" ImageUrl="~/BookDoc Admin/images/hospital.png" runat="server" meta:resourcekey="Image1Resource3" />
                                            </li>
                                            <li>
                                            <a class="users-list-name">
                                                <asp:Label ID="Label22" runat="server" Text='<%# Bind("h_name") %>' meta:resourcekey="Label22Resource3"></asp:Label></a>
                                                </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="form-group text-center">
                            <%-- <% if (Session["Language"].ToString() == "Auto")
                                 { %>--%>
                            <a href="Hospital.aspx" class="uppercase">View all hospitals</a>
                          <%--  <%}
    else
    { %>
                             <a href="Hospital.aspx?l=ar-EG" class="uppercase">عرض جميع المستشفيات</a>
                            <%} %>--%>
                        </div>
                   
                    </div>
                  
                </div>
            </div>


               <div class="col-md-3">
                  <div class="box box-primary">
                    <div class="box-header with-border">
                         <%-- <% if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <h3 class="box-title">Latest hospital doctors</h3>
                      <%--  <%}
    else
    { %>
                         <h3 class="box-title">أحدث أطباء المستشفى</h3>
                        <%} %>--%>
                        <div class="box-tools pull-right">

                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                      
                  <asp:GridView ID="GridView4" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="False" ShowHeader="False" meta:resourcekey="GridView4Resource1">
                            <Columns>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <ul class="users-list clearfix">
                                            <li>
                                                <asp:Image ID="Image1" CssClass="img-circle img-sm" ImageUrl="~/BookDoc Admin/images/user.png" runat="server" meta:resourcekey="Image1Resource4" />
                                            </li>
                                   <li>
                                               <a class="users-list-name">  <asp:Label ID="Label22" runat="server" Text='<%# Bind("hd_name") %>' meta:resourcekey="Label22Resource4"></asp:Label></a>
                                      
                                            </li>     
                                        </ul>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                       
                        <div class="form-group text-center">
                          <%--   <% if (Session["Language"].ToString() == "Auto")
                                 { %>--%>
                            <a href="hospital_doctor.aspx" class="uppercase">View all hospital doctors</a>
                           <%-- <%}
    else
    { %>
                            <a href="hospital_doctor.aspx?l=ar-EG" class="uppercase">عرض جميع أطباء المستشفى</a>
                            <%} %>--%>
                        </div>
                   
                    </div>
                  
                </div>
            </div>


        </div>

          <div class="row">
                <div class="col-lg-3 col-xs-6">
              <!-- small box -->
              <div class="small-box bg-green">
                <div class="inner">
                  <h3>
              <asp:Label ID="Label23" runat="server" Text="Label" meta:resourcekey="Label23Resource1"></asp:Label></h3>
                  <p>Registared Users</p>
                </div>
                <div class="icon">
                  <i class="ion ion-person-add"></i>
                </div>
                <a href="users.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
              </div>
            </div><!-- ./col -->
            <div class="col-lg-3 col-xs-6">
              <!-- small box -->
              <div class="small-box bg-green">
                <div class="inner">
                  <h3>
                  <asp:Label ID="Label24" runat="server" Text="Label" meta:resourcekey="Label24Resource1"></asp:Label></h3>
                  <p>Doctors Requests</p>
                </div>
                <div class="icon">
                  <i class="fa fa-stethoscope"></i>
                </div>
                <a href="Doctor request.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
              </div>
            </div><!-- ./col -->
            <div class="col-lg-3 col-xs-6">
              <!-- small box -->
              <div class="small-box bg-green">
                <div class="inner">
                  <h3>
                      <asp:Label ID="Label25" runat="server" Text="Label" meta:resourcekey="Label25Resource1"></asp:Label></h3>
                  <p>Hospital Requests</p>
                </div>
                <div class="icon">
                  <i class="fa fa-hospital-o"></i>
                </div>
                <a href="HospitalRequest.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
              </div>
            </div><!-- ./col -->
          
            <div class="col-lg-3 col-xs-6">
              <!-- small box -->
              <div class="small-box bg-green">
                <div class="inner">
                  <h3>
                      <asp:Label ID="Label26" runat="server" Text="Label" meta:resourcekey="Label26Resource1"></asp:Label></h3>
                  <p>Registered Hospital Doctors</p>
                </div>
                <div class="icon">
                  <i class="fa fa-stethoscope"></i>
                </div>
                <a href="hospital_doctor.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
              </div>
            </div><!-- ./col -->
          </div><!-- /.row -->


        
    


    </section>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
            </asp:Timer>
            <h2><b>
                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1"></asp:Label></b></h2>
        </ContentTemplate>
    </asp:UpdatePanel>






    
</asp:Content>

