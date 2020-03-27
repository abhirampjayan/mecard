<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordReset.aspx.cs" Inherits="Index_HospitaLogin" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
       <link rel="shortcut icon" type="text/css" href="images/titlelogo.png" />
    <title>Medifi | LogIn</title>
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
                     <asp:Panel ID="PnlSecurityCode" runat="server" CssClass="panel panel-default " defaultbutton="BtnContinue" meta:resourcekey="PnlSecurityCodeResource1">
                    <div class="row table-responsive">
                        <div class="col-md-12 col-sm-12">
                            <div class="box" style="margin-bottom: 0px;">
                                <div class="box-header">
                                       <%if (Session["Language"].ToString() == "Auto")
                                           { %> 
                                    <h4 class="box-title">Enter Security Code</h4>
                                    <%}
    else
    { %>
                                     <h4 class="box-title">أدخل رمز الأمان</h4>
                                    <%} %>
                                      <div class="pull-right">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="false">عربى</asp:LinkButton></div>
                                </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Panel ID="PnlCodeFailed" runat="server" Visible="False" CssClass="box box-danger box-solid" BackColor="LightGray" meta:resourcekey="PnlCodeFailedResource1">
                                            <div style="padding: 5px;">
                                                <div class="form-group">
                                                    <%if (Session["Language"].ToString() == "Auto")
                                                        { %> 
                                                    <label style="color: red;">The number that you've entered doesn't match your code. Please try again.</label>
                                                    <%}
    else
    { %>
                                                    <label style="color: red;">الرقم الذي أدخلته لا يتطابق مع الشفرة. حاول مرة اخرى.</label>
                                                    <%} %>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                         <%if (Session["Language"].ToString() == "Auto")
                                             { %> 
                                        <label>Please check your email or phone for a message with your code. Your code is 6 digits long.</label>
                                        <%}
    else
    { %>
                                        <label>يرجى التحقق من بريدك الإلكتروني أو هاتفك للحصول على رسالة تحتوي على الشفرة. الرمز الخاص بك هو 6 أرقام طويلة</label>
                                        <%} %>
                                        <div class="form-group">

                                            <label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please fill this field" ForeColor="Red" ValidationGroup="a" ControlToValidate="TxtCode" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="TxtCode" runat="server" CssClass="form-control" placeholder="Enter security code" ValidationGroup="a" meta:resourcekey="TxtCodeResource1" TextMode="Number"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <div class="pull-right">
                                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm" OnClick="BtnCancel_Click" meta:resourcekey="BtnCancelResource1" />&nbsp;&nbsp;
                                  <asp:Button ID="BtnContinue" runat="server" Text="Continue" CssClass="btn btn-sm btn-primary BtnSearch1"  ValidationGroup="a" OnClick="BtnContinue_Click" meta:resourcekey="BtnContinueResource1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlNewPassword" runat="server" CssClass="panel panel-default" Visible="False" defaultbutton="BtnContinue1" meta:resourcekey="PnlNewPasswordResource1">
                    <div class="row table-responsive">
                        <div class="col-md-12 col-sm-12">
                            <div class="box">
                                <div class="box-header">
                                    <h4 class="box-title">Enter New Password</h4>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-sm-9">
                                            <div class="form-group">
                                                <label>
                                                     <%if (Session["Language"].ToString() == "Auto")
                                                         { %> 
                                                    Enter New Password
                                                    <%}
    else
    { %>
                                                    أدخل كلمة مرور جديدة
                                                    <%} %>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Please fill this field" ForeColor="Red" ValidationGroup="b" ControlToValidate="TxtNewPassword" Display="Dynamic" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="* Minimum 6 characters required" ControlToValidate="TxtNewPassword" ForeColor="Red" ValidationGroup="b" Display="Dynamic" ValidationExpression="^[\s\S]{6,}$" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                                </label>
                                                <asp:TextBox ID="TxtNewPassword" runat="server" TextMode="Password" placeholder="Enter new password" ValidationGroup="b" CssClass="form-control" meta:resourcekey="TxtNewPasswordResource1"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                     <%if (Session["Language"].ToString() == "Auto")
                                                         { %> 
                                                    Confirm New Password
                                                    <%}
    else
    { %>
                                                    تأكيد كلمة المرور الجديدة
                                                    <%} %>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Please fill this field" Display="Dynamic" ValidationGroup="b" ForeColor="Red" ControlToValidate="TxtConfrimPassword" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="* Password mismatch" ForeColor="Red" Display="Dynamic" ValidationGroup="b" ControlToValidate="TxtConfrimPassword" ControlToCompare="TxtNewPassword" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                                                </label>

                                                <asp:TextBox ID="TxtConfrimPassword" runat="server" TextMode="Password" placeholder="Enter confrim password" ValidationGroup="b" CssClass="form-control" meta:resourcekey="TxtConfrimPasswordResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="pull-right">
                                                <asp:Button ID="BtnCancel1" runat="server" Text="Cancel" CssClass="btn btn-sm " OnClick="BtnCancel1_Click" meta:resourcekey="BtnCancel1Resource1" />&nbsp;&nbsp;
                                   <asp:Button ID="BtnContinue1" runat="server" Text="Continue" CssClass="btn btn-sm btn-primary BtnSearch1"  ValidationGroup="b" OnClick="BtnContinue1_Click" meta:resourcekey="BtnContinue1Resource1" />
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
