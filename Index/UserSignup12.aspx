<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSignup.aspx.cs" Inherits="Index_userReg" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link rel="shortcut icon" type="text/css" href="images/titlelogo.png" />
    <title>MediFi|User SignUp </title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.5 -->
    <!--<link rel="stylesheet" href="../../bootstrap/css/bootstrap.min.css">-->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <%--  <link href="../css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />--%>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <!--<link rel="stylesheet" href="../../dist/css/AdminLTE.min.css">-->
    <link href="css/AdminLTE.min.css" rel="stylesheet" />
    <!-- iCheck -->
    <!--<link rel="stylesheet" href="../../plugins/iCheck/square/blue.css">-->
    <link href="../Design/plugins/iCheck/square/blue.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
            <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <link href="css/AdminLTE.min.css" rel="stylesheet" />
    <link href="loginstyle.css" rel="stylesheet" />
    <link href="regstyle.css" rel="stylesheet" />
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <%-- <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>--%>
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>
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
    <link href="../css/datepicker3.css" rel="stylesheet" />

    <script src="../css/bootstrap-datepicker.js"></script>
</head>
<body class="hold-transition" style="background-image: url('../images/back4.jpg'); background-repeat: no-repeat; background-size: cover">

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%  if (Session["Language"].ToString() == "Auto")
            {%>
        <div>
            <%}
                else
                { %>
            <div dir="rtl">

                <%} %>
                <div class="login-box">



                    <div class="loginbody">


                        <div class="login-logo" style="margin-bottom: 1px">
                            <%  if (Session["Language"].ToString() == "Auto")
                                {%>
                            <a href="../default.aspx">
                                <%}
                                    else
                                    { %>
                                <a href="../default.aspx?l=ar-EG">
                                    <%} %>
                                    <img src="../hakkeem/img/logo.png" style="margin-left: 1cm;" />

                                </a>
                                <p class="pull-right" style="font-size: medium; font-weight: bold; margin-top: 1cm; position: relative;left: -20px;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="ar-EG" OnClick="LinkButton1_Click" Text="عربى" Visible="false" meta:resourcekey="LinkButton1Resource2"></asp:LinkButton>
                                </p>
                                <p class="login-box-msg text-center" style="font-size: 16px; font-weight: bold; margin-bottom: 0px; margin-top: 3%;">
                                    <asp:Label ID="Label6" runat="server" Text="User Registration" meta:resourcekey="Label6Resource1" style="text-transform:uppercase"></asp:Label>
                                </p>
                        </div>


                        <div class="login-box-body">



                            <%--   <%  if (Session["Language"].ToString() == "Auto")
                                  {%>
                                    <div class="col-md-6">
                            <%}
    else
    { %>
                                         <div class="col-md-6 col-md-push-6">
                                             <%} %>--%>


                            <div class="row">
                                <%if (Session["Language"].ToString() == "Auto")
                                    {%>
                                <div class="col-md-6">
                                    <%}
                                        else
                                        { %>
                                    <div class="col-md-6 col-md-push-6">
                                        <%} %>
                                        <div class="form-group has-feedback">
                                            <label>
                                                <asp:Label ID="Label10" runat="server" Text="First name" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Enter your name" ValidationGroup="bb" ForeColor="Red" ControlToValidate="textbox3" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox3" ErrorMessage="* Enter valid name" ForeColor="Red" ValidationExpression="[a-zA-Z]*$" Display="Dynamic" meta:resourcekey="RegularExpressionValidator5Resource1"></asp:RegularExpressionValidator>
                                            <asp:TextBox ID="TextBox3" CssClass="form-control" Width="100%" onkeyup="javascript:capitalize(this.id, this.value);" runat="server" ValidationGroup="bb" TabIndex="1" meta:resourcekey="TextBox3Resource1"></asp:TextBox>


                                        </div>
                                    </div>
                                    <%if (Session["Language"].ToString() == "Auto")
                                        {%>
                                    <div class="col-md-6">
                                        <%}
                                            else
                                            { %>
                                        <div class="col-md-6 col-md-pull-6">
                                            <%} %>
                                            <div class="form-group has-feedback">
                                                <label>
                                                    <asp:Label ID="Label12" runat="server" Text="Last name" meta:resourcekey="Label12Resource1"></asp:Label></label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="bb" Display="Dynamic" runat="server" ErrorMessage="* Enter last name" ForeColor="Red" ControlToValidate="txtLastName" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtLastName" ErrorMessage="* Enter valid name" Display="Dynamic" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$" meta:resourcekey="RegularExpressionValidator6Resource1"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" ValidationGroup="bb" onkeyup="javascript:capitalize(this.id, this.value);" TabIndex="2" meta:resourcekey="txtLastNameResource1"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>



                                    <div class="row">
                                        <%if (Session["Language"].ToString() == "Auto")
                                            {%>
                                        <div class="col-md-6">
                                            <%}
                                                else
                                                { %>
                                            <div class="col-md-6 col-md-push-6">
                                                <%} %>
                                                <div class="form-group has-feedback">
                                                    <label>
                                                        <asp:Label ID="Label11" runat="server" Text="Phone Number" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Enter contact number" ValidationGroup="bb" ForeColor="Red" ControlToValidate="textbox7" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="bb" ControlToValidate="TextBox7" runat="server" ErrorMessage="* Enter valid number" ForeColor="Red" Display="Dynamic" ValidationExpression="[0-9]{9}" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="TextBox7" CssClass="form-control" onkeyup="myFunction()" TabIndex="3" TextMode="Phone" runat="server" ValidationGroup="bb" meta:resourcekey="TextBox7Resource1"></asp:TextBox>

                                                </div>
                                            </div>
                                            <%if (Session["Language"].ToString() == "Auto")
                                                {%>
                                            <div class="col-md-6">
                                                <%}
                                                    else
                                                    { %>
                                                <div class="col-md-6 col-md-pull-6">
                                                    <%} %>
                                                    <div class="form-group has-feedback">
                                                        <label>
                                                            <asp:Label ID="Label13" runat="server" Text=" Date of birth" meta:resourcekey="Label13Resource1"></asp:Label></label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="bb" runat="server" ErrorMessage="* Select date of birth" ForeColor="Red" ControlToValidate="TextBox4" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="TextBox4" CssClass="form-control datepicker" TabIndex="4" onkeydown="return false" onpaste="return false" ValidationGroup="bb" runat="server" meta:resourcekey="TextBox4Resource1"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <%if (Session["Language"].ToString() == "Auto")
                                                    {%>
                                                <div class="col-md-6">
                                                    <%}
                                                        else
                                                        { %>
                                                    <div class="col-md-6 col-md-push-6">
                                                        <%} %>
                                                        <div class="form-group has-feedback">
                                                            <label>
                                                                <asp:Label ID="Label14" runat="server" Text=" Email" meta:resourcekey="Label14Resource1"></asp:Label></label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Enter your Email" ValidationGroup="bb" ControlToValidate="TextBox1" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="bb" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="TextBox1" ErrorMessage="* Enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                                            <asp:TextBox ID="TextBox1" TextMode="Email" TabIndex="5" ValidationGroup="bb" CssClass="form-control" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <%if (Session["Language"].ToString() == "Auto")
                                                        {%>
                                                    <div class="col-md-6">
                                                        <%}
                                                            else
                                                            { %>
                                                        <div class="col-md-6 col-md-pull-6">
                                                            <%} %>
                                                            <div class="form-group has-feedback">
                                                                <label>
                                                                    <asp:Label ID="Label15" runat="server" Text="Gender" meta:resourcekey="Label15Resource1"></asp:Label></label>
                                                                <asp:DropDownList ID="DropDownList1" ValidationGroup="bb" CssClass="form-control" TabIndex="6" runat="server" meta:resourcekey="DropDownList1Resource1">
                                                                    <asp:ListItem meta:resourcekey="ListItemResource1">Male</asp:ListItem>
                                                                    <asp:ListItem meta:resourcekey="ListItemResource2">Female</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="row">
                                                        <%if (Session["Language"].ToString() == "Auto")
                                                            {%>
                                                        <div class="col-md-6">
                                                            <%}
                                                                else
                                                                { %>
                                                            <div class="col-md-6 col-md-push-6">
                                                                <%} %>
                                                                <div class="form-group has-feedback">
                                                                    <label>
                                                                        <asp:Label ID="Label1" runat="server" Text="Create Password" meta:resourcekey="Label1Resource1"></asp:Label></label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="textbox5" runat="server" ValidationGroup="bb" ErrorMessage="* Enter a password" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="* Minimum 6 characters required" ForeColor="Red" ControlToValidate="textbox5" Display="Dynamic" ValidationGroup="bb" ValidationExpression="^[\s\S]{6,}$" meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>
                                                                    <asp:TextBox ID="textbox5" CssClass="form-control" TabIndex="7" TextMode="Password" ValidationGroup="bb" runat="server" meta:resourcekey="textbox5Resource1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%if (Session["Language"].ToString() == "Auto")
                                                                {%>
                                                            <div class="col-md-6">
                                                                <%}
                                                                    else
                                                                    { %>
                                                                <div class="col-md-6 col-md-pull-6">
                                                                    <%} %>
                                                                    <div class="form-group has-feedback">
                                                                        <label>
                                                                            <asp:Label ID="Label16" runat="server" Text=" Confirm password" meta:resourcekey="Label16Resource1"></asp:Label></label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter password" ForeColor="Red" ValidationGroup="bb" ControlToValidate="TextBox6" Display="Dynamic" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="CompareValidator2" ControlToCompare="textbox5" ControlToValidate="textbox6" runat="server" ForeColor="Red" ValidationGroup="bb" ErrorMessage="* Re-enter password" Display="Dynamic" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                                                                        <asp:TextBox ID="TextBox6" CssClass="form-control" TabIndex="8" TextMode="Password" ValidationGroup="bb" runat="server" meta:resourcekey="TextBox6Resource1"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                         





                                                            <div class="col-md-12 col-sm-12">
                                                                <div class="form-group" >
                                                                      <div class="social-auth-links"> 
                                                                          <label style="font-weight:normal;">             
                                                                    <asp:CheckBox ID="ChkAgree" runat="server" meta:resourcekey="ChkAgreeResource1" />
                                                                        
                                                                    <%  if (Session["Language"].ToString() == "Auto")
                                                                        {%>
                                     <span style="font-size:16px;cursor:pointer">Please read and accept the<span style="color:#4aa9af">Terms of use</span>   and <span  style="color:#4aa9af">Privacy policy.</span></span>
                                                                    <%}
                                                                        else
                                                                        {
                                                                    %><span style="cursor:pointer">
                                         يرجى قراءة وقبول<span style="color:#4aa9af">تعليمات الاستخدام</span>   و <span style="color:#4aa9af" >سياسة الخصوصية.</span></span>

                                                                    <%} %>
                                                                              </label>
                                                                </div>
                                                                      </div>
                                                                <div class="social-auth-links text-center">

                                                                    <asp:Button ID="Button2" runat="server" Text="Get Started" CssClass="btn btn-md btn-primary btn-block btn-flat" Style="width: 50%; margin: auto;" ValidationGroup="bb" meta:resourcekey="Button2Resource1" OnClick="Button2_Click" />
                                                                    <%--   <button type="submit" class="btn btn-lg btn-primary btn-block btn-flat" style="width: 300px; margin: auto;">
                                Sign In</button>--%>
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                    <!--<a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign in using Facebook</a>
                            <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i> Sign in using Google+</a>-->
                                                                </div>
                                                                <div class="form-group">
                                                                    <div style="font-size: large">
                                                                        <asp:Label ID="Label21" runat="server" Text=" Already have one ? " meta:resourcekey="Label21Resource1"></asp:Label>
                                                                        <%  if (Session["Language"].ToString() == "Auto")
                                                                            {%>
                                                                        <a href="SignInSignUp.aspx" style="text-decoration: none; color: #4aa9af;">
                                                                            <%}
                                                                                else
                                                                                { %>
                                                                            <a href="SignInSignUp.aspx?l=ar-EG" style="text-decoration: none; color: #4aa9af;">
                                                                                <%} %>
                                                                                <asp:Label ID="Label32" runat="server" Text="SignIn" meta:resourcekey="Label32Resource1"></asp:Label></a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <footer class="myfooter">
                                                    <!--<div class="pull-right hidden-xs">
                    <b>Version</b> 2.3.0
                </div>-->
                                                    <%  if (Session["Language"].ToString() == "Auto")
                                                        {%>
                                                    <strong><a href="#">MediFi</a>&copy; 2019 .</strong> All rights reserved.
            <%}
                else
                { %>
                                                    <strong><a href="#">حكيم</a>&copy; 2019 .</strong> كل الحقوق محفوظة
            <%} %>
                                                </footer>

                                                <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="modal-content">
                                                                    <div class="modal-header">
                               <%--                                         <%  if (Session["Language"].ToString() == "Auto")
                                                                            {%>
                                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                                                                        <%}
                                                                            else
                                                                            { %>
                                                                        <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                        <%} %>--%>
                                                                        <%  if (Session["Language"].ToString() == "Auto")
                                                                                                                                     {%>

                                                                                            <asp:Button ID="Button21" CssClass="close" data-dismiss="modal" runat="server" Text="x" UseSubmitBehavior="false" OnClick="Button21_Click" />


                                                                                          <%--  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                                                                                            <%}
                                    else
                                    { %><asp:Button ID="Button22" CssClass="close pull-left" data-dismiss="modal" runat="server" Text="x" UseSubmitBehavior="false" OnClick="Button21_Click" />
                                                                                         <%--   <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                                                                                            <%} %>
                                                                        <h4 class="modal-title">
                                                                            <asp:Label ID="Label33" runat="server" Text="Enter OTP" meta:resourcekey="Label33Resource1"></asp:Label></h4>
                                                                    </div>
                                                                    <div class="modal-body">

                                                                        <div class="form-group">
                                                                            <asp:Label ID="Label25" runat="server" Text="Please check your email and enter your OTP to continue your registration process.." meta:resourcekey="Label25Resource1"></asp:Label>
                                                                        </div>
                                                                        <div class="form-group">
                                                                           
                                                                             <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="* Enter OTP." ForeColor="Red" ControlToValidate="TxtOTP" ValidationGroup="cc" meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator></label>--%>
                                                                            <asp:TextBox ID="TxtOTP" runat="server" CssClass="form-control"  placeholder="Enter OTP here"  meta:resourcekey="TxtOTPResource1"></asp:TextBox>
                                                                        </div>
                                                                         <div class="form-group">
                                                                             <asp:Label ID="Label24" ForeColor="Red" runat="server" meta:resourcekey="Label24Resource1"></asp:Label>
                                                                            </div> 
                                                                        <div class="form-group">
                                                                            <%  if (Session["Language"].ToString() == "Auto")
                                                                                {%>
                                                                            <div>
                                                                                <%}
                                                                                    else
                                                                                    { %>
                                                                                <div class="pull-left">
                                                                                    <%} %>
                                                                                    <asp:Button ID="BtnSubmitOTP" runat="server" Text="Submit" CssClass="btn btn-success" BackColor="#4AA9AF" BorderColor="#4AA9AF" OnClick="BtnSubmitOTP_Click" ValidationGroup="cc" UseSubmitBehavior="False" meta:resourcekey="BtnSubmitOTPResource1" />
                                                                                    <asp:Button ID="BtnResendOTP" runat="server" Text="Resend" CssClass="btn btn-success" BackColor="#4AA9AF" BorderColor="#4AA9AF" OnClick="BtnResendOTP_Click" UseSubmitBehavior="False" meta:resourcekey="BtnResendOTPResource1" />
                                                                                </div>
                                                                            </div>
                                                                            

                                                                        </div>
                                                                       
                                                                    </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="BtnSubmitOTP" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="BtnResendOTP" EventName="Click" />
                                                                                                                                              <asp:AsyncPostBackTrigger ControlID="Button21" EventName="Click" />
                                                                                     <asp:AsyncPostBackTrigger ControlID="Button22" EventName="Click" />
                                                                   </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
    </form>

    <script type="text/javascript">
        function capitalize(textboxid, str) {
            // string with alteast one character
            if (str && str.length >= 1) {
                var firstChar = str.charAt(0);
                var remainingStr = str.slice(1);
                str = firstChar.toUpperCase() + remainingStr;
            }
            document.getElementById(textboxid).value = str;
        }
    </script>


    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script>
        $("#TextBox7").focus(function () {
            $(this).attr('placeholder', 'Enter a number start with 5')
        }).blur(function () {
            $(this).attr('placeholder', '123456789')
        })
    </script>

    <script>
        $("#TextBox7").keyup(function (e) {
            $("#mypopup").html('');

            var validstr = '';
            var dInput = $(this).val();
            var numpattern = /^\d+$/;




            for (var i = 0; i < dInput.length; i++) {

                if ((i == 0)) {
                    if (numpattern.test(dInput[i])) {
                        console.log('validnum' + dInput[i]);
                        validstr += dInput[i];
                        if (+dInput[i] == 5) {

                        }
                        else {

                            swal("Enter a number start with 5");
                            $(this).val('');
                            return false;

                        }

                    }
                    else {
                        //$("#mypopup").html("Digits Only").show();
                        swal("***Enter a number***");

                    }
                }

                if ((i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8)) {
                    if (numpattern.test(dInput[i])) {
                        console.log('validnum' + dInput[i]);
                        validstr += dInput[i];
                    } else {
                        $("#mypopup").html("Digits Only").show();
                        swal("**Enter a number***");


                    }
                }

            }

            $(this).val(validstr);
            return false;

        });
    </script>

    
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
