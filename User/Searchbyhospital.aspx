<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="Searchbyhospital.aspx.cs" Inherits="User_Search_by_hospital" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .cnt{
            margin-top:1.4cm;
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
            position: absolute;
            right: 0;
            bottom: 0px;
        }

        #doctorname {
            color: #000000;
            font-weight: bold;
            font-size: 20px;
            font-family: Tahoma,Verdana,Segoe,sans-serif;
            text-decoration: none;
        }

        #ardoctorname {
            color: #000000;
            font-weight: bold;
            font-size: 20px;
            font-family: Tahoma,Verdana,Segoe,sans-serif;
            text-decoration: none;
        }

        #doctorname:hover {
            color: #4aa9af;
            text-decoration: none;
        }

        #ardoctorname:hover {
            color: #4aa9af;
            text-decoration: none;
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

        #armarker {
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
            /*margin-left: 10%;*/
            margin-right: 5%;
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

        #armarker1 {
            font-size: 20px;
            color: black;
        }

        #hospitalname {
            color: #000000;
            font-weight: normal;
            font-size: 15px;
            padding-top: 1px;
            font-family: sharp-sans-semibold,Arial,sans-serif;
        }

            #hospitalname:hover {
                text-decoration: none;
            }

        #height1 {
            height: 750px;
            z-index: 1;
            overflow-y: scroll;
        }

        #label1 {
            LEFT: 1.5%;
            POSITION: absolute;
            TOP: 23.5%;
            height: 26cm;
            Z-index: 1;
            overflow-y: scroll;
            margin-bottom:1cm;
        }

        #label2 {
            /*LEFT: 1.5%;*/
            POSITION: absolute;
            TOP: 23.5%;
            height: 26cm;
            Z-index: 1;
            overflow-y: scroll;
            right:0;
             margin-bottom:1cm;
        }


        ::-webkit-scrollbar {
            display: none;
        }

        @media screen and (max-width:1300px) {
            #label1 {
                margin-top: 22%;
            }
        }

        @media screen and (max-width:1220px) {
            #label1 {
                margin-top: 25%;
            }
        }

        @media screen and (max-width:1080px) {
            #label1 {
                margin-top: 25%;
            }

            /*#adress {
                margin-left: 5%;
                margin-top: -2%;
            }*/

            /*#aradress {
                margin-left: 5%;
                margin-top: -2%;
            }*/
        }

        @media screen and (max-width:995px) {
            #label1 {
                margin-top: 25%;
                width: 100%;
            }

            /*#adress {
                margin-left: 5%;
                margin-top: -2%;
            }

            #aradress {
                margin-left: 5%;
                margin-top: -2%;
            }*/

            #mapdiv {
                display: none;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:950px) {
            #label1 {
                margin-top: 25%;
                width: 100%;
            }

            /*#doctorname {
                margin-left: 9%;
            }
             #ardoctorname {
                margin-left: 9%;
            }

            #speciality1 {
                margin-left: 9%;
            }*/

            #latestcomment {
                margin-left: 9%;
            }

            #star {
                margin-left: 9%;
            }

            /*#adress {
                margin-left: 5%;
                margin-top: -2%;
            }

            #aradress {
                margin-left: 5%;
                margin-top: -2%;
            }*/

            #mapdiv {
                display: none;
            }

            #checkdiv {
                display: none;
            }

            #height1 {
                height: 1650px;
                z-index: 1;
            }
        }

        @media screen and (max-width:930px) {
            #label1 {
                margin-top: 25%;
                width: 100%;
            }

            /*#marker {
                margin-left: 1%;
            }
             #armarker {
                margin-left: 1%;
            }*/

            /*#doctorname {
                margin-left: 9%;
            }
             #ardoctorname {
                margin-left: 9%;
            }
            #hospitalname {
                margin-left: 9%;
            }*/

            /*#speciality1 {
                margin-left: 9%;
            }*/

            #star {
                margin-left: 9%;
            }

            #latestcomment {
                margin-left: 9%;
            }

            /*#adress {
                margin-left: 3%;
                margin-top: -2%;
            }

            #aradress {
                margin-left: 3%;
                margin-top: -2%;
            }*/

            #notavailable {
                margin-top: 8%;
            }

            #mapdiv {
                display: none;
            }

            #checkdiv {
                display: none;
            }
        }

        @media screen and (max-width:770px) {
            #label1 {
                margin-top: 30%;
                width: 100%;
            }

            /*#marker {
                margin-left: 1%;
            }
            
            #armarker {
                margin-left: 1%;
            }
            #doctorname {
                margin-left: 8%;
            }
              #ardoctorname {
                margin-left: 8%;
            }
            #hospitalname {
                margin-left: 8%;
            }*/

            /*#speciality1 {
                margin-left: 8%;
            }*/

            #star {
                margin-left: 8%;
            }

            #latestcomment {
                margin-left: 8%;
            }

            /*#adress {
                margin-left: 3%;
                margin-top: -2%;
            }

            #aradress {
                margin-left: 3%;
                margin-top: -2%;
            }*/

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

            #mapdiv {
                display: none;
            }

            #height1 {
                height: 1800px;
                z-index: 1;
            }
        }

        @media screen and (max-width:630px) {
            #label1 {
                margin-top: 35%;
                border-top: 1px solid #ddd;
            }

            /*#marker {
                margin-left: 1%;
            }
             #armarker {
                margin-left: 1%;
            }

            #doctorname {
                margin-left: 7%;
            }
              #ardoctorname {
                margin-left: 7%;
            }
            #hospitalname {
                margin-left: 7%;
            }*/

            /*#speciality1 {
                margin-left: 7%;
            }*/

            #star {
                margin-left: 7%;
            }

            #latestcomment {
                margin-left: 7%;
            }

            #sliderp {
                padding-left: 75px;
            }

            /*#adress {
                margin-left: 3%;
                margin-top: -2%;
            }*/

            /*#aradress {
                margin-left: 3%;
                margin-top: -2%;
            }*/

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

            #mapdiv {
                display: none;
            }
        }

        @media screen and (max-width:590px) {
            #sliderp {
                padding-left: 20px;
            }

            #checkdiv {
                display: none;
            }

            #mapdiv {
                display: none;
            }
        }

        @media screen and (max-width:560px) {
            #sliderp {
                padding-left: 52px;
            }

            #checkdiv {
                display: none;
            }

            #mapdiv {
                display: none;
            }
        }

        @media screen and (max-width:530px) {
            #label1 {
                margin-top: 40%;
                border-top: 1px solid #ddd;
            }

            #label2 {
                margin-top: 40%;
                border-top: 1px solid #ddd;
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

            /*#marker {
                margin-left: 1%;
            }
             #armarker {
                margin-left: 1%;
            }
            #hospitalname {
                margin-left: 6%;
            }

            #doctorname {
                margin-left: 6%;
            }
             #ardoctorname {
                margin-left: 6%;
            }*/
            /*#speciality1 {
                margin-left: 6%;
            }*/

            #star {
                margin-left: 6%;
            }

            #latestcomment {
                margin-left: 6%;
            }

            /*#adress {
                margin-left: 4%;
                margin-top: -3%;
            }

            #aradress {
                margin-left: 4%;
                margin-top: -3%;
            }*/

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

            #mapdiv {
                display: none;
            }

            #height1 {
                height: 2000px;
                z-index: 1;
            }
        }

        @media screen and (max-width:430px) {
            #label1 {
                margin-top: 45%;
                /*border-top: 1px solid #ddd;*/
            }

            #label2 {
                margin-top: 45%;
                /*border-top: 1px solid #ddd;*/
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

            /*#adress {
                margin-left: 9%;
                margin-top: -9%;
            }

            #aradress {
                margin-left: 9%;
                margin-top: -9%;
            }

            #hospitalname {
                margin-left: 45%;
                margin-top: -37%;
            }*/

            /*#speciality1 {
                margin-left: 45%;
            }*/


            /*#doctornameo {
                margin-left: 45%;
                margin-top: -37%;
            }
             #ardoctornameo {
                margin-left: 45%;
                margin-top: -37%;
            }
            #adress {
                margin-left: 7%;
            }

            #aradress {
                margin-left: 7%;
            }*/

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

            #mapdiv {
                display: none;
            }
        }

        @media screen and (max-width:414px) {
            #label1 {
                margin-top: 4cm;
                left: 0px;
                width: 100%;
                right: 0px;
            }

            #label2 {
                margin-top: 4cm;
                left: 0;
                width: 100%;
                right: 0px;
            }
            /*#HyperLink8 {
                font-weight: bold;
                position: relative;
                top: -38px;
            }*/

            #adress {
                margin-left: 15%;
                margin-top: -26px;
            }

            #marker1 {
                margin-top: 42px;
                margin-left: 35px;
            }

            #doctornameo {
                margin-top: -137px;
                margin-left: 11px;
            }

            #hospitalname {
                margin-left: 11px;
            }

            #speciality1 {
                margin-left: 11px;
            }

            #ardoctorname {
                margin-right: 125px;
                position: relative;
                top: -114px;
            }

            #arhospitalname {
                margin-right: 125px;
                position: relative;
                top: -123px;
            }

            #armarker1 {
                margin-top: -90px;
                /*position: absolute;*/
                 margin-left: 45px;
            }

            #aradress {
                margin-top: -94px;
                margin-left: 35px;
                /*position: absolute;*/
            }
            .user-panel{
                    margin-left: 6cm;
                    /*float:right;*/
            }
        }

        @media screen and (max-width:410px) {
            #doctornameo {
                margin-left: 40%;
                margin-top: -37%;
            }
             #ardoctornameo {
                margin-left: 40%;
                margin-top: -37%;
            }
            #hospitalname {
                margin-left: 45%;
                margin-top: -37%;
            }
             #arhospitalname {
                margin-left: 45%;
                margin-top: -37%;
            }
            #speciality1 {
                margin-left: 45%;
            }

            #adress {
                margin-left: 9%;
            }

            #aradress {
                margin-left: 9%;
            }

            #checkdiv {
                display: none;
            }

            #mapdiv {
                display: none;
            }

            #height1 {
                height: 2100px;
                z-index: 1;
            }
        }

        @media screen and (max-width:390px) {

            #label1 {
                margin-top: 2.5cm;
            }

            #label2 {
                margin-top: 2.5cm;
            }

            #sliderp {
                padding-left: 20px;
            }

            #doctornameo {
                margin-left: 40%;
                margin-top: -40%;
            }
             #ardoctornameo {
                margin-left: 40%;
                margin-top: -40%;
            }
            #speciality1 {
                margin-left: 45%;
            }

            #hospitalname {
                margin-left: 45%;
                margin-top: -40%;
            }
              #arhospitalname {
                margin-left: 45%;
                margin-top: -40%;
            }
            #adress {
                margin-left: 9%;
            }

            #aradress {
                margin-left: 9%;
            }

            #checkdiv {
                display: none;
            }

            #mapdiv {
                display: none;
            }
        }

        @media screen and (max-width:375px) {
            #label1 {
                margin-top: 4cm;
                left: -11px;
                width: 107%;
            }

            #label2 {
                margin-top: 4cm;
                width: 108%;
                left: -14px;
            }

         
            #adress {
                margin-left: 15%;
                margin-top: -26px;
            }

            #marker1 {
                margin-top: 42px;
                margin-left: 35px;
            }

            #doctornameo {
                margin-top: -137px;
                margin-left: 11px;
            }

            #hospitalname {
                margin-left: 11px;
            }

            #speciality1 {
                margin-left: 11px;
            }

            #ardoctorname {
                /*margin-right: 125px;*/
                /* margin-right: 125px; */
                position: relative;
                top: -15px;
                margin-left: -130px;
            }

            #arhospitalname {
                /* margin-right: -225px; */
                position: relative;
                top: -15px;
                margin-left: 12px;
            }

            #armarker1 {
                margin-top: 12px;
                /*position: absolute;*/
            }

            #aradress {
               margin-top: -17px;
                /*position: absolute;*/
            }
            .navbar-toggle {
                color: #4aa9af;
                border: solid 1px #4aa9af;
                margin: 4px 26px 4px 4px;
                padding: 8px 8px;
            }
        }

        @media screen and (max-width:360px) {
            #label1 {
                margin-top: 5.5cm;
                font-size: 8px;
                border-top: 1px solid #ddd;
            }

            #label2 {
                margin-top: 5.5cm;
                font-size: 8px;
                border-top: 1px solid #ddd;
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
                margin-left: 40%;
                margin-top: -40%;
            }
             #ardoctornameo {
                margin-left: 40%;
                margin-top: -40%;
            }
            #speciality1 {
                margin-left: 45%;
            }

            #hospitalname {
                margin-left: 45%;
                margin-top: -40%;
            }
             #arhospitalname {
                margin-left: 45%;
                margin-top: -40%;
            }
            #adress {
                margin-left: 9%;
                margin-top: 2%;
            }

            #aradress {
                margin-left: 9%;
                margin-top: 2%;
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

            #mapdiv {
                display: none;
            }

            #height1 {
                height: 2150px;
                z-index: 1;
            }
        }




        @media screen and (max-width:330px) {
            #label1 {
                margin-top: 7cm;
                border-top: 1px solid #ddd;
            }

            #label2 {
                margin-top: 7cm;
                border-top: 1px solid #ddd;
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
                margin-top: 2%;
            }

            #aradress {
                margin-left: 9%;
                margin-top: 2%;
            }


            #doctornameo {
                margin-left: 45%;
                margin-top: -37%;
            }
             #ardoctornameo {
                margin-left: 45%;
                margin-top: -37%;
            }
            #speciality1 {
                margin-left: 45%;
            }

            #hospitalname {
                margin-left: 40%;
                margin-top: -40%;
            }
             #arhospitalname {
                margin-left: 40%;
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

            #mapdiv {
                display: none;
            }

            #height1 {
                height: 2200px;
                z-index: 1;
            }

            .layout-top-nav .main-header > .navbar {
                margin-left: 0;
                height: 129px;
            }
        }

        @media screen and (max-width:320px) {
            #label1 {
                margin-top: 4.7cm;
            }

            #label2 {
                margin-top: 4.6cm;
                /* right: -25px; */
                width: 109%;
            }



            #adress {
                margin-left: 15%;
                margin-top: -29px;
            }

            #marker1 {
                margin-top: 42px;
                margin-left: 29px;
            }

            #doctornameo {
                margin-top: -137px;
                margin-left: 133px;
            }

            #hospitalname {
                margin-left: 133px;
            }

            #speciality1 {
                margin-left: 133px;
            }

            #ardoctorname {
                margin-left: -7%;
                margin-top: 0;
                /* margin-right: -89px; */
                margin-right: 0;
            }

            #arhospitalname {
                /* margin-right: 68px; */
                position: relative;
                top: -20px;
            }

            #armarker1 {
                margin-top: -10px;
                /*position: absolute;*/
            }

            #aradress {
                  margin-top: -23px;
                /*position: absolute;*/
            }
            .user-panel {
                margin-left:0;
                margin-right:0;
            }
            /*.aruser-panel{
                margin-right:0;
            }*/
        }



        @media screen and (max-width:300px) {
            #doctornameo {
                margin-left: 45%;
                margin-top: -37%;
            }
             #ardoctornameo {
                margin-left: 45%;
                margin-top: -37%;
            }
            #hospitalname {
                margin-left: 40%;
                margin-top: -40%;
            }
               #arhospitalname {
                margin-left: 40%;
                margin-top: -40%;
            }
            #speciality1 {
                margin-left: 45%;
            }

            #adress {
                margin-left: 9%;
            }

            #aradress {
                margin-left: 9%;
            }

            #checkdiv {
                display: none;
            }

            #mapdiv {
                display: none;
            }
        }

        @media screen and (max-width:260px) {
            #label1 {
                margin-top: 85%;
                border-top: 1px solid #ddd;
            }

            #label2 {
                margin-top: 85%;
                border-top: 1px solid #ddd;
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
                margin-left: 40%;
                margin-top: -40%;
            }
             #ardoctornameo {
                margin-left: 40%;
                margin-top: -40%;
            }
            #speciality1 {
                margin-left: 45%;
            }

            #hospitalname {
                margin-left: 45%;
                margin-top: -40%;
            }
             #arhospitalname {
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

            #aradress {
                margin-left: 9%;
                margin-top: -9%;
            }

            #checkdiv {
                display: none;
            }

            #mapdiv {
                display: none;
            }

            #height1 {
                height: 2300px;
                z-index: 1;
            }
        }

        @media screen and (max-width:220px) {
            #label1 {
                margin-top: 95%;
            }

            #label2 {
                margin-top: 95%;
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
                margin-left: 40%;
                margin-top: -40%;
            }
             #ardoctorname {
                font-size: 16px;
            }

            #ardoctornameo {
                margin-left: 40%;
                margin-top: -40%;
            }
            #speciality1 {
                margin-left: 45%;
            }

            #hospitalname {
                margin-left: 45%;
                margin-top: -40%;
            }
              #arhospitalname {
                margin-left: 45%;
                margin-top: -40%;
            }
            #adress {
                margin-left: 9%;
                margin-top: -9%;
            }

            #aradress {
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

            #mapdiv {
                display: none;
            }

            #height1 {
                height: 2350px;
                z-index: 1;
            }
        }

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
    </style>


    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCNo1WjdpDfX7wvziSy2HeS0d-axd0Yv50&callback=initMap" type="text/javascript"></script>

    <script type="text/javascript">

        var markers = [
        <asp:Repeater ID="rptMarkers" runat="server">
        <ItemTemplate>
                 {
                     "id":'<%# Eval("id")%>',
                     "hd_id": '<%# Eval("hd_id") %>',
                     "lat": '<%# Eval("Latitude") %>',
                     "lng": '<%# Eval("Longitude") %>',
                     "h_name":'<%# Eval("h_name") %>',
                     "address":'<%# Eval("h_address") %>',
                     "photo":'<%# Eval("h_photo") %>',
                     "h_id":'<%# Eval("h_id")%>',
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
        window.onload = function () {
            
            mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                //center: new google.maps.LatLng('24.301039','44.122252'),
                zoom: 13,
                scrollwheel: false,
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
                    id:data.h_id,
                    icon:icon1,
                    lat:data.lat,
                    longi:data.lng,
                });
                
                
                allMarkers.push(marker);

                 
                (function (marker, data) {
                    google.maps.event.addListener(marker, "mouseover", function (e) {
                        var imgpath=(data.photo).slice(2);
                        if(imgpath=="")
                        {
                            imgpath="../doctorimages/doctor.png";
                        }
                        var infoContent='<div id="iw-container">'+' <div id="image" >' +
                                         '<img src="'+imgpath+'" alt="'+data.name +'" id="imgs" >'+
                                          '</div>'+
                                           '<div id="content">'+
                                        '<div id="name">' + data.h_name + '</div>'+
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

                    //google.maps.event.addListener(marker, "click", function (e) {
                    //    window.location='Hospital doctor availability.aspx?docid='+data.hd_id+'&Lat='+data.lat +'&Long='+data.lng+'';
                    //});

                    function markertime(marker){

                    }
                })(marker, data);
            }
            
        }

        function hover(id){
           
            for ( var i = 0; i< allMarkers.length; i++) {
                if (id == allMarkers[i].id) {
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

    <%--Ends map script  --%>
    <script src="js/jquery.min.js"></script>
    <link href="css/jquery.bxslider.css" rel="stylesheet" type="text/css" media="all" />
    <script src="js/jquery.bxslider.js"></script>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->

    <script type="text/javascript">
        $('.bxslider').bxSlider({
            mode: 'fade',
            captions: true,
            pager: false,
            infiniteLoop: false,
            hideControlOnEnd: true
        });
    </script>
        <script type="text/javascript">

        //function CheckParts() {
        //    __doPostBack('', '');
        //};
        $(document).ready(function () {
           
            $("#<%=txtZipCodeSearch.ClientID %>").autocomplete({
              
             
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
                    $( "#<%=txtZipCodeSearch.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


            //

        }); 
    </script>

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function CheckParts() {
            __doPostBack('', '');
        };
        $(document).ready(function () {
            $("#<%=txtContactsSearch.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetCustomers1") %>',
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
                    $( "#<%=txtContactsSearch.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


        

        }); 
    </script>




    <link href="SearchStyle/style.css" rel="stylesheet" />
    <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
  <%--  <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">

        <asp:HiddenField ID="hfCustomerId" runat="server" />
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <div class="row">
            <div class="cnt">
            <div class="box box-primary" style="background-color: #4aa9af">
                <div class="box-header"></div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                         <%--   <%if (Session["Speciality"].ToString() == "Auto")
                                { %>--%>
                            <div class="col-md-6">
                               <%-- <%}
                                else
                                { %>
                                <div class="col-md-6 col-md-push-6">
                                    <%} %>--%>

                                   <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                        { %>--%>
                                    <div class="col-md-7 o1">
                                       <%-- <%}
                                        else
                                        { %>
                                        <div class="col-md-6 o1 col-md-push-6">
                                            <%} %>--%>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtContactsSearch" CssClass="searchtxt" placeholder="Doctor name or Specialty" runat="server" AutoPostBack="True" OnTextChanged="txtContactsSearch_TextChanged" meta:resourcekey="txtContactsSearchResource1"></asp:TextBox>


                                            </div>
                                        </div>
                                    <%--    <%if (Session["Speciality"].ToString() == "Auto")
                                            { %>--%>
                                        <div class="col-md-5 o2">
                                           <%-- <%}
                                            else
                                            { %>
                                            <div class="col-md-6 o2 col-md-pull-6">
                                                <%} %>--%>
                                                <div class="form-group">
                                                     <asp:HiddenField ID="hfcityId" runat="server" />
                                                    <asp:TextBox ID="txtZipCodeSearch" CssClass="searchtxt" placeholder="City" runat="server" AutoPostBack="True" OnTextChanged="txtZipCodeSearch_TextChanged" meta:resourcekey="txtZipCodeSearchResource1"></asp:TextBox>
                                                    <%--<ajaxToolkit:AutoCompleteExtender runat="server" ServiceMethod="SearchCity" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" BehaviorID="txtZipCodeSearch_AutoCompleteExtender" TargetControlID="txtZipCodeSearch" ID="txtZipCodeSearch_AutoCompleteExtender"></ajaxToolkit:AutoCompleteExtender>--%>
                                                </div>
                                            </div>
                                        </div>

                                       <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                            { %>--%>
                                        <div class="col-md-6">
                                            <%--<%}
                                            else
                                            { %>
                                            <div class="col-md-6 col-md-pull-6">

                                                <%} %>--%>
                                               <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                                    { %>--%>
                                                <div class="col-md-8 o3">
                                                   <%-- <%}
                                                    else
                                                    { %>
                                                    <div class="col-md-6 o3 col-md-push-6">
                                                        <%} %>--%>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtLangSearch" CssClass="searchtxt" placeholder="Hospital name" runat="server" AutoPostBack="True" OnTextChanged="txtLangSearch_TextChanged" meta:resourcekey="txtLangSearchResource1"></asp:TextBox>
                                                            <%--   <ajaxToolkit:AutoCompleteExtender runat="server" MinimumPrefixLength="2"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" ServiceMethod="SearchLanguage" ServicePath="" DelimiterCharacters="" BehaviorID="txtLangSearch_AutoCompleteExtender" TargetControlID="txtLangSearch" ID="txtLangSearch_AutoCompleteExtender">
                                </ajaxToolkit:AutoCompleteExtender>--%>
                                                        </div>
                                                    </div>
                                                    <%--<%if (Session["Speciality"].ToString() == "Auto")
                                                        { %>--%>
                                                    <div class="col-md-4 o4">
                                                       <%-- <%}
                                                        else
                                                        { %>
                                                        <div class="col-md-6 o4 col-md-pull-6">
                                                            <%} %>--%>
                                                            <asp:Button ID="Button4" CssClass="searchbtn" runat="server" Text="Find" OnClick="Button4_Click" meta:resourcekey="Button4Resource1" />

                                                            <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Search doctors" OnClick="Button1_Click" Visible="False" meta:resourcekey="Button1Resource1" />


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                   <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                        { %>--%>

                                    <div class="col-md-8">
                                    </div>

                                    <div class="col-md-4" id="mapdiv" data-spy="affix" data-offset-top="205" <%--data-offset-bottom="30"--%>>

                                        <div id="dvMap" style="height: 26cm; margin-top: -1%"></div>
                                    </div>
                                   <%-- <%}
                                    else
                                    { %>
                                    <style type="text/css">
                                        @media (max-width: 991px) {


                                            .ordering {
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
                                    <div class="col-md-8 pull-right">
                                    </div>

                                    <div class="col-md-4 pull-left" id="mapdiv" data-spy="affix" data-offset-top="205" <%--data-offset-bottom="30"--%>>

                                        <%--<div id="dvMap" style="height: 26cm; margin-top: -1%"></div>
                                    </div>--%>
                                   <%-- <%} %>--%>
                                </div>

                            </div>
                            <style>
                                .affix {
                                    top: 7px;
                                    position: relative;
                                    height: auto;
                                }
                                /*#dvMap {
            height: 600px;
        }*/
                                /*section#copy {
                                    padding: 20px 0;
                                    color: #fff;
                                    background: #313131;
                                    margin-top: 12cm;
                                    position: absolute;
                                    width: 100%;
                                }*/
                            </style>

                            <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
                            <link href="SearchStyle/style.css" rel="stylesheet" />
                            <script src="../Design/plugins/iCheck/icheck.min.js"></script>
                            <script>
                                $(function () {
                                    $('input').iCheck({
                                        checkboxClass: 'icheckbox_square-red',
                                        radioClass: 'iradio_square-blue',
                                        //increaseArea: '20%' // optional
                                    });
                                });
                            </script>


                            <%--Ends map script  --%>
                            <script src="js/jquery.min.js"></script>
                            <link href="css/jquery.bxslider.css" rel="stylesheet" type="text/css" media="all" />
                            <script src="js/jquery.bxslider.js"></script>
                            <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->

                            <script type="text/javascript">
                                $('.bxslider').bxSlider({
                                    mode: 'fade',
                                    captions: true,
                                    pager: false,
                                    infiniteLoop: false,
                                    hideControlOnEnd: true
                                });
                            </script>




                         <%--   <%if (Session["Speciality"].ToString() == "Auto")
                                { %>--%>
                            <%--<style type="text/css">
                                @media screen and (max-width:375px) {
                                    .skin-green .main-header .navbar .nav > li > a {
                                        position: relative;
                                        /*top:47px;*/
                                        top: 6px;
                                    }

                                    #LinkButton3 {
                                        margin-top: -23px;
                                    }
                                }
                                @media screen and (max-width:320px) {
                                    #LinkButton3 {
                                        margin-top: -7px;
                                    }

                                    .layout-top-nav .main-header > .navbar {
                                        height: 98px;
                                    }
                                }
                            </style>--%>
                           <%-- <%}
                            else
                            { %>--%>
                         <%--   <style type="text/css">
                                @media screen and (max-width:414px) {
                                      .skin-green .main-header .navbar .nav > li > a {
                                        position: relative;
                                        top: 47px;
                                        /*top:6px;*/
                                    }
                                }
                                @media screen and (max-width:375px) {
                                    .skin-green .main-header .navbar .nav > li > a {
                                        position: relative;
                                        top: 47px;
                                        /*top:6px;*/
                                    }
                                }
                                @media screen and (max-width:320px) {
                                       #LinkButton3 {
                                        /*margin-top: -2px;*/
                                    }
                                         #HyperLink8 {
                                       
                                        margin-top: -6px;
                                    }
                                    #LinkButton4 {
                                        margin-top: -38px;
                                    }
                                    #HyperLink9{
                                            margin-top: -37px;
                                    }
                                      .layout-top-nav .main-header > .navbar {
                                        height: 100px;
                                    }
                                }

                            </style>--%>

                           <%-- <%} %>--%>







                            <style type="text/css">
                                /*section#copy {
                                    width: 100%;
                                    padding: 20px 0;
                                    position: absolute;
                                    z-index: 1000000;
                                    color: #fff;
                                    background: #313131;
                                   
                                    margin-top: 0px;
                                }*/
                                @media screen and (max-width:414px) {
                                     /*section#copy {
                                        width: 100%;
                                        padding: 20px 0;
                                        position: absolute;
                                        z-index: 1000000;
                                        color: #fff;
                                        background: #313131;
                                       
                                        bottom: 0;
                                    
                                        margin-bottom: -18.1cm;
                                    }*/
                                }
                                @media screen and (max-width:375px) {
             
                                    /*section#copy {
                                        width: 100%;
                                        padding: 20px 0;
                                        position: absolute;
                                        z-index: 1000000;
                                        color: #fff;
                                        background: #313131;
                                     
                                        bottom: 0;
                                       
                                        margin-bottom: -20.1cm;
                                    }*/
                                }

                                @media screen and (max-width:320px) {

                                    .layout-top-nav .main-header > .navbar {
                                        margin-left: 0;
                                       
                                    }

                                    /*section#copy {
                                        width: 100%;
                                        padding: 20px 0;
                                        position: absolute;
                                        z-index: 1000000;
                                        color: #fff;
                                        background: #313131;
                                       
                                        margin-top: 0px;
                                        margin-bottom: -22cm;
                                    }*/
                                    .user-panel {
                                        margin-left: 0;
                                        margin-right: 0;
                                    }
                                }
                            </style>
                          <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>

