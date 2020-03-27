<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" CodeFile="Hospitaldoctoravailability.aspx.cs" Inherits="User_Hospital_doctor_availability" Culture="en-US" meta:resourcekey="PageResource2" uiCulture="en-US" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>--%>
    <%--<script src="../Design/plugins/slimScroll/jquery.slimscroll.min.js"></script>--%>
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
    <style type="text/css">
         /*#iw-container {
            margin-bottom: 10px;
        }
        #content {
            float: right;
            margin: 10px;
        }*/
    </style>


     <%--Script for loading map  --%>
     <%--<script type="text/javascript" src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyB4Wo74MK6m0aOMNpgY7381lYNkrMsFeyQ&sensor=false&libraries=places"></script>--%>
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
            var lng = document.getElementById('<%=Long.ClientID%>').value;
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

             //Marker mouse over addreess shows
             (function(marker,myLatlng){
                 google.maps.event.addListener(marker, "mouseover", function (e) {
                     //alert("Haaiiii");
                    
                     var geocoder = geocoder = new google.maps.Geocoder();
                     geocoder.geocode({ 'latLng': myLatlng }, function (results, status) {
                         if (status == google.maps.GeocoderStatus.OK) {
                             if (results[0]) {
                                 //alert("Make sure this your exact location ?" + "\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                                 var infoContent ='<div id="iw-container">' + 
                                        '<div id="content">' +
                                        '<div>' +results[0].formatted_address+ '</div>' +
                                        '</div>' +
                                        '</div>';
                                 infoWindow.setContent(infoContent);
                                 infoWindow.open(map, marker);
                             }
                         }
                     });

                    
                 });

                 google.maps.event.addListener(marker, "mouseout", function (e) {
                     //infoWindow.close();

                     setTimeout(function () {

                         infoWindow.close();
                     },2500);
                 });

             })(marker, myLatlng);
             // Ends 

             map.panBy(350, 0);
         }

     </script>
    <%--Ends map script  --%>
    
  <!-- Latest compiled and minified CSS -->

      <%--<script src="../js/jquery-1.3.2.js"></script>    --%>
<!-- jQuery library -->

     
   <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
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
      <section class="content" style="margin-bottom:1cm;margin-top:1.5cm;">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-default">
                <div class="box-body">
                    <div class="row">
                        <%-- <%if (Session["Speciality"].ToString() == "Auto")
                             { %>--%>
                        <div class="col-md-4">
                            <%--<%}
    else
    { %>
                             <div class="col-md-4 pull-right">
                            <%} %>--%>
                                 <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                      { %>--%>
                                 <div>
                                 <%--    <%}
    else
    { %>
                                      <div class="pull-right">
                                     <%} %>--%>
                            <asp:Image ID="Image1" CssClass="img-responsive img-circle img-lg" runat="server" meta:resourcekey="Image1Resource2" />
                                     </div>
                        </div>
                            <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                 { %>--%>
                        <div class="col-md-4">
                          <%--  <%}
    else
    { %>
                             <div class="col-md-4 pull-right">
                            <%} %>--%>
                            <div class="form-group">
                                <h2><b>Dr.<asp:Label ID="lblname" runat="server" Text="Label" meta:resourcekey="lblnameResource2"></asp:Label></b></h2>
                            </div>
                            <div class="form-group">
                                <b>
                                    <asp:Label ID="lblql" runat="server" Text="Label" meta:resourcekey="lblqlResource2"></asp:Label></b>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblspec" runat="server" Text="Label" meta:resourcekey="lblspecResource2"></asp:Label>
                            </div>

                        </div>
                           <%--   <%if (Session["Speciality"].ToString() == "Auto")
                                  { %>--%>
                        <div class="col-md-4">
                          <%--  <%}
    else
    { %>
 <div class="col-md-4 pull-left">
                            <%} %>--%>
                            <div class="form-group">
                                <asp:Label ID="Label13" runat="server" Text="Hospital Name:" meta:resourcekey="Label13Resource1"></asp:Label>   <b>
                                    <asp:Label ID="hname" runat="server" Text="Label" meta:resourcekey="hnameResource2"></asp:Label></b>
                            </div>
                            <div class="form-group">
                                <b><%--<asp:LinkButton ID="LinkButton1" runat="server">Read patient review</asp:LinkButton>--%></b>
                            </div>

                        </div>
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
          <%--   <%if (Session["Speciality"].ToString() == "Auto")
                 { %>--%>
            <div class="col-md-12">
               <%-- <%}
    else
    { %>
                <div class="col-md-12 pull-right">
                <%} %>--%>
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Label ID="Label12" runat="server" Text="Search availability" meta:resourcekey="Label12Resource1"></asp:Label></h3>
                    </div>
                    <div class="box-body">

                        <div class="form-group">
                            <div id="Div1" class="table-responsive">


                                <asp:DataList ID="DataList2" CssClass="table" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" meta:resourcekey="DataList2Resource2">
                                    <ItemTemplate>
                                        <div class="box box-primary box-solid">
                                            <div class="box-header with-border">
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("date") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                                <div class="box-tools pull-right">
                                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                </div>
                                            </div>
                                            <div class="box-body">
                                                <asp:DataList ID="DataList3" runat="server" RepeatColumns="4" OnItemCommand="DataList3_ItemCommand" meta:resourcekey="DataList3Resource2">

                                                    <ItemTemplate>

                                                        <div class="form-group">
                                                            <asp:Label ID="Label7" runat="server" Visible="False" Text='<%# Bind("date") %>' meta:resourcekey="Label7Resource2"></asp:Label>
                                                            <asp:Label ID="Label8" Visible="False" runat="server" Text='<%# Bind("time") %>' meta:resourcekey="Label8Resource2"></asp:Label>
                                                            <%--<asp:Button ID="Button2" CommandName="doc" CssClass="btn btn-sm btn-bitbucket" runat="server" Text='<%# Bind("time") %>' CommandArgument='<%#Bind("email") %>' />--%>
                                                            &nbsp;<asp:LinkButton ID="LinkButton2" CommandName="doc" CssClass="btn btn-sm btn-default" Text='<%# Bind("time") %>' CommandArgument='<%# Bind("email") %>' runat="server" meta:resourcekey="LinkButton2Resource2"></asp:LinkButton>
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

           <%-- <div class="col-md-5">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title">
                    </div>
                    <div class="box-body">
                   
                    </div>
                    <div class="box-footer">
                        <div class="form-group">
                           
                             <div>
                                
                                 <div class="pull-left">

                             
                        </div>
                            </div>
                    </div>
                </div>
                 <div>

              
                </div>
            </div>
        </div>--%>
      



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
                                   <%-- <%}
    else
    { %>
                                    <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <%} %>--%>
                                 <%--   <%if (Session["Speciality"].ToString() == "Auto")
                                        { %>--%>
                                    <h4 class="modal-title">
                                     <%--   <%}
    else
    { %>
                                         <h4 class="modal-title pull-right">
                                        <%} %>--%>
                                        <asp:Label ID="lblModalTitle" runat="server" Text="Login" meta:resourcekey="lblModalTitleResource2"></asp:Label></h4>
                                </div>
                                 <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                      { %>--%>
                                <div class="modal-body">
                                    <%--<%}
    else
    { %>
                                     <div class="modal-body" dir="rtl">
                                    <%} %>--%>

                                    <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label2" runat="server" Text=" Login ID"></asp:Label></label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="v" ControlToValidate="login" runat="server" ErrorMessage="* Required" ForeColor="Red" meta:resourcekey="RequiredFieldValidator3Resource2"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="login" placeholder="Email id or Contact number" ValidationGroup="v" CssClass="form-control" runat="server" meta:resourcekey="loginResource2"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="Label3" runat="server" Text="Password" meta:resourcekey="Label3Resource1"></asp:Label></label>
                                                    <asp:RequiredFieldValidator ControlToValidate="password" ForeColor="Red" ValidationGroup="v" ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Required" meta:resourcekey="RequiredFieldValidator4Resource2"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="password" CssClass="form-control" ValidationGroup="v" placeholder="Password" TextMode="Password" runat="server" meta:resourcekey="passwordResource2"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Index/SignInSignUp.aspx" runat="server" meta:resourcekey="HyperLink1Resource2">Create new account</asp:HyperLink>
                                              <%--   <%if (Session["Speciality"].ToString() == "Auto")
                                                     { %>--%>
                                                <div>
                                                   <%-- <%}
    else
    { %>
                                               
    <div class="pull-left">
                                                    <%} %>--%>
                                                <asp:Button ID="Button2" CssClass="btn btn-sm btn-success pull-right" ValidationGroup="v" UseSubmitBehavior="False" runat="server" Text="SignIn" OnClick="Button2_Click"  meta:resourcekey="Button2Resource3" />
                                                     </div>
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

                       <div class="modal fade" id="myModal2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal2" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
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
                                    <h4 class="modal-title">
                                     </h4>
                                </div>
                                <div class="modal-body">

                              
                                    <div class="form-group">
                                        <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label>
                                    </div>





                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </ContentTemplate>
                     <%--   <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
            </div>


                <div class="modal fade" id="myModal3" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <asp:UpdatePanel ID="upModal3" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                             { %>--%>
                                         <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title">
                                          <%--  <%}
    else
    { %>
                                              <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title pull-right">
                                            <%} %>--%>
                                                                        <asp:Label ID="Label4" runat="server" Text="Book an Appointment" meta:resourcekey="Label4Resource1"></asp:Label>

                                        </h4>

                                    </div>
                                   <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                         { %>--%>
                                    <div class="modal-body">
                                       <%-- <%}
    else
    { %>
                                        <div class="modal-body" dir="rtl">
                                        <%} %>--%>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label5" runat="server" Text="Date" meta:resourcekey="Label5Resource1"></asp:Label></label>
                                            <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" ReadOnly="True" CausesValidation="True" ValidationGroup="a" meta:resourcekey="TextBox1Resource2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required" ForeColor="Red" ControlToValidate="textbox1" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label9" runat="server" Text="Time" meta:resourcekey="Label9Resource1"></asp:Label></label>
                                            <asp:TextBox ID="Time" CssClass="form-control" ReadOnly="True" placeholder="Time (Eg. 02.00 PM)" runat="server" ValidationGroup="a" meta:resourcekey="TimeResource2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required" ForeColor="Red" ControlToValidate="time" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator2Resource2"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label10" runat="server" Text="Payment" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                            <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" meta:resourcekey="DropDownList2Resource2">
                                                <asp:ListItem meta:resourcekey="ListItemResource4">Payment my self</asp:ListItem>

                                            </asp:DropDownList>

                                        </div>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label11" runat="server" Text="Reason to visit" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                            <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" meta:resourcekey="DropDownList1Resource2">
                                                <asp:ListItem meta:resourcekey="ListItemResource5">Illness</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource6">Monthly check up</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                       
                                    </div>
                                    <div class="modal-footer">
                                        <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                             { %>--%>
                                         <div>
                                            <%-- <%}
    else
    { %><div class="pull-left">
                                             <%} %>--%>
        <asp:Button ID="Button1" runat="server" Text="Take appointment" CssClass="btn btn-success pull-right" UseSubmitBehavior="false" data-dismiss="modal" OnClick="LinkButton1_Click" />
                                            <%--<asp:LinkButton ID="LinkButton1" ValidationGroup="a" CssClass="btn btn-success pull-right" runat="server" data-dismiss="modal" OnClick="LinkButton1_Click" meta:resourcekey="LinkButton1Resource2">Take appointment</asp:LinkButton>--%>

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


    </div>
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
          </section>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
<!-- Latest compiled JavaScript -->
  
</asp:Content>

