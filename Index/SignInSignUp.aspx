<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignInSignUp.aspx.cs" Inherits="Index_userlogin" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
       <link rel="shortcut icon" type="text/css" href="images/titlelogo.png" />
    <title>Medifi | Login</title>
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
                        <asp:Label ID="Label3" runat="server" Text="User login" meta:resourcekey="Label3Resource1" style="text-transform:uppercase"></asp:Label>
                    </p>
                </div>
                <!-- /.login-logo -->
                <div class="login-box-body">

                    <div>
                         <div class="row">
                              <% if (Session["Language"].ToString() == "Auto")
                                  {%>
                        <div class="col-md-4 pull-left">
                            <%}
    else
    { %>
                             <div class="col-md-4 pull-right" dir="rtl">
                            <%} %>
                        <asp:Label ID="Label1" runat="server" Text="Medifi id" meta:resourcekey="Label1Resource1"></asp:Label>
                                  </div>
                              <% if (Session["Language"].ToString() == "Auto")
                                  {%>
                        <div class="col-md-8 pull-left"><%}
    else
    { %>
<div class="col-md-8 pull-right">
                            <%} %>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="#F93434" ControlToValidate="Email" ValidationGroup="a" runat="server" ErrorMessage="* Please enter username" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>

                        </div>
                            </div>
                             <% 
                                 string t = "";
                                 if (Session["Language"].ToString() == "Auto")
                                 {%>
                            <div class="form-group">
                                <%}
    else
    { %>
                                <div class="form-group" dir="rtl">
                                <%} %>
                            <asp:TextBox ID="Email" CssClass="form-control" placeholder="emailid" runat="server" ValidationGroup="a" meta:resourcekey="EmailResource1"></asp:TextBox>

                          </div>
                        <div class="row">
                             <% if (Session["Language"].ToString() == "Auto")
                                 {%>
                            <div class="col-md-4 pull-left">
                                <%}
    else
    { %>
                                <div class="col-md-4 pull-right" dir="rtl">
                                <%} %>
                        <asp:Label ID="Label2" runat="server" Text="Password" meta:resourcekey="Label2Resource1"></asp:Label>
                                       </div>
                                 <% if (Session["Language"].ToString() == "Auto")
                                     {%>
                            <div class="col-md-8 pull-left">
                                <%}
    else
    { %>
                                 <div class="col-md-8 pull-right">
                                <%} %>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a" runat="server" ErrorMessage="* please enter password" ForeColor="#F93434" ControlToValidate="Password" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>

                       </div>
                              </div>
                                 <% if (Session["Language"].ToString() == "Auto")
                                     {%>
                        <div class="form-group">
                            <%}
    else
    { %>
                            <div class="form-group" dir="rtl">
                            <%} %>
                            <asp:TextBox ID="Password" CssClass="form-control" placeholder="Password" TextMode="Password" runat="server" ValidationGroup="a" meta:resourcekey="PasswordResource1"></asp:TextBox>


                          </div>
                              </div>
                                 <% if (Session["Language"].ToString() == "Auto")
                                     {%>
                        <div class="form-group">
                            <%}
    else
    { %>
                            <div class="form-group" dir="rtl">
                            <%} %>
                        </div>
                        <div class="row">
                              <% if (Session["Language"].ToString() == "Auto")
                                  {%>
                            <div class="col-xs-8">
                                <%}
    else
    { %>
                                <div class="col-xs-8 pull-right" dir="rtl">
                                <%} %>
                                <div class="checkbox icheck">
                                    <label>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Remember Me" meta:resourcekey="CheckBox1Resource1" />
                                    </label>
                                </div>
                            </div>
                            <!-- /.col -->
                            <div class="col-xs-4">
                            </div>
                            <!-- /.col -->
                        </div>
                    </div>
                    <div class="social-auth-links1 text-center">
                        <p></p>
                        <asp:Button ID="Button1" runat="server" ValidationGroup="a" Text="Sign In" OnClick="Button1_Click" class="btn btn-primary btn-block btn-flat" meta:resourcekey="Button1Resource1" />

                        <%--<button type="submit" class="btn btn-lg btn-primary btn-block btn-flat">Sign In</button>--%>
                        <!--<a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign in using Facebook</a>
                    <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i> Sign in using Google+</a>-->
                    </div>
                    <!-- /.social-auth-links -->
                    <%-- <a href="#">I forgot my password</a><br>
                    <a href="register.html" class="text-center">Register a new membership</a>--%>
                               <% if (Session["Language"].ToString() == "Auto")
                                  {%>
                                <div><%}
    else
    { %>
<div dir="rtl">
                                    <%} %>
     <% if (Session["Language"].ToString() == "Auto")
         {%>
                    <a href="forgot password.aspx?usertype=user" style="text-decoration: none;">
                        <%}
    else
    { %>
                        <a href="forgot password.aspx?l=ar-EG&usertype=user" style="text-decoration: none;">
                        <%} %>
                        <asp:Label ID="Label4" runat="server" Text="Forgot Password?" meta:resourcekey="Label4Resource1"></asp:Label></a>
                    <p class="p-bottom-w3ls">
                        <asp:Label ID="Label7" runat="server" Text="Not an account ? " meta:resourcekey="Label7Resource1"></asp:Label>
                        <% if (Session["Language"].ToString() == "Auto")
                            {%>
                        <a href="UserSignup.aspx" style="text-decoration: none;">
                            <%}
    else
    { %>
                            <a href="UserSignup.aspx?l=ar-EG" style="text-decoration: none;">
                            <%} %>
                            <asp:Label ID="Label5" runat="server" Text="Register Here" meta:resourcekey="Label5Resource1"></asp:Label></a>
                    </p>
                </div>
                <!-- /.login-box-body -->

                <span style="margin-left: 40%; font-weight: bold; font-size: 16px;">
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="ar-EG" OnClick="LinkButton1_Click" Text="عربى" Visible="false" meta:resourcekey="LinkButton2Resource1"></asp:LinkButton></span>
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
                                          
                                            <strong><a href="#"><asp:Label ID="Label8" runat="server" Text="حكيم"></asp:Label></a>
                                                <asp:Label ID="Label9" runat="server" Text="2019"></asp:Label>&copy;</strong>
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
