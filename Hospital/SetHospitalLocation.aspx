﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="SetHospitalLocation.aspx.cs" Inherits="Hospital_SetHospitalLocation" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #iw-container {
            margin-bottom: 10px;
        }

        #image {
            float: left;
        }

        #imgs {
            width: 60px;
            height: 60px;
            border: 1px solid #ddd;
            border-radius: 50%;
        }

        #content {
            float: right;
            margin: 10px;
        }

        #txtsearch {
            background-color: #fff;
            padding: 0 11px 0 13px;
            width: 400px;
            font-family: Roboto;
            font-size: 15px;
            font-weight: 300;
            text-overflow: ellipsis;
        }

            #txtsearch:focus {
                border-color: #4d90fe;
                margin-left: -1px;
                padding-left: 14px;
                width: 401px;
            }

        .apply {
            margin-top: 16px;
            border: 1px solid transparent;
            border-radius: 2px 0 0 2px;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            height: 32px;
            outline: none;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
        }
    </style>
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
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB4Wo74MK6m0aOMNpgY7381lYNkrMsFeyQ&sensor=false&libraries=places"></script>
    <script type="text/javascript">
        var markers = [
       <asp:Repeater ID="rptMarkers" runat="server">
       <ItemTemplate>
                {
                    "id":'<%# Eval("id")%>',
                    "name": '<%# Eval("Name") %>',
                    "lat": '<%# Eval("Latitude") %>',
                    "lng": '<%# Eval("Longitude") %>',
                    "address":'<%# Eval("h_address") %>',
                    "photo":'<%# Eval("h_photo") %>',
                }
             </ItemTemplate>
             <SeparatorTemplate>
                ,
                </SeparatorTemplate>
        </asp:Repeater>
        ];
    </script>

    <script type="text/javascript">
        var allMarkers=[];
        var icon2='../images/hovericon.png';
        var icon1='../images/add-location-point.png';
        var Lat;
        var Lng;
        window.onload = function () {
            if(markers[0].lat !='24.774265' && markers[0].lng !='46.738586' ){
                var mapOptions = {
                    center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                    zoom: 14,
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    mapTypeControl: false,
                    mapTypeControlOptions: {
                        style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                        position: google.maps.ControlPosition.TOP_CENTER
                    },
                    zoomControl: true,
                    zoomControlOptions: {
                        position: google.maps.ControlPosition.LEFT_TOP
                    },
                    scaleControl:false,
                    streetViewControl: false,
                    streetViewControlOptions: {
                        position: google.maps.ControlPosition.LEFT_TOP
                    },
                    styles:[
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
            
                var infoWindow = new google.maps.InfoWindow();
                var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
                for (i = 0; i < markers.length; i++) {
                    var data = markers[i]
                    var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                    var marker = new google.maps.Marker({
                        position: myLatlng,
                        map: map,
                        title: data.title,
                        id:data.id,
                        icon:icon1,
                    });
                    allMarkers.push(marker);
                    (function (marker, data) {
                        google.maps.event.addListener(marker, "mouseover", function (e) {
                            var imgpath=(data.photo).slice(2);

                            var infoContent='<div id="iw-container">'+' <div id="image" >' +
                                             '<img src="Clinic/../../'+imgpath+'" alt="'+data.name +'" id="imgs" >'+
                                              '</div>'+
                                               '<div id="content">'+
                                            '<div>' + data.name + '</div>'+
                                            '<div>' + data.address + '</div>'+
                                               '</div>'+
                                            '</div>';
                            infoWindow.setContent(infoContent);
                            infoWindow.open(map, marker);
                        });

                        google.maps.event.addListener(marker, "mouseout", function (e) {
                            infoWindow.close();

                        });

                        google.maps.event.addListener(marker,"click",function(e){
                            //alert("Latitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                            // getting the exact location details

                            var latlng = new google.maps.LatLng(e.latLng.lat(), e.latLng.lng());
                            var geocoder = geocoder = new google.maps.Geocoder();
                            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                                if (status == google.maps.GeocoderStatus.OK) {
                                    if (results[1]) {
                                        alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                                        document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                                    }
                                }
                            });
                            placeMarker(e.latLng);
                            document.getElementById("<%=Lat.ClientID%>").value=e.latLng.lat();
                            document.getElementById("<%=Lng.ClientID%>").value=e.latLng.lng();

                        });








                    })(marker, data);
                }

                 
                //// Create the search box and link it to the UI element.
                var input =(document.getElementById('txtsearch'));  
                map.controls[google.maps.ControlPosition.TOP_RIGHT].push(input)
                //var searchBox = new google.maps.places.SearchBox((input));

                var autocomplete = new google.maps.places.Autocomplete(input);
                autocomplete.bindTo('bounds', map);

                autocomplete.addListener('place_changed', function() {
                    //marker.setVisible(false);
                    var place = autocomplete.getPlace();
                    if (!place.geometry) {
                        // User entered the name of a Place that was not suggested and
                        // pressed the Enter key, or the Place Details request failed.
                        window.alert("No details available for input: '" + place.name + "'");
                        return;
                    }

                    else
                    {
                           

                        var latlng = new google.maps.LatLng(place.geometry.location.lat(), place.geometry.location.lng());
                        //alert(place.geometry.location.lat());
                        //alert(place.geometry.location.lng());
                        //alert(latlng);
                        var geocoder = geocoder = new google.maps.Geocoder();
                        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK) {
                                if (results[0]) {
                                    alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + place.geometry.location.lat()+ "\r\nLongitude: " + place.geometry.location.lng());
                                    document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                                }
                            }
                        });

                        if (place.geometry.viewport) {
                            map.fitBounds(place.geometry.viewport);
                            //placeMarker(place.geometry.viewport);
                        } else {
                             
                            //placeMarker(pos);
                            map.setCenter(place.geometry.location);
                            map.setZoom(17);  // Why 17? Because it looks good.
                        }
                        placeMarker(place.geometry.location);
                     
                        document.getElementById("<%=Lat.ClientID%>").value=place.geometry.location.lat();
                        document.getElementById("<%=Lng.ClientID%>").value=place.geometry.location.lng();



                    }
                     
                });

                /////Getting current location...

                // Create the DIV to hold the control and call the constructor passing in this DIV
                var geolocationDiv = document.createElement('div');
                var geolocationControl = new GeolocationControl(geolocationDiv, map);

                map.controls[google.maps.ControlPosition.BOTTOM_CENTER].push(geolocationDiv);

                function GeolocationControl(controlDiv, map) {

                    // Set CSS for the control button
                    var controlUI = document.createElement('div');
                    controlUI.style.backgroundColor = '#444';
                    controlUI.style.borderStyle = 'solid';
                    controlUI.style.borderWidth = '1px';
                    controlUI.style.borderColor = 'white';
                    controlUI.style.height = '28px';
                    controlUI.style.marginTop = '5px';
                    controlUI.style.cursor = 'pointer';
                    controlUI.style.textAlign = 'center';
                    controlUI.title = 'Click to center map on your location';
                    controlDiv.appendChild(controlUI);

                    // Set CSS for the control text
                    var controlText = document.createElement('div');
                    controlText.style.fontFamily = 'Arial,sans-serif';
                    controlText.style.fontSize = '10px';
                    controlText.style.color = 'white';
                    controlText.style.paddingLeft = '10px';
                    controlText.style.paddingRight = '10px';
                    controlText.style.marginTop = '8px';
                    controlText.innerHTML = 'Click to choose your current location';
                    controlUI.appendChild(controlText);

                    // Setup the click event listeners to geolocate user
                    google.maps.event.addDomListener(controlUI, 'click', geolocate);
                }
                 
                function geolocate() {

                    if (navigator.geolocation) {

                        navigator.geolocation.getCurrentPosition(function (position) {


                            var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

                            var geocoder = geocoder = new google.maps.Geocoder();
                            geocoder.geocode({ 'latLng': pos }, function (results, status) {
                                if (status == google.maps.GeocoderStatus.OK) {
                                    if (results[0]) {
                                        //alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                                        document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                                    }
                                }
                            });

                            // Create a marker and center map on user location
                            //marker = new google.maps.Marker({
                            //    position: pos,
                            //    draggable: true,
                            //    animation: google.maps.Animation.DROP,
                            //    map: map
                            //});
                            document.getElementById("<%=Lat.ClientID%>").value=position.coords.latitude;
                            document.getElementById("<%=Lng.ClientID%>").value=position.coords.longitude;

                            placeMarker(pos);

                            map.setCenter(pos);
                        });
                    }
                }

                /////Ends Getting Current Location...

                // Ends here 
                google.maps.event.addListener(map,"click",function(e){
                    //alert("Latitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                    // getting the exact location details

                    var latlng = new google.maps.LatLng(e.latLng.lat(), e.latLng.lng());
                    var geocoder = geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            if (results[1]) {
                                alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                                document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                             }
                         }
                     });
                     placeMarker(e.latLng);
                     document.getElementById("<%=Lat.ClientID%>").value=e.latLng.lat();
                     document.getElementById("<%=Lng.ClientID%>").value=e.latLng.lng();

                 });

                 function placeMarker(location){
                     deleteOverlays();

                     var marker1 = new google.maps.Marker({
                         position: location, 
                         map: map,
                         icon:icon1,
                     });
                     
                     ( function (marker1, data) {
                         google.maps.event.addListener(marker1, "mouseover", function (e) {
                             var imgpath=(data.photo).slice(2);
                             var infoContent='<div id="iw-container">'+' <div id="image" >' +
                                              '<img src="Clinic/../../'+imgpath+'" alt="'+data.name +'" id="imgs" >'+
                                               '</div>'+
                                                '<div id="content">'+
                                             '<div>' + data.name + '</div>'+                                            
                                             '<div>' + data.address + '</div>'+
                                                '</div>'+
                                             '</div>';
                             //infoWindow.setContent(infoContent);
                             //infoWindow.open(map, marker1);
                         });

                         google.maps.event.addListener(marker1, "mouseout", function (e) {
                             infoWindow.close();

                         });


                         google.maps.event.addListener(marker1,"click",function(e){
                             //alert("Latitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                             // getting the exact location details

                             var latlng = new google.maps.LatLng(e.latLng.lat(), e.latLng.lng());
                             var geocoder = geocoder = new google.maps.Geocoder();
                             geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                                 if (status == google.maps.GeocoderStatus.OK) {
                                     if (results[1]) {
                                         alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                                         document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                                     }
                                 }
                             });
                             placeMarker(e.latLng);
                             document.getElementById("<%=Lat.ClientID%>").value=e.latLng.lat();
                             document.getElementById("<%=Lng.ClientID%>").value=e.latLng.lng();

                         });


                     })(marker1,data);
                     allMarkers.push(marker1);
                 };

                 function deleteOverlays(){
                     if (allMarkers) {
                         for (i in allMarkers) {
                             allMarkers[i].setMap(null);
                         }
                         allMarkers.length = 0;
                     }

                 };
             }
                // If Location is not set till..
             else{

                 var mapOptions = {
                     center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                     zoom: 14,
                     mapTypeId: google.maps.MapTypeId.ROADMAP,
                     mapTypeControl: false,
                     mapTypeControlOptions: {
                         style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                         position: google.maps.ControlPosition.TOP_CENTER
                     },
                     zoomControl: true,
                     zoomControlOptions: {
                         position: google.maps.ControlPosition.LEFT_TOP
                     },
                     scaleControl:false,
                     streetViewControl: false,
                     streetViewControlOptions: {
                         position: google.maps.ControlPosition.LEFT_TOP
                     },
                     styles:[
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
            
                 var infoWindow = new google.maps.InfoWindow();
                 var latlngbounds = new google.maps.LatLngBounds();
                 var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
                 var data=markers[0];

                // Create the search box and link it to the UI element.
                 var input =(document.getElementById('txtsearch')); 
                 map.controls[google.maps.ControlPosition.TOP_RIGHT].push(input);

                 var autocomplete = new google.maps.places.Autocomplete(input);
                 autocomplete.bindTo('bounds', map);

                 autocomplete.addListener('place_changed', function() {
                     var place = autocomplete.getPlace();
                     if (!place.geometry) {
                         // User entered the name of a Place that was not suggested and
                         // pressed the Enter key, or the Place Details request failed.
                         window.alert("No details available for input: '" + place.name + "'");
                         return;
                     }

                     else
                     {
                           

                         var latlng = new google.maps.LatLng(place.geometry.location.lat(), place.geometry.location.lng());
                         //alert(place.geometry.location.lat());
                         //alert(place.geometry.location.lng());
                         //alert(latlng);
                         var geocoder = geocoder = new google.maps.Geocoder();
                         geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                             if (status == google.maps.GeocoderStatus.OK) {
                                 if (results[0]) {
                                     alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + place.geometry.location.lat()+ "\r\nLongitude: " + place.geometry.location.lng());
                                     document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                                 }
                             }
                         });

                         if (place.geometry.viewport) {
                             map.fitBounds(place.geometry.viewport);
                             //placeMarker(place.geometry.viewport);
                         } else {
                             
                             //placeMarker(pos);
                             map.setCenter(place.geometry.location);
                             map.setZoom(17);  // Why 17? Because it looks good.
                         }
                         placeMarker(place.geometry.location);
                     
                         document.getElementById("<%=Lat.ClientID%>").value=place.geometry.location.lat();
                         document.getElementById("<%=Lng.ClientID%>").value=place.geometry.location.lng();



                     }
                     
                     
                 });

                 
                /////Getting current location...

                // Create the DIV to hold the control and call the constructor passing in this DIV
                 var geolocationDiv = document.createElement('div');
                 var geolocationControl = new GeolocationControl(geolocationDiv, map);

                 map.controls[google.maps.ControlPosition.BOTTOM_CENTER].push(geolocationDiv);

                 function GeolocationControl(controlDiv, map) {

                     // Set CSS for the control button
                     var controlUI = document.createElement('div');
                     controlUI.style.backgroundColor = '#444';
                     controlUI.style.borderStyle = 'solid';
                     controlUI.style.borderWidth = '1px';
                     controlUI.style.borderColor = 'white';
                     controlUI.style.height = '28px';
                     controlUI.style.marginTop = '5px';
                     controlUI.style.cursor = 'pointer';
                     controlUI.style.textAlign = 'center';
                     controlUI.title = 'Click to center map on your location';
                     controlDiv.appendChild(controlUI);

                     // Set CSS for the control text
                     var controlText = document.createElement('div');
                     controlText.style.fontFamily = 'Arial,sans-serif';
                     controlText.style.fontSize = '10px';
                     controlText.style.color = 'white';
                     controlText.style.paddingLeft = '10px';
                     controlText.style.paddingRight = '10px';
                     controlText.style.marginTop = '8px';
                     controlText.innerHTML = 'Click to choose your current location';
                     controlUI.appendChild(controlText);

                     // Setup the click event listeners to geolocate user
                     google.maps.event.addDomListener(controlUI, 'click', geolocate);
                 }
                 
                 function geolocate() {

                     if (navigator.geolocation) {

                         navigator.geolocation.getCurrentPosition(function (position) {


                             var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

                             var geocoder = geocoder = new google.maps.Geocoder();
                             geocoder.geocode({ 'latLng': pos }, function (results, status) {
                                 if (status == google.maps.GeocoderStatus.OK) {
                                     if (results[0]) {
                                         //alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                                         document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                                     }
                                 }
                             });

                             // Create a marker and center map on user location
                             //marker = new google.maps.Marker({
                             //    position: pos,
                             //    draggable: true,
                             //    animation: google.maps.Animation.DROP,
                             //    map: map
                             //});
                             document.getElementById("<%=Lat.ClientID%>").value=position.coords.latitude;
                             document.getElementById("<%=Lng.ClientID%>").value=position.coords.longitude;

                             placeMarker(pos);

                             map.setCenter(pos);
                         });
                     }
                 }

                /////Ends Getting Current Location...

                 google.maps.event.addListener(map, 'click', function (e) {
                     //alert("Latitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                     // getting the exact location details

                     var latlng = new google.maps.LatLng(e.latLng.lat(), e.latLng.lng());
                     var geocoder = geocoder = new google.maps.Geocoder();
                     geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                         if (status == google.maps.GeocoderStatus.OK) {
                             if (results[1]) {
                                 alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                                 document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                               }
                           }
                       });

                       placeMarker(e.latLng);
                       document.getElementById("<%=Lat.ClientID%>").value=e.latLng.lat();
                     document.getElementById("<%=Lng.ClientID%>").value=e.latLng.lng();
                   });
                // Insert the marker on the map
                   function placeMarker(location) {

                       deleteOverlays();

                       var marker = new google.maps.Marker({
                           position: location, 
                           map: map,
                           icon:icon1,
                       });

                       allMarkers.push(marker);


                       google.maps.event.addListener(marker, 'click', function (e) {
                           //alert("Latitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                           // getting the exact location details

                           var latlng = new google.maps.LatLng(e.latLng.lat(), e.latLng.lng());
                           var geocoder = geocoder = new google.maps.Geocoder();
                           geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                               if (status == google.maps.GeocoderStatus.OK) {
                                   if (results[1]) {
                                       alert("Make sure this your exact location ?"+"\r\nAddress: " + results[0].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
                                       document.getElementById("<%=HosAddress.ClientID%>").value=results[0].formatted_address;
                                 }
                             }
                         });

                         placeMarker(e.latLng);
                         document.getElementById("<%=Lat.ClientID%>").value=e.latLng.lat();
                         document.getElementById("<%=Lng.ClientID%>").value=e.latLng.lng();
                     });
                 }
                // Delete all previuos markers
                 function deleteOverlays() {
                     if (allMarkers) {
                         for (i in allMarkers) {
                             allMarkers[i].setMap(null);
                         }
                         allMarkers.length = 0;
                     }
                 }
             }
             
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <%-- <% if (Session["Language"].ToString() == "Auto")
          {%>--%>
    <div class="container-fluid">
       <%-- <%}
    else
    { %>
         <div class="container-fluid" dir="rtl">
        <%} %>--%>
        <div style="margin-top: 1%">


            <div class="row">
                <div class="col-md-12 col-lg-12 col-sm-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h2 class="box-title">
                                <asp:Label ID="Label1" runat="server" Text="Must choose your Hospital location. Because Hakkeem users find your hospital by this location." meta:resourcekey="Label1Resource1"></asp:Label></h2>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <%-- <% if (Session["Language"].ToString() == "Auto")
                                                 {%>--%>
                                            <input id="txtsearch" class="apply" type="text" placeholder="Enter Search Place.." onkeydown="return (event.keyCode!=13);">
                                          <%--  <%}
    else
    { %>
                                                <input id="txtsearch" class="apply" type="text" placeholder="أدخل مكان البحث" onkeydown="return (event.keyCode!=13);">
                                            <%} %>--%>
                                            <div id="dvMap" style="width: 100%; height: 400px;">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="box-footer">
                            <input type="hidden" id="Lat" runat="server" />
                            <input type="hidden" id="Lng" runat="server" />
                            <input type="hidden" id="HosAddress" runat="server" />
                            <%-- <% if (Session["Language"].ToString() == "Auto")
                                 {%>--%>
                            <div>
                               <%-- <%}
    else
    { %>
                                <div class="pull-left">
                                    <%} %>--%>
                            <asp:Button ID="BtnChangeLocation" runat="server" Text="Change Location" CssClass="btn btn-primary" Visible="False" OnClick="BtnChangeLocation_Click" meta:resourcekey="BtnChangeLocationResource1" />
                            <asp:Button ID="BtnSetLocation" runat="server" Text="Set Location" CssClass="btn btn-primary" Visible="False" OnClick="BtnSetLocation_Click" meta:resourcekey="BtnSetLocationResource1" />
                        </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>




        <!-- Bootstrap Modal Dialog -->
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <%-- <% if (Session["Language"].ToString() == "Auto")
                                     {%>--%>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                               <%-- <%}
    else
    { %>
                                  <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <%} %>--%>
                                <h4 class="modal-title">
                                    <asp:Label ID="lblModalTitle" runat="server" Text="Hakkeem" meta:resourcekey="lblModalTitleResource1"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="Label7" runat="server" Text="Label" meta:resourcekey="Label7Resource1"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Close" OnClick="Button1_Click" UseSubmitBehavior="False" meta:resourcekey="Button1Resource1" />
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
              <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

        <!-- Latest compiled JavaScript -->
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
      
        <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />
</asp:Content>

