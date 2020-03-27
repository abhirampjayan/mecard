<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Hakkeem_Index" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Hakkeem | A Tahcom Product</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- BOOTSTRAP -->
    <link rel="stylesheet" type="text/css" href="style/css/bootstrap.min.css" />
    <!-- FONT -->
    <link rel="stylesheet" type="text/css" href="style/css/font-awesome.css" />
    <!-- Stylesheet -->
    <link rel="stylesheet" type="text/css" href="style/style.css" media="all" />
    <link rel="stylesheet" type="text/css" href="style/responsive.css" />



    <link href="../css/sweetalert.css" rel="stylesheet" />
    <link href="../css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.autocomplete.js" type="text/javascript"></script>

   
    <%----- with photo --%>
</head>
<body>

    <form id="form1" runat="server">


        <div class="wrapper">
         
            <!-- NAV -->
            <nav class="navigation menu_area">
                <!-- CONTAINER -->
                <div class="container">
                    <!-- Brand and toggle get grouped for better mobile display -->
                    <div class="col-md-10">
                       
                        <div class="navbar-header">
                          <asp:HyperLink ID="HyperLink2" CssClass="navbar-brand" Font-Bold="True" runat="server" NavigateUrl="../default.aspx"> 
                                <img src="style/img/logo.png" alt="" /></asp:HyperLink>
                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#nav-collapse" aria-expanded="false">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <!-- HEADER LOGO -->
                          
                            <!-- /HEADER LOGO -->
                        </div>
                        <!-- Collect the nav links, forms, and other content for toggling -->
                        <div id="nav-collapse" class="top-nav collapse navbar-collapse navbar-right">
                     
                             <ul class="nav navbar-nav">
                                 <li>
                                    <asp:HyperLink ID="HyperLink7" runat="server" CssClass="stylelink" NavigateUrl="Search.aspx" meta:resourcekey="HyperLink7Resource1">Doctor</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink3" runat="server" CssClass="stylelink" meta:resourcekey="HyperLink3Resource1">Post doctor feedback</asp:HyperLink>
                                </li>

                                <li>
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="stylelink" meta:resourcekey="HyperLink1">Account</asp:HyperLink></li>
                                <li>
                                    <asp:HyperLink ID="HyperLink4" runat="server" CssClass="stylelink" meta:resourcekey="HyperLink4Resource1">History</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink5" runat="server" CssClass="stylelink" meta:resourcekey="HyperLink5Resource1">Appointments</asp:HyperLink>

                                </li>
                               
                            
                            </ul>
                          
                        </div>
                    </div>

                    
                        <div class="col-md-2">
                             <div class="navbar-custom-menu">
                            <ul class="nav navbar-nav">
                                <li style="margin-top: 12px;">
                                    <asp:Image ID="ImgUser" runat="server" CssClass="img img-circle img-responsive" Width="40px" meta:resourcekey="ImgUserResource1" />
                                </li>
                                <li>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" ForeColor="#4aa9af" OnClick="LinkButton1_Click" meta:resourcekey="LinkButton1Resource1">Patient</asp:LinkButton>
                                </li>
                                <li>

                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="stylelink" ForeColor="Black" OnClick="LinkButton2_Click" meta:resourcekey="LinkButton2Resource1" Visible="False"></asp:LinkButton>
                                </li>
                                <li>

                                    <asp:LinkButton ID="LinkButton3" Font-Bold="true" runat="server" CssClass="stylelink" ForeColor="Black" CommandArgument="ar-EG" OnClick="LinkButton3_Click"></asp:LinkButton>

                                </li>
                            </ul>
                        </div>
                       <%-- <ul class="langusge">
                            <button class="btn-btn">Arabic</button>
                            <button class="btn-btn eng">English</button>
                        </ul>--%>
                    </div>
                </div>
                 <br />
                <!-- /.END CONTAINER -->
            </nav>
          
            <!-- SLIDER  -->
         
                    <!-- CONTAINER -->
   
                      <section id="Subscribe">
                <div class="container">
                   
                        <div class="Subscribe_content text-left">
                             <div class="row">
                            <h2></h2>
                            <div class="col-lg-4">
 <asp:TextBox ID="txtContactsSearch" CssClass="form-control" placeholder="Doctor name or Specialty" runat="server" ValidationGroup="cc" AutoPostBack="true" OnTextChanged="txtContactsSearch_TextChanged"></asp:TextBox>
                            </div>
                              <div class="col-lg-4">
                              <asp:DropDownList ID="DropDownList2" AutoPostBack="true" CssClass="form-control" ValidationGroup="cc" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList>
                         </div>
                              <div class="col-lg-3">
                                       <asp:DropDownList ID="DropDownList1" AutoPostBack="true" CssClass="form-control" ValidationGroup="cc" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                                  </div>
                             <div class="col-lg-1">
                                      <asp:Button ID="Button4" CssClass="btn btn-sm btn-block btn-info" runat="server" Text="Find" OnClick="Button4_Click" ValidationGroup="cc" />
                                  </div>
                             </div>


                             <div class="row">

                                 </div>

                            </div>
                    </div>
                </div>
            </section>
                    <!-- /.END CONTAINER -->
              
            <!-- /.END SLIDER  -->

     
            <!-- FOOTER MENU -->
     
            <!-- /.END FOOTER MENU -->

            <!-- HOME COPY RIGHT -->
            <section id="copy">
                <div class="container">
                    <div class="row">
                        <div class="copy_right text-center">
                            <p>Hakkeem &copy 2018, All Rights Reserved</p>
                        </div>
                    </div>
                </div>
            </section>
            <!-- /.END COPY RIGHT -->

            <!-- The scroll to top feature -->
            <div class="scroll-top-wrapper ">
                <span class="scroll-top-inner">
                    <i class="fa fa-2x fa-arrow-circle-up"></i>
                </span>
            </div>
        </div>




        <!-- Start Javascript  -->

        

        <!-- BOOTSTRAP JS -->
        <script type="text/javascript" src="style/js/bootstrap.min.js"></script>
        <!-- MY MAIN JS -->
        <script type="text/javascript" src="style/js/main.js"></script>
        <!-- MENU SCRIPT JS -->
        <script type="text/javascript" src="style/js/script.js"></script>

        <!-- Plugins -->
        <script src="../js/bootstrap.min.js"></script>
        <script src="../js/menu.js"></script>
        <script src="../js/owl-carousel/owl.carousel.min.js"></script>
        <script src="../js/rs-plugin/js/jquery.themepunch.tools.min.js"></script>
        <script src="../js/rs-plugin/js/jquery.themepunch.revolution.min.js"></script>
        <script src="../js/jquery.easing.min.js"></script>
        <script src="../js/isotope/isotope.pkgd.js"></script>
        <script src="../js/jflickrfeed.min.js"></script>
        <script src="../js/tweecool.js"></script>
        <script src="../js/flexslider/jquery.flexslider.js"></script>
        <script src="../js/easypie/jquery.easypiechart.min.js"></script>
        <script src="../js/jquery-ui.js"></script>
        <script src="../js/jquery.appear.js"></script>
        <script src="../js/jquery.inview.js"></script>
        <script src="../js/jquery.countdown.min.js"></script>
        <script src="../js/jquery.sticky.js"></script>
        <script src="../js/magnific-popup/jquery.magnific-popup.min.js"></script>
        <script src="../js/jquery.easing/jquery.easing.js"></script>
        <script src="../js/particles.js"></script>
        <script src="../js/main.js"></script>
        <script type="text/javascript" src="../js/move-top.js"></script>
        <script src="../js/app.js"></script>

        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>
        <script src="../js/gmaps/greyscale.js"></script>

    </form>
     <%-- <script type="text/javascript" src="js/jquery.min.js"></script> --%>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    
    
</body>
</html>
