<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HospitalRequestLogin.aspx.cs" Inherits="Index_HospitalRequestLogin" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>MediFi | Hospital Login</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="../../bootstrap/css/bootstrap.min.css">
    <link href="../Design/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../Design/dist/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="../Design/plugins/iCheck/square/blue.css">

</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server">
    <div>
            <div class="login-box">
                <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                    <div class="box box-solid box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Welcome to BookDoc</h3>
                        </div>
                        <div class="box-body">
                            <div class="login-logo">
                                <a href="#"><b>Book</b>Doc</a>
                            </div>
                            <!-- /.login-logo -->
                            <p class="login-box-msg">Sign in to confirm your registration</p>
                            <div>
                                <div class="form-group">
                                    <label> Hospital Registration Number</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="TxtH_RegnNo" ValidationGroup="a" runat="server" ErrorMessage="* Please fill this field" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                     <div class="form-group has-feedback">
                                    <asp:TextBox ID="TxtH_RegnNo" CssClass="form-control" placeholder="Hospital registration number" runat="server" ValidationGroup="a" meta:resourcekey="TxtH_RegnNoResource1"></asp:TextBox>
                                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                                </div>
                                </div>
                               
                                <div class="form-group">
                                    <label>BookDoc registration date</label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" runat="server" ErrorMessage="* Please fill this field" ForeColor="Red" ControlToValidate="TxtH_RegnDate" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                     <div class="form-group has-feedback">
                                    <asp:TextBox ID="TxtH_RegnDate" CssClass="form-control" placeholder="Registration date"  runat="server" TextMode="Date" meta:resourcekey="TxtH_RegnDateResource1"></asp:TextBox>
                                    <span class="glyphicon glyphicon-calendar form-control-feedback"></span>
                                </div>
                                </div>
                               
                                <div class="row">
                                    <div class="col-xs-8">
                                    </div>
                                    <!-- /.col -->
                                    <div class="col-xs-4">
                                        <asp:Button ID="BtnSignIn" CssClass="btn btn-flat btn-primary btn-block" runat="server" ValidationGroup="a" Text="Sign In" OnClick="BtnSignIn_Click" meta:resourcekey="BtnSignInResource1"/>
                                    </div>
                                    <!-- /.col -->
                                </div>
                            </div>

                            <%-- <div class="social-auth-links text-center">
                            <p>- OR -</p>
                            <a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i>Sign in using Facebook</a>
                            <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i>Sign in using Google+</a>
                        </div>--%>
                            <a class="text-center">
                                </a>
                            <div class="row">
                                <div class="col-sm-12" style="margin-top: 10px;">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" meta:resourcekey="FileUpload1Resource1" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please upload file" ControlToValidate="FileUpload1" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                    <asp:Button ID="BtnUpload" CssClass="btn btn-flat btn-primary btn-block " runat="server" ValidationGroup="a" Text="Upload and confirm" OnClick="BtnUpload_Click" Visible="False" meta:resourcekey="BtnUploadResource1"/>

                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>

            </div>
        </div>
    </form>
     <!-- jQuery 2.1.4 -->
    <script src="../Design/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.5 -->
    <script src="../Design/bootstrap/js/bootstrap.min.js"></script>
    <!-- iCheck -->
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
