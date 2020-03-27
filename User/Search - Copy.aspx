<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" Culture="en-US" AutoEventWireup="true" CodeFile="Search - Copy.aspx.cs" Inherits="User_AbhinSearch" meta:resourcekey="PageResource2" UICulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        #table1 {
            border-collapse: separate;
            border-spacing: 0px 10px;
            border-color: #ecf0f5;
        }

        #table2 {
            border-collapse: separate;
            border-spacing: 0px 10px;
            border-color: #ecf0f5;
        }



        #buttontime {
            background-color: #4aa9af;
            border-color: #4aa9af;
            color: white;
        }

        #btnover:hover #buttontime {
            background-color: lightgrey;
            border-color: lightgrey;
            color: #4aa9af;
        }

            #btnover:hover #buttontime:hover {
                background-color: #4aa9af;
                border-color: #4aa9af;
                color: #ecf0f5;
            }

        #iw-container {
            margin-bottom: 10px;
            height: 100px;
            width: 230px;
        }

        #doctorname {
            color: black;
            font-weight: bold;
            font-size: 20px;
            font-family: Tahoma,Verdana,Segoe,sans-serif;
        }

            #doctorname:hover {
                color: #4aa9af;
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

        #name {
            font-size: 15px;
            font-weight: bold;
        }

        #speciality {
            font-weight: bold;
        }

        #adress1 {
            margin-top: 10px;
        }

        #content {
            margin-left: 37%;
            float: right;
            margin-top: -25%;
        }


        #item:hover {
            cursor: pointer;
        }

        .affix {
            top: 0px;
            position: fixed;
            right: 0px;
            bottom: 0px;
        }

        #testbg {
            background: #ff4b7d;
        }

        #label1 {
            LEFT: 2%;
            POSITION: absolute;
            margin-top: 5.2cm;
            height: 15cm;
            Z-index: 10000;
            overflow-y: scroll;
            margin-bottom: 1cm;
        }

        #label2 {
            /*LEFT: 2%;*/
            POSITION: absolute;
            margin-top: 5.2cm;
            height: 15cm;
            Z-index: 10000;
            overflow-y: scroll;
            right: 0;
            direction: rtl;
            margin-bottom: 1cm;
        }

        ::-webkit-scrollbar {
            display: none;
        }

        ::-webkit-scrollbar {
            -webkit-appearance: none;
        }

        html {
            -ms-overflow-style: -ms-autohiding-scrollbar;
        }

        #buttontime {
            text-align: center;
            margin-top: 5%;
            width: 64px;
        }

            #buttontime:hover {
                background-color: #4aa9af;
            }

        #datedisplay {
            margin-bottom: 13px;
            font-size: 12px;
            color: black;
        }

        #sliderp {
            padding-left: 42px;
            margin-left: 8%;
        }

        #marker {
            margin-left: -11%;
        }

        #armarker {
            /*margin-left:50%;*/
            margin-top: 17%;
        }

        #speciality1 {
            color: #000000;
            font-weight: normal;
            font-size: 13px;
            padding-top: 1px;
            font-family: sharp-sans-semibold,Arial,sans-serif;
        }

        #latestcomment {
            color: #000000;
            font-weight: normal;
            font-size: 13px;
            padding-top: 1px;
            font-family: sharp-sans-semibold,Arial,sans-serif;
            font-style: italic;
        }

            #latestcomment:hover {
                color: red;
            }

        #adress {
            color: #000000;
            font-weight: normal;
            margin-left: 10%;
            margin-top: -10%;
            font-size: 13px;
            padding-top: 2%;
            font-family: BlinkMacSystemFont,-apple-system,Segoe UI,Roboto,Helvetica,Arial,sans-serif;
        }

        #aradress {
            color: #000000;
            font-weight: normal;
            /*margin-left:3%;*/
            margin-top: -11%;
            font-size: 13px;
            /*padding-top: 2%;*/
            font-family: BlinkMacSystemFont,-apple-system,Segoe UI,Roboto,Helvetica,Arial,sans-serif;
        }

        #notavailable {
            margin-top: 15%;
            margin-left: 2%;
        }

        #notavailable {
            font-weight: normal;
        }

        #marker1 {
            font-size: 20px;
            color: black;
        }

        #armarker1 {
            font-size: 20px;
            color: black;
            /*margin-left:-2%;*/
            /*margin-top:0.1cm;*/
            margin-right: -10%;
        }

        @media screen and (max-width:1220px) {
            #label1 {
                margin-top: 6cm;
            }

            #label2 {
                margin-top: 6cm;
            }
        }


        @media screen and (max-width:1080px) {
            #label1 {
                margin-top: 6.2cm;
            }

            #label2 {
                margin-top: 6.2cm;
            }

            #adress {
                margin-left: 5%;
                margin-top: -2%;
            }

            #doctor_reputed {
                margin-top: 0.7cm;
            }
        }

        @media screen and (max-width:995px) {
            #label1 {
                margin-top: 6cm;
                width: 100%;
            }

            #label2 {
                margin-top: 6cm;
                width: 100%;
            }

            .docimg {
                width: 100px;
            }


            #adress {
                margin-left: 5%;
                margin-top: -2%;
            }

            #checkdiv {
                display: none;
            }

            #marker1 {
                position: relative;
                top: 30px;
            }

            #armarker1 {
                font-size: 20px;
                color: black;
                /*margin-left:-2%;*/
                /*margin-top:0.1cm;*/
                margin-right: 1%;
            }

            #aradress {
                color: #000000;
                font-weight: normal;
                /*margin-left:3%;*/
                margin-top: -11%;
                font-size: 13px;
                /*padding-top: 2%;*/
                font-family: BlinkMacSystemFont,-apple-system,Segoe UI,Roboto,Helvetica,Arial,sans-serif;
                margin-right: 29px;
            }
        }

        @media screen and (max-width:950px) {
            #label1 {
                margin-top: 11cm;
                width: 100%;
            }

            #label2 {
                margin-top: 11cm;
                width: 100%;
            }

            #doctorname {
                margin-left: 9%;
            }

            #speciality1 {
                margin-left: 9%;
            }

            #latestcomment {
                margin-left: 9%;
            }

            #star {
                margin-left: 9%;
            }

            #adress {
                margin-left: 4.5cm;
                margin-top: -2%;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:930px) {
            #label1 {
                margin-top: 11.5cm;
                width: 100%;
            }

            #label2 {
                margin-top: 11.5cm;
                width: 100%;
            }

            #marker {
                margin-left: 2.3cm;
            }

            #doctorname {
                margin-left: 9%;
            }

            #speciality1 {
                margin-left: 9%;
            }

            #star {
                /*margin-left: 9%;*/
            }

            #latestcomment {
                margin-left: 9%;
            }

            #adress {
                margin-left: 3%;
                margin-top: -2%;
            }

            #notavailable {
                margin-top: 8%;
            }

            #checkdiv {
                display: none;
            }

            .bx-next {
                margin-left: 5cm;
            }
        }



        @media screen and (max-width:770px) {
            #dvMap {
                display: none;
            }

            #label1 {
                margin-top: 12.5cm;
                width: 100%;
            }

            #label2 {
                margin-top: 12.5cm;
                width: 100%;
            }

            #marker {
                margin-left: 1%;
            }

            #doctorname {
                margin-left: 8%;
            }

            #speciality1 {
                margin-left: 8%;
            }

            #star {
                /*margin-left: 8%;*/
            }

            #latestcomment {
                margin-left: 8%;
            }

            #adress {
                margin-left: 3%;
                margin-top: -2%;
            }

            #available1 {
                font-size: 10px;
                margin-top: 11%;
            }

            #notavailable {
                margin-top: 10%;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:630px) {
            #label1 {
                margin-top: 13.7cm;
                height: 640px;
            }

            #label2 {
                margin-top: 13.7cm;
                height: 640px;
            }

            #marker {
                margin-left: 1%;
            }

            #doctorname {
                margin-left: 7%;
            }

            #speciality1 {
                margin-left: 7%;
            }

            #star {
                /*margin-left: 7%;*/
            }

            #latestcomment {
                margin-left: 7%;
            }

            #sliderp {
                padding-left: 75px;
            }

            #adress {
                margin-left: 3%;
                margin-top: -2%;
            }

            #available1 {
                font-size: 10px;
                margin-top: -8%;
            }

            #notavailable {
                margin-top: 10%;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:590px) {
            #label1 {
                margin-top: 13.7cm;
                height: 640px;
            }

            #label2 {
                margin-top: 13.7cm;
                height: 640px;
            }

            #sliderp {
                padding-left: 62px;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:560px) {
            #label1 {
                margin-top: 13.7cm;
                height: 640px;
            }

            #label2 {
                margin-top: 13.7cm;
                height: 640px;
            }

            #sliderp {
                padding-left: 52px;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:530px) {
            #label1 {
                margin-top: 13.7cm;
                height: 640px;
            }

            #label2 {
                margin-top: 13.7cm;
                height: 640px;
            }

            #buttontime {
                width: 62px;
                font-size: 12px;
            }

            #datedisplay {
                font-size: 12px;
            }

            #sliderp {
                padding-left: 46px;
            }

            #marker {
                margin-left: 1%;
            }

            #doctorname {
                margin-left: 6%;
            }

            #speciality1 {
                margin-left: 6%;
            }

            #star {
                /*margin-left: 6%;*/
            }

            #latestcomment {
                margin-left: 6%;
            }

            #adress {
                margin-left: 4%;
                margin-top: -3%;
            }

            #available1 {
                font-size: 12px;
                margin-top: -9%;
            }

            #notavailable {
                margin-top: 10%;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:430px) {
            #topdiv {
                border-bottom: 0.5px solid lightgrey;
            }

            #label1 {
                margin-top: 12.5cm;
                height: 650px;
            }

            #label2 {
                margin-top: 12.5cm;
                height: 640px;
            }

            #table1 {
                border-collapse: separate;
                border-spacing: 0px 10px;
                border-color: #ecf0f5;
            }

            #buttontime {
                width: 58px;
                font-size: 11px;
            }

            #datedisplay {
                font-size: 11px;
            }

            #sliderp {
                padding-left: 28px;
            }

            #adress {
                margin-left: 9%;
                margin-top: 9%;
                font-style: normal;
            }

            #comment {
                margin-left: 12%;
            }

            #doctornameo {
                margin-left: 45%;
                margin-top: -34%;
            }

            #doctornameoar {
                /*margin-left: 45%;
                margin-top: -37%;*/
            }

            #adress {
                margin-left: 7%;
                margin-top: -5%;
            }

            #available1 {
                font-size: 10px;
                margin-top: -9%;
            }

            #notavailable {
                margin-top: 16%;
            }

            #checkdiv {
                display: none;
            }

            #doctor_reputed {
                padding-top: 1cm;
            }
        }

        @media screen and (max-width:410px) {
            #doctornameo {
                margin-left: 45%;
                margin-top: -37%;
            }

            #adress {
                margin-left: 9%;
                margin-top: -9%;
            }

            #checkdiv {
                display: none;
            }

            #hieghta {
                height: 1600px;
            }

            #comment {
                margin-left: 12%;
            }
        }

        @media screen and (max-width:390px) {
            #topdiv {
                border-bottom: 0.5px solid lightgrey;
            }

            #label1 {
                margin-top: 12.5cm;
            }

            #label2 {
                margin-top: 12.5cm;
            }

            #sliderp {
                padding-left: 20px;
            }

            #doctornameo {
                margin-left: 45%;
                margin-top: -40%;
            }

            #adress {
                margin-left: 9%;
                margin-top: 0%;
            }

            #checkdiv {
                display: none;
            }

            #comment {
                margin-left: 12%;
            }
        }

        @media screen and (max-width:360px) {
            #topdiv {
                border-bottom: 0.5px solid lightgrey;
            }

            #label1 {
                margin-top: 12.5cm;
                font-size: 8px;
            }

            #label2 {
                margin-top: 12.5cm;
                font-size: 8px;
            }

            #table1 {
                border-collapse: separate;
                border-spacing: 0px 10px;
                border-color: #ecf0f5;
            }

            #buttontime {
                width: 54px;
                font-size: 10px;
            }

            #datedisplay {
                font-size: 10px;
            }

            #sliderp {
                padding-left: 13px;
            }


            #doctornameo {
                margin-left: 45%;
                margin-top: -40%;
            }

            #adress {
                margin-left: 9%;
                margin-top: -9%;
            }

            #available1 {
                font-size: 10px;
                margin-top: -9%;
            }

            #notavailable {
                margin-top: 16%;
            }

            #checkdiv {
                display: none;
            }

            #comment {
                margin-left: 12%;
            }
        }

        @media screen and (max-width:330px) {
            #label1 {
                margin-top: 12.5cm;
            }

            #label2 {
                margin-top: 12.5cm;
            }

            #table1 {
                border-collapse: separate;
                border-spacing: 0px 10px;
                border-color: #ecf0f5;
            }

            #buttontime {
                width: 46px;
                font-size: 8px;
            }

            #datedisplay {
                font-size: 8px;
            }

            #sliderp {
                padding-left: 13px;
            }

            #adress {
                margin-left: 9%;
                margin-top: -9%;
            }



            #doctornameo {
                margin-left: 45%;
                margin-top: -40%;
            }

            #available1 {
                font-size: 9px;
                margin-top: -9%;
            }

            #notavailable {
                margin-top: 16%;
            }

            #checkdiv {
                display: none;
            }

            #comment {
                margin-left: 12%;
            }
        }

        @media screen and (max-width:300px) {
            #doctornameo {
                margin-left: 45%;
                margin-top: -40%;
            }

            #adress {
                margin-left: 9%;
                margin-top: -9%;
            }

            #checkdiv {
                display: none;
            }

            #comment {
                margin-left: 12%;
            }
        }

        @media screen and (max-width:260px) {
            #label1 {
                margin-top: 12.5cm;
            }

            #label2 {
                margin-top: 12.5cm;
            }


            #buttontime {
                width: 43px;
                font-size: 7px;
            }

            #datedisplay {
                font-size: 7px;
            }

            #sliderp {
                padding-left: 13px;
            }

            #doctornameo {
                margin-left: 45%;
                margin-top: -40%;
            }


            #available1 {
                font-size: 8px;
                margin-top: -9%;
            }

            #notavailable {
                margin-top: 1%;
            }

            #adress {
                margin-left: 9%;
                margin-top: -9%;
            }

            #checkdiv {
                display: none;
            }

            #comment {
                margin-left: 12%;
            }
        }

        @media screen and (max-width:220px) {
            #label1 {
                margin-top: 12cm;
            }

            #label2 {
                margin-top: 12.5cm;
            }

            #buttontime {
                width: 52px;
                font-size: 8px;
            }

            #datedisplay {
                font-size: 8px;
            }

            #sliderp {
                padding-left: 2px;
            }

            #doctorname {
                font-size: 16px;
            }

            #doctornameo {
                margin-left: 45%;
                margin-top: -40%;
            }

            #adress {
                margin-left: 9%;
                margin-top: -9%;
            }


            #available1 {
                font-size: 7px;
                margin-top: -9%;
            }

            #notavailable {
                margin-top: 16%;
            }

            #checkdiv {
                display: none;
            }
        }
        /*div.dd_chk_select div#caption{
            top:0.2cm;
        }*/
    </style>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCNo1WjdpDfX7wvziSy2HeS0d-axd0Yv50&callback=initMap"
        type="text/javascript"></script>



    <script type="text/javascript">

        var markers = [
        <asp:Repeater ID="rptMarkers" runat="server">
        <ItemTemplate>
                 {
                     "id":'<%# Eval("id")%>',
                    
                     "name": '<%# Eval("Name") %>',
                     "lat": '<%# Eval("Latitude") %>',
                     "lng": '<%# Eval("Longitude") %>',
                     "speciality":'<%# Eval("d_specialties") %>',
                     "d_hakkimid":'<%# Eval("d_hakkimid") %>',
                     "address":'<%# Eval("d_address") %>',
                     "photo":'<%# Eval("d_photo") %>',
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
        var icon2='mapicons/add-location-hover.png';
        var icon1='mapicons/add-location-point.png';
        var map="";
        var mapOptions;
        window.onload =function () {
            mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                //center: new google.maps.LatLng('24.301039','44.122252'),
                zoom: 13,
                scrollwheel: false,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: false,
                mapTypeControlOptions: {
                    style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                    position: google.maps.ControlPosition.BOTTOM_LEFT,
                },
                zoomControl: true,
                zoomControlOptions: {
                    //position: google.maps.ControlPosition.LEFT_TOP
                    position: google.maps.ControlPosition.LEFT_TOP
                },
                scaleControl:false,
                streetViewControl: false,
                streetViewControlOptions: {
                    position: google.maps.ControlPosition.LEFT_TOP
                },
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
          
            var infoWindow = new google.maps.InfoWindow();
            map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            for (i = 0; i < markers.length; i++) {
                var data = markers[i]
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    //title: data.name,
                    id:data.id,
                    icon:icon1,
                    lat:data.lat,
                    longi:data.lng,
                });
                
                
                allMarkers.push(marker);

                 
                (function (marker, data) {
                    google.maps.event.addListener(marker, "mouseover", function (e) {
                        var imgpath=(data.photo).slice(2);
                        var infoContent='<div id="iw-container">'+' <div id="image" >' +
                                         '<img src="Clinic/../../'+imgpath+'" alt="'+data.name +'" id="imgs" >'+
                                          '</div>'+
                                           '<div id="content">'+
                                        '<div id="name">' + data.name + '</div>'+
                                         '<div id="speciality">' + data.speciality + '</div>'+
                                        '<div id="adress1">' + data.address + '</div>'+
                                           '</div>'+
                                        '</div>';
                        infoWindow.setContent(infoContent);
                        infoWindow.open(map, marker);
                    });

                    // Zoom to 9 when clicking on marker
                    google.maps.event.addListener(marker,'mouseover',function() {
                        //map.setZoom(14);
                        map.setCenter(marker.getPosition());
                    });

                    google.maps.event.addListener(marker, "mouseout", function (e) {
                        infoWindow.close();
                        map.setZoom(13);
                    });

                    google.maps.event.addListener(marker, "click", function (e) {
                        window.location='doctoravailability.aspx?docid1='+data.id+'&Lat='+data.lat +'&Long='+data.lng+'';
                    });
                    google.maps.event.addListener(map, 'dragend', function() {
                       <%-- if(document.getElementById("<%= CheckBox1.ClientID %>").checked)
                        {
                            showVisibleMarkers();
                        }--%>
                    });
                    function showVisibleMarkers() {
                        var bounds = map.getBounds(),
                            count = 0;
                        var str="";    
                        for (var i = 0; i < allMarkers.length; i++) {
                            var marker = allMarkers[i],
                                infoPanel = $('.info-' + (i+1) ); // array indexes start at zero, but not our class names :)
                                           
                            if(bounds.contains(marker.getPosition())===true) {
                               
                                //alert(allMarkers[i].id);

                                //infoPanel.show();
                                //count++;
                              
                                str=str+','+allMarkers[i].id;
                             

                            }
                            else {
                                infoPanel.hide();
                            }
                        }
                        //alert(count);
                        //  $('#infos h2 span').html(count);

                        window.location='search.aspx?did='+str+'';


                    }
                    function markertime(marker){

                    }

                  


                })(marker, data);
            }
            
        }

        function hover(id){
            //  var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions); 
            for ( var i = 0; i< allMarkers.length; i++) {
                if (id == allMarkers[i].id) {
                    // 
                   
                   
                    allMarkers[i].setIcon(icon2); 
                    //map.setZoom(14);
                    //function clickroute(lati,long) {
                    var latLng = new google.maps.LatLng(allMarkers[i].lat, allMarkers[i].longi); //Makes a latlng
                    map.panTo(latLng); //Make map global
                    //}
                
                    //  map.allMarkers[i].setZoom(14);

                    // allMarkers[i].maps.setZoom(14);
                 
                    break;
                }
               
            }
          





        }

        function out(id){
            for ( var i = 0; i< allMarkers.length; i++) {
                if (id == allMarkers[i].id) {
                    map.setZoom(13);
                    allMarkers[i].setIcon(icon1); 
                   
                    break;
                }
            }
        }
         
    </script>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="js/jquery.min.js"></script>
    <link href="css/jquery.bxslider.css" rel="stylesheet" type="text/css" media="all" />
    <script src="js/jquery.bxslider.js"></script>
    <script type="text/javascript">
        $('.bxslider').bxSlider({
            mode: 'fade',
            captions: true,
            pager: false,
            infiniteLoop: false,
            hideControlOnEnd: true
        });
    </script>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="../css/jquery.autocomplete.css" rel="stylesheet" />
    <script src="../js/jquery.autocomplete.js"></script>


    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    <%
        if (Session["Speciality"].ToString() != "Auto")
        {%>

    <link rel="stylesheet" type="text/css" href="searchstyle/css/bootstrap1.min.css" />
    <%
        }
    %>




    <script type="text/javascript">
         
      
        $(document).ready(function () {


          
            $("#<%=txtContactsSearch.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetCustomers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        dir:"rtl",
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
                    $("#<%=hfCustomerId.ClientID %>").val(i.item.val);
                    $( "#<%=txtContactsSearch.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


            //

        }); 
    </script>
    <script type="text/javascript">

        //function CheckParts() {
        //    __doPostBack('', '');
        //};
        $(document).ready(function () {
            $("#<%=txtContactsSearch1.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetCustomers") %>',
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
                    $("#<%=hfCustomerId.ClientID %>").val(i.item.val);
                    $( "#<%=txtContactsSearch1.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


            //

        }); 
    </script>


    <%--      <link href="../css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#<%=txtContactsSearch.ClientID%>").autocomplete("Search.ashx", {
                
                width: '20%',
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

            $("#<%=txtContactsSearch1.ClientID%>").autocomplete("Search.ashx", {
                width: '20%',
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
    </script>--%>


    <link href="css/tipped.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/tipped.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            Tipped.create('.function', function(element) {
                return {
                    title: $(element).data('title'),
                    content: $(element).data('content')
                };
            }, {
                skin: 'light',
                

            });
        });
    </script>
    <style type="text/css">
        .dd_chk_select {
            height: 40px !important;
            text-align: center;
            border-radius: 5px;
        }
    </style>
    <link href="../css/sweetalert.css" rel="stylesheet" />
 <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/jquery.MultiFile.js" type="text/javascript"></script>
    <%-- <link href="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/css/jquery-countryselector.css" rel="stylesheet" />
    <link href="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/css/jquery-countryselector.min.css" rel="stylesheet" />--%>
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css">
    <%--  <script src="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/js/jquery.countrySelector.js"></script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 

    <!-- REPUTED  -->
    <section id="doctor_reputed">
        <div class="container">
            <div class="row">
                <%--<asp:DropDownCheckBoxes ID="DropDownCheckBoxes2" runat="server" Width="180px" UseSelectAllNode="false">
        <Style SelectBoxWidth="195" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="90" />
        <Items>
            <asp:ListItem Text="Mango" Value="1"></asp:ListItem>
            <asp:ListItem Text="Apple" Value="2"></asp:ListItem>
            <asp:ListItem Text="Banana" Value="3"></asp:ListItem>
        </Items>
    </asp:DropDownCheckBoxes>
    &nbsp;
    <asp:ExtendedRequiredFieldValidator ID="ExtendedRequiredFieldValidator1" runat="server"
        ControlToValidate="DropDownCheckBoxes1" ErrorMessage="Required" ForeColor="Red"></asp:ExtendedRequiredFieldValidator>--%>
                <%if (Session["Speciality"].ToString() == "Auto")
                    { %>
                <div class="inner-button">
                    <div class="col-md-3 col-sm-6">
                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                        <asp:HiddenField ID="hdnId" runat="server" />
                        <asp:TextBox ID="txtContactsSearch" placeholder="Doctor name" CssClass="searchtxt" runat="server" meta:resourcekey="txtContactsSearchResource2"></asp:TextBox>
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <%--<asp:DropDownList ID="DropDownList1" CssClass="searchdrop" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1"></asp:DropDownList>--%>
                        <%--<asp:DropDownList Visible="false" ID="DropDownList1" runat="server" CssClass="searchdrop" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                        </asp:DropDownList>--%>

 
                                            <asp:ListBox ID="DropDownList1" runat="server" SelectionMode="Multiple" CssClass="form-control select2" data-placeholder="Select a State" meta:resourcekey="LsbLanguagesResource1">
                                                <%--<asp:ListItem meta:resourcekey="ListItemResource1" Text="English"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource2" Text="Arabic"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource3" Text="Malayalam"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource4" Text="Hindi"></asp:ListItem>--%>
                                            </asp:ListBox>


                          <script src="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/js/jquery.countrySelector.js"></script>
    <script>$('.tokenize-demo').tokenize2();</script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $("#DdlNationality").countrySelector({ value: 'FRA' });
        $('.multipleInputDynamic').fastselect();
        //});


    </script>
    <script src="../js/app.min.js"></script>

                        <%-- <asp:dropdowncheckboxes ID="DropDownList1" runat="server"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1"
                            AddJQueryReference="True" CssClass="searchdrop" RepeatDirection="Horizontal" UseSelectAllNode="True">
                            <%--<Style SelectBoxWidth="300" DropDownBoxBoxWidth="300" DropDownBoxBoxHeight="84" />--%>

                        <%-- <Texts SelectBoxCaption="--Language--" />
                        </asp:dropdowncheckboxes>--%>

                        <%-- <asp:UpdatePanel ID="updatepanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="TextBoxlanguage" runat="server"></asp:TextBox>
                                <ajaxToolkit:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" Enabled="true" TargetControlID="TextBoxlanguage" DynamicServicePath="" PopupControlID="Panel1" OffsetY="22"></ajaxToolkit:PopupControlExtender>--%>
                        <%--   <asp:PopupControlExtender ID="TextBox1_PopupControlExtender" runat="server" DynamicServicePath=""
                                    Enabled="True" ExtenderControlID="" TargetControlID="TextBox1" PopupControlID="Panel1"
                                    OffsetY="22">
                                </asp:PopupControlExtender>--%>
                        <%-- <asp:Panel ID="Panel1" runat="server" Height="116px" Width="145px" BorderStyle="Solid"
                                    BorderWidth="2px" Direction="LeftToRight" ScrollBars="Auto" BackColor="Red"
                                    Style="display: none">
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True"
                                        >
                                    </asp:CheckBoxList>

                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <asp:HiddenField ID="HiddenField3" runat="server" />
                    </div>
                    <style>
                        div.dd_chk_select div#caption {
                            overflow: scroll;
                            /*height: 16px;*/
                            /*margin-right: 20px;
                            margin-left: 2px;*/
                            text-align: left;
                            vertical-align: middle;
                            /*position: relative;*/
                            top: 0.2cm;
                            /*width: 100%;*/
                            width: 100%;
                            height: 40px;
                            padding: 0 10px;
                            border-radius: 4px;
                            border: 0px;
                            margin-top: 0.05cm;
                            font-size: 14px;
                            font-family: 'Montserrat', sans-serif;
                        }

                        div.dd_chk_drop {
                            background-color: white;
                            border: 1px solid #CCCCCC;
                            text-align: left;
                            z-index: 100000000;
                            left: -1px;
                            top: 1cm;
                            min-width: 100%;
                        }

                            div.dd_chk_drop div#checks {
                                border-style: none;
                                padding: 4px 4px 4px 4px;
                                max-height: 100px;
                                overflow: scroll;
                            }
                    </style>
                    <div class="col-md-3 col-sm-6">
                        <asp:DropDownList ID="DropDownList2" CssClass="searchdrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged1" meta:resourcekey="DropDownList2Resource2"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-6">
                        <asp:Button ID="Button1" runat="server" CssClass="searchbtn" Text="Find" OnClick="Button1_Click1" meta:resourcekey="Button1Resource2" />
                    </div>
                </div>
                <%}
                    else
                    { %>
                <div class="inner-button">
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

                    <div class="col-md-2 col-sm-6 one">
                        <asp:Button ID="Button2" runat="server" CssClass="searchbtn" Text="Find" OnClick="Button1_Click1" meta:resourcekey="Button1Resource2" />
                    </div>
                    <div class="col-md-3 col-sm-6 two">
                        <asp:DropDownList ID="DropDownList3" CssClass="searchdrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged1" meta:resourcekey="DropDownList2Resource2"></asp:DropDownList>
                    </div>
                    <style>
                        div.dd_chk_select div#caption {
                            overflow: scroll;
                            /*height: 16px;*/
                            /*margin-right: 20px;
                            margin-left: 2px;*/
                            text-align: left;
                            vertical-align: middle;
                            /*position: relative;*/
                            top: 0.2cm;
                            /*width: 100%;*/
                            width: 100%;
                            height: 40px;
                            padding: 0 10px;
                            border-radius: 4px;
                            border: 0px;
                            margin-top: 0.05cm;
                            font-size: 14px;
                            font-family: 'Montserrat', sans-serif;
                        }

                        div.dd_chk_drop {
                            background-color: white;
                            border: 1px solid #CCCCCC;
                            text-align: left;
                            z-index: 100000000;
                            left: -1px;
                            top: 1cm;
                            min-width: 100%;
                            /*direction:rtl;*/
                        }

                            div.dd_chk_drop div#checks {
                                border-style: none;
                                padding: 4px 4px 4px 4px;
                                max-height: 100px;
                                overflow: scroll;
                            }
                    </style>
                    <div class="col-md-4 col-sm-6 three">
                        <%--<asp:DropDownList ID="DropDownList1" CssClass="searchdrop" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1"></asp:DropDownList>--%>
                          <asp:ListBox ID="DropDownCheckBoxes1" runat="server" SelectionMode="Multiple" CssClas="form-control select2" data-placeholder="Select a State" meta:resourcekey="LsbLanguagesResource1">
                                                <asp:ListItem meta:resourcekey="ListItemResource1" Text="English"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource2" Text="Arabic"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource3" Text="Malayalam"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource4" Text="Hindi"></asp:ListItem>
                                            </asp:ListBox>



                  <%--      <asp:DropDownList ID="DropDownCheckBoxes1" CssClass="searchdrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">

                            <asp:ListItem>--لغة--</asp:ListItem>
                        </asp:DropDownList>--%>

                        <%--    <asp:dropdowncheckboxes ID="DropDownCheckBoxes1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1"
                            AddJQueryReference="True" CssClass="searchdrop" RepeatDirection="Horizontal" UseButtons="False" UseSelectAllNode="True">
                            <%--<Style SelectBoxWidth="300" DropDownBoxBoxWidth="300" DropDownBoxBoxHeight="84" />--%>
                        <%--   meta:resourcekey="DropDownList1Resource2"--%>
                        <%--      <Texts SelectBoxCaption="--لغة--" />
                        </asp:dropdowncheckboxes>--%>
                    </div>

                    <div class="col-md-3 col-sm-6 four" dir="rtl">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <asp:TextBox ID="txtContactsSearch1" placeholder="اسم الطبيب" CssClass="searchtxt" runat="server" meta:resourcekey="txtContactsSearchResource2"></asp:TextBox>
                    </div>
                </div>
                <%} %>
            </div>
        </div>
    </section>
    <!-- /.END REPUTED  -->
    <!-- SKILLS -->
    <section id="skills">
        <div class="container">

            <%if (Session["Speciality"].ToString() == "Auto")
                { %>
            <div class="row">
                <div class="col-md-3">
                    <asp:DropDownList ID="Illness" CssClass="searchsmalldrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Illness_SelectedIndexChanged1" meta:resourcekey="IllnessResource2"></asp:DropDownList>

                </div>
                <div class="col-md-2">
                    <asp:Button ID="Button3" CssClass="searchsmallbtn" runat="server" Text="Any gender" OnClick="Button3_Click" meta:resourcekey="Button3Resource2" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="Button4" CssClass="searchsmallbtn" runat="server" Text="Male" OnClick="Button4_Click1" meta:resourcekey="Button4Resource2" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="Button5" CssClass="searchsmallbtn" runat="server" Text="Female" OnClick="Button5_Click" meta:resourcekey="Button5Resource2" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="Button6" runat="server" CssClass="searchsmallbtn" Text="Hospital doctor" meta:resourcekey="Button6Resource2" OnClick="Button6_Click1" />
                </div>
            </div>
            <%}
                else
                { %>
            <div class="row">
                <style type="text/css">
                    @media (max-width: 991px) {
                        .row {
                            display: flex;
                            flex-flow: column;
                        }

                        .five {
                            order: 1;
                        }

                        .four {
                            order: 2;
                        }

                        .three {
                            order: 3;
                        }

                        .two {
                            order: 4;
                        }

                        .one {
                            order: 5;
                        }

                        #ordering {
                            display: flex;
                            flex-flow: column;
                        }

                        .o1 {
                            order: 3;
                        }

                        .o2 {
                            order: 2;
                        }

                        .o3 {
                            order: 1;
                        }
                    }
                </style>
                <div class="col-md-3 one">
                    <asp:Button ID="Button7" PostBackUrl="~/User/Searchbyhospital.aspx?l=ar-EG" runat="server" CssClass="searchsmallbtn" Text="Hospital doctor" meta:resourcekey="Button6Resource2" />
                </div>
                <div class="col-md-2 two">
                    <asp:Button ID="Button8" CssClass="searchsmallbtn" runat="server" Text="Female" OnClick="Button5_Click" meta:resourcekey="Button5Resource2" />
                </div>
                <div class="col-md-2 three">
                    <asp:Button ID="Button9" CssClass="searchsmallbtn" runat="server" Text="Male" OnClick="Button4_Click1" meta:resourcekey="Button4Resource2" />
                </div>
                <div class="col-md-2 four">
                    <asp:Button ID="Button10" CssClass="searchsmallbtn" runat="server" Text="Any gender" OnClick="Button3_Click" meta:resourcekey="Button3Resource2" />
                </div>
                <div class="col-md-3 five">
                    <asp:DropDownList ID="Illness1" CssClass="searchsmalldrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Illness_SelectedIndexChanged1" meta:resourcekey="IllnessResource2"></asp:DropDownList>

                </div>
            </div>
            <%} %>
        </div>
    </section>
    <!-- /.END SKILLS -->
    <section class="content" style="background-color: #ecf0f5">
        <div class="row">
            <%if (Session["Speciality"].ToString() == "Auto")
                { %>

            <div class="col-md-8">
            </div>

            <div class="col-md-4" data-spy="affix" data-offset-top="205" <%--data-offset-bottom="100"--%>>
                <div id="dvMap" style="height: 15cm;">
                </div>
            </div>

            <%}
                else
                {%>

            <style type="text/css">
                .affix {
                    top: 0px;
                    position: fixed;
                    /*right: 0px;*/
                    bottom: 0px;
                    left: 0;
                }
            </style>
            <div class="col-md-8 pull-right">
            </div>
            <div class="col-md-4 pull-left" dir="rtl" data-spy="affix" data-offset-top="205" <%--data-offset-bottom="100"--%>>
                <div id="dvMap" style="height: 15cm;">
                </div>
            </div>





            <%   } %>
        </div>
   
    </section>

    <script src="../js/sweetalert.min.js"></script>
    <script>
        $($('.ui-autocomplete-input')[0]).css('width','300px')
   
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../css/jquery.autocomplete.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery.autocomplete.css" rel="stylesheet" />
  
      <!-- Select2 -->
    <link href="../Design/plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="../js/select2.full.min.js"></script>
    <script src="../Design/plugins/select2/select2.full.min.js"></script>
     <script>
         $(function () {
             //Initialize Select2 Elements
             $(".select2").select2();
         });

         </script>
      <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>
