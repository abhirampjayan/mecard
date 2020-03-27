<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" CodeFile="Viewhospitaldoctorreview.aspx.cs" Inherits="User_View_hospital_doctor_review" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /*#iw-container {
            margin-bottom: 10px;
        }

        #content {
            float: right;
            margin: 10px;
        }

        .affix {
            top: 0px;
            position: fixed;
            right: 0;
            max-width: 450px;
        }*/

        /*#ContentPlaceHolder1_Image1 {
            margin-left: 20%;
        }*/

        /*#doctorname {
            margin-left: -15%;
        }

        #rating {
            margin-left: 10%;
        }*/

        body {
            /*margin:0px auto;
width:980px;
font-family:"Trebuchet MS", Arial, Helvetica, sans-serif;	
background:#C9C9C9;*/
        }

        .blankstar {
            background-image: url(../images/blank_star.png);
            width: 16px;
            height: 16px;
            cursor: none;
        }

        .waitingstar {
            background-image: url(../images/half_star.png);
            width: 16px;
            height: 16px;
            cursor: none;
        }

        .shiningstar {
            background-image: url(../images/shining_star.png);
            width: 16px;
            height: 16px;
            cursor: none;
        }

        .cnt {
            margin-top: 1.5cm;
            margin-bottom: 1cm;
        }


        /*section#copy {
            padding: 20px 0;
            color: #fff;
            background: #313131;
            margin-top: 0;
            position: absolute;
            width: 100%;
        }



        @media screen and (max-width:991px) {
            section#copy {
                padding: 20px 0;
                color: #fff;
                background: #313131;
                margin-top: 0cm;
                position: absolute;
                width: 100%;
                bottom: 0cm;
            }
        }
        @media screen and (max-width:768px) {
            section#copy {
                padding: 20px 0;
                color: #fff;
                background: #313131;
                margin-top: 0cm;
                position: absolute;
                width: 100%;
                bottom: 9cm;
            }
        }
        @media screen and (max-width:414px) {
            section#copy {
                padding: 20px 0;
                color: #fff;
                background: #313131;
                margin-top: 0cm;
                position: absolute;
                width: 100%;
                bottom: 17cm;
            }
        }
         @media screen and (max-width:375px) {
            section#copy {
                padding: 20px 0;
                color: #fff;
                background: #313131;
                margin-top: 0cm;
                position: absolute;
                width: 100%;
                bottom: 4cm;
            }
        }
          @media screen and (max-width:412px) {
            section#copy {
                padding: 20px 0;
                color: #fff;
                background: #313131;
                margin-top: 0cm;
                position: absolute;
                width: 100%;
                bottom: 15cm;
            }
        }
           @media screen and (max-width:360px) {
            section#copy {
                padding: 20px 0;
                color: #fff;
                background: #313131;
                margin-top: 0cm;
                position: absolute;
                width: 100%;
                bottom: 3cm;
            }
        }*/
    </style>
    <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
    <%-- Map script --%>
    <%--<script type="text/javascript" src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyB4Wo74MK6m0aOMNpgY7381lYNkrMsFeyQ&sensor=false"></script>--%>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCNo1WjdpDfX7wvziSy2HeS0d-axd0Yv50&callback=initMap"
        type="text/javascript"></script>

    <script type="text/javascript">

        function getQueryVariable(variable) {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == variable) { return pair[1]; }
            }
            return (false);
        }

        var icon1 = 'mapicons/add-location-point.png';
        window.onload = function () {
            var lat = document.getElementById('<%=Lat.ClientID%>').value;
            var lng = document.getElementById('<%=Long.ClientID%>').value;
            var myLatlng = new google.maps.LatLng(lat, lng);
            var mapOptions = {
                center: myLatlng,
                zoom: 16,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: false,

                zoomControl: true,
                zoomControlOptions: {
                    position: google.maps.ControlPosition.LEFT_TOP
                },
                scaleControl: false,
                streetViewControl: false,
                styles: [
   {
       "featureType": "poi.park",
       "elementType": "geometry.fill",
       "stylers": [
           {
               "color": "#a8d34e"
           }
       ]
   },
   {
       "featureType": "road.highway",
       "elementType": "geometry.fill",
       "stylers": [
           {
               "color": "#ffa354"
           }
       ]
   },
   {
       "featureType": "road.highway",
       "elementType": "geometry.stroke",
       "stylers": [
           {
               "color": "#fff6ee"
           }
       ]
   },
   {
       "featureType": "water",
       "elementType": "geometry.fill",
       "stylers": [
           {
               "visibility": "on"
           },
           {
               "color": "#18d3ce"
           }
       ]
   },
   {
       "featureType": "water",
       "elementType": "geometry.stroke",
       "stylers": [
           {
               "color": "#3fb5bb"
           }
       ]
   }
                ]
            };
            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);

            var infoWindow = new google.maps.InfoWindow();

            var marker = new google.maps.Marker(
                {
                    position: myLatlng,
                    map: map,
                    icon: icon1,
                });

            (function (marker, myLatlng) {
                google.maps.event.addListener(marker, "mouseover", function (e) {
                    var geocoder = geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'latLng': myLatlng }, function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            if (results[0]) {
                                var infoContent = '<div id="iw-container">' +
                                      '<div id="content">' +
                                      '<div>' + results[0].formatted_address + '</div>' +
                                      '</div>' +
                                      '</div>';
                                infoWindow.setContent(infoContent);
                                infoWindow.open(map, marker);
                            }
                        }
                    });
                });

                google.maps.event.addListener(marker, "mouseout", function (e) {
                    setTimeout(function () { infoWindow.close(); }, 2500);
                });
            })(marker, myLatlng);

            //map.panBy(350, 0);
        }

    </script>
    <%-- Ends map script --%>

    <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="cnt">
        <asp:HiddenField ID="Lat" runat="server" />
        <asp:HiddenField ID="Long" runat="server" />
        <section class="content">

            <div class="col-md-12">
                <div class="box box-default">
                    <div class="box-body">
                        <div class="row">
                          <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                { %>--%>
                            <div class="col-md-4">
                              <%--  <%}
                                    else
                                    { %>
                                <div class="col-md-4 pull-right">
                                    <%} %>--%>
                                    <div class="user-panel">
                                        <div class="image">
                                            <asp:Image ID="Image1" CssClass="img-circle" runat="server" meta:resourcekey="Image1Resource1" />
                                        </div>
                                    </div>
                                </div>
                            <%--    <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <div class="col-md-4" id="doctorname">
                                  <%--  <%}
                                        else
                                        { %>
                                    <div class="col-md-4 pull-right" id="doctorname">
                                        <%} %>--%>
                                        <div class="form-group">
                                            <h2><b>Dr.<asp:Label ID="lblname" runat="server" Text="Label" meta:resourcekey="lblnameResource1"></asp:Label></b></h2>
                                        </div>
                                        <div class="form-group">
                                            <b>
                                                <asp:Label ID="lblql" runat="server" Text="Label" meta:resourcekey="lblqlResource1"></asp:Label></b>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblspec" runat="server" Text="Label" meta:resourcekey="lblspecResource1"></asp:Label>
                                        </div>

                                    </div>
                                   <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                        { %>--%>
                                    <div class="col-md-4" id="rating">
                                       <%-- <%}
                                            else
                                            { %>
                                        <div class="col-md-4 pull-left" id="rating">
                                            <%} %>--%>
                                            <ul class="nav navbar-nav">
                                                <li class="page-scroll"><a href="#div1" style="color: #4aa9af;"><b>
                                                    <asp:Label ID="Label7" runat="server" Text="Read patient review" meta:resourcekey="Label7Resource1"></asp:Label></b></a></li>

                                            </ul>
                                            <br />
                                            <div class="form-group">
                                                <%--  <ajaxToolkit:Rating ID="Rating1" ReadOnly="True" runat="server" StarCssClass="blankstar"
                                    WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar"
                                    EmptyStarCssClass="blankstar" BehaviorID="Rating1_RatingExtender" meta:resourcekey="Rating1Resource1">
                                </ajaxToolkit:Rating>--%>
                                                <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal><asp:Literal ID="Literal2" runat="server" meta:resourcekey="Literal2Resource1"></asp:Literal><asp:Literal ID="Literal3" runat="server" meta:resourcekey="Literal3Resource1"></asp:Literal>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                          <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                { %>--%>
                            <div class="col-md-12">
                              <%--  <%}
                                else
                                { %>
                                <div class="col-md-12" dir="rtl">
                                    <%} %>--%>

                                    <div class="form-group">



                                        <div class="box-body">
                                            <div class="row">
                                               <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                                    { %>--%>
                                                <div class="col-md-7">
                                                  <%--  <%}
                                                    else
                                                    { %>
                                                    <div class="col-md-7 col-md-push-5">
                                                        <%} %>--%>
                                                        <h3>
                                                            <asp:Label ID="Label8" runat="server" Text="Qualifications and Experience" meta:resourcekey="Label8Resource1"></asp:Label></h3>

                                                        <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table" AutoGenerateRows="False" BackColor="#ECF0F5" BorderStyle="None" CellPadding="4" ForeColor="#666666" GridLines="Horizontal" meta:resourcekey="DetailsView1Resource1">
                                                            <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                            <FieldHeaderStyle Font-Bold="True" />
                                                            <Fields>
                                                                <asp:TemplateField HeaderText="Education" meta:resourcekey="TemplateFieldResource1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_education") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hospital Affiliations" meta:resourcekey="TemplateFieldResource11">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_hospital_afili") %>' meta:resourcekey="Label4Resource2"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Languages Spoken" meta:resourcekey="TemplateFieldResource2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_language") %>' meta:resourcekey="Label4Resource3"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Board Certifications" meta:resourcekey="TemplateFieldResource3">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_college") %>' meta:resourcekey="Label4Resource4"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Professional Memberships" meta:resourcekey="TemplateFieldResource4">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_profesional_mem") %>' meta:resourcekey="Label4Resource5"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Specialties" meta:resourcekey="TemplateFieldResource5">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_specialties") %>' meta:resourcekey="Label4Resource6"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doctor address" meta:resourcekey="TemplateFieldResource6">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("hd_address") %>' meta:resourcekey="Label5Resource1"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hospital address" meta:resourcekey="TemplateFieldResource7">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("hd_address2") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="About doctor" meta:resourcekey="TemplateFieldResource8">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("hd_about_you") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Fields>
                                                        </asp:DetailsView>
                                                    </div>
                                                  <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                                        { %>--%>
                                                    <div class="col-md-5">
                                                       <%-- <%}
                                                        else
                                                        { %>
                                                        <div class="col-md-5 col-md-pull-7">
                                                            <%} %>--%>
                                                            <h3>
                                                                <asp:Label ID="Label14" runat="server" Text="Hospital details" meta:resourcekey="Label14Resource1"></asp:Label></h3>
                                                            <div class="table-responsive">
                                                                <asp:DetailsView ID="DetailsView2" runat="server" CssClass="table table-bordered" AutoGenerateRows="False" BackColor="#ECF0F5" BorderStyle="None" CellPadding="4" ForeColor="#666666" GridLines="Horizontal" meta:resourcekey="DetailsView2Resource1">
                                                                    <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                                    <FieldHeaderStyle Font-Bold="True" />
                                                                    <Fields>
                                                                        <asp:TemplateField HeaderText="Hospital name" meta:resourcekey="TemplateFieldResource9">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("h_name") %>' meta:resourcekey="Label10Resource1"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Location" meta:resourcekey="TemplateFieldResource10">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label11" runat="server" Text="Label" meta:resourcekey="Label11Resource1"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Address" meta:resourcekey="TemplateFieldResource12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("h_address") %>' meta:resourcekey="Label12Resource1"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Contact number" meta:resourcekey="TemplateFieldResource13">
                                                                            <ItemTemplate>
                                                                              <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                                                                    { %>--%>
                                                                                <div dir="ltr" class="pull-left">
                                                                                   <%-- <%}
                                                                                  else
                                                                                  { %><div dir="ltr" class="pull-right">
        <%} %>--%>
        <asp:Label ID="Label13" runat="server" Text='<%# Bind("h_contact") %>' meta:resourcekey="Label13Resource1"></asp:Label>
    </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Fields>
                                                                </asp:DetailsView>
                                                            </div>
                                                            <div id="dvMap" style="width: 100%; height: 100px;">
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="row">
                                        <div id="div1">

                                          <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                                { %>--%>
                                            <div class="col-md-12">
                                              <%--  <%}
                                                else
                                                { %>
                                                <div class="col-md-12" dir="rtl">
                                                    <%} %>--%>
                                                    <div class="box box-primary">
                                                        <div class="box-header">
                                                            <h3 class="box-title">
                                                                <asp:Label ID="Label9" runat="server" Text="Patient reviews" meta:resourcekey="Label9Resource1"></asp:Label></h3>
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="form-group">
                                                                <asp:DataList ID="DataList1" CssClass="table table-hover" runat="server" meta:resourcekey="DataList1Resource1">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-12">
                                                                            <div class="box">
                                                                                <div class="box-body">
                                                                                    <div class="row">
                                                                                        <div class="col-md-3">
                                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("u_email") %>' Visible="False" meta:resourcekey="Label3Resource1"></asp:Label>
                                                                                            <b>
                                                                                                <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></b>
                                                                                        </div>
                                                                                        <div class="col-md-9">
                                                                                            <%--<asp:Label ID="Label2" runat="server" Text='<%# Bind("u_review") %>' meta:resourcekey="Label2Resource1"></asp:Label>--%>
                                                                                            <asp:Label ID="Label2" runat="server"  meta:resourcekey="Label2Resource1"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="box-footer">
                                                                                    <div class="col-md-4">
                                                                                        <span class="btn btn-default btn-sm">Beside manner
                                                                            <asp:Literal ID="Literal11" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal></span>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <span class="btn btn-default btn-sm">Waiting time
                                                                            <asp:Literal ID="Literal21" runat="server" meta:resourcekey="Literal2Resource1"></asp:Literal></span>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <span class="btn btn-default btn-sm">Service
                                                                            <asp:Literal ID="Literal31" runat="server" meta:resourcekey="Literal3Resource1"></asp:Literal></span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
        </section>

    </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>

