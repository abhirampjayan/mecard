<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hospital_Regn.aspx.cs" Inherits="Index_hospial_join" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link rel="shortcut icon" type="text/css" href="images/titlelogo.png" />
    <title>MediFi|Hospital Join </title>
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

</head>
<body class="hold-transition" style="background-image: url('../images/back4.jpg'); background-repeat: no-repeat; background-size: cover">
    <form id="form1" runat="server">
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
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <%  if (Session["Language"].ToString() == "Auto")
                                {%>
                            <a href="../default.aspx">
                                <%}
                                    else
                                    { %>
                                <a href="../default.aspx?l=ar-EG">
                                    <%} %>
                                    <img style="margin-left: 1cm" src="../hakkeem/img/logo.png" />
                                </a>
                                <p class="pull-right" style="font-size: medium; font-weight: bold; margin-top: 1cm; position:relative;left:-20px">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="ar-EG" Text="عربى" Visible="false" meta:resourcekey="LinkButton1Resource2" OnClick="LinkButton1_Click"></asp:LinkButton>
                                </p>
                                <p class="login-box-msg" style="font-size: 16px; font-weight: bold; margin-bottom: 0px; margin-top: 3%">
                                    <asp:Label ID="Label6" runat="server" Text="Hospital Registration" meta:resourcekey="Label6Resource1" style="text-transform:uppercase"></asp:Label>
                                </p>
                        </div>
                        <!-- /.login-logo -->
                        <div class="login-box-body">

                         

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
                                            <asp:Label ID="Label9" runat="server" Text=" Hospital Name" meta:resourcekey="Label9Resource1"></asp:Label>
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Enter hospital name" ForeColor="Red" ControlToValidate="txt_Hname" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txt_Hname" onkeyup="javascript:capitalize(this.id, this.value);" CssClass="form-control" ValidationGroup="a" runat="server" TabIndex="1" meta:resourcekey="txt_HnameResource1"></asp:TextBox>

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
                                            <asp:Label ID="Label13" runat="server" Text=" Hospital registration number" meta:resourcekey="Label13Resource1"></asp:Label></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Enter number" ForeColor="Red" ControlToValidate="txt_HRegnNo" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Enter only numbers" ForeColor="Red" ControlToValidate="txt_HRegnNo" Display="Dynamic" ValidationExpression="^\d+$" ValidationGroup="a" meta:resourcekey="RegularExpressionValidator5Resource1"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txt_HRegnNo" CssClass="form-control" runat="server" TabIndex="2" ValidationGroup="a" meta:resourcekey="txt_HRegnNoResource1"></asp:TextBox>

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
                                  <%--  <div class="form-group has-feedback">
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Hospital Email" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" ControlToValidate="txt_HEmail" runat="server" ErrorMessage="* Enter valid email" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator4Resource1"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Enter hospital email" ForeColor="Red" ControlToValidate="txt_HEmail" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txt_HEmail" CssClass="form-control" TextMode="Email" TabIndex="3" runat="server" ValidationGroup="a" meta:resourcekey="txt_HEmailResource1"></asp:TextBox>


                                    </div>--%>
                                            <div class="form-group has-feedback">
                                        <label>
                                            <asp:Label ID="Label14" runat="server" Text=" Hospital phone number" meta:resourcekey="Label14Resource1"></asp:Label></label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ControlToValidate="txt_HPhone" runat="server" ErrorMessage="* Enter valid number" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]+$" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="* Enter PhoneNo" ForeColor="Red" ControlToValidate="txt_HPhone" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource11"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txt_HPhone"  CssClass="form-control" TabIndex="4" runat="server" ValidationGroup="a" TextMode="Phone" ></asp:TextBox>

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
                                   <%-- <div class="form-group has-feedback">
                                        <label>
                                            <asp:Label ID="Label14" runat="server" Text=" Hospital phone number" meta:resourcekey="Label14Resource1"></asp:Label></label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ControlToValidate="txt_HPhone" runat="server" ErrorMessage="* Enter valid number" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]+$" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="* Enter PhoneNo" ForeColor="Red" ControlToValidate="txt_HPhone" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource11"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txt_HPhone"  CssClass="form-control" TabIndex="4" runat="server" ValidationGroup="a" TextMode="Phone" ></asp:TextBox>

                                    </div>--%>
                                            <div class="form-group has-feedback">
                                        <label>
                                            <asp:Label ID="Label10" runat="server" Text="Hospital Email" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" ControlToValidate="txt_HEmail" runat="server" ErrorMessage="* Enter valid email" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator4Resource1"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Enter hospital email" ForeColor="Red" ControlToValidate="txt_HEmail" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txt_HEmail" CssClass="form-control" TextMode="Email" TabIndex="3" runat="server" ValidationGroup="a" meta:resourcekey="txt_HEmailResource1"></asp:TextBox>


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
                                            <asp:Label ID="Label11" runat="server" Text="Hospital Address" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="* Enter hospital Address" ForeColor="Red" ControlToValidate="txt_HAddress" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txt_HAddress" CssClass="form-control" ValidationGroup="a" runat="server" TabIndex="7" meta:resourcekey="txt_HAddressResource1" TextMode="MultiLine"></asp:TextBox>


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
                                            <asp:Label ID="Label15" runat="server" Text="About Hospital" meta:resourcekey="Label15Resource1"></asp:Label></label>
                                        <asp:TextBox ID="txt_HAbout"  CssClass="form-control" TabIndex="8" ValidationGroup="a" runat="server" meta:resourcekey="txt_HAboutResource1" TextMode="MultiLine"></asp:TextBox>

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
                                            <asp:Label ID="Label12" runat="server" Text="Create Password" meta:resourcekey="Label12Resource1"></asp:Label></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Enter password" ControlToValidate="TxtPassword" ValidationGroup="a" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="* Minimum 6 characters required" ForeColor="Red" ControlToValidate="TxtPassword" Display="Dynamic" ValidationGroup="a" ValidationExpression="^[\s\S]{6,}$" meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" TabIndex="9" CssClass="form-control" ValidationGroup="a" meta:resourcekey="TxtPasswordResource1"></asp:TextBox>


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
                                            <asp:Label ID="Label16" runat="server" Text="Confirm password" meta:resourcekey="Label16Resource1"></asp:Label></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* Enter password" ControlToValidate="TxtConfirm" ValidationGroup="a" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Mismatched" ForeColor="Red" Display="Dynamic" ControlToCompare="TxtPassword" ControlToValidate="TxtConfirm" ValidationGroup="a" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                                        <asp:TextBox ID="TxtConfirm" runat="server" TabIndex="10" TextMode="Password" CssClass="form-control" ValidationGroup="a" meta:resourcekey="TxtConfirmResource1"></asp:TextBox>

                                    </div>
                                </div>
                     



                            <div class="col-md-12">
                                <div class="form-group">
                                     <div class="social-auth-links"> 
                                         <label style="font-weight:normal">
                                    <asp:CheckBox ID="ckbTerms" runat="server" meta:resourcekey="ckbTermsResource1" />
                                    <%--<a style="font-size: medium">
                                        <asp:Label ID="Label1" runat="server" Text="Please read and accept the Terms of use and Privacy policy." meta:resourcekey="Label1Resource1"></asp:Label></a>--%>
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

                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-md btn-primary btn-block btn-flat" Text="Get Started" OnClick="Button1_Click" ValidationGroup="a" Style="width: 50%; margin: auto;" meta:resourcekey="Button1Resource1" />

                                    <asp:HiddenField ID="HiddenField1" runat="server" />

                                </div>

                                <div class="form-group">
                                    <div style="font-size: large">
                                        <asp:Label ID="Label2" runat="server" Text="Already you have one?" meta:resourcekey="Label2Resource3"></asp:Label>
                                        <%  if (Session["Language"].ToString() == "Auto")
                                            {%>
                                        <a href="Hospita Login.aspx">
                                            <%}
                                                else
                                                { %>
                                            <a href="Hospita Login.aspx?l=ar-EG">

                                                <%} %>
                                                <asp:Label ID="Label17" runat="server" Text="SignIn" meta:resourcekey="Label17Resource1"></asp:Label></a>
                                    </div>
                                </div>

                            </div>

                        </div>



                    </div>
                </div>


                <footer class="myfooter">

                    <%  if (Session["Language"].ToString() == "Auto")
                        {%>
                    <strong><a href="#">MediFi</a>&copy; 2019 .</strong> All rights reserved.
            
           <%}
               else
               { %>
                    <strong><a href="#">حكيم</a>&copy; 2019 .</strong> كل الحقوق محفوظة.
            <%} %>
                </footer>




                <!-- Bootstrap Modal Dialog -->
                <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">

                                                                                          <%  if (Session["Language"].ToString() == "Auto")
                                                                                                                                     {%>

                                                                                            <asp:Button ID="Button21" CssClass="close btn-link" data-dismiss="modal" runat="server" Text="x" UseSubmitBehavior="false" OnClick="Button21_Click" />


                                                                                          <%--  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                                                                                            <%}
                                    else
                                    { %><asp:Button ID="Button22" CssClass="close pull-left" data-dismiss="modal" runat="server" Text="x" UseSubmitBehavior="false" OnClick="Button21_Click" />
                                                                                         <%--   <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                                                                                            <%} %>


                                  <%--      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                                        <h4 class="modal-title">
                                            Enter OTP</h4>
                                    </div>
                                    <div class="modal-body">

                                        <div class="form-group">
                                            <asp:Label ID="Label24" runat="server" Text="Please check your email and enter your OTP to continue your registration process.." meta:resourcekey="Label24Resource1"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="* Please enter OTP" ForeColor="Red" ControlToValidate="TxtOTP" ValidationGroup="abc" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>--%></label>
                                            <asp:TextBox ID="TxtOTP" runat="server" CssClass="form-control" placeholder="Enter OTP here" OnTextChanged="TxtOTP_TextChanged" meta:resourcekey="TxtOTPResource1"></asp:TextBox>
                                       <asp:Label ID="Label31" ForeColor="Red" runat="server" meta:resourcekey="Label31Resource1"></asp:Label>
                                             </div>
                                        <div class="form-group">
                                            <asp:Button ID="BtnSubmitOTP" runat="server" Text="Submit" CssClass="btn btn-success" BackColor="#4AA9AF" BorderColor="#4AA9AF" OnClick="BtnSubmitOTP_Click" ValidationGroup="abc" meta:resourcekey="BtnSubmitOTPResource1"  />
                                            <asp:Button ID="BtnResendOTP" runat="server" Text="Resend" CssClass="btn btn-success" BackColor="#4AA9AF" BorderColor="#4AA9AF" OnClick="BtnResendOTP_Click" meta:resourcekey="BtnResendOTPResource1"  />
                                      
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
    </form>
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script>
        $("#txt_HPhone").focus(function () {
            $(this).attr('placeholder', 'Enter a number')
        }).blur(function () {
            $(this).attr('placeholder', '123456789')
        })
    </script>

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


   <%-- <script>
        $("#txt_HPhone").keyup(function (e) {
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
    </script>--%>
    <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="js/wickedpicker.js"></script>
    <script type="text/javascript">
        $('.timepicker').wickedpicker({ twentyFour: false });
    </script>
    <!-- Calendar -->
    <link rel="stylesheet" href="css/jquery-ui.css" />
    <script src="js/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#datepicker,#datepicker1,#datepicker2,#datepicker3").datepicker();
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
