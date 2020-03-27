<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" Culture="en-US" UICulture="en-US" CodeFile="doctoravailability.aspx.cs" Inherits="User_doctor_availability" meta:resourcekey="PageResource2" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>--%>
    <script src="../Design/plugins/slimScroll/jquery.slimscroll.min.js"></script>


    <style>
                   /* ----------- iPhone 4 and 4S ----------- */

/* Portrait and Landscape */
@media only screen 
  and (min-device-width: 320px) 
  and (max-device-width: 480px)
  and (-webkit-min-device-pixel-ratio: 2) {

        .font-small{
        font-size:15px;
    }
    
        #lblname{
            font-size:15px;
        }
         #lblname1{
            font-size:15px;
        }
         #doctorname{
             text-align:right;
         }
}

/* Portrait */
@media only screen 
  and (min-device-width: 320px) 
  and (max-device-width: 480px)
  and (-webkit-min-device-pixel-ratio: 2)
  and (orientation: portrait) {


    .font-small{
        font-size:15px;
    }

      #lblname{
            font-size:15px;
        }
         #lblname1{
            font-size:15px;
        }
}

/* Landscape */
@media only screen 
  and (min-device-width: 320px) 
  and (max-device-width: 480px)
  and (-webkit-min-device-pixel-ratio: 2)
  and (orientation: landscape) {


        .font-small{
        font-size:15px;
    }
           #lblname{
            font-size:15px;
        }
         #lblname1{
            font-size:15px;
        }
}

/* ----------- iPhone 5, 5S, 5C and 5SE ----------- */

/* Portrait and Landscape */
@media only screen 
  and (min-device-width: 320px) 
  and (max-device-width: 568px)
  and (-webkit-min-device-pixel-ratio: 2) {

        .font-small{
        font-size:15px;
    }
     
           #lblname{
            font-size:15px;
        }
         #lblname1{
            font-size:15px;
        }
}

    </style>

    <script type="text/javascript">
        //$(function () {
        //    $('#updates').slimScroll({
        //        height: '2000px',

        //    });
        //});
        $(function () {
            $('#Div1').slimScroll({
                height: '500px'

            });
        });
    </script>
    <script type="text/javascript">
        function Validate() {
            return confirm('Save in db?');
        }
    </script>
    <style type="text/css">
        /*#iw-container {
            margin-bottom: 10px;
        }

        #content {
            float: right;
            margin: 10px;
        }

        #doctorname {
            margin-left: -15%;
            margin-top: -2%;
        }

        #rating {
            margin-left: 15%;
        }

        #ContentPlaceHolder1_Image1 {
            margin-left: 20%;
        }*/
    </style>



    <%--Script for loading map  --%>
    <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCNo1WjdpDfX7wvziSy2HeS0d-axd0Yv50&sensor=false&libraries=places"></script>--%>
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
            var lat = document.getElementById('<%=Lat.ClientID%>').value;
            var lng = document.getElementById('<%=Long.ClientID%>').value
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


   
























    <%--Ends map script  --%>
    <%-- <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>--%>


    <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>

    <link href="../css/sweetalert.css" rel="stylesheet" />

    <script src="../js/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="Lat" runat="server" />
    <asp:HiddenField ID="Long" runat="server" />
    <section class="content" style="margin-top: 1.5cm; margin-bottom: 1cm">

        <asp:HiddenField ID="HiddenField1" runat="server" />

        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000" OnTick="Timer1_Tick"></asp:Timer>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-default">
                    <div class="box-body">
                        <div class="row">
                          <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                { %>--%>
                            <div class="col-md-4">
                                <asp:Image ID="Image1" CssClass="img-circle img-responsive img-lg" runat="server" meta:resourcekey="Image1Resource2" />
                            </div>
                            <div class="col-md-8" id="doctorname" style="text-align:right">
                                <div class="form-group">

                                    <h2><b><span class="font-small">Dr.</span><asp:Label ID="lblname" runat="server" Text="Label" meta:resourcekey="lblnameResource2" class="font-small"></asp:Label></b></h2>
                                </div>
                                <div class="form-group">
                                    <b>
                                        <asp:Label ID="lblql" runat="server" Text="Label" meta:resourcekey="lblqlResource2"></asp:Label></b>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblspec" runat="server" Text="Label" meta:resourcekey="lblspecResource2"></asp:Label>
                                </div>

                            </div>
                           <%-- <div class="col-md-4" id="rating">
                                <b>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-success btn" OnClick="LinkButton1_Click" meta:resourcekey="LinkButton1Resource2">Read patient review</asp:LinkButton></b>
                            </div>--%>
                       <%--     <%}
                                else
                                { %>

                            <div class="col-md-4 pull-right">
                                <asp:Image ID="Image2" CssClass="img-circle img-responsive img-lg pull-right" runat="server" meta:resourcekey="Image2Resource1" />
                            </div>
                            <div class="col-md-8" dir="rtl" id="doctorname"  style="text-align:left">
                                <div class="form-group">
                                    <h2><b><span class="font-small">Dr.</span><asp:Label ID="lblname1" runat="server" Text="Label" meta:resourcekey="lblname1Resource1" class="font-small"></asp:Label></b></h2>
                                </div>
                                <div class="form-group">
                                    <b>
                                        <asp:Label ID="lblql1" runat="server" Text="Label" meta:resourcekey="lblql1Resource1"></asp:Label></b>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblspec1" runat="server" Text="Label" meta:resourcekey="lblspec1Resource1"></asp:Label>
                                </div>

                            </div>--%>
                         <%--   <div class="col-md-4 pull-right" dir="rtl" id="rating">
                                <b>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn-success btn pull-right" OnClick="LinkButton1_Click" meta:resourcekey="LinkButton3Resource1">قراءة مراجعة المريض</asp:LinkButton></b>
                            </div>--%>


                         <%--   <%} %>--%>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="dvMap" style="width: 100%; height: 150px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <%-- <div class="col-md-5">

                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title"></h3>
                        </div>
                        <div class="box-body">
                        </div>
                        <div class="box-footer">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>


                </div>--%>
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                           <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                { %>--%>
                            <h3 class="box-title" style="font-size:12px">Search availability</h3>

                            <div class="box-tools pull-right">
                                <%--<button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
                                <span class="label-success label text-success">Available</span><span class="label-danger label text-danger">Booked</span><%--<span class="label label-warning text-warning">Not confirmed</span>--%>
                            </div>
                         <%--   <%}
                                else
                                { %>
                            <div class="row">
                                <div class="col-md-3 pull-left">
                                    <div class="box-tools pull-left">
                                        <span dir="rtl" class="label-success label text-success">متاح</span><span class="label-danger label text-danger">حجز</span>
                                    </div>
                                </div>
                                <div class="col-md-9 pull-right">
                                    <h3 class="box-title pull-right" style="font-size:12px">
                                        <asp:Label ID="Label3" runat="server" Text="توفر البحث" meta:resourcekey="Label3Resource1"></asp:Label></h3>
                                </div>
                            </div>
                            <% }%>--%>
                        </div>
                        <div class="box-body">

                            <div class="form-group">

                                <div class="table-responsive">


                                    <asp:DataList ID="DataList2" CssClass="table" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" meta:resourcekey="DataList2Resource2">
                                        <ItemTemplate>
                                            <div class="box box-default box-solid">

                                                <div class="box-header with-border">
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                                    <div class="box-tools pull-right">
                                                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                    </div>
                                                </div>


                                                <div class="box-body">
                                                    <asp:DataList ID="DataList3" runat="server" RepeatColumns="5" OnItemCommand="DataList3_ItemCommand" meta:resourcekey="DataList3Resource2">

                                                        <ItemTemplate>

                                                            <div class="form-group">
                                                                <asp:Label ID="Label7" runat="server" Visible="False" Text='<%# Bind("date") %>' meta:resourcekey="Label7Resource2"></asp:Label>
                                                                <asp:Label ID="Label8" Visible="False" runat="server" Text='<%# Bind("time") %>' meta:resourcekey="Label8Resource2"></asp:Label>
                                                                <div>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="doc" CssClass="btn btn-xs btn-success" Text='<%# Bind("time") %>' CommandArgument='<%# Bind("d_hakkimid") %>' meta:resourcekey="LinkButton2Resource2"></asp:LinkButton>
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
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <!--//model popup for alert-->


            <!--//modal popup-->
        </div>


        <!-- Bootstrap Modal Dialog -->
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                               <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                              <%--  <%}
                                    else
                                    { %>
                                <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <%} %>--%>
                                <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                     { %>--%>
                                <h4 class="modal-title">
                                    <%--<%}
    else
    { %><h4 class="modal-title pull-right">
                                    <%} %>--%>
                                    <asp:Label ID="lblModalTitle" runat="server" Text="Login" meta:resourcekey="lblModalTitleResource2"></asp:Label></h4>
                            </div>
                             <%--<%if (Session["Speciality"].ToString() == "Auto")
                                 { %>--%>
                            <div class="modal-body">
                                <%--<%}
    else
    { %>
                                 <div class="modal-body" dir="rtl">
                                <%} %>--%>
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label12" runat="server" Text=" Login Id"></asp:Label></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="v" ControlToValidate="login" runat="server" ErrorMessage="* Required" ForeColor="Red" meta:resourcekey="RequiredFieldValidator3Resource2"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="login" placeholder="Email id or Hakkeem Id" ValidationGroup="v" CssClass="form-control" runat="server" meta:resourcekey="loginResource2"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Password" meta:resourcekey="Label13Resource1"></asp:Label></label>
                                    <asp:RequiredFieldValidator ControlToValidate="password" ForeColor="Red" ValidationGroup="v" ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Required" meta:resourcekey="RequiredFieldValidator4Resource2"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="password" CssClass="form-control" ValidationGroup="v" placeholder="Password" TextMode="Password" runat="server" meta:resourcekey="passwordResource2"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Index/UserSignup.aspx" ForeColor="#4AA9AF" runat="server" meta:resourcekey="HyperLink1Resource2">Create new account</asp:HyperLink>
                                    <asp:Button ID="Button2" CssClass="btn btn-sm btn-success pull-right" ValidationGroup="v" runat="server" Text="SignIn" OnClick="Button2_Click"  meta:resourcekey="Button2Resource3" />
                                </div>
                                <div class="form-group">
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Label ID="Label1" Visible="False" runat="server" Text="Label" ForeColor="#FF3300" meta:resourcekey="Label1Resource2"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>



        <%--book an appointment modal--%>

        <div class="modal fade" id="myModal1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                              <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                               <%-- <%}
                                    else
                                    { %>
                                <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <%} %>--%>
                               <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <h4 class="modal-title">
                                   <%-- <%}
                                    else
                                    { %>
                                    <h4 class="modal-title pull-right">
                                        <%} %>--%>
                                        <asp:Label ID="Label4" runat="server" Text="Book an Appointment" meta:resourcekey="Label4Resource1"></asp:Label>
                                    </h4>
                            <%--    <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <div class="modal-body">
                                    <%--<%}
                                    else
                                    { %>
                                    <div class="modal-body" dir="rtl">
                                        <%} %>--%>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label5" runat="server" Text="Date" meta:resourcekey="Label5Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="bb" runat="server" ErrorMessage="* Enter date" ForeColor="Red" ControlToValidate="textbox1" meta:resourcekey="RequiredFieldValidator2Resource2"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" ValidationGroup="bb" Enabled="False" ReadOnly="True" meta:resourcekey="TextBox1Resource2"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label9" runat="server" Text="Time" meta:resourcekey="Label9Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="bb" runat="server" ErrorMessage="* Enter time" ForeColor="Red" ControlToValidate="textbox2" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TextBox2" CssClass="form-control" placeholder="Time (Eg. 2 PM)" runat="server" ValidationGroup="bb" ReadOnly="True" meta:resourcekey="TextBox2Resource2"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label10" runat="server" Text="Payment" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                            <asp:DropDownList ID="DropDownList2" CssClass="form-control appdrop" runat="server" meta:resourcekey="DropDownList2Resource2">
                                                <asp:ListItem meta:resourcekey="ListItemResource4">Payment my self</asp:ListItem>

                                            </asp:DropDownList>

                                        </div>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label11" runat="server" Text="Reason to visit" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                            <asp:DropDownList ID="DropDownList1" CssClass="form-control appdrop" runat="server" meta:resourcekey="DropDownList1Resource2">
                                                <asp:ListItem meta:resourcekey="ListItemResource5">Illness</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource6">Monthly check up</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Label ID="Label2" CssClass="pull-left" Font-Bold="True" runat="server" Text="Label" meta:resourcekey="Label2Resource2"></asp:Label>
                                        <asp:Button ID="Button1" CssClass="btn btn-success pull-right" ValidationGroup="bb" runat="server" Text="Take appointment" data-dismiss="modal" UseSubmitBehavior="false" OnClick="Button1_Click" meta:resourcekey="Button1Resource3" />
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>


        <%--Alert modal--%>

        <div class="modal fade" id="myModal2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal2" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                 <%--<%if (Session["Speciality"].ToString() == "Auto")
                                     { %>--%>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Hakkeem</h4>
          <%--                      <%}
    else
    { %>
                                 <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title pull-right">حكيم</h4>
                                <%} %>--%>
                            </div>
                             <%--<%if (Session["Speciality"].ToString() == "Auto")
                                 { %>--%>
                            <div class="modal-body">
                               <%-- <%}
    else
    { %>
                                <div class="modal-body" dir="rtl">
                                <%} %>--%>
                                <asp:Label ID="Label14" runat="server"></asp:Label>
                            </div>
                            <div class="modal-footer">

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </section>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.4;
        }

        .modalPopup {
            /*background-color: #FFFFFF;*/
            border-width: 0px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 3px;
            width: 500px;
            height: 300px;
        }
    </style>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  
    <script src="../Design/dist/js/app.min.js"></script>
  
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>

