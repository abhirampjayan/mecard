<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="testnewmaster.aspx.cs" Inherits="User_testnewmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        #table1 {
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
            margin-top: 5.5cm;
            height: 15cm;
            Z-index: 10000;
            overflow-y: scroll;
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

        #notavailable {
            margin-top: 50px;
            margin-left: 10px;
        }

        #notavailable {
            font-weight: normal;
        }

        #marker1 {
            font-size: 20px;
            color: black;
        }

        @media screen and (max-width:1220px) {
            #label1 {
                margin-top: 6cm;
            }
        }

        @media screen and (max-width:1080px) {
            #label1 {
                margin-top: 6.2cm;
            }

            #adress {
                margin-left: 5%;
                margin-top: -2%;
            }
        }

        @media screen and (max-width:995px) {
            #label1 {
                margin-top: 8cm;
                width: 100%;
            }

            #adress {
                margin-left: 5%;
                margin-top: -2%;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:950px) {
            #label1 {
                margin-top: 8cm;
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
                margin-left: 5%;
                margin-top: -2%;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:930px) {
            #label1 {
                margin-top: 9cm;
                width: 100%;
            }

            #marker {
                margin-left: 1%;
            }

            #doctorname {
                margin-left: 9%;
            }

            #speciality1 {
                margin-left: 9%;
            }

            #star {
                margin-left: 9%;
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
        }

        @media screen and (max-width:930px) {
            #label1 {
                margin-top: 13.7cm;
                width: 100%;
            }
        }

        @media screen and (max-width:770px) {
            #dvMap {
                display: none;
            }

            #label1 {
                margin-top: 13.7cm;
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
                margin-left: 8%;
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
                margin-left: 7%;
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
                margin-left: 6%;
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
                margin-top: 13.7cm;
                height: 650px;
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
                margin-top: -9%;
            }

            #comment {
                margin-left: 12%;
            }

            #doctornameo {
                margin-left: 45%;
                margin-top: -37%;
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
                margin-top: 13%;
            }

            #checkdiv {
                display: none;
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
                margin-top: 13.7cm;
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
                margin-top: -9%;
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
                margin-top: 13.7cm;
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
                margin-top: 13%;
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
                margin-top: 14.5cm;
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
                margin-top: 13%;
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
                margin-top: 16cm;
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
                margin-top: 18cm;
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
                margin-top: 13%;
            }

            #checkdiv {
                display: none;
            }
        }
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
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- REPUTED  -->
    <section id="doctor_reputed">
        <div class="container">
            <div class="row">
                <div class="inner-button">
                    <div class="col-md-3 col-sm-6">
                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                        <asp:HiddenField ID="hdnId" runat="server" />
                        <asp:TextBox ID="txtContactsSearch" placeholder="Doctor name" CssClass="searchtxt" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <asp:DropDownList ID="DropDownList1" CssClass="searchdrop" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-sm-6">
                        <asp:DropDownList ID="DropDownList2" CssClass="searchdrop" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-6">
                        <asp:Button ID="Button1" runat="server" CssClass="searchbtn" Text="Find" />
                    </div>

                </div>
            </div>
        </div>
    </section>
    <!-- /.END REPUTED  -->
    <!-- SKILLS -->
    <section id="skills">
        <div class="container">
            <div class="row">

                <div class="col-md-3">
                    <asp:DropDownList ID="Illness" CssClass="searchsmalldrop" runat="server"></asp:DropDownList>

                </div>
                <div class="col-md-2">
                    <asp:Button ID="Button2" CssClass="searchsmallbtn" runat="server" Text="Any gender" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="Button3" CssClass="searchsmallbtn" runat="server" Text="Male" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="Button4" CssClass="searchsmallbtn" runat="server" Text="Female" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="Button5" runat="server" CssClass="searchsmallbtn" Text="Hospital doctor" />
                </div>
            </div>
        </div>
    </section>
    <!-- /.END SKILLS -->
    <section class="content" style="background-color:#ecf0f5">

   

    <div class="row">
        <div class="col-md-8">

            <%-- <asp:DataList ID="DataList1" CssClass="table" runat="server" BorderStyle="None">
                                <ItemTemplate>
                                    <div class="form-group" id="item" onmouseover="hover('<%#Eval("d_id") %>')" onmouseout="out('<%# Eval("d_id") %>')">
                                        <div class="box box-solid">
                                            <div class="box-body">
                                                <div class="col-md-3 col-sm-6">
                                                    <div class="docimage">
                                                        <asp:Image ID="Image1" CssClass="img-circle img-rounded img-thumbnail img-responsive docimg" runat="server" ImageUrl='<%# Bind("d_photo") %>' />
                                                        <asp:Label ID="hakkeemid" Visible="false" runat="server" Text='<%# Bind("d_hakkimid") %>'></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-8">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="docname" CssClass="docname" runat="server" Text='<%# Bind("d_name") %>'></asp:LinkButton>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("d_specialties") %>'></asp:Label>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="rating" runat="server" Text="Label"></asp:Label>
                                                        <p style="font-size: smaller; font-style: italic">
                                                            <asp:Label ID="latest_comment" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("d_address") %>'></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-8"></div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>--%>
        </div>

        <div class="col-md-4" data-spy="affix" data-offset-top="205" <%--data-offset-bottom="100"--%>>
            <div id="dvMap" style="height: 15cm;">
            </div>
        </div>
    </div>

     </section>


    <!-- Bootstrap Modal Dialog -->
    <%--<div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text="Hakkeem"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <h1>Doctors not available...!</h1>
                        </div>
                        <div class="modal-footer">

                            <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary" data-dismiss="modal" UseSubmitBehavior="false" Text="Search again" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>--%>
</asp:Content>

