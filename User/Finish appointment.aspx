<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" CodeFile="Finish appointment.aspx.cs" Inherits="User_Finish_appointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <%--   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-bottom:1cm;margin-top:1.5cm;">
    <%-- <%if (Session["Speciality"].ToString() == "Auto")
         { %>--%>
      <section class="content">
         <%-- <%}
    else
    { %>
           <section class="content" dir="rtl">
          <%} %>--%>
    <div class="container">
        <div style="margin-top: 10%">
            <div class="col-md-12">
                <div class="box box-solid">
                    <div class="box-header"></div>
                    <div class="box-body">
                         <%-- <%if (Session["Speciality"].ToString() == "Auto")
                              { %>--%>
                        <h1 Style="line-height:39px;">Thank you, your appointment is fixed on 
                           <%-- <%}
    else
    { %>
                             <h1>شكرا لك، يتم إصلاح موعدك على 
                            <%} %>--%>
                            <asp:Label ID="Label1" runat="server" ></asp:Label>
                        </h1>

                    </div>
                    <div class="box-footer">
                       <%-- <%if (Session["Speciality"].ToString() == "Auto")
                            { %>--%>
                        <h4>Hakkeem Team!</h4>
                       <%-- <%}
    else
    { %>
                         <h4>فريق حكيم!</h4>
                        <%} %>--%>
                    </div>
                </div>
            </div>
        </div>

    </div>
          </section>
          </div>
    <%--        <style type="text/css">
        #copy {
            width: 100%;
            padding: 20px 0;
            position: absolute;
            z-index: 1000000;
            color: #fff;
            background: #313131;
            /* margin-top: 6cm; */
            bottom: 0;
        }
    </style>--%>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>

