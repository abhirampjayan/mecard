<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor Join.aspx.cs" Inherits="Index_JoinDoctor" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link rel="shortcut icon" type="text/css" href="images/titlelogo.png" />
    <title>Medifi|Doctor Join </title>
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
    <script src="../user/js/jquery.min.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
   <%-- <script type="text/javascript">

        //function CheckParts() {
        //    __doPostBack('', '');
        //};
        $(document).ready(function () {

            $("#<%=city.ClientID %>").autocomplete({


                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("../Service.asmx/GetCityName") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))

                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });

                },
                select: function (e, i) {
                    $("#<%=hfcityId.ClientID %>").val(i.item.val);
                    $("#<%=city.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


            //

        });
    </script>--%>
     <%--   <script type="text/javascript">
    $(function () {
        $("#city").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '../Service.asmx/GetCityName',
                  
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                };
                            }))
                        } else {
                            //If no records found, set the default "No match found" item with value -1.
                            response([{ label: 'No results found.', val: -1}]);
                        }
                    }
                });
            },
            select: function (e, u) {
                //If the No match found" item is selected, clear the TextBox.
                if (u.item.val == -1) {
                    //Clear the AutoComplete TextBox.
                    $(this).val("");
                    return false;
                }
            }
        });
    });
</script>--%>
    <style>
        .btn-primary.active.focus, .btn-primary.active:focus, .btn-primary.active:hover, .btn-primary:active.focus, .btn-primary:active:focus, .btn-primary:active:hover, .open > .dropdown-toggle.btn-primary.focus, .open > .dropdown-toggle.btn-primary:focus, .open > .dropdown-toggle.btn-primary:hover {
            color: #fff;
            background-color: #4aa9af !important;
            border-color: #4aa9af !important;
            box-shadow: none !important;
        }
    </style>

    <script type="text/javascript">
    $(function () {
        $("#specialty").autocomplete({
            source: function (request, response) {
                $.ajax({
                      url: '../Service.asmx/GetSpecialityName',
                  
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                };
                            }))
                        } else {
                            //If no records found, set the default "No match found" item with value -1.
                            response([{ label: 'No results found.', val: -1}]);
                        }
                    }
                });
            },
            select: function (e, u) {
                //If the No match found" item is selected, clear the TextBox.
                if (u.item.val == -1) {
                    //Clear the AutoComplete TextBox.
                    $(this).val("");
                    return false;
                }
            }
        });
    });
</script>

   <%-- <script type="text/javascript">

        //function CheckParts() {
        //    __doPostBack('', '');
        //};
        $(document).ready(function () {

            $("#<%=specialty.ClientID %>").autocomplete({


                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("../Service.asmx/GetSpecialityName") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))

                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });

                },
                select: function (e, i) {
                    $("#<%=hfspId.ClientID %>").val(i.item.val);
                    $("#<%=specialty.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


            //

        });
    </script>--%>


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
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=BtnSubmitOTP.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

</head>
<body class="hold-transition" style="background-image: url('../images/back4.jpg'); background-repeat: no-repeat; background-size: cover">
    <form id="form1" runat="server">



        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>



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




                        <div class="login-logo" style="margin-bottom: 0px">
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
                                <p class="pull-right" style="font-size: medium; font-weight: bold; margin-top: 1cm; position: relative; left: -20px">
                                    <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" CommandArgument="ar-EG" Text="عربى" OnClick="LinkButton1_Click" meta:resourcekey="LinkButton1Resource2"></asp:LinkButton>
                                </p>
                                <p class="login-box-msg" style="font-size: 16px; font-weight: bold; margin-bottom: 0px; margin-top: 3%">
                                    <asp:Label ID="Label6" runat="server" Text="Doctor Registration" meta:resourcekey="Label6Resource1" Style="text-transform: uppercase"></asp:Label>
                                </p>
                        </div>
                        <!-- /.login-logo -->
                        <div class="login-box-body" style="padding: 35px">


                            <div class="row">



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
                                                    <asp:Label ID="Label1" runat="server" Text="First Name" meta:resourcekey="Label1Resource1"></asp:Label></label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Enter your first name" ForeColor="Red" ControlToValidate="Fname" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="Fname" ErrorMessage="* Enter valid name" ForeColor="Red" ValidationExpression="[a-zA-Z]*$" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RegularExpressionValidator5Resource1"></asp:RegularExpressionValidator>

                                                <asp:TextBox ID="Fname" onkeyup="javascript:capitalize(this.id, this.value);" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="FnameResource1"></asp:TextBox>

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
                                                        <asp:Label ID="Label2" runat="server" Text="Last Name" meta:resourcekey="Label2Resource1"></asp:Label></label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="* Please enter last name" ForeColor="Red" ControlToValidate="Lname" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator11Resource1" Style="font-size: 16px"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="Lname" ErrorMessage="* Enter valid name" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$" Display="Dynamic" ValidationGroup="a" meta:resourcekey="RegularExpressionValidator6Resource1"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="Lname" onkeyup="javascript:capitalize(this.id, this.value);" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="LnameResource1"></asp:TextBox>
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
                                                            <asp:Label ID="Label3" runat="server" Text="Phone number" meta:resourcekey="Label3Resource1"></asp:Label></label>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ControlToValidate="phoneno" runat="server" ErrorMessage="* Enter valid number" Display="Dynamic" ForeColor="Red" ValidationExpression="[0-9]{9}" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Enter your phone number" ForeColor="Red" ControlToValidate="phoneno" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                                        <asp:TextBox ID="phoneno" CssClass="form-control" ValidationGroup="a" MaxLength="10" runat="server" meta:resourcekey="phonenoResource1"></asp:TextBox>

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
                                                        <%--<div class="form-group has-feedback">
                                                            <label>
                                                                <asp:Label ID="Label4" runat="server" Text="Email" meta:resourcekey="Label4Resource1"></asp:Label></label>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" ControlToValidate="email" runat="server" ErrorMessage="* Enter valid email" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="* Please enter email id" ForeColor="Red" ControlToValidate="email" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>

                                                            <asp:TextBox ID="email" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="emailResource1"></asp:TextBox>
                                                        </div>--%>
                                                         <div class="form-group has-feedback">
                                                                <label>
                                                                    <asp:Label ID="Label5" runat="server" Text="Speciality" meta:resourcekey="Label5Resource1"></asp:Label></label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="* Enter your specialty" ForeColor="Red" ControlToValidate="specialty" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator>
                                                                <asp:HiddenField ID="hfspId" runat="server" />
                                                                <asp:DropDownList ID="specialty" runat="server"  CssClass="form-control drop" ValidationGroup="a"></asp:DropDownList>

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
                                                           <%-- <div class="form-group has-feedback">
                                                                <label>
                                                                    <asp:Label ID="Label5" runat="server" Text="Speciality" meta:resourcekey="Label5Resource1"></asp:Label></label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="* Enter your specialty" ForeColor="Red" ControlToValidate="specialty" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator>
                                                                <asp:HiddenField ID="hfspId" runat="server" />
                                                                <asp:TextBox ID="specialty" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="specialtyResource1"></asp:TextBox>

                                                            </div>--%>
                                                            <div class="form-group has-feedback">
                                                            <label>
                                                                <asp:Label ID="Label4" runat="server" Text="Email" meta:resourcekey="Label4Resource1"></asp:Label></label>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" ControlToValidate="email" runat="server" ErrorMessage="* Enter valid email" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="* Please enter email id" ForeColor="Red" ControlToValidate="email" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>

                                                            <asp:TextBox ID="email" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="emailResource1"></asp:TextBox>
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
                                                                <div class="form-group">
                                                                    <label>
                                                                        <asp:Label ID="Label7" runat="server" Text="Qualification" meta:resourcekey="Label7Resource1"></asp:Label></label>

                                                                    <div class="row">
                                                                        <%if (Session["Language"].ToString() == "Auto")
                                                                                                                 {%>
                                                                        <div class="col-md-6">
                                                                            <%}
                                    else
                                    { %>
                                                                            <div class="col-md-6 col-md-push-6">
                                                                                <%} %>

                                                                                <asp:DropDownList ID="drpGraduation" CssClass="form-control" runat="server" meta:resourcekey="drpGraduationResource1">
                                                                                    <asp:ListItem meta:resourcekey="ListItemResource1">---UG---</asp:ListItem>
                                                                                    <asp:ListItem meta:resourcekey="ListItemResource2">MBBS</asp:ListItem>
                                                                                    <asp:ListItem meta:resourcekey="ListItemResource3">BDS</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                            <%if (Session["Language"].ToString() == "Auto")
                                                                                                                     {%>
                                                                            <div class="col-md-6">
                                                                                <%}
                                    else
                                    { %>
                                                                                <div class="col-md-6 col-md-pull-6">
                                                                                    <%} %>

                                                                                    <asp:DropDownList ID="drpPostGraduation" CssClass="form-control" runat="server" meta:resourcekey="drpPostGraduationResource1">
                                                                                        <asp:ListItem meta:resourcekey="ListItemResource4">---PG---</asp:ListItem>
                                                                                        <asp:ListItem meta:resourcekey="ListItemResource5">MD</asp:ListItem>
                                                                                        <asp:ListItem meta:resourcekey="ListItemResource6">MS</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>
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
                                                                            <div class="form-group">
                                                                                <label>
                                                                                    <asp:Label ID="Label10" runat="server" Text="City" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="* Please select your city" ForeColor="Red"  InitialValue="choose your city" ControlToValidate="city" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator22Resource1"></asp:RequiredFieldValidator>
                                                                               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ErrorMessage="* Enter only characters" ControlToValidate="city" Display="Dynamic" ValidationGroup="a" ValidationExpression="[a-zA-Z ]*$"></asp:RegularExpressionValidator>--%>
                                                                                <asp:HiddenField ID="hfcityId" runat="server" />
                                                                                <asp:DropDownList ID="city" CssClass="form-control" ValidationGroup="a" runat="server"></asp:DropDownList>
                                                                                <%--<asp:TextBox ID="city" CssClass="form-control" ValidationGroup="a" runat="server" meta:resourcekey="cityResource1"></asp:TextBox>--%>

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
                                                                                <div class="form-group">
                                                                                    <label>
                                                                                        <asp:Label ID="Label11" runat="server" Text="Gender" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                                                                    <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" meta:resourcekey="DropDownList1Resource1">
                                                                                        <asp:ListItem meta:resourcekey="ListItemResource7">Male</asp:ListItem>
                                                                                        <asp:ListItem meta:resourcekey="ListItemResource8">Female</asp:ListItem>
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
                                                                                            <asp:Label ID="Label8" runat="server" Text="Create Password" meta:resourcekey="Label8Resource1"></asp:Label></label>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Enter password" ForeColor="Red" ControlToValidate="TxtPassword" ValidationGroup="a" Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="* Minimum 6 characters required" ForeColor="Red" ControlToValidate="TxtPassword" Display="Dynamic" ValidationGroup="a" ValidationExpression="^[\s\S]{6,}$" meta:resourcekey="RegularExpressionValidator4Resource1"></asp:RegularExpressionValidator>
                                                                                        <asp:TextBox ID="TxtPassword" TextMode="Password" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="TxtPasswordResource1"></asp:TextBox>

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
                                                                                                <asp:Label ID="Label9" runat="server" Text="Confirm Password" meta:resourcekey="Label9Resource1"></asp:Label></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="* Enter password" ForeColor="Red" ControlToValidate="TxtConfirmPass" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password mismatched" ForeColor="Red" Display="Dynamic" ControlToCompare="TxtPassword" ControlToValidate="TxtConfirmPass" ValidationGroup="a" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                                                                                            <asp:TextBox ID="TxtConfirmPass" TextMode="Password" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="TxtConfirmPassResource1"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>



                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">


                                                                                        <div class="checkbox icheck">
                                                                                            <label style="margin-left: -14px">

                                                                                                <asp:CheckBox ID="ChkTerms" runat="server" meta:resourcekey="CheckBox1Resource1" />
                                                                                                <%-- <span>
                                                                          <a style="font-size: medium;>
}">
                                                                                            <asp:Label ID="Label12" runat="server" Text="Please read and accept the Terms of use and Privacy policy." meta:resourcekey="Label12Resource1"></asp:Label></a>
                                                                    </span>--%>
                                                                                                <%  if (Session["Language"].ToString() == "Auto")
                                                                                                                                         {%>
                                                                                                <span style="font-size: 16px">Please read and accept the<span style="color: #4aa9af">Terms of use</span>   and <span style="color: #4aa9af">Privacy policy.</span></span>
                                                                                                <%}
                                    else
                                    {
                                                                                                %>
                                         يرجى قراءة وقبول<span style="color: #4aa9af">تعليمات الاستخدام</span>   و <span style="color: #4aa9af">سياسة الخصوصية.</span>

                                                                                                <%} %>
                                                                                            </label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div class="social-auth-links text-center">

                                                                                            <asp:Button ID="Button1" CssClass="btn btn-md btn-primary btn-block btn-flat" OnClick="Button1_Click" ValidationGroup="a" runat="server" Text="SignUp" Style="width: 50%; margin: auto;" meta:resourcekey="Button1Resource2" />
                                                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <div style="font-size: large">
                                                                                            <asp:Label ID="Label16" runat="server" Text="Already you have one?" meta:resourcekey="Label16Resource1"></asp:Label>
                                                                                            <%  if (Session["Language"].ToString() == "Auto")
                                                                                                                                     {%>
                                                                                            <a href="Doctor login.aspx">
                                                                                                <%}
                                    else
                                    { %>
                                                                                                <a href="Doctor login.aspx?l=ar-EG">

                                                                                                    <%} %>
                                                                                                    <asp:Label ID="Label17" runat="server" Text="SignIn" meta:resourcekey="Label17Resource1"></asp:Label></a>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                            </div>













                                                                        </div>
                                                                    </div>
                                                                    <!-- /.login-box -->

                                                                    <footer class="myfooter">
                                                                        <!--<div class="pull-right hidden-xs">
                    <b>Version</b> 2.3.0
                </div>-->
                                                                        <strong>
                                                                            <a href="#">
                                                                                <asp:Label ID="Label13" runat="server" Text="MediFi" meta:resourcekey="Label13Resource1"></asp:Label></a>&copy;
                <asp:Label ID="Label15" runat="server" Text="2019" meta:resourcekey="Label15Resource1"></asp:Label>
                                                                            .</strong>
                                                                        <asp:Label ID="Label14" runat="server" Text="All rights reserved" meta:resourcekey="Label14Resource1"></asp:Label>
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

                                                                                            <asp:Button ID="Button21" CssClass="close" data-dismiss="modal" runat="server" Text="x" UseSubmitBehavior="false" OnClick="Button21_Click" />


                                                                                          <%--  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                                                                                            <%}
                                    else
                                    { %><asp:Button ID="Button22" CssClass="close pull-left" data-dismiss="modal" runat="server" Text="x" UseSubmitBehavior="false" OnClick="Button21_Click" />
                                                                                         <%--   <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                                                                                            <%} %>
                                                                                            <h4 class="modal-title">
                                                                                                <asp:Label ID="Label25" runat="server" Text="Enter OTP" meta:resourcekey="Label25Resource1"></asp:Label>
                                                                                            </h4>
                                                                                        </div>
                                                                                        <div class="modal-body">

                                                                                            <div class="form-group">
                                                                                                <asp:Label ID="Label26" runat="server" Text=" Please check your email and enter your OTP to continue your registration process.." meta:resourcekey="Label26Resource1"></asp:Label>
                                                                                            </div>
                                                                                            <div class="form-group">
                                                                                                <%--  <label>--%>
                                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Please enter OTP" ControlToValidate="TxtOTP" ForeColor="Red" ValidationGroup="cc" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator></label>--%>
                                                                                                <asp:TextBox ID="TxtOTP" runat="server" CssClass="form-control" placeholder="Enter OTP here" OnTextChanged="TxtOTP_TextChanged" meta:resourcekey="TxtOTPResource1"></asp:TextBox>
                                                                                                <asp:Label ID="Label31" Visible="false" ForeColor="Red" runat="server" meta:resourcekey="Label31Resource1"></asp:Label>
                                                                                            </div>
                                                                                            <div class="form-group">
                                                                                                <asp:Button ID="BtnSubmitOTP" runat="server" Text="Submit" CssClass="btn btn-success" BackColor="#4AA9AF" BorderColor="#4AA9AF" OnClick="BtnSubmitOTP_Click" ValidationGroup="cc" meta:resourcekey="BtnSubmitOTPResource1" />



                                                                                                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Timer ID="Timer1" runat="server" Interval="3000" OnTick="Timer1_Tick" Enabled="false"></asp:Timer>
                                     
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                                                                                                <%-- <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=BtnSubmitOTP.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>--%>
                                                                                                <script>
function myFunction() {
    document.getElementById("BtnSubmitOTP0").disabled = true;
}
                                                                                                </script>
                                                                                                <asp:Button ID="BtnSubmitOTP0" runat="server" BackColor="#4AA9AF" BorderColor="#4AA9AF" CssClass="btn btn-success" OnClick="BtnSubmitOTP0_Click" Text="Resend" meta:resourcekey="BtnSubmitOTP0Resource1" />


                                                                                            </div>
                                                                                            <div class="text-center">
                                                                                                <%-- <a style="color: #4aa9af" href="../default.aspx">
                                        <asp:Label ID="Label27" runat="server" Text="Or goto Hakkeem webisite" meta:resourcekey="Label27Resource1"></asp:Label></a>--%>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="BtnSubmitOTP" EventName="Click" />
                                                                                    <asp:AsyncPostBackTrigger ControlID="BtnSubmitOTP0" EventName="Click" />
                                                                                     <asp:AsyncPostBackTrigger ControlID="Button21" EventName="Click" />
                                                                                     <asp:AsyncPostBackTrigger ControlID="Button22" EventName="Click" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>





                                                                </div>
                                                            </div>
    </form>

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




    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script>
        $($('.ui-autocomplete-input')[0]).css('width', '300px')

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


    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>

    <%--<script>
        $("#phoneno").focus(function () {
            $(this).attr('placeholder', 'Enter a number start with 5')
        }).blur(function () {
            $(this).attr('placeholder', '123456789')
        })
    </script>--%>

    <script>
        $("#phoneno1").keyup(function (e) {
            $("#mypopup").html('');

            var validstr = '';
            var dInput = $(this).val();
            var numpattern = /^\d+$/;




            for (var i = 0; i < dInput.length; i++) {

                if ((i == 0)) {
                    if (numpattern.test(dInput[i])) {
                        console.log('validnum' + dInput[i]);
                        validstr += dInput[i];
                        //if (+dInput[i] == 5) {

                        //}
                        //else {

                        //    swal("Enter a number start with 5");
                        //    $(this).val('');
                        //    return false;

                        //}

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


</body>
</html>
