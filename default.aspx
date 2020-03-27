<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Hakkeem_Index" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html id="Html1" runat="server" dir='<%$ Resources:MulResource, TextDirection %>' xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      <link rel="shortcut icon" type="text/css" href="img/titlelogo.png" />
    <title>Medifi | A Medifi Product</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- BOOTSTRAP -->
    <link rel="stylesheet" type="text/css" href="css1/bootstrap.min.css" />
    <!-- FONT -->
    <link rel="stylesheet" type="text/css" href="css1/font-awesome.css" />
    <!-- Stylesheet -->
    <link rel="stylesheet" type="text/css" href="style1.css" media="all" />
    <link rel="stylesheet" type="text/css" href="responsive.css" />



    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#<%=txtSearch.ClientID%>").autocomplete("Search.ashx", {
                width: '19.5%',
                formatItem: function (data, i, n, value) {

                    var name;

                    if (value.split(",")[1] == "") {
                        name = "../images/doctor.png";
                    }
                    else {
                        name = value.split(",")[1];
                    }
                    return "<div class='table-responsive'><img style='width:40px;height:40px' class='img img-circle' src='" + name + "'/> " + value.split(",")[0] + "</div>";
                },
                formatResult: function (data, value) {
                    return value.split(",")[0];
                }
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#<%=txtSearch1.ClientID%>").autocomplete("Search.ashx", {
                width: '19.5%',
                formatItem: function (data, i, n, value) {

                    var name;

                    if (value.split(",")[1] == "") {
                        name = "../images/doctor.png";
                    }
                    else {
                        name = value.split(",")[1];
                    }
                    return "<div class='table-responsive'><img style='width:40px;height:40px' class='img img-circle' src='" + name + "'/> " + value.split(",")[0] + "</div>";
                },
                formatResult: function (data, value) {
                    return value.split(",")[0];
                }
            });
        });
    </script>



    <%----- with photo --%>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="wrapper">
            <%-- <nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>                        
      </button>
      <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl="~/Hakkeem/Index.aspx" ImageUrl="../../images/Logo.png" width="45px" alt="" meta:resourcekey="ImageButton1Resource1" />
    </div>
    <div class="collapse navbar-collapse" id="myNavbar">
      
     
       <ul class="nav navbar-nav navbar-right">
        <li class="active"><a href="#1"> <asp:Label ID="topcontent1" runat="server" Text="Doctor" meta:resourcekey="Label1Resource1"></asp:Label></a></li>
           <li><a href="#2" ><asp:Label ID="topcontent2" runat="server" Text="Hospital" meta:resourcekey="Label2Resource1"></asp:Label></a></li>
        <li><a href="#team15" > <asp:Label ID="topcontent3" runat="server" Text="App Features" meta:resourcekey="Label3Resource1"></asp:Label></a></li>
      
           <%try
               {
                   if (Session["ggggg"].ToString() == "0")
                   {
                      %>
           <li style="margin-top:3%;">
            <span href="#" >   <asp:HyperLink ID="HyperLink5" runat="server" meta:resourcekey="HyperLink5Resource1">[HyperLink5]</asp:HyperLink></span></li>
                     <% }
                         else
                         {
%>
   <li class="dropdown" style="margin-top:3%;">
          <span class="dropdown-toggle" data-toggle="dropdown" href="#" ><asp:HyperLink ID="HyperLink11" class="dropbtn" runat="server" Text="signin/join" meta:resourcekey="HyperLink1"></asp:HyperLink><span class="caret"></span></span>
          <ul class="dropdown-menu">
            <li><span href="#"> <asp:HyperLink ID="HyperLink22" NavigateUrl="../Index/Doctor login.aspx" runat="server" Text="Doctor" meta:resourcekey="HyperLink22Resource1"></asp:HyperLink></span></li>
            <li><span href="#"><asp:HyperLink ID="HyperLink33" NavigateUrl="../Index/Hospita Login.aspx" runat="server" Text="Hospital" meta:resourcekey="HyperLink33Resource1"></asp:HyperLink></span></li>
            <li><span href="#"> <asp:HyperLink ID="HyperLink44" NavigateUrl="../Index/SignInSignUp.aspx" runat="server" Text="User" meta:resourcekey="HyperLink44Resource1"></asp:HyperLink></span></li>
          </ul>
        </li>
            <%
                    }
                }
                catch (Exception ex)
                {%>
   <li class="dropdown" style="margin-top:3%;">
          <span class="dropdown-toggle" data-toggle="dropdown" href="#" ><asp:HyperLink ID="HyperLink1" class="dropbtn" runat="server" Text="signin/join" meta:resourcekey="HyperLink1"></asp:HyperLink><span class="caret"></span></span>
          <ul class="dropdown-menu">
            <li><span href="#"> <asp:HyperLink ID="HyperLink2" NavigateUrl="../Index/Doctor login.aspx" runat="server" Text="Doctor" meta:resourcekey="HyperLink2Resource1"></asp:HyperLink></span></li>
            <li><span href="#"><asp:HyperLink ID="HyperLink3" NavigateUrl="../Index/Hospita Login.aspx" runat="server" Text="Hospital" meta:resourcekey="HyperLink3Resource1"></asp:HyperLink></span></li>
            <li><span href="#"> <asp:HyperLink ID="HyperLink4" NavigateUrl="../Index/SignInSignUp.aspx" runat="server" Text="User" meta:resourcekey="HyperLink4Resource1"></asp:HyperLink></span></li>
          </ul>
        </li>
            <%   } %>
          

        <li style="margin-top:3%;"><span href="#" id="topcontent"><asp:LinkButton ID="LinkButton1"  runat="server"  CommandArgument="ar-EG" OnClick="LinkButton1_Click" Text="عربى" meta:resourcekey="LinkButton1Resource1"></asp:LinkButton></span></li>
      </ul>
     
    </div>
  </div>
</nav>--%>
            <!-- NAV -->
            <nav class="navigation menu_area">
                <!-- CONTAINER -->
                <div class="container">
                    <!-- Brand and toggle get grouped for better mobile display -->
                     <%if (Session["Speciality"].ToString() != "Auto")
                         { %>
                    <div class="col-md-12">

                         <asp:Button ID="Button5" CssClass="pull-right" runat="server" Text="English" style=" background: #49a9ae;
    margin-left: -3px;
    color: #fff;
    border: 0px solid;
    padding: 2px;
    width: 81px;
    margin: -3px;
    font-weight: 400;
    margin-top: 15px;
    line-height: 31px;"  OnClick="Button3_Click" /> 
                        <div class="navbar-header pull-right" >
                            <button type="button" class="navbar-toggle collapsed nav-toggle-mob-ar"  data-toggle="collapse" data-target="#nav-collapse" aria-expanded="false">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <!-- HEADER LOGO -->
                                                
                            
                                
                            <a class="navbar-brand brand-logo nav-logo-mob-ar" href="default.aspx?l=ar-EG">
                                <img src="img/logo.png" alt="" />
                            </a>
                            <!-- /HEADER LOGO -->
                        </div>
                        <!-- Collect the nav links, forms, and other content for toggling -->
                      
                         <style type="text/css">
                             @media (max-width: 991px) {
                                 .nav {
                                     display: flex;
                                     flex-flow: column;
                                 }

                                 .four {
                                     order: 1;
                                 }

                                 .three {
                                     order: 2;
                                 }

                                 .two {
                                     order: 3;
                                 }

                                 .one {
                                     order: 4;
                                 }
                             }
                         </style>


                           <div id="nav-collapse" class="top-nav collapse navbar-collapse nav-mob-ar">
                             <ul class="nav navbar-nav">
                               
                            

                                <%try
                                    {
                                        if (Session["ggggg"].ToString() == "0")
                                        {
                                %>
                                <li>
                                    <span href="#">
                                        <asp:HyperLink ID="HyperLink6" runat="server" meta:resourcekey="HyperLink5Resource1">[HyperLink5]</asp:HyperLink></span></li>
                                <% }
                                    else
                                    {
                                %>
                                <li class="dropdown one" style="margin-top: -0.5%;">
                                    <span class="dropdown-toggle" data-toggle="dropdown" href="#">
                                        <asp:HyperLink ID="HyperLink7" class="dropbtn" runat="server" Text="signin/join" meta:resourcekey="HyperLink1"></asp:HyperLink><%--<span class="caret"></span>--%></span>
                                    <ul class="dropdown-menu" style="min-width: 140px;">
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink8" NavigateUrl="../Index/Doctor login.aspx?l=ar-EG" runat="server" Text="Doctor" meta:resourcekey="HyperLink22Resource1"></asp:HyperLink></span></li>
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink9" NavigateUrl="../Index/Hospita Login.aspx?l=ar-EG" runat="server" Text="Hospital" meta:resourcekey="HyperLink33Resource1"></asp:HyperLink></span></li>
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink10" NavigateUrl="../Index/SignInSignUp.aspx?l=ar-EG" runat="server" Text="User" meta:resourcekey="HyperLink44Resource1"></asp:HyperLink></span></li>
                                    </ul>
                                </li>
                                <%
                                        }
                                    }
                                    catch (Exception ex)
                                    {%>
                                <li class="dropdown one" style="margin-top: -0.5%;">
                                    <span class="dropdown-toggle" data-toggle="dropdown" href="#">
                                        <asp:HyperLink ID="HyperLink12" class="dropbtn" runat="server" Text="signin/join" meta:resourcekey="HyperLink1"></asp:HyperLink><%--<span class="caret"></span>--%></span>
                                    <ul class="dropdown-menu" style="min-width: 140px;">
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink13" NavigateUrl="Index/Doctor login.aspx?l=ar-EG" runat="server" Text="Doctor" meta:resourcekey="HyperLink2Resource1"></asp:HyperLink></span></li>
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink14" NavigateUrl="Index/Hospita Login.aspx?l=ar-EG" runat="server" Text="Hospital" meta:resourcekey="HyperLink3Resource1"></asp:HyperLink></span></li>
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink15" NavigateUrl="Index/SignInSignUp.aspx?l=ar-EG" runat="server" Text="User" meta:resourcekey="HyperLink4Resource1"></asp:HyperLink></span></li>
                                    </ul>
                                </li>




                                <%   } %>

                                 <li class="two"><a href="#hakkeem_app">
                                    <asp:Label ID="Label55" runat="server" Text="APP FEATURES" meta:resourcekey="Label52"></asp:Label></a></li>
                                  <li class="three"><a href="#join">
                                    <asp:Label ID="Label54" runat="server" Text="HOSPITAL" meta:resourcekey="Label51"></asp:Label></a></li>
                                 <li class="four"><a href="#doctor_reputed">
                                    <asp:Label ID="Label53" runat="server" Text="DOCTOR" meta:resourcekey="Label50"></asp:Label></a></li>
                              
                               
                            </ul>
                        </div>
                        </div>
                     
                         <%--   <asp:HyperLink ID="HyperLink6" runat="server" CssClass="arb">Arabic</asp:HyperLink>
                                  <asp:HyperLink ID="HyperLink7" runat="server" CssClass="eng">English</asp:HyperLink>--%>
                         
<%--                            <asp:Button ID="Button4" runat="server" Text="Arabic" style=" background: #49a9ae;
    color: #fff;
    border: 0px solid;
    padding: 2px;
    width: 81px;
    margin: 0;
    font-weight: 400;
    margin-top: 0px;
    line-height: 31px; " OnClick="Button2_Click" meta:resourcekey="Button2Resource1"/> --%> 
       

                          <%--  <button class="btn-btn">Arabic</button>
                            <button class="btn-btn eng">English</button>--%>
                       
                          <%-- <style>
                            input[type=submit].btn-btn {
 background: #49a9ae;
    color: #fff;
    border: 0px solid;
    padding: 2px;
    width: 81px;
    margin: 0;
    font-weight: 400;
    margin-top: 0px;
    line-height: 31px; 
                            }
                        </style>
                         <style>
                            input[type=submit].btn-btn eng {
 background: #4b4b4b;
    margin-left: -3px;
                            }
                        </style>--%>
                        <%--<style>
                     #arb {
  background: #49a9ae;
    color: #fff;
    border: 0px solid;
    padding: 2px;
    width: 81px;
    margin: 0;
    font-weight: 400;
    margin-top: 0px;
    line-height: 31px; 
}
#eng {
 background: #4b4b4b;
    margin-left: -3px;
}
</style>--%>
                  
                        <%



                            }
                            else
                            {
                                    %>
                    <div class="col-md-12">
                        
                                                              <asp:Button ID="Button2" runat="server" Visible="false" Text="عربى" style=" background: #49a9ae;
    color: #fff;
    border: 0px solid;
    padding: 2px;
    width: 81px;
    margin: 0;
    font-weight: 400;
    margin-top: 15px;
    line-height: 31px; " OnClick="Button3_Click" CssClass="pull-left" /> 
                     
                            <div class="navbar-header" >
                            <button type="button" class="navbar-toggle" collapsed" data-toggle="collapse" data-target="#nav-collapse" aria-expanded="false">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <!-- HEADER LOGO --> 
                            <a class="navbar-brand brand-logo" href="default.aspx">
                                <img src="img/logo.png" alt="" />
                            </a>
                            <!-- /HEADER LOGO -->
                                                
                       
                         <%--   <asp:HyperLink ID="HyperLink6" runat="server" CssClass="arb">Arabic</asp:HyperLink>
                                  <asp:HyperLink ID="HyperLink7" runat="server" CssClass="eng">English</asp:HyperLink>--%>
    
       

                  
                        </div>
                 
                           <div id="nav-collapse" class="top-nav collapse navbar-collapse navbar-right nav-mob-en">
                            <ul class="nav navbar-nav">
                                <li><a href="#doctor_reputed">
                                    <asp:Label ID="Label50" runat="server" Text="DOCTOR" meta:resourcekey="Label50"  ></asp:Label></a></li>
                                <li><a href="#join">
                                    <asp:Label ID="Label51" runat="server" Text="HOSPITAL" meta:resourcekey="Label51" ></asp:Label></a></li>
                                <li><a href="#hakkeem_app">
                                    <asp:Label ID="Label52" runat="server" Text="APP FEATURES" meta:resourcekey="Label52"></asp:Label></a></li>
                                <%-- <li><a href="#">SIGNIN / JOIN</a></li>--%>
                                <%--<li><a href="#">JOIN</a></li>--%>

                                <%try
                                    {
                                        if (Session["ggggg"].ToString() == "0")
                                        {
                                %>
                                <li>
                                    <span href="#">
                                        <asp:HyperLink ID="HyperLink5" runat="server" meta:resourcekey="HyperLink5Resource1">[HyperLink5]</asp:HyperLink></span></li>
                                <% }
                                    else
                                    {
                                %>
                                <li class="dropdown" style="margin-top: -0.5%;">
                                    <span class="dropdown-toggle" data-toggle="dropdown" href="#">
                                        <asp:HyperLink ID="HyperLink11"  runat="server" Text="signin/join" meta:resourcekey="HyperLink1"  ></asp:HyperLink><span class="caret"></span><%--<span class="caret"></span>--%>

                                    </span>
                                    <ul class="dropdown-menu" style="border: none;
    box-shadow: none;min-width: 140px;">
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink22" NavigateUrl="Index/Doctor login.aspx" runat="server" Text="Doctor" meta:resourcekey="HyperLink22Resource1"></asp:HyperLink></span></li>
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink33" NavigateUrl="Index/Hospita Login.aspx" runat="server" Text="Hospital" meta:resourcekey="HyperLink33Resource1"></asp:HyperLink></span></li>
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink44" NavigateUrl="Index/SignInSignUp.aspx" runat="server" Text="User" meta:resourcekey="HyperLink44Resource1"></asp:HyperLink></span></li>
                                    </ul>
                                </li>
                                <%
                                        }
                                    }
                                    catch (Exception ex1)
                                    {%>
                                <li class="dropdown" style="margin-top: -0.5%;">
                                    <span class="dropdown-toggle" data-toggle="dropdown" href="#">
                                        <asp:HyperLink ID="HyperLink1"  runat="server" Text="signin/join" meta:resourcekey="HyperLink1" ></asp:HyperLink><span class="caret"></span></span>
                                    <ul class="dropdown-menu" style="border: none;
    box-shadow: none;min-width: 140px;">
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink2" NavigateUrl="Index/Doctor login.aspx" runat="server" Text="Doctor" meta:resourcekey="HyperLink2Resource1"></asp:HyperLink></span></li>
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink3" NavigateUrl="Index/Hospita Login.aspx" runat="server" Text="Hospital" meta:resourcekey="HyperLink3Resource1"></asp:HyperLink></span></li>
                                        <li><span href="#">
                                            <asp:HyperLink ID="HyperLink4" NavigateUrl="Index/SignInSignUp.aspx" runat="server" Text="User" meta:resourcekey="HyperLink4Resource1"></asp:HyperLink></span></li>
                                    </ul>
                                </li>
                                <%   } %>
                            </ul>
                        </div>

                          </div>
                   
           
                         
                            <%
                                }

%>



                     
                   

                   
                </div>
                <!-- /.END CONTAINER -->
            </nav>
            <!-- /.END NAV -->


            <!-- SLIDER  -->
            <div id="slider">
                <section class="intro-section">
                    <!-- CONTAINER -->
                    <div class="container">
                        <!-- WELCOM MESSAGE -->
                         <% 

                             if (Session["Speciality"].ToString() != "Auto")
                             {



                                    %>

                        
                        <div class="col-md-12 col-sm-12 text-center silder_content">
                            <!-- HANDING -->
                            <div class="inner-handing">
                                <h2>
                                    <asp:Label ID="Label1" runat="server" Text="Feeling not well? Find a doctor." meta:resourcekey="Label1Resource1"></asp:Label></h2>
                                <p>
                                    <asp:Label ID="Label2" runat="server" Text="
Manage your health.
Verified doctors to choose from. Your health records when you need them." meta:resourcekey="Label2Resource1"></asp:Label>
                                </p>
                            </div>
                            <!-- /.END HANDING -->
                            <!-- BUTTON -->

                            <style type="text/css">
                                @media (max-width: 991px) {
                                    .inner-button {
                                        display: flex;
                                        flex-flow: column;
                                    }

                                    .four {
                                        order: 1;
                                    }

                                    .three {
                                        order: 2;
                                    }

                                    .two {
                                        order: 3;
                                    }

                                    .one {
                                        order: 4;
                                    }
                                }
                            </style>



                            <div class="inner-button">
                                  <div class="col-md-2 col-sm-6 col_spas one">
                                    <asp:Button ID="Searchbtn" runat="server" Text="Search Doctor" OnClick="Searchbtn_Click" meta:resourcekey="SearchbtnResource1" CssClass="btn Search" /><%--<button class="btn Search">Search Doctor</button>--%>
                                </div>
                                
           
                                  <div class="col-md-3 col-sm-6 col_spas two">
                                    <asp:DropDownList ID="Speciality0"   CssClass="drop" runat="server" OnSelectedIndexChanged="Speciality0_SelectedIndexChanged" meta:resourcekey="Speciality0Resource1" >

                                     
                                    </asp:DropDownList><%--<input type="text" class="name" placeholder="Location"/>--%>
                                </div>
                                                      <div class="col-md-4 col-sm-6 col_spas three">
                                    <asp:DropDownList ID="Speciality" AutoPostBack="true" CssClass="drop" runat="server" OnSelectedIndexChanged="Speciality_SelectedIndexChanged" meta:resourcekey="SpecialityResource1">

                                     
                                    </asp:DropDownList><%--<input type="text" class="name" placeholder="Speciality"/>--%>
                                </div>
                                <div class="col-md-3 col-sm-6 col_spas four" >
                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="اسم الطبيب أو المستشفى" CssClass="name" OnTextChanged="txtSearch_TextChanged" meta:resourcekey="txtSearchResource1" AutoPostBack="True"></asp:TextBox><%--<input type="text" class="name" placeholder="Doctor or Hospital Name"/>--%>
                                </div>
                               
                              
                              
                                <asp:HiddenField ID="hfCustomerId" runat="server" />
                                <h1 style="padding-top:10%">

                                    <asp:Label ID="Label3" runat="server" Text="BOOK AN APPOINTMENT NOW!" meta:resourcekey="Label3Resource1"></asp:Label></h1>
                            </div>
                            <!-- /.END BUTTONS -->
                        </div>

                         



   <% }
       else
       {%>
                             

                           <div class="col-md-12 col-sm-12 text-center silder_content">
                            <!-- HANDING -->
                            <div class="inner-handing">
                                <h2>
                                    <asp:Label ID="Label56" runat="server" Text="Feeling not well? Find a doctor." meta:resourcekey="Label1Resource1"></asp:Label></h2>
                                <p>
                                    <asp:Label ID="Label57" runat="server" Text=" Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has
							been the industry's standard dummy text ever since Lorem Ipsum is simply dummy." meta:resourcekey="Label2Resource1"></asp:Label>
                                </p>
                            </div>
                            <!-- /.END HANDING -->
                            <!-- BUTTON -->
                            <div class="inner-button">
                                <div class="col-md-3 col-sm-6 col_spas" >
                                    <asp:TextBox ID="txtSearch1" runat="server" placeholder="Doctor Name" CssClass="name" OnTextChanged="txtSearch_TextChanged" meta:resourcekey="txtSearchResource1"></asp:TextBox><%--<input type="text" class="name" placeholder="Doctor or Hospital Name"/>--%>
                                </div>
                            
                                <div class="col-md-3 col-sm-6 col_spas">
                                    <asp:DropDownList ID="DropDownList2"  AutoPostBack="true"  CssClass="drop" runat="server" OnSelectedIndexChanged="Speciality_SelectedIndexChanged" meta:resourcekey="Speciality0Resource1">

                                        
                                    </asp:DropDownList><%--<input type="text" class="name" placeholder="Location"/>--%>
                                </div>
                                    <div class="col-md-4 col-sm-6 col_spas">
                                    <asp:DropDownList ID="DropDownList1"   CssClass="drop" runat="server" OnSelectedIndexChanged="Speciality0_SelectedIndexChanged" meta:resourcekey="SpecialityResource1">

                                       
                                    </asp:DropDownList><%--<input type="text" class="name" placeholder="Speciality"/>--%>
                                </div>
                                <div class="col-md-2 col-sm-6 col_spas">
                                    <asp:Button ID="Button6" runat="server" Text="Search Doctor" OnClick="Searchbtn_Click" meta:resourcekey="SearchbtnResource1" CssClass="btn Search" /><%--<button class="btn Search">Search Doctor</button>--%>
                                </div>
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <h1 style="padding-top:10%">
                                   <%-- <asp:HyperLink ID="h1" runat="server"  NavigateUrl="~/Index/SignInSignUp.aspx"><asp:Label ID="Label58" runat="server" Text="BOOK AN APPOINTMENT NOW!" meta:resourcekey="Label3Resource1"></asp:Label></asp:HyperLink>--%>
                       <a href="../Index/SignInSignUp.aspx" class="book" style="font-size:25px">           <asp:Label ID="Label58" runat="server" Text="BOOK AN APPOINTMENT NOW!" meta:resourcekey="Label3Resource1"></asp:Label></a>  </h1>
                            </div>
                            <!-- /.END BUTTONS -->
                        </div>





                                                <%}%>
                                           




                    </div>
                    <!-- /.END CONTAINER -->
                </section>
            </div>
            <!-- /.END SLIDER  -->

            <%--  <div class="padding10"></div>--%>
            <!-- INNER CONTENT -->
            <!-- doctor -->
            <asp:HiddenField ID="HiddenField1" runat="server" Value="1" />

            <%--<div class="container-fluid no-padding" id="seth2" style=" background:#fff;margin-left:2%;margin-right:2%;">
          <div class="container padding80" id="seth" style="margin-top: -2%;" >
                <div class="col-md-8 col-md-offset-2 text-center space50" id="1" style="margin-top: -3%; margin-bottom: 2%;">
                    <h1><b style="font-size: 1.4em;">
                        <asp:Label ID="Label41" runat="server" Text="Are you a well reputed doctor?" meta:resourcekey="Label41Resource1"></asp:Label></b></h1>

                </div>
                <div class="container">
                    <div class="section-info ">
                        <div class="col-md-6">
                            <h4><b style="font-size: 1.25em; color: #021753; margin-left: 18%;">
                                <asp:Label ID="Label42" runat="server" Text="Why do doctors join Hakkeem" meta:resourcekey="Label42Resource1"></asp:Label></b></h4>
                            <div class="space30"></div>
                            <div class="row">
                                <div class="col-md-6">
                                    <h4 style="margin-left: 14%; color: #021753">
                                        <asp:Label ID="Label43" runat="server" Text="Multiply Performance" meta:resourcekey="Label43Resource1"></asp:Label></h4>
                                    <div class="space30"></div>
                                    <h1><b style="font-size: 1.8em;">
                                        <asp:Label ID="Label44" runat="server" Text="78%" meta:resourcekey="Label44Resource1"></asp:Label></b></h1>
                                    <h5 style="color: #021753;">
                                        <asp:Label ID="Label45" runat="server" Text="78% of patients who rebook in the same specialty chose the same practice." meta:resourcekey="Label45Resource1"></asp:Label></h5>

                                </div>
                                <div class="col-md-6">
                                    <h4 style="margin-left: 14%; color: #021753">
                                        <asp:Label ID="Label46" runat="server" Text="Improve Apperance" meta:resourcekey="Label46Resource1"></asp:Label></h4>
                                    <div class="space30"></div>
                                    <h1><b style="font-size: 1.8em;">
                                        <asp:Label ID="Label47" runat="server" Text="9%" meta:resourcekey="Label47Resource1"></asp:Label></b></h1>
                                    <h5 style="color: #021753;">
                                        <asp:Label ID="Label48" runat="server" Text="Hakkeem Reminders improves attendance by 9%." meta:resourcekey="Label48Resource1"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="space30"></div>
                            <div class="row">
                                <div class="col-md-6">
                                    <h4 style="margin-left: 14%; color: #021753">
                                        <asp:Label ID="Label49" runat="server" Text="High Refunding" meta:resourcekey="Label49Resource1"></asp:Label></h4>
                                    <div class="space30"></div>
                                    <h1><b style="font-size: 1.8em;">
                                        <asp:Label ID="Label50" runat="server" Text="91%" meta:resourcekey="Label50Resource1"></asp:Label></b></h1>
                                    <h5 style="color: #021753;">
                                        <asp:Label ID="Label51" runat="server" Text="91% of Hakkeem patients are commercially insured or paying with cash." meta:resourcekey="Label51Resource1"></asp:Label></h5>
                                </div>
                                <div class="col-md-6">
                                    <h4 style="margin-left: 14%; color: #021753">
                                        <asp:Label ID="Label52" runat="server" Text="Large search presence" meta:resourcekey="Label52Resource1"></asp:Label></h4>
                                    <div class="space30"></div>
                                    <h1><b style="font-size: 1.8em;">
                                        <asp:Label ID="Label53" runat="server" Text="100M" meta:resourcekey="Label53Resource1"></asp:Label></b></h1>
                                    <h5 style="color: #021753;">
                                        <asp:Label ID="Label54" runat="server" Text="120 million annual doctor searches show Hakkeem in the top results." meta:resourcekey="Label54Resource1"></asp:Label></h5>
                                </div>
                            </div>


                        </div>
                        <div class="col-md-6">
                            <h4><b style="font-size: 1.25em; color: #021753;">
                                <asp:Label ID="Label55" runat="server" Text="Across the country  doctors rely on Hakkeem" meta:resourcekey="Label55Resource1"></asp:Label></b></h4>

                           
                            <div>
                                <a href="doctor.aspx" class="btn btn-md btn-info" role="button" style="margin-left:20%;margin-top:4%;"><b>
                                    <asp:Label ID="Label56" runat="server" Text="List Your skills" meta:resourcekey="Label56Resource1"></asp:Label></b></a>
                            </div>

                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>

            </div>
            </div>--%>

            <!-- REPUTED  -->
            <section id="doctor_reputed">
                <div class="container text-center">
                    <div class="row">
                        <div class="doctor_reputed_content">
                            <div class="reputed_parsen">
                                <%
                                    if (Session["Speciality"].ToString() != "Auto")
                                    {

                                    %>

                                 <div class="col-md-3 col-sm-6">
                                    <div class="Performance">
                                        <img src="img/form4.png" alt="" class="img1" />
                                        <h4>
                                            <asp:Label ID="Label13" runat="server" Text="Large search presence" meta:resourcekey="Label13Resource1"></asp:Label></h4>
                                        <h1>
                                            <asp:Label ID="Label14" runat="server" Text="M100" meta:resourcekey="Label14Resource1"></asp:Label></h1>
                                        <p>
                                            <asp:Label ID="Label15" runat="server" Text="  120 million annual doctor
									searches show MediFi in
									the top results." meta:resourcekey="Label15Resource1"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                  <div class="col-md-3 col-sm-6">
                                    <div class="Performance">
                                        <img src="img/form3.png" alt="" class="img1"/>
                                        <h4>
                                            <asp:Label ID="Label10" runat="server" Text="High Refunding" meta:resourcekey="Label10Resource1"></asp:Label></h4>
                                        <h1>
                                            <asp:Label ID="Label11" runat="server" Text="91%" meta:resourcekey="Label11Resource1"></asp:Label></h1>
                                        <p>
                                            <asp:Label ID="Label12" runat="server" Text=" 91% of MediFi patients
									are commercially insured
									or paying with cash." meta:resourcekey="Label12Resource1"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                 <div class="col-md-3 col-sm-6">
                                    <div class="Performance">
                                        <img src="img/form2.png" alt=""class="img1" />
                                        <h4>
                                            <asp:Label ID="Label7" runat="server" Text="Improve Apperance" meta:resourcekey="Label7Resource1"></asp:Label></h4>
                                        <h1>
                                            <asp:Label ID="Label8" runat="server" Text="9%" meta:resourcekey="Label8Resource1"></asp:Label></h1>
                                        <p>
                                            <asp:Label ID="Label9" runat="server" Text="  MediFi Reminders improves
									attendance by 9%." meta:resourcekey="Label9Resource1"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="Performance">
                                        <img src="img/form1.png" alt="" class="img1" />
                                        <h4>
                                            <asp:Label ID="Label4" runat="server" Text="Multiply Performance" meta:resourcekey="Label4Resource1"></asp:Label></h4>
                                        <h1>
                                            <asp:Label ID="Label5" runat="server" Text="78%" meta:resourcekey="Label5Resource1"></asp:Label></h1>
                                        <p>
                                            <asp:Label ID="Label6" runat="server" Text=" 78٪ من المرضى الذين ريبوك
في نفس التخصص اختار
نفس الممارسة." meta:resourcekey="Label6Resource6"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                               
                              
                               

                                <%}
                                    else
                                    { %>
                                 <div class="col-md-3 col-sm-6">
                                    <div class="Performance">
                                        <img src="img/form1.png" alt="" class="img1"/>
                                        <h4>
                                            <asp:Label ID="Label89" runat="server" Text="Multiply Performance" meta:resourcekey="Label4Resource1"></asp:Label></h4>
                                        <h1>
                                            <asp:Label ID="Label90" runat="server" Text="78%" meta:resourcekey="Label5Resource1"></asp:Label></h1>
                                        <p>
                                            <asp:Label ID="Label91" runat="server" Text=" 78% of patients who rebook
									in the same specialty chose
									the same practice." meta:resourcekey="Label6Resource1"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="Performance">
                                        <img src="img/form2.png" alt="" class="img1" />
                                        <h4>
                                            <asp:Label ID="Label92" runat="server" Text="Improve Apperance" meta:resourcekey="Label7Resource1"></asp:Label></h4>
                                        <h1>
                                            <asp:Label ID="Label93" runat="server" Text="9%" meta:resourcekey="Label8Resource1"></asp:Label></h1>
                                        <p>
                                            <asp:Label ID="Label94" runat="server" Text="  MediFi Reminders improves
									attendance by 9%." meta:resourcekey="Label9Resource1"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="Performance">
                                        <img src="img/form3.png" alt="" class="img1" />
                                        <h4>
                                            <asp:Label ID="Label95" runat="server" Text="High Refunding" meta:resourcekey="Label10Resource1"></asp:Label></h4>
                                        <h1>
                                            <asp:Label ID="Label96" runat="server" Text="91%" meta:resourcekey="Label11Resource1"></asp:Label></h1>
                                        <p>
                                            <asp:Label ID="Label97" runat="server" Text=" 91% of MediFi patients
									are commercially insured
									or paying with cash." meta:resourcekey="Label12Resource1"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <div class="Performance">
                                        <img src="img/form4.png" alt="" class="img1" />
                                        <h4>
                                            <asp:Label ID="Label98" runat="server" Text="Large search presence" meta:resourcekey="Label13Resource1"></asp:Label></h4>
                                        <h1>
                                            <asp:Label ID="Label99" runat="server" Text="100M" meta:resourcekey="Label14Resource1"></asp:Label></h1>
                                        <p>
                                            <asp:Label ID="Label100" runat="server" Text="  120 million annual doctor
									searches show MediFi in
									the top results." meta:resourcekey="Label15Resource1"></asp:Label>
                                        </p>
                                    </div>
                                </div>


                                <%} %>

                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- /.END REPUTED  -->


            <%--  <div class="container-fluid no-padding" id="hospital1" style="margin-top:1.5%;margin-left:2%;margin-right:2%;" >
                <div class="container padding80" id="2" style="margin-top:1.5%;">
              
                    <div class="col-md-8 col-md-offset-2 text-center space50" id="hosp1">
                        <h1><b style="font-size: 1.4em;margin-left:25%;">
                            <asp:Label ID="Label57" runat="server" Text="Till you not join?" meta:resourcekey="Label57Resource1"></asp:Label></b></h1>
                        <p></p>
                    </div>
                    <div class="container">
                        <div class="section-info ">
                            <div class="col-md-6" id="hosp" style="margin-top:5%;">
                                <h1><b>
                                    <asp:Label ID="Label58" runat="server" Text="Patients are losing" meta:resourcekey="Label58Resource1"></asp:Label><br>
                                    <asp:Label ID="Label59" runat="server" Text=" patience when trying" meta:resourcekey="Label59Resource1"></asp:Label><br>
                                    <asp:Label ID="Label60" runat="server" Text="to access your care." meta:resourcekey="Label60Resource1"></asp:Label></b></h1>
                                <div class="space30"></div>
                                <div class="panel-group" id="accordion">

                                    <div class="clearfix space10"></div>

                                    <div class="clearfix space10"></div>

                                    <div class="clearfix space10"></div>

                                </div>
                                <div>
                                    <h5><b style="font-size: 1.3em; margin-bottom: 6%; color: #ffffff;">
                                        <asp:Label ID="Label61" runat="server" Text="See why we're the perfect partner for health systems." meta:resourcekey="Label61Resource1"></asp:Label></b></h5>
                                    <div class="space40"></div>
                                    <a href="hospital.aspx" class="btn btn-md btn-info" role="button" style="margin-left:11%;margin-top:3%;"><b>
                                        <asp:Label ID="Label62" runat="server" Text="Join us & our partners" meta:resourcekey="Label62Resource1"></asp:Label></b></a>
                                </div>
                            </div>
                            <div class="col-md-6" style="margin-top:10%;">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div id="skills">

                                            <img src="../images/other/hospitallogo1.png" class="img-responsive" alt="" />
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="col-md-6">
                                        <div id="skills1">
                                            <img src="../../images/other/hospitallogo2.png" class="img-responsive" alt="" />

                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div id="skills">

                                            <img src="../../images/other/hospitallogo3.png" class="img-responsive" alt="" />
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="col-md-6">
                                        <div id="skills1">

                                            <img src="../../images/other/hospitallogo4.png" class="img-responsive" alt="" />
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
             </div>--%>

            <!-- TITLE -->
            <section id="title">
                <div class="container">
                    <div class="row">
                        <div class="title reputed_title text-center">
                            <h2>
                                <asp:Label ID="Label16" runat="server" Text="Are you a well reputed doctor?" meta:resourcekey="Label16Resource1"></asp:Label></h2>
                            <h3>
                                <asp:Label ID="Label17" runat="server" Text="Why do doctors join MediFi?" meta:resourcekey="Label17Resource1"></asp:Label></h3>
                        </div>
                    </div>
                </div>
            </section>
            <!-- /.END TITLE -->

            <!-- SKILLS -->
            <section id="skills">
                <div class="container">

                     <% 

                         if (Session["Speciality"].ToString() != "Auto")
                         {



                                    %>
                    <div class="row">
                        <div class="col-md-offset-1 col-md-10">
                            <div class="col-md-9 col-sm-9 pull-right" style="margin-top:48px">
                                <div class="skills_title text-center">
                                    <h3>
                                        <asp:Label ID="Label18" runat="server" Text="Across the country doctors rely on MediFi" meta:resourcekey="Label18Resource1"></asp:Label></h3>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3">
                                <div class="skills_btn">

                                    <asp:HyperLink ID="HyperLink16" class="btn List" runat="server" meta:resourcekey="Label19Resource1" NavigateUrl="../index/doctor join.aspx?l=ar-EG">List Your Skills</asp:HyperLink>
                                   <%-- <button class="btn List" >
                                        <asp:Label ID="Label19" runat="server" Text="List Your Skills" meta:resourcekey="Label19Resource1"></asp:Label></button>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%}
                        else
                        { %>

                      <div class="row">
                        <div class="col-md-offset-1 col-md-10">
                            <div class="col-md-9 col-sm-9"  style="margin-top:48px">
                                <div class="skills_title text-center">
                                    <h3>
                                        <asp:Label ID="Label59" runat="server" Text="Across the country doctors rely on MediFi" meta:resourcekey="Label18Resource1"></asp:Label></h3>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3">
                                <div class="skills_btn">
                                            <asp:HyperLink ID="HyperLink17" class="btn List" runat="server" meta:resourcekey="Label19Resource1" NavigateUrl="../index/doctor join.aspx">List Your Skills</asp:HyperLink>
                                 <%--   <button class="btn List" >
                                        <asp:Label ID="Label60" runat="server" Text="List Your Skills" meta:resourcekey="Label19Resource1"></asp:Label></button>--%>
                                </div>
                            </div>
                        </div>
                    </div>




                    <%} %>

                </div>
            </section>
            <!-- /.END SKILLS -->

            <!-- TITLE -->
            <section id="join">
                <div class="container">

                     <% 

                         if (Session["Speciality"].ToString() != "Auto")
                         { %>


                    <div class="row">
                        <div class="title join_title-ar text-center">
                            <h2>
                                <asp:Label ID="Label20" runat="server" Text="Till you not join?" meta:resourcekey="Label20Resource1"></asp:Label></h2>
                            <div class="col-md-7 col-sm-8 text-left pull-right" style="margin-top:47px;position:relative;left:20px">
                                <h3>
                                    <asp:Label ID="Label21" runat="server" Text="Patients are losing patience rely when trying to access your care." meta:resourcekey="Label21Resource1"></asp:Label></h3>
                            </div>
                            <div class="col-md-3 col-sm-4 text-right">
                                   <a href="../Index/hospital_Regn.aspx?l=ar-EG" class="btn Join text-left">   
                                    <asp:Label ID="Label22" runat="server" Text="Join Us & Our Partner" meta:resourcekey="Label22Resource1"></asp:Label></a>
                            </div>

                        </div>
                    </div>

                    <%}
                        else
                        { %>

                     <div class="row">
                        <div class="title join_title text-center">
                            <h2>
                                <asp:Label ID="Label61" runat="server" Text="Till you not join?" meta:resourcekey="Label20Resource1"></asp:Label></h2>
                            <div class="col-md-9 col-sm-8 text-left" style="margin-top:47px">
                                <h3>
                                    <asp:Label ID="Label62" runat="server" Text="Patients are losing patience rely when trying to access your care." meta:resourcekey="Label21Resource1"></asp:Label></h3>
                            </div>
                            <div class="col-md-3 col-sm-4 text-right">
                           
                              <a href="../Index/hospital_Regn.aspx" class="btn Join text-left">      <asp:Label ID="Label63" runat="server" Text="Join Us & Our Partner" meta:resourcekey="Label22Resource1"></asp:Label></a>
                            </div>

                        </div>
                    </div>



                    <%} %>
                </div>
            </section>
            <!-- /.END TITLE -->

            <section id="new_feature_area">
                <div class="container">
                    <div class="row">
                        <%--<div class="feature">

                             <% 

                                 if (Session["Speciality"].ToString() != "Auto")
                                 { %>
                              <div class="col-md-3 col-sm-6">
                                <div class="single_logo">
                                    <img src="img/logo3.jpg" alt="" />
                                </div>
                            </div>
                             <div class="col-md-3 col-sm-6">
                                <div class="single_logo fourslog">
                                    <img src="img/logo4.jpg" alt="" />
                                </div>
                            </div>
                             <div class="col-md-3 col-sm-6">
                                <div class="single_logo">
                                    <img src="img/logo1.jpg" alt="" />
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="single_logo">
                                    <img src="img/logo2.jpg" alt="" />
                                </div>
                            </div>
                           
                           
                          
                            <%}
                                else
                                { %>
                              <div class="col-md-3 col-sm-6">
                                <div class="single_logo">
                                    <img src="img/logo2.jpg" alt="" />
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="single_logo">
                                    <img src="img/logo1.jpg" alt="" />
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="single_logo fourslog">
                                    <img src="img/logo4.jpg" alt="" />
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6">
                                <div class="single_logo">
                                    <img src="img/logo3.jpg" alt="" />
                                </div>
                            </div>


                            <%} %>
                        </div>--%>
                    </div>
                </div>
            </section>

            <!-- HAKKEEM APP -->
            <section id="hakkeem_app">
                <div class="container">
                       <% 

                           if (Session["Speciality"].ToString() != "Auto")
                           { %>

                    <div class="row">
                        <div class="hakkeem_app_content">
                            <div class="col-md-7 col-sm-6 pull-right" style="position:relative;left:-50px">
                                <div class="hakkeem_app_text text-right">
                                    <h2>
                                        <asp:Label ID="Label23" runat="server" Text="Get The MediFi App" meta:resourcekey="Label23Resource1"></asp:Label></h2>
                                </div>
                            </div>
                            <div class="col-offset-1 col-md-4 col-sm-6">
                                <div class="hakkeem_app_img text-center">
                                    <img src="img/get_phone.png" alt="" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%}
                        else
                        { %>
                     <div class="row">
                        <div class="hakkeem_app_content">
                            <div class="col-md-7 col-sm-6">
                                <div class="hakkeem_app_text text-right">
                                    <h2>
                                        <asp:Label ID="Label64" runat="server" Text="Get The MediFi App" meta:resourcekey="Label23Resource1"></asp:Label></h2>
                                </div>
                            </div>
                            <div class="col-offset-1 col-md-4 col-sm-6">
                                <div class="hakkeem_app_img text-center">
                                    <img src="img/get_phone.png" alt="" />
                                </div>
                            </div>
                        </div>
                    </div>


                    <%} %>
                </div>
            </section>
            <!-- /.END HAKKEEM APP -->
            <!-- DOWNLOAD -->
            <section id="Download">
                <div class="container">

                     <% 

                         if (Session["Speciality"].ToString() != "Auto")
                         { %>

                    <div class="row">
                        <div class="hakkeem_app_content pull-right">
                                <div class="col-md-5">
                                <div class="Download_title hakkeem_app_text text-center">
                                    <h3>
                                        <asp:Label ID="Label27" runat="server" Text="Available now" meta:resourcekey="Label27Resource1"></asp:Label></h3>
                                    <!-- PLAY DOWNLOAS -->
                                    <div class="col-md-6 col-sm-4 margin_left_right">
                                        <a href="#" class="">
                                            <div class="media play_btn">
                                                <div class="media-left play_btn_icon">
                                                    <i class="fa fa-android"></i>
                                                </div>
                                                <div class="media-body play_btn_text">
                                                    <h4 class="media-heading">
                                                        <asp:Label ID="Label28" runat="server" Text="Download on the" meta:resourcekey="Label28Resource1"></asp:Label></h4>
                                                    <h5>
                                                        <asp:Label ID="Label29" runat="server" Text="Play Store" meta:resourcekey="Label29Resource1"></asp:Label></h5>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <!-- /.END PLAY DOWNLOAS -->
                                    <!-- PLAY DOWNLOAS -->
                                    <div class="col-md-6 col-sm-4 margin_center">
                                        <a href="#">
                                            <div class="media play_btn">
                                                <div class="media-left play_btn_icon">
                                                    <i class="fa fa-apple"></i>
                                                </div>
                                                <div class="media-body play_btn_text">
                                                    <h4 class="media-heading">
                                                        <asp:Label ID="Label30" runat="server" Text="Download on the" meta:resourcekey="Label30Resource1"></asp:Label></h4>
                                                    <h5>
                                                        <asp:Label ID="Label31" runat="server" Text="Play Store" meta:resourcekey="Label31Resource1"></asp:Label></h5>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <!-- /.END PLAY DOWNLOAS -->
                                </div>
                            </div>
                            <div class="col-md-7">
                               <div class="row">
                        <div class="Subscribe_content text-center" dir="ltr">
                                   
                                    <h3>
                                        <asp:Label ID="Label24" runat="server" Text="Download the MediFi App" meta:resourcekey="Label24Resource1" ForeColor="White"></asp:Label></h3>
                                    <p>
                                        <asp:Label ID="Label25" runat="server" Text="Send me a link to the app" meta:resourcekey="Label25Resource1" ForeColor="White"></asp:Label></p>
                                    <button class="canttri">
                                        <asp:Label ID="Label26" runat="server" Text="+966"></asp:Label></button>
                            <asp:TextBox ID="applink1" runat="server" onkeyup="myFunction()" ValidationGroup="a" placeholder="345678 512" OnTextChanged="applink_TextChanged" style="height:45px" MaxLength="9"></asp:TextBox>
                                                                   <asp:Button ID="submit123" ValidationGroup="a" runat="server" Text="Send Link" meta:resourcekey="submit123Resource1" CssClass="bnt send" OnClick="submit123_Click"  style="position:relative;left:-8px" />

                            <span id="mypopup" class="popuptext"></span>
                                    <p>
                                   
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please fill out this field" ValidationGroup="a" ControlToValidate="applink1" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                            </div>
                                </div>
                        
                        </div>
                    </div>

                    <%}
                        else
                        { %>
                     <div class="row">
                        <div class="hakkeem_app_content">
                            <div class="col-md-7">
                                <%--<div class="Download_title hakkeem_app_text text-left">--%>
                                  <div class="row">
                        <div class="Subscribe_content text-center">

                                    <h3>
                                        <asp:Label ID="Label65" runat="server" Text="Download the MediFi App" meta:resourcekey="Label24Resource1" ForeColor="White"></asp:Label></h3>
                                    <p>
                                        <asp:Label ID="Label66" runat="server" Text="Send me a link to the app" meta:resourcekey="Label25Resource1" ForeColor="White"></asp:Label></p>

                                    <button class="canttri">
                                        <asp:Label ID="Label67" runat="server" Text="+966" meta:resourcekey="Label26Resource1"></asp:Label></button><%--<input type="tel" id="demo" placeholder="123 4565"/>--%><asp:TextBox ID="applink" runat="server"  ValidationGroup="a" placeholder="Start No. with 5" meta:resourcekey="applinkResource1" MaxLength="9"></asp:TextBox><asp:Button ID="Button7" ValidationGroup="a" runat="server" Text="Send Link" meta:resourcekey="submit123Resource1" CssClass="bnt send" OnClick="submit123_Click"  style="position:relative;left:-5px" /><%--<button class="bnt send">SEND LINK</button>--%><span id="mypopup" class="popuptext"></span>
                                    <p>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="applink" ForeColor="Red" ValidationGroup="a" ValidationExpression="^((?!(0))(?!(1))(?!(2))(?!(3))(?!(4))(?!(6))(?!(7))(?!(8))(?!(9))[0-9]{9})$" runat="server" ErrorMessage="* Enter a valid number start with 5" Display="Dynamic" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Please fill out this field" ValidationGroup="a" ControlToValidate="applink" ForeColor="Red" Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" ControlToValidate="applink" runat="server" ErrorMessage="* Enter valid number" ForeColor="Red" Display="Dynamic" ValidationExpression="[0-9]{9}" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                    </p>
                                </div>
                                      </div>
                            </div>
                            <div class="col-md-5">
                                <div class="Download_title hakkeem_app_text text-center">
                                    <h3>
                                        <asp:Label ID="Label68" runat="server" Text="Available now"></asp:Label></h3>
                                    <!-- PLAY DOWNLOAS -->
                                    <div class="col-md-6 col-sm-4 margin_left_right text-center">
                                        <a href="#" class="">
                                            <div class="media play_btn">
                                                <div class="media-left play_btn_icon">
                                                    <i class="fa fa-android"></i>
                                                </div>
                                                <div class="media-body play_btn_text">
                                                    <h4 class="media-heading">
                                                        <asp:Label ID="Label69" runat="server" Text="Download on the" meta:resourcekey="Label28Resource1"></asp:Label></h4>
                                                    <h5>
                                                        <asp:Label ID="Label70" runat="server" Text="Play Store" meta:resourcekey="Label29Resource1"></asp:Label></h5>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <!-- /.END PLAY DOWNLOAS -->
                                    <!-- PLAY DOWNLOAS -->
                                    <div class="col-md-6 col-sm-4 margin_center  text-center">
                                        <a href="#">
                                            <div class="media play_btn">
                                                <div class="media-left play_btn_icon">
                                                    <i class="fa fa-apple"></i>
                                                </div>
                                                <div class="media-body play_btn_text">
                                                    <h4 class="media-heading">
                                                        <asp:Label ID="Label71" runat="server" Text="Download on the" meta:resourcekey="Label30Resource1"></asp:Label></h4>
                                                    <h5>
                                                        <asp:Label ID="Label72" runat="server" Text="Play Store" meta:resourcekey="Label31Resource1"></asp:Label></h5>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <!-- /.END PLAY DOWNLOAS -->
                                </div>
                            </div>
                        </div>
                    </div>

                    <%} %>
                </div>
            </section>
            <!-- /.END DOWNLOAD -->

            <!--  app ends          
 
            <!-- FOOTER -->

            <%-- <footer id="myfooter" style="margin-top:1.5%;">
		<div class="container">
			<div class="row">
				<div class="col-md-3" style="font-weight:bold;text-align:center;">
									<a href="#" style="color: white;" >
                                        <asp:Label ID="Label118" runat="server" Text="Hakkeem" meta:resourcekey="Label118Resource1"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="About.aspx" id="ftr1">
                                        <asp:Label ID="Label119" runat="server" Text="About" meta:resourcekey="Label119Resource1"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="Contact Us.aspx"  >
                                    <asp:Label ID="Label122" runat="server" Text="Contact Us" meta:resourcekey="Label122Resource1"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="Blog.aspx" >
                                    <asp:Label ID="Label125" runat="server" Text="Blog" meta:resourcekey="Label125Resource1"></asp:Label></a>
					
				</div>
				<div class="col-md-3"  style="font-weight:bold;text-align:center;">

									<a href="#" style="color: white;">
                                        <asp:Label ID="Label126" runat="server" Text="Search By" meta:resourcekey="Label126Resource1"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="#" >
                                        <asp:Label ID="Label127" runat="server" Text="Doctor Name" meta:resourcekey="Label127Resource1"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="#" ">
                                        <asp:Label ID="Label129" runat="server" Text="Speciality"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="#">
                                        <asp:Label ID="Label131" runat="server" Text="Language" meta:resourcekey="Label131Resource1"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="#" >
                                        <asp:Label ID="Label132" runat="server" Text="Location" meta:resourcekey="Label132Resource1"></asp:Label> </a><br>
                                    <div class="space10"></div>
                                    <a href="#" >
                                        <asp:Label ID="Label133" runat="server" Text="Hospital" meta:resourcekey="Label133Resource1"></asp:Label></a>
					


					
				</div>
				
				
					<div class="col-md-3"  style="font-weight:bold;text-align:center;">
					 				<a href="#" style="color:white;">
                                         <asp:Label ID="Label150" runat="server" Text="Are you a Top" meta:resourcekey="Label150Resource1"></asp:Label><br>
                                         <asp:Label ID="Label151" runat="server" Text="Doctor" meta:resourcekey="Label151Resource1"></asp:Label>?</a><br>
                                    <div class="space10"></div>
                                    <a href="#">
                                        <asp:Label ID="Label152" runat="server" Text="join Hakkeem today!" meta:resourcekey="Label152Resource1"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="#" >
                                        <asp:Label ID="Label153" runat="server" Text="Hakkeem for Health" meta:resourcekey="Label153Resource1"></asp:Label><br>
                                        <asp:Label ID="Label154" runat="server" Text="Systems" meta:resourcekey="Label154Resource1"></asp:Label></a><br>
                                    <div class="space10"></div>
                                    <a href="#" >
                                        <asp:Label ID="Label155" runat="server" Text="Learn more" meta:resourcekey="Label155Resource1"></asp:Label></a>
					
					
				</div>
				<div class="col-md-3"  style="font-weight:bold;text-align:center;">
				
                    <a><p><i class="fa fa-facebook fa-stack-1x fa-inverse" style="color:#3a589b;font-size:1.6em;"></i></p></a><br />
                    <a><p><i class="fa fa-twitter fa-stack-1x fa-inverse" style="color:#0fb5ee;font-size:1.6em;"></i></p></a><br />
                     <a> <p></p><i class="fa fa-linkedin" style=" color:#2380b0;font-size:1.6em;"></i></a><br />
                    <a><p>&nbsp&nbsp<i class="fa fa-google-plus" style=" color:#f4511e;font-size:1.6em;"></i></p></a> 

					
				</div>
			</div>
			<div class="space40"></div>
			<div class="row">
			<div class="col-md-2">
			</div>
			<div class="col-md-8">
			 <h5 style="margin-left:32%;"><b style="color:white;">
                 <asp:Label ID="Label161" runat="server" Text="Hakkeem. Developed by" meta:resourcekey="Label161Resource1"></asp:Label></b> <a href="http://www.Hakkeem.com" target="_blank"><b>
                     <asp:Label ID="Label162" runat="server" Text="Hakkeem" meta:resourcekey="Label162Resource1"></asp:Label></b></a></h5>
			</div>
			<div class="col-md-2">
			</div>
        </div>
		</div>
	</footer> --%>


            <!-- FOOTER COPYRIGHT -->
            <!-- SUBSCRIBE -->
            <section id="Subscribe">
                <div class="container">
                     <% 

                         if (Session["Speciality"].ToString() != "Auto")
                         { %>
                    <div class="row">
                        <div class="Subscribe_content text-center">
                            <h2>
                                <asp:Label ID="Label32" runat="server" Text="Subscribe Us For Newsletter:" meta:resourcekey="Label32Resource1"></asp:Label></h2>

                            <%--<input type="tel" class="subscribe_box" placeholder="Your Email Goes Here" />--%>
                            <%-- <button class="bnt Subscribe_btn">Subscribe</button>--%>
                            <asp:TextBox ID="TextBox1" CssClass="subscribe_box" placeholder="أدخل بريدك الإلكتروني" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                     
                               <asp:Button ID="Button1" CssClass="bnt Subscribe_btn" runat="server" Text="Subscribe" ValidationGroup="p" meta:resourcekey="Button1Resource1" OnClick="Button1_Click"  style="position:relative;left:5px;height:46px" />
                         <p>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  ValidationGroup="p" ErrorMessage="الرجاء إدخال البريد الإلكتروني الصحيح"  ControlToValidate="TextBox1" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                         
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="* يرجى إدخال البريد الإلكتروني الخاص بك" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBox1" Display="Dynamic" ForeColor="Red" ValidationGroup="pp"></asp:RegularExpressionValidator>
                           </p>
                               </div>
                    </div>
                    <%}
                        else
                        { %>
                     <div class="row">
                        <div class="Subscribe_content text-center">
                            <h2>
                                <asp:Label ID="Label101" runat="server" Text="Subscribe Us For Newsletter:" meta:resourcekey="Label32Resource1"></asp:Label></h2>

                   <%--   <input type="tel" class="subscribe_box" placeholder="Your Email Goes Here" />--%>
                            <%-- <button class="bnt Subscribe_btn">Subscribe</button>--%>
                            <asp:TextBox ID="TextBox4" CssClass="subscribe_box" placeholder="Enter Your Email" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                            <asp:Button ID="Button8" CssClass="bnt Subscribe_btn" runat="server" Text="Subscribe" ValidationGroup="pp" meta:resourcekey="Button1Resource1" OnClick="Button1_Click"  style="position:relative;left:-5px;height:46px" />
           <p>      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Please Enter the your email"  ValidationGroup="pp"  ControlToValidate="TextBox4" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                         
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="* Please Enter the valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBox4" Display="Dynamic" ForeColor="Red" ValidationGroup="pp"></asp:RegularExpressionValidator>
                </p>         </div>
                    </div>

                    <%} %>
                </div>
            </section>
            <!-- /.END SUBSCRIBE -->
            <!-- FOOTER MENU -->
            <section id="footer">
                <div class="container text-center">
                    <div class="row">
                        <div class="footer_content">
                             <% 

                                 if (Session["Speciality"].ToString() != "Auto")
                                 { %>
                                    <style>
                                    a{
                                        color:#fff;
                                        font-size:15px;
                                    }
                                    a:hover{
                                        color:#49a9ae;
                                    }
                                </style>
                            
                              <div class="col-md-4 col-sm-6 ">
                                <div class="menu_title">
                                    <h3>
                                        <asp:Label ID="Label43" runat="server" Text="Are you a Top Doctor?" meta:resourcekey="Label43Resource1"></asp:Label></h3>
                               
                                    <a href="">
                                            <asp:Label ID="Label44" runat="server" Text="Join MediFi Today!" meta:resourcekey="Label44Resource1"></asp:Label></a>
                                  
                                    
                                </div>
                            </div>
                            
                                 <div class="col-md-4 col-sm-6">
                                <div class="menu_title">
                                    <h3>
                                        <asp:Label ID="Label33" runat="server" Text="Get Touch With Us" meta:resourcekey="Label33Resource1"></asp:Label></h3>
                                   
                                <a href="about1.aspx?l=ar-EG">
                                            <asp:Label ID="Label34" runat="server" Text="About" meta:resourcekey="Label34Resource1"></asp:Label></a>&nbsp;&nbsp;<span style="color:#fff">ι</span>&nbsp;&nbsp;	
                                       <a href="contactUs.aspx?l=ar-EG">
                                            <asp:Label ID="Label35" runat="server" Text="Contact" meta:resourcekey="Label35Resource1"></asp:Label></a>
                                 <%--   &nbsp;&nbsp;<span  style="color:#fff">ι</span>&nbsp;&nbsp;	
                                      <a href="">
                                            <asp:Label ID="Label36" runat="server" Text="Blog" meta:resourcekey="Label36Resource1"></asp:Label></a>--%>
                                  
                                </div>
                            </div>

                               <div class="col-md-4 col-sm-6">
                                <div class="menu_title">
                                    <h3>
                                        <asp:Label ID="Label48" runat="server" Text="Follow Us on" meta:resourcekey="Label48Resource1"></asp:Label></h3>
                                    <a href=" https://www.facebook.com/" target="_blank"><i class="fa fa-facebook"></i></a>
                                    <a href="https://twitter.com/" target="_blank"><i class="fa fa-twitter"></i></a>
                                    <a href="https://www.instagram.com/" target="_blank"><i class="fa fa-instagram"></i></a>
                                    <a href="https://www.linkedin.com/" target="_blank"><i class="fa fa-linkedin"></i></a>
                                </div>
                            </div>

                           <%--  <div class="col-md-3 col-sm-6">
                                <div class="menu_title">
                                    <h3>
                                        <asp:Label ID="Label37" runat="server" Text="Search By" meta:resourcekey="Label37Resource1"></asp:Label></h3>
                                    <ul>
                                        <li><a href="../user/search.aspx?l=ar-EG">
                                            <asp:Label ID="Label38" runat="server" Text="Doctor Name" meta:resourcekey="Label38Resource1"></asp:Label></a></li>
                                        <li><a href="../user/search.aspx?l=ar-EG">
                                            <asp:Label ID="Label39" runat="server" Text="Speciality" meta:resourcekey="Label39Resource1"></asp:Label></a></li>
                                        <li><a href="../user/search.aspx?l=ar-EG">
                                            <asp:Label ID="Label40" runat="server" Text="Language" meta:resourcekey="Label40Resource1"></asp:Label></a></li>
                                        <li><a href="../user/search.aspx?l=ar-EG">
                                            <asp:Label ID="Label41" runat="server" Text="Location" meta:resourcekey="Label41Resource1"></asp:Label> </a></li>
                                        <li><a href="../user/searchbyhospital.aspx?l=ar-EG">
                                            <asp:Label ID="Label42" runat="server" Text="Hospital" meta:resourcekey="Label42Resource1"></asp:Label></a></li>
                                    </ul>
                                </div>
                            </div>--%>
                       
                           

                          
                         
                            <%}
                                else
                                { %>

                                <style>
                                    a{
                                        color:#fff;
                                        font-size:15px;
                                    }
                                    a:hover{
                                        color:#49a9ae;
                                    }
                                </style>
                                <div class="col-md-4 col-sm-6 ">
                                <div class="menu_title">
                                    <h3>
                                        <asp:Label ID="Label83" runat="server" Text="Are you a Top Doctor?" meta:resourcekey="Label43Resource1"></asp:Label></h3>
                                   <a href="../index/Doctor Join.aspx">
                                            <asp:Label ID="Label84" runat="server" Text="Join MediFi today!" meta:resourcekey="Label44Resource1"></asp:Label></a>
                                </div>
                            </div>

                              <div class="col-md-4 col-sm-6">
                                <div class="menu_title">
                                    <h3>
                                        <asp:Label ID="Label73" runat="server" Text="Get Touch With Us " meta:resourcekey="Label33Resource1"></asp:Label></h3>
                                <a href="about1.aspx">
                                            <asp:Label ID="Label74" runat="server" Text="About" meta:resourcekey="Label34Resource1"></asp:Label></a>&nbsp;&nbsp;<span  style="color:#fff">ι</span>&nbsp;&nbsp;	
                                    <a href="ContactUs.aspx">
                                            <asp:Label ID="Label75" runat="server" Text="Contact" meta:resourcekey="Label35Resource1"></asp:Label></a>
                                  <%--  &nbsp;&nbsp;<span  style="color:#fff">ι</span>&nbsp;&nbsp;	
                                      <a href="">
                                            <asp:Label ID="Label76" runat="server" Text="Blog" meta:resourcekey="Label36Resource1"></asp:Label></a>--%>
                                    
                                </div>
                            </div>
                          <%--  <div class="col-md-3 col-sm-6">
                                <div class="menu_title">
                                    <h3>
                                        <asp:Label ID="Label77" runat="server" Text="Search By" meta:resourcekey="Label37Resource1"></asp:Label></h3>
                                    <ul>
                                        <li><a href="../user/search.aspx">
                                            <asp:Label ID="Label78" runat="server" Text="Doctor Name" meta:resourcekey="Label38Resource1"></asp:Label></a></li>
                                        <li><a href="../user/search.aspx">
                                            <asp:Label ID="Label79" runat="server" Text="Speciality" meta:resourcekey="Label39Resource1"></asp:Label></a></li>
                                        <li><a href="../user/search.aspx">
                                            <asp:Label ID="Label80" runat="server" Text="Language" meta:resourcekey="Label40Resource1"></asp:Label></a></li>
                                        <li><a href="../user/search.aspx">
                                            <asp:Label ID="Label81" runat="server" Text="Location" meta:resourcekey="Label41Resource1"></asp:Label> </a></li>
                                        <li><a href="../user/searchbyhospital.aspx">
                                            <asp:Label ID="Label82" runat="server" Text="Hospital" meta:resourcekey="Label42Resource1"></asp:Label></a></li>
                                    </ul>
                                </div>
                            </div>--%>

                        
                            <div class="col-md-4 col-sm-6">
                                <div class="menu_title">
                                    <h3>
                                     <span style="margin-left:-10px">
                                        <asp:Label ID="Label88" runat="server" Text="Follow Us on" meta:resourcekey="Label48Resource1"></asp:Label></h3>
                                    <a href=" https://www.facebook.com/" target="_blank"><i class="fa fa-facebook"></i></a>
                                    <a href="https://twitter.com/" target="_blank"><i class="fa fa-twitter"></i></a>
                                    <a href="https://www.instagram.com/" target="_blank"><i class="fa fa-instagram"></i></a>
                                    <a href="https://www.linkedin.com/" target="_blank"><i class="fa fa-linkedin"></i></a>
                                   </span>
                                </div>
                            </div>

                            <%} %>
                        </div>
                    </div>
                </div>
            </section>
            <!-- /.END FOOTER MENU -->

            <!-- HOME COPY RIGHT -->
            <section id="copy">
                <div class="container">
                    <div class="row">
                        <style type="text/css">
                            .ftr{
                                color:#fff;
                                
                            }
                            .ftr:hover{
                                color:#fff;
                                text-decoration:none;
                            }
                        </style>
                        <div class="copy_right text-center">
                          
   <% 

       if (Session["Speciality"].ToString() == "Auto")
       { %>

                        <a href="#" class="ftr">       <span class="copy-color">MediFi</span></a>
                                <asp:Label ID="Label49" runat="server" Text=" &amp;copy 2018, All Rights Reserved" ></asp:Label>
                         <% }


                             else
                             {
                                 
%> 
                              <a href="#" class="ftr"><span class="copy-color">حكيم</span></a>    
         <asp:Label ID="Label19" runat="server" Text="&amp;copy  2017، جميع الحقوق محفوظة" ></asp:Label>                    
                           
                          
                               
                            <%
                             }
                             %>
                             </div>
                    </div>
                </div>
            </section>
            <!-- /.END COPY RIGHT -->

            <!-- The scroll to top feature -->
            <% 

                if (Session["Speciality"].ToString() != "Auto")
                { %>
            <div class="scroll-top-wrapper pull-left ">
                <span class="scroll-top-inner">
                    <i class="fa fa-2x fa-arrow-circle-up"></i>
                </span>
            </div>
            <%}
                else
                { %>
            <div class="scroll-top-wrapper pull-right">
                <span class="scroll-top-inner">
                    <i class="fa fa-2x fa-arrow-circle-up"></i>
                </span>
            </div>
            <%} %>
        </div>






        <script src="js/sweetalert-dev.js"></script>
        <script src="js/sweetalert.min.js"></script>

        <script>
            //$("#applink").focus(function () {
            //    $(this).attr('placeholder', 'Enter a number start with 5')
            //}).blur(function () {
            //    $(this).attr('placeholder', '123456789')
            //})
        </script>
        <script>
            $("#applink1").focus(function () {
                $(this).attr('placeholder', 'أدخل رقم يبدأ ب 5')
            }).blur(function () {
                $(this).attr('placeholder', '987654321')
            })
        </script>

        <script>
            $("#applink").keyup(function (e) {
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

                                swal("Start No. with 5");
                                $(this).val('');
                                return false;

                            }

                        }
                        else {
                            //$("#mypopup").html("Digits Only").show();
                            swal("***Enter a number***");

                        }
                    }

                    if ((i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9)) {
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


        <script>
            $("#applink1").keyup(function (e) {
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

                                swal("أدخل رقم يبدأ ب 5");
                                $(this).val('');
                                return false;

                            }

                        }
                        else {
                            //$("#mypopup").html("Digits Only").show();
                            swal("***أدخل رقما***");

                        }
                    }

                    if ((i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9)) {
                        if (numpattern.test(dInput[i])) {
                            console.log('validnum' + dInput[i]);
                            validstr += dInput[i];
                        } else {
                            $("#mypopup").html("أرقام فقط").show();
                            swal("**أدخل رقما***");


                        }
                    }

                }

                $(this).val(validstr);
                return false;

            });
        </script>

        <!-- Start Javascript  -->



        <!-- BOOTSTRAP JS -->
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
        <!-- MY MAIN JS -->
        <script type="text/javascript" src="js/main.js"></script>
        <!-- MENU SCRIPT JS -->
        <script type="text/javascript" src="js/script.js"></script>

        <!-- Plugins -->
        <script src="js/bootstrap.min.js"></script>
        <script src="js/menu.js"></script>
        <script src="js/owl-carousel/owl.carousel.min.js"></script>
        <script src="js/rs-plugin/js/jquery.themepunch.tools.min.js"></script>
        <script src="js/rs-plugin/js/jquery.themepunch.revolution.min.js"></script>
        <script src="js/jquery.easing.min.js"></script>
        <script src="js/isotope/isotope.pkgd.js"></script>
        <script src="js/jflickrfeed.min.js"></script>
        <script src="js/tweecool.js"></script>
        <script src="js/flexslider/jquery.flexslider.js"></script>
        <script src="js/easypie/jquery.easypiechart.min.js"></script>
        <script src="js/jquery-ui.js"></script>
        <script src="js/jquery.appear.js"></script>
        <script src="js/jquery.inview.js"></script>
        <script src="js/jquery.countdown.min.js"></script>
        <script src="js/jquery.sticky.js"></script>
        <script src="js/magnific-popup/jquery.magnific-popup.min.js"></script>
        <script src="js/jquery.easing/jquery.easing.js"></script>
        <script src="js/particles.js"></script>
        <script src="js/main.js"></script>
        <script type="text/javascript" src="js/move-top.js"></script>
        <script src="js/app.js"></script>

        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>
        <script src="js/gmaps/greyscale.js"></script>

    </form>
    <%-- <script type="text/javascript" src="js/jquery.min.js"></script> --%>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>


</body>
</html>
