<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HospitalDoctorLogin.aspx.cs" Inherits="Index_hdoclogin" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>MediFi | Log in</title>
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
    <link href="loginstyle.css" rel="stylesheet" />
</head>
<body class="hold-transition" style="background-image: url('../images/back4.jpg'); background-repeat: no-repeat; background-size: cover">
    <form id="form1" runat="server">
         <% if (Session["Language"].ToString() == "Auto")
             {%>
        <div class="login-box">
            <%}
    else
    { %>
            <div class="login-box" dir="rtl">
            <%} %>
            <div class="loginbody">
                <div class="login-logo" style="margin-bottom: 1px">
                    <a href="../default.aspx">
                        <img style="margin-top: 5%" src="../hakkeem/img/logo.png" />
                    </a>
                    <p class="login-box-msg" style="font-size: 16px; font-weight: bold; margin-bottom: 0px;margin-top:3%">
                        <asp:Label ID="Label4" runat="server" Text="Hospital doctor login" meta:resourcekey="Label4Resource2"></asp:Label></p>
                </div>
                <!-- /.login-logo -->
                <div class="login-box-body">

                    <div>
                        <asp:Label ID="Label1" runat="server" Text="Doctor email" meta:resourcekey="Label1Resource2"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="#F93434" ControlToValidate="login" ValidationGroup="a" runat="server" ErrorMessage="*Please Enter username" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="login" CssClass="form-control" runat="server" ValidationGroup="a" meta:resourcekey="loginResource1"></asp:TextBox>
                            <%--<input type="email" class="form-control" placeholder="Hakkeem id">--%>
                          
                        </div>
                        <asp:Label ID="Label7" runat="server" Text="Hospital MediFi id" meta:resourcekey="Label7Resource1"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="#F93434" ControlToValidate="TxtHospital" ValidationGroup="a" runat="server" ErrorMessage="*Please Enter hospital id" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="TxtHospital" CssClass="form-control" runat="server" ValidationGroup="a" meta:resourcekey="TxtHospitalResource1"></asp:TextBox>

                        </div>
                        <asp:Label ID="Label2" runat="server" Text="Password" meta:resourcekey="Label2Resource2"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a" runat="server" ErrorMessage="*Please Enter password" ForeColor="#F93434" ControlToValidate="Password" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>

                        <div class="form-group has-feedback">
                            <asp:TextBox ID="Password" CssClass="form-control" TextMode="Password" runat="server" meta:resourcekey="PasswordResource1"></asp:TextBox>
                            <%--<input type="password" class="form-control" placeholder="Password">--%>
                          
                        </div>
                        <div class="row">
                            <div class="col-xs-8">
                                <div class="checkbox icheck">
                                    <label>
                                        <%-- <input type="checkbox">
                                        Remember Me--%>
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
                    <div class="social-auth-links text-center">
                        <p></p>
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-block btn-flat" ValidationGroup="a" Text="Sign In" OnClick="Button1_Click" meta:resourcekey="Button2Resource1" />
                        <%--<button type="submit" class="btn btn-lg btn-primary btn-block btn-flat">Sign In</button>--%>
                        <!--<a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign in using Facebook</a>
                    <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i> Sign in using Google+</a>-->
                    </div>
                    <!-- /.social-auth-links -->
                    <%-- <a href="#">I forgot my password</a><br>
                    <a href="register.html" class="text-center">Register a new membership</a>--%>
                       <% if (Session["Language"].ToString() == "Auto")
                           {%>
                    <a href="forgot password.aspx?usertype=hosdoctor">
                        <%}
    else
    { %>
                        <a href="forgot password.aspx?l=ar-EG&usertype=hosdoctor">
                        <%} %>
                        <asp:Label ID="Label3" runat="server" Text="Forgot password?" meta:resourcekey="Label3Resource2"></asp:Label></a>

                     <span style="margin-left: 40%; font-weight: bold; font-size: 16px">
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="ar-EG" OnClick="LinkButton1_Click" Visible="false" Text="عربى" meta:resourcekey="LinkButton1Resource2"></asp:LinkButton></span>
                </div>
                <!-- /.login-box-body -->

                <%--<span style="margin-left:40%;font-weight:bold;font-size:16px"><asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="ar-EG" Text="عربى" meta:resourcekey="LinkButton1Resource2"></asp:LinkButton></span>--%>
            </div>
        </div>
        <!-- /.login-box -->

        <footer class="myfooter">
            <!--<div class="pull-right hidden-xs">
            <b>Version</b> 2.3.0
        </div>-->
              <% if (Session["Language"].ToString() == "Auto")
                  {%>
            <strong>&copy; 2019 <a href="#">MediFi</a>.</strong> All rights reserved.
            <%}
    else
    { %>
            <strong dir="rtl">&copy; 2019 <a href="#">حكيم</a>.</strong> كل الحقوق محفوظة
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
