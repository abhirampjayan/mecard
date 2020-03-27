<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgot password.aspx.cs" Inherits="Index_HospitaLogin" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
       <link rel="shortcut icon" type="text/css" href="images/titlelogo.png" />
    <title>MediFi | LogIn</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.5 -->
    <!--<link rel="stylesheet" href="../../bootstrap/css/bootstrap.min.css">-->
    <link href="../Design/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <!--<link rel="stylesheet" href="../../dist/css/AdminLTE.min.css">-->
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <!-- iCheck -->
    <!--<link rel="stylesheet" href="../../plugins/iCheck/square/blue.css">-->
    <link href="../Design/plugins/iCheck/square/blue.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="loginstyle.css" rel="stylesheet" />
</head>
<body class="hold-transition" style="background-image: url('../images/back4.jpg'); background-repeat: no-repeat; background-size: cover">
    <form id="form1" runat="server">

        <div class="login-box">



            <div class="loginbody">
                <div class="login-logo" style="margin-bottom: 1px">
                     <% if (Session["Language"].ToString() == "Auto")
                         {%>
                    <a href="../default.aspx">
                        <%}
    else
    { %>
                        <a href="../default.aspx?l=ar-EG">
                        <%} %>
                        <img style="margin-top: 5%" src="../hakkeem/img/logo.png" />
                    </a>
                    <p class="login-box-msg" style="font-size: 16px; font-weight: bold; margin-bottom: 0px; margin-top: 3%">
                        <asp:Label ID="Label3" runat="server" Text="Hospital login" meta:resourcekey="Label3Resource1"></asp:Label>
                    </p>
                </div>
                <!-- /.login-logo -->
                <div class="login-box-body">
                         <asp:Panel ID="PnlFindAcnt" runat="server" CssClass="panel panel-default " DefaultButton="BtnSearch" meta:resourcekey="PnlFindAcntResource1">
                    <div class="row table-responsive">
                        <div class="col-md-12 col-sm-12" style="left: 0px; top: 0px; height: 309px">
                            <div class="box" style="margin-bottom: 0px;">
                                <div class="box-header">
                                      <%if (Session["Language"].ToString() == "Auto")
                                          { %> 
           
                                    <h5 class="box-title" style="font-size:1em;font-weight:bold;">Find Your Account</h5>
                                    <%}
    else
    { %>
                                     <h4 class="box-title pull-right" style="font-size:2em;font-weight:bold;">جد حسابك</h4>

                                    <%} %>

                                     <%if (Session["Language"].ToString() == "Auto")
                                         { %> 
                                    <div class="pull-right">
                                        <%}
    else
    { %>
                                           <div class="pull-left">
                                        <%} %>
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="false"></asp:LinkButton></div>
                                </div>
                            </div>
                                 <%if (Session["Language"].ToString() == "Auto")
                                     { %> 
                            <div class="box-body">
                                <%}
    else
    { %>
                                <div class="box-body" dir="rtl">
                                <%} %>
                                <div class="col-md-12">
                                    <div class="form-group">
                                       
                                        <label id="LblHeading" runat="server" style="color:red;font-weight:bold;" visible="False">Please enter your email address or phone number to search your account</label>

                                        <div class="form-group">
                                            <label>
                                                    <asp:Panel ID="PnlUserFailed" runat="server" Visible="False"   meta:resourcekey="PnlUserFailedResource1">
                                            <%if (Session["Language"].ToString() == "Auto")
                                                { %> 
                                                    <label style="color: red;">Given identification is invalid</label>
                                                        <%}
    else
    { %>
                                                        <label style="color: red;">معرف البريد الإلكتروني غير صالح أو فونينومبر</label>
                                                        <%} %>
                                                
                                        </asp:Panel>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please fill this field" ForeColor="Red" ValidationGroup="a" ControlToValidate="TxtEmail" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" placeholder="Enter email or phone" ValidationGroup="a" meta:resourcekey="TxtEmailResource1"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Enabled="False" ErrorMessage="* Please fill this field" ForeColor="Red" ValidationGroup="a" ControlToValidate="TxtHosRegnNo" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="TxtHosRegnNo" runat="server" CssClass="form-control" Enabled="False" Visible="False" placeholder="Enter hospital registration number" ValidationGroup="a" meta:resourcekey="TxtHosRegnNoResource1"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                           


                                               
                                  <asp:Button ID="BtnSearch" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="BtnSearch_Click" ValidationGroup="a" meta:resourcekey="BtnSearchResource1" />
                                          <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="BtnCancel_Click" meta:resourcekey="BtnCancelResource1" />&nbsp;&nbsp;
                                            <br />       
                                        
                                        </div>


                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlRecovryOptions" runat="server" CssClass="panel panel-default" Visible="False" DefaultButton="BtnContinue" meta:resourcekey="PnlRecovryOptionsResource1">
                    <div class="row table-responsive">
                        <div class="col-md-12 col-sm-12">
                            <div class="box" style="margin-bottom: 0px;">
                                <div class="box-header">
                                      <%if (Session["Language"].ToString() == "Auto")
                                          { %> 
                                    <h4 class="box-title">Reset Your Password</h4>
                                    <%}
    else
    { %>
                                     <h4 class="box-title pull-right">اعد ضبط كلمه السر</h4>
                                    <%} %>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="form-group">
                                      
                                            <div class="form-group">
                                                <h4 class="box-title">
                                                    <asp:Label ID="LblUName" runat="server" meta:resourcekey="LblUNameResource1"></asp:Label></h4>
                                            </div>
                                            <div class="form-group">
                                                <asp:Panel ID="PnlNoSelect" runat="server" Visible="False" CssClass="box box-danger box-solid" BackColor="LightGray" meta:resourcekey="PnlNoSelectResource1">
                                                    <div style="padding: 5px;">
                                                        <div class="form-group">
                                                            <label style="color: red;">Please select any option.</label>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                   <%if (Session["Language"].ToString() == "Auto")
                                                       { %> 
                                                <label>How would you like to reset your password ?</label>
                                                <%}
    else
    { %>
                                                  <label>كيف تريد إعادة تعيين كلمة المرور الخاصة بك ؟</label>
                                                <%} %>
                                            </div>
                                            <label>
                                                <asp:RadioButton ID="RdbReset" runat="server" GroupName="a" meta:resourcekey="RdbResetResource1" />
                                                   <%if (Session["Language"].ToString() == "Auto")
                                                       { %> 
                                                Email me a code to reset my password 
                                                <%}
    else
    { %>
                                                أرسل لي رمزا لإعادة تعيين كلمة المرور
                                                <%} %>
                                     <span style="color: orangered; padding-left: 15px;">
                                         <asp:Label ID="LblEmail" runat="server" Text="Label" meta:resourcekey="LblEmailResource1"></asp:Label></span>
                                                <span>
                                                    <asp:Label ID="lblHRegNo" runat="server" Text="Label" Visible="False" meta:resourcekey="lblHRegNoResource1"></asp:Label></span>
                                            </label>
                                            <label>
                                                <asp:RadioButton ID="RdbReset1" runat="server" GroupName="a" meta:resourcekey="RdbReset1Resource1" />
                                                   <%if (Session["Language"].ToString() == "Auto")
                                                       { %> 
                                                Text me a code to reset my password
                                                <%}
    else
    { %>
                                                نص لي رمز لإعادة تعيين كلمة المرور الخاصة بي
                                                <%} %>
                                  <span style="color: orangered; padding-left: 15px;">
                                      <asp:Label ID="LblText" runat="server" Text="Label" meta:resourcekey="LblTextResource1"></asp:Label></span>
                                            </label>
                                     
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="pull-right">
                                                     <asp:Button ID="BtnContinue" runat="server" Text="Continue" CssClass="btn btn-sm btn-primary BtnSearch1"  OnClick="BtnContinue_Click" meta:resourcekey="BtnContinueResource1" />&nbsp;&nbsp;
                                                <asp:Button ID="BtnCancel1" runat="server" Text="Cancel" CssClass="btn btn-sm" OnClick="BtnCancel1_Click" meta:resourcekey="BtnCancel1Resource1" />&nbsp;&nbsp;
                             
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
                </div>
                </div>
        </div>
        <!-- /.login-box -->

      <footer class="myfooter">
                                            <!--<div class="pull-right hidden-xs">
            <b>Version</b> 2.3.0
        </div>-->
                                            <% if (Session["Language"].ToString() == "Auto")
                                                {%>
                                            <strong> <a href="#">
                                                    <asp:Label ID="Label11" runat="server" Text="MediFi"></asp:Label></a>&copy;
                                                <asp:Label ID="Label10" runat="server" Text="2019"></asp:Label>
                                               </strong>
                                            <asp:Label ID="Label12" runat="server" Text="All rights reserved."></asp:Label>
                                            <%}
                                            else
                                            { %>
                                          
                                            <strong><a href="#"><asp:Label ID="Label4" runat="server" Text="حكيم"></asp:Label></a>
                                                <asp:Label ID="Label5" runat="server" Text="2019"></asp:Label>&copy;</strong>
                                              <asp:Label ID="Label6" runat="server" Text="كل الحقوق محفوظة"></asp:Label>
                                            <%} %>
                                        </footer>
    </form>
    <!-- jQuery 2.1.4 -->
    <!--<script src="../../plugins/jQuery/jQuery-2.1.4.min.js"></script>-->
    <script src="../Design/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.5 -->
    <!--<script src="../../bootstrap/js/bootstrap.min.js"></script>-->
    <script src="../Design/bootstrap/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <!--<script src="../../plugins/iCheck/icheck.min.js"></script>-->
    <script src="../Design/plugins/iCheck/icheck.min.js"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });
        });
    </script>
</body>
</html>
