<%@ Page Language="C#" AutoEventWireup="true" CodeFile="welcome doctor.aspx.cs" Inherits="Index_welcome_doctor" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>MediFi | Lockscreen</title>
    <%--<!-- Tell the browser to be responsive to screen width -->--%>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="../design/bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../design/dist/css/AdminLTE.min.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body class="hold-transition lockscreen">
    <form id="form1" runat="server">
       

     <div class="lockscreen-wrapper">
      <div class="lockscreen-logo">
        <a href="Index.aspx"><b>BookDoc</b></a>
      </div>
      <!-- User name -->
      <div class="lockscreen-name">
         <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></div>

      <!-- START LOCK SCREEN ITEM -->
      <div class="lockscreen-item">
        <!-- lockscreen image -->
        <div class="lockscreen-image">
          <img src="../Images/f_banner-home.jpg" alt="User Image">
        </div>
        <!-- /.lockscreen-image -->

        <!-- lockscreen credentials (contains the form) -->
        <div class="lockscreen-credentials">
          <div class="input-group">
           <%-- <input type="password" class="form-control" placeholder="password">--%>
           <asp:TextBox ID="TextBox1" CssClass="form-control" TextMode="Password" placeholder="Enter OTP" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
            <div class="input-group-btn">
              <%--<button class="btn"><i class="fa fa-arrow-right text-muted"></i></button>--%>
               <asp:LinkButton ID="LinkButton1" cssclass="btn" runat="server" OnClick="LinkButton1_Click" meta:resourcekey="LinkButton1Resource1"><i class="fa fa-arrow-right text-muted"></i></asp:LinkButton>
            </div>
          </div>
        </div><!-- /.lockscreen credentials -->

      </div><!-- /.lockscreen-item -->
      <div class="help-block text-center">
       Please check your email and enter your OTP to continue your registration process
      </div>
      <div class="text-center">
        <a href="~/Index.aspx">Or goto bookdoc webisite</a>
      </div>
      <div class="lockscreen-footer text-center">
        Copyright &copy; 2018-2018 <b><a href="http://goldenetqan.com" class="text-black">GoldenEtqan</a></b><br>
        All rights reserved
      </div>
    </div><!-- /.center -->
            
        
    </form>
     <!-- jQuery 2.1.4 -->
    <script src="../design/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.5 -->
    <script src="../design/bootstrap/js/bootstrap.min.js"></script>
</body>
</html>
