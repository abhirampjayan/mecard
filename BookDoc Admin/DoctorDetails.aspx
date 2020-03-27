<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="DoctorDetails.aspx.cs" Inherits="BookDoc_Admin_DoctorDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <%-- Map latitude and Longitude--%><asp:HiddenField ID="Lat" runat="server" />
    <asp:HiddenField ID="Long" runat="server" />
    <%--End Map latitude and Longitude--%>

    <div class="cnt">
        <section class="content">
            <%--     <style type="text/css">
            @media (max-width: 991px) {
                .content {
                    min-height: 366px;
                    padding: 71px;
                    margin-right: auto;
                    margin-left: auto;
                    padding-left: 15px;
                    padding-right: 15px;
                }
            }
        </style>--%>
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
            //var lat = getQueryVariable('Lat');
            //var lng = getQueryVariable('Long');
            var lat = document.getElementById('<%=Lat.ClientID%>').value
            var lng=document.getElementById('<%=Long.ClientID%>').value
            var myLatlng = new google.maps.LatLng(lat, lng);
            var mapOptions = {
                center: myLatlng,
                zoom: 12,
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


            // Marker hover address shows
            (function (marker, myLatlng) {
                google.maps.event.addListener(marker, "mouseover", function (e) {

                    var geocoder = geocoder = new google.maps.Geocoder();

                    geocoder.geocode({ "latLng": myLatlng }, function (results, status) {
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
            //Ends..........

            map.panBy(350, 0);
        }

    </script>


            <div class="row">
              <%--  <asp:HiddenField ID="Lat" runat="server" />
    <asp:HiddenField ID="Long" runat="server" />--%>
                <div class="col-md-12">
                    <div class="box box-default">
                        <div class="box-body">
                          <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                { %>--%>
                            <div class="container-fuild">


                                <div class="row">
                                    <div class="col-md-2 pull-right">
                                        <asp:Image ID="Image1" CssClass="img-circle img-lg img-responsive" runat="server" meta:resourcekey="Image1Resource2" />
                                    </div>
                                    <div class="col-md-10 pull-left" id="doctorname" style="text-align:left">
                                        <div class="form-group">
                                            <h2><b><span class="font-small">Dr.</span><asp:Label ID="lblname" runat="server" Text="Label" meta:resourcekey="lblnameResource2"  class="font-small"></asp:Label></b></h2>
                                        </div>
                                        <div class="form-group">
                                            <b>
                                                <asp:Label ID="lblql" runat="server" Text="Label" meta:resourcekey="lblqlResource2"></asp:Label></b>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblspec" runat="server" Text="Label" meta:resourcekey="lblspecResource2"></asp:Label>
                                        </div>

                                          <div class="form-group">
                                            <%--     <ajaxToolkit:Rating ID="Rating1" ReadOnly="true" runat="server" StarCssClass="blankstar" 
        WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" 
        EmptyStarCssClass="blankstar"></ajaxToolkit:Rating>--%>
                                            <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource2"></asp:Literal><asp:Literal ID="Literal2" runat="server" meta:resourcekey="Literal2Resource2"></asp:Literal><asp:Literal ID="Literal3" runat="server" meta:resourcekey="Literal3Resource2"></asp:Literal>
                                        </div>
                                         <div class="form-group">
                                       <%-- <ul class="nav navbar-nav">
                                            <li class="page-scroll">--%><a href="#div1" class="page-scroll" style="color: #4aa9af"><b>Read patient review</b></a><%--</li>

                                        </ul>--%>
                                            </div>

                                    </div>
                                 
                                        <%--<b>--%> <%--<asp:LinkButton ID="LinkButton1" runat="server">Read patient review</asp:LinkButton>--%>

                                        <%-- </b>--%>
                                       
                                        <%--<br />--%>
                                      
                                    
                                </div>
                            </div>
                         <%--   <%}
                                else
                                { %>
                            <div class="container-fluid">


                                <div class="row">
                                      <div class="col-md-2 pull-left">
                                        <asp:Image ID="Image2" CssClass="img-circle img-lg img-responsive pull-right" runat="server" meta:resourcekey="Image2Resource1" />
                                    </div>
                                  
                                    <div class="col-md-10 pull-right " dir="rtl" id="doctorname"  style="text-align:right" class="doctorname">
                                        <div class="form-group">
                                            <h2><b><span class="font-small">Dr.</span><asp:Label ID="lblname1" runat="server" Text="Label" meta:resourcekey="Label12Resource1"  class="font-small"></asp:Label></b></h2>
                                        </div>
                                        <div class="form-group">
                                            <b>
                                                <asp:Label ID="lblql1" runat="server" Text="Label" meta:resourcekey="Label13Resource1"></asp:Label></b>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblspec1" runat="server" Text="Label" meta:resourcekey="Label14Resource1"></asp:Label>
                                        </div>
                                               <div class="form-group" dir="rtl" id="rating">

                                            <asp:Literal ID="Literal7" runat="server" meta:resourcekey="Literal7Resource1"></asp:Literal><asp:Literal ID="Literal8" runat="server" meta:resourcekey="Literal8Resource1"></asp:Literal><asp:Literal ID="Literal9" runat="server" meta:resourcekey="Literal9Resource1"></asp:Literal>
                                        </div>
                                          <div class="form-group">--%>
                                       <%-- <ul class="nav navbar-nav">--%>
                                            <%--<li class="page-scroll">--%>
                                        <%--<a href="#div1" class="page-scroll" style="color: #4aa9af; font-weight: bolder"><b>قراءة مراجعة المريض</b></a>--%>

                                         <%--   </li>--%>

                                      <%--  </ul>--%>
                                            <%--  </div>
                                    </div>--%>
                                  
                                    
                                 
                                     <%--   <br />--%>

                                   <%-- </div>

                            </div>

                            <%} %>--%>
                        </div>
                        <div class="box-footer">
                            <%-- <div class="col-md-4" data-spy="affix" data-offset-top="205">--%>
                            <div id="dvMap" style="width: 100%; height: 150px;">
                            </div>
                            <%-- </div>--%>
                        </div>
                    </div>
                </div>

            </div>



            <section class="container-fluid">
                <%-- <%if (Session["Speciality"].ToString() == "Auto")
                     { %>--%>
                <div class="row">
                  <%--  <%}
    else
    { %>
                    <div class="row" dir="rtl">
                    <%} %>--%>
                    <div class="col-md-6">
                        <h3 style="color: #000000; font-size: large; font-weight: bold;">
                            <asp:Label ID="Label12" runat="server" Text="Qualifications and Experience" meta:resourcekey="Label12Resource2"></asp:Label></h3>
                        <div class="box box-default">


                            <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-responsive" AutoGenerateRows="False" BackColor="#ECF0F5" BorderStyle="None" CellPadding="4" ForeColor="#666666" GridLines="Horizontal" meta:resourcekey="DetailsView1Resource2">
                                <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <FieldHeaderStyle Font-Bold="True" />
                                <Fields>
                                    <asp:TemplateField HeaderText="Education" meta:resourcekey="TemplateFieldResource10">
                                        <ItemTemplate>

                                            <asp:Label ID="Label41" runat="server" Text='<%# Bind("d_education") %>' meta:resourcekey="Label41Resource2"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hospital Affiliations" meta:resourcekey="TemplateFieldResource11">
                                        <ItemTemplate>
                                            <asp:Label ID="Label42" runat="server" Text='<%# Bind("d_hospital_afili") %>' meta:resourcekey="Label42Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Languages Spoken" meta:resourcekey="TemplateFieldResource12">
                                        <ItemTemplate>
                                            <asp:Label ID="Label43" runat="server" meta:resourcekey="Label43Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Board Certifications" meta:resourcekey="TemplateFieldResource13">
                                        <ItemTemplate>
                                            <asp:Label ID="Label44" runat="server" Text='<%# Bind("d_college") %>' meta:resourcekey="Label44Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Professional Memberships" meta:resourcekey="TemplateFieldResource14">
                                        <ItemTemplate>
                                            <asp:Label ID="Label45" runat="server" Text='<%# Bind("d_profesional_mem") %>' meta:resourcekey="Label45Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specialties" meta:resourcekey="TemplateFieldResource15">
                                        <ItemTemplate>
                                            <asp:Label ID="Label46" runat="server" Text='<%# Bind("d_specialties") %>' meta:resourcekey="Label46Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Doctor address" meta:resourcekey="TemplateFieldResource16">
                                        <ItemTemplate>
                                            <asp:Label ID="Label47" runat="server" Text='<%# Bind("d_address") %>' meta:resourcekey="Label47Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hospital address" meta:resourcekey="TemplateFieldResource17">
                                        <ItemTemplate>
                                            <asp:Label ID="Label48" runat="server" Text='<%# Bind("d_address2") %>' meta:resourcekey="Label48Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="About doctor" meta:resourcekey="TemplateFieldResource18">
                                        <ItemTemplate>
                                            <asp:Label ID="Label49" runat="server" Text='<%# Bind("d_about_you") %>' meta:resourcekey="Label49Resource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            </asp:DetailsView>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <h3 style="color: #000000; font-size: large; font-weight: bold;">
                            <asp:Label ID="Label13" runat="server" Text="Availability" meta:resourcekey="Label13Resource2"></asp:Label></h3>
                        <asp:Label ID="Label11" runat="server" Text="Label" meta:resourcekey="Label11Resource2"></asp:Label>
                        <div class="table-responsive">
                            <asp:DataList ID="DataList2" dir="ltr" CssClass="table" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" meta:resourcekey="DataList2Resource2">
                                <ItemTemplate>
                                    <div class="box box-default box-solid">
                                        <div class="box-header with-border">
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                           <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                                 { %>--%>
                                            <div class="box-tools pull-right">
                                              <%--  <%}
    else
    { %>
                                                <div class="box-tools pull-left">
                                                <%} %>--%>
                                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                            </div>
                                        </div>
                                        <div class="box-body">

                                            <asp:DataList ID="DataList3" runat="server" RepeatColumns="3" meta:resourcekey="DataList3Resource2">

                                                <ItemTemplate>

                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server" Visible="False" Text='<%# Bind("date") %>' meta:resourcekey="Label7Resource2"></asp:Label>
                                                        <asp:Label ID="Label8" Visible="False" runat="server" Text='<%# Bind("time") %>' meta:resourcekey="Label8Resource2"></asp:Label>
                                                        <div dir="ltr">
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="doc" CssClass="btn btn-sm btn-primary" Text='<%# Bind("time") %>' CommandArgument='<%# Bind("email") %>' meta:resourcekey="LinkButton2Resource2"></asp:LinkButton>
                                                            &nbsp;
                                                        </div>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:DataList>

                                        </div>

                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>

                     <%--   <div style="padding-left: 1%; padding-right: 1%">
                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-default form-control" Style="font-weight: bold; margin-top: -6%;"  meta:resourcekey="LinkButton1Resource2" Text="More availabilities"></asp:LinkButton>

                        </div>--%>
                    </div>
                </div>


                </section>
                </div>
 



 <%-- <%if (Session["Speciality"].ToString() == "Auto")
      { %>--%>
                <div class="row">
                   <%-- <%}
    else
    { %>
                    <div class="row" dir="rtl">
                    <%} %>--%>

                    <div id="div1">



                        <div class="col-md-12" style="margin-bottom: 1cm">
                            <div class="box box-default">
                                <div class="box-header">
                                    <h3 style="color: #000000; font-size: large; font-weight: bold;">
                                        <asp:Label ID="Label17" runat="server" Text="Verified patient reviews" meta:resourcekey="Label17Resource1"></asp:Label></h3>
                                </div>

                                <div>
                                    <div class="form-group">
                                        <asp:DataList ID="DataList1" CssClass="table" runat="server" meta:resourcekey="DataList1Resource2">
                                            <ItemTemplate>


                                                <div class="row">
                                                   <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                                         { %>--%>
                                                    <div class="col-md-2">
                                                      <%--  <%}
    else
    { %>
                                                        <div class="col-md-2 pull-right">
                                                        <%} %>--%>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("u_email") %>' Visible="False" meta:resourcekey="Label3Resource2"></asp:Label>
                                                        <b>
                                                            <asp:Label ID="Label9" Font-Size="16px" Text='<%# Bind("date") %>' runat="server" meta:resourcekey="Label9Resource2"></asp:Label>
                                                        </b>
                                                        <div style="margin-top: 2%">

                                                            <asp:Label ID="Label1" Font-Size="16px" runat="server" meta:resourcekey="Label1Resource2"></asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="Label10" Text="(Verified patient)" runat="server" meta:resourcekey="Label10Resource2"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <span style="font-weight: bold; font-size: 16px">
                                                            <asp:Label ID="Label14" runat="server" Text="Wait Time" meta:resourcekey="Label14Resource2"></asp:Label></span>
                                                        <div style="margin-top: 2%">
                                                            <asp:Literal ID="Literal4" runat="server" meta:resourcekey="Literal4Resource2"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <span style="font-weight: bold; font-size: 16px">
                                                            <asp:Label ID="Label15" runat="server" Text="Bedside Manner" meta:resourcekey="Label15Resource1"></asp:Label></span>
                                                        <div style="margin-top: 2%">
                                                            <asp:Literal ID="Literal5" runat="server" meta:resourcekey="Literal5Resource2"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <span style="font-weight: bold; font-size: 16px">
                                                            <asp:Label ID="Label16" runat="server" Text="Service" meta:resourcekey="Label16Resource1"></asp:Label></span>
                                                        <div style="margin-top: 2%">
                                                            <asp:Literal ID="Literal6" runat="server" meta:resourcekey="Literal6Resource2"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">

                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("u_review") %>' meta:resourcekey="Label2Resource2"></asp:Label>
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
</asp:Content>

